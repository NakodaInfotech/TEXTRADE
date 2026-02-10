
Imports BL
Imports System.ComponentModel
Public Class YarnInterGodownTransfer
    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Public TEMPGODOWNNO As Integer          'used for editing
    Public EDIT As Boolean          'used for editing
    Dim TEMPROW As Integer
    Dim TEMPUPLOADROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer
    Dim TEMPMTRS As Double = 0.0


    Dim PARTYCHALLANNO As String
    Dim ALLOWMANUALJONO As Boolean = False



    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
    Sub clear()

        EP.Clear()
        TXTGODOWNNO.Clear()

        If ALLOWMANUALJONO = True Then
            TXTGODOWNNO.ReadOnly = False
            TXTGODOWNNO.BackColor = Color.LemonChiffon
        Else
            TXTGODOWNNO.ReadOnly = True
            TXTGODOWNNO.BackColor = Color.Linen
        End If
        'CMBFROMGODOWN.Text = ""
        'CMBTOGODOWN.Text = ""
        TXTFROM.Clear()
        TXTTO.Clear()
        CMBTRANSPORTNAME.Text = ""
        TXTADD.Clear()
        TXTDATE.Text = Now.Date
        tstxtbillno.Clear()
        txtremarks.Clear()
        CMDSELECTSTOCK.Enabled = True
        lbllocked.Visible = False
        PBlock.Visible = False
        LBLTOTALMTRS.Text = 0.0
        LBLTOTALPCS.Text = 0.0
        TXTREFRENCE.Clear()
        TXTISSUEBY.Clear()
        TXTBARCODE.Clear()
        GRIDJO.RowCount = 0
        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False
        getmaxno()
        txtsrno.Clear()
        'CMBPIECETYPE.Text = "FRESH"
        CMBYARNQUALITY.Text = ""
        CMBMILL.Text = ""
        TXTJOBBERLOTNO.Clear()
        TXTGRIDLOTNO.Clear()
        TXTPSHADE.Clear()
        CMBDESIGN.Text = ""
        cmbcolor.Text = ""
        txtqty.Clear()
        TXTWT.Clear()
        TXTCONES.Clear()
        TXTLRNO.Clear()
        DTLRDATE.Value = Now.Date
        LIFTINGDATE.Text = Now.Date
    End Sub


    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        'CMBFROMGODOWN.Focus()
    End Sub
    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(TRANSFER_no),0) + 1 ", " INTERGODOWNTRANSFER ", " AND TRANSFER_cmpid=" & CmpId & " and  TRANSFER_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTGODOWNNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub
    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If CMBFROMGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBFROMGODOWN, " Please Fill Godown")
                bln = False
            End If

            If CMBTOGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBTOGODOWN, " Please Fill Godown")
                bln = False
            End If

            If CMBFROMGODOWN.Text.Trim = CMBTOGODOWN.Text.Trim Then
                EP.SetError(CMBFROMGODOWN, " From && To Godown cannot be same")
                bln = False
            End If

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, " Inward Done, Delete Inward First")
                bln = False
            End If

            If GRIDJO.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If

            If TXTDATE.Text = "__/__/____" Then
                EP.SetError(TXTDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(TXTDATE.Text) Then
                    EP.SetError(TXTDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            If Val(TXTGODOWNNO.Text.Trim) = 0 Then
                EP.SetError(TXTGODOWNNO, "Enter  Godown No")
                bln = False
            End If

            'If ALLOWMANUALJONO = True Then
            '    If TXTGODOWNNO.Text <> "" And CMBNAME.Text.Trim <> "" And EDIT = False Then
            '        Dim OBJCMN As New ClsCommon
            '        Dim dttable As DataTable = OBJCMN.search(" ISNULL(JOBOUT.JO_NO,0)  AS JONO", "", " JOBOUT ", "  AND JOBOUT.JO_NO=" & TXTGODOWNNO.Text.Trim & " AND JOBOUT.JO_CMPID = " & CmpId & " AND JOBOUT.JO_LOCATIONID = " & Locationid & " AND JOBOUT.JO_YEARID = " & YearId)
            '        If dttable.Rows.Count > 0 Then
            '            EP.SetError(TXTGODOWNNO, "Job Out No Already Exist")
            '            bln = False
            '        End If
            '    End If
            'End If





            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            If TXTGODOWNNO.ReadOnly = False Then
                alParaval.Add(Val(TXTGODOWNNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(Format(Convert.ToDateTime(TXTDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBFROMGODOWN.Text.Trim)
            alParaval.Add(CMBTOGODOWN.Text.Trim)
            alParaval.Add(CMBTRANSPORTNAME.Text.Trim)
            alParaval.Add(TXTREFRENCE.Text.Trim)
            alParaval.Add(TXTISSUEBY.Text.Trim)

            alParaval.Add(Val(LBLTOTALPCS.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)




            Dim gridsrno As String = ""
            Dim PIECETYPE As String = ""
            Dim YARNQUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim DESIGN As String = ""
            Dim PARTYLOTNO As String = ""
            Dim PARTYCOLOR As String = ""
            Dim COLOR As String = ""
            Dim LOTNO As String = ""
            Dim BAGS As String = ""
            Dim WEIGHT As String = ""
            Dim CONES As String = ""
            Dim LRNO As String = ""
            Dim LRDATE As String = ""
            Dim LIFTINGDATE As String = ""
            Dim BARCODE As String = "" 'BARCODE ADDED
            Dim OUTPCS As String = ""
            Dim OUTMTRS As String = ""
            Dim PONO As String = ""
            Dim GRIDGSRNO As String = ""
            Dim FROMTYPE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDJO.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(GSRNO.Index).Value.ToString
                        YARNQUALITY = row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        PARTYLOTNO = row.Cells(GJOBBERLOTNO.Index).Value.ToString
                        PARTYCOLOR = row.Cells(GPCOLOR.Index).Value.ToString
                        COLOR = row.Cells(GCOLOR.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        BAGS = row.Cells(GQTY.Index).Value.ToString
                        WEIGHT = row.Cells(GWT.Index).Value.ToString
                        CONES = row.Cells(GCONES.Index).Value.ToString
                        LRNO = row.Cells(GLRNO.Index).Value
                        LRDATE = Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        LIFTINGDATE = row.Cells(GLIFTINGDATE.Index).Value
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        OUTPCS = row.Cells(GOUTPCS.Index).Value.ToString
                        OUTMTRS = row.Cells(GOUTMTRS.Index).Value.ToString
                        PONO = row.Cells(GPONO.Index).Value.ToString
                        GRIDGSRNO = row.Cells(GGRIDSRNO.Index).Value.ToString
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString

                    Else
                        gridsrno = gridsrno & "|" & row.Cells(GSRNO.Index).Value.ToString
                        YARNQUALITY = YARNQUALITY & "|" & row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        PARTYLOTNO = PARTYLOTNO & "|" & row.Cells(GJOBBERLOTNO.Index).Value.ToString
                        PARTYCOLOR = PARTYCOLOR & "|" & row.Cells(GPCOLOR.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GCOLOR.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        BAGS = BAGS & "|" & row.Cells(GQTY.Index).Value
                        WEIGHT = WEIGHT & "|" & row.Cells(GWT.Index).Value
                        CONES = CONES & "|" & row.Cells(GCONES.Index).Value
                        LRNO = LRNO & "|" & row.Cells(GLRNO.Index).Value
                        LRDATE = LRDATE & "|" & Format(Convert.ToDateTime(row.Cells(GLRDATE.Index).Value).Date, "MM/dd/yyyy")
                        LIFTINGDATE = LIFTINGDATE & "|" & (row.Cells(GLIFTINGDATE.Index).Value)
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        OUTPCS = OUTPCS & "|" & row.Cells(GOUTPCS.Index).Value.ToString
                        OUTMTRS = OUTMTRS & "|" & row.Cells(GOUTMTRS.Index).Value.ToString
                        PONO = PONO & "|" & row.Cells(GPONO.Index).Value.ToString
                        GRIDGSRNO = GRIDGSRNO & "|" & row.Cells(GGRIDSRNO.Index).Value.ToString
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(PIECETYPE)
            alParaval.Add(YARNQUALITY)
            alParaval.Add(MILLNAME)
            alParaval.Add(DESIGN)
            alParaval.Add(PARTYLOTNO)
            alParaval.Add(PARTYCOLOR)
            alParaval.Add(COLOR)
            alParaval.Add(LOTNO)
            alParaval.Add(BAGS)
            alParaval.Add(WEIGHT)
            alParaval.Add(CONES)
            alParaval.Add(LRNO)
            alParaval.Add(LRDATE)
            alParaval.Add(LIFTINGDATE)
            alParaval.Add(BARCODE)
            alParaval.Add(OUTPCS)
            alParaval.Add(OUTMTRS)
            alParaval.Add(PONO)
            alParaval.Add(GRIDGSRNO)
            alParaval.Add(FROMTYPE)



            Dim objCUTTING As New ClsYarnInterGodownTransfer()
            objCUTTING.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = objCUTTING.SAVE()
                MsgBox("Details Added")

                'If ClientName = "SVS" Then
                '    If MsgBox("Wish to Stock Reco Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '        RECOSAVE()
                '    End If
                'End If

                TXTGODOWNNO.Text = DTTABLE.Rows(0).Item(0)
                If ClientName <> "MJFABRIC" Then PRINTREPORT(DTTABLE.Rows(0).Item(0))

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                alParaval.Add(TEMPGODOWNNO)
                IntResult = objCUTTING.UPDATE()
                MsgBox("Details Updated")
                If ClientName <> "MJFABRIC" Then PRINTREPORT(TEMPGODOWNNO)
                EDIT = False
            End If
            clear()
            TXTDATE.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub



    Private Sub YarnInterGodownTransfer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
        ElseIf e.KeyCode = Keys.P And e.Alt = True Then
            Call PrintToolStripButton_Click(sender, e)
        End If
    End Sub

    Private Sub YarnInterGodownTransfer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GDN'")
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

                Dim objJO As New ClsInterGodownTransfer()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPGODOWNNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)
                objJO.alParaval = ALPARAVAL
                Dim dttable As DataTable = objJO.SELECTGODOWN()

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTGODOWNNO.Text = TEMPGODOWNNO
                        TXTGODOWNNO.ReadOnly = True
                        TXTDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBFROMGODOWN.Text = Convert.ToString(dr("FROMGODOWN").ToString)
                        CMBTOGODOWN.Text = Convert.ToString(dr("TOGODOWN").ToString)
                        CMBTRANSPORTNAME.Text = Convert.ToString(dr("TRANSPORTNAME").ToString)
                        TXTREFRENCE.Text = dr("REFRENCE")
                        TXTISSUEBY.Text = dr("ISSUE")

                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        LBLTOTALMTRS.Text = Val(dr("LBLTOTALMTRS"))
                        LBLTOTALPCS.Text = Val(dr("LBLTOTALPCS"))

                        'Item Grid


                        GRIDJO.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE").ToString, dr("ITEM").ToString, dr("QUALITY").ToString, dr("LOTNO").ToString, dr("BALENO").ToString, dr("DESIGN").ToString, dr("COLORNAME").ToString, Format(dr("PCS"), "0.00"), dr("UNIT"), Format(dr("MTRS"), "0.00"), dr("BARCODE").ToString, Format(dr("OUTPCS"), "0.00"), Format(dr("OUTMTRS"), "0.00"), dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"))


                    Next
                    total()
                    GRIDJO.FirstDisplayedScrollingRowIndex = GRIDJO.RowCount - 1
                    chkchange.CheckState = CheckState.Checked
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
    Sub fillcmb()
        Try
            If CMBFROMGODOWN.Text.Trim = "" Then fillGODOWN(CMBFROMGODOWN, EDIT)
            If CMBTOGODOWN.Text.Trim = "" Then fillGODOWN(CMBTOGODOWN, EDIT)
            If CMBTRANSPORTNAME.Text.Trim = "" Then filltransname(CMBTRANSPORTNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
            fillitemname(CMBYARNQUALITY, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            FILLMILL(CMBMILL, EDIT)
            FILLDESIGN(CMBDESIGN, CMBYARNQUALITY.Text.Trim)
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, CMBYARNQUALITY.Text.Trim)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJEMB As New InterGodownTransferDetails
            OBJEMB.MdiParent = MDIMain
            OBJEMB.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORTNAME_Enter(sender As Object, e As EventArgs) Handles CMBTRANSPORTNAME.Enter
        Try
            If CMBTRANSPORTNAME.Text.Trim = "" Then FILLNAME(CMBTRANSPORTNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORTNAME_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBTRANSPORTNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'"
                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBTRANSPORTNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORTNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBTRANSPORTNAME.Validating
        Try
            If CMBTRANSPORTNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBTRANSPORTNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "SUNDRY CREDITORS")
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



    Sub fillgrid()
        Try
            GRIDJO.Enabled = True

            If GRIDDOUBLECLICK = False Then
                GRIDJO.Rows.Add(Val(txtsrno.Text.Trim), CMBYARNQUALITY.Text.Trim, CMBMILL.Text.Trim, CMBDESIGN.Text.Trim, TXTJOBBERLOTNO.Text.Trim, TXTPSHADE.Text.Trim, cmbcolor.Text.Trim, TXTGRIDLOTNO.Text.Trim, Format(Val(txtqty.Text.Trim), "0.00"), Format(Val(TXTWT.Text.Trim), "0.00"), Format(Val(TXTCONES.Text.Trim), "0.00"), TXTLRNO.Text.Trim, Format(DTLRDATE.Value.Date, "dd/MM/yyyy"), LIFTINGDATE.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, 0, "")
                getsrno(GRIDJO)
            ElseIf GRIDDOUBLECLICK = True Then
                GRIDJO.Item(GSRNO.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
                GRIDJO.Item(GYARNQUALITY.Index, TEMPROW).Value = CMBYARNQUALITY.Text.Trim
                GRIDJO.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILL.Text.Trim
                GRIDJO.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
                GRIDJO.Item(GJOBBERLOTNO.Index, TEMPROW).Value = Val(TXTJOBBERLOTNO.Text.Trim)
                GRIDJO.Item(GPCOLOR.Index, TEMPROW).Value = Val(TXTPSHADE.Text.Trim)
                GRIDJO.Item(GCOLOR.Index, TEMPROW).Value = cmbcolor.Text.Trim
                GRIDJO.Item(GLOTNO.Index, TEMPROW).Value = Format(Val(TXTGRIDLOTNO.Text.Trim), "0.00")
                GRIDJO.Item(GQTY.Index, TEMPROW).Value = Format(Val(txtqty.Text.Trim), "0.00")
                GRIDJO.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.00")
                GRIDJO.Item(GCONES.Index, TEMPROW).Value = Format(Val(TXTCONES.Text.Trim), "0.00")
                GRIDJO.Item(GLRNO.Index, TEMPROW).Value = Val(TXTLRNO.Text.Trim)
                GRIDJO.Item(GLRDATE.Index, TEMPROW).Value = Format(DTLRDATE.Value.Date, "dd/MM/yyyy")
                GRIDJO.Item(GLIFTINGDATE.Index, TEMPROW).Value = Val(LIFTINGDATE.Text.Trim)

                GRIDDOUBLECLICK = False
            End If

            total()

            GRIDJO.FirstDisplayedScrollingRowIndex = GRIDJO.RowCount - 1

            txtsrno.Clear()
            'cmbitemname.Text = ""
            'CMBQUALITY.Text = ""
            'CMBDESIGN.Text = ""
            'cmbcolor.Text = ""






            If GRIDJO.RowCount > 0 Then
                txtsrno.Text = Val(GRIDJO.Rows(GRIDJO.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If
            CMBYARNQUALITY.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub EDITROW()
        Try
            If GRIDJO.CurrentRow.Index >= 0 And GRIDJO.Item(GSRNO.Index, GRIDJO.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDJO.Item(GSRNO.Index, GRIDJO.CurrentRow.Index).Value.ToString
                CMBYARNQUALITY.Text = GRIDJO.Item(GYARNQUALITY.Index, GRIDJO.CurrentRow.Index).Value.ToString
                CMBMILL.Text = GRIDJO.Item(GMILLNAME.Index, GRIDJO.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDJO.Item(GDESIGN.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTJOBBERLOTNO.Text = GRIDJO.Item(GJOBBERLOTNO.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTPSHADE.Text = GRIDJO.Item(GPCOLOR.Index, GRIDJO.CurrentRow.Index).Value.ToString
                cmbcolor.Text = GRIDJO.Item(GCOLOR.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTGRIDLOTNO.Text = GRIDJO.Item(GLOTNO.Index, GRIDJO.CurrentRow.Index).Value.ToString
                txtqty.Text = GRIDJO.Item(GQTY.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTWT.Text = GRIDJO.Item(GWT.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTCONES.Text = GRIDJO.Item(GCONES.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TXTLRNO.Text = GRIDJO.Item(GLRNO.Index, GRIDJO.CurrentRow.Index).Value.ToString
                DTLRDATE.Text = GRIDJO.Item(GLRDATE.Index, GRIDJO.CurrentRow.Index).Value
                LIFTINGDATE.Text = GRIDJO.Item(GLIFTINGDATE.Index, GRIDJO.CurrentRow.Index).Value.ToString
                TEMPROW = GRIDJO.CurrentRow.Index
                txtsrno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDJO.RowCount = 0
LINE1:
            TEMPGODOWNNO = Val(TXTGODOWNNO.Text) - 1
            If TEMPGODOWNNO > 0 Then
                EDIT = True
                YarnInterGodownTransfer_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDJO.RowCount = 0 And TEMPGODOWNNO > 1 Then
                TXTGODOWNNO.Text = TEMPGODOWNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
LINE1:
            TEMPGODOWNNO = Val(TXTGODOWNNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTGODOWNNO.Text.Trim
            clear()
            If Val(TXTGODOWNNO.Text) - 1 >= TEMPGODOWNNO Then
                EDIT = True
                YarnInterGodownTransfer_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDJO.RowCount = 0 And TEMPGODOWNNO < MAXNO Then
                TXTGODOWNNO.Text = TEMPGODOWNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBFROMGODOWN_Enter(sender As Object, e As EventArgs) Handles CMBFROMGODOWN.Enter
        Try
            If CMBFROMGODOWN.Text.Trim = "" Then fillGODOWN(CMBFROMGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMGODOWN_Validating(sender As Object, e As CancelEventArgs) Handles CMBFROMGODOWN.Validating
        Try
            If CMBFROMGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBFROMGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub CMBTOGODOWN_Enter(sender As Object, e As EventArgs) Handles CMBTOGODOWN.Enter
        Try
            If CMBTOGODOWN.Text.Trim = "" Then fillGODOWN(CMBTOGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOGODOWN_Validating(sender As Object, e As CancelEventArgs) Handles CMBTOGODOWN.Validating
        Try
            If CMBTOGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBTOGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDJO_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDJO.CellDoubleClick
        EDITROW()

    End Sub



    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then
                PRINTREPORT(TEMPGODOWNNO)
                PRINTBARCODE()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub PRINTREPORT(ByVal GODOWNNO As Integer)
        Try
            If MsgBox("Wish to Print?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJGDN As New GDNDESIGN
                OBJGDN.MdiParent = MDIMain
                OBJGDN.FRMSTRING = "GODOWNTRANSFER"
                OBJGDN.FORMULA = "{INTERGODOWNTRANSFER.TRANSFER_NO}=" & Val(GODOWNNO) & " and {INTERGODOWNTRANSFER.TRANSFER_yearid}=" & YearId
                OBJGDN.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub PRINTBARCODE()
        Try
            If ALLOWBARCODEPRINT Then


                If ClientName = "PARAS" And UserName <> "Admin" Then Exit Sub

                'PRINT BARCODE
                Dim TEMPMSG As Integer = MsgBox("Wish to Print Barcode?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                'GET FRESH DATA FROM DATABASE (ONLY GRID)
                'THIS IS DONE COZ FOR MULTIUSER THE NOS WILL BE SAME
                'SO WE WILL ADD BARCODE IN SP AND THEN FETCH THAT DATA HERE AFTER THAT WE WILL PRINT BARCODES
                GRIDJO.RowCount = 0
                Dim objJO As New ClsYarnInterGodownTransfer()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPGODOWNNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)
                objJO.alParaval = ALPARAVAL
                Dim dttable As DataTable = objJO.SELECTGODOWN()

                For Each dr As DataRow In dttable.Rows
                    GRIDJO.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE").ToString, dr("ITEM").ToString, dr("QUALITY").ToString, dr("LOTNO").ToString, dr("BALENO").ToString, dr("DESIGN").ToString, dr("COLORNAME").ToString, Format(dr("PCS"), "0.00"), dr("UNIT"), Format(dr("MTRS"), "0.00"), dr("BARCODE").ToString, Format(dr("OUTPCS"), "0.00"), Format(dr("OUTMTRS"), "0.00"), dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"))
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

                If ClientName = "DAKSH" Or ClientName = "MILUXE" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                If ClientName = "MANSI" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED" & Chr(13) & "3 FOR MRP")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
                End If

                If ClientName = "RAJKRIPA" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR LUMP" & Chr(13) & "2 FOR CUTPACK")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                If ClientName = "MANS" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR SALVATROE" & Chr(13) & "2 FOR DONBION")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If


                If ClientName = "AXIS" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR PCS" & Chr(13) & "2 FOR MTRS")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                If ClientName = "KRISHNA" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
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


                For Each ROW As DataGridViewRow In GRIDJO.Rows
                    'TO PRINT BARCODE FROM SELECTED SRNO
                    If (Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0) Then
                        If Val(ROW.Cells(GSRNO.Index).Value) < Val(TXTFROM.Text.Trim) Or Val(ROW.Cells(GSRNO.Index).Value) > Val(TXTTO.Text.Trim) Then GoTo NEXTLINE
                    End If
                    Dim GRIDDESC As String = ""
                    'If ClientName = "KCRAYON" Or ClientName = "NTC" Or ClientName = "KRFABRICS" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "SHREENAKODA" Or ClientName = "RAJKRIPA" Then GRIDDESC = ROW.Cells(GBALENO.Index).Value
                    ''FOR DAKSH WE ARE PASSING REMARKS IN GRIDDESC AS WE WANT TO PRINT THIS REMARKS IN BARCODE
                    'If ClientName = "DAKSH" Or ClientName = "SHALIBHADRA" Then GRIDDESC = txtremarks.Text.Trim

                    'IF barcode is used the BARCODE printING WILL BE BLOCKED
                    If Val(ROW.Cells(GOUTMTRS.Index).Value) > 0 Then GoTo NEXTLINE

                    'BARCODEPRINTING(ROW.Cells(GBARCODE.Index).Value, ROW.Cells(GPIECETYPE.Index).Value, ROW.Cells(GYARNQUALITY.Index).Value, ROW.Cells(GMILLNAME.Index).Value, ROW.Cells(GDESIGN.Index).Value, ROW.Cells(GJOBBERLOTNO.Index).Value, ROW.Cells(GPCOLOR.Index).Value, ROW.Cells(GCOLOR.Index).Value, ROW.Cells(GLOTNO.Index).Value, ROW.Cells(GQTY.Index).Value, ROW.Cells(GWT.Index).Value, ROW.Cells(GCONES.Index).Value, GRIDDESC, Val(ROW.Cells(GLRNO.Index).Value), Val(ROW.Cells(GLRDATE.Index).Value), 0, "", TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, "", "")
NEXTLINE:

                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then

                Dim TEMPMSG As Integer = MsgBox("Wish to Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBJEMB As New ClsInterGodownTransfer

                ALPARAVAL.Add(TEMPGODOWNNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)
                OBJEMB.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJEMB.Delete()
                MsgBox("Entry Deleted Succesfully")
                EDIT = False
                clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(sender As Object, e As EventArgs) Handles CMDSELECTSTOCK.Click
        '        Try
        '            Dim DTJO As New DataTable
        '            Dim OBJSELECTGDN As New SelectYarnStockGdnTransfer
        '            OBJSELECTGDN.GODOWN = CMBFROMGODOWN.Text.Trim
        '            OBJSELECTGDN.ShowDialog()
        '            DTJO = OBJSELECTGDN.DT
        '            If DTJO.Rows.Count > 0 Then
        '                For Each DTROWPS As DataRow In DTJO.Rows

        '                    ''CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
        '                    'For Each ROW As DataGridViewRow In GRIDJO.Rows
        '                    '    If DTROWPS("BARCODE") <> "" And LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(DTROWPS("BARCODE")) Then GoTo LINE1
        '                    'Next

        '                    GRIDJO.Rows.Add(0, DTROWPS("YARNQUALITY"), DTROWPS("MILLNAME"), DTROWPS("DESIGNNO"), "", "", DTROWPS("COLOR"), DTROWPS("LOTNO"), Val(DTROWPS("BAGS")), Format(Val(DTROWPS("WT")), "0.00"), Format(Val(DTROWPS("CONES")), "0.00"), "", "", "")
        'LINE1:
        '                Next
        '                getsrno(GRIDJO)
        '                'total()
        '                GRIDJO.FirstDisplayedScrollingRowIndex = GRIDJO.RowCount - 1
        '            End If
        '            total()
        '        Catch ex As Exception
        '            Throw ex
        '        End Try
    End Sub
    Sub total()
        Try
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALPCS.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDJO.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(ROW.Cells(GQTY.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(sender As Object, e As EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub txtremarks_KeyDown(sender As Object, e As KeyEventArgs) Handles txtremarks.KeyDown
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

    Private Sub DTLRDATE_Validating(sender As Object, e As CancelEventArgs) Handles DTLRDATE.Validating
        Try
            If ClientName = "MJFABRIC" Then Exit Sub
            If CMBYARNQUALITY.Text.Trim <> "" And Val(TXTWT.Text.Trim) > 0 Then
                'If GRIDDOUBLECLICK = False Then
                '    If EDIT = True Then
                '        'GET LAST BARCODE SRNO
                '        Dim LSRNO As Integer = 0
                '        Dim RSRNO As Integer = 0
                '        Dim SNO As Integer = 0
                '        LSRNO = InStr(GRIDJOBIN.Rows(GRIDJOBIN.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                '        RSRNO = InStr(LSRNO + 1, GRIDJOBIN.Rows(GRIDJOBIN.RowCount - 1).Cells(GBARCODE.Index).Value, "/")
                '        SNO = GRIDJOBIN.Rows(GRIDJOBIN.RowCount - 1).Cells(GBARCODE.Index).Value.ToString.Substring(LSRNO, (RSRNO - LSRNO) - 1)

                '        TXTBARCODE.Text = "JI-" & Val(TXTJINO.Text.Trim) & "/" & SNO + 1 & "/" & YearId
                '    Else
                '        TXTBARCODE.Text = "JI-" & Val(TXTJINO.Text.Trim) & "/" & GRIDJOBIN.RowCount + 1 & "/" & YearId
                '    End If
                'End If
                fillgrid()

            Else
                'If CMBJONO.Text.Trim = "" Then
                '    MsgBox("Enter Job Out No.", MsgBoxStyle.Critical)
                '    CMBJONO.Focus()
                If CMBYARNQUALITY.Text.Trim = "" Then
                    MsgBox("Enter  Yarn Quality", MsgBoxStyle.Critical)
                    CMBYARNQUALITY.Focus()
                    'ElseIf CMBQUALITY.Text.Trim = "" Then
                    '    MsgBox("Enter Quality", MsgBoxStyle.Critical)
                    '    CMBQUALITY.Focus()
                    ''ElseIf CMBQUALITY.Text.Trim = "" And ClientName <> "KCRAYON" Then
                    ''    MsgBox("Enter Quality", MsgBoxStyle.Critical)
                    ''    CMBQUALITY.Focus()
                    ''ElseIf CMBDESIGN.Text.Trim = "" Then
                    ''    MsgBox("Enter Design", MsgBoxStyle.Critical)
                    ''    CMBDESIGN.Focus()
                    ''ElseIf CMBDESIGN.Text.Trim = "" And ClientName <> "KCRAYON" Then
                    ''    MsgBox("Enter Design", MsgBoxStyle.Critical)
                    ''    CMBDESIGN.Focus()
                    ''ElseIf Val(txtqty.Text.Trim) = 0 Then
                    ''    MsgBox("Enter Quantity", MsgBoxStyle.Critical)
                    ''    txtqty.Focus()
                    ''ElseIf cmbqtyunit.Text.Trim = "" Then
                    ''    MsgBox("Enter Unit", MsgBoxStyle.Critical)
                    ''    cmbqtyunit.Focus()
                ElseIf Val(TXTWT.Text.Trim) = 0 Then
                    MsgBox("Enter Weight", MsgBoxStyle.Critical)
                    TXTWT.Focus()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validating(sender As Object, e As CancelEventArgs) Handles TXTBARCODE.Validating
        Try
            If TXTBARCODE.Text.Trim <> "" Then
                'CHECKING WHETHER IS IS GONE OUT OR NOT
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("TYPE, FROMNO", "", " OUTBARCODESTOCK ", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "' AND CMPID = " & CmpId & " AND LOCATIONID = " & Locationid & " AND YEARID = " & YearId)
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

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 Then

                If CMBFROMGODOWN.Text.Trim = "" Then
                    MsgBox("Select Godown First", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                TXTBARCODE.Text = TXTBARCODE.Text.Replace(" TRIAL", "")

                'GET DATA FROM BARCODE
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("*", "", "BARCODESTOCK", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "' AND DONE = 0 AND CMPID = " & CmpId & " AND LOCATIONID  = " & Locationid & " AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then

                    'VALIDATE GODOWN
                    If DT.Rows(0).Item("GODOWN") <> CMBFROMGODOWN.Text.Trim Then
                        MsgBox("Item Not in Selected Godown", MsgBoxStyle.Critical)
                        TXTBARCODE.Clear()
                        Exit Sub
                    End If

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    For Each ROW As DataGridViewRow In GRIDJO.Rows
                        If LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(TXTBARCODE.Text.Trim) Then GoTo LINE1
                    Next


                    Dim PCS As Double = 0
                    If ClientName = "TCOT" Or ClientName = "VALIANT" Then PCS = Val(DT.Rows(0).Item("PCS")) Else PCS = 1

                    GRIDJO.Rows.Add(GRIDJO.RowCount + 1, DT.Rows(0).Item("PIECETYPE"), DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("LOTNO"), DT.Rows(0).Item("BALENO"), DT.Rows(0).Item("DESIGNNO"), DT.Rows(0).Item("COLOR"), PCS, DT.Rows(0).Item("UNIT"), Format(Val(DT.Rows(0).Item("MTRS")), "0.00"), DT.Rows(0).Item("BARCODE"), 0, 0, DT.Rows(0).Item("FROMNO"), DT.Rows(0).Item("FROMSRNO"), DT.Rows(0).Item("TYPE"))

                    total()
                    GRIDJO.FirstDisplayedScrollingRowIndex = GRIDJO.RowCount - 1

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

    Private Sub TXTDATE_GotFocus(sender As Object, e As EventArgs) Handles TXTDATE.GotFocus
        TXTDATE.SelectionStart = 0

    End Sub

    Private Sub tstxtbillno_Validating(sender As Object, e As CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDJO.RowCount = 0
                TEMPGODOWNNO = Val(tstxtbillno.Text)
                If TEMPGODOWNNO > 0 Then
                    EDIT = True
                    YarnInterGodownTransfer_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class