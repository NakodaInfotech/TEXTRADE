
Imports System.ComponentModel
Imports BL

Public Class OpeningBankReco
    Public EDIT As Boolean
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public TEMPBANKRECONO As Integer
    Public FRMSTRING As String

    Public Sub FILLGRID()

        If GRIDDOUBLECLICK = False Then
            GRIDBANKRECO.Rows.Add(Val(TXTGRIDSRNO.Text.Trim), CMBCHEQUE.Text.Trim, TXTENTRYNO.Text.Trim, CMBREGISTER.Text.Trim, CMBPARTYNAME.Text.Trim, TXTCHQNO.Text.Trim, Val(TXTAMOUNT.Text.Trim), "")
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDBANKRECO.Item(GSRNO.Index, TEMPROW).Value = Val(TXTGRIDSRNO.Text.Trim)
            GRIDBANKRECO.Item(GTYPE.Index, TEMPROW).Value = CMBCHEQUE.Text.Trim
            GRIDBANKRECO.Item(gentryno.Index, TEMPROW).Value = TXTENTRYNO.Text.Trim
            GRIDBANKRECO.Item(gregister.Index, TEMPROW).Value = CMBREGISTER.Text.Trim
            GRIDBANKRECO.Item(gpartyname.Index, TEMPROW).Value = CMBPARTYNAME.Text.Trim
            GRIDBANKRECO.Item(GCHQNO.Index, TEMPROW).Value = TXTCHQNO.Text.Trim
            GRIDBANKRECO.Item(GAMOUNT.Index, TEMPROW).Value = Val(TXTAMOUNT.Text.Trim)

            GRIDDOUBLECLICK = False
        End If
        TOTAL()

        GRIDBANKRECO.FirstDisplayedScrollingRowIndex = GRIDBANKRECO.RowCount - 1

        TXTGRIDSRNO.Text = Val(GRIDBANKRECO.RowCount) + 1
        TXTENTRYNO.Clear()
        CMBPARTYNAME.Text = ""
        TXTCHQNO.Clear()
        TXTAMOUNT.Clear()
        CMBCHEQUE.Focus()

    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(OPENING_SRNO),0) + 1 ", " OPENINGBANKRECO ", " and OPENING_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSRNO.Text = DTTABLE.Rows(0).Item(0)
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

    Public Sub FILLCMB()
        Try
            If CMBOPENBANK.Text.Trim = "" Then fillledger(CMBOPENBANK, EDIT, " and (groupmaster.group_SECONDARY = 'BANK A/C' OR groupmaster.group_SECONDARY = 'BANK OD A/C') and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
            If CMBPARTYNAME.Text.Trim <> "" Then fillledger(CMBPARTYNAME, EDIT, " AND acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTDATE_Validating(sender As Object, e As CancelEventArgs) Handles DTDATE.Validating
        Try
            If DTDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If EDIT = True Then

                If MsgBox("Wish to Delete Bank Reco?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPBANKRECONO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(YearId)


                Dim OBJPRO As New ClsOpeningBankReco
                OBJPRO.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJPRO.DELETE
                MsgBox("Opening Bank Reco Deleted")

                CLEAR()
                EDIT = False
                CMBOPENBANK.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try

            GRIDBANKRECO.RowCount = 0
LINE1:
            TEMPBANKRECONO = Val(TXTSRNO.Text) - 1
            If TEMPBANKRECONO > 0 Then
                EDIT = True
                OpeningBankReco_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDBANKRECO.RowCount = 0 And TEMPBANKRECONO > 1 Then
                TXTSRNO.Text = TEMPBANKRECONO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningBankReco_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            FILLCMB()
            CLEAR()

            If EDIT = True Then
                Dim OBJCMN As New ClsCommon
                Dim OBJCLSBANK As New ClsOpeningBankReco()
                Dim dttable As DataTable = OBJCLSBANK.selectBank_edit(TEMPBANKRECONO, CmpId, YearId)
                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTSRNO.Text = TEMPBANKRECONO
                        TXTSRNO.ReadOnly = True
                        CMBOPENBANK.Text = Convert.ToString(dr("BANKNAME"))
                        DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")

                        GRIDBANKRECO.Rows.Add(Val(dr("SRNO")), dr("TYPE").ToString, dr("ENTRYNO").ToString, dr("REGISTERNAME").ToString, dr("PARTYNAME").ToString, dr("CHQNO").ToString, Format(Val(dr("AMOUNT")), "0.00"), dr("RECODATE").ToString)

                    Next
                    getsrno(GRIDBANKRECO)

                    TOTAL()
                    GRIDBANKRECO.FirstDisplayedScrollingRowIndex = GRIDBANKRECO.RowCount - 1
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

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True
            Dim OBJCMN As New ClsCommon

            If GRIDBANKRECO.RowCount = 0 Then
                EP.SetError(CMBOPENBANK, "Fill Details")
                bln = False
            End If

            If CMBOPENBANK.Text.Length = 0 Then
                EP.SetError(CMBOPENBANK, "Fill Bank Name")
                bln = False
            End If

            If DTDATE.Text = "__/__/____" Then
                EP.SetError(DTDATE, " Please Enter Proper Date")
                bln = False
            End If

            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alparaval As New ArrayList
            alparaval.Add(Format(Convert.ToDateTime(DTDATE.Text).Date, "MM/dd/yyyy"))
            alparaval.Add(CMBOPENBANK.Text.Trim)
            alparaval.Add(Val(TXTCHQDEPOSITED.Text.Trim))
            alparaval.Add(Val(TXTCHQISSUED.Text.Trim))
            alparaval.Add(CmpId)
            alparaval.Add(Userid)
            alparaval.Add(YearId)

            Dim GRIDSRNO As String = ""
            Dim TYPE As String = ""
            Dim ENTRYNO As String = ""
            Dim REGISTER As String = ""
            Dim PARTYNAME As String = ""
            Dim CHQNO As String = ""
            Dim AMOUNT As String = ""
            Dim RECODATE As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDBANKRECO.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(row.Cells(GSRNO.Index).Value)
                        TYPE = row.Cells(GTYPE.Index).Value.ToString
                        ENTRYNO = row.Cells(GENTRYNO.Index).Value
                        REGISTER = row.Cells(GREGISTER.Index).Value
                        PARTYNAME = row.Cells(GPARTYNAME.Index).Value
                        CHQNO = row.Cells(GCHQNO.Index).Value
                        AMOUNT = Val(row.Cells(GAMOUNT.Index).Value)
                        RECODATE = row.Cells(GRECODATE.Index).Value

                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
                        TYPE = TYPE & "|" & row.Cells(GTYPE.Index).Value.ToString
                        ENTRYNO = ENTRYNO & "|" & row.Cells(GENTRYNO.Index).Value
                        REGISTER = REGISTER & "|" & row.Cells(GREGISTER.Index).Value
                        PARTYNAME = PARTYNAME & "|" & row.Cells(GPARTYNAME.Index).Value
                        CHQNO = CHQNO & "|" & row.Cells(GCHQNO.Index).Value
                        AMOUNT = AMOUNT & "|" & Val(row.Cells(GAMOUNT.Index).Value)
                        RECODATE = RECODATE & "|" & row.Cells(GRECODATE.Index).Value

                    End If
                End If
            Next
            alparaval.Add(GRIDSRNO)
            alparaval.Add(TYPE)
            alparaval.Add(ENTRYNO)
            alparaval.Add(REGISTER)
            alparaval.Add(PARTYNAME)
            alparaval.Add(CHQNO)
            alparaval.Add(AMOUNT)
            alparaval.Add(RECODATE)



            Dim OBJBANKRECO As New ClsOpeningBankReco
            OBJBANKRECO.alParaval = alparaval

            If EDIT = False Then
                Dim DT As DataTable = OBJBANKRECO.SAVE()
                TXTENTRYNO.Text = DT.Rows(0).Item(0)
                MsgBox("Details Added")

            ElseIf EDIT = True Then
                alparaval.Add(TEMPBANKRECONO)
                Dim IntResult As Integer = OBJBANKRECO.UPDATE()
                MsgBox("Details Updated")

            End If

            EDIT = False
            CLEAR()
            CMBOPENBANK.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            EDIT = False
            CMBOPENBANK.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub CLEAR()
        EP.Clear()

        DTDATE.Text = Now.Date
        CMBOPENBANK.Text = ""
        TXTENTRYNO.Clear()
        CMBPARTYNAME.Text = ""
        TXTAMOUNT.Clear()
        TXTCHQNO.Clear()
        getmaxno()
        GRIDBANKRECO.RowCount = 0

        TXTCHQDEPOSITED.Clear()
        TXTCHQISSUED.Clear()
        TXTGRIDSRNO.Text = GRIDBANKRECO.RowCount + 1
    End Sub

    Private Sub TXTAMOUNT_Validating(sender As Object, e As CancelEventArgs) Handles TXTAMOUNT.Validating
        Try
            If CMBOPENBANK.Text.Trim <> "" And CMBCHEQUE.Text.Trim <> "" And TXTENTRYNO.Text.Trim <> "" And CMBPARTYNAME.Text.Trim <> "" And Val(TXTAMOUNT.Text.Trim) > 0 Then FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTDATE_GotFocus(sender As Object, e As EventArgs) Handles DTDATE.GotFocus
        DTDATE.SelectionStart = 0
    End Sub



    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            GRIDBANKRECO.RowCount = 0
LINE1:
            TEMPBANKRECONO = Val(TXTSRNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTSRNO.Text.Trim
            CLEAR()
            If Val(TXTSRNO.Text) - 1 >= TEMPBANKRECONO Then
                EDIT = True
                OpeningBankReco_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDBANKRECO.RowCount = 0 And TEMPBANKRECONO < MAXNO Then
                TXTSRNO.Text = TEMPBANKRECONO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOPENBANK_Enter(sender As Object, e As EventArgs) Handles CMBOPENBANK.Enter
        Try
            If CMBOPENBANK.Text.Trim = "" Then fillledger(CMBOPENBANK, EDIT, " and (groupmaster.group_SECONDARY = 'BANK A/C' OR groupmaster.group_SECONDARY = 'BANK OD A/C') and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOPENBANK_Validating(sender As Object, e As CancelEventArgs) Handles CMBOPENBANK.Validating
        Try
            If CMBOPENBANK.Text.Trim <> "" Then ledgervalidate(CMBOPENBANK, CMBACCCODE, e, Me, TXTADD, " and (groupmaster.group_SECONDARY = 'BANK A/C' OR groupmaster.group_SECONDARY = 'BANK OD A/C') and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        TXTCHQDEPOSITED.Text = 0.00
        TXTCHQISSUED.Text = 0.00

        For Each row As DataGridViewRow In GRIDBANKRECO.Rows
            If row.Cells(GTYPE.Index).Value = "Cheque Deposited" Then
                TXTCHQDEPOSITED.Text = Format(Val(TXTCHQDEPOSITED.Text.Trim) + Val(row.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
            Else
                TXTCHQISSUED.Text = Format(Val(TXTCHQISSUED.Text.Trim) + Val(row.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
            End If
        Next

    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then ledgervalidate(CMBPARTYNAME, CMBACCCODE, e, Me, TXTADD, " AND acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBREGISTER_Enter(sender As Object, e As EventArgs) Handles CMBREGISTER.Enter
        Try
            If CMBREGISTER.Text.Trim = "" Then fillregister(CMBREGISTER, " and (register_type = 'RECEIPT' OR register_type = 'PAYMENT')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBANKRECO_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDBANKRECO.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDBANKRECO.CurrentRow.Index >= 0 And GRIDBANKRECO.Item(GSRNO.Index, GRIDBANKRECO.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True

                TXTGRIDSRNO.Text = GRIDBANKRECO.Item(GSRNO.Index, GRIDBANKRECO.CurrentRow.Index).Value.ToString
                CMBCHEQUE.Text = GRIDBANKRECO.Item(GTYPE.Index, GRIDBANKRECO.CurrentRow.Index).Value.ToString
                TXTENTRYNO.Text = GRIDBANKRECO.Item(GENTRYNO.Index, GRIDBANKRECO.CurrentRow.Index).Value.ToString
                CMBREGISTER.Text = GRIDBANKRECO.Item(GREGISTER.Index, GRIDBANKRECO.CurrentRow.Index).Value.ToString
                CMBPARTYNAME.Text = GRIDBANKRECO.Item(GPARTYNAME.Index, GRIDBANKRECO.CurrentRow.Index).Value.ToString
                TXTCHQNO.Text = GRIDBANKRECO.Item(GCHQNO.Index, GRIDBANKRECO.CurrentRow.Index).Value.ToString
                TXTAMOUNT.Text = GRIDBANKRECO.Item(GAMOUNT.Index, GRIDBANKRECO.CurrentRow.Index).Value.ToString


                TEMPROW = GRIDBANKRECO.CurrentRow.Index
                CMBCHEQUE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillledger(CMBPARTYNAME, EDIT, " AND acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJOP As New OpeningBankRecoDetails
            OBJOP.MdiParent = MDIMain
            OBJOP.FRMSTRING = FRMSTRING
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class