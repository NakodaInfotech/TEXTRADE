Imports BL
Imports System.ComponentModel

Public Class TransportInsuranceDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Dim tempId As Integer
    Public TEMPTINO, tempPolicyNo As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        If CMBCHARGES.Text.Trim = "" Then FILLNAME(CMBCHARGES, EDIT, " AND (GROUPMASTER.GROUP_SECONDARY ='Indirect Income' OR GROUPMASTER.GROUP_SECONDARY ='Sales A/C' OR GROUPMASTER.GROUP_SECONDARY ='Indirect Expenses' or GROUPMASTER.GROUP_SECONDARY ='Direct Income' OR GROUPMASTER.GROUP_SECONDARY ='Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY ='Duties & Taxes')")
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True

        If EDIT = False Or (EDIT = True And tempPolicyNo <> TXTPOLICYNO.Text.Trim) Then
            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.SEARCH("TI_POLICYNO", "", "TRANSPORTINSURANCE", " and TI_POLICYNO = '" & TXTPOLICYNO.Text.Trim & "' And TI_YEARID = " & YearId)
            If dttable.Rows.Count > 0 Then
                EP.SetError(TXTPOLICYNO, "Policy No Already Exist")
                bln = False
            End If
        End If

        Return bln
    End Function

    Private Sub TransportInsuranceDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'OPENING'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            CLEAR()
            fillcmb()
            CMBCALC.SelectedIndex = 0
            'If USERGODOWN <> "" Then cmbgodown.Text = USERGODOWN Else cmbgodown.Text = ""
            'openingdate.Value = AccFrom.Date

            'cmbpiecetype.Text = "FRESH"


            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.Execute_Any_String(" SELECT TRANSPORTINSURANCE.TI_NO AS TINO , ISNULL(TRANSPORTINSURANCE.TI_SRNO, 0) AS GRIDSRNO, TRANSPORTINSURANCE.TI_DATE AS DATE, ISNULL(TRANSPORTINSURANCE.TI_INSURERNAME, '') AS INSURERNAME, ISNULL(TRANSPORTINSURANCE.TI_POLICYNO, '') AS POLICYNO, ISNULL(LEDGERS.Acc_cmpname, '') AS LEDGER, ISNULL(TRANSPORTINSURANCE.TI_PERCENT, 0) AS [PERCENT],  ISNULL(TRANSPORTINSURANCE.TI_CALCON, '') AS CALCON FROM  TRANSPORTINSURANCE LEFT OUTER JOIN LEDGERS ON TRANSPORTINSURANCE.TI_LEDGERID = LEDGERS.Acc_id WHERE  TRANSPORTINSURANCE.TI_YEARID = " & YearId & " ORDER BY TI_NO", "", "")
            If dttable.Rows.Count > 0 Then
                GRIDINSURANCE.RowCount = 0
                For Each DR As DataRow In dttable.Rows
                    GRIDINSURANCE.Rows.Add(Val(DR("GRIDSRNO")), DR("TINO"), Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy"), DR("INSURERNAME"), DR("POLICYNO"), DR("LEDGER"), Format(Val(DR("PERCENT")), "0.00"), DR("CALCON"))
                Next
                getsrno(GRIDINSURANCE)
                GRIDINSURANCE.FirstDisplayedScrollingRowIndex = GRIDINSURANCE.RowCount - 1
            End If

            If GRIDINSURANCE.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDINSURANCE.Rows(GRIDINSURANCE.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTSRNO.Text = 1
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBCHARGES.Text.Trim = "" Then FILLNAME(CMBCHARGES, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Sales A/C' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses')")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Function CHECKDUPLICATE() As Boolean
        Try
            Dim bln As Boolean = True
            For Each row As DataGridViewRow In GRIDINSURANCE.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> row.Index) Or GRIDDOUBLECLICK = False Then
                    If TXTPOLICYNO.Text.Trim = row.Cells(GPOLICYNO.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub EDITROW()
        Try
            If GRIDINSURANCE.CurrentRow.Index >= 0 And GRIDINSURANCE.Item(GSRNO.Index, GRIDINSURANCE.CurrentRow.Index).Value <> Nothing Then


                GRIDDOUBLECLICK = True
                TXTNO.Text = GRIDINSURANCE.Item(GNO.Index, GRIDINSURANCE.CurrentRow.Index).Value.ToString
                TXTSRNO.Text = GRIDINSURANCE.Item(GSRNO.Index, GRIDINSURANCE.CurrentRow.Index).Value.ToString
                TXTINSURER.Text = GRIDINSURANCE.Item(GINSURER.Index, GRIDINSURANCE.CurrentRow.Index).Value.ToString
                TXTPOLICYNO.Text = GRIDINSURANCE.Item(GPOLICYNO.Index, GRIDINSURANCE.CurrentRow.Index).Value.ToString
                CMBCHARGES.Text = GRIDINSURANCE.Item(GLEDGER.Index, GRIDINSURANCE.CurrentRow.Index).Value.ToString
                TXTPER.Text = GRIDINSURANCE.Item(GPER.Index, GRIDINSURANCE.CurrentRow.Index).Value.ToString
                CMBCALC.Text = GRIDINSURANCE.Item(GCALCON.Index, GRIDINSURANCE.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDINSURANCE.CurrentRow.Index
                TXTSRNO.Focus()
            End If
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
            Dim OBJSM As New ClsTransInsurance

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)
            ALPARAVAL.Add(0)


            ALPARAVAL.Add(TXTSRNO.Text.Trim)
            ALPARAVAL.Add(Format(Convert.ToDateTime(WEFDATE.Text).Date, "MM/dd/yyyy"))

            ALPARAVAL.Add(TXTINSURER.Text.Trim)
            ALPARAVAL.Add(TXTPOLICYNO.Text.Trim)
            ALPARAVAL.Add(CMBCHARGES.Text.Trim)
            ALPARAVAL.Add(TXTPER.Text.Trim)
            ALPARAVAL.Add(CMBCALC.Text.Trim)


            OBJSM.alParaval = ALPARAVAL
            If GRIDDOUBLECLICK = False Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = OBJSM.SAVE()
                If DT.Rows.Count > 0 Then TXTNO.Text = DT.Rows(0).Item(0)
                'BARCODE()
            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                ALPARAVAL.Add(TXTNO.Text.Trim)
                Dim INTRES As Integer = OBJSM.UPDATE()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TransportInsuranceDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub CMBCHARGES_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Enter
        Try
            If CMBCHARGES.Text.Trim = "" Then FILLNAME(CMBCHARGES, EDIT, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Sales A/C' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCHARGES.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" Then NAMEVALIDATE(CMBCHARGES, CMBCODE, e, Me, TXTADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Sales A/C' )")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        GRIDINSURANCE.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDINSURANCE.Rows.Add(Val(TXTSRNO.Text.Trim), TXTNO.Text.Trim, WEFDATE.Text, TXTINSURER.Text.Trim, TXTPOLICYNO.Text.Trim, CMBCHARGES.Text.Trim, Format(Val(TXTPER.Text.Trim), "0.00"), CMBCALC.Text.Trim)
            getsrno(GRIDINSURANCE)
            GRIDINSURANCE.FirstDisplayedScrollingRowIndex = GRIDINSURANCE.RowCount - 1
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDINSURANCE.Item(GSRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDINSURANCE.Item(GDATE.Index, TEMPROW).Value = WEFDATE.Text
            GRIDINSURANCE.Item(GINSURER.Index, TEMPROW).Value = TXTINSURER.Text.Trim
            GRIDINSURANCE.Item(GPOLICYNO.Index, TEMPROW).Value = TXTPOLICYNO.Text.Trim
            GRIDINSURANCE.Item(GLEDGER.Index, TEMPROW).Value = CMBCHARGES.Text.Trim
            GRIDINSURANCE.Item(GPER.Index, TEMPROW).Value = Format(Val(TXTPER.Text.Trim), "0.00")
            GRIDINSURANCE.Item(GCALCON.Index, TEMPROW).Value = CMBCALC.Text.Trim
            GRIDDOUBLECLICK = False
        End If


        '   TXTSRNO.Text = GRIDINSURANCE.RowCount + 1
        TXTINSURER.Clear()
        TXTPOLICYNO.Clear()
        CMBCHARGES.Text = ""
        CMBCALC.Text = ""
        TXTNO.Clear()
        WEFDATE.Text = Now.Date
        WEFDATE.Focus()

    End Sub

    Sub GETSRNO(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(GSRNO.Index).Value = row.Index + 1
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            TXTSRNO.Text = GRIDINSURANCE.RowCount + 1
            TXTINSURER.Clear()
            TXTPOLICYNO.Clear()
            CMBCHARGES.Text = ""
            CMBCALC.Text = ""
            TXTNO.Clear()
            WEFDATE.Text = Now.Date
            WEFDATE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSRNO_GotFocus(sender As Object, e As EventArgs) Handles TXTSRNO.GotFocus
        If GRIDDOUBLECLICK = False Then
            If GRIDINSURANCE.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDINSURANCE.Rows(GRIDINSURANCE.RowCount - 1).Cells(GSRNO.Index).Value) + 1
            Else
                TXTSRNO.Text = 1
            End If
        End If
    End Sub

    Private Sub GRIDINSURANCE_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDINSURANCE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDINSURANCE.RowCount > 0 Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                Dim TEMPMSG As Integer = MsgBox("Wish To Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                'DELETE FROM TRANSPORT INSURANCE
                Dim OBJSM As New ClsTransInsurance
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(GRIDINSURANCE.Rows(GRIDINSURANCE.CurrentRow.Index).Cells(GNO.Index).Value)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)

                OBJSM.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJSM.DELETE()

                GRIDINSURANCE.Rows.RemoveAt(GRIDINSURANCE.CurrentRow.Index)
                GETSRNO(GRIDINSURANCE)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDINSURANCE_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDINSURANCE.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub TXTPER_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTPER.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCALC_Validated(sender As Object, e As EventArgs) Handles CMBCALC.Validated
        Try
            If WEFDATE.Text <> "__/__/____" And TXTPOLICYNO.Text.Trim <> "" And CMBCHARGES.Text.Trim <> "" And Val(TXTPER.Text.Trim) > 0 And TXTINSURER.Text.Trim <> "" And CMBCALC.Text.Trim <> "" Then
                If Not CHECKWEFDATE() Then
                    MsgBox("W.E.F. Date already Present in Grid below")
                    Exit Sub
                End If

                If Not CHECKDUPLICATE() Then
                    MsgBox("Policy No Already Exists")
                    TXTPOLICYNO.Focus()
                    Exit Sub
                End If
                SAVE()

                FILLGRID()
                CLEAR()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKWEFDATE() As Boolean
        Try
            Dim bln As Boolean = True
            For Each ROW As DataGridViewRow In GRIDINSURANCE.Rows
                If (GRIDDOUBLECLICK = True And TEMPROW <> ROW.Index) Or GRIDDOUBLECLICK = False Then
                    If WEFDATE.Text = ROW.Cells(GDATE.Index).Value Then bln = False
                End If
            Next
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class