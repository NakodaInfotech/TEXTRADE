
Imports BL
Imports System.IO
Imports System.ComponentModel

Public Class ItemMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK, GRIDPROCESSDOUBLECLICK, GRIDSTORESDOUBLECLICK, GRIDITEMDOUBLECLICK, GRIDMATCHDOUBLECLICK, GRIDWARPDOUBLECLICK, GRIDCOLORDOUBLECLICK, GRIDWEFTDOUBLECLICK, GRIDSHADEDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPPROW, TEMPUPLOADROW, TEMPSROW, TEMPMATCHROW, TEMPWARPROW, TEMPCOLORROW, TEMPWEFTROW, TEMPSHADEROW, TEMPCOLOROW, TEMPITEMROW As Integer
    Public EDIT As Boolean
    Public tempItemName, tempItemCODE, frmstring As String
    Dim DT_ITEMDETAILS As New DataTable
    Dim tempItemId As Integer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        FILLCMB()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            'IF WE MAKE ANY CHANGES IN SAVE CODE THEN DO THE SAME CHANGS IN THE FOLLOWING FORMS
            '1) UPLOADSTOCK AND UPLOADITEM ON MDIMAIN
            '2) TXTCHNO_VALIDATED IN GRN
            '2) TOOLUPLOADEXCEL_CLICK IN GRN
            '3) TXTCCMPSO_VALIDATED IN SALEORDER
            '4) CMDSELECTGDN_CLICK IN PURCHASEINVOICE
            '5) TXTCHNO_VALIDATED IN STOCKRECO
            '6) CMDSELECTCHALLAN_CLICK IN SALEINVOICE
            '7) CMDSELECTSO_CLICK IN SALEINVOICE
            '8) TXTPRNO_Validating IN MATERIALRECEIPT
            '9) CMDSELECTPO CLICK IN PURCHASEINVOICE


            Ep.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(cmbmaterial.Text.Trim)
            alParaval.Add(cmbcategory.Text.Trim)
            alParaval.Add(TXTDISPLAYNAME.Text.Trim)
            alParaval.Add(UCase(cmbitemname.Text.Trim))

            alParaval.Add(CMBDEPARTMENT.Text.Trim)
            alParaval.Add(CMBCODE.Text.Trim)
            alParaval.Add(cmbunit.Text.Trim)
            alParaval.Add(TXTFOLD.Text.Trim)
            alParaval.Add(TXTRATE.Text.Trim)
            alParaval.Add(TXTVALUATIONRATE.Text.Trim)
            alParaval.Add(Val(TXTTRANSPORTRATE.Text.Trim))
            alParaval.Add(Val(TXTCHECKINGRATE.Text.Trim))
            alParaval.Add(Val(TXTPACKINGRATE.Text.Trim))
            alParaval.Add(Val(TXTDESIGNRATE.Text.Trim))
            alParaval.Add(txtreorder.Text.Trim)
            alParaval.Add(txtupper.Text.Trim)
            alParaval.Add(txtlower.Text.Trim)
            alParaval.Add(CMBHSNCODE.Text.Trim)
            alParaval.Add(CHKBLOCKED.CheckState)
            alParaval.Add(CHKHIDEINDESIGN.CheckState)

            alParaval.Add(TXTWIDTH.Text.Trim)
            alParaval.Add(TXTGREYWIDTH.Text.Trim)
            alParaval.Add(TXTSHRINKFROM.Text.Trim)
            alParaval.Add(TXTSHRINKTO.Text.Trim)
            alParaval.Add(TXTSELVEDGE.Text.Trim)




            'FOR GRIDPARAMETER
            Dim RATETYPE As String = ""
            Dim RATE As String = ""

            For Each ROW As DataGridViewRow In GRIDRATE.Rows
                If ROW.Cells(gratetype.Index).Value <> Nothing Then
                    If RATETYPE = "" Then
                        RATETYPE = ROW.Cells(gratetype.Index).Value.ToString
                        RATE = ROW.Cells(grate.Index).Value
                    Else
                        RATETYPE = RATETYPE & "|" & ROW.Cells(gratetype.Index).Value.ToString
                        RATE = RATE & "|" & ROW.Cells(grate.Index).Value
                    End If
                End If
            Next


            alParaval.Add(RATETYPE)
            alParaval.Add(RATE)

            Dim YARNQUALITY As String = ""
            Dim PER As String = ""

            For Each ROW As DataGridViewRow In GRIDCOMP.Rows
                If ROW.Cells(GYARNQUALITY.Index).Value <> Nothing Then
                    If YARNQUALITY = "" Then
                        YARNQUALITY = ROW.Cells(GYARNQUALITY.Index).Value.ToString
                        PER = Val(ROW.Cells(GPER.Index).Value)
                    Else
                        YARNQUALITY = YARNQUALITY & "|" & ROW.Cells(GYARNQUALITY.Index).Value.ToString
                        PER = PER & "|" & Val(ROW.Cells(GPER.Index).Value)
                    End If
                End If
            Next


            alParaval.Add(YARNQUALITY)
            alParaval.Add(PER)



            Dim gridsrno As String = ""
            Dim PROCESS As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDPROCESS.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(PSRNO.Index).Value.ToString
                        PROCESS = row.Cells(PPROCESS.Index).Value.ToString
                    Else
                        gridsrno = gridsrno & "|" & row.Cells(PSRNO.Index).Value.ToString
                        PROCESS = PROCESS & "|" & row.Cells(PPROCESS.Index).Value.ToString
                    End If
                End If
            Next


            alParaval.Add(gridsrno)
            alParaval.Add(PROCESS)

            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(frmstring)

            If PBPHOTO.Image IsNot Nothing Then
                Dim MS As New IO.MemoryStream
                PBPHOTO.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                alParaval.Add(MS.ToArray)
            Else
                alParaval.Add(DBNull.Value)
            End If
            alParaval.Add(TXTWARP.Text.Trim)
            alParaval.Add(TXTWEFT.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)





            Dim WARPSRNO As String = ""
            Dim WARPQUALITY As String = ""
            Dim WARPSHADE As String = ""
            Dim WARPENDS As String = ""
            Dim WARPWT As String = ""
            Dim WARPRATE As String = ""
            Dim WARPAMOUNT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDWARP.Rows
                If row.Cells(0).Value <> Nothing Then
                    If WARPSRNO = "" Then
                        WARPSRNO = Val(row.Cells(WSRNO.Index).Value)
                        WARPQUALITY = row.Cells(WQUALITY.Index).Value.ToString
                        WARPSHADE = row.Cells(WSHADE.Index).Value.ToString
                        WARPENDS = Val(row.Cells(WENDS.Index).Value)
                        WARPWT = Val(row.Cells(WWT.Index).Value)
                        WARPRATE = Val(row.Cells(WRATE.Index).Value)
                        WARPAMOUNT = Val(row.Cells(WAMOUNT.Index).Value)
                    Else
                        WARPSRNO = WARPSRNO & "|" & Val(row.Cells(WSRNO.Index).Value)
                        WARPQUALITY = WARPQUALITY & "|" & row.Cells(WQUALITY.Index).Value.ToString
                        WARPSHADE = WARPSHADE & "|" & row.Cells(WSHADE.Index).Value.ToString
                        WARPENDS = WARPENDS & "|" & Val(row.Cells(WENDS.Index).Value)
                        WARPWT = WARPWT & "|" & Val(row.Cells(WWT.Index).Value)
                        WARPRATE = WARPRATE & "|" & Val(row.Cells(WRATE.Index).Value)
                        WARPAMOUNT = WARPAMOUNT & "|" & Val(row.Cells(WAMOUNT.Index).Value)
                    End If
                End If
            Next


            alParaval.Add(WARPSRNO)
            alParaval.Add(WARPQUALITY)
            alParaval.Add(WARPSHADE)
            alParaval.Add(WARPENDS)
            alParaval.Add(WARPWT)
            alParaval.Add(WARPRATE)
            alParaval.Add(WARPAMOUNT)



            Dim WEFTSRNO As String = ""
            Dim WEFTQUALITY As String = ""
            Dim WEFTSHADE As String = ""
            Dim WEFTPICK As String = ""
            Dim WEFTWT As String = ""
            Dim WEFTRATE As String = ""
            Dim WEFTAMOUNT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDWEFT.Rows
                If row.Cells(0).Value <> Nothing Then
                    If WEFTSRNO = "" Then
                        WEFTSRNO = Val(row.Cells(FSRNO.Index).Value)
                        WEFTQUALITY = row.Cells(FQUALITY.Index).Value.ToString
                        WEFTSHADE = row.Cells(FSHADE.Index).Value.ToString
                        WEFTPICK = Val(row.Cells(FPICK.Index).Value)
                        WEFTWT = Val(row.Cells(FWT.Index).Value)
                        WEFTRATE = Val(row.Cells(FRATE.Index).Value)
                        WEFTAMOUNT = Val(row.Cells(FAMOUNT.Index).Value)
                    Else
                        WEFTSRNO = WEFTSRNO & "|" & Val(row.Cells(FSRNO.Index).Value)
                        WEFTQUALITY = WEFTQUALITY & "|" & row.Cells(FQUALITY.Index).Value.ToString
                        WEFTSHADE = WEFTSHADE & "|" & row.Cells(FSHADE.Index).Value.ToString
                        WEFTPICK = WEFTPICK & "|" & Val(row.Cells(FPICK.Index).Value)
                        WEFTWT = WEFTWT & "|" & Val(row.Cells(FWT.Index).Value)
                        WEFTRATE = WEFTRATE & "|" & Val(row.Cells(FRATE.Index).Value)
                        WEFTAMOUNT = WEFTAMOUNT & "|" & Val(row.Cells(FAMOUNT.Index).Value)
                    End If
                End If
            Next


            alParaval.Add(WEFTSRNO)
            alParaval.Add(WEFTQUALITY)
            alParaval.Add(WEFTSHADE)
            alParaval.Add(WEFTPICK)
            alParaval.Add(WEFTWT)
            alParaval.Add(WEFTRATE)
            alParaval.Add(WEFTAMOUNT)


            alParaval.Add(Val(TXTWARPTL.Text.Trim))
            alParaval.Add(Val(TXTWEFTTL.Text.Trim))
            alParaval.Add(Val(TXTREED.Text.Trim))
            alParaval.Add(Val(TXTREEDSPACE.Text.Trim))
            alParaval.Add(Val(TXTPICKS.Text.Trim))
            alParaval.Add(Val(TXTTOTALWT.Text.Trim))
            alParaval.Add(Val(TXTTOTALWARPWT.Text.Trim))
            alParaval.Add(Val(TXTTOTALWEFTWT.Text.Trim))
            alParaval.Add(TXTWEAVE.Text.Trim)
            alParaval.Add(CMBGREYCATEGORY.Text.Trim)

            alParaval.Add(TXTACTWT.Text.Trim)
            alParaval.Add(TXTACTAMOUNT.Text.Trim)
            alParaval.Add(TXTDHARAPERCENT.Text.Trim)
            alParaval.Add(TXTDHARAAMOUNT.Text.Trim)
            alParaval.Add(TXTWASTAGEPERCENT.Text.Trim)
            alParaval.Add(TXTWASTAGEAMOUNT.Text.Trim)
            alParaval.Add(TXTWEAVINGCHGS.Text.Trim)
            alParaval.Add(TXTWEAVINGAMOUNT.Text.Trim)
            alParaval.Add(TXTGSTPERCENT.Text.Trim)
            alParaval.Add(TXTGSTAMOUNT.Text.Trim)
            alParaval.Add(TXTAMOUNT.Text.Trim)
            alParaval.Add(TXTTOTALGSTPERCENT.Text.Trim)
            alParaval.Add(TXTTOTALAMOUNT.Text.Trim)
            alParaval.Add(TXTTOTALWARPAMOUNT.Text.Trim)
            alParaval.Add(TXTTOTALWEFTAMOUNT.Text.Trim)



            Dim COLORSRNO As String = ""
            Dim COLOR As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDSHADE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If COLORSRNO = "" Then
                        COLORSRNO = row.Cells(GSRNO.Index).Value.ToString
                        COLOR = row.Cells(GCOLOR.Index).Value.ToString
                    Else
                        COLORSRNO = COLORSRNO & "|" & row.Cells(GSRNO.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GCOLOR.Index).Value.ToString
                    End If
                End If
            Next


            alParaval.Add(COLORSRNO)
            alParaval.Add(COLOR)
            alParaval.Add(TXTVALUELOSSPER.Text.Trim)
            alParaval.Add(CMBCOSTCENTERNAME.Text.Trim)
            alParaval.Add(TXTGSM.Text.Trim)
            alParaval.Add(TXTPERCENT.Text.Trim)
            alParaval.Add(CHKGARMENT.CheckState)


            Dim SHADESRNO As String = ""
            Dim SHADE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCOLOR.Rows
                If row.Cells(0).Value <> Nothing Then
                    If SHADESRNO = "" Then
                        SHADESRNO = Val(row.Cells(GSHADESRNO.Index).Value)
                        SHADE = row.Cells(GSHADE.Index).Value.ToString
                    Else
                        SHADESRNO = SHADESRNO & "|" & Val(row.Cells(GSHADESRNO.Index).Value)
                        SHADE = SHADE & "|" & row.Cells(GSHADE.Index).Value.ToString
                    End If
                End If
            Next



            alParaval.Add(SHADESRNO)
            alParaval.Add(SHADE)


            Dim ITEMSRNO As String = ""
            Dim ITEMNAME As String = ""
            Dim ITEMDESIGN As String = ""
            Dim ITEMSHADE As String = ""
            Dim MTRS As String = ""
            Dim SHADEGRIDSRNO As String = ""

            For i As Integer = 0 To DT_ITEMDETAILS.Rows.Count - 1
                If DT_ITEMDETAILS.Rows(i).Item(0) <> Nothing Then
                    If ITEMSRNO = "" Then
                        ITEMSRNO = Val(DT_ITEMDETAILS.Rows(i).Item("ITEMSRNO"))
                        ITEMNAME = DT_ITEMDETAILS.Rows(i).Item("ITEM")
                        ITEMDESIGN = DT_ITEMDETAILS.Rows(i).Item("ITEMDESIGN")
                        ITEMSHADE = DT_ITEMDETAILS.Rows(i).Item("ITEMSHADE")
                        MTRS = Val(DT_ITEMDETAILS.Rows(i).Item("MTRS"))
                        SHADEGRIDSRNO = Val(DT_ITEMDETAILS.Rows(i).Item("SHADESRNO"))
                    Else
                        ITEMSRNO = ITEMSRNO & "|" & Val(DT_ITEMDETAILS.Rows(i).Item("ITEMSRNO"))
                        ITEMNAME = ITEMNAME & "|" & DT_ITEMDETAILS.Rows(i).Item("ITEM")
                        ITEMDESIGN = ITEMDESIGN & "|" & DT_ITEMDETAILS.Rows(i).Item("ITEMDESIGN")
                        ITEMSHADE = ITEMSHADE & "|" & DT_ITEMDETAILS.Rows(i).Item("ITEMSHADE")
                        MTRS = MTRS & "|" & Val(DT_ITEMDETAILS.Rows(i).Item("MTRS"))
                        SHADEGRIDSRNO = SHADEGRIDSRNO & "|" & Val(DT_ITEMDETAILS.Rows(i).Item("SHADESRNO"))
                    End If
                End If
            Next


            alParaval.Add(ITEMSRNO)
            alParaval.Add(ITEMNAME)
            alParaval.Add(ITEMDESIGN)
            alParaval.Add(ITEMSHADE)
            alParaval.Add(MTRS)
            alParaval.Add(SHADEGRIDSRNO)

            Dim objclsItemMaster As New clsItemmaster
            objclsItemMaster.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objclsItemMaster.SAVE()
                ADDRATE()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempItemId)
                IntResult = objclsItemMaster.UPDATE()
                MsgBox("Details Updated")

            End If
            EDIT = False

            CLEAR()
            cmbitemname.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Function CHECKDUPLICATE() As Boolean
        Try
            Dim BLN As Boolean = True
            pcase(cmbitemname)
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable
            If (EDIT = False) Or (EDIT = True And LCase(cmbitemname.Text) <> LCase(tempItemName)) Then
                dt = objclscommon.search("item_name", "", "ItemMaster", " and item_name = '" & cmbitemname.Text.Trim & "'  And item_cmpid = " & CmpId & " And item_locationid = " & Locationid & " And item_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Item Name Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                    BLN = False
                End If
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function ERRORVALID() As Boolean

        Dim bln As Boolean = True
        If TXTDISPLAYNAME.Text.Trim.Length = 0 And ClientName <> "VINTAGEINDIA" Then
            Ep.SetError(TXTDISPLAYNAME, "Fill Item Name")
            bln = False
        End If

        If CMBHSNCODE.Text.Trim.Length = 0 And ClientName <> "VINTAGEINDIA" And ClientName <> "CC" Then
            Ep.SetError(CMBHSNCODE, "Fill HSN Code")
            bln = False
        End If

        If cmbitemname.Text.Trim.Length = 0 Then
            Ep.SetError(cmbitemname, "Fill Item Name")
            bln = False
        End If

        If (ClientName = "LEEFABRICO" Or ClientName = "SOFTAS" Or ClientName = "RADHA") And TXTWIDTH.Text.Trim.Length = 0 Then
            Ep.SetError(TXTWIDTH, "Fill Width")
            bln = False
        End If

        If ClientName = "SANGHVI" And TXTSELVEDGE.Text.Trim.Length = 0 Then
            Ep.SetError(TXTSELVEDGE, "Fill Tally Item Name")
            bln = False
        End If

        If Not CHECKDUPLICATE() Then
            Ep.SetError(cmbitemname, "Item Name Already Exists")
            bln = False
        End If

        If CMBCODE.Text.Trim.Length = 0 And ClientName <> "VINTAGEINDIA" Then
            Ep.SetError(CMBCODE, "Fill Item Code")
            bln = False
        End If

        If cmbmaterial.Text.Trim.Length = 0 Then
            Ep.SetError(cmbmaterial, "Select Material Type")
            bln = False
        End If

        If Val(TXTTOTALPER.Text.Trim) <> 100 And GRIDCOMP.RowCount > 0 Then
            Ep.SetError(TXTTOTALPER, "Check %")
            bln = False
        End If

        If ClientName = "AVIS" Then
            If cmbcategory.Text.Trim.Length = 0 Then
                Ep.SetError(cmbcategory, "Select Category")
                bln = False
            End If
            If Val(TXTVALUELOSSPER.Text.Trim) = 0 And CHKHIDEINDESIGN.Checked = False And CHKBLOCKED.Checked = False Then
                Ep.SetError(TXTVALUELOSSPER, "Enter Value Loss %")
                bln = False
            End If
            If Val(TXTSHRINKFROM.Text.Trim) = 0 And CHKHIDEINDESIGN.Checked = False And CHKBLOCKED.Checked = False Then
                Ep.SetError(TXTSHRINKFROM, "Enter Shrinkage %")
                bln = False
            End If
        End If

        If ClientName = "ALENCOT" Then
            If cmbcategory.Text.Trim.Length = 0 Then
                Ep.SetError(cmbcategory, "Select Category")
                bln = False
            End If

            If txtremarks.Text.Trim.Length = 0 Then
                Ep.SetError(txtremarks, "Enter Remarks")
                bln = False
            End If

            If TXTWIDTH.Text.Trim.Length = 0 Then
                Ep.SetError(TXTWIDTH, "Enter Width")
                bln = False
            End If
        End If
        'If ClientName = "VINTAGEINDIA" Then
        '    If cmbcategory.Text.Trim.Length = 0 Then
        '        Ep.SetError(cmbcategory, "Select Category")
        '        bln = False
        '    End If
        'End If
        If ClientName = "SNCM" Then
            If cmbcategory.Text.Trim.Length = 0 Then
                Ep.SetError(cmbcategory, "Select Category")
                bln = False
            End If
            If CMBGREYCATEGORY.Text.Trim.Length = 0 Then
                Ep.SetError(CMBGREYCATEGORY, "Select Sub Category")
                bln = False
            End If
            If cmbunit.Text.Trim.Length = 0 Then
                Ep.SetError(cmbunit, "Select Unit")
                bln = False
            End If
        End If



        Return bln
    End Function

    Sub CLEAR()

        cmbmaterial.Text = ""
        cmbcategory.Text = ""
        CMBGREYCATEGORY.Text = ""
        TXTDISPLAYNAME.Clear()
        cmbitemname.Text = ""
        CMBDEPARTMENT.Text = ""
        If ClientName = "MAHAVIRPOLYCOT" Then cmbunit.Text = "Mtrs" Else cmbunit.Text = ""
        CMBCODE.Text = ""
        TXTFOLD.Clear()
        TXTRATE.Clear()
        TXTVALUATIONRATE.Clear()
        TXTTRANSPORTRATE.Clear()
        TXTPACKINGRATE.Clear()
        TXTCHECKINGRATE.Clear()
        TXTDESIGNRATE.Clear()
        txtlower.Clear()
        txtreorder.Clear()
        txtupper.Clear()
        If ClientName = "SOFTAS" Then CMBHSNCODE.Text = "540782" Else CMBHSNCODE.Text = ""
        TXTPHOTOIMGPATH.Clear()
        PBPHOTO.Image = Nothing
        If ClientName = "SAFFRON" Then txtremarks.Text = "SPUNTEX MIX QUALITY" Else txtremarks.Clear()
        CMBYARNQUALITY.Text = ""
        TXTPER.Clear()
        CHKBLOCKED.CheckState = CheckState.Unchecked
        CHKHIDEINDESIGN.CheckState = CheckState.Unchecked

        If ClientName = "KEMLINO" Then
            TXTWIDTH.Text = "147 CMS"
        ElseIf ClientName = "MOHATUL" Then
            TXTWIDTH.Text = "58"
        ElseIf ClientName = "SOFTAS" Then
            TXTWIDTH.Text = "36 INCH"
            If CmpName = "SIDDHIM COTFAB LLP" Then TXTWIDTH.Text = "58 INCH"
        Else
            TXTWIDTH.Clear()
        End If
        TXTGREYWIDTH.Clear()
        TXTSHRINKFROM.Clear()
        TXTSHRINKTO.Clear()
        TXTSELVEDGE.Clear()
        TXTWARP.Clear()
        TXTWEFT.Clear()

        CMBPROCESS.Text = ""
        TXTPSRNO.Clear()
        TXTSHADESRNO.Clear()
        TXTITEMSRNO.Clear()
        DT_ITEMDETAILS.Reset()
        DT_ITEMDETAILS.Columns.Add("ITEMSRNO")
        DT_ITEMDETAILS.Columns.Add("ITEM")
        DT_ITEMDETAILS.Columns.Add("ITEMDESIGN")
        DT_ITEMDETAILS.Columns.Add("ITEMSHADE")
        DT_ITEMDETAILS.Columns.Add("MTRS")
        DT_ITEMDETAILS.Columns.Add("SHADESRNO")

        GRIDRATE.RowCount = 0
        GRIDCOMP.RowCount = 0
        GRIDPROCESS.RowCount = 0

        TXTTOTALPER.Clear()

        TXTWARPTL.Clear()
        TXTWEFTTL.Clear()
        TXTREED.Clear()
        TXTREEDSPACE.Clear()
        TXTPICKS.Clear()
        TXTTOTALWT.Clear()
        TXTWEAVE.Clear()

        TXTACTWT.Clear()
        TXTACTAMOUNT.Clear()
        TXTDHARAPERCENT.Clear()
        TXTWASTAGEPERCENT.Clear()
        TXTWEAVINGCHGS.Clear()
        TXTGSTPERCENT.Clear()
        TXTAMOUNT.Clear()
        TXTTOTALGSTPERCENT.Clear()
        TXTTOTALAMOUNT.Clear()

        TXTWARPSRNO.Clear()
        CMBWARPQUALITY.Text = ""
        CMBWARPSHADE.Text = ""
        TXTWARPDENIER.Clear()
        TXTWARPENDS.Clear()
        TXTWARPWT.Clear()
        TXTTOTALWARPWT.Clear()
        TXTDHARAAMOUNT.Clear()
        TXTWASTAGEAMOUNT.Clear()
        TXTWEAVINGAMOUNT.Clear()
        TXTGSTAMOUNT.Clear()
        TXTTOTALWARPAMOUNT.Clear()
        TXTTOTALWEFTAMOUNT.Clear()
        GRIDWARP.RowCount = 0
        TXTWEFTSRNO.Clear()
        CMBWEFTQUALITY.Text = ""
        CMBWEFTSHADE.Text = ""
        TXTWEFTDENIER.Clear()
        TXTWEFTPICK.Clear()
        TXTWEFTWT.Clear()
        TXTTOTALWEFTWT.Clear()
        GRIDWEFT.RowCount = 0
        TXTVALUELOSSPER.Clear()


        TXTSRNO.Clear()
        CMBCOLOR.Text = ""
        GRIDSHADE.RowCount = 0

        GRIDDOUBLECLICK = False
        GRIDPROCESSDOUBLECLICK = False
        GRIDSTORESDOUBLECLICK = False
        GRIDMATCHDOUBLECLICK = False
        GRIDWARPDOUBLECLICK = False
        GRIDWEFTDOUBLECLICK = False

        TXTRATE1.Clear()
        TXTRATE2.Clear()
        TXTRATE3.Clear()
        TXTRATE4.Clear()
        TXTRATE5.Clear()
        TXTRATE6.Clear()
        TXTRATE7.Clear()
        TXTRATE8.Clear()
        TXTRATE9.Clear()
        TXTRATE10.Clear()
        TXTRATE11.Clear()
        TXTRATE12.Clear()
        TXTRATE13.Clear()
        TXTRATE14.Clear()
        TXTRATE15.Clear()

        GRIDCOLOR.RowCount = 0
        GRIDITEM.RowCount = 0
        CMBCOSTCENTERNAME.Text = ""
        TXTGSM.Clear()
        TXTPERCENT.Clear()
        CHKGARMENT.CheckState = CheckState.Unchecked
    End Sub

    Sub FILLGRIDCOLOR()
        Try
            If GRIDSHADEDOUBLECLICK = False Then
                GRIDSHADE.Rows.Add(Val(TXTSRNO.Text.Trim), CMBCOLOR.Text.Trim)
                getsrno(GRIDSHADE)
            ElseIf GRIDSHADEDOUBLECLICK = True Then
                GRIDSHADE.Item(GSRNO.Index, TEMPSHADEROW).Value = Val(TXTSRNO.Text.Trim)
                GRIDSHADE.Item(GCOLOR.Index, TEMPSHADEROW).Value = CMBCOLOR.Text.Trim
                TEMPSHADEROW = GRIDSHADE.CurrentRow.Index
                TXTSRNO.Focus()
                GRIDSHADEDOUBLECLICK = False
            End If
            CMBCOLOR.Text = ""
            GRIDSHADE.ClearSelection()
            CMBCOLOR.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBCOLOR.Validated
        If CMBCOLOR.Text.Trim <> "" Then
            If Not CHECKCOLO() Then
                MsgBox("Color already Present in Grid below")
                Exit Sub
            End If
            FILLGRIDCOLOR()
        End If
    End Sub

    Function CHECKCOLO() As Boolean
        Try
            Dim bln As Boolean = True
            For Each row As DataGridViewRow In GRIDSHADE.Rows
                If (GRIDSHADEDOUBLECLICK = True And TEMPSHADEROW <> row.Index) Or GRIDSHADEDOUBLECLICK = False Then
                    If CMBCOLOR.Text.Trim = row.Cells(GCOLOR.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub CMBWARPQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBWARPQUALITY.Enter
        Try
            If CMBWARPQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBWARPQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBWARPQUALITY.Validating
        Try
            If CMBWARPQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBWARPQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPSHADE_Enter(sender As Object, e As EventArgs) Handles CMBWARPSHADE.Enter
        Try
            If CMBWARPSHADE.Text.Trim = "" Then FILLCOLOR(CMBWARPSHADE, "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBWARPSHADE.Validating
        Try
            If CMBWARPSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBWARPSHADE, e, Me, "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBWEFTQUALITY.Enter
        Try
            If CMBWEFTQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBWEFTQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBWEFTQUALITY.Validating
        Try
            If CMBWEFTQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBWEFTQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTSHADE_Enter(sender As Object, e As EventArgs) Handles CMBWEFTSHADE.Enter
        Try
            If CMBWEFTSHADE.Text.Trim = "" Then FILLCOLOR(CMBWEFTSHADE, "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBWEFTSHADE.Validating
        Try
            If CMBWEFTSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBWEFTSHADE, e, Me, "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWARPTL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTWARPTL.KeyPress, TXTWEFTTL.KeyPress, TXTREEDSPACE.KeyPress, TXTWARPENDS.KeyPress, TXTREED.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTPICKS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTPICKS.KeyPress, TXTWARPWT.KeyPress, TXTWEFTPICK.KeyPress, TXTWEFTWT.KeyPress, TXTPERCENT.KeyPress, TXTGSM.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub CMBWARPQUALITY_Validated(sender As Object, e As EventArgs) Handles CMBWARPQUALITY.Validated
        TXTWARPDENIER.Text = FETCHDENIER(CMBWARPQUALITY.Text.Trim)
        CALC()
    End Sub

    Private Sub CMBWEFTQUALITY_Validated(sender As Object, e As EventArgs) Handles CMBWEFTQUALITY.Validated
        TXTWEFTDENIER.Text = FETCHDENIER(CMBWEFTQUALITY.Text.Trim)
        CALC()
    End Sub

    Sub CALC()
        Try
            If Val(TXTWARPWT.Text.Trim) = 0 And Val(TXTWARPENDS.Text.Trim) > 0 And Val(TXTWARPTL.Text.Trim) > 0 And Val(TXTWARPDENIER.Text.Trim) > 0 Then
                If ClientName = "NAYRA" Then
                    TXTWARPWT.Text = Format((Val(TXTWARPENDS.Text.Trim) * Val(TXTREEDSPACE.Text.Trim) * Val(TXTWARPTL.Text.Trim) * Val(TXTWARPDENIER.Text.Trim)) / 9000000, "0.000")
                Else
                    TXTWARPWT.Text = Format((Val(TXTWARPENDS.Text.Trim) * Val(TXTWARPTL.Text.Trim) * Val(TXTWARPDENIER.Text.Trim)) / 9000000, "0.000")
                End If
            End If

            If CMBWEFTQUALITY.Text.Trim <> "" And Val(TXTWEFTPICK.Text.Trim) > 0 And Val(TXTREEDSPACE.Text.Trim) > 0 And Val(TXTWEFTTL.Text.Trim) > 0 And Val(TXTWEFTWT.Text.Trim) = 0 Then
                If ClientName = "NAYRA" Then
                    TXTWEFTWT.Text = Format(((Val(TXTREEDSPACE.Text.Trim) * Val(TXTWEFTPICK.Text.Trim) * Val(TXTWEFTTL.Text.Trim)) / 1699.33) / Val(TXTWEFTDENIER.Text.Trim), "0.000")
                Else
                    TXTWEFTWT.Text = Format((Val(TXTREEDSPACE.Text.Trim) * Val(TXTWEFTPICK.Text.Trim) * Val(TXTWEFTTL.Text.Trim) * Val(TXTWEFTDENIER.Text.Trim)) / 9000000, "0.000")
                End If
            End If

            TXTWARPAMOUNT.Text = Format(Val(TXTWARPWT.Text.Trim) * Val(TXTWARPRATE.Text.Trim), "0.000")
            TXTWEFTAMOUNT.Text = Format(Val(TXTWEFTWT.Text.Trim) * Val(TXTWEFTRATE.Text.Trim), "0.000")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLWARPGRID()

        If GRIDWARPDOUBLECLICK = False Then
            GRIDWARP.Rows.Add(Val(TXTWARPSRNO.Text.Trim), CMBWARPQUALITY.Text.Trim, CMBWARPSHADE.Text.Trim, Val(TXTWARPDENIER.Text.Trim), Val(TXTWARPENDS.Text.Trim), Val(TXTWARPWT.Text.Trim), Format(Val(TXTWARPRATE.Text.Trim), "0.00"), Format(Val(TXTWARPAMOUNT.Text.Trim), "0.00"))
            getsrno(GRIDWARP)
        ElseIf GRIDWARPDOUBLECLICK = True Then
            GRIDWARP.Item(WSRNO.Index, TEMPWARPROW).Value = Val(TXTWARPSRNO.Text.Trim)
            GRIDWARP.Item(WQUALITY.Index, TEMPWARPROW).Value = CMBWARPQUALITY.Text.Trim
            GRIDWARP.Item(WSHADE.Index, TEMPWARPROW).Value = CMBWARPSHADE.Text.Trim
            GRIDWARP.Item(WDENIER.Index, TEMPWARPROW).Value = Val(TXTWARPDENIER.Text.Trim)
            GRIDWARP.Item(WENDS.Index, TEMPWARPROW).Value = Val(TXTWARPENDS.Text.Trim)
            GRIDWARP.Item(WWT.Index, TEMPWARPROW).Value = Val(TXTWARPWT.Text.Trim)
            GRIDWARP.Item(WRATE.Index, TEMPWARPROW).Value = Format(Val(TXTWARPRATE.Text.Trim), "0.00")
            GRIDWARP.Item(WAMOUNT.Index, TEMPWARPROW).Value = Format(Val(TXTWARPAMOUNT.Text.Trim), "0.00")

            TEMPWARPROW = GRIDWARP.CurrentRow.Index
            GRIDWARPDOUBLECLICK = False
        End If
        TOTAL()
        CMBWARPQUALITY.Text = ""
        CMBWARPSHADE.Text = ""
        TXTWARPDENIER.Clear()
        TXTWARPENDS.Clear()
        TXTWARPWT.Clear()
        TXTWARPRATE.Clear()
        TXTWARPAMOUNT.Clear()
        TXTWARPSRNO.Text = GRIDWARP.RowCount + 1
        CMBWARPQUALITY.Focus()
    End Sub

    Sub FILLWEFTGRID()

        If GRIDWEFTDOUBLECLICK = False Then
            GRIDWEFT.Rows.Add(Val(TXTWEFTSRNO.Text.Trim), CMBWEFTQUALITY.Text.Trim, CMBWEFTSHADE.Text.Trim, Val(TXTWEFTDENIER.Text.Trim), Val(TXTWEFTPICK.Text.Trim), Val(TXTWEFTWT.Text.Trim), Val(TXTWEFTRATE.Text.Trim), Val(TXTWEFTAMOUNT.Text.Trim))
            getsrno(GRIDWEFT)
        ElseIf GRIDWEFTDOUBLECLICK = True Then
            GRIDWEFT.Item(FSRNO.Index, TEMPWEFTROW).Value = Val(TXTWEFTSRNO.Text.Trim)
            GRIDWEFT.Item(FQUALITY.Index, TEMPWEFTROW).Value = CMBWEFTQUALITY.Text.Trim
            GRIDWEFT.Item(FSHADE.Index, TEMPWEFTROW).Value = CMBWEFTSHADE.Text.Trim
            GRIDWEFT.Item(FDENIER.Index, TEMPWEFTROW).Value = Val(TXTWEFTDENIER.Text.Trim)
            GRIDWEFT.Item(FPICK.Index, TEMPWEFTROW).Value = Val(TXTWEFTPICK.Text.Trim)
            GRIDWEFT.Item(FWT.Index, TEMPWEFTROW).Value = Val(TXTWEFTWT.Text.Trim)
            GRIDWEFT.Item(FRATE.Index, TEMPWEFTROW).Value = Format(Val(TXTWEFTRATE.Text.Trim), "0.00")
            GRIDWEFT.Item(FAMOUNT.Index, TEMPWEFTROW).Value = Format(Val(TXTWEFTAMOUNT.Text.Trim), "0.00")

            TEMPWEFTROW = GRIDWEFT.CurrentRow.Index
            GRIDWEFTDOUBLECLICK = False
        End If
        TOTAL()
        CMBWEFTQUALITY.Text = ""
        CMBWEFTSHADE.Text = ""
        TXTWEFTDENIER.Clear()
        TXTWEFTPICK.Clear()
        TXTWEFTWT.Clear()
        TXTWEFTRATE.Clear()
        TXTWEFTAMOUNT.Clear()
        TXTWEFTSRNO.Text = GRIDWEFT.RowCount + 1
        CMBWEFTQUALITY.Focus()
    End Sub

    Function CHECKWARP() As Boolean
        Try
            Dim BLN As Boolean = True
            For Each row As DataGridViewRow In GRIDWARP.Rows
                If (GRIDWARPDOUBLECLICK = True And TEMPWARPROW <> row.Index) Or GRIDWARPDOUBLECLICK = False Then
                    If CMBWARPQUALITY.Text.Trim = row.Cells(WQUALITY.Index).Value And CMBWARPSHADE.Text.Trim = row.Cells(WSHADE.Index).Value Then BLN = False
                End If
            Next
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function CHECKWEFT() As Boolean
        Try
            Dim BLN As Boolean = True
            For Each row As DataGridViewRow In GRIDWEFT.Rows
                If (GRIDWEFTDOUBLECLICK = True And TEMPWEFTROW <> row.Index) Or GRIDWEFTDOUBLECLICK = False Then
                    If CMBWEFTQUALITY.Text.Trim = row.Cells(FQUALITY.Index).Value And CMBWEFTSHADE.Text.Trim = row.Cells(FSHADE.Index).Value Then BLN = False
                End If
            Next
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GRIDWARP_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWARP.CellDoubleClick
        Try
            If e.RowIndex >= 0 AndAlso GRIDWARP.Item(WQUALITY.Index, e.RowIndex).Value <> Nothing Then
                GRIDWARPDOUBLECLICK = True
                TEMPWARPROW = e.RowIndex
                TXTWARPSRNO.Text = Val(GRIDWARP.Item(WSRNO.Index, e.RowIndex).Value)
                CMBWARPQUALITY.Text = GRIDWARP.Item(WQUALITY.Index, e.RowIndex).Value
                CMBWARPSHADE.Text = GRIDWARP.Item(WSHADE.Index, e.RowIndex).Value
                TXTWARPDENIER.Text = GRIDWARP.Item(WDENIER.Index, e.RowIndex).Value
                TXTWARPENDS.Text = GRIDWARP.Item(WENDS.Index, e.RowIndex).Value
                TXTWARPWT.Text = GRIDWARP.Item(WWT.Index, e.RowIndex).Value
                TXTWARPRATE.Text = GRIDWARP.Item(WRATE.Index, e.RowIndex).Value
                TXTWARPAMOUNT.Text = GRIDWARP.Item(WAMOUNT.Index, e.RowIndex).Value
                CMBWARPQUALITY.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFT_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWEFT.CellDoubleClick
        Try
            If e.RowIndex >= 0 AndAlso GRIDWEFT.Item(FQUALITY.Index, e.RowIndex).Value <> Nothing Then
                GRIDWEFTDOUBLECLICK = True
                TEMPWEFTROW = e.RowIndex
                TXTWEFTSRNO.Text = Val(GRIDWEFT.Item(FSRNO.Index, e.RowIndex).Value)
                CMBWEFTQUALITY.Text = GRIDWEFT.Item(FQUALITY.Index, e.RowIndex).Value
                CMBWEFTSHADE.Text = GRIDWEFT.Item(FSHADE.Index, e.RowIndex).Value
                TXTWEFTDENIER.Text = GRIDWEFT.Item(FDENIER.Index, e.RowIndex).Value
                TXTWEFTPICK.Text = GRIDWEFT.Item(FPICK.Index, e.RowIndex).Value
                TXTWEFTWT.Text = GRIDWEFT.Item(FWT.Index, e.RowIndex).Value
                TXTWEFTRATE.Text = GRIDWEFT.Item(FRATE.Index, e.RowIndex).Value
                TXTWEFTAMOUNT.Text = GRIDWEFT.Item(FAMOUNT.Index, e.RowIndex).Value
                CMBWEFTQUALITY.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWARPENDS_Validated(sender As Object, e As EventArgs) Handles TXTWARPENDS.Validated, TXTWEFTPICK.Validated
        CALC()
    End Sub

    Private Sub GRIDWARP_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWARP.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDWARP.RowCount > 0 Then
                If GRIDWARPDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                GRIDWARP.Rows.RemoveAt(GRIDWARP.CurrentRow.Index)
                getsrno(GRIDWARP)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFT_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWEFT.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDWEFT.RowCount > 0 Then
                If GRIDWEFTDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                GRIDWEFT.Rows.RemoveAt(GRIDWEFT.CurrentRow.Index)
                getsrno(GRIDWEFT)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcategory_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcategory.Enter
        Try
            If cmbcategory.Text.Trim = "" Then fillCATEGORY(cmbcategory, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcategory_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcategory.Validating
        Try
            If cmbcategory.Text.Trim <> "" Then CATEGORYVALIDATE(cmbcategory, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbunit_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbunit.Enter
        Try
            If cmbunit.Text.Trim = "" Then fillunit(cmbunit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbunit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbunit.Validating
        Try
            If cmbunit.Text.Trim <> "" Then unitvalidate(cmbunit, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ItemMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Sub FILLCMB()

        Dim objclscommon As New ClsCommonMaster
        Dim dt As DataTable

        fillCATEGORY(cmbcategory, False)
        fillCATEGORY(CMBGREYCATEGORY, False)

        dt = objclscommon.search("item_name", "", "ItemMaster", " AND ITEM_FRMSTRING = '" & frmstring & "' and Item_cmpid = " & CmpId & " and Item_locationid = " & Locationid & " and Item_yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "Item_name"
            cmbitemname.DisplayMember = "Item_name"
            cmbitemname.Text = ""
        End If
        cmbitemname.DataSource = dt


        dt = objclscommon.search("item_CODE", "", "ItemMaster", " AND ITEM_FRMSTRING = '" & frmstring & "' and Item_cmpid = " & CmpId & " and Item_locationid = " & Locationid & " and Item_yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "Item_CODE"
            CMBCODE.DisplayMember = "Item_CODE"
            CMBCODE.Text = ""
        End If
        CMBCODE.DataSource = dt

        If CMBPROCESS.Text.Trim = "" Then FILLPROCESS(CMBPROCESS)
        If CMBHSNCODE.Text.Trim = "" Then FILLHSNITEMDESC(CMBHSNCODE)
        fillYARNQUALITY(CMBWARPQUALITY, EDIT)
        fillYARNQUALITY(CMBWEFTQUALITY, EDIT)
        FILLCOLOR(CMBWARPSHADE, "", "")
        FILLCOLOR(CMBWEFTSHADE, "", "")
        FILLCOLOR(CMBCOLOR, "", "")
        FILLCOLOR(CMBSHADE, "", "")
        FILLCOLOR(CMBGRIDSHADE, "", "")
        fillitemname(CMBGRIDITEMNAME, "")
        FILLDESIGN(CMBGRIDDESIGN, "")

    End Sub

    Private Sub CMBHSNCODE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBHSNCODE.Enter
        Try
            If CMBHSNCODE.Text.Trim = "" Then FILLHSNITEMDESC(CMBHSNCODE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLHSNITEMDESC(ByRef CMBHSNCODE As ComboBox)
        Try
            Dim objclscommon As New ClsCommon
            Dim dt As DataTable

            dt = objclscommon.SEARCH(" ISNULL(HSN_CODE, '') AS HSNCODE ", "", " HSNMASTER ", " AND HSN_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "HSNCODE"
                CMBHSNCODE.DataSource = dt
                CMBHSNCODE.DisplayMember = "HSNCODE"
                CMBHSNCODE.Text = ""
            End If
            CMBHSNCODE.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBHSNCODE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBHSNCODE.Validating
        Try
            If CMBHSNCODE.Text.Trim <> "" Then HSNITEMDESCVALIDATE(CMBHSNCODE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ItemMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            CLEAR()

            cmbitemname.Text = tempItemName
            CMBCODE.Text = tempItemCODE
            If ClientName = "SAFFRON" Then txtremarks.Text = "SPUNTEX MIX QUALITY" Else txtremarks.Clear()
            If ClientName = "SOFTAS" Then CMBHSNCODE.Text = "540782"
            If ClientName = "MOHATUL" Then TXTWIDTH.Text = "58"

            If frmstring = "MERCHANT" Then
                cmbmaterial.Visible = False
                lblmaterial.Visible = False
                cmbmaterial.Text = "Finished Goods"
            End If

            If EDIT = True Then

                Dim objCommon As New ClsCommonMaster
                'Dim dttable As DataTable = objCommon.search("  ITEMMASTER.item_id AS ITEMID, MATERIALTYPEMASTER.material_name AS MATERIALTYPE, ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, ITEMMASTER.item_name AS ITEMNAME, ISNULL(ITEMMASTER.item_code, '') AS ITEMCODE, ISNULL(ITEMMASTER.item_BLOCKED, 0) AS BLOCKED, ISNULL(ITEMMASTER.item_HIDEINDESIGN, 0) AS HIDEINDESIGN,  ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(DEPARTMENTMASTER.DEPARTMENT_name, '') AS DEPARTMENT, ITEMMASTER.item_reorder AS REORDER, ITEMMASTER.ITEM_FOLD AS FOLD, ITEMMASTER.ITEM_RATE AS RATE, ITEMMASTER.ITEM_VALUATIONRATE AS VALUATIONRATE, ITEMMASTER.ITEM_TRANSRATE AS TRANSPORTRATE,ITEMMASTER.ITEM_CHECKRATE AS CHECKINGRATE,ITEMMASTER.ITEM_PACKRATE AS PACKINGRATE,ITEMMASTER.ITEM_DESIGNRATE AS DESIGNRATE, ITEMMASTER.item_upper AS UPPER, ITEMMASTER.item_lower AS LOWER, ISNULL(ITEM_WIDTH,'') AS WIDTH, ISNULL(ITEM_GREYWIDTH,'') AS GREYWIDTH, ISNULL(ITEM_SHRINKFROM,0) AS SHRINKFROM, ISNULL(ITEM_SHRINKTO,0) AS SHRINKTO, ISNULL(ITEM_SELVEDGE,'') AS SELVEDGE, ISNULL(ITEMMASTER.item_remarks, '') AS REMARKS,ITEMMASTER.ITEM_PHOTO AS IMGPATH,ISNULL(ITEM_WARP,'') AS WARP ,ISNULL(ITEM_WEFT,'') AS WEFT, ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS DISPLAYNAME, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(ITEM_WARPTL, 0) AS WARPTL, ISNULL(ITEM_WEFTTL, 0) AS WEFTTL, ISNULL(ITEM_REED, 0) AS REED, ISNULL(ITEM_REEDSPACE, 0) AS REEDSPACE, ISNULL(ITEM_PICKS, 0) AS PICKS, ISNULL(ITEM_WEAVE, '') AS WEAVE, ISNULL(GREYCATEGORYMASTER.category_name, '') AS GREYCATEGORY, ISNULL(ITEMMASTER.ITEM_ACTUALWT, 0) AS ACTUALWT, ISNULL(ITEMMASTER.ITEM_ACTUALAMOUNT, 0) AS ACTUALAMOUNT, ISNULL(ITEMMASTER.ITEM_DHARAPER, 0) AS DHARAPER, ISNULL(ITEMMASTER.ITEM_WASTAGEPER, 0) AS WASTAGEPER, ISNULL(ITEMMASTER.ITEM_WEAVINGCHRGS, 0) AS WEAVINGCHRGS, ISNULL(ITEMMASTER.ITEM_GSTPER, 0) AS GSTPER, ISNULL(ITEMMASTER.ITEM_AMOUNT, 0) AS AMOUNT, ISNULL(ITEMMASTER.ITEM_TOTALGSTPER, 0) AS TOTALGSTPER, ISNULL(ITEMMASTER.ITEM_TOTALAMOUNT, 0) AS TOTALAMOUNT, ISNULL(ITEMMASTER.ITEM_WARPTOTALAMOUNT, 0) AS WARPTOTALAMOUNT, ISNULL(ITEMMASTER.ITEM_WEFTTOTALAMOUNT, 0) AS WEFTTOTALAMOUNT, ISNULL(ITEMMASTER.ITEM_VALUELOSSPER, 0) AS VALUELOSSPER , ISNULL(COSTCENTERMASTER.COSTCENTER_name, '') AS COSTCENTERNAME", "", " ITEMMASTER INNER JOIN MATERIALTYPEMASTER ON ITEMMASTER.item_materialtypeid = MATERIALTYPEMASTER.material_id LEFT OUTER JOIN COSTCENTERMASTER ON ITEMMASTER.ITEM_COSTCENTERNAMEID = COSTCENTERMASTER.COSTCENTER_id LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN UNITMASTER ON ITEMMASTER.item_unitid = UNITMASTER.unit_id LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN CATEGORYMASTER AS GREYCATEGORYMASTER ON ITEMMASTER.ITEM_GREYCATEGORYID = GREYCATEGORYMASTER.category_id LEFT OUTER JOIN DEPARTMENTMASTER ON ITEMMASTER.item_departmentid = DEPARTMENTMASTER.DEPARTMENT_id ", " and ITEMMASTER.Item_Name = '" & tempItemName & "' AND ITEMMASTER.ITEM_FRMSTRING = '" & frmstring & "' and ITEMMASTER.Item_yearid = " & YearId)
                Dim dttable As DataTable = objCommon.search("ITEMMASTER.item_id AS ITEMID, MATERIALTYPEMASTER.material_name AS MATERIALTYPE, ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, ITEMMASTER.item_name AS ITEMNAME,  ISNULL(ITEMMASTER.item_code, '') AS ITEMCODE, ISNULL(ITEMMASTER.ITEM_BLOCKED, 0) AS BLOCKED, ISNULL(ITEMMASTER.ITEM_HIDEINDESIGN, 0) AS HIDEINDESIGN, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT,  ISNULL(DEPARTMENTMASTER.DEPARTMENT_name, '') AS DEPARTMENT, ITEMMASTER.item_reorder AS REORDER, ITEMMASTER.ITEM_FOLD AS FOLD, ITEMMASTER.ITEM_RATE AS RATE,  ITEMMASTER.ITEM_VALUATIONRATE AS VALUATIONRATE, ITEMMASTER.ITEM_TRANSRATE AS TRANSPORTRATE, ITEMMASTER.ITEM_CHECKRATE AS CHECKINGRATE, ITEMMASTER.ITEM_PACKRATE AS PACKINGRATE,  ITEMMASTER.ITEM_DESIGNRATE AS DESIGNRATE, ITEMMASTER.item_upper AS UPPER, ITEMMASTER.item_lower AS LOWER, ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_GREYWIDTH, '')  AS GREYWIDTH, ISNULL(ITEMMASTER.ITEM_SHRINKFROM, 0) AS SHRINKFROM, ISNULL(ITEMMASTER.ITEM_SHRINKTO, 0) AS SHRINKTO, ISNULL(ITEMMASTER.ITEM_SELVEDGE, '') AS SELVEDGE,  ISNULL(ITEMMASTER.item_remarks, '') AS REMARKS, ITEMMASTER.ITEM_PHOTO AS IMGPATH, ISNULL(ITEMMASTER.ITEM_WARP, '') AS WARP, ISNULL(ITEMMASTER.ITEM_WEFT, '') AS WEFT,  ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS DISPLAYNAME, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(ITEMMASTER.ITEM_WARPTL, 0) AS WARPTL, ISNULL(ITEMMASTER.ITEM_WEFTTL, 0) AS WEFTTL,  ISNULL(ITEMMASTER.ITEM_REED, 0) AS REED, ISNULL(ITEMMASTER.ITEM_REEDSPACE, 0) AS REEDSPACE, ISNULL(ITEMMASTER.ITEM_PICKS, 0) AS PICKS, ISNULL(ITEMMASTER.ITEM_WEAVE, '') AS WEAVE,  ISNULL(GREYCATEGORYMASTER.category_name, '') AS GREYCATEGORY, ISNULL(ITEMMASTER.ITEM_ACTUALWT, 0) AS ACTUALWT, ISNULL(ITEMMASTER.ITEM_ACTUALAMOUNT, 0) AS ACTUALAMOUNT,  ISNULL(ITEMMASTER.ITEM_DHARAPER, 0) AS DHARAPER, ISNULL(ITEMMASTER.ITEM_WASTAGEPER, 0) AS WASTAGEPER, ISNULL(ITEMMASTER.ITEM_WEAVINGCHRGS, 0) AS WEAVINGCHRGS,  ISNULL(ITEMMASTER.ITEM_GSTPER, 0) AS GSTPER, ISNULL(ITEMMASTER.ITEM_AMOUNT, 0) AS AMOUNT, ISNULL(ITEMMASTER.ITEM_TOTALGSTPER, 0) AS TOTALGSTPER, ISNULL(ITEMMASTER.ITEM_TOTALAMOUNT, 0)  AS TOTALAMOUNT, ISNULL(ITEMMASTER.ITEM_WARPTOTALAMOUNT, 0) AS WARPTOTALAMOUNT, ISNULL(ITEMMASTER.ITEM_WEFTTOTALAMOUNT, 0) AS WEFTTOTALAMOUNT, ISNULL(ITEMMASTER.ITEM_VALUELOSSPER,  0) AS VALUELOSSPER, ISNULL(COSTCENTERMASTER.COSTCENTER_name, '') AS COSTCENTERNAME, ISNULL(ITEMMASTER.ITEM_GSM, 0) AS GSM, ISNULL(ITEMMASTER.ITEM_PERCENT, 0) AS [PERCENT], ISNULL(ITEMMASTER.ITEM_GARMENT, 0) AS GARMENT , ISNULL(COLORMASTER.COLOR_name,'') AS SHADE ,  ISNULL(ITEMMASTER_SHADEDETAILS.ITEM_SHADESRNO,0) AS SHADESRNO  ", "", "   ITEMMASTER LEFT OUTER JOIN MATERIALTYPEMASTER ON ITEMMASTER.item_materialtypeid = MATERIALTYPEMASTER.material_id LEFT OUTER JOIN ITEMMASTER_SHADEDETAILS ON ITEMMASTER.item_id = ITEMMASTER_SHADEDETAILS.ITEM_ID AND ITEMMASTER.item_yearid = ITEMMASTER_SHADEDETAILS.ITEM_YEARID LEFT OUTER JOIN COLORMASTER ON ITEMMASTER_SHADEDETAILS.ITEM_SHADECOLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN COSTCENTERMASTER ON ITEMMASTER.ITEM_COSTCENTERNAMEID = COSTCENTERMASTER.COSTCENTER_id LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN UNITMASTER ON ITEMMASTER.item_unitid = UNITMASTER.unit_id LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN CATEGORYMASTER AS GREYCATEGORYMASTER ON ITEMMASTER.ITEM_GREYCATEGORYID = GREYCATEGORYMASTER.category_id LEFT OUTER JOIN DEPARTMENTMASTER ON ITEMMASTER.item_departmentid = DEPARTMENTMASTER.DEPARTMENT_id  ", " and ITEMMASTER.Item_Name = '" & tempItemName & "' AND ITEMMASTER.ITEM_FRMSTRING = '" & frmstring & "' and ITEMMASTER.Item_yearid = " & YearId)

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If dttable.Rows.Count > 0 Then
                    For Each ROW As DataRow In dttable.Rows

                        tempItemId = ROW("ITEMID")
                        cmbmaterial.Text = ROW("MATERIALTYPE").ToString
                        cmbcategory.Text = ROW("CATEGORY").ToString
                        CMBGREYCATEGORY.Text = ROW("GREYCATEGORY").ToString
                        TXTDISPLAYNAME.Text = ROW("DISPLAYNAME").ToString
                        cmbitemname.Text = ROW("ITEMNAME").ToString
                        CMBCODE.Text = ROW("ITEMCODE").ToString
                        tempItemCODE = ROW("ITEMCODE").ToString
                        cmbunit.Text = ROW("UNIT").ToString
                        CMBDEPARTMENT.Text = ROW("DEPARTMENT").ToString
                        txtreorder.Text = Val(ROW("REORDER").ToString)
                        TXTFOLD.Text = ROW("FOLD").ToString
                        TXTRATE.Text = Val(ROW("RATE").ToString)
                        TXTVALUATIONRATE.Text = Val(ROW("VALUATIONRATE").ToString)
                        TXTTRANSPORTRATE.Text = Val(ROW("TRANSPORTRATE").ToString)
                        TXTCHECKINGRATE.Text = Val(ROW("CHECKINGRATE").ToString)
                        TXTPACKINGRATE.Text = Val(ROW("PACKINGRATE").ToString)
                        TXTDESIGNRATE.Text = Val(ROW("DESIGNRATE").ToString)
                        txtupper.Text = Val(ROW("UPPER").ToString)
                        txtlower.Text = Val(ROW("LOWER").ToString)
                        CMBHSNCODE.Text = ROW("HSNCODE").ToString
                        CHKBLOCKED.Checked = Convert.ToBoolean(dttable.Rows(0).Item("BLOCKED"))
                        CHKHIDEINDESIGN.Checked = Convert.ToBoolean(dttable.Rows(0).Item("HIDEINDESIGN"))

                        TXTWIDTH.Text = ROW("WIDTH").ToString
                        TXTGREYWIDTH.Text = ROW("GREYWIDTH").ToString
                        TXTSHRINKFROM.Text = Val(ROW("SHRINKFROM"))
                        TXTSHRINKTO.Text = Val(ROW("SHRINKTO"))
                        TXTSELVEDGE.Text = ROW("SELVEDGE").ToString

                        txtremarks.Text = ROW("REMARKS").ToString
                        If IsDBNull(dttable.Rows(0).Item("IMGPATH")) = False Then
                            PBPHOTO.Image = Image.FromStream(New IO.MemoryStream(DirectCast(dttable.Rows(0).Item("IMGPATH"), Byte())))
                            TXTPHOTOIMGPATH.Text = dttable.Rows(0).Item("IMGPATH").ToString
                        Else
                            PBPHOTO.Image = Nothing
                            TXTWARP.Text = ROW("WARP").ToString
                            TXTWEFT.Text = ROW("WEFT").ToString
                        End If


                        TXTWARPTL.Text = Val(dttable.Rows(0).Item("WARPTL"))
                        TXTWEFTTL.Text = Val(dttable.Rows(0).Item("WEFTTL"))
                        TXTREED.Text = Val(dttable.Rows(0).Item("REED"))
                        TXTREEDSPACE.Text = Val(dttable.Rows(0).Item("REEDSPACE"))
                        TXTPICKS.Text = Val(dttable.Rows(0).Item("PICKS"))
                        TXTWEAVE.Text = dttable.Rows(0).Item("WEAVE")
                        TXTACTWT.Text = dttable.Rows(0).Item("ACTUALWT")
                        TXTACTAMOUNT.Text = dttable.Rows(0).Item("ACTUALAMOUNT")
                        TXTDHARAPERCENT.Text = dttable.Rows(0).Item("DHARAPER")
                        TXTWASTAGEPERCENT.Text = dttable.Rows(0).Item("WASTAGEPER")
                        TXTWEAVINGCHGS.Text = dttable.Rows(0).Item("WEAVINGCHRGS")
                        TXTGSTPERCENT.Text = dttable.Rows(0).Item("GSTPER")
                        TXTAMOUNT.Text = dttable.Rows(0).Item("AMOUNT")
                        TXTTOTALGSTPERCENT.Text = dttable.Rows(0).Item("TOTALGSTPER")
                        TXTTOTALAMOUNT.Text = dttable.Rows(0).Item("TOTALAMOUNT")
                        TXTTOTALWARPAMOUNT.Text = dttable.Rows(0).Item("WARPTOTALAMOUNT")
                        TXTTOTALWEFTAMOUNT.Text = dttable.Rows(0).Item("WEFTTOTALAMOUNT")
                        TXTVALUELOSSPER.Text = dttable.Rows(0).Item("VALUELOSSPER")
                        CMBCOSTCENTERNAME.Text = ROW("COSTCENTERNAME").ToString
                        TXTGSM.Text = dttable.Rows(0).Item("GSM")
                        TXTPERCENT.Text = dttable.Rows(0).Item("PERCENT")
                        CHKGARMENT.Checked = Convert.ToBoolean(dttable.Rows(0).Item("GARMENT"))
                        If ROW("SHADE") <> "" Then GRIDCOLOR.Rows.Add(Val(ROW("SHADESRNO")), ROW("SHADE"))

                    Next



                    'CHARGES GRID
                    Dim OBJCMN As New ClsCommon
                    Dim dttable1 As DataTable = OBJCMN.SEARCH(" ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS YARNQUALITY, ISNULL(ITEMMASTER_COMPOSITION.ITEM_PER, 0) AS PER ", "", " YARNQUALITYMASTER INNER JOIN ITEMMASTER_COMPOSITION ON YARNQUALITYMASTER.YARN_ID = ITEMMASTER_COMPOSITION.ITEM_YARNQUALITYID RIGHT OUTER JOIN ITEMMASTER ON ITEMMASTER_COMPOSITION.ITEM_YEARID = ITEMMASTER.item_yearid AND ITEMMASTER_COMPOSITION.ITEM_ID = ITEMMASTER.item_id ", " AND ITEMMASTER_COMPOSITION.ITEM_ID = " & tempItemId & " AND ITEMMASTER_COMPOSITION.ITEM_YEARID = " & YearId)
                    If dttable1.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable1.Rows
                            GRIDCOMP.Rows.Add(DTR("YARNQUALITY"), DTR("PER"))
                        Next
                    End If


                    'PROCESS GRID
                    Dim dt As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER_PROCESS.ITEM_SRNO, 0) AS GRIDSRNO, ISNULL(PROCESSMASTER.PROCESS_NAME, '') AS PROCESS", "", "  PROCESSMASTER LEFT OUTER JOIN ITEMMASTER_PROCESS ON PROCESSMASTER.PROCESS_ID = ITEMMASTER_PROCESS.ITEM_PROCESSID ", " AND ITEMMASTER_PROCESS.ITEM_ID = " & tempItemId & " AND ITEMMASTER_PROCESS.ITEM_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        For Each DTR1 As DataRow In dt.Rows
                            GRIDPROCESS.Rows.Add(DTR1("GRIDSRNO"), DTR1("PROCESS"))
                        Next
                    End If



                    'WARPGRID
                    dt = OBJCMN.SEARCH(" ITEMMASTER_WARPDETAILS.ITEM_WARPSRNO AS SRNO, YARNQUALITYMASTER.YARN_NAME AS YARNQUALITY, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ITEMMASTER_WARPDETAILS.ITEM_WARPENDS AS ENDS, ITEMMASTER_WARPDETAILS.ITEM_WARPWT AS WT, ITEMMASTER_WARPDETAILS.ITEM_WARPRATE AS RATE, ITEMMASTER_WARPDETAILS.ITEM_WARPAMOUNT AS AMOUNT ", "", " ITEMMASTER_WARPDETAILS INNER JOIN YARNQUALITYMASTER ON ITEMMASTER_WARPDETAILS.ITEM_WARPQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN COLORMASTER ON ITEMMASTER_WARPDETAILS.ITEM_WARPSHADEID = COLORMASTER.COLOR_id ", " AND ITEMMASTER_WARPDETAILS.ITEM_ID = " & Val(tempItemId) & " AND ITEMMASTER_WARPDETAILS.ITEM_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        For Each ROW As DataRow In dt.Rows
                            TXTWARPDENIER.Text = FETCHDENIER(ROW("YARNQUALITY"))
                            GRIDWARP.Rows.Add(Val(ROW("SRNO")), ROW("YARNQUALITY"), ROW("SHADE"), Val(TXTWARPDENIER.Text.Trim), Val(ROW("ENDS")), Val(ROW("WT")), Val(ROW("RATE")), Val(ROW("AMOUNT")))
                            TXTWARPDENIER.Clear()
                        Next
                    End If


                    'WEFT
                    dt = OBJCMN.SEARCH(" ITEMMASTER_WEFTDETAILS.ITEM_WEFTSRNO AS SRNO, YARNQUALITYMASTER.YARN_NAME AS YARNQUALITY, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ITEMMASTER_WEFTDETAILS.ITEM_WEFTPICK AS PICK, ITEMMASTER_WEFTDETAILS.ITEM_WEFTWT AS WT, ITEMMASTER_WEFTDETAILS.ITEM_WEFTRATE AS RATE, ITEMMASTER_WEFTDETAILS.ITEM_WEFTAMOUNT AS AMOUNT ", "", " ITEMMASTER_WEFTDETAILS INNER JOIN YARNQUALITYMASTER ON ITEMMASTER_WEFTDETAILS.ITEM_WEFTQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN COLORMASTER ON ITEMMASTER_WEFTDETAILS.ITEM_WEFTSHADEID = COLORMASTER.COLOR_id ", " AND ITEMMASTER_WEFTDETAILS.ITEM_ID = " & Val(tempItemId) & " AND ITEMMASTER_WEFTDETAILS.ITEM_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        For Each ROW As DataRow In dt.Rows
                            TXTWEFTDENIER.Text = FETCHDENIER(ROW("YARNQUALITY"))
                            GRIDWEFT.Rows.Add(Val(ROW("SRNO")), ROW("YARNQUALITY"), ROW("SHADE"), Val(TXTWEFTDENIER.Text.Trim), Val(ROW("PICK")), Val(ROW("WT")), Val(ROW("RATE")), Val(ROW("AMOUNT")))
                            TXTWEFTDENIER.Clear()
                        Next
                    End If


                    'COLOR GRID
                    dt = OBJCMN.SEARCH(" ISNULL(ITEMMASTER_COLOR.ITEM_SRNO, 0) AS GRIDSRNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR", "", " ITEMMASTER_COLOR LEFT OUTER JOIN COLORMASTER ON ITEMMASTER_COLOR.ITEM_COLORID = COLORMASTER.COLOR_id ", " AND ITEMMASTER_COLOR.ITEM_ID = " & Val(tempItemId) & " AND ITEMMASTER_COLOR.ITEM_YEARID = " & YearId & " ORDER BY ITEMMASTER_COLOR.ITEM_SRNO")
                    If dt.Rows.Count > 0 Then
                        For Each DTR1 As DataRow In dt.Rows
                            GRIDSHADE.Rows.Add(DTR1("GRIDSRNO"), DTR1("COLOR"))
                        Next
                    End If

                    'ITEMMASTER_SHADEDETAILS GRID
                    'Dim dtt As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER_SHADEDETAILS.ITEM_SHADESRNO, 0) AS SHADESRNO, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE ", "", " ITEMMASTER_SHADEDETAILS LEFT OUTER JOIN COLORMASTER ON ITEMMASTER_SHADEDETAILS.ITEM_SHADECOLORID = COLORMASTER.COLOR_id  ", " AND ITEMMASTER_SHADEDETAILS.ITEM_ID = " & Val(tempItemId) & " AND ITEMMASTER_SHADEDETAILS.ITEM_YEARID = " & YearId & " ORDER BY ITEMMASTER_SHADEDETAILS.ITEM_SHADESRNO")
                    'If dtt.Rows.Count > 0 Then
                    '    For Each DTR1 As DataRow In dtt.Rows
                    '        GRIDCOLOR.Rows.Add(DTR1("SHADESRNO"), DTR1("SHADE"))
                    '    Next
                    'End If

                    ''ITEMMASTER_SHADEITEMDETAILS GRID
                    Dim dtt As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER_SHADEITEMDETAILS.ITEM_SHADEITEMSRNO, 0) AS SHADEITEMSRNO,ISNULL(ITEMMASTER_SHADEITEMDETAILS.ITEM_SHADESRNO, 0) AS SHADESRNO, ISNULL(ITEMMASTER.ITEM_name, '') AS ITEM, ISNULL(DESIGNMASTER.DESIGN_nO, '') AS ITEMDESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS ITEMSHADE, ISNULL(ITEMMASTER_SHADEITEMDETAILS.ITEM_SHADEMTRS,0)AS SHADEMTRS ", "", " ITEMMASTER_SHADEITEMDETAILS LEFT OUTER JOIN COLORMASTER ON ITEMMASTER_SHADEITEMDETAILS.ITEM_SHADECOLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ITEMMASTER_SHADEITEMDETAILS.ITEM_SHADEDESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON ITEMMASTER_SHADEITEMDETAILS.ITEM_SHADEITEMID = ITEMMASTER.item_id  ", "  AND ITEMMASTER_SHADEITEMDETAILS.ITEM_ID = " & Val(tempItemId) & " AND ITEMMASTER_SHADEITEMDETAILS.ITEM_YEARID = " & YearId & " ORDER BY ITEMMASTER_SHADEITEMDETAILS.ITEM_SHADEITEMSRNO")
                    If dtt.Rows.Count > 0 Then
                        For Each DTR1 As DataRow In dtt.Rows
                            DT_ITEMDETAILS.Rows.Add(DTR1("SHADEITEMSRNO"), DTR1("ITEM"), DTR1("ITEMDESIGN"), DTR1("ITEMSHADE"), Format(Val(DTR1("SHADEMTRS")), "0.00"), DTR1("SHADESRNO"))
                        Next
                    End If

                    'GET RATE FROM ITEMPRICELIST
                    Dim DTRATE As DataTable = objCommon.search("  RATE1, RATE2, RATE3, RATE4, RATE5, RATE6, RATE7, RATE8, RATE9, RATE10, RATE11, RATE12, RATE13, RATE14, RATE15 ", "", " ITEMPRICELIST ", " AND ITEMID = " & dttable.Rows(0).Item("ITEMID") & " AND YEARID = " & YearId)
                    If DTRATE.Rows.Count > 0 Then
                        TXTRATE1.Text = Val(DTRATE.Rows(0).Item("RATE1"))
                        TXTRATE2.Text = Val(DTRATE.Rows(0).Item("RATE2"))
                        TXTRATE3.Text = Val(DTRATE.Rows(0).Item("RATE3"))
                        TXTRATE4.Text = Val(DTRATE.Rows(0).Item("RATE4"))
                        TXTRATE5.Text = Val(DTRATE.Rows(0).Item("RATE5"))
                        TXTRATE6.Text = Val(DTRATE.Rows(0).Item("RATE6"))
                        TXTRATE7.Text = Val(DTRATE.Rows(0).Item("RATE7"))
                        TXTRATE8.Text = Val(DTRATE.Rows(0).Item("RATE8"))
                        TXTRATE9.Text = Val(DTRATE.Rows(0).Item("RATE9"))
                        TXTRATE10.Text = Val(DTRATE.Rows(0).Item("RATE10"))
                        TXTRATE11.Text = Val(DTRATE.Rows(0).Item("RATE11"))
                        TXTRATE12.Text = Val(DTRATE.Rows(0).Item("RATE12"))
                        TXTRATE13.Text = Val(DTRATE.Rows(0).Item("RATE13"))
                        TXTRATE14.Text = Val(DTRATE.Rows(0).Item("RATE14"))
                        TXTRATE15.Text = Val(DTRATE.Rows(0).Item("RATE15"))
                    End If

                    TOTAL()
                End If

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Function FETCHDENIER(QUALITYNAME As String) As Double
        Try
            Dim DENIER As Double = 0
            If QUALITYNAME <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.Execute_Any_String("SELECT ISNULL(YARN_DENIER,0) AS DENIER FROM YARNQUALITYMASTER WHERE YARN_NAME = '" & QUALITYNAME & "' AND YARN_YEARID = " & YearId, "", "")
                If DT.Rows.Count > 0 Then DENIER = Val(DT.Rows(0).Item("DENIER"))
            End If
            Return DENIER
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbitemname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitemname.Enter, CMBGRIDITEMNAME.Enter
        Try
            If cmbitemname.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("item_name", "", " ItemMaster ", " and ITEM_FRMSTRING = '" & frmstring & "' and Item_cmpid = " & CmpId & " and Item_locationid = " & Locationid & " and Item_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "Item_name"
                    cmbitemname.DataSource = dt
                    cmbitemname.DisplayMember = "Item_name"
                    cmbitemname.Text = ""
                End If
                cmbitemname.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitemname.Validated
        Try
            If CMBCODE.Text.Trim = "" And cmbitemname.Text.Trim <> "" Then CMBCODE.Text = cmbitemname.Text.Trim
            If TXTDISPLAYNAME.Text.Trim = "" And cmbitemname.Text.Trim <> "" Then TXTDISPLAYNAME.Text = cmbitemname.Text.Trim
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbitemname.Validating, CMBGRIDITEMNAME.Validating
        If cmbitemname.Text.Trim <> "" Then
            uppercase(cmbitemname)
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable
            If (EDIT = False) Or (EDIT = True And LCase(cmbitemname.Text) <> LCase(tempItemName)) Then
                dt = objclscommon.search("item_name", "", "ItemMaster", " and item_name = '" & cmbitemname.Text.Trim & "'  And item_cmpid = " & CmpId & " And item_locationid = " & Locationid & " And item_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Item Name Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        '**** code for to delete the selected imtem from item master *****
        ' ****Logic 
        ' looking for in SalesOrder_Desc Table if Item master Name is Exists OR Not
        If USERDELETE = False Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If
        If cmbitemname.Text.Trim = "" Then
            MsgBox("Item Name Can Not Be Blank ")
            Exit Sub
        End If

        If EDIT = False Then
            'since user can delete Master only in edit mode
            MsgBox("Item Name Can Delete only in Edit Mode", MsgBoxStyle.Critical, "TEXTRADE")
            Exit Sub
        End If
        If cmbitemname.Text.Trim <> "" Then
            pcase(cmbitemname)
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable

            dt = objclscommon.search("item_name", "", " dbo.ITEMMASTER RIGHT OUTER JOIN  dbo.SALEORDER_DESC ON dbo.ITEMMASTER.item_id = dbo.SALEORDER_DESC.so_itemid ", " and item_name = '" & cmbitemname.Text.Trim & "' AND item_yearid = " & YearId)
            If dt.Rows.Count > 0 Then
                MsgBox("Item Name Already Used in Transaction Forms", MsgBoxStyle.Critical, "TEXTRADE")
                Exit Sub
            End If

            dt = objclscommon.search("ITEMNAME", "", " BARCODESTOCK ", " and ITEMNAME = '" & cmbitemname.Text.Trim & "' AND YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                MsgBox("Item Name Already Used in Transaction Forms", MsgBoxStyle.Critical, "TEXTRADE")
                Exit Sub
            End If

            dt = objclscommon.search("ITEMNAME", "", " OUTBARCODESTOCK ", " and ITEMNAME = '" & cmbitemname.Text.Trim & "' AND YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                MsgBox("Item Name Already Used in Transaction Forms", MsgBoxStyle.Critical, "TEXTRADE")
                Exit Sub
            End If

        End If
        'Dim tempMsg As Integer
        ''if above all conditions are false then only user can delete Particular Master
        If MsgBox("Delete Item Name ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim alParaval As New ArrayList
            alParaval.Add(cmbitemname.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            Dim clsitemst As New clsItemmaster
            clsitemst.alParaval = alParaval
            IntResult = clsitemst.Delete()
            MsgBox("Item Deleted")
            CLEAR()
            EDIT = False
        End If

    End Sub

    Private Sub CMBDEPARTMENT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDEPARTMENT.Enter
        Try
            If CMBDEPARTMENT.Text.Trim = "" Then filldepartment(CMBDEPARTMENT, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDEPARTMENT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDEPARTMENT.Validating
        Try
            If CMBDEPARTMENT.Text.Trim <> "" Then DEPARTMENTVALIDATE(CMBDEPARTMENT, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCODE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCODE.Enter
        Try
            If CMBCODE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("item_CODE", "", " ItemMaster ", " and ITEM_FRMSTRING = '" & frmstring & "' and Item_cmpid = " & CmpId & " and Item_locationid = " & Locationid & " and Item_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "Item_CODE"
                    CMBCODE.DataSource = dt
                    CMBCODE.DisplayMember = "Item_CODE"
                    CMBCODE.Text = ""
                End If
                CMBCODE.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBCODE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCODE.Validating
        Try
            If CMBCODE.Text.Trim <> "" Then
                uppercase(CMBCODE)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                If (EDIT = False) Or (EDIT = True And LCase(CMBCODE.Text) <> LCase(tempItemCODE)) Then
                    dt = objclscommon.search("item_CODE", "", "ItemMaster", " and item_CODE = '" & CMBCODE.Text.Trim & "' And item_cmpid = " & CmpId & " And item_locationid = " & Locationid & " And item_yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Item Code Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtrate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTGRIDRATE.KeyPress, TXTRATE.KeyPress, TXTSHRINKFROM.KeyPress, TXTSHRINKTO.KeyPress, TXTPER.KeyPress, TXTVALUATIONRATE.KeyPress, TXTVALUELOSSPER.KeyPress, TXTRATE1.KeyPress, TXTRATE2.KeyPress, TXTRATE3.KeyPress, TXTRATE4.KeyPress, TXTRATE5.KeyPress, TXTRATE6.KeyPress, TXTRATE6.KeyPress, TXTRATE7.KeyPress, TXTRATE8.KeyPress, TXTRATE8.KeyPress, TXTRATE9.KeyPress, TXTRATE10.KeyPress, TXTRATE11.KeyPress, TXTRATE12.KeyPress, TXTRATE13.KeyPress, TXTRATE14.KeyPress, TXTRATE15.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub txtrate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTGRIDRATE.Validating
        Try
            If Val(TXTGRIDRATE.Text.Trim) > 0 And cmbratetype.Text.Trim <> "" Then
                If Not checkRATETYPE() Then
                    MsgBox("Rate already Present in Grid below")
                    Exit Sub
                End If

                fillgrid()
                cmbratetype.Text = ""
                TXTGRIDRATE.Clear()
                cmbratetype.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function checkRATETYPE() As Boolean
        Try
            Dim bln As Boolean = True
            For Each row As DataGridViewRow In GRIDRATE.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> row.Index) Or GRIDDOUBLECLICK = False Then
                    If cmbratetype.Text.Trim = row.Cells(gratetype.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub fillgrid()

        If GRIDDOUBLECLICK = False Then
            GRIDRATE.Rows.Add(cmbratetype.Text.Trim, Val(TXTGRIDRATE.Text.Trim))
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDRATE.Item("GRATETYPE", TEMPROW).Value = cmbratetype.Text.Trim
            GRIDRATE.Item("GRATE", TEMPROW).Value = Val(TXTGRIDRATE.Text.Trim)
            GRIDDOUBLECLICK = False
        End If

        GRIDRATE.ClearSelection()

    End Sub

    Private Sub GRIDRATE_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDRATE.CellDoubleClick
        Try
            If e.RowIndex >= 0 And GRIDRATE.Item("GRATETYPE", e.RowIndex).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TEMPROW = e.RowIndex
                cmbratetype.Text = GRIDRATE.Item("GRATETYPE", e.RowIndex).Value
                TXTGRIDRATE.Text = GRIDRATE.Item("GRATE", e.RowIndex).Value
                cmbratetype.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDRATE_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDRATE.KeyDown
        If e.KeyCode = Keys.Delete Then
            GRIDRATE.Rows.RemoveAt(GRIDRATE.CurrentRow.Index)
        End If
    End Sub

    Private Sub CMDPHOTOUPLOAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPHOTOUPLOAD.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpeg;*.png"
        OpenFileDialog1.ShowDialog()
        TXTPHOTOIMGPATH.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTPHOTOIMGPATH.Text.Trim.Length <> 0 Then PBPHOTO.ImageLocation = TXTPHOTOIMGPATH.Text.Trim
    End Sub

    Private Sub CMDPHOTOREMOVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPHOTOREMOVE.Click
        Try
            PBPHOTO.Image = Nothing
            TXTPHOTOIMGPATH.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPHOTOVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPHOTOVIEW.Click
        Try
            If TXTPHOTOIMGPATH.Text.Trim <> "" Then
                If Path.GetExtension(TXTPHOTOIMGPATH.Text.Trim) = ".pdf" Then
                    System.Diagnostics.Process.Start(TXTPHOTOIMGPATH.Text.Trim)
                Else
                    Dim objVIEW As New ViewImage
                    objVIEW.pbsoftcopy.Image = PBPHOTO.Image
                    objVIEW.ShowDialog()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBHSNCODE_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBHSNCODE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectHSN
                OBJLEDGER.STRSEARCH = " AND HSN_TYPE='GOODS'"
                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then TXTHSNCODE.Text = OBJLEDGER.TEMPCODE

                If OBJLEDGER.TEMPCODE <> "" Then CMBHSNCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPCODEDESC <> "" And ClientName = "SAFFRON" Then
                    txtremarks.Text = OBJLEDGER.TEMPCODEDESC
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ItemMaster_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                LBLDEPT.Visible = False
                CMBDEPARTMENT.Visible = False
                lblcategory.Visible = False
                cmbcategory.Visible = False

                txtreorder.Visible = False
                If ClientName = "SANGHVI" Then
                    LBLFOLD.Visible = False
                    TXTFOLD.Visible = False
                    LBLSELVEDGE.Text = "Tally Item Name"
                    TXTRATE.Visible = False
                    LBLREORDER.Visible = False
                End If
                GPRATE.Visible = False
                LBLUNIT.Visible = False
                cmbunit.Visible = False
                LBLUPPER.Visible = False
                txtupper.Visible = False
                LBLLOWER.Visible = False
                txtlower.Visible = False
                LBLOPRATE.Visible = False
                LBLDISPLAYNAME.Text = "Mill No"
            End If
            If ClientName = "SUPEEMA" Then
                GRPITEMDETAILS.Visible = True
            End If
            If ClientName = "TINUMINU" Then
                LBLREORDER.Text = "Area"
                LBLOPRATE.Visible = True
                txtreorder.Visible = True
            End If

            If ClientName = "SNCM" Then
                LBLGREYCATEGORY.Text = "Sub Category"
            End If

            If ClientName = "MAHAVIR" Or ClientName = "BARKHA" Or ClientName = "MAHAJAN" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Or ClientName = "SMS" Or ClientName = "MINALFAB" Then LBLREORDER.Text = "Cut"
            If ClientName = "MASHOK" Then
                LBLREORDER.Text = "Day Prod"
            End If

            If ClientName <> "MOHAN" And ClientName <> "MARKIN" And ClientName <> "KARAN" And ClientName <> "SPCORP" And ClientName <> "VALIANT" Then GRPCOMPOSITION.Visible = False
            If ClientName = "KARAN" Or ClientName = "TINUMINU" Then LBLFOLD.Text = "Stitches"
            If ClientName <> "YASHVI" Then
                GPPROCESS.Visible = False
            End If



            If ClientName = "ALENCOT" Then
                cmbcategory.BackColor = Color.LemonChiffon
                txtremarks.BackColor = Color.LemonChiffon
                TXTWIDTH.BackColor = Color.LemonChiffon
            End If

            If ClientName = "AVIS" Then
                cmbcategory.BackColor = Color.LemonChiffon
                GPPROCESS.Visible = False
                LBLSHRINKFROM.Text = "Shrinkage %"
                TXTVALUELOSSPER.BackColor = Color.LemonChiffon
                LBLCOSTCENTER.Visible = True
                CMBCOSTCENTERNAME.Visible = True
            End If

            If ClientName = "YASHVI" Or ClientName = "AVIS" Then
                TXTWARP.Visible = False
                TXTWEFT.Visible = False
                TXTSELVEDGE.Visible = False
                LBLWARP.Visible = False
                Label10.Visible = False
                LBLSELVEDGE.Visible = False
                If ClientName = "YASHVI" Then
                    PBPHOTO.Visible = True
                    CMDPHOTOUPLOAD.Visible = True
                    CMDPHOTOREMOVE.Visible = True
                    CMDPHOTOVIEW.Visible = True
                    TXTTOTALWT.ReadOnly = False
                    TXTTOTALWT.BackColor = Color.LemonChiffon
                End If
            End If

            If ClientName = "NAYRA" Or ClientName = "AKASHDEEP" Or ClientName = "VSTRADERS" Then
                LBLSELVEDGE.Text = "Composition"
                GPWARPWEFT.Visible = True
                GPWARPWEFTCALC.Visible = True
                If ClientName = "NAYRA" Then
                    WENDS.HeaderText = "Reed"
                    FPICK.HeaderText = "Pick"
                End If
            End If

            If ClientName = "SUPRIYA" Then
                LBLWEAVE.Text = "Count"
                LBLSELVEDGE.Text = "Construction"
            End If

            If ClientName = "KDFAB" Then CHKHIDEINDESIGN.Text = "Nett Rate"

            If FETCHITEMWISESHADE = True Then GRPSHADE.Visible = True

            If ClientName = "VINTAGEINDIA" Then
                TXTGSM.Visible = True
                TXTPERCENT.Visible = True
                LBLGSM.Visible = True
                LBLPERCENT.Visible = True
                TXTTOTALWT.ReadOnly = False
                TXTTOTALWT.BackColor = Color.White
                CMBCODE.Enabled = False
                Label18.Enabled = False
                txtupper.Enabled = False
                txtlower.Enabled = False
                LBLUPPER.Enabled = False
                LBLLOWER.Enabled = False
                CMBCODE.TabStop = False
                TXTRATE.Enabled = False
                LBLOPRATE.Enabled = False
                txtreorder.Enabled = False
                LBLREORDER.Enabled = False
                TXTFOLD.Enabled = False
                LBLFOLD.Enabled = False
                TXTTRANSPORTRATE.Enabled = False
                Label8.Enabled = False
                TXTPACKINGRATE.Enabled = False
                Label13.Enabled = False
                TXTCHECKINGRATE.Enabled = False
                Label11.Visible = False
                TXTDESIGNRATE.Enabled = False
                Label12.Enabled = False
                TXTVALUELOSSPER.Enabled = False
                Label26.Enabled = False
                TXTDISPLAYNAME.Enabled = False
                LBLDISPLAYNAME.Enabled = False
                TXTRATE1.Enabled = False
                TXTRATE2.Enabled = False
                TXTRATE3.Enabled = False
                TXTRATE4.Enabled = False
                TXTRATE5.Enabled = False
                TXTRATE6.Enabled = False
                TXTRATE7.Enabled = False
                TXTRATE8.Enabled = False
                TXTRATE9.Enabled = False
                TXTRATE10.Enabled = False
                TXTRATE11.Enabled = False
                TXTRATE12.Enabled = False
                TXTRATE13.Enabled = False
                TXTRATE14.Enabled = False
                TXTRATE15.Enabled = False
                Label41.Enabled = False
                Label39.Enabled = False
                Label38.Enabled = False
                Label37.Enabled = False
                Label36.Enabled = False
                Label47.Enabled = False
                Label46.Enabled = False
                Label45.Enabled = False
                Label44.Enabled = False
                Label42.Enabled = False
                Label28.Enabled = False
                Label9.Enabled = False
                Label7.Enabled = False
                Label5.Enabled = False
                Label27.Enabled = False
                cmbcategory.TabStop = False
                LBLGREYCATEGORY.Text = "Sub Category"
                LBLWARP.Enabled = False
                TXTWARP.TabStop = False
                TXTWARP.Enabled = False
                LBLWARPTL.Enabled = False
                TXTWARPTL.TabStop = False
                TXTWARPTL.Enabled = False
                Label21.Enabled = False
                TXTREEDSPACE.TabStop = False
                TXTREEDSPACE.Enabled = False
                LBLWEFTTL.Enabled = False
                TXTWEFTTL.TabStop = False
                TXTWEFTTL.Enabled = False
                Label23.Enabled = False
                TXTREED.TabStop = False
                TXTREED.Enabled = False
                Label22.Enabled = False
                TXTPICKS.Enabled = False
                Label10.Enabled = False
                TXTWEFT.TabStop = False
                TXTWEFT.Enabled = False

            End If
            If ClientName = "SNCM" Then
                cmbunit.BackColor = Color.LemonChiffon
                cmbcategory.BackColor = Color.LemonChiffon
                CMBGREYCATEGORY.BackColor = Color.LemonChiffon
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBYARNQUALITY.Enter
        Try
            If CMBYARNQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBYARNQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBYARNQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBYARNQUALITY.Validating
        Try
            If CMBYARNQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBYARNQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTPER.Validating
        Try
            If Val(TXTPER.Text.Trim) < 0 And Val(TXTPER.Text.Trim) > 100 Then e.Cancel = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTPER.Validated
        Try
            If Val(TXTPER.Text.Trim) > 0 And CMBYARNQUALITY.Text.Trim <> "" Then
                If Not checkPERTYPE() Then
                    MsgBox("% already Present in Grid below")
                    Exit Sub
                End If

                fillgridCOMP()
                TOTAL()

                CMBYARNQUALITY.Text = ""
                TXTPER.Clear()
                CMBYARNQUALITY.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgridCOMP()

        If GRIDDOUBLECLICK = False Then
            GRIDCOMP.Rows.Add(CMBYARNQUALITY.Text.Trim, Val(TXTPER.Text.Trim))
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDCOMP.Item("GYARNQUALITY", TEMPROW).Value = CMBYARNQUALITY.Text.Trim
            GRIDCOMP.Item("GPER", TEMPROW).Value = Val(TXTPER.Text.Trim)
            GRIDDOUBLECLICK = False
        End If

        TOTAL()
        CMBYARNQUALITY.Text = ""
        TXTPER.Clear()

        GRIDCOMP.ClearSelection()

    End Sub

    Function checkPERTYPE() As Boolean
        Try
            Dim bln As Boolean = True
            For Each row As DataGridViewRow In GRIDCOMP.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> row.Index) Or GRIDDOUBLECLICK = False Then
                    If CMBYARNQUALITY.Text.Trim = row.Cells(GYARNQUALITY.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GRIDCOMP_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCOMP.CellDoubleClick
        Try
            If e.RowIndex >= 0 And GRIDCOMP.Item("GYARNQUALITY", e.RowIndex).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TEMPROW = e.RowIndex
                CMBYARNQUALITY.Text = GRIDCOMP.Item("GYARNQUALITY", e.RowIndex).Value
                TXTPER.Text = GRIDCOMP.Item("GPER", e.RowIndex).Value
                CMBYARNQUALITY.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCOMP_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCOMP.KeyDown
        If e.KeyCode = Keys.Delete Then
            GRIDCOMP.Rows.RemoveAt(GRIDCOMP.CurrentRow.Index)
        End If
    End Sub

    Sub TOTAL()
        Try
            TXTTOTALPER.Text = "0.00"
            TXTTOTALWARPWT.Clear()
            TXTTOTALWEFTWT.Clear()
            TXTTOTALWARPAMOUNT.Clear()
            TXTTOTALWEFTAMOUNT.Clear()
            TXTACTAMOUNT.Clear()
            TXTDHARAAMOUNT.Clear()
            TXTWASTAGEAMOUNT.Clear()
            TXTWEAVINGAMOUNT.Clear()
            TXTGSTPERCENT.Clear()
            TXTGSTAMOUNT.Clear()
            TXTTOTALGSTPERCENT.Clear()
            TXTTOTALMTRS.Clear()
            For Each ROW As DataGridViewRow In GRIDCOMP.Rows
                TXTTOTALPER.Text = Format(Val(TXTTOTALPER.Text) + Val(ROW.Cells(GPER.Index).EditedFormattedValue), "0.00")
            Next

            For Each ROW As DataGridViewRow In GRIDWARP.Rows
                TXTTOTALWARPWT.Text = Format(Val(TXTTOTALWARPWT.Text.Trim) + Val(ROW.Cells(WWT.Index).Value), "0.00")
                TXTTOTALWARPAMOUNT.Text = Format(Val(TXTTOTALWARPAMOUNT.Text.Trim) + Val(ROW.Cells(WAMOUNT.Index).Value), "0.00")
            Next

            For Each ROW As DataGridViewRow In GRIDWEFT.Rows
                TXTTOTALWEFTWT.Text = Format(Val(TXTTOTALWEFTWT.Text.Trim) + Val(ROW.Cells(FWT.Index).Value), "0.00")
                TXTTOTALWEFTAMOUNT.Text = Format(Val(TXTTOTALWEFTAMOUNT.Text.Trim) + Val(ROW.Cells(FAMOUNT.Index).Value), "0.00")
            Next
            For Each ROW As DataGridViewRow In GRIDITEM.Rows
                TXTTOTALMTRS.Text = Format(Val(TXTTOTALMTRS.Text.Trim) + Val(ROW.Cells(GMTRS.Index).Value), "0.00")
            Next

            TXTTOTALWT.Text = Format(Val(TXTTOTALWARPWT.Text.Trim) + Val(TXTTOTALWEFTWT.Text.Trim), "0.00")
            TXTTOTALAMOUNT.Text = Format(Val(TXTTOTALWARPAMOUNT.Text.Trim) + Val(TXTTOTALWEFTAMOUNT.Text.Trim), "0.00")

            If Val(TXTACTWT.Text.Trim) > 0 Then
                TXTACTAMOUNT.Text = Format(Val(TXTACTWT.Text.Trim) * Val(TXTTOTALAMOUNT.Text.Trim) / Val(TXTTOTALWT.Text.Trim), "0.00")
            End If

            TXTDHARAAMOUNT.Text = Format(Val(TXTDHARAPERCENT.Text.Trim) * Val(TXTACTAMOUNT.Text.Trim) / 100, "0.00")
            TXTWASTAGEAMOUNT.Text = Format(Val(TXTWASTAGEPERCENT.Text.Trim) * Val(TXTACTAMOUNT.Text.Trim) / 100, "0.00")
            TXTWEAVINGAMOUNT.Text = Format(Val(TXTPICKS.Text.Trim) * Val(TXTWEAVINGCHGS.Text.Trim), "0.00")

            Dim OBJCMN As New ClsCommonMaster
            Dim dttable As DataTable = OBJCMN.search(" TOP 1 ISNULL(HSNMASTER_DESC.HSN_IGST, 0) AS IGST ", "", " HSNMASTER INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID ", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Now.Date, "MM/dd/yyyy") & "' AND HSNMASTER.HSN_CODE= '" & CMBHSNCODE.Text.Trim & "' AND HSNMASTER.HSN_YEARID=" & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")
            If dttable.Rows.Count > 0 Then TXTGSTPERCENT.Text = Val(dttable.Rows(0).Item("IGST"))

            TXTGSTAMOUNT.Text = Format((TXTWEAVINGAMOUNT.Text.Trim) * Val(TXTGSTPERCENT.Text.Trim) / 100, "0.00")
            TXTAMOUNT.Text = Format(Val(TXTACTAMOUNT.Text.Trim) + Val(TXTDHARAAMOUNT.Text.Trim) + Val(TXTWASTAGEAMOUNT.Text.Trim) + Val(TXTWEAVINGAMOUNT.Text.Trim) + Val(TXTGSTAMOUNT.Text.Trim), "0.00")
            TXTTOTALGSTPERCENT.Text = Format(Val(TXTGSTPERCENT.Text.Trim) * Val(TXTAMOUNT.Text.Trim) / 100, "0.00")

        Catch ex As Exception
            Throw ex
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

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillPROCESSgrid()

        If GRIDPROCESSDOUBLECLICK = False Then
            GRIDPROCESS.Rows.Add(Val(TXTPSRNO.Text.Trim), CMBPROCESS.Text.Trim)
            getsrno(GRIDPROCESS)
        ElseIf GRIDPROCESSDOUBLECLICK = True Then
            GRIDPROCESS.Item("PSRNO", TEMPPROW).Value = Val(TXTPSRNO.Text.Trim)
            GRIDPROCESS.Item("PPROCESS", TEMPPROW).Value = CMBPROCESS.Text.Trim
            TEMPPROW = GRIDPROCESS.CurrentRow.Index
            TXTPSRNO.Focus()
            GRIDPROCESSDOUBLECLICK = False
        End If
        CMBPROCESS.Text = ""
        GRIDPROCESS.ClearSelection()

    End Sub

    Private Sub CMBPROCESS_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBPROCESS.Validated
        If CMBPROCESS.Text.Trim <> "" Then
            fillPROCESSgrid()
        Else
            If CMBPROCESS.Text.Trim = "" Then
                MsgBox("Enter Process Name....", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub TXTPSRNO_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTPSRNO.GotFocus
        TXTPSRNO.Text = Val(GRIDPROCESS.RowCount + 1)
    End Sub

    Private Sub GRIDPROCESS_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDPROCESS.CellDoubleClick
        Try
            If e.RowIndex >= 0 AndAlso GRIDPROCESS.Item("PPROCESS", e.RowIndex).Value <> Nothing Then
                GRIDPROCESSDOUBLECLICK = True
                TEMPPROW = e.RowIndex
                TXTPSRNO.Text = Val(GRIDPROCESS.Item("PSRNO", e.RowIndex).Value)
                CMBPROCESS.Text = GRIDPROCESS.Item("PPROCESS", e.RowIndex).Value
                CMBPROCESS.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDPROCESS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDPROCESS.KeyDown
        If e.KeyCode = Keys.Delete Then
            GRIDPROCESS.Rows.RemoveAt(GRIDPROCESS.CurrentRow.Index)
            getsrno(GRIDPROCESS)
        End If
    End Sub

    Private Sub TXTTRANSPORTRATE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTTRANSPORTRATE.KeyPress, TXTDESIGNRATE.KeyPress, TXTPACKINGRATE.KeyPress, TXTCHECKINGRATE.KeyPress, TXTTOTALWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTFOLD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTFOLD.KeyPress
        Try
            If ClientName = "KARAN" Then numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYCATEGORY_Enter(sender As Object, e As EventArgs) Handles CMBGREYCATEGORY.Enter
        Try
            If CMBGREYCATEGORY.Text.Trim = "" Then fillCATEGORY(CMBGREYCATEGORY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGREYCATEGORY_Validating(sender As Object, e As CancelEventArgs) Handles CMBGREYCATEGORY.Validating
        Try
            If CMBGREYCATEGORY.Text.Trim <> "" Then CATEGORYVALIDATE(CMBGREYCATEGORY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSHADE_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSHADE.CellDoubleClick
        Try
            If e.RowIndex >= 0 AndAlso GRIDSHADE.Item("GCOLOR", e.RowIndex).Value <> Nothing Then
                GRIDSHADEDOUBLECLICK = True
                TEMPCOLORROW = e.RowIndex
                TXTSRNO.Text = Val(GRIDSHADE.Item("GSRNO", e.RowIndex).Value)
                CMBCOLOR.Text = GRIDSHADE.Item("GCOLOR", e.RowIndex).Value
                CMBCOLOR.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSHADE_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDSHADE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSHADE.RowCount > 0 Then
                If GRIDSHADEDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDSHADE.Rows.RemoveAt(GRIDSHADE.CurrentRow.Index)
                getsrno(GRIDSHADE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCOLOR.Enter, CMBSHADE.Enter
        Try
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR, "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCOLOR.Validating, CMBSHADE.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me, "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWARPRATE_Validated(sender As Object, e As EventArgs) Handles TXTWARPRATE.Validated, TXTWARPWT.Validated, TXTWEFTWT.Validated, TXTWEFTRATE.Validated
        Try
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWARPAMOUNT_Validated(sender As Object, e As EventArgs) Handles TXTWARPAMOUNT.Validated
        Try
            If CMBWARPQUALITY.Text.Trim <> "" And Val(TXTWARPENDS.Text.Trim) > 0 And Val(TXTWARPWT.Text.Trim) > 0 Then
                If Not CHECKWARP() Then
                    MsgBox("Yarn already Present in Grid below")
                    Exit Sub
                End If
                FILLWARPGRID()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWEFTAMOUNT_Validated(sender As Object, e As EventArgs) Handles TXTWEFTAMOUNT.Validated
        Try
            If CMBWEFTQUALITY.Text.Trim <> "" And Val(TXTWEFTPICK.Text.Trim) > 0 And Val(TXTWEFTWT.Text.Trim) > 0 Then
                If Not CHECKWEFT() Then
                    MsgBox("Yarn already Present in Grid below")
                    Exit Sub
                End If
                FILLWEFTGRID()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTACTWT_Validated(sender As Object, e As EventArgs) Handles TXTACTWT.Validated, TXTDHARAPERCENT.Validated, TXTWASTAGEPERCENT.Validated, TXTWEAVINGCHGS.Validated, TXTGSTPERCENT.Validated
        Try
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ADDRATE()
        Try

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If ClientName = "TINUMINU" And cmbitemname.Text.Trim <> "" Then
                DT = OBJCMN.Execute_Any_String("INSERT INTO PRICELISTMASTER VALUES ((SELECT  isnull(MAX(pl_no), 0) + 1 FROM PRICELISTMASTER) ,(select item_created from itemmaster where  ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "'AND ITEMMASTER.ITEM_YEARID = " & YearId & ") , 0 ,0,(select item_id from itemmaster where  ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "'AND ITEMMASTER.ITEM_YEARID = " & YearId & "),0,0,0,(select ITEM_RATE = (select ITEMMASTER.ITEM_FOLD  * (1000 / ITEMMASTER.item_reorder) * ITEMMASTER.ITEM_RATE / 1000 AS PLRATE from ITEMMASTER where ISNULL(ITEMMASTER.ITEM_DISPLAYNAME,'') = '" & cmbitemname.Text.Trim & "' AND ITEM_YEARID = " & YearId & ") from itemmaster where ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "'AND ITEMMASTER.ITEM_YEARID = " & YearId & ")," & CmpId & "," & Userid & "," & YearId & ",(select item_created from itemmaster where  ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "'AND ITEMMASTER.ITEM_YEARID = " & YearId & "),(select item_created from itemmaster where  ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "'AND ITEMMASTER.ITEM_YEARID = " & YearId & ")," & Userid & ")", "", "")

            Else
                If cmbitemname.Text.Trim = "" Or (Val(TXTRATE1.Text.Trim) = 0 And Val(TXTRATE2.Text.Trim) = 0 And Val(TXTRATE3.Text.Trim) = 0 And Val(TXTRATE4.Text.Trim) = 0 And Val(TXTRATE5.Text.Trim) = 0 And Val(TXTRATE6.Text.Trim) = 0 And Val(TXTRATE7.Text.Trim) = 0 And Val(TXTRATE8.Text.Trim) = 0 And Val(TXTRATE9.Text.Trim) = 0 And Val(TXTRATE10.Text.Trim) = 0 And Val(TXTRATE11.Text.Trim) = 0 And Val(TXTRATE12.Text.Trim) = 0 And Val(TXTRATE13.Text.Trim) = 0 And Val(TXTRATE14.Text.Trim) = 0 And Val(TXTRATE15.Text.Trim) = 0) Then Exit Sub

                DT = OBJCMN.Execute_Any_String(" DELETE ITEMPRICELIST FROM ITEMPRICELIST INNER JOIN ITEMMASTER ON ITEMMASTER.ITEM_ID = ITEMPRICELIST.ITEMID WHERE ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId, "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO ITEMPRICELIST VALUES ((SELECT ISNULL(CATEGORYMASTER.category_id,0) FROM ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = category_id WHERE ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId & "), (SELECT ISNULL(ITEMMASTER.ITEM_ID,0) FROM ITEMMASTER WHERE ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId & ")," & Val(TXTRATE1.Text.Trim) & "," & Val(TXTRATE2.Text.Trim) & "," & Val(TXTRATE3.Text.Trim) & "," & Val(TXTRATE4.Text.Trim) & "," & Val(TXTRATE5.Text.Trim) & "," & Val(TXTRATE6.Text.Trim) & "," & Val(TXTRATE7.Text.Trim) & "," & Val(TXTRATE8.Text.Trim) & "," & Val(TXTRATE9.Text.Trim) & "," & Val(TXTRATE10.Text.Trim) & "," & Val(TXTRATE11.Text.Trim) & "," & Val(TXTRATE12.Text.Trim) & "," & Val(TXTRATE13.Text.Trim) & "," & Val(TXTRATE14.Text.Trim) & "," & Val(TXTRATE15.Text.Trim) & "," & CmpId & "," & Userid & "," & YearId & ")", "", "")

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

    Private Sub TXTWIDTH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTWIDTH.KeyPress
        If ClientName = "VINTAGEINDIA" Then numdotkeypress(e, sender, Me)
    End Sub

    Private Sub CMBGRIDDESIGN_Enter(sender As Object, e As EventArgs) Handles CMBGRIDDESIGN.Enter
        Try
            If CMBGRIDDESIGN.Text.Trim = "" Then FILLDESIGN(CMBGRIDDESIGN, CMBGRIDITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGRIDDESIGN_Validating(sender As Object, e As CancelEventArgs) Handles CMBGRIDDESIGN.Validating
        Try
            If CMBGRIDDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBGRIDDESIGN, e, Me, CMBGRIDITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Validated(sender As Object, e As EventArgs) Handles CMBSHADE.Validated
        Try
            If CMBSHADE.Text.Trim <> "" Then
                If Not CHECKSHADE() Then
                    MsgBox("Shade already Present in Grid below ")
                    Exit Sub
                End If

                FILLSHADEGRID()
                CMBSHADE.Text = ""
                CMBGRIDITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function CHECKSHADE() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDCOLOR.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Or GRIDDOUBLECLICK = False Then
                    If CMBSHADE.Text.Trim = ROW.Cells(GSHADE.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Sub FILLSHADEGRID()

        If GRIDCOLORDOUBLECLICK = False Then
            GRIDCOLOR.Rows.Add(Val(TXTSHADESRNO.Text.Trim), CMBSHADE.Text.Trim)
            getsrno(GRIDCOLOR)
        ElseIf GRIDCOLORDOUBLECLICK = True Then
            GRIDCOLOR.Item(GSHADESRNO.Index, TEMPCOLOROW).Value = Val(TXTSHADESRNO.Text.Trim)
            GRIDCOLOR.Item(GSHADE.Index, TEMPCOLOROW).Value = CMBSHADE.Text.Trim

            'TEMPCOLORROW = GRIDCOLOR.CurrentRow.Index
            GRIDCOLORDOUBLECLICK = False
        End If
        GRIDCOLOR.FirstDisplayedScrollingRowIndex = GRIDCOLOR.RowCount - 1

        GRIDCOLOR.Rows(GRIDCOLOR.RowCount - 1).Selected = True
        GRIDCOLOR.CurrentCell = GRIDCOLOR.Item(0, GRIDCOLOR.RowCount - 1)
        TXTSHADESRNO.Text = GRIDCOLOR.RowCount + 1

        GRIDITEM.RowCount = 0
        TXTITEMSRNO.Text = GRIDITEM.RowCount + 1
        CMBSHADE.Text = ""
        CMBSHADE.Focus()
    End Sub
    Sub FILLITEMDETAILSGRID()
        If GRIDITEMDOUBLECLICK = False Then
            GRIDITEM.Rows.Add(Val(TXTITEMSRNO.Text), CMBGRIDITEMNAME.Text.Trim, CMBGRIDDESIGN.Text.Trim, CMBGRIDSHADE.Text.Trim, Val(TXTMTRS.Text.Trim), GRIDCOLOR.Rows(GRIDCOLOR.CurrentRow.Index).Cells(GSHADESRNO.Index).Value)
            DT_ITEMDETAILS.Rows.Add(Val(TXTITEMSRNO.Text), CMBGRIDITEMNAME.Text.Trim, CMBGRIDDESIGN.Text.Trim, CMBGRIDSHADE.Text.Trim, Val(TXTMTRS.Text), GRIDCOLOR.Rows(GRIDCOLOR.CurrentRow.Index).Cells(GSHADESRNO.Index).Value)
            getsrno(GRIDITEM)

        ElseIf GRIDITEMDOUBLECLICK = True Then
            For I As Integer = 0 To DT_ITEMDETAILS.Rows.Count - 1
                If GRIDCOLOR.Item(GSHADESRNO.Index, TEMPCOLOROW).Value = DT_ITEMDETAILS.Rows(I).Item("SHADESRNO") And GRIDITEM.Item(GITEMSRNO.Index, TEMPITEMROW).Value = DT_ITEMDETAILS.Rows(I).Item("ITEMSRNO") Then
                    DT_ITEMDETAILS.Rows(I).Item("ITEM") = CMBGRIDITEMNAME.Text
                    DT_ITEMDETAILS.Rows(I).Item("ITEMDESIGN") = CMBGRIDDESIGN.Text.Trim
                    DT_ITEMDETAILS.Rows(I).Item("ITEMSHADE") = CMBGRIDSHADE.Text.Trim
                    DT_ITEMDETAILS.Rows(I).Item("MTRS") = Val(TXTMTRS.Text.Trim)
                End If
            Next
LINE1:
            GRIDITEM.Item(GITEMNAME.Index, TEMPITEMROW).Value = CMBGRIDITEMNAME.Text.Trim
            GRIDITEM.Item(GDESIGN.Index, TEMPITEMROW).Value = CMBGRIDDESIGN.Text.Trim
            GRIDITEM.Item(GITEMSHADE.Index, TEMPITEMROW).Value = CMBGRIDSHADE.Text.Trim
            GRIDITEM.Item(GMTRS.Index, TEMPITEMROW).Value = Val(TXTMTRS.Text.Trim)

            GRIDITEMDOUBLECLICK = False
        End If
        TXTITEMSRNO.Text = GRIDITEM.RowCount + 1
        CMBGRIDITEMNAME.Text = ""
        CMBGRIDDESIGN.Text = ""
        CMBGRIDSHADE.Text = ""
        TXTMTRS.Clear()

        CMBGRIDITEMNAME.Focus()
    End Sub

    Private Sub GRIDCOLOR_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDCOLOR.CellClick
        Try
            If GRIDCOLOR.Rows.Count > 0 Then
                GRIDITEM.RowCount = 0
                GRIDSHADEDOUBLECLICK = False
                For i As Integer = 0 To DT_ITEMDETAILS.Rows.Count - 1
                    ' If DT_ITEMDETAILS.Rows(i).Item("SHADESRNO") = GRIDCOLOR.Rows(GRIDCOLOR.CurrentRow.Index).Cells(GSHADESRNO.Index).Value Then
                    If DT_ITEMDETAILS.Rows(i).Item("SHADESRNO") = GRIDCOLOR.Item(GSHADESRNO.Index, e.RowIndex).Value Then
                        GRIDITEM.Rows.Add(DT_ITEMDETAILS.Rows(i).Item("ITEMSRNO"), DT_ITEMDETAILS.Rows(i).Item("ITEM"), DT_ITEMDETAILS.Rows(i).Item("ITEMDESIGN"), DT_ITEMDETAILS.Rows(i).Item("ITEMSHADE"), DT_ITEMDETAILS.Rows(i).Item("MTRS"), DT_ITEMDETAILS.Rows(i).Item("SHADESRNO"))
                    End If
                Next
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCOLOR_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDCOLOR.CellDoubleClick
        Try
            If e.RowIndex >= 0 And GRIDCOLOR.Item(GSHADE.Index, e.RowIndex).Value <> Nothing Then
                GRIDCOLORDOUBLECLICK = True
                TEMPCOLOROW = e.RowIndex
                TXTSHADESRNO.Text = GRIDCOLOR.Item(GSHADESRNO.Index, e.RowIndex).Value
                CMBSHADE.Text = GRIDCOLOR.Item(GSHADE.Index, e.RowIndex).Value
                CMBSHADE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCOLOR_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDCOLOR.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
LINE1:
                For I As Integer = 0 To DT_ITEMDETAILS.Rows.Count - 1
                    If GRIDCOLOR.Rows(GRIDCOLOR.CurrentRow.Index).Cells(GSHADESRNO.Index).Value = Val(DT_ITEMDETAILS.Rows(I).Item("SHADESRNO")) Then
                        DT_ITEMDETAILS.Rows.RemoveAt(I)
                        GoTo LINE1
                    End If
                Next
                For I As Integer = 0 To DT_ITEMDETAILS.Rows.Count - 1
                    If GRIDCOLOR.Rows(GRIDCOLOR.CurrentRow.Index).Cells(GSHADESRNO.Index).Value < Val(DT_ITEMDETAILS.Rows(I).Item("SHADESRNO")) Then
                        DT_ITEMDETAILS.Rows(I).Item("SHADESRNO") = Val(DT_ITEMDETAILS.Rows(I).Item("SHADESRNO")) - 1
                    End If
                Next
                GRIDCOLOR.Rows.RemoveAt(GRIDCOLOR.CurrentRow.Index)
                GRIDITEM.RowCount = 0

                getsrno(GRIDCOLOR)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_Validated(sender As Object, e As EventArgs) Handles TXTMTRS.Validated
        Try
            If CMBGRIDITEMNAME.Text.Trim <> "" And GRIDCOLOR.RowCount > 0 And Val(TXTMTRS.Text.Trim) > 0 Then
                FILLITEMDETAILSGRID()
                TOTAL()
                CMBGRIDITEMNAME.Text = ""
                CMBGRIDDESIGN.Text = ""
                CMBGRIDSHADE.Text = ""
                TXTMTRS.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDITEM_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDITEM.CellDoubleClick
        Try
            If e.RowIndex >= 0 And GRIDITEM.Item(GITEMNAME.Index, e.RowIndex).Value <> Nothing Then
                GRIDITEMDOUBLECLICK = True
                TEMPITEMROW = e.RowIndex
                TXTITEMSRNO.Text = Val(GRIDITEM.Item(GITEMSRNO.Index, e.RowIndex).Value)
                CMBGRIDITEMNAME.Text = GRIDITEM.Item(GITEMNAME.Index, e.RowIndex).Value
                CMBGRIDDESIGN.Text = GRIDITEM.Item(GDESIGN.Index, e.RowIndex).Value
                CMBGRIDSHADE.Text = GRIDITEM.Item(GITEMSHADE.Index, e.RowIndex).Value
                TXTMTRS.Text = Val(GRIDITEM.Item(GMTRS.Index, e.RowIndex).Value)

                CMBGRIDITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDITEM_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDITEM.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim del As Boolean = False
                If GRIDITEM.RowCount > 0 Then
                    Dim row As Integer = GRIDCOLOR.Rows(GRIDCOLOR.CurrentRow.Index).Cells(GSHADESRNO.Index).Value
                    For I As Integer = 0 To DT_ITEMDETAILS.Rows.Count - 1
                        If GRIDCOLOR.Rows(GRIDCOLOR.CurrentRow.Index).Cells(GSHADESRNO.Index).Value = Val(DT_ITEMDETAILS.Rows(I).Item("SHADESRNO")) And GRIDITEM.Rows(GRIDITEM.CurrentRow.Index).Cells(GITEMSRNO.Index).Value = Val(DT_ITEMDETAILS.Rows(I).Item("ITEMSRNO")) Then
                            If del = False Then
                                DT_ITEMDETAILS.Rows.RemoveAt(I)
                                GRIDITEM.Rows.RemoveAt(GRIDITEM.CurrentRow.Index)
                                del = True
                                GoTo line1
                            End If
                        End If
                    Next
line1:
                    For I As Integer = 0 To DT_ITEMDETAILS.Rows.Count - 1
                        If GRIDCOLOR.Rows(GRIDCOLOR.CurrentRow.Index).Cells(GSHADESRNO.Index).Value = Val(DT_ITEMDETAILS.Rows(I).Item("ITEMSRNO")) And del = True And row < Val(DT_ITEMDETAILS.Rows(I).Item(GITEMSRNO.Index)) Then
                            DT_ITEMDETAILS.Rows(I).Item("SHADESRNO") = Val(DT_ITEMDETAILS.Rows(I).Item("SHADESRNO")) - 1
                        End If
                    Next
                    getsrno(GRIDITEM)
                    TOTAL()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class