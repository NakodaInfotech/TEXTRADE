
Imports System.ComponentModel
Imports BL

Public Class StoreItemMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean              'Used for edit
    Public TEMPITEMNAME As String           'Used for edit name
    Public TEMPID As Integer            'Used for edit id
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(TXTNAME.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)



            'GRID 
            Dim GRIDSRNO As String = ""
            Dim GRIDITEMNAME As String = ""
            Dim GRIDQTY As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDSTORE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDITEMNAME = "" Then
                        GRIDSRNO = Val(row.Cells(GSRNO.Index).Value)
                        GRIDITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        GRIDQTY = Val(row.Cells(GQTY.Index).Value)

                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
                        GRIDITEMNAME = GRIDITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value.ToString
                        GRIDQTY = GRIDQTY & "|" & Val(row.Cells(GQTY.Index).Value)

                    End If
                End If
            Next


            ALPARAVAL.Add(GRIDSRNO)
            ALPARAVAL.Add(GRIDITEMNAME)
            ALPARAVAL.Add(GRIDQTY)

            ALPARAVAL.Add(CMBPARTYNAME.Text.Trim)
            ALPARAVAL.Add(CMBUNIT.Text.Trim)
            ALPARAVAL.Add(CMBHSNCODE.Text.Trim)
            ALPARAVAL.Add(CMBDEBITLEDGER.Text.Trim)
            ALPARAVAL.Add(cmbcategory.Text.Trim)



            Dim OBJADD As New ClsStoreItemMaster
            OBJADD.ALPARAVAL = ALPARAVAL
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = OBJADD.SAVE()
                MsgBox("Details Added")

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                ALPARAVAL.Add(TEMPID)

                IntResult = OBJADD.UPDATE()
                MsgBox("Details Updated")
                EDIT = False
            End If
            CLEAR()
            TXTNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True

            If TXTNAME.Text.Trim.Length = 0 Then
                EP.SetError(TXTNAME, "Fill Name")
                BLN = False
            Else
                If (EDIT = False) Or (EDIT = True And LCase(TEMPITEMNAME) <> LCase(TXTNAME.Text.Trim)) Then
                    ' search duplication 
                    Dim OBJCMN As New ClsCommon
                    Dim dt As New DataTable
                    dt = OBJCMN.search(" STOREITEM_NAME ", "", " StoreItemMaster ", " AND STOREITEM_NAME = '" & TXTNAME.Text.Trim & "' AND  STOREITEM_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        EP.SetError(TXTNAME, "Name Already Exists")
                        BLN = False
                    End If
                    uppercase(TXTNAME)
                End If
            End If

            If CMBUNIT.Text.Trim = "" Then
                EP.SetError(CMBUNIT, "Fill Unit")
                BLN = False
            End If

            If CMBHSNCODE.Text.Trim = "" Then
                EP.SetError(CMBHSNCODE, "Fill HSN Code")
                BLN = False
            End If


            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub CLEAR()
        Try
            EP.Clear()
            TXTNAME.Clear()
            CMBPARTYNAME.Text = ""
            CMBUNIT.Text = ""
            CMBHSNCODE.Text = ""
            CMBDEBITLEDGER.Text = ""
            cmbcategory.Text = ""


            'GRID
            TXTQTY.Clear()
            CMBPARTYNAME.Text = ""
            GRIDSTORE.RowCount = 0
            TXTSRNO.Text = GRIDSTORE.RowCount + 1

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNAME.Validating
        Try

            If (EDIT = False) Or (EDIT = True And LCase(TEMPITEMNAME) <> LCase(TXTNAME.Text.Trim)) Then
                ' search duplication 
                If TXTNAME.Text.Trim <> Nothing Then
                    Dim OBJCMN As New ClsCommonMaster
                    Dim dt As DataTable = OBJCMN.search(" STOREITEM_NAME ", "", " StoreItemMaster ", " AND STOREITEM_NAME = '" & TXTNAME.Text.Trim & "' AND  STOREITEM_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Name Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                        e.Cancel = True
                    End If
                    uppercase(TXTNAME)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StoreItemMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then               'FOR EXIT
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.OemQuotes Then
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            fillname(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' ")
            fillunit(CMBUNIT)
            FILLSTOREITEMNAME(CMBSTOREITEMNAME)
            FILLHSNITEMDESC(CMBHSNCODE)
            FILLNAME(CMBDEBITLEDGER, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'INDIRECT EXPENSES' OR GROUPMASTER.GROUP_SECONDARY = 'DIRECT EXPENSES' OR GROUPMASTER.GROUP_SECONDARY = 'FIXED ASSETS' OR GROUPMASTER.GROUP_SECONDARY = 'PURCHASE A/C')")
            fillCATEGORY(cmbcategory, False)

        Catch ex As Exception
            Throw ex
        End Try
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

            dt = objclscommon.search(" ISNULL(HSN_CODE, '') AS HSNCODE ", "", " HSNMASTER ", " AND HSN_YEARID = " & YearId)
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

    Private Sub StoreItemMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'STORE ITEM MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            FILLCMB
            CLEAR()
            TXTNAME.Text = TEMPITEMNAME
            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPID)
                ALPARAVAL.Add(YearId)
                Dim OBJSELECT As New ClsStoreItemMaster
                OBJSELECT.ALPARAVAL = ALPARAVAL
                Dim dttable As DataTable = OBJSELECT.GETSTOREITEM()
                For Each DR As DataRow In dttable.Rows
                    TXTNAME.Text = DR("NAME")
                    TEMPITEMNAME = DR("NAME")
                    CMBPARTYNAME.Text = DR("PARTYNAME")
                    CMBUNIT.Text = DR("UNIT")
                    CMBHSNCODE.Text = DR("HSNCODE")
                    CMBDEBITLEDGER.Text = DR("DEBITLEDGER")
                    cmbcategory.Text = DR("CATEGORY")

                    If Val(DR("SRNO")) > 0 Then GRIDSTORE.Rows.Add(Val(DR("SRNO")), DR("ITEMNAME"), Val(DR("QTY")))
                Next

            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Store Item Permanently?", MsgBoxStyle.YesNo, "TEXTRADE") = MsgBoxResult.No Then Exit Sub


                Dim OBJSTORE As New ClsStoreItemMaster
                Dim ALPARAVAL As New ArrayList

                ALPARAVAL.Add(TEMPID)
                ALPARAVAL.Add(YearId)

                OBJSTORE.ALPARAVAL = ALPARAVAL
                Dim INTRES As Integer = OBJSTORE.DELETE()
                EDIT = False
                CLEAR()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            EDIT = False
            TXTNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQTY_Validated(sender As Object, e As EventArgs) Handles TXTQTY.Validated
        Try
            If CMBSTOREITEMNAME.Text.Trim <> "" And Val(TXTQTY.Text.Trim) > 0 Then FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDSTORE.Rows.Add(Val(TXTSRNO.Text.Trim), CMBSTOREITEMNAME.Text.Trim, Val(TXTQTY.Text.Trim))
                getsrno(GRIDSTORE)
            ElseIf GRIDDOUBLECLICK = True Then
                GRIDSTORE.Item(GITEMNAME.Index, TEMPROW).Value = CMBSTOREITEMNAME.Text.Trim
                GRIDSTORE.Item(GQTY.Index, TEMPROW).Value = Val(TXTQTY.Text.Trim)
                GRIDDOUBLECLICK = False
            End If

            TXTSRNO.Text = GRIDSTORE.RowCount + 1
            CMBSTOREITEMNAME.Text = ""
            TXTQTY.Clear()
            CMBSTOREITEMNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSTORE_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSTORE.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub EDITROW()
        Try
            If GRIDSTORE.CurrentRow.Index >= 0 And GRIDSTORE.Item(GSRNO.Index, GRIDSTORE.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TXTSRNO.Text = Val(GRIDSTORE.Item(GSRNO.Index, GRIDSTORE.CurrentRow.Index).Value)
                CMBSTOREITEMNAME.Text = GRIDSTORE.Item(GITEMNAME.Index, GRIDSTORE.CurrentRow.Index).Value.ToString
                TXTQTY.Text = Val(GRIDSTORE.Item(GQTY.Index, GRIDSTORE.CurrentRow.Index).Value)

                TEMPROW = GRIDSTORE.CurrentRow.Index
                CMBSTOREITEMNAME.Focus()
            End If
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

    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillname(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSTOREITEMNAME_Enter(sender As Object, e As EventArgs) Handles CMBSTOREITEMNAME.Enter
        Try
            If CMBSTOREITEMNAME.Text.Trim = "" Then FILLSTOREITEMNAME(CMBSTOREITEMNAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSTORE_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDSTORE.KeyDown
        If e.KeyCode = Keys.Delete And GRIDSTORE.RowCount > 0 Then
            If GRIDDOUBLECLICK = True Then
                MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                Exit Sub
            End If

            GRIDSTORE.Rows.RemoveAt(GRIDSTORE.CurrentRow.Index)
            getsrno(GRIDSTORE)

        ElseIf e.KeyCode = Keys.F5 Then
            EDITROW()
        End If
    End Sub

    Private Sub CMBSTOREITEMNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBSTOREITEMNAME.Validating
        Try
            If CMBSTOREITEMNAME.Text.Trim <> "" Then STOREITEMVALIDATE(CMBSTOREITEMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQTY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTQTY.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then namevalidate(CMBPARTYNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBUNIT_Enter(sender As Object, e As EventArgs) Handles CMBUNIT.Enter
        Try
            If CMBUNIT.Text.Trim = "" Then fillunit(CMBUNIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBUNIT_Validating(sender As Object, e As CancelEventArgs) Handles CMBUNIT.Validating
        Try
            If CMBUNIT.Text.Trim <> "" Then unitvalidate(CMBUNIT, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDEBITLEDGER_Enter(sender As Object, e As EventArgs) Handles CMBDEBITLEDGER.Enter
        Try
            If CMBDEBITLEDGER.Text.Trim = "" Then fillname(CMBDEBITLEDGER, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY = 'INDIRECT EXPENSES' OR GROUPMASTER.GROUP_SECONDARY = 'DIRECT EXPENSES' OR GROUPMASTER.GROUP_SECONDARY = 'FIXED ASSETS' OR GROUPMASTER.GROUP_SECONDARY = 'PURCHASE A/C')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDEBITLEDGER_Validating(sender As Object, e As CancelEventArgs) Handles CMBDEBITLEDGER.Validating
        Try
            If CMBDEBITLEDGER.Text.Trim <> "" Then namevalidate(CMBDEBITLEDGER, CMBCODE, e, Me, TXTADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'INDIRECT EXPENSES' OR GROUPMASTER.GROUP_SECONDARY = 'DIRECT EXPENSES' OR GROUPMASTER.GROUP_SECONDARY = 'FIXED ASSETS' OR GROUPMASTER.GROUP_SECONDARY = 'PURCHASE A/C')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcategory_Validating(sender As Object, e As CancelEventArgs) Handles cmbcategory.Validating
        Try
            If cmbcategory.Text.Trim <> "" Then CATEGORYVALIDATE(cmbcategory, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcategory_Enter(sender As Object, e As EventArgs) Handles cmbcategory.Enter
        Try
            If cmbcategory.Text.Trim = "" Then fillCATEGORY(cmbcategory, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class