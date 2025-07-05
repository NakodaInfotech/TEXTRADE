
Imports BL

Public Class GroupSummaryMonthly

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub FILLGRID()
        Try
            gridregister.RowCount = 0

            Dim OBJGROUP As New ClsGroupRegister
            Dim ALPARAVAL As New ArrayList
            Dim DT As DataTable

            ALPARAVAL.Add(CMBNAME.Text.Trim)
            If chkdate.CheckState = CheckState.Checked Then
                ALPARAVAL.Add(1)
                ALPARAVAL.Add(dtfrom.Value.Date)
                ALPARAVAL.Add(dtto.Value.Date)
            Else
                ALPARAVAL.Add(0)
                ALPARAVAL.Add(AccFrom)
                ALPARAVAL.Add(AccTo)
            End If
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(YearId)

            OBJGROUP.alParaval = ALPARAVAL
            DT = OBJGROUP.GROUPMONTHLY()

            If DT.Rows.Count > 0 Then
                For Each DTROW As DataRow In DT.Rows
                    gridregister.Rows.Add(MonthName(DTROW(0)), Format(Val(DTROW("DEBIT")), "0.00"), Format(Val(DTROW("CREDIT")), "0.00"), 0.0)
                Next
            End If

            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            txtdr.Text = "0.00"
            txtcr.Text = "0.00"
            txtclosing.Text = "0.00"
            txtopening.Text = "0.00"

            'GETTING OP BAL
            Dim objcommon As New ClsCommon
            Dim DT As DataTable = objcommon.SEARCH(" CASE WHEN SUM(T.OPBALDR)-SUM(T.OPBALCR) > 0 THEN  SUM(T.OPBALDR)-SUM(T.OPBALCR) ELSE SUM(T.OPBALCR)-SUM(T.OPBALDR) END AS OPBAL, CASE WHEN SUM(T.OPBALDR)-SUM(T.OPBALCR) > 0 THEN  'Dr.' ELSE 'Cr.' END AS DRCR ", "", " (SELECT ISNULL(SUM(Acc_opbal),0) AS OPBALDR, 0 AS OPBALCR FROM LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.ACC_GROUPID = GROUPMASTER.GROUP_ID WHERE GROUP_NAME = '" & CMBNAME.Text.Trim & "' AND LEDGERS.Acc_yearid = " & YearId & " AND Acc_drcr = 'Dr.' UNION ALL SELECT 0 AS OPBALDR, ISNULL(SUM(Acc_opbal),0) AS OPBALCR FROM LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.ACC_GROUPID = GROUPMASTER.GROUP_ID WHERE GROUP_NAME = '" & CMBNAME.Text.Trim & "' AND LEDGERS.Acc_yearid = " & YearId & " AND Acc_drcr = 'Cr.') AS T ", "")
            If dt.Rows.Count > 0 Then
                txtopening.Text = Format(Val(DT.Rows(0).Item("OPBAL").ToString), "0.00")
                txtclosing.Text = Format(Val(txtopening.Text), "0.00")
                lbldrcr.Text = DT.Rows(0).Item("DRCR").ToString
                lblopdrcr.Text = DT.Rows(0).Item("DRCR").ToString
            End If

            If lbldrcr.Text = "Cr." And Val(txtclosing.Text) > 0 Then txtclosing.Text = Val(txtclosing.Text) * (-1)


            For Each row As DataGridViewRow In gridregister.Rows
                txtdr.Text = Format(Val(txtdr.Text) + Val(row.Cells(1).Value), "0.00")
                txtcr.Text = Format(Val(txtcr.Text) + Val(row.Cells(2).Value), "0.00")


                txtclosing.Text = Format(Val(txtclosing.Text) + Val(Val(row.Cells(1).Value) - Val(row.Cells(2).Value)), "0.00")

                If Val(txtclosing.Text) >= 0 Then
                    If Val(Val(row.Cells(1).Value) - Val(row.Cells(2).Value)) < 0 Then
                        row.Cells(3).Value = Format(Val(txtclosing.Text), "0.00") & " Dr"
                    Else
                        row.Cells(3).Value = Format(Val(txtclosing.Text), "0.00") & " Dr"
                    End If
                ElseIf Val(txtclosing.Text) < 0 Then
                    If Val(Val(row.Cells(1).Value) - Val(row.Cells(2).Value)) >= 0 Then
                        row.Cells(3).Value = Format(Val(txtclosing.Text) * (-1), "0.00") & " Cr"
                    Else
                        row.Cells(3).Value = Format(Val(txtclosing.Text) * (-1), "0.00") & " Cr"
                    End If
                End If
            Next
            If Val(txtclosing.Text) < 0 Then
                txtclosing.Text = Format(Val(txtclosing.Text) * (-1), "0.00")
                lbldrcr.Text = "Cr"
            Else
                lbldrcr.Text = "Dr"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkdate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkdate.CheckedChanged
        Try
            dtfrom.Enabled = chkdate.CheckState
            dtto.Enabled = chkdate.CheckState
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub dtto_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtto.Validated
        Try
            If chkdate.CheckState = CheckState.Checked Then FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            'OPEN BANK A/C AND BANK OD A/C
            If CMBNAME.Text.Trim = "" Then FILLGROUP(CMBNAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then GROUPVALIDATE(CMBNAME, e, Me)
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BankRegister_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim OBJREG As New registerdesign

            OBJREG.FRMSTRING = "GROUPSUMMMONTHLY"
            OBJREG.REG = CMBNAME.Text.Trim

            If chkdate.CheckState = CheckState.Checked Then
                OBJREG.CHECK = 1
                OBJREG.FROMDATE = dtfrom.Value.Date
                OBJREG.TODATE = dtto.Value.Date
                OBJREG.PERIOD = "GROUP SUMMARY MONTHLY (" & CMBNAME.Text.Trim & ") - " & Format(dtfrom.Value, "dd/MM/yyyy") & " - " & Format(dtto.Value, "dd/MM/yyyy")
            Else
                OBJREG.CHECK = 0
                OBJREG.FROMDATE = AccFrom
                OBJREG.TODATE = AccTo
                OBJREG.PERIOD = "GROUP SUMMARY MONTHLY (" & CMBNAME.Text.Trim & ") - " & Format(AccFrom.Date, "dd/MM/yyyy") & " - " & Format(AccTo.Date, "dd/MM/yyyy")
            End If
            If lblopdrcr.Text = "Dr." Then
                OBJREG.OPENING = Val(txtopening.Text)
            Else
                OBJREG.OPENING = -1 * Val(txtopening.Text)
            End If
            OBJREG.MdiParent = MDIMain
            OBJREG.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class