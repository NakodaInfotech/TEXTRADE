Imports BL
Imports System.IO
Imports System.ComponentModel

Public Class SaleGatePass

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim ALLOWMANUALSALNO As Boolean = False
    Public EDIT As Boolean
    Public TEMPENTRYNO As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click

        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If
            Dim alParaval As New ArrayList

            If TXTENTRYNO.ReadOnly = False Then
                alParaval.Add(Val(TXTENTRYNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(Format(Convert.ToDateTime(DTDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBAGENT.Text.Trim)
            alParaval.Add(CMBTRANS.Text.Trim)
            alParaval.Add(CMBDELIVERY.Text.Trim)
            alParaval.Add(CMBFROMCITY.Text.Trim)
            alParaval.Add(CMBTOCITY.Text.Trim)

            alParaval.Add(Val(LBLTOTALPCS.Text.Trim))
            alParaval.Add(Val(LBLTOTALMTRS.Text.Trim))
            alParaval.Add(Val(LBLTOTALBALES.Text.Trim))

            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(TXTVEHICLENO.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(YearId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(Val(LBLTOTALAMT.Text.Trim))


            Dim gridsrno As String = ""
            Dim CHK As String = ""
            Dim GRIDNAME As String = ""
            Dim GRIDTRANSNAME As String = ""
            Dim GRIDTOCITY As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim PRINTDESC As String = ""
            Dim BALENO As String = ""
            Dim PCS As String = ""
            Dim MTRS As String = ""
            Dim NOOFBALES As String = ""
            Dim GDNNO As String = ""
            Dim PARTYPONO As String = ""
            Dim GDNTYPE As String = ""
            Dim RATE As String = ""
            Dim AMOUNT As String = ""
            Dim BARCODE As String = ""
            Dim UNIT As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDGP.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(GSRNO.Index).Value.ToString
                        CHK = row.Cells(GVERIFIED.Index).Value.ToString
                        GRIDNAME = row.Cells(GNAME.Index).Value.ToString
                        GRIDTRANSNAME = row.Cells(GTRANSPORT.Index).Value.ToString
                        GRIDTOCITY = row.Cells(GTOCITY.Index).Value.ToString
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(GSHADE.Index).Value.ToString
                        If row.Cells(GDESCRIPTION.Index).Value <> Nothing Then PRINTDESC = row.Cells(GDESCRIPTION.Index).Value.ToString Else PRINTDESC = ""
                        BALENO = row.Cells(GBALENO.Index).Value.ToString
                        PCS = Val(row.Cells(Gpcs.Index).Value.ToString)
                        MTRS = Val(row.Cells(Gmtrs.Index).Value)
                        NOOFBALES = Val(row.Cells(GNOOFBALES.Index).Value)
                        GDNNO = Val(row.Cells(GGDNNO.Index).Value)
                        PARTYPONO = row.Cells(GPARTYPONO.Index).Value
                        GDNTYPE = row.Cells(GGDNTYPE.Index).Value
                        RATE = Val(row.Cells(GRATE.Index).Value)
                        AMOUNT = Val(row.Cells(GAMOUNT.Index).Value)
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        UNIT = row.Cells(GQTYUNIT.Index).Value.ToString

                    Else

                        gridsrno = gridsrno & "|" & row.Cells(GSRNO.Index).Value
                        CHK = CHK & "|" & row.Cells(GVERIFIED.Index).Value
                        GRIDNAME = GRIDNAME & "|" & row.Cells(GNAME.Index).Value.ToString
                        GRIDTRANSNAME = GRIDTRANSNAME & "|" & row.Cells(GTRANSPORT.Index).Value.ToString
                        GRIDTOCITY = GRIDTOCITY & "|" & row.Cells(GTOCITY.Index).Value.ToString
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GSHADE.Index).Value.ToString
                        If row.Cells(GDESCRIPTION.Index).Value <> Nothing Then PRINTDESC = PRINTDESC & "|" & row.Cells(GDESCRIPTION.Index).Value.ToString Else PRINTDESC = PRINTDESC & "|" & ""
                        BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString
                        PCS = PCS & "|" & Val(row.Cells(Gpcs.Index).Value)
                        MTRS = MTRS & "|" & Val(row.Cells(Gmtrs.Index).Value)
                        NOOFBALES = NOOFBALES & "|" & Val(row.Cells(GNOOFBALES.Index).Value)
                        GDNNO = GDNNO & "|" & Val(row.Cells(GGDNNO.Index).Value)
                        PARTYPONO = PARTYPONO & "|" & row.Cells(GPARTYPONO.Index).Value
                        GDNTYPE = GDNTYPE & "|" & row.Cells(GGDNTYPE.Index).Value
                        RATE = RATE & "|" & Val(row.Cells(GRATE.Index).Value)
                        AMOUNT = AMOUNT & "|" & Val(row.Cells(GAMOUNT.Index).Value)
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        UNIT = UNIT & "|" & row.Cells(GQTYUNIT.Index).Value.ToString

                    End If
                End If



            Next

            alParaval.Add(gridsrno)
            alParaval.Add(CHK)
            alParaval.Add(GRIDNAME)
            alParaval.Add(GRIDTRANSNAME)
            alParaval.Add(GRIDTOCITY)
            alParaval.Add(ITEMNAME)
            alParaval.Add(QUALITY)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(PRINTDESC)
            alParaval.Add(BALENO)
            alParaval.Add(PCS)
            alParaval.Add(MTRS)
            alParaval.Add(NOOFBALES)
            alParaval.Add(GDNNO)
            alParaval.Add(PARTYPONO)
            alParaval.Add(GDNTYPE)
            alParaval.Add(RATE)
            alParaval.Add(AMOUNT)
            alParaval.Add(BARCODE)
            alParaval.Add(UNIT)

            If PBIMAGE1.Image IsNot Nothing Then
                Dim MS As New IO.MemoryStream
                PBIMAGE1.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                alParaval.Add(MS.ToArray)
            Else
                alParaval.Add(DBNull.Value)
            End If
            If CHKPARTIAL.Checked = True Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(CMBVEHICLENAME.Text.Trim)

            Dim OBJCLSPROFORMA As New ClsGatePass()
            OBJCLSPROFORMA.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTT As DataTable = OBJCLSPROFORMA.SAVE()
                TXTENTRYNO.Text = DTT.Rows(0).Item(0)
                MsgBox("Details Added")

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPENTRYNO)
                Dim IntResult As Integer = OBJCLSPROFORMA.UPDATE()
                MsgBox("Details Updated")

            End If

            If ClientName <> "SNCM" Then PRINTREPORT(Val(TXTENTRYNO.Text.Trim))
            EDIT = False
            CLEAR()
            CMBNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PRINTREPORT(ENTRYNO As Integer)
        Try
            If MsgBox("Print Gate Pass", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim OBJGP As New GDNDESIGN
                OBJGP.MdiParent = MDIMain
                OBJGP.FRMSTRING = "GATEPASS"
                OBJGP.FORMULA = "{SALEGATEPASS.GP_NO}=" & Val(ENTRYNO) & " and {SALEGATEPASS.GP_yearid}=" & YearId
                OBJGP.Show()
            End If

            If ClientName <> "AVIS" Then Exit Sub
            If MsgBox("Print Packing Slip", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJGPPS As New GDNDESIGN
            OBJGPPS.MdiParent = MDIMain
            OBJGPPS.FRMSTRING = "GPPACKINGSLIP"
            OBJGPPS.FORMULA = "{SALEGATEPASS.GP_NO}=" & Val(ENTRYNO) & " and {SALEGATEPASS.GP_yearid}=" & YearId
            OBJGPPS.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    EP.SetError(lbllocked, "Entry Locked, Invoice Made")
                    Exit Sub
                End If

                If MsgBox("Wish to Delete Sale Gate Pass?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                'DONE BY GULKIT
                'BEFORE UPDATING REVERSE THE ENTRY IN SCHEDULEMASTER_DESC
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPENTRYNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)


                Dim OBJPRO As New ClsGatePass
                OBJPRO.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJPRO.DELETE
                MsgBox("Sale Gate Pass Deleted")

                CLEAR()
                EDIT = False
                CMBNAME.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAXNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(GP_no),0) + 1 ", " SALEGATEPASS ", " and GP_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTENTRYNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Sub CLEAR()

        EP.Clear()
        TXTENTRYNO.Clear()
        DTDATE.Text = Now.Date
        tstxtbillno.Clear()

        'If ALLOWMANUALGDNNO = True Then
        '    TXTENTRYNO.ReadOnly = False
        '    TXTENTRYNO.BackColor = Color.LemonChiffon
        'Else
        '    TXTENTRYNO.ReadOnly = True
        '    TXTENTRYNO.BackColor = Color.Linen
        'End If

        If ALLOWMANUALGDNNO = True Then

            TXTENTRYNO.ReadOnly = True
            TXTENTRYNO.BackColor = Color.Linen

        Else
            TXTENTRYNO.ReadOnly = False
            TXTENTRYNO.BackColor = Color.LemonChiffon
        End If
        CHKPARTIAL.CheckState = CheckState.Unchecked
        lbllocked.Visible = False
        PBlock.Visible = False

        CMBNAME.Text = ""
        CMBAGENT.Text = ""
        CMBTRANS.Text = ""

        CMBDELIVERY.Text = ""
        If CMPCITYNAME <> "" Then CMBFROMCITY.Text = CMPCITYNAME Else CMBFROMCITY.Text = ""
        CMBTOCITY.Text = ""

        EP.Clear()
        txtremarks.Clear()
        TXTVEHICLENO.Clear()
        CMBVEHICLENAME.Text = ""
        TXTSRNO.Text = 1
        CMBITEM.Text = ""
        CMBQUALITY.Text = ""
        CMBDESIGN.Text = ""
        CMBSHADE.Text = ""
        TXTDESCRIPTION.Clear()
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTMTRS.Clear()
        TXTNOOFBALES.Clear()
        GRIDGP.RowCount = 0

        LBLTOTALPCS.Text = 0.0
        LBLTOTALMTRS.Text = 0
        LBLTOTALBALES.Text = 0.0
        LBLTOTALAMT.Text = 0.0
        PBIMAGE1.Image = Nothing
        CMBNAME.Enabled = True

        GETMAXNO()
        CMDSELECTGDN.Enabled = True
        LBLWHATSAPP.Visible = False
        TXTBARCODE.Clear()
        If ALLOWMANUALSALNO = True Then
            TXTENTRYNO.ReadOnly = False
            TXTENTRYNO.BackColor = Color.LemonChiffon
        Else
            TXTENTRYNO.ReadOnly = True
            TXTENTRYNO.BackColor = Color.Linen
        End If
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUPMASTER.GROUP_NAME <> 'HASTE DEBTORS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVARY_Enter(sender As Object, e As EventArgs) Handles CMBDELIVERY.Enter
        Try
            If CMBDELIVERY.Text.Trim = "" Then FILLNAME(CMBDELIVERY, EDIT, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
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

    Private Sub SaleGatePass_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow



            DTROW = USERRIGHTS.Select("FormName = 'GDN'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            Cursor.Current = Cursors.WaitCursor
            'If ClientName = "AVIS" Then ALLOWMANUALSALNO = True
            FILLCMB()
            CLEAR()



            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJCMN As New ClsCommon
                Dim OBJCLSPROFORMA As New ClsGatePass()
                Dim dttable As New DataTable

                dttable = OBJCLSPROFORMA.selectGATE(TEMPENTRYNO, CmpId, YearId)
                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTENTRYNO.Text = TEMPENTRYNO
                        TXTENTRYNO.ReadOnly = True
                        DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(dr("NAME"))
                        CMBAGENT.Text = Convert.ToString(dr("AGENT"))
                        CMBTRANS.Text = Convert.ToString(dr("TRANSPORT"))

                        CMBDELIVERY.Text = Convert.ToString(dr("DELIVARY"))
                        CMBFROMCITY.Text = Convert.ToString(dr("FROMCITY"))
                        CMBTOCITY.Text = Convert.ToString(dr("TOCITY"))
                        txtremarks.Text = Convert.ToString(dr("REMARKS"))
                        TXTVEHICLENO.Text = Convert.ToString(dr("VEHICLENO"))
                        CMBVEHICLENAME.Text = Convert.ToString(dr("VEHICLENAME"))

                        GRIDGP.Rows.Add(dr("SRNO"), dr("CHK"), dr("GRIDNAME"), dr("GRIDTRANSNAME"), dr("GRIDTOCITY"), Convert.ToString(dr("ITEMNAME")), Convert.ToString(dr("QUALITY")), Convert.ToString(dr("DESIGN")), Convert.ToString(dr("COLOR")), dr("PRINTDESC"), Convert.ToString(dr("BALENO")), Format(Val(dr("PCS")), "0.00"), Format(Val(dr("MTRS")), "0.00"), Val(dr("NOOFBALES")), Format(Val(dr("RATE")), "0.00"), Format(Val(dr("AMOUNT")), "0.00"), Val(dr("GDNNO")), dr("PARTYPONO"), dr("GDNTYPE"), dr("BARCODE"), dr("UNIT"))

                        If Convert.ToBoolean(dr("SALEDONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If
                        If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True

                        If IsDBNull(dr("IMAGE1")) = False Then
                            PBIMAGE1.Image = Image.FromStream(New IO.MemoryStream(DirectCast(dr("IMAGE1"), Byte())))
                        Else
                            PBIMAGE1.Image = Nothing
                        End If
                        CHKPARTIAL.Checked = Convert.ToBoolean(dr("PARTIALSAVE"))
                    Next

                    TOTAL()
                    GRIDGP.FirstDisplayedScrollingRowIndex = GRIDGP.RowCount - 1
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

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If CMBDELIVERY.Text.Trim = "" And CMBNAME.Text.Trim <> "" Then CMBDELIVERY.Text = CMBNAME.Text.Trim

        If ALLOWMANUALSALNO = True Then
            If Val(TXTENTRYNO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMNn As New ClsCommon
                Dim dttable As DataTable = OBJCMNn.SEARCH(" ISNULL(SALEGATEPASS.GP_NO,0)  AS RECONO", "", " SALEGATEPASS ", "  AND SALEGATEPASS.GP_NO=" & Val(TXTENTRYNO.Text.Trim) & " AND SALEGATEPASS.GP_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Rec No Already Exist")
                    bln = False
                End If
            End If
        End If

        Dim OBJCMN As New ClsCommon
        Dim DT As New DataTable
        If Val(TXTENTRYNO.Text.Trim) = 0 Then
            EP.SetError(TXTENTRYNO, "Enter Invoice No")
            bln = False
        End If

        If lbllocked.Visible = True Then
            EP.SetError(lbllocked, "Entry Locked, Invoice Made")
            bln = False
        End If

        If ClientName = "SNCM" And TXTVEHICLENO.Text.Trim = "" Then
            EP.SetError(TXTVEHICLENO, "Enter Vehicle No")
            bln = False
        End If
        If ClientName = "SNCM" And CMBTRANS.Text.Trim = "" Then
            EP.SetError(CMBTRANS, "Enter Transporter Name")
            bln = False
        End If

        If ClientName = "AVIS" Or ClientName = "SNCM" Then
            If ClientName = "AVIS" And CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, " Please Fill Party Name ")
                bln = False
            End If

            If ClientName = "AVIS" And CMBAGENT.Text.Trim.Length = 0 Then
                EP.SetError(CMBAGENT, " Please Fill Agent Name ")
                bln = False
            End If

            For Each ROW As DataGridViewRow In GRIDGP.Rows
                'CHECK WHETHER PARTICULAR ITEM | DESIGN | SHADE IS ALREADY SENT TO THIS PARTY IN THE FORM (UNIT) OF SAMEPLE OR NOT, IF NOT THEN GIVE INTIMATION
                'DONT GIVE YEARID CLAUSE, IT MAY BE SENT IN LAST YEAR
                Dim DTSMP As DataTable = OBJCMN.SEARCH(" GDN.GDN_NO AS GDNNO", "", " GDN INNER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id INNER JOIN UNITMASTER ON GDN_DESC.GDN_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id ", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & ROW.Cells(GITEMNAME.Index).Value & "' AND DESIGNMASTER.DESIGN_NO = '" & ROW.Cells(GDESIGN.Index).Value & "' AND COLORMASTER.COLOR_NAME  = '" & ROW.Cells(GSHADE.Index).Value & "' AND UNITMASTER.UNIT_ABBR ='Sample'")
                If DTSMP.Rows.Count = 0 And ClientName = "AVIS" Then
                    If MsgBox("Sample of Design No " & ROW.Cells(GDESIGN.Index).Value & " & Shade No " & ROW.Cells(GSHADE.Index).Value & " is not Sent to Party, Wish To Proceed? ", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        EP.SetError(CMBNAME, "Sample of Design No " & ROW.Cells(GDESIGN.Index).Value & " & Shade No " & ROW.Cells(GSHADE.Index).Value & " is not Sent to Party")
                        bln = False
                        Exit For
                    End If
                End If


                'THIS PARTICULAR CODE IS FOR SNCM FOR GATEPASS WHETHER ANY ONE OF THE CHECK BOX IS UNCHECK THEN GIVE ERROR 
                'BY WASEEM
                If ClientName = "SNCM" And ROW.Cells(GVERIFIED.Index).Value = 0 Then
                    'EP.SetError(TXTBARCODE, "Please Scan Barcodes of All the Bales in the List")
                    'bln = False
                    'Exit For
                    CHKPARTIAL.CheckState = CheckState.Checked
                End If
            Next

        End If

        If GRIDGP.RowCount = 0 Then
            EP.SetError(CMBNAME, "Enter Bill Details")
            bln = False
        End If

        If DTDATE.Text = "__/__/____" Then
            EP.SetError(DTDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTDATE.Text) Then
                EP.SetError(DTDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If
        Return bln

    End Function

    Private Sub SaleGatePass_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNoCancel)
                    If tempmsg = vbCancel Then Exit Sub
                    If tempmsg = vbYes Then cmdOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call PrintToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolPREVIOUS_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_CLICK(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
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
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            If CMBAGENT.Text.Trim = "" Then FILLNAME(CMBAGENT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
            If CMBTRANS.Text.Trim = "" Then FILLNAME(CMBTRANS, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
            If CMBFROMCITY.Text.Trim = "" Then fillCITY(CMBFROMCITY, EDIT)
            If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, EDIT)
            If CMBDELIVERY.Text.Trim = "" Then FILLNAME(CMBDELIVERY, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
            If CMBVEHICLENAME.Text.Trim = "" Then FILLVEHICLE(CMBVEHICLENAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLPRIVIOUS.Click
        Try
            GRIDGP.RowCount = 0
LINE1:
            TEMPENTRYNO = Val(TXTENTRYNO.Text) - 1
            If TEMPENTRYNO > 0 Then
                EDIT = True
                SaleGatePass_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDGP.RowCount = 0 And TEMPENTRYNO > 1 Then
                TXTENTRYNO.Text = TEMPENTRYNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_CLICK(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLNEXT.Click
        Try
            GRIDGP.RowCount = 0
LINE1:
            TEMPENTRYNO = Val(TXTENTRYNO.Text) + 1
            GETMAXNO()
            Dim MAXNO As Integer = TXTENTRYNO.Text.Trim
            CLEAR()
            If Val(TXTENTRYNO.Text) - 1 >= TEMPENTRYNO Then
                EDIT = True
                SaleGatePass_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDGP.RowCount = 0 And TEMPENTRYNO < MAXNO Then
                TXTENTRYNO.Text = TEMPENTRYNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPENTOOLSTRIP.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objpodtls As New SaleGatePassDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(sender As Object, e As EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillledger(CMBAGENT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(sender As Object, e As CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then NAMEVALIDATE(CMBAGENT, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTGDN_Click(sender As Object, e As EventArgs) Handles CMDSELECTGDN.Click
        Try

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If CMBNAME.Text = "" And (ClientName = "AVIS" Or ClientName = "RADHA") Then
                MsgBox("Select Party Name First !", MsgBoxStyle.Critical)
                Exit Sub
            End If


            Dim DTTABLE As DataTable
            Dim OBJSELECTPO As New SelectGdn
            OBJSELECTPO.PARTYNAME = CMBNAME.Text.Trim
            OBJSELECTPO.FRMSTRING = "SELECTGATEPASS"
            OBJSELECTPO.ShowDialog()

            DTTABLE = OBJSELECTPO.DT1

            Dim TEMPCHALLANNO As String = ""

            Dim i As Integer = 0
            If DTTABLE.Rows.Count > 0 Then

                ''  GETTING DISTINCT CHALLAN NO IN TEXTBOX
                Dim DV As DataView = DTTABLE.DefaultView
                Dim NEWDT As DataTable = DV.ToTable(True, "GDNNO")
                For Each DTR As DataRow In NEWDT.Rows
                    If TEMPCHALLANNO.Trim = "" Then
                        TEMPCHALLANNO = DTR("GDNNO").ToString
                    Else
                        TEMPCHALLANNO = TEMPCHALLANNO & "," & DTR("GDNNO").ToString
                    End If
                Next

                Dim OBJCMN As New ClsCommon()
                Dim DT1 As New DataTable
                If DTTABLE.Rows(0).Item("FROMTYPE") = "GDN" Then
                    If ClientName = "AVIS" Or ClientName = "RADHA" Then
                        DT1 = OBJCMN.SEARCH("  LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '') AS DELIVERYNAME, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, GDN.GDN_NO AS GDNNO, SUM(GDN_DESC.GDN_PCS) AS PCS, SUM(GDN_DESC.GDN_MTRS) AS MTRS, ISNULL(GDN.GDN_BALENOFROM,0) AS NOOFBALES, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO,'') AS PARTYPONO, 'GDN' AS FROMTYPE, ISNULL(TRANSLEDGERS.ACC_RD, 0) AS TRANSRATE, CAST(GDN.GDN_REMARKS AS VARCHAR(1000)) AS REMARKS, '' AS TRANSREFNO ", "", " LEDGERS AS AGENTLEDGERS RIGHT OUTER JOIN GDN_DESC INNER JOIN GDN ON GDN_DESC.GDN_NO = GDN.GDN_NO AND GDN_DESC.GDN_YEARID = GDN.GDN_YEARID INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON GDN.GDN_CITYid = TOCITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON GDN.GDN_DISPATCHTOID = DELIVERYLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON GDN.GDN_transid = TRANSLEDGERS.Acc_id ON AGENTLEDGERS.Acc_id = GDN.GDN_AGENTID  ", "  and GDN.GDN_NO IN(" & TEMPCHALLANNO & ") and ROUND(ISNULL(GDN.GDN_OUTMTRS,0),0) = 0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY GDN.GDN_NO,ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), LEDGERS.Acc_cmpname, ISNULL(AGENTLEDGERS.Acc_cmpname, ''), ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(DELIVERYLEDGERS.Acc_cmpname, ''), ISNULL(TOCITYMASTER.city_name, ''), GDN.GDN_BALENOFROM, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO,''), ISNULL(TRANSLEDGERS.ACC_RD, 0), CAST(GDN.GDN_REMARKS AS VARCHAR(1000)) ")
                    ElseIf ClientName = "SNCM" Then
                        DT1 = OBJCMN.SEARCH("  LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '') AS DELIVERYNAME, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, GDN.GDN_NO AS GDNNO, SUM(GDN_DESC.GDN_PCS) AS PCS, SUM(GDN_DESC.GDN_MTRS) AS MTRS, ISNULL(GDN.GDN_BALENOFROM,0) AS NOOFBALES, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO,'') AS PARTYPONO, 'GDN' AS FROMTYPE, ISNULL(TRANSLEDGERS.ACC_RD, 0) AS TRANSRATE, CAST(GDN.GDN_REMARKS AS VARCHAR(1000)) AS REMARKS, '' AS TRANSREFNO , ISNULL( GDN_DESC.GDN_BARCODE,'' ) AS BARCODE , ISNULL(UNITMASTER.unit_abbr,'') AS UNIT ", "", " LEDGERS AS TRANSLEDGERS RIGHT OUTER JOIN GDN_DESC INNER JOIN GDN ON GDN_DESC.GDN_NO = GDN.GDN_NO AND GDN_DESC.GDN_YEARID = GDN.GDN_YEARID INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN UNITMASTER ON GDN_DESC.GDN_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN COLORMASTER ON GDN_DESC.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GDN_DESC.GDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON GDN_DESC.GDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON GDN.GDN_CITYid = TOCITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON GDN.GDN_DISPATCHTOID = DELIVERYLEDGERS.Acc_id ON TRANSLEDGERS.Acc_id = GDN.GDN_transid LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON GDN.GDN_AGENTID = AGENTLEDGERS.Acc_id   ", "  and GDN.GDN_NO IN(" & TEMPCHALLANNO & ") and ROUND(ISNULL(GDN.GDN_OUTMTRS,0),0) = 0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY GDN.GDN_NO,ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), LEDGERS.Acc_cmpname, ISNULL(AGENTLEDGERS.Acc_cmpname, ''), ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(DELIVERYLEDGERS.Acc_cmpname, ''), ISNULL(TOCITYMASTER.city_name, ''), GDN.GDN_BALENOFROM, ISNULL(GDN_DESC.GDN_GRIDPARTYPONO,''), ISNULL(TRANSLEDGERS.ACC_RD, 0), CAST(GDN.GDN_REMARKS AS VARCHAR(1000))  , ISNULL( GDN_DESC.GDN_BARCODE,'' ), ISNULL(UNITMASTER.unit_abbr,'') ")
                    Else
                        DT1 = OBJCMN.SEARCH("  LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '')  AS DELIVERYNAME, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, '' AS ITEMNAME, '' AS QUALITY, '' AS DESIGNNO, '' AS COLOR, GDN.GDN_NO AS GDNNO, GDN.GDN_TOTALPCS AS PCS, SUM(GDN.GDN_TOTALMTRS) AS MTRS, ISNULL(GDN.GDN_BALENOFROM, 0) AS NOOFBALES, '' AS PARTYPONO, 'GDN' AS FROMTYPE, ISNULL(TRANSLEDGERS.ACC_RD, 0) AS TRANSRATE, CAST(GDN.GDN_REMARKS AS VARCHAR(1000)) AS REMARKS, ISNULL(GDN.GDN_TRANSREFNO,'') AS TRANSREFNO ", "", " LEDGERS AS AGENTLEDGERS RIGHT OUTER JOIN GDN INNER JOIN LEDGERS ON GDN.GDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON GDN.GDN_CITYid = TOCITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON GDN.GDN_DISPATCHTOID = DELIVERYLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON GDN.GDN_transid = TRANSLEDGERS.Acc_id ON AGENTLEDGERS.Acc_id = GDN.GDN_AGENTID  ", "  and GDN.GDN_NO IN(" & TEMPCHALLANNO & ") and ISNULL(GDN.GDN_TRANSCHALLANDONE,0) = 0 AND GDN.GDN_YEARID = " & YearId & " GROUP BY GDN.GDN_NO, LEDGERS.Acc_cmpname, ISNULL(AGENTLEDGERS.Acc_cmpname, ''), ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(DELIVERYLEDGERS.Acc_cmpname, ''), ISNULL(TOCITYMASTER.city_name, ''), GDN.GDN_BALENOFROM, GDN.GDN_TOTALPCS, ISNULL(TRANSLEDGERS.ACC_RD, 0), CAST(GDN.GDN_REMARKS AS VARCHAR(1000)), ISNULL(GDN.GDN_TRANSREFNO,'')  ")
                    End If

                ElseIf DTTABLE.Rows(0).Item("FROMTYPE") = "OPENINGGDN" Then
                    If ClientName = "SNCM" Then
                        DT1 = OBJCMN.SEARCH("  LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '') AS DELIVERYNAME, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, OPENINGGDN.OPENINGGDN_NO AS GDNNO, SUM(OPENINGGDN_DESC.OPENINGGDN_PCS) AS PCS, SUM(OPENINGGDN_DESC.OPENINGGDN_MTRS) AS MTRS, ISNULL(OPENINGGDN.OPENINGGDN_BALENOFROM,0) AS NOOFBALES, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO,'') AS PARTYPONO, 'OPENINGGDN' AS FROMTYPE, ISNULL(TRANSLEDGERS.ACC_RD, 0) AS TRANSRATE, CAST(OPENINGGDN.OPENINGGDN_REMARKS AS VARCHAR(1000)) AS REMARKS, '' AS TRANSREFNO, isnull(OPENINGGDN_DESC.OPENINGGDN_BARCODE,'') as BARCODE , ISNULL(UNITMASTER.unit_abbr ,'') AS UNIT ", "", " LEDGERS AS TRANSLEDGERS RIGHT OUTER JOIN OPENINGGDN_DESC INNER JOIN OPENINGGDN ON OPENINGGDN_DESC.OPENINGGDN_NO = OPENINGGDN.OPENINGGDN_NO AND OPENINGGDN_DESC.OPENINGGDN_YEARID = OPENINGGDN.OPENINGGDN_YEARID INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN UNITMASTER ON OPENINGGDN_DESC.OPENINGGDN_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN COLORMASTER ON OPENINGGDN_DESC.OPENINGGDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = TOCITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON OPENINGGDN.OPENINGGDN_DISPATCHTOID = DELIVERYLEDGERS.Acc_id ON TRANSLEDGERS.Acc_id = OPENINGGDN.OPENINGGDN_transid LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON OPENINGGDN.OPENINGGDN_AGENTID = AGENTLEDGERS.Acc_id   ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & TEMPCHALLANNO & ") and ROUND(ISNULL(OPENINGGDN.OPENINGGDN_OUTMTRS,0),0) = 0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY OPENINGGDN.OPENINGGDN_NO,ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), LEDGERS.Acc_cmpname, ISNULL(AGENTLEDGERS.Acc_cmpname, ''), ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(DELIVERYLEDGERS.Acc_cmpname, ''), ISNULL(TOCITYMASTER.city_name, ''), OPENINGGDN.OPENINGGDN_BALENOFROM, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO,''), ISNULL(TRANSLEDGERS.ACC_RD, 0), CAST(OPENINGGDN.OPENINGGDN_REMARKS AS VARCHAR(1000)),ISNULL(OPENINGGDN_DESC.OPENINGGDN_BARCODE,''), ISNULL(UNITMASTER.unit_abbr ,'') ")
                    ElseIf ClientName = "AVIS" Or ClientName = "RADHA" Then
                        DT1 = OBJCMN.SEARCH("  LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '') AS DELIVERYNAME, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, OPENINGGDN.OPENINGGDN_NO AS GDNNO, SUM(OPENINGGDN_DESC.OPENINGGDN_PCS) AS PCS, SUM(OPENINGGDN_DESC.OPENINGGDN_MTRS) AS MTRS, ISNULL(OPENINGGDN.OPENINGGDN_BALENOFROM,0) AS NOOFBALES, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO,'') AS PARTYPONO, 'OPENINGGDN' AS FROMTYPE, ISNULL(TRANSLEDGERS.ACC_RD, 0) AS TRANSRATE, CAST(OPENINGGDN.OPENINGGDN_REMARKS AS VARCHAR(1000)) AS REMARKS, '' AS TRANSREFNO ", "", " LEDGERS AS AGENTLEDGERS RIGHT OUTER JOIN OPENINGGDN_DESC INNER JOIN OPENINGGDN ON OPENINGGDN_DESC.OPENINGGDN_NO = OPENINGGDN.OPENINGGDN_NO AND OPENINGGDN_DESC.OPENINGGDN_YEARID = OPENINGGDN.OPENINGGDN_YEARID INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON OPENINGGDN_DESC.OPENINGGDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON OPENINGGDN_DESC.OPENINGGDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGGDN_DESC.OPENINGGDN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGGDN_DESC.OPENINGGDN_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = TOCITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON OPENINGGDN.OPENINGGDN_DISPATCHTOID = DELIVERYLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGERS.Acc_id ON AGENTLEDGERS.Acc_id = OPENINGGDN.OPENINGGDN_AGENTID  ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & TEMPCHALLANNO & ") and ROUND(ISNULL(OPENINGGDN.OPENINGGDN_OUTMTRS,0),0) = 0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY OPENINGGDN.OPENINGGDN_NO,ISNULL(ITEMMASTER.item_name, ''), ISNULL(QUALITYMASTER.QUALITY_name, ''), ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(COLORMASTER.COLOR_name, ''), LEDGERS.Acc_cmpname, ISNULL(AGENTLEDGERS.Acc_cmpname, ''), ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(DELIVERYLEDGERS.Acc_cmpname, ''), ISNULL(TOCITYMASTER.city_name, ''), OPENINGGDN.OPENINGGDN_BALENOFROM, ISNULL(OPENINGGDN_DESC.OPENINGGDN_GRIDPARTYPONO,''), ISNULL(TRANSLEDGERS.ACC_RD, 0), CAST(OPENINGGDN.OPENINGGDN_REMARKS AS VARCHAR(1000)) ")
                    Else
                        DT1 = OBJCMN.SEARCH("  LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '')  AS DELIVERYNAME, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, '' AS ITEMNAME, '' AS QUALITY, '' AS DESIGNNO, '' AS COLOR, OPENINGGDN.OPENINGGDN_NO AS GDNNO, OPENINGGDN.OPENINGGDN_TOTALPCS AS PCS, SUM(OPENINGGDN.OPENINGGDN_TOTALMTRS) AS MTRS, ISNULL(OPENINGGDN.OPENINGGDN_BALENOFROM, 0) AS NOOFBALES, '' AS PARTYPONO, 'OPENINGGDN' AS FROMTYPE, ISNULL(TRANSLEDGERS.ACC_RD, 0) AS TRANSRATE, CAST(OPENINGGDN.OPENINGGDN_REMARKS AS VARCHAR(1000)) AS REMARKS, ISNULL(OPENINGGDN.OPENINGGDN_TRANSREFNO,'') AS TRANSREFNO ", "", " LEDGERS AS AGENTLEDGERS RIGHT OUTER JOIN OPENINGGDN INNER JOIN LEDGERS ON OPENINGGDN.OPENINGGDN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON OPENINGGDN.OPENINGGDN_CITYid = TOCITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON OPENINGGDN.OPENINGGDN_DISPATCHTOID = DELIVERYLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON OPENINGGDN.OPENINGGDN_transid = TRANSLEDGERS.Acc_id ON AGENTLEDGERS.Acc_id = OPENINGGDN.OPENINGGDN_AGENTID  ", "  and OPENINGGDN.OPENINGGDN_NO IN(" & TEMPCHALLANNO & ") and ISNULL(OPENINGGDN.OPENINGGDN_TRANSCHALLANDONE,0) = 0 AND OPENINGGDN.OPENINGGDN_YEARID = " & YearId & " GROUP BY OPENINGGDN.OPENINGGDN_NO, LEDGERS.Acc_cmpname, ISNULL(AGENTLEDGERS.Acc_cmpname, ''), ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(DELIVERYLEDGERS.Acc_cmpname, ''), ISNULL(TOCITYMASTER.city_name, ''), OPENINGGDN.OPENINGGDN_BALENOFROM, OPENINGGDN.OPENINGGDN_TOTALPCS, ISNULL(TRANSLEDGERS.ACC_RD, 0), CAST(OPENINGGDN.OPENINGGDN_REMARKS AS VARCHAR(1000)), ISNULL(OPENINGGDN.OPENINGGDN_TRANSREFNO,'') ")
                    End If

                ElseIf DTTABLE.Rows(0).Item("FROMTYPE") = "JOBOUT" Then
                    If ClientName = "SNCM" Then
                        DT1 = OBJCMN.SEARCH("  LEDGERS.Acc_cmpname AS NAME, '' AS AGENT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '')  AS DELIVERYNAME, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, '' AS ITEMNAME, '' AS QUALITY, '' AS DESIGNNO, '' AS COLOR, JOBOUT.JO_NO AS GDNNO, JOBOUT.JO_TOTALPCS AS PCS, SUM(JOBOUT.JO_TOTALMTRS) AS MTRS, ISNULL(JOBOUT.JO_BALENUMBER, 0) AS NOOFBALES, '' AS PARTYPONO, 'JOBOUT' AS FROMTYPE, ISNULL(TRANSLEDGERS.ACC_RD, 0) AS TRANSRATE, CAST(JOBOUT.JO_REMARKS AS VARCHAR(1000)) AS REMARKS, '' as TRANSREFNO , ISNULL(JOBOUT_DESC.JO_BARCODE,'') AS BARCODE , ISNULL(UNITMASTER.unit_abbr,'') AS UNIT ", "", "   JOBOUT INNER JOIN LEDGERS ON JOBOUT.JO_ledgerid = LEDGERS.Acc_id INNER JOIN JOBOUT_DESC ON JOBOUT.JO_no = JOBOUT_DESC.JO_NO AND JOBOUT.JO_yearid = JOBOUT_DESC.JO_YEARID LEFT OUTER JOIN  UNITMASTER ON JOBOUT_DESC.JO_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON JOBOUT.JO_TOCITYID = TOCITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON JOBOUT.JO_PACKINGID = DELIVERYLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON JOBOUT.JO_transledgerid = TRANSLEDGERS.Acc_id  ", "  and JOBOUT.JO_NO IN(" & TEMPCHALLANNO & ") and ISNULL(JOBOUT.JO_GPDONE,0) = 0 AND JOBOUT.JO_YEARID = " & YearId & " GROUP BY JOBOUT.JO_NO, LEDGERS.Acc_cmpname, ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(DELIVERYLEDGERS.Acc_cmpname, ''), ISNULL(TOCITYMASTER.city_name, ''), JOBOUT.JO_BALENUMBER, JOBOUT.JO_TOTALPCS, ISNULL(TRANSLEDGERS.ACC_RD, 0), CAST(JOBOUT.JO_REMARKS AS VARCHAR(1000)), ISNULL(JOBOUT_DESC.JO_BARCODE,''), ISNULL(UNITMASTER.unit_abbr,'') ")
                    Else
                        DT1 = OBJCMN.SEARCH("  LEDGERS.Acc_cmpname AS NAME, '' AS AGENT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '')  AS DELIVERYNAME, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, '' AS ITEMNAME, '' AS QUALITY, '' AS DESIGNNO, '' AS COLOR, JOBOUT.JO_NO AS GDNNO, JOBOUT.JO_TOTALPCS AS PCS, SUM(JOBOUT.JO_TOTALMTRS) AS MTRS, ISNULL(JOBOUT.JO_BALENUMBER, 0) AS NOOFBALES, '' AS PARTYPONO, 'JOBOUT' AS FROMTYPE, ISNULL(TRANSLEDGERS.ACC_RD, 0) AS TRANSRATE, CAST(JOBOUT.JO_REMARKS AS VARCHAR(1000)) AS REMARKS, '' as TRANSREFNO ", "", " JOBOUT INNER JOIN LEDGERS ON JOBOUT.JO_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON JOBOUT.JO_TOCITYID = TOCITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON JOBOUT.JO_PACKINGID = DELIVERYLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON JOBOUT.JO_transledgerid = TRANSLEDGERS.Acc_id  ", "  and JOBOUT.JO_NO IN(" & TEMPCHALLANNO & ") and ISNULL(JOBOUT.JO_GPDONE,0) = 0 AND JOBOUT.JO_YEARID = " & YearId & " GROUP BY JOBOUT.JO_NO, LEDGERS.Acc_cmpname, ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(DELIVERYLEDGERS.Acc_cmpname, ''), ISNULL(TOCITYMASTER.city_name, ''), JOBOUT.JO_BALENUMBER, JOBOUT.JO_TOTALPCS, ISNULL(TRANSLEDGERS.ACC_RD, 0), CAST(JOBOUT.JO_REMARKS AS VARCHAR(1000))")
                    End If
                ElseIf DTTABLE.Rows(0).Item("FROMTYPE") = "PURRETURN" Then
                    If ClientName = "SNCM" Then
                        DT1 = OBJCMN.SEARCH(" LEDGERS.Acc_cmpname AS NAME, '' AS AGENT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '')  AS DELIVERYNAME, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, '' AS ITEMNAME, '' AS QUALITY, '' AS DESIGNNO, '' AS COLOR, PURCHASERETURN.PR_NO AS GDNNO, PURCHASERETURN.PR_TOTALPCS AS PCS, SUM(PURCHASERETURN.PR_TOTALMTRS) AS MTRS, ISNULL(PURCHASERETURN.PR_NOOFBALES, 0) AS NOOFBALES, '' AS PARTYPONO, 'PURRETURN' AS FROMTYPE, ISNULL(TRANSLEDGERS.ACC_RD, 0) AS TRANSRATE, CAST(PURCHASERETURN.PR_REMARKS AS VARCHAR(1000)) AS REMARKS, '' as TRANSREFNO , ISNULL(PURCHASERETURN_DESC.PR_BARCODE,'') AS BARCODE , ISNULL(UNITMASTER.unit_abbr,'') AS UNIT ", "", "  PURCHASERETURN INNER JOIN LEDGERS ON PURCHASERETURN.PR_ledgerid = LEDGERS.Acc_id INNER JOIN PURCHASERETURN_DESC ON PURCHASERETURN.PR_no = PURCHASERETURN_DESC.PR_no AND PURCHASERETURN.PR_yearid = PURCHASERETURN_DESC.PR_yearid LEFT OUTER JOIN UNITMASTER ON PURCHASERETURN_DESC.PR_QTYUNITID = UNITMASTER.unit_id LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON PURCHASERETURN.PR_TOCITYID = TOCITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON PURCHASERETURN.PR_DELIVERYATID = DELIVERYLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON PURCHASERETURN.PR_transledgerid = TRANSLEDGERS.Acc_id  ", "  and PURCHASERETURN.PR_NO IN(" & TEMPCHALLANNO & ") and ISNULL(PURCHASERETURN.PR_GPDONE,0) = 0 AND PURCHASERETURN.PR_YEARID = " & YearId & "  GROUP BY PURCHASERETURN.PR_NO, LEDGERS.Acc_cmpname, ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(DELIVERYLEDGERS.Acc_cmpname, ''), ISNULL(TOCITYMASTER.city_name, ''), PURCHASERETURN.PR_NOOFBALES, PURCHASERETURN.PR_TOTALPCS, ISNULL(TRANSLEDGERS.ACC_RD, 0), CAST(PURCHASERETURN.PR_REMARKS AS VARCHAR(1000)), ISNULL(PURCHASERETURN_DESC.PR_BARCODE,'') , ISNULL(UNITMASTER.unit_abbr,'') ")
                    Else
                        DT1 = OBJCMN.SEARCH(" LEDGERS.Acc_cmpname AS NAME, '' AS AGENT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(DELIVERYLEDGERS.Acc_cmpname, '')  AS DELIVERYNAME, ISNULL(TOCITYMASTER.city_name, '') AS TOCITY, '' AS ITEMNAME, '' AS QUALITY, '' AS DESIGNNO, '' AS COLOR, PURCHASERETURN.PR_NO AS GDNNO, PURCHASERETURN.PR_TOTALPCS AS PCS, SUM(PURCHASERETURN.PR_TOTALMTRS) AS MTRS, ISNULL(PURCHASERETURN.PR_NOOFBALES, 0) AS NOOFBALES, '' AS PARTYPONO, 'PURRETURN' AS FROMTYPE, ISNULL(TRANSLEDGERS.ACC_RD, 0) AS TRANSRATE, CAST(PURCHASERETURN.PR_REMARKS AS VARCHAR(1000)) AS REMARKS, '' as TRANSREFNO  ", "", " PURCHASERETURN INNER JOIN LEDGERS ON PURCHASERETURN.PR_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER AS TOCITYMASTER ON PURCHASERETURN.PR_TOCITYID = TOCITYMASTER.city_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON PURCHASERETURN.PR_DELIVERYATID = DELIVERYLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON PURCHASERETURN.PR_transledgerid = TRANSLEDGERS.Acc_id  ", "  and PURCHASERETURN.PR_NO IN(" & TEMPCHALLANNO & ") and ISNULL(PURCHASERETURN.PR_GPDONE,0) = 0 AND PURCHASERETURN.PR_YEARID = " & YearId & "  GROUP BY PURCHASERETURN.PR_NO, LEDGERS.Acc_cmpname, ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(DELIVERYLEDGERS.Acc_cmpname, ''), ISNULL(TOCITYMASTER.city_name, ''), PURCHASERETURN.PR_NOOFBALES, PURCHASERETURN.PR_TOTALPCS, ISNULL(TRANSLEDGERS.ACC_RD, 0), CAST(PURCHASERETURN.PR_REMARKS AS VARCHAR(1000)) ")
                    End If
                End If

                If DT1.Rows.Count > 0 Then
                    If ClientName = "AVIS" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "RADHA" Then
                        If ClientName = "AVIS" Then CMBTRANS.Text = DT1.Rows(0).Item("TRANSNAME")
                        CMBTOCITY.Text = DT1.Rows(0).Item("TOCITY")
                        CMBDELIVERY.Text = DT1.Rows(0).Item("DELIVERYNAME")
                        txtremarks.Text = DT1.Rows(0).Item("REMARKS")
                    End If
                    For Each dr As DataRow In DT1.Rows
                        If ClientName = "SNCM" Then
                            GRIDGP.Rows.Add(0, 0, dr("NAME"), dr("TRANSNAME"), dr("TOCITY"), dr("ITEMNAME"), dr("QUALITY"), dr("DESIGNNO"), dr("COLOR"), dr("TRANSREFNO"), Val(dr("GDNNO")), Format(Val(dr("PCS")), "0"), Format(Val(dr("MTRS")), "0.00"), Val(dr("NOOFBALES")), Val(dr("TRANSRATE")), 0, Val(dr("GDNNO")), dr("PARTYPONO"), dr("FROMTYPE"), dr("BARCODE"), dr("UNIT"))
                        Else
                            GRIDGP.Rows.Add(0, 0, dr("NAME"), dr("TRANSNAME"), dr("TOCITY"), dr("ITEMNAME"), dr("QUALITY"), dr("DESIGNNO"), dr("COLOR"), dr("TRANSREFNO"), Val(dr("GDNNO")), Format(Val(dr("PCS")), "0"), Format(Val(dr("MTRS")), "0.00"), Val(dr("NOOFBALES")), Val(dr("TRANSRATE")), 0, Val(dr("GDNNO")), dr("PARTYPONO"), dr("FROMTYPE"), "", "")
                        End If
                    Next
                End If

                GRIDGP.FirstDisplayedScrollingRowIndex = GRIDGP.RowCount - 1
                getsrno(GRIDGP)
                If ClientName = "SNCM" Then CMDSELECTGDN.Enabled = False
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

    Private Sub cmbtrans_Enter(sender As Object, e As EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then FILLNAME(CMBTRANS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        If EDIT = True Then PRINTREPORT(TEMPENTRYNO)
    End Sub

    Private Sub cmbname_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS", CMBTRANS.Text, CMBAGENT.Text)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try

            LBLTOTALMTRS.Text = 0.0
            LBLTOTALPCS.Text = 0.0
            LBLTOTALAMT.Text = 0.0

            If GRIDGP.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDGP.Rows
                    If Val(row.Cells(GRATE.Index).EditedFormattedValue) > 0 And Val(row.Cells(GNOOFBALES.Index).EditedFormattedValue) > 0 Then row.Cells(GAMOUNT.Index).Value = Format(Val(row.Cells(GRATE.Index).EditedFormattedValue) * Val(row.Cells(GNOOFBALES.Index).EditedFormattedValue), "0.00")
                    LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(row.Cells(Gpcs.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(row.Cells(Gmtrs.Index).EditedFormattedValue), "0.00")
                    LBLTOTALAMT.Text = Format(Val(LBLTOTALAMT.Text) + Val(row.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
                Next
            End If
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
            For i = 0 To GRIDGP.Rows.Count - 1
                If Not GRIDGP.Rows(i).IsNewRow Then
                    cellValue = GRIDGP(GBALENO.Index, i).EditedFormattedValue.ToString()
                    If cellValue <> "" Then
                        If Not dic.ContainsKey(cellValue) Then
                            dic.Add(cellValue, 1)
                            LBLTOTALBALES.Text = Val(LBLTOTALBALES.Text) + Val(GRIDGP(GNOOFBALES.Index, i).EditedFormattedValue)
                        Else
                            dic(cellValue) += 1
                        End If
                    End If
                End If
            Next
            'LBLTOTALBALES.Text = Val(dic.Count)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(sender As Object, e As CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then NAMEVALIDATE(CMBTRANS, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(sender As Object, e As EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" And CMBDELIVERY.Text.Trim = "" Then CMBDELIVERY.Text = CMBNAME.Text.Trim
            If ClientName = "VINTAGEINDIA" Then
                Call CMBTRANS_Validated(CMBTRANS, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDELIVERY_Validating(sender As Object, e As CancelEventArgs) Handles CMBDELIVERY.Validating
        Try
            If CMBDELIVERY.Text.Trim <> "" Then NAMEVALIDATE(CMBDELIVERY, CMBCODE, e, Me, TXTADD, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry CREDITORS", "ACCOUNTS")
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

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SENDWHATSAPP(TEMPENTRYNO)
            DT = OBJCMN.Execute_Any_String("UPDATE SALEGATEPASS SET GP_SENDWHATSAPP = 1 WHERE GP_NO = " & TEMPENTRYNO & " AND GP_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(GPNO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJGDN As New GDNDESIGN
            OBJGDN.MdiParent = MDIMain
            OBJGDN.DIRECTPRINT = True
            OBJGDN.FRMSTRING = "GATEPASS"
            OBJGDN.DIRECTMAIL = True
            OBJGDN.vendorname = CMBNAME.Text.Trim
            OBJGDN.agentname = CMBAGENT.Text.Trim
            OBJGDN.FORMULA = "{SALEGATEPASS.GP_NO}=" & Val(GPNO) & " and {SALEGATEPASS.GP_yearid}=" & YearId
            OBJGDN.JONO = GPNO
            OBJGDN.NOOFCOPIES = 1
            OBJGDN.Show()
            OBJGDN.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBAGENT.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\GATEPASS_" & Val(GPNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add("GATEPASS_" & Val(GPNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            EDIT = False
            CMBNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub tstxtbillno_Validating(sender As Object, e As CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDGP.RowCount = 0
                TEMPENTRYNO = Val(tstxtbillno.Text)
                If TEMPENTRYNO > 0 Then
                    EDIT = True
                    SaleGatePass_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDGP_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDGP.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDGP.Rows.Count > 0 Then
                Dim TEMPCHNO As Integer = GRIDGP.CurrentRow.Cells(GBALENO.Index).Value
LINE1:
                For Each ROW As DataGridViewRow In GRIDGP.Rows
                    If ROW.Cells(GBALENO.Index).Value = TEMPCHNO Then
                        GRIDGP.Rows.RemoveAt(ROW.Index)
                        GoTo LINE1
                    End If
                Next
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleGatePass_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "AVIS" Or ClientName = "RADHA" Then
                CMBNAME.BackColor = Color.LemonChiffon
                CMBAGENT.BackColor = Color.LemonChiffon
                GNAME.Visible = False
                GTRANSPORT.Visible = False
                GTOCITY.Visible = False
            Else
                LBLTRANSPORT.Text = "Local Transport"
                GITEMNAME.Visible = False
                GDESIGN.Visible = False
                GSHADE.Visible = False
                GQUALITY.Visible = False
            End If
            If ClientName = "SNCM" Then
                TXTBARCODE.Visible = True
                Label2.Visible = True
                CMBTRANS.BackColor = Color.LemonChiffon
                TXTVEHICLENO.BackColor = Color.LemonChiffon
                CMBNAME.Visible = False
                CMBAGENT.Visible = False
                CMBDELIVERY.Visible = False
                CMBFROMCITY.Visible = False
                CMBTOCITY.Visible = False
                LBLNAME.Visible = False
                LBLAGENT.Visible = False
                LBLDELIVERY.Visible = False
                LBLFROMCITY.Visible = False
                LBLTOCITY.Visible = False
                PBIMAGE1.Visible = True
                CMDCOMPRESS.Visible = True
                CMDPHOTOREMOVE1.Visible = True
                CMDPHOTOUPLOAD1.Visible = True
                CMDPHOTOVIEW1.Visible = True
                TabControl2.Visible = True

            End If

            If ClientName = "DRDRAPES" Then GDESCRIPTION.ReadOnly = False

            If ClientName = "VINTAGEINDIA" Or ClientName = "MAHAVIRPOLYCOT" Then
                LBLTXTVEHICLENO.Visible = False
                TXTVEHICLENO.Visible = False

                CMBVEHICLENAME.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGP_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDGP.CellValidating
        Try
            Dim colNum As Integer = GRIDGP.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum
                Case GRATE.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)
                    If bValid Then
                        If GRIDGP.CurrentCell.Value = Nothing Then GRIDGP.CurrentCell.Value = "0.00"
                        GRIDGP.CurrentCell.Value = Convert.ToDecimal(GRIDGP.Item(colNum, e.RowIndex).Value)
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

    Private Sub TXTENTRYNO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTENTRYNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTENTRYNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTENTRYNO.Validating
        Try
            If Val(TXTENTRYNO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(SALEGATEPASS.GP_NO,0)  AS RECONO", "", " SALEGATEPASS ", "  AND SALEGATEPASS.GP_NO=" & Val(TXTENTRYNO.Text.Trim) & " AND SALEGATEPASS.GP_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Rec No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPHOTOUPLOAD1_Click(sender As Object, e As EventArgs) Handles CMDPHOTOUPLOAD1.Click

        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png"
        OpenFileDialog1.ShowDialog()
        TXTPHOTOIMGPATH1.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTPHOTOIMGPATH1.Text.Trim.Length <> 0 Then PBIMAGE1.ImageLocation = TXTPHOTOIMGPATH1.Text.Trim

    End Sub

    Private Sub CMDPHOTOREMOVE1_Click(sender As Object, e As EventArgs) Handles CMDPHOTOREMOVE1.Click
        PBIMAGE1.Image = Nothing
        TXTPHOTOIMGPATH1.Clear()
    End Sub

    Private Sub CMDPHOTOVIEW1_Click(sender As Object, e As EventArgs) Handles CMDPHOTOVIEW1.Click
        Try
            If TXTPHOTOIMGPATH1.Text.Trim <> "" Then
                If Path.GetExtension(TXTPHOTOIMGPATH1.Text.Trim) = ".pdf" Then
                    System.Diagnostics.Process.Start(TXTPHOTOIMGPATH1.Text.Trim)
                Else
                    Dim objVIEW As New ViewImage
                    objVIEW.pbsoftcopy.Image = PBIMAGE1.Image
                    objVIEW.ShowDialog()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCOMPRESS_Click(sender As Object, e As EventArgs) Handles CMDCOMPRESS.Click
        Try
            If PBIMAGE1.ImageLocation <> "" Then
                AUTOCOMPRESSIMG()
                TXTPHOTOIMGPATH1.Text = OpenFileDialog1.FileName
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub AUTOCOMPRESSIMG()
        Try
            Dim imgSource As System.Drawing.Image = PBIMAGE1.Image
            Dim imgOutput As System.Drawing.Image
            Dim intPercent As Integer = 50
            Dim intType As Integer = 0

            'resize the image by percent
            Dim intX, intY As Integer
            intX = Int(imgSource.Width / 100 * intPercent)
            intY = Int(imgSource.Height / 100 * intPercent)
            Dim bm As Drawing.Bitmap = New System.Drawing.Bitmap(intX, intY)
            Dim g As System.Drawing.Graphics = Drawing.Graphics.FromImage(bm)

            Select Case intType
                Case 0
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.Default
                Case 1
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.High
                Case 2
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBilinear
                Case 3
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
            End Select

            g.DrawImage(imgSource, 0, 0, intX, intY)
            imgOutput = bm
            PBIMAGE1.Image = imgOutput

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTDATE_GotFocus(sender As Object, e As EventArgs) Handles DTDATE.GotFocus
        DTDATE.SelectionStart = 0
    End Sub

    Private Sub CMBTRANS_Validated(sender As Object, e As EventArgs) Handles CMBTRANS.Validated
        Try
            If ClientName = "VINTAGEINDIA" Or ClientName = "MAHAVIRPOLYCOT" Then
                If CMBTRANS.Text.Trim <> "" Then
                    'GET VEHICLE NO FROM REMARKS FROM ACCOUNT MASTER
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(VEHICLEMASTER.VEHICLE_NAME,'')	AS VEHICLENAME, ISNULL(LEDGERS.Acc_cmpname,'') AS TRANSNAME ", "", "LEDGERS LEFT OUTER JOIN VEHICLEMASTER ON LEDGERS.Acc_yearid = VEHICLEMASTER.VEHICLE_YEARID AND LEDGERS.Acc_id = VEHICLEMASTER.VEHICLE_TRANSID", " AND LEDGERS.Acc_cmpname  ='" & CMBTRANS.Text.Trim & "' AND LEDGERS.Acc_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        CMBVEHICLENAME.Text = DT.Rows(0).Item("VEHICLENAME")
                    End If
                End If
            Else

                If CMBTRANS.Text.Trim <> "" Then
                    'GET VEHICLE NO FROM REMARKS FROM ACCOUNT MASTER
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(Acc_remarks, '') AS REMARKS", "", "LEDGERS", " AND LEDGERS.Acc_cmpname  ='" & CMBTRANS.Text.Trim & "' AND LEDGERS.Acc_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TXTVEHICLENO.Text = DT.Rows(0).Item("REMARKS")
                    End If
                End If


            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 Then
                Dim MATCHFOUND As Boolean = False
                For Each ROW As DataGridViewRow In GRIDGP.Rows
                    If ROW.Cells(GBARCODE.Index).Value.ToString = TXTBARCODE.Text.Trim Then
                        ' GRIDGP.Rows(GRIDGP.RowCount - 1).DefaultCellStyle.BackColor = Color.Green
                        ROW.Cells(GVERIFIED.Index).Value = 1
                    End If
                Next
            End If
            TXTBARCODE.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBVEHICLENAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBVEHICLENAME.Validating
        Try
            If CMBVEHICLENAME.Text.Trim <> "" Then VEHICLEVALIDATE(CMBVEHICLENAME, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class




