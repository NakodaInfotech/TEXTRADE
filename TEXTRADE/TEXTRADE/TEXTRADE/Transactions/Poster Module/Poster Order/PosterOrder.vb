
Imports BL
Imports System.ComponentModel
Imports System.IO

Public Class PosterOrder

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer

    Public TEMPPONO As String
    Public EDIT As Boolean

    Private Sub PosterOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'POSTER ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            fillcmb()
            clear()
            CMBNAME.Enabled = True

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objclsSO As New ClsPosterOrder()
                Dim DT As DataTable = objclsSO.selectSo(TEMPPONO, CmpId, Locationid, YearId)
                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows
                        TXTSRNO.Text = TEMPPONO
                        'TEMPPONO.ReadOnly = True
                        DTDATE.Text = Format(Convert.ToDateTime(DR("DATE")), "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(DR("NAME"))
                        TXTDELIVERYAT.Text = Convert.ToString(DR("DELIVERYAT"))
                        CMBPARTYNAME.Text = Convert.ToString(DR("PARTYNAME"))
                        TXTCOL1.Text = DR("COL1")
                        TXTCOL2.Text = DR("COL2")
                        TXTCOL3.Text = DR("COL3")
                        TXTCOL4.Text = DR("COL4")
                        TXTCOL5.Text = DR("COL5")
                        LBLTOTAL12X18.Text = Val(DR("TOTAL12X18"))
                        LBLTOTAL12X24.Text = Val(DR("TOTAL12X24"))
                        LBLTOTAL8X10.Text = Val(DR("TOTAL8X10"))
                        LBLTOTAL6X8.Text = Val(DR("TOTAL6X8"))
                        LBLTOTAL4X6.Text = Val(DR("TOTAL4X6"))
                        TXTREMARKS.Text = DR("REMARKS")
                        TXTPRINTTYPE.Text = DR("PRINTTYPE")


                        GRIDPOSTER.Rows.Add(DR("GRIDSRNO").ToString, DR("ITEMNAME"), DR("DESIGN"), DR("COLOR"), DR("DESC").ToString, Val(DR("D12X18")), Val(DR("D12X24")), Val(DR("D8X10")), Val(DR("D6X8")), Val(DR("D4X6")), Val(DR("COLUMN1")), Val(DR("COLUMN2")), Val(DR("COLUMN3")), Val(DR("COLUMN4")), Val(DR("COLUMN5")), DR("DONE"), DR("CLOSED"))
                        If Convert.ToBoolean(DR("DONE")) = True Or Convert.ToBoolean(DR("CLOSED")) = True Then
                            If Convert.ToBoolean(DR("CLOSED")) = True Then LBLCLOSED.Visible = True
                            GRIDPOSTER.Rows(GRIDPOSTER.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                            LBLLOCKED.Visible = True
                            PBLOCK.Visible = True
                        End If

                    Next
                    GRIDPOSTER.FirstDisplayedScrollingRowIndex = GRIDPOSTER.RowCount - 1
                Else
                    EDIT = False
                    clear()

                End If
                TOTAL()

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'")
            If CMBPARTYNAME.Text.Trim = "" Then FILLNAME(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
            FILLCOLOR(CMBCOLOR, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim IntResult As Integer

            'CALL TOTAL HERE, DONE BY GULKIT
            TOTAL()

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            If TXTSRNO.ReadOnly = True Then
                alParaval.Add(0)
            Else
                alParaval.Add(Val(TXTSRNO.Text.Trim))
            End If

            alParaval.Add(Format(Convert.ToDateTime(DTDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(TXTDELIVERYAT.Text.Trim)
            alParaval.Add(CMBPARTYNAME.Text.Trim)
            alParaval.Add(TXTCOL1.Text.Trim)
            alParaval.Add(TXTCOL2.Text.Trim)
            alParaval.Add(TXTCOL3.Text.Trim)
            alParaval.Add(TXTCOL4.Text.Trim)
            alParaval.Add(TXTCOL5.Text.Trim)
            alParaval.Add(LBLTOTAL12X18.Text.Trim)
            alParaval.Add(LBLTOTAL12X24.Text.Trim)
            alParaval.Add(LBLTOTAL8X10.Text.Trim)
            alParaval.Add(LBLTOTAL6X8.Text.Trim)
            alParaval.Add(LBLTOTAL4X6.Text.Trim)
            alParaval.Add(LBLTOTALCOLUMN1.Text.Trim)
            alParaval.Add(LBLTOTALCOLUMN2.Text.Trim)
            alParaval.Add(LBLTOTALCOLUMN3.Text.Trim)
            alParaval.Add(LBLTOTALCOLUMN4.Text.Trim)
            alParaval.Add(LBLTOTALCOLUMN5.Text.Trim)
            alParaval.Add(TXTREMARKS.Text.Trim)
            alParaval.Add(TXTPRINTTYPE.Text.Trim)


            alParaval.Add(CmpId)
            alParaval.Add(YearId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(0)

            Dim GRIDSRNO As String = ""
            Dim ITEMNAME As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim DESC As String = ""
            Dim D12X18 As String = ""
            Dim D12X24 As String = ""
            Dim D8X10 As String = ""
            Dim D6X8 As String = ""
            Dim D4X6 As String = ""
            Dim COLUMN1 As String = ""
            Dim COLUMN2 As String = ""
            Dim COLUMN3 As String = ""
            Dim COLUMN4 As String = ""
            Dim COLUMN5 As String = ""
            Dim DONE As String = ""
            Dim CLOSED As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDPOSTER.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = row.Cells(GSRNO.Index).Value.ToString
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(GCOLOR.Index).Value.ToString
                        DESC = row.Cells(GDESC.Index).Value.ToString
                        D12X18 = Val(row.Cells(G12X18.Index).Value)
                        D12X24 = Val(row.Cells(G12X24.Index).Value)
                        D8X10 = Val(row.Cells(G8X10.Index).Value)
                        D6X8 = Val(row.Cells(G6X8.Index).Value)
                        D4X6 = Val(row.Cells(G4X6.Index).Value)
                        COLUMN1 = Val(row.Cells(GCOLUMN1.Index).Value)
                        COLUMN2 = Val(row.Cells(GCOLUMN2.Index).Value)
                        COLUMN3 = Val(row.Cells(GCOLUMN3.Index).Value)
                        COLUMN4 = Val(row.Cells(GCOLUMN4.Index).Value)
                        COLUMN5 = Val(row.Cells(GCOLUMN5.Index).Value)
                        If row.Cells(GDONE.Index).Value = True Then DONE = 1 Else DONE = 0
                        If row.Cells(GCLOSED.Index).Value = True Then CLOSED = 1 Else CLOSED = 0


                    Else
                        GRIDSRNO = GRIDSRNO & "|" & row.Cells(GSRNO.Index).Value.ToString
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GCOLOR.Index).Value.ToString
                        DESC = DESC & "|" & row.Cells(GDESC.Index).Value.ToString
                        D12X18 = D12X18 & "|" & Val(row.Cells(G12X18.Index).Value)
                        D12X24 = D12X24 & "|" & Val(row.Cells(G12X24.Index).Value)
                        D8X10 = D8X10 & "|" & Val(row.Cells(G8X10.Index).Value)
                        D6X8 = D6X8 & "|" & Val(row.Cells(G6X8.Index).Value)
                        D4X6 = D4X6 & "|" & Val(row.Cells(G4X6.Index).Value)
                        COLUMN1 = COLUMN1 & "|" & Val(row.Cells(GCOLUMN1.Index).Value)
                        COLUMN2 = COLUMN2 & "|" & Val(row.Cells(GCOLUMN2.Index).Value)
                        COLUMN3 = COLUMN3 & "|" & Val(row.Cells(GCOLUMN3.Index).Value)
                        COLUMN4 = COLUMN4 & "|" & Val(row.Cells(GCOLUMN4.Index).Value)
                        COLUMN5 = COLUMN5 & "|" & Val(row.Cells(GCOLUMN5.Index).Value)
                        If row.Cells(GDONE.Index).Value = True Then DONE = DONE & "|" & "1" Else DONE = DONE & "|" & "0"
                        If row.Cells(GCLOSED.Index).Value = True Then CLOSED = CLOSED & "|" & "1" Else CLOSED = CLOSED & "|" & "0"

                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(ITEMNAME)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(DESC)
            alParaval.Add(D12X18)
            alParaval.Add(D12X24)
            alParaval.Add(D8X10)
            alParaval.Add(D6X8)
            alParaval.Add(D4X6)
            alParaval.Add(COLUMN1)
            alParaval.Add(COLUMN2)
            alParaval.Add(COLUMN3)
            alParaval.Add(COLUMN4)
            alParaval.Add(COLUMN5)
            alParaval.Add(DONE)
            alParaval.Add(CLOSED)


            Dim objclsPurord As New ClsPosterOrder()
            objclsPurord.alParaval = alParaval


            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DT As DataTable = objclsPurord.SAVE()
                MessageBox.Show("Details Added")
                TXTSRNO.Text = DT.Rows(0).Item(0)
            Else
                alParaval.Add(TEMPPONO)
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                IntResult = objclsPurord.UPDATE()
                MessageBox.Show("Details Updated")
            End If

            'If MsgBox("Wish to Print Order?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then PRINTREPORT()


            EDIT = False
            clear()
            CMBNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean

        Dim bln As Boolean = True
        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, " Please Fill  Name ")
            bln = False
        End If




        Dim OBJCMN As New ClsCommon
        Dim DTTABLE As New DataTable




        'we have OPENED THE LOCK
        'If lbllocked.Visible = True And LBLCLOSED.Visible = False Then
        '    EP.SetError(lbllocked, "Unable to Update, SO Locked")
        '    bln = False
        'ElseIf lbllocked.Visible = True And LBLCLOSED.Visible = True Then
        '    EP.SetError(LBLCLOSED, "Unable to Update, SO Closed")
        '    bln = False
        'End If

        If DTDATE.Text = "__/__/____" Then
            EP.SetError(DTDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTDATE.Text) Then
                EP.SetError(DTDATE, "Date not in Accounting Year")
                bln = False
            End If
            If Convert.ToDateTime(DTDATE.Text).Date < SOBLOCKDATE.Date Then
                EP.SetError(DTDATE, "Date is Blocked, Please make entries after " & Format(SOBLOCKDATE.Date, "dd/MM/yyyy"))
                bln = False
            End If



            If TXTSRNO.Text.Trim = "" Then
                EP.SetError(TXTSRNO, "Enter Party SR No")
                bln = False
            End If

            If GRIDPOSTER.RowCount = 0 Then
                EP.SetError(CMDOK, "Enter Details of Poster Order")
                bln = False
            End If

        End If


LINE1:
        For Each ROW As DataGridViewRow In GRIDPOSTER.Rows
            If Val(ROW.Cells(G12X18.Index).Value) = 0 And Val(ROW.Cells(G12X24.Index).Value) = 0 And Val(ROW.Cells(G8X10.Index).Value) = 0 And Val(ROW.Cells(G6X8.Index).Value) = 0 And Val(ROW.Cells(G4X6.Index).Value) = 0 And Val(ROW.Cells(GCOLUMN1.Index).Value) = 0 And Val(ROW.Cells(GCOLUMN2.Index).Value) = 0 And Val(ROW.Cells(GCOLUMN3.Index).Value) = 0 And Val(ROW.Cells(GCOLUMN4.Index).Value) = 0 And Val(ROW.Cells(GCOLUMN5.Index).Value) = 0 Then
                GRIDPOSTER.Rows.RemoveAt(ROW.Index)
                GoTo LINE1
            End If
        Next


        If Val(TXTSRNO.Text.Trim) > 0 And EDIT = False And TXTSRNO.ReadOnly = False Then
            DTTABLE = OBJCMN.SEARCH("  ISNULL(POSTER_no, 0) AS SONO ", "", " POSTERORDER ", "  AND POSTERORDER.POSTER_no=" & TXTSRNO.Text.Trim & " AND POSTERORDER.POSTER_CMPID = " & CmpId & " AND POSTERORDER.POSTER_LOCATIONID = " & Locationid & " AND POSTERORDER.POSTER_YEARID = " & YearId)
            If DTTABLE.Rows.Count > 0 Then
                EP.SetError(TXTSRNO, "Poster Order No Already Exist")
                bln = False
            End If
        End If



        Return bln
    End Function

    Sub TOTAL()
        Try
            LBLTOTAL12X18.Text = 0.00
            LBLTOTAL12X24.Text = 0.00
            LBLTOTAL8X10.Text = 0.00
            LBLTOTAL6X8.Text = 0.00
            LBLTOTAL4X6.Text = 0.00
            LBLTOTALCOLUMN1.Text = 0.00
            LBLTOTALCOLUMN2.Text = 0.00
            LBLTOTALCOLUMN3.Text = 0.00
            LBLTOTALCOLUMN4.Text = 0.00
            LBLTOTALCOLUMN5.Text = 0.00

            If GRIDPOSTER.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDPOSTER.Rows
                    LBLTOTAL12X18.Text = Format(Val(LBLTOTAL12X18.Text) + Val(row.Cells(G12X18.Index).EditedFormattedValue), "0.00")
                    LBLTOTAL12X24.Text = Format(Val(LBLTOTAL12X24.Text) + Val(row.Cells(G12X24.Index).EditedFormattedValue), "0.00")
                    LBLTOTAL8X10.Text = Format(Val(LBLTOTAL8X10.Text) + Val(row.Cells(G8X10.Index).EditedFormattedValue), "0.00")
                    LBLTOTAL6X8.Text = Format(Val(LBLTOTAL6X8.Text) + Val(row.Cells(G6X8.Index).EditedFormattedValue), "0.00")
                    LBLTOTAL4X6.Text = Format(Val(LBLTOTAL4X6.Text) + Val(row.Cells(G4X6.Index).EditedFormattedValue), "0.00")
                    LBLTOTALCOLUMN1.Text = Format(Val(LBLTOTALCOLUMN1.Text) + Val(row.Cells(GCOLUMN1.Index).EditedFormattedValue), "0.00")
                    LBLTOTALCOLUMN2.Text = Format(Val(LBLTOTALCOLUMN2.Text) + Val(row.Cells(GCOLUMN2.Index).EditedFormattedValue), "0.00")
                    LBLTOTALCOLUMN3.Text = Format(Val(LBLTOTALCOLUMN3.Text) + Val(row.Cells(GCOLUMN3.Index).EditedFormattedValue), "0.00")
                    LBLTOTALCOLUMN4.Text = Format(Val(LBLTOTALCOLUMN4.Text) + Val(row.Cells(GCOLUMN4.Index).EditedFormattedValue), "0.00")
                    LBLTOTALCOLUMN5.Text = Format(Val(LBLTOTALCOLUMN5.Text) + Val(row.Cells(GCOLUMN5.Index).EditedFormattedValue), "0.00")
                Next

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Sub CLEAR()
        DTDATE.Text = Now.Date
        CMBNAME.Text = ""
        If ClientName = "SOFTAS" Then TXTDELIVERYAT.Text = "BHIWANDI" Else TXTDELIVERYAT.Clear()
        TSTXTBILLNO.Clear()
        TXTSONO.Clear()
        CMBPARTYNAME.Text = ""
        TXTCOL1.Clear()
        TXTCOL2.Clear()
        TXTCOL3.Clear()
        TXTCOL4.Clear()
        TXTCOL5.Clear()
        LBLTOTAL12X18.Text = ""
        LBLTOTAL12X24.Text = ""
        LBLTOTAL8X10.Text = ""
        LBLTOTAL6X8.Text = ""
        LBLTOTAL4X6.Text = ""
        LBLTOTALCOLUMN1.Text = ""
        LBLTOTALCOLUMN2.Text = ""
        LBLTOTALCOLUMN3.Text = ""
        LBLTOTALCOLUMN4.Text = ""
        LBLTOTALCOLUMN5.Text = ""
        TXTREMARKS.Clear()
        TXTPRINTTYPE.Text = ""
        getmax_PO_no()

        TXTGRIDSRNO.Clear()
        CMBITEMNAME.Text = ""
        CMBDESIGN.Text = ""
        CMBCOLOR.Text = ""
        TXT12X18.Clear()
        TXT12X24.Clear()
        TXT8X10.Clear()
        TXT6X8.Clear()
        TXT4X6.Clear()
        TXTCOLUMN1.Clear()
        TXTCOLUMN2.Clear()
        TXTCOLUMN3.Clear()
        TXTCOLUMN4.Clear()
        TXTCOLUMN5.Clear()
        TXTDESC.Clear()
        GRIDPOSTER.RowCount = 0



    End Sub

    Sub FILLGRID()

        GRIDPOSTER.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDPOSTER.Rows.Add(Val(TXTSRNO.Text.Trim), CMBITEMNAME.Text.Trim, CMBDESIGN.Text.Trim, CMBCOLOR.Text.Trim, TXTDESC.Text.Trim, Val(TXT12X18.Text.Trim), Val(TXT12X24.Text.Trim), Val(TXT8X10.Text.Trim), Val(TXT6X8.Text.Trim), Val(TXT4X6.Text.Trim), Val(TXTCOLUMN1.Text.Trim), Val(TXTCOLUMN2.Text.Trim), Val(TXTCOLUMN3.Text.Trim), Val(TXTCOLUMN4.Text.Trim), Val(TXTCOLUMN5.Text.Trim), 0, 0)
            getsrno(GRIDPOSTER)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDPOSTER.Item(GSRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDPOSTER.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
            GRIDPOSTER.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDPOSTER.Item(GCOLOR.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
            GRIDPOSTER.Item(GDESC.Index, TEMPROW).Value = TXTDESC.Text.Trim

            GRIDPOSTER.Item(G12X18.Index, TEMPROW).Value = Val(TXT12X18.Text.Trim)
            GRIDPOSTER.Item(G12X24.Index, TEMPROW).Value = Val(TXT12X24.Text.Trim)
            GRIDPOSTER.Item(G8X10.Index, TEMPROW).Value = Val(TXT8X10.Text.Trim)
            GRIDPOSTER.Item(G6X8.Index, TEMPROW).Value = Val(TXT6X8.Text.Trim)
            GRIDPOSTER.Item(G4X6.Index, TEMPROW).Value = Val(TXT4X6.Text.Trim)
            GRIDPOSTER.Item(GCOLUMN1.Index, TEMPROW).Value = Val(TXTCOLUMN1.Text.Trim)
            GRIDPOSTER.Item(GCOLUMN2.Index, TEMPROW).Value = Val(TXTCOLUMN2.Text.Trim)
            GRIDPOSTER.Item(GCOLUMN3.Index, TEMPROW).Value = Val(TXTCOLUMN3.Text.Trim)
            GRIDPOSTER.Item(GCOLUMN4.Index, TEMPROW).Value = Val(TXTCOLUMN4.Text.Trim)
            GRIDPOSTER.Item(GCOLUMN5.Index, TEMPROW).Value = Val(TXTCOLUMN5.Text.Trim)

            GRIDDOUBLECLICK = False
        End If
        TOTAL()
        GRIDPOSTER.FirstDisplayedScrollingRowIndex = GRIDPOSTER.RowCount - 1

        TXTSRNO.Text = GRIDPOSTER.RowCount + 1
        TXT12X18.Clear()
        TXT12X24.Clear()
        TXT8X10.Clear()
        TXT6X8.Clear()
        TXT4X6.Clear()
        TXTCOLUMN1.Clear()
        TXTCOLUMN2.Clear()
        TXTCOLUMN3.Clear()
        TXTCOLUMN4.Clear()
        TXTCOLUMN5.Clear()
        CMBCOLOR.Text = ""
        CMBDESIGN.Text = ""
        TXTDESC.Clear()
        CMBITEMNAME.Focus()

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

    Private Sub TXTCOLUMN5_Validated(sender As Object, e As EventArgs) Handles TXTCOLUMN5.Validated
        Try
            If CMBITEMNAME.Text.Trim <> "" Then
                FILLGRID()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If ClientName = "AVIS" Then
                If CMBPARTYNAME.Text.Trim = "" Then FILLNAME(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUP_NAME <> 'HASTE DEBTORS'")
            Else
                If CMBPARTYNAME.Text.Trim = "" Then FILLNAME(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBPARTYNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If ClientName = "AVIS" Then
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND GROUP_NAME <> 'HASTE CREDITORS'")
            Else
                If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry CREDITORS'", "Sundry CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(sender As Object, e As EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(sender As Object, e As EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validating(sender As Object, e As CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If ClientName = "AVIS" And CMBDESIGN.Text.Trim <> "" Then CMBITEMNAME.Text = ""
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Enter(sender As Object, e As EventArgs) Handles CMBCOLOR.Enter
        Try
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Validating(sender As Object, e As CancelEventArgs) Handles CMBCOLOR.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub cmbitemname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBITEMNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "MERCHANT"
                OBJItem.STRSEARCH = " and ITEM_cmpid = " & CmpId & " and ITEM_LOCATIONid = " & Locationid & " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then CMBITEMNAME.Text = OBJItem.TEMPNAME
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
    Private Sub cmbcolor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCOLOR.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCOLOR As New SelectShade
                OBJCOLOR.ShowDialog()
                If OBJCOLOR.TEMPNAME <> "" Then CMBCOLOR.Text = OBJCOLOR.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridPOSTER_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDPOSTER.CellDoubleClick
        EDITROW()
    End Sub
    Sub EDITROW()
        Try
            If GRIDPOSTER.CurrentRow.Index >= 0 And GRIDPOSTER.Item(GSRNO.Index, GRIDPOSTER.CurrentRow.Index).Value <> Nothing Then

                'If Convert.ToBoolean(GRIDSO.Rows(GRIDSO.CurrentRow.Index).Cells(GDONE.Index).Value) = True Then 'If row.Cells(16).Value <> "0" Then 
                '    MsgBox("Item Locked. First Delete from POSTERORDER")
                '    Exit Sub
                'End If
                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDPOSTER.Item(GSRNO.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = GRIDPOSTER.Item(GITEMNAME.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDPOSTER.Item(GDESIGN.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                CMBCOLOR.Text = GRIDPOSTER.Item(GCOLOR.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                TXTDESC.Text = GRIDPOSTER.Item(GDESC.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                TXT12X18.Text = GRIDPOSTER.Item(G12X18.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                TXT12X24.Text = GRIDPOSTER.Item(G12X24.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                TXT8X10.Text = GRIDPOSTER.Item(G8X10.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                TXT6X8.Text = GRIDPOSTER.Item(G6X8.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                TXT4X6.Text = GRIDPOSTER.Item(G4X6.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                TXTCOLUMN1.Text = GRIDPOSTER.Item(GCOLUMN1.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                TXTCOLUMN2.Text = GRIDPOSTER.Item(GCOLUMN2.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                TXTCOLUMN3.Text = GRIDPOSTER.Item(GCOLUMN3.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                TXTCOLUMN4.Text = GRIDPOSTER.Item(GCOLUMN4.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString
                TXTCOLUMN5.Text = GRIDPOSTER.Item(GCOLUMN5.Index, GRIDPOSTER.CurrentRow.Index).Value.ToString


                TEMPROW = GRIDPOSTER.CurrentRow.Index
                CMBITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            GRIDPOSTER.RowCount = 0
LINE1:
            TEMPPONO = Val(TXTSRNO.Text) - 1
            If TEMPPONO > 0 Then
                EDIT = True
                PosterOrder_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDPOSTER.RowCount = 0 And TEMPPONO > 1 Then
                TXTSRNO.Text = TEMPPONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDPOSTER.RowCount = 0
LINE1:
            TEMPPONO = Val(TXTSRNO.Text) + 1
            getmax_PO_no()
            Dim MAXNO As Integer = TXTSRNO.Text.Trim
            CLEAR()
            If Val(TXTSRNO.Text) - 1 >= TEMPPONO Then
                EDIT = True
                PosterOrder_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDPOSTER.RowCount = 0 And TEMPPONO < MAXNO Then
                TXTSRNO.Text = TEMPPONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub getmax_PO_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(POSTER_no),0) + 1 ", "POSTERORDER", " AND POSTER_cmpid=" & CmpId & " and POSTER_locationid=" & Locationid & " and POSTER_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            TXTSRNO.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub gridPOSTER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDPOSTER.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDPOSTER.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbMERCHANT.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block


                'DONT ALLOW TO DELETE ANY ROW IF LOCKED IS VISIBLE
                If LBLLOCKED.Visible = True Then
                    MessageBox.Show("Unable to Delete Row, Poster Order is Locked")
                    Exit Sub
                End If


                GRIDPOSTER.Rows.RemoveAt(GRIDPOSTER.CurrentRow.Index)
                TOTAL()
                getsrno(GRIDPOSTER)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objpodtls As New PosterOrderDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            If MsgBox("Wish to Print Poster Order Report?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJPO As New PosterOrderDesign
            OBJPO.FRMSTRING = "POSTER"
            OBJPO.WHERECLAUSE = "{POSTERORDER.POSTER_NO} = " & Val(TEMPPONO) & " AND {POSTERORDER.POSTER_YEARID} = " & YearId
            OBJPO.BILLNO = TEMPPONO
            OBJPO.MdiParent = MDIMain
            OBJPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Call CMDOK_Click(sender, e)
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TSTXTBILLNO.Validating
        Try
            If Val(TSTXTBILLNO.Text.Trim) > 0 Then
                GRIDPOSTER.RowCount = 0
                TEMPPONO = Val(TSTXTBILLNO.Text)
                If TEMPPONO > 0 Then
                    EDIT = True
                    PosterOrder_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If LBLLOCKED.Visible = True Then
                    EP.SetError(LBLLOCKED, "Entry Locked, Invoice Made")
                    Exit Sub
                End If

                If MsgBox("Wish to Delete Poster Order?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                'DONE BY GULKIT
                'BEFORE UPDATING REVERSE THE ENTRY IN SCHEDULEMASTER_DESC
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPPONO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)


                Dim OBJPRO As New ClsPosterOrder
                OBJPRO.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJPRO.Delete
                MsgBox("Poster Order Deleted")

                clear()
                EDIT = False
                CMBNAME.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PosterOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                TSTXTBILLNO.Focus()
                TSTXTBILLNO.SelectAll()
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for grid foucs
                GRIDPOSTER.Focus()
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                Toolnext_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SENDWHATSAPP(TEMPPONO)
            DT = OBJCMN.Execute_Any_String("UPDATE POSTERORDER SET POSTER_SENDWHATSAPP = 1 WHERE POSTER_NO = " & TEMPPONO & " AND POSTER_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(POSTERNO As Integer)
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
            OBJJO.FRMSTRING = "POSTERORDER"
            OBJJO.DIRECTMAIL = False
            OBJJO.DIRECTWHATSAPP = True
            OBJJO.PRINTSETTING = PrintDialog
            OBJJO.FORMULA = "{POSTERORDER.POSTER_NO}=" & Val(POSTERNO) & " and {POSTERORDER.POSTER_yearid}=" & YearId
            OBJJO.JONO = Val(POSTERNO)
            OBJJO.NOOFCOPIES = 1
            OBJJO.Show()
            OBJJO.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\POSTER_" & Val(POSTERNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add("POSTER_" & Val(POSTERNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdselectOrder_Click(sender As Object, e As EventArgs) Handles cmdselectOrder.Click
        Try
            EP.Clear()
            If CMBPARTYNAME.Text.Trim = "" Then
                MsgBox("Please Fill Party Name", MsgBoxStyle.Critical)
                CMBPARTYNAME.Focus()
                Exit Sub
            End If


            Dim OBJSO As New SelectSO
            OBJSO.PARTYNAME = CMBPARTYNAME.Text.Trim
            OBJSO.ShowDialog()
            Dim DT As DataTable = OBJSO.DT
            If DT.Rows.Count > 0 Then

                For Each DR As DataRow In DT.Rows
                    GRIDPOSTER.Rows.Add(0, DR("ITEMNAME"), DR("DESIGN"), DR("COLOR"), "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
                Next
            End If

                getsrno(GRIDPOSTER)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXT12X18_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXT12X18.KeyPress, TXT12X24.KeyPress, TXT8X10.KeyPress, TXT6X8.KeyPress, TXT4X6.KeyPress, TXTCOLUMN1.KeyPress, TXTCOLUMN2.KeyPress, TXTCOLUMN3.KeyPress, TXTCOLUMN4.KeyPress, TXTCOLUMN5.KeyPress, TSTXTBILLNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTSONO_Validated(sender As Object, e As EventArgs) Handles TXTSONO.Validated
        Try
            If Val(TXTSONO.Text.Trim) > 0 And EDIT = False Then
                If MsgBox("Fetch Data From SO No " & Val(TXTSONO.Text.Trim) & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR", "", " SALEORDER INNER JOIN SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID LEFT OUTER JOIN DESIGNMASTER ON DESIGNMASTER.DESIGN_id = SALEORDER_DESC.SO_DESIGNID LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER ON SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER AS COLORMASTER ON SALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_id ", " AND SALEORDER.SO_NO = " & Val(TXTSONO.Text.Trim) & " AND SALEORDER.SO_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows
                        GRIDPOSTER.Rows.Add(0, DR("ITEM"), DR("DESIGN"), DR("COLOR"), "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
                    Next
                    getsrno(GRIDPOSTER)
                    TOTAL()
                Else
                    MsgBox("Invalid SO No", MsgBoxStyle.Critical)
                    TXTSONO.Clear()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDPOSTER_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDPOSTER.CellValidating
        Try
            Dim colNum As Integer = GRIDPOSTER.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return
            Select Case colNum

                Case G12X18.Index, G12X24.Index, G8X10.Index, G6X8.Index, G4X6.Index, GCOLUMN1.Index, GCOLUMN2.Index, GCOLUMN3.Index, GCOLUMN4.Index, GCOLUMN5.Index
                    Dim dDebit As Integer
                    Dim bValid As Boolean = Integer.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDPOSTER.CurrentCell.Value = Nothing Then GRIDPOSTER.CurrentCell.Value = "0"
                        GRIDPOSTER.CurrentCell.Value = Convert.ToInt16(GRIDPOSTER.Item(colNum, e.RowIndex).Value)
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If
                    TOTAL()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class