
Imports BL
Imports System.IO
Imports System.Net
Imports System.ComponentModel

Public Class JobOut

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Public TEMPJONO As Integer          'used for editing
    Public EDIT As Boolean          'used for editing
    Dim TEMPROW As Integer
    Dim TEMPUPLOADROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer
    Dim TEMPMTRS As Double = 0.0
    Dim PARTYCHALLANNO As String
    Public SELECTIONFORMULA As String = ""


    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        EP.Clear()
        TXTJONO.Clear()
        CHKLOTREADY.CheckState = CheckState.Unchecked

        If ALLOWMANUALJONO = True Then
            TXTJONO.ReadOnly = False
            TXTJONO.BackColor = Color.LemonChiffon
        Else
            TXTJONO.ReadOnly = True
            TXTJONO.BackColor = Color.Linen
        End If

        TXTTYPEJONO.Clear()
        If CMBTYPE.Items.Count > 0 Then
            CMBTYPE.Enabled = True
            CMBTYPE.SelectedIndex = 0
            GETMAXTYPEJONO()
        End If

        CMBNAME.Text = ""
        CMBPARTYNAME.Text = ""
        TXTCHALLANNO.Clear()
        CMBPROCESS.Text = ""
        If USERGODOWN <> "" Then CMBGODOWN.Text = USERGODOWN Else CMBGODOWN.Text = ""

        txtDeliveryadd.Clear()
        TXTADD.Clear()
        JODATE.Text = Now.Date
        tstxtbillno.Clear()
        CMBPACKING.Text = ""
        TXTMOBILENO.Clear()
        TXTAVGWT.Clear()

        cmbtrans.Text = ""
        CMBDISPATCHFROM.Text = ""

        txtlrno.Clear()
        lrdate.Value = Now.Date
        txtremarks.Clear()
        txtwidth.Clear()

        txtuploadsrno.Clear()
        txtuploadremarks.Clear()
        gridupload.RowCount = 0
        txtimgpath.Clear()
        TXTNEWIMGPATH.Clear()
        TXTFILENAME.Clear()
        PBSoftCopy.ImageLocation = ""
        gridupload.RowCount = 0

        CMDSELECTSTOCK.Enabled = True
        LBLCLOSED.Visible = False
        lbllocked.Visible = False
        LBLGATEPASSDONE.Visible = False
        PBlock.Visible = False



        LBLTOTALMTRS.Text = 0.0
        LBLTOTALPCS.Text = 0.0
        LBLTOTALAMOUNT.Text = 0.0
        TXTBARCODE.Clear()
        TXTVEHICLENO.Clear()
        TXTEWAYBILLNO.Clear()
        TXTBALENUMBER.Clear()
        GRIDJO.RowCount = 0

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False
        getmaxno()

        TXTLOTNO.Clear()
        GRIDLOT.RowCount = 0
        txtuploadsrno.Text = 1


        txtsrno.Clear()
        CMBPIECETYPE.Text = "FRESH"
        cmbitemname.Text = ""
        CMBQUALITY.Text = ""
        TXTBALENO.Clear()
        CMBDESIGN.Text = ""
        If CMPCITYNAME <> "" Then CMBFROMCITY.Text = CMPCITYNAME Else CMBFROMCITY.Text = ""
        CMBTOCITY.Text = ""
        CMBPACKING.Text = ""
        cmbcolor.Text = ""
        TXTCUT.Clear()
        TXTDESCRIPTION.Clear()
        TXTPCS.Clear()
        TXTGRIDLOT.Clear()
        TXTMTRS.Clear()
        TXTRATE.Clear()
        LBLAVGRATE.Text = 0.0
        CMBPER.Text = "Mtrs"
        TXTAMOUNT.Clear()
        lbltotalwt.Text = 0.0
        LBLWHATSAPP.Visible = False

    End Sub

    Sub TOTAL()
        Try
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALPCS.Text = 0.0
            lbltotalwt.Text = 0.0
            LBLTOTALAMOUNT.Text = 0.0
            LBLAVGRATE.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDJO.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    If ROW.Cells(gcut.Index).EditedFormattedValue > 0 Then ROW.Cells(GMTRS.Index).Value = ROW.Cells(GPCS.Index).EditedFormattedValue * ROW.Cells(gcut.Index).EditedFormattedValue
                    LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(ROW.Cells(GPCS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    If ROW.Cells(GPER.Index).EditedFormattedValue = "Mtrs" Then
                        If ROW.Cells(GRATE.Index).EditedFormattedValue > 0 Then ROW.Cells(GAMOUNT.Index).Value = Format((Val(ROW.Cells(GRATE.Index).EditedFormattedValue) * Val(ROW.Cells(GMTRS.Index).EditedFormattedValue)), "0.00")
                    Else
                        If ROW.Cells(GRATE.Index).EditedFormattedValue > 0 Then ROW.Cells(GAMOUNT.Index).Value = Format((Val(ROW.Cells(GRATE.Index).EditedFormattedValue) * Val(ROW.Cells(GPCS.Index).EditedFormattedValue)), "0.00")
                    End If
                    lbltotalwt.Text = Format(Val(lbltotalwt.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALAMOUNT.Text = Format(Val(LBLTOTALAMOUNT.Text) + Val(ROW.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
                    LBLAVGRATE.Text = Format(Val(LBLAVGRATE.Text) + Val(ROW.Cells(GRATE.Index).EditedFormattedValue) / GRIDJO.Rows.Count, "0.00")

                End If
            Next
            BALECOUNT()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub BALECOUNT()
        Try
            Dim TEMPBALES As Integer = 0
            Dim dic As New Dictionary(Of String, Integer)()
            Dim cellValue As String
            For i = 0 To GRIDJO.Rows.Count - 1
                If Not GRIDJO.Rows(i).IsNewRow Then
                    cellValue = GRIDJO(GBALENO.Index, i).EditedFormattedValue.ToString()
                    If cellValue <> "" Then
                        If Not dic.ContainsKey(cellValue) Then
                            dic.Add(cellValue, 1)
                        Else
                            dic(cellValue) += 1
                        End If
                    End If
                End If
            Next
            TEMPBALES = Val(dic.Count)
            If ClientName = "SNCM" Then TXTBALENUMBER.Text = Val(TEMPBALES)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(JO_no),0) + 1 ", " JOBOUT ", " AND JO_cmpid=" & CmpId & " and JO_locationid=" & Locationid & " and JO_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTJONO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True

            If CMBGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBGODOWN, " Please Fill Godown")
                bln = False
            End If

            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, " Please Fill Name")
                bln = False
            End If

            If CMBPROCESS.Text.Trim.Length = 0 Then
                EP.SetError(CMBPROCESS, " Select Process Name")
                bln = False
            End If

            If LBLCLOSED.Visible = True Then
                EP.SetError(LBLCLOSED, " Issue Closed")
                bln = False
            End If

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, " Inward Done, Delete Inward First")
                bln = False
            End If

            If LBLGATEPASSDONE.Visible = True Then
                EP.SetError(LBLGATEPASSDONE, " Gate Pass Made, Delete Gate Pass First")
                bln = False
            End If

            If GRIDJO.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If TXTEWAYBILLNO.Text.Trim <> "" And EDIT = True And ClientName = "YASHVI" Then
                'NEED TO CHECK FROM DATABASE WHETHER THERE IS EWAYBILL NO IN DATABASE OR NOT, IF NOT PRESENT IN DATABASE THEN DONT VALIDATE
                'COZ IF THERE IS NO EWAYBILL NO IN DATABASE THEN USER CAN UPDATE THE EWAYBILLNO
                DT = OBJCMN.SEARCH("JO_EWAYBILLNO AS EWAYBILLNO", "", " JOBOUT ", " AND JO_NO = " & Val(TEMPJONO) & " AND JO_YEARID = " & YearId)
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("EWAYBILLNO") <> "" Then
                    EP.SetError(TXTJONO, "Unable To Modify Job Out, Eway Bill already Generated")
                    bln = False
                End If
            End If


            If TXTCHALLANNO.Text.Trim <> "" And ClientName <> "RADHA" Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(TXTCHALLANNO.Text.Trim)) Then
                    'for search
                    DT = OBJCMN.SEARCH(" JO_challanno, LEDGERS.ACC_cmpname", "", " JOBOUT inner join LEDGERS on LEDGERS.ACC_id = JO_ledgerid ", " and JO_challanno = '" & TXTCHALLANNO.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & CMBNAME.Text.Trim & "' AND JO_YEARID =" & YearId)
                    If DT.Rows.Count > 0 Then
                        EP.SetError(TXTCHALLANNO, "Challan No. Already Exists")
                        bln = False
                    End If
                End If
            End If




            If JODATE.Text = "__/__/____" Then
                EP.SetError(JODATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(JODATE.Text) Then
                    EP.SetError(JODATE, "Date not in Accounting Year")
                    bln = False
                End If

                If Convert.ToDateTime(JODATE.Text).Date < JOBLOCKDATE.Date Then
                    EP.SetError(JODATE, "Date is Blocked, Please make entries after " & Format(JOBLOCKDATE.Date, "dd/MM/yyyy"))
                    bln = False
                End If
            End If

            If Val(TXTJONO.Text.Trim) = 0 Then
                EP.SetError(TXTJONO, "Enter Job Out No")
                bln = False
            End If


            If ClientName = "INDRAPUJAFABRICS" And Val(TXTBALENUMBER.Text.Trim) = 0 Then TXTBALENUMBER.Text = 1


            If ALLOWMANUALJONO = True Then
                If TXTJONO.Text <> "" And CMBNAME.Text.Trim <> "" And EDIT = False Then
                    Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(JOBOUT.JO_NO,0)  AS JONO", "", " JOBOUT ", "  AND JOBOUT.JO_NO=" & TXTJONO.Text.Trim & " AND JOBOUT.JO_CMPID = " & CmpId & " AND JOBOUT.JO_LOCATIONID = " & Locationid & " AND JOBOUT.JO_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        EP.SetError(TXTJONO, "Job Out No Already Exist")
                        bln = False
                    End If
                End If
            End If

            If ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                If CMBTYPE.Text.Trim = "" Then
                    EP.SetError(CMBTYPE, " Please Select Job Out Type")
                    bln = False
                End If

                If Val(TXTTYPEJONO.Text.Trim) = 0 Then
                    EP.SetError(CMBTYPE, " Please Select Job Out Type")
                    bln = False
                End If
            End If



            'CHECK WHETHER BARCODE IS USED OR NOT
            If EDIT = False And ALLOWBARCODEPRINT = True And CHECKBARCODEERRORVALID = True Then
                For Each ROW As DataGridViewRow In GRIDJO.Rows
                    If ROW.Cells(GBARCODE.Index).Value <> "" Then
                        Dim DT1 As DataTable = OBJCMN.SEARCH("*", "", "OUTBARCODESTOCK", " AND BARCODE = '" & ROW.Cells(GBARCODE.Index).Value & "' AND YEARID = " & YearId)
                        If DT1.Rows.Count > 0 Then
                            EP.SetError(CMBNAME, "Barcode Already Used")
                            bln = False
                        End If
                    End If
                Next
            End If



            If TXTLOTNO.Text.Trim = "" Then
                If ClientName = "VINAYAK" Then
                    TXTLOTNO.Text = Val(txtsrno.Text.Trim) & "/" & Format(AccFrom.Date, "yy") & "-" & Format(AccTo.Date, "yy")
                Else
                    If MsgBox("Lot No is Kept Blank, Please Enter Lot No Or Job Out No in this Column, Wish to Proceed without Lot No?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        EP.SetError(TXTLOTNO, "Please fill Lot No/Entry No")
                        bln = False
                    End If
                End If
            End If

            If Val(TXTBALENUMBER.Text.Trim) = 0 Then TXTBALENUMBER.Text = 1


            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            'WHILE ADDING COLUMN IN JOBOUT DONT FORGET TO ADD SAME COLUMNS IN FORMS GIVEN BELOW
            '1) JOBIN -- DIRECTJOBOUT 
            '2) GRN --- DIRECTJOBOUT
            '3) GREYRECDKNITTING --- DIRECTISSUEJOBBER


            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            If TXTJONO.ReadOnly = False Then
                alParaval.Add(Val(TXTJONO.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(Format(Convert.ToDateTime(JODATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBPROCESS.Text.Trim)
            alParaval.Add(CMBPARTYNAME.Text.Trim)
            alParaval.Add(TXTLOTNO.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(txtlrno.Text.Trim)
            alParaval.Add(lrdate.Value)
            alParaval.Add(TXTVEHICLENO.Text.Trim)
            alParaval.Add(CMBFROMCITY.Text.Trim)
            alParaval.Add(CMBTOCITY.Text.Trim)
            alParaval.Add(CMBPACKING.Text.Trim)
            alParaval.Add(TXTEWAYBILLNO.Text.Trim)
            alParaval.Add(TXTBALENUMBER.Text.Trim)
            alParaval.Add(Val(LBLTOTALPCS.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(Val(lbltotalwt.Text))
            alParaval.Add(Val(LBLAVGRATE.Text))
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CHKLOTREADY.Checked)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)




            Dim gridsrno As String = ""
            Dim PIECETYPE As String = ""
            Dim BALENO As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim DESCRIPTION As String = ""
            Dim CUT As String = ""
            Dim PCS As String = ""
            Dim UNIT As String = ""
            Dim MTRS As String = ""
            Dim WT As String = ""
            Dim RATE As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""

            Dim BARCODE As String = "" 'BARCODE ADDED

            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""

            Dim FRAMES As String = ""
            Dim EMBPRODDONE As String = ""
            Dim GRIDLOTNO As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDJO.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = Val(row.Cells(GSRNO.Index).Value)
                        PIECETYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = row.Cells(GBALENO.Index).Value.ToString Else BALENO = ""
                        ITEMNAME = row.Cells(GMERCHANT.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(GCOLOR.Index).Value.ToString

                        If row.Cells(GDESCRIPTION.Index).Value <> Nothing Then DESCRIPTION = row.Cells(GDESCRIPTION.Index).Value.ToString Else DESCRIPTION = ""

                        If CHALLANCUTWHEN1 = True Then
                            If Val(row.Cells(gcut.Index).Value) = 0 And Val(row.Cells(GPCS.Index).Value) = 1 Then CUT = Format(Val(row.Cells(GMTRS.Index).Value), "0.00") Else CUT = Val(row.Cells(gcut.Index).Value)
                        ElseIf CHALLANCUT = True Then
                            CUT = Val(row.Cells(gcut.Index).Value)
                        Else
                            If Val(row.Cells(gcut.Index).Value) = 0 Then CUT = Format(Val(row.Cells(GMTRS.Index).Value) / Val(row.Cells(GPCS.Index).Value), "0.00") Else CUT = Val(row.Cells(gcut.Index).Value)
                        End If

                        PCS = Val(row.Cells(GPCS.Index).Value)
                        UNIT = row.Cells(GUNIT.Index).Value
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        WT = Val(row.Cells(GWT.Index).Value)
                        RATE = Val(row.Cells(GRATE.Index).Value)
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        OUTPCS = Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = Val(row.Cells(GOUTMTRS.Index).Value)
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString
                        FRAMES = Val(row.Cells(GFRAMES.Index).Value)
                        EMBPRODDONE = row.Cells(GEMBPRODDONE.Index).Value
                        GRIDLOTNO = row.Cells(GGRIDLOT.Index).Value.ToString
                        PER = row.Cells(GPER.Index).Value
                        AMOUNT = row.Cells(GAMOUNT.Index).Value

                    Else
                        gridsrno = gridsrno & "|" & Val(row.Cells(GSRNO.Index).Value)
                        PIECETYPE = PIECETYPE & "|" & row.Cells(GPIECETYPE.Index).Value.ToString
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString Else BALENO = BALENO & "|" & ""
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GMERCHANT.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GCOLOR.Index).Value.ToString


                        If row.Cells(GDESCRIPTION.Index).Value <> Nothing Then DESCRIPTION = DESCRIPTION & "|" & row.Cells(GDESCRIPTION.Index).Value.ToString Else DESCRIPTION = DESCRIPTION & "|" & ""

                        If CHALLANCUTWHEN1 = True Then
                            If Val(row.Cells(gcut.Index).Value) = 0 And Val(row.Cells(GPCS.Index).Value) = 1 Then CUT = CUT & "|" & Format(Val(row.Cells(GMTRS.Index).Value), "0.00") Else CUT = CUT & "|" & Val(row.Cells(gcut.Index).Value)
                        ElseIf CHALLANCUT = True Then
                            CUT = CUT & "|" & Val(row.Cells(gcut.Index).Value)
                        Else
                            If Val(row.Cells(gcut.Index).Value) = 0 Then CUT = CUT & "|" & Format(Val(row.Cells(GMTRS.Index).Value) / Val(row.Cells(GPCS.Index).Value), "0.00") Else CUT = CUT & "|" & Val(row.Cells(gcut.Index).Value)
                        End If

                        PCS = PCS & "|" & Val(row.Cells(GPCS.Index).Value)
                        UNIT = UNIT & "|" & row.Cells(GUNIT.Index).Value
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        RATE = RATE & "|" & Val(row.Cells(GRATE.Index).Value)
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        OUTPCS = OUTPCS & "|" & Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = OUTMTRS & "|" & Val(row.Cells(GOUTMTRS.Index).Value)
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString

                        FRAMES = FRAMES & "|" & Val(row.Cells(GFRAMES.Index).Value)
                        EMBPRODDONE = EMBPRODDONE & "|" & row.Cells(GEMBPRODDONE.Index).Value
                        GRIDLOTNO = GRIDLOTNO & "|" & row.Cells(GGRIDLOT.Index).Value.ToString
                        PER = PER & "|" & row.Cells(GPER.Index).Value
                        AMOUNT = AMOUNT & "|" & row.Cells(GAMOUNT.Index).Value

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
            alParaval.Add(DESCRIPTION)
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
            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(Val(TXTTYPEJONO.Text.Trim))
            alParaval.Add(LBLTOTALAMOUNT.Text.Trim)

            alParaval.Add("") 'VEHICLENO
            alParaval.Add("")  'FROMCITY
            alParaval.Add("")     'TOCITY
            alParaval.Add("")     'PACKING
            alParaval.Add("")   'EWAYBILLNO
            alParaval.Add(Val(TXTAVGWT.Text.Trim))
            alParaval.Add(CMBDISPATCHFROM.Text.Trim)
            alParaval.Add(txtwidth.Text.Trim)

            Dim objCUTTING As New ClsCuttingIssue()
            objCUTTING.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = objCUTTING.SAVE()
                MsgBox("Details Added")

                If ClientName = "SVS" Then
                    If MsgBox("Wish to Stock Reco Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        RECOSAVE()
                    End If
                End If

                TXTJONO.Text = DTTABLE.Rows(0).Item(0)
                If ClientName <> "MJFABRIC" Then PRINTREPORT(DTTABLE.Rows(0).Item(0))

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                alParaval.Add(TEMPJONO)
                IntResult = objCUTTING.UPDATE()
                MsgBox("Details Updated")
                If ClientName <> "SNCM" Then PRINTREPORT(TEMPJONO)
                EDIT = False
            End If

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
            JODATE.Focus()




        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub RECOSAVE()
        Try

            Dim alParaval As New ArrayList

            alParaval.Add(0)    'manualNO
            alParaval.Add(Format(Convert.ToDateTime(JODATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBGODOWN.Text.Trim)


            alParaval.Add("")    'TRANS
            alParaval.Add("")    'CHALLAN

            alParaval.Add(Val(LBLTOTALPCS.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(0)    'INPCS
            alParaval.Add(0)    'INMTRS


            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)


            Dim gridsrno As String = ""
            Dim PIECETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim LOTNO As String = ""
            Dim PCS As String = ""
            Dim MTRS As String = ""
            'Dim RATE As String = ""

            Dim BARCODE As String = "" 'BARCODE ADDED
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""
            Dim RATE As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""


            Dim PRESENT As Boolean = False
            For Each row As Windows.Forms.DataGridViewRow In GRIDJO.Rows
                Dim objclscommon As New ClsCommonMaster
                Dim dt1 As DataTable = objclscommon.search(" ISNULL(MTRS,0) AS MTRS ", "", " BARCODESTOCK", " AND GODOWN='" & CMBGODOWN.Text.Trim & "' AND FROMNO= " & Val(row.Cells(GFROMNO.Index).Value) & " AND FROMSRNO= " & Val(row.Cells(GFROMSRNO.Index).Value) & " AND TYPE='" & row.Cells(GFROMTYPE.Index).Value & "' AND Yearid = " & YearId)
                If dt1.Rows.Count > 0 Then
                    If Val(dt1.Rows(0).Item(0)) <= 3 Then
                        PRESENT = True
                        TEMPMTRS = Val(dt1.Rows(0).Item(0))
                        If gridsrno = "" Then
                            gridsrno = row.Cells(GSRNO.Index).Value.ToString
                            PIECETYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                            ITEMNAME = row.Cells(GMERCHANT.Index).Value.ToString
                            QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                            DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                            COLOR = row.Cells(GCOLOR.Index).Value.ToString
                            LOTNO = row.Cells(GGRIDLOT.Index).Value.ToString
                            PCS = row.Cells(GPCS.Index).Value.ToString
                            MTRS = TEMPMTRS
                            RATE = row.Cells(GRATE.Index).Value
                            PER = row.Cells(GPER.Index).Value
                            AMOUNT = row.Cells(GAMOUNT.Index).Value
                            BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                            FROMNO = row.Cells(GFROMNO.Index).Value.ToString
                            FROMSRNO = row.Cells(GFROMSRNO.Index).Value.ToString
                            FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString

                        Else
                            gridsrno = gridsrno & "," & row.Cells(GSRNO.Index).Value.ToString
                            PIECETYPE = PIECETYPE & "," & row.Cells(GPIECETYPE.Index).Value.ToString
                            ITEMNAME = ITEMNAME & "," & row.Cells(GMERCHANT.Index).Value.ToString
                            QUALITY = QUALITY & "," & row.Cells(GQUALITY.Index).Value.ToString
                            DESIGN = DESIGN & "," & row.Cells(GDESIGN.Index).Value.ToString
                            COLOR = COLOR & "," & row.Cells(GCOLOR.Index).Value.ToString
                            LOTNO = LOTNO & "," & row.Cells(GGRIDLOT.Index).Value.ToString
                            PCS = PCS & "," & row.Cells(GPCS.Index).Value.ToString
                            MTRS = MTRS & "," & TEMPMTRS
                            RATE = RATE & "|" & row.Cells(GRATE.Index).Value
                            PER = PER & "|" & row.Cells(GPER.Index).Value
                            AMOUNT = AMOUNT & "|" & row.Cells(GAMOUNT.Index).Value
                            BARCODE = BARCODE & "," & row.Cells(GBARCODE.Index).Value.ToString
                            FROMNO = FROMNO & "," & row.Cells(GFROMNO.Index).Value.ToString
                            FROMSRNO = FROMSRNO & "," & row.Cells(GFROMSRNO.Index).Value.ToString
                            FROMTYPE = FROMTYPE & "," & row.Cells(GFROMTYPE.Index).Value.ToString

                        End If

                    End If
                End If
            Next


            alParaval.Add(gridsrno)
            alParaval.Add(PIECETYPE)
            alParaval.Add(ITEMNAME)
            alParaval.Add(QUALITY)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(LOTNO)
            alParaval.Add(PCS)
            alParaval.Add(MTRS)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMOUNT)

            alParaval.Add(BARCODE)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)



            Dim INGRIDSRNO As String = ""
            Dim INPIECETYPE As String = ""
            Dim INITEMNAME As String = ""
            Dim INQUALITY As String = ""
            Dim INBALENO As String = ""
            Dim INGRIDDESC As String = ""
            Dim INLOTNO As String = ""
            Dim INDESIGN As String = ""
            Dim INCOLOR As String = ""
            Dim INCUT As String = ""
            Dim INPCS As String = ""
            Dim INQTYUNIT As String = ""
            Dim INMTRS As String = ""
            Dim INRACK As String = ""
            Dim INSHELF As String = ""
            Dim INBARCODE As String = ""
            Dim INDONE As String = ""
            Dim INOUTPCS As String = ""
            Dim INOUTMTRS As String = ""

            alParaval.Add(INGRIDSRNO)
            alParaval.Add(INPIECETYPE)
            alParaval.Add(INITEMNAME)
            alParaval.Add(INQUALITY)
            alParaval.Add(INBALENO)
            alParaval.Add(INGRIDDESC)
            alParaval.Add(INLOTNO)
            alParaval.Add(INDESIGN)
            alParaval.Add(INCOLOR)
            alParaval.Add(INCUT)
            alParaval.Add(INPCS)
            alParaval.Add(INQTYUNIT)
            alParaval.Add(INMTRS)
            alParaval.Add(0)      'INMTRS
            alParaval.Add("Mtrs")      'INPER
            alParaval.Add(0)      'INAMOUNT
            alParaval.Add(INRACK)
            alParaval.Add(INSHELF)
            alParaval.Add(INBARCODE)
            alParaval.Add(INDONE)
            alParaval.Add(INOUTPCS)
            alParaval.Add(INOUTMTRS)

            alParaval.Add("")   'NAME


            alParaval.Add(LBLTOTALAMOUNT.Text.Trim)
            alParaval.Add(0)
            alParaval.Add(LBLAVGRATE.Text.Trim)
            Dim objclsPurord As New ClsStockAdjustment()
            objclsPurord.alParaval = alParaval

            If PRESENT = True Then Dim DT As DataTable = objclsPurord.SAVE()
            MsgBox("Reco done Successfully!")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JOBOUT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If ERRORVALID() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.Alt = True And (e.KeyCode = Windows.Forms.Keys.D1) Then
            TabControl1.Focus()
            TabControl1.SelectedIndex = (0)
        ElseIf e.Alt = True And (e.KeyCode = Windows.Forms.Keys.D2) Then
            TabControl1.SelectedIndex = (1)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        ElseIf e.KeyCode = Keys.F5 Then
            GRIDJO.Focus()
        ElseIf e.KeyCode = Keys.OemPipe Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            toolprevious_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
        ElseIf e.KeyCode = Keys.F5 Then     'grid focus
            YarnRecd.Focus()
        ElseIf e.KeyCode = Keys.P And e.Alt = True Then
            Call PrintToolStripButton_Click(sender, e)
        End If
    End Sub

    Private Sub JOBOUT_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'JOB OUT'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            'If ClientName = "MANINATH" Or ClientName = "PURVITEX" Or ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "RUCHITA" Or ClientName = "SHREENAKODA" Or ClientName = "RADHA" Or ClientName = "VINAYAK" Then
            '    ALLOWMANUALJONO = True
            'End If

            fillcmb()
            CLEAR()

            If ClientName = "SVS" Then GPCS.ReadOnly = True

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objJO As New ClsCuttingIssue()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPJONO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)
                objJO.alParaval = ALPARAVAL
                Dim dttable As DataTable = objJO.SELECTJO()

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTJONO.Text = TEMPJONO
                        TXTJONO.ReadOnly = True

                        CMBTYPE.Text = Convert.ToString(dr("JOBOUTTYPE").ToString)
                        CMBTYPE.Enabled = False
                        TXTTYPEJONO.Text = Val(dr("TYPEJOBOUTNO"))

                        JODATE.Text = Format(Convert.ToDateTime(dr("JODATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                        CMBPROCESS.Text = Convert.ToString(dr("PROCESS").ToString)
                        CMBPARTYNAME.Text = Convert.ToString(dr("PARTYNAME").ToString)
                        CMBGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                        TXTCHALLANNO.Text = dr("CHALLANNO")
                        PARTYCHALLANNO = TXTCHALLANNO.Text.Trim

                        cmbtrans.Text = dr("TRANSNAME").ToString
                        txtlrno.Text = dr("LRNO").ToString
                        lrdate.Text = Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy")
                        TXTVEHICLENO.Text = dr("VEHICLENO")
                        CMBFROMCITY.Text = Convert.ToString(dr("FROMCITY"))
                        CMBTOCITY.Text = Convert.ToString(dr("TOCITY"))
                        CMBPACKING.Text = Convert.ToString(dr("PACKING"))
                        TXTAVGWT.Text = Val(dr("AVGWT"))

                        TXTEWAYBILLNO.Text = dr("EWAYBILLNO")
                        TXTBALENUMBER.Text = dr("BALENUMBER")
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        'CHKLOTREADY.Checked = Convert.ToBoolean(dr("LOTREADY"))
                        TXTLOTNO.Text = dr("LOTNO").ToString
                        LBLAVGRATE.Text = Val(dr("TOTALRATE"))
                        lbltotalwt.Text = Val(dr("TOTALWT"))
                        CMBDISPATCHFROM.Text = Convert.ToString(dr("DISPATCHFROM"))

                        If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True

                        'Item Grid


                        GRIDJO.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE").ToString, dr("BALENO").ToString, dr("ITEM").ToString, dr("QUALITY").ToString, dr("DESIGN").ToString, dr("COLOR").ToString, dr("GRIDLOTNO"), dr("DESCRIPTION").ToString, Format(Val(dr("CUT")), "0.00"), Format(dr("PCS"), "0.00"), dr("UNIT"), Format(dr("MTRS"), "0.00"), Format(dr("WT"), "0.00"), Val(dr("RATE")), dr("PER").ToString, Format(Val(dr("AMOUNT")), "0.00"), dr("BARCODE").ToString, Format(dr("OUTPCS"), "0.00"), Format(dr("OUTMTRS"), "0.00"), dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"), Val(dr("FRAMES")), dr("EMBPRODDONE"))

                        If Convert.ToDecimal(dr("RECDMTRS")) > 0 Or Convert.ToBoolean(dr("LOTCOMPLETED")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        If Convert.ToBoolean(dr("CLOSE")) = True Then
                            LBLCLOSED.Visible = True
                            PBlock.Visible = True
                        End If

                        If Convert.ToBoolean(dr("GPDONE")) = True Then
                            LBLGATEPASSDONE.Visible = True
                            PBlock.Visible = True
                        End If
                        txtwidth.Text = Val(dr("WIDTH"))

                    Next


                    Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.SEARCH(" JO_GRIDSRNO AS GRIDSRNO, JO_REMARKS AS REMARKS, JO_NAME AS NAME, JO_IMGPATH AS IMGPATH, JO_NEWIMGPATH AS NEWIMGPATH", "", " JOBOUT_UPLOAD", " AND JO_NO = " & TEMPJONO & " AND JO_CMPID = " & CmpId & " AND JO_LOCATIONID = " & Locationid & " AND JO_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), DTR("IMGPATH"), DTR("NEWIMGPATH"))
                        Next
                    End If
                    TOTAL()
                    GRIDJO.FirstDisplayedScrollingRowIndex = GRIDJO.RowCount - 1
                    chkchange.CheckState = CheckState.Checked
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Sub fillcmb()
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT' ")
            If CMBFROMCITY.Text.Trim = "" Then fillCITY(CMBFROMCITY, EDIT)
            If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, EDIT)
            If CMBPACKING.Text.Trim = "" Then filljobbername(CMBPACKING, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
            If CMBPROCESS.Text.Trim = "" Then FILLPROCESS(CMBPROCESS)
            FILLJOBOUTTYPE(CMBTYPE)
            If CMBPIECETYPE.Text.Trim = "" Then fillPIECETYPE(CMBPIECETYPE)
            fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            fillQUALITY(CMBQUALITY, EDIT)
            FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
            If CMBDISPATCHFROM.Text.Trim = "" Then FILLNAME(CMBDISPATCHFROM, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
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

            Dim OBJEMB As New JobOutDetails
            OBJEMB.MdiParent = MDIMain
            OBJEMB.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtrans.KeyDown
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

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then NAMEVALIDATE(cmbtrans, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "SUNDRY CREDITORS")
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

    Private Sub CMDSELECTDO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            Dim DTJO As New DataTable
            Dim OBJSELECTGDN As New SelectStockGDN
            OBJSELECTGDN.GODOWN = CMBGODOWN.Text.Trim
            If ClientName = "RADHA" And CMBPARTYNAME.Text.Trim <> "" Then OBJSELECTGDN.SELECTIONFORMULA = " AND PURNAME = '" & CMBPARTYNAME.Text.Trim & "'"
            If ALLOWPACKINGSLIP = True And ClientName <> "MARKIN" Then OBJSELECTGDN.FILTER = " AND BARCODE = ''"
            OBJSELECTGDN.ShowDialog()
            DTJO = OBJSELECTGDN.DT
            If DTJO.Rows.Count > 0 Then
                For Each DTROWPS As DataRow In DTJO.Rows

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    For Each ROW As DataGridViewRow In GRIDJO.Rows
                        If DTROWPS("BARCODE") <> "" And LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(DTROWPS("BARCODE")) Then GoTo LINE1
                    Next

                    Dim PER As String = "Mtrs"

                    'FETCH PER FROM ITEMMASTER
                    If ClientName = "SUBHLAXMI" Or ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                        Dim OBJCMN As New ClsCommon
                        Dim DTPER As DataTable = OBJCMN.SEARCH("ISNULL(UNIT_ABBR,'Mtrs') AS UNIT", "", " ITEMMASTER LEFT OUTER JOIN UNITMASTER ON ITEMMASTER.ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEMMASTER.ITEM_NAME = '" & DTROWPS("ITEMNAME") & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                        If DTPER.Rows.Count > 0 AndAlso DTPER.Rows(0).Item("UNIT") = "Pcs" Then PER = "Pcs"
                    End If


                    If ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                        GRIDJO.Rows.Add(0, DTROWPS("PIECETYPE"), DTROWPS("BALENO"), DTROWPS("ITEMNAME"), DTROWPS("QUALITY"), DTROWPS("DESIGNNO"), DTROWPS("COLOR"), DTROWPS("LOTNO"), "", DTROWPS("CUT"), Val(DTROWPS("PCS")), DTROWPS("UNIT"), Format(Val(DTROWPS("MTRS")), "0.00"), 0, Val(DTROWPS("PURRATE")), PER, 0, DTROWPS("BARCODE"), 0, 0, DTROWPS("FROMNO"), DTROWPS("FROMSRNO"), DTROWPS("TYPE"), 0, 0)
                    Else
                        GRIDJO.Rows.Add(0, DTROWPS("PIECETYPE"), DTROWPS("BALENO"), DTROWPS("ITEMNAME"), DTROWPS("QUALITY"), DTROWPS("DESIGNNO"), DTROWPS("COLOR"), DTROWPS("LOTNO"), "", 0, Val(DTROWPS("PCS")), DTROWPS("UNIT"), Format(Val(DTROWPS("MTRS")), "0.00"), 0, Val(DTROWPS("PURRATE")), PER, 0, DTROWPS("BARCODE"), 0, 0, DTROWPS("FROMNO"), DTROWPS("FROMSRNO"), DTROWPS("TYPE"), 0, 0)
                    End If
                    If ClientName <> "AVIS" Then TXTLOTNO.Text = DTROWPS("LOTNO")
                    If ClientName = "RADHA" Or ClientName = "VINTAGEINDIA" Then TXTCHALLANNO.Text = DTROWPS("CHALLANNO")
                    If ClientName = "SAKARIA" Or ClientName = "CC" Or ClientName = "C3" Or ClientName = "RADHA" Or ClientName = "VINTAGEINDIA" Or ClientName = "SNCM" Then CMBPARTYNAME.Text = DTROWPS("PURNAME")

LINE1:
                Next
                getsrno(GRIDJO)
                TOTAL()
                GRIDJO.FirstDisplayedScrollingRowIndex = GRIDJO.RowCount - 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDJO.RowCount = 0
                TEMPJONO = Val(tstxtbillno.Text)
                If TEMPJONO > 0 Then
                    EDIT = True
                    JOBOUT_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            GRIDJO.Enabled = True

            If GRIDDOUBLECLICK = False Then
                GRIDJO.Rows.Add(Val(txtsrno.Text.Trim), CMBPIECETYPE.Text.Trim, TXTBALENO.Text.Trim, cmbitemname.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, cmbcolor.Text.Trim, TXTGRIDLOT.Text.Trim, TXTDESCRIPTION.Text.Trim, Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTPCS.Text.Trim), "0.00"), "", Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTWT.Text.Trim), "0.00"), Val(TXTRATE.Text.Trim), CMBPER.Text.Trim, Format(Val(TXTAMOUNT.Text.Trim), "0.00"), TXTBARCODE.Text.Trim, 0, 0, 0, 0, "", 0, 0)
                getsrno(GRIDJO)
            ElseIf GRIDDOUBLECLICK = True Then
                GRIDJO.Item(GSRNO.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
                GRIDJO.Item(GPIECETYPE.Index, TEMPROW).Value = CMBPIECETYPE.Text.Trim
                GRIDJO.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
                GRIDJO.Item(GMERCHANT.Index, TEMPROW).Value = cmbitemname.Text.Trim
                GRIDJO.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
                GRIDJO.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
                GRIDJO.Item(GCOLOR.Index, TEMPROW).Value = cmbcolor.Text.Trim
                GRIDJO.Item(GGRIDLOT.Index, TEMPROW).Value = TXTGRIDLOT.Text.Trim
                GRIDJO.Item(GDESCRIPTION.Index, TEMPROW).Value = TXTDESCRIPTION.Text.Trim
                GRIDJO.Item(gcut.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
                GRIDJO.Item(GPCS.Index, TEMPROW).Value = Format(Val(TXTPCS.Text.Trim), "0.00")
                GRIDJO.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
                GRIDJO.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.00")
                GRIDJO.Item(GRATE.Index, TEMPROW).Value = Val(TXTRATE.Text.Trim)
                GRIDJO.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
                GRIDJO.Item(GAMOUNT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")
                GRIDDOUBLECLICK = False
            End If

            TOTAL()

            GRIDJO.FirstDisplayedScrollingRowIndex = GRIDJO.RowCount - 1

            txtsrno.Clear()
            TXTRATE.Clear()
            TXTAMOUNT.Clear()

            TXTBALENO.Clear()
            If ClientName = "VINAYAK" Then
                TXTMTRS.Clear()
            End If



            If GRIDJO.RowCount > 0 Then
                txtsrno.Text = Val(GRIDJO.Rows(GRIDJO.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If
            If ClientName = "SMS" Or ClientName = "VINAYAK" Then TXTMTRS.Focus() Else CMBPIECETYPE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
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

    Sub EDITROW()
        Try
            If GRIDJO.CurrentRow.Index >= 0 And GRIDJO.Item(GSRNO.Index, GRIDJO.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDJO.Item(GSRNO.Index, GRIDJO.CurrentRow.Index).Value.ToString
                CMBPIECETYPE.Text = GRIDJO.Item(GPIECETYPE.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTBALENO.Text = GRIDJO.Item(GBALENO.Index, GRIDJO.CurrentRow.Index).Value.ToString
                cmbitemname.Text = GRIDJO.Item(GMERCHANT.Index, GRIDJO.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDJO.Item(GQUALITY.Index, GRIDJO.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDJO.Item(GDESIGN.Index, GRIDJO.CurrentRow.Index).Value.ToString
                cmbcolor.Text = GRIDJO.Item(GCOLOR.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTGRIDLOT.Text = GRIDJO.Item(GGRIDLOT.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTDESCRIPTION.Text = GRIDJO.Item(GDESCRIPTION.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTCUT.Text = GRIDJO.Item(gcut.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTPCS.Text = GRIDJO.Item(GPCS.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTMTRS.Text = GRIDJO.Item(GMTRS.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTWT.Text = GRIDJO.Item(GWT.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTRATE.Text = Val(GRIDJO.Item(GRATE.Index, GRIDJO.CurrentRow.Index).Value)
                CMBPER.Text = GRIDJO.Item(GPER.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTAMOUNT.Text = Val(GRIDJO.Item(GAMOUNT.Index, GRIDJO.CurrentRow.Index).Value)
                TEMPROW = GRIDJO.CurrentRow.Index
                CMBPIECETYPE.Focus()
            End If
        Catch ex As Exception
            Throw ex
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
        TXTNEWIMGPATH.Text = Application.StartupPath & "\UPLOADDOCS\" & TXTJONO.Text.Trim & txtuploadsrno.Text.Trim & TXTFILENAME.Text.Trim
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

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDJO.RowCount = 0
LINE1:
            TEMPJONO = Val(TXTJONO.Text) - 1
            If TEMPJONO > 0 Then
                EDIT = True
                JOBOUT_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDJO.RowCount = 0 And TEMPJONO > 1 Then
                TXTJONO.Text = TEMPJONO
                GoTo LINE1
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
LINE1:
            TEMPJONO = Val(TXTJONO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTJONO.Text.Trim
            CLEAR()
            If Val(TXTJONO.Text) - 1 >= TEMPJONO Then
                EDIT = True
                JOBOUT_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDJO.RowCount = 0 And TEMPJONO < MAXNO Then
                TXTJONO.Text = TEMPJONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS", cmbtrans.Text)
        Catch ex As Exception
            Throw ex
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

    Private Sub GRIDJO_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDJO.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPJONO)
            PRINTEWB()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub PRINTREPORT(ByVal JONO As Integer)
        Try
            TEMPMSG = MsgBox("Wish to Print Job Out?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                Dim OBJGDN As New GDNDESIGN
                OBJGDN.MdiParent = MDIMain
                If ClientName = "MANSI" Then
                    If MsgBox("Print Job Out for Garments", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJGDN.FRMSTRING = "JOBOUTGARMENT" Else OBJGDN.FRMSTRING = "JOBOUT"
                Else
                    OBJGDN.FRMSTRING = "JOBOUT"
                End If
                If (ClientName <> "TINUMINU" And ClientName <> "SIDDHGIRI" And ClientName <> "SNCM") AndAlso MsgBox("Wish to Print Job Out With GST...?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJGDN.GSTRPT = True
                OBJGDN.FORMULA = "{JOBOUT.JO_NO}=" & Val(JONO) & " and {JOBOUT.JO_yearid}=" & YearId
                OBJGDN.HIDEPCSDETAILS = CHKHIDEPCS.Checked
                OBJGDN.Show()
            End If


            If ClientName = "SNCM" Then
                Dim TEMPMSG2 As Integer = MsgBox("Wish to Print Taka Details?", MsgBoxStyle.YesNo)
                If TEMPMSG2 = vbYes Then

                    Dim OBJCM As New ClsCommon
                    Dim DTINSERT As New DataTable
                    Dim DT As DataTable = OBJCM.Execute_Any_String("DELETE FROM TEMPTAKADETAILS", " ", " WHERE TEMPTAKADETAILS.TAKA_YEARID = " & YearId)
                    DT = OBJCM.SEARCH(" JOBOUT.JO_no AS JONO, JOBOUT_DESC.JO_BALENO AS BALENO, JOBOUT_DESC.JO_MTRS AS MTRS  ", "", " JOBOUT INNER JOIN JOBOUT_DESC ON JOBOUT.JO_no = JOBOUT_DESC.JO_NO AND JOBOUT.JO_yearid = JOBOUT_DESC.JO_YEARID ", " and JOBOUT.JO_NO = " & JONO & " and JOBOUT.JO_yearid =" & YearId & " ORDER BY JOBOUT_DESC.JO_BALENO")
                    Dim TEMPSRNO As Integer = 1
                    Dim TEMPBALE As String = ""
                    For Each DTROWPS As DataRow In DT.Rows
                        If TEMPBALE <> DTROWPS("BALENO") Then
                            TEMPBALE = DTROWPS("BALENO")
                            TEMPSRNO = 1
                        End If
                        DTINSERT = OBJCM.Execute_Any_String(" INSERT INTO TEMPTAKADETAILS VALUES (" & DTROWPS("JONO") & "," & TEMPSRNO & ",'" & DTROWPS("BALENO") & "'," & DTROWPS("MTRS") & "," & CmpId & "," & YearId & ") ", "", "")
                        TEMPSRNO += 1
                    Next

                    Dim OBJGDN As New GDNDESIGN
                    OBJGDN.FORMULA = "{TEMPTAKADETAILS.TAKA_JOBNO}=" & Val(JONO) & " and {TEMPTAKADETAILS.TAKA_yearid}=" & YearId
                    OBJGDN.FRMSTRING = "JOTAKADETAILS"
                    OBJGDN.MdiParent = MDIMain
                    OBJGDN.Show()
                End If
            End If


            If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "MSANCHITKUMAR" Then
                Dim TEMPMSG2 As Integer = MsgBox("Wish to Print Job Out Banner?", MsgBoxStyle.YesNo)
                If TEMPMSG2 = vbYes Then
                    Dim OBJGDN As New GDNDESIGN
                    OBJGDN.FORMULA = "{JOBOUT.JO_NO}=" & Val(JONO) & " and {JOBOUT.JO_yearid}=" & YearId
                    OBJGDN.FRMSTRING = "JOBOUTBANNER"
                    OBJGDN.MdiParent = MDIMain
                    OBJGDN.Show()
                End If
            End If


            If ClientName <> "SOFTAS" And ClientName <> "SIDDHGIRI" And ClientName <> "TINUMINU" And ClientName <> "SNCM" AndAlso MsgBox("Wish to Print Cutting Sheet?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJGDN As New GDNDESIGN
                OBJGDN.MdiParent = MDIMain
                OBJGDN.FRMSTRING = "JOBCUTTING"
                OBJGDN.FORMULA = "{JOBOUT.JO_NO}=" & Val(JONO) & " and {JOBOUT.JO_yearid}=" & YearId
                OBJGDN.HIDEPCSDETAILS = CHKHIDEPCS.Checked
                OBJGDN.Show()
            End If


            If ClientName = "KCRAYON" Then
                If MsgBox("Wish to Print Job Sheet?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim OBJJO As New JobOutDesign
                    OBJJO.MdiParent = MDIMain
                    OBJJO.FRMSTRING = "JOBSHEET"
                    OBJJO.WHERECLAUSE = "{JOBOUT.JO_NO}=" & Val(JONO) & " and {JOBOUT.JO_yearid}=" & YearId
                    OBJJO.Show()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLOSE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLOSE.Click
        Try
            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            TEMPMSG = MsgBox("Wish to Close Job Out?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                TEMPMSG = MsgBox("Are you Sure?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then

                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTJONO.Text)
                    alParaval.Add(1)
                    alParaval.Add(CmpId)
                    alParaval.Add(Locationid)
                    alParaval.Add(YearId)

                    Dim intresult As Integer
                    Dim clsobjjo As New ClsCuttingIssue()
                    clsobjjo.alParaval = alParaval
                    intresult = clsobjjo.CLOSE()
                    MsgBox("Job Out Closed")
                    CLEAR()

                Else
                    Exit Sub
                End If
                Exit Sub
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then

                If ClientName = "SAFFRON" Then
                    If lbllocked.Visible = True Then
                        MsgBox("Entry Locked", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If

                Dim TEMPMSG As Integer = MsgBox("Wish to Delete Job Out?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBJEMB As New ClsCuttingIssue

                ALPARAVAL.Add(TEMPJONO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)
                OBJEMB.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJEMB.Delete()
                MsgBox("Job Out Deleted Succesfully")
                CLEAR()
                EDIT = False
                CMBNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTBARCODE.Validating
        Try
            If TXTBARCODE.Text.Trim <> "" And CHECKBARCODEERRORVALID = True Then
                'CHECKING WHETHER IS IS GONE OUT OR NOT
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("TOP 1 TYPE, FROMNO", "", " OUTBARCODESTOCK ", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "' AND CMPID = " & CmpId & " AND LOCATIONID = " & Locationid & " AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    MsgBox("Barcode Already Used in " & DT.Rows(0).Item("TYPE") & " Sr No " & DT.Rows(0).Item("FROMNO"))
                    TXTBARCODE.Clear()
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
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

    Private Sub CMBPARTYNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "KARAN" Or ClientName = "SUBHLAXMI" Or ClientName = "CC" Or ClientName = "C3" Or ClientName = "SAKARIA" Or ClientName = "SNCM" Then
                FILLNAME(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            Else
                FILLNAME(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBPARTYNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "KARAN" Or ClientName = "CC" Or ClientName = "C3" Or ClientName = "SAKARIA" Or ClientName = "SNCM" Then
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                Else
                    OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                    OBJLEDGER.ShowDialog()
                    If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                    If OBJLEDGER.TEMPNAME <> "" Then CMBPARTYNAME.Text = OBJLEDGER.TEMPNAME
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then
                If ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "KARAN" Or ClientName = "CC" Or ClientName = "C3" Or ClientName = "SAKARIA" Or ClientName = "SNCM" Then NAMEVALIDATE(CMBPARTYNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS", cmbtrans.Text) Else NAMEVALIDATE(CMBPARTYNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'", "SUNDRY DEBTORS", "ACCOUNTS", cmbtrans.Text)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JODATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles JODATE.GotFocus
        JODATE.SelectionStart = 0
    End Sub

    Private Sub JODATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles JODATE.Validating
        Try
            If JODATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(JODATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHALLANNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHALLANNO.Validating
        Try
            If TXTCHALLANNO.Text.Trim.Length > 0 And ClientName <> "RADHA" Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(TXTCHALLANNO.Text.Trim)) Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    Dim dt As DataTable = objclscommon.SEARCH(" JO_challanno, LEDGERS.ACC_cmpname", "", " JOBOUT inner join LEDGERS on LEDGERS.ACC_id = JO_ledgerid ", " and JO_challanno = '" & TXTCHALLANNO.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & CMBNAME.Text.Trim & "' AND JO_YEARID =" & YearId)
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

    Private Sub CMBPROCESS_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPROCESS.Enter
        Try
            If CMBPROCESS.Text.Trim = "" Then FILLPROCESS(CMBPROCESS)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPROCESS.Validating
        Try
            If CMBPROCESS.Text.Trim <> "" Then PROCESSVALIDATE(CMBPROCESS, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTJONO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTJONO.KeyPress, tstxtbillno.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTJONO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTJONO.Validating
        Try
            If Val(TXTJONO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(JOBOUT.JO_NO,0)  AS JONO", "", " JOBOUT ", "  AND JOBOUT.JO_NO=" & TXTJONO.Text.Trim & " AND JOBOUT.JO_CMPID = " & CmpId & " AND JOBOUT.JO_LOCATIONID = " & Locationid & " AND JOBOUT.JO_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Job Out No Already Exists")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAXTYPEJONO()
        Try
            'GET MAX NO WITH RESPECT TO SELECTED JOBOUTTYPE
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("isnull(max(JO_TYPENO),0) + 1", "", "JOBOUT INNER JOIN  JOBOUTTYPEMASTER ON JO_TYPEID = JOTYPE_ID", " AND JOTYPE_NAME = '" & CMBTYPE.Text.Trim & "' AND JO_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then TXTTYPEJONO.Text = Val(DT.Rows(0).Item(0))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JobOut_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ITEMCOSTCENTRE = True Then
                GRATE.ReadOnly = True
                GPER.ReadOnly = True
                GAMOUNT.ReadOnly = True
            End If
            If ClientName = "DRDRAPES" Then LBLCHALLAN.Text = "So No"
            If ClientName = "MANSI" Then GQUALITY.HeaderText = "Size"
            If ClientName = "KARAN" Then
                LBLPARTYNAME.Text = "Weaver Name"
                GDESCRIPTION.ReadOnly = False
            End If

            'NO NEED OF THIS CODE AS WE HAVE ADDED AMOUNT COLUMN
            'If ClientName <> "SUBHLAXMI" And ClientName <> "SANGHVI" Then GRATE.HeaderText = "Amount"


            If ClientName = "SONU" Then GDESCRIPTION.ReadOnly = False

            If ClientName = "MJFABRIC" Or ClientName = "MANAS" Then
                CMBPARTYNAME.TabStop = False
                TXTBARCODE.TabStop = False
                TXTMOBILENO.TabStop = False
                TXTLOTNO.TabStop = False
                TXTCHALLANNO.TabStop = False
                TXTEWAYBILLNO.TabStop = False
                TXTBALENUMBER.TabStop = False
                txtsrno.TabStop = False
                TXTBALENO.TabStop = False
                CMBQUALITY.TabStop = False
                CMBDESIGN.TabStop = False
                cmbcolor.TabStop = False
                TXTDESCRIPTION.TabStop = False
                TXTCUT.TabStop = False
                TXTRATE.TabStop = True
                CMDSELECTSTOCK.TabStop = False
            End If

            If ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "NVAHAN" Or ClientName = "KREEVE" Or ClientName = "MANAS" Or ClientName = "MOHATUL" Or ClientName = "SMS" Or ClientName = "SHREEVALLABH" Or ClientName = "RAJDEEP" Or ClientName = "VINAYAK" Or ClientName = "MVIKASKUMAR" Or ClientName = "ALENCOT" Or ClientName = "MANISH" Then
                If ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                    LBLPARTYNAME.Text = "Dyeing Name"
                    GDESCRIPTION.HeaderText = "Chart No"
                End If
                txtsrno.Visible = True
                CMBPIECETYPE.Visible = True
                TXTBALENO.Visible = True
                cmbitemname.Visible = True
                CMBQUALITY.Visible = True
                CMBDESIGN.Visible = True
                cmbcolor.Visible = True
                TXTDESCRIPTION.Visible = True
                TXTCUT.Visible = True
                TXTPCS.Visible = True
                TXTMTRS.Visible = True
                TXTGRIDLOT.Visible = True
                TXTWT.Visible = True
                TXTRATE.Visible = True
                CMBPER.Visible = True
                TXTAMOUNT.Visible = True
            End If

            If ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                LBLTYPE.Visible = True
                CMBTYPE.Visible = True
                TXTTYPEJONO.Visible = True
                LBLSRNO.Visible = True
            Else
                LBLTYPE.Visible = False
                CMBTYPE.Visible = False
                TXTTYPEJONO.Visible = False
            End If

            If ClientName = "AVIS" Then
                LBLPARTYNAME.Visible = False
                CMBPARTYNAME.Visible = False
                GDESCRIPTION.ReadOnly = False
            End If

            If ClientName = "INDRANI" Then
                GBALENO.HeaderText = "SO No"
                GBALENO.ReadOnly = True
                GPCS.ReadOnly = True
                GMTRS.ReadOnly = True
            End If
            If ClientName = "SPCORP" Then TXTJONO.ReadOnly = False
            If ClientName = "KRISHNA" Then CHKLOTREADY.Visible = True
            If ClientName = "TINUMINU" Then LBLCHALLAN.Text = "Program No"
            If ClientName = "SNCM" Then
                LBLCHALLAN.Text = "Beam No"
                GMTRS.ReadOnly = True
                lblwidth.Visible = True
                txtwidth.Visible = True
            End If

            If ClientName = "VINTAGEINDIA" Then
                LBLLOTNO.Text = "Jobber Lot No"
                LBLCHALLAN.Text = "Jobber Challan No"
                GBALENO.HeaderText = "Pcs No"
                TXTCHALLANNO.Enabled = False
                CMBPARTYNAME.Enabled = False
            End If


            If ALLOWMANUALJONO = True Then
                TXTJONO.ReadOnly = False
                TXTJONO.BackColor = Color.LemonChiffon
            Else
                TXTJONO.ReadOnly = True
                TXTJONO.BackColor = Color.Linen
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
    Private Sub CMBQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJQUALITY As New SelectQuality
                OBJQUALITY.ShowDialog()
                If OBJQUALITY.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQUALITY.TEMPNAME
            End If
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
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub CMBCOLOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcolor.Enter
        Try
            If cmbcolor.Text.Trim = "" Then FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcolor.Validating
        Try
            If cmbcolor.Text.Trim <> "" Then COLORVALIDATE(cmbcolor, e, Me, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
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

    Private Sub CMBTYPE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTYPE.Validated
        Try
            GETMAXTYPEJONO()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDJO_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDJO.CellValidating
        Try
            Dim colNum As Integer = GRIDJO.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GPCS.Index, GMTRS.Index, gcut.Index, GRATE.Index, GAMOUNT.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDJO.CurrentCell.Value = Nothing Then GRIDJO.CurrentCell.Value = "0.00"
                        GRIDJO.CurrentCell.Value = Convert.ToDecimal(GRIDJO.Item(colNum, e.RowIndex).Value)
                        '' everything is good
                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

            End Select

            If EDIT = False And ClientName = "MANINATH" Then
                Dim TEMPITEM As String = ""
                For I As Integer = GRIDJO.CurrentRow.Index + 1 To GRIDJO.RowCount - 1
                    If I = GRIDJO.CurrentRow.Index + 1 Then TEMPITEM = GRIDJO.Item(GMERCHANT.Index, I - 1).Value
                    If GRIDJO.Item(GMERCHANT.Index, I).Value = TEMPITEM Then
                        GRIDJO.Item(GBALENO.Index, I).Value = GRIDJO.Item(GBALENO.Index, I - 1).EditedFormattedValue

                    End If
                Next
            End If

        Catch ex As Exception

            Throw ex
        End Try
    End Sub

    Private Sub GRIDJO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDJO.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDJO.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                'end of block
                GRIDJO.Rows.RemoveAt(GRIDJO.CurrentRow.Index)
                getsrno(GRIDJO)
                TOTAL()
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTCUT.Validated, TXTPCS.Validated, TXTMTRS.Validated, TXTRATE.Validated, CMBPER.Validated
        CALC()
    End Sub

    Sub CALC()
        Try
            If Val(TXTPCS.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 Then TXTMTRS.Text = Format(Val(TXTPCS.Text.Trim) * Val(TXTCUT.Text.Trim), "0.00")
            If CMBPER.Text = "Mtrs" Then TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(TXTMTRS.Text.Trim), "0.00") Else TXTAMOUNT.Text = Format(Val(TXTRATE.Text.Trim) * Val(TXTPCS.Text.Trim), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRATE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTRATE.Validated
        If ClientName = "SUCCESS" Or ClientName = "MJFABRIC" Then
            If CMBPIECETYPE.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And Val(TXTMTRS.Text.Trim) > 0 Then fillgrid()
        End If
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

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            If EDIT = False Then Exit Sub
            GENERATEEWB()
            PRINTEWB()
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

    Private Sub CMBPACKING_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPACKING.Enter
        Try
            If CMBPACKING.Text.Trim = "" Then FILLNAME(CMBPACKING, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(sender As Object, e As EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" Then
                'GET REGISTER , AGENCT AND TRANS
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS_1.ACC_CMPNAME,'') AS TRANSNAME, ISNULL(LEDGERS_2.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(REGISTER_NAME,'') AS REGISTERNAME, ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(LEDGERS.ACC_DISC,0) AS DISCPER,  ISNULL(LEDGERS.ACC_CDPER,0) AS CDPER, isnull(LEDGERS.ACC_CRDAYS,0) AS CRDAYS, ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO, ISNULL(TERMMASTER.TERM_NAME,'') AS TERM, ISNULL(LEDGERS.ACC_AGENTCOMM,'') AS AGENTCOMM, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME, ISNULL(LEDGERS.ACC_OVERSEAS,0) AS OVERSEAS, ISNULL(LEDGERS.ACC_TCS,0) AS TCS ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN TERMMASTER ON LEDGERS.ACC_TERMID = TERM_ID  LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_DELIVERYATID = CITY_ID ", " and LEDGERS.acc_cmpname = '" & CMBNAME.Text.Trim & "' and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    If cmbtrans.Text.Trim = "" Then cmbtrans.Text = DT.Rows(0).Item("TRANSNAME")
                    If CMBTOCITY.Text.Trim = "" Then CMBTOCITY.Text = DT.Rows(0).Item("CITYNAME")
                    TXTMOBILENO.Text = DT.Rows(0).Item("MOBILENO")
                    If CMBPACKING.Text.Trim = "" Then CMBPACKING.Text = CMBNAME.Text.Trim
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLSMS_Click(sender As Object, e As EventArgs) Handles TOOLSMS.Click
        If EDIT = False Then Exit Sub
        SMSCODE()
    End Sub

    Private Sub CMBPACKING_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPACKING.Validating
        Try
            If CMBPACKING.Text.Trim <> "" Then NAMEVALIDATE(CMBPACKING, CMBCODE, e, Me, txtDeliveryadd, " AND  (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GENERATEEWB()
        Try
            If ALLOWEWAYBILL = False Then Exit Sub
            If CMBNAME.Text.Trim = "" Then Exit Sub
            If EDIT = False Then Exit Sub

            'If Val(LBLTOTALCGSTAMT.Text.Trim) = 0 And Val(TXTCGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALSGSTAMT.Text.Trim) = 0 And Val(TXTSGSTAMT1.Text.Trim) = 0 And Val(LBLTOTALIGSTAMT.Text.Trim) = 0 And Val(TXTIGSTAMT1.Text.Trim) = 0 Then Exit Sub


            If txtlrno.Text.Trim <> "" AndAlso lrdate.Text <> "__/__/____" Then
                If Convert.ToDateTime(lrdate.Text).Date < Convert.ToDateTime(JODATE.Text).Date Then
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

            'Dim TEMPCMPADD1 As String = ""
            'Dim TEMPCMPADD2 As String = ""
            'Dim PARTYGSTIN As String = ""
            'Dim PARTYPINCODE As String = ""
            'Dim PARTYSTATECODE As String = ""
            'Dim PARTYSTATENAME As String = ""
            'Dim PARTYKMS As Double = 0
            'Dim PARTYADD1 As String = ""
            'Dim PARTYADD2 As String = ""
            'Dim TRANSGSTIN As String = ""


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
            TEMPCMPADD2 = DT.Rows(0).Item("ADD2")
            DISPATCHFROM = CmpName
            DISPATCHFROMGSTIN = CMPGSTIN
            DISPATCHFROMPINCODE = CMPPINCODE
            DISPATCHFROMSTATECODE = CMPSTATECODE
            DISPATCHFROMSTATENAME = CMPSTATENAME


            'PARTY GST DETAILS 
            DT = OBJCMN.SEARCH(" ISNULL(ACC_GSTIN, '') AS GSTIN, (CASE WHEN ISNULL(ACC_DELIVERYPINCODE,'') <> '' THEN ISNULL(ACC_DELIVERYPINCODE,'') ELSE ISNULL(ACC_ZIPCODE,'') END) AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN ISNULL(ACC_SHIPPINGADD,'') ELSE ISNULL(ACC_ADD1,'') END) AS ADD1, (CASE WHEN ISNULL(ACC_SHIPPINGADD,'') <> '' THEN '' ELSE ISNULL(ACC_ADD2,'') END) AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ACC_YEARID = " & YearId)

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
            If CMBPACKING.Text.Trim <> "" AndAlso CMBNAME.Text.Trim <> CMBPACKING.Text.Trim Then
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
            If CMBDISPATCHFROM.Text.Trim <> "" AndAlso CMBNAME.Text.Trim <> CMBDISPATCHFROM.Text.Trim Then
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
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTJONO.Text.Trim) & ",'JOBOUT','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
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
                j = j & """supplyType"":""O"","
                j = j & """subSupplyType"":""4"","
                j = j & """subSupplyDesc"":"""","
                j = j & """docType"":""CHL"","
                j = j & """docNo"":""" & Val(TXTJONO.Text.Trim) & """" & ","
                j = j & """docDate"":""" & JODATE.Text & """" & ","
                j = j & """fromGstin"":""" & CMPGSTIN & """" & ","
                j = j & """fromTrdName"":""" & CmpName & """" & ","
                j = j & """fromAddr1"":""" & TEMPCMPADD1 & """" & ","
                j = j & """fromAddr2"":""" & TEMPCMPADD2 & """" & ","
                j = j & """fromPlace"":""" & CMBFROMCITY.Text.Trim & """" & ","
                j = j & """fromPincode"":""" & DISPATCHFROMPINCODE & """" & ","
                j = j & """actFromStateCode"":""" & DISPATCHFROMSTATECODE & """" & ","
                j = j & """fromStateCode"":""" & CMPSTATECODE & """" & ","
                j = j & """toGstin"":""" & PARTYGSTIN & """" & ","
                j = j & """toTrdName"":""" & CMBNAME.Text.Trim & """" & ","
                j = j & """toAddr1"":""" & PARTYADD1 & """" & ","
                j = j & """toAddr2"":""" & PARTYADD2 & """" & ","
                j = j & """toPlace"":""" & CMBTOCITY.Text.Trim & "-" & SHIPTOGSTIN & "-" & CMBPACKING.Text.Trim & """" & ","
                j = j & """toPincode"":""" & PARTYPINCODE & """" & ","
                j = j & """actToStateCode"":""" & SHIPTOSTATECODE & """" & ","
                j = j & """toStateCode"":""" & PARTYSTATECODE & """" & ","

                j = j & """transactionType"":""4"","
                j = j & """dispatchFromGSTIN"":""" & DISPATCHFROMGSTIN & """" & ","
                j = j & """dispatchFromTradeName"":""" & DISPATCHFROM & """" & ","
                j = j & """shipToGSTIN"":""" & SHIPTOGSTIN & """" & ","
                j = j & """shipToTradeName"":""" & CMBPACKING.Text.Trim & """" & ","
                j = j & """otherValue"":""0"","


                Dim CGSTPER, SGSTPER, IGSTPER As Double
                Dim CGSTAMT, SGSTAMT, IGSTAMT As Double
                Dim HSNCODE As String = ""
                Dim DTHSN As DataTable = OBJCMN.SEARCH("HSNMASTER.HSN_CODE AS HSNCODE, HSNMASTER.HSN_CGST AS CGSTPER, HSN_SGST AS SGSTPER, HSN_IGST AS IGSTPER", "", " ITEMMASTER INNER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID", " AND ITEMMASTER.ITEM_NAME = '" & GRIDJO.Rows(0).Cells(GMERCHANT.Index).Value & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
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
                If DISPATCHFROMPINCODE = PARTYPINCODE Then PARTYKMS = 10


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
                j = j & """productName"":""" & GRIDJO.Item(GMERCHANT.Index, 0).Value & """" & ","
                j = j & """productDesc"":""" & GRIDJO.Item(GMERCHANT.Index, 0).Value & """" & ","
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
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTJONO.Text.Trim) & ",'JOBOUT','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
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
            DT = OBJCMN.Execute_Any_String("UPDATE JOBOUT SET JO_EWAYBILLNO = '" & TXTEWAYBILLNO.Text.Trim & "' FROM JOBOUT WHERE JOB_NO = " & Val(TEMPJONO) & "  AND INVOICE_YEARID = " & YearId, "", "")

            'ADD DATA IN EWAYENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTJONO.Text.Trim) & ",'JOBOUT','" & TOKEN & "','" & EWBNO & "','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


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
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTJONO.Text.Trim) & ",'JOBOUT','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTJONO.Text.Trim) & ",'JOBOUT','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

            Catch ex As WebException
                RESPONSE = ex.Response
                MsgBox("Error While Printing EWB, Please check the Data Properly")
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTJONO.Text.Trim) & ",'JOBOUT','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTJONO.Text.Trim) & ",'JOBOUT','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SENDWHATSAPP(TEMPJONO)
            DT = OBJCMN.Execute_Any_String("UPDATE JOBOUT SET JO_SENDWHATSAPP = 1 WHERE JO_NO = " & TEMPJONO & " AND JO_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(JONO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJJO As New GDNDESIGN
            OBJJO.MdiParent = MDIMain
            OBJJO.DIRECTPRINT = True
            OBJJO.FRMSTRING = "JOBOUT"
            OBJJO.DIRECTMAIL = False
            OBJJO.DIRECTWHATSAPP = True
            OBJJO.PRINTSETTING = PRINTDIALOG
            If MsgBox("Wish to Send Whatsapp With GST...?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJJO.GSTRPT = True
            OBJJO.FORMULA = "{JOBOUT.JO_NO}=" & Val(JONO) & " and {JOBOUT.JO_yearid}=" & YearId
            OBJJO.JONO = Val(JONO)
            OBJJO.NOOFCOPIES = 1
            OBJJO.Show()
            OBJJO.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\JOBOUT_" & Val(JONO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add("JOBOUT_" & Val(JONO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SMSCODE()
        If ALLOWSMS = True Then
            If ClientName <> "KOTHARI" And TXTMOBILENO.Text.Trim = "" Then Exit Sub
            If ClientName = "KOTHARI" And CMBPACKING.Text.Trim = "" Then Exit Sub

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
            '    MSG = MSG & lrdate.Text

            'ElseIf ClientName = "KCRAYON" Then
            '    MSG = "INV NO " & Val(TEMPINVOICENO) & "\n"
            '    DT = OBJCMN.search("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
            '    If DT.Rows.Count > 0 Then MSG = MSG & "TRANSPORT NAME-" & DT.Rows(0).Item("TRANSCODE") & " & LRNO-" & txtlrno.Text.Trim & "\n"
            '    For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
            '        MSG = MSG & ROW.Cells(GITEMNAME.Index).Value & "-" & Format(Val(ROW.Cells(GMTRS.Index).Value), "0.00") & "\n"
            '    Next
            '    MSG = MSG & "THANK YOU"

            'ElseIf ClientName = "NVAHAN" Then
            '    MSG = "GOODS DESP" & "\n"
            '    MSG = MSG & "INV-" & Val(TEMPINVOICENO) & "\n"
            '    MSG = MSG & "LRNO-" & txtlrno.Text.Trim & "\n"
            '    MSG = MSG & "DT-" & lrdate.Text & "\n"
            '    DT = OBJCMN.search("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
            '    If DT.Rows.Count > 0 Then MSG = MSG & "TRANS-" & DT.Rows(0).Item("TRANSCODE") & "\n"
            '    MSG = MSG & "ITEM-" & GRIDINVOICE.Rows(0).Cells(GITEMNAME.Index).Value & "\n" & "PCS-" & Val(GRIDINVOICE.Rows(0).Cells(GPCS.Index).Value) & "\n" & "MTRS-" & Val(GRIDINVOICE.Rows(0).Cells(GMTRS.Index).Value) & "\n" & "BALE-" & GRIDINVOICE.Rows(0).Cells(GBALENO.Index).Value

            'ElseIf ClientName = "YASHVI" Then
            '    MSG = CMBNAME.Text.Trim & "\n"
            '    MSG = MSG & "BALENO-" & txtchallan.Text.Trim & "\n"
            '    DT = OBJCMN.search("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
            '    If DT.Rows.Count > 0 Then MSG = MSG & DT.Rows(0).Item("TRANSCODE") & "\n"
            '    MSG = MSG & "LRNO-" & txtlrno.Text.Trim & "\n"
            '    MSG = MSG & "DT-" & lrdate.Text & "\n"
            '    MSG = MSG & "QTY-" & Val(LBLTOTALMTRS.Text.Trim) & "\n"
            '    MSG = MSG & CmpName

            If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                MSG = "CHALLAN NO " & Val(TEMPJONO) & "\n"
                MSG = MSG & "DT-" & JODATE.Text & "\n"
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & "TRANSPORT NAME-" & DT.Rows(0).Item("TRANSCODE") & " & LRNO-" & txtlrno.Text.Trim & "\n"
                MSG = MSG & "LRDT-" & lrdate.Text & "\n"
                MSG = MSG & "PCS-" & Val(LBLTOTALPCS.Text.Trim) & "\n"
                MSG = MSG & "MTRS-" & Val(LBLTOTALMTRS.Text.Trim) & "\n"
                'MSG = MSG & " & BUNDLES-" & TXTBALENOFROM.Text.Trim & "\n"
                ''For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                ''    MSG = MSG & ROW.Cells(GITEMNAME.Index).Value & "-" & Format(Val(ROW.Cells(Gmtrs.Index).Value), "0.00") & "\n"
                ''Next
                MSG = MSG & "THANK YOU"
                'Else
                '    MSG = "SALE NO " & Val(TEMPINVOICENO) & "\n"
                'DT = OBJCMN.search("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbtrans.Text.Trim & "' AND ACC_YEARID = " & YearId)
                'If DT.Rows.Count > 0 Then MSG = MSG & "TRANSPORT NAME-" & DT.Rows(0).Item("TRANSCODE") & " & LRNO-" & txtlrno.Text.Trim & "\n"
                'For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                '    MSG = MSG & ROW.Cells(GITEMNAME.Index).Value & "-" & Format(Val(ROW.Cells(GMTRS.Index).Value), "0.00") & "\n"
                'Next
                'MSG = MSG & "THANK YOU"
            End If

            If SENDMSG(MSG, TXTMOBILENO.Text.Trim) = "1701" Then
                MsgBox("Message Sent")
                DT = OBJCMN.Execute_Any_String("UPDATE JOBOUT SET JO_SMSSEND = 1 WHERE JO_NO = " & TEMPJONO & " AND JO_YEARID = " & YearId, "", "")
                LBLSMS.Visible = True
            Else
                MsgBox("Error Sending Message")
            End If
        End If
    End Sub

    Private Sub TXTBALENUMBER_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTBALENUMBER.KeyPress
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLOTNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTLOTNO.Validating
        Try
            If CMBNAME.Text.Trim <> "" And TXTLOTNO.Text.Trim <> "" And ClientName <> "VINTAGEINDIA" Then
                'CHECK IN LOT_VIEW FOR LOTNO DUPLLICATION
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" GRNNO ", "", " LOT_VIEW ", " AND LOTNO = '" & TXTLOTNO.Text.Trim & "' AND JOBBERNAME = '" & CMBNAME.Text.Trim & "' AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If (EDIT = False) Or (EDIT = True And Val(DT.Rows(0).Item("GRNNO")) <> Val(TXTJONO.Text.Trim)) Then
                        MsgBox("Lot No Already Exists in Entry No " & DT.Rows(0).Item("GRNNO"), MsgBoxStyle.Critical)
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTAMOUNT_Validated(sender As Object, e As EventArgs) Handles TXTAMOUNT.Validated
        Try
            If CMBPIECETYPE.Text.Trim <> "" And cmbitemname.Text.Trim <> "" And Val(TXTMTRS.Text.Trim) > 0 Then fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTMTRS.KeyPress, TXTCUT.KeyPress, TXTRATE.KeyPress, TXTPCS.KeyPress, TXTWT.KeyPress, TXTAVGWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 Then

                If CMBGODOWN.Text.Trim = "" Then
                    MsgBox("Select Godown First", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                'GET DATA FROM BARCODE
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("TOP 1 *", "", "BARCODESTOCK", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "' AND DONE = 0 AND CMPID = " & CmpId & " AND LOCATIONID  = " & Locationid & " AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then

                    'VALIDATE GODOWN
                    If DT.Rows(0).Item("GODOWN") <> CMBGODOWN.Text.Trim Then
                        MsgBox("Item Not in Selected Godown", MsgBoxStyle.Critical)
                        TXTBARCODE.Clear()
                        Exit Sub
                    End If

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    For Each ROW As DataGridViewRow In GRIDJO.Rows
                        If LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(TXTBARCODE.Text.Trim) Then GoTo LINE1
                    Next


                    Dim PCS As Double = 0
                    Dim RATE As Double = Val(DT.Rows(0).Item("PURRATE"))
                    Dim PER As String = "Mtrs"

                    'FETCH PER FROM ITEMMASTER
                    If ClientName = "SUBHLAXMI" Or ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                        Dim DTPER As DataTable = OBJCMN.SEARCH("ISNULL(UNIT_ABBR,'Mtrs') AS UNIT", "", " ITEMMASTER LEFT OUTER JOIN UNITMASTER ON ITEMMASTER.ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEMMASTER.ITEM_NAME = '" & DT.Rows(0).Item("ITEMNAME") & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                        If DTPER.Rows.Count > 0 AndAlso DTPER.Rows(0).Item("UNIT") = "Pcs" Then PER = "Pcs"
                    End If

                    If ClientName = "TCOT" Or ClientName = "SUPEEMA" Then PCS = Val(DT.Rows(0).Item("PCS")) Else PCS = 1

                    'GET WRATE FROM DESIGNMASTER
                    If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                        Dim DTRATE As DataTable = OBJCMN.Execute_Any_String(" SELECT ISNULL(DESIGN_WRATE,0) AS WRATE FROM DESIGNMASTER WHERE DESIGN_NO = '" & DT.Rows(0).Item("DESIGNNO") & "' AND DESIGN_YEARID = " & YearId, "", "")
                        If DTRATE.Rows.Count > 0 Then RATE = Val(DTRATE.Rows(0).Item("WRATE"))
                    End If

                    GRIDJO.Rows.Add(GRIDJO.RowCount + 1, DT.Rows(0).Item("PIECETYPE"), DT.Rows(0).Item("BALENO"), DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("DESIGNNO"), DT.Rows(0).Item("COLOR"), DT.Rows(0).Item("LOTNO"), "", Format(Val(DT.Rows(0).Item("CUT")), "0.00"), PCS, DT.Rows(0).Item("UNIT"), Format(Val(DT.Rows(0).Item("MTRS")), "0.00"), 0, RATE, PER, 0, DT.Rows(0).Item("BARCODE"), 0, 0, DT.Rows(0).Item("FROMNO"), DT.Rows(0).Item("FROMSRNO"), DT.Rows(0).Item("TYPE"), 0, 0)
                    TOTAL()
                    GRIDJO.FirstDisplayedScrollingRowIndex = GRIDJO.RowCount - 1
                    If ClientName = "SAKARIA" Or ClientName = "CC" Or ClientName = "C3" Or ClientName = "VINTAGEINDIA" Then CMBPARTYNAME.Text = DT.Rows(0).Item("PURNAME")

LINE1:
                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()
                    'Else
                    '    MsgBox("Invalid Barcode / Barcode already Used", MsgBoxStyle.Critical)
                    '    GoTo LINE1
                    '    Exit Sub
                Else
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    TXTBARCODE.Clear()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLOTNO_Validated(sender As Object, e As EventArgs) Handles TXTLOTNO.Validated
        Try
            If ClientName = "VINTAGEINDIA" And TXTLOTNO.Text.Trim <> "" And GRIDJO.RowCount = 0 Then
                If CMBGODOWN.Text.Trim = "" Then
                    MsgBox("Select Godown First", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                Dim OBJCMN As New ClsCommon
                Dim PER As String = "Mtrs"
                Dim DT As DataTable = OBJCMN.SEARCH(" *", "", "BARCODESTOCK", " AND LOTNO = '" & TXTLOTNO.Text.Trim & "' AND DONE = 0 AND CMPID = " & CmpId & " AND LOCATIONID  = " & Locationid & " AND YEARID = " & YearId & " ORDER BY FROMSRNO ")
                If DT.Rows.Count > 0 Then
                    Dim tempmsg As Integer = MessageBox.Show("Do You Want To Fetch Data From Stock ?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then
                        If DT.Rows(0).Item("GODOWN") <> CMBGODOWN.Text.Trim Then
                            MsgBox("Item Not in Selected Godown", MsgBoxStyle.Critical)
                            TXTLOTNO.Clear()
                            Exit Sub
                        End If
                        For Each DTROWPS As DataRow In DT.Rows
                            GRIDJO.Rows.Add(0, DTROWPS("PIECETYPE"), DTROWPS("BALENO"), DTROWPS("ITEMNAME"), DTROWPS("QUALITY"), DTROWPS("DESIGNNO"), DTROWPS("COLOR"), DTROWPS("LOTNO"), "", 0, Val(DTROWPS("PCS")), DTROWPS("UNIT"), Format(Val(DTROWPS("MTRS")), "0.00"), 0, Val(DTROWPS("PURRATE")), PER, 0, DTROWPS("BARCODE"), 0, 0, DTROWPS("FROMNO"), DTROWPS("FROMSRNO"), DTROWPS("TYPE"), 0, 0)
                            CMBPARTYNAME.Text = DTROWPS("PURNAME")
                            TXTCHALLANNO.Text = DTROWPS("CHALLANNO")
                        Next


                        getsrno(GRIDJO)
                        TOTAL()
                        GRIDJO.FirstDisplayedScrollingRowIndex = GRIDJO.RowCount - 1
                    End If
                End If
            End If
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

    Private Sub CMBDISPATCHFROM_Enter(sender As Object, e As EventArgs) Handles CMBDISPATCHFROM.Enter
        Try
            If CMBDISPATCHFROM.Text.Trim = "" Then FILLNAME(CMBDISPATCHFROM, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class