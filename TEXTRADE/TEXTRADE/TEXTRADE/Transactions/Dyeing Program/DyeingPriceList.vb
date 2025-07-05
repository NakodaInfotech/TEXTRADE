
Imports System.ComponentModel
Imports BL

Public Class DyeingPriceList

    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPCHGSROW As Integer
    Public EDIT As Boolean
    Public TEMPDPLNO As String
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE, GRIDCHGSDOUBLECLICK As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdEXIT_Click(sender As Object, e As EventArgs) Handles cmdEXIT.Click
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            If TXTDPLNO.ReadOnly = False Then
                alParaval.Add(Val(TXTDPLNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(Format(Convert.ToDateTime(DPLDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBPARTYNAME.Text.Trim)
            alParaval.Add(Val(TXTEXTRACHGS.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            If CHKBLOCK.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)

            Dim GRIDSRNO As String = ""
            Dim TYPE As String = ""
            Dim PROCESS As String = ""
            Dim WHITE As String = ""
            Dim CREAM As String = ""
            Dim LIGHT As String = ""
            Dim MEDIUM As String = ""
            Dim DARK As String = ""
            Dim EXTRADARK As String = ""
            Dim RAINBOW As String = ""
            Dim PROCWHITE As String = ""
            Dim PROCDYED As String = ""
            Dim DISCHARGE As String = ""
            Dim KHADI As String = ""
            Dim ABOVETWOSCREEN As String = ""
            Dim MISCRATE As String = ""

            For Each ROW As DataGridViewRow In GRIDDPL.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = ROW.Cells(GSRNO.Index).Value.ToString
                        TYPE = ROW.Cells(gtype.Index).Value.ToString
                        PROCESS = ROW.Cells(GPROCESS.Index).Value.ToString
                        WHITE = Val(ROW.Cells(gwhite.Index).Value)
                        CREAM = Val(ROW.Cells(gcream.Index).Value)
                        LIGHT = Val(ROW.Cells(glight.Index).Value)
                        MEDIUM = Val(ROW.Cells(gmedium.Index).Value)
                        DARK = Val(ROW.Cells(gdark.Index).Value)
                        EXTRADARK = Val(ROW.Cells(gextradark.Index).Value)
                        RAINBOW = Val(ROW.Cells(grainbow.Index).Value)
                        PROCWHITE = Val(ROW.Cells(GPROCWHITE.Index).Value)
                        PROCDYED = Val(ROW.Cells(GPROCDYED.Index).Value)
                        DISCHARGE = Val(ROW.Cells(gdischarge.Index).Value)
                        KHADI = Val(ROW.Cells(gkhadi.Index).Value)
                        ABOVETWOSCREEN = Val(ROW.Cells(GABOVETWOSCREEN.Index).Value)
                        MISCRATE = Val(ROW.Cells(gmiscrate.Index).Value)
                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(ROW.Cells(GSRNO.Index).Value)
                        TYPE = TYPE & "|" & ROW.Cells(gtype.Index).Value
                        PROCESS = PROCESS & "|" & ROW.Cells(GPROCESS.Index).Value
                        WHITE = WHITE & "|" & Val(ROW.Cells(gwhite.Index).Value)
                        CREAM = CREAM & "|" & Val(ROW.Cells(gcream.Index).Value)
                        LIGHT = LIGHT & "|" & Val(ROW.Cells(glight.Index).Value)
                        MEDIUM = MEDIUM & "|" & Val(ROW.Cells(gmedium.Index).Value)
                        DARK = DARK & "|" & Val(ROW.Cells(gdark.Index).Value)
                        EXTRADARK = EXTRADARK & "|" & Val(ROW.Cells(gextradark.Index).Value)
                        RAINBOW = RAINBOW & "|" & Val(ROW.Cells(grainbow.Index).Value)
                        PROCWHITE = PROCWHITE & "|" & Val(ROW.Cells(GPROCWHITE.Index).Value)
                        PROCDYED = PROCDYED & "|" & Val(ROW.Cells(GPROCDYED.Index).Value)
                        DISCHARGE = DISCHARGE & "|" & Val(ROW.Cells(gdischarge.Index).Value)
                        KHADI = KHADI & "|" & Val(ROW.Cells(gkhadi.Index).Value)
                        ABOVETWOSCREEN = ABOVETWOSCREEN & "|" & Val(ROW.Cells(GABOVETWOSCREEN.Index).Value)
                        MISCRATE = MISCRATE & "|" & Val(ROW.Cells(gmiscrate.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(TYPE)
            alParaval.Add(PROCESS)
            alParaval.Add(WHITE)
            alParaval.Add(CREAM)
            alParaval.Add(LIGHT)
            alParaval.Add(MEDIUM)
            alParaval.Add(DARK)
            alParaval.Add(EXTRADARK)
            alParaval.Add(RAINBOW)
            alParaval.Add(PROCWHITE)
            alParaval.Add(PROCDYED)
            alParaval.Add(DISCHARGE)
            alParaval.Add(KHADI)
            alParaval.Add(ABOVETWOSCREEN)
            alParaval.Add(MISCRATE)

            Dim CSRNO As String = ""
            Dim CCHGS As String = ""
            Dim CAMT As String = ""
            Dim CTAXID As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCHGS.Rows
                If row.Cells(0).Value <> Nothing Then
                    If CSRNO = "" Then
                        CSRNO = row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = row.Cells(ECHARGES.Index).Value.ToString
                        CAMT = row.Cells(EAMT.Index).Value.ToString
                        CTAXID = Val(row.Cells(ETAXID.Index).Value)

                    Else
                        CSRNO = CSRNO & "|" & row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = CCHGS & "|" & row.Cells(ECHARGES.Index).Value.ToString
                        CAMT = CAMT & "|" & row.Cells(EAMT.Index).Value.ToString
                        CTAXID = CTAXID & "|" & Val(row.Cells(ETAXID.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(CSRNO)
            alParaval.Add(CCHGS)
            alParaval.Add(CAMT)
            alParaval.Add(CTAXID)

            Dim OBJDPL As New ClsDyeingPriceList()
            OBJDPL.ALPARAVAL = alParaval

            If EDIT = False Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = OBJDPL.SAVE()
                MessageBox.Show("Details Added")
                TXTDPLNO.Text = DT.Rows(0).Item(0)
            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                OBJDPL.ALPARAVAL.Add(TEMPDPLNO)
                Dim IntResult As Integer = OBJDPL.UPDATE()
                MessageBox.Show("Details Updated")
            End If

            PRINTREPORT()

            EDIT = False
            CLEAR()
            CMBPARTYNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True
        If CMBPARTYNAME.Text.Trim = "" Then
            EP.SetError(CMBPARTYNAME, " Please Fill Party Name ")
            bln = False
        End If

        If GRIDDPL.RowCount = 0 Then
            EP.SetError(CMBPARTYNAME, " Please Enter Data in grid")
            bln = False
        End If

        If DPLDATE.Text = "__/__/____" Then
            EP.SetError(DPLDATE, " Please Enter Proper Date")
            bln = False
        End If


        'CHECK WHETHER THIS PARTY'S PRICE LIST IS ALREADY PRESENT OR NOT, ON FRESH MODE ONLU
        If EDIT = False And CMBPARTYNAME.Text.Trim <> "" Then
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" PL_NO AS PLNO", "", " DYEINGPRICELIST INNER JOIN LEDGERS ON PL_LEDGERID = LEDGERS.ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & CMBPARTYNAME.Text.Trim & "' AND PL_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                EP.SetError(CMBPARTYNAME, "Price List for Dyeing Already Present, Please Modify the Existing Price List")
                bln = False
            End If
        End If

        Return bln
    End Function

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDDPL.Rows.Add(Val(TXTSRNO.Text.Trim), CMBDYEINGTYPE.Text.Trim, CMBPROCESS.Text.Trim, Val(TXTWHITE.Text.Trim), Val(TXTCREAM.Text.Trim), Val(TXTLIGHT.Text.Trim), Val(TXTMEDIUM.Text.Trim), Val(TXTDARK.Text.Trim), Val(TXTEXTRADARK.Text.Trim), Val(TXTRAINBOW.Text.Trim), Val(TXTPROCWHITE.Text.Trim), Val(TXTPROCDYED.Text.Trim), Val(TXTDISCHARGE.Text.Trim), Val(TXTKHADI.Text.Trim), Val(TXTABOVETWOSCREEN.Text.Trim), Val(TXTMISCRATE.Text.Trim))
            ElseIf GRIDDOUBLECLICK = True Then
                GRIDDPL.Item(GSRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
                GRIDDPL.Item(gtype.Index, TEMPROW).Value = CMBDYEINGTYPE.Text.Trim
                GRIDDPL.Item(GPROCESS.Index, TEMPROW).Value = CMBPROCESS.Text.Trim
                GRIDDPL.Item(gwhite.Index, TEMPROW).Value = Val(TXTWHITE.Text.Trim)
                GRIDDPL.Item(gcream.Index, TEMPROW).Value = Val(TXTCREAM.Text.Trim)
                GRIDDPL.Item(glight.Index, TEMPROW).Value = Val(TXTLIGHT.Text.Trim)
                GRIDDPL.Item(gmedium.Index, TEMPROW).Value = Val(TXTMEDIUM.Text.Trim)
                GRIDDPL.Item(gdark.Index, TEMPROW).Value = Val(TXTDARK.Text.Trim)
                GRIDDPL.Item(gextradark.Index, TEMPROW).Value = Val(TXTEXTRADARK.Text.Trim)
                GRIDDPL.Item(grainbow.Index, TEMPROW).Value = Val(TXTRAINBOW.Text.Trim)
                GRIDDPL.Item(GPROCWHITE.Index, TEMPROW).Value = Val(TXTPROCWHITE.Text.Trim)
                GRIDDPL.Item(GPROCDYED.Index, TEMPROW).Value = Val(TXTPROCDYED.Text.Trim)
                GRIDDPL.Item(gdischarge.Index, TEMPROW).Value = Val(TXTDISCHARGE.Text.Trim)
                GRIDDPL.Item(gkhadi.Index, TEMPROW).Value = Val(TXTKHADI.Text.Trim)
                GRIDDPL.Item(GABOVETWOSCREEN.Index, TEMPROW).Value = Val(TXTABOVETWOSCREEN.Text.Trim)
                GRIDDPL.Item(gmiscrate.Index, TEMPROW).Value = Val(TXTMISCRATE.Text.Trim)
                GRIDDOUBLECLICK = False
            End If
            getsrno(GRIDDPL)
            GRIDDPL.FirstDisplayedScrollingRowIndex = GRIDDPL.RowCount - 1

            TXTSRNO.Text = GRIDDPL.RowCount + 1
            CMBDYEINGTYPE.Text = ""
            CMBPROCESS.Text = ""
            TXTWHITE.Text = ""
            TXTCREAM.Text = ""
            TXTLIGHT.Clear()
            TXTMEDIUM.Clear()
            TXTDARK.Clear()
            TXTEXTRADARK.Clear()
            TXTRAINBOW.Clear()
            TXTPROCWHITE.Clear()
            TXTPROCDYED.Clear()
            TXTDISCHARGE.Clear()
            TXTKHADI.Clear()
            TXTABOVETWOSCREEN.Clear()
            TXTMISCRATE.Clear()

            If ClientName = "VINTAGEINDIA" Then CMBPROCESS.Focus() Else CMBDYEINGTYPE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            DPLDATE.Text = Now.Date
            CMBPARTYNAME.Text = ""
            TXTEXTRACHGS.Clear()
            txtremarks.Clear()
            CMBDYEINGTYPE.Text = ""
            CMBPROCESS.Text = ""
            TXTCREAM.Text = ""
            TXTWHITE.Text = ""
            TXTLIGHT.Text = ""
            TXTMEDIUM.Text = ""
            TXTDARK.Clear()
            TXTEXTRADARK.Clear()
            TXTRAINBOW.Clear()
            GRIDDPL.RowCount = 0
            GRIDCHGS.RowCount = 0
            GRIDCHGSDOUBLECLICK = False
            txtbillno.Clear()
            EP.Clear()
            GETMAX_DPLNO()
            GRIDDOUBLECLICK = False
            TXTCHGSSRNO.Clear()
            TXTSRNO.Text = 1
            TXTCHGSAMT.Clear()
            CMBCHARGES.Text = ""
            TXTPROCWHITE.Clear()
            TXTPROCDYED.Clear()
            TXTDISCHARGE.Clear()
            TXTKHADI.Clear()
            TXTABOVETWOSCREEN.Clear()
            TXTMISCRATE.Clear()
            CHKBLOCK.Checked = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKVALIDATE() As Boolean
        Try
            Dim BLN As Boolean = True
            For Each ROW As DataGridViewRow In GRIDCHGS.Rows
                If GRIDCHGSDOUBLECLICK = False Or (GRIDCHGSDOUBLECLICK = True And TEMPCHGSROW <> ROW.Index) Then
                    If CMBCHARGES.Text.Trim = ROW.Cells(ECHARGES.Index).Value Then
                        BLN = False
                    End If
                End If
            Next
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub EDITCHGSROW()
        Try
            TXTCHGSAMT.ReadOnly = False
            If GRIDCHGS.CurrentRow.Index >= 0 And GRIDCHGS.Item(ESRNO.Index, GRIDCHGS.CurrentRow.Index).Value <> Nothing Then
                GRIDCHGSDOUBLECLICK = True
                TXTCHGSSRNO.Text = GRIDCHGS.Item(ESRNO.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                CMBCHARGES.Text = GRIDCHGS.Item(ECHARGES.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSAMT.Text = GRIDCHGS.Item(EAMT.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTTAXID.Text = GRIDCHGS.Item(ETAXID.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TEMPCHGSROW = GRIDCHGS.CurrentRow.Index
                CMBCHARGES.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHGS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHGS.KeyDown
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
                'TOTAL()
                getsrno(GRIDCHGS)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITCHGSROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillchgsgrid()
        Try
            If GRIDCHGSDOUBLECLICK = False Then
                GRIDCHGS.Rows.Add(Val(TXTCHGSSRNO.Text.Trim), CMBCHARGES.Text.Trim, Val(TXTCHGSAMT.Text.Trim), Val(TXTTAXID.Text.Trim))
                getsrno(GRIDCHGS)
            ElseIf GRIDCHGSDOUBLECLICK = True Then
                GRIDCHGS.Item(ESRNO.Index, TEMPCHGSROW).Value = Val(TXTCHGSSRNO.Text.Trim)
                GRIDCHGS.Item(ECHARGES.Index, TEMPCHGSROW).Value = CMBCHARGES.Text.Trim
                GRIDCHGS.Item(EAMT.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSAMT.Text.Trim), "0.00")
                GRIDCHGS.Item(ETAXID.Index, TEMPCHGSROW).Value = Format(Val(TXTTAXID.Text.Trim))

                GRIDCHGSDOUBLECLICK = False

            End If

            GRIDCHGS.FirstDisplayedScrollingRowIndex = GRIDCHGS.RowCount - 1

            TXTCHGSSRNO.Text = GRIDCHGS.RowCount + 1
            CMBCHARGES.Text = ""
            TXTCHGSAMT.Clear()
            CMBCHARGES.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_DPLNO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(PL_no),0) + 1 ", "DYEINGPRICELIST", " AND PL_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTDPLNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub SamplePricelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            FILLDYEDTYPE(CMBDYEINGTYPE)
            FILLPROCESS(CMBPROCESS)
            CLEAR()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objclsSMP As New ClsDyeingPriceList()
                Dim dt_po As DataTable = objclsSMP.selectSMP(TEMPDPLNO, CmpId, Locationid, YearId)

                If dt_po.Rows.Count > 0 Then
                    For Each dr As DataRow In dt_po.Rows
                        TXTDPLNO.Text = TEMPDPLNO
                        DPLDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                        CMBPARTYNAME.Text = Convert.ToString(dr("NAME"))
                        TXTEXTRACHGS.Text = Val(dr("EXTRACHGS"))
                        txtremarks.Text = Convert.ToString(dr("REMARKS"))
                        GRIDDPL.Rows.Add(Val(dr("GRIDSRNO")), dr("TYPE"), dr("PROCESS"), Val(dr("WHITE")), Val(dr("CREAM")), Val(dr("LIGHT")), Val(dr("MEDIUM")), Val(dr("DARK")), Val(dr("EXTRADARK")), Val(dr("RAINBOW")), Val(dr("PROCWHITE")), Val(dr("PROCDYED")), Val(dr("DISCHARGE")), Val(dr("KHADI")), Val(dr("ABOVETWOSCREEN")), Val(dr("MISCRATE")))
                        CHKBLOCK.Checked = Convert.ToBoolean(dr("BLOCK"))
                    Next
                    GRIDDPL.FirstDisplayedScrollingRowIndex = GRIDDPL.RowCount - 1
                    getsrno(GRIDDPL)

                    'CHARGES GRID
                    Dim OBJCM2 As New ClsCommon
                    Dim dt2 As DataTable = OBJCM2.SEARCH(" DYEINGPRICELIST_CHGS.PL_CHGSGRIDSRNO AS CHGSGRIDSRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS CHARGES, ISNULL(DYEINGPRICELIST_CHGS.PL_AMT, 0) AS AMT, ISNULL(TAXMASTER.tax_id, 0) AS TAXID", "", " DYEINGPRICELIST INNER JOIN DYEINGPRICELIST_CHGS ON DYEINGPRICELIST.PL_NO = DYEINGPRICELIST_CHGS.PL_no AND DYEINGPRICELIST.PL_YEARID = DYEINGPRICELIST_CHGS.PL_yearid LEFT OUTER JOIN TAXMASTER ON DYEINGPRICELIST_CHGS.PL_TAXID = TAXMASTER.tax_id AND DYEINGPRICELIST_CHGS.PL_yearid = TAXMASTER.tax_yearid LEFT OUTER JOIN LEDGERS ON DYEINGPRICELIST_CHGS.PL_CHARGESID = LEDGERS.Acc_id AND DYEINGPRICELIST_CHGS.PL_yearid = LEDGERS.Acc_yearid", "AND DYEINGPRICELIST_CHGS.PL_NO = " & TEMPDPLNO & " AND DYEINGPRICELIST_CHGS.PL_YEARID = " & YearId)
                    If dt2.Rows.Count > 0 Then
                        For Each DTR As DataRow In dt2.Rows
                            GRIDCHGS.Rows.Add(DTR("CHGSGRIDSRNO"), DTR("CHARGES"), DTR("AMT"), DTR("TAXID"))
                        Next
                    End If
                    getsrno(GRIDCHGS)

                Else
                    EDIT = False
                    CLEAR()
                End If
            End If

            TXTSRNO.Text = Val(GRIDDPL.RowCount) + 1
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBPARTYNAME.Focus()
    End Sub

    Private Sub DyeingPriceList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1 Then
                TabControl1.SelectedIndex = 0
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2 Then
                TabControl1.SelectedIndex = 1
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                txtbillno.Focus()
                txtbillno.SelectAll()
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for grid foucs
                GRIDDPL.Focus()
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                ToolPREVIOUS_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                Toolnext_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            End If
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

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Dyeing Price List ?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPDPLNO)
                    alParaval.Add(YearId)

                    Dim clspo As New ClsDyeingPriceList()
                    clspo.ALPARAVAL = alParaval
                    Dim IntResult As Integer = clspo.Delete()
                    MsgBox("Price List Deleted")
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

    Sub EDITROW()
        Try
            If GRIDDPL.CurrentRow.Index >= 0 And GRIDDPL.Item(GSRNO.Index, GRIDDPL.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TEMPROW = GRIDDPL.CurrentRow.Index

                TXTSRNO.Text = Val(GRIDDPL.Item(GSRNO.Index, GRIDDPL.CurrentRow.Index).Value)
                CMBDYEINGTYPE.Text = GRIDDPL.Item(gtype.Index, GRIDDPL.CurrentRow.Index).Value.ToString
                CMBPROCESS.Text = GRIDDPL.Item(GPROCESS.Index, GRIDDPL.CurrentRow.Index).Value.ToString
                TXTWHITE.Text = Val(GRIDDPL.Item(gwhite.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTCREAM.Text = Val(GRIDDPL.Item(gcream.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTLIGHT.Text = Val(GRIDDPL.Item(glight.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTMEDIUM.Text = Val(GRIDDPL.Item(gmedium.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTDARK.Text = Val(GRIDDPL.Item(gdark.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTEXTRADARK.Text = Val(GRIDDPL.Item(gextradark.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTRAINBOW.Text = Val(GRIDDPL.Item(grainbow.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTPROCWHITE.Text = Val(GRIDDPL.Item(GPROCWHITE.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTPROCDYED.Text = Val(GRIDDPL.Item(GPROCDYED.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTDISCHARGE.Text = Val(GRIDDPL.Item(gdischarge.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTKHADI.Text = Val(GRIDDPL.Item(gkhadi.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTABOVETWOSCREEN.Text = Val(GRIDDPL.Item(GABOVETWOSCREEN.Index, GRIDDPL.CurrentRow.Index).Value)
                TXTMISCRATE.Text = Val(GRIDDPL.Item(gmiscrate.Index, GRIDDPL.CurrentRow.Index).Value)
                CMBDYEINGTYPE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDDPL_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDDPL.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDDPL.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDDPL.Rows.RemoveAt(GRIDDPL.CurrentRow.Index)
                getsrno(GRIDDPL)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
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
            Dim objpodtls As New DyeingPriceListDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdOK_Click(sender, e)
    End Sub

    Private Sub ToolPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolPREVIOUS.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            GRIDDPL.RowCount = 0
LINE1:
            TEMPDPLNO = Val(TXTDPLNO.Text) - 1
            If TEMPDPLNO > 0 Then
                EDIT = True
                SamplePricelist_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDDPL.RowCount = 0 And TEMPDPLNO > 1 Then
                TXTDPLNO.Text = TEMPDPLNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            GRIDDPL.RowCount = 0
LINE1:
            TEMPDPLNO = Val(TXTDPLNO.Text) + 1
            GETMAX_DPLNO()
            Dim MAXNO As Integer = TXTDPLNO.Text.Trim
            CLEAR()
            If Val(TXTDPLNO.Text) - 1 >= TEMPDPLNO Then
                EDIT = True
                SamplePricelist_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDDPL.RowCount = 0 And TEMPDPLNO < MAXNO Then
                TXTDPLNO.Text = TEMPDPLNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish to Print Price List?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub
            Dim OBJSMP As New SaleOrderDesign
            OBJSMP.MdiParent = MDIMain
            OBJSMP.FRMSTRING = "DYEINGPRICELIST"
            OBJSMP.FORMULA = "{DYEINGPRICELIST.DPL_no}=" & Val(TXTDPLNO.Text.Trim) & " and {DYEINGPRICELIST.DPL_yearid}=" & YearId
            OBJSMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub DPLDATE_Validating(sender As Object, e As CancelEventArgs) Handles DPLDATE.Validating
        Try
            If DPLDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DPLDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDDPL_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDDPL.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtbillno_Validating(sender As Object, e As CancelEventArgs) Handles txtbillno.Validating
        Try
            If Val(txtbillno.Text.Trim) > 0 Then
                GRIDDPL.RowCount = 0
                TEMPDPLNO = Val(txtbillno.Text)
                If TEMPDPLNO > 0 Then
                    EDIT = True
                    SamplePricelist_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillname(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTEXTRACHGS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTEXTRACHGS.KeyPress, TXTWHITE.KeyPress, TXTCREAM.KeyPress, TXTLIGHT.KeyPress, TXTDARK.KeyPress, TXTEXTRADARK.KeyPress, TXTRAINBOW.KeyPress, TXTPROCWHITE.KeyPress, TXTPROCDYED.KeyPress, TXTDISCHARGE.KeyPress, TXTKHADI.KeyPress, TXTABOVETWOSCREEN.KeyPress, TXTMISCRATE.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTMISCRATE_Validating(sender As Object, e As CancelEventArgs) Handles TXTMISCRATE.Validating
        Try
            If (CMBDYEINGTYPE.Text.Trim <> "" Or CMBPROCESS.Text.Trim <> "") And (Val(TXTWHITE.Text.Trim) > 0 Or Val(TXTCREAM.Text.Trim) > 0 Or Val(TXTLIGHT.Text.Trim) > 0 Or Val(TXTMEDIUM.Text.Trim) > 0 Or Val(TXTDARK.Text.Trim) > 0 Or Val(TXTEXTRADARK.Text.Trim) > 0 Or Val(TXTRAINBOW.Text.Trim) > 0 Or Val(TXTPROCWHITE.Text.Trim) > 0 Or Val(TXTPROCDYED.Text.Trim) > 0 Or Val(TXTDISCHARGE.Text.Trim) > 0 Or Val(TXTMISCRATE.Text.Trim) > 0) Then

                'SKIP THIS WHEN PROCESS IS PRESENT
                If CMBDYEINGTYPE.Text.Trim <> "" And CMBPROCESS.Text.Trim = "" And Not CHECKCMBDYEINGTYPE() Then
                    MsgBox("Dyed Type already Present in Grid below ")
                    CMBDYEINGTYPE.Focus()
                    Exit Sub
                End If

                If CMBPROCESS.Text.Trim <> "" And Not CHECKCMBPROCESS() Then
                    MsgBox("Process already Present in Grid below ")
                    CMBDYEINGTYPE.Focus()
                    Exit Sub
                End If

                FILLGRID()
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then namevalidate(CMBPARTYNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validated(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Validated
        Try
            'CHECK WHETHER THIS PARTY'S PRICE LIST IS ALREADY PRESENT OR NOT, ON FRESH MODE ONLU
            If EDIT = False And CMBPARTYNAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 PL_NO AS PLNO", "", " DYEINGPRICELIST INNER JOIN LEDGERS ON PL_LEDGERID = LEDGERS.ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & CMBPARTYNAME.Text.Trim & "' AND PL_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    MsgBox("Price List for Dyeing Already Present, Please Modify the Existing Price List", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Enter(sender As Object, e As EventArgs) Handles CMBCHARGES.Enter
        Try
            If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' or GROUPMASTER.GROUP_SECONDARY = 'Sales A/C')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCHARGES.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' or GROUPMASTER.GROUP_SECONDARY = 'Sales A/C')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBCHARGES.Text = OBJLEDGER.TEMPNAME
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
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS.ACC_ADDLESS,'ADD') AS ADDLESS ", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBCHARGES.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If UCase(DT.Rows(0).Item("ADDLESS")) = "LESS" Then

                        If Val(TXTCHGSAMT.Text.Trim) = 0 Then TXTCHGSAMT.Text = "-"

                    End If
                End If
            Else
                TXTCHGSAMT.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub filltax()
        Try
            TXTCHGSAMT.ReadOnly = False
            TXTTAXID.Text = 0
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable = objclscommon.search(" ISNULL(tax_tax, 0) as TAX, TAX_ID AS TAXID ", "", " TAXMASTER", " AND tax_name = '" & CMBCHARGES.Text & "'  AND tax_cmpid=" & CmpId & " AND tax_LOCATIONID = " & Locationid & " AND tax_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                TXTTAXID.Text = Val(dt.Rows(0).Item("TAXID"))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSAMT_Validating(sender As Object, e As CancelEventArgs) Handles TXTCHGSAMT.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" And (Val(TXTCHGSAMT.Text.Trim) <> 0) Then
                fillchgsgrid()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHGS_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDCHGS.CellDoubleClick
        EDITCHGSROW()
    End Sub

    Private Sub TXTCHGSAMT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTCHGSAMT.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, sender, Me)
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

    Private Sub CMBCHARGES_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCHARGES.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" Then namevalidate(CMBCHARGES, CMBCODE, e, Me, TXTTAXID, "  and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' or GROUPMASTER.GROUP_SECONDARY = 'Sales A/C')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKCMBDYEINGTYPE() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDDPL.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Or GRIDDOUBLECLICK = False Then
                    If CMBDYEINGTYPE.Text.Trim = ROW.Cells(gtype.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function CHECKCMBPROCESS() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDDPL.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Or GRIDDOUBLECLICK = False Then
                    If CMBPROCESS.Text.Trim = ROW.Cells(GPROCESS.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub DyeingPriceList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "VINTAGEINDIA" Then
                gtype.Visible = False
                CMBDYEINGTYPE.Visible = False

                CMBPROCESS.Visible = True
                CMBPROCESS.Left = CMBDYEINGTYPE.Left
                CMBPROCESS.Top = CMBDYEINGTYPE.Top
                GPROCESS.Visible = True
                CMBPROCESS.TabIndex = 0

                TXTWHITE.Enabled = False
                TXTCREAM.Enabled = False
                TXTLIGHT.Enabled = False
                TXTMEDIUM.Enabled = False
                TXTDARK.Enabled = False
                TXTEXTRADARK.Enabled = False
                TXTRAINBOW.Enabled = False
                TXTPROCWHITE.Enabled = False
                TXTPROCDYED.Enabled = False
                TXTDISCHARGE.Enabled = False
                TXTKHADI.Enabled = False
                TXTABOVETWOSCREEN.Enabled = False

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESS_Enter(sender As Object, e As EventArgs) Handles CMBPROCESS.Enter
        Try
            If CMBPROCESS.Text.Trim = "" Then FILLPROCESS(CMBPROCESS)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROCESS_Validating(sender As Object, e As CancelEventArgs) Handles CMBPROCESS.Validating
        Try
            If CMBPROCESS.Text.Trim <> "" Then PROCESSVALIDATE(CMBPROCESS, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class



