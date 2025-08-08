
Imports System.ComponentModel
Imports BL
Imports System.IO

Public Class GreyIssueProcess

    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Public EDIT As Boolean          'used for editing
    Public TEMPGRNNO As Integer     'used for poation no while editing
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim PARTYCHALLANNO As String
    Dim ALLOWMANUALGRNNO As Boolean = False

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub clear()

        tstxtbillno.Clear()
        EP.Clear()

        If ALLOWMANUALGRNNO = True Then
            TXTGREYISSNO.ReadOnly = False
            TXTGREYISSNO.BackColor = Color.LemonChiffon
        Else
            TXTGREYISSNO.ReadOnly = True
            TXTGREYISSNO.BackColor = Color.Linen
        End If

        GREYISSDATE.Text = Now.Date
        CMBNAME.Enabled = True
        CMBNAME.Text = ""
        TXTPURNAME.Clear()
        CHALLANDATE.Text = Now.Date
        txtchallan.Clear()

        cmbtrans.Text = ""
        txtlrno.Clear()
        lrdate.Value = Now.Date

        txtremarks.Clear()

        lbllocked.Visible = False
        PBlock.Visible = False
        LBLWHATSAPP.Visible = False
        getmaxno()
        CMBAGENT.Text = ""
        TXTCRDAYS.Clear()
        TXTREFLOTNO.Clear()

        GRIDISSUE.RowCount = 0

        CMDSELECTGREY.Enabled = True
        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        getmaxno()

        LBLTOTALMTRS.Text = 0
        LBLTOTALBALES.Text = 0
        LBLTOTALQTY.Text = 0
        LBLTOTALAMT.Text = 0

    End Sub

    Sub TOTAL()
        Try
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALQTY.Text = 0.0
            LBLTOTALAMT.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDISSUE.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    LBLTOTALQTY.Text = Format(Val(LBLTOTALQTY.Text) + Val(ROW.Cells(gQty.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    If ROW.Cells(GRATE.Index).EditedFormattedValue > 0 Then ROW.Cells(GAMT.Index).Value = Format(Val(ROW.Cells(GMTRS.Index).EditedFormattedValue) * Val(ROW.Cells(GRATE.Index).EditedFormattedValue), "0.00")
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
            For i = 0 To GRIDISSUE.Rows.Count - 1
                If Not GRIDISSUE.Rows(i).IsNewRow Then
                    cellValue = GRIDISSUE(GBALENO.Index, i).EditedFormattedValue.ToString()
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

    Private Sub GREYISSDATE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GREYISSDATE.Enter
        GREYISSDATE.SelectAll()
    End Sub

    Private Sub GREYISSDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GREYISSDATE.Validating
        Try
            If GREYISSDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(GREYISSDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    'SAME DATE FOR CHALLANDATE / LRDATE 
                    If ClientName = "DAKSH" Or ClientName = "SHALIBHADRA" Then
                        CHALLANDATE.Text = GREYISSDATE.Text
                        lrdate.Value = Convert.ToDateTime(GREYISSDATE.Text).Date
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getmaxno()
        Dim DTTABLE As DataTable = getmax(" isnull(max(GREYISS_no),0) + 1 ", "GREYISSUEPROCESS", " AND GREYISS_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTGREYISSNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True
            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, " Please Fill Company Name ")
                bln = False
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If lbllocked.Visible = True And UserName <> "Admin" Then
                EP.SetError(lbllocked, "GRN Done, Delete GRN First")
                bln = False
            End If

            If GRIDISSUE.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If

            If ClientName <> "CC" And ClientName <> "GELATO" And ClientName <> "MOMAI" And ClientName <> "AXIS" And ClientName <> "KREEVE" Then
                For Each row As DataGridViewRow In GRIDISSUE.Rows
                    DT = OBJCMN.search("MATERIAL_NAME", "", "  ITEMMASTER INNER JOIN MATERIALTYPEMASTER ON ITEMMASTER.item_materialtypeid = MATERIALTYPEMASTER.material_id AND ITEMMASTER.item_cmpid = MATERIALTYPEMASTER.material_cmpid AND ITEMMASTER.item_locationid = MATERIALTYPEMASTER.material_locationid AND ITEMMASTER.item_yearid = MATERIALTYPEMASTER.material_yearid ", " AND ITEMMASTER.ITEM_NAME = '" & row.Cells(gitemname.Index).Value & "' AND ITEM_CMPID = " & CmpId & " AND ITEM_LOCATIONID = " & Locationid & " AND ITEM_YEARID = " & YearId)
                    If Val(row.Cells(GMTRS.Index).Value) = 0 And (DT.Rows(0).Item(0) = "Raw Material" Or DT.Rows(0).Item(0) = "Semi Finished Goods" Or DT.Rows(0).Item(0) = "Finished Goods") Then
                        EP.SetError(TabControl1, "MTRS Cannot be kept Blank")
                        bln = False
                    End If

                    If ITEMCOSTCENTRE = True And Val(row.Cells(GRATE.Index).Value) = 0 Then
                        EP.SetError(CMBNAME, "Rate Cannot be 0")
                        bln = False
                    End If
                Next
            End If

            If ALLOWMANUALGRNNO = True Then
                If Val(TXTGREYISSNO.Text.Trim) <> 0 And EDIT = False Then
                    Dim OBJCMNn As New ClsCommon
                    Dim dttable As DataTable = OBJCMNn.search(" ISNULL(GREYISSUEPROCESS.GREYISS_NO,0)  AS GRNNO", "", " GREYISSUEPROCESS ", "  AND GREYISSUEPROCESS.GREYISS_NO=" & Val(TXTGREYISSNO.Text.Trim) & " AND GREYISSUEPROCESS.GREYISS_YEARID = " & YearId)
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
                    DT = objclscommon.search(" GREYISSUEPROCESS.GREYISS_challanno, LEDGERS.ACC_cmpname", "", " GREYISSUEPROCESS inner join LEDGERS on LEDGERS.ACC_id = GREYISSUEPROCESS.GREYISS_ledgerid ", " and GREYISSUEPROCESS.GREYISS_challanno = '" & txtchallan.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & CMBNAME.Text.Trim & "' AND GREYISS_YEARID =" & YearId)
                    If DT.Rows.Count > 0 Then
                        EP.SetError(txtchallan, "Challan No. Already Exists")
                        bln = False
                    End If
                End If
            End If


            If GREYISSDATE.Text = "__/__/____" Then
                EP.SetError(GREYISSDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(GREYISSDATE.Text) Then
                    EP.SetError(GREYISSDATE, "Date not in Accounting Year")
                    bln = False
                End If
                If Convert.ToDateTime(GREYISSDATE.Text).Date < GREYRPBLOCKDATE.Date Then
                    EP.SetError(GREYISSDATE, "Date is Blocked, Please make entries after " & Format(GREYRPBLOCKDATE.Date, "dd/MM/yyyy"))
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

            If TXTGREYISSNO.ReadOnly = False Then
                alParaval.Add(Val(TXTGREYISSNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(Format(Convert.ToDateTime(GREYISSDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(TXTPURNAME.Text.Trim)
            alParaval.Add(txtchallan.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(cmbtrans.Text.Trim)
            alParaval.Add(txtlrno.Text.Trim)
            alParaval.Add(lrdate.Value)

            alParaval.Add(Val(LBLTOTALBALES.Text))
            alParaval.Add(Val(LBLTOTALQTY.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
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
            Dim MTRS As String = ""
            Dim RATE As String = ""
            Dim AMOUNT As String = ""
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDISSUE.Rows
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
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        RATE = Val(row.Cells(GRATE.Index).Value)
                        AMOUNT = Val(row.Cells(GAMT.Index).Value)
                        OUTPCS = Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = Val(row.Cells(GOUTMTRS.Index).Value)
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString

                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(gsrno.Index).Value)
                        ITEMNAME = ITEMNAME & "|" & row.Cells(gitemname.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString Else BALENO = BALENO & "|" & ""
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        QTY = QTY & "|" & Val(row.Cells(gQty.Index).Value)
                        qtyunit = qtyunit & "|" & row.Cells(gqtyunit.Index).Value
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        RATE = RATE & "|" & Val(row.Cells(GRATE.Index).Value)
                        AMOUNT = AMOUNT & "|" & Val(row.Cells(GAMT.Index).Value)
                        OUTPCS = OUTPCS & "|" & Val(row.Cells(GOUTPCS.Index).Value)
                        OUTMTRS = OUTMTRS & "|" & Val(row.Cells(GOUTMTRS.Index).Value)
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString

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
            alParaval.Add(MTRS)
            alParaval.Add(RATE)
            alParaval.Add(AMOUNT)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)

            alParaval.Add(CMBAGENT.Text.Trim)
            alParaval.Add(TXTCRDAYS.Text.Trim)
            alParaval.Add(TXTREFLOTNO.Text.Trim)


            Dim OBJGREYISS As New ClsGreyIssueProcess()
            OBJGREYISS.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = OBJGREYISS.SAVE()
                TXTGREYISSNO.Text = Val(DTTABLE.Rows(0).Item(0))
                MsgBox("Details Added")

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPGRNNO)
                Dim IntResult As Integer = OBJGREYISS.UPDATE()
                MsgBox("Details Updated")

            End If


            If ClientName = "BRILLANTO" Or ClientName = "DAKSH" Then PRINTREPORT(Val(TXTGREYISSNO.Text.Trim))

            EDIT = False

            clear()
            If ClientName = "AMAN" Then txtchallan.Focus() Else CMBNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub GREYISS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            GRIDISSUE.Focus()
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

    Private Sub GREYISS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            fillcmb()
            clear()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJGREYISS As New ClsGreyIssueProcess()
                Dim dttable As DataTable = OBJGREYISS.SELECTGREY(TEMPGRNNO, YearId)

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTGREYISSNO.Text = TEMPGRNNO
                        TXTGREYISSNO.ReadOnly = True
                        GREYISSDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                        TXTPURNAME.Text = dr("PURNAME")

                        txtchallan.Text = Convert.ToString(dr("CHALLANNO").ToString)
                        PARTYCHALLANNO = txtchallan.Text.Trim
                        CHALLANDATE.Text = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")

                        cmbtrans.Text = dr("TRANSNAME").ToString
                        txtlrno.Text = dr("LRNO").ToString
                        lrdate.Text = Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy")

                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        CMBAGENT.Text = Convert.ToString(dr("AGENT").ToString)
                        TXTCRDAYS.Text = dr("CRDAYS").ToString
                        TXTREFLOTNO.Text = dr("REFLOTNO")

                        'Item Grid
                        GRIDISSUE.Rows.Add(dr("GRIDSRNO").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGNNO").ToString, dr("COLOR"), Format(dr("qty"), "0.00"), dr("QTYUNIT").ToString, Format(dr("MTRS"), "0.00"), Format(dr("RATE"), "0.00"), Format(dr("AMOUNT"), "0.00"), Val(dr("OUTPCS")), Val(dr("OUTMTRS")), Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("FROMTYPE"))

                        If Val(dr("OUTMTRS")) > 0 Then
                            GRIDISSUE.Rows(GRIDISSUE.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                    Next

                    total()
                Else
                    EDIT = False
                    clear()
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
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
            If cmbtrans.Text.Trim = "" Then FILLNAME(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT'")
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

            Dim OBJGREYISSDTLS As New GreyIssueProcessDetails
            OBJGREYISSDTLS.MdiParent = MDIMain
            OBJGREYISSDTLS.Show()
            OBJGREYISSDTLS.BringToFront()
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

    Private Sub txtchallan_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtchallan.Validating
        Try
            If txtchallan.Text.Trim.Length > 0 Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(txtchallan.Text.Trim)) Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    Dim dt As DataTable = objclscommon.search(" GREYISSUEPROCESS.GREYISS_challanno, LEDGERS.ACC_cmpname", "", " GREYISSUEPROCESS inner join LEDGERS on LEDGERS.ACC_id = GREYISSUEPROCESS.GREYISS_ledgerid ", " and GREYISSUEPROCESS.GREYISS_challanno = '" & txtchallan.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & CMBNAME.Text.Trim & "' AND GREYISS_YEARID =" & YearId)
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

    Private Sub CMDSELECTGREY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTGREY.Click
        Try

            If CMBNAME.Text.Trim = "" Then
                MsgBox("Select Party Name", MsgBoxStyle.Critical)
                CMBNAME.Focus()
                Exit Sub
            End If

            Dim DTPO As New DataTable
            Dim OBJSELECTGREY As New SelectGreyTransport
            OBJSELECTGREY.TRANSNAME = cmbtrans.Text.Trim
            OBJSELECTGREY.ShowDialog()
            DTPO = OBJSELECTGREY.DT

            If DTPO.Rows.Count > 0 Then

                TXTPURNAME.Text = DTPO.Rows(0).Item("NAME")
                cmbtrans.Text = DTPO.Rows(0).Item("TRANSPORT")
                txtlrno.Text = DTPO.Rows(0).Item("LRNO")
                If ClientName = "AVIS" Then txtchallan.Text = DTPO.Rows(0).Item("LRNO")
                lrdate.Value = Convert.ToDateTime(DTPO.Rows(0).Item("LRDATE")).Date
                txtremarks.Text = DTPO.Rows(0).Item("REMARKS")
                CMBAGENT.Text = DTPO.Rows(0).Item("AGENT")
                TXTCRDAYS.Text = DTPO.Rows(0).Item("CRDAYS")

                'BEFORE ADDING THE ROW IN ORDERDER GRID CHECK WHETHER SAME ORDERNO AN SRNO IS PRESENT IN GRID OR NOT
                For Each DTROW As DataRow In DTPO.Rows
                    For Each ROW As DataGridViewRow In GRIDISSUE.Rows
                        If Val(ROW.Cells(GFROMNO.Index).Value) = Val(DTROW("GREYRECNO")) And Val(ROW.Cells(GFROMSRNO.Index).Value) = Val(DTROW("GRIDSRNO")) And ROW.Cells(GFROMTYPE.Index).Value = DTROW("TYPE") Then GoTo NEXTLINE
                    Next


                    GRIDISSUE.Rows.Add(0, DTROW("ITEMNAME"), "", DTROW("BALENO"), DTROW("DESIGNNO"), DTROW("COLOR"), Val(DTROW("PCS")), DTROW("UNIT"), Val(DTROW("MTRS")), Val(DTROW("RATE")), 0, 0, 0, DTROW("GREYRECNO"), DTROW("GRIDSRNO"), DTROW("TYPE"))

NEXTLINE:
                Next
                getsrno(GRIDISSUE)

            End If

            CMDSELECTGREY.Enabled = True
            total()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDISSUE.RowCount = 0
                TEMPGRNNO = Val(tstxtbillno.Text)
                If TEMPGRNNO > 0 Then
                    EDIT = True
                    GREYISS_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDISSUE.RowCount = 0
LINE1:
            TEMPGRNNO = Val(TXTGREYISSNO.Text) - 1
            If TEMPGRNNO > 0 Then
                EDIT = True
                GREYISS_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDISSUE.RowCount = 0 And TEMPGRNNO > 1 Then
                TXTGREYISSNO.Text = TEMPGRNNO
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
            GRIDISSUE.RowCount = 0
LINE1:
            TEMPGRNNO = Val(TXTGREYISSNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTGREYISSNO.Text.Trim
            clear()
            If Val(TXTGREYISSNO.Text) - 1 >= TEMPGRNNO Then
                EDIT = True
                GREYISS_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDISSUE.RowCount = 0 And TEMPGRNNO < MAXNO Then
                TXTGREYISSNO.Text = TEMPGRNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
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

                If MsgBox("Delete GREYISSUEPROCESS?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub

                Dim alParaval As New ArrayList
                alParaval.Add(Val(TXTGREYISSNO.Text.Trim))
                alParaval.Add(Userid)
                alParaval.Add(CmpId)
                alParaval.Add(YearId)

                Dim OBJGREY As New ClsGreyIssueProcess()
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

    Private Sub gridGREYISS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDISSUE.KeyDown

        Try
            If e.KeyCode = Keys.Delete And GRIDISSUE.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row Is In Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                If Val(GRIDISSUE.CurrentRow.Cells(GOUTMTRS.Index).Value) > 0 Then
                    MessageBox.Show("Row Locked, You Cannot Delete This Row")
                    Exit Sub
                End If


                'end of block
                GRIDISSUE.Rows.RemoveAt(GRIDISSUE.CurrentRow.Index)
                getsrno(GRIDISSUE)
                total()

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS", cmbtrans.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                If ClientName = "AMAN" Or ClientName = "AARYA" Then OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'" Else OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
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
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
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

    Sub PRINTREPORT(ByVal GREYISSNO As Integer)
        Try
            'If MsgBox("Wish to Print GREYISSUEPROCESS...?", MsgBoxStyle.YesNo) = vbYes Then
            '    Dim OBJGDN As New GRNDesign
            '    OBJGDN.MdiParent = MDIMain
            '    OBJGDN.FRMSTRING = "GREYISSUEPROCESS"
            '    OBJGDN.WHERECLAUSE = "{GREYISSUEPROCESS.GREYISS_no}=" & Val(GREYISSNO) & " and {GREYISSUEPROCESS.GREYISS_yearid}=" & YearId
            '    OBJGDN.Show()
            'End If


            If MsgBox("Wish to Print Mill Letter ?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJGDN As New GRNDesign
                OBJGDN.MdiParent = MDIMain
                OBJGDN.FRMSTRING = "LETTER"
                OBJGDN.WHERECLAUSE = "{GREYISSUEPROCESS.GREYISS_NO}=" & Val(GREYISSNO) & " and {GREYISSUEPROCESS.GREYISS_YEARID}=" & YearId
                If (ClientName = "SNCM") AndAlso MsgBox("Wish to Print Party Name ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJGDN.PRINTTRANS = True
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

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        cmdok_Click(sender, e)
    End Sub

    Private Sub txtgrnno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTGREYISSNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub txtgrnno_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTGREYISSNO.Validating
        Try
            If Val(TXTGREYISSNO.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(GREYISSUEPROCESS.GREYISS_NO,0)  AS GRNNO", "", " GREYISSUEPROCESS ", "  AND GREYISSUEPROCESS.GREYISS_NO=" & Val(TXTGREYISSNO.Text.Trim) & " AND GREYISSUEPROCESS.GREYISS_YEARID = " & YearId)
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
            DT = OBJCMN.Execute_Any_String("UPDATE GREYISSUEPROCESS SET GREYISS_SENDWHATSAPP = 1 WHERE GREYISS_NO = " & TEMPGRNNO & " AND GREYISS_YEARID = " & YearId, "", "")
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
            OBJGRN.FRMSTRING = "GREYISSUEPROCESS"
            OBJGRN.DIRECTMAIL = False
            OBJGRN.DIRECTWHATSAPP = True
            OBJGRN.PRINTSETTING = PRINTDIALOG
            OBJGRN.WHERECLAUSE = "{GREYISSUEPROCESS.GREYISS_no}=" & Val(GRNNO) & " and {GREYISSUEPROCESS.GREYISS_yearid}=" & YearId
            OBJGRN.GRNNO = Val(GRNNO)
            OBJGRN.NOOFCOPIES = 1
            OBJGRN.Show()
            OBJGRN.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\GREYISS_" & Val(GRNNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add("GREYISS_" & Val(GRNNO) & ".pdf")
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
                GREYISSDATE.Text = CHALLANDATE.Text
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDISSUE_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDISSUE.CellValidating
        Try
            Dim colNum As Integer = GRIDISSUE.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return
            Select Case colNum

                Case GRATE.Index, gQty.Index, GMTRS.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDISSUE.CurrentCell.Value = Nothing Then GRIDISSUE.CurrentCell.Value = "0.00"
                        GRIDISSUE.CurrentCell.Value = Convert.ToDecimal(GRIDISSUE.Item(colNum, e.RowIndex).Value)
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

    Private Sub GREYISSDATE_GotFocus(sender As Object, e As EventArgs) Handles GREYISSDATE.GotFocus
        GREYISSDATE.SelectionStart = 0
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

    Private Sub GreyIssueProcess_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "SNCM" Then
                Me.Text = "Grey Rec in Godown"
                LBLGRN.Text = "Grey Rec in Godown"
                LBLREFLOTNO.Visible = True
                TXTREFLOTNO.Visible = True
                GRIDISSUE.ReadOnly = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class