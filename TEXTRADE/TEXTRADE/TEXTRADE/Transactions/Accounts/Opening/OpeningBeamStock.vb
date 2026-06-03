
Imports System.ComponentModel
Imports BL

Public Class OpeningBeamStock

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPOPROLLSTOCKNO As Integer
    Public FRMSTRING As String

    Sub GETSRNO(ByRef grid As System.Windows.Forms.DataGridView)
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
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALROLLNO.Text = 0
            LBLTOTALBEAMWT.Text = 0.0

            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If ROW.Cells(GBEAMSTOCKNO.Index).Value <> Nothing Then
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GTOTALMTRS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALROLLNO.Text = Val(LBLTOTALROLLNO.Text) + 1
                    LBLTOTALBEAMWT.Text = Format(Val(LBLTOTALBEAMWT.Text) + Val(ROW.Cells(GBEAMWT.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            TXTOPROLLSSTOCKNO.Clear()
            If USERGODOWN <> "" Then CMBGODOWN.Text = USERGODOWN Else CMBGODOWN.Text = ""
            CMBNAME.Text = ""
            CMBMILL.Text = ""
            TXTBEAMNO.Clear()
            TXTBEAMNAME.Clear()
            TXTTOTALENDS.Clear()
            TXTTOTALMTRS.Clear()
            TXTGAMANO.Clear()
            TXTSECTION.Clear()
            TXTBEAMWT.Clear()
            TXTBREAKAGE.Clear()
            TXTREMARKS.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpeningStockRolls_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        FILLNAME(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        FILLMILL(CMBMILL, EDIT)
        FILLROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1 ", "HAVING SUM(QTY - ISSQTY) >0")
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim <> "" Then fillGODOWN(CMBGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, cmbcode, e, Me, TXTADD, "AND GROUPMASTER.GROUP_SECONDARY='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'", "SUNDRY CREDITORS", "ACCOUNTS")
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

    Sub FILLGRID()
        Try
            GRIDSTOCK.Enabled = True

            If GRIDDOUBLECLICK = False Then
                GRIDSTOCK.Rows.Add(Val(txtsrno.Text.Trim), Val(TXTNO.Text.Trim), CMBGODOWN.Text.Trim, CMBNAME.Text.Trim, CMBMILL.Text.Trim, TXTBEAMNO.Text.Trim, TXTBEAMNAME.Text.Trim, Val(TXTTOTALENDS.Text.Trim), Val(TXTTOTALMTRS.Text.Trim), Val(TXTGAMANO.Text.Trim), Val(TXTSECTION.Text.Trim), CMBROLLNO.Text.Trim, Val(TXTBEAMWT.Text.Trim), Val(TXTBREAKAGE.Text.Trim), TXTREMARKS.Text.Trim, 0, 0)

                GRIDSTOCK.FirstDisplayedScrollingRowIndex = GRIDSTOCK.RowCount - 1
            ElseIf GRIDDOUBLECLICK = True Then
                GRIDSTOCK.Item(GGRIDSRNO.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
                GRIDSTOCK.Item(GNAME.Index, TEMPROW).Value = CMBNAME.Text.Trim
                GRIDSTOCK.Item(GMILL.Index, TEMPROW).Value = CMBMILL.Text.Trim
                GRIDSTOCK.Item(GBEAMNO.Index, TEMPROW).Value = Val(TXTBEAMNO.Text.Trim)
                GRIDSTOCK.Item(GBEAMNAME.Index, TEMPROW).Value = TXTBEAMNAME.Text.Trim
                GRIDSTOCK.Item(GTOTALENDS.Index, TEMPROW).Value = Val(TXTTOTALENDS.Text.Trim)
                GRIDSTOCK.Item(GTOTALMTRS.Index, TEMPROW).Value = Val(TXTTOTALMTRS.Text.Trim)
                GRIDSTOCK.Item(GGAMANO.Index, TEMPROW).Value = Val(TXTGAMANO.Text.Trim)
                GRIDSTOCK.Item(GSECTION.Index, TEMPROW).Value = Val(TXTSECTION.Text.Trim)
                GRIDSTOCK.Item(GROLLNO.Index, TEMPROW).Value = CMBROLLNO.Text.Trim
                GRIDSTOCK.Item(GBEAMWT.Index, TEMPROW).Value = Val(TXTBEAMWT.Text.Trim)
                GRIDSTOCK.Item(GBREAKAGE.Index, TEMPROW).Value = Val(TXTBREAKAGE.Text.Trim)
                GRIDSTOCK.Item(GREMARKS.Index, TEMPROW).Value = TXTREMARKS.Text.Trim


                GRIDDOUBLECLICK = False
            End If
            GETSRNO(GRIDSTOCK)
            TXTNO.Clear()
            TXTBEAMNO.Text = TXTBEAMNO.Text + 1
            txtsrno.Text = GRIDSTOCK.RowCount + 1
            CMBNAME.Text = ""
            CMBMILL.Text = ""
            TXTBEAMNAME.Text = ""
            TXTTOTALENDS.Clear()
            TXTTOTALMTRS.Clear()
            TXTGAMANO.Clear()
            TXTSECTION.Clear()
            CMBROLLNO.DataSource = Nothing
            CMBROLLNO.Text = ""

            If CMBROLLNO.Text = "" Then
                Dim strUsedRolls As String = ""
                For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                    If ROW.IsNewRow Then Continue For
                    If GRIDDOUBLECLICK = True And ROW.Index = TEMPROW Then Continue For
                    Dim cellVal As String = If(ROW.Cells(GROLLNO.Index).Value IsNot Nothing, ROW.Cells(GROLLNO.Index).Value.ToString.Trim, "")
                    If cellVal <> "" Then strUsedRolls = strUsedRolls & "'" & cellVal & "',"
                Next
                If strUsedRolls <> "" Then strUsedRolls = " AND ITEMNAME NOT IN (" & strUsedRolls.TrimEnd(",") & ") "
                FILLROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1 " & strUsedRolls, "HAVING SUM(QTY - ISSQTY) >0")
            End If
            TXTBEAMWT.Clear()
            TXTBREAKAGE.Clear()
            TXTREMARKS.Clear()
            TOTAL()

            TXTBEAMNO.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub TXTREMARKS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTREMARKS.Validating
        Try
            If TXTBEAMNO.Text.Trim <> "" And TXTBEAMNAME.Text.Trim <> "" And Val(TXTTOTALENDS.Text.Trim) > 0 And Val(TXTTOTALMTRS.Text.Trim) > 0 And CMBROLLNO.Text.Trim <> "" Then

                If CMBTYPE.Text.Trim = "INHOUSE" And CMBGODOWN.Text.Trim = "" Then Exit Sub
                If CMBTYPE.Text.Trim = "JOBBERSTOCK" And CMBNAME.Text.Trim = "" Then Exit Sub

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(openingdate.Value.Date)
                ALPARAVAL.Add(CMBTYPE.Text.Trim)
                ALPARAVAL.Add(Val(txtsrno.Text.Trim))
                ALPARAVAL.Add(CMBGODOWN.Text.Trim)
                ALPARAVAL.Add(CMBNAME.Text.Trim)
                ALPARAVAL.Add(CMBMILL.Text.Trim)
                ALPARAVAL.Add(Val(TXTBEAMNO.Text.Trim))
                ALPARAVAL.Add(TXTBEAMNAME.Text.Trim)

                ALPARAVAL.Add(Val(TXTTOTALENDS.Text.Trim))
                ALPARAVAL.Add(Val(TXTTOTALMTRS.Text.Trim))
                ALPARAVAL.Add(Val(TXTGAMANO.Text.Trim))
                ALPARAVAL.Add(Val(TXTSECTION.Text.Trim))
                ALPARAVAL.Add(CMBROLLNO.Text.Trim)
                ALPARAVAL.Add(Val(TXTBEAMWT.Text.Trim))
                ALPARAVAL.Add(Val(TXTBREAKAGE.Text.Trim))
                ALPARAVAL.Add(TXTREMARKS.Text.Trim)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)


                Dim OBJOPENSTOCK As New ClsOpeningBeamStock
                OBJOPENSTOCK.alParaval = ALPARAVAL

                If EDIT = False Then
                    Dim DT As DataTable = OBJOPENSTOCK.SAVE()
                    If DT.Rows.Count > 0 Then TXTNO.Text = DT.Rows(0).Item(0)
                Else
                    ALPARAVAL.Add(TXTNO.Text.Trim)
                    Dim INTRES As Integer = OBJOPENSTOCK.UPDATE()
                    EDIT = False
                End If

                FILLGRID()

                If Not GRIDDOUBLECLICK Then
                    Dim strUsedRolls As String = ""
                    For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                        If ROW.IsNewRow Then Continue For
                        Dim cellVal As String = If(ROW.Cells(GROLLNO.Index).Value IsNot Nothing, ROW.Cells(GROLLNO.Index).Value.ToString.Trim, "")
                        If cellVal <> "" Then strUsedRolls = strUsedRolls & "'" & cellVal & "',"
                    Next
                    If strUsedRolls <> "" Then strUsedRolls = " AND ITEMNAME NOT IN (" & strUsedRolls.TrimEnd(",") & ") "
                    FILLROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1 " & strUsedRolls, "HAVING SUM(QTY - ISSQTY) >0")
                End If

                EDIT = False
                CMBGODOWN.Focus()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub OpeningStockRolls_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'OPENING'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            CMBTYPE.Text = FRMSTRING
            FILLCMB()
            openingdate.Value = AccFrom.Date

            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.Execute_Any_String(" SELECT ISNULL(STOCKMASTER_BEAM.SMBEAM_NO, 0) AS OPBEAMSTOCKNO, ISNULL(STOCKMASTER_BEAM.SMBEAM_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(MILLMASTER.MILL_NAME, '') AS MILL, ISNULL(STOCKMASTER_BEAM.SMBEAM_BEAMNO, '0') AS BEAMNO, ISNULL(STOCKMASTER_BEAM.SMBEAM_TOTALENDS, 0) AS TOTALENDS, ISNULL(STOCKMASTER_BEAM.SMBEAM_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(STOCKMASTER_BEAM.SMBEAM_GAMANO, 0) AS GAMANO, ISNULL(STOCKMASTER_BEAM.SMBEAM_SECTION, 0) AS SECTION, ISNULL(STOCKMASTER_BEAM.SMBEAM_ROLLNO, 0) AS INT, ISNULL(STOCKMASTER_BEAM.SMBEAM_BEAMWT, 0) AS BEAMWT, ISNULL(STOCKMASTER_BEAM.SMBEAM_REMARKS, '') AS REMARKS, ISNULL(STOCKMASTER_BEAM.SMBEAM_OUTWT, 0) AS OUTWT, ISNULL(STOCKMASTER_BEAM.SMBEAM_OUTMTRS, 0) AS OUTMTRS, ISNULL(STOCKMASTER_BEAM.SMBEAM_DONE, 0) AS DONE, ISNULL(STOCKMASTER_BEAM.SMBEAM_BREAKAGE, 0) AS BREAKAGE, STOCKMASTER_BEAM.SMBEAM_DATE AS DATE, ISNULL(STOREITEMMASTER.STOREITEM_NAME, '') AS ROLLNO, ISNULL(STOCKMASTER_BEAM.SMBEAM_BEAMNAME, '') AS BEAMNAME FROM STOCKMASTER_BEAM INNER JOIN GODOWNMASTER ON STOCKMASTER_BEAM.SMBEAM_GODOWNID = GODOWNMASTER.GODOWN_id INNER JOIN LEDGERS ON STOCKMASTER_BEAM.SMBEAM_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN MILLMASTER ON STOCKMASTER_BEAM.SMBEAM_MILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN STOREITEMMASTER ON STOCKMASTER_BEAM.SMBEAM_ROLLNO = STOREITEMMASTER.STOREITEM_ID   WHERE  STOCKMASTER_BEAM.SMBEAM_YEARID = " & YearId & " ORDER BY SMBEAM_NO", "", "")
            If dttable.Rows.Count > 0 Then
                For Each ROW As DataRow In dttable.Rows
                    openingdate.Value = Format(Convert.ToDateTime(ROW("DATE")).Date, "dd/MM/yyyy")

                    GRIDSTOCK.Rows.Add(Val(ROW("GRIDSRNO")), Val(ROW("OPBEAMSTOCKNO")), ROW("GODOWN"), ROW("NAME"), ROW("MILL"), Val(ROW("BEAMNO")), ROW("BEAMNAME"), Val(ROW("TOTALENDS")), Format(Val(ROW("TOTALMTRS")), "0.00"), Format(Val(ROW("GAMANO")), "0.00"), Format(Val(ROW("SECTION")), "0.00"), Val(ROW("ROLLNO")), Format(Val(ROW("BEAMWT")), "0.00"), Format(Val(ROW("BREAKAGE")), "0.00"), ROW("REMARKS"), Val(ROW("OUTMTRS")), Val(ROW("OUTWT")))
                    If Val(ROW("OUTMTRS")) > 0 Or Val(ROW("OUTWT")) > 0 Then GRIDSTOCK.Rows(GRIDSTOCK.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                Next
                GETSRNO(GRIDSTOCK)
                GRIDSTOCK.FirstDisplayedScrollingRowIndex = GRIDSTOCK.RowCount - 1
            End If
            txtsrno.Text = Val(GRIDSTOCK.RowCount) + 1
            FILLROLLITEM(CMBROLLNO, EDIT, "AND ROLLITEM = 1 ", "HAVING SUM(QTY - ISSQTY) >0")
            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSTOCK_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSTOCK.CellDoubleClick
        EDITROW(e)
    End Sub

    Sub EDITROW(ByVal e As DataGridViewCellEventArgs)
        Try
            If e.RowIndex < 0 Then Exit Sub

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If GRIDSTOCK.Item(GOUTMTRS.Index, e.RowIndex).Value > 0 Or GRIDSTOCK.Item(GOUTWT.Index, e.RowIndex).Value > 0 Then
                MsgBox("Rolls Locked, it is used further", MsgBoxStyle.Critical)
                Exit Sub
            End If


            txtsrno.Text = GRIDSTOCK.Item(GGRIDSRNO.Index, GRIDSTOCK.CurrentRow.Index).Value.ToString
            TXTNO.Text = GRIDSTOCK.Item(GBEAMSTOCKNO.Index, GRIDSTOCK.CurrentRow.Index).Value.ToString
            CMBGODOWN.Text = GRIDSTOCK.Item(GGODOWN.Index, e.RowIndex).Value.ToString
            CMBNAME.Text = GRIDSTOCK.Item(GNAME.Index, e.RowIndex).Value.ToString
            CMBMILL.Text = GRIDSTOCK.Item(GMILL.Index, e.RowIndex).Value.ToString
            TXTBEAMNO.Text = Val(GRIDSTOCK.Item(GBEAMNO.Index, e.RowIndex).Value)
            TXTBEAMNAME.Text = GRIDSTOCK.Item(GBEAMNAME.Index, e.RowIndex).Value
            TXTTOTALENDS.Text = Val(GRIDSTOCK.Item(GTOTALENDS.Index, e.RowIndex).Value)
            TXTTOTALMTRS.Text = Val(GRIDSTOCK.Item(GTOTALMTRS.Index, e.RowIndex).Value)
            TXTGAMANO.Text = Val(GRIDSTOCK.Item(GGAMANO.Index, e.RowIndex).Value)
            TXTSECTION.Text = Val(GRIDSTOCK.Item(GSECTION.Index, e.RowIndex).Value)
            CMBROLLNO.Text = GRIDSTOCK.Item(GROLLNO.Index, e.RowIndex).Value.ToString

            TXTBEAMWT.Text = Val(GRIDSTOCK.Item(GBEAMWT.Index, e.RowIndex).Value)
            TXTBREAKAGE.Text = Val(GRIDSTOCK.Item(GBREAKAGE.Index, e.RowIndex).Value)
            TXTREMARKS.Text = GRIDSTOCK.Item(GREMARKS.Index, e.RowIndex).Value.ToString

            GRIDDOUBLECLICK = True
            EDIT = True
            TEMPROW = e.RowIndex
            CMBNAME.Focus()

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

                    If GRIDSTOCK.Item(GOUTMTRS.Index, GRIDSTOCK.CurrentRow.Index).Value > 0 Or GRIDSTOCK.Item(GOUTWT.Index, GRIDSTOCK.CurrentRow.Index).Value > 0 Then
                        MsgBox("Rolls Locked, it is used further", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    Dim TEMPMSG As Integer = MsgBox("Delete Details", MsgBoxStyle.YesNo)
                    If TEMPMSG = vbYes Then
                        Dim ALPARAVAL As New ArrayList
                        Dim OBJNO As New ClsOpeningBeamStock

                        OBJNO.alParaval = ALPARAVAL
                        ALPARAVAL.Add(Val(GRIDSTOCK.CurrentRow.Cells(GBEAMSTOCKNO.Index).Value))
                        ALPARAVAL.Add(YearId)

                        Dim INTRES As DataTable = OBJNO.DELETE()
                        GRIDSTOCK.Rows.RemoveAt(GRIDSTOCK.CurrentRow.Index)
                        GETSRNO(GRIDSTOCK)
                        txtsrno.Text = GRIDSTOCK.RowCount + 1
                        TOTAL()

                    End If

                End If
            End If
        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub TXTROLLS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTOTALENDS.KeyPress, TXTTOTALMTRS.KeyPress, TXTGAMANO.KeyPress, TXTSECTION.KeyPress, TXTBEAMWT.KeyPress, TXTBREAKAGE.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTBEAMNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTBEAMNO.Validating
        If Val(TXTBEAMNO.Text) > 0 And GRIDSTOCK.RowCount > 0 Then
            If Not CHECKBEAM() Then
                MsgBox("Beam No already Present in Grid below")
                TXTBEAMNO.Clear()
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

    Function CHECKBEAM() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Or GRIDDOUBLECLICK = False Then
                    If TXTBEAMNO.Text.Trim = ROW.Cells(GBEAMNO.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function CHECKROLL() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDSTOCK.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Or GRIDDOUBLECLICK = False Then
                    If CMBROLLNO.Text.Trim = ROW.Cells(GROLLNO.Index).Value.ToString Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub CMBROLLNO_Validating(sender As Object, e As CancelEventArgs) Handles CMBROLLNO.Validating
        If CMBROLLNO.Text <> "" And GRIDSTOCK.RowCount > 0 Then
            If Not CHECKROLL() Then
                MsgBox("Roll No already Present in Grid below")
                CMBROLLNO.Text = ""
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub
End Class