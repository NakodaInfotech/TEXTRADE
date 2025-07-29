
Imports BL
Imports System.IO
Imports System.ComponentModel
Imports System.Net

Public Class GRN

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK, GRIDMTRSDOUBLECLICK As Boolean
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Public EDIT As Boolean          'used for editing
    Public tempgrnno As Integer     'used for poation no while editing
    Public temptypename, TEMPPARTYBILLNO As String 'used for poation no while editing
    Dim TEMPROW, TEMPMTRSROW As Integer
    Dim TEMPUPLOADROW As Integer
    Public Shared selectPOtable As New DataTable
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer
    Public FRMSTRING As String
    Dim PARTYCHALLANNO As String
    Dim ALLOWMANUALGRNNO As Boolean = False

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

        CHKDIRECTFROMKNITTING.Checked = False
        TXTGREYKNITTINGNO.Clear()

        tstxtbillno.Clear()
        TXTFROM.Clear()
        TXTTO.Clear()
        LBLCATEGORY.Text = ""
        EP.Clear()
        GRIDBALESUMM.RowCount = 0
        GRIDORDER.RowCount = 0

        CHK30.CheckState = CheckState.Unchecked
        CHK32.CheckState = CheckState.Unchecked
        CHK34.CheckState = CheckState.Unchecked
        CHK36.CheckState = CheckState.Unchecked
        CHK38.CheckState = CheckState.Unchecked
        CHK40.CheckState = CheckState.Unchecked
        CHK42.CheckState = CheckState.Unchecked
        CHK44.CheckState = CheckState.Unchecked
        CHK46.CheckState = CheckState.Unchecked
        CHK48.CheckState = CheckState.Unchecked
        CHK50.CheckState = CheckState.Unchecked
        CHK52.CheckState = CheckState.Unchecked
        CHK54.CheckState = CheckState.Unchecked
        CHK56.CheckState = CheckState.Unchecked
        CHK58.CheckState = CheckState.Unchecked

        If ALLOWMANUALGRNNO = True Then
            txtgrnno.ReadOnly = False
            txtgrnno.BackColor = Color.LemonChiffon
        Else
            txtgrnno.ReadOnly = True
            txtgrnno.BackColor = Color.Linen
        End If

        If ClientName <> "AVIS" And ClientName <> "SNCM" Then cmbname.Enabled = True
        If ClientName <> "AMAN" And ClientName <> "MANISH" Then cmbname.Text = ""
        CMBCMPNAME.Text = ""
        TXTCHNO.Clear()
        CMBWEAVER.Text = ""
        CMBBROKER.Text = ""
        CMBSENDER.Text = ""
        CMBQUALITY.Text = ""
        CMBDESIGN.Text = ""
        If USERGODOWN <> "" Then cmbGodown.Text = USERGODOWN Else cmbGodown.Text = ""
        CMBTONAME.Text = ""
        cmbprocess.Text = ""
        TXTLOTNO.Clear()
        TXTLOTNO.Enabled = True
        txtPartyMtrs.Clear()
        txtPartyMtrs.Clear()
        CHALLANDATE.Text = Now.Date
        If ClientName <> "MANISH" Then txtchallan.Clear()
        txtpono.Clear()
        podate.Value = Now.Date
        If cmbtype.Items.Count > 0 Then cmbtype.SelectedIndex = (0)
        CHKLOTREADY.CheckState = CheckState.Unchecked

        TXTDMTRS.Clear()
        GRIDMTRS.RowCount = 0
        GBMTRS.Visible = False

        CMBDYEINGTYPE.SelectedIndex = -1

        txtadd.Clear()
        GRNDATE.Text = Now.Date
        RECDATE.Text = Now.Date

        cmbtrans.Text = ""
        txtlrno.Clear()
        lrdate.Value = Now.Date
        txttransref.Clear()
        txttransremarks.Clear()

        TXTPARTYBILLNO.Clear()
        PARTYBILLDATE.Value = Now.Date

        txtremarks.Clear()

        txtuploadsrno.Text = 1
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        gridupload.RowCount = 0
        txtimgpath.Clear()
        TXTNEWIMGPATH.Clear()
        TXTFILENAME.Clear()
        PBSoftCopy.ImageLocation = ""


        lbllocked.Visible = False
        PBlock.Visible = False
        LBLWHATSAPP.Visible = False
        LBLBARCODE.Visible = False

        'clearing itemgrid textboxes and combos
        txtsrno.Text = 1
        cmbitemname.Text = ""
        txtgridremarks.Clear()
        TXTBALENO.Clear()
        If ClientName = "KOTHARI" Then CMBQUALITY.Text = "FINISHED" Else CMBQUALITY.Text = ""
        CMBRACK.Text = ""
        CMBSHELF.Text = ""

        cmbcolor.Text = ""
        If ClientName = "MSANCHITKUMAR" Or ClientName = "KEMLINO" Or ClientName = "MOHATUL" Then txtqty.Clear() Else txtqty.Text = 1
        If ClientName = "YASHVI" Or ClientName = "KEMLINO" Or ClientName = "SOFTAS" Or ClientName = "SHREENAKODA" Or ClientName = "MANISH" Or ClientName = "VALIANT" Or ClientName = "RADHA" Then cmbqtyunit.Text = "LUMP" Else cmbqtyunit.Text = "Pcs"
        If ClientName = "AVIS" Or ClientName = "SNCM" Or ClientName = "MAHAVIRPOLYCOT" Then cmbqtyunit.Text = "Mtrs"
        If ClientName = "MNIKHIL" Or ClientName = "HRITI" Then cmbqtyunit.Text = "ROLL"

        TXTCUT.Clear()
        If ClientName = "INDRANI" Then TXTMTRS.Text = 1 Else TXTMTRS.Clear()
        TXTWT.Clear()
        gridgrn.RowCount = 0
        cmbtrans.Text = ""
        txtlrno.Clear()
        TXTPURRATE.Clear()
        TXTSALERATE.Clear()
        TXTWHOLESALERATE.Clear()

        cmdselectPO.Enabled = True
        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        If FRMSTRING = "GRN" Then
            cmbtype.Text = "G.R.N"
            txtsrno.Visible = False
            CMBQUALITY.Visible = False
            cmbitemname.Visible = False
            txtgridremarks.Visible = False

            txtqty.Visible = False
            cmbqtyunit.Visible = False
            TXTCUT.Visible = False
            TXTMTRS.Visible = False
            TXTWT.Visible = False
            GRNDATE.Visible = False
            lblgrndate.Visible = False

        ElseIf FRMSTRING = "GRNJOB" Then

            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then cmbtype.Text = "FANCY MATERIAL" Else cmbtype.Text = "Job Work"

            If ClientName = "MSANCHITKUMAR" Then
                Me.Text = "Grey Rec Note"
                LBLGRN.Text = "Grey Rec Note"
            End If

        ElseIf FRMSTRING = "GRN FANCY" Then
            cmbtype.Text = "Fancy Material"

            Me.Text = "Finish Inward"
            LBLGRN.Text = "Finish Inward"


            If ClientName = "VINTAGEINDIA" Then
                Me.Text = "Inward"
                LBLGRN.Text = "Inward"
                'TXTLOTNO.Text = txtgrnno.Text
            End If


            gitemname.HeaderText = "Item Name"
            txtPartyMtrs.Visible = False
            If ClientName = "MAHAVIRPOLYCOT" Then cmbqtyunit.Text = "UNCHECK LUMP"

        Else
            cmbtype.Text = "Inwards"
            gdesc.Width = 300
            txtgridremarks.Width = 300
            gqtyunit.Width = 120
            cmbitemname.Left = 30
            txtgridremarks.Left = 180
            cmbqtyunit.Width = 120
            TabControl1.Width = 800
            gridgrn.Width = 800
            CMBQUALITY.Visible = False
            TXTMTRS.Visible = False
            txtPartyMtrs.Visible = False
            GQUALITY.Visible = False
            GMTRS.Visible = False
            LBLWEAVER.Visible = False
            CMBWEAVER.Visible = False
            LBLBROKER.Visible = False
            CMBBROKER.Visible = False
            LBLSENDER.Visible = False
            CMBSENDER.Visible = False
            LBLBALES.Visible = False
            LBLTOTALMTRS.Visible = False
            LBLTOTALWT.Visible = False
            TXTTOTALBALES.Visible = False
        End If
        getmaxno()
        HIDEVIEW()

        'If GODOWNNAME <> "HEAD OFFICE" Then
        '    cmbGodown.Enabled = False
        'Else
        '    cmbGodown.Enabled = True
        'End If

        LBLTOTALMTRS.Text = 0
        LBLTOTALMTRS.Text = 0
        TXTTOTALBALES.Clear()
        lbltotalqty.Text = 0
        LBLTOTALWT.Text = 0
        TXTBALEWT.Clear()
        CMBPIECETYPE.Text = "FRESH"
        CMBPER.Text = "MTRS"
        TXTAMOUNT.Clear()
        LBLTOTALAMOUNT.Text = 0.0
        TXTREFLOTNO.Clear()

        If CMPCITYNAME <> "" Then CMBFROMCITY.Text = CMPCITYNAME Else CMBFROMCITY.Text = ""
        CMBTOCITY.Text = ""
        TXTVEHICLENO.Clear()
        CMBPACKING.Text = ""
        TXTDELIVERYADD.Clear()
        TXTEWAYBILLNO.Clear()
        If (ClientName = "AVIS" Or ClientName = "SNCM") And FRMSTRING = "GRNJOB" Then TXTLOTNO.Text = "Z-" & Val(txtgrnno.Text.Trim)
        If ClientName = "SNCM" And FRMSTRING = "GRN FANCY" Then TXTLOTNO.Text = Val(txtgrnno.Text.Trim) & "/" & AccFrom.Year.ToString.Substring(2, 2) & "-" & AccTo.Year.ToString.Substring(2, 2)

        If ClientName = "VINTAGEINDIA" Then
            TXTLOTNO.Text = txtgrnno.Text.Trim
            TXTBALENO.Text = gridgrn.RowCount + 1
            cmbGodown.Enabled = True
        End If

    End Sub

    Sub HIDEVIEW()
        Try
            If FRMSTRING = "GRNJOB" Then
                LBLDYEINGNAME.Visible = True
                CMBTONAME.Visible = True
                LBLLOTNO.Visible = True
                TXTLOTNO.Visible = True
                LBLLOTDATE.Visible = True
                RECDATE.Visible = True
            ElseIf FRMSTRING = "GRN FANCY" Then
                LBLGODOWN.Visible = True
                cmbGodown.Visible = True
                LBLLOTNO.Visible = True
                TXTLOTNO.Visible = True
                LBLBROKER.Visible = True
                CMBBROKER.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALWT.Text = 0.0
            lbltotalqty.Text = 0.0
            GRIDBALESUMM.RowCount = 0

            For Each ROW As DataGridViewRow In gridgrn.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    lbltotalqty.Text = Format(Val(lbltotalqty.Text) + Val(ROW.Cells(gQty.Index).EditedFormattedValue), "0.00")
                    If ROW.Cells(gcut.Index).EditedFormattedValue > 0 Then ROW.Cells(GMTRS.Index).Value = Val(ROW.Cells(gQty.Index).EditedFormattedValue) * Val(ROW.Cells(gcut.Index).EditedFormattedValue)
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")
                    If ROW.Cells(GPER.Index).EditedFormattedValue = "Mtrs" Then
                        ROW.Cells(GAMOUNT.Index).Value = Format(Val(ROW.Cells(GMTRS.Index).EditedFormattedValue) * Val(ROW.Cells(GPURRATE.Index).EditedFormattedValue), "0.00")
                    Else
                        ROW.Cells(GAMOUNT.Index).Value = Format(Val(ROW.Cells(gQty.Index).EditedFormattedValue) * Val(ROW.Cells(GPURRATE.Index).EditedFormattedValue), "0.00")
                    End If
                    LBLTOTALAMOUNT.Text = Format(Val(LBLTOTALAMOUNT.Text) + Val(ROW.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        'cmbtype.Enabled = True
        EDIT = False
        cmbname.Focus()
    End Sub

    Private Sub RECDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles RECDATE.GotFocus
        RECDATE.SelectionStart = 0
    End Sub

    Private Sub CHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHALLANDATE.GotFocus
        CHALLANDATE.SelectionStart = 0
    End Sub

    Private Sub RECDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles RECDATE.Validating
        Try
            If RECDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(RECDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRNDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles GRNDATE.GotFocus
        GRNDATE.SelectionStart = 0
    End Sub

    Private Sub GRNDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GRNDATE.Validating
        Try
            If GRNDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(GRNDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    'SAME DATE FOR CHALLANDATE / LRDATE 
                    If ClientName = "DAKSH" Or ClientName = "SHALIBHADRA" Or ClientName = "LEEFABRICO" Then
                        CHALLANDATE.Text = GRNDATE.Text
                        lrdate.Value = Convert.ToDateTime(GRNDATE.Text).Date
                        podate.Value = GRNDATE.Text
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PARTYBILLDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PARTYBILLDATE.Validating
        If Not datecheck(PARTYBILLDATE.Value) Then
            MsgBox("Date Not in Current Accounting Year")
            e.Cancel = True
        End If
    End Sub

    Private Sub TXTPARTYBILLNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTPARTYBILLNO.Validating
        Try
            If TXTPARTYBILLNO.Text.Trim <> "" Then
                If (EDIT = False) Or (EDIT = True And TEMPPARTYBILLNO <> TXTPARTYBILLNO.Text.Trim) Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" GRN_NO AS GRNNO", "", " GRN INNER JOIN LEDGERS ON GRN.GRN_LEDGERID = LEDGERS.Acc_id ", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND GRN_PARTYBILLNO = '" & TXTPARTYBILLNO.Text.Trim & "' AND GRN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        MsgBox("Party Bill No Already Exists in Entry No " & DT.Rows(0).Item("GRNNO"))
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(grn_no),0) + 1 ", "GRN", " AND GRN_TYPE='" & cmbtype.Text & "' AND grn_cmpid=" & CmpId & " and grn_locationid=" & Locationid & " and grn_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            txtgrnno.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub txtuploadsrno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtuploadsrno.KeyPress
        enterkeypress(e, Me)
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True
            If cmbtype.Text.Trim.Length = 0 Then
                EP.SetError(cmbtype, "Enter Register Name")
                bln = False
            End If

            If cmbname.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, " Please Fill Company Name ")
                bln = False
            End If

            If txtchallan.Text.Trim = "" And (ClientName = "AMAN" Or ClientName = "RADHA" Or ClientName = "LEEFABRICO" Or ClientName = "MOHATUL") Then
                EP.SetError(txtchallan, " Please Fill Challan No")
                bln = False
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If FRMSTRING = "GRN" Or FRMSTRING = "GRNJOB" Then
                If ClientName <> "AVIS" And ClientName <> "KENCOT" And CMBTONAME.Text.Trim.Length = 0 Then
                    EP.SetError(CMBTONAME, " Please Fill Company Name ")
                    bln = False
                End If
                If ClientName = "SOFTAS" And TXTLOTNO.Text.Trim = "" Then
                    EP.SetError(TXTLOTNO, " Please Enter Lot No")
                    bln = False
                End If

                'LOTNO DUPLICATION
                If CMBTONAME.Text.Trim <> "" And TXTLOTNO.Text.Trim <> "" Then
                    DT = OBJCMN.SEARCH(" GRN_NO AS GRNNO ", "", " GRN INNER JOIN LEDGERS ON GRN_TOLEDGERID = ACC_ID AND ACC_CMPID = GRN_CMPID AND ACC_LOCATIONID = GRN_LOCATIONID AND ACC_YEARID = GRN_YEARID ", " AND GRN_TYPE = '" & cmbtype.Text.Trim & "' AND ACC_CMPNAME = '" & CMBTONAME.Text.Trim & "' AND GRN_PLOTNO = '" & TXTLOTNO.Text.Trim & "' AND GRN_CMPID = " & CmpId & " AND GRN_LOCATIONID = " & Locationid & " AND GRN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If (EDIT = False) Or (EDIT = True And Val(DT.Rows(0).Item(0)) <> Val(txtgrnno.Text.Trim)) Then
                            MsgBox("Lot No Already Exists in Inward No " & DT.Rows(0).Item(0), MsgBoxStyle.Critical)
                            bln = False
                        End If
                    End If
                End If
            Else
                If cmbGodown.Text.Trim.Length = 0 Then
                    EP.SetError(cmbGodown, " Please Select Godown")
                    bln = False
                End If
                CMBTONAME.Text = ""
            End If

            If lbllocked.Visible = True And UserName <> "Admin" Then
                EP.SetError(lbllocked, "Checking Done, Delete Checking First")
                bln = False
            End If

            If gridgrn.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If





            'FOR ORDER CHECKING, FIRST REMOVE GDNQTY
            Dim TEMPORDERROWNO As Integer = -1
            Dim TEMPORDERMATCH As Boolean = False
            If GRIDORDER.RowCount > 0 Then

                For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                    ORDROW.Cells(OGRNQTY.Index).Value = 0
                    ORDROW.Cells(OGRNMTRS.Index).Value = 0
                Next

                'GET MULTISONO
                Dim MULTISONO() As String = (From row As DataGridViewRow In GRIDORDER.Rows.Cast(Of DataGridViewRow)() Where Not row.IsNewRow Select CStr(row.Cells(OFROMNO.Index).Value)).Distinct.ToArray
                txtpono.Clear()
                For Each a As String In MULTISONO
                    If txtpono.Text = "" Then
                        txtpono.Text = a
                    Else
                        txtpono.Text = txtpono.Text & "," & a
                    End If
                Next

                For Each ROW As DataGridViewRow In gridgrn.Rows
                    For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                        If ROW.Cells(gitemname.Index).Value = ORDROW.Cells(OITEMNAME.Index).Value And ROW.Cells(GDESIGN.Index).Value = ORDROW.Cells(ODESIGN.Index).Value And ROW.Cells(gcolor.Index).Value = ORDROW.Cells(OCOLOR.Index).Value Then
                            TEMPORDERMATCH = True
                            'IF ITEM / DESIGN / SHADE IS MATCHED BUT THE QTY IS FULL THEN WE NEED TO KEEP THIS ROWNO IN TEMP AND NEED TO CHECK FURTHER ALSO
                            'IF WE GET ANY NEW MATHING THEN WE NEED TO INSERT THERE
                            'IF NO MATCHING IS FOUND IN FURTHER ROWS THEN WE NEED TO ADD QTY IN THIS TEMPROW
                            If Val(ORDROW.Cells(OGRNMTRS.Index).Value) >= Val(ORDROW.Cells(OMTRS.Index).Value) Then
                                TEMPORDERROWNO = ORDROW.Index
                                GoTo CHECKNEXTLINE
                            End If
                            ORDROW.Cells(OGRNQTY.Index).Value = Val(ORDROW.Cells(OGRNQTY.Index).Value) + Val(ROW.Cells(gQty.Index).Value)
                            ORDROW.Cells(OGRNMTRS.Index).Value = Val(ORDROW.Cells(OGRNMTRS.Index).Value) + Val(ROW.Cells(GMTRS.Index).Value)
                            ROW.Cells(GPURRATE.Index).Value = Val(ORDROW.Cells(ORATE.Index).Value)
                            TEMPORDERROWNO = -1
                            Exit For
CHECKNEXTLINE:
                        End If
                    Next
                    'IF NO FURTHER MACHING IS FOUND BUT WE HAVE TEMPORDERROWNO THEN ADD VALUE IN THAT ROW
                    If TEMPORDERROWNO >= 0 Then
                        GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNQTY.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNQTY.Index).Value) + Val(ROW.Cells(gQty.Index).Value)
                        GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNMTRS.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNMTRS.Index).Value) + Val(ROW.Cells(GMTRS.Index).Value)
                        ROW.Cells(GPURRATE.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(ORATE.Index).Value)
                        TEMPORDERROWNO = -1
                    End If
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

            'lock challan for excess of 20%
            If ClientName = "ANOX" Then
                For Each ROW As DataGridViewRow In GRIDORDER.Rows
                    ROW.DefaultCellStyle.BackColor = Color.Empty
                    If Val(ROW.Cells(OGRNMTRS.Index).Value) > 0 And ((Val(ROW.Cells(OGRNMTRS.Index).Value) - Val(ROW.Cells(OMTRS.Index).Value)) / Val(ROW.Cells(OMTRS.Index).Value)) * 100 > 20 Then
                        If MsgBox("Inward Greater then Allowed Ordered Mtrs, Wish to Proceed", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            ROW.DefaultCellStyle.BackColor = Color.LightPink
                            EP.SetError(cmbname, "Inward Greater then Allowed Ordered Mtrs")
                            bln = False
                        End If
                    End If
                Next
            End If



            'coz if it it other item type then mtrs will be blank
            'if want to enable then check for materialtype
            If ClientName <> "CC" And ClientName <> "C3" And ClientName <> "GELATO" And ClientName <> "MOMAI" And ClientName <> "AXIS" And ClientName <> "KREEVE" Then
                For Each row As DataGridViewRow In gridgrn.Rows
                    DT = OBJCMN.SEARCH("MATERIAL_NAME", "", "  ITEMMASTER INNER JOIN MATERIALTYPEMASTER ON ITEMMASTER.item_materialtypeid = MATERIALTYPEMASTER.material_id AND ITEMMASTER.item_cmpid = MATERIALTYPEMASTER.material_cmpid AND ITEMMASTER.item_locationid = MATERIALTYPEMASTER.material_locationid AND ITEMMASTER.item_yearid = MATERIALTYPEMASTER.material_yearid ", " AND ITEMMASTER.ITEM_NAME = '" & row.Cells(gitemname.Index).Value & "' AND ITEM_CMPID = " & CmpId & " AND ITEM_LOCATIONID = " & Locationid & " AND ITEM_YEARID = " & YearId)
                    If Val(row.Cells(GMTRS.Index).Value) = 0 And (DT.Rows(0).Item(0) = "Raw Material" Or DT.Rows(0).Item(0) = "Semi Finished Goods" Or DT.Rows(0).Item(0) = "Finished Goods") Then
                        EP.SetError(TabControl1, "MTRS Cannot be kept Blank")
                        bln = False
                    End If

                    If ITEMCOSTCENTRE = True And Val(row.Cells(GPURRATE.Index).Value) = 0 Then
                        EP.SetError(cmbname, "Rate Cannot be 0")
                        bln = False
                    End If
                Next
            End If

            If ALLOWMANUALGRNNO = True Then
                If Val(txtgrnno.Text.Trim) <> 0 And EDIT = False Then
                    Dim OBJCMNn As New ClsCommon
                    Dim dttable As DataTable = OBJCMNn.SEARCH(" ISNULL(GRN.GRN_NO,0)  AS GRNNO", "", " GRN ", "  AND GRN.GRN_NO=" & txtgrnno.Text.Trim & " AND GRN.GRN_TYPE = '" & cmbtype.Text.Trim & "' AND GRN.GRN_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        MsgBox("GRN No Already Exist")
                        bln = False
                    End If
                End If
            End If


            If txtchallan.Text.Trim <> "" And ClientName <> "MANISH" Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(txtchallan.Text.Trim)) Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    DT = objclscommon.SEARCH(" GRN.GRN_challanno, LEDGERS.ACC_cmpname", "", " GRN inner join LEDGERS on LEDGERS.ACC_id = GRN.GRN_ledgerid AND LEDGERS.ACC_CMPid = GRN.GRN_CMPid AND LEDGERS.ACC_LOCATIONid = GRN.GRN_lOCATIONid AND LEDGERS.ACC_YEARid = GRN.GRN_YEARid", " and GRN.GRN_challanno = '" & txtchallan.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & cmbname.Text.Trim & "' AND GRN_CMPID =" & CmpId & " AND GRN_LOCATIONID =" & Locationid & " AND GRN_YEARID =" & YearId)
                    If DT.Rows.Count > 0 Then
                        EP.SetError(txtchallan, "Challan No. Already Exists")
                        bln = False
                    End If
                End If
            End If



            If FRMSTRING = "GRN FANCY" And txtchallan.Text.Trim = "" And ClientName = "SBA" Then
                EP.SetError(txtchallan, "Enter Challan No.")
                bln = False
            End If


            If FRMSTRING = "GRNJOB" Then
                If RECDATE.Text = "__/__/____" Then
                    EP.SetError(RECDATE, " Please Enter Proper Date")
                    bln = False
                End If
            End If


            If GRNDATE.Text = "__/__/____" Then
                EP.SetError(GRNDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(GRNDATE.Text) Then
                    EP.SetError(GRNDATE, "Date not in Accounting Year")
                    bln = False
                End If

                'GRN DATE CANNOT BE LESS THEN PODATE
                If Val(txtpono.Text.Trim) > 0 And Convert.ToDateTime(GRNDATE.Text).Date < podate.Value.Date Then
                    EP.SetError(RECDATE, "Date Cannot be before PO Date")
                    bln = False
                End If

                If Convert.ToDateTime(GRNDATE.Text).Date < GRNBLOCKDATE.Date Then
                    EP.SetError(GRNDATE, "Date is Blocked, Please make entries after " & Format(GRNBLOCKDATE.Date, "dd/MM/yyyy"))
                    bln = False
                End If
            End If


            If ClientName = "YASHVI" Then
                Dim OBJCM As New ClsCommon
                DT = OBJCM.SEARCH("  ITEM_TOTALWT AS TOTALWT  ", "", " ITEMMASTER", " and ITEMMASTER.ITEM_NAME = '" & gridgrn.Item(gitemname.Index, TEMPROW).Value & "'  AND ITEM_CMPID =" & CmpId & " AND ITEM_LOCATIONID =" & Locationid & " AND ITEM_YEARID =" & YearId)
                If DT.Rows.Count > 0 Then
                    If DT.Rows(0).Item("TOTALWT") <> Val(TXTBALEWT.Text.Trim) Then
                        EP.SetError(TXTBALEWT, "Total Wt Doesn't match")
                        bln = False
                    End If
                End If
            End If
            'CHEKC BARCODE IS PRESENT IN DATABASE OR NOT
            'THIS CODE IS OF NO USE, COZ WE HAVE ENTERED BARCODE IN SP
            'If Not CHECKBARCODE() Then
            '    bln = False
            '    EP.SetError(TabControl1, "Barcode already present, Please re-enter data")
            'End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Function CHECKBARCODE() As Boolean
        Try
            Dim BLN As Boolean = True
            If FRMSTRING = "GRN FANCY" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(GRN_BARCODE,'') AS BARCODE ", "", " GRN_DESC ", " AND GRN_DESC.GRN_YEARID =  " & YearId)
                If DT.Rows.Count > 0 Then
                    For Each DTR As DataRow In DT.Rows
                        For Each ROW As Windows.Forms.DataGridViewRow In gridgrn.Rows
                            If ((EDIT = False) And Convert.ToString(DTR("BARCODE")) = ROW.Cells(GBARCODE.Index).Value.ToString) Then
                                BLN = False
                                Exit Function
                            End If
                        Next
                    Next
                End If
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            'IF WE MAKE ANY CHANGES IN SAVE CODE THEN DO THE SAME CHANGS IN THE FOLLOWING FORMS
            '1) GREYRECDKNITING -- DIRECTISSUEGRN
            '2) GREYCHECKING  (some clients directly do checking)

            Cursor.Current = Cursors.WaitCursor
            EP.Clear()

            If Not ERRORVALID() Then
                Exit Sub
            End If

            ' If CMBTONAME.Text.Trim <> "" Then ADDPOUT(TXTPOUTNO)
            Dim alParaval As New ArrayList

            If txtgrnno.ReadOnly = False Then
                alParaval.Add(Val(txtgrnno.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(cmbtype.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(GRNDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(cmbGodown.Text.Trim)
            alParaval.Add(CMBBROKER.Text.Trim)
            alParaval.Add(CMBSENDER.Text.Trim)
            alParaval.Add(CMBTONAME.Text.Trim)
            alParaval.Add(TXTPOUTNO.Text.Trim)
            alParaval.Add(TXTLOTNO.Text.Trim)
            alParaval.Add(RECDATE.Text)
            alParaval.Add(cmbprocess.Text.Trim)
            alParaval.Add(txtchallan.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(txtpono.Text.Trim)
            alParaval.Add(Format(podate.Value.Date, "MM/dd/yyyy"))

            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(txtlrno.Text.Trim)
            alParaval.Add(lrdate.Value)

            alParaval.Add(txttransref.Text.Trim)
            alParaval.Add(txttransremarks.Text.Trim)

            alParaval.Add(Val(TXTBALEWT.Text.Trim))

            alParaval.Add(Val(lbltotalqty.Text))
            alParaval.Add(TXTTOTALBALES.Text.Trim)

            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(Val(LBLTOTALWT.Text))

            alParaval.Add(CMBDYEINGTYPE.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(TXTPARTYBILLNO.Text.Trim)
            alParaval.Add(Format(PARTYBILLDATE.Value.Date, "MM/dd/yyyy"))
            alParaval.Add(CHKLOTREADY.Checked)

            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)


            Dim gridsrno As String = ""
            Dim PIECETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim gridremarks As String = ""
            Dim QUALITY As String = ""
            Dim BALENO As String = ""
            Dim DESIGN As String = ""

            Dim COLOR As String = ""
            Dim qty As String = ""
            Dim qtyunit As String = ""
            Dim CUT As String = ""
            Dim MTRS As String = ""
            Dim RACK As String = ""
            Dim SHELF As String = ""
            Dim WT As String = ""
            Dim PURRATE As String = ""
            Dim SALERATE As String = ""
            Dim WHOLESALERATE As String = ""
            Dim BARCODE As String = ""
            Dim DONE As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""
            Dim PONO As String = ""
            Dim POGRIDSRNO As String = ""
            Dim CHECKDONE As String = ""
            Dim FROMTYPE As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In gridgrn.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        PIECETYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                        ITEMNAME = row.Cells(gitemname.Index).Value.ToString
                        gridremarks = row.Cells(gdesc.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = row.Cells(GBALENO.Index).Value.ToString Else BALENO = ""
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        qty = row.Cells(gQty.Index).Value.ToString
                        qtyunit = row.Cells(gqtyunit.Index).Value.ToString
                        CUT = row.Cells(gcut.Index).Value.ToString
                        MTRS = row.Cells(GMTRS.Index).Value
                        RACK = row.Cells(GRACK.Index).Value.ToString
                        SHELF = row.Cells(GSHELF.Index).Value.ToString
                        WT = row.Cells(GWT.Index).Value
                        PURRATE = row.Cells(GPURRATE.Index).Value
                        SALERATE = row.Cells(GSALERATE.Index).Value
                        WHOLESALERATE = row.Cells(GWHOLESALERATE.Index).Value
                        PER = row.Cells(GPER.Index).Value
                        AMOUNT = row.Cells(GAMOUNT.Index).Value
                        BARCODE = row.Cells(GBARCODE.Index).Value
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = 1
                        Else
                            DONE = 0
                        End If
                        OUTPCS = Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = Val(row.Cells(GOUTMTRS.Index).Value)
                        PONO = row.Cells(GPONO.Index).Value.ToString
                        POGRIDSRNO = row.Cells(GGRIDSRNO.Index).Value.ToString
                        If row.Cells(GCHECKDONE.Index).Value = True Then
                            CHECKDONE = 1
                        Else
                            CHECKDONE = 0
                        End If
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString
                        'Rate = row.Cells(GRATE.Index).Value


                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        PIECETYPE = PIECETYPE & "|" & row.Cells(GPIECETYPE.Index).Value.ToString
                        ITEMNAME = ITEMNAME & "|" & row.Cells(gitemname.Index).Value.ToString
                        gridremarks = gridremarks & "|" & row.Cells(gdesc.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString Else BALENO = BALENO & "|" & ""
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        qty = qty & "|" & row.Cells(gQty.Index).Value
                        qtyunit = qtyunit & "|" & row.Cells(gqtyunit.Index).Value
                        CUT = CUT & "|" & row.Cells(gcut.Index).Value
                        MTRS = MTRS & "|" & row.Cells(GMTRS.Index).Value
                        RACK = RACK & "|" & row.Cells(GRACK.Index).Value.ToString
                        SHELF = SHELF & "|" & row.Cells(GSHELF.Index).Value.ToString
                        WT = WT & "|" & row.Cells(GWT.Index).Value
                        PURRATE = PURRATE & "|" & row.Cells(GPURRATE.Index).Value
                        SALERATE = SALERATE & "|" & row.Cells(GSALERATE.Index).Value
                        WHOLESALERATE = WHOLESALERATE & "|" & row.Cells(GWHOLESALERATE.Index).Value
                        PER = PER & "|" & row.Cells(GPER.Index).Value
                        AMOUNT = AMOUNT & "|" & row.Cells(GAMOUNT.Index).Value
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = DONE & "|" & "1"
                        Else
                            DONE = DONE & "|" & "0"
                        End If
                        OUTPCS = OUTPCS & "|" & Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = OUTMTRS & "|" & Val(row.Cells(GOUTMTRS.Index).Value)
                        PONO = PONO & "|" & row.Cells(GPONO.Index).Value.ToString
                        POGRIDSRNO = POGRIDSRNO & "|" & row.Cells(GGRIDSRNO.Index).Value.ToString
                        If row.Cells(GCHECKDONE.Index).Value = True Then
                            CHECKDONE = CHECKDONE & "|" & "1"
                        Else
                            CHECKDONE = CHECKDONE & "|" & "0"
                        End If
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString
                        'RATE = RATE & "|" & row.Cells(GRATE.Index).Value


                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(PIECETYPE)
            alParaval.Add(ITEMNAME)
            alParaval.Add(gridremarks)
            alParaval.Add(QUALITY)
            alParaval.Add(BALENO)
            alParaval.Add(DESIGN)

            alParaval.Add(COLOR)
            alParaval.Add(qty)
            alParaval.Add(qtyunit)
            alParaval.Add(CUT)
            alParaval.Add(MTRS)
            alParaval.Add(RACK)
            alParaval.Add(SHELF)
            alParaval.Add(WT)
            alParaval.Add(PURRATE)
            alParaval.Add(SALERATE)
            alParaval.Add(WHOLESALERATE)
            alParaval.Add(PER)
            alParaval.Add(AMOUNT)
            alParaval.Add(BARCODE)
            alParaval.Add(DONE)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)
            alParaval.Add(PONO)
            alParaval.Add(POGRIDSRNO)
            alParaval.Add(CHECKDONE)
            alParaval.Add(FROMTYPE)


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
            Dim ORDERPCS As String = ""
            Dim ORDERMTRS As String = ""
            Dim ORDERFROMNO As String = ""
            Dim ORDERFROMSRNO As String = ""
            Dim ORDERFROMTYPE As String = ""
            Dim ORDERGRNPCS As String = ""
            Dim ORDERGRNMTRS As String = ""
            Dim ORDERRATE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDORDER.Rows
                If row.Cells(0).Value <> Nothing AndAlso Val(row.Cells(OGRNQTY.Index).Value) > 0 Then

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
                        ORDERGRNPCS = Val(row.Cells(OGRNQTY.Index).Value)
                        ORDERGRNMTRS = Val(row.Cells(OGRNMTRS.Index).Value)
                        ORDERRATE = Val(row.Cells(ORATE.Index).Value)
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
                        ORDERGRNPCS = ORDERGRNPCS & "|" & Val(row.Cells(OGRNQTY.Index).Value)
                        ORDERGRNMTRS = ORDERGRNMTRS & "|" & Val(row.Cells(OGRNMTRS.Index).Value)
                        ORDERRATE = ORDERRATE & "|" & Val(row.Cells(ORATE.Index).Value)
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
            alParaval.Add(ORDERGRNPCS)
            alParaval.Add(ORDERGRNMTRS)
            alParaval.Add(ORDERRATE)



            alParaval.Add(Val(LBLTOTALAMOUNT.Text.Trim))
            alParaval.Add(CHKDIRECTFROMKNITTING.Checked)
            alParaval.Add(Val(TXTGREYKNITTINGNO.Text.Trim))

            alParaval.Add(TXTVEHICLENO.Text.Trim)
            alParaval.Add(CMBFROMCITY.Text.Trim)
            alParaval.Add(CMBTOCITY.Text.Trim)
            alParaval.Add(CMBPACKING.Text.Trim)
            alParaval.Add(TXTEWAYBILLNO.Text.Trim)

            alParaval.Add(Val(TXTCHNO.Text.Trim))   'USED FOR KNOWING WHETHER WE HAVE FETCHED DATA FROM OTHER COMPANY
            alParaval.Add(TXTREFLOTNO.Text.Trim)




            Dim objclsGRN As New ClsGrn()
            objclsGRN.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = objclsGRN.SAVE()
                txtgrnno.Text = Val(DTTABLE.Rows(0).Item(0))
                MsgBox("Details Added")

                If FRMSTRING = "GRNJOB" And (ClientName = "SOFTAS" Or ClientName = "BRILLANTO" Or ClientName = "SBA" Or ClientName = "MANSI") And TXTLOTNO.Text.Trim <> "" Then
                    If MsgBox("Wish to Create GRN Checking", MsgBoxStyle.YesNo) = MsgBoxResult.No Then GoTo SKIPLINE

                    Dim OBJCHECKNIG As New GRNChecking
                    OBJCHECKNIG.AUTOCHECKING = True
                    OBJCHECKNIG.TEMPGRNNO = Val(txtgrnno.Text)
                    OBJCHECKNIG.MdiParent = MDIMain
                    OBJCHECKNIG.Show()
                End If

                'If ClientName = "SOFTAS" Then
                '    If MsgBox("Wish to Create Purchase Invoice....", MsgBoxStyle.YesNo) = MsgBoxResult.No Then GoTo SKIPLINE

                '    Dim OBJCHECKNIG As New PurchaseMaster
                '    OBJCHECKNIG.AUTOPURCHASE = True
                '    OBJCHECKNIG.TEMPGRNNO = Val(txtgrnno.Text)
                '    OBJCHECKNIG.MdiParent = MDIMain
                '    OBJCHECKNIG.Show()
                '    OBJCHECKNIG.cmbname.Focus()
                'End If

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempgrnno)
                IntResult = objclsGRN.UPDATE()
                MsgBox("Details Updated")

            End If
SKIPLINE:
            If FRMSTRING = "GRN FANCY" Then
                If ClientName <> "RADHA" And ClientName <> "SNCM" And ClientName <> "VINTAGEINDIA" And ClientName <> "BIGAPPLE" Then PRINTBARCODE()
                If ClientName <> "AMAN" And ClientName <> "RADHA" And ClientName <> "SNCM" And ClientName <> "BIGAPPLE" Then PRINTREPORT(Val(txtgrnno.Text.Trim))

                'DIRECTLY ISSUE TO JOBOUT
                If EDIT = False And (ClientName = "KRFABRICS") Then
                    If MsgBox("Issue Directly to Jobber?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        Dim OBJISSUE As New YarnDirectIssueJobber
                        OBJISSUE.ShowDialog()
                        If OBJISSUE.CMBJOBBER.Text.Trim = "" Then GoTo LINE1
                        DIRECTJOBOUT(OBJISSUE.CMBJOBBER.Text.Trim, OBJISSUE.CMBPROCESS.Text.Trim, OBJISSUE.txtremarks.Text.Trim)
                    End If
                End If
LINE1:

            End If

            If ClientName = "BRILLANTO" Or ClientName = "DAKSH" Then PRINTREPORT(Val(txtgrnno.Text.Trim))

            EDIT = False

            'COPY SCANNED DOCS FILES 
            For Each ROW As DataGridViewRow In gridupload.Rows
                If FileIO.FileSystem.DirectoryExists(Application.StartupPath & "\UPLOADDOCS") = False Then
                    FileIO.FileSystem.CreateDirectory(Application.StartupPath & "\UPLOADDOCS")
                End If
                If FileIO.FileSystem.FileExists(Application.StartupPath & "\UPLOADDOCS") = False Then
                    System.IO.File.Copy(ROW.Cells(GIMGPATH.Index).Value, ROW.Cells(GNEWIMGPATH.Index).Value, True)
                End If
            Next

            CLEAR()
            If ClientName = "AMAN" Then
                txtchallan.Focus()
            ElseIf ClientName = "LEEFABRICO" Then
                GRNDATE.Focus()
            Else
                cmbname.Focus()
            End If


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
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
                gridgrn.RowCount = 0
                Dim OBJGRN As New ClsGrn
                Dim dttable As DataTable = OBJGRN.selectGRN(Val(txtgrnno.Text.Trim), CmpId, Locationid, YearId, cmbtype.Text)
                For Each dr As DataRow In dttable.Rows
                    gridgrn.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGNNO").ToString, dr("DESC").ToString, dr("COLOR"), Format(dr("qty"), "0.00"), dr("QTYUNIT").ToString, Format(dr("CUT"), "0.00"), Format(dr("MTRS"), "0.00"), dr("RACK"), dr("SHELF"), Format(dr("WT"), "0.00"), Format(dr("PURRATE"), "0.00"), Format(dr("SALERATE"), "0.00"), Format(dr("WHOLESALERATE"), "0.00"), dr("PER").ToString, Format(Val(dr("AMOUNT")), "0.00"), dr("BARCODE").ToString, dr("DONE").ToString, Val(dr("OUTPCS")), Val(dr("OUTMTRS")), dr("GRIDPONO").ToString, dr("POGRIDSRNO").ToString, dr("CHECKDONE"), dr("FROMTYPE"))
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

                If ClientName = "KRISHNA" Or ClientName = "KOTHARI" Or ClientName = "SIMPLEX" Then
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

                For Each ROW As DataGridViewRow In gridgrn.Rows

                    'TO PRINT BARCODE FROM SELECTED SRNO
                    If (Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0) Then
                        If Val(ROW.Cells(gsrno.Index).Value) < Val(TXTFROM.Text.Trim) Or Val(ROW.Cells(gsrno.Index).Value) > Val(TXTTO.Text.Trim) Then GoTo NEXTLINE
                    End If
                    Dim GRIDREMARKS As String = ROW.Cells(gdesc.Index).Value
                    Dim TEMPNAME As String = ""
                    If ClientName = "NTC" Or ClientName = "KRFABRICS" Or ClientName = "KOTHARI" Then GRIDREMARKS = ROW.Cells(GBALENO.Index).Value
                    If ClientName = "RAJKRIPA" Or ClientName = "MOMAI" Then TEMPNAME = cmbname.Text.Trim
                    'IF barcode is used the BARCODE printING WILL BE BLOCKED
                    If Val(ROW.Cells(GOUTMTRS.Index).Value) > 0 Then GoTo NEXTLINE

                    BARCODEPRINTING(ROW.Cells(GBARCODE.Index).Value, ROW.Cells(GPIECETYPE.Index).Value, ROW.Cells(gitemname.Index).Value, ROW.Cells(GQUALITY.Index).Value, ROW.Cells(GDESIGN.Index).Value, ROW.Cells(gcolor.Index).Value, ROW.Cells(gqtyunit.Index).Value, TXTLOTNO.Text.Trim, ROW.Cells(GBALENO.Index).Value, GRIDREMARKS, Val(ROW.Cells(GMTRS.Index).Value), Val(ROW.Cells(gQty.Index).Value), Val(ROW.Cells(gcut.Index).Value), ROW.Cells(GRACK.Index).Value, TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, "", TEMPNAME, ROW.Cells(GSHELF.Index).Value, GRNDATE.Text)
                    Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.Execute_Any_String("UPDATE GRN SET GRN_BARCODEPRINTED = 1 WHERE GRN_NO = " & txtgrnno.Text.Trim & " AND GRN_YEARID = " & YearId, "", "")
                    LBLBARCODE.Visible = True

NEXTLINE:

                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub DIRECTJOBOUT(JOBBERNAME As String, PROCESS As String, REMARKS As String)
        Try

            'GET FRESH DATA FROM DATABASE (ONLY GRID)
            'THIS IS DONE COZ FOR MULTIUSER THE NOS WILL BE SAME
            gridgrn.RowCount = 0
            Dim OBJGRN As New ClsGrn()
            Dim dttable As New DataTable
            dttable = OBJGRN.selectGRN(Val(txtgrnno.Text.Trim), CmpId, Locationid, YearId, cmbtype.Text.Trim)
            For Each dr As DataRow In dttable.Rows
                gridgrn.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGNNO").ToString, dr("DESC").ToString, dr("COLOR"), Format(dr("qty"), "0.00"), dr("QTYUNIT").ToString, Format(dr("CUT"), "0.00"), Format(dr("MTRS"), "0.00"), dr("RACK"), dr("SHELF"), Format(dr("WT"), "0.00"), Format(dr("PURRATE"), "0.00"), Format(dr("SALERATE"), "0.00"), Format(dr("WHOLESALERATE"), "0.00"), dr("PER").ToString, Format(Val(dr("AMOUNT")), "0.00"), dr("BARCODE").ToString, dr("DONE").ToString, Val(dr("OUTPCS")), Val(dr("OUTMTRS")), dr("GRIDPONO").ToString, dr("POGRIDSRNO").ToString, dr("CHECKDONE"), dr("FROMTYPE"))
            Next



            Dim alParaval As New ArrayList
            alParaval.Add(0)       'JONO
            alParaval.Add(Format(Convert.ToDateTime(GRNDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbGodown.Text.Trim)
            alParaval.Add(JOBBERNAME)
            alParaval.Add(PROCESS)
            alParaval.Add("")       'PARTYNAME
            alParaval.Add(TXTLOTNO.Text.Trim) 'LOTNO
            alParaval.Add(txtchallan.Text.Trim) 'CHALLANNO
            alParaval.Add(cmbtrans.Text.Trim) 'TRANSPORT
            alParaval.Add(txtlrno.Text.Trim)       'LRNO
            alParaval.Add(Format(lrdate.Value.Date, "MM/dd/yyyy"))    'LR DATE
            alParaval.Add("")       'VEHICLENO
            alParaval.Add("")       'FROMCITY
            alParaval.Add("")       'TOCITY
            alParaval.Add("")       'PACKING
            alParaval.Add("")       'EWAYBILLNO
            alParaval.Add(0)        'NOOFBALES
            alParaval.Add(Val(lbltotalqty.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(0)        'TOTALWT
            alParaval.Add(0)        'TOTALRATE
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(0)        'LOTREADY
            alParaval.Add(CmpId)
            alParaval.Add(0)        'LOCATIONID
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)        'TRANSFER


            Dim gridsrno As String = ""
            Dim PIECETYPE As String = ""
            Dim BALENO As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim GRIDDESC As String = ""
            Dim CUT As String = ""
            Dim PCS As String = ""
            Dim UNIT As String = ""
            Dim MTRS As String = ""
            Dim WT As String = ""
            Dim RATE As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""
            Dim BARCODE As String = "" 'BARCODE ADDED
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""
            Dim FRAMES As String = ""
            Dim EMBPRODDONE As String = ""
            Dim GRIDLOTNO As String = ""

            For Each row As Windows.Forms.DataGridViewRow In gridgrn.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = Val(row.Cells(gsrno.Index).Value)
                        PIECETYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = row.Cells(GBALENO.Index).Value.ToString Else BALENO = ""
                        ITEMNAME = row.Cells(gitemname.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        GRIDDESC = ""
                        CUT = 0
                        PCS = Val(row.Cells(gQty.Index).Value)
                        UNIT = row.Cells(gqtyunit.Index).Value
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        WT = 0
                        RATE = Val(row.Cells(GPURRATE.Index).Value)
                        PER = row.Cells(GPER.Index).Value.ToString
                        AMOUNT = Val(row.Cells(GAMOUNT.Index).Value)
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        OUTPCS = 0
                        OUTMTRS = 0
                        FROMNO = Val(txtgrnno.Text.Trim)
                        FROMSRNO = Val(row.Cells(gsrno.Index).Value)
                        FROMTYPE = "GRN"
                        FRAMES = 0
                        EMBPRODDONE = 0
                        GRIDLOTNO = TXTLOTNO.Text.Trim

                    Else
                        gridsrno = gridsrno & "|" & Val(row.Cells(gsrno.Index).Value)
                        PIECETYPE = PIECETYPE & "|" & row.Cells(GPIECETYPE.Index).Value.ToString
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString Else BALENO = BALENO & "|" & ""
                        ITEMNAME = ITEMNAME & "|" & row.Cells(gitemname.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        GRIDDESC = GRIDDESC & "|" & ""
                        CUT = CUT & "|" & 0
                        PCS = PCS & "|" & Val(row.Cells(gQty.Index).Value)
                        UNIT = UNIT & "|" & row.Cells(gqtyunit.Index).Value.ToString
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        WT = WT & "|" & 0
                        RATE = RATE & "|" & Val(row.Cells(GPURRATE.Index).Value)
                        PER = PER & "|" & row.Cells(GPER.Index).Value.ToString
                        AMOUNT = AMOUNT & "|" & Val(row.Cells(GAMOUNT.Index).Value)
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        OUTPCS = OUTPCS & "|" & Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = OUTMTRS & "|" & Val(row.Cells(GOUTMTRS.Index).Value)
                        FROMNO = FROMNO & "|" & Val(txtgrnno.Text.Trim)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(gsrno.Index).Value)
                        FROMTYPE = FROMTYPE & "|" & "GRN"
                        FRAMES = FRAMES & "|" & 0
                        EMBPRODDONE = EMBPRODDONE & "|" & 0
                        GRIDLOTNO = GRIDLOTNO & "|" & TXTLOTNO.Text.Trim
                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(PIECETYPE)
            alParaval.Add(BALENO)
            alParaval.Add(ITEMNAME)
            alParaval.Add(QUALITY)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(GRIDDESC)
            alParaval.Add(CUT)
            alParaval.Add(PCS)
            alParaval.Add(UNIT)
            alParaval.Add(MTRS)
            alParaval.Add(WT)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMOUNT)
            alParaval.Add(BARCODE)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)
            alParaval.Add(FRAMES)
            alParaval.Add(EMBPRODDONE)
            alParaval.Add(GRIDLOTNO)

            Dim griduploadsrno As String = ""
            Dim imgpath As String = ""
            Dim uploadremarks As String = ""
            Dim name As String = ""
            Dim NEWIMGPATH As String = ""
            Dim FILENAME As String = ""

            alParaval.Add(griduploadsrno)
            alParaval.Add(uploadremarks)
            alParaval.Add(name)
            alParaval.Add(imgpath)
            alParaval.Add(NEWIMGPATH)
            alParaval.Add(FILENAME)


            alParaval.Add("")       'JOBOUTTYPE
            alParaval.Add(0)        'TYPEJOBOUTNO
            alParaval.Add(Val(LBLTOTALAMOUNT.Text.Trim))        'TOTALAMT

            alParaval.Add("") 'VEHICLENO
            alParaval.Add("")  'FROMCITY
            alParaval.Add("")     'TOCITY
            alParaval.Add("")     'PACKING
            alParaval.Add("")   'EWAYBILLNO
            alParaval.Add(0)    'AVGWT
            alParaval.Add(0)    'DISPATCHFROM
            alParaval.Add(0)   'width

            Dim OBJJO As New ClsCuttingIssue()
            OBJJO.alParaval = alParaval
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            dttable = OBJJO.SAVE()
            MsgBox("Entry Issued to Jobber")


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If ERRORVALID() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNoCancel)
                If tempmsg = vbCancel Then Exit Sub
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1 Then       'for Delete
            TabControl1.SelectedIndex = (0)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2 Then       'for Delete
            TabControl1.SelectedIndex = (1)
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.F5 Then
            gridgrn.Focus()
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            toolprevious_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.P And e.Alt = True Then
            Call PrintToolStripButton_Click(sender, e)
        End If
    End Sub

    Private Sub GRN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            If cmbtype.Items.Count > 0 Then cmbtype.SelectedIndex = (0)
            If ClientName = "KENCOT" Or ClientName = "LEEFABRICO" Then ALLOWMANUALGRNNO = True

            CLEAR()

            'DONE BY GULKIT
            'If ClientName = "MABHAY" Or ClientName = "SVS" Then
            '    txtqty.Text = "1"
            '    txtqty.BackColor = Color.Linen
            '    txtqty.ReadOnly = True
            'End If
            If ClientName = "SVS" Or ClientName = "BRILLANTO" Then
                txtqty.Text = "1"
                txtqty.BackColor = Color.Linen
                txtqty.ReadOnly = True
            End If

            If ClientName = "RADHA" Then
                CMBBROKER.Visible = False
                LBLBROKER.Visible = False
                CMBDYEINGTYPE.Visible = False
                LBLDYEINGTYPE.Visible = False

            End If

            If ClientName = "PURVITEX" Then
                txtgrnno.BackColor = Color.LemonChiffon
            End If

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objclsGRN As New ClsGrn()
                Dim dttable As New DataTable

                dttable = objclsGRN.selectGRN(tempgrnno, CmpId, Locationid, YearId, cmbtype.Text)

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows
                        cmbtype.Text = Convert.ToString(dr("TYPE"))

                        txtgrnno.Text = tempgrnno
                        txtgrnno.ReadOnly = True

                        GRNDATE.Text = Format(Convert.ToDateTime(dr("GRNDATE")).Date, "dd/MM/yyyy")
                        cmbname.Text = Convert.ToString(dr("NAME").ToString)
                        CMBTONAME.Text = Convert.ToString(dr("TONAME").ToString)
                        '  CMBWEAVER.Text = Convert.ToString(dr("WEAVER").ToString)
                        CMBBROKER.Text = Convert.ToString(dr("BROKER").ToString)
                        CMBSENDER.Text = Convert.ToString(dr("SENDER").ToString)
                        cmbGodown.Text = Convert.ToString(dr("GODOWN").ToString)
                        cmbprocess.Text = Convert.ToString(dr("PROCESS").ToString)
                        TXTLOTNO.Text = Convert.ToString(dr("LOTNO").ToString)
                        RECDATE.Text = dr("RECDATE")
                        TXTPOUTNO.Text = dr("POUTNO").ToString

                        txtchallan.Text = Convert.ToString(dr("CHALLANNO").ToString)
                        PARTYCHALLANNO = txtchallan.Text.Trim

                        CHALLANDATE.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")

                        txtpono.Text = Convert.ToString(dr("PONO").ToString)
                        podate.Value = Format(Convert.ToDateTime(dr("PODATE")).Date, "dd/MM/yyyy")


                        cmbtrans.Text = dr("TRANSNAME").ToString
                        txttransref.Text = dr("transrefno").ToString
                        txtlrno.Text = dr("LRNO").ToString
                        lrdate.Text = Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy")
                        txttransremarks.Text = dr("transremarks").ToString
                        TXTBALEWT.Text = Val(dr("BALEWT"))

                        TXTTOTALBALES.Text = Format(Val(dr("TOTALBALES")), "0.00")
                        CMBDYEINGTYPE.Text = dr("DYEINGTYPE")
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)

                        TXTPARTYBILLNO.Text = dr("PARTYBILLNO")
                        TEMPPARTYBILLNO = dr("PARTYBILLNO")
                        PARTYBILLDATE.Value = Format(Convert.ToDateTime(dr("PARTYBILLDATE")).Date, "dd/MM/yyyy")

                        CHKDIRECTFROMKNITTING.Checked = Convert.ToBoolean(dr("DIRECTFROMKNITTING"))
                        TXTGREYKNITTINGNO.Text = Val(dr("GREYKNITTINGNO"))

                        CHKLOTREADY.Checked = Convert.ToBoolean(dr("LOTREADY"))
                        If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True

                        TXTVEHICLENO.Text = dr("VEHICLENO")
                        CMBFROMCITY.Text = Convert.ToString(dr("FROMCITY"))
                        CMBTOCITY.Text = Convert.ToString(dr("TOCITY"))
                        CMBPACKING.Text = Convert.ToString(dr("PACKING"))
                        TXTEWAYBILLNO.Text = dr("EWAYBILLNO")
                        TXTREFLOTNO.Text = dr("REFLOTNO")



                        'Item Grid
                        gridgrn.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGNNO").ToString, dr("DESC").ToString, dr("COLOR"), Format(dr("qty"), "0.00"), dr("QTYUNIT").ToString, Format(dr("CUT"), "0.00"), Format(dr("MTRS"), "0.00"), dr("RACK"), dr("SHELF"), Format(dr("WT"), "0.00"), Format(dr("PURRATE"), "0.00"), Format(dr("SALERATE"), "0.00"), Format(dr("WHOLESALERATE"), "0.00"), dr("PER"), Format(dr("AMOUNT"), "0.00"), dr("BARCODE").ToString, dr("DONE").ToString, Val(dr("OUTPCS")), Val(dr("OUTMTRS")), dr("GRIDPONO").ToString, dr("POGRIDSRNO").ToString, dr("CHECKDONE"), dr("FROMTYPE"))

                        If Convert.ToBoolean(dr("CHECKDONE")) = True Or Convert.ToBoolean(dr("INHOUSECHECKDONE")) = True Or Convert.ToBoolean(dr("DONE")) = True Or Convert.ToBoolean(dr("PROGRAMDONE")) = True Or Val(dr("OUTMTRS")) > 0 Then
                            If Convert.ToBoolean(dr("CHECKDONE")) = True Or Convert.ToBoolean(dr("INHOUSECHECKDONE")) = True Then TXTLOTNO.Enabled = False
                            gridgrn.Rows(gridgrn.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            If ClientName <> "KARAN" Then
                                lbllocked.Visible = True
                                PBlock.Visible = True
                            End If
                        End If
                        If Convert.ToBoolean(dr("BARCODEPRINTED")) = True Then LBLBARCODE.Visible = True
                    Next
                    cmbtype.Enabled = False

                    TOTAL()
                    Validate()
                Else
                    EDIT = False
                    CLEAR()
                End If

                Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.SEARCH(" GRN_GRIDSRNO AS GRIDSRNO, GRN_REMARKS AS REMARKS, GRN_NAME AS NAME, GRN_IMGPATH AS IMGPATH, GRN_NEWIMGPATH AS NEWIMGPATH", "", " GRN_UPLOAD", " AND GRN_NO = " & tempgrnno & " AND GRN_CMPID = " & CmpId & " AND GRN_LOCATIONID = " & Locationid & " AND GRN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), DTR("IMGPATH"), DTR("NEWIMGPATH"))
                    Next
                End If


                'ORDER GRID
                'Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.SEARCH(" GRN_PODETAILS.GRN_GRIDSRNO AS GRIDSRNO, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, GRN_PODETAILS.GRN_ORDERPCS AS ORDERQTY, ISNULL(GRN_PODETAILS.GRN_ORDERMTRS,0) AS ORDERMTRS, GRN_PODETAILS.GRN_FROMNO AS FROMNO, GRN_PODETAILS.GRN_FROMSRNO AS FROMSRNO, GRN_PODETAILS.GRN_FROMTYPE AS FROMTYPE, GRN_PODETAILS.GRN_PCS AS GRNQTY, ISNULL(GRN_PODETAILS.GRN_MTRS,0) AS GRNMTRS, ISNULL(GRN_PODETAILS.GRN_RATE,0) AS RATE ", "", " GRN_PODETAILS INNER JOIN ITEMMASTER ON GRN_PODETAILS.GRN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON GRN_PODETAILS.GRN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GRN_PODETAILS.GRN_DESIGNID = DESIGNMASTER.DESIGN_id  ", " AND GRN_PODETAILS.GRN_NO = " & tempgrnno & " AND GRN_PODETAILS.GRN_TYPE = '" & cmbtype.Text.Trim & "'  AND GRN_PODETAILS.GRN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        GRIDORDER.Rows.Add(Val(DTR("GRIDSRNO")), DTR("ITEMNAME"), DTR("DESIGNNO"), DTR("COLOR"), Val(DTR("ORDERQTY")), Val(DTR("ORDERMTRS")), Val(DTR("FROMNO")), Val(DTR("FROMSRNO")), DTR("FROMTYPE"), Val(DTR("GRNQTY")), Val(DTR("GRNMTRS")), Val(DTR("RATE")))
                    Next
                End If
                getsrno(GRIDORDER)

                chkchange.CheckState = CheckState.Checked


            End If

            If gridgrn.RowCount > 0 Then
                txtsrno.Text = Val(gridgrn.Rows(gridgrn.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If
            If ClientName = "VINTAGEINDIA" Then
                TXTBALENO.Text = gridgrn.RowCount + 1
                If cmbGodown.Text.Trim <> "" Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As New DataTable
                    DT = OBJCMN.SEARCH(" ISNULL(GRN.GRN_GODOWNCHECKED,0 )AS GODOWNCHECKED ", "", " GRN ", " AND GRN_CMPID = " & CmpId & " AND GRN_NO  = " & tempgrnno & " AND GRN_GODOWNCHECKED  = 1 AND GRN_LOCATIONID = " & Locationid & " AND GRN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        cmbGodown.Enabled = False
                    Else
                        cmbGodown.Enabled = True
                    End If
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Sub FILLCMB()
        Try
            If cmbGodown.Text.Trim = "" Then fillGODOWN(cmbGodown, EDIT)
            If cmbname.Text.Trim = "" Then
                If ClientName = "AMAN" Then
                    FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' and ACC_TYPE = 'ACCOUNTS'")
                ElseIf ClientName = "TINUMINU" Or ClientName = "RADHA" Or ClientName = "SIMPLEX" Then
                    FILLNAME(cmbname, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') and ACC_TYPE = 'ACCOUNTS'")
                Else
                    FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
                End If
            End If
            If CMBBROKER.Text.Trim = "" Then FILLNAME(CMBBROKER, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'AGENT'")
            If CMBTONAME.Text.Trim = "" Then FILLNAME(CMBTONAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
            If cmbprocess.Text.Trim = "" Then FILLPROCESS(cmbprocess)
            'If CMBCODE.Text.Trim = "" Then fillACCCODE(CMBCODE, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
            'If CMBTOCODE.Text.Trim = "" Then fillACCCODE(CMBTOCODE, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")

            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT'")
            If FRMSTRING = "Inwards" Then
                fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'ITEM'")
            Else
                fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            End If
            fillQUALITY(CMBQUALITY, EDIT)
            FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
            fillunit(cmbqtyunit)
            FILLDYEDTYPE(CMBDYEINGTYPE)
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

    Private Sub cmbGodown_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbGodown.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectGodown
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then cmbGodown.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbGodown.Validating
        Try
            If cmbGodown.Text.Trim <> "" Then GODOWNVALIDATE(cmbGodown, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPIECETYPE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBPIECETYPE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJPieceType As New SelectPieceType
                OBJPieceType.ShowDialog()
                If OBJPieceType.TEMPNAME <> "" Then CMBPIECETYPE.Text = OBJPieceType.TEMPNAME
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

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objgrndetails As New GRNDetails
            objgrndetails.MdiParent = MDIMain
            objgrndetails.FRMSTRING = FRMSTRING
            objgrndetails.Show()
            objgrndetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESS_ENTER(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbprocess.Enter
        Try
            If cmbprocess.Text.Trim = "" Then FILLPROCESS(cmbprocess)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then NAMEVALIDATE(cmbtrans, CMBCODE, e, Me, TXTTRANSADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "Sundry Creditors", "TRANSPORT")
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

    Private Sub txtchallan_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtchallan.Validating
        Try
            If txtchallan.Text.Trim.Length > 0 And ClientName <> "MANISH" Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(txtchallan.Text.Trim)) Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    Dim dt As New DataTable
                    dt = objclscommon.SEARCH(" GRN.GRN_challanno, LEDGERS.ACC_cmpname", "", " GRN inner join LEDGERS on LEDGERS.ACC_id = GRN.GRN_ledgerid AND LEDGERS.ACC_CMPid = GRN.GRN_CMPid AND LEDGERS.ACC_LOCATIONid = GRN.GRN_lOCATIONid AND LEDGERS.ACC_YEARid = GRN.GRN_YEARid", " and GRN.GRN_challanno = '" & txtchallan.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & cmbname.Text.Trim & "' AND GRN_CMPID =" & CmpId & " AND GRN_LOCATIONID =" & Locationid & " AND GRN_YEARID =" & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Challan No. Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdselectpo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdselectPO.Click
        Try

            If cmbname.Text.Trim = "" And ClientName <> "AVIS" And ClientName <> "SNCM" And ClientName <> "REALCORPORATION" Then
                MsgBox("Select Party Name", MsgBoxStyle.Critical)
                cmbname.Focus()
                Exit Sub
            End If


            If CMBTONAME.Text.Trim = "" And (ClientName = "AVIS" Or ClientName = "SNCM" Or ClientName = "REALCORPORATION") And FRMSTRING = "GRNJOB" Then
                MsgBox("Select Dyeing Name", MsgBoxStyle.Critical)
                CMBTONAME.Focus()
                Exit Sub
            End If


            Dim DTPO As New DataTable
            If (ClientName = "AVIS" Or ClientName = "SNCM" Or ClientName = "REALCORPORATION") And FRMSTRING = "GRNJOB" Then
                Dim OBJSELECTPO As New SelectGreyIssueProcess
                OBJSELECTPO.DYEINGNAME = CMBTONAME.Text.Trim
                OBJSELECTPO.ShowDialog()
                DTPO = OBJSELECTPO.DT
                If DTPO.Rows.Count > 0 Then
                    ''  GETTING DISTINCT CHALLAN NO IN TEXTBOX
                    Dim DV As DataView = DTPO.DefaultView
                    Dim NEWDT As DataTable = DV.ToTable(True, "REFLOTNO")
                    For Each DTR As DataRow In NEWDT.Rows
                        If TXTREFLOTNO.Text.Trim = "" Then
                            TXTREFLOTNO.Text = DTR("REFLOTNO").ToString
                        Else
                            TXTREFLOTNO.Text = TXTREFLOTNO.Text & "," & DTR("REFLOTNO").ToString
                        End If
                    Next
                    cmbname.Text = DTPO.Rows(0).Item("PURNAME")
                    cmbtrans.Text = DTPO.Rows(0).Item("TRANSPORT")
                    txtlrno.Text = DTPO.Rows(0).Item("LRNO")
                    If ClientName = "AVIS" Then txtchallan.Text = DTPO.Rows(0).Item("LRNO")
                    lrdate.Value = DTPO.Rows(0).Item("LRDATE")
                    txtremarks.Text = DTPO.Rows(0).Item("REMARKS")
                    'TXTREFLOTNO.Text = DTPO.Rows(0).Item("REFLOTNO")
                    'TXTREFLOTNO.Text = Convert.ToString(DTPO.Rows(0).Item("REFLOTNO").ToString)

                    For Each ROW As DataRow In DTPO.Rows
                        gridgrn.Rows.Add(0, "FRESH", ROW("ITEMNAME"), "", "", ROW("DESIGNNO"), "", ROW("COLOR"), Val(ROW("PCS")), "Pcs", 0, Val(ROW("MTRS")), "", "", 0, Val(ROW("RATE")), 0, 0, "Mtrs", 0, "", 0, 0, 0, Val(ROW("GREYISSNO")), Val(ROW("GRIDSRNO")), 0, ROW("TYPE"))
                    Next
                End If



            Else

                Dim OBJSELECTPO As New SelectPO
                OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
                OBJSELECTPO.FRMSTRING = FRMSTRING
                OBJSELECTPO.ShowDialog()
                DTPO = OBJSELECTPO.DT
                If DTPO.Rows.Count > 0 Then

                    ''  GETTING DISTINCT PONO NO IN TEXTBOX
                    Dim DV As DataView = DTPO.DefaultView
                    Dim NEWDT As DataTable = DV.ToTable(True, "PONO")
                    For Each DTR As DataRow In NEWDT.Rows
                        If txtpono.Text.Trim = "" Then
                            txtpono.Text = DTR("PONO").ToString
                        Else
                            txtpono.Text = txtpono.Text & "," & DTR("PONO").ToString
                        End If
                    Next

                    fillledger(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = '" & DTPO.Rows(0).Item("GROUPNAME") & "' ")
                    cmbname.Text = DTPO.Rows(0).Item("NAME")
                    CMBBROKER.Text = DTPO.Rows(0).Item("AGENT")
                    cmbtrans.Text = DTPO.Rows(0).Item("TRANSPORT")

                    TXTPOGRIDSRNO.Text = DTPO.Rows(0).Item("GRIDSRNO")
                    podate.Value = DTPO.Rows(0).Item("PODATE")

                    CMBTONAME.Text = DTPO.Rows(0).Item("TONAME")
                    DUEDATE.Value = DTPO.Rows(0).Item("DUEDATE")
                    txtpono.Enabled = False


                    'BEFORE ADDING THE ROW IN ORDERDER GRID CHECK WHETHER SAME ORDERNO AN SRNO IS PRESENT IN GRID OR NOT
                    For Each DTROW As DataRow In DTPO.Rows
                        For Each ROW As DataGridViewRow In GRIDORDER.Rows
                            If Val(ROW.Cells(OFROMNO.Index).Value) = Val(DTROW("PONO")) And Val(ROW.Cells(OFROMSRNO.Index).Value) = Val(DTROW("GRIDSRNO")) And ROW.Cells(OFROMTYPE.Index).Value = DTROW("TYPE") Then GoTo NEXTLINE
                        Next

                        GRIDORDER.Rows.Add(0, DTROW("ITEMNAME"), DTROW("DESIGNNO"), DTROW("COLOR"), Val(DTROW("QTY")), Val(DTROW("MTRS")), DTROW("PONO"), DTROW("GRIDSRNO"), DTROW("TYPE"), 0, 0, Val(DTROW("RATE")))
                        If ClientName = "MOMAI" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                            'FILL SAME DATA IN GRNGRID
                            gridgrn.Rows.Add(0, "FRESH", DTROW("ITEMNAME"), "", "", DTROW("DESIGNNO"), "", DTROW("COLOR"), Val(DTROW("QTY")), "Pcs", 0, Val(DTROW("MTRS")), "", "", 0, 0, 0, 0, "Mtrs", 0, "", 0, 0, 0, DTROW("PONO"), DTROW("GRIDSRNO"), 0, "")
                        End If
NEXTLINE:
                    Next
                    getsrno(GRIDORDER)

                End If

            End If
            getsrno(gridgrn)
            cmdselectPO.Enabled = True
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                gridgrn.RowCount = 0
                tempgrnno = Val(tstxtbillno.Text)
                If tempgrnno > 0 Then
                    EDIT = True
                    GRN_Load(sender, e)
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

        gridgrn.Enabled = True
        Dim TEMPQTY As Integer
        'Dim TEMPQTY As Integer = Val(txtqty.Text.Trim)

        If FRMSTRING = "GRN FANCY" Then
            TEMPQTY = Val(txtqty.Text.Trim)
        Else
            TEMPQTY = "1"
        End If

        If ClientName = "SVS" Or ClientName = "CC" Or ClientName = "C3" Or ClientName = "GELATO" Or ClientName = "INDRANI" Or ClientName = "SST" Then txtqty.Text = "1"
        If ClientName = "MOMAI" Or (ClientName = "AXIS" And Val(TXTMTRS.Text.Trim) = 0) Then
            If CMBPIECETYPE.Text.Trim = "FRESH" Then txtqty.Text = 1 Else TEMPQTY = 1
        End If

        If FRMSTRING = "GRN FANCY" And Val(TXTCUT.Text.Trim) > 0 Then
            If ClientName = "SBA" Or ClientName = "POOJA" Or ClientName = "KARAN" Or ClientName = "AVIS" Or ClientName = "SNCM" Or ClientName = "MOHATUL" Or ClientName = "GELATO" Or ClientName = "CHINTAN" Or ClientName = "SSC" Or ClientName = "SST" Or ClientName = "MBB" Or ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "AFW" Then txtqty.Text = "1"
        End If

        If (ClientName = "SSC" Or ClientName = "MBB") And Val(TXTCUT.Text.Trim) = 0 Then TEMPQTY = 1

        If GRIDDOUBLECLICK = False Then
            If ClientName = "CC" Or ClientName = "C3" Or (ClientName = "AXIS" And Val(TXTMTRS.Text.Trim) = 0) Or ClientName = "POOJA" Or ClientName = "SBA" Or ClientName = "MOMAI" Or ClientName = "INDRANI" Or ClientName = "GELATO" Or ClientName = "KARAN" Or ClientName = "AVIS" Or ClientName = "SNCM" Or ClientName = "MOHATUL" Or ClientName = "MNARESH" Or ClientName = "CHINTAN" Or ClientName = "SSC" Or ClientName = "SST" Or ClientName = "MBB" Or ClientName = "SONU" Or ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "AFW" Then
                For I As Integer = 1 To TEMPQTY
                    If FRMSTRING = "GRN FANCY" Then
                        If GRIDDOUBLECLICK = False Then
                            If EDIT = True Then
                                'GET LAST BARCODE SRNO
                                Dim LSRNO As Integer = 0
                                Dim RSRNO As Integer = 0
                                Dim SNO As Integer = 0
                                LSRNO = InStr(gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                                RSRNO = InStr(LSRNO + 1, gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                                SNO = gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                                TXTBARCODE.Text = "G-" & Val(txtgrnno.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                            Else
                                TXTBARCODE.Text = "G-" & Val(txtgrnno.Text.Trim) & "/" & gridgrn.RowCount + 1 & "/" & YearId
                            End If
                        End If
                    End If


                    'FETCH RATE FROM DESIGNMASTER
                    If ClientName = "GELATO" And (CHK30.Checked = True Or CHK32.Checked = True Or CHK34.Checked = True Or CHK36.Checked = True Or CHK38.Checked = True Or CHK40.Checked = True Or CHK42.Checked = True Or CHK44.Checked = True Or CHK46.Checked = True Or CHK48.Checked = True Or CHK50.Checked = True Or CHK52.Checked = True Or CHK54.Checked = True Or CHK56.Checked = True Or CHK58.Checked = True) Then
                        Dim TEMPSIZE As String = ""
                        'WE HAVE INSERT THE RECORDS OF ALL THE SIZES SELECTED FOR GELATO
                        For K As Integer = 1 To 15
                            If K = 1 And CHK30.CheckState = CheckState.Checked Then
                                TEMPSIZE = "30"
                                GoTo INSERTDATA
                            End If
                            If K = 2 And CHK32.CheckState = CheckState.Checked Then
                                TEMPSIZE = "32"
                                GoTo INSERTDATA
                            End If
                            If K = 3 And CHK34.CheckState = CheckState.Checked Then
                                TEMPSIZE = "34"
                                GoTo INSERTDATA
                            End If
                            If K = 4 And CHK36.CheckState = CheckState.Checked Then
                                TEMPSIZE = "36"
                                GoTo INSERTDATA
                            End If
                            If K = 5 And CHK38.CheckState = CheckState.Checked Then
                                TEMPSIZE = "38"
                                GoTo INSERTDATA
                            End If
                            If K = 6 And CHK40.CheckState = CheckState.Checked Then
                                TEMPSIZE = "40"
                                GoTo INSERTDATA
                            End If
                            If K = 7 And CHK42.CheckState = CheckState.Checked Then
                                TEMPSIZE = "42"
                                GoTo INSERTDATA
                            End If
                            If K = 8 And CHK44.CheckState = CheckState.Checked Then
                                TEMPSIZE = "44"
                                GoTo INSERTDATA
                            End If
                            If K = 9 And CHK46.CheckState = CheckState.Checked Then
                                TEMPSIZE = "46"
                                GoTo INSERTDATA
                            End If
                            If K = 10 And CHK48.CheckState = CheckState.Checked Then
                                TEMPSIZE = "48"
                                GoTo INSERTDATA
                            End If
                            If K = 11 And CHK50.CheckState = CheckState.Checked Then
                                TEMPSIZE = "50"
                                GoTo INSERTDATA
                            End If
                            If K = 12 And CHK52.CheckState = CheckState.Checked Then
                                TEMPSIZE = "52"
                                GoTo INSERTDATA
                            End If
                            If K = 13 And CHK54.CheckState = CheckState.Checked Then
                                TEMPSIZE = "54"
                                GoTo INSERTDATA
                            End If
                            If K = 14 And CHK56.CheckState = CheckState.Checked Then
                                TEMPSIZE = "56"
                                GoTo INSERTDATA
                            End If
                            If K = 15 And CHK58.CheckState = CheckState.Checked Then
                                TEMPSIZE = "58"
                                GoTo INSERTDATA
                            End If


INSERTDATA:
                            If TEMPSIZE <> "" Then gridgrn.Rows.Add(Val(txtsrno.Text.Trim), CMBPIECETYPE.Text.Trim, cmbitemname.Text.Trim, CMBQUALITY.Text.Trim, TXTBALENO.Text.Trim, CMBDESIGN.Text.Trim, txtgridremarks.Text.Trim, TEMPSIZE, Format(Val(txtqty.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), CMBRACK.Text.Trim, CMBSHELF.Text.Trim, Format(Val(TXTWT.Text.Trim), "0.00"), Format(Val(TXTPURRATE.Text.Trim), "0.00"), Format(Val(TXTSALERATE.Text.Trim), "0.00"), Format(Val(TXTWHOLESALERATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Val(TXTAMOUNT.Text.Trim), TXTBARCODE.Text.Trim, 0, 0, 0, 0, 0, 0, "")
                            TEMPSIZE = ""
                        Next
                        GoTo LINE1
                    End If

                    If FRMSTRING <> "GRN FANCY" Then TXTBARCODE.Clear()
                    gridgrn.Rows.Add(Val(txtsrno.Text.Trim), CMBPIECETYPE.Text.Trim, cmbitemname.Text.Trim, CMBQUALITY.Text.Trim, TXTBALENO.Text.Trim, CMBDESIGN.Text.Trim, txtgridremarks.Text.Trim, cmbcolor.Text.Trim, Format(Val(txtqty.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), CMBRACK.Text.Trim, CMBSHELF.Text.Trim, Format(Val(TXTWT.Text.Trim), "0.00"), Format(Val(TXTPURRATE.Text.Trim), "0.00"), Format(Val(TXTSALERATE.Text.Trim), "0.00"), Format(Val(TXTWHOLESALERATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Val(TXTAMOUNT.Text.Trim), TXTBARCODE.Text.Trim, 0, 0, 0, 0, 0, 0, "")
LINE1:
                Next
            Else
                If FRMSTRING <> "GRN FANCY" Then TXTBARCODE.Clear()
                gridgrn.Rows.Add(Val(txtsrno.Text.Trim), CMBPIECETYPE.Text.Trim, cmbitemname.Text.Trim, CMBQUALITY.Text.Trim, TXTBALENO.Text.Trim, CMBDESIGN.Text.Trim, txtgridremarks.Text.Trim, cmbcolor.Text.Trim, Format(Val(txtqty.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), CMBRACK.Text.Trim, CMBSHELF.Text.Trim, Format(Val(TXTWT.Text.Trim), "0.00"), Format(Val(TXTPURRATE.Text.Trim), "0.00"), Format(Val(TXTSALERATE.Text.Trim), "0.00"), Format(Val(TXTWHOLESALERATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Val(TXTAMOUNT.Text.Trim), TXTBARCODE.Text.Trim, 0, 0, 0, 0, 0, 0, "")
            End If
            getsrno(gridgrn)
        ElseIf GRIDDOUBLECLICK = True Then
            If FRMSTRING <> "GRN FANCY" Then TXTBARCODE.Clear()

            gridgrn.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            gridgrn.Item(GPIECETYPE.Index, TEMPROW).Value = CMBPIECETYPE.Text.Trim

            gridgrn.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
            gridgrn.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            gridgrn.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
            gridgrn.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            gridgrn.Item(gdesc.Index, TEMPROW).Value = txtgridremarks.Text.Trim
            gridgrn.Item(gcolor.Index, TEMPROW).Value = cmbcolor.Text.Trim
            gridgrn.Item(gQty.Index, TEMPROW).Value = Format(Val(txtqty.Text.Trim), "0.00")
            gridgrn.Item(gqtyunit.Index, TEMPROW).Value = cmbqtyunit.Text.Trim
            gridgrn.Item(gcut.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
            gridgrn.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            gridgrn.Item(GRACK.Index, TEMPROW).Value = CMBRACK.Text.Trim
            gridgrn.Item(GSHELF.Index, TEMPROW).Value = CMBSHELF.Text.Trim
            gridgrn.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.00")
            'gridgrn.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")
            gridgrn.Item(GPER.Index, TEMPROW).Value = Format(Val(CMBPER.Text.Trim), "0.00")
            gridgrn.Item(GAMOUNT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")
            gridgrn.Item(GPURRATE.Index, TEMPROW).Value = Format(Val(TXTPURRATE.Text.Trim), "0.00")
            gridgrn.Item(GSALERATE.Index, TEMPROW).Value = Format(Val(TXTSALERATE.Text.Trim), "0.00")
            gridgrn.Item(GWHOLESALERATE.Index, TEMPROW).Value = Format(Val(TXTWHOLESALERATE.Text.Trim), "0.00")
            gridgrn.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
            gridgrn.Item(GAMOUNT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")
            gridgrn.Item(GBARCODE.Index, TEMPROW).Value = TXTBARCODE.Text.Trim
            GRIDDOUBLECLICK = False

        End If

        TOTAL()

        gridgrn.FirstDisplayedScrollingRowIndex = gridgrn.RowCount - 1

        If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "BRILLANTO" Or ClientName = "INDRANI" Or ClientName = "VINIT" Or ClientName = "VALIANT" Or ClientName = "BIGAPPLE" Then TXTBALENO.Clear()
        If ClientName = "SOFTAS" Then CMBQUALITY.Text = ""

        txtgridremarks.Clear()
        TXTCUT.Clear()
        If ClientName = "INDRANI" Then TXTMTRS.Text = 1 Else TXTMTRS.Clear()
        TXTPURRATE.Clear()
        TXTSALERATE.Clear()
        TXTWHOLESALERATE.Clear()
        TXTWT.Clear()
        txtPartyMtrs.Clear()
        txtCheckPcs.Clear()
        TXTBARCODE.Clear()
        txtsrno.Text = gridgrn.RowCount + 1

        If ClientName = "INDRANI" Or ClientName = "AXIS" Or ClientName = "SUCCESS" Or ClientName = "BRILLANTO" Or ClientName = "VINIT" Or ClientName = "VALIANT" Or ClientName = "REALCORPORATION" Or ClientName = "BIGAPPLE" Then
            TXTBALENO.Focus()
        ElseIf ClientName = "MOMAI" Or ClientName = "KREEVE" Or ClientName = "BALAJI" Or ClientName = "TINUMINU" Then
            If FRMSTRING = "GRN FANCY" And (ClientName = "BALAJI") Then TXTBALENO.Text = Val(TXTBALENO.Text.Trim) + 1
            CMBDESIGN.Text = ""
            txtqty.Clear()
            CMBDESIGN.Focus()
        ElseIf ClientName = "DILIP" Or ClientName = "DILIPNEW" Or ClientName = "MAHAJAN" Or ClientName = "VINTAGEINDIA" Then
            If FRMSTRING = "GRN FANCY" Then TXTBALENO.Text = Val(TXTBALENO.Text.Trim) + 1
            txtqty.Focus()
        ElseIf ClientName = "MOHATUL" Or ClientName = "MNIKHIL" Or ClientName = "HRITI" Or ClientName = "ANOX" Or ClientName = "MANISH" Or ClientName = "AFW" Then
            cmbcolor.Focus()
        ElseIf ClientName = "GELATO" Or ClientName = "CC" Or ClientName = "C3" Or ClientName = "LEEFABRICO" Or ClientName = "SONU" Or ClientName = "SIDDHPOLYCOT" Then
            cmbitemname.Focus()
            If ClientName = "LEEFABRICO" Or ClientName = "SIDDHPOLYCOT" Then txtqty.Clear()
        Else
            TXTMTRS.Focus()
        End If


        If ClientName = "VINTAGEINDIA" Then txtqty.Text = 1

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
        TXTNEWIMGPATH.Text = Application.StartupPath & "\UPLOADDOCS\" & txtgrnno.Text.Trim & txtuploadsrno.Text.Trim & TXTFILENAME.Text.Trim
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
            Dim TEMPMSG As Integer = MsgBox("This Will Delete File, Wish To Proceed?", MsgBoxStyle.YesNo)
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

    Sub EDITROW()
        Try

            If (Convert.ToBoolean(gridgrn.CurrentRow.Cells(GDONE.Index).Value) = True Or Val(gridgrn.CurrentRow.Cells(GOUTPCS.Index).Value) > 0 Or Val(gridgrn.CurrentRow.Cells(GOUTMTRS.Index).Value) > 0) And ClientName <> "VALIANT" And UserName <> "Admin" Then
                MessageBox.Show("Row Locked, You Cannot Edit This Row")
                Exit Sub
            End If

            If gridgrn.CurrentRow.Index >= 0 And gridgrn.Item(gsrno.Index, gridgrn.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                txtsrno.Text = gridgrn.Item(gsrno.Index, gridgrn.CurrentRow.Index).Value.ToString
                CMBPIECETYPE.Text = gridgrn.Item(GPIECETYPE.Index, gridgrn.CurrentRow.Index).Value.ToString
                cmbitemname.Text = gridgrn.Item(gitemname.Index, gridgrn.CurrentRow.Index).Value.ToString
                txtgridremarks.Text = gridgrn.Item(gdesc.Index, gridgrn.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = gridgrn.Item(GQUALITY.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTBALENO.Text = gridgrn.Item(GBALENO.Index, gridgrn.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = gridgrn.Item(GDESIGN.Index, gridgrn.CurrentRow.Index).Value.ToString
                cmbcolor.Text = gridgrn.Item(gcolor.Index, gridgrn.CurrentRow.Index).Value.ToString
                txtqty.Text = gridgrn.Item(gQty.Index, gridgrn.CurrentRow.Index).Value.ToString
                cmbqtyunit.Text = gridgrn.Item(gqtyunit.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTCUT.Text = gridgrn.Item(gcut.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTMTRS.Text = gridgrn.Item(GMTRS.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTWT.Text = gridgrn.Item(GWT.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTPURRATE.Text = gridgrn.Item(GPURRATE.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTSALERATE.Text = gridgrn.Item(GSALERATE.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTWHOLESALERATE.Text = gridgrn.Item(GWHOLESALERATE.Index, gridgrn.CurrentRow.Index).Value.ToString
                'TXTRATE.Text = gridgrn.Item(GRATE.Index, gridgrn.CurrentRow.Index).Value.ToString
                CMBPER.Text = gridgrn.Item(GPER.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTAMOUNT.Text = gridgrn.Item(GAMOUNT.Index, gridgrn.CurrentRow.Index).Value.ToString
                TXTBARCODE.Text = gridgrn.Item(GBARCODE.Index, gridgrn.CurrentRow.Index).Value.ToString
                TEMPROW = gridgrn.CurrentRow.Index
                CMBPIECETYPE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridgrn_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridgrn.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor

            gridgrn.RowCount = 0
LINE1:
            temptypename = cmbtype.Text.Trim
            tempgrnno = Val(txtgrnno.Text) - 1
            If tempgrnno > 0 Then
                EDIT = True
                GRN_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If gridgrn.RowCount = 0 And tempgrnno > 1 Then
                txtgrnno.Text = tempgrnno
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmbtype_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtype.Enter
        Try
            getmaxno()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtype_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtype.Validating
        Try
            If cmbtype.Text.Trim.Length > 0 And EDIT = False Then
                getmaxno()
                cmbtype.Enabled = False
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            gridgrn.RowCount = 0
LINE1:
            tempgrnno = Val(txtgrnno.Text) + 1
            temptypename = cmbtype.Text.Trim
            getmaxno()
            Dim MAXNO As Integer = txtgrnno.Text.Trim
            CLEAR()
            If Val(txtgrnno.Text) - 1 >= tempgrnno Then
                EDIT = True
                GRN_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If gridgrn.RowCount = 0 And tempgrnno < MAXNO Then
                txtgrnno.Text = tempgrnno
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtTOTALBALES_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTOTALBALES.KeyPress, txtqty.KeyPress
        numdot(e, sender, Me)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub podate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles podate.Validating
        If Not datecheck(podate.Value) Then
            MsgBox("Date Not In Current Accounting Year")
            e.Cancel = True
        End If
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
                    MsgBox("Unable To Delete, Checking Done / Item Used", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                'If ClientName = "AVIS" And CMBTONAME.Text.Trim <> "" Then
                '    MsgBox("Unable To Delete, Lot Issued To Dyeing", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If

                TEMPMSG = MsgBox("Delete GRN?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(txtgrnno.Text.Trim)
                    alParaval.Add(cmbtype.Text.Trim)
                    alParaval.Add(Userid)
                    alParaval.Add(CmpId)
                    alParaval.Add(Locationid)
                    alParaval.Add(YearId)

                    Dim Clsgrn As New ClsGrn()
                    Clsgrn.alParaval = alParaval
                    IntResult = Clsgrn.Delete()
                    MsgBox("GRN Deleted")
                    CLEAR()
                    EDIT = False
                End If
            Else
                MsgBox("Delete Is only In Edit Mode")
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

    Private Sub gridgrn_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles gridgrn.CellValidating
        Try
            If ClientName <> "MSANCHITKUMAR" And ClientName <> "AVIS" And ClientName <> "SNCM" Then Exit Sub

            Dim colNum As Integer = gridgrn.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GMTRS.Index, gcut.Index, gQty.Index, GWT.Index, GPURRATE.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If gridgrn.CurrentCell.Value = Nothing Then gridgrn.CurrentCell.Value = "0.00"
                        gridgrn.CurrentCell.Value = Convert.ToDecimal(gridgrn.Item(colNum, e.RowIndex).Value)
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

    Private Sub gridgrn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridgrn.KeyDown

        Try
            If e.KeyCode = Keys.Delete And gridgrn.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row Is In Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                If Convert.ToBoolean(gridgrn.CurrentRow.Cells(GDONE.Index).Value) = True Or Val(gridgrn.CurrentRow.Cells(GOUTPCS.Index).Value) > 0 Or Val(gridgrn.CurrentRow.Cells(GOUTMTRS.Index).Value) > 0 Then
                    MessageBox.Show("Row Locked, You Cannot Delete This Row")
                    Exit Sub
                End If


                'end of block
                gridgrn.Rows.RemoveAt(gridgrn.CurrentRow.Index)
                getsrno(gridgrn)
                TOTAL()
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Keys.F12 And gridgrn.RowCount > 0 Then
                If gridgrn.CurrentRow.Cells(gitemname.Index).Value <> "" Then
                    gridgrn.Rows.Add(CloneWithValues(gridgrn.CurrentRow))
                    getsrno(gridgrn)
                    TOTAL()
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            If index = GBARCODE.Index Then
                If EDIT = True Then
                    'GET LAST BARCODE SRNO
                    Dim LSRNO As Integer = 0
                    Dim RSRNO As Integer = 0
                    Dim SNO As Integer = 0
                    LSRNO = InStr(gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                    RSRNO = InStr(LSRNO + 1, gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                    SNO = gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                    CloneWithValues.Cells(index).Value = "G-" & Val(txtgrnno.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                Else
                    CloneWithValues.Cells(index).Value = "G-" & Val(txtgrnno.Text.Trim) & "/" & gridgrn.RowCount + 1 & "/" & YearId
                End If
            Else
                CloneWithValues.Cells(index).Value = row.Cells(index).Value
            End If
        Next
    End Function

    Sub ADDPOUT(ByRef POOUT As TextBox)
        Dim alParaval As New ArrayList
        alParaval.Add("JTOJ")
        alParaval.Add("JTOJ-" & TXTPOUTNO.Text)
        alParaval.Add("")

        alParaval.Add(Format(Convert.ToDateTime(GRNDATE.Text).Date, "MM/dd/yyyy"))
        alParaval.Add(cmbname.Text.Trim)
        alParaval.Add(CMBTONAME.Text.Trim)
        alParaval.Add("Bleach")
        alParaval.Add(cmbtrans.Text.Trim)
        alParaval.Add(txtlrno.Text.Trim)
        alParaval.Add(lrdate.Value)
        alParaval.Add(txttransref.Text.Trim)
        alParaval.Add(txttransremarks.Text.Trim)

        'alParaval.Add(Val(TXTBALEWT.Text.Trim))

        alParaval.Add(Val(lbltotalqty.Text))
        alParaval.Add(Val(LBLTOTALMTRS.Text))

        alParaval.Add(0)
        alParaval.Add(0)


        alParaval.Add(cmbprocess.Text.Trim)
        alParaval.Add("JOBBER")
        alParaval.Add("JOBBER")
        alParaval.Add(0)
        alParaval.Add(0)
        alParaval.Add("")
        alParaval.Add("")

        alParaval.Add(txtremarks.Text.Trim)
        alParaval.Add(1)
        alParaval.Add(0)
        alParaval.Add(CmpId)
        alParaval.Add(Locationid)
        alParaval.Add(Userid)
        alParaval.Add(YearId)
        alParaval.Add(0)


        Dim gridsrno As String = ""
        Dim LOTNO As String = ""
        Dim PIECETYPE As String = ""
        Dim BALENO As String = ""
        Dim ITEMNAME As String = ""
        Dim QUALITY As String = ""
        Dim PROCESS As String = ""
        Dim DYEINGNO As String = ""
        Dim DESIGNNO As String = ""
        Dim COLOR As String = ""
        Dim RECDATE As String = ""
        Dim CUT As String = ""
        Dim APPRPCS As String = ""
        Dim APPRMTRS As String = ""
        Dim OUTPCS As String = ""
        Dim OUTMTRS As String = ""
        Dim FROMNO As String = ""
        Dim FROMSRNO As String = ""
        Dim FROMTYPE As String = ""
        Dim GRNNO As String = ""
        Dim GRNSRNO As String = ""
        Dim GRNTYPE As String = ""



        For Each row As Windows.Forms.DataGridViewRow In gridgrn.Rows
            If row.Cells(0).Value <> Nothing Then
                If gridsrno = "" Then
                    gridsrno = row.Cells(gsrno.Index).Value.ToString
                    LOTNO = TXTLOTNO.Text
                    PIECETYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                    ITEMNAME = row.Cells(gitemname.Index).Value.ToString
                    QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                    PROCESS = cmbprocess.Text

                    OUTPCS = 0
                    OUTMTRS = 0
                    FROMNO = txtgrnno.Text
                    FROMSRNO = row.Cells(gsrno.Index).Value.ToString
                    FROMTYPE = "GRN"
                    GRNNO = txtgrnno.Text
                    GRNSRNO = row.Cells(gsrno.Index).Value.ToString
                    GRNTYPE = cmbtype.Text

                Else

                    gridsrno = gridsrno & "," & row.Cells(gsrno.Index).Value
                    QUALITY = QUALITY & "," & row.Cells(GQUALITY.Index).Value.ToString
                    PIECETYPE = PIECETYPE & "," & row.Cells(GPIECETYPE.Index).Value

                    ITEMNAME = ITEMNAME & "," & row.Cells(gitemname.Index).Value
                    LOTNO = LOTNO & "," & TXTLOTNO.Text
                    PROCESS = PROCESS & "," & cmbprocess.Text

                    OUTPCS = OUTPCS & "," & "0"
                    OUTMTRS = OUTMTRS & "," & "0"
                    FROMNO = FROMNO & "," & txtgrnno.Text
                    FROMSRNO = FROMSRNO & "," & row.Cells(gsrno.Index).Value
                    FROMTYPE = FROMTYPE & "," & "GRN"
                    GRNNO = GRNNO & "," & txtgrnno.Text
                    GRNSRNO = GRNSRNO & "," & row.Cells(gsrno.Index).Value
                    GRNTYPE = GRNTYPE & "," & cmbtype.Text

                End If
            End If
        Next

        alParaval.Add(gridsrno)
        alParaval.Add(LOTNO)
        alParaval.Add(PIECETYPE)
        alParaval.Add(BALENO)
        alParaval.Add(ITEMNAME)
        alParaval.Add(QUALITY)
        alParaval.Add(PROCESS)
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")

        alParaval.Add(APPRPCS)
        alParaval.Add(APPRMTRS)
        alParaval.Add(OUTPCS)
        alParaval.Add(OUTMTRS)
        alParaval.Add(FROMNO)
        alParaval.Add(FROMSRNO)
        alParaval.Add(FROMTYPE)
        alParaval.Add(GRNNO)
        alParaval.Add(GRNSRNO)
        alParaval.Add(GRNTYPE)

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
                    griduploadsrno = griduploadsrno & "," & row.Cells(0).Value.ToString
                    uploadremarks = uploadremarks & "," & row.Cells(1).Value.ToString
                    name = name & "," & row.Cells(2).Value.ToString
                    imgpath = imgpath & "," & row.Cells(3).Value.ToString
                    NEWIMGPATH = NEWIMGPATH & "," & row.Cells(GNEWIMGPATH.Index).Value.ToString

                End If
            End If
        Next

        alParaval.Add(griduploadsrno)
        alParaval.Add(uploadremarks)
        alParaval.Add(name)
        alParaval.Add(imgpath)
        alParaval.Add(NEWIMGPATH)
        alParaval.Add(FILENAME)


        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")

        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")

        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")
        alParaval.Add("")

        Dim objCLSPROCESSOUT As New ClsProcessOut()
        objCLSPROCESSOUT.alParaval = alParaval
        If EDIT = False Then
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim DTTABLE As DataTable = objCLSPROCESSOUT.save()

            POOUT.Text = DTTABLE.Rows(0).Item(0)

        Else
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            alParaval.Add(TXTPOUTNO.Text)

            IntResult = objCLSPROCESSOUT.Update()
        End If
    End Sub

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcolor.Validating
        Try
            If cmbcolor.Text.Trim <> "" Then COLORVALIDATE(cmbcolor, e, Me, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBBROKER.Enter
        Try
            If CMBBROKER.Text.Trim = "" Then FILLNAME(CMBBROKER, EDIT, " And GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and LEDGERS.ACC_TYPE = 'AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBBROKER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' and LEDGERS.ACC_TYPE = 'AGENT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBROKER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBBROKER.Validating
        Try
            If CMBBROKER.Text.Trim <> "" Then NAMEVALIDATE(CMBBROKER, CMBCODE, e, Me, TXTWEAVERADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and LEDGERS.ACC_TYPE = 'AGENT'", "Sundry Creditors")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTWT.KeyPress, TXTDMTRS.KeyPress, TXTCUT.KeyPress, TXTAMOUNT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then
                If ClientName = "AMAN" Then
                    FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' and ACC_TYPE = 'ACCOUNTS'")
                ElseIf ClientName = "TINUMINU" Or ClientName = "RADHA" Or ClientName = "SIMPLEX" Or ClientName = "VINTAGEINDIA" Then
                    FILLNAME(cmbname, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') and ACC_TYPE = 'ACCOUNTS'")
                Else
                    FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then
                If ClientName = "AMAN" Then
                    NAMEVALIDATE(cmbname, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'", "Sundry Debtors", "ACCOUNTS", cmbtrans.Text, CMBBROKER.Text)
                ElseIf ClientName = "TINUMINU" Or ClientName = "RADHA" Or ClientName = "SIMPLEX" Or ClientName = "VINTAGEINDIA" Then
                    NAMEVALIDATE(cmbname, CMBCODE, e, Me, txtadd, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')", "Sundry Creditors", "ACCOUNTS", cmbtrans.Text, CMBBROKER.Text)
                Else
                    NAMEVALIDATE(cmbname, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS", cmbtrans.Text, CMBBROKER.Text)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTONAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTONAME.Enter
        Try
            If CMBTONAME.Text.Trim = "" Then FILLNAME(CMBTONAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTONAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTONAME.Validating
        Try
            If CMBTONAME.Text.Trim <> "" Then NAMEVALIDATE(CMBTONAME, CMBTOCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbqtyunit.Validated
        Try
            If ClientName = "YAMUNESH" Then
                If cmbitemname.Text.Trim <> "" And Val(txtqty.Text.Trim) > 0 And cmbqtyunit.Text.Trim <> "" And TXTBALENO.Text.Trim.Length <> 0 Then
                    If FRMSTRING = "GRN FANCY" Then
                        If GRIDDOUBLECLICK = False Then
                            If EDIT = True Then
                                'GET LAST BARCODE SRNO
                                Dim LSRNO As Integer = 0
                                Dim RSRNO As Integer = 0
                                Dim SNO As Integer = 0
                                LSRNO = InStr(gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                                RSRNO = InStr(LSRNO + 1, gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                                SNO = gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                                TXTBARCODE.Text = "G-" & Val(txtgrnno.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                            Else
                                TXTBARCODE.Text = "G-" & Val(txtgrnno.Text.Trim) & "/" & gridgrn.RowCount + 1 & "/" & YearId
                            End If
                        End If
                    End If
                    FILLGRID()

                ElseIf CMBPIECETYPE.Text.Trim = "" Then
                    MsgBox("Enter Piece Type", MsgBoxStyle.Critical)
                    CMBPIECETYPE.Focus()
                    Exit Sub

                ElseIf cmbitemname.Text.Trim = "" Then
                    MsgBox("Enter Item Name", MsgBoxStyle.Critical)
                    cmbitemname.Focus()
                    Exit Sub
                ElseIf cmbqtyunit.Text.Trim = "" Then
                    MsgBox("Enter Quantity Unit", MsgBoxStyle.Critical)
                    cmbqtyunit.Focus()
                    Exit Sub
                ElseIf Val(txtqty.Text.Trim) <= 0 Then
                    MsgBox("Enter Quantity", MsgBoxStyle.Critical)
                    txtqty.Focus()
                    Exit Sub
                ElseIf TXTBALENO.Text.Trim = "" Then
                    MsgBox("Enter Desc", MsgBoxStyle.Critical)
                    TXTBALENO.Focus()
                    Exit Sub
                End If


            ElseIf ClientName = "AVIS" And cmbqtyunit.Text.Trim <> "" Then
                If UCase(cmbqtyunit.Text.Trim) = "FENT" Then
                    CMBPIECETYPE.Text = "FENT"
                ElseIf UCase(cmbqtyunit.Text.Trim) = "2ND" Then
                    CMBPIECETYPE.Text = "SECOND"
                ElseIf UCase(cmbqtyunit.Text.Trim) = "2ND TP" Then
                    CMBPIECETYPE.Text = "SECOND"
                ElseIf UCase(cmbqtyunit.Text.Trim) = "SHORTAGE" Then
                    CMBPIECETYPE.Text = "SHORTAGE"
                ElseIf UCase(cmbqtyunit.Text.Trim) = "TP" Then
                    CMBPIECETYPE.Text = "TWOPART"
                ElseIf UCase(cmbqtyunit.Text.Trim) = "PCS" Then
                    CMBPIECETYPE.Text = "PIECES"
                Else
                    CMBPIECETYPE.Text = "FRESH"
                End If

            ElseIf ClientName = "MOMAI" Then
                TXTAMOUNT_Validated(sender, e)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
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

    Private Sub CMBSENDER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSENDER.Enter
        Try
            If CMBSENDER.Text.Trim = "" Then FILLNAME(CMBSENDER, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSENDER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSENDER.Validating
        Try
            If CMBSENDER.Text.Trim <> "" Then NAMEVALIDATE(CMBSENDER, CMBCODE, e, Me, TXTWEAVERADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitemname.Enter
        Try
            If FRMSTRING = "Inwards" Then
                If cmbitemname.Text.Trim = "" Then fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'ITEM'")
            Else
                If cmbitemname.Text.Trim = "" Then fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            End If
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

    Private Sub txtPartyMtrs_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPartyMtrs.Validated
        If Val(TXTMTRS.Text) <> 0 And Val(txtPartyMtrs.Text) <> 0 Then
            txtCheckPcs.Text = Format(Val(TXTMTRS.Text) - Val(txtPartyMtrs.Text), "0.00")
        End If
    End Sub

    Private Sub TXTMTRS_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTMTRS.Validated
        Try
            CALC()
            If ClientName = "MANIBHADRA" Or ClientName = "AVIS" Or ClientName = "SNCM" Or ClientName = "DILIP" Or ClientName = "DILIPNEW" Or ClientName = "VALIANT" Or ClientName = "AMAN" Or ClientName = "LEEFABRICO" Or ClientName = "MANISH" Or ClientName = "RADHA" Or ClientName = "AFW" Then TXTAMOUNT_Validated(sender, e)

            'GET WT AUTO CALCULATED, IF USER HAS WRITTEN TOTALWT IN ITEMMASTER
            If ClientName = "VINTAGEINDIA" And Val(TXTMTRS.Text.Trim) > 0 And cmbitemname.Text.Trim <> "" And Val(TXTWT.Text.Trim) = 0 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(ITEM_TOTALWT,0) AS TOTALWT", "", " ITEMMASTER ", " AND ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TXTWT.Text = Format((Val(DT.Rows(0).Item("TOTALWT")) / 100) * Val(TXTMTRS.Text.Trim), "0.00")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTMTRS.Validating
        If Val(TXTMTRS.Text) > 0 Then txtPartyMtrs.Text = Val(TXTMTRS.Text)
    End Sub

    Private Sub CMBCODE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCODE.Enter
        Try
            If CMBCODE.Text.Trim = "" Then fillACCCODE(CMBCODE, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCODE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCODE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
                If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If ClientName = "AMAN" Then
                    OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                ElseIf ClientName = "TINUMINU" Or ClientName = "RADHA" Or ClientName = "SIMPLEX" Or ClientName = "VINTAGEINDIA" Then
                    OBJLEDGER.STRSEARCH = " and (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                Else
                    OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                End If
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
                If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCODE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTOCODE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
                If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTONAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTONAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
                If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCODE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCODE.Validating
        Try
            If CMBCODE.Text.Trim <> "" Then ACCCODEVALIDATE(CMBCODE, cmbname, e, Me, TXTTRANSADD, "", "SUNDRY CREDITORS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCODE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTOCODE.Validating
        Try
            If CMBTOCODE.Text.Trim <> "" Then ACCCODEVALIDATE(CMBTOCODE, CMBTONAME, e, Me, TXTTRANSADD, "", "SUNDRY CREDITORS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLOTNO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTLOTNO.Validating
        Try
            If CMBTONAME.Text.Trim <> "" And TXTLOTNO.Text.Trim <> "" Then
                'CHECK IN LOT_VIEW FOR LOTNO DUPLLICATION
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" GRNNO ", "", " LOT_VIEW ", " AND LOTNO = '" & TXTLOTNO.Text.Trim & "' AND JOBBERNAME = '" & CMBTONAME.Text.Trim & "' AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If (EDIT = False) Or (EDIT = True And Val(DT.Rows(0).Item("GRNNO")) <> Val(txtgrnno.Text.Trim)) Then
                        MsgBox("Lot No Already Exists in Inward No " & DT.Rows(0).Item("GRNNO"), MsgBoxStyle.Critical)
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            If Val(txtqty.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 Then TXTMTRS.Text = Format(Val(txtqty.Text.Trim) * Val(TXTCUT.Text.Trim), "0.00")
            If CMBPER.Text = "Mtrs" Then TXTAMOUNT.Text = Format(Val(TXTPURRATE.Text.Trim) * Val(TXTMTRS.Text.Trim), "0.00") Else TXTAMOUNT.Text = Format(Val(TXTPURRATE.Text.Trim) * Val(txtqty.Text.Trim), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTCUT.Validated
        CALC()
    End Sub

    Private Sub txtqty_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtqty.Validated
        CALC()
        If ClientName = "AFW" And Val(txtqty.Text.Trim) > 1 Then TXTCUT.TabStop = True Else TXTCUT.TabStop = False
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
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtrans.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans.Text = OBJLEDGER.TEMPNAME
                'If OBJLEDGER.TEMPAGENT <> "" Then cmbtrans.Text = OBJLEDGER.TEMPAGENT
            End If
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

    Sub PRINTREPORT(ByVal GRNNO As Integer)
        Try
            If MsgBox("Wish to Print GRN...?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJGDN As New GRNDesign
                OBJGDN.MdiParent = MDIMain
                If ClientName = "MAHAVIRPOLYCOT" Or ClientName = "VINIT" Then
                    If cmbtype.Text.Trim = "FANCY MATERIAL" Then OBJGDN.FRMSTRING = "FINISHGRN" Else OBJGDN.FRMSTRING = "GRN"
                Else
                    OBJGDN.FRMSTRING = "GRN"
                End If
                OBJGDN.WHERECLAUSE = "{GRN.GRN_no}=" & Val(GRNNO) & " AND {GRN.GRN_TYPE} = '" & cmbtype.Text.Trim & "'  and {GRN.GRN_yearid}=" & YearId
                OBJGDN.Show()
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then
                PRINTREPORT(tempgrnno)
                PRINTEWB()
                If cmbtype.Text.Trim = "FANCY MATERIAL" Then PRINTBARCODE()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRN_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "SONU" Or ClientName = "ANMOL" Or ClientName = "LEEFABRICO" Or ClientName = "SNCM" Then
                cmbtrans.TabStop = True
                txtlrno.TabStop = True
                lrdate.TabStop = True
            End If

            If ClientName = "BIGAPPLE" And cmbtype.Text.Trim = "FANCY MATERIAL" Then TOOLUPLOADEXCEL.Visible = True

            If ClientName = "PARAS" Or ClientName = "VINTAGEINDIA" Then LBLCATEGORY.Visible = True

            If ClientName = "LEEFABRICO" Then
                cmdselectPO.TabStop = False
                TXTLOTNO.TabStop = False
                txtpono.BackColor = Color.White
                txtpono.TabStop = True
                txtpono.ReadOnly = False
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
                cmbqtyunit.TabStop = False
                TXTCUT.TabStop = False
            End If

            If ClientName = "VALIANT" And cmbtype.Text = "Job Work" Then
                cmdselectPO.TabStop = False
                CMBQUALITY.TabStop = False
                cmbqtyunit.TabStop = False
                TXTCUT.TabStop = False
                txtqty.TabStop = False
            End If

            If HIDEYARN = False Then
                CHKDIRECTFROMKNITTING.Visible = True
                LBLGREYKNTTINGNO.Visible = True
                TXTGREYKNITTINGNO.Visible = True
            End If

            If ClientName = "AFW" Then
                cmdselectPO.TabStop = False
                cmbGodown.TabStop = False
                CMBBROKER.TabStop = False
                CMBQUALITY.TabStop = False
                CMBDESIGN.TabStop = False

            End If

            If ClientName = "BALAJI" Then
                cmdselectPO.TabStop = False
                TXTLOTNO.TabStop = False
                CMBQUALITY.TabStop = False
                cmbqtyunit.TabStop = False
            End If

            If ClientName = "MANISH" Then
                cmdselectPO.TabStop = False
                CMBBROKER.TabStop = False
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
                GBALENO.HeaderText = "P.Challan No"
            End If

            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Or ClientName = "YAMUNESH" Then
                CHKBARCODE.Visible = True
                GBALENO.HeaderText = "Desc"

                gcut.Visible = False
                TXTCUT.Visible = False
                If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    GWT.Visible = False
                    TXTWT.Visible = False
                End If
                LBLTOTALWT.Visible = False
                GPURRATE.Visible = True
                TXTPURRATE.Visible = True
                GSALERATE.Visible = True
                TXTSALERATE.Visible = True
                GWHOLESALERATE.Visible = True
                TXTWHOLESALERATE.Visible = True

                TXTMTRS.Left = cmbqtyunit.Left + cmbqtyunit.Width
                CMBRACK.Left = TXTMTRS.Left + TXTMTRS.Width
                CMBSHELF.Left = CMBRACK.Left + CMBRACK.Width
                TXTPURRATE.Left = CMBSHELF.Left + CMBSHELF.Width
                TXTSALERATE.Left = TXTPURRATE.Left + TXTPURRATE.Width
                TXTWHOLESALERATE.Left = TXTSALERATE.Left + TXTSALERATE.Width

            ElseIf ClientName = "MSANCHITKUMAR" Or ClientName = "DAKSH" Or ClientName = "SHALIBHADRA" Then
                LBLDYEINGTYPE.Visible = True
                CMBDYEINGTYPE.Visible = True
                If ClientName = "MSANCHITKUMAR" Then
                    LBLPARTYBILLNO.Visible = True
                    LBLPARTYBILLDATE.Visible = True
                    PARTYBILLDATE.Visible = True
                    TXTPARTYBILLNO.Visible = True
                End If
            End If

            If ClientName = "BRILLANTO" Then
                CMBQUALITY.TabStop = False
                CMBDESIGN.TabStop = False
                cmbcolor.TabStop = False
                txtqty.TabStop = False
                cmbqtyunit.TabStop = False
                TXTCUT.TabStop = False
                CMBRACK.TabStop = False
                CMBSHELF.TabStop = False
                GBALENO.HeaderText = "Piece No"
            End If

            If ClientName <> "CC" And ClientName <> "C3" Then
                GPURRATE.Visible = False
                TXTPURRATE.Visible = False
                GSALERATE.Visible = False
                TXTSALERATE.Visible = False
                GWHOLESALERATE.Visible = False
                TXTWHOLESALERATE.Visible = False
                GPER.Visible = False
                CMBPER.Visible = False
                GAMOUNT.Visible = False
                TXTAMOUNT.Visible = False
            End If

            If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "NTC" Or ClientName = "KOTHARI" Or ClientName = "SHREENAKODA" Then GBALENO.HeaderText = "Description"
            If ClientName = "MAHAVIRPOLYCOT" Then
                GBALENO.HeaderText = "P.Design No"
                TXTBALEWT.Visible = True
                LBLBALEWT.Visible = True
            End If


            If ClientName = "INDRANI" Or ClientName = "MNIKHIL" Or ClientName = "HRITI" Then
                If ClientName = "INDRANI" Then GBALENO.HeaderText = "SO No" Else TXTBALENO.TabStop = False
                CMBQUALITY.TabStop = False
                cmbqtyunit.TabStop = False
                TXTCUT.TabStop = False
                CMBRACK.TabStop = False
                CMBSHELF.TabStop = False
            End If


            If ClientName = "YASHVI" Or ClientName = "MNARESH" Then
                TXTBALEWT.Visible = True
                LBLBALEWT.Visible = True
                TXTPURRATE.Visible = True
                GPURRATE.Visible = True
                GPURRATE.ReadOnly = False

                CMBPER.Visible = True
                CMBPER.Left = TXTPURRATE.Left + TXTPURRATE.Width
                GPER.Visible = True

                TXTAMOUNT.Visible = True
                TXTAMOUNT.Left = CMBPER.Left + CMBPER.Width
                GAMOUNT.Visible = True
            End If

            If ClientName = "YASHVI" Or ClientName = "KRISHNA" Or ClientName = "RMANILAL" Or ClientName = "VINTAGEINDIA" Or ClientName = "SNCM" Then
                If ClientName = "YASHVI" Or ClientName = "KRISHNA" Or ClientName = "RMANILAL" Or ClientName = "SNCM" Then
                    LBLCMPNAME.Visible = True
                    CMBCMPNAME.Visible = True
                End If
                LBLCHNO.Visible = True
                TXTCHNO.Visible = True
            End If

            If ClientName = "AVIS" Or ClientName = "SNCM" Or ClientName = "REALCORPORATION" Then
                LBLDYEINGTYPE.Visible = False
                CMBDYEINGTYPE.Visible = False
                CMBQUALITY.TabStop = False
                If ClientName <> "REALCORPORATION" Then TXTBALENO.TabStop = False
                GPURRATE.ReadOnly = False
                    GMTRS.ReadOnly = False
                    If ClientName = "AVIS" Then
                        RECDATE.TabStop = False
                        CMBBROKER.TabStop = False
                        txtchallan.TabStop = False
                    End If

                    If cmbtype.Text = "Job Work" Then
                        cmbname.Enabled = False
                        txtsrno.Visible = False
                        CMBPIECETYPE.Visible = False
                        cmbitemname.Visible = False
                        CMBQUALITY.Visible = False
                        TXTBALENO.Visible = False
                        CMBDESIGN.Visible = False
                        cmbcolor.Visible = False
                        txtqty.Visible = False
                        cmbqtyunit.Visible = False
                        TXTCUT.Visible = False
                        TXTMTRS.Visible = False
                        CMBRACK.Visible = False
                        CMBSHELF.Visible = False
                        TXTWT.Visible = False
                        cmdselectPO.Text = "Select Grey"
                        gQty.ReadOnly = False
                        CMBPER.Visible = False
                        TXTAMOUNT.Visible = False
                        If ClientName = "SNCM" Then
                            Me.Text = "Grey Iss To Process"
                            LBLGRN.Text = "Grey Iss To Process"
                            LBLREFLOTNO.Visible = True
                            TXTREFLOTNO.Visible = True
                        End If
                    Else
                        TXTPURRATE.Visible = True
                        GPURRATE.Visible = True
                        GPURRATE.ReadOnly = False

                        CMBPER.Visible = True
                        CMBPER.Left = TXTPURRATE.Left + TXTPURRATE.Width
                        GPER.Visible = True

                        TXTAMOUNT.Visible = True
                        TXTAMOUNT.Left = CMBPER.Left + CMBPER.Width
                        GAMOUNT.Visible = True
                    End If



                    LBLBALES.Visible = True
                    TXTTOTALBALES.Visible = True
                    TXTTOTALBALES.TabStop = True

                    cmbqtyunit.Text = "Mtrs"
                    CMBPIECETYPE.TabStop = False

                End If


                If ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "MNARESH" Then
                GPURRATE.Visible = True
                GPURRATE.ReadOnly = False
            End If


            If ClientName = "RMANILAL" Then TXTCUT.TabStop = False

            If ClientName = "SBA" Then
                CMBRACK.TabStop = False
                CMBSHELF.TabStop = False
            End If

            If ClientName = "MOMAI" Then
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
            End If

            If ClientName = "DILIP" Or ClientName = "DILIPNEW" Then
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
                CMBDESIGN.TabStop = False
                cmbcolor.TabStop = False
                TXTCUT.TabStop = False
            End If

            If ClientName = "KRISHNA" Or ClientName = "AMAN" Or ClientName = "MBB" Then CHKLOTREADY.Visible = True
            If ClientName = "MBB" Then CHKLOTREADY.Text = "Barcode Printed"

            If ClientName = "GELATO" Or ClientName = "KREEVE" Or ClientName = "MANSI" Then
                If ClientName = "GELATO" Then
                    gcolor.HeaderText = "Size"
                    PANELSIZE.Visible = True
                    CMBQUALITY.TabStop = False
                Else
                    GQUALITY.HeaderText = "Size"
                End If
                TXTBALENO.TabStop = False
                TXTCUT.TabStop = False
                CMBRACK.TabStop = False
                CMBSHELF.TabStop = False
            End If

            If ClientName = "RADHA" Then
                cmdselectPO.TabStop = False
                CMBBROKER.TabStop = False
                CHKLOTREADY.Text = "Prog Recd"
                CHKLOTREADY.Visible = True
                LBLLOTDATE.Visible = True
                RECDATE.Visible = True
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
                CMBDESIGN.TabStop = False
                cmbcolor.TabStop = False
                TXTCUT.TabStop = False
            End If


            If ClientName = "AMAN" Then
                cmdselectPO.TabStop = False
                TXTLOTNO.TabStop = False
                CMBPIECETYPE.TabStop = False
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
                CMBDESIGN.TabStop = False
                cmbcolor.TabStop = False
                TXTCUT.TabStop = False
                CMBRACK.TabStop = False
                CMBSHELF.TabStop = False
                txtchallan.BackColor = Color.LemonChiffon
                CHKLOTREADY.Text = "Lock Challan"
            End If

            If ClientName = "SOFTAS" Or ClientName = "SHREENAKODA" Then
                cmdselectPO.TabStop = False
                cmbGodown.TabStop = False
                CMBBROKER.TabStop = False
                CMBPIECETYPE.TabStop = False
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
                TXTCUT.TabStop = False
                CMBSHELF.TabStop = False
                'TXTRATE.TabStop = False
                CMBPER.TabStop = False
            End If

            If ClientName = "VSTRADERS" Then cmbtype.Text = "PITCH DYED"


            If ClientName = "VINTAGEINDIA" Then
                TXTLOTNO.Text = txtgrnno.Text.Trim
                GBALENO.HeaderText = "Pcs No"
                cmdselectPO.TabStop = False
                TXTLOTNO.TabStop = False
                'txtpono.BackColor = Color.Whites
                txtpono.TabStop = False
                txtpono.ReadOnly = False
                CMBQUALITY.TabStop = False
                TXTCUT.TabStop = False
                cmbqtyunit.TabStop = False
                CMBRACK.TabStop = False
                CMBSHELF.TabStop = False
                LBL3.Visible = True
                CMBDESIGN.TabStop = False
                CMBDESIGN.Enabled = False
                cmbcolor.TabStop = False
                cmbcolor.Enabled = False
                txtqty.TabStop = False
                txtqty.ReadOnly = True
                TXTBALENO.TabStop = False
                TXTBALENO.ReadOnly = True
                cmbGodown.TabStop = False
                CMBBROKER.TabStop = False

                If UserName <> "Admin" Then TXTLOTNO.ReadOnly = True

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPURRATE_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPURRATE.KeyPress
        Try
            numdot(e, TXTPURRATE, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSALERATE_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTSALERATE.KeyPress
        Try
            numdot(e, TXTSALERATE, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWHOLESALERATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTWHOLESALERATE.Validating
        Try
            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                If CMBPIECETYPE.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And Val(txtqty.Text.Trim) > 0 And cmbqtyunit.Text.Trim <> "" And (Val(TXTPURRATE.Text.Trim) > 0 Or Val(TXTSALERATE.Text.Trim) > 0) Then
                    FILLGRID()
                ElseIf CMBPIECETYPE.Text.Trim = "" Then
                    MsgBox("Enter Proper Data", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWHOLESALERATE_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWHOLESALERATE.KeyPress
        Try
            numdot(e, TXTWHOLESALERATE, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        cmdok_Click(sender, e)
    End Sub

    Private Sub CMBDESIGN_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Validated
        Try
            If CMBDESIGN.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_PURRATE,0) AS PURRATE, ISNULL(DESIGN_SALERATE,0) AS SALERATE, ISNULL(DESIGN_WRATE,0) AS WRATE, ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEM_ID ", " AND DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' AND DESIGN_YEARID =  " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTPURRATE.Text = Val(DT.Rows(0).Item("PURRATE"))
                    TXTSALERATE.Text = Val(DT.Rows(0).Item("SALERATE"))
                    TXTWHOLESALERATE.Text = Val(DT.Rows(0).Item("WRATE"))
                    If (ClientName = "AVIS" Or ClientName = "KRISHNA" Or ClientName = "NTC") Then cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")
                End If
            End If
        Catch ex As Exception
            Throw ex
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

    Private Sub TXTBALENO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTBALENO.KeyPress
        Try
            If ClientName = "INDRANI" Then numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBALEWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTBALEWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTDMTRS_Validated(sender As Object, e As EventArgs) Handles TXTDMTRS.Validated
        Try
            If Val(TXTDMTRS.Text.Trim) > 0 Then
                FILLMTRSGRID()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLMTRSGRID()
        Try
            If GRIDMTRSDOUBLECLICK = False Then
                GRIDMTRS.Rows.Add(Val(TXTDMTRS.Text.Trim))
            ElseIf GRIDMTRSDOUBLECLICK = True Then
                GRIDMTRS.Item(DMTRS.Index, TEMPMTRSROW).Value = Val(TXTDMTRS.Text.Trim)
                GRIDMTRSDOUBLECLICK = False
            End If
            TXTDMTRS.Clear()
            TXTDMTRS.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLOSE_Click(sender As Object, e As EventArgs) Handles CMDCLOSE.Click
        Try
            For Each ROW As DataGridViewRow In GRIDMTRS.Rows
                TXTMTRS.Text = ROW.Cells(DMTRS.Index).Value

                If GRIDDOUBLECLICK = False And EDIT = True Then
                    'GET LAST BARCODE SRNO
                    Dim LSRNO As Integer = 0
                    Dim RSRNO As Integer = 0
                    Dim SNO As Integer = 0
                    LSRNO = InStr(gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                    RSRNO = InStr(LSRNO + 1, gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                    SNO = gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)
                    TXTBARCODE.Text = "G-" & Val(txtgrnno.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                End If

                FILLGRID()
            Next
            TXTBALENO.Text = Val(TXTBALENO.Text) + 1
            GRIDMTRS.RowCount = 0
            TXTDMTRS.Clear()
            GBMTRS.Visible = False
            TXTBALENO.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtgrnno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtgrnno.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub txtgrnno_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtgrnno.Validating
        Try
            If Val(txtgrnno.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(GRN.GRN_NO,0)  AS GRNNO", "", " GRN ", "  AND GRN.GRN_NO=" & txtgrnno.Text.Trim & " AND GRN.GRN_TYPE = '" & cmbtype.Text.Trim & "' AND GRN.GRN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("GRN No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHNO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTCHNO_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTCHNO.Validated
        Try
            If (ClientName = "YASHVI" Or ClientName = "KRISHNA" Or ClientName = "RMANILAL" Or ClientName = "SNCM") And CMBCMPNAME.Text.Trim <> "" And Val(TXTCHNO.Text.Trim) > 0 And EDIT = False And FRMSTRING = "GRN FANCY" Then
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


                'FETCH GREYISSUEPROCESS DATA
                If ClientName = "SNCM" Then
                    Dim OBJCLSGREYISS As New ClsGreyIssueProcess
                    DTTABLE = OBJCLSGREYISS.SELECTGREY(Val(TXTCHNO.Text.Trim), TEMPYEARID)
                    If MsgBox("Fetch data from Entry No " & TXTCHNO.Text.Trim & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                    For Each dr As DataRow In DTTABLE.Rows

                        'CHECKING WHETHER ITEM IS PRESENT IN CURRENT YEAR OR NOT, IF NOT PRESENT THEN ADD NEW ITEM
                        If dr("ITEMNAME") <> "" Then
                            DT = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & dr("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count = 0 Then
                                'ADD NEW ITEMNAME 
                                ALPARAVAL.Clear()


                                ALPARAVAL.Add("Finished Goods")
                                ALPARAVAL.Add("")   'CATEGORY
                                ALPARAVAL.Add(UCase(dr("ITEMNAME")))        'DISPLAYNAME
                                ALPARAVAL.Add(UCase(dr("ITEMNAME"))) 'ITEMNAME

                                ALPARAVAL.Add("")   'DEPARTMENT
                                ALPARAVAL.Add(UCase(dr("ITEMNAME")))        'CODE
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
                                ALPARAVAL.Add(0)    'SHADEsrno

                                Dim objclsItemMaster As New clsItemmaster
                                objclsItemMaster.alParaval = ALPARAVAL
                                Dim IntResult As Integer = objclsItemMaster.SAVE()

                            End If
                        End If


                        If dr("DESIGNNO") <> "" Then
                            DTTABLE = OBJCMN.SEARCH("DESIGN_ID AS DESIGNID", "", "DESIGNMASTER", " AND DESIGN_NO = '" & dr("DESIGNNO") & "' AND DESIGN_YEARID = " & YearId)
                            If DTTABLE.Rows.Count = 0 Then
                                'ADD NEW DESIGN
                                Dim OBJDESIGN As New ClsDesignMaster
                                OBJDESIGN.alParaval.Add(UCase(dr("DESIGNNO")))
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
                                Dim DTTEMP As DataTable = OBJCMN.SEARCH("ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEMMASTER.ITEM_ID", " AND DESIGN_NO = '" & dr("DESIGNNO") & "' AND DESIGN_YEARID = " & TEMPYEARID)
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
                        TXTLOTNO.Text = dr("REFLOTNO")
                        txtchallan.Text = dr("CHALLANNO")
                        CHALLANDATE.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")
                        gridgrn.Rows.Add(dr("GRIDSRNO").ToString, "FRESH", dr("ITEMNAME").ToString, dr("QUALITY"), dr("BALENO"), dr("DESIGNNO"), "", dr("COLOR"), Format(Val(dr("QTY")), "0"), dr("QTYUNIT"), 0, Format(Val(dr("MTRS")), "0.00"), "", "", 0, 0, 0, 0, "Mtrs", 0, "", 0, "", 0, 0, 0, 0, 0, 0, "")
                    Next
                    TOTAL()
                    gridgrn.FirstDisplayedScrollingRowIndex = gridgrn.RowCount - 1

                    Exit Sub
                End If




                'NOW FETCH CHALLAN DATA
                Dim objclsGDN As New ClsGDN()
                DTTABLE = objclsGDN.SELECTGDN(Val(TXTCHNO.Text.Trim), TEMPCMPID, 0, TEMPYEARID)
                If DTTABLE.Rows.Count > 0 Then

                    If MsgBox("Fetch data from Entry No " & TXTCHNO.Text.Trim & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                    For Each dr As DataRow In DTTABLE.Rows

                        'CHECKING WHETHER ITEM IS PRESENT IN CURRENT YEAR OR NOT, IF NOT PRESENT THEN ADD NEW ITEM
                        If dr("ITEMNAME") <> "" Then
                            DT = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & dr("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count = 0 Then
                                'ADD NEW ITEMNAME 
                                ALPARAVAL.Clear()


                                ALPARAVAL.Add("Finished Goods")
                                ALPARAVAL.Add("")   'CATEGORY
                                ALPARAVAL.Add(UCase(dr("ITEMNAME")))        'DISPLAYNAME
                                ALPARAVAL.Add(UCase(dr("ITEMNAME"))) 'ITEMNAME

                                ALPARAVAL.Add("")   'DEPARTMENT
                                ALPARAVAL.Add(UCase(dr("ITEMNAME")))        'CODE
                                ALPARAVAL.Add(dr("UNIT"))
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


                                OBJQUALITY.alParaval.Add(CmpId)
                                OBJQUALITY.alParaval.Add(Locationid)
                                OBJQUALITY.alParaval.Add(Userid)
                                OBJQUALITY.alParaval.Add(YearId)
                                OBJQUALITY.alParaval.Add(0)
                                OBJQUALITY.alParaval.Add("") 'PHOTO
                                Dim INTRESCAT As Integer = OBJQUALITY.save()
                            End If
                        End If

                        cmbtrans.Text = dr("TRANSNAME").ToString
                        gridgrn.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE"), dr("ITEMNAME").ToString, dr("QUALITY"), dr("BALENO"), dr("DESIGN"), dr("PRINTDESC"), dr("COLOR"), Format(Val(dr("PCS")), "0"), dr("UNIT"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("MTRS")), "0.00"), "", "", 0, 0, 0, 0, "Mtrs", 0, dr("BARCODE"), 0, "", 0, 0, 0, 0, 0, 0, "")
                    Next
                    TOTAL()
                    gridgrn.FirstDisplayedScrollingRowIndex = gridgrn.RowCount - 1
                End If

            End If


            'GET CHALLAN DATA 
            If ClientName = "VINTAGEINDIA" And Val(TXTCHNO.Text.Trim) > 0 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" GDN.GDN_NO, GDN.GDN_date AS CHALLANDATE, GODOWNMASTER.GODOWN_name AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTLEDGERS.Acc_name, '') AS AGENT, ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS [DESC], ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO, ISNULL(GDN_DESC.GDN_PCS, 0) AS QTY, ISNULL(GDN_DESC.GDN_CUT, 0) AS CUT, ISNULL(GDN_DESC.GDN_MTRS, 0) AS MTRS, ISNULL(UNITMASTER.unit_abbr, '') AS QTYUNIT, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY", "", "    GDN INNER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN UNITMASTER ON GDN_DESC.GDN_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON GDN.GDN_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON GDN.GDN_YEARID = LEDGERS.Acc_yearid AND GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_YEARID = GODOWNMASTER.GODOWN_yearid AND GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id ", " AND GDN.GDN_NO ='" & TXTCHNO.Text.Trim & "' AND GDN.GDN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    Dim tempmsg As Integer = MessageBox.Show("Do You Want To Fetch Data From Challan?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then
                        For Each dr As DataRow In DT.Rows
                            cmbname.Text = DT.Rows(0).Item("PARTYNAME")
                            CHALLANDATE.Text = DT.Rows(0).Item("CHALLANDATE")
                            CMBBROKER.Text = DT.Rows(0).Item("AGENT")
                            cmbGodown.Text = DT.Rows(0).Item("GODOWN")
                            TXTLOTNO.Text = txtgrnno.Text.Trim + "/" + TXTCHNO.Text.Trim
                            gridgrn.Rows.Add(0, dr("PIECETYPE").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGNNO").ToString, dr("DESC").ToString, dr("COLOR"), Format(dr("qty"), "0.00"), dr("QTYUNIT").ToString, Format(dr("CUT"), "0.00"), Format(dr("MTRS"), "0.00"), "", "", 0.00, 0.00, 0.00, 0.00, "", 0.00, "", 0, 0, 0, "", "", 0, "")
                            getsrno(gridgrn)
                            TOTAL()
                        Next
                    End If
                Else
                    MsgBox("This Challan No Does Not Exists in Challan Entry")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDMTRS_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDMTRS.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDMTRS.RowCount > 0 Then
                If GRIDMTRSDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDMTRS.Rows.RemoveAt(GRIDMTRS.CurrentRow.Index)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SENDWHATSAPP(tempgrnno)
            DT = OBJCMN.Execute_Any_String("UPDATE GRN SET GRN_SENDWHATSAPP = 1 WHERE GRN_NO = " & tempgrnno & " AND GRN_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(GRNNO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJGRN As New GRNDesign
            OBJGRN.MdiParent = MDIMain
            OBJGRN.DIRECTPRINT = True
            If ClientName = "BRILLANTO" Then
                If cmbtype.Text.Trim = "GRN FANCY" Then OBJGRN.FRMSTRING = "FINISHGRN" Else OBJGRN.FRMSTRING = "GRN"
            Else
                OBJGRN.FRMSTRING = "GRN"
            End If
            OBJGRN.DIRECTMAIL = False
            OBJGRN.DIRECTWHATSAPP = True
            OBJGRN.PRINTSETTING = PRINTDIALOG
            OBJGRN.WHERECLAUSE = "{GRN.GRN_no}=" & Val(GRNNO) & " AND {GRN.GRN_TYPE} = '" & cmbtype.Text.Trim & "'  and {GRN.GRN_yearid}=" & YearId
            OBJGRN.GRNNO = Val(GRNNO)
            OBJGRN.NOOFCOPIES = 1
            OBJGRN.Show()
            OBJGRN.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = cmbname.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\GRN_" & Val(GRNNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add("GRN_" & Val(GRNNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDMTRS_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDMTRS.CellDoubleClick
        Try
            If GRIDMTRS.CurrentRow.Index >= 0 And GRIDMTRS.Item(DMTRS.Index, GRIDMTRS.CurrentRow.Index).Value <> Nothing Then
                GRIDMTRSDOUBLECLICK = True
                TXTDMTRS.Text = GRIDMTRS.Item(DMTRS.Index, GRIDMTRS.CurrentRow.Index).Value.ToString
                TEMPMTRSROW = GRIDMTRS.CurrentRow.Index
                TXTDMTRS.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_Enter(sender As Object, e As EventArgs) Handles TXTMTRS.Enter
        If ClientName = "KOCHAR" And GRIDDOUBLECLICK = False And FRMSTRING = "GRN FANCY" And CMBPIECETYPE.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And Val(txtqty.Text.Trim) > 0 And cmbqtyunit.Text.Trim <> "" Then
            GBMTRS.Visible = True
            TXTDMTRS.Focus()
        End If
    End Sub

    Private Sub cmbitemname_Validated(sender As Object, e As EventArgs) Handles cmbitemname.Validated
        Try
            'GET CATEGORY
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(CATEGORY_NAME,'') AS CATEGORY, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEM_CATEGORYID = CATEGORY_ID LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNIT_ID", " AND ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEM_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                LBLCATEGORY.Text = DT.Rows(0).Item("CATEGORY")
                If DT.Rows(0).Item("UNIT") <> "" Then cmbqtyunit.Text = DT.Rows(0).Item("UNIT")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress, TXTFROM.KeyPress, TXTTO.KeyPress
        numkeypress(e, sender, Me)
    End Sub
    Private Sub CHALLANDATE_Validating(sender As Object, e As CancelEventArgs) Handles CHALLANDATE.Validating
        Try
            If CHALLANDATE.Text.Trim <> "__/__/____" Then
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

    Private Sub CHALLANDATE_Validated(sender As Object, e As EventArgs) Handles CHALLANDATE.Validated
        Try
            If CHALLANDATE.Text.Trim <> "__/__/____" And (ClientName = "MOHATUL" Or ClientName = "RADHA" Or ClientName = "VALIANT") Then
                GRNDATE.Text = CHALLANDATE.Text
                RECDATE.Text = CHALLANDATE.Text
            End If
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

    Private Sub cmbcolor_Enter(sender As Object, e As EventArgs) Handles cmbcolor.Enter
        Try
            Cursor.Current = Cursors.WaitCursor
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub TXTAMOUNT_Validated(sender As Object, e As EventArgs) Handles TXTAMOUNT.Validated
        Try
            'MTRS NOT MANDATORY FOR MOMAI
            If CMBPIECETYPE.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And Val(txtqty.Text.Trim) > 0 And cmbqtyunit.Text.Trim <> "" Then
                If ClientName <> "MOMAI" And ClientName <> "AXIS" And ClientName <> "GELATO" And ClientName <> "KREEVE" And Val(TXTMTRS.Text.Trim) = 0 Then Exit Sub

                If GRIDDOUBLECLICK = False And FRMSTRING = "GRN FANCY" Then
                    If EDIT = True Then
                        'GET LAST BARCODE SRNO
                        Dim LSRNO As Integer = 0
                        Dim RSRNO As Integer = 0
                        Dim SNO As Integer = 0
                        LSRNO = InStr(gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                        RSRNO = InStr(LSRNO + 1, gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                        SNO = gridgrn.Rows(gridgrn.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                        TXTBARCODE.Text = "G-" & Val(txtgrnno.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                    Else
                        TXTBARCODE.Text = "G-" & Val(txtgrnno.Text.Trim) & "/" & gridgrn.RowCount + 1 & "/" & YearId
                    End If
                End If
                FILLGRID()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWT_Validated(sender As Object, e As EventArgs) Handles TXTWT.Validated
        Try
            'GET MTRS AUTO CALCULATED, IF USER HAS WRITTEN FINISHED WIDTH AND GSM IN ITEMMASTER
            If ClientName = "VINTAGEINDIA" And Val(TXTMTRS.Text.Trim) = 0 And cmbitemname.Text.Trim <> "" And Val(TXTWT.Text.Trim) > 0 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(ITEM_WIDTH,0) AS WIDTH, ISNULL(ITEM_GSM,0) AS GSM", "", " ITEMMASTER ", " AND ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 AndAlso Val(DT.Rows(0).Item("WIDTH")) > 0 And Val(DT.Rows(0).Item("GSM")) > 0 Then TXTMTRS.Text = Format((Val(TXTWT.Text.Trim) * 1000) / ((Val(DT.Rows(0).Item("WIDTH")) / 39.37) * Val(DT.Rows(0).Item("GSM"))), "0.00")
            End If

            If TXTAMOUNT.Visible = False Then Call TXTAMOUNT_Validated(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBRACK_Validated(sender As Object, e As EventArgs) Handles CMBRACK.Validated
        Try
            If ClientName = "SOFTAS" Then TXTAMOUNT_Validated(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMCITY_Enter(sender As Object, e As EventArgs) Handles CMBFROMCITY.Enter
        Try
            If CMBFROMCITY.Text.Trim = "" Then fillCITY(CMBFROMCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMCITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBFROMCITY.Validating
        Try
            If CMBFROMCITY.Text.Trim <> "" Then CITYVALIDATE(CMBFROMCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Enter(sender As Object, e As EventArgs) Handles CMBTOCITY.Enter
        Try
            If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBTOCITY.Validating
        Try
            If CMBTOCITY.Text.Trim <> "" Then CITYVALIDATE(CMBTOCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMCITY_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBFROMCITY.KeyDown
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

    Private Sub CMBTOCITY_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBTOCITY.KeyDown
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

    Private Sub TOOLUPLOADEXCEL_Click(sender As Object, e As EventArgs) Handles TOOLUPLOADEXCEL.Click
        Try
            If EDIT = True Then Exit Sub


            OpenFileDialog1.Filter = "Excel (*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
            OpenFileDialog1.ShowDialog()
            OpenFileDialog1.AddExtension = True

            Dim cPart As Microsoft.Office.Interop.Excel.Range
            Dim oExcel As Microsoft.Office.Interop.Excel.Application = CreateObject("Excel.Application")
            Dim oBook As Microsoft.Office.Interop.Excel.Workbook = oExcel.Workbooks.Open(OpenFileDialog1.FileName, , False)
            Dim oSheet As New Microsoft.Office.Interop.Excel.Worksheet
            oSheet = oBook.Worksheets("Sheet1")


            Dim DTSAVE As New System.Data.DataTable
            DTSAVE.Columns.Add("LOTNO")
            DTSAVE.Columns.Add("ITEMNAME")
            DTSAVE.Columns.Add("DESIGNNO")
            DTSAVE.Columns.Add("COLOR")
            DTSAVE.Columns.Add("BALENO")
            DTSAVE.Columns.Add("PCS")
            DTSAVE.Columns.Add("QTY")   'MTRS
            DTSAVE.Columns.Add("UNIT")
            DTSAVE.Columns.Add("WT")

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
                    DTROWSAVE("ITEMNAME") = oSheet.Range("B" & I.ToString).Text
                Else
                    DTROWSAVE("ITEMNAME") = ""
                End If

                If IsDBNull(oSheet.Range("C" & I.ToString).Text) = False Then
                    DTROWSAVE("DESIGNNO") = oSheet.Range("C" & I.ToString).Text
                Else
                    DTROWSAVE("DESIGNNO") = ""
                End If

                If IsDBNull(oSheet.Range("D" & I.ToString).Text) = False Then
                    DTROWSAVE("COLOR") = oSheet.Range("D" & I.ToString).Text
                Else
                    DTROWSAVE("COLOR") = ""
                End If

                If IsDBNull(oSheet.Range("E" & I.ToString).Text) = False Then
                    DTROWSAVE("BALENO") = oSheet.Range("E" & I.ToString).Text
                Else
                    DTROWSAVE("BALENO") = ""
                End If


                If IsDBNull(oSheet.Range("F" & I.ToString).Text) = False Then
                    DTROWSAVE("PCS") = Val(oSheet.Range("F" & I.ToString).Text)
                Else
                    DTROWSAVE("PCS") = 0
                End If

                If IsDBNull(oSheet.Range("G" & I.ToString).Text) = False Then
                    DTROWSAVE("QTY") = Val(oSheet.Range("G" & I.ToString).Text)
                Else
                    DTROWSAVE("QTY") = 0
                End If

                If IsDBNull(oSheet.Range("H" & I.ToString).Text) = False Then
                    DTROWSAVE("UNIT") = oSheet.Range("H" & I.ToString).Text
                Else
                    DTROWSAVE("UNIT") = ""
                End If

                If IsDBNull(oSheet.Range("I" & I.ToString).Text) = False Then
                    DTROWSAVE("WT") = Val(oSheet.Range("I" & I.ToString).Text)
                Else
                    DTROWSAVE("WT") = 0
                End If


                If Val(DTROWSAVE("QTY")) = 0 Then GoTo SKIPLINE



                Dim ALPARAVAL As New ArrayList
                'CHECK WHETHER ITEMNAME IS PRESENT OR NOT IF NOT PRESENT THEN ADD NEW
                Dim OBJCMN As New ClsCommon
                Dim DTTABLE As New DataTable
                If DTROWSAVE("ITEMNAME") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", "ITEMMASTER ", "AND ITEM_NAME = '" & DTROWSAVE("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW ITEMNAME 
                        ALPARAVAL.Clear()


                        ALPARAVAL.Add("Finished Goods")
                        ALPARAVAL.Add("")   'CATEGORY
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

                        'WE HAVE USED FIXED HSN HERE
                        ALPARAVAL.Add(0)

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
                If DTROWSAVE("DESIGNNO") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("DESIGN_ID AS DESIGNID", "", "DESIGNMASTER", " AND DESIGN_NO = '" & DTROWSAVE("DESIGNNO") & "' AND DESIGN_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW DESIGN
                        Dim OBJDESIGN As New ClsDesignMaster
                        OBJDESIGN.alParaval.Add(UCase(DTROWSAVE("DESIGNNO")))
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



                'ADD NEW UNIT
                'PIECETYPE SAVE
                If DTROWSAVE("UNIT") <> "" Then
                    DTTABLE = OBJCMN.SEARCH("UNIT_ID AS UNITID", "", "UNITMASTER", " AND UNIT_ABBR = '" & DTROWSAVE("UNIT") & "' AND UNIT_YEARID = " & YearId)
                    If DTTABLE.Rows.Count = 0 Then
                        'ADD NEW UNIT
                        Dim OBJUNIT As New ClsUnitMaster
                        OBJUNIT.alParaval.Add(UCase(DTROWSAVE("UNIT"))) 'NAME
                        OBJUNIT.alParaval.Add(UCase(DTROWSAVE("UNIT"))) 'ABBR
                        OBJUNIT.alParaval.Add("")   'REMARKS
                        OBJUNIT.alParaval.Add(CmpId)
                        OBJUNIT.alParaval.Add(0)
                        OBJUNIT.alParaval.Add(Userid)
                        OBJUNIT.alParaval.Add(YearId)
                        OBJUNIT.alParaval.Add(0)   'TRANSFER

                        Dim INTRESCAT As Integer = OBJUNIT.SAVE()
                    End If
                End If



                TXTLOTNO.Text = DTROWSAVE("LOTNO")
                gridgrn.Rows.Add(0, "FRESH", DTROWSAVE("ITEMNAME"), "", DTROWSAVE("BALENO"), DTROWSAVE("DESIGNNO"), "", DTROWSAVE("COLOR"), Format(Val(DTROWSAVE("PCS")), "0.00"), DTROWSAVE("UNIT"), 0, Format(Val(DTROWSAVE("QTY")), "0.00"), "", "", Format(Val(DTROWSAVE("WT")), "0.00"), 0, 0, 0, "Mtrs", 0, "", 0, 0, 0, 0, 0, 0, "")

                DTROWSAVE = DTSAVE.NewRow()

SKIPLINE:
            Next

            oBook.Close()
            getsrno(gridgrn)

            Exit Sub


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Enter(sender As Object, e As EventArgs) Handles CMBPACKING.Enter
        Try
            If CMBPACKING.Text.Trim = "" Then FILLNAME(CMBPACKING, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Validating(sender As Object, e As CancelEventArgs) Handles CMBPACKING.Validating
        Try
            If CMBPACKING.Text.Trim <> "" Then NAMEVALIDATE(CMBPACKING, CMBCODE, e, Me, TXTDELIVERYADD, " AND  (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles TOOLEWAYBILL.Click
        Try
            If EDIT = False Then Exit Sub
            GENERATEEWB()
            PRINTEWB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GENERATEEWB()
        Try
            If ALLOWEWAYBILL = False Then Exit Sub
            If cmbname.Text.Trim = "" Then Exit Sub
            If EDIT = False Then Exit Sub

            'If Val(LBLTOTALCGSTAMT.Text.Trim) = 0 And Val(TXTCGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALSGSTAMT.Text.Trim) = 0 And Val(TXTSGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALIGSTAMT.Text.Trim) = 0 And Val(TXTIGSTAMT1.Text.Trim) = 0 Then Exit Sub


            If txtlrno.Text.Trim <> "" AndAlso lrdate.Text <> "__/__/____" Then
                If Convert.ToDateTime(lrdate.Text).Date < Convert.ToDateTime(GRNDATE.Text).Date Then
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
            Dim PARTYKMS As Double = 0
            Dim PARTYADD1 As String = ""
            Dim PARTYADD2 As String = ""
            Dim TRANSGSTIN As String = ""

            Dim OBJCMN As New ClsCommon
            'CMP ADDRESS DETAILS
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(CMP_DISPATCHFROM, '') AS ADD1, ISNULL(CMP_ADD2,'') AS ADD2 ", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
            TEMPCMPADD2 = DT.Rows(0).Item("ADD2")


            'PARTY GST DETAILS 
            DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN,  (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            Else
                PARTYGSTIN = DT.Rows(0).Item("GSTIN")
                PARTYSTATENAME = DT.Rows(0).Item("STATENAME")
                PARTYSTATECODE = DT.Rows(0).Item("STATECODE")
                PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                PARTYADD1 = DT.Rows(0).Item("ADD1")
                PARTYADD2 = DT.Rows(0).Item("ADD2")
            End If


            'FETCH PINCODE / KMS / ADD1 / ADD2 OF SHIPTO IF IT IS NOT SAME AS CMBNAME
            If CMBPACKING.Text.Trim <> "" AndAlso cmbname.Text.Trim <> CMBPACKING.Text.Trim Then
                DT = OBJCMN.SEARCH("  (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & CMBPACKING.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("PINCODE") = "" Or Val(DT.Rows(0).Item("KMS")) = 0 Then
                    MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                    'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                    PARTYADD1 = DT.Rows(0).Item("ADD1")
                    PARTYADD2 = DT.Rows(0).Item("ADD2")
                End If
            End If



            'TRANSPORT GSTIN
            DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then TRANSGSTIN = DT.Rows(0).Item("GSTIN")
            'If TRANSGSTIN = "" Then
            '    MsgBox("Enter Transport GSTIN", MsgBoxStyle.Critical)
            '    Exit Sub
            'End If



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
            If CMPEWAYCOUNTER - USEDEWAYCOUNTER = 0 Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF BALANCECOUNTERS ARE 1% THEN INTIMATE
            If CMPEWAYCOUNTER - USEDEWAYCOUNTER < Format((CMPEWAYCOUNTER * 0.01), "0") Then
                MsgBox("Only " & (CMPEWAYCOUNTER - USEDEWAYCOUNTER) & " API's Left, Kindly contact Nakoda Infotech for Renewal of EWB Package", MsgBoxStyle.Critical)
                Exit Sub
            End If


            'FOR GENERATING EWAY BILL WE NEED TO FIRST GENERATE THE TOKEN
            'THIS IS FOR SANDBOX TEST
            'Dim URL As New Uri("http://testapi.taxprogsp.co.in/ewaybillapi/dec/v1.02/authenticate?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&ewbpwd=" & CMPEWBPASS)
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
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTJONO.Text.Trim) & ",'JOBOUT','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
            'IF STATUS IS FAILED THEN ERROR MESSAGE
            If TEMPSTATUS = "FAILED" Then
                MsgBox("Unable to create Eway Bill", MsgBoxStyle.Critical)
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(txtgrnno.Text.Trim) & ",'GRN','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End If



            'GENERATING EWAY BILL 
            'FOR SANBOX TEST
            'Dim FURL As New Uri("http://testapi.taxprogsp.co.in/ewaybillapi/dec/v1.02/ewayapi?action=GENEWAYBILL&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&authtoken=" & TOKEN)
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
                j = j & """supplyType"":""I"","
                j = j & """subSupplyType"":""1"","
                j = j & """subSupplyDesc"":"""","
                j = j & """docType"":""CHL"","
                j = j & """docNo"":""" & Val(txtgrnno.Text.Trim) & """" & ","
                j = j & """docDate"":""" & GRNDATE.Text & """" & ","
                j = j & """fromGstin"":""" & CMPGSTIN & """" & ","
                j = j & """fromTrdName"":""" & CmpName & """" & ","
                j = j & """fromAddr1"":""" & TEMPCMPADD1 & """" & ","
                j = j & """fromAddr2"":""" & TEMPCMPADD2 & """" & ","
                j = j & """fromPlace"":""" & CMBFROMCITY.Text.Trim & """" & ","
                j = j & """fromPincode"":""" & CMPPINCODE & """" & ","
                j = j & """actFromStateCode"":""" & CMPSTATECODE & """" & ","
                j = j & """fromStateCode"":""" & CMPSTATECODE & """" & ","
                j = j & """toGstin"":""" & PARTYGSTIN & """" & ","
                j = j & """toTrdName"":""" & cmbname.Text.Trim & """" & ","
                j = j & """toAddr1"":""" & PARTYADD1 & """" & ","
                j = j & """toAddr2"":""" & PARTYADD2 & """" & ","
                j = j & """toPlace"":""" & CMBTOCITY.Text.Trim & """" & ","
                j = j & """toPincode"":""" & PARTYPINCODE & """" & ","
                j = j & """actToStateCode"":""" & PARTYSTATECODE & """" & ","
                j = j & """toStateCode"":""" & PARTYSTATECODE & """" & ","

                j = j & """transactionType"":""4"","
                j = j & """dispatchFromGSTIN"":""" & CMPGSTIN & """" & ","
                j = j & """dispatchFromTradeName"":""" & CmpName & """" & ","
                j = j & """shipToGSTIN"":""" & PARTYGSTIN & """" & ","
                j = j & """shipToTradeName"":""" & cmbname.Text.Trim & """" & ","
                j = j & """otherValue"":""0"","


                Dim CGSTPER, SGSTPER, IGSTPER As Double
                Dim CGSTAMT, SGSTAMT, IGSTAMT As Double
                Dim HSNCODE As String = ""
                Dim DTHSN As DataTable = OBJCMN.SEARCH("HSNMASTER.HSN_CODE AS HSNCODE, HSNMASTER.HSN_CGST AS CGSTPER, HSN_SGST AS SGSTPER, HSN_IGST AS IGSTPER", "", " ITEMMASTER INNER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID", " AND ITEMMASTER.ITEM_NAME = '" & gridgrn.Rows(0).Cells(gitemname.Index).Value & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                If DTHSN.Rows.Count <= 0 Then
                    MsgBox("Check HSN Properly", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    HSNCODE = Val(DTHSN.Rows(0).Item("HSNCODE"))

                    'IF PARTY STATE IS LOCAL THEN CGST AND SGST ELSE IGST
                    If PARTYSTATECODE = CMPSTATECODE Then
                        CGSTPER = Val(DTHSN.Rows(0).Item("CGSTPER"))
                        CGSTAMT = Format(Val(DTHSN.Rows(0).Item("CGSTPER")) * Val(LBLTOTALAMOUNT.Text.Trim) / 100, "0.00")
                        SGSTPER = Val(DTHSN.Rows(0).Item("SGSTPER"))
                        SGSTAMT = Format(Val(DTHSN.Rows(0).Item("SGSTPER")) * Val(LBLTOTALAMOUNT.Text.Trim) / 100, "0.00")
                        IGSTPER = 0
                        IGSTAMT = 0
                    Else
                        CGSTPER = 0
                        SGSTPER = 0
                        IGSTPER = Val(DTHSN.Rows(0).Item("IGSTPER"))
                        IGSTAMT = Format(Val(DTHSN.Rows(0).Item("IGSTPER")) * Val(LBLTOTALAMOUNT.Text.Trim) / 100, "0.00")
                    End If


                End If


                'THIS CODE IS WRITTEN COZ WHEN BILLTO AND SHIPTO ARE IN THE SAME PINCODE THEN WE HAVE TO PASS MINIMUM 10 KMS
                'OR ELSE IT WILL GIVE ERROR
                If CMPPINCODE = PARTYPINCODE Then PARTYKMS = 10


                j = j & """totalValue"":""" & Val(LBLTOTALAMOUNT.Text.Trim) & """" & ","
                j = j & """cgstValue"":""" & Val(CGSTAMT) & """" & ","
                j = j & """sgstValue"":""" & Val(SGSTAMT) & """" & ","
                j = j & """igstValue"":""" & Val(IGSTAMT) & """" & ","
                j = j & """cessValue"":""" & "0" & """" & ","
                j = j & """cessNonAdvolValue"":""" & "0" & """" & ","
                j = j & """totInvValue"":""" & Format(Val(LBLTOTALAMOUNT.Text.Trim) + Val(CGSTAMT) + Val(SGSTAMT) + Val(IGSTAMT), "0") & """" & ","
                j = j & """transporterId"":""" & TRANSGSTIN & """" & ","
                j = j & """transporterName"":""" & cmbtrans.Text.Trim & """" & ","


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
                    If lrdate.Text <> "__/__/____" Then j = j & """transDocDate"":""" & lrdate.Text & """" & "," Else j = j & """transDocDate"":"""","
                    j = j & """vehicleNo"":""" & TXTVEHICLENO.Text.Trim & """" & ","
                    j = j & """vehicleType"":""" & "R" & """" & ","
                End If



                j = j & """itemList"":[{"
                j = j & """productName"":""" & gridgrn.Item(gitemname.Index, 0).Value & """" & ","
                j = j & """productDesc"":""" & gridgrn.Item(gitemname.Index, 0).Value & """" & ","
                j = j & """hsnCode"":""" & HSNCODE & """" & ","
                j = j & """quantity"":""" & Val(LBLTOTALMTRS.Text.Trim) & """" & ","
                j = j & """qtyUnit"":""" & "MTR" & """" & ","
                j = j & """cgstRate"":""" & Val(CGSTPER) & """" & ","
                j = j & """sgstRate"":""" & Val(SGSTPER) & """" & ","
                j = j & """igstRate"":""" & Val(IGSTPER) & """" & ","
                j = j & """cessRate"":""" & "0" & """" & ","
                j = j & """cessNonAdvol"":""" & "0" & """" & ","
                j = j & """taxableAmount"":""" & Val(LBLTOTALAMOUNT.Text.Trim) & """"

                j = j & " }]}"

                Dim stream As Stream = REQUEST.GetRequestStream()
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(j)
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()

            Catch ex As WebException
                RESPONSE = ex.Response
                MsgBox("Error While Generating EWB, Please check the Data Properly")
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(txtgrnno.Text.Trim) & ",'GRN','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
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
            DT = OBJCMN.Execute_Any_String("UPDATE GRN SET GRN_EWAYBILLNO = '" & TXTEWAYBILLNO.Text.Trim & "' WHERE GRN_NO = " & Val(tempgrnno) & " AND GRN.GRN_TYPE = '" & cmbtype.Text.Trim & "' AND GRN_YEARID = " & YearId, "", "")

            'ADD DATA IN EWAYENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(txtgrnno.Text.Trim) & ",'GRN','" & TOKEN & "','" & EWBNO & "','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


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
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(txtgrnno.Text.Trim) & ",'GRN','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(txtgrnno.Text.Trim) & ",'GRN','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

            Catch ex As WebException
                RESPONSE = ex.Response
                MsgBox("Error While Printing EWB, Please check the Data Properly")
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(txtgrnno.Text.Trim) & ",'GRN','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(txtgrnno.Text.Trim) & ",'GRN','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtchallan_Validated(sender As Object, e As EventArgs) Handles txtchallan.Validated
        Try
            'GET CHALLAN DATA 
            'If ClientName = "VINTAGEINDIA" And Val(txtchallan.Text.Trim) > 0 Then
            '    Dim OBJCMN As New ClsCommon
            '    Dim DT As DataTable = OBJCMN.SEARCH(" GDN.GDN_NO, GDN.GDN_date AS CHALLANDATE, GODOWNMASTER.GODOWN_name AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTLEDGERS.Acc_name, '') AS AGENT, ISNULL(GDN_DESC.GDN_PRINTDESC, '') AS [DESC], ISNULL(GDN_DESC.GDN_BALENO, '') AS BALENO, ISNULL(GDN_DESC.GDN_PCS, 0) AS QTY, ISNULL(GDN_DESC.GDN_CUT, 0) AS CUT, ISNULL(GDN_DESC.GDN_MTRS, 0) AS MTRS, ISNULL(UNITMASTER.unit_abbr, '') AS QTYUNIT, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY", "", "    GDN INNER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON GDN_DESC.GDN_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN UNITMASTER ON GDN_DESC.GDN_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON GDN.GDN_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON GDN.GDN_YEARID = LEDGERS.Acc_yearid AND GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN GODOWNMASTER ON GDN.GDN_YEARID = GODOWNMASTER.GODOWN_yearid AND GDN.GDN_GODOWNID = GODOWNMASTER.GODOWN_id ", " AND GDN.GDN_NO ='" & txtchallan.Text.Trim & "' AND GDN.GDN_YEARID = " & YearId)
            '    If DT.Rows.Count > 0 Then
            '        Dim tempmsg As Integer = MessageBox.Show("Do You Want To Fetch Data From Challan?", "", MessageBoxButtons.YesNo)
            '        If tempmsg = vbYes Then
            '            For Each dr As DataRow In DT.Rows
            '                cmbname.Text = DT.Rows(0).Item("PARTYNAME")
            '                CHALLANDATE.Text = DT.Rows(0).Item("CHALLANDATE")
            '                CMBBROKER.Text = DT.Rows(0).Item("AGENT")
            '                cmbGodown.Text = DT.Rows(0).Item("GODOWN")
            '                TXTLOTNO.Text = txtgrnno.Text.Trim + "/" + txtchallan.Text.Trim
            '                gridgrn.Rows.Add(0, dr("PIECETYPE").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGNNO").ToString, dr("DESC").ToString, dr("COLOR"), Format(dr("qty"), "0.00"), dr("QTYUNIT").ToString, Format(dr("CUT"), "0.00"), Format(dr("MTRS"), "0.00"), "", "", 0.00, 0.00, 0.00, 0.00, "", 0.00, "", 0, 0, 0, "", "", 0, "")
            '                getsrno(gridgrn)
            '                TOTAL()
            '            Next
            '        End If
            '    Else
            '        MsgBox("This Challan No Does Not Exists in Challan Entry")
            '    End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class