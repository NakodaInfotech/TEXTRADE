
Imports System.ComponentModel
Imports BL
Imports System.IO

Public Class GreyReceivedTransport

    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Public EDIT As Boolean          'used for editing
    Public TEMPGRNNO As Integer     'used for poation no while editing
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public Shared selectPOtable As New DataTable
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim PARTYCHALLANNO As String
    Dim ALLOWMANUALGRNNO As Boolean = False

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub clear()

        tstxtbillno.Clear()
        EP.Clear()
        GRIDORDER.RowCount = 0

        If ALLOWMANUALGRNNO = True Then
            TXTGREYRECNO.ReadOnly = False
            TXTGREYRECNO.BackColor = Color.LemonChiffon
        Else
            TXTGREYRECNO.ReadOnly = True
            TXTGREYRECNO.BackColor = Color.Linen
        End If

        CMBNAME.Enabled = True
        CMBNAME.Text = ""
        CMBQUALITY.Text = ""
        CMBDESIGN.Text = ""
        CHALLANDATE.Text = Now.Date
        txtchallan.Clear()
        txtpono.Clear()
        podate.Value = Now.Date
        GREYRECDATE.Text = Now.Date

        cmbtrans.Text = ""
        txtlrno.Clear()
        lrdate.Value = Now.Date

        txtremarks.Clear()

        txtuploadsrno.Text = 1
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        gridupload.RowCount = 0
        txtimgpath.Clear()
        TXTNEWIMGPATH.Clear()
        TXTFILENAME.Clear()
        PBSoftCopy.ImageLocation = ""
        CMBAGENT.Text = ""
        TXTCRDAYS.Clear()


        lbllocked.Visible = False
        PBlock.Visible = False
        LBLWHATSAPP.Visible = False


        'clearing itemgrid textboxes and combos
        txtsrno.Text = 1
        cmbitemname.Text = ""
        TXTBALENO.Clear()
        If ClientName = "KOTHARI" Then CMBQUALITY.Text = "FINISHED" Else CMBQUALITY.Text = ""

        cmbcolor.Text = ""
        If ClientName = "MSANCHITKUMAR" Or ClientName = "KEMLINO" Or ClientName = "MOHATUL" Then txtqty.Clear()
        If ClientName = "YASHVI" Or ClientName = "KEMLINO" Or ClientName = "SOFTAS" Or ClientName = "SHREENAKODA" Then cmbqtyunit.Text = "LUMP" Else cmbqtyunit.Text = "Pcs"
        If ClientName = "AVIS" Then cmbqtyunit.Text = "Mtrs"
        If ClientName = "MNIKHIL" Or ClientName = "HRITI" Then cmbqtyunit.Text = "ROLL"

        TXTCUT.Clear()
        If ClientName = "INDRANI" Then TXTMTRS.Text = 1 Else TXTMTRS.Clear()
        TXTWT.Clear()
        GRIDGRN.RowCount = 0
        cmbtrans.Text = ""
        txtlrno.Clear()
        TXTRATE.Clear()
        TXTAMT.Clear()

        cmdselectPO.Enabled = True
        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        getmaxno()

        LBLTOTALMTRS.Text = 0
        LBLTOTALBALES.Text = 0
        LBLTOTALQTY.Text = 0
        LBLTOTALWT.Text = 0
        LBLTOTALAMT.Text = 0

    End Sub

    Sub total()
        Try
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALWT.Text = 0.0
            LBLTOTALQTY.Text = 0.0
            LBLTOTALAMT.Text = 0.00

            For Each ROW As DataGridViewRow In GRIDGRN.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALQTY.Text = Format(Val(LBLTOTALQTY.Text) + Val(ROW.Cells(gQty.Index).EditedFormattedValue), "0.00")
                    If ROW.Cells(gcut.Index).EditedFormattedValue > 0 Then ROW.Cells(GMTRS.Index).Value = Val(ROW.Cells(gQty.Index).EditedFormattedValue) * Val(ROW.Cells(gcut.Index).EditedFormattedValue)
                    If ROW.Cells(GRATE.Index).EditedFormattedValue > 0 Then ROW.Cells(GAMT.Index).Value = Format(Val(ROW.Cells(GMTRS.Index).EditedFormattedValue) * Val(ROW.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALAMT.Text = Format(Val(LBLTOTALAMT.Text) + Val(ROW.Cells(GAMT.Index).EditedFormattedValue), "0.00")
                End If
            Next
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
            For i = 0 To GRIDGRN.Rows.Count - 1
                If Not GRIDGRN.Rows(i).IsNewRow Then
                    cellValue = GRIDGRN(GBALENO.Index, i).EditedFormattedValue.ToString()
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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Private Sub CHALLANDATE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHALLANDATE.Enter
        CHALLANDATE.SelectAll()
    End Sub

    Private Sub GRNDATE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GREYRECDATE.Enter
        GREYRECDATE.SelectAll()
    End Sub

    Private Sub GRNDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GREYRECDATE.Validating
        Try
            If GREYRECDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(GREYRECDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    'SAME DATE FOR CHALLANDATE / LRDATE 
                    If ClientName = "DAKSH" Or ClientName = "SHALIBHADRA" Then
                        CHALLANDATE.Text = GREYRECDATE.Text
                        lrdate.Value = Convert.ToDateTime(GREYRECDATE.Text).Date
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getmaxno()
        Dim DTTABLE As DataTable = getmax(" isnull(max(GREYREC_no),0) + 1 ", "GREYRECTRANSPORT", " AND GREYREC_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTGREYRECNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True
            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, " Please Fill Company Name ")
                bln = False
            End If

            If cmbtrans.Text.Trim.Length = 0 Then
                EP.SetError(cmbtrans, " Please Enter Transport Name ")
                bln = False
            End If

            If ClientName = "SNCM" Then
                If txtlrno.Text.Trim = "" Then
                    EP.SetError(txtlrno, " Please Enter LR No ")
                    bln = False
                End If
                If txtchallan.Text.Trim = "" Then
                    EP.SetError(txtchallan, " Please Enter Challan No ")
                    bln = False
                End If
            End If

                Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If lbllocked.Visible = True And UserName <> "Admin" Then
                EP.SetError(lbllocked, "Grey Issue Done, Delete Issue First")
                bln = False
            End If

            If GRIDGRN.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If

            If ClientName <> "CC" And ClientName <> "GELATO" And ClientName <> "MOMAI" And ClientName <> "AXIS" And ClientName <> "KREEVE" Then
                For Each row As DataGridViewRow In GRIDGRN.Rows
                    DT = OBJCMN.search("MATERIAL_NAME", "", "  ITEMMASTER INNER JOIN MATERIALTYPEMASTER ON ITEMMASTER.item_materialtypeid = MATERIALTYPEMASTER.material_id AND ITEMMASTER.item_cmpid = MATERIALTYPEMASTER.material_cmpid AND ITEMMASTER.item_locationid = MATERIALTYPEMASTER.material_locationid AND ITEMMASTER.item_yearid = MATERIALTYPEMASTER.material_yearid ", " AND ITEMMASTER.ITEM_NAME = '" & row.Cells(gitemname.Index).Value & "' AND ITEM_CMPID = " & CmpId & " AND ITEM_LOCATIONID = " & Locationid & " AND ITEM_YEARID = " & YearId)
                    If Val(row.Cells(GMTRS.Index).Value) = 0 And (DT.Rows(0).Item(0) = "Raw Material" Or DT.Rows(0).Item(0) = "Semi Finished Goods" Or DT.Rows(0).Item(0) = "Finished Goods") Then
                        EP.SetError(TabControl1, "MTRS Cannot be kept Blank")
                        bln = False
                    End If
                Next
            End If

            If ALLOWMANUALGRNNO = True Then
                If Val(TXTGREYRECNO.Text.Trim) <> 0 And EDIT = False Then
                    Dim OBJCMNn As New ClsCommon
                    Dim dttable As DataTable = OBJCMNn.search(" ISNULL(GREYRECTRANSPORT.GREYREC_NO,0)  AS GRNNO", "", " GREYRECTRANSPORT ", "  AND GREYRECTRANSPORT.GREYREC_NO=" & Val(TXTGREYRECNO.Text.Trim) & " AND GREYRECTRANSPORT.GREYREC_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        MsgBox("Entry No Already Exist")
                        bln = False
                    End If
                End If
            End If


            If txtchallan.Text.Trim <> "" Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(txtchallan.Text.Trim)) Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    DT = objclscommon.search(" GREYRECTRANSPORT.GREYREC_challanno, LEDGERS.ACC_cmpname", "", " GREYRECTRANSPORT inner join LEDGERS on LEDGERS.ACC_id = GREYRECTRANSPORT.GREYREC_ledgerid ", " and GREYRECTRANSPORT.GREYREC_challanno = '" & txtchallan.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & CMBNAME.Text.Trim & "' AND GREYREC_YEARID =" & YearId)
                    If DT.Rows.Count > 0 Then
                        EP.SetError(txtchallan, "Challan No. Already Exists")
                        bln = False
                    End If
                End If
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

                For Each ROW As DataGridViewRow In GRIDGRN.Rows
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
                            ROW.Cells(GRATE.Index).Value = Val(ORDROW.Cells(ORATE.Index).Value)
                            TEMPORDERROWNO = -1
                            Exit For
CHECKNEXTLINE:
                        End If
                    Next
                    'IF NO FURTHER MACHING IS FOUND BUT WE HAVE TEMPORDERROWNO THEN ADD VALUE IN THAT ROW
                    If TEMPORDERROWNO >= 0 Then
                        GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNQTY.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNQTY.Index).Value) + Val(ROW.Cells(gQty.Index).Value)
                        GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNMTRS.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNMTRS.Index).Value) + Val(ROW.Cells(GMTRS.Index).Value)
                        ROW.Cells(GRATE.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(ORATE.Index).Value)
                        TEMPORDERROWNO = -1
                    End If
                    If TEMPORDERMATCH = False Then
                        ROW.DefaultCellStyle.BackColor = Color.LightGreen
                        If MsgBox("There are Items which are not Present in Selected Order, Wish to Proceed", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            EP.SetError(CMBNAME, "There are Items which are not Present in Selected Order")
                            bln = False
                        End If
                    End If
                    TEMPORDERMATCH = False
                Next
            End If
            total()

            'lock challan for excess of 20%
            If ClientName = "ANOX" Then
                For Each ROW As DataGridViewRow In GRIDORDER.Rows
                    ROW.DefaultCellStyle.BackColor = Color.Empty
                    If Val(ROW.Cells(OGRNMTRS.Index).Value) > 0 And ((Val(ROW.Cells(OGRNMTRS.Index).Value) - Val(ROW.Cells(OMTRS.Index).Value)) / Val(ROW.Cells(OMTRS.Index).Value)) * 100 > 20 Then
                        If MsgBox("Inward Greater then Allowed Ordered Mtrs, Wish to Proceed", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            ROW.DefaultCellStyle.BackColor = Color.LightPink
                            EP.SetError(CMBNAME, "Inward Greater then Allowed Ordered Mtrs")
                            bln = False
                        End If
                    End If
                Next
            End If


            For Each row As DataGridViewRow In GRIDGRN.Rows
                If ITEMCOSTCENTRE = True And Val(row.Cells(GRATE.Index).Value) = 0 Then
                    EP.SetError(CMBNAME, "Rate Cannot be 0")
                    bln = False
                End If
            Next


            If GREYRECDATE.Text = "__/__/____" Then
                EP.SetError(GREYRECDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(GREYRECDATE.Text) Then
                    EP.SetError(GREYRECDATE, "Date not in Accounting Year")
                    bln = False
                End If

                'GREYRECTRANSPORT DATE CANNOT BE LESS THEN PODATE
                If Val(txtpono.Text.Trim) > 0 And Convert.ToDateTime(GREYRECDATE.Text).Date < podate.Value.Date Then
                    EP.SetError(GREYRECDATE, "Date Cannot be before PO Date")
                    bln = False
                End If
                If Convert.ToDateTime(GREYRECDATE.Text).Date < GREYRTBLOCKDATE.Date Then
                    EP.SetError(GREYRECDATE, "Date is Blocked, Please make entries after " & Format(GREYRTBLOCKDATE.Date, "dd/MM/yyyy"))
                    bln = False
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

            ' If CMBTONAME.Text.Trim <> "" Then ADDPOUT(TXTPOUTNO)
            Dim alParaval As New ArrayList

            If TXTGREYRECNO.ReadOnly = False Then
                alParaval.Add(Val(TXTGREYRECNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(Format(Convert.ToDateTime(GREYRECDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(txtpono.Text.Trim)
            alParaval.Add(Format(podate.Value.Date, "MM/dd/yyyy"))
            alParaval.Add(txtchallan.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(txtlrno.Text.Trim)
            alParaval.Add(lrdate.Value)

            alParaval.Add(Val(LBLTOTALBALES.Text))
            alParaval.Add(Val(LBLTOTALQTY.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(Val(LBLTOTALWT.Text))
            alParaval.Add(Val(LBLTOTALAMT.Text))

            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim GRIDSRNO As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim BALENO As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim QTY As String = ""
            Dim qtyunit As String = ""
            Dim CUT As String = ""
            Dim MTRS As String = ""
            Dim WT As String = ""
            Dim RATE As String = ""
            Dim AMOUNT As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGRN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(row.Cells(gsrno.Index).Value)
                        ITEMNAME = row.Cells(gitemname.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = row.Cells(GBALENO.Index).Value.ToString Else BALENO = ""
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        QTY = Val(row.Cells(gQty.Index).Value)
                        qtyunit = row.Cells(gqtyunit.Index).Value.ToString
                        CUT = Val(row.Cells(gcut.Index).Value)
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        WT = Val(row.Cells(GWT.Index).Value)
                        RATE = Val(row.Cells(GRATE.Index).Value)
                        AMOUNT = Val(row.Cells(GAMT.Index).Value)
                        OUTPCS = Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = Val(row.Cells(GOUTMTRS.Index).Value)

                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(gsrno.Index).Value)
                        ITEMNAME = ITEMNAME & "|" & row.Cells(gitemname.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString Else BALENO = BALENO & "|" & ""
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        QTY = QTY & "|" & Val(row.Cells(gQty.Index).Value)
                        qtyunit = qtyunit & "|" & row.Cells(gqtyunit.Index).Value
                        CUT = CUT & "|" & Val(row.Cells(gcut.Index).Value)
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        RATE = RATE & "|" & Val(row.Cells(GRATE.Index).Value)
                        AMOUNT = AMOUNT & "|" & Val(row.Cells(GAMT.Index).Value)
                        OUTPCS = OUTPCS & "|" & Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = OUTMTRS & "|" & Val(row.Cells(GOUTMTRS.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(ITEMNAME)
            alParaval.Add(QUALITY)
            alParaval.Add(BALENO)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(QTY)
            alParaval.Add(qtyunit)
            alParaval.Add(CUT)
            alParaval.Add(MTRS)
            alParaval.Add(WT)
            alParaval.Add(RATE)
            alParaval.Add(AMOUNT)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)

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

            alParaval.Add(CMBAGENT.Text.Trim)
            alParaval.Add(TXTCRDAYS.Text.Trim)


            Dim OBJGREYREC As New ClsGreyRecTransport()
            OBJGREYREC.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = OBJGREYREC.SAVE()
                TXTGREYRECNO.Text = Val(DTTABLE.Rows(0).Item(0))
                MsgBox("Details Added")

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPGRNNO)
                Dim IntResult As Integer = OBJGREYREC.UPDATE()
                MsgBox("Details Updated")

            End If


            If ClientName = "BRILLANTO" Or ClientName = "DAKSH" Then PRINTREPORT(Val(TXTGREYRECNO.Text.Trim))

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

            clear()
            If ClientName = "AMAN" Then txtchallan.Focus() Else CMBNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub GREYREC_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            GRIDGRN.Focus()
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

    Private Sub GREYREC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            fillcmb()
            clear()

            If ClientName = "SVS" Or ClientName = "BRILLANTO" Then
                txtqty.Text = "1"
                txtqty.BackColor = Color.Linen
                txtqty.ReadOnly = True
            End If

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJGREYREC As New ClsGreyRecTransport()
                Dim dttable As DataTable = OBJGREYREC.SELECTGREY(TEMPGRNNO, CmpId, YearId)

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTGREYRECNO.Text = TEMPGRNNO
                        TXTGREYRECNO.ReadOnly = True
                        GREYRECDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)

                        txtpono.Text = Convert.ToString(dr("PONO").ToString)
                        podate.Value = Format(Convert.ToDateTime(dr("PODATE")).Date, "dd/MM/yyyy")

                        txtchallan.Text = Convert.ToString(dr("CHALLANNO").ToString)
                        PARTYCHALLANNO = txtchallan.Text.Trim
                        CHALLANDATE.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")

                        cmbtrans.Text = dr("TRANSNAME").ToString
                        txtlrno.Text = dr("LRNO").ToString
                        lrdate.Text = Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy")

                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        CMBAGENT.Text = Convert.ToString(dr("AGENT").ToString)
                        TXTCRDAYS.Text = dr("CRDAYS").ToString

                        'If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True


                        'Item Grid
                        GRIDGRN.Rows.Add(dr("GRIDSRNO").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGNNO").ToString, dr("COLOR"), Format(dr("qty"), "0.00"), dr("QTYUNIT").ToString, Format(dr("CUT"), "0.00"), Format(dr("MTRS"), "0.00"), Format(dr("WT"), "0.00"), Format(dr("RATE"), "0.00"), Format(dr("AMOUNT"), "0.00"), Val(dr("OUTPCS")), Val(dr("OUTMTRS")))

                        If Val(dr("OUTMTRS")) > 0 Then
                            GRIDGRN.Rows(GRIDGRN.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                    Next

                    total()
                Else
                    EDIT = False
                    clear()
                End If

                Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.search(" GREYREC_GRIDSRNO AS GRIDSRNO, GREYREC_REMARKS AS REMARKS, GREYREC_NAME AS NAME, GREYREC_IMGPATH AS IMGPATH, GREYREC_NEWIMGPATH AS NEWIMGPATH", "", " GREYRECTRANSPORT_UPLOAD", " AND GREYREC_NO = " & TEMPGRNNO & " AND GREYREC_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), DTR("IMGPATH"), DTR("NEWIMGPATH"))
                    Next
                End If


                'ORDER GRID
                'Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.search(" GREYRECTRANSPORT_PODETAILS.GREYREC_GRIDSRNO AS GRIDSRNO, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, GREYRECTRANSPORT_PODETAILS.GREYREC_ORDERPCS AS ORDERQTY, ISNULL(GREYRECTRANSPORT_PODETAILS.GREYREC_ORDERMTRS,0) AS ORDERMTRS, GREYRECTRANSPORT_PODETAILS.GREYREC_FROMNO AS FROMNO, GREYRECTRANSPORT_PODETAILS.GREYREC_FROMSRNO AS FROMSRNO, GREYRECTRANSPORT_PODETAILS.GREYREC_FROMTYPE AS FROMTYPE, GREYRECTRANSPORT_PODETAILS.GREYREC_PCS AS GRNQTY, ISNULL(GREYRECTRANSPORT_PODETAILS.GREYREC_MTRS,0) AS GRNMTRS, ISNULL(GREYRECTRANSPORT_PODETAILS.GREYREC_RATE,0) AS RATE ", "", " GREYRECTRANSPORT_PODETAILS INNER JOIN ITEMMASTER ON GREYRECTRANSPORT_PODETAILS.GREYREC_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON GREYRECTRANSPORT_PODETAILS.GREYREC_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GREYRECTRANSPORT_PODETAILS.GREYREC_DESIGNID = DESIGNMASTER.DESIGN_id  ", " AND GREYRECTRANSPORT_PODETAILS.GREYREC_NO = " & Val(TEMPGRNNO) & " AND GREYRECTRANSPORT_PODETAILS.GREYREC_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        GRIDORDER.Rows.Add(Val(DTR("GRIDSRNO")), DTR("ITEMNAME"), DTR("DESIGNNO"), DTR("COLOR"), Val(DTR("ORDERQTY")), Val(DTR("ORDERMTRS")), Val(DTR("FROMNO")), Val(DTR("FROMSRNO")), DTR("FROMTYPE"), Val(DTR("GRNQTY")), Val(DTR("GRNMTRS")), Val(DTR("RATE")))
                    Next
                End If
                getsrno(GRIDORDER)

            End If


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Sub fillcmb()
        Try
            If CMBNAME.Text.Trim = "" Then
                If ClientName = "AMAN" Then
                    fillname(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' and ACC_TYPE = 'ACCOUNTS'")
                Else
                    fillname(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
                End If
            End If
            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT'")
            fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            fillQUALITY(CMBQUALITY, EDIT)
            FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
            fillunit(cmbqtyunit)
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)

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

            Dim OBJGREYRECDTLS As New GreyReceivedTransportDetails
            OBJGREYRECDTLS.MdiParent = MDIMain
            OBJGREYRECDTLS.Show()
            OBJGREYRECDTLS.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "Sundry Creditors", "TRANSPORT")
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
            If txtchallan.Text.Trim.Length > 0 Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(txtchallan.Text.Trim)) Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    Dim dt As DataTable = objclscommon.search(" GREYRECTRANSPORT.GREYREC_challanno, LEDGERS.ACC_cmpname", "", " GREYRECTRANSPORT inner join LEDGERS on LEDGERS.ACC_id = GREYRECTRANSPORT.GREYREC_ledgerid ", " and GREYRECTRANSPORT.GREYREC_challanno = '" & txtchallan.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & CMBNAME.Text.Trim & "' AND GREYREC_YEARID =" & YearId)
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

            If CMBNAME.Text.Trim = "" Then
                MsgBox("Select Party Name", MsgBoxStyle.Critical)
                CMBNAME.Focus()
                Exit Sub
            End If

            Dim DTPO As New DataTable
            Dim OBJSELECTPO As New SelectPO
            OBJSELECTPO.PARTYNAME = CMBNAME.Text.Trim
            OBJSELECTPO.FRMSTRING = "GREY"
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

                fillledger(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = '" & DTPO.Rows(0).Item("GROUPNAME") & "' ")
                CMBNAME.Text = DTPO.Rows(0).Item("NAME")
                cmbtrans.Text = DTPO.Rows(0).Item("TRANSPORT")


                txtpono.Enabled = False
                podate.Value = DTPO.Rows(0).Item("PODATE")
                CMBAGENT.Text = DTPO.Rows(0).Item("AGENT")
                TXTCRDAYS.Text = DTPO.Rows(0).Item("CRDAYS")




                'BEFORE ADDING THE ROW IN ORDERDER GRID CHECK WHETHER SAME ORDERNO AN SRNO IS PRESENT IN GRID OR NOT
                For Each DTROW As DataRow In DTPO.Rows
                    For Each ROW As DataGridViewRow In GRIDORDER.Rows
                        If Val(ROW.Cells(OFROMNO.Index).Value) = Val(DTROW("PONO")) And Val(ROW.Cells(OFROMSRNO.Index).Value) = Val(DTROW("GRIDSRNO")) And ROW.Cells(OFROMTYPE.Index).Value = DTROW("TYPE") Then GoTo NEXTLINE
                    Next

                    'for AVIS GET THIS ITEMNAME AND OTHER DETAILS IN GRID COMBO BOX
                    If ClientName = "AVIS" Or ClientName = "SNCM" Then
                        cmbitemname.Text = DTROW("ITEMNAME")
                        CMBDESIGN.Text = DTROW("DESIGNNO")
                        cmbcolor.Text = DTROW("COLOR")
                        txtremarks.Text = DTPO.Rows(0).Item("REMARKS")
                        If ClientName = "SNCM" Then
                            TXTRATE.Text = Val(DTROW("RATE"))
                            TXTMTRS.Text = Val(DTROW("MTRS"))
                            cmbqtyunit.Text = "Pcs"
                        End If
                    End If


                    GRIDORDER.Rows.Add(0, DTROW("ITEMNAME"), DTROW("DESIGNNO"), DTROW("COLOR"), Val(DTROW("QTY")), Val(DTROW("MTRS")), DTROW("PONO"), DTROW("GRIDSRNO"), DTROW("TYPE"), 0, 0, Val(DTROW("RATE")))
                    If ClientName = "MOMAI" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                        'FILL SAME DATA IN GRNGRID
                        GRIDGRN.Rows.Add(0, "FRESH", DTROW("ITEMNAME"), "", "", DTROW("DESIGNNO"), "", DTROW("COLOR"), Val(DTROW("QTY")), "Pcs", 0, Val(DTROW("MTRS")), "", "", 0, 0, 0, 0, "", 0, Val(DTROW("RATE")), 0, DTROW("PONO"), DTROW("GRIDSRNO"))
                    End If
NEXTLINE:
                Next
                getsrno(GRIDORDER)
                getsrno(GRIDGRN)

            End If

            cmdselectPO.Enabled = True
            total()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDGRN.RowCount = 0
                TEMPGRNNO = Val(tstxtbillno.Text)
                If TEMPGRNNO > 0 Then
                    EDIT = True
                    GREYREC_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()

        GRIDGRN.Enabled = True
        Dim TEMPQTY As Integer = Val(txtqty.Text.Trim)
        If ClientName = "SVS" Or ClientName = "CC" Or ClientName = "GELATO" Or ClientName = "INDRANI" Then txtqty.Text = "1"

        If GRIDDOUBLECLICK = False Then
            GRIDGRN.Rows.Add(Val(txtsrno.Text.Trim), cmbitemname.Text.Trim, CMBQUALITY.Text.Trim, TXTBALENO.Text.Trim, CMBDESIGN.Text.Trim, cmbcolor.Text.Trim, Format(Val(txtqty.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTWT.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), Format(Val(TXTAMT.Text.Trim), "0.00"), 0, 0)
            getsrno(GRIDGRN)
        ElseIf GRIDDOUBLECLICK = True Then

            GRIDGRN.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDGRN.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
            GRIDGRN.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDGRN.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
            GRIDGRN.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDGRN.Item(gcolor.Index, TEMPROW).Value = cmbcolor.Text.Trim
            GRIDGRN.Item(gQty.Index, TEMPROW).Value = Format(Val(txtqty.Text.Trim), "0.00")
            GRIDGRN.Item(gqtyunit.Index, TEMPROW).Value = cmbqtyunit.Text.Trim
            GRIDGRN.Item(gcut.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
            GRIDGRN.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDGRN.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.00")
            GRIDGRN.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")
            GRIDGRN.Item(GAMT.Index, TEMPROW).Value = Format(Val(TXTAMT.Text.Trim), "0.00")
            GRIDDOUBLECLICK = False

        End If

        total()

        GRIDGRN.FirstDisplayedScrollingRowIndex = GRIDGRN.RowCount - 1

        If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "BRILLANTO" Or ClientName = "INDRANI" Then TXTBALENO.Clear()
        If ClientName = "SOFTAS" Then CMBQUALITY.Text = ""

        TXTCUT.Clear()
        If ClientName = "INDRANI" Then TXTMTRS.Text = 1 Else TXTMTRS.Clear()
        TXTRATE.Clear()
        TXTAMT.Clear()
        TXTWT.Clear()
        txtsrno.Text = GRIDGRN.RowCount + 1

        If ClientName = "INDRANI" Or ClientName = "AXIS" Or ClientName = "SUCCESS" Or ClientName = "BRILLANTO" Or ClientName = "OWAIS" Then
            TXTBALENO.Focus()
        ElseIf ClientName = "MOMAI" Or ClientName = "KREEVE" Then
            CMBDESIGN.Text = ""
            txtqty.Clear()
            CMBDESIGN.Focus()
        ElseIf ClientName = "MOHATUL" Or ClientName = "MNIKHIL" Or ClientName = "HRITI" Or ClientName = "ANOX" Then
            cmbcolor.Focus()
        ElseIf ClientName = "GELATO" Then
            cmbitemname.Focus()
        Else
            TXTMTRS.Focus()
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
        TXTNEWIMGPATH.Text = Application.StartupPath & "\UPLOADDOCS\" & TXTGREYRECNO.Text.Trim & txtuploadsrno.Text.Trim & TXTFILENAME.Text.Trim
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

    Private Sub txtuploadsrno_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuploadsrno.Enter
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

            If Val(GRIDGRN.CurrentRow.Cells(GOUTMTRS.Index).Value) > 0 Then
                MessageBox.Show("Row Locked, You Cannot Edit This Row")
                Exit Sub
            End If

            If GRIDGRN.CurrentRow.Index >= 0 And GRIDGRN.Item(gsrno.Index, GRIDGRN.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDGRN.Item(gsrno.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                cmbitemname.Text = GRIDGRN.Item(gitemname.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDGRN.Item(GQUALITY.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                TXTBALENO.Text = GRIDGRN.Item(GBALENO.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDGRN.Item(GDESIGN.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                cmbcolor.Text = GRIDGRN.Item(gcolor.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                txtqty.Text = GRIDGRN.Item(gQty.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                cmbqtyunit.Text = GRIDGRN.Item(gqtyunit.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                TXTCUT.Text = GRIDGRN.Item(gcut.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                TXTMTRS.Text = GRIDGRN.Item(GMTRS.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                TXTWT.Text = GRIDGRN.Item(GWT.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                TXTRATE.Text = GRIDGRN.Item(GRATE.Index, GRIDGRN.CurrentRow.Index).Value.ToString
                TXTAMT.Text = GRIDGRN.Item(GAMT.Index, GRIDGRN.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDGRN.CurrentRow.Index
                cmbitemname.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridGREYREC_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDGRN.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDGRN.RowCount = 0
LINE1:
            TEMPGRNNO = Val(TXTGREYRECNO.Text) - 1
            If TEMPGRNNO > 0 Then
                EDIT = True
                GREYREC_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDGRN.RowCount = 0 And TEMPGRNNO > 1 Then
                TXTGREYRECNO.Text = TEMPGRNNO
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
            GRIDGRN.RowCount = 0
LINE1:
            TEMPGRNNO = Val(TXTGREYRECNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTGREYRECNO.Text.Trim
            clear()
            If Val(TXTGREYRECNO.Text) - 1 >= TEMPGRNNO Then
                EDIT = True
                GREYREC_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDGRN.RowCount = 0 And TEMPGRNNO < MAXNO Then
                TXTGREYRECNO.Text = TEMPGRNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtTOTALBALES_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtqty.KeyPress, TXTMTRS.KeyPress, TXTRATE.KeyPress, TXTCUT.KeyPress, TXTAMT.KeyPress
        numdotkeypress(e, sender, Me)
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
                    MsgBox("Unable To Delete, Checking Done / Item Used", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete GREYRECTRANSPORT?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub

                Dim alParaval As New ArrayList
                alParaval.Add(TXTGREYRECNO.Text.Trim)
                alParaval.Add(Userid)
                alParaval.Add(CmpId)
                alParaval.Add(YearId)

                Dim OBJGREY As New ClsGreyRecTransport()
                OBJGREY.alParaval = alParaval
                IntResult = OBJGREY.DELETE()
                MsgBox("Entry Deleted")
                clear()
                EDIT = False
            Else
                MsgBox("Delete Is only In Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbqtyunit_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbqtyunit.Enter
        Try
            If cmbqtyunit.Text.Trim = "" Then fillunit(cmbqtyunit)
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

    Private Sub gridGREYREC_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDGRN.KeyDown

        Try
            If e.KeyCode = Keys.Delete And GRIDGRN.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row Is In Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                If Val(GRIDGRN.CurrentRow.Cells(GOUTMTRS.Index).Value) > 0 Then
                    MessageBox.Show("Row Locked, You Cannot Delete This Row")
                    Exit Sub
                End If


                'end of block
                GRIDGRN.Rows.RemoveAt(GRIDGRN.CurrentRow.Index)
                getsrno(GRIDGRN)
                total()
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcolor.Enter
        Try
            Cursor.Current = Cursors.WaitCursor
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcolor.Validating
        Try
            If cmbcolor.Text.Trim <> "" Then COLORVALIDATE(cmbcolor, e, Me, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then
                If ClientName = "AMAN" Then
                    fillname(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' and ACC_TYPE = 'ACCOUNTS'")
                Else
                    fillname(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then
                If ClientName = "AMAN" Then
                    namevalidate(CMBNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'", "Sundry Debtors", "ACCOUNTS", cmbtrans.Text)
                Else
                    namevalidate(CMBNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS", cmbtrans.Text)
                End If
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

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If ClientName = "AMAN" Then OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'" Else OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            If Val(txtqty.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 Then TXTMTRS.Text = Format(Val(txtqty.Text.Trim) * Val(TXTCUT.Text.Trim), "0.00")
            If Val(TXTMTRS.Text.Trim) > 0 And Val(TXTRATE.Text.Trim) > 0 Then TXTAMT.Text = Format(Val(TXTMTRS.Text.Trim) * Val(TXTRATE.Text.Trim), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTCUT.Validated, txtqty.Validated, TXTRATE.Validated, TXTMTRS.Validated
        CALC()
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
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans.Text = OBJLEDGER.TEMPNAME
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

    Sub PRINTREPORT(ByVal GREYRECNO As Integer)
        Try
            If MsgBox("Wish to Print GREYRECTRANSPORT...?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJGDN As New GRNDesign
                OBJGDN.MdiParent = MDIMain
                OBJGDN.FRMSTRING = "GREYRECTRANSPORT"
                OBJGDN.WHERECLAUSE = "{GREYRECTRANSPORT.GREYREC_no}=" & Val(GREYRECNO) & " and {GREYRECTRANSPORT.GREYREC_yearid}=" & YearId
                OBJGDN.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPGRNNO)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GREYREC_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "SONU" Then
                cmbtrans.TabStop = True
                txtlrno.TabStop = True
                lrdate.TabStop = True
            End If

            If ClientName = "INDRANI" Or ClientName = "MNIKHIL" Or ClientName = "HRITI" Then
                If ClientName = "INDRANI" Then GBALENO.HeaderText = "SO No" Else TXTBALENO.TabStop = False
                CMBQUALITY.TabStop = False
                cmbqtyunit.TabStop = False
                TXTCUT.TabStop = False
            End If



            If ClientName = "AVIS" Then
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
                txtqty.Text = 1
                GRATE.ReadOnly = False
                CMBDESIGN.TabStop = False
                cmbcolor.TabStop = False
            End If


            cmbqtyunit.Text = "Mtrs"


            If ClientName = "RMANILAL" Then TXTCUT.TabStop = False


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

            If ClientName = "SOFTAS" Or ClientName = "SHREENAKODA" Then
                cmdselectPO.TabStop = False
                CMBQUALITY.TabStop = False
                TXTBALENO.TabStop = False
                TXTCUT.TabStop = False
            End If

            If ClientName = "SNCM" Then
                txtlrno.BackColor = Color.LemonChiffon
            End If

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
                Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEM_ID ", " AND DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' AND DESIGN_YEARID =  " & YearId)
                If DT.Rows.Count > 0 Then
                    If (ClientName = "AVIS" Or ClientName = "KRISHNA" Or ClientName = "NTC") Then cmbitemname.Text = DT.Rows(0).Item("ITEMNAME")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtgrnno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTGREYRECNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub txtgrnno_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTGREYRECNO.Validating
        Try
            If Val(TXTGREYRECNO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(GREYRECTRANSPORT.GREYREC_NO,0)  AS GRNNO", "", " GREYRECTRANSPORT ", "  AND GREYRECTRANSPORT.GREYREC_NO=" & Val(TXTGREYRECNO.Text.Trim) & " AND GREYRECTRANSPORT.GREYREC_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Entry No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SENDWHATSAPP(TEMPGRNNO)
            DT = OBJCMN.Execute_Any_String("UPDATE GREYRECTRANSPORT SET GREYREC_SENDWHATSAPP = 1 WHERE GREYREC_NO = " & TEMPGRNNO & " AND GREYREC_YEARID = " & YearId, "", "")
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
            OBJGRN.FRMSTRING = "GREYRECTRANSPORT"
            OBJGRN.DIRECTMAIL = False
            OBJGRN.DIRECTWHATSAPP = True
            OBJGRN.PRINTSETTING = PRINTDIALOG
            OBJGRN.WHERECLAUSE = "{GREYRECTRANSPORT.GREYREC_no}=" & Val(GRNNO) & " and {GREYRECTRANSPORT.GREYREC_yearid}=" & YearId
            OBJGRN.GRNNO = Val(GRNNO)
            OBJGRN.NOOFCOPIES = 1
            OBJGRN.Show()
            OBJGRN.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\GREYREC_" & Val(GRNNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add("GREYREC_" & Val(GRNNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress
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
            If CHALLANDATE.Text.Trim <> "__/__/____" And ClientName = "MOHATUL" Then
                GREYRECDATE.Text = CHALLANDATE.Text
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTAMT_Validated(sender As Object, e As EventArgs) Handles TXTAMT.Validated
        Try
            If cmbitemname.Text.Trim <> "" And Val(txtqty.Text.Trim) > 0 And cmbqtyunit.Text.Trim <> "" And Val(TXTMTRS.Text.Trim) > 0 Then fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECDATE_GotFocus(sender As Object, e As EventArgs) Handles GREYRECDATE.GotFocus
        GREYRECDATE.SelectionStart = 0
    End Sub

    Private Sub CHALLANDATE_GotFocus(sender As Object, e As EventArgs) Handles CHALLANDATE.GotFocus
        CHALLANDATE.SelectionStart = 0
    End Sub

    Private Sub CMBAGENT_Enter(sender As Object, e As EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillagentledger(CMBAGENT, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(sender As Object, e As CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class