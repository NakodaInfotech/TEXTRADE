
Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Accessibility

Public Class GreyRecdKnitting

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Public EDIT As Boolean          'used for editing
    Public TEMPGREYNO As Integer     'used for poation no while editing
    Dim TEMPROW As Integer
    Dim TEMPUPLOADROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer
    Dim PARTYCHALLANNO As String
    Public FRMSTRING As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGodown.Enter
        Try
            If cmbGodown.Text.Trim = "" Then fillGODOWN(cmbGodown, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbGodown.Validating
        Try
            If cmbGodown.Text.Trim <> "" Then GODOWNVALIDATE(cmbGodown, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
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

    Private Sub CMBNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then NAMEVALIDATE(cmbname, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS", cmbtrans.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            'If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")

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
            'If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "SUNDRY CREDITORS")
            If cmbtrans.Text.Trim <> "" Then NAMEVALIDATE(cmbtrans, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "Sundry Creditors", "TRANSPORT")

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(GREY_NO),0) + 1 ", " GREYRECDKNITTING ", " AND GREY_cmpid=" & CmpId & " and GREY_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTGREYNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If cmbGodown.Text.Trim.Length = 0 Then
                EP.SetError(cmbGodown, " Please Fill Godown")
                bln = False
            End If

            If cmbname.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, " Please Fill Name")
                bln = False
            End If

            If GRIDGREY.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If

            If GREYDATE.Text = "__/__/____" Then
                EP.SetError(GREYDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(GREYDATE.Text) Then
                    EP.SetError(GREYDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, "Item Used, Item Locked")
                bln = False
            End If

            For Each ROW As DataGridViewRow In GRIDGREY.Rows
                If ROW.Cells(GWT.Index).Value = 0 Then
                    EP.SetError(TXTWT, "WT Cannot be 0")
                    bln = False
                End If
            Next
            'If ALLOWMANUALJONO = True Then
            '    If TXTJONO.Text <> "" And CMBNAME.Text.Trim <> "" And EDIT = False Then
            '        Dim OBJCMN As New ClsCommon
            '        Dim dttable As DataTable = OBJCMN.search(" ISNULL(JOBOUT.JO_NO,0)  AS JONO", "", " JOBOUT ", "  AND JOBOUT.JO_NO=" & TXTJONO.Text.Trim & " AND JOBOUT.JO_CMPID = " & CmpId & " AND JOBOUT.JO_LOCATIONID = " & Locationid & " AND JOBOUT.JO_YEARID = " & YearId)
            '        If dttable.Rows.Count > 0 Then
            '            EP.SetError(TXTJONO, "Job Out No Already Exist")
            '            bln = False
            '        End If
            '    End If
            'End If


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

                For Each ROW As DataGridViewRow In GRIDGREY.Rows
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
                            'ROW.Cells(GPURRATE.Index).Value = Val(ORDROW.Cells(ORATE.Index).Value)
                            TEMPORDERROWNO = -1
                            Exit For
CHECKNEXTLINE:
                        End If
                    Next
                    'IF NO FURTHER MACHING IS FOUND BUT WE HAVE TEMPORDERROWNO THEN ADD VALUE IN THAT ROW
                    If TEMPORDERROWNO >= 0 Then
                        GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNQTY.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNQTY.Index).Value) + Val(ROW.Cells(gQty.Index).Value)
                        GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNMTRS.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGRNMTRS.Index).Value) + Val(ROW.Cells(GMTRS.Index).Value)
                        'ROW.Cells(GPURRATE.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(ORATE.Index).Value)
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


            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(GREYDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbGodown.Text.Trim)
            alParaval.Add(cmbname.Text.Trim)

            alParaval.Add(txtchallan.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(dtpchallan.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(txtlrno.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(lrdate.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(Val(lbltotalqty.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(Val(LBLTOTALWT.Text))

            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim gridsrno As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim LOOMNO As String = ""
            Dim ROLLNO As String = ""
            Dim qty As String = ""
            Dim QTYUNIT As String = ""
            Dim MTRS As String = ""
            Dim WT As String = ""
            Dim RACK As String = ""
            Dim SHELF As String = ""
            Dim BARCODE As String = ""
            Dim DONE As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGREY.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString

                        ITEMNAME = row.Cells(gitemname.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        LOOMNO = row.Cells(GLOOMNO.Index).Value.ToString
                        ROLLNO = row.Cells(GROLLNO.Index).Value.ToString
                        qty = row.Cells(gQty.Index).Value.ToString
                        QTYUNIT = row.Cells(gqtyunit.Index).Value.ToString
                        MTRS = row.Cells(GMTRS.Index).Value
                        WT = row.Cells(GWT.Index).Value
                        RACK = row.Cells(GRACK.Index).Value.ToString
                        SHELF = row.Cells(GSHELF.Index).Value.ToString
                        BARCODE = row.Cells(GBARCODE.Index).Value
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = 1
                        Else
                            DONE = 0
                        End If
                        OUTPCS = row.Cells(GOUTPCS.Index).Value
                        OUTMTRS = row.Cells(GOUTMTRS.Index).Value


                    Else

                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value
                        ITEMNAME = ITEMNAME & "|" & row.Cells(gitemname.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        LOOMNO = LOOMNO & "|" & row.Cells(GLOOMNO.Index).Value.ToString
                        ROLLNO = ROLLNO & "|" & row.Cells(GROLLNO.Index).Value.ToString
                        qty = qty & "|" & row.Cells(gQty.Index).Value
                        QTYUNIT = QTYUNIT & "|" & row.Cells(gqtyunit.Index).Value
                        MTRS = MTRS & "|" & row.Cells(GMTRS.Index).Value
                        WT = WT & "|" & row.Cells(GWT.Index).Value
                        RACK = RACK & "|" & row.Cells(GRACK.Index).Value.ToString
                        SHELF = SHELF & "|" & row.Cells(GSHELF.Index).Value.ToString
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value
                        If row.Cells(GDONE.Index).Value = True Then
                            DONE = DONE & "|" & "1"
                        Else
                            DONE = DONE & "|" & "0"
                        End If
                        OUTPCS = OUTPCS & "|" & row.Cells(GOUTPCS.Index).Value
                        OUTMTRS = OUTMTRS & "|" & row.Cells(GOUTMTRS.Index).Value

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(ITEMNAME)
            alParaval.Add(QUALITY)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(LOOMNO)
            alParaval.Add(ROLLNO)
            alParaval.Add(qty)
            alParaval.Add(QTYUNIT)
            alParaval.Add(MTRS)
            alParaval.Add(WT)
            alParaval.Add(RACK)
            alParaval.Add(SHELF)
            alParaval.Add(BARCODE)
            alParaval.Add(DONE)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)



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
                If row.Cells(0).Value <> Nothing Then

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



            alParaval.Add(CMBDYEINGNAME.Text.Trim)

            alParaval.Add("") 'VEHICLENO
            alParaval.Add("")  'FROMCITY
            alParaval.Add("")     'TOCITY
            alParaval.Add("")     'PACKING
            alParaval.Add("")   'EWAYBILLNO

            Dim objCUTTING As New ClsGreyRecdKnitting()
            objCUTTING.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = objCUTTING.SAVE()
                MsgBox("Details Added")
                TXTGREYNO.Text = DTTABLE.Rows(0).Item(0)
                PRINTREPORT(DTTABLE.Rows(0).Item(0))

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                alParaval.Add(TEMPGREYNO)
                IntResult = objCUTTING.UPDATE()
                MsgBox("Details Updated")

                If gridupload.RowCount > 0 Then SAVEUPLOAD()
                EDIT = False
            End If



            If EDIT = False And (ClientName = "KARAN" Or ClientName = "VALIANT") Then
                If MsgBox("Issue Grey Directly to Jobber?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim OBJISSUE As New YarnDirectIssueJobber
                    OBJISSUE.ShowDialog()
                    If OBJISSUE.CMBJOBBER.Text.Trim = "" Then GoTo LINE1
                    If ClientName = "KARAN" Then
                        DIRECTISSUEJOBBER(OBJISSUE.CMBJOBBER.Text.Trim, OBJISSUE.CMBPROCESS.Text.Trim, OBJISSUE.JOBBERID)
                    Else
                        DIRECTISSUEGRN(OBJISSUE.CMBJOBBER.Text.Trim, OBJISSUE.CMBPROCESS.Text.Trim, OBJISSUE.JOBBERID)
                    End If
                End If
            End If
LINE1:

            CLEAR()
            GREYDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub DIRECTISSUEJOBBER(ByVal JOBBERNAME As String, PROCESSNAME As String, JOBBERID As Integer)
        Try


            GRIDGREY.RowCount = 0
            Dim objJO As New ClsGreyRecdKnitting()
            Dim dttable As DataTable = objJO.selectGREY(Val(TXTGREYNO.Text), CmpId, YearId)
            If dttable.Rows.Count > 0 Then
                For Each dr As DataRow In dttable.Rows
                    GRIDGREY.Rows.Add(dr("GRIDSRNO").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("DESIGNNO").ToString, dr("COLOR").ToString, dr("LOOMNO"), dr("ROLLNO"), Format(dr("qty"), "0.00"), dr("UNIT").ToString, Format(dr("MTRS"), "0.00"), Format(dr("WT"), "0.00"), dr("RACK"), dr("SHELF"), dr("BARCODE"), dr("DONE").ToString, dr("OUTPCS"), dr("OUTMTRS"))
                Next
            End If



            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(0)    'JONO
            ALPARAVAL.Add(Format(Convert.ToDateTime(GREYDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(cmbGodown.Text.Trim)
            ALPARAVAL.Add(JOBBERNAME)
            ALPARAVAL.Add(PROCESSNAME)
            ALPARAVAL.Add(cmbname.Text.Trim)   'PARTYNAME (add WEAVER NAME FOR KARAN)
            ALPARAVAL.Add("")   'LOTNO
            ALPARAVAL.Add(txtchallan.Text.Trim)
            ALPARAVAL.Add(cmbtrans.Text.Trim)
            ALPARAVAL.Add("")   'LRNO
            ALPARAVAL.Add(lrdate.Value)
            ALPARAVAL.Add("")   'VEHICLENO
            ALPARAVAL.Add("")   'FROMCITY
            ALPARAVAL.Add("")   'TOCITY
            ALPARAVAL.Add(JOBBERNAME)   'PACKING
            ALPARAVAL.Add("")   'EWAYBILLNO
            ALPARAVAL.Add(0)    'NOOFBALES
            ALPARAVAL.Add(Val(lbltotalqty.Text))
            ALPARAVAL.Add(Val(LBLTOTALMTRS.Text))
            ALPARAVAL.Add(Val(LBLTOTALWT.Text))
            ALPARAVAL.Add(0)    'RATE
            ALPARAVAL.Add(txtremarks.Text.Trim)
            ALPARAVAL.Add(0)    'CHKLOTREADY
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)
            ALPARAVAL.Add(0)




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
            Dim PER As String = ""
            Dim AMOUNT As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""

            Dim BARCODE As String = "" 'BARCODE ADDED

            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""
            Dim FRAMES As String = ""
            Dim EMBPRODDONE As String = ""
            Dim GRIDLOTNO As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGREY.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = Val(row.Cells(gsrno.Index).Value)
                        PIECETYPE = "FRESH"
                        If row.Cells(GROLLNO.Index).Value <> Nothing Then BALENO = row.Cells(GROLLNO.Index).Value.ToString Else BALENO = ""
                        ITEMNAME = row.Cells(gitemname.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        DESCRIPTION = ""
                        CUT = 0
                        PCS = Val(row.Cells(gQty.Index).Value)
                        UNIT = row.Cells(gqtyunit.Index).Value
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        WT = Val(row.Cells(GWT.Index).Value)
                        RATE = 0
                        PER = "Mtrs"
                        AMOUNT = 0
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        OUTPCS = 0
                        OUTMTRS = 0
                        FROMNO = Val(TXTGREYNO.Text.Trim)
                        FROMSRNO = Val(row.Cells(gsrno.Index).Value)
                        FROMTYPE = "KNITTING"
                        FRAMES = 0
                        EMBPRODDONE = 0
                        GRIDLOTNO = ""

                    Else
                        gridsrno = gridsrno & "|" & Val(row.Cells(gsrno.Index).Value)
                        PIECETYPE = PIECETYPE & "|" & "FRESH"
                        If row.Cells(GROLLNO.Index).Value <> Nothing Then BALENO = BALENO & "|" & row.Cells(GROLLNO.Index).Value.ToString Else BALENO = BALENO & "|" & ""
                        ITEMNAME = ITEMNAME & "|" & row.Cells(gitemname.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        DESCRIPTION = DESCRIPTION & "|" & ""
                        CUT = CUT & "|" & 0
                        PCS = PCS & "|" & Val(row.Cells(gQty.Index).Value)
                        UNIT = UNIT & "|" & row.Cells(gqtyunit.Index).Value
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        RATE = RATE & "|" & 0
                        PER = PER & "|" & "Mtrs"
                        AMOUNT = AMOUNT & "|" & 0
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        OUTPCS = OUTPCS & "|" & 0
                        OUTMTRS = OUTMTRS & "|" & 0
                        FROMNO = FROMNO & "|" & Val(TXTGREYNO.Text.Trim)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(gsrno.Index).Value)
                        FROMTYPE = FROMTYPE & "|" & "KNITTING"
                        FRAMES = FRAMES & "|" & 0
                        EMBPRODDONE = EMBPRODDONE & "|" & 0
                        GRIDLOTNO = GRIDLOTNO & "|" & ""

                    End If
                End If
            Next

            ALPARAVAL.Add(gridsrno)
            ALPARAVAL.Add(PIECETYPE)
            ALPARAVAL.Add(BALENO)
            ALPARAVAL.Add(ITEMNAME)
            ALPARAVAL.Add(QUALITY)
            ALPARAVAL.Add(DESIGN)
            ALPARAVAL.Add(COLOR)
            ALPARAVAL.Add(DESCRIPTION)
            ALPARAVAL.Add(CUT)
            ALPARAVAL.Add(PCS)
            ALPARAVAL.Add(UNIT)
            ALPARAVAL.Add(MTRS)
            ALPARAVAL.Add(WT)
            ALPARAVAL.Add(RATE)
            ALPARAVAL.Add(PER)
            ALPARAVAL.Add(AMOUNT)      ' AMOUNT
            ALPARAVAL.Add(BARCODE)
            ALPARAVAL.Add(OUTPCS)
            ALPARAVAL.Add(OUTMTRS)
            ALPARAVAL.Add(FROMNO)
            ALPARAVAL.Add(FROMSRNO)
            ALPARAVAL.Add(FROMTYPE)
            ALPARAVAL.Add(FRAMES)
            ALPARAVAL.Add(EMBPRODDONE)
            ALPARAVAL.Add(GRIDLOTNO)

            Dim griduploadsrno As String = ""
            Dim imgpath As String = ""
            Dim uploadremarks As String = ""
            Dim name As String = ""
            Dim NEWIMGPATH As String = ""
            Dim FILENAME As String = ""

            ''Saving Upload Grid
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

            ALPARAVAL.Add(griduploadsrno)
            ALPARAVAL.Add(uploadremarks)
            ALPARAVAL.Add(name)
            ALPARAVAL.Add(imgpath)
            ALPARAVAL.Add(NEWIMGPATH)
            ALPARAVAL.Add(FILENAME)

            ALPARAVAL.Add("")   'JOTYPE
            ALPARAVAL.Add(0)    'JOYPENO
            ALPARAVAL.Add(0)    'TOTALAMT

            ALPARAVAL.Add("")   'VEHICLE
            ALPARAVAL.Add(0)    'FCITYID
            ALPARAVAL.Add(0)    'TCITYID
            ALPARAVAL.Add(0)    'PACKINGID
            ALPARAVAL.Add("")   'EWAYBILLNO
            ALPARAVAL.Add(0)    'AVGWT
            ALPARAVAL.Add(0)    'DISPATCHFROM
            ALPARAVAL.Add(0)   'width



            Dim OBJYAARNISSUE As New ClsCuttingIssue
            OBJYAARNISSUE.alParaval = ALPARAVAL
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim DT As DataTable = OBJYAARNISSUE.SAVE()
            MsgBox("Grey Issued To Dyeing")



            'UPDATE DONE = TRUE AND ADD GRN NO IN GREYRECDKNITTING AND REMOVE FROM STOCK
            Dim OBJCMN As New ClsCommon
            Dim DTGREY As DataTable = OBJCMN.Execute_Any_String("UPDATE GREYRECDKNITTING SET GREY_DYEINGLEDGERID = " & JOBBERID & ", GREY_DIRECTENTRYNO = " & Val(DT.Rows(0).Item(0)) & ", GREY_DIRECTTYPE = 'JOBOUT' WHERE GREY_NO = " & Val(TXTGREYNO.Text.Trim) & " AND GREY_YEARID = " & YearId, "", "")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub DIRECTISSUEGRN(ByVal JOBBERNAME As String, PROCESSNAME As String, JOBBERID As Integer)
        Try


            GRIDGREY.RowCount = 0
            Dim objJO As New ClsGreyRecdKnitting()
            Dim dttable As DataTable = objJO.selectGREY(Val(TXTGREYNO.Text), CmpId, YearId)
            If dttable.Rows.Count > 0 Then
                For Each dr As DataRow In dttable.Rows
                    GRIDGREY.Rows.Add(dr("GRIDSRNO").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("DESIGNNO").ToString, dr("COLOR").ToString, dr("LOOMNO"), dr("ROLLNO"), Format(dr("qty"), "0.00"), dr("UNIT").ToString, Format(dr("MTRS"), "0.00"), Format(dr("WT"), "0.00"), dr("RACK"), dr("SHELF"), dr("BARCODE"), dr("DONE").ToString, dr("OUTPCS"), dr("OUTMTRS"))
                Next
            End If



            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(0)    'GRNNO
            ALPARAVAL.Add("Job Work")    'TYPE
            ALPARAVAL.Add(Format(Convert.ToDateTime(GREYDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(cmbname.Text.Trim)
            ALPARAVAL.Add(cmbGodown.Text.Trim)
            ALPARAVAL.Add("")   'BROKER
            ALPARAVAL.Add("")   'SENDER
            ALPARAVAL.Add(JOBBERNAME)
            ALPARAVAL.Add("")   'POUTNO
            ALPARAVAL.Add("")   'LOTNO
            ALPARAVAL.Add(Format(Convert.ToDateTime(GREYDATE.Text).Date, "dd/MM/yyyy")) 'RECDATE
            ALPARAVAL.Add("")   'PROCESS
            ALPARAVAL.Add(txtchallan.Text.Trim)
            ALPARAVAL.Add(Format(dtpchallan.Value.Date, "MM/dd/yyyy")) 'CHALLANDATE
            ALPARAVAL.Add("")   'PONO
            ALPARAVAL.Add(Format(Convert.ToDateTime(GREYDATE.Text).Date, "MM/dd/yyyy")) 'PODATE

            ALPARAVAL.Add(cmbtrans.Text.Trim)
            ALPARAVAL.Add("")   'LRNO
            ALPARAVAL.Add(lrdate.Value)

            ALPARAVAL.Add("")   'TRANSREFNO
            ALPARAVAL.Add("")   'TRANSREMARKS
            ALPARAVAL.Add(0)    'BALEWT

            ALPARAVAL.Add(Val(lbltotalqty.Text))
            ALPARAVAL.Add(0)   'TOTALBALES
            ALPARAVAL.Add(Val(LBLTOTALMTRS.Text))
            ALPARAVAL.Add(Val(LBLTOTALWT.Text))

            ALPARAVAL.Add("")   'DYEINGTYPE
            ALPARAVAL.Add(txtremarks.Text.Trim)
            ALPARAVAL.Add("")   'PARTYBILLNO
            ALPARAVAL.Add(Format(Convert.ToDateTime(GREYDATE.Text).Date, "MM/dd/yyyy")) 'PARTYBILLDATE
            ALPARAVAL.Add(0)    'CHKLOTREADY

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)
            ALPARAVAL.Add(0)




            Dim GRIDSRNO As String = ""
            Dim PIECETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim GRIDREMARKS As String = ""
            Dim QUALITY As String = ""
            Dim BALENO As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim PCS As String = ""
            Dim UNIT As String = ""
            Dim CUT As String = ""
            Dim MTRS As String = ""
            Dim RACK As String = ""
            Dim SHELF As String = ""
            Dim WT As String = ""
            Dim PURRATE As String = ""
            Dim SALERATE As String = ""
            Dim WHOLESALERATE As String = ""
            Dim BARCODE As String = "" 'BARCODE ADDED
            Dim DONE As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""
            Dim PONO As String = ""
            Dim POGRIDSRNO As String = ""
            Dim CHECKDONE As String = ""
            Dim FROMTYPE As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGREY.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(row.Cells(gsrno.Index).Value)
                        PIECETYPE = "FRESH"
                        ITEMNAME = row.Cells(gitemname.Index).Value.ToString
                        GRIDREMARKS = ""
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        If row.Cells(GROLLNO.Index).Value <> Nothing Then BALENO = row.Cells(GROLLNO.Index).Value.ToString Else BALENO = ""
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        PCS = Val(row.Cells(gQty.Index).Value)
                        UNIT = row.Cells(gqtyunit.Index).Value
                        CUT = 0
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        RACK = ""
                        SHELF = ""
                        WT = Val(row.Cells(GWT.Index).Value)
                        PURRATE = 0
                        SALERATE = 0
                        WHOLESALERATE = 0
                        PER = "Mtrs"
                        AMOUNT = 0
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        DONE = 0
                        OUTPCS = 0
                        OUTMTRS = 0
                        PONO = 0
                        POGRIDSRNO = 0
                        CHECKDONE = 0
                        FROMTYPE = "KNITTING"

                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(gsrno.Index).Value)
                        PIECETYPE = PIECETYPE & "|" & "FRESH"
                        ITEMNAME = ITEMNAME & "|" & row.Cells(gitemname.Index).Value.ToString
                        GRIDREMARKS = GRIDREMARKS & "|" & ""
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        If row.Cells(GROLLNO.Index).Value <> Nothing Then BALENO = BALENO & "|" & row.Cells(GROLLNO.Index).Value.ToString Else BALENO = BALENO & "|" & ""
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        PCS = PCS & "|" & Val(row.Cells(gQty.Index).Value)
                        UNIT = UNIT & "|" & row.Cells(gqtyunit.Index).Value
                        CUT = CUT & "|" & 0
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        RACK = RACK & "|" & ""
                        SHELF = SHELF & "|" & ""
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        PURRATE = PURRATE & "|" & 0
                        SALERATE = SALERATE & "|" & 0
                        WHOLESALERATE = WHOLESALERATE & "|" & 0
                        PER = PER & "|" & "Mtrs"
                        AMOUNT = AMOUNT & "|" & 0
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        DONE = DONE & "|" & ""
                        OUTPCS = OUTPCS & "|" & 0
                        OUTMTRS = OUTMTRS & "|" & 0
                        PONO = PONO & "|" & 0
                        POGRIDSRNO = POGRIDSRNO & "|" & 0
                        CHECKDONE = CHECKDONE & "|" & 0
                        FROMTYPE = FROMTYPE & "|" & "KNITTING"

                    End If
                End If
            Next

            ALPARAVAL.Add(GRIDSRNO)
            ALPARAVAL.Add(PIECETYPE)
            ALPARAVAL.Add(ITEMNAME)
            ALPARAVAL.Add(GRIDREMARKS)
            ALPARAVAL.Add(QUALITY)
            ALPARAVAL.Add(BALENO)
            ALPARAVAL.Add(DESIGN)
            ALPARAVAL.Add(COLOR)
            ALPARAVAL.Add(PCS)
            ALPARAVAL.Add(UNIT)
            ALPARAVAL.Add(CUT)
            ALPARAVAL.Add(MTRS)
            ALPARAVAL.Add(RACK)
            ALPARAVAL.Add(SHELF)
            ALPARAVAL.Add(WT)
            ALPARAVAL.Add(PURRATE)
            ALPARAVAL.Add(SALERATE)
            ALPARAVAL.Add(WHOLESALERATE)
            ALPARAVAL.Add(PER)
            ALPARAVAL.Add(AMOUNT)      ' AMOUNT
            ALPARAVAL.Add(BARCODE)
            ALPARAVAL.Add(DONE)
            ALPARAVAL.Add(OUTPCS)
            ALPARAVAL.Add(OUTMTRS)
            ALPARAVAL.Add(PONO)
            ALPARAVAL.Add(POGRIDSRNO)
            ALPARAVAL.Add(CHECKDONE)
            ALPARAVAL.Add(FROMTYPE)

            Dim griduploadsrno As String = ""
            Dim imgpath As String = ""
            Dim uploadremarks As String = ""
            Dim name As String = ""
            Dim NEWIMGPATH As String = ""
            Dim FILENAME As String = ""

            ALPARAVAL.Add(griduploadsrno)
            ALPARAVAL.Add(uploadremarks)
            ALPARAVAL.Add(name)
            ALPARAVAL.Add(imgpath)
            ALPARAVAL.Add(NEWIMGPATH)
            ALPARAVAL.Add(FILENAME)



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


            ALPARAVAL.Add(ORDERGRIDSRNO)
            ALPARAVAL.Add(ORDERITEMNAME)
            ALPARAVAL.Add(ORDERDESIGN)
            ALPARAVAL.Add(ORDERCOLOR)
            ALPARAVAL.Add(ORDERPCS)
            ALPARAVAL.Add(ORDERMTRS)
            ALPARAVAL.Add(ORDERFROMNO)
            ALPARAVAL.Add(ORDERFROMSRNO)
            ALPARAVAL.Add(ORDERFROMTYPE)
            ALPARAVAL.Add(ORDERGRNPCS)
            ALPARAVAL.Add(ORDERGRNMTRS)
            ALPARAVAL.Add(ORDERRATE)

            ALPARAVAL.Add(0)    'TOTALAMT
            ALPARAVAL.Add(1)    'DIRECTFROMKNITTING
            ALPARAVAL.Add(Val(TXTGREYNO.Text.Trim))    'GREYKNITTINGNO

            ALPARAVAL.Add("") 'VEHICLENO
            ALPARAVAL.Add("")  'FROMCITY
            ALPARAVAL.Add("")     'TOCITY
            ALPARAVAL.Add("")     'PACKING
            ALPARAVAL.Add("")   'EWAYBILLNO

            ALPARAVAL.Add(0)    'CHNO
            ALPARAVAL.Add("")   'REFLOTNO



            Dim OBJYAARNISSUE As New ClsGrn
            OBJYAARNISSUE.alParaval = ALPARAVAL
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim DT As DataTable = OBJYAARNISSUE.SAVE()
            MsgBox("Grey Issued To Dyeing")


            'UPDATE DONE = TRUE AND ADD GRN NO IN GREYRECDKNITTING AND REMOVE FROM STOCK
            Dim OBJCMN As New ClsCommon
            Dim DTGREY As DataTable = OBJCMN.Execute_Any_String("UPDATE GREYRECDKNITTING SET GREY_DYEINGLEDGERID = " & JOBBERID & ", GREY_DIRECTENTRYNO = " & Val(DT.Rows(0).Item(0)) & ", GREY_DIRECTTYPE = 'GRN' WHERE GREY_NO = " & Val(TXTGREYNO.Text.Trim) & " AND GREY_YEARID = " & YearId, "", "")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJBILL As New ClsGreyRecdKnitting


            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPGREYNO)
                    'ALPARAVAL.Add(TEMPREGNAME)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSoftCopy.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSoftCopy.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJBILL.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJBILL.SAVEUPLOAD()
                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal YARNNO As Integer)
        'Try
        '    TEMPMSG = MsgBox("Wish to Print Yarn Issue?", MsgBoxStyle.YesNo)
        '    If TEMPMSG = vbYes Then
        '        Dim OBJGDN As New GDNDESIGN
        '        OBJGDN.MdiParent = MDIMain
        '        OBJGDN.FRMSTRING = "JOBOUT"
        '        'OBJGDN.FORMULA = "{JOBOUT.JO_NO}=" & Val(JONO) & " and {JOBOUT.JO_yearid}=" & YearId
        '        OBJGDN.Show()
        '    End If

        '    If ClientName = "KCRAYON" Then
        '        If MsgBox("Wish to Print Job Sheet?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '            Dim OBJJO As New JobOutDesign
        '            OBJJO.MdiParent = MDIMain
        '            OBJJO.FRMSTRING = "JOBSHEET"
        '            'OBJJO.WHERECLAUSE = "{JOBOUT.JO_NO}=" & Val(JONO) & " and {JOBOUT.JO_yearid}=" & YearId
        '            OBJJO.Show()
        '        End If
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub JOBOUT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If errorvalid() = True Then
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
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            'SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            toolprevious_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
        ElseIf e.KeyCode = Keys.F5 Then     'grid focus
            YarnRecd.Focus()
        End If
    End Sub

    Sub CLEAR()

        EP.Clear()
        If USERGODOWN <> "" Then cmbGodown.Text = USERGODOWN Else cmbGodown.Text = ""
        cmbname.Text = ""
        txtchallan.Clear()

        txtadd.Clear()
        GREYDATE.Text = Now.Date
        tstxtbillno.Clear()

        cmbtrans.Text = ""
        txtlrno.Clear()
        lrdate.Value = Now.Date
        txtremarks.Clear()

        txtuploadsrno.Clear()
        txtuploadremarks.Clear()
        gridupload.RowCount = 0
        txtimgpath.Clear()
        TXTNEWIMGPATH.Clear()
        TXTFILENAME.Clear()
        PBSoftCopy.Image = Nothing
        PBSoftCopy.ImageLocation = ""
        gridupload.RowCount = 0


        lbltotalqty.Text = 0.0
        LBLTOTALWT.Text = 0.0
        LBLTOTALMTRS.Text = 0.0

        txtsrno.Clear()

        cmbitemname.Text = ""
        CMBQUALITY.Text = ""
        CMBDESIGN.Text = ""
        cmbcolor.Text = ""
        TXTROLLNO.Clear()
        TXTWT.Clear()
        cmbqtyunit.Text = ""
        TXTMTRS.Clear()
        CMBRACK.Text = ""
        CMBSHELF.Text = ""
        TXTQTY.Text = 1

        lbllocked.Visible = False
        PBlock.Visible = False

        GRIDGREY.RowCount = 0

        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False
        getmaxno()

        If GRIDGREY.RowCount > 0 Then
            txtsrno.Text = Val(GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(0).Value) + 1
        Else
            txtsrno.Text = 1
        End If

        If gridupload.RowCount > 0 Then
            txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
        Else
            txtuploadsrno.Text = 1
        End If
        TXTLOOMNO.Clear()
        GRIDORDER.RowCount = 0

    End Sub

    Sub total()
        Try
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALWT.Text = 0.0
            lbltotalqty.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDGREY.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    lbltotalqty.Text = Format(Val(lbltotalqty.Text) + Val(ROW.Cells(gQty.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")

                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        cmbname.Focus()
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPGREYNO)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try

            If lbllocked.Visible = True Then
                MsgBox("Unable To Delete, Entry Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If EDIT = True Then

                If MsgBox("Wish to Delete Grey Recd...?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBJEMB As New ClsGreyRecdKnitting

                ALPARAVAL.Add(TEMPGREYNO)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(YearId)
                OBJEMB.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJEMB.Delete()
                MsgBox("Grey Recd Deleted Succesfully")
                CLEAR()
                EDIT = False
                cmbname.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Sub FILLCMB()
        Try
            If cmbGodown.Text.Trim = "" Then fillGODOWN(cmbGodown, EDIT)
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT'")
            FILLNAME(CMBDYEINGNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            fillitemname(cmbitemname, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            fillQUALITY(CMBQUALITY, EDIT)
            FILLDESIGN(CMBDESIGN, cmbitemname.Text.Trim)
            fillunit(cmbqtyunit)
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
            FILLRACK(CMBRACK)
            FILLSHELF(CMBSHELF)

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GreyRecdKnitting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            CLEAR()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objJO As New ClsGreyRecdKnitting()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPGREYNO)
                ALPARAVAL.Add(CmpId)
                'ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)
                objJO.alParaval = ALPARAVAL
                Dim dttable As DataTable = objJO.selectGREY(TEMPGREYNO, CmpId, YearId)

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTGREYNO.Text = TEMPGREYNO
                        GREYDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        cmbname.Text = Convert.ToString(dr("NAME").ToString)
                        cmbGodown.Text = Convert.ToString(dr("GODOWN").ToString)

                        txtchallan.Text = Convert.ToString(dr("CHALLANNO").ToString)
                        dtpchallan.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")
                        cmbtrans.Text = dr("TRANSNAME").ToString
                        CMBDYEINGNAME.Text = dr("DYEINGNAME")
                        txtlrno.Text = dr("LRNO").ToString
                        lrdate.Text = Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy")
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)


                        GRIDGREY.Rows.Add(dr("GRIDSRNO").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("DESIGNNO").ToString, dr("COLOR").ToString, dr("LOOMNO"), dr("ROLLNO"), Format(dr("qty"), "0.00"), dr("UNIT").ToString, Format(dr("MTRS"), "0.00"), Format(dr("WT"), "0.00"), dr("RACK"), dr("SHELF"), dr("BARCODE"), dr("GRIDDONE").ToString, dr("OUTPCS"), dr("OUTMTRS"))

                        If Convert.ToBoolean(dr("GRIDDONE")) = True Or Val(dr("OUTMTRS")) > 0 Then
                            GRIDGREY.Rows(GRIDGREY.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If


                    Next

                    If dttable.Rows(0).Item("DONE") = True Then
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If


                    Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.SEARCH(" GREYRECDKNITTING_UPLOAD.GREY_SRNO AS GRIDSRNO, GREYRECDKNITTING_UPLOAD.GREY_REMARKS AS REMARKS, GREYRECDKNITTING_UPLOAD.GREY_NAME AS NAME, GREYRECDKNITTING_UPLOAD.GREY_PHOTO AS IMGPATH ", "", " GREYRECDKNITTING_UPLOAD ", " AND GREYRECDKNITTING_UPLOAD.GREY_NO = " & TEMPGREYNO & " AND GREY_YEARID = " & YearId & " ORDER BY GREYRECDKNITTING_UPLOAD.GREY_SRNO")
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If

                    'ORDER GRID
                    'Dim OBJCMN As New ClsCommon
                    dttable = OBJCMN.SEARCH(" GREYRECDKNITTING_PODETAILS.GREY_GRIDSRNO AS GRIDSRNO, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, GREYRECDKNITTING_PODETAILS.GREY_ORDERPCS AS ORDERQTY, ISNULL(GREYRECDKNITTING_PODETAILS.GREY_ORDERMTRS,0) AS ORDERMTRS, GREYRECDKNITTING_PODETAILS.GREY_FROMNO AS FROMNO, GREYRECDKNITTING_PODETAILS.GREY_FROMSRNO AS FROMSRNO, GREYRECDKNITTING_PODETAILS.GREY_FROMTYPE AS FROMTYPE, GREYRECDKNITTING_PODETAILS.GREY_PCS AS GRNQTY, ISNULL(GREYRECDKNITTING_PODETAILS.GREY_MTRS,0) AS GRNMTRS, ISNULL(GREYRECDKNITTING_PODETAILS.GREY_RATE,0) AS RATE ", "", " GREYRECDKNITTING_PODETAILS INNER JOIN ITEMMASTER ON GREYRECDKNITTING_PODETAILS.GREY_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON GREYRECDKNITTING_PODETAILS.GREY_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GREYRECDKNITTING_PODETAILS.GREY_DESIGNID = DESIGNMASTER.DESIGN_id  ", " AND GREYRECDKNITTING_PODETAILS.GREY_NO = " & TEMPGREYNO & "  AND GREYRECDKNITTING_PODETAILS.GREY_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            GRIDORDER.Rows.Add(Val(DTR("GRIDSRNO")), DTR("ITEMNAME"), DTR("DESIGNNO"), DTR("COLOR"), Val(DTR("ORDERQTY")), Val(DTR("ORDERMTRS")), Val(DTR("FROMNO")), Val(DTR("FROMSRNO")), DTR("FROMTYPE"), Val(DTR("GRNQTY")), Val(DTR("GRNMTRS")), Val(DTR("RATE")))
                        Next
                    End If
                    getsrno(GRIDORDER)

                    total()
                    GRIDGREY.FirstDisplayedScrollingRowIndex = GRIDGREY.RowCount - 1
                    'chkchange.CheckState = CheckState.Checked
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
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

    Private Sub GREYDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles GREYDATE.GotFocus
        GREYDATE.SelectAll()
    End Sub

    Private Sub GREYDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GREYDATE.Validating
        Try
            If GREYDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(GREYDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGREY_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDGREY.CellDoubleClick
        EDITROW()
    End Sub

    Sub EDITROW()
        Try
            If GRIDGREY.CurrentRow.Index >= 0 And GRIDGREY.Item(gsrno.Index, GRIDGREY.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDGREY.Item(gsrno.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                cmbitemname.Text = GRIDGREY.Item(gitemname.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDGREY.Item(GQUALITY.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDGREY.Item(GDESIGN.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                cmbcolor.Text = GRIDGREY.Item(gcolor.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                TXTLOOMNO.Text = GRIDGREY.Item(GLOOMNO.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                TXTROLLNO.Text = GRIDGREY.Item(GROLLNO.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                TXTQTY.Text = GRIDGREY.Item(gQty.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                cmbqtyunit.Text = GRIDGREY.Item(gqtyunit.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                TXTMTRS.Text = GRIDGREY.Item(GMTRS.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                TXTWT.Text = GRIDGREY.Item(GWT.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                CMBRACK.Text = GRIDGREY.Item(GRACK.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                CMBSHELF.Text = GRIDGREY.Item(GSHELF.Index, GRIDGREY.CurrentRow.Index).Value.ToString
                TEMPROW = GRIDGREY.CurrentRow.Index
                txtsrno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdupload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdupload.Click

        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpeg;*.png"
        OpenFileDialog1.ShowDialog()
        txtimgpath.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If txtimgpath.Text.Trim.Length <> 0 Then PBSoftCopy.ImageLocation = txtimgpath.Text.Trim
    End Sub

    Private Sub txtuploadname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
        Try
            If txtuploadremarks.Text.Trim <> "" And txtuploadname.Text.Trim <> "" And PBSoftCopy.ImageLocation <> "" Then
                FILLUPLOAD()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLUPLOAD()

        If GRIDUPLOADDOUBLECLICK = False Then
            gridupload.Rows.Add(Val(txtuploadsrno.Text.Trim), txtuploadremarks.Text.Trim, txtuploadname.Text.Trim, PBSoftCopy.Image)
            getsrno(gridupload)
        ElseIf GRIDUPLOADDOUBLECLICK = True Then

            gridupload.Item(GUSRNO.Index, TEMPUPLOADROW).Value = txtuploadsrno.Text.Trim
            gridupload.Item(GUREMARKS.Index, TEMPUPLOADROW).Value = txtuploadremarks.Text.Trim
            gridupload.Item(GUNAME.Index, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
            gridupload.Item(GUIMGPATH.Index, TEMPUPLOADROW).Value = PBSoftCopy.Image

            GRIDUPLOADDOUBLECLICK = False

        End If
        gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1

        txtuploadsrno.Text = gridupload.RowCount + 1
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSoftCopy.Image = Nothing
        txtimgpath.Clear()

        txtuploadremarks.Focus()

    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And gridupload.Item(GUSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDUPLOADDOUBLECLICK = True
                txtuploadsrno.Text = gridupload.Item(GUSRNO.Index, e.RowIndex).Value
                txtuploadremarks.Text = gridupload.Item(GUREMARKS.Index, e.RowIndex).Value
                txtuploadname.Text = gridupload.Item(GUNAME.Index, e.RowIndex).Value
                PBSoftCopy.Image = gridupload.Item(GUIMGPATH.Index, e.RowIndex).Value

                TEMPUPLOADROW = e.RowIndex
                txtuploadremarks.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
        Try
            If e.KeyCode = Keys.Delete And gridupload.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                If GRIDUPLOADDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                gridupload.Rows.RemoveAt(gridupload.CurrentRow.Index)
                getsrno(gridupload)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSoftCopy.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtuploadsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuploadsrno.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(GUSRNO.Index).Value) + 1
            Else
                txtuploadsrno.Text = 1
            End If
        End If
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress
        numkeypress(e, TXTQTY, Me)
    End Sub
    Private Sub txtrollno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTROLLNO.KeyPress
        If ClientName <> "VALIANT" Then numkeypress(e, TXTROLLNO, Me)
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress, TXTMTRS.KeyPress
        numdotkeypress(e, sender, Me)
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

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDGREY.RowCount = 0
                TEMPGREYNO = Val(tstxtbillno.Text)
                If TEMPGREYNO > 0 Then
                    EDIT = True
                    GreyRecdKnitting_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDGREY.RowCount = 0
LINE1:
            TEMPGREYNO = Val(TXTGREYNO.Text) - 1
            If TEMPGREYNO > 0 Then
                EDIT = True
                GreyRecdKnitting_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDGREY.RowCount = 0 And TEMPGREYNO > 1 Then
                TXTGREYNO.Text = TEMPGREYNO
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
            TEMPGREYNO = Val(TXTGREYNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTGREYNO.Text.Trim
            CLEAR()
            If Val(TXTGREYNO.Text) - 1 >= TEMPGREYNO Then
                EDIT = True
                GreyRecdKnitting_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDGREY.RowCount = 0 And TEMPGREYNO < MAXNO Then
                TXTGREYNO.Text = TEMPGREYNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
        'Try
        '    If txtimgpath.Text.Trim <> "" Then
        '        If Path.GetExtension(txtimgpath.Text.Trim) = ".pdf" Then
        '            System.Diagnostics.Process.Start(txtimgpath.Text.Trim)
        '        Else
        '            Dim objVIEW As New ViewImage
        '            objVIEW.pbsoftcopy.ImageLocation = PBSoftCopy.ImageLocation
        '            objVIEW.ShowDialog()
        '        End If
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try

        Try
            If gridupload.SelectedRows.Count > 0 Then
                Dim objVIEW As New ViewImage
                objVIEW.pbsoftcopy.Image = PBSoftCopy.Image
                objVIEW.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGREY_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDGREY.CellValidating
        Try
            Dim colNum As Integer = GRIDGREY.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GWT.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDGREY.CurrentCell.Value = Nothing Then GRIDGREY.CurrentCell.Value = "0.00"
                        GRIDGREY.CurrentCell.Value = Convert.ToDecimal(GRIDGREY.Item(colNum, e.RowIndex).Value)
                        '' everything is good
                        total()
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

    Private Sub GRIDGREY_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDGREY.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDGREY.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDGREY.Rows.RemoveAt(GRIDGREY.CurrentRow.Index)
                getsrno(GRIDGREY)
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
                    row.Cells(GUSRNO.Index).Value = i + 1
                    i = i + 1
                End If
            Next
            'End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            GRIDGREY.Enabled = True

            If GRIDDOUBLECLICK = False Then
                GRIDGREY.Rows.Add(Val(txtsrno.Text.Trim), cmbitemname.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, cmbcolor.Text.Trim, TXTLOOMNO.Text.Trim, TXTROLLNO.Text.Trim, Format(Val(TXTQTY.Text.Trim), "0.00"), cmbqtyunit.Text.Trim, Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTWT.Text.Trim), "0.00"), CMBRACK.Text.Trim, CMBSHELF.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0)
                getsrno(GRIDGREY)
            ElseIf GRIDDOUBLECLICK = True Then
                GRIDGREY.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)

                GRIDGREY.Item(gitemname.Index, TEMPROW).Value = cmbitemname.Text.Trim
                GRIDGREY.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim

                GRIDGREY.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
                GRIDGREY.Item(gcolor.Index, TEMPROW).Value = cmbcolor.Text.Trim
                GRIDGREY.Item(GLOOMNO.Index, TEMPROW).Value = TXTLOOMNO.Text.Trim
                GRIDGREY.Item(GROLLNO.Index, TEMPROW).Value = TXTROLLNO.Text.Trim
                GRIDGREY.Item(gQty.Index, TEMPROW).Value = Val(TXTQTY.Text.Trim)
                GRIDGREY.Item(gqtyunit.Index, TEMPROW).Value = cmbqtyunit.Text.Trim
                GRIDGREY.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
                GRIDGREY.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.00")
                GRIDGREY.Item(GRACK.Index, TEMPROW).Value = CMBRACK.Text.Trim
                GRIDGREY.Item(GSHELF.Index, TEMPROW).Value = CMBSHELF.Text.Trim
                GRIDDOUBLECLICK = False
            End If

            total()

            GRIDGREY.FirstDisplayedScrollingRowIndex = GRIDGREY.RowCount - 1

            txtsrno.Clear()
            'cmbitemname.Text = ""
            ' CMBQUALITY.Text = ""
            ' CMBDESIGN.Text = ""
            ' cmbcolor.Text = ""


            TXTWT.Clear()
            'TXTROLLNO.Clear()
            'txtqty.Clear()
            'cmbqtyunit.Text = ""
            TXTMTRS.Clear()
            TXTLOOMNO.Clear()


            CMBRACK.Text = ""
            CMBSHELF.Text = ""
            If GRIDGREY.RowCount > 0 Then
                txtsrno.Text = Val(GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If

            If ClientName = "RAKSHA" Then
                TXTROLLNO.Text = Val(TXTROLLNO.Text.Trim) + 1
            End If
            cmbitemname.Focus()
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

            Dim OBJEMB As New GreyRecdKnittingDetails
            OBJEMB.MdiParent = MDIMain
            OBJEMB.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtuploadsrno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtuploadsrno.KeyPress
        enterkeypress(e, Me)
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
            COLORVALIDATE(cmbcolor, e, Me, CMBDESIGN.Text.Trim, cmbitemname.Text.Trim)
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
                Dim OBJQUALITY As New SelectQuality
                OBJQUALITY.ShowDialog()
                If OBJQUALITY.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQUALITY.TEMPNAME
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

    Private Sub cmbQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub cmbqtyunit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbqtyunit.Validating
        Try
            If cmbqtyunit.Text.Trim <> "" Then unitvalidate(cmbqtyunit, e, Me)
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

    Private Sub CMBSHELF_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSHELF.Validated
        Try
            If cmbitemname.Text.Trim <> "" And Val(TXTQTY.Text.Trim) > 0 And cmbqtyunit.Text.Trim <> "" And Val(TXTWT.Text.Trim) > 0 Then
                If GRIDDOUBLECLICK = False Then
                    If EDIT = True Then
                        'GET LAST BARCODE SRNO
                        Dim LSRNO As Integer = 0
                        Dim RSRNO As Integer = 0
                        Dim SNO As Integer = 0
                        LSRNO = InStr(GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                        RSRNO = InStr(LSRNO + 1, GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                        SNO = GRIDGREY.Rows(GRIDGREY.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                        TXTBARCODE.Text = "GRK-" & Val(TXTGREYNO.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                    Else
                        TXTBARCODE.Text = "GRK-" & Val(TXTGREYNO.Text.Trim) & "/" & GRIDGREY.RowCount + 1 & "/" & YearId
                    End If
                End If
                fillgrid()
            Else

                If cmbitemname.Text.Trim = "" Then
                    MsgBox("Enter Item Name", MsgBoxStyle.Critical)
                    cmbitemname.Focus()
                ElseIf Val(TXTQTY.Text.Trim) = 0 Then
                    MsgBox("Enter Quantity", MsgBoxStyle.Critical)
                    TXTQTY.Focus()
                ElseIf cmbqtyunit.Text.Trim = "" Then
                    MsgBox("Enter Unit", MsgBoxStyle.Critical)
                    cmbqtyunit.Focus()
                ElseIf Val(TXTWT.Text.Trim) = 0 Then
                    MsgBox("Enter Wt.", MsgBoxStyle.Critical)
                    TXTWT.Focus()
                End If
            End If
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

    Private Sub GreyRecdKnitting_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "MARKIN" Then Me.Text = "Grey Recd From Jobber"
            If ClientName = "RAKSHA" Then CMBQUALITY.TabStop = False
            If ClientName = "RAKSHA" Then CMBDESIGN.TabStop = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMTRS_Validated(sender As Object, e As EventArgs) Handles TXTMTRS.Validated
        Try
            'GET WT AUTO
            If ClientName = "NAYRA" And cmbitemname.Text.Trim <> "" And Val(TXTMTRS.Text.Trim) > 0 And Val(TXTWT.Text.Trim) = 0 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(ITEMMASTER.ITEM_TOTALWT,0) AS QUALITYWT, ISNULL(ITEMMASTER.ITEM_WEFTTL,0) AS WEFTTL ", "", " ITEMMASTER ", " AND ITEMMASTER.ITEM_NAME = '" & cmbitemname.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If Val(DT.Rows(0).Item("QUALITYWT")) > 0 And Val(DT.Rows(0).Item("WEFTTL")) > 0 Then TXTWT.Text = Format((Val(DT.Rows(0).Item("QUALITYWT")) * Val(TXTMTRS.Text.Trim)) / Val(DT.Rows(0).Item("WEFTTL")), "0.000")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdselectPO_Click(sender As Object, e As EventArgs) Handles cmdselectPO.Click
        Try

            If cmbname.Text.Trim = "" Then
                MsgBox("Select Party Name", MsgBoxStyle.Critical)
                cmbname.Focus()
                Exit Sub
            End If




            Dim DTPO As New DataTable

            Dim OBJSELECTPO As New SelectPO
            OBJSELECTPO.PARTYNAME = cmbname.Text.Trim
            OBJSELECTPO.FRMSTRING = "GREY RECD KNITTING"
            OBJSELECTPO.ShowDialog()
            DTPO = OBJSELECTPO.DT
            If DTPO.Rows.Count > 0 Then

                ''  GETTING DISTINCT PONO NO IN TEXTBOX
                'Dim DV As DataView = DTPO.DefaultView
                'Dim NEWDT As DataTable = DV.ToTable(True, "PONO")
                'For Each DTR As DataRow In NEWDT.Rows
                '    If txtpono.Text.Trim = "" Then
                '        txtpono.Text = DTR("PONO").ToString
                '    Else
                '        txtpono.Text = txtpono.Text & "," & DTR("PONO").ToString
                '    End If
                'Next

                fillledger(cmbname, EDIT, " And GROUPMASTER.GROUP_SECONDARY = '" & DTPO.Rows(0).Item("GROUPNAME") & "' ")
                cmbname.Text = DTPO.Rows(0).Item("NAME")
                cmbtrans.Text = DTPO.Rows(0).Item("TRANSPORT")




                'BEFORE ADDING THE ROW IN ORDERDER GRID CHECK WHETHER SAME ORDERNO AN SRNO IS PRESENT IN GRID OR NOT
                For Each DTROW As DataRow In DTPO.Rows
                    For Each ROW As DataGridViewRow In GRIDORDER.Rows
                        If Val(ROW.Cells(OFROMNO.Index).Value) = Val(DTROW("PONO")) And Val(ROW.Cells(OFROMSRNO.Index).Value) = Val(DTROW("GRIDSRNO")) And ROW.Cells(OFROMTYPE.Index).Value = DTROW("TYPE") Then GoTo NEXTLINE
                    Next

                    GRIDORDER.Rows.Add(0, DTROW("ITEMNAME"), DTROW("DESIGNNO"), DTROW("COLOR"), Val(DTROW("QTY")), Val(DTROW("MTRS")), DTROW("PONO"), DTROW("GRIDSRNO"), DTROW("TYPE"), 0, 0, Val(DTROW("RATE")))
                    'If ClientName = "MOMAI" Or ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                    '    'FILL SAME DATA IN GRNGRID
                    '    gridgrn.Rows.Add(0, "FRESH", DTROW("ITEMNAME"), "", "", DTROW("DESIGNNO"), "", DTROW("COLOR"), Val(DTROW("QTY")), "Pcs", 0, Val(DTROW("MTRS")), "", "", 0, 0, 0, 0, "Mtrs", 0, "", 0, 0, 0, DTROW("PONO"), DTROW("GRIDSRNO"), 0, "")
                    'End If
NEXTLINE:
                Next
                getsrno(GRIDORDER)

            End If

            getsrno(GRIDGREY)
            cmdselectPO.Enabled = True
            total()
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

    Private Sub CMBDYEINGNAME_Enter(sender As Object, e As EventArgs) Handles CMBDYEINGNAME.Enter
        Try
            If CMBDYEINGNAME.Text.Trim = "" Then FILLNAME(CMBDYEINGNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDYEINGNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBDYEINGNAME.Validating
        Try
            If CMBDYEINGNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBDYEINGNAME, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class