
Imports BL

Public Class OpeningStockRollsWarper

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPOPROLLSTOCKWARPER As Integer

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALROLLS.Text = 0
            LBLTOTALWT.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If ROW.Cells(GOPROLLSTOCKNOSIZER.Index).Value <> Nothing Then
                    LBLTOTALROLLS.Text = Format(Val(LBLTOTALROLLS.Text) + Val(ROW.Cells(GROLLS.Index).EditedFormattedValue), "0")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.000")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        TXTOPROLLSSTOCKNOSIZER.Clear()
        CMBWARPER.Text = ""
        CMBYARNQUALITY.Text = ""
        CMBMILL.Text = ""
        TXTTOTALENDS.Clear()
        TXTROLLS.Clear()
        TXTWT.Clear()
        TXTPROGRAMNO.Clear()
        TXTREMARKS.Clear()
    End Sub

    Private Sub OpeningStockRollsSizer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.OemQuotes Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        FILLNAME(CMBWARPER, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        fillYARNQUALITY(CMBYARNQUALITY, EDIT)
        FILLMILL(CMBMILL, EDIT)
    End Sub

    Sub FILLGRID()
        Try

            Dim OBJOPSTOCK As New ClsOpeningStockRollsWarper
            OBJOPSTOCK.alParaval.Add(0)
            OBJOPSTOCK.alParaval.Add(YearId)
            Dim dttable As DataTable = OBJOPSTOCK.GETSTOCKROLLS()
            If dttable.Rows.Count > 0 Then
                'ITEM GRID
                For Each ROW As DataRow In dttable.Rows
                    GRIDSTOCK.Rows.Add(Val(ROW("OPROLLSTOCKNOWARPER")), ROW("WARPER"), ROW("YARNQUALITY"), ROW("MILL"), Val(ROW("ENDS")), Val(ROW("ROLLS")), Format(Val(ROW("WT")), "0.000"), Val(ROW("PROGRAMNO")), ROW("REMARKS"), Val(ROW("OUTROLLS")), Val(ROW("OUTWT")))
                    If Val(ROW("OUTROLLS")) > 0 Or Val(ROW("OUTWT")) > 0 Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                Next
            End If
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub TXTREMARKS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTREMARKS.Validating
        If CMBWARPER.Text.Trim <> "" And CMBYARNQUALITY.Text.Trim <> "" And Val(TXTTOTALENDS.Text.Trim) > 0 And Val(TXTROLLS.Text.Trim) > 0 And Val(TXTWT.Text.Trim) > 0 Then

            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBWARPER.Text.Trim)
            ALPARAVAL.Add(CMBYARNQUALITY.Text.Trim)
            ALPARAVAL.Add(CMBMILL.Text.Trim)
            ALPARAVAL.Add(Val(TXTTOTALENDS.Text.Trim))
            ALPARAVAL.Add(Val(TXTROLLS.Text.Trim))
            ALPARAVAL.Add(Format(Val(TXTWT.Text.Trim), "0.000"))
            ALPARAVAL.Add(Val(TXTPROGRAMNO.Text.Trim))
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim OBJOPENSTOCK As New ClsOpeningStockRollsWarper
            OBJOPENSTOCK.alParaval = ALPARAVAL

            If EDIT = False Then
                Dim DT As DataTable = OBJOPENSTOCK.SAVE()
            Else

                ALPARAVAL.Add(Val(TXTOPROLLSSTOCKNOSIZER.Text))
                Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                GRIDDOUBLECLICK = False
                EDIT = False
            End If

            GRIDSTOCK.RowCount = 0
            FILLGRID()
            CLEAR()
            CMBWARPER.Focus()
        Else
            MsgBox("Enter Proper Details")
        End If
    End Sub

    Private Sub OpeningStockRollsSizer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'OPENING'")
        USERADD = DTROW(0).Item(1)
        USEREDIT = DTROW(0).Item(2)
        USERVIEW = DTROW(0).Item(3)
        USERDELETE = DTROW(0).Item(4)

        Dim OBJSEARCH As New ClsCommon
        Dim dttable As New DataTable

        If USEREDIT = False And USERVIEW = False Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        FILLCMB()
        FILLGRID()
        TOTAL()
    End Sub

    Private Sub GRIDSTOCK_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSTOCK.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If GRIDSTOCK.Item(GOUTROLLS.Index, e.RowIndex).Value > 0 Or GRIDSTOCK.Item(GOUTWT.Index, e.RowIndex).Value > 0 Then
                MsgBox("Rolls Locked, it is used further", MsgBoxStyle.Critical)
                Exit Sub
            End If

            TXTOPROLLSSTOCKNOSIZER.Text = GRIDSTOCK.Item(GOPROLLSTOCKNOSIZER.Index, e.RowIndex).Value
            CMBWARPER.Text = GRIDSTOCK.Item(GWARPER.Index, e.RowIndex).Value
            CMBYARNQUALITY.Text = GRIDSTOCK.Item(GYARNQUALITY.Index, e.RowIndex).Value
            CMBMILL.Text = GRIDSTOCK.Item(GMILL.Index, e.RowIndex).Value
            TXTTOTALENDS.Text = Val(GRIDSTOCK.Item(GENDS.Index, e.RowIndex).Value)
            TXTROLLS.Text = Val(GRIDSTOCK.Item(GROLLS.Index, e.RowIndex).Value)
            TXTWT.Text = Val(GRIDSTOCK.Item(GWT.Index, e.RowIndex).Value)
            TXTPROGRAMNO.Text = Val(GRIDSTOCK.Item(GPROGRAMNO.Index, e.RowIndex).Value)
            TXTREMARKS.Text = GRIDSTOCK.Item(GREMARKS.Index, e.RowIndex).Value

            GRIDDOUBLECLICK = True
            EDIT = True
            TEMPROW = e.RowIndex
            CMBWARPER.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSTOCK_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDSTOCK.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If GRIDSTOCK.SelectedCells.Count > 0 Then

                    If USERDELETE = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If

                    If GRIDDOUBLECLICK = True Then
                        MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                        Exit Sub
                    End If

                    If GRIDSTOCK.Item(GOUTROLLS.Index, GRIDSTOCK.CurrentRow.Index).Value > 0 Or GRIDSTOCK.Item(GOUTWT.Index, GRIDSTOCK.CurrentRow.Index).Value > 0 Then
                        MsgBox("Rolls Locked, it is used further", MsgBoxStyle.Critical)
                        Exit Sub
                    End If


                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningStockRollsWarper

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(GRIDSTOCK.Rows(GRIDSTOCK.CurrentRow.Index).Cells(GOPROLLSTOCKNOSIZER.Index).Value)
                        ALPARAVAL.Add(YearId)

                        Dim INTRES As DataTable = OBJNO.DELETE()
                        GRIDSTOCK.Rows.RemoveAt(GRIDSTOCK.CurrentRow.Index)
                    End If
                    TOTAL()

                End If
            End If
        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress
        numdot3(e, sender, Me)
    End Sub

    Private Sub TXTROLLS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTROLLS.KeyPress, TXTTOTALENDS.KeyPress, TXTPROGRAMNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub CMBWARPER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBWARPER.Enter
        Try
            If CMBWARPER.Text.Trim = "" Then FILLNAME(CMBWARPER, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBWARPER.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBWARPER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBWARPER.Validating
        Try
            If CMBWARPER.Text.Trim <> "" Then NAMEVALIDATE(CMBWARPER, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'", "SUNDRY CREDITORS", "ACCOUNTS", "", "")
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

    Private Sub CMBYARNQUALITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBYARNQUALITY.Validating
        Try
            If CMBYARNQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBYARNQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then FILLMILL(CMBMILL, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then MILLVALIDATE(CMBMILL, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class