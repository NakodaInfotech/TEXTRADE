
Imports BL

Public Class OpeningStock

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Dim CLEAR As Boolean = False
    Public EDIT As Boolean
    Public tempMsg As Integer
    Public FRMSTRING As String

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If gridstock.RowCount = 0 Then
            EP.SetError(txtpcs, "Enter Item Details")
            bln = False
        End If

        For Each row As DataGridViewRow In gridstock.Rows
            If Val(row.Cells(Gpcs.Index).Value) = 0 Then
                EP.SetError(cmbtype, "Pcs Cannot be 0")
                bln = False
            End If
            If Val(row.Cells(gMtrs.Index).Value) = 0 Then
                EP.SetError(cmbtype, "Mtrs Cannot be 0")
                bln = False
            End If
            If row.Cells(gQuality.Index).Value = "" Then
                EP.SetError(cmbtype, "Quality cannot be Blank")
                bln = False
            End If
            If row.Cells(GMERCHANT.Index).Value = "" Then
                EP.SetError(cmbtype, "Item Name cannot be Blank")
                bln = False
            End If
            If row.Cells(Gunit.Index).Value = "" Then
                EP.SetError(cmbtype, "Unit cannot be Blank")
                bln = False
            End If
            If cmbtype.Text = "INHOUSE" Then
                If row.Cells(GDESIGN.Index).Value = "" Then
                    EP.SetError(cmbtype, "Design cannot be Blank")
                    bln = False
                End If
            ElseIf cmbtype.Text = "JOBBERSTOCK" Then
                If row.Cells(gtoname.Index).Value = "" Then
                    EP.SetError(cmbtype, "Jobber Name cannot be Blank")
                    bln = False
                End If
            End If
        Next
        Return bln
    End Function

    Sub EDITROW()
        Try
            If gridstock.CurrentRow.Index >= 0 And gridstock.Item(gsrno.Index, gridstock.CurrentRow.Index).Value <> Nothing Then

                If Convert.ToBoolean(gridstock.Rows(gridstock.CurrentRow.Index).Cells(gdone.Index).Value) = True Then 'If row.Cells(16).Value <> "0" Then 
                    MsgBox("Item Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If gridstock.Rows(gridstock.CurrentRow.Index).DefaultCellStyle.BackColor = Color.Yellow Then
                    MsgBox("Item Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                GRIDDOUBLECLICK = True
                TXTNO.Text = gridstock.Item(GNO.Index, gridstock.CurrentRow.Index).Value.ToString
                txtsrno.Text = gridstock.Item(gsrno.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTLOTNO.Text = gridstock.Item(GLOTNO.Index, gridstock.CurrentRow.Index).Value.ToString
                cmbmerchant.Text = gridstock.Item(GMERCHANT.Index, gridstock.CurrentRow.Index).Value.ToString
                cmbquality.Text = gridstock.Item(gQuality.Index, gridstock.CurrentRow.Index).Value.ToString
                CMBPROCESS.Text = gridstock.Item(gProcess.Index, gridstock.CurrentRow.Index).Value.ToString
                cmbname.Text = gridstock.Item(gname.Index, gridstock.CurrentRow.Index).Value.ToString
                cmbtoname.Text = gridstock.Item(gtoname.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTBILLNO.Text = gridstock.Item(GBILLNO.Index, gridstock.CurrentRow.Index).Value.ToString
                cmbgodown.Text = gridstock.Item(GGODOWN.Index, gridstock.CurrentRow.Index).Value.ToString
                cmbpiecetype.Text = gridstock.Item(gpiecetype.Index, gridstock.CurrentRow.Index).Value.ToString
                txtcut.Text = gridstock.Item(GCUT.Index, gridstock.CurrentRow.Index).Value.ToString
                CMBDESIGNNO.Text = gridstock.Item(GDESIGN.Index, gridstock.CurrentRow.Index).Value.ToString
                cmbcolor.Text = gridstock.Item(gcolor.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTWT.Text = gridstock.Item(GWT.Index, gridstock.CurrentRow.Index).Value.ToString
                txtpcs.Text = gridstock.Item(Gpcs.Index, gridstock.CurrentRow.Index).Value.ToString
                cmbunit.Text = gridstock.Item(Gunit.Index, gridstock.CurrentRow.Index).Value.ToString
                txtMtrs.Text = gridstock.Item(gMtrs.Index, gridstock.CurrentRow.Index).Value.ToString
                CMBPER.Text = gridstock.Item(GPer.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTRATE.Text = gridstock.Item(GRATE.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTAMOUNT.Text = gridstock.Item(GAMOUNT.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTADDLESS.Text = Val(gridstock.Item(GADDLESS.Index, gridstock.CurrentRow.Index).Value)
                TXTNETTRATE.Text = Val(gridstock.Item(GNETTRATE.Index, gridstock.CurrentRow.Index).Value)
                TXTPARTYCHNO.Text = gridstock.Item(GPARTYCHNO.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTBALENO.Text = gridstock.Item(GBALENO.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTGRIDREMARKS.Text = gridstock.Item(GGRIDREMARKS.Index, gridstock.CurrentRow.Index).Value.ToString
                CMBDYEINGJOB.Text = gridstock.Item(GDYEINGJOB.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTREMARKS.Text = gridstock.Item(GREMARKS.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTLRNO.Text = gridstock.Item(GLRNO.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTBARCODE.Text = gridstock.Item(gBarcode.Index, gridstock.CurrentRow.Index).Value.ToString
                TEMPROW = gridstock.CurrentRow.Index
                txtsrno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridstock_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridstock.CellContentClick
        Dim OBJ As Object = gridstock.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

        If IsDBNull(OBJ) Then
            TXTSEARCHBARCODE.Text = "" ' blank if dbnull values
        Else
            TXTSEARCHBARCODE.Text = CType(OBJ, String)
        End If
    End Sub

    Private Sub gridSO_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridstock.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' and LEDGERS.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.acc_YEARid = " & YearId
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then
                If ClientName = "RADHA" Then
                    NAMEVALIDATE(cmbname, CMBCODE, e, Me, txtadd, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors')", "Sundry Creditors", "ACCOUNTS")
                Else
                    NAMEVALIDATE(cmbname, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "ACCOUNTS")
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then
                If ClientName = "RADHA" Then
                    FILLNAME(cmbname, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' OR GroupMaster.GROUP_SECONDARY = 'Sundry Debtors')")
                Else
                    FILLNAME(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'")
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtoname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtoname.Validating
        Try
            If cmbtoname.Text.Trim <> "" Then namevalidate(cmbtoname, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtoname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtoname.Enter
        Try
            If cmbtoname.Text.Trim = "" Then fillname(cmbtoname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbMERCHANT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbmerchant.Enter
        Try
            If cmbmerchant.Text.Trim = "" Then fillitemname(cmbmerchant, " AND ITEMMASTER.ITEM_FRMSTRING IN ('MERCHANT')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbMERCHANT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbmerchant.Validating
        Try
            If cmbmerchant.Text.Trim <> "" Then itemvalidate(cmbmerchant, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT' ", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbPIECETYPE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbpiecetype.Enter
        Try
            If cmbpiecetype.Text.Trim = "" Then fillPIECETYPE(cmbpiecetype)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbPIECETYPE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbpiecetype.Validating
        Try
            If cmbpiecetype.Text.Trim <> "" Then PIECETYPEvalidate(cmbpiecetype, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try

            fillPIECETYPE(cmbpiecetype)
            If cmbmerchant.Text.Trim = "" Then fillitemname(cmbmerchant, " AND ITEMMASTER.ITEM_FRMSTRING IN ('MERCHANT')")
            fillQUALITY(cmbquality, EDIT)
            FILLDESIGN(CMBDESIGNNO, cmbmerchant.Text.Trim)
            FILLCOLOR(cmbcolor, CMBDESIGNNO.Text.Trim, cmbmerchant.Text.Trim)
            If ClientName = "RADHA" Then FILLNAME(cmbname, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' OR GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors')") Else fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'")
            fillname(cmbtoname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'")
            fillGODOWN(cmbgodown, EDIT)
            fillunit(cmbunit)
            If cmbunit.Text.Trim = "" Then fillunit(cmbunit)
            If CMBPROCESS.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" distinct process_NAME ", "", " processmaster ", " and process_cmpid=" & CmpId & " and process_locationid = " & Locationid & " and process_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "process_NAME"
                    CMBPROCESS.DataSource = dt
                    CMBPROCESS.DisplayMember = "process_NAME"
                    CMBPROCESS.Text = ""
                End If
                CMBPROCESS.SelectAll()
            End If
            cmbunit.Text = "Pcs"
            cmbtype.Text = FRMSTRING
            FILLRACK(CMBRACK)
            FILLSHELF(CMBSHELF)

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()

        gridstock.Enabled = True

        If GRIDDOUBLECLICK = False Then
            gridstock.Rows.Add(Val(txtsrno.Text.Trim), Val(TXTNO.Text.Trim), TXTLOTNO.Text.Trim, cmbpiecetype.Text.Trim, cmbmerchant.Text.Trim, cmbquality.Text.Trim, CMBDESIGNNO.Text.Trim, cmbcolor.Text.Trim, CMBPROCESS.Text.Trim, cmbname.Text.Trim, cmbtoname.Text, Val(TXTBILLNO.Text.Trim), cmbgodown.Text.Trim, txtcut.Text.Trim, Val(TXTWT.Text.Trim), cmbunit.Text.Trim, Val(txtpcs.Text.Trim), Val(txtMtrs.Text.Trim), CMBPER.Text.Trim, CMBRACK.Text.Trim, CMBSHELF.Text.Trim, Val(TXTRATE.Text.Trim), Val(TXTAMOUNT.Text.Trim), Val(TXTADDLESS.Text.Trim), Val(TXTNETTRATE.Text.Trim), TXTPARTYCHNO.Text.Trim, TXTBALENO.Text.Trim, TXTGRIDREMARKS.Text.Trim, CMBDYEINGJOB.Text.Trim, TXTREMARKS.Text.Trim, TXTLRNO.Text.Trim, TXTBARCODE.Text.Trim, 0, 0, 0, 0)
            getsrno(gridstock)
            gridstock.FirstDisplayedScrollingRowIndex = gridstock.RowCount - 1
        ElseIf GRIDDOUBLECLICK = True Then
            gridstock.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            gridstock.Item(GLOTNO.Index, TEMPROW).Value = TXTLOTNO.Text.Trim
            gridstock.Item(gpiecetype.Index, TEMPROW).Value = cmbpiecetype.Text.Trim
            gridstock.Item(GMERCHANT.Index, TEMPROW).Value = cmbmerchant.Text.Trim
            gridstock.Item(gQuality.Index, TEMPROW).Value = cmbquality.Text.Trim
            gridstock.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGNNO.Text.Trim
            gridstock.Item(gcolor.Index, TEMPROW).Value = cmbcolor.Text.Trim
            gridstock.Item(gProcess.Index, TEMPROW).Value = CMBPROCESS.Text.Trim
            gridstock.Item(gname.Index, TEMPROW).Value = cmbname.Text.Trim
            gridstock.Item(gtoname.Index, TEMPROW).Value = cmbtoname.Text.Trim
            gridstock.Item(GBILLNO.Index, TEMPROW).Value = Val(TXTBILLNO.Text.Trim)
            gridstock.Item(GGODOWN.Index, TEMPROW).Value = cmbgodown.Text.Trim
            gridstock.Item(GCUT.Index, TEMPROW).Value = txtcut.Text.Trim
            gridstock.Item(GWT.Index, TEMPROW).Value = Val(TXTWT.Text.Trim)
            gridstock.Item(Gunit.Index, TEMPROW).Value = cmbunit.Text.Trim
            gridstock.Item(Gpcs.Index, TEMPROW).Value = Val(txtpcs.Text.Trim)
            gridstock.Item(gMtrs.Index, TEMPROW).Value = txtMtrs.Text.Trim
            gridstock.Item(GPer.Index, TEMPROW).Value = CMBPER.Text.Trim
            gridstock.Item(GRACK.Index, TEMPROW).Value = CMBRACK.Text.Trim
            gridstock.Item(GSHELF.Index, TEMPROW).Value = CMBSHELF.Text.Trim
            gridstock.Item(GRATE.Index, TEMPROW).Value = TXTRATE.Text.Trim
            gridstock.Item(GAMOUNT.Index, TEMPROW).Value = TXTAMOUNT.Text.Trim
            gridstock.Item(GADDLESS.Index, TEMPROW).Value = Val(TXTADDLESS.Text.Trim)
            gridstock.Item(GNETTRATE.Index, TEMPROW).Value = Val(TXTNETTRATE.Text.Trim)
            gridstock.Item(GPARTYCHNO.Index, TEMPROW).Value = TXTPARTYCHNO.Text.Trim
            gridstock.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
            gridstock.Item(GGRIDREMARKS.Index, TEMPROW).Value = TXTGRIDREMARKS.Text.Trim
            gridstock.Item(GDYEINGJOB.Index, TEMPROW).Value = CMBDYEINGJOB.Text.Trim
            gridstock.Item(GREMARKS.Index, TEMPROW).Value = TXTREMARKS.Text.Trim
            gridstock.Item(GLRNO.Index, TEMPROW).Value = TXTLRNO.Text.Trim
            GRIDDOUBLECLICK = False
        End If

        If CLEAR = True Then
            txtsrno.Text = gridstock.RowCount + 1
            If ClientName = "REALCORPORATION" Then
                cmbmerchant.Text = ""
                CMBDESIGNNO.Text = ""
                cmbcolor.Text = ""
            End If

            If ClientName <> "TINUMINU" And ClientName <> "RADHA" Then cmbname.Text = ""
            If ClientName <> "RADHA" Then TXTBILLNO.Clear()

            TXTWT.Clear()
            txtcut.Clear()
            txtpcs.Text = 1
            TXTYARDS.Clear()
            txtMtrs.Clear()
            If ClientName <> "KRFABRICS" Then CMBRACK.Text = ""
            CMBSHELF.Text = ""
            'CMBPER.Text = ""
            TXTRATE.Clear()
            TXTAMOUNT.Clear()
            If ClientName = "DILIP" Or ClientName = "DILIPNEW" Then TXTREMARKS.Text = Val(TXTREMARKS.Text.Trim) + 1 Else TXTREMARKS.Clear()
            If ClientName <> "TINUMINU" And ClientName <> "RADHA" Then TXTPARTYCHNO.Clear()
            TXTBALENO.Clear()
            TXTLRNO.Clear()
            TXTGRIDREMARKS.Clear()
            TXTBARCODE.Clear()
            TXTNO.Clear()
            TXTADDLESS.Clear()
            TXTNETTRATE.Clear()
            If ClientName = "KDFAB" Or ClientName = "SANGHVI" Or ClientName = "MANIBHADRA" Or ClientName = "TINUMINU" Then
                txtMtrs.Focus()
            ElseIf ClientName = "DILIP" Or ClientName = "DILIPNEW" Then
                txtpcs.Focus()
            Else
                TXTLOTNO.Focus()
            End If
        End If

    End Sub

    Private Sub OpeningStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.F5 Then
            gridstock.Focus()
        ElseIf e.KeyCode = Keys.F12 And gridstock.RowCount > 0 Then
            If gridstock.SelectedRows.Count = 0 Then Exit Sub
            Dim TEMPROWINDEX As Integer = gridstock.CurrentRow.Index
            TXTLOTNO.Text = gridstock.Item(GLOTNO.Index, TEMPROWINDEX).Value
            cmbpiecetype.Text = gridstock.Item(gpiecetype.Index, TEMPROWINDEX).Value
            cmbmerchant.Text = gridstock.Item(GMERCHANT.Index, TEMPROWINDEX).Value
            cmbquality.Text = gridstock.Item(gQuality.Index, TEMPROWINDEX).Value
            CMBDESIGNNO.Text = gridstock.Item(GDESIGN.Index, TEMPROWINDEX).Value
            cmbcolor.Text = gridstock.Item(gcolor.Index, TEMPROWINDEX).Value
            CMBPROCESS.Text = gridstock.Item(gProcess.Index, TEMPROWINDEX).Value
            cmbname.Text = gridstock.Item(gname.Index, TEMPROWINDEX).Value
            cmbtoname.Text = gridstock.Item(gtoname.Index, TEMPROWINDEX).Value
            TXTBILLNO.Text = gridstock.Item(GBILLNO.Index, TEMPROWINDEX).Value
            cmbgodown.Text = gridstock.Item(GGODOWN.Index, TEMPROWINDEX).Value

            txtcut.Text = gridstock.Item(GCUT.Index, TEMPROWINDEX).Value
            TXTWT.Text = gridstock.Item(GWT.Index, TEMPROWINDEX).Value
            cmbunit.Text = gridstock.Item(Gunit.Index, TEMPROWINDEX).Value
            txtpcs.Text = gridstock.Item(Gpcs.Index, TEMPROWINDEX).Value

            txtMtrs.Text = gridstock.Item(gMtrs.Index, TEMPROWINDEX).Value
            CMBRACK.Text = gridstock.Item(GRACK.Index, TEMPROWINDEX).Value
            CMBSHELF.Text = gridstock.Item(GSHELF.Index, TEMPROWINDEX).Value

            TXTRATE.Text = gridstock.Item(GRATE.Index, TEMPROWINDEX).Value
            TXTAMOUNT.Text = gridstock.Item(GAMOUNT.Index, TEMPROWINDEX).Value
            TXTADDLESS.Text = gridstock.Item(GADDLESS.Index, TEMPROWINDEX).Value
            TXTNETTRATE.Text = gridstock.Item(GNETTRATE.Index, TEMPROWINDEX).Value
            TXTREMARKS.Text = gridstock.Item(GREMARKS.Index, TEMPROWINDEX).Value
            TXTLRNO.Text = gridstock.Item(GLRNO.Index, TEMPROWINDEX).Value
            TXTPARTYCHNO.Text = gridstock.Item(GPARTYCHNO.Index, TEMPROWINDEX).Value
            TXTBALENO.Text = gridstock.Item(GBALENO.Index, TEMPROWINDEX).Value
            TXTGRIDREMARKS.Text = gridstock.Item(GGRIDREMARKS.Index, TEMPROWINDEX).Value
            CMBDYEINGJOB.Text = gridstock.Item(GDYEINGJOB.Index, TEMPROWINDEX).Value
            TXTBARCODE.Text = gridstock.Item(gBarcode.Index, TEMPROWINDEX).Value

            txtMtrs.Focus()
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub CMBPER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPER.Validating
        Try
            If CMBPER.Text = "Mtrs" Then
                TXTAMOUNT.Text = Val(txtMtrs.Text) * Val(TXTRATE.Text)
            Else
                TXTAMOUNT.Text = Val(txtpcs.Text) * Val(TXTRATE.Text)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_ENTER(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbgodown.Enter
        Try
            If cmbgodown.Text.Trim = "" Then fillGODOWN(cmbgodown, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbgodown.Validating
        Try
            If cmbgodown.Text.Trim <> "" Then GODOWNVALIDATE(cmbgodown, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpeningStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'OPENING'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillcmb()
            CMBDYEINGJOB.SelectedIndex = 0
            If USERGODOWN <> "" Then cmbgodown.Text = USERGODOWN Else cmbgodown.Text = ""
            openingdate.Value = AccFrom.Date

            cmbpiecetype.Text = "FRESH"


            If FRMSTRING = "INHOUSE" Then
                If ClientName = "MABHAY" Then
                    cmbmerchant.Text = "FINISHED FABRICS"
                    CHKPRINT.Visible = True
                    LBLFROM.Visible = True
                    LBLTO.Visible = True
                    TXTFROM.Visible = True
                    TXTTO.Visible = True
                ElseIf ClientName = "SVS" Then
                    gridstock.Columns(GLOTNO.Index).HeaderText = "Bale No"
                    gridstock.Columns(GWT.Index).HeaderText = "Width"
                End If
            End If

            If ClientName = "MIDAS" Then CHKPRINT.CheckState = CheckState.Unchecked

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.Execute_Any_String(" SELECT  STOCKMASTER.SM_TYPE AS TYPE, STOCKMASTER.SM_DATE AS DATE, ISNULL(STOCKMASTER.SM_GRIDSRNO, 0) AS GRIDSRNO, STOCKMASTER.SM_NO AS SMNO, ISNULL(STOCKMASTER.SM_LOTNO, '') AS LOTNO, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS MERCHANT, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(PROCESSMASTER.PROCESS_NAME, '') AS PROCESS, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME,  ISNULL(TONAME.Acc_cmpname, '') AS TONAME, ISNULL(STOCKMASTER.SM_BILLNO, '') AS BILLNO, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, STOCKMASTER.SM_CUT AS CUT, ISNULL(STOCKMASTER.SM_WT, 0) AS WT, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(STOCKMASTER.SM_PCS, 0) AS PCS, ISNULL(STOCKMASTER.SM_MTRS, 0) AS MTRS, ISNULL(STOCKMASTER.SM_PER, '') AS PER,ISNULL(STOCKMASTER.SM_RATE, 0) AS RATE, ISNULL(STOCKMASTER.SM_AMOUNT, 0) AS AMOUNT, ISNULL(STOCKMASTER.SM_REMARKS, '') AS REMARKS, ISNULL(STOCKMASTER.SM_LRNO, '') AS LRNO, ISNULL(STOCKMASTER.SM_BARCODE, '') AS BARCODE, ISNULL(STOCKMASTER.SM_DONE, 0) AS DONE, ISNULL(STOCKMASTER.SM_LOTCOMPLETED, 0) AS LOTCOMPLETED, ISNULL(STOCKMASTER.SM_OUTPCS, 0) AS OUTPCS, ISNULL(STOCKMASTER.SM_OUTMTRS, 0) AS OUTMTRS, STOCKMASTER.SM_CMPID AS CMPID, STOCKMASTER.SM_LOCATIONID AS LOCATIONID, STOCKMASTER.SM_YEARID AS YEARID, ISNULL(SHELFMASTER.SHELF_NAME, '') AS SHELF, ISNULL(RACKMASTER.RACK_NAME, '') AS RACK, ISNULL(STOCKMASTER.SM_PARTYCHALLANNO,'') AS PARTYCHNO, ISNULL(STOCKMASTER.SM_BALENO,'') AS BALENO, ISNULL(STOCKMASTER.SM_GRIDREMARKS,'') AS GRIDREMARKS, ISNULL(STOCKMASTER.SM_DYEINGJOB,'DYEING') AS DYEINGJOB, ISNULL(STOCKMASTER.SM_ADDLESS,0) AS ADDLESS, ISNULL(STOCKMASTER.SM_NETTRATE,0) AS NETTRATE FROM STOCKMASTER LEFT OUTER JOIN SHELFMASTER ON STOCKMASTER.SM_SHELFID = SHELFMASTER.SHELF_ID LEFT OUTER JOIN RACKMASTER ON STOCKMASTER.SM_RACKID = RACKMASTER.RACK_ID LEFT OUTER JOIN LEDGERS AS TONAME ON STOCKMASTER.SM_LEDGERIDTO = TONAME.Acc_id LEFT OUTER JOIN DESIGNMASTER ON STOCKMASTER.SM_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN GODOWNMASTER ON STOCKMASTER.SM_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN PIECETYPEMASTER ON STOCKMASTER.SM_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN QUALITYMASTER ON STOCKMASTER.SM_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN PROCESSMASTER ON STOCKMASTER.SM_PROCESSID = PROCESSMASTER.PROCESS_ID LEFT OUTER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN UNITMASTER ON STOCKMASTER.SM_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN COLORMASTER ON STOCKMASTER.SM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN ITEMMASTER ON STOCKMASTER.SM_ITEMID = ITEMMASTER.item_id WHERE STOCKMASTER.SM_TYPE = '" & FRMSTRING & "' AND STOCKMASTER.SM_YEARID = " & YearId & " ORDER BY SM_NO", "", "")
            If dttable.Rows.Count > 0 Then
                gridstock.RowCount = 0
                For Each DR As DataRow In dttable.Rows
                    openingdate.Value = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                    CMBPROCESS.Text = Convert.ToString(DR("TYPE").ToString)
                    gridstock.Rows.Add(Val(DR("GRIDSRNO")), DR("SMNO"), DR("LOTNO"), DR("PIECETYPE"), DR("MERCHANT"), DR("QUALITY"), DR("DESIGN"), DR("COLOR"), DR("PROCESS"), DR("NAME"), DR("TONAME"), Val(DR("BILLNO")), DR("GODOWN"), Format(Val(DR("CUT")), "0.00"), Val(DR("WT")), DR("UNIT"), Val(DR("PCS")), Format(Val(DR("MTRS")), "0.00"), DR("PER").ToString, DR("RACK"), DR("SHELF"), Format(Val(DR("RATE")), "0.00"), Format(Val(DR("AMOUNT")), "0.00"), Format(Val(DR("ADDLESS")), "0.00"), Format(Val(DR("NETTRATE")), "0.00"), DR("PARTYCHNO"), DR("BALENO"), DR("GRIDREMARKS"), DR("DYEINGJOB"), DR("REMARKS"), DR("LRNO"), DR("BARCODE"), DR("DONE"), Format(Val(DR("OUTPCS")), "0.00"), Format(Val(DR("OUTMTRS")), "0.00"))
                    If Convert.ToBoolean(DR("DONE")) = True Or Convert.ToBoolean(DR("LOTCOMPLETED")) = True Or Val(DR("OUTMTRS")) > 0 Or Val(DR("OUTPCS")) > 0 Then gridstock.Rows(gridstock.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                Next
                getsrno(gridstock)
                gridstock.FirstDisplayedScrollingRowIndex = gridstock.RowCount - 1
            End If

            If gridstock.RowCount > 0 Then
                txtsrno.Text = Val(gridstock.Rows(gridstock.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbunit_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbunit.Validated
        Try
            If cmbtype.Text = "STORE" Then
                If cmbmerchant.Text.Trim <> "" And cmbname.Text.Trim <> "" And Val(TXTWT.Text.Trim) > 0 And Val(txtpcs.Text) <> 0 And cmbunit.Text.Trim <> "" Then
                    fillgrid()
                Else
                    If cmbmerchant.Text.Trim = "" Then
                        MsgBox("Please Fill Item Name ")
                        cmbmerchant.Focus()
                        Exit Sub
                    End If


                    If Val(txtpcs.Text.Trim) = 0 Then
                        MsgBox("Please Fill Quantity ")
                        txtpcs.Focus()
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
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


    Private Sub CMBDESIGNNO_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGNNO.Enter
        Try
            If CMBDESIGNNO.Text.Trim = "" Then FILLDESIGN(CMBDESIGNNO, cmbmerchant.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGNNO.Validating
        Try
            If CMBDESIGNNO.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGNNO, e, Me, cmbmerchant.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcolor.Enter
        Try
            If cmbcolor.Text.Trim = "" Then FILLCOLOR(cmbcolor, CMBDESIGNNO.Text.Trim, cmbmerchant.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcolor.Validating
        Try
            If cmbcolor.Text.Trim <> "" Then COLORVALIDATE(cmbcolor, e, Me, CMBDESIGNNO.Text.Trim, cmbmerchant.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbquality_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbquality.Enter
        Try
            If cmbquality.Text.Trim = "" Then fillQUALITY(cmbquality, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbquality_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbquality.Validating
        Try
            If cmbquality.Text.Trim <> "" Then QUALITYVALIDATE(cmbquality, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SAVE()
        Try
            If ISLOCKYEAR = True Then
                MsgBox("Unable to Make changes, Year is Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If


            Dim ALPARAVAL As New ArrayList
            Dim OBJSM As New ClsStockMaster

            ALPARAVAL.Add(openingdate.Value.Date)
            ALPARAVAL.Add(cmbtype.Text.Trim)

            ALPARAVAL.Add(txtsrno.Text.Trim)
            ALPARAVAL.Add(TXTLOTNO.Text.Trim)
            ALPARAVAL.Add(cmbpiecetype.Text.Trim)
            ALPARAVAL.Add(cmbmerchant.Text.Trim)
            ALPARAVAL.Add(cmbquality.Text.Trim)
            ALPARAVAL.Add(CMBDESIGNNO.Text.Trim)
            ALPARAVAL.Add(cmbcolor.Text.Trim)
            ALPARAVAL.Add(CMBPROCESS.Text.Trim)
            ALPARAVAL.Add(cmbname.Text.Trim)
            ALPARAVAL.Add(cmbtoname.Text.Trim)
            ALPARAVAL.Add(Val(TXTBILLNO.Text.Trim))
            ALPARAVAL.Add(cmbgodown.Text.Trim)
            ALPARAVAL.Add(Val(txtcut.Text.Trim))
            ALPARAVAL.Add(Val(TXTWT.Text.Trim))
            ALPARAVAL.Add(cmbunit.Text.Trim)
            ALPARAVAL.Add(Val(txtpcs.Text.Trim))
            ALPARAVAL.Add(Val(txtMtrs.Text.Trim))
            ALPARAVAL.Add(CMBPER.Text.Trim)
            ALPARAVAL.Add(CMBRACK.Text.Trim)
            ALPARAVAL.Add(CMBSHELF.Text.Trim)
            ALPARAVAL.Add(Val(TXTRATE.Text.Trim))
            ALPARAVAL.Add(Val(TXTAMOUNT.Text.Trim))
            ALPARAVAL.Add(Val(TXTADDLESS.Text.Trim))
            ALPARAVAL.Add(Val(TXTNETTRATE.Text.Trim))
            ALPARAVAL.Add(TXTPARTYCHNO.Text.Trim)
            ALPARAVAL.Add(TXTBALENO.Text.Trim)
            ALPARAVAL.Add(TXTGRIDREMARKS.Text.Trim)
            ALPARAVAL.Add(CMBDYEINGJOB.Text.Trim)
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(TXTLRNO.Text.Trim)
            ALPARAVAL.Add(TXTBARCODE.Text.Trim)

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(0)

            OBJSM.alParaval = ALPARAVAL
            If GRIDDOUBLECLICK = False Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = OBJSM.save()
                MessageBox.Show("Details Added")
                'If DT.Rows.Count > 0 Then TXTNO.Text = DT.Rows(0).Item(0)
                'BARCODE()
            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                ALPARAVAL.Add(TXTNO.Text.Trim)
                Dim INTRES As Integer = OBJSM.UPDATE()
                MessageBox.Show("Details Updated")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub BARCODE()
        Try
            'GET BARCODE NO FROM DATABASE
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" SM_BARCODE AS BARCODE ", "", " STOCKMASTER ", " AND SM_NO = " & Val(TXTNO.Text.Trim) & " AND SM_CMPID = " & CmpId & " AND SM_LOCATIONID = " & Locationid & " AND SM_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then TXTBARCODE.Text = DT.Rows(0).Item("BARCODE")
            PRINTBARCODE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTBARCODE()
        Try
            If ALLOWBARCODEPRINT = True Then
                If cmbtype.Text.Trim = "INHOUSE" Then

                    If CHKPRINT.Checked = False And Val(TXTFROM.Text.Trim) = 0 Then Exit Sub

                    Dim WHOLESALEBARCODE As Integer = 0
                    If ClientName = "CC"  Or ClientName = "C3" Or ClientName = "SHREEDEV" Then WHOLESALEBARCODE = MsgBox("Wish to Print Wholesale Barcode?", MsgBoxStyle.YesNo)

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

                    If ClientName = "RAJKRIPA" Or ClientName = "MOHATUL" Then
                        TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR LUMP" & Chr(13) & "2 FOR CUTPACK")
                        If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                    End If

                    If ClientName = "KRISHNA" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "SIMPLEX" Then
                        TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR BOX STICKER")
                        If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                    End If

                    If ClientName = "MANS" Then
                        TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR SALVATROE" & Chr(13) & "2 FOR DONBION" & Chr(13) & "2 FOR OCM")
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


                    If CHKPRINT.CheckState = CheckState.Checked Then
                        BARCODEPRINTING(TXTBARCODE.Text.Trim, cmbpiecetype.Text.Trim, cmbmerchant.Text.Trim, cmbquality.Text.Trim, CMBDESIGNNO.Text.Trim, cmbcolor.Text.Trim, cmbunit.Text.Trim, TXTLOTNO.Text.Trim, TXTBALENO.Text.Trim, TXTREMARKS.Text.Trim, Val(txtMtrs.Text.Trim), Val(txtpcs.Text.Trim), Val(txtcut.Text.Trim), CMBRACK.Text.Trim, TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, TXTBILLNO.Text.Trim, cmbname.Text.Trim, CMBSHELF.Text.Trim, AccFrom.Date)
                    Else
                        If Val(TXTTO.Text.Trim) > 0 And Val(TXTFROM.Text.Trim) > 0 Then
                            If (Val(TXTTO.Text.Trim) < Val(TXTFROM.Text.Trim)) Or (Val(TXTFROM.Text.Trim) > gridstock.RowCount) Or (Val(TXTTO.Text.Trim) > gridstock.RowCount) Then
                                MsgBox("Invalid No Entered", MsgBoxStyle.Critical)
                                TXTFROM.Focus()
                                Exit Sub
                            End If
                            Dim TEMPMSG As Integer = MsgBox("Wish to Print Bar Code?", MsgBoxStyle.YesNo)
                            If TEMPMSG = vbNo Then Exit Sub

                            For i As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)

                                'IF barcode is used the BARCODE printING WILL BE BLOCKED
                                If Convert.ToBoolean(gridstock.Item(gdone.Index, i - 1).Value) = False Then
                                    BARCODEPRINTING(gridstock.Item(gBarcode.Index, i - 1).Value, gridstock.Item(gpiecetype.Index, i - 1).Value, gridstock.Item(GMERCHANT.Index, i - 1).Value, gridstock.Item(gQuality.Index, i - 1).Value, gridstock.Item(GDESIGN.Index, i - 1).Value, gridstock.Item(gcolor.Index, i - 1).Value, gridstock.Item(Gunit.Index, i - 1).Value, gridstock.Item(GLOTNO.Index, i - 1).Value, gridstock.Item(GBALENO.Index, i - 1).Value, gridstock.Item(GREMARKS.Index, i - 1).Value, Val(gridstock.Item(gMtrs.Index, i - 1).Value), Val(gridstock.Item(Gpcs.Index, i - 1).Value), Val(gridstock.Item(GCUT.Index, i - 1).Value), gridstock.Item(GRACK.Index, i - 1).Value, TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, gridstock.Item(GBILLNO.Index, i - 1).Value, gridstock.Item(gname.Index, i - 1).Value, gridstock.Item(GSHELF.Index, i - 1).Value, AccFrom.Date)
                                End If

                            Next
                            TXTFROM.Clear()
                            TXTTO.Clear()
                        End If
                    End If
                End If
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtpcs_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtpcs.Validating
        Try
            If Val(txtcut.Text.Trim) > 0 Then txtMtrs.Text = Format(Val(txtcut.Text.Trim) * Val(txtpcs.Text.Trim), "0.00")
            If ClientName = "DJIMPEX" Or ClientName = "CHINTAN" Then TXTYARDS.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress, txtMtrs.KeyPress, txtcut.KeyPress, TXTRATE.KeyPress, TXTYARDS.KeyPress, TXTADDLESS.KeyPress, TXTNETTRATE.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtpcs_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpcs.KeyPress, TXTBILLNO.KeyPress, TXTFROM.KeyPress, TXTTO.KeyPress
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtcut_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtcut.Validating
        If Val(txtcut.Text.Trim) > 0 Then txtMtrs.Text = Format(Val(txtcut.Text.Trim) * Val(txtpcs.Text.Trim), "0.00")
    End Sub

    Private Sub gridstock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridstock.KeyDown
        Try
            If e.KeyCode = Keys.Delete And gridstock.RowCount > 0 Then

                If ISLOCKYEAR = True Then
                    MsgBox("Unable to Make changes, Year is Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                If gridstock.Rows(gridstock.CurrentRow.Index).DefaultCellStyle.BackColor = Color.Yellow Then
                    MessageBox.Show("Row Locked, You Cannot Delete This Row")
                    Exit Sub
                End If

                If Val(gridstock.Rows(gridstock.CurrentRow.Index).Cells(goutmtrs.Index).Value) > 0 Then
                    MessageBox.Show("Row Locked, You Cannot Delete This Row")
                    Exit Sub
                End If

                Dim TEMPMSG As Integer = MsgBox("Wish To Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                'DELETE FROM STOCKMASTER
                Dim OBJSM As New ClsStockMaster
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(gridstock.Rows(gridstock.CurrentRow.Index).Cells(GNO.Index).Value)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)

                OBJSM.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJSM.DELETE()

                gridstock.Rows.RemoveAt(gridstock.CurrentRow.Index)
                getsrno(gridstock)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTTO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTTO.Validating
        Try
            PRINTBARCODE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbpiecetype_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbpiecetype.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJPieceType As New SelectPieceType
                OBJPieceType.ShowDialog()
                If OBJPieceType.TEMPNAME <> "" Then cmbpiecetype.Text = OBJPieceType.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbmerchant_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbmerchant.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "MERCHANT"
                OBJItem.STRSEARCH = " and ITEM_cmpid = " & CmpId & " and ITEM_LOCATIONid = " & Locationid & " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then cmbmerchant.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbquality_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbquality.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJQUALITY As New SelectQuality
                OBJQUALITY.ShowDialog()
                If OBJQUALITY.TEMPNAME <> "" Then cmbquality.Text = OBJQUALITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNNO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDESIGNNO.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJDESIGN As New SelectDesign
                OBJDESIGN.FRMSTRING = "DESIGN"
                OBJDESIGN.ShowDialog()
                If OBJDESIGN.TEMPNAME <> "" Then CMBDESIGNNO.Text = OBJDESIGN.TEMPNAME
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
                OBJCOLOR.FRMSTRING = "COLOR"
                OBJCOLOR.ShowDialog()
                If OBJCOLOR.TEMPNAME <> "" Then cmbcolor.Text = OBJCOLOR.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtoname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtoname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' and LEDGERS.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.acc_YEARid = " & YearId
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbgodown_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbgodown.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJGODOWN As New SelectGodown
                OBJGODOWN.FRMSTRING = "GODOWN"
                OBJGODOWN.ShowDialog()
                If OBJGODOWN.TEMPNAME <> "" Then cmbgodown.Text = OBJGODOWN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTREMARKS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTREMARKS.Validating
        If cmbpiecetype.Text.Trim <> "" And cmbmerchant.Text.Trim <> "" And Val(txtpcs.Text.Trim) > 0 Then
            If ClientName <> "AXIS" And ClientName <> "GELATO" And Val(txtMtrs.Text.Trim) = 0 Then Exit Sub
            If cmbtype.Text.Trim = "INHOUSE" Then
                If cmbgodown.Text.Trim <> "" Then
                    Dim TEMPQTY As Integer = Val(txtpcs.Text.Trim)
                    If ClientName = "CHINTAN" Then TEMPQTY = Val(TXTYARDS.Text.Trim)

                    If ClientName = "AVIS" Or ClientName = "MARKIN" Or (ClientName = "AXIS" And Val(txtMtrs.Text.Trim) > 0) Then
                        If Val(txtcut.Text.Trim) > 0 Then txtMtrs.Text = Val(txtcut.Text.Trim)
                        TXTBARCODE.Text = ""
                        SAVE()
                    ElseIf ClientName = "AXIS" And Val(txtMtrs.Text.Trim) = 0 Then
                        txtpcs.Text = 1
                        If Val(txtcut.Text.Trim) > 0 Then txtMtrs.Text = Val(txtcut.Text.Trim)
                        For I As Integer = 1 To Val(TEMPQTY)
                            TXTBARCODE.Text = ""
                            SAVE()
                        Next
                    Else
                        If ALLOWBARCODEPRINT = True Then
                            If (ClientName = "SSC" Or ClientName = "VINIT" Or ClientName = "RUCHITA") And Val(txtcut.Text.Trim) = 0 Then
                                TEMPQTY = 1
                            Else
                                If ClientName <> "CHINTAN" Then txtpcs.Text = 1
                            End If
                            If Val(txtcut.Text.Trim) > 0 Then txtMtrs.Text = Val(txtcut.Text.Trim)

                            For I As Integer = 1 To Val(TEMPQTY)
                                TXTBARCODE.Text = ""
                                SAVE()
                            Next
                        Else
                            SAVE()
                        End If
                    End If
                    total()
                    CLEAR = True
                    If ALLOWBARCODEPRINT = True And Val(TEMPQTY) > 1 Then
                        Dim EARGS As System.EventArgs
                        Call OpeningStock_Load(sender, EARGS)
                        TXTLOTNO.Focus()
                    Else
                        fillgrid()
                    End If
                Else
                    MsgBox("Enter Proper Details")
                    Exit Sub
                End If
            ElseIf cmbtype.Text = "JOBBERSTOCK" Then
                If cmbtoname.Text.Trim <> "" Then
                    cmbgodown.Text = ""

                    'CHECK WHETHER BILLNO IS ALREADY PRESENT IN GRID OR NOT
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable
                    'DT = OBJCMN.search("SM_BILLNO AS BILLNO", "", " STOCKMASTER ", " AND SM_BILLNO = " & Val(TXTBILLNO.Text.Trim) & " AND SM_NO <> " & Val(TXTNO.Text.Trim) & " AND SM_YEARID = " & YearId)
                    'If DT.Rows.Count > 0 Then
                    '    MsgBox("Bill no Already Present", MsgBoxStyle.Critical)
                    '    TXTBILLNO.Focus()
                    '    Exit Sub
                    'End If

                    Dim TEMPPCS As Integer = Val(txtpcs.Text.Trim)
                    Dim TEMPMTRS As Double = Val(txtMtrs.Text.Trim)
                    If ClientName = "MABHAY" Or ClientName = "SVS" Then
                        'If ClientName <> "MSANCHITKUMAR" And ClientName <> "CC" And ClientName <> "BARKHA" Then txtpcs.Text = 1
                        If ALLOWBARCODEPRINT = True Then txtpcs.Text = 1
                        If Val(txtcut.Text.Trim) > 0 Then txtMtrs.Text = Val(txtcut.Text.Trim)
                        If Val(TEMPPCS) > 1 Then
                            txtMtrs.Text = Format(Val(txtMtrs.Text.Trim) / Val(TEMPPCS), "0.00")
                            CLEAR = False
                        End If
                        For I As Integer = 1 To Val(TEMPPCS)
                            SAVE()
                            If I = TEMPPCS Then CLEAR = True
                            fillgrid()
                        Next
                    Else
                        SAVE()
                        CLEAR = True
                        fillgrid()
                    End If
                Else
                    If cmbpiecetype.Text = "" Then
                        MsgBox("Enter Piecetype Details")
                        cmbpiecetype.Focus()
                    ElseIf cmbtoname.Text = "" Then
                        MsgBox("Enter Jobber Name")
                        cmbtoname.Focus()
                    ElseIf TXTLOTNO.Text = "" Then
                        MsgBox("Enter Lot No.")
                        TXTLOTNO.Focus()
                    ElseIf cmbmerchant.Text = "" Then
                        MsgBox("Enter Item Details")
                        cmbmerchant.Focus()
                    ElseIf Val(txtpcs.Text.Trim) <= 0 Then
                        MsgBox("Enter Pcs")
                        txtpcs.Focus()
                    ElseIf Val(txtMtrs.Text.Trim) <= 0 Then
                        MsgBox("Enter Mtrs")
                        txtMtrs.Focus()
                    End If
                    Exit Sub
                End If
            End If
        Else
            If cmbpiecetype.Text = "" Then
                MsgBox("Enter Piecetype Details")
                cmbpiecetype.Focus()
            ElseIf cmbmerchant.Text = "" Then
                MsgBox("Enter Item Details")
                cmbmerchant.Focus()
            ElseIf cmbquality.Text = "" Then
                MsgBox("Enter Quality Details")
                cmbquality.Focus()
            ElseIf CMBDESIGNNO.Text = "" Then
                MsgBox("Enter Design Details")
                CMBDESIGNNO.Focus()
            ElseIf Val(txtpcs.Text.Trim) <= 0 Then
                MsgBox("Enter Pcs")
                txtpcs.Focus()
            ElseIf Val(txtMtrs.Text.Trim) <= 0 Then
                MsgBox("Enter Mtrs")
                txtMtrs.Focus()
            End If
            Exit Sub
        End If
    End Sub

    Sub total()
        Try
            If ClientName <> "CC" And ClientName <> "C3" And ClientName <> "SHREEDEV" Then
                If gridstock.RowCount > 0 Then
                    For Each row As DataGridViewRow In gridstock.Rows
                        If row.Cells(GPer.Index).EditedFormattedValue = "Mtrs" Then
                            row.Cells(GAMOUNT.Index).Value = (row.Cells(gMtrs.Index).EditedFormattedValue * row.Cells(GRATE.Index).EditedFormattedValue)
                        ElseIf row.Cells(GPer.Index).EditedFormattedValue = "Qty" Then
                            row.Cells(GAMOUNT.Index).Value = (row.Cells(Gpcs.Index).EditedFormattedValue * row.Cells(GRATE.Index).EditedFormattedValue)
                        End If
                        'row.Cells(GAMOUNT.Index).Value = (row.Cells(gMtrs.Index).EditedFormattedValue * row.Cells(GRATE.Index).EditedFormattedValue)
                    Next
                End If
                'TXTAMOUNT.Text = Val(txtMtrs.Text.Trim) * Val(TXTRATE.Text.Trim)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtMtrs_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMtrs.Validated
        total()
    End Sub

    Private Sub TXTRATE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTRATE.Validated
        total()
    End Sub

    Private Sub TXTRATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTRATE.Validating
        'If ClientName <> "CC" Then TXTAMOUNT.Text = Val(txtMtrs.Text) * Val(TXTRATE.Text)
        total()
    End Sub

    Private Sub OpeningStock_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                TXTBARCODE.Visible = True
                LBLBARCODE.Visible = True
                GRATE.HeaderText = "Pur Rate"
                GAMOUNT.HeaderText = "Sale Rate"
                TXTAMOUNT.ReadOnly = False
                TXTAMOUNT.BackColor = Color.White
            End If
            If ClientName = "SPCORP" Then CMBDYEINGJOB.Enabled = True

            If ClientName = "SBA" Or ClientName = "SSC" Or ClientName = "REALCORPORATION" Then
                TXTLOTNO.TabStop = False
                cmbname.TabStop = False
                cmbtoname.TabStop = False
                TXTBILLNO.TabStop = False
                If ClientName <> "SSC" Then txtcut.TabStop = False
                TXTWT.TabStop = False
                CMBPER.TabStop = False
                If ClientName <> "REALCORPORATION" Then CMBRACK.TabStop = False
                CMBSHELF.TabStop = False
                TXTRATE.TabStop = False
                TXTAMOUNT.TabStop = False
            End If

            If (ClientName = "DJIMPEX" Or ClientName = "CHINTAN") And cmbtype.Text.Trim = "INHOUSE" Then TXTYARDS.Visible = True

            If ClientName = "SONU" Then cmbunit.Text = "ROLL"
            If ClientName = "PURPLE" Then CHKPRINT.CheckState = CheckState.Checked
            If ClientName = "PARAS" And UserName <> "Admin" Then Exit Sub

            If ClientName = "MAHAVIRPOLYCOT" And cmbtype.Text.Trim <> "INHOUSE" Then
                TXTADDLESS.TabStop = True
                TXTNETTRATE.TabStop = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTBARCODE.Validated
        Try
            If (ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV") And TXTBARCODE.Text.Trim <> "" Then
                'FETCH ENTRY FROM LAST YEAR TO ENTER SAME IN OPENINGSTOCK, DO NOT PUT YEARID CLAUSE
                'DONE BY GULKIT DO NOT CHANGE
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("*", "", "BARCODESTOCK", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "'")
                If DT.Rows.Count > 0 Then

                    'CHECK WHETHER SAME BARCODE IS ENTERED OR NOT
                    Dim DTCHECK As DataTable = OBJCMN.search("*", "", "BARCODESTOCK", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "' AND YEARID = " & YearId)
                    If DTCHECK.Rows.Count > 0 Then
                        MsgBox("Barcode Already Present", MsgBoxStyle.Critical)
                        TXTBARCODE.Clear()
                        TXTBARCODE.Focus()
                        Exit Sub
                    End If

                    txtsrno.Text = gridstock.RowCount + 1
                    cmbpiecetype.Text = DT.Rows(0).Item("PIECETYPE")
                    cmbmerchant.Text = DT.Rows(0).Item("ITEMNAME")
                    cmbquality.Text = DT.Rows(0).Item("QUALITY")
                    CMBDESIGNNO.Text = DT.Rows(0).Item("DESIGNNO")
                    cmbcolor.Text = DT.Rows(0).Item("COLOR")
                    cmbtoname.Text = DT.Rows(0).Item("JOBBERNAME")
                    TXTBILLNO.Text = DT.Rows(0).Item("CHALLANNO")
                    cmbgodown.Text = DT.Rows(0).Item("GODOWN")
                    txtcut.Text = Val(DT.Rows(0).Item("CUT"))
                    cmbunit.Text = DT.Rows(0).Item("UNIT")
                    txtpcs.Text = Val(DT.Rows(0).Item("PCS"))
                    txtMtrs.Text = Val(DT.Rows(0).Item("MTRS"))
                    TXTRATE.Text = Val(DT.Rows(0).Item("PURRATE"))
                    TXTAMOUNT.Text = Val(DT.Rows(0).Item("SALERATE"))

                    total()
                    SAVE()
                    CLEAR = True
                    fillgrid()
                    TXTBARCODE.Focus()
                Else
                    MsgBox("Invaid Barcode", MsgBoxStyle.Critical)
                    TXTBARCODE.Clear()
                    Exit Sub
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

    Private Sub CMBDESIGNNO_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGNNO.Validated
        Try
            If CMBDESIGNNO.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" ISNULL(DESIGN_PURRATE,0) AS PURRATE, ISNULL(DESIGN_SALERATE,0) AS SALERATE, ISNULL(DESIGN_WRATE,0) AS WRATE, ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGN_ITEMID = ITEM_ID ", " AND DESIGN_NO = '" & CMBDESIGNNO.Text.Trim & "' AND DESIGN_YEARID =  " & YearId)
                TXTRATE.Text = Val(DT.Rows(0).Item("PURRATE"))
                TXTAMOUNT.Text = Val(DT.Rows(0).Item("SALERATE"))
                If (ClientName = "AVIS" Or ClientName = "KRISHNA" Or ClientName = "NTC") Then cmbmerchant.Text = DT.Rows(0).Item("ITEMNAME")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtMtrs_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMtrs.Validating
        Try
            If ClientName = "MANIBHADRA" Then TXTREMARKS_Validating(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSEARCHBARCODE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTSEARCHBARCODE.Validated
        For Each ROW As DataGridViewRow In gridstock.Rows
            If LCase(ROW.Cells(gBarcode.Index).Value) = LCase(TXTSEARCHBARCODE.Text.Trim) Then
                gridstock.FirstDisplayedScrollingRowIndex = ROW.Index
                gridstock.Rows(ROW.Index).Selected = True
                TXTSEARCHBARCODE.Clear()
                Exit Sub
            End If
        Next
    End Sub

    Private Sub TXTYARDS_Validated(sender As Object, e As EventArgs) Handles TXTYARDS.Validated
        Try
            If Val(TXTYARDS.Text.Trim) > 0 And ClientName <> "CHINTAN" Then txtMtrs.Text = Format(Val(TXTYARDS.Text.Trim) * 0.914, "0.00")
            txtMtrs.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub gridstock_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles gridstock.DataError
    '    If (e.Context = (DataGridViewDataErrorContexts.Formatting Or DataGridViewDataErrorContexts.PreferredSize)) Then
    '        e.ThrowException = False
    '    End If
    'End Sub
End Class