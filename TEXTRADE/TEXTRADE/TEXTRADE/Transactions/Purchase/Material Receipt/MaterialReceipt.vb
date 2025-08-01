
Imports BL
Imports System.IO
Imports System.ComponentModel

Public Class MaterialReceipt

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim GRIDUPLOADDOUBLECLICK, GRIDMTRSDOUBLECLICK As Boolean
    Public EDIT As Boolean          'used for editing
    Public TEMPMATRECNO As Integer     'used for poation no while editing
    Dim TEMPROW As Integer
    Dim TEMPMTRSROW As Integer
    Dim TEMPUPLOADROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer
    Dim PARTYCHALLANNO As String
    Public TEMPPARTYNAME As String
    Dim DT_MTRSDETAILS As New DataTable
    'Dim TEMPDTMTRS As New DataTable
    Dim ALLOWMANUALMATRECNO As Boolean = False
    Public TEMPPRNO As Integer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        FILLCMB()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        EP.Clear()
        LBLCATEGORY.Text = ""
        CHKCONTDESIGNRECD.CheckState = CheckState.Unchecked
        CHKRETURN.CheckState = CheckState.Unchecked

        TXTFROM.Clear()
        TXTTO.Clear()
        TXTGRIDDESC.Clear()
        CMBCHECKSRNO.Items.Clear()
        CMBBALENO.Items.Clear()
        CMBLOTNO.Items.Clear()
        TXTBALPCS.Clear()
        TXTBALMTRS.Clear()
        TXTMATRECNO.Clear()
        MATRECDATE.Text = Now.Date
        tstxtbillno.Clear()
        'cmbGodown.Text = ""
        If USERGODOWN <> "" Then cmbGodown.Text = USERGODOWN Else cmbGodown.Text = ""
        cmbname.Enabled = True
        cmbname.Text = ""
        CMBLOTNO.Text = ""
        CMBLOTNO.Enabled = True

        TXTPROGRAMNO.Clear()

        DT_MTRSDETAILS.Reset()
        DT_MTRSDETAILS.Columns.Add("DSRNO")
        DT_MTRSDETAILS.Columns.Add("DMTRS")
        DT_MTRSDETAILS.Columns.Add("MAINSRNO")

        'TEMPDTMTRS.Reset()
        'TEMPDTMTRS.Columns.Add("DSRNO")
        'TEMPDTMTRS.Columns.Add("DMTRS")
        'TEMPDTMTRS.Columns.Add("MAINSRNO")

        TXTCHALLANNO.Clear()
        CHALLANDATE.Text = Now.Date

        TXTFROMNO.Clear()
        TXTFROMTYPE.Clear()

        TXTCHECKNO.Clear()
        CHECKDATE.Value = Now.Date

        txtsrno.Clear()
        TXTDSRNO.Clear()
        CMBGRIDLOTNO.Text = ""
        If GRIDLOTNO = False Then CMBGRIDLOTNO.Enabled = False
        CMBCHECKSRNO.Text = ""
        CMBBALENO.Text = ""
        cmbitemname.Text = ""
        LBLHSN.Text = ""
        CMBQUALITY.Text = ""
        TXTBALENO.Clear()
        CMBDESIGN.Text = ""
        cmbcolor.Text = ""
        cmbcolor.DropDownStyle = ComboBoxStyle.DropDown
        TXTCUT.Clear()
        TXTWT.Clear()
        txtqty.Text = 1
        If ClientName = "YASHVI" Or ClientName = "BRILLANTO" Or ClientName = "AVIS" Or ClientName = "KEMLINO" Or ClientName = "SOFTAS" Or ClientName = "SHREENAKODA" Or ClientName = "VSTRADERS" Or ClientName = "VALIANT" Or ClientName = "KARAN" Then cmbqtyunit.Text = "LUMP" Else cmbqtyunit.Text = "Pcs"
        If ClientName = "MAHAVIRPOLYCOT" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Then cmbqtyunit.Text = "UNCHECK LUMP"
        TXTMTRS.Clear()
        TXTRATE.Clear()
        CMBPER.Text = "Mtrs"
        TXTAMOUNT.Clear()
        GRIDMATREC.RowCount = 0
        CMBRACK.Text = ""
        CMBSHELF.Text = ""
        CMBYESNO.SelectedIndex = 0

        txtadd.Clear()

        cmbtrans.Text = ""
        txtlrno.Clear()
        lrdate.Value = Now.Date
        txtremarks.Clear()

        txtuploadsrno.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        gridupload.RowCount = 0
        txtimgpath.Clear()
        TXTNEWIMGPATH.Clear()
        TXTFILENAME.Clear()
        PBSoftCopy.ImageLocation = ""


        lbllocked.Visible = False
        PBlock.Visible = False

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        getmaxno()

        LBLTOTALWT.Text = 0
        lbltotalqty.Text = 0
        LBLTOTALMTRS.Text = 0
        LBLTOTALRECDMTRS.Text = 0
        LBLTOTALDIFF.Text = 0
        TXTDIFF.Clear()
        TXTRECDMTRS.Clear()
        TXTRUNNINGBALPCS.Clear()
        TXTRUNNINGBALMTRS.Clear()
        GRIDMTRS.RowCount = 0
        GRIDORDER.RowCount = 0
        CMBPARTYNAME.Text = ""

        TXTDSRNO.Text = 1

        If GRIDMATREC.RowCount > 0 Then
            txtsrno.Text = Val(GRIDMATREC.Rows(GRIDMATREC.RowCount - 1).Cells(0).Value) + 1
        Else
            txtsrno.Text = 1
        End If

        If gridupload.RowCount > 0 Then
            txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
        Else
            txtuploadsrno.Text = 1
        End If

        CMBCHECKSRNO.Enabled = True


        If ALLOWMANUALMATRECNO = True Then
            TXTMATRECNO.ReadOnly = False
            TXTMATRECNO.BackColor = Color.LemonChiffon
        Else
            TXTMATRECNO.ReadOnly = True
            TXTMATRECNO.BackColor = Color.Linen
        End If

        LBLTOTALAMT.Text = 0.0

        CHKMANUALRATE.CheckState = CheckState.Unchecked
        TXTRATE.ReadOnly = True
        GRATE.ReadOnly = True
        TXTPCSNO.Clear()
        GBMTRS1.Visible = False
        GBMTRS.Visible = False
        TXTREFLOTNO.Clear()
        CMBPIECETYPE.Text = "FRESH"
        CMBCMPNAME.Text = ""
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALRECDMTRS.Text = 0.0
            LBLTOTALWT.Text = 0.0
            lbltotalqty.Text = 0
            LBLTOTALDIFF.Text = 0.0
            LBLTOTALAMT.Text = 0.0
            TXTRUNNINGBALPCS.Clear()
            TXTRUNNINGBALMTRS.Clear()

            For Each ROW As DataGridViewRow In GRIDMATREC.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    If ROW.Cells(gcut.Index).EditedFormattedValue > 0 Then ROW.Cells(GMTRS.Index).Value = ROW.Cells(gQty.Index).EditedFormattedValue * ROW.Cells(gcut.Index).EditedFormattedValue
                    ROW.Cells(GDIFF.Index).Value = Format(Val(ROW.Cells(GMTRS.Index).EditedFormattedValue) - Val(ROW.Cells(GRECDMTRS.Index).EditedFormattedValue), "0.00")
                    lbltotalqty.Text = Format(Val(lbltotalqty.Text) + Val(ROW.Cells(gQty.Index).EditedFormattedValue), "0")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALRECDMTRS.Text = Format(Val(LBLTOTALRECDMTRS.Text) + Val(ROW.Cells(GRECDMTRS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALDIFF.Text = Format(Val(LBLTOTALDIFF.Text) + Val(ROW.Cells(GDIFF.Index).EditedFormattedValue), "0.00")
                    If ROW.Cells(GPER.Index).EditedFormattedValue = "Mtrs" Then
                        ROW.Cells(GAMOUNT.Index).Value = Format(Val(ROW.Cells(GRECDMTRS.Index).EditedFormattedValue) * Val(ROW.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                    Else
                        ROW.Cells(GAMOUNT.Index).Value = Format(Val(ROW.Cells(gQty.Index).EditedFormattedValue) * Val(ROW.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                    End If
                    LBLTOTALAMT.Text = Format(Val(LBLTOTALAMT.Text) + Val(ROW.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
                End If
            Next

            TXTRUNNINGBALPCS.Text = Val(TXTBALPCS.Text.Trim) - Val(lbltotalqty.Text.Trim)
            TXTRUNNINGBALMTRS.Text = Val(TXTBALMTRS.Text.Trim) - Val(LBLTOTALRECDMTRS.Text.Trim)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        If ClientName = "SOFTAS" Then MATRECDATE.Focus() Else cmbGodown.Focus()
    End Sub

    Private Sub MATRECDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MATRECDATE.GotFocus
        MATRECDATE.SelectionStart = 0
    End Sub

    Private Sub MATRECDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MATRECDATE.Validating
        Try
            If MATRECDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(MATRECDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    'SAME DATE FOR CHALLANDATE / LRDATE 
                    If ClientName = "DAKSH" Or ClientName = "SHALIBHADRA" Or ClientName = "SOFTAS" Then
                        CHALLANDATE.Text = Convert.ToDateTime(MATRECDATE.Text).Date
                        lrdate.Value = Convert.ToDateTime(MATRECDATE.Text).Date
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHALLANDATE.GotFocus
        CHALLANDATE.SelectionStart = 0
    End Sub

    Private Sub CHALLANDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CHALLANDATE.Validating
        Try
            If CHALLANDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CHALLANDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    'SAME DATE FOR CHALLANDATE / LRDATE 
                    If ClientName = "MOHATUL" Then
                        MATRECDATE.Text = Convert.ToDateTime(CHALLANDATE.Text).Date
                        lrdate.Value = Convert.ToDateTime(CHALLANDATE.Text).Date
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(MATREC_no),0) + 1 ", " MATERIALRECEIPT ", " AND MATREC_cmpid=" & CmpId & " and MATREC_locationid=" & Locationid & " and MATREC_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            TXTMATRECNO.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub txtuploadsrno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtuploadsrno.KeyPress
        enterkeypress(e, Me)
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True

            If (ClientName = "SVS" Or ClientName = "SBA" Or ClientName = "MOHATUL") And TXTCHALLANNO.Text.Trim.Length = 0 Then
                EP.SetError(TXTCHALLANNO, " Please Fill Challan No. ")
                bln = False
            End If


            If UserName <> "Admin" Then
                If CHKMANUALRATE.Checked = True Then
                    TXTRATE.ReadOnly = False
                    EP.SetError(CHKMANUALRATE, " ALLOW MANUAL RATE")
                    CHKMANUALRATE.Checked = False
                End If
            End If

            If cmbname.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, " Please Select Name ")
                bln = False
            End If

            If GRIDLOTNO = False And CMBLOTNO.Text.Trim.Length = 0 Then
                EP.SetError(CMBLOTNO, " Please Select Lot No")
                bln = False
            End If

            If cmbGodown.Text.Trim.Length = 0 Then
                EP.SetError(cmbGodown, " Please Select Godown")
                bln = False
            End If
            'If UserName = "Admin" And CHKMANUALRATE.Checked = True Then
            '    TXTRATE.Enabled = True

            'End If


            'THEY GET TP PCS EVEN AFTER THE LOT GETS COMPLETED
            If lbllocked.Visible = True And UserName <> "Admin" And ClientName <> "MAHAVIRPOLYCOT" Then
                EP.SetError(lbllocked, "Item Used, Item Locked")
                bln = False
            End If


            If GRIDMATREC.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If


            If GRIDORDER.RowCount = 0 And CHKRETURN.CheckState = CheckState.Unchecked And (ClientName = "AVIS" Or ClientName = "YASHVI") Then
                EP.SetError(cmbname, "Select Program Details")
                bln = False
            End If


            'FOR ORDER CHECKING, FIRST REMOVE GDNQTY
            Dim TEMPORDERROWNO As Integer = -1
            Dim TEMPORDERMATCH As Boolean = False
            If GRIDORDER.RowCount > 0 Then

                For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                    ORDROW.Cells(OGDNQTY.Index).Value = 0
                    ORDROW.Cells(OGDNMTRS.Index).Value = 0
                Next





                'FOR SALEORDER ON MTRS
                For Each ROW As DataGridViewRow In GRIDMATREC.Rows

                    Dim GREYRATE As Double = 0.0
                    Dim OVERHEADS As Double = 0.0
                    Dim TRANSPORTRATE As Double = 0.0
                    Dim SHRINKAGEPER As Double = 0.0
                    Dim SHRINKAGERATE As Double = 0.0
                    Dim VALUELOSSPER As Double = 0.0
                    Dim VALUELOSSRATE As Double = 0.0
                    Dim PROGRAMRATE As Double = 0.0
                    Dim FINALRATE As Double = 0.0


                    'GET GREYRATE FROM LOT_VIEW
                    Dim OBJCMN As New ClsCommon
                    Dim DTRATE As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(PURRATE,0) AS GREYRATE", "", " LOT_VIEW ", " AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND LOTNO = '" & ROW.Cells(GLOTNO.Index).Value & "' AND YEARID = " & YearId)
                    If DTRATE.Rows.Count > 0 Then GREYRATE = Val(DTRATE.Rows(0).Item("GREYRATE"))


                    'GET OVERHEADS | SHRINKAGE % | VALUE LOSS %
                    DTRATE = OBJCMN.SEARCH(" TOP 1 ISNULL(ITEM_TRANSRATE,0) + ISNULL(ITEM_CHECKRATE,0) + ISNULL(ITEM_PACKRATE,0) + ISNULL(ITEM_DESIGNRATE,0) AS OVERHEADS, ISNULL(ITEM_SHRINKFROM,0) AS SHRINKAGEPER, ISNULL(ITEM_VALUELOSSPER,0) AS VALUELOSSPER,  ISNULL(ITEM_TRANSRATE,0) AS TRANSPORTCOST ", "", " ITEMMASTER ", " AND ITEMMASTER.ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                    If DTRATE.Rows.Count > 0 Then
                        OVERHEADS = Val(DTRATE.Rows(0).Item("OVERHEADS"))
                        TRANSPORTRATE = Val(DTRATE.Rows(0).Item("TRANSPORTCOST"))
                        SHRINKAGEPER = 100 - Val(DTRATE.Rows(0).Item("SHRINKAGEPER"))

                        SHRINKAGERATE = Format(Val(((GREYRATE + TRANSPORTRATE) / SHRINKAGEPER) * Val(DTRATE.Rows(0).Item("SHRINKAGEPER"))), "0.00")
                        'SHRINKAGERATE = Format(Val((SHRINKAGEPER * GREYRATE) / 100), "0.00")

                        VALUELOSSPER = 100 - Val(DTRATE.Rows(0).Item("VALUELOSSPER"))
                    End If



                    For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                        If ROW.Cells(gitemname.Index).Value = ORDROW.Cells(OITEMNAME.Index).Value And ROW.Cells(GDESIGN.Index).Value = ORDROW.Cells(ODESIGN.Index).Value And ROW.Cells(gcolor.Index).Value = ORDROW.Cells(OCOLOR.Index).Value Then
                            TEMPORDERMATCH = True
                            'IF ITEM / DESIGN / SHADE IS MATCHED BUT THE QTY IS FULL THEN WE NEED TO KEEP THIS ROWNO IN TEMP AND NEED TO CHECK FURTHER ALSO
                            'IF WE GET ANY NEW MATHING THEN WE NEED TO INSERT THERE
                            'IF NO MATCHING IS FOUND IN FURTHER ROWS THEN WE NEED TO ADD QTY IN THIS TEMPROW
                            If Val(ORDROW.Cells(OGDNMTRS.Index).Value) >= Val(ORDROW.Cells(OMTRS.Index).Value) Then
                                TEMPORDERROWNO = ORDROW.Index
                                GoTo CHECKNEXTLINEMTRS
                            End If

                            'GET RATES FROM GRN + OVERHEAD RATES FROM ITEMMASTER + (SHRINKAGE % FROM ITEMMASTER * GREY RATE) + (VALUE LOSS % FROM ITEMMASTER * RATE AFTER SHRINKAGE %)
                            ORDROW.Cells(OGDNMTRS.Index).Value = Val(ORDROW.Cells(OGDNMTRS.Index).Value) + Val(ROW.Cells(GRECDMTRS.Index).Value)

                            'FIRST CALC FINALRATE WITHOUT VALUELOSS
                            PROGRAMRATE = Val(ORDROW.Cells(ORATE.Index).Value)
                            FINALRATE = Format(Val(GREYRATE + OVERHEADS + PROGRAMRATE + SHRINKAGERATE), "0.00")

                            'THEN CALC FINALRATE WITH VALUELOSSRATE
                            VALUELOSSRATE = Format(Val((FINALRATE / VALUELOSSPER) * Val(DTRATE.Rows(0).Item("VALUELOSSPER"))), "0.00")
                            'VALUELOSSRATE = Format(FINALRATE * VALUELOSSPER / 100, "0.00")
                            FINALRATE = Format(Val(GREYRATE + OVERHEADS + PROGRAMRATE + SHRINKAGERATE + VALUELOSSRATE), "0.00")

                            ROW.Cells(GRATE.Index).Value = FINALRATE

                            TEMPORDERROWNO = -1
                            Exit For
CHECKNEXTLINEMTRS:
                        End If
                    Next
                    'IF NO FURTHER MACHING IS FOUND BUT WE HAVE TEMPORDERROWNO THEN ADD VALUE IN THAT ROW
                    If TEMPORDERROWNO >= 0 Then
                        GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNMTRS.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNMTRS.Index).Value) + Val(ROW.Cells(GRECDMTRS.Index).Value)
                        'FIRST CALC FINALRATE WITHOUT VALUELOSS
                        PROGRAMRATE = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(ORATE.Index).Value)
                        FINALRATE = Format(Val(GREYRATE + OVERHEADS + PROGRAMRATE + SHRINKAGERATE), "0.00")

                        'THEN CALC FINALRATE WITH VALUELOSSRATE
                        VALUELOSSRATE = Format(FINALRATE * VALUELOSSPER / 100, "0.00")
                        FINALRATE = Format(Val(GREYRATE + OVERHEADS + PROGRAMRATE + SHRINKAGERATE + VALUELOSSRATE), "0.00")

                        ROW.Cells(GRATE.Index).Value = FINALRATE
                        TEMPORDERROWNO = -1
                    End If
                    If TEMPORDERMATCH = False Then
                        ROW.DefaultCellStyle.BackColor = Color.LightGreen
                        If (ClientName = "AVIS" Or ClientName = "YASHVI") And CHKRETURN.CheckState = CheckState.Unchecked Then
                            EP.SetError(cmbname, "There are Items which are not Present in Selected Program")
                            bln = False
                        Else
                            If MsgBox("There are Items which are not Present in Selected Program, Wish to Proceed", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                EP.SetError(cmbname, "There are Items which are not Present in Selected Program")
                                bln = False
                            End If
                        End If
                    End If
                    TEMPORDERMATCH = False
                Next
            End If
            TOTAL()

            For Each ROW As DataGridViewRow In GRIDMATREC.Rows
                If ROW.Cells(GRECDMTRS.Index).Value = 0 Then
                    EP.SetError(TXTRECDMTRS, "Recd Mtrs Cannot be 0")
                    bln = False
                End If

                If ITEMCOSTCENTRE = True And Val(ROW.Cells(GRATE.Index).Value) = 0 Then
                    EP.SetError(cmbname, "Rate Cannot be 0")
                    bln = False
                End If
            Next


            If TXTCHALLANNO.Text.Trim <> "" Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(TXTCHALLANNO.Text.Trim)) Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    Dim dt As DataTable = objclscommon.SEARCH(" MATREC_challanno, LEDGERS.ACC_cmpname", "", " MATERIALRECEIPT inner join LEDGERS on LEDGERS.ACC_id = MATERIALRECEIPT.MATREC_ledgerid ", " and MATERIALRECEIPT.MATREC_challanno = '" & TXTCHALLANNO.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & cmbname.Text.Trim & "' AND MATREC_YEARID =" & YearId)
                    If dt.Rows.Count > 0 Then
                        EP.SetError(TXTCHALLANNO, "Challan No. Already Exists")
                        bln = False
                    End If
                End If
            End If


            If MATRECDATE.Text = "__/__/____" Then
                EP.SetError(MATRECDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(MATRECDATE.Text) Then
                    EP.SetError(MATRECDATE, "Date not in Accounting Year")
                    bln = False
                End If

                If Convert.ToDateTime(MATRECDATE.Text).Date < DYEINGRECBLOCKDATE.Date Then
                    EP.SetError(MATRECDATE, "Date is Blocked, Please make entries after " & Format(DYEINGRECBLOCKDATE.Date, "dd/MM/yyyy"))
                    bln = False
                End If
            End If

            'CHEKC BARCODE IS PRESENT IN DATABASE OR NOT
            'THIS CODE IS OF NO USE NOW, COZ WE HAVE SAVED BARCODE ON SP
            'If Not CHECKBARCODE() Then
            '    bln = False
            '    EP.SetError(TabControl1, "Barcode already present, Please re-enter data")
            'End If

            TOTAL()
            If ClientName = "SHREENAKODA" And Val(TXTBALPCS.Text.Trim) < Val(lbltotalqty.Text.Trim) Then
                EP.SetError(TXTBALPCS, "Pcs cannot be Greater than Balance Pcs")
                bln = False
            End If


            If ALLOWMANUALMATRECNO = True Then
                If Val(TXTMATRECNO.Text.Trim) <> 0 And EDIT = False Then
                    Dim OBJCMNn As New ClsCommon
                    Dim dttable As DataTable = OBJCMNn.SEARCH(" ISNULL(MATREC_NO,0)  AS MATRECNO", "", " MATERIALRECEIPT ", "  AND MATREC_NO=" & Val(TXTMATRECNO.Text.Trim) & " AND MATREC_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        MsgBox("Dyeing Receipt No Already Exist")
                        bln = False
                    End If
                End If
            End If


            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            If TXTMATRECNO.ReadOnly = False Then
                alParaval.Add(Val(TXTMATRECNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(Format(Convert.ToDateTime(MATRECDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbGodown.Text.Trim)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(CHKCONTDESIGNRECD.CheckState)
            alParaval.Add(CMBLOTNO.Text.Trim)
            alParaval.Add(Val(TXTPROGRAMNO.Text.Trim))
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(TXTCHECKPCS.Text.Trim))
            alParaval.Add(Val(TXTCHECKMTRS.Text.Trim))
            alParaval.Add(TXTCHECKNO.Text.Trim)
            alParaval.Add(CHECKDATE.Value.Date)

            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(txtlrno.Text.Trim)
            alParaval.Add(lrdate.Value)

            alParaval.Add(Val(LBLTOTALWT.Text))
            alParaval.Add(Val(lbltotalqty.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(Val(LBLTOTALRECDMTRS.Text))
            alParaval.Add(Val(LBLTOTALDIFF.Text))

            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)


            Dim gridsrno As String = ""
            Dim GRIDLOTNO As String = ""
            Dim CHECKSRNO As String = ""
            Dim PCSTYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim GRIDDESC As String = ""
            Dim QUALITY As String = ""
            Dim BALENO As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim CUT As String = ""
            Dim WT As String = ""
            Dim qty As String = ""
            Dim qtyunit As String = ""
            Dim MTRS As String = ""
            Dim RECDMTRS As String = ""
            Dim RACK As String = ""
            Dim SHELF As String = ""
            Dim YESNO As String = ""
            Dim DIFF As String = ""
            Dim BARCODE As String = ""
            Dim DONE As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""
            Dim RATE As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""
            Dim PCSNO As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDMATREC.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        GRIDLOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        CHECKSRNO = row.Cells(GCHECKSRNO.Index).Value.ToString
                        PCSTYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                        ITEMNAME = row.Cells(gitemname.Index).Value.ToString.Trim
                        GRIDDESC = row.Cells(GGRIDDESC.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        BALENO = row.Cells(GBALENO.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        CUT = row.Cells(gcut.Index).Value.ToString
                        WT = row.Cells(GWT.Index).Value
                        qty = row.Cells(gQty.Index).Value.ToString
                        qtyunit = row.Cells(gqtyunit.Index).Value.ToString
                        MTRS = row.Cells(GMTRS.Index).Value
                        RECDMTRS = row.Cells(GRECDMTRS.Index).Value
                        RACK = row.Cells(GRACK.Index).Value.ToString
                        SHELF = row.Cells(GSHELF.Index).Value.ToString
                        YESNO = row.Cells(GYESNO.Index).Value.ToString
                        DIFF = row.Cells(GDIFF.Index).Value
                        BARCODE = row.Cells(GBARCODE.Index).Value
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = 1
                        Else
                            DONE = 0
                        End If
                        OUTPCS = row.Cells(GOUTPCS.Index).Value
                        OUTMTRS = row.Cells(GOUTMTRS.Index).Value
                        FROMNO = row.Cells(GFROMNO.Index).Value
                        FROMSRNO = row.Cells(GFROMSRNO.Index).Value
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value
                        RATE = row.Cells(GRATE.Index).Value
                        PER = row.Cells(GPER.Index).Value
                        AMOUNT = row.Cells(GAMOUNT.Index).Value
                        PCSNO = row.Cells(GPCSNO.Index).Value

                    Else

                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        GRIDLOTNO = GRIDLOTNO & "|" & row.Cells(GLOTNO.Index).Value
                        CHECKSRNO = CHECKSRNO & "|" & row.Cells(GCHECKSRNO.Index).Value
                        PCSTYPE = PCSTYPE & "|" & row.Cells(GPIECETYPE.Index).Value
                        ITEMNAME = ITEMNAME & "|" & row.Cells(gitemname.Index).Value.ToString.Trim
                        GRIDDESC = GRIDDESC & "|" & row.Cells(GGRIDDESC.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        CUT = CUT & "|" & row.Cells(gcut.Index).Value
                        WT = WT & "|" & row.Cells(GWT.Index).Value
                        qty = qty & "|" & row.Cells(gQty.Index).Value
                        qtyunit = qtyunit & "|" & row.Cells(gqtyunit.Index).Value
                        MTRS = MTRS & "|" & row.Cells(GMTRS.Index).Value
                        RECDMTRS = RECDMTRS & "|" & row.Cells(GRECDMTRS.Index).Value
                        RACK = RACK & "|" & row.Cells(GRACK.Index).Value.ToString
                        SHELF = SHELF & "|" & row.Cells(GSHELF.Index).Value.ToString
                        YESNO = YESNO & "|" & row.Cells(GYESNO.Index).Value.ToString
                        DIFF = DIFF & "|" & row.Cells(GDIFF.Index).Value
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = DONE & "|" & "1"
                        Else
                            DONE = DONE & "|" & "0"
                        End If
                        OUTPCS = OUTPCS & "|" & row.Cells(GOUTPCS.Index).Value
                        OUTMTRS = OUTMTRS & "|" & row.Cells(GOUTMTRS.Index).Value
                        FROMNO = FROMNO & "|" & row.Cells(GFROMNO.Index).Value
                        FROMSRNO = FROMSRNO & "|" & row.Cells(GFROMSRNO.Index).Value
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value
                        RATE = RATE & "|" & row.Cells(GRATE.Index).Value
                        PER = PER & "|" & row.Cells(GPER.Index).Value
                        AMOUNT = AMOUNT & "|" & row.Cells(GAMOUNT.Index).Value
                        PCSNO = PCSNO & "|" & row.Cells(GPCSNO.Index).Value

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(GRIDLOTNO)
            alParaval.Add(CHECKSRNO)
            alParaval.Add(PCSTYPE)
            alParaval.Add(ITEMNAME)
            alParaval.Add(GRIDDESC)
            alParaval.Add(QUALITY)
            alParaval.Add(BALENO)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(CUT)
            alParaval.Add(WT)
            alParaval.Add(qty)
            alParaval.Add(qtyunit)
            alParaval.Add(MTRS)
            alParaval.Add(RECDMTRS)
            alParaval.Add(RACK)
            alParaval.Add(SHELF)
            alParaval.Add(YESNO)
            alParaval.Add(DIFF)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMOUNT)
            alParaval.Add(BARCODE)
            alParaval.Add(DONE)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)
            alParaval.Add(PCSNO)

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



            Dim ORDERGRIDSRNO As String = ""
            Dim ORDERITEMNAME As String = ""
            Dim ORDERDESIGN As String = ""
            Dim ORDERCOLOR As String = ""
            Dim ORDERMTRS As String = ""
            Dim ORDERFROMNO As String = ""
            Dim ORDERFROMSRNO As String = ""
            Dim ORDERFROMTYPE As String = ""
            Dim ORDERGDNPCS As String = ""
            Dim ORDERGDNMTRS As String = ""
            Dim ORDERRATE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDORDER.Rows
                If row.Cells(0).Value <> Nothing AndAlso (Val(row.Cells(OGDNQTY.Index).Value) > 0 Or Val(row.Cells(OGDNMTRS.Index).Value) > 0) Then

                    If ORDERGRIDSRNO = "" Then
                        ORDERGRIDSRNO = Val(row.Cells(OSRNO.Index).Value)
                        ORDERITEMNAME = row.Cells(OITEMNAME.Index).Value.ToString
                        ORDERDESIGN = row.Cells(ODESIGN.Index).Value.ToString
                        ORDERCOLOR = row.Cells(OCOLOR.Index).Value.ToString
                        ORDERMTRS = Val(row.Cells(OMTRS.Index).Value)
                        ORDERFROMNO = Val(row.Cells(OFROMNO.Index).Value)
                        ORDERFROMSRNO = Val(row.Cells(OFROMSRNO.Index).Value)
                        ORDERFROMTYPE = row.Cells(OFROMTYPE.Index).Value.ToString
                        ORDERGDNPCS = Val(row.Cells(OGDNQTY.Index).Value)
                        ORDERGDNMTRS = Val(row.Cells(OGDNMTRS.Index).Value)
                        ORDERRATE = Val(row.Cells(ORATE.Index).Value)
                    Else
                        ORDERGRIDSRNO = ORDERGRIDSRNO & "|" & Val(row.Cells(OSRNO.Index).Value)
                        ORDERITEMNAME = ORDERITEMNAME & "|" & row.Cells(OITEMNAME.Index).Value.ToString
                        ORDERDESIGN = ORDERDESIGN & "|" & row.Cells(ODESIGN.Index).Value.ToString
                        ORDERCOLOR = ORDERCOLOR & "|" & row.Cells(OCOLOR.Index).Value.ToString
                        ORDERMTRS = ORDERMTRS & "|" & Val(row.Cells(OMTRS.Index).Value)
                        ORDERFROMNO = ORDERFROMNO & "|" & Val(row.Cells(OFROMNO.Index).Value)
                        ORDERFROMSRNO = ORDERFROMSRNO & "|" & Val(row.Cells(OFROMSRNO.Index).Value)
                        ORDERFROMTYPE = ORDERFROMTYPE & "|" & row.Cells(OFROMTYPE.Index).Value.ToString
                        ORDERGDNPCS = ORDERGDNPCS & "|" & Val(row.Cells(OGDNQTY.Index).Value)
                        ORDERGDNMTRS = ORDERGDNMTRS & "|" & Val(row.Cells(OGDNMTRS.Index).Value)
                        ORDERRATE = ORDERRATE & "|" & Val(row.Cells(ORATE.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(ORDERGRIDSRNO)
            alParaval.Add(ORDERITEMNAME)
            alParaval.Add(ORDERDESIGN)
            alParaval.Add(ORDERCOLOR)
            alParaval.Add(ORDERMTRS)
            alParaval.Add(ORDERFROMNO)
            alParaval.Add(ORDERFROMSRNO)
            alParaval.Add(ORDERFROMTYPE)
            alParaval.Add(ORDERGDNPCS)
            alParaval.Add(ORDERGDNMTRS)
            alParaval.Add(ORDERRATE)

            alParaval.Add(CHKRETURN.Checked)
            alParaval.Add(CMBPARTYNAME.Text.Trim)

            alParaval.Add(Val(LBLTOTALAMT.Text.Trim))

            If CHKMANUALRATE.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If


            Dim DSRNO As String = ""
            Dim DMTRS As String = ""
            Dim MAINSRNO As String = ""

            For i As Integer = 0 To DT_MTRSDETAILS.Rows.Count - 1
                If DT_MTRSDETAILS.Rows(i).Item(0) <> Nothing Then
                    If DSRNO = "" Then
                        DSRNO = Val(DT_MTRSDETAILS.Rows(i).Item("DSRNO"))
                        DMTRS = DT_MTRSDETAILS.Rows(i).Item("DMTRS")
                        MAINSRNO = Val(DT_MTRSDETAILS.Rows(i).Item("MAINSRNO"))
                    Else
                        DSRNO = DSRNO & "|" & Val(DT_MTRSDETAILS.Rows(i).Item("DSRNO"))
                        DMTRS = DMTRS & "|" & DT_MTRSDETAILS.Rows(i).Item("DMTRS")
                        MAINSRNO = MAINSRNO & "|" & Val(DT_MTRSDETAILS.Rows(i).Item("MAINSRNO"))
                    End If
                End If
            Next


            alParaval.Add(DSRNO)
            alParaval.Add(DMTRS)
            alParaval.Add(MAINSRNO)

            Dim OBJMATREC As New ClsMaterialReceipt()
            OBJMATREC.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = OBJMATREC.SAVE()
                MsgBox("Details Added")
                TXTMATRECNO.Text = DTTABLE.Rows(0).Item(0)
            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPMATRECNO)
                IntResult = OBJMATREC.UPDATE()
                MsgBox("Details Updated")
            End If


            If ClientName <> "SNCM" Then PRINTBARCODE()
            If ClientName <> "SOFTAS" And ClientName <> "AVIS" And ClientName <> "SNCM" Then PRINTREPORT()


            'COPY SCANNED DOCS FILES 
            For Each ROW As DataGridViewRow In gridupload.Rows
                If FileIO.FileSystem.DirectoryExists(Application.StartupPath & "\UPLOADDOCS") = False Then
                    FileIO.FileSystem.CreateDirectory(Application.StartupPath & "\UPLOADDOCS")
                End If
                If FileIO.FileSystem.FileExists(Application.StartupPath & "\UPLOADDOCS") = False Then
                    System.IO.File.Copy(ROW.Cells(GIMGPATH.Index).Value, ROW.Cells(GNEWIMGPATH.Index).Value, True)
                End If
            Next


            'DIRECTLY ISSUE TO PACKING
            If EDIT = False And (ClientName = "KARAN") Then
                If MsgBox("Issue Grey Directly to Packing?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then DIRECTISSUEPACKING()
            End If

            If EDIT = False And (ClientName = "INDRAPUJAFABRICS") Then
                If MsgBox("Issue Grey Directly to Packing?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then DIRECTISSUEPACKING()
            End If

            EDIT = False

            CLEAR()
            cmbGodown.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub DIRECTISSUEPACKING()
        Try


            'GET FRESH DATA FROM DATABASE (ONLY GRID)
            'THIS IS DONE COZ FOR MULTIUSER THE NOS WILL BE SAME
            'SO WE WILL ADD BARCODE IN SP AND THEN FETCH THAT DATA HERE AFTER THAT WE WILL PRINT BARCODES
            GRIDMATREC.RowCount = 0
            Dim OBJMATREC As New ClsMaterialReceipt()
            Dim dttable As New DataTable
            dttable = OBJMATREC.SELECTMATREC(Val(TXTMATRECNO.Text.Trim), CmpId, Locationid, YearId)
            For Each dr As DataRow In dttable.Rows
                ' GRIDMATREC.Rows.Add(dr("GRIDSRNO").ToString, dr("GRIDLOTNO"), dr("CHECKSRNO"), dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGN").ToString, dr("COLOR"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("WT")), "0.00"), Format(Val(dr("qty")), "0.00"), dr("UNIT").ToString, Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RECDMTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER").ToString, Format(Val(dr("AMOUNT")), "0.00"), dr("RACK").ToString, dr("SHELF").ToString, dr("YESNO"), Format(Val(dr("DIFF")), "0.00"), dr("BARCODE"), dr("GRIDDONE").ToString, dr("OUTPCS"), dr("OUTMTRS"), dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"), dr("PCSNO"))
                GRIDMATREC.Rows.Add(dr("GRIDSRNO").ToString, dr("GRIDLOTNO"), dr("CHECKSRNO"), dr("PIECETYPE").ToString, dr("ITEMNAME").ToString, dr("GRIDDESC").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGN").ToString, dr("COLOR"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("WT")), "0.00"), Format(Val(dr("qty")), "0.00"), dr("UNIT").ToString, Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RECDMTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER").ToString, Format(Val(dr("AMOUNT")), "0.00"), dr("RACK").ToString, dr("SHELF").ToString, dr("YESNO").ToString, Format(Val(dr("DIFF")), "0.00"), dr("BARCODE"), dr("GRIDDONE").ToString, dr("OUTPCS"), dr("OUTMTRS"), dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"), dr("PCSNO"))
            Next



            Dim alParaval As New ArrayList
            alParaval.Add(0)        'MANUALISSNO
            alParaval.Add(Format(Convert.ToDateTime(MATRECDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbGodown.Text.Trim)
            alParaval.Add("")       'CONTRACTOR
            alParaval.Add(TXTCHALLANNO.Text.Trim) 'REFNO
            alParaval.Add(0)        'SLTP


            alParaval.Add("") 'WEAVERNAME
            alParaval.Add("") 'WEAVERCHNO
            alParaval.Add(TXTCHALLANNO.Text.Trim) 'CHALLANNO


            alParaval.Add(Val(lbltotalqty.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim gridsrno As String = ""
            Dim PIECETYPE As String = ""
            Dim BALENO As String = ""
            Dim ITEMNAME As String = ""
            Dim LOTNO As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim PCS As String = ""
            Dim UNIT As String = ""
            Dim MTRS As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""
            Dim RATE As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""

            Dim BARCODE As String = "" 'BARCODE ADDED

            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""
            Dim GREYMTRS As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDMATREC.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString

                        PIECETYPE = "FRESH"
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = row.Cells(GBALENO.Index).Value.ToString Else BALENO = ""
                        ITEMNAME = row.Cells(gitemname.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        PCS = row.Cells(gQty.Index).Value.ToString
                        UNIT = row.Cells(gqtyunit.Index).Value
                        MTRS = row.Cells(GRECDMTRS.Index).Value.ToString
                        RATE = row.Cells(GRATE.Index).Value
                        PER = row.Cells(GPER.Index).Value
                        AMOUNT = row.Cells(GAMOUNT.Index).Value
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString

                        OUTPCS = 0
                        OUTMTRS = 0
                        FROMNO = TXTMATRECNO.Text.Trim
                        FROMSRNO = row.Cells(gsrno.Index).Value.ToString
                        FROMTYPE = "MATREC"
                        GREYMTRS = Val(row.Cells(GMTRS.Index).Value)


                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value.ToString

                        PIECETYPE = PIECETYPE & "|" & "FRESH"
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString Else BALENO = BALENO & "|" & ""
                        ITEMNAME = ITEMNAME & "|" & row.Cells(gitemname.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        PCS = PCS & "|" & row.Cells(gQty.Index).Value.ToString
                        UNIT = UNIT & "|" & row.Cells(gqtyunit.Index).Value.ToString
                        MTRS = MTRS & "|" & row.Cells(GRECDMTRS.Index).Value.ToString
                        RATE = RATE & "|" & row.Cells(GRATE.Index).Value
                        PER = PER & "|" & row.Cells(GPER.Index).Value
                        AMOUNT = AMOUNT & "|" & row.Cells(GAMOUNT.Index).Value
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        OUTPCS = OUTPCS & "|" & row.Cells(GOUTPCS.Index).Value.ToString
                        OUTMTRS = OUTMTRS & "|" & row.Cells(GOUTMTRS.Index).Value.ToString
                        FROMNO = FROMNO & "|" & Val(TXTMATRECNO.Text.Trim)
                        FROMSRNO = FROMSRNO & "|" & row.Cells(gsrno.Index).Value.ToString
                        FROMTYPE = FROMTYPE & "|" & "MATREC"
                        GREYMTRS = GREYMTRS & "|" & Val(row.Cells(GMTRS.Index).Value)


                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(PIECETYPE)
            alParaval.Add(BALENO)
            alParaval.Add(ITEMNAME)
            alParaval.Add(LOTNO)
            alParaval.Add(QUALITY)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(PCS)
            alParaval.Add(UNIT)
            alParaval.Add(MTRS)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMOUNT)
            alParaval.Add(BARCODE)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)
            alParaval.Add(GREYMTRS)


            Dim griduploadsrno As String = ""
            Dim imgpath As String = ""
            Dim uploadremarks As String = ""
            Dim name As String = ""
            Dim NEWIMGPATH As String = ""
            Dim FILENAME As String = ""

            'Saving Upload Grid
            'For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
            '    If row.Cells(0).Value <> Nothing Then
            '        If griduploadsrno = "" Then
            '            griduploadsrno = row.Cells(0).Value.ToString
            '            uploadremarks = row.Cells(1).Value.ToString
            '            name = row.Cells(2).Value.ToString
            '            imgpath = row.Cells(3).Value.ToString
            '            NEWIMGPATH = row.Cells(GNEWIMGPATH.Index).Value.ToString

            '        Else
            '            griduploadsrno = griduploadsrno & "|" & row.Cells(0).Value.ToString
            '            uploadremarks = uploadremarks & "|" & row.Cells(1).Value.ToString
            '            name = name & "|" & row.Cells(2).Value.ToString
            '            imgpath = imgpath & "|" & row.Cells(3).Value.ToString
            '            NEWIMGPATH = NEWIMGPATH & "|" & row.Cells(GNEWIMGPATH.Index).Value.ToString

            '        End If
            '    End If
            'Next

            alParaval.Add(griduploadsrno)
            alParaval.Add(uploadremarks)
            alParaval.Add(name)
            alParaval.Add(imgpath)
            alParaval.Add(NEWIMGPATH)
            alParaval.Add(FILENAME)

            alParaval.Add(LBLTOTALAMT.Text.Trim)

            Dim OBJISSUE As New ClsIssueToPacking()
            OBJISSUE.alParaval = alParaval
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            dttable = OBJISSUE.SAVE()
            MsgBox("Grey Issued to Packing")

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub PRINTBARCODE()
        Try
            If ALLOWBARCODEPRINT Then

                'AS PER HARSH BHAI
                If ClientName = "PARAS" Then Exit Sub


                'PRINT BARCODE
                Dim TEMPMSG As Integer = MsgBox("Wish to Print Barcode?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                'GET FRESH DATA FROM DATABASE (ONLY GRID)
                'THIS IS DONE COZ FOR MULTIUSER THE NOS WILL BE SAME
                'SO WE WILL ADD BARCODE IN SP AND THEN FETCH THAT DATA HERE AFTER THAT WE WILL PRINT BARCODES
                GRIDMATREC.RowCount = 0
                Dim OBJMATREC As New ClsMaterialReceipt()
                Dim dttable As DataTable = OBJMATREC.SELECTMATREC(Val(TXTMATRECNO.Text.Trim), CmpId, Locationid, YearId)
                For Each dr As DataRow In dttable.Rows
                    GRIDMATREC.Rows.Add(dr("GRIDSRNO").ToString, dr("GRIDLOTNO"), dr("CHECKSRNO"), dr("PIECETYPE").ToString, dr("ITEMNAME").ToString, dr("GRIDDESC").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGN").ToString, dr("COLOR"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("WT")), "0.00"), Format(Val(dr("qty")), "0.00"), dr("UNIT").ToString, Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RECDMTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER").ToString, Format(Val(dr("AMOUNT")), "0.00"), dr("RACK").ToString, dr("SHELF").ToString, dr("YESNO"), Format(Val(dr("DIFF")), "0.00"), dr("BARCODE"), dr("GRIDDONE").ToString, dr("OUTPCS"), dr("OUTMTRS"), dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"), dr("PCSNO"))
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


                For Each ROW As DataGridViewRow In GRIDMATREC.Rows

                    'TO PRINT BARCODE FROM SELECTED SRNO
                    If (Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0) Then
                        If Val(ROW.Cells(gsrno.Index).Value) < Val(TXTFROM.Text.Trim) Or Val(ROW.Cells(gsrno.Index).Value) > Val(TXTTO.Text.Trim) Then GoTo NEXTLINE
                    End If
                    Dim GRIDDESC As String = ""
                    Dim DYEINGNAME As String = ""
                    If ClientName = "KCRAYON" Then GRIDDESC = ROW.Cells(GBALENO.Index).Value
                    If ClientName = "RAJKRIPA" Then
                        DYEINGNAME = cmbname.Text.Trim
                        GRIDDESC = ROW.Cells(GBALENO.Index).Value
                    End If

                    'IF barcode is used the BARCODE printING WILL BE BLOCKED
                    If Val(ROW.Cells(GOUTMTRS.Index).Value) > 0 Then GoTo NEXTLINE

                    BARCODEPRINTING(ROW.Cells(GBARCODE.Index).Value, "FRESH", ROW.Cells(gitemname.Index).Value, ROW.Cells(GQUALITY.Index).Value, ROW.Cells(GDESIGN.Index).Value, ROW.Cells(gcolor.Index).Value, ROW.Cells(gqtyunit.Index).Value, ROW.Cells(GLOTNO.Index).Value, ROW.Cells(GBALENO.Index).Value, GRIDDESC, Val(ROW.Cells(GRECDMTRS.Index).Value), Val(ROW.Cells(gQty.Index).Value), Val(ROW.Cells(gcut.Index).Value), ROW.Cells(GRACK.Index).Value, TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, "", DYEINGNAME, ROW.Cells(GSHELF.Index).Value)
NEXTLINE:

                Next
            End If
            '                    Dim dirresults As String = ""

            '                    'Writing in file
            '                    Dim oWrite As System.IO.StreamWriter
            '                    'oWrite = File.CreateText(Application.StartupPath & "\Barcode.txt")
            '                    'oWrite = File.CreateText(Application.StartupPath & "\Barcode.txt")
            '                    oWrite = File.CreateText("D:\Barcode.txt")


            '                    'TO PRINT BARCODE FROM SELECTED SRNO
            '                    If (Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0) Then
            '                        If Val(ROW.Cells(gsrno.Index).Value) < Val(TXTFROM.Text.Trim) Or Val(ROW.Cells(gsrno.Index).Value) > Val(TXTTO.Text.Trim) Then GoTo NEXTLINE
            '                    End If


            '                    If ClientName = "SVS" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>I8,A")
            '                        oWrite.WriteLine("ZN")
            '                        oWrite.WriteLine("q400")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("Q200,25")
            '                        oWrite.WriteLine("KI80")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>N")
            '                        oWrite.WriteLine("A376,160,2,2,1,1,N,""QUALITY""")
            '                        oWrite.WriteLine("A376,114,2,2,1,1,N,""D.NO""")
            '                        oWrite.WriteLine("A376,136,2,2,1,1,N,""SHADE""")
            '                        oWrite.WriteLine("B384,91,2,1,2,4,61,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A279,24,2,2,1,1,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A197,114,2,2,1,1,N,""QTY""")
            '                        oWrite.WriteLine("A376,183,2,2,1,1,N,""" & CmpName & """")    'cmpname
            '                        oWrite.WriteLine("A277,114,2,2,1,1,N,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("A291,114,2,2,1,1,N,"":""")
            '                        oWrite.WriteLine("A291,136,2,2,1,1,N,"":""")
            '                        oWrite.WriteLine("A277,136,2,2,1,1,N,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("A291,162,2,2,1,1,N,"":""")
            '                        oWrite.WriteLine("A277,162,2,2,1,1,N,""" & ROW.Cells(GQUALITY.Index).Value & """")
            '                        oWrite.WriteLine("A157,114,2,2,1,1,N,"":""")
            '                        oWrite.WriteLine("A143,114,2,2,1,1,N,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & " " & ROW.Cells(gqtyunit.Index).Value & """")
            '                        'oWrite.WriteLine("A143,114,2,2,1,1,N,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & " MTR""")
            '                        oWrite.WriteLine("P1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "MARKIN" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='75.1 mm'></xpml>SIZE 97.5 mm, 75.1 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='75.1 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 709,566,""0"",180,16,16,""" & CmpName & """")
            '                        oWrite.WriteLine("TEXT 738,421,""0"",180,14,14,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 738,353,""0"",180,14,14,""COLOR""")
            '                        oWrite.WriteLine("TEXT 738,285,""0"",180,14,14,""LOTNO""")
            '                        oWrite.WriteLine("TEXT 738,488,""0"",180,14,14,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 738,216,""0"",180,14,14,""MTRS""")
            '                        oWrite.WriteLine("BARCODE 738,160,""128M"",74,0,180,3,6,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 597,79,""0"",180,16,16,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 527,488,""0"",180,14,14,"":""")
            '                        oWrite.WriteLine("TEXT 527,421,""0"",180,14,14,"":""")
            '                        oWrite.WriteLine("TEXT 527,353,""0"",180,14,14,"":""")
            '                        oWrite.WriteLine("TEXT 527,285,""0"",180,14,14,"":""")
            '                        oWrite.WriteLine("TEXT 527,216,""0"",180,14,14,"":""")
            '                        oWrite.WriteLine("TEXT 498,488,""0"",180,14,14,""" & ROW.Cells(gitemname.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("TEXT 498,421,""0"",180,14,14,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("TEXT 498,353,""0"",180,14,14,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 498,285,""0"",180,14,14,""" & ROW.Cells(GLOTNO.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 498,227,""0"",180,22,22,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("BAR 43,505, 695, 3")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "SHALIBHADRA" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='25.4 mm'></xpml>I8,A")
            '                        oWrite.WriteLine("ZN")
            '                        oWrite.WriteLine("q406")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("Q203,25")
            '                        oWrite.WriteLine("KI80")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.4 mm'></xpml>N")
            '                        oWrite.WriteLine("B369,101,2,1,2,4,51,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A295,43,2,4,1,1,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A380,179,2,4,1,1,N,""Lot""")
            '                        oWrite.WriteLine("A380,138,2,4,1,1,N,""D.No""")
            '                        oWrite.WriteLine("A309,179,2,4,1,1,N,"":""")
            '                        oWrite.WriteLine("A292,179,2,4,1,1,N,""" & CMBLOTNO.Text.Trim & """")
            '                        oWrite.WriteLine("A308,138,2,4,1,1,N,"":""")
            '                        oWrite.WriteLine("A292,138,2,4,1,1,N,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("A124,186,2,4,1,1,N,""Mtrs""")
            '                        oWrite.WriteLine("A176,150,2,3,2,2,N,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("P1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "KCRAYON" Then

            '                        oWrite.WriteLine("SIZE 101.6 mm, 50.8 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 783,377,""2"",180,3,3,""" & ROW.Cells(GDESIGN.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 783,283,""2"",180,2,2,""SHADE""")
            '                        If ROW.Cells(GBALENO.Index).Value <> "" Then oWrite.WriteLine("TEXT 111,283,""2"",180,2,2,""TP""") Else oWrite.WriteLine("TEXT 111,283,""2"",180,2,2,""""")
            '                        oWrite.WriteLine("TEXT 405,216,""2"",180,2,2,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 783,216,""2"",180,2,2,""MTRS""")
            '                        oWrite.WriteLine("TEXT 265,216,""2"",180,2,2,"":""")
            '                        oWrite.WriteLine("TEXT 672,216,""2"",180,2,2,"":""")
            '                        oWrite.WriteLine("TEXT 631,283,""2"",180,2,2,"":""")
            '                        oWrite.WriteLine("TEXT 603,283,""2"",180,2,2,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 631,216,""2"",180,2,2,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("TEXT 237,216,""2"",180,2,2,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("BARCODE 783,161,""128M"",95,0,180,4,8,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 601,55,""2"",180,2,2,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "DAKSH" Then

            '                        oWrite.WriteLine("G0")
            '                        oWrite.WriteLine("n")
            '                        oWrite.WriteLine("M0500")
            '                        oWrite.WriteLine("O0214")
            '                        oWrite.WriteLine("V0")
            '                        oWrite.WriteLine("t1")
            '                        oWrite.WriteLine("Kf0070")
            '                        oWrite.WriteLine("L")
            '                        oWrite.WriteLine("D11")
            '                        oWrite.WriteLine("ySPM")
            '                        oWrite.WriteLine("A2")
            '                        oWrite.WriteLine("1911C2401560027LINEN VENZO")
            '                        oWrite.WriteLine("1X1100001550005L263001")
            '                        oWrite.WriteLine("1e4203600230043B" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1911C1000060084" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1911C1401280011" & ROW.Cells(gitemname.Index).Value)
            '                        oWrite.WriteLine("1911C1001090012SHADE")
            '                        oWrite.WriteLine("1911C1000610012QUALITY")
            '                        oWrite.WriteLine("1911C1001090077:")
            '                        oWrite.WriteLine("1911C1000610077:")
            '                        oWrite.WriteLine("1911C1401060086" & ROW.Cells(gcolor.Index).Value)
            '                        oWrite.WriteLine("1911C1000610086" & ROW.Cells(GQUALITY.Index).Value)
            '                        oWrite.WriteLine("1911C1000840012MTRS")
            '                        oWrite.WriteLine("1911C1000840077:")
            '                        oWrite.WriteLine("1911C1400810086" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00"))
            '                        oWrite.WriteLine("1911C1001090162D. NO")
            '                        oWrite.WriteLine("1911C1001090204:")
            '                        oWrite.WriteLine("1911C1201080212" & ROW.Cells(GDESIGN.Index).Value)
            '                        oWrite.WriteLine("1911C1000840162LOT")
            '                        oWrite.WriteLine("1911C1000840204:")
            '                        oWrite.WriteLine("1911C1000840213" & ROW.Cells(GLOTNO.Index).Value)
            '                        oWrite.WriteLine("Q0001")
            '                        oWrite.WriteLine("E")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "PARAS" Then

            '                        oWrite.WriteLine("SIZE 99.10 mm, 50 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 620,371,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 600,374,""ROMAN.TTF"",180,1,16,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 782,371,""ROMAN.TTF"",180,1,14,""QUALITY""")
            '                        oWrite.WriteLine("TEXT 782,310,""ROMAN.TTF"",180,1,14,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 600,310,""ROMAN.TTF"",180,1,14,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 620,310,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 360,310,""ROMAN.TTF"",180,1,14,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 237,310,""ROMAN.TTF"",180,1,14,"":""")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If
            '                        oWrite.WriteLine("TEXT 211,310,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")


            '                        oWrite.WriteLine("TEXT 782,249,""ROMAN.TTF"",180,1,14,""LOTNO""")
            '                        oWrite.WriteLine("TEXT 620,249,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 600,249,""ROMAN.TTF"",180,1,14,""" & CMBLOTNO.Text.Trim & """")
            '                        oWrite.WriteLine("TEXT 363,249,""ROMAN.TTF"",180,1,14,""SHADE""")
            '                        oWrite.WriteLine("TEXT 231,249,""ROMAN.TTF"",180,1,14,"": """)
            '                        oWrite.WriteLine("TEXT 211,249,""ROMAN.TTF"",180,1,14,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 782,187,""ROMAN.TTF"",180,1,14,""MTRS""")
            '                        oWrite.WriteLine("TEXT 620,187,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("BARCODE 776,134,""128M"",83,0,180,4,8,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 499,47,""ROMAN.TTF"",180,1,11,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 600,192,""ROMAN.TTF"",180,1,18,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "ARIHANT" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("BARCODE 508,154,""128M"",106,0,180,2,4,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 408,42,""0"",180,10,10,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 508,378,""0"",180,16,16,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 508,316,""0"",180,12,12,""D.NO""")
            '                        oWrite.WriteLine("TEXT 508,265,""0"",180,12,12,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 166,316,""0"",180,12,12,""" & ROW.Cells(GLOTNO.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 508,210,""0"",180,12,12,""MTRS""")
            '                        oWrite.WriteLine("TEXT 405,316,""0"",180,12,12,"":""")
            '                        oWrite.WriteLine("TEXT 405,265,""0"",180,12,12,"":""")
            '                        oWrite.WriteLine("TEXT 405,210,""0"",180,12,12,"":""")
            '                        oWrite.WriteLine("TEXT 377,316,""0"",180,12,12,""" & ROW.Cells(GDESIGN.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If
            '                        oWrite.WriteLine("TEXT 377,265,""0"",180,12,12,""" & TEMPWIDTH & """")

            '                        oWrite.WriteLine("TEXT 377,217,""0"",180,18,18,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "KEMLINO" Then

            '                        oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 581,354,""ROMAN.TTF"",180,1,19,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 738,282,""ROMAN.TTF"",180,1,14,""D.NO""")
            '                        oWrite.WriteLine("TEXT 738,228,""ROMAN.TTF"",180,1,14,""SHADE""")
            '                        oWrite.WriteLine("TEXT 738,172,""ROMAN.TTF"",180,1,14,""MTRS""")
            '                        oWrite.WriteLine("TEXT 738,119,""ROMAN.TTF"",180,1,14,""UNIT""")
            '                        oWrite.WriteLine("QRCODE 237,280,L,10,A,180,M2,S7,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 237,65,""ROMAN.TTF"",180,1,10,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 609,282,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 609,228,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 609,172,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 609,119,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 581,282,""ROMAN.TTF"",180,1,14,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 581,228,""ROMAN.TTF"",180,1,14,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 581,176,""ROMAN.TTF"",180,1,18,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If
            '                        oWrite.WriteLine("TEXT 581,67,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")


            '                        oWrite.WriteLine("TEXT 738,67,""ROMAN.TTF"",180,1,14,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 609,67,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 581,119,""ROMAN.TTF"",180,1,14,""" & ROW.Cells(gqtyunit.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 738,348,""ROMAN.TTF"",180,1,14,""PROD""")
            '                        oWrite.WriteLine("TEXT 609,348,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("BAR 29,297, 708, 3")
            '                        oWrite.WriteLine("TEXT 410,119,""ROMAN.TTF"",180,1,14,""" & ROW.Cells(GLOTNO.Index).Value & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "PURVITEX" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.8 mm'></xpml>SIZE 101.6 mm, 50.8 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.8 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("BARCODE 790,113,""128M"",68,0,180,4,8,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 506,40,""ROMAN.TTF"",180,1,10,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 794,384,""ROMAN.TTF"",180,1,16,""QUALITY""")
            '                        oWrite.WriteLine("TEXT 793,313,""ROMAN.TTF"",180,1,16,""SHADE""")
            '                        oWrite.WriteLine("TEXT 789,171,""ROMAN.TTF"",180,1,16,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 794,242,""ROMAN.TTF"",180,1,16,""MTRS""")
            '                        oWrite.WriteLine("TEXT 588,384,""ROMAN.TTF"",180,1,16,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 614,384,""ROMAN.TTF"",180,1,16,"":""")
            '                        oWrite.WriteLine("TEXT 614,313,""ROMAN.TTF"",180,1,16,"":""")
            '                        oWrite.WriteLine("TEXT 588,313,""ROMAN.TTF"",180,1,16,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 614,171,""ROMAN.TTF"",180,1,16,"":""")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("TEXT 588,171,""ROMAN.TTF"",180,1,16,""" & TEMPWIDTH & """")

            '                        oWrite.WriteLine("TEXT 614,243,""0"",180,16,17,"":""")
            '                        oWrite.WriteLine("TEXT 588,252,""ROMAN.TTF"",180,1,24,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("TEXT 233,171,""ROMAN.TTF"",180,1,16,""" & CMBLOTNO.Text.Trim & """")
            '                        oWrite.WriteLine("TEXT 255,171,""ROMAN.TTF"",180,1,16,"":""")
            '                        oWrite.WriteLine("TEXT 412,171,""ROMAN.TTF"",180,1,16,""LOT NO""")
            '                        oWrite.WriteLine("TEXT 372,242,""ROMAN.TTF"",180,1,16,""DESC""")
            '                        oWrite.WriteLine("TEXT 255,242,""ROMAN.TTF"",180,1,16,"":""")
            '                        oWrite.WriteLine("TEXT 233,242,""ROMAN.TTF"",180,1,16,"" """)
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "DJIMPEX" Then

            '                        oWrite.WriteLine("SIZE 99.10 mm, 50 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 768,362,""ROMAN.TTF"",180,1,14,""QUALITY""")
            '                        oWrite.WriteLine("TEXT 768,303,""ROMAN.TTF"",180,1,14,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 768,244,""ROMAN.TTF"",180,1,14,""SHADE""")
            '                        oWrite.WriteLine("TEXT 768,185,""ROMAN.TTF"",180,1,14,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 271,232,""ROMAN.TTF"",180,1,14,""MTRS""")
            '                        oWrite.WriteLine("TEXT 614,362,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 614,303,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 614,244,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 614,185,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 170,235,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 593,362,""ROMAN.TTF"",180,1,14,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 593,303,""ROMAN.TTF"",180,1,14,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 593,244,""ROMAN.TTF"",180,1,14,""" & ROW.Cells(gcolor.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("TEXT 593,185,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("TEXT 149,235,""ROMAN.TTF"",180,1,16,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("BARCODE 768,133,""128M"",76,0,180,4,8,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 768,51,""ROMAN.TTF"",180,1,12,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 253,51,""ROMAN.TTF"",180,1,11,""WWW.DJIMPEX.IN""")
            '                        oWrite.WriteLine("TEXT 270,185,""ROMAN.TTF"",180,1,14,""YDS""")
            '                        oWrite.WriteLine("TEXT 170,185,""ROMAN.TTF"",180,1,14,"":""")
            '                        oWrite.WriteLine("TEXT 149,189,""ROMAN.TTF"",180,1,16,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value) * 1.094, "0.00") & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "RATAN" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 745,378,""0"",180,11,11,""QUALITY""")
            '                        oWrite.WriteLine("TEXT 745,330,""0"",180,11,11,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 745,282,""0"",180,11,11,""SHADE""")
            '                        oWrite.WriteLine("TEXT 308,186,""0"",180,11,11,""MTRS""")
            '                        oWrite.WriteLine("TEXT 745,186,""0"",180,13,13,""WIDTH""")
            '                        oWrite.WriteLine("BARCODE 745,126,""128M"",70,0,180,3,6,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 567,50,""0"",180,12,12,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 590,378,""0"",180,11,11,"":""")
            '                        oWrite.WriteLine("TEXT 590,330,""0"",180,11,11,"":""")
            '                        oWrite.WriteLine("TEXT 590,282,""0"",180,11,11,"":""")
            '                        oWrite.WriteLine("TEXT 590,186,""0"",180,11,11,"":""")
            '                        oWrite.WriteLine("TEXT 216,186,""0"",180,11,11,"":""")
            '                        oWrite.WriteLine("TEXT 564,382,""0"",180,15,15,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 564,331,""0"",180,13,13,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 564,282,""0"",180,11,11,""" & ROW.Cells(gcolor.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("TEXT 564,186,""0"",180,11,11,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("TEXT 188,193,""0"",180,18,18,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("TEXT 745,234,""0"",180,11,11,""LOT NO""")
            '                        oWrite.WriteLine("TEXT 590,234,""0"",180,11,11,"":""")
            '                        oWrite.WriteLine("TEXT 564,234,""0"",180,11,11,""" & ROW.Cells(GLOTNO.Index).Value & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "KENCOT" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.8 mm'></xpml>SIZE 101.6 mm, 50.8 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("SPEED 4")
            '                        oWrite.WriteLine("DENSITY 10")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.8 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 506,377,""ROMAN.TTF"",180,1,17,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("BARCODE 780,140,""128M"",85,0,180,4,8,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 484,50,""ROMAN.TTF"",180,1,9,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 780,298,""ROMAN.TTF"",180,1,14,""DESIGN NO""")
            '                        oWrite.WriteLine("TEXT 321,298,""ROMAN.TTF"",180,1,14,""SHADE NO""")
            '                        oWrite.WriteLine("TEXT 585,302,""ROMAN.TTF"",180,1,17,"":""")
            '                        oWrite.WriteLine("TEXT 125,302,""ROMAN.TTF"",180,1,17,"":""")
            '                        oWrite.WriteLine("TEXT 555,311,""ROMAN.TTF"",180,1,24,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 95,305,""ROMAN.TTF"",180,1,17,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 382,209,""ROMAN.TTF"",180,1,14,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 266,214,""ROMAN.TTF"",180,1,17,"":""")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("TEXT 243,213,""0"",180,17,17,""" & TEMPWIDTH & """")

            '                        oWrite.WriteLine("TEXT 677,214,""ROMAN.TTF"",180,1,17,"":""")
            '                        oWrite.WriteLine("TEXT 780,209,""ROMAN.TTF"",180,1,14,""MTRS""")
            '                        oWrite.WriteLine("TEXT 625,223,""ROMAN.TTF"",180,1,24,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("TEXT 780,373,""ROMAN.TTF"",180,1,14,""MERCHANT NO :""")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "DRDRAPES" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 734,287,""0"",180,13,13,""Quality""")
            '                        oWrite.WriteLine("TEXT 734,242,""0"",180,13,13,""Design""")
            '                        oWrite.WriteLine("TEXT 735,197,""0"",180,13,13,""Shade""")
            '                        oWrite.WriteLine("TEXT 734,151,""0"",180,13,13,""Mtrs""")
            '                        oWrite.WriteLine("TEXT 615,286,""0"",180,13,13,"":""")
            '                        oWrite.WriteLine("TEXT 615,241,""0"",180,13,13,"":""")
            '                        oWrite.WriteLine("TEXT 615,195,""0"",180,13,13,"":""")
            '                        oWrite.WriteLine("TEXT 615,150,""0"",180,13,13,"":""")
            '                        oWrite.WriteLine("TEXT 595,286,""0"",180,13,13,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 595,241,""0"",180,14,14,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 595,196,""0"",180,14,14,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 595,151,""0"",180,14,14,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("BARCODE 726,107,""128M"",55,0,180,3,6,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 537,47,""0"",180,10,10,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "SUCCESS" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='100.1 mm'></xpml>SIZE 99.10 mm, 100.1 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='100.1 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 767,429,""0"",180,24,24,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("BARCODE 682,578,""128M"",89,0,180,3,6,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 491,483,""0"",180,10,10,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 767,339,""0"",180,16,16,""D. NO""")
            '                        oWrite.WriteLine("TEXT 610,339,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 583,339,""0"",180,16,16,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 340,339,""0"",180,16,16,""SHADE""")
            '                        oWrite.WriteLine("TEXT 190,339,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 167,339,""0"",180,16,16,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 767,272,""0"",180,16,16,""GRADE""")
            '                        oWrite.WriteLine("TEXT 610,272,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 583,272,""0"",180,16,16,""FRESH""")
            '                        oWrite.WriteLine("TEXT 340,272,""0"",180,16,16,""MTRS""")
            '                        oWrite.WriteLine("TEXT 190,272,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 167,272,""0"",180,16,16,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("TEXT 750,183,""0"",180,12,12,""FAST TO NORMAL WASHING. BLENDED FABRIC""")
            '                        oWrite.WriteLine("TEXT 652,137,""0"",180,12,12,""POLYSTER - 65%     VISCOSE - 35%""")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "YASHVI" Then

            '                        oWrite.WriteLine("SIZE 72.5 mm, 50 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 526,255,""ROMAN.TTF"",180,1,11,""QUALITY""")
            '                        oWrite.WriteLine("TEXT 526,220,""ROMAN.TTF"",180,1,11,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 526,185,""ROMAN.TTF"",180,1,11,""SHADE NO""")
            '                        oWrite.WriteLine("TEXT 526,150,""ROMAN.TTF"",180,1,11,""MTRS""")
            '                        oWrite.WriteLine("TEXT 526,115,""ROMAN.TTF"",180,1,11,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 357,255,""ROMAN.TTF"",180,1,11,"":""")
            '                        oWrite.WriteLine("TEXT 357,220,""ROMAN.TTF"",180,1,11,"":""")
            '                        oWrite.WriteLine("TEXT 357,185,""ROMAN.TTF"",180,1,11,"":""")
            '                        oWrite.WriteLine("TEXT 357,150,""ROMAN.TTF"",180,1,11,"":""")
            '                        oWrite.WriteLine("TEXT 357,115,""ROMAN.TTF"",180,1,11,"":""")
            '                        oWrite.WriteLine("TEXT 337,255,""ROMAN.TTF"",180,1,11,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 337,220,""ROMAN.TTF"",180,1,11,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 337,185,""ROMAN.TTF"",180,1,11,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 337,150,""ROMAN.TTF"",180,1,11,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("TEXT 218,150,""ROMAN.TTF"",180,1,11,""" & ROW.Cells(gqtyunit.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                            TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
            '                            TEMPREMARKS = DT.Rows(0).Item("REMARKS")
            '                        End If

            '                        oWrite.WriteLine("TEXT 337,115,""ROMAN.TTF"",180,1,11,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("TEXT 526,311,""ROMAN.TTF"",180,1,15,""" & TEMPHEADER & """")
            '                        oWrite.WriteLine("TEXT 30,259,""ROMAN.TTF"",270,1,8,""" & TEMPREMARKS & """")
            '                        oWrite.WriteLine("BARCODE 522,82,""128M"",50,0,180,2,4,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 422,27,""ROMAN.TTF"",180,1,10,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "TARUN" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 755,241,""0"",180,14,14,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 299,241,""0"",180,14,14,""SHADE""")
            '                        oWrite.WriteLine("TEXT 755,184,""0"",180,14,14,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 755,352,""0"",180,14,14,""MERCHANT""")
            '                        oWrite.WriteLine("TEXT 755,299,""0"",180,14,14,""QUALITY""")
            '                        oWrite.WriteLine("BARCODE 767,136,""128M"",55,0,180,4,8,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 502,75,""0"",180,12,12,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 544,352,""0"",180,14,14,"":""")
            '                        oWrite.WriteLine("TEXT 544,299,""0"",180,14,14,"":""")
            '                        oWrite.WriteLine("TEXT 544,241,""0"",180,14,14,"":""")
            '                        oWrite.WriteLine("TEXT 544,184,""0"",180,14,14,"":""")
            '                        oWrite.WriteLine("TEXT 163,241,""0"",180,14,14,"":""")
            '                        oWrite.WriteLine("TEXT 299,184,""0"",180,14,14,""MTRS""")
            '                        oWrite.WriteLine("TEXT 163,184,""0"",180,14,14,"":""")
            '                        oWrite.WriteLine("TEXT 516,352,""0"",180,14,14,""" & ROW.Cells(gitemname.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                            TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
            '                            TEMPREMARKS = DT.Rows(0).Item("REMARKS")
            '                        End If

            '                        oWrite.WriteLine("TEXT 516,299,""0"",180,14,14,""" & TEMPCATEGORY & """")
            '                        oWrite.WriteLine("TEXT 516,241,""0"",180,14,14,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 516,184,""0"",180,14,14,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("TEXT 139,241,""0"",180,14,14,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 139,184,""0"",180,14,14,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "YUMILONE" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 760,375,""0"",180,16,16,""MERCHANT""")
            '                        oWrite.WriteLine("TEXT 760,320,""0"",180,16,16,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 760,265,""0"",180,16,16,""SHADE""")
            '                        oWrite.WriteLine("TEXT 760,210,""0"",180,16,16,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 311,210,""0"",180,16,16,""MTRS""")
            '                        oWrite.WriteLine("BARCODE 767,143,""128M"",88,0,180,4,8,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 502,49,""0"",180,12,12,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 539,375,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 539,320,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 539,265,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 539,210,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 190,210,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 518,375,""0"",180,16,16,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 518,320,""0"",180,16,16,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 518,265,""0"",180,16,16,""" & ROW.Cells(gcolor.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                            TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
            '                            TEMPREMARKS = DT.Rows(0).Item("REMARKS")
            '                        End If

            '                        oWrite.WriteLine("TEXT 518,210,""0"",180,16,16,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("TEXT 167,215,""0"",180,20,20,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "ALENCOT" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.8 mm'></xpml>G0")
            '                        oWrite.WriteLine("n")
            '                        oWrite.WriteLine("M0500")
            '                        oWrite.WriteLine("O0214")
            '                        oWrite.WriteLine("V0")
            '                        oWrite.WriteLine("t1")
            '                        oWrite.WriteLine("Kf0070")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.8 mm'></xpml>L")
            '                        oWrite.WriteLine("D11")
            '                        oWrite.WriteLine("ySPM")
            '                        oWrite.WriteLine("A2")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                            TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
            '                            TEMPREMARKS = DT.Rows(0).Item("REMARKS")
            '                        End If

            '                        oWrite.WriteLine("1911C1401710016" & TEMPREMARKS)
            '                        oWrite.WriteLine("1911C1200750149" & TEMPWIDTH)
            '                        oWrite.WriteLine("1911C1201440070" & TEMPCATEGORY)

            '                        oWrite.WriteLine("1911C1201440007Quality")
            '                        oWrite.WriteLine("1911C1201210007Design")
            '                        oWrite.WriteLine("1911C1001450063:")
            '                        oWrite.WriteLine("1911C1201210070" & ROW.Cells(gitemname.Index).Value)
            '                        oWrite.WriteLine("1911C1001210063:")
            '                        oWrite.WriteLine("1911C1200750007Mtrs")
            '                        oWrite.WriteLine("1911C1000760063:")
            '                        oWrite.WriteLine("1911C1200750070" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00"))
            '                        oWrite.WriteLine("1e4204000300000B" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1911A1200110028" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1X1100101710011P0010001016900110169017701710177")
            '                        oWrite.WriteLine("1911C1200980007Shade")
            '                        oWrite.WriteLine("1911C1000990063:")
            '                        oWrite.WriteLine("1911C1200980070" & ROW.Cells(gcolor.Index).Value)
            '                        oWrite.WriteLine("Q0001")
            '                        oWrite.WriteLine("E")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "AVIS" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.8 mm'></xpml>G0")
            '                        oWrite.WriteLine("n")
            '                        oWrite.WriteLine("M0739")
            '                        oWrite.WriteLine("O0214")
            '                        oWrite.WriteLine("V0")
            '                        oWrite.WriteLine("t1")
            '                        oWrite.WriteLine("Kf0070")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.8 mm'></xpml>L")
            '                        oWrite.WriteLine("D11")
            '                        oWrite.WriteLine("ySPM")
            '                        oWrite.WriteLine("A2")
            '                        oWrite.WriteLine("1911C1402500039Quality")
            '                        oWrite.WriteLine("1911C1402230039D. No")
            '                        oWrite.WriteLine("1911C1401950039Shade")
            '                        oWrite.WriteLine("1911C1401670039Grade")
            '                        oWrite.WriteLine("1911C1401390039Mtrs")
            '                        oWrite.WriteLine("1e6303800410038B" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1911C1200220120" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1911C1402500118:")
            '                        oWrite.WriteLine("1911C1402230118:")
            '                        oWrite.WriteLine("1911C1401950118:")
            '                        oWrite.WriteLine("1911C1401670118:")
            '                        oWrite.WriteLine("1911C1401390118:")
            '                        oWrite.WriteLine("1911C1402500141" & ROW.Cells(gitemname.Index).Value)
            '                        oWrite.WriteLine("1911C1402230141" & ROW.Cells(GDESIGN.Index).Value)
            '                        oWrite.WriteLine("1911C1401950141" & ROW.Cells(gcolor.Index).Value)
            '                        oWrite.WriteLine("1911C1401670141" & ROW.Cells(gqtyunit.Index).Value)
            '                        oWrite.WriteLine("1911C1401390141" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00"))
            '                        oWrite.WriteLine("1911C1001180141")
            '                        oWrite.WriteLine("1911C1400890039Lot No")
            '                        oWrite.WriteLine("1911C1400890118:")
            '                        oWrite.WriteLine("1911C1400890141" & ROW.Cells(GLOTNO.Index).Value)
            '                        oWrite.WriteLine("Q0001")
            '                        oWrite.WriteLine("E")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "SBA" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.8 mm'></xpml>G0")
            '                        oWrite.WriteLine("n")
            '                        oWrite.WriteLine("M0500")
            '                        oWrite.WriteLine("O0214")
            '                        oWrite.WriteLine("V0")
            '                        oWrite.WriteLine("t1")
            '                        oWrite.WriteLine("Kf0070")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.8 mm'></xpml>L")
            '                        oWrite.WriteLine("D11")
            '                        oWrite.WriteLine("ySPM")
            '                        oWrite.WriteLine("A2")
            '                        oWrite.WriteLine("1911A2401590067" & ROW.Cells(gitemname.Index).Value)
            '                        oWrite.WriteLine("1911A1001430011QUALITY")
            '                        oWrite.WriteLine("1911A1001430079:")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS, TEMPCMPSTAMP As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                            TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
            '                            TEMPREMARKS = DT.Rows(0).Item("REMARKS")
            '                        End If

            '                        oWrite.WriteLine("1911A1001430090" & TEMPCATEGORY)
            '                        oWrite.WriteLine("1911A1001240090" & TEMPREMARKS)
            '                        oWrite.WriteLine("1911A1001070090" & TEMPWIDTH)

            '                        oWrite.WriteLine("1911A1001070011WIDTH")
            '                        oWrite.WriteLine("1911A1001070079:")

            '                        oWrite.WriteLine("1911A1001070185DESIGN NO")
            '                        oWrite.WriteLine("1911A1001070267:")
            '                        oWrite.WriteLine("1911A1001070276" & ROW.Cells(GDESIGN.Index).Value)
            '                        oWrite.WriteLine("1e6304700360025B" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1911A0800220128" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1911A1000880011MTRS")
            '                        oWrite.WriteLine("1911A1000880079:")
            '                        oWrite.WriteLine("1911A1400850090" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00"))
            '                        oWrite.WriteLine("1911A1000880185SHADE")
            '                        oWrite.WriteLine("1911A1000880267:")
            '                        oWrite.WriteLine("1911A1000880276" & ROW.Cells(gcolor.Index).Value)
            '                        oWrite.WriteLine("1911A1000080140A PRODUCT OF ")
            '                        oWrite.WriteLine("1X1100000010253L117028")
            '                        oWrite.WriteLine("A1")
            '                        DT = OBJCMN.search(" ISNULL(CMPMASTER.CMP_BUSINESSLINE, '') AS CMPSTAMP", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            '                        If DT.Rows.Count > 0 Then TEMPCMPSTAMP = DT.Rows(0).Item("CMPSTAMP")
            '                        oWrite.WriteLine("1911A1800010255" & TEMPCMPSTAMP)

            '                        oWrite.WriteLine("A2")
            '                        oWrite.WriteLine("1X1100001610007L376003")

            '                        oWrite.WriteLine("Q0001")
            '                        oWrite.WriteLine("E")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "POOJA" Then

            '                        oWrite.WriteLine("SIZE 98.5 mm, 37.5 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 754,267,""1"",180,2,2,""ITEM""")
            '                        oWrite.WriteLine("TEXT 637,267,""1"",180,2,2,"":""")
            '                        oWrite.WriteLine("BARCODE 762,103,""39"",65,0,180,3,8,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 473,30,""1"",180,1,1,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 754,204,""1"",180,2,2,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 637,204,""1"",180,2,2,"":""")
            '                        oWrite.WriteLine("TEXT 750,141,""1"",180,2,2,""COLOR""")
            '                        oWrite.WriteLine("TEXT 637,141,""1"",180,2,2,"":""")
            '                        oWrite.WriteLine("TEXT 352,141,""1"",180,2,2,""MTRS""")
            '                        oWrite.WriteLine("TEXT 263,141,""1"",180,2,2,"":""")
            '                        oWrite.WriteLine("TEXT 243,162,""3"",180,2,2,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("TEXT 609,274,""1"",180,3,3,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 609,204,""1"",180,2,2,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 609,141,""1"",180,2,2,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 372,200,""1"",180,2,2,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 263,200,""1"",180,2,2,"":""")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("TEXT 239,200,""1"",180,2,2,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("BAR 37, 219, 719, 3")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "DETLINE" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.8 mm'></xpml>I8,A")
            '                        oWrite.WriteLine("q792")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("Q406,25")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.8 mm'></xpml>N")
            '                        oWrite.WriteLine("A762,304,2,1,3,3,N,""D. NO""")
            '                        oWrite.WriteLine("A595,304,2,1,3,3,N,"":""")

            '                        oWrite.WriteLine("A554,304,2,1,3,3,N,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("A762,237,2,1,3,3,N,""SHADE""")
            '                        oWrite.WriteLine("A595,237,2,1,3,3,N,"":""")
            '                        oWrite.WriteLine("A554,237,2,1,3,3,N,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("A762,173,2,1,3,3,N,""WIDTH""")
            '                        oWrite.WriteLine("A595,173,2,1,3,3,N,"":""")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("A554,173,2,1,3,3,N,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("A423,239,2,1,3,3,N,""MTRS""")
            '                        oWrite.WriteLine("A303,237,2,1,3,3,N,"":""")
            '                        oWrite.WriteLine("A266,241,2,2,3,3,N,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("B762,119,2,1,3,6,65,N,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("A647,50,2,2,2,2,N,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        'oWrite.WriteLine("A521,381,2,2,3,3,N,""")
            '                        oWrite.WriteLine("LO246,326,298,3")
            '                        oWrite.WriteLine("P1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "MYCOT" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='100.1 mm'></xpml>SIZE 97.5 mm, 100.1 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='100.1 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 757,509,""2"",180,2,2,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 757,436,""2"",180,2,2,""SHADE""")
            '                        oWrite.WriteLine("TEXT 757,366,""2"",180,2,2,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 366,366,""2"",180,2,2,""MTRS""")
            '                        oWrite.WriteLine("BARCODE 767,294,""128M"",96,0,180,4,8,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 529,188,""1"",180,2,2,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 588,509,""2"",180,2,2,"":""")
            '                        oWrite.WriteLine("TEXT 588,436,""2"",180,2,2,"":""")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("TEXT 559,366,""2"",180,2,2,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("TEXT 244,366,""2"",180,2,2,"":""")
            '                        oWrite.WriteLine("TEXT 559,509,""2"",180,2,2,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 559,436,""2"",180,2,2,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 588,366,""2"",180,2,2,"":""")
            '                        oWrite.WriteLine("TEXT 211,372,""3"",180,2,2,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "RMANILAL" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 757,377,""0"",180,16,16,""ITEM NAME""")
            '                        oWrite.WriteLine("TEXT 757,313,""0"",180,16,16,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 757,248,""0"",180,16,16,""SHADE""")
            '                        oWrite.WriteLine("TEXT 526,377,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 526,315,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 526,251,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 505,377,""0"",180,16,16,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 504,315,""0"",180,16,16,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 504,251,""0"",180,16,16,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("BARCODE 767,126,""128M"",77,0,180,4,8,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 502,44,""0"",180,16,16,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 757,184,""0"",180,16,16,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 348,184,""0"",180,16,16,""MTRS""")
            '                        oWrite.WriteLine("TEXT 526,184,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 218,184,""0"",180,16,16,"":""")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("TEXT 504,184,""0"",180,16,16,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("TEXT 190,189,""0"",180,20,20,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "SUNCOTT" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
            '                        oWrite.WriteLine("ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 754,375,""0"",180,16,16,""ITEM""")
            '                        oWrite.WriteLine("TEXT 754,316,""0"",180,16,16,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 754,258,""0"",180,16,16,""SHADE""")
            '                        oWrite.WriteLine("TEXT 754,197,""0"",180,16,16,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 338,197,""0"",180,16,16,""MTRS""")
            '                        oWrite.WriteLine("TEXT 592,375,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 592,316,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 592,258,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 592,197,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 210,197,""0"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 564,380,""0"",180,20,20,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 564,316,""0"",180,16,16,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 564,258,""0"",180,16,16,""" & ROW.Cells(gcolor.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("TEXT 564,197,""0"",180,16,16,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("TEXT 190,201,""0"",180,20,20,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("BARCODE 767,135,""128M"",74,0,180,4,8,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 503,55,""0"",180,12,12,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "MANMANDIR" Then

            '                        oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
            '                        oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        oWrite.WriteLine("DIRECTION 0,0")
            '                        oWrite.WriteLine("REFERENCE 0,0")
            '                        oWrite.WriteLine("OFFSET 0 mm")
            '                        oWrite.WriteLine("SET PEEL OFF")
            '                        oWrite.WriteLine("SET CUTTER OFF")
            '                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
            '                        oWrite.WriteLine("SET TEAR ON")
            '                        oWrite.WriteLine("CLS")
            '                        oWrite.WriteLine("CODEPAGE 1252")
            '                        oWrite.WriteLine("TEXT 754,375,""ROMAN.TTF"",180,16,16,""ITEM""")
            '                        oWrite.WriteLine("TEXT 754,318,""ROMAN.TTF"",180,16,16,""DESIGN""")
            '                        oWrite.WriteLine("TEXT 754,258,""ROMAN.TTF"",180,16,16,""SHADE""")
            '                        oWrite.WriteLine("TEXT 754,197,""ROMAN.TTF"",180,16,16,""WIDTH""")
            '                        oWrite.WriteLine("TEXT 338,197,""ROMAN.TTF"",180,16,16,""MTRS""")
            '                        oWrite.WriteLine("TEXT 592,375,""ROMAN.TTF"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 592,318,""ROMAN.TTF"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 592,258,""ROMAN.TTF"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 592,197,""ROMAN.TTF"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 210,197,""ROMAN.TTF"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 564,380,""ROMAN.TTF"",180,20,20,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 564,318,""ROMAN.TTF"",180,16,16,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 564,258,""ROMAN.TTF"",180,16,16,""" & ROW.Cells(gcolor.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("TEXT 564,197,""ROMAN.TTF"",180,16,16,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("TEXT 190,201,""ROMAN.TTF"",180,20,20,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("BARCODE 767,135,""128M"",74,0,180,4,8,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("TEXT 503,55,""ROMAN.TTF"",180,12,12,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("TEXT 358,318,""ROMAN.TTF"",180,16,16,""LOT NO""")
            '                        oWrite.WriteLine("TEXT 192,318,""ROMAN.TTF"",180,16,16,"":""")
            '                        oWrite.WriteLine("TEXT 160,318,""ROMAN.TTF"",180,16,16,""" & CMBLOTNO.Text.Trim & """")
            '                        oWrite.WriteLine("PRINT 1,1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "CC" Or ClientName = "SHREEDEV" Then

            '                        oWrite.WriteLine("I8,A")
            '                        oWrite.WriteLine("ZN")
            '                        oWrite.WriteLine("q418")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("Q203,25")
            '                        oWrite.WriteLine("N")
            '                        oWrite.WriteLine("B397,102,2,1,2,4,65,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A306,30,2,3,1,1,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE

            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(DESIGN_PURRATE,0) AS PURRATE, ISNULL(DESIGN_SALERATE,0) AS SALERATE, ISNULL(DESIGN_WRATE,0) AS WRATE", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & ROW.Cells(GDESIGN.Index).Value & "' AND DESIGN_YEARID =  " & YearId)

            '                        If DT.Rows.Count > 0 Then
            '                            If WHOLESALEBARCODE = 7 Then oWrite.WriteLine("A147,179,2,4,1,1,N,""" & Val(DT.Rows(0).Item("SALERATE")) & "/-""") Else oWrite.WriteLine("A147,179,2,4,1,1,N,""" & Val(DT.Rows(0).Item("WRATE")) / 10 & """")
            '                        Else
            '                            oWrite.WriteLine("A147,179,2,4,1,1,N,""")    'SALERATE
            '                        End If

            '                        oWrite.WriteLine("A401,175,2,2,1,1,N,""D.No""")
            '                        oWrite.WriteLine("A351,175,2,2,1,1,N,"":""")
            '                        oWrite.WriteLine("A339,179,2,3,1,1,N,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("A401,134,2,2,1,1,N,""Item""")
            '                        oWrite.WriteLine("A351,134,2,2,1,1,N,"":""")
            '                        oWrite.WriteLine("A339,139,2,3,1,1,N,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("P1")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "MNARESH" Then

            '                        oWrite.WriteLine("I8,A")
            '                        oWrite.WriteLine("ZN")
            '                        oWrite.WriteLine("q799")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("KIZZQ0")
            '                        oWrite.WriteLine("KI9+0.0")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("Q400,25")
            '                        oWrite.WriteLine("Arglabel 500 31")
            '                        oWrite.WriteLine("exit")
            '                        oWrite.WriteLine("N")
            '                        oWrite.WriteLine("A770,367,2,2,2,2,N,""ITEM""")
            '                        oWrite.WriteLine("B776,132,2,1,4,8,78,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A538,48,2,1,2,2,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A770,200,2,2,2,2,N,""WIDTH""")
            '                        oWrite.WriteLine("A651,367,2,2,2,2,N,"":""")
            '                        oWrite.WriteLine("A651,200,2,2,2,2,N,"":""")
            '                        oWrite.WriteLine("A625,367,2,2,2,2,N,""" & ROW.Cells(gitemname.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("A625,200,2,2,2,2,N,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("A289,214,2,3,3,3,N,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("A421,200,2,2,2,2,N,""MTRS""")
            '                        oWrite.WriteLine("A318,200,2,2,2,2,N,"":""")
            '                        oWrite.WriteLine("A770,256,2,2,2,2,N,""SHADE""")
            '                        oWrite.WriteLine("A651,256,2,2,2,2,N,"":""")
            '                        oWrite.WriteLine("A625,256,2,2,2,2,N,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("A770,312,2,2,2,2,N,""D.NO""")
            '                        oWrite.WriteLine("A651,312,2,2,2,2,N,"":""")
            '                        oWrite.WriteLine("A625,312,2,2,2,2,N,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("P1")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "MANINATH" Then

            '                        oWrite.WriteLine("I8,A")
            '                        oWrite.WriteLine("ZN")
            '                        oWrite.WriteLine("q812")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("Q406,25")
            '                        oWrite.WriteLine("N")
            '                        oWrite.WriteLine("A772,386,2,4,2,2,N,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("A772,310,2,3,2,2,N,""D.NO""")
            '                        oWrite.WriteLine("A772,243,2,3,2,2,N,""SHADE""")
            '                        oWrite.WriteLine("A772,174,2,3,2,2,N,""MTRS""")
            '                        oWrite.WriteLine("B772,110,2,1,3,6,67,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A592,37,2,4,1,1,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A367,174,2,3,2,2,N,""WIDTH""")
            '                        oWrite.WriteLine("A608,310,2,3,2,2,N,"":""")
            '                        oWrite.WriteLine("A608,243,2,3,2,2,N,"":""")
            '                        oWrite.WriteLine("A608,174,2,3,2,2,N,"":""")
            '                        oWrite.WriteLine("A219,174,2,3,2,2,N,"":""")
            '                        oWrite.WriteLine("A580,310,2,3,2,2,N,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("A580,243,2,3,2,2,N,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("A580,174,2,3,2,2,N,""" & Format(Val(ROW.Cells(GMTRS.Index).Value), "0.00") & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("A184,174,2,3,2,2,N,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("P1")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "DEVEN" Then

            '                        oWrite.WriteLine("I8,A")
            '                        oWrite.WriteLine("ZN")
            '                        oWrite.WriteLine("q609")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("KIZZQ0")
            '                        oWrite.WriteLine("KI9+0.0")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("Q426,25")
            '                        oWrite.WriteLine("Arglabel 533 31")
            '                        oWrite.WriteLine("exit")
            '                        oWrite.WriteLine("N")
            '                        oWrite.WriteLine("A562,385,2,2,3,3,N,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("A563,313,2,1,2,2,N,""LOT""")
            '                        oWrite.WriteLine("A456,313,2,1,2,2,N,"":""")
            '                        oWrite.WriteLine("A433,313,2,1,2,2,N,""" & CMBLOTNO.Text.Trim & """")
            '                        oWrite.WriteLine("A202,313,2,1,2,2,N,""CMS""")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("A105,313,2,1,2,2,N,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("A133,313,2,1,2,2,N,"":""")
            '                        oWrite.WriteLine("A563,259,2,1,2,2,N,""D NO""")
            '                        oWrite.WriteLine("A455,259,2,1,2,2,N,"":""")
            '                        oWrite.WriteLine("A432,259,2,1,2,2,N,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("A223,259,2,1,2,2,N,""S NO""")
            '                        oWrite.WriteLine("A104,259,2,1,2,2,N,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("A132,259,2,1,2,2,N,"":""")
            '                        oWrite.WriteLine("A563,206,2,1,2,2,N,""MTRS""")
            '                        oWrite.WriteLine("A455,206,2,1,2,2,N,"":""")
            '                        oWrite.WriteLine("A432,206,2,1,3,3,N,""" & ROW.Cells(GRECDMTRS.Index).Value & """")
            '                        oWrite.WriteLine("B583,142,2,1,3,6,89,N,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("A411,47,2,4,1,1,N,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        oWrite.WriteLine("P1")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "SANGHVI" Then

            '                        oWrite.WriteLine("I8,A")
            '                        oWrite.WriteLine("ZN")
            '                        oWrite.WriteLine("q406")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("Q305,25")
            '                        oWrite.WriteLine("N")
            '                        oWrite.WriteLine("A386,197,2,4,1,1,N,""COLOR""")
            '                        oWrite.WriteLine("A386,155,2,4,1,1,N,""MTRS""")
            '                        oWrite.WriteLine("A300,197,2,4,1,1,N,"":""")
            '                        oWrite.WriteLine("A300,155,2,4,1,1,N,"":""")
            '                        oWrite.WriteLine("A362,280,2,4,1,1,N,""TINU MINU EMBROIDERY""")
            '                        oWrite.WriteLine("A151,239,2,4,1,1,N,""WIDTH""")
            '                        oWrite.WriteLine("A277,197,2,4,1,1,N,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        oWrite.WriteLine("A277,155,2,4,1,1,N,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("A51,239,2,4,1,1,N,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("A67,239,2,4,1,1,N,"":""")
            '                        oWrite.WriteLine("B390,112,2,1,2,4,63,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A313,43,2,4,1,1,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A386,239,2,4,1,1,N,""D.NO""")
            '                        oWrite.WriteLine("A300,239,2,4,1,1,N,"":""")
            '                        oWrite.WriteLine("A278,239,2,4,1,1,N,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("A151,155,2,4,1,1,N,""" & ROW.Cells(GBALENO.Index).Value & """")
            '                        oWrite.WriteLine("P1")
            '                        oWrite.Dispose()


            '                    ElseIf ClientName = "MJFABRIC" Then

            '                        oWrite.WriteLine("I8,A")
            '                        oWrite.WriteLine("ZN")
            '                        oWrite.WriteLine("q799")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("Q400,25")
            '                        oWrite.WriteLine("N")
            '                        oWrite.WriteLine("A774,312,2,2,2,2,N,""QUALITY""")
            '                        oWrite.WriteLine("A774,365,2,2,2,2,N,""DESIGN""")
            '                        oWrite.WriteLine("A774,252,2,2,2,2,N,""SHADE""")
            '                        oWrite.WriteLine("A774,193,2,2,2,2,N,""WIDTH""")
            '                        oWrite.WriteLine("A355,193,2,2,2,2,N,""MTRS""")
            '                        oWrite.WriteLine("B782,141,2,1,4,8,90,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A543,45,2,1,2,2,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A598,365,2,2,2,2,N,"":""")
            '                        oWrite.WriteLine("A598,312,2,2,2,2,N,"":""")
            '                        oWrite.WriteLine("A598,252,2,2,2,2,N,"":""")
            '                        oWrite.WriteLine("A598,193,2,2,2,2,N,"":""")
            '                        oWrite.WriteLine("A247,193,2,2,2,2,N,"":""")
            '                        oWrite.WriteLine("A558,365,2,2,2,2,N,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("A558,314,2,2,2,2,N,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("A558,254,2,2,2,2,N,""" & ROW.Cells(gcolor.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        End If

            '                        oWrite.WriteLine("A558,193,2,2,2,2,N,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("A213,205,2,4,2,2,N,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("P1")
            '                        oWrite.Dispose()


            '                    ElseIf ClientName = "KDFAB" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='101.6 mm'></xpml>I8,A")
            '                        oWrite.WriteLine("ZN")
            '                        oWrite.WriteLine("q792")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("KIZZQ0")
            '                        oWrite.WriteLine("KI9+0.0")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("Q812,25")
            '                        oWrite.WriteLine("Arglabel 1016 31")
            '                        oWrite.WriteLine("exit")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='101.6 mm'></xpml>N")
            '                        oWrite.WriteLine("B761,309,2,1,3,6,161,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A717,134,2,4,2,2,N,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        oWrite.WriteLine("A754,563,2,3,2,2,N,""NAME""")
            '                        oWrite.WriteLine("A754,477,2,3,2,2,N,""D.NO""")
            '                        oWrite.WriteLine("A754,391,2,3,2,2,N,""MTRS""")
            '                        oWrite.WriteLine("A637,563,2,3,2,2,N,"":""")
            '                        oWrite.WriteLine("A637,477,2,3,2,2,N,"":""")
            '                        oWrite.WriteLine("A637,391,2,3,2,2,N,"":""")
            '                        oWrite.WriteLine("A606,563,2,3,2,2,N,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("A606,477,2,3,2,2,N,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("A595,400,2,2,4,4,N,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

            '                        oWrite.WriteLine("A113,477,2,3,2,2,N,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("A294,391,2,3,2,2,N,""" & ROW.Cells(GBALENO.Index).Value & """")
            '                        oWrite.WriteLine("P1")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "BRILLANTO" Then

            '                        oWrite.WriteLine("I8,A,001")
            '                        oWrite.WriteLine("")
            '                        oWrite.WriteLine("")
            '                        oWrite.WriteLine("Q384,024")
            '                        oWrite.WriteLine("q863")
            '                        oWrite.WriteLine("rN")
            '                        oWrite.WriteLine("S3")
            '                        oWrite.WriteLine("D14")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("R253,0")
            '                        oWrite.WriteLine("f100")
            '                        oWrite.WriteLine("N")
            '                        oWrite.WriteLine("A341,164,2,3,1,1,N,""Grade""")
            '                        oWrite.WriteLine("A342,202,2,3,1,1,N,""Shade No.""")
            '                        oWrite.WriteLine("A344,238,2,3,1,1,N,""Width""")
            '                        oWrite.WriteLine("A344,274,2,3,1,1,N,""Mtrs""")
            '                        oWrite.WriteLine("A342,309,2,3,1,1,N,""M. Name""")
            '                        oWrite.WriteLine("A213,164,2,3,1,1,N,"":""")
            '                        oWrite.WriteLine("A213,202,2,3,1,1,N,"":""")
            '                        oWrite.WriteLine("A213,238,2,3,1,1,N,"":""")
            '                        oWrite.WriteLine("A213,274,2,3,1,1,N,"":""")
            '                        oWrite.WriteLine("A213,309,2,3,1,1,N,"":""")
            '                        oWrite.WriteLine("A213,345,2,3,1,1,N,"":""")
            '                        oWrite.WriteLine("A198,164,2,3,1,1,N,""FRESH""")
            '                        oWrite.WriteLine("A198,202,2,3,1,1,N,""" & ROW.Cells(gcolor.Index).Value & """")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                        oWrite.WriteLine("A198,238,2,3,1,1,N,""" & TEMPWIDTH & """")
            '                        oWrite.WriteLine("A111,273,2,3,1,1,N,""" & ROW.Cells(gqtyunit.Index).Value & """")

            '                        oWrite.WriteLine("A198,274,2,3,1,1,N,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        oWrite.WriteLine("A198,309,2,3,1,1,N,""" & ROW.Cells(gitemname.Index).Value & """")
            '                        oWrite.WriteLine("A198,345,2,3,1,1,N,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        oWrite.WriteLine("A342,345,2,3,1,1,N,""Design No""")
            '                        oWrite.WriteLine("B352,122,2,1,2,6,81,B,""" & ROW.Cells(GBARCODE.Index).Value & """")

            '                        oWrite.WriteLine("P1")
            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "MIDAS" Then

            '                        oWrite.WriteLine("<xpml><page quantity='0' pitch='38.2 mm'></xpml>G0")
            '                        oWrite.WriteLine("n")
            '                        oWrite.WriteLine("M0500")
            '                        oWrite.WriteLine("O0214")
            '                        oWrite.WriteLine("V0")
            '                        oWrite.WriteLine("t1")
            '                        oWrite.WriteLine("Kf0070")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='38.2 mm'></xpml>L")
            '                        oWrite.WriteLine("D11")
            '                        oWrite.WriteLine("ySPM")
            '                        oWrite.WriteLine("A2")
            '                        oWrite.WriteLine("1911C1401220034" & ROW.Cells(gitemname.Index).Value)
            '                        oWrite.WriteLine("1911A1001000012D. NO")
            '                        oWrite.WriteLine("1X1100001190011L226001")
            '                        oWrite.WriteLine("1911A1000800012SHADE")
            '                        oWrite.WriteLine("1911A1000600012MTRS")
            '                        oWrite.WriteLine("1e4203200240011B" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1911A0800100062" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1911A1001000074:")
            '                        oWrite.WriteLine("1911A1000800074:")
            '                        oWrite.WriteLine("1911A1000600074:")
            '                        oWrite.WriteLine("1911A1001000086" & ROW.Cells(GDESIGN.Index).Value)
            '                        oWrite.WriteLine("1911A1000800086" & ROW.Cells(gcolor.Index).Value)
            '                        oWrite.WriteLine("1911C1200590086" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00"))
            '                        ''''neha
            '                        oWrite.WriteLine("1911A1000600164PCS-" & Format(Val(ROW.Cells(gQty.Index).Value), "0"))
            '                        oWrite.WriteLine("Q0001")
            '                        oWrite.WriteLine("E")
            '                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")

            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "TCOT" Then

            '                        oWrite.WriteLine("G0")
            '                        oWrite.WriteLine("n")
            '                        oWrite.WriteLine("M0690")
            '                        oWrite.WriteLine("O0214")
            '                        oWrite.WriteLine("V0")
            '                        oWrite.WriteLine("t1")
            '                        oWrite.WriteLine("Kf0070")
            '                        oWrite.WriteLine("L")
            '                        oWrite.WriteLine("D11")
            '                        oWrite.WriteLine("ySPM")
            '                        oWrite.WriteLine("A2")
            '                        oWrite.WriteLine("1911C1202480037QLTY")
            '                        oWrite.WriteLine("1911C1202250037DSGN.NO")
            '                        oWrite.WriteLine("1911C1202040037CH.NO.")
            '                        oWrite.WriteLine("1911C1201820037SHD.NO.")
            '                        oWrite.WriteLine("1911C1201600037LOT NO")
            '                        oWrite.WriteLine("1911C1201380037WIDTH")
            '                        oWrite.WriteLine("1911C1201160037MTRS")
            '                        oWrite.WriteLine("1911C1200940037GRADE")
            '                        oWrite.WriteLine("1911C1200710037RACK")
            '                        oWrite.WriteLine("1911C1202480124:")
            '                        oWrite.WriteLine("1911C1202250124:")
            '                        oWrite.WriteLine("1911C1202040124:")
            '                        oWrite.WriteLine("1911C1201820124:")
            '                        oWrite.WriteLine("1911C1201600124:")
            '                        oWrite.WriteLine("1911C1200940124:")
            '                        oWrite.WriteLine("1911C1201160124:")
            '                        oWrite.WriteLine("1911C1201380124:")
            '                        oWrite.WriteLine("1911C1200710124:")
            '                        oWrite.WriteLine("1e6303300310036B" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1911C1200110114" & ROW.Cells(GBARCODE.Index).Value)
            '                        oWrite.WriteLine("1911C1202480138" & ROW.Cells(gitemname.Index).Value)
            '                        oWrite.WriteLine("1911C1202250138" & ROW.Cells(GDESIGN.Index).Value)
            '                        oWrite.WriteLine("1911C1202040138" & ROW.Cells(GBALENO.Index).Value)
            '                        oWrite.WriteLine("1911C1201820138" & ROW.Cells(gcolor.Index).Value)
            '                        oWrite.WriteLine("1911C1201600138" & CMBLOTNO.Text.Trim)

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH As String = ""
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")


            '                        oWrite.WriteLine("1911C1201380138" & TEMPWIDTH)
            '                        oWrite.WriteLine("1911C1201160138" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00"))
            '                        oWrite.WriteLine("1911C1200710138" & ROW.Cells(GRACK.Index).Value)
            '                        oWrite.WriteLine("1911C1200940138FRESH")
            '                        oWrite.WriteLine("Q0001")
            '                        oWrite.WriteLine("E")

            '                        oWrite.Dispose()

            '                    ElseIf ClientName = "SAFFRON" Then

            '                        'PRINT ALL PIECES
            '                        'If ROW.Cells(GPIECETYPE.Index).Value <> "FRESH" Then GoTo NEXTLINE

            '                        oWrite.WriteLine("I8,A,001")
            '                        oWrite.WriteLine("")
            '                        oWrite.WriteLine("")
            '                        oWrite.WriteLine("Q400,024")
            '                        oWrite.WriteLine("q831")
            '                        oWrite.WriteLine("rN")
            '                        oWrite.WriteLine("S5")
            '                        oWrite.WriteLine("D2")
            '                        oWrite.WriteLine("ZT")
            '                        oWrite.WriteLine("JF")
            '                        oWrite.WriteLine("O")
            '                        oWrite.WriteLine("R136,0")
            '                        oWrite.WriteLine("f100")
            '                        oWrite.WriteLine("N")

            '                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
            '                        Dim TEMPWIDTH, TEMPCONTAIN As String
            '                        Dim OBJCMN As New ClsCommon
            '                        Dim DT As DataTable = OBJCMN.search(" ISNULL(CATEGORYMASTER.category_remarks, '') AS WIDTH, ISNULL(ITEMMASTER.item_remarks, '') AS CONTAIN, ISNULL(ITEMMASTER.item_DISPLAYNAME, '') AS DISPLAYNAME, ISNULL(HSN_CODE,'') AS HSNCODE ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id  LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID", " AND ITEM_NAME = '" & ROW.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & YearId)
            '                        If DT.Rows.Count > 0 Then
            '                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
            '                            TEMPCONTAIN = DT.Rows(0).Item("CONTAIN")
            '                        End If

            '                        oWrite.WriteLine("A419,146,0,1,3,3,N,""" & DT.Rows(0).Item("HSNCODE") & """")    'HSNCODE
            '                        oWrite.WriteLine("A151,154,0,1,2,2,N,""" & TEMPWIDTH & """")    'GIVE ITEM CATEGORY'S REMARKS
            '                        oWrite.WriteLine("A133,102,0,1,3,3,N,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")    'MTRS
            '                        oWrite.WriteLine("A459,104,0,1,3,3,N,""" & ROW.Cells(gcolor.Index).Value & """")       'COLOR
            '                        oWrite.WriteLine("A8,6,0,1,3,3,N,""" & DT.Rows(0).Item("DISPLAYNAME") & """")       'QUALITY
            '                        oWrite.WriteLine("A171,199,0,1,2,2,N,""" & TEMPCONTAIN & """")        'ITEMREMARKS
            '                        oWrite.WriteLine("A231,57,0,1,3,3,N,""" & ROW.Cells(gitemname.Index).Value & """")      'ITEMNAME
            '                        oWrite.WriteLine("A11,200,0,1,2,2,N,""Contain:""")
            '                        oWrite.WriteLine("A318,154,0,1,2,2,N,""HSN :""")
            '                        oWrite.WriteLine("A318,111,0,1,2,2,N,""Shade :""")
            '                        oWrite.WriteLine("A11,153,0,1,2,2,N,""Width :""")
            '                        oWrite.WriteLine("A11,60,0,1,2,2,N,""Design No :""")
            '                        oWrite.WriteLine("A11,107,0,1,2,2,N,""Mtrs :""")
            '                        oWrite.WriteLine("B8,257,0,1,2,6,87,B,""" & ROW.Cells(GBARCODE.Index).Value & """")       'BARCODE
            '                        oWrite.WriteLine("P1")

            '                        oWrite.Dispose()

            '                        'Else

            '                        '    oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 99.10 mm, 50 mm")
            '                        '    oWrite.WriteLine("GAP 3 mm, 0 mm")
            '                        '    oWrite.WriteLine("SET RIBBON ON")
            '                        '    oWrite.WriteLine("DIRECTION 0,0")
            '                        '    oWrite.WriteLine("REFERENCE 0,0")
            '                        '    oWrite.WriteLine("OFFSET 0 mm")
            '                        '    oWrite.WriteLine("SET PEEL OFF")
            '                        '    oWrite.WriteLine("SET CUTTER OFF")
            '                        '    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
            '                        '    oWrite.WriteLine("CLS")
            '                        '    oWrite.WriteLine("CODEPAGE 1252")
            '                        '    oWrite.WriteLine("TEXT 758,273,""0"",180,16,16,""D.NO""")
            '                        '    oWrite.WriteLine("TEXT 646,50,""0"",180,25,14,""" & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
            '                        '    oWrite.WriteLine("TEXT 661,373,""0"",180,24,16,""" & CmpName & """")    'cmpname
            '                        '    oWrite.WriteLine("TEXT 625,326,""0"",180,18,10,""MEELON SILK MILLS""")   'barcode
            '                        '    oWrite.WriteLine("TEXT 622,274,""0"",180,16,16,"":""")
            '                        '    oWrite.WriteLine("TEXT 622,215,""0"",180,16,16,"":""")
            '                        '    oWrite.WriteLine("TEXT 214,217,""0"",180,16,16,"":""")
            '                        '    oWrite.WriteLine("TEXT 582,273,""0"",180,16,16,""" & ROW.Cells(GDESIGN.Index).Value & """")
            '                        '    oWrite.WriteLine("TEXT 366,214,""0"",180,16,16,""SHADE""")
            '                        '    oWrite.WriteLine("TEXT 190,214,""0"",180,16,16,""" & ROW.Cells(gcolor.Index).Value & """")
            '                        '    oWrite.WriteLine("TEXT 758,214,""0"",180,16,16,""MTRS""")
            '                        '    oWrite.WriteLine("TEXT 582,214,""0"",180,16,16,""" & Format(Val(ROW.Cells(GRECDMTRS.Index).Value), "0.00") & """")
            '                        '    oWrite.WriteLine("BARCODE 698,159,""128M"",104,0,180,3,6,""" & ROW.Cells(GBARCODE.Index).Value & """")
            '                        '    oWrite.WriteLine("PRINT 1,1")
            '                        '    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")

            '                        '    oWrite.Dispose()
            '                    End If


            '                    'Printing Barcode
            '                    Dim psi As New ProcessStartInfo()
            '                    psi.FileName = "cmd.exe"
            '                    psi.RedirectStandardInput = False
            '                    psi.RedirectStandardOutput = True
            '                    'psi.Arguments = "/c print " & Application.StartupPath & "\Barcode.txt"    ' specify your command
            '                    psi.Arguments = "/c print D:\Barcode.txt"    ' specify your command
            '                    psi.UseShellExecute = False

            '                    Dim proc As Process
            '                    proc = Process.Start(psi)
            '                    dirresults = proc.StandardOutput.ReadToEnd() ' // read from stdout
            '                    '// do something with result stream
            '                    proc.WaitForExit()
            '                    proc.Dispose()

            'NEXTLINE:
            '                    'THIS LINE IS WRITTEN TO DISPOSE THE BARCODE NOTEPAD OBJECT, WHEN CURSOR COMES DIRECTLY ON NEXTLINE CODE
            '                    oWrite.Dispose()

            '                Next
            '            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If ClientName = "SNCM" AndAlso MsgBox("Wish to Print Packing Slip", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJPS As New MATRECDesign
                OBJPS.MdiParent = MDIMain
                OBJPS.FRMSTRING = "MATRECPS"
                OBJPS.BLANKPAPER = CHKBLANKPAPER.Checked
                OBJPS.WHERECLAUSE = "{MATERIALRECEIPT.MATREC_no}=" & Val(TXTMATRECNO.Text.Trim) & " AND {MATERIALRECEIPT.MATREC_yearid}=" & YearId

                If (Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0) AndAlso Val(TXTFROM.Text.Trim) <= Val(TXTTO.Text.Trim) Then
                    OBJPS.WHERECLAUSE = OBJPS.WHERECLAUSE & " AND {MATERIALRECEIPT_DESC.MATREC_GRIDSRNO}>=" & Val(TXTFROM.Text.Trim) & " AND {MATERIALRECEIPT_DESC.MATREC_GRIDSRNO}<=" & Val(TXTTO.Text.Trim)
                End If

                OBJPS.Show()
            End If

            If MsgBox("Wish to Print Dyeing Receipt...?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub
            Dim OBJMATREC As New MATRECDesign
            OBJMATREC.MdiParent = MDIMain
            OBJMATREC.FRMSTRING = "MATREC"
            OBJMATREC.WHERECLAUSE = "{MATERIALRECEIPT.MATREC_no}=" & Val(TXTMATRECNO.Text.Trim) & " AND {MATERIALRECEIPT.MATREC_yearid}=" & YearId
            OBJMATREC.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MaterialReceipt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If ERRORVALID() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNoCancel)
                If tempmsg = vbCancel Then Exit Sub
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'Grid Focus
            GRIDMATREC.Focus()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1 Then       'for Delete
            TabControl1.SelectedIndex = (0)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2 Then       'for Delete
            TabControl1.SelectedIndex = (1)
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            toolprevious_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.P And e.Alt = True Then
            Call PrintToolStripButton_Click(sender, e)
        End If
    End Sub

    Private Sub MaterialReceipt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            If ClientName = "OWAIS" Then ALLOWMANUALMATRECNO = True
            CLEAR()

            If ClientName = "SVS" Then
                txtqty.Text = "1"
                txtqty.BackColor = Color.Linen
                txtqty.ReadOnly = True
            End If

            CMBPIECETYPE.Text = "FRESH"


            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If


                Dim OBJMATREC As New ClsMaterialReceipt()
                Dim dttable As New DataTable

                dttable = OBJMATREC.SELECTMATREC(TEMPMATRECNO, CmpId, Locationid, YearId)

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTMATRECNO.Text = TEMPMATRECNO
                        MATRECDATE.Text = Format(Convert.ToDateTime(dr("MATRECDATE")).Date, "dd/MM/yyyy")
                        CHALLANDATE.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")
                        TXTCHALLANNO.Text = dr("CHALLANNO")
                        PARTYCHALLANNO = TXTCHALLANNO.Text.Trim

                        cmbGodown.Text = Convert.ToString(dr("GODOWN").ToString)
                        cmbname.Text = Convert.ToString(dr("NAME").ToString)
                        cmbname.Enabled = False
                        CMBLOTNO.Text = dr("LOTNO")
                        TXTPROGRAMNO.Text = Val(dr("PROGRAMNO"))

                        TXTCHECKPCS.Text = Val(dr("CHKPCS"))
                        TXTCHECKMTRS.Text = Val(dr("CHKMTRS"))

                        TXTCHECKNO.Text = Val(dr("CHKNO"))

                        cmbtrans.Text = dr("TRANSNAME").ToString
                        txtlrno.Text = dr("LRNO").ToString
                        lrdate.Text = Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy")

                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                        TXTFROMNO.Text = dr("FROMNO")
                        TXTFROMTYPE.Text = dr("FROMTYPE")
                        CHKCONTDESIGNRECD.Checked = Convert.ToBoolean(dr("CONTDESIGNRECD"))
                        CHKRETURN.Checked = Convert.ToBoolean(dr("FORRETURN"))
                        CMBPARTYNAME.Text = Convert.ToString(dr("PARTYNAME").ToString)



                        'IF BILL RAILED THEN LOCK
                        If Convert.ToBoolean(dr("DONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If


                        'Item Grid
                        GRIDMATREC.Rows.Add(dr("GRIDSRNO").ToString, dr("GRIDLOTNO"), dr("CHECKSRNO"), dr("PIECETYPE").ToString, dr("ITEMNAME").ToString, dr("GRIDDESC").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGN").ToString, dr("COLOR"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("WT")), "0.00"), Format(Val(dr("qty")), "0.00"), dr("UNIT").ToString, Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RECDMTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER").ToString, Format(Val(dr("AMOUNT")), "0.00"), dr("RACK").ToString, dr("SHELF").ToString, dr("YESNO").ToString, Format(Val(dr("DIFF")), "0.00"), dr("BARCODE"), dr("GRIDDONE").ToString, dr("OUTPCS"), dr("OUTMTRS"), dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"), dr("PCSNO"))

                        If Val(dr("OUTMTRS")) > 0 Then
                            GRIDMATREC.Rows(GRIDMATREC.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        If Convert.ToBoolean(dr("INHOUSECHECKDONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If
                        CHKMANUALRATE.Checked = Convert.ToBoolean(dr("MANUALRATE"))

                        If UserName = "Admin" Then
                            If CHKMANUALRATE.CheckState = CheckState.Checked Then
                                TXTRATE.ReadOnly = False
                                GRATE.ReadOnly = False
                            End If
                        End If

                    Next

                    TOTAL()
                Else
                    EDIT = False
                    CLEAR()
                End If

                'UPLOAD
                Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.SEARCH(" MATREC_GRIDSRNO AS GRIDSRNO, MATREC_REMARKS AS REMARKS, MATREC_NAME AS NAME, MATREC_IMGPATH AS IMGPATH, MATREC_NEWIMGPATH AS NEWIMGPATH", "", " MATERIALRECEIPT_UPLOAD", " AND MATREC_NO = " & TEMPMATRECNO & " AND MATREC_CMPID = " & CmpId & " AND MATREC_LOCATIONID = " & Locationid & " AND MATREC_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), DTR("IMGPATH"), DTR("NEWIMGPATH"))
                    Next
                End If


                'ORDER GRID
                'Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.SEARCH(" MATERIALRECEIPT_PROGDETAILS.MATREC_GRIDSRNO AS GRIDSRNO, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(MATERIALRECEIPT_PROGDETAILS.MATREC_ORDERMTRS,0) AS ORDERMTRS, MATERIALRECEIPT_PROGDETAILS.MATREC_FROMNO AS FROMNO, MATERIALRECEIPT_PROGDETAILS.MATREC_FROMSRNO AS FROMSRNO, MATERIALRECEIPT_PROGDETAILS.MATREC_FROMTYPE AS FROMTYPE, MATERIALRECEIPT_PROGDETAILS.MATREC_PCS AS GDNQTY, ISNULL(MATERIALRECEIPT_PROGDETAILS.MATREC_MTRS,0) AS GDNMTRS, ISNULL(MATERIALRECEIPT_PROGDETAILS.MATREC_RATE,0) AS RATE ", "", " MATERIALRECEIPT_PROGDETAILS INNER JOIN ITEMMASTER ON MATERIALRECEIPT_PROGDETAILS.MATREC_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON MATERIALRECEIPT_PROGDETAILS.MATREC_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON MATERIALRECEIPT_PROGDETAILS.MATREC_DESIGNID = DESIGNMASTER.DESIGN_id  ", " AND MATERIALRECEIPT_PROGDETAILS.MATREC_NO = " & TEMPMATRECNO & " AND MATERIALRECEIPT_PROGDETAILS.MATREC_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        GRIDORDER.Rows.Add(Val(DTR("GRIDSRNO")), DTR("ITEMNAME"), DTR("DESIGNNO"), DTR("COLOR"), Val(DTR("ORDERMTRS")), Val(DTR("FROMNO")), Val(DTR("FROMSRNO")), DTR("FROMTYPE"), Val(DTR("GDNQTY")), Val(DTR("GDNMTRS")), Val(DTR("RATE")))
                    Next
                End If
                getsrno(GRIDORDER)


                chkchange.CheckState = CheckState.Checked

                Dim OBJCM As New ClsCommon
                Dim DT As DataTable = OBJCM.SEARCH(" MATREC_DSRNO AS DSRNO, MATREC_METERS AS DMTRS, MATREC_MAINSRNO AS MAINSRNO ", "", " MATERIALRECEIPT_MTRS ", " AND MATERIALRECEIPT_MTRS.MATREC_NO = " & TEMPMATRECNO & " AND MATERIALRECEIPT_MTRS.MATREC_YEARID=" & YearId & " ORDER BY  MATERIALRECEIPT_MTRS.MATREC_DSRNO ")
                For Each DR As DataRow In DT.Rows
                    DT_MTRSDETAILS.Rows.Add(Val(DR("DSRNO")), Format(Val(DR("DMTRS")), "0.00"), Val(DR("MAINSRNO")))
                Next

            End If

            If GRIDMATREC.RowCount > 0 Then
                txtsrno.Text = Val(GRIDMATREC.Rows(GRIDMATREC.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If

            If ClientName = "MONOGRAM" Then
                TXTRATE.TabStop = True
                TXTRATE.ReadOnly = False
            End If


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Sub FILLCMB()
        Try
            fillGODOWN(cmbGodown, EDIT)
            FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
            fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            fillQUALITY(CMBQUALITY, EDIT)
            FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
            fillunit(cmbqtyunit)
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
            FILLRACK(CMBRACK)
            FILLSHELF(CMBSHELF)
            If CMBPIECETYPE.Text.Trim = "" Then fillPIECETYPE(CMBPIECETYPE)

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("CMP_NAME AS CMPNAME", "", "CMPMASTER", " AND CMP_ID <> " & CmpId)
            For Each ROW As DataRow In DT.Rows
                CMBCMPNAME.Items.Add(ROW("CMPNAME"))
            Next

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_ENTER(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGodown.Enter
        Try
            If cmbGodown.Text.Trim = "" Then fillGODOWN(cmbGodown, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbGodown.Validating
        Try
            If cmbGodown.Text.Trim <> "" Then GODOWNVALIDATE(cmbGodown, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub CMBSHELF_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSHELF.Validating
        Try
            If CMBSHELF.Text.Trim <> "" Then SHELFVALIDATE(CMBSHELF, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJMATREC As New MaterialReceiptDetails
            OBJMATREC.MdiParent = MDIMain
            OBJMATREC.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then NAMEVALIDATE(cmbtrans, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "Sundry Creditors")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDMATREC.RowCount = 0
                TEMPMATRECNO = Val(tstxtbillno.Text)
                If TEMPMATRECNO > 0 Then
                    EDIT = True
                    MaterialReceipt_Load(sender, e)
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

        GRIDMATREC.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDMATREC.Rows.Add(Val(txtsrno.Text.Trim), CMBGRIDLOTNO.Text.Trim, CMBCHECKSRNO.Text.Trim, CMBPIECETYPE.Text.Trim, cmbitemname.Text.Trim, TXTGRIDDESC.Text.Trim, CMBQUALITY.Text.Trim, TXTBALENO.Text.Trim, CMBDESIGN.Text.Trim, cmbcolor.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTWT.Text.Trim), "0.00"), Format(Val(txtqty.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRECDMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMOUNT.Text.Trim), "0.00"), CMBRACK.Text.Trim, CMBSHELF.Text.Trim, CMBYESNO.Text.Trim, Format(Val(TXTDIFF.Text.Trim), "0.00"), TXTBARCODE.Text.Trim, 0, 0, 0, TXTFROMNO.Text.Trim, CMBCHECKSRNO.Text.Trim, TXTFROMTYPE.Text.Trim, TXTPCSNO.Text.Trim)
            getsrno(GRIDMATREC)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDMATREC.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDMATREC.Item(GLOTNO.Index, TEMPROW).Value = CMBGRIDLOTNO.Text.Trim
            GRIDMATREC.Item(GCHECKSRNO.Index, TEMPROW).Value = Val(CMBCHECKSRNO.Text.Trim)
            GRIDMATREC.Item(GPIECETYPE.Index, TEMPROW).Value = CMBPIECETYPE.Text.Trim
            GRIDMATREC.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
            GRIDMATREC.Item(GGRIDDESC.Index, TEMPROW).Value = TXTGRIDDESC.Text.Trim
            GRIDMATREC.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDMATREC.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
            GRIDMATREC.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDMATREC.Item(gcolor.Index, TEMPROW).Value = cmbcolor.Text.Trim
            GRIDMATREC.Item(gcut.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
            GRIDMATREC.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.00")
            GRIDMATREC.Item(gQty.Index, TEMPROW).Value = Format(Val(txtqty.Text.Trim), "0")
            GRIDMATREC.Item(gqtyunit.Index, TEMPROW).Value = cmbqtyunit.Text.Trim
            GRIDMATREC.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDMATREC.Item(GRECDMTRS.Index, TEMPROW).Value = Format(Val(TXTRECDMTRS.Text.Trim), "0.00")
            GRIDMATREC.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")
            GRIDMATREC.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
            GRIDMATREC.Item(GAMOUNT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")
            GRIDMATREC.Item(GRACK.Index, TEMPROW).Value = CMBRACK.Text.Trim
            GRIDMATREC.Item(GSHELF.Index, TEMPROW).Value = CMBSHELF.Text.Trim
            GRIDMATREC.Item(GYESNO.Index, TEMPROW).Value = CMBYESNO.Text.Trim
            GRIDMATREC.Item(GDIFF.Index, TEMPROW).Value = Format(Val(TXTDIFF.Text.Trim), "0.00")


            'WE WILL REMOVE THE DATA AND REINSERT, THIS IS CODE FOR REMOAL, FOR INSERTING WE HAVE ENTERED CODE BELOW
            If ClientName = "SNCM" And EDIT = False Then
LINE1:
                For I As Integer = 0 To DT_MTRSDETAILS.Rows.Count - 1
                    If GRIDMATREC.Rows(GRIDMATREC.CurrentRow.Index).Cells(gsrno.Index).Value = Val(DT_MTRSDETAILS.Rows(I).Item("MAINSRNO")) Then
                        DT_MTRSDETAILS.Rows.RemoveAt(I)
                        GoTo LINE1
                    End If
                Next
            End If



            If ClientName <> "SNCM" Then GRIDDOUBLECLICK = False
            CMBCHECKSRNO.Enabled = True
        End If

        TOTAL()
        'FILLGRIDMTRS(TXTRECDMTRS.Text.Trim, txtsrno.Text.Trim)
        If ClientName <> "SNCM" Then GRIDMATREC.FirstDisplayedScrollingRowIndex = GRIDMATREC.RowCount - 1
        txtsrno.Text = GRIDMATREC.RowCount + 1

        'cmbitemname.Text = ""
        If ClientName = "SOFTAS" Then CMBQUALITY.Text = ""
        If ClientName = "MAHAVIRPOLYCOT" Then
            CMBCHECKSRNO.Text = Val(CMBCHECKSRNO.Text.Trim) + 1
            CMBDESIGN.Text = ""
            cmbcolor.Text = ""
        End If

        If ClientName <> "KOTHARI" And ClientName <> "KOTHARINEW" Then TXTCUT.Clear()
        If ClientName <> "DILIP" And ClientName <> "DILIPNEW" And ClientName <> "SUBHLAXMI" And ClientName <> "SUPEEMA" And ClientName <> "SARAYU" Then TXTBALENO.Clear()
        TXTWT.Clear()
        TXTMTRS.Clear()
        TXTRECDMTRS.Clear()
        If ClientName <> "SHREENAKODA" Then CMBRACK.Text = ""
        CMBSHELF.Text = ""
        CMBYESNO.SelectedIndex = 0
        TXTDIFF.Clear()
        TXTPCSNO.Clear()
        If ClientName = "AVIS" Or ClientName = "SUPRIYA" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "SOFTAS" Or ClientName = "SHREENAKODA" Then
            TXTRECDMTRS.Focus()
        ElseIf ClientName = "DILIP" Or ClientName = "DILIPNEW" Or ClientName = "SUBHLAXMI" Or ClientName = "OWAIS" Or ClientName = "MAHAJAN" Then
            TXTBALENO.Text = Val(TXTBALENO.Text.Trim) + 1
            txtqty.Focus()
        ElseIf ClientName = "SNCM" Then

            GRIDMTRS.EndEdit() '
            ' Remove all rows for the current entry before adding new ones
            For Each MTRSROW1 As DataGridViewRow In GRIDMTRS.Rows
                Dim currentMainSrNo As Object = MTRSROW1.Cells(GMAINSRNO.Index).Value
                For i As Integer = DT_MTRSDETAILS.Rows.Count - 1 To 0 Step -1
                    If DT_MTRSDETAILS.Rows(i)("MAINSRNO") = currentMainSrNo Then
                        DT_MTRSDETAILS.Rows.RemoveAt(i)
                    End If
                Next

                ' Now add new rows for this entry as usual
                For Each MTRSROW As DataGridViewRow In GRIDMTRS.Rows
                    If Not MTRSROW.IsNewRow Then
                        Dim newRow As DataRow = DT_MTRSDETAILS.NewRow()
                        newRow("DSRNO") = MTRSROW.Cells(DSRNO.Index).Value
                        newRow("DMTRS") = MTRSROW.Cells(DMTRS.Index).Value
                        newRow("MAINSRNO") = currentMainSrNo
                        DT_MTRSDETAILS.Rows.Add(newRow)
                    End If
                Next
            Next

            txtqty.Clear()
            TXTBALENO.Focus()
            GRIDDOUBLECLICK = False
        Else
            txtsrno.Focus()
        End If


        GRIDMTRS.RowCount = 0
        TXTDSRNO.Text = GRIDMTRS.RowCount + 1

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
        TXTNEWIMGPATH.Text = Application.StartupPath & "\UPLOADDOCS\" & TXTMATRECNO.Text.Trim & txtuploadsrno.Text.Trim & TXTFILENAME.Text.Trim
        On Error Resume Next

        If txtimgpath.Text.Trim.Length <> 0 Then
            PBSoftCopy.ImageLocation = txtimgpath.Text.Trim
            PBSoftCopy.Load(txtimgpath.Text.Trim)
            txtuploadsrno.Focus()
        End If
    End Sub

    Private Sub txtuploadname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
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

    Private Sub gridupload_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
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

    Private Sub txtuploadsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuploadsrno.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(GGRIDUPLOADSRNO.Index).Value) + 1
            Else
                txtuploadsrno.Text = 1
            End If
        End If
    End Sub

    Private Sub GRIDMATREC_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDMATREC.CellDoubleClick
        If GRIDMATREC.CurrentRow.DefaultCellStyle.BackColor <> Color.Yellow Then
            EDITROW()
        Else
            MsgBox("This Row Is Locked")
        End If
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDMATREC.RowCount = 0
LINE1:
            TEMPMATRECNO = Val(TXTMATRECNO.Text) - 1
            If TEMPMATRECNO > 0 Then
                EDIT = True
                MaterialReceipt_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDMATREC.RowCount = 0 And TEMPMATRECNO > 1 Then
                TXTMATRECNO.Text = TEMPMATRECNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            GRIDMATREC.RowCount = 0
LINE1:
            TEMPMATRECNO = Val(TXTMATRECNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTMATRECNO.Text.Trim
            CLEAR()
            If Val(TXTMATRECNO.Text) - 1 >= TEMPMATRECNO Then
                EDIT = True
                MaterialReceipt_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDMATREC.RowCount = 0 And TEMPMATRECNO < MAXNO Then
                TXTMATRECNO.Text = TEMPMATRECNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtqty.KeyPress
        numkeypress(e, txtqty, Me)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try

            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Material Receipt Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                TEMPMSG = MsgBox("Delete Material Receipt?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTMATRECNO.Text.Trim)
                    alParaval.Add(CmpId)
                    alParaval.Add(Userid)
                    alParaval.Add(Locationid)
                    alParaval.Add(YearId)

                    Dim OBJMATREC As New ClsMaterialReceipt()
                    OBJMATREC.alParaval = alParaval
                    IntResult = OBJMATREC.Delete()
                    MsgBox("Material Receipt Deleted")
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

    Private Sub cmbqtyunit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbqtyunit.Validating
        Try
            If cmbqtyunit.Text.Trim <> "" Then unitvalidate(cmbqtyunit, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDMATREC_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDMATREC.CellValidating
        Try
            Dim colNum As Integer = GRIDMATREC.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GRATE.Index, GRECDMTRS.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDMATREC.CurrentCell.Value = Nothing Then GRIDMATREC.CurrentCell.Value = "0.00"
                        GRIDMATREC.CurrentCell.Value = Convert.ToDecimal(GRIDMATREC.Item(colNum, e.RowIndex).Value)
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

    Sub EDITROW()
        Try
            If GRIDMATREC.CurrentRow.Index >= 0 And GRIDMATREC.Item(gsrno.Index, GRIDMATREC.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                CMBCHECKSRNO.Enabled = False
                txtsrno.Text = GRIDMATREC.Item(gsrno.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                CMBGRIDLOTNO.Text = GRIDMATREC.Item(GLOTNO.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                CMBCHECKSRNO.Text = GRIDMATREC.Item(GCHECKSRNO.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                cmbitemname.Text = GRIDMATREC.Item(gitemname.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                TXTGRIDDESC.Text = GRIDMATREC.Item(GGRIDDESC.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDMATREC.Item(GQUALITY.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                CMBBALENO.Text = GRIDMATREC.Item(GBALENO.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                TXTBALENO.Text = GRIDMATREC.Item(GBALENO.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDMATREC.Item(GDESIGN.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                cmbcolor.Text = GRIDMATREC.Item(gcolor.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                TXTCUT.Text = GRIDMATREC.Item(gcut.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                TXTWT.Text = GRIDMATREC.Item(GWT.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                txtqty.Text = GRIDMATREC.Item(gQty.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                cmbqtyunit.Text = GRIDMATREC.Item(gqtyunit.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                TXTMTRS.Text = GRIDMATREC.Item(GMTRS.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                TXTRATE.Text = GRIDMATREC.Item(GRATE.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                CMBPER.Text = GRIDMATREC.Item(GPER.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                TXTAMOUNT.Text = GRIDMATREC.Item(GAMOUNT.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                TXTRECDMTRS.Text = GRIDMATREC.Item(GRECDMTRS.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                CMBRACK.Text = GRIDMATREC.Item(GRACK.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                CMBSHELF.Text = GRIDMATREC.Item(GSHELF.Index, GRIDMATREC.CurrentRow.Index).Value.ToString
                CMBYESNO.Text = GRIDMATREC.Item(GYESNO.Index, GRIDMATREC.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDMATREC.CurrentRow.Index
                If ClientName = "AVIS" Then
                    TXTRECDMTRS.Focus()
                ElseIf ClientName = "SNCM" Then
                    TXTBALENO.Focus()
                Else
                    txtsrno.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDMATREC_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDMATREC.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDMATREC.RowCount > 0 And GRIDMATREC.CurrentRow.DefaultCellStyle.BackColor <> Color.Yellow Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                'end of block

                If ClientName = "SNCM" Then
LINE1:
                    For I As Integer = 0 To DT_MTRSDETAILS.Rows.Count - 1
                        If GRIDMATREC.Rows(GRIDMATREC.CurrentRow.Index).Cells(gsrno.Index).Value = Val(DT_MTRSDETAILS.Rows(I).Item("MAINSRNO")) Then
                            DT_MTRSDETAILS.Rows.RemoveAt(I)
                            GoTo LINE1
                        End If
                    Next
                    For I As Integer = 0 To DT_MTRSDETAILS.Rows.Count - 1
                        If GRIDMATREC.Rows(GRIDMATREC.CurrentRow.Index).Cells(gsrno.Index).Value < Val(DT_MTRSDETAILS.Rows(I).Item("MAINSRNO")) Then
                            DT_MTRSDETAILS.Rows(I).Item("MAINSRNO") = Val(DT_MTRSDETAILS.Rows(I).Item("MAINSRNO")) - 1
                        End If
                    Next

                End If

                GRIDMATREC.Rows.RemoveAt(GRIDMATREC.CurrentRow.Index)
                getsrno(GRIDMATREC)
                TOTAL()
                GRIDVIEW()

            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()

                'ElseIf e.KeyCode = Keys.F12 And GRIDMATREC.RowCount > 0 And EDIT = False Then
                '    If GRIDMATREC.CurrentRow.Cells(gitemname.Index).Value <> "" Then
                '        GRIDMATREC.Rows.Insert(GRIDMATREC.CurrentRow.Index + 1, CloneWithValues(GRIDMATREC.CurrentRow))
                '        GRIDMATREC.Rows(GRIDMATREC.CurrentRow.Index + 1).Cells(GRECDMTRS.Index).Selected = True
                '        GRIDMATREC.Focus()
                '        getsrno(GRIDMATREC)
                '        TOTAL()
                '    End If
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

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcolor.Validating
        Try
            If cmbcolor.Text.Trim <> "" Then COLORVALIDATE(cmbcolor, e, Me, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress, TXTDMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then NAMEVALIDATE(cmbname, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS", cmbtrans.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKSRNO() As Boolean
        Try
            Dim BLN As Boolean = True
            For Each ROW As DataGridViewRow In GRIDMATREC.Rows
                If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And ROW.Index <> TEMPROW) Then
                    'If ROW.Cells(GCHECKSRNO.Index).Value = CMBCHECKSRNO.Text.Trim Then
                    '    EP.SetError(CMBCHECKSRNO, "Sr Already Present in Grid Below")
                    '    BLN = False
                    'End If
                End If
            Next
            Return BLN
        Catch ex As Exception

        End Try
    End Function

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

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            If ClientName = "SOFTAS" And Val(TXTRECDMTRS.Text.Trim) > 0 And Val(txtqty.Text.Trim) > 0 Then TXTCUT.Text = Format(Val(TXTRECDMTRS.Text.Trim) / Val(txtqty.Text.Trim), "0.00")
            If Val(txtqty.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 Then TXTMTRS.Text = Format(Val(txtqty.Text.Trim) * Val(TXTCUT.Text.Trim), "0.00")
            'If Val(TXTMTRS.Text.Trim) > 0 And Val(TXTRECDMTRS.Text.Trim) > 0 Then TXTDIFF.Text = Format(Val(TXTMTRS.Text.Trim) - Val(TXTRECDMTRS.Text.Trim), "0.00")
            If Val(TXTRECDMTRS.Text.Trim) > 0 Then TXTDIFF.Text = Format(Val(TXTMTRS.Text.Trim) - Val(TXTRECDMTRS.Text.Trim), "0.00")
            If CMBPER.Text = "Mtrs" Then TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(TXTRECDMTRS.Text.Trim), "0.00") Else TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(txtqty.Text.Trim), "0.00")
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
            If CMBDESIGN.Text.Trim <> "" Then
                If ClientName = "AVIS" Then DESIGNVALIDATE(CMBDESIGN, e, Me) Else DESIGNVALIDATE(CMBDESIGN, e, Me, cmbitemname.Text.Trim)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Validated
        Try
            CMBLOTNO.Items.Clear()
            CMBGRIDLOTNO.Items.Clear()
            If EDIT = False And cmbname.Text.Trim <> "" Then
                'FILLLOTNO
                Dim OBJCMN As New ClsCommon
                'WE HAVE COMMENTED THIS CODE COZ OF DONE... WHEN WE TRANSFER STOCK FROM LAST YEAR WE GET SINGLE ENTRY SUMMARY, IF WE LOCK THAT ENTRY THEN WE WONT GET THE LOTNO AGAIN
                'Dim DT As DataTable = OBJCMN.search(" DISTINCT LOTNO ", "", " (SELECT   DISTINCT  CHECKINGMASTER.CHECK_LOTNO AS LOTNO, CHECKINGMASTER.CHECK_NO AS FROMNO,  CHECKINGMASTER.CHECK_cmpid AS CMPID, CHECKINGMASTER.CHECK_locationid AS LOCATIONID, CHECKINGMASTER.CHECK_yearid AS YEARID FROM CHECKINGMASTER INNER JOIN CHECKINGMASTER_DESC ON CHECKINGMASTER.CHECK_NO = CHECKINGMASTER_DESC.CHECK_NO AND CHECKINGMASTER.CHECK_YEARID = CHECKINGMASTER_DESC.CHECK_YEARID INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND (CHECKINGMASTER.CHECK_TYPE = 'JOB WORK') AND CHECKINGMASTER_DESC.CHECK_CHECKINGDONE = 0  UNION ALL SELECT   DISTINCT  STOCKMASTER.SM_LOTNO, SM_NO AS FROMNO, SM_CMPID, SM_LOCATIONID, SM_YEARID  FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND SM_TYPE = 'JOBBERSTOCK' AND SM_LOTNO <> '' AND SM_DONE = 0 ) AS T ", " AND T.YEARID = " & YearId)

                'THIS IS DONE BY GULKIT FOR SOFTAS, WE NEED FETCH LOTNO FROM PURCHASE 
                'Dim DT As DataTable = OBJCMN.search(" DISTINCT LOTNO ", "", " (SELECT   DISTINCT  CHECKINGMASTER.CHECK_LOTNO AS LOTNO, CHECKINGMASTER.CHECK_NO AS FROMNO,  CHECKINGMASTER.CHECK_cmpid AS CMPID, CHECKINGMASTER.CHECK_locationid AS LOCATIONID, CHECKINGMASTER.CHECK_yearid AS YEARID FROM CHECKINGMASTER INNER JOIN CHECKINGMASTER_DESC ON CHECKINGMASTER.CHECK_NO = CHECKINGMASTER_DESC.CHECK_NO AND CHECKINGMASTER.CHECK_YEARID = CHECKINGMASTER_DESC.CHECK_YEARID INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND (CHECKINGMASTER.CHECK_TYPE = 'JOB WORK') AND CHECKINGMASTER_DESC.CHECK_CHECKINGDONE = 0  UNION ALL SELECT   DISTINCT  STOCKMASTER.SM_LOTNO, SM_NO AS FROMNO, SM_CMPID, SM_LOCATIONID, SM_YEARID  FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND SM_TYPE = 'JOBBERSTOCK' AND (SM_LOTNO <> '' OR  SM_LOTNO <> 0)) AS T ", " AND T.YEARID = " & YearId)
                Dim DT As New DataTable

                If LOTSTATUSONMTRS = False Then
                    If ClientName = "SOFTAS" Then
                        DT = OBJCMN.SEARCH(" DISTINCT LOTNO ", "", " LOT_VIEW ", " AND BALPCS > 0 AND GRNTYPE <> 'JOBOUT' AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND YEARID = " & YearId & " AND (DYEINGJOB = 'DYEING' OR DYEINGJOB = '')")
                    Else
                        If ClientName = "MAHAVIRPOLYCOT" Then
                            DT = OBJCMN.SEARCH(" DISTINCT LOTNO ", "", " LOT_VIEW_PCSDETAILS ", " AND BALPCS > 0 AND LOTCOMPLETED = 0 AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND YEARID = " & YearId)
                        Else
                            Dim WHERECLAUSE As String = ""
                            If ClientName = "VALIANT" Or ClientName = "KARAN" Then WHERECLAUSE = " OR ISNULL(SM_DYEINGJOB,'') = 'JOB' "
                            DT = OBJCMN.SEARCH(" DISTINCT LOTNO ", "", " (Select DISTINCT CHECKINGMASTER.CHECK_LOTNO As LOTNO, CHECKINGMASTER.CHECK_NO As FROMNO, CHECKINGMASTER.CHECK_cmpid As CMPID, CHECKINGMASTER.CHECK_locationid As LOCATIONID, CHECKINGMASTER.CHECK_yearid AS YEARID FROM CHECKINGMASTER INNER JOIN CHECKINGMASTER_DESC ON CHECKINGMASTER.CHECK_NO = CHECKINGMASTER_DESC.CHECK_NO And CHECKINGMASTER.CHECK_YEARID = CHECKINGMASTER_DESC.CHECK_YEARID INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND (CHECKINGMASTER.CHECK_TYPE = 'JOB WORK') AND CHECKINGMASTER_DESC.CHECK_CHECKINGDONE = 0  UNION ALL SELECT   DISTINCT  STOCKMASTER.SM_LOTNO, SM_NO AS FROMNO, SM_CMPID, SM_LOCATIONID, SM_YEARID  FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND SM_TYPE = 'JOBBERSTOCK' and SM_DONE = 0 AND (SM_LOTNO <> '' OR  SM_LOTNO <> 0) AND (ISNULL(SM_DYEINGJOB,'') = 'DYEING' OR ISNULL(SM_DYEINGJOB,'') = '' " & WHERECLAUSE & " )) AS T ", " AND T.YEARID = " & YearId)
                        End If
                    End If
                Else
                    DT = OBJCMN.SEARCH(" DISTINCT LOTNO ", "", " LOT_VIEW ", " AND LOTCOMPLETED = 'FALSE' AND GRNTYPE <> 'JOBOUT' AND CHECKMTRS > 0 AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND YEARID = " & YearId & " AND (DYEINGJOB = 'DYEING' OR DYEINGJOB = '')")
                End If
                If DT.Rows.Count > 0 Then
                    For Each DTROW As DataRow In DT.Rows
                        CMBLOTNO.Items.Add(DTROW("LOTNO"))
                        CMBGRIDLOTNO.Items.Add(DTROW("LOTNO"))
                    Next
                    cmbname.Enabled = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOTNO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBLOTNO.KeyDown, CMBGRIDLOTNO.KeyDown
        Try
            If cmbname.Text.Trim <> "" And EDIT = False Then
                If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
                If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

                If e.KeyCode = Keys.F1 Then
                    Dim OBJLEDGER As New SelectLotNo
                    OBJLEDGER.JOBBERNAME = cmbname.Text.Trim
                    OBJLEDGER.ShowDialog()
                    If OBJLEDGER.DT.Rows.Count > 0 Then
                        CMBLOTNO.Text = OBJLEDGER.DT.Rows(0).Item("LOTNO")
                        CMBGRIDLOTNO.Text = OBJLEDGER.DT.Rows(0).Item("LOTNO")
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDLOTNO_Validating(sender As Object, e As CancelEventArgs) Handles CMBGRIDLOTNO.Validating
        Try
            If CMBGRIDLOTNO.Text.Trim <> "" Then
                'FILL CHECKSRNO
                CMBCHECKSRNO.Items.Clear()
                CMBBALENO.Items.Clear()
                Dim OBJCMN As New ClsCommon

                'THIS IS COMMENTED COZ WE CANNOT WORK WITH DONE 0 IN STOCKMASTER
                'WHEN WE TRANSFER STOCK FROM LAST YEAR WE GET ONLY SINGLE LINE WITH MULTIPLE TAKA,  AND IF WE LOCK THAT THEN WE WILL NOT GET SRNO HERE
                'Dim DT As DataTable = OBJCMN.search(" SRNO, FROMNO, FROMTYPE ", "", " (SELECT   DISTINCT  CHECKINGMASTER_DESC.CHECK_GRIDSRNO AS SRNO, CHECKINGMASTER.CHECK_NO AS FROMNO,'CHECKING' AS FROMTYPE, CHECKINGMASTER.CHECK_cmpid AS CMPID, CHECKINGMASTER.CHECK_locationid AS LOCATIONID, CHECKINGMASTER.CHECK_yearid AS YEARID FROM CHECKINGMASTER INNER JOIN CHECKINGMASTER_DESC ON CHECKINGMASTER.CHECK_NO = CHECKINGMASTER_DESC.CHECK_NO AND CHECKINGMASTER.CHECK_CMPID = CHECKINGMASTER_DESC.CHECK_CMPID AND CHECKINGMASTER.CHECK_LOCATIONID = CHECKINGMASTER_DESC.CHECK_LOCATIONID AND CHECKINGMASTER.CHECK_YEARID = CHECKINGMASTER_DESC.CHECK_YEARID  INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id AND CHECKINGMASTER.CHECK_CMPID = LEDGERS.Acc_cmpid AND CHECKINGMASTER.CHECK_LOCATIONID = LEDGERS.Acc_locationid AND CHECKINGMASTER.CHECK_YEARID = LEDGERS.Acc_yearid  WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND (CHECKINGMASTER.CHECK_LOTNO = '" & CMBLOTNO.Text.Trim & "') AND CHECKINGMASTER_DESC.CHECK_CHECKINGDONE = 0 AND CHECKINGMASTER_DESC.CHECK_APPROVED = 1 UNION ALL SELECT   DISTINCT  STOCKMASTER.SM_NO, STOCKMASTER.SM_NO AS FROMNO, 'OPENING' AS FROMTYPE,  SM_CMPID, SM_LOCATIONID, SM_YEARID  FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id AND STOCKMASTER.SM_CMPID = LEDGERS.Acc_cmpid AND STOCKMASTER.SM_LOCATIONID = LEDGERS.Acc_locationid And STOCKMASTER.SM_YEARID = LEDGERS.Acc_yearid WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND SM_TYPE = 'JOBBERSTOCK' AND SM_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND SM_DONE = 0) AS T ", " AND T.CMPID = " & CmpId & " AND T.LOCATIONID = " & Locationid & " AND T.YEARID = " & YearId)
                Dim DT As New DataTable
                If LOTSTATUSONMTRS = False Then
                    If ClientName = "SOFTAS" Then
                        DT = OBJCMN.SEARCH(" 0 as SRNO, CHECKNO AS FROMNO, GRNTYPE AS FROMTYPE, '' AS BALENO, ITEMNAME, DESIGN, PARTYNAME ", "", " LOT_VIEW ", " AND LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "' AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND (ACCEPTEDPCS-RECDPCS) > 0 AND LOTCOMPLETED = 'FALSE' AND DYEINGJOB <> 'JOB' AND YEARID = " & YearId)
                    Else
                        If ClientName = "MAHAVIRPOLYCOT" Or ClientName = "RADHA" Or ClientName = "VALIANT" Or ClientName = "KARAN" Then
                            DT = OBJCMN.SEARCH(" GRNNO AS SRNO, GRNNO AS FROMNO, GRNTYPE AS FROMTYPE, BALENO, PARTYNAME ", "", " LOT_VIEW_PCSDETAILS ", " AND BALENO NOT IN (SELECT DISTINCT BALENO FROM LOT_VIEW_PCSDETAILS WHERE LOTNO= '" & CMBGRIDLOTNO.Text.Trim & "' AND DYEINGJOB = 'DYEING' AND YEARID = " & YearId & " AND BALPCS<=0) AND BALPCS > 0 AND LOTCOMPLETED = 0 AND DYEINGJOB = 'DYEING' AND LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "' AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND YEARID = " & YearId)
                        Else
                            DT = OBJCMN.SEARCH(" SRNO, FROMNO, FROMTYPE, BALENO, '' AS PARTYNAME ", "", " (SELECT   DISTINCT  CHECKINGMASTER_DESC.CHECK_GRIDSRNO AS SRNO, CHECKINGMASTER.CHECK_NO AS FROMNO, (CASE WHEN ISNULL(CHECK_GRIDREMARKS,'') <> '' THEN ISNULL(CHECK_GRIDREMARKS,'') ELSE CAST(CHECKINGMASTER_DESC.CHECK_GRIDSRNO AS VARCHAR(10)) END) AS BALENO,'CHECKING' AS FROMTYPE, CHECKINGMASTER.CHECK_cmpid AS CMPID, CHECKINGMASTER.CHECK_locationid AS LOCATIONID, CHECKINGMASTER.CHECK_yearid AS YEARID FROM CHECKINGMASTER INNER JOIN CHECKINGMASTER_DESC ON CHECKINGMASTER.CHECK_NO = CHECKINGMASTER_DESC.CHECK_NO AND CHECKINGMASTER.CHECK_CMPID = CHECKINGMASTER_DESC.CHECK_CMPID AND CHECKINGMASTER.CHECK_LOCATIONID = CHECKINGMASTER_DESC.CHECK_LOCATIONID AND CHECKINGMASTER.CHECK_YEARID = CHECKINGMASTER_DESC.CHECK_YEARID  INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id AND CHECKINGMASTER.CHECK_CMPID = LEDGERS.Acc_cmpid AND CHECKINGMASTER.CHECK_LOCATIONID = LEDGERS.Acc_locationid AND CHECKINGMASTER.CHECK_YEARID = LEDGERS.Acc_yearid  WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND (CHECKINGMASTER.CHECK_LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "') AND CHECKINGMASTER_DESC.CHECK_CHECKINGDONE = 0 AND CHECKINGMASTER_DESC.CHECK_APPROVED = 1 UNION ALL SELECT   DISTINCT  STOCKMASTER.SM_NO, STOCKMASTER.SM_NO AS FROMNO, (CASE WHEN VERSION_CLIENTNAME = 'VALIANT' THEN CAST(ISNULL(SM_BALENO,'') AS VARCHAR(100)) ELSE CAST(ISNULL(SM_REMARKS,'') AS VARCHAR(100)) END) AS BALENO, 'OPENING' AS FROMTYPE, SM_CMPID, SM_LOCATIONID, SM_YEARID  FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id CROSS JOIN VERSION WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND SM_TYPE = 'JOBBERSTOCK' AND SM_LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "' AND (SM_PCS-SM_OUTPCS)>0) AS T ", " AND T.CMPID = " & CmpId & " AND T.LOCATIONID = " & Locationid & " AND T.YEARID = " & YearId)
                        End If
                    End If
                Else
                    DT = OBJCMN.SEARCH(" 0 as SRNO, CHECKNO AS FROMNO, GRNTYPE AS FROMTYPE, '' AS BALENO, ITEMNAME, DESIGN, PARTYNAME , CHALLANNO  ", "", " LOT_VIEW ", " AND LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "' AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND LOTCOMPLETED = 'FALSE' AND DYEINGJOB <> 'JOB' AND YEARID = " & YearId)
                End If

                If DT.Rows.Count > 0 Then

                    'IF CHECKNO IS NULL MEANS USER HAS NOT DONE GREYCHECKING ENTRY
                    If IsDBNull(DT.Rows(0).Item("FROMNO")) = True Then
                        MsgBox("Grey Checking Not Done, Kindly do Grey checking Entry First", MsgBoxStyle.Critical)
                        Exit Sub
                    End If


                    For Each DTROW As DataRow In DT.Rows
                        CMBCHECKSRNO.Items.Add(DTROW("SRNO"))
                    Next
                    For Each DTROW As DataRow In DT.Rows
                        CMBBALENO.Items.Add(DTROW("BALENO"))
                    Next

                    TXTFROMTYPE.Text = DT.Rows(0).Item("FROMTYPE")
                    TXTFROMNO.Text = DT.Rows(0).Item("FROMNO")

                    If ClientName = "VINTAGEINDIA" Then
                        TXTCHALLANNO.Text = DT.Rows(0).Item("CHALLANNO")
                    End If


                    CMBLOTNO.Enabled = False
                    If ClientName = "SOFTAS" Then CMBCHECKSRNO.Text = 1
                    If ClientName = "KEMLINO" Or ClientName = "SHREENAKODA" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "YASHVI" Or ClientName = "MOHATUL" Or ClientName = "SIDDHGIRI" Or ClientName = "RMANILAL" Then
                        cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")
                        CMBDESIGN.Text = DT.Rows(0).Item("DESIGN")
                    End If
                    CMBPARTYNAME.Text = DT.Rows(0).Item("PARTYNAME")

                Else
                    MsgBox("Invalid Lot No Entered", MsgBoxStyle.Critical)
                    If ClientName = "YASHVI" Or ClientName = "VALIANT" Then
                        'GET FROMTYPE EVENIF LOTNO IS CLOSED FOR YASHVI, WITHOUT YEARID, IT MAY BE CLOSED IN LAST YEAR
                        DT = OBJCMN.SEARCH(" GRNTYPE AS FROMTYPE ", "", " LOT_VIEW ", " AND LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "' AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND DYEINGJOB <> 'JOB'")
                        If DT.Rows.Count > 0 Then TXTFROMTYPE.Text = DT.Rows(0).Item("FROMTYPE") Else TXTFROMTYPE.Text = "OPENING"
                    Else
                        e.Cancel = True
                    End If
                End If

                Dim DT1 As DataTable = OBJCMN.SEARCH("GRNTYPE, ISNULL(SUM(ACCEPTEDPCS),0) - ISNULL(SUM(RECDPCS),0) AS BALPCS, ISNULL(SUM(ACCEPTEDMTRS),0) - ISNULL(SUM(RECDMTRS),0)  AS BALMTRS ", "", " LOT_VIEW ", " AND LOT_VIEW.LOTNO='" & CMBGRIDLOTNO.Text.Trim & "' AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND LOT_VIEW.YEARID = " & YearId & " GROUP BY GRNTYPE")
                If DT1.Rows.Count > 0 Then
                    TXTFROMTYPE.Text = DT1.Rows(0).Item("GRNTYPE")
                    TXTBALPCS.Text = DT1.Rows(0).Item("BALPCS")
                    TXTBALMTRS.Text = DT1.Rows(0).Item("BALMTRS")
                    CMBLOTNO.Enabled = False
                End If

                Dim DT2 As DataTable = OBJCMN.SEARCH("ISNULL(CHECKINGMASTER.CHECK_REFLOTNO,'') AS REFLOTNO", "", " CHECKINGMASTER LEFT OUTER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id ", " AND CHECK_LOTNO='" & CMBGRIDLOTNO.Text.Trim & "' AND LEDGERS.Acc_cmpname = '" & cmbname.Text.Trim & "' AND CHECK_YEARID = " & YearId)
                If DT2.Rows.Count > 0 And ClientName = "SNCM" Then
                    TXTREFLOTNO.Text = DT2.Rows(0).Item("REFLOTNO")
                End If

                'GETTING PROGRAMNO
                DT = OBJCMN.SEARCH("DISTINCT PROGRAMMASTER_DESC.PROGRAM_NO AS PROGRAMNO, COLORMASTER.COLOR_name AS COLORNAME ", "", " PROGRAMMASTER INNER JOIN PROGRAMMASTER_DESC ON PROGRAMMASTER.PROGRAM_NO = PROGRAMMASTER_DESC.PROGRAM_NO AND PROGRAMMASTER.PROGRAM_YEARID = PROGRAMMASTER_DESC.PROGRAM_YEARID INNER JOIN COLORMASTER ON PROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id INNER JOIN LEDGERS ON LEDGERS.ACC_ID = PROGRAM_LEDGERID ", " AND PROGRAM_LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "' AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND PROGRAMMASTER.PROGRAM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTPROGRAMNO.Text = Val(DT.Rows(0).Item("PROGRAMNO"))

                    'cmbcolor.DataSource = Nothing
                    'For Each DTROW As DataRow In DT.Rows
                    '    cmbcolor.Items.Add(DTROW("COLORNAME"))
                    'Next
                    '    If ClientName <> "INDRAPUJAFABRICS" Then cmbcolor.DropDownStyle = ComboBoxStyle.DropDownList
                    'Else
                    '    cmbcolor.DropDownStyle = ComboBoxStyle.DropDown
                End If


            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBLOTNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBLOTNO.Validating
        Try
            If CMBLOTNO.Text.Trim <> "" Then
                If GRIDLOTNO = False Then CMBGRIDLOTNO.Text = CMBLOTNO.Text.Trim

                'FILL CHECKSRNO
                CMBCHECKSRNO.Items.Clear()
                CMBBALENO.Items.Clear()
                Dim OBJCMN As New ClsCommon

                'THIS IS COMMENTED COZ WE CANNOT WORK WITH DONE 0 IN STOCKMASTER
                'WHEN WE TRANSFER STOCK FROM LAST YEAR WE GET ONLY SINGLE LINE WITH MULTIPLE TAKA,  AND IF WE LOCK THAT THEN WE WILL NOT GET SRNO HERE
                'Dim DT As DataTable = OBJCMN.search(" SRNO, FROMNO, FROMTYPE ", "", " (SELECT   DISTINCT  CHECKINGMASTER_DESC.CHECK_GRIDSRNO AS SRNO, CHECKINGMASTER.CHECK_NO AS FROMNO,'CHECKING' AS FROMTYPE, CHECKINGMASTER.CHECK_cmpid AS CMPID, CHECKINGMASTER.CHECK_locationid AS LOCATIONID, CHECKINGMASTER.CHECK_yearid AS YEARID FROM CHECKINGMASTER INNER JOIN CHECKINGMASTER_DESC ON CHECKINGMASTER.CHECK_NO = CHECKINGMASTER_DESC.CHECK_NO AND CHECKINGMASTER.CHECK_CMPID = CHECKINGMASTER_DESC.CHECK_CMPID AND CHECKINGMASTER.CHECK_LOCATIONID = CHECKINGMASTER_DESC.CHECK_LOCATIONID AND CHECKINGMASTER.CHECK_YEARID = CHECKINGMASTER_DESC.CHECK_YEARID  INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id AND CHECKINGMASTER.CHECK_CMPID = LEDGERS.Acc_cmpid AND CHECKINGMASTER.CHECK_LOCATIONID = LEDGERS.Acc_locationid AND CHECKINGMASTER.CHECK_YEARID = LEDGERS.Acc_yearid  WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND (CHECKINGMASTER.CHECK_LOTNO = '" & CMBLOTNO.Text.Trim & "') AND CHECKINGMASTER_DESC.CHECK_CHECKINGDONE = 0 AND CHECKINGMASTER_DESC.CHECK_APPROVED = 1 UNION ALL SELECT   DISTINCT  STOCKMASTER.SM_NO, STOCKMASTER.SM_NO AS FROMNO, 'OPENING' AS FROMTYPE,  SM_CMPID, SM_LOCATIONID, SM_YEARID  FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id AND STOCKMASTER.SM_CMPID = LEDGERS.Acc_cmpid AND STOCKMASTER.SM_LOCATIONID = LEDGERS.Acc_locationid And STOCKMASTER.SM_YEARID = LEDGERS.Acc_yearid WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND SM_TYPE = 'JOBBERSTOCK' AND SM_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND SM_DONE = 0) AS T ", " AND T.CMPID = " & CmpId & " AND T.LOCATIONID = " & Locationid & " AND T.YEARID = " & YearId)
                Dim DT As New DataTable
                If LOTSTATUSONMTRS = False Then
                    DT = OBJCMN.SEARCH(" SRNO, FROMNO, FROMTYPE, BALENO, '' AS PARTYNAME, '' AS ITEMNAME ", "", " (SELECT   DISTINCT  CHECKINGMASTER_DESC.CHECK_GRIDSRNO AS SRNO, CHECKINGMASTER.CHECK_NO AS FROMNO, ISNULL(CHECK_GRIDREMARKS,'') AS BALENO,'CHECKING' AS FROMTYPE, CHECKINGMASTER.CHECK_cmpid AS CMPID, CHECKINGMASTER.CHECK_locationid AS LOCATIONID, CHECKINGMASTER.CHECK_yearid AS YEARID FROM CHECKINGMASTER INNER JOIN CHECKINGMASTER_DESC ON CHECKINGMASTER.CHECK_NO = CHECKINGMASTER_DESC.CHECK_NO AND CHECKINGMASTER.CHECK_CMPID = CHECKINGMASTER_DESC.CHECK_CMPID AND CHECKINGMASTER.CHECK_LOCATIONID = CHECKINGMASTER_DESC.CHECK_LOCATIONID AND CHECKINGMASTER.CHECK_YEARID = CHECKINGMASTER_DESC.CHECK_YEARID  INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id AND CHECKINGMASTER.CHECK_CMPID = LEDGERS.Acc_cmpid AND CHECKINGMASTER.CHECK_LOCATIONID = LEDGERS.Acc_locationid AND CHECKINGMASTER.CHECK_YEARID = LEDGERS.Acc_yearid  WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND (CHECKINGMASTER.CHECK_LOTNO = '" & CMBLOTNO.Text.Trim & "') AND CHECKINGMASTER_DESC.CHECK_CHECKINGDONE = 0 AND CHECKINGMASTER_DESC.CHECK_APPROVED = 1 UNION ALL SELECT   DISTINCT  STOCKMASTER.SM_NO, STOCKMASTER.SM_NO AS FROMNO, CAST(ISNULL(SM_REMARKS,'') AS VARCHAR(100)) AS BALENO, 'OPENING' AS FROMTYPE, SM_CMPID, SM_LOCATIONID, SM_YEARID  FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id AND STOCKMASTER.SM_CMPID = LEDGERS.Acc_cmpid AND STOCKMASTER.SM_LOCATIONID = LEDGERS.Acc_locationid And STOCKMASTER.SM_YEARID = LEDGERS.Acc_yearid WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND SM_TYPE = 'JOBBERSTOCK' AND SM_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND (SM_PCS-SM_OUTPCS)>0) AS T ", " AND T.CMPID = " & CmpId & " AND T.LOCATIONID = " & Locationid & " AND T.YEARID = " & YearId)
                Else
                    DT = OBJCMN.SEARCH(" 0 as SRNO, CHECKNO AS FROMNO, GRNTYPE AS FROMTYPE, '' AS BALENO, PARTYNAME, ITEMNAME ", "", " LOT_VIEW ", " AND LOTNO = '" & CMBLOTNO.Text.Trim & "' AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND LOTCOMPLETED = 'FALSE' AND YEARID = " & YearId)
                End If

                If DT.Rows.Count > 0 Then
                    For Each DTROW As DataRow In DT.Rows
                        CMBCHECKSRNO.Items.Add(DTROW("SRNO"))
                    Next
                    For Each DTROW As DataRow In DT.Rows
                        CMBBALENO.Items.Add(DTROW("BALENO"))
                    Next

                    TXTFROMTYPE.Text = DT.Rows(0).Item("FROMTYPE")
                    TXTFROMNO.Text = DT.Rows(0).Item("FROMNO")
                    CMBLOTNO.Enabled = False
                    If ClientName = "SOFTAS" Then CMBCHECKSRNO.Text = 1
                    CMBPARTYNAME.Text = DT.Rows(0).Item("PARTYNAME")
                    cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")

                End If

                Dim DT1 As DataTable = OBJCMN.SEARCH("GRNTYPE, ISNULL(SUM(ACCEPTEDPCS),0) - ISNULL(SUM(RECDPCS),0) AS BALPCS, ISNULL(SUM(ACCEPTEDMTRS),0) - ISNULL(SUM(RECDMTRS),0)  AS BALMTRS ", "", " LOT_VIEW ", " AND LOT_VIEW.LOTNO= '" & CMBLOTNO.Text.Trim & "' AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND LOT_VIEW.YEARID = " & YearId & " GROUP BY GRNTYPE")
                If DT1.Rows.Count > 0 Then
                    TXTFROMTYPE.Text = DT1.Rows(0).Item("GRNTYPE")
                    TXTBALPCS.Text = DT1.Rows(0).Item("BALPCS")
                    TXTBALMTRS.Text = DT1.Rows(0).Item("BALMTRS")
                    CMBLOTNO.Enabled = False
                Else
                    If MsgBox("Lot No Not Present in Jobber Stock, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                End If


                'GETTING PROGRAMNO
                'THIS CODE GETS PROGRAM ONLY FROM PROGRAM MASTER AND NOT FROM OPENING
                'DT = OBJCMN.SEARCH("DISTINCT PROGRAMMASTER_DESC.PROGRAM_NO AS PROGRAMNO, COLORMASTER.COLOR_name AS COLORNAME ", "", " PROGRAMMASTER INNER JOIN PROGRAMMASTER_DESC ON PROGRAMMASTER.PROGRAM_NO = PROGRAMMASTER_DESC.PROGRAM_NO AND PROGRAMMASTER.PROGRAM_YEARID = PROGRAMMASTER_DESC.PROGRAM_YEARID INNER JOIN COLORMASTER ON PROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id INNER JOIN LEDGERS ON LEDGERS.ACC_ID = PROGRAM_LEDGERID ", " AND PROGRAM_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND PROGRAMMASTER.PROGRAM_YEARID = " & YearId)
                DT = OBJCMN.SEARCH("*", "", "(SELECT DISTINCT PROGRAMMASTER_DESC.PROGRAM_NO AS PROGRAMNO, COLORMASTER.COLOR_name AS COLORNAME FROM PROGRAMMASTER INNER JOIN PROGRAMMASTER_DESC ON PROGRAMMASTER.PROGRAM_NO = PROGRAMMASTER_DESC.PROGRAM_NO AND PROGRAMMASTER.PROGRAM_YEARID = PROGRAMMASTER_DESC.PROGRAM_YEARID INNER JOIN COLORMASTER ON PROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id INNER JOIN LEDGERS ON LEDGERS.ACC_ID = PROGRAM_LEDGERID WHERE PROGRAM_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND PROGRAMMASTER.PROGRAM_YEARID = " & YearId & " UNION ALL SELECT DISTINCT OPENINGPROGRAMMASTER_DESC.OPPROGRAM_NO AS PROGRAMNO, COLORMASTER.COLOR_name AS COLORNAME FROM OPENINGPROGRAMMASTER INNER JOIN OPENINGPROGRAMMASTER_DESC ON OPENINGPROGRAMMASTER.OPPROGRAM_NO = OPENINGPROGRAMMASTER_DESC.OPPROGRAM_NO AND OPENINGPROGRAMMASTER.OPPROGRAM_YEARID = OPENINGPROGRAMMASTER_DESC.OPPROGRAM_YEARID INNER JOIN COLORMASTER ON OPENINGPROGRAMMASTER_DESC.OPPROGRAM_COLORID = COLORMASTER.COLOR_id INNER JOIN LEDGERS ON LEDGERS.ACC_ID = OPPROGRAM_LEDGERID  WHERE OPPROGRAM_LOTNO = '" & CMBLOTNO.Text.Trim & "' AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND OPENINGPROGRAMMASTER.OPPROGRAM_YEARID = " & YearId & ") AS T")
                If DT.Rows.Count > 0 Then
                    TXTPROGRAMNO.Text = Val(DT.Rows(0).Item("PROGRAMNO"))
                    cmbcolor.DataSource = Nothing
                    For Each DTROW As DataRow In DT.Rows
                        cmbcolor.Items.Add(DTROW("COLORNAME"))
                    Next
                    If ClientName <> "INDRAPUJAFABRICS" Then cmbcolor.DropDownStyle = ComboBoxStyle.DropDownList
                Else
                    cmbcolor.DropDownStyle = ComboBoxStyle.DropDown
                End If


            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHECKSRNO_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCHECKSRNO.Validated
        Try
            If CMBCHECKSRNO.Text.Trim <> "" And TXTFROMNO.Text.Trim <> "" And TXTFROMTYPE.Text.Trim <> "" And cmbname.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                If TXTFROMTYPE.Text.Trim = "OPENING" Then
                    DT = OBJCMN.SEARCH(" ISNULL(SM_MTRS, 0) As MTRS, ISNULL(DESIGN_NO,'') AS DESIGNNO, ISNULL(ITEM_NAME,'') AS ITEMNAME", "", " STOCKMASTER INNER JOIN ITEMMASTER ON ITEM_ID = SM_ITEMID LEFT OUTER JOIN DESIGNMASTER ON SM_DESIGNID = DESIGN_ID ", " AND SM_NO = " & CMBCHECKSRNO.Text.Trim & " AND SM_TYPE = 'JOBBERSTOCK' AND SM_YEARID = " & YearId)
                ElseIf TXTFROMTYPE.Text.Trim = "PURCHASE" Then
                    DT = OBJCMN.SEARCH(" ISNULL(PURCHASEMASTER_DESC.BILL_MTRS,0) AS MTRS, '' AS DESIGNNO, ISNULL(ITEM_NAME,'') AS ITEMNAME ", "", " PURCHASEMASTER_DESC INNER JOIN PURCHASEMASTER ON PURCHASEMASTER_DESC.BILL_NO = PURCHASEMASTER.BILL_NO AND PURCHASEMASTER_DESC.BILL_REGISTERID = PURCHASEMASTER.BILL_REGISTERID AND PURCHASEMASTER_DESC.BILL_YEARID = PURCHASEMASTER.BILL_YEARID LEFT OUTER JOIN ITEMMASTER ON ITEM_ID = PURCHASEMASTER_DESC.BILL_ITEMID   ", " AND PURCHASEMASTER_DESC.BILL_NO = " & TXTFROMNO.Text.Trim & " AND PURCHASEMASTER_DESC.BILL_GRIDSRNO  = " & CMBCHECKSRNO.Text.Trim & " AND PURCHASEMASTER_DESC.BILL_YEARID = " & YearId)
                Else
                    DT = OBJCMN.SEARCH(" ISNULL(CHECKINGMASTER_DESC.CHECK_CHECKEDMTRS,0) AS MTRS, ISNULL(DESIGN_NO,'') AS DESIGNNO, ISNULL(ITEM_NAME,'') AS ITEMNAME", "", " CHECKINGMASTER_DESC INNER JOIN CHECKINGMASTER ON CHECKINGMASTER_DESC.CHECK_NO = CHECKINGMASTER.CHECK_NO AND CHECKINGMASTER_DESC.CHECK_YEARID = CHECKINGMASTER.CHECK_YEARID INNER JOIN GRN_DESC ON CHECKINGMASTER.CHECK_GRNNO = GRN_DESC.GRN_NO AND CHECKINGMASTER.CHECK_GRNGRIDSRNO = GRN_DESC.GRN_GRIDSRNO AND CHECKINGMASTER.CHECK_TYPE = GRN_DESC.GRN_GRIDTYPE AND CHECKINGMASTER.CHECK_YEARID = GRN_DESC.GRN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GRN_DESC.GRN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON ITEM_ID = CHECKINGMASTER.CHECK_ITEMID ", " AND CHECKINGMASTER.CHECK_NO = " & TXTFROMNO.Text.Trim & " AND CHECKINGMASTER_DESC.CHECK_GRIDSRNO = " & CMBCHECKSRNO.Text.Trim & " AND CHECKINGMASTER_DESC.CHECK_YEARID = " & YearId)
                End If
                If DT.Rows.Count > 0 Then

                    If ClientName <> "AVIS" Then
                        TXTMTRS.Text = Format(Val(DT.Rows(0).Item("MTRS")), "0.00")
                        'TXTCUT.Text = Format(Val(DT.Rows(0).Item("MTRS")), "0.00")
                    End If

                    If ClientName <> "KCRAYON" And ClientName <> "RMANILAL" And ClientName <> "MAHAVIRPOLYCOT" Then CMBDESIGN.Text = DT.Rows(0).Item("DESIGNNO")
                    If ClientName <> "RMANILAL" And ClientName <> "SNCM" Then cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRECDMTRS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRECDMTRS.KeyPress, TXTRATE.KeyPress, TXTAMOUNT.KeyPress, TXTMTRS.KeyPress, TXTCUT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub cmbGodown_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbGodown.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then cmbGodown.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHECKSRNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCHECKSRNO.Validating
        Try
            For Each ROW As DataGridViewRow In GRIDMATREC.Rows
                If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And Val(CMBCHECKSRNO.Text.Trim) <> TEMPROW) Then
                    'If Val(CMBCHECKSRNO.Text.Trim) = ROW.Cells(GCHECKSRNO.Index).Value Then
                    '    MsgBox("Pcs No Already Present Below", MsgBoxStyle.Critical)
                    '    e.Cancel = True
                    'End If
                End If
            Next
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
                OBJItem.STRSEARCH = " and ITEM_cmpid = " & CmpId & " and ITEM_LOCATIONid = " & Locationid & " and ITEM_YEARid = " & YearId
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

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Try
            Call cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        If EDIT = True Then
            PRINTBARCODE()
            PRINTREPORT()
        End If
    End Sub

    Private Sub TXTCHALLANNO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHALLANNO.Validating
        Try
            If TXTCHALLANNO.Text.Trim <> "" Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(TXTCHALLANNO.Text.Trim)) Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    Dim dt As DataTable = objclscommon.SEARCH(" MATREC_challanno, LEDGERS.ACC_cmpname", "", " MATERIALRECEIPT inner join LEDGERS on LEDGERS.ACC_id = MATERIALRECEIPT.MATREC_ledgerid ", " and MATERIALRECEIPT.MATREC_challanno = '" & TXTCHALLANNO.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & cmbname.Text.Trim & "' AND MATREC_YEARID =" & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Challan No. Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CHECKBARCODE() As Boolean
        Try
            Dim BLN As Boolean = True
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(MATREC_BARCODE,'') AS BARCODE ", "", " MATERIALRECEIPT_DESC ", " AND MATERIALRECEIPT_DESC.MATREC_YEARID =  " & YearId)
            If DT.Rows.Count > 0 Then
                For Each DTR As DataRow In DT.Rows
                    For Each ROW As Windows.Forms.DataGridViewRow In GRIDMATREC.Rows
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

    Private Sub MaterialReceipt_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "PARAS" Then LBLCATEGORY.Visible = True
            If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "KDFAB" Or ClientName = "KCRAYON" Or ClientName = "RAJKRIPA" Then GBALENO.HeaderText = "Description"
            If ClientName = "SOFTAS" Then CMBLOTNO.DropDownStyle = ComboBoxStyle.DropDownList
            If ClientName = "SUCCESS" Or ClientName = "VALIANT" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KARAN" Then
                CMBCHECKSRNO.Visible = False
                CMBBALENO.Visible = True
                TXTBALENO.Enabled = False
                If ClientName = "MAHAVIRPOLYCOT" Then TXTCUT.ReadOnly = True
                If ClientName = "SUCCESS" Then
                    TXTBALENO.Enabled = True
                    TXTCUT.ReadOnly = False
                End If

            End If


            If ClientName = "SOFTAS" Or ClientName = "SHREENAKODA" Then
                CMBCHECKSRNO.TabStop = False
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
                TXTCUT.TabStop = False
                TXTWT.TabStop = False
                TXTRATE.TabStop = False
                CMBPER.TabStop = False
                If ClientName = "SOFTAS" Then TXTGRIDDESC.TabStop = False
            End If

            If ClientName = "DILIP" Or ClientName = "DILIPNEW" Or ClientName = "OWAIS" Then
                CMBQUALITY.TabStop = False
                CMBDESIGN.TabStop = False
                cmbcolor.TabStop = False
                TXTCUT.TabStop = False
                TXTWT.TabStop = False
            End If

            If ClientName = "BRILLANTO" Then
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
                TXTWT.TabStop = False
                CMBRACK.TabStop = False
                CMBSHELF.TabStop = False
                txtqty.TabStop = False
                TXTCUT.TabStop = False
            End If

            If ClientName = "AVIS" Then
                GRECDMTRS.ReadOnly = False
                cmbitemname.TabStop = False
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
                TXTCUT.TabStop = False
                TXTWT.TabStop = False
                txtqty.TabStop = False
                cmbqtyunit.Text = "LUMP"
                cmbqtyunit.TabStop = False
                CMBRACK.TabStop = False
                CMBSHELF.TabStop = False
                CMBCHECKSRNO.TabStop = False
                MATRECDATE.TabStop = False
                If UserName <> "Admin" Then
                    CHKRETURN.Visible = False
                End If
            End If

            If ITEMCOSTCENTRE = True And UserName = "Admin" Then CHKMANUALRATE.Visible = True

            If GRIDLOTNO = False Then
                CMBGRIDLOTNO.Enabled = False
            Else
                LBLLOTNO.Visible = False
                CMBLOTNO.Visible = False
            End If

            If ClientName = "MNARESH" Then
                TXTMTRS.ReadOnly = False
                TXTMTRS.Enabled = True
                TXTMTRS.TabStop = True
                TXTMTRS.BackColor = Color.White
            End If

            If ClientName = "MAHAVIRPOLYCOT" Or ClientName = "VALIANT" Or ClientName = "KARAN" Then
                TXTRATE.Visible = False
                GRATE.Visible = False
                CMBPER.Visible = False
                GPER.Visible = False
                TXTAMOUNT.Visible = False
                GAMOUNT.Visible = False
                CMBSHELF.Visible = False
                GSHELF.Visible = False
                CMBRACK.Left = TXTRECDMTRS.Right
                CMBYESNO.Left = CMBRACK.Right
                TXTDIFF.Left = CMBYESNO.Right
                CMBBALENO.DropDownStyle = ComboBoxStyle.DropDownList
                TXTPCSNO.Visible = True
            End If
            If ClientName = "SNCM" Then
                TXTRECDMTRS.ReadOnly = True
                TXTCUT.Enabled = False
                CMBQUALITY.TabStop = False
                TXTRECDMTRS.BackColor = Color.Linen
                TXTCUT.BackColor = Color.Linen
                txtqty.ReadOnly = True
                txtqty.Enabled = False
                txtqty.TabStop = False
                TXTWT.TabStop = False
                txtqty.BackColor = Color.Linen
                LBLFROM.Text = "Print Slip From"
                TXTREFLOTNO.Visible = True
                LBLREFLOTNO.Visible = True
                LBLCMPNAME.Visible = True
                CMBCMPNAME.Visible = True
                LBL3.Visible = True
                TXTPRNO.Visible = True
            End If

            If ClientName = "MONOGRAM" Then
                TXTRATE.TabStop = True
                TXTRATE.ReadOnly = False
            End If

            If ClientName = "VINTAGEINDIA" And UserName <> "Admin" Then
                TXTCHALLANNO.Enabled = False
                CMBPARTYNAME.Enabled = False
            End If
            If ClientName = "KARAN" Then
                TXTBALENO.Visible = False
                CMBQUALITY.Visible = True
                GBALENO.Visible = False
                GQUALITY.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBALENO_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBALENO.Validated
        Try
            If CMBBALENO.Text.Trim <> "" And TXTFROMNO.Text.Trim <> "" And TXTFROMTYPE.Text.Trim <> "" And (CMBLOTNO.Text.Trim <> "" Or CMBGRIDLOTNO.Text.Trim <> "") And cmbname.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                If ClientName = "MAHAVIRPOLYCOT" Or ClientName = "VALIANT" Or ClientName = "KARAN" Then
                    DT = OBJCMN.SEARCH(" ISNULL(BALMTRS,0) AS MTRS, ISNULL(DESIGN,'') AS DESIGNNO, ITEMNAME, GRNNO AS FROMSRNO, COLOR, PARTYDESIGNNO ", "", " LOT_VIEW_PCSDETAILS ", " AND DYEINGJOB = 'DYEING' AND BALENO = '" & CMBBALENO.Text.Trim & "' AND LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "' AND JOBBERNAME = '" & cmbname.Text.Trim & "' AND YEARID = " & YearId)
                Else
                    If TXTFROMTYPE.Text.Trim = "OPENING" Then
                        Dim WHERECLAUSE As String = ""
                        'If ClientName = "VALIANT" Or ClientName = "RAJKRIPA" Then WHERECLAUSE = " AND ISNULL(CAST(SM_BALENO AS VARCHAR(200)),'') = '" & CMBBALENO.Text.Trim & "'" Else WHERECLAUSE = " AND ISNULL(CAST(SM_REMARKS AS VARCHAR(200)),'') = '" & CMBBALENO.Text.Trim & "'"
                        If ClientName = "RAJKRIPA" Then WHERECLAUSE = " AND ISNULL(CAST(SM_BALENO AS VARCHAR(200)),'') = '" & CMBBALENO.Text.Trim & "'" Else WHERECLAUSE = " AND ISNULL(CAST(SM_REMARKS AS VARCHAR(200)),'') = '" & CMBBALENO.Text.Trim & "'"
                        DT = OBJCMN.SEARCH(" ISNULL(SM_MTRS,0) AS MTRS, ISNULL(DESIGN_NO,'') AS DESIGNNO, ISNULL(ITEM_NAME,'') AS ITEMNAME, SM_NO AS FROMSRNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR ", "", " STOCKMASTER INNER JOIN LEDGERS ON LEDGERS.ACC_ID = STOCKMASTER.SM_LEDGERIDTO INNER JOIN ITEMMASTER ON ITEM_ID = SM_ITEMID LEFT OUTER JOIN DESIGNMASTER ON SM_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON SM_COLORID = COLOR_ID ", WHERECLAUSE & " AND SM_LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "' AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' " & " AND SM_TYPE = 'JOBBERSTOCK' AND SM_YEARID = " & YearId)
                    Else
                        'If ClientName = "VALIANT" Then
                        '    DT = OBJCMN.SEARCH(" ISNULL(CHECKINGMASTER_DESC.CHECK_CHECKEDMTRS,0) AS MTRS, ISNULL(DESIGN_NO,'') AS DESIGNNO, ISNULL(ITEM_NAME,'') AS ITEMNAME, CHECKINGMASTER_DESC.CHECK_GRIDSRNO AS FROMSRNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR  ", "", " CHECKINGMASTER_DESC INNER JOIN CHECKINGMASTER ON CHECKINGMASTER_DESC.CHECK_NO = CHECKINGMASTER.CHECK_NO AND CHECKINGMASTER_DESC.CHECK_YEARID = CHECKINGMASTER.CHECK_YEARID INNER JOIN GRN_DESC ON CHECKINGMASTER.CHECK_GRNNO = GRN_DESC.GRN_NO AND CHECKINGMASTER_DESC.CHECK_GRIDSRNO = GRN_DESC.GRN_GRIDSRNO AND CHECKINGMASTER.CHECK_TYPE = GRN_DESC.GRN_GRIDTYPE AND CHECKINGMASTER.CHECK_YEARID = GRN_DESC.GRN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GRN_DESC.GRN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON CHECKINGMASTER.CHECK_ITEMID = ITEM_ID LEFT OUTER JOIN COLORMASTER ON GRN_DESC.GRN_COLORID = COLOR_ID ", " AND ISNULL(CHECKINGMASTER_DESC.CHECK_GRIDREMARKS,'') = '" & CMBBALENO.Text.Trim & "' AND CHECKINGMASTER.CHECK_LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "' AND CHECKINGMASTER_DESC.CHECK_YEARID = " & YearId)
                        'Else
                        '    DT = OBJCMN.SEARCH(" ISNULL(CHECKINGMASTER_DESC.CHECK_CHECKEDMTRS,0) AS MTRS, ISNULL(DESIGN_NO,'') AS DESIGNNO, ISNULL(ITEM_NAME,'') AS ITEMNAME, CHECKINGMASTER_DESC.CHECK_GRIDSRNO AS FROMSRNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR  ", "", " CHECKINGMASTER_DESC INNER JOIN CHECKINGMASTER ON CHECKINGMASTER_DESC.CHECK_NO = CHECKINGMASTER.CHECK_NO AND CHECKINGMASTER_DESC.CHECK_YEARID = CHECKINGMASTER.CHECK_YEARID INNER JOIN GRN_DESC ON CHECKINGMASTER.CHECK_GRNNO = GRN_DESC.GRN_NO AND CHECKINGMASTER.CHECK_GRNGRIDSRNO = GRN_DESC.GRN_GRIDSRNO AND CHECKINGMASTER.CHECK_TYPE = GRN_DESC.GRN_GRIDTYPE AND CHECKINGMASTER.CHECK_YEARID = GRN_DESC.GRN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GRN_DESC.GRN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON CHECKINGMASTER.CHECK_ITEMID = ITEM_ID LEFT OUTER JOIN COLORMASTER ON GRN_DESC.GRN_COLORID = COLOR_ID ", " AND ISNULL(CHECKINGMASTER_DESC.CHECK_GRIDREMARKS,'') = '" & CMBBALENO.Text.Trim & "' AND CHECKINGMASTER.CHECK_LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "' AND CHECKINGMASTER_DESC.CHECK_YEARID = " & YearId)
                        'End If
                        DT = OBJCMN.SEARCH(" ISNULL(CHECKINGMASTER_DESC.CHECK_CHECKEDMTRS,0) AS MTRS, ISNULL(DESIGN_NO,'') AS DESIGNNO, ISNULL(ITEM_NAME,'') AS ITEMNAME, CHECKINGMASTER_DESC.CHECK_GRIDSRNO AS FROMSRNO, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR  ", "", " CHECKINGMASTER_DESC INNER JOIN CHECKINGMASTER ON CHECKINGMASTER_DESC.CHECK_NO = CHECKINGMASTER.CHECK_NO AND CHECKINGMASTER_DESC.CHECK_YEARID = CHECKINGMASTER.CHECK_YEARID INNER JOIN GRN_DESC ON CHECKINGMASTER.CHECK_GRNNO = GRN_DESC.GRN_NO AND CHECKINGMASTER.CHECK_GRNGRIDSRNO = GRN_DESC.GRN_GRIDSRNO AND CHECKINGMASTER.CHECK_TYPE = GRN_DESC.GRN_GRIDTYPE AND CHECKINGMASTER.CHECK_YEARID = GRN_DESC.GRN_YEARID LEFT OUTER JOIN DESIGNMASTER ON GRN_DESC.GRN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON CHECKINGMASTER.CHECK_ITEMID = ITEM_ID LEFT OUTER JOIN COLORMASTER ON GRN_DESC.GRN_COLORID = COLOR_ID ", " AND ISNULL(CHECKINGMASTER_DESC.CHECK_GRIDREMARKS,'') = '" & CMBBALENO.Text.Trim & "' AND CHECKINGMASTER.CHECK_LOTNO = '" & CMBGRIDLOTNO.Text.Trim & "' AND CHECKINGMASTER_DESC.CHECK_YEARID = " & YearId)
                    End If

                End If
                If DT.Rows.Count > 0 Then
                    CMBCHECKSRNO.Text = Val(DT.Rows(0).Item("FROMSRNO"))
                    TXTBALENO.Text = CMBBALENO.Text.Trim
                    TXTMTRS.Text = Format(Val(DT.Rows(0).Item("MTRS")), "0.00")
                    If ClientName <> "VALIANT" Then TXTCUT.Text = Format(Val(DT.Rows(0).Item("MTRS")), "0.00")
                    CMBDESIGN.Text = DT.Rows(0).Item("DESIGNNO")
                    cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")
                    If ClientName = "VALIANT" Then
                        cmbcolor.Text = DT.Rows(0).Item("COLOR")
                        TXTPCSNO.Text = DT.Rows(0).Item("PARTYDESIGNNO")
                    End If
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
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEM_ID", " AND DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' AND DESIGN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTPROG_Click(sender As Object, e As EventArgs) Handles CMDSELECTPROG.Click
        Try
            If cmbname.Text.Trim = "" Then
                MsgBox("Select Party Name", MsgBoxStyle.Critical)
                cmbname.Focus()
                Exit Sub
            End If

            Dim DTSO As New DataTable
            Dim OBJSELECTPROG As New SelectProgram
            OBJSELECTPROG.PARTYNAME = cmbname.Text.Trim
            OBJSELECTPROG.ShowDialog()
            DTSO = OBJSELECTPROG.DT

            If DTSO.Rows.Count > 0 Then

                TXTPROGRAMNO.Text = DTSO.Rows(0).Item("PROGNO")
                'BEFORE ADDING THE ROW IN ORDERDER GRID CHECK WHETHER SAME ORDERNO AN SRNO IS PRESENT IN GRID OR NOT
                For Each DTROW As DataRow In DTSO.Rows
                    For Each ROW As DataGridViewRow In GRIDORDER.Rows
                        If Val(ROW.Cells(OFROMNO.Index).Value) = Val(DTROW("PROGNO")) And Val(ROW.Cells(OFROMSRNO.Index).Value) = Val(DTROW("GRIDSRNO")) And ROW.Cells(OFROMTYPE.Index).Value = DTROW("TYPE") Then GoTo NEXTLINE
                    Next

                    'ADD PROGRAM DETAILS IN ITEMGRID
                    If ClientName = "AVIS" And CMBGRIDLOTNO.Text.Trim <> "" And EDIT = False Then
                        CALC()
                        GRIDMATREC.Rows.Add(0, CMBGRIDLOTNO.Text.Trim, "", "", DTROW("ITEMNAME"), "", "", "", DTROW("DESIGN"), DTROW("COLOR"), 0, 0, 1, cmbqtyunit.Text.Trim, 0, Format(Val(TXTRECDMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMOUNT.Text.Trim), "0.00"), CMBRACK.Text.Trim, CMBSHELF.Text.Trim, CMBYESNO.Text.Trim, Format(Val(TXTDIFF.Text.Trim), "0.00"), TXTBARCODE.Text.Trim, 0, 0, 0, TXTFROMNO.Text.Trim, CMBCHECKSRNO.Text.Trim, TXTFROMTYPE.Text.Trim, "")
                        GRIDMATREC.Rows(0).Cells(GRECDMTRS.Index).Selected = True
                        GRIDMATREC.Focus()
                    End If
                    GRIDORDER.Rows.Add(0, DTROW("ITEMNAME"), DTROW("DESIGN"), DTROW("COLOR"), DTROW("MTRS"), DTROW("PROGNO"), DTROW("GRIDSRNO"), DTROW("TYPE"), 0, 0, Val(DTROW("RATE")))
                    cmbitemname.Text = DTROW("ITEMNAME")
                    CMBDESIGN.Text = DTROW("DESIGN")
                    cmbcolor.Text = DTROW("COLOR")
NEXTLINE:
                Next
                getsrno(GRIDMATREC)
                getsrno(GRIDORDER)

            End If

            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validated(sender As Object, e As EventArgs) Handles cmbitemname.Validated
        Try
            'GET CATEGORY
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("isnull(CATEGORY_NAME,'') AS CATEGORY, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEM_CATEGORYID = CATEGORY_ID LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNIT_ID", " AND ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEM_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                LBLCATEGORY.Text = DT.Rows(0).Item("CATEGORY")
                If DT.Rows(0).Item("UNIT") <> "" And ClientName = "SIDDHGIRI" Then cmbqtyunit.Text = DT.Rows(0).Item("UNIT")
            End If

            If cmbitemname.Text.Trim <> "" And EDIT = False Then
                'GET DISPLAY NAME IN GRIDREMARKS
                If ClientName = "RAJKRIPA" Then
                    DT = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS DISPLAYNAME", "", " ITEMMASTER ", " AND ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        For Each DTROW As DataRow In DT.Rows
                            TXTBALENO.Text = (DT.Rows(0).Item("DISPLAYNAME"))
                        Next
                    End If
                End If
            End If

            If cmbitemname.Text.Trim <> "" Then
                DT = OBJCMN.SEARCH(" ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE", "", " ITEMMASTER INNER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID", " AND ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then LBLHSN.Text = (DT.Rows(0).Item("HSNCODE"))
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBALENO_Validating(sender As Object, e As CancelEventArgs) Handles TXTBALENO.Validating
        Try
            'CHECK DUPLICATION
            If ClientName = "DILIP" Or ClientName = "DILIPNEW" And TXTBALENO.Text.Trim.Length > 0 Then
                Dim WHERECLAUSE As String = ""
                Dim objclscommon As New ClsCommon()
                If EDIT = True Then WHERECLAUSE = " AND MATERIALRECEIPT.MATREC_NO <> " & TEMPMATRECNO
                Dim dt As DataTable = objclscommon.SEARCH(" MATREC_BALENO AS BALENO, LEDGERS.ACC_cmpname", "", "  MATERIALRECEIPT INNER JOIN MATERIALRECEIPT_DESC ON MATERIALRECEIPT.MATREC_NO = MATERIALRECEIPT_DESC.MATREC_NO AND MATERIALRECEIPT.MATREC_yearid = MATERIALRECEIPT_DESC.MATREC_YEARID INNER JOIN LEDGERS ON MATERIALRECEIPT.MATREC_ledgerid = LEDGERS.Acc_id  ", " and MATERIALRECEIPT_DESC.MATREC_BALENO = '" & TXTBALENO.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & cmbname.Text.Trim & "' AND MATERIALRECEIPT.MATREC_YEARID =" & YearId & WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    MsgBox("Bale No. Already Present", MsgBoxStyle.Critical, "TEXTRADE")
                    e.Cancel = True
                End If
            End If
            If ClientName = "SNCM" And TXTBALENO.Text.Trim.Length > 0 Then
                Dim WHERECLAUSE As String = ""
                Dim objclscommon As New ClsCommon()
                If EDIT = True Then WHERECLAUSE = " AND MATERIALRECEIPT.MATREC_NO <> " & TEMPMATRECNO
                Dim dt As DataTable = objclscommon.SEARCH(" MATREC_BALENO AS BALENO, LEDGERS.ACC_cmpname", "", "  MATERIALRECEIPT INNER JOIN MATERIALRECEIPT_DESC ON MATERIALRECEIPT.MATREC_NO = MATERIALRECEIPT_DESC.MATREC_NO AND MATERIALRECEIPT.MATREC_yearid = MATERIALRECEIPT_DESC.MATREC_YEARID INNER JOIN LEDGERS ON MATERIALRECEIPT.MATREC_ledgerid = LEDGERS.Acc_id  ", " and MATERIALRECEIPT_DESC.MATREC_BALENO = '" & TXTBALENO.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & cmbname.Text.Trim & "' AND MATERIALRECEIPT.MATREC_YEARID =" & YearId & WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    MsgBox("Bale No. Already Present", MsgBoxStyle.Critical, "TEXTRADE")
                    e.Cancel = True
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress, TXTFROM.KeyPress, TXTTO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub GRIDORDER_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDORDER.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDORDER.RowCount > 0 Then GRIDORDER.Rows.RemoveAt(GRIDORDER.CurrentRow.Index)
            getsrno(GRIDORDER)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then FILLNAME(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBPARTYNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBPARTYNAME, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS", cmbtrans.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Enter(sender As Object, e As EventArgs) Handles cmbcolor.Enter
        Try
            'If Val(TXTPROGRAMNO.Text.Trim) = 0 Then FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim)
            If cmbcolor.Text.Trim = "" Then FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKMANUALRATE_CheckedChanged(sender As Object, e As EventArgs) Handles CHKMANUALRATE.CheckedChanged
        Try
            If UserName <> "Admin" Then Exit Sub
            TXTRATE.ReadOnly = Not CHKMANUALRATE.Checked
            GRATE.ReadOnly = Not CHKMANUALRATE.Checked
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

    Private Sub TXTRATE_Validated(sender As Object, e As EventArgs) Handles TXTRATE.Validated, CMBPER.Validated, TXTRECDMTRS.Validated, TXTCUT.Validated, txtqty.Validated
        CALC()
        If ClientName = "AVIS" Or ClientName = "DILIP" Or ClientName = "DILIPNEW" Or ClientName = "SNCM" Then TXTDIFF_Validated(sender, e)
    End Sub

    Private Sub CMBRACK_Validated(sender As Object, e As EventArgs) Handles CMBRACK.Validated
        Try
            If ClientName = "SOFTAS" Then TXTDIFF_Validated(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMATRECNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTMATRECNO.Validating
        Try
            If Val(TXTMATRECNO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(MATREC_NO,0)  AS MATRECNO", "", " MATERIALRECEIPT ", "  AND MATREC_NO=" & Val(TXTMATRECNO.Text.Trim) & " AND MATREC_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Dyeing Receipt No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTDIFF_Validated(sender As Object, e As EventArgs) Handles TXTDIFF.Validated
        Try
            'If CMBCHECKSRNO.Text.Trim <> "" And CMBLOTNO.Text.Trim <> "" And TXTFROMTYPE.Text.Trim <> "" And TXTFROMNO.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And Val(txtqty.Text.Trim) > 0 And cmbqtyunit.Text.Trim <> "" And Val(TXTMTRS.Text.Trim) > 0 And Val(TXTRECDMTRS.Text.Trim) > 0 Then
            If GRIDLOTNO = False And CMBGRIDLOTNO.Text.Trim = "" And CMBLOTNO.Text.Trim <> "" Then CMBGRIDLOTNO.Text = CMBLOTNO.Text.Trim

            If CMBGRIDLOTNO.Text.Trim <> "" And TXTFROMTYPE.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And Val(txtqty.Text.Trim) > 0 And cmbqtyunit.Text.Trim <> "" And Val(TXTRECDMTRS.Text.Trim) > 0 Then
                EP.Clear()
                If Not CHECKSRNO() Then
                    Exit Sub
                End If

                'If Val(CMBCHECKSRNO.Text.Trim) = 0 And LOTSTATUSONMTRS = False Then
                '    If MsgBox("Pcs No not selected, This will be considered as TP, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                'End If

                If GRIDDOUBLECLICK = False Then
                    If EDIT = True Then
                        'GET LAST BARCODE SRNO
                        Dim LSRNO As Integer = 0
                        Dim RSRNO As Integer = 0
                        Dim SNO As Integer = 0
                        LSRNO = InStr(GRIDMATREC.Rows(GRIDMATREC.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                        RSRNO = InStr(LSRNO + 1, GRIDMATREC.Rows(GRIDMATREC.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                        SNO = GRIDMATREC.Rows(GRIDMATREC.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                        TXTBARCODE.Text = "D-" & Val(TXTMATRECNO.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                    Else
                        TXTBARCODE.Text = "D-" & Val(TXTMATRECNO.Text.Trim) & "/" & GRIDMATREC.RowCount + 1 & "/" & YearId
                    End If
                End If
                If ClientName = "SNCM" And CMBPIECETYPE.Text = "FRESH" Then
                    If TXTBALENO.Text.Trim <> "" Then
                        FILLGRID()
                    Else
                        MsgBox("Enter Bale No Details", MsgBoxStyle.Critical)
                        TXTBALENO.Focus()
                    End If
                Else
                    FILLGRID()
                End If
            Else
                If ClientName <> "AVIS" And ClientName <> "SOFTAS" Then MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBALENO_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBBALENO.KeyDown
        Try
            If (ClientName = "VALIANT" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KARAN") And e.KeyCode = Keys.F1 And CMBGRIDLOTNO.Text.Trim <> "" And cmbname.Text.Trim <> "" Then
                Dim OBJSELECTPCS As New SelectPcsNoForMatRec
                OBJSELECTPCS.DYEINGNAME = cmbname.Text.Trim
                OBJSELECTPCS.LOTNO = CMBGRIDLOTNO.Text.Trim
                OBJSELECTPCS.ShowDialog()
                If OBJSELECTPCS.SRNO > 0 Then CMBBALENO.Text = OBJSELECTPCS.SRNO
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBALENO_Validating(sender As Object, e As CancelEventArgs) Handles CMBBALENO.Validating
        Try
            'CHECKING WHETHER SAME BALENO IS SELECTED IN GRID BELOW OR NOT
            If ClientName = "VALIANT" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "KARAN" And GRIDDOUBLECLICK = False And CMBBALENO.Text.Trim <> "" And CMBGRIDLOTNO.Text.Trim <> "" Then
                For Each ROW As DataGridViewRow In GRIDMATREC.Rows
                    If ROW.Cells(GLOTNO.Index).Value = CMBGRIDLOTNO.Text.Trim And ROW.Cells(GBALENO.Index).Value = CMBBALENO.Text.Trim Then
                        If MsgBox("Same Pcs No is already Taken, Wish to Proceed with Same PCS No", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            e.Cancel = True
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDMTRS_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDMTRS.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim del As Boolean = False
                If GRIDMTRS.RowCount > 0 Then
                    Dim row As Integer = GRIDMATREC.Rows(GRIDMATREC.CurrentRow.Index).Cells(gsrno.Index).Value
                    For I As Integer = 0 To DT_MTRSDETAILS.Rows.Count - 1
                        If GRIDMATREC.Rows(GRIDMATREC.CurrentRow.Index).Cells(gsrno.Index).Value = Val(DT_MTRSDETAILS.Rows(I).Item("MAINSRNO")) And GRIDMTRS.Rows(GRIDMTRS.CurrentRow.Index).Cells(DSRNO.Index).Value = Val(DT_MTRSDETAILS.Rows(I).Item("DSRNO")) Then
                            If del = False Then
                                DT_MTRSDETAILS.Rows.RemoveAt(I)
                                GRIDMTRS.Rows.RemoveAt(GRIDMTRS.CurrentRow.Index)
                                del = True
                                GoTo line1
                            End If
                        End If
                    Next
line1:
                    For I As Integer = 0 To DT_MTRSDETAILS.Rows.Count - 1
                        If GRIDMATREC.Rows(GRIDMATREC.CurrentRow.Index).Cells(gsrno.Index).Value = Val(DT_MTRSDETAILS.Rows(I).Item("MAINSRNO")) And del = True And row < Val(DT_MTRSDETAILS.Rows(I).Item(gsrno.Index)) Then
                            DT_MTRSDETAILS.Rows(I).Item("DSRNO") = Val(DT_MTRSDETAILS.Rows(I).Item("DSRNO")) - 1
                        End If
                    Next
                    TOTALMTRS()
                    getsrno(GRIDMTRS)
                    TXTDSRNO.Text = GRIDMTRS.RowCount + 1
                    TXTDMTRS.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDMTRS_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDMTRS.CellDoubleClick
        Try
            If GRIDMTRS.CurrentRow.Index >= 0 And GRIDMTRS.Item(DMTRS.Index, GRIDMTRS.CurrentRow.Index).Value <> Nothing Then
                GRIDMTRSDOUBLECLICK = True
                TXTDSRNO.Text = GRIDMTRS.Item(DSRNO.Index, GRIDMTRS.CurrentRow.Index).Value.ToString
                TXTDMTRS.Text = GRIDMTRS.Item(DMTRS.Index, GRIDMTRS.CurrentRow.Index).Value
                TEMPMTRSROW = GRIDMTRS.CurrentRow.Index
                TXTDMTRS.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLMTRSGRID()
        Try
            If GRIDMTRSDOUBLECLICK = False Then
                GRIDMTRS.Rows.Add(Val(TXTDSRNO.Text.Trim), Format(Val(TXTDMTRS.Text.Trim), "0.00"), Val(txtsrno.Text.Trim))
                'TEMPDTMTRS.Rows.Add(Val(TXTDSRNO.Text.Trim), Format(Val(TXTDMTRS.Text.Trim), "0.00"), Val(txtsrno.Text.Trim))
                getsrno(GRIDMTRS)
            ElseIf GRIDMTRSDOUBLECLICK = True Then
                'For I As Integer = 0 To TEMPDTMTRS.Rows.Count - 1
                '    If GRIDMTRS.Item(DSRNO.Index, TEMPMTRSROW).Value = TEMPDTMTRS.Rows(I).Item("DSRNO") And GRIDMTRS.Item(GMAINSRNO.Index, TEMPMTRSROW).Value = TEMPDTMTRS.Rows(I).Item("MAINSRNO") Then
                '        TEMPDTMTRS.Rows(I).Item("DMTRS") = Format(Val(TXTDMTRS.Text.Trim), "0.00")
                '        TEMPDTMTRS.Rows(I).Item("MAINSRNO") = Val(txtsrno.Text.Trim)
                '    End If
                'Next

                GRIDMTRS.Item(DSRNO.Index, TEMPMTRSROW).Value = Val(TXTDSRNO.Text.Trim)
                GRIDMTRS.Item(DMTRS.Index, TEMPMTRSROW).Value = Format(Val(TXTDMTRS.Text.Trim), "0.00")
                GRIDMTRS.Item(GMAINSRNO.Index, TEMPMTRSROW).Value = Val(txtsrno.Text.Trim)
                GRIDMTRSDOUBLECLICK = False
            End If
            TXTDMTRS.Clear()
            TXTDSRNO.Clear()
            TXTDMTRS.Focus()
            TXTDSRNO.Text = GRIDMTRS.RowCount + 1
            TOTALMTRS()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLOSE_Click(sender As Object, e As EventArgs) Handles CMDCLOSE.Click
        Try
            ' Hide the GBMTRS panel
            GBMTRS.Visible = False

            ' Focus back to the TXTRECDMTRS field
            cmbqtyunit.Focus()

        Catch ex As Exception
            ' Handle any exceptions
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Sub TOTALMTRS()
        Try
            TXTRECDMTRS.Clear()
            txtqty.Clear()
            For Each ROW As DataGridViewRow In GRIDMTRS.Rows
                TXTRECDMTRS.Text = Format(Val(TXTRECDMTRS.Text.Trim) + Val(ROW.Cells(DMTRS.Index).EditedFormattedValue), "0.00")
                txtqty.Text = Val(GRIDMTRS.RowCount)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub TXTDMTRS_Validated(sender As Object, e As EventArgs) Handles TXTDMTRS.Validated
        Try
            If Format(Val(TXTDMTRS.Text.Trim), "0.00") > 0 And GBMTRS.Visible = True Then
                FILLMTRSGRID()
                'GRIDVIEW()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub cmbqtyunit_Validated(sender As Object, e As EventArgs) Handles cmbqtyunit.Validated
    '    Try
    '        If cmbqtyunit.Text <> "" And GBMTRS.Visible = True Then TXTDMTRS.Focus()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Sub UPDATEDT(TMAINSRNO As Integer)
        Try
            'FIRST DELETE ALL ROWS OF MAINSRNO THEN REINSERT
LINE1:
            For Each ROW As DataRow In DT_MTRSDETAILS.Rows
                If Val(ROW("MAINSRNO")) = TMAINSRNO Then
                    ROW.Delete()
                    GoTo LINE1
                End If
            Next

            For Each ROW As DataGridViewRow In GRIDMTRS.Rows
                DT_MTRSDETAILS.Rows.Add(Val(ROW.Cells(DSRNO.Index).Value), ROW.Cells(DMTRS.Index).Value, TMAINSRNO)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GRIDVIEW(Optional ROWNO As Integer = -1)
        Try
            If ClientName = "SNCM" Then
                GBMTRS1.Visible = True
                If GRIDMATREC.Rows.Count > 0 Then
                    If ROWNO = -1 Then ROWNO = GRIDMATREC.CurrentRow.Index
                    GRIDMTRS1.RowCount = 0
                    For i As Integer = 0 To DT_MTRSDETAILS.Rows.Count - 1
                        If DT_MTRSDETAILS.Rows(i).Item("MAINSRNO") = Val(GRIDMATREC.Rows(ROWNO).Cells(gsrno.Index).Value) Then
                            GRIDMTRS1.Rows.Add(DT_MTRSDETAILS.Rows(i).Item("DSRNO"), DT_MTRSDETAILS.Rows(i).Item("DMTRS"), DT_MTRSDETAILS.Rows(i).Item("MAINSRNO"))
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub GRIDMATREC_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDMATREC.RowEnter
        GRIDVIEW(e.RowIndex)
    End Sub

    Private Sub cmbcolor_Validated(sender As Object, e As EventArgs) Handles cmbcolor.Validated
        Try
            If ClientName = "SNCM" Then
                If cmbitemname.Text.Trim = "" Or CMBGRIDLOTNO.Text.Trim = "" Or (CMBPIECETYPE.Text.Trim = "FRESH" And TXTBALENO.Text.Trim = "") Then
                    MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    GBMTRS.Visible = True


                    If GRIDDOUBLECLICK = False Then
                        'TEMPDTMTRS.Clear()
                        GRIDMTRS.RowCount = 0
                        GRIDMTRSDOUBLECLICK = False
                        'Dim i As Integer = 0
                        'While i < TEMPDTMTRS.Rows.Count
                        '    If TEMPDTMTRS.Rows(i).Item("MAINSRNO") = Val(txtsrno.Text.Trim) Then
                        '        TEMPDTMTRS.Rows.RemoveAt(i)
                        '        'GRIDMTRS.Rows.RemoveAt(GRIDMTRS.CurrentRow.Index)
                        '    Else
                        '        i += 1 ' Only increment if no row is removed
                        '    End If
                        'End While
                        TOTALMTRS()
                    Else
                        If GRIDMATREC.Rows.Count > 0 Then
                            GRIDMTRS.RowCount = 0
                            GRIDMTRSDOUBLECLICK = False
                            For i As Integer = 0 To DT_MTRSDETAILS.Rows.Count - 1
                                If DT_MTRSDETAILS.Rows(i).Item("MAINSRNO") = Val(GRIDMATREC.CurrentRow.Cells(gsrno.Index).Value) Then
                                    GRIDMTRS.Rows.Add(DT_MTRSDETAILS.Rows(i).Item("DSRNO"), DT_MTRSDETAILS.Rows(i).Item("DMTRS"), DT_MTRSDETAILS.Rows(i).Item("MAINSRNO"))
                                End If
                            Next
                            TOTALMTRS()
                        End If
                    End If
                    TXTDSRNO.Text = GRIDMTRS.RowCount + 1
                    TXTDMTRS.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
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

    Private Sub TXTBALENO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTBALENO.KeyPress
        Try
            If ClientName = "MSANCHITKUMAR" Then
                numkeypress(e, TXTBALENO, Me)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPRNO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTPRNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTPRNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTPRNO.Validating
        Try
            If (ClientName = "SNCM") And CMBCMPNAME.Text.Trim <> "" And Val(TXTPRNO.Text.Trim) > 0 And EDIT = False Then
                'GET YEARID FROM SELECTED CMP 
                Dim TEMPYEARID As Integer = 0
                Dim TEMPCMPID As Integer = 0
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("YEAR_ID AS YEARID, YEAR_CMPID AS CMPID", "", "YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMP_NAME = '" & CMBCMPNAME.Text.Trim & "' AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "'")
                If DT.Rows.Count > 0 Then
                    TEMPYEARID = DT.Rows(0).Item("YEARID")
                    TEMPCMPID = DT.Rows(0).Item("CMPID")
                End If



                Dim ALPARAVAL As New ArrayList
                Dim DTTABLE As New DataTable


                'FETCH PURCHASE RETURN DATA
                Dim OBJCLSGREYISS As New ClsPurchaseReturnChallan
                ALPARAVAL.Add(Val(TXTPRNO.Text.Trim))
                ALPARAVAL.Add(TEMPYEARID)
                OBJCLSGREYISS.alParaval = ALPARAVAL
                DTTABLE = OBJCLSGREYISS.SELECTPURCHASERETURNCHALLAN()
                If MsgBox("Fetch data from Purchase Return Challan No " & TXTPRNO.Text.Trim & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                For Each dr As DataRow In DTTABLE.Rows

                    'CHECKING WHETHER ITEM IS PRESENT IN CURRENT YEAR OR NOT, IF NOT PRESENT THEN ADD NEW ITEM
                    If dr("ITEM") <> "" Then
                        DT = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & dr("ITEM") & "' AND ITEM_YEARID = " & YearId)
                        If DT.Rows.Count = 0 Then
                            'ADD NEW ITEMNAME 
                            ALPARAVAL.Clear()


                            ALPARAVAL.Add("Finished Goods")
                            ALPARAVAL.Add("")   'CATEGORY
                            ALPARAVAL.Add(UCase(dr("ITEM")))        'DISPLAYNAME
                            ALPARAVAL.Add(UCase(dr("ITEM"))) 'ITEMNAME

                            ALPARAVAL.Add("")   'DEPARTMENT
                            ALPARAVAL.Add(UCase(dr("ITEM")))        'CODE
                            ALPARAVAL.Add(dr("QTYUNIT"))
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

                            Dim DTHSN As DataTable = OBJCMN.SEARCH("ISNULL(HSN_ID, 0) AS HSNCODEID", "", " HSNMASTER", " AND HSN_CODE = '" & dr("HSNCODE") & "' AND HSN_YEARID = " & YearId)
                            If DTHSN.Rows.Count > 0 Then ALPARAVAL.Add(dr("HSNCODE")) Else ALPARAVAL.Add("") 'HSNCODEID

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
                            ALPARAVAL.Add(0)    'SHADEGRIDSRNO
                            Dim objclsItemMaster As New clsItemmaster
                            objclsItemMaster.alParaval = ALPARAVAL
                            Dim IntResult As Integer = objclsItemMaster.SAVE()

                        End If
                    End If


                    If dr("DESIGN") <> "" Then
                        DTTABLE = OBJCMN.SEARCH("DESIGN_ID AS DESIGNID", "", "DESIGNMASTER", " AND DESIGN_NO = '" & dr("DESIGN") & "' AND DESIGN_YEARID = " & YearId)
                        If DTTABLE.Rows.Count = 0 Then
                            'ADD NEW DESIGN
                            Dim OBJDESIGN As New ClsDesignMaster
                            OBJDESIGN.alParaval.Add(UCase(dr("DESIGN")))
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

                            'GET ITEMNAME FROM TEMPYEARID   
                            Dim DTTEMP As DataTable = OBJCMN.SEARCH("ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEMMASTER.ITEM_ID", " AND DESIGN_NO = '" & dr("DESIGN") & "' AND DESIGN_YEARID = " & TEMPYEARID)
                            If DTTEMP.Rows.Count > 0 Then OBJDESIGN.alParaval.Add(DTTEMP.Rows(0).Item("ITEMNAME")) Else OBJDESIGN.alParaval.Add("")  'ITEM

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
                        DTTABLE = OBJCMN.SEARCH("COLOR_ID AS COLORID", "", "COLORMASTER", " AND COLOR_NAME = '" & dr("COLOR") & "' AND COLOR_YEARID = " & YearId)
                        If DTTABLE.Rows.Count = 0 Then
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
                        DTTABLE = OBJCMN.SEARCH("QUALITY_ID AS QUALITYID", "", "QUALITYMASTER", " AND QUALITY_NAME = '" & dr("QUALITY") & "' AND QUALITY_YEARID = " & YearId)
                        If DTTABLE.Rows.Count = 0 Then
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
                            OBJQUALITY.alParaval.Add("") 'IMAGEPATH



                            OBJQUALITY.alParaval.Add(CmpId)
                            OBJQUALITY.alParaval.Add(Locationid)
                            OBJQUALITY.alParaval.Add(Userid)
                            OBJQUALITY.alParaval.Add(YearId)
                            OBJQUALITY.alParaval.Add(0)
                            Dim INTRESCAT As Integer = OBJQUALITY.save()
                        End If
                    End If

                    cmbtrans.Text = dr("TRANSNAME")
                    txtlrno.Text = dr("LRNO")
                    lrdate.Value = dr("LRDATE")

                    GRIDMATREC.Rows.Add(dr("GRIDSRNO").ToString, dr("LOTNO"), 0, dr("PIECETYPE").ToString, dr("ITEM").ToString, dr("GRIDREMARKS").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGN").ToString, dr("COLOR").ToString, 0, 0, Format(Val(dr("qty")), "0.00"), dr("UNIT").ToString, 0, Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER").ToString, Format(Val(dr("AMOUNT")), "0.00"), "", "", "", "", dr("BARCODE"), 0, 0, 0, 0, 0, "", 0)
                Next
                TOTAL()
                GRIDMATREC.FirstDisplayedScrollingRowIndex = GRIDMATREC.RowCount - 1

                Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYESNO_Validated(sender As Object, e As EventArgs) Handles CMBYESNO.Validated
        Try
            If ClientName = "MAHAVIRPOLYCOT" And CMBYESNO.Text.Trim = "Y" And Val(TXTDIFF.Text.Trim) > 10 Then
                If MsgBox("Mtrs Greater then 10, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    TXTRECDMTRS.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLOSE_Validated(sender As Object, e As EventArgs) Handles CMDCLOSE.Validated
        Try
            'If GBMTRS.Visible = True Then
            '    MsgBox("CLOSE THE MTRS BOX", MsgBoxStyle.Critical)
            '    Exit Sub
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class