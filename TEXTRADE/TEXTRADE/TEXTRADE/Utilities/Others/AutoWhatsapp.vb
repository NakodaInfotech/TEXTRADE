
Imports BL
Imports DevExpress.CodeParser
Imports DevExpress.XtraGrid.Views.Base

Public Class AutoWhatsapp

    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer

    Private Sub AutoWhatsapp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FILLCMB()

            Dim OBJCMN As New ClsCommon
            Dim DTTABLE As DataTable = OBJCMN.Execute_Any_String(" SELECT  AUTOWA_GRIDSRNO AS GRIDSRNO, AUTOWA_TYPE AS TYPE, AUTOWA_SCHEDULER AS SCHEDULER, AUTOWA_SCHDATE AS SCHDATE, ISNULL(AUTOWHATSAPP.AUTOWA_MON,0) AS MON, ISNULL(AUTOWHATSAPP.AUTOWA_TUE,0) AS TUE, ISNULL(AUTOWHATSAPP.AUTOWA_WED,0) AS WED, ISNULL(AUTOWHATSAPP.AUTOWA_THU,0) AS THU, ISNULL(AUTOWHATSAPP.AUTOWA_FRI,0) AS FRI, ISNULL(AUTOWHATSAPP.AUTOWA_SAT,0) AS SAT, ISNULL(AUTOWHATSAPP.AUTOWA_SUN,0)  AS SUN, AUTOWA_TIME AS TIME, AUTOWA_ID AS NO FROM AUTOWHATSAPP WHERE AUTOWA_CMPID = " & CmpId & " ORDER BY AUTOWA_GRIDSRNO", "", "")
            If DTTABLE.Rows.Count > 0 Then
                GRIDAUTOWA.RowCount = 0
                For Each DR As DataRow In DTTABLE.Rows
                    GRIDAUTOWA.Rows.Add(DR("GRIDSRNO"), DR("TYPE"), DR("SCHEDULER"), DR("SCHDATE"), DR("MON"), DR("TUE"), DR("WED"), DR("THU"), DR("FRI"), DR("SAT"), DR("SUN"), DR("TIME"), DR("NO"))
                Next
                GRIDAUTOWA.FirstDisplayedScrollingRowIndex = GRIDAUTOWA.RowCount - 1
            End If

            TXTSRNO.Text = Val(GRIDAUTOWA.RowCount) + 1

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DTLEDGERS As DataTable = OBJCMN.SEARCH(" CAST(0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, ISNULL(CITYMASTER.city_name, '') AS CITY ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.ACC_GROUPID = GROUPMASTER.GROUP_ID LEFT JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id ", " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS') AND LEDGERS.Acc_TYPE = 'ACCOUNTS' AND LEDGERS.Acc_YEARID = " & YearId & " ORDER BY LEDGERS.ACC_CMPNAME")
            GRIDLEDGERDETAILS.DataSource = DTLEDGERS
            If DTLEDGERS.Rows.Count > 0 Then
                GRIDLEDGER.FocusedRowHandle = GRIDLEDGER.RowCount - 1
                GRIDLEDGER.TopRowIndex = GRIDLEDGER.RowCount - 15
            End If
            Dim DT As DataTable = OBJCMN.SEARCH(" CAST(0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME, ISNULL(CITYMASTER.city_name, '') AS CITY ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.ACC_GROUPID = GROUPMASTER.GROUP_ID LEFT JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id ", " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.Acc_TYPE = 'AGENT' AND LEDGERS.Acc_YEARID = " & YearId & " ORDER BY LEDGERS.ACC_CMPNAME")
            GRIDAGENTDETAILS.DataSource = DT
            If DT.Rows.Count > 0 Then
                GRIDAGENT.FocusedRowHandle = GRIDAGENT.RowCount - 1
                GRIDAGENT.TopRowIndex = GRIDAGENT.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTTIME_Validated(sender As Object, e As EventArgs) Handles DTTIME.Validated
        Try

            'CHECK DUPLICATION OF TYPE IN GRID
            If GRIDDOUBLECLICK = False Then
                For Each ROW As DataGridViewRow In GRIDAUTOWA.Rows
                    If LCase(ROW.Cells(GTYPE.Index).Value) = LCase(CMBTYPE.Text.Trim) Then Exit Sub
                Next
            End If

            If CMBTYPE.Text <> "" And (CHKMONDAY.Checked = True Or CHKTUESDAY.Checked = True Or CHKWEDNESDAY.Checked = True Or CHKTHURSDAY.Checked = True Or CHKFRIDAY.Checked = True Or CHKSATURDAY.Checked = True Or CHKSUNDAY.Checked = True) Then
                FILLGRID()
                CLEAR()
                CMBTYPE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        If GRIDDOUBLECLICK = False Then
            GRIDAUTOWA.Rows.Add(Val(TXTSRNO.Text.Trim), CMBTYPE.Text.Trim, CMBSCHEDULER.Text.Trim, SCHDATE.Value, CHKMONDAY.Checked, CHKTUESDAY.Checked, CHKWEDNESDAY.Checked, CHKTHURSDAY.Checked, CHKFRIDAY.Checked, CHKSATURDAY.Checked, CHKSUNDAY.Checked, DTTIME.Text.Trim, 0)
            GETSRNO(GRIDAUTOWA)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDAUTOWA.Item(GTYPE.Index, TEMPROW).Value = CMBTYPE.Text
            GRIDAUTOWA.Item(GSCHEDULER.Index, TEMPROW).Value = CMBSCHEDULER.Text
            GRIDAUTOWA.Item(GDATE.Index, TEMPROW).Value = SCHDATE.Value
            GRIDAUTOWA.Item(GMON.Index, TEMPROW).Value = CHKMONDAY.Checked
            GRIDAUTOWA.Item(GTUE.Index, TEMPROW).Value = CHKTUESDAY.Checked
            GRIDAUTOWA.Item(GWED.Index, TEMPROW).Value = CHKWEDNESDAY.Checked
            GRIDAUTOWA.Item(GTHU.Index, TEMPROW).Value = CHKTHURSDAY.Checked
            GRIDAUTOWA.Item(GFRI.Index, TEMPROW).Value = CHKFRIDAY.Checked
            GRIDAUTOWA.Item(GSAT.Index, TEMPROW).Value = CHKSATURDAY.Checked
            GRIDAUTOWA.Item(GSUN.Index, TEMPROW).Value = CHKSUNDAY.Checked
            GRIDAUTOWA.Item(GTIME.Index, TEMPROW).Value = DTTIME.Text.Trim
            GRIDDOUBLECLICK = False
        End If

        GETSRNO(GRIDAUTOWA)
        GRIDAUTOWA.FirstDisplayedScrollingRowIndex = GRIDAUTOWA.RowCount - 1

    End Sub

    Sub GETSRNO(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            TXTSRNO.Text = GRIDAUTOWA.RowCount + 1
            CMBTYPE.SelectedIndex = -1
            CMBTYPE.Enabled = True
            CHKMONDAY.CheckState = CheckState.Unchecked
            CHKTUESDAY.CheckState = CheckState.Unchecked
            CHKWEDNESDAY.CheckState = CheckState.Unchecked
            CHKTHURSDAY.CheckState = CheckState.Unchecked
            CHKFRIDAY.CheckState = CheckState.Unchecked
            CHKSATURDAY.CheckState = CheckState.Unchecked
            CHKSUNDAY.CheckState = CheckState.Unchecked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDAUTOWA_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDAUTOWA.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDAUTOWA.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row Is In Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                If MsgBox("Wish To Delete?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub

                GRIDAUTOWA.Rows.RemoveAt(GRIDAUTOWA.CurrentRow.Index)
                GETSRNO(GRIDAUTOWA)

            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDAUTOWA_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDAUTOWA.CellDoubleClick
        EDITROW()
    End Sub

    Sub EDITROW()
        Try
            If GRIDAUTOWA.CurrentRow.Index >= 0 And GRIDAUTOWA.Item(GSRNO.Index, GRIDAUTOWA.CurrentRow.Index).Value <> Nothing Then


                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDAUTOWA.Item(GSRNO.Index, GRIDAUTOWA.CurrentRow.Index).Value.ToString
                CMBTYPE.Text = GRIDAUTOWA.Item(GTYPE.Index, GRIDAUTOWA.CurrentRow.Index).Value.ToString
                CMBTYPE.Enabled = False
                CMBSCHEDULER.Text = GRIDAUTOWA.Item(GSCHEDULER.Index, GRIDAUTOWA.CurrentRow.Index).Value.ToString
                SCHDATE.Text = GRIDAUTOWA.Item(GDATE.Index, GRIDAUTOWA.CurrentRow.Index).Value.ToString
                CHKMONDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GMON.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKTUESDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GTUE.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKWEDNESDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GWED.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKTHURSDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GTHU.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKFRIDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GFRI.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKSATURDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GSAT.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKSUNDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GSUN.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                DTTIME.Text = GRIDAUTOWA.Item(GTIME.Index, GRIDAUTOWA.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDAUTOWA.CurrentRow.Index
                TXTSRNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True

        If GRIDAUTOWA.RowCount = 0 Then
            EP.SetError(CMBTYPE, "Select Type")
            bln = False
        End If
        Return bln
    End Function

    Private Sub CMDSAVE_Click(sender As Object, e As EventArgs) Handles CMDSAVE.Click
        Try
            Dim DTTABLE As DataTable
            If ISLOCKYEAR = True Then
                MsgBox("Unable to Make changes, Year is Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If


            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alparaval As New ArrayList

            Dim GRIDSRNO As String = ""
            Dim TYPE As String = ""
            Dim SCHEDULER As String = ""
            Dim SCHDATE As String = ""
            Dim MONDAY As String = ""
            Dim TUESDAY As String = ""
            Dim WEDNESDAY As String = ""
            Dim THURSDAY As String = ""
            Dim FRIDAY As String = ""
            Dim SATURDAY As String = ""
            Dim SUNDAY As String = ""
            Dim TIME As String = ""
            Dim WANO As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDAUTOWA.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If GRIDSRNO = "" Then

                        GRIDSRNO = Val(row.Cells(GSRNO.Index).Value)
                        TYPE = row.Cells(GTYPE.Index).Value.ToString
                        SCHEDULER = row.Cells(GSCHEDULER.Index).Value.ToString
                        SCHDATE = row.Cells(GDATE.Index).Value
                        MONDAY = row.Cells(GMON.Index).Value
                        TUESDAY = row.Cells(GTUE.Index).Value
                        WEDNESDAY = row.Cells(GWED.Index).Value
                        THURSDAY = row.Cells(GTHU.Index).Value
                        FRIDAY = row.Cells(GFRI.Index).Value
                        SATURDAY = row.Cells(GSAT.Index).Value
                        SUNDAY = row.Cells(GSUN.Index).Value
                        TIME = row.Cells(GTIME.Index).Value.ToString
                        WANO = Val(row.Cells(GNO.Index).Value)

                    Else

                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
                        TYPE = TYPE & "|" & row.Cells(GTYPE.Index).Value.ToString
                        SCHEDULER = SCHEDULER & "|" & row.Cells(GSCHEDULER.Index).Value.ToString
                        SCHDATE = SCHDATE & "|" & row.Cells(GDATE.Index).Value
                        MONDAY = MONDAY & "|" & row.Cells(GMON.Index).Value
                        TUESDAY = TUESDAY & "|" & row.Cells(GTUE.Index).Value
                        WEDNESDAY = WEDNESDAY & "|" & row.Cells(GWED.Index).Value
                        THURSDAY = THURSDAY & "|" & row.Cells(GTHU.Index).Value
                        FRIDAY = FRIDAY & "|" & row.Cells(GFRI.Index).Value
                        SATURDAY = SATURDAY & "|" & row.Cells(GSAT.Index).Value
                        SUNDAY = SUNDAY & "|" & row.Cells(GSUN.Index).Value
                        TIME = TIME & "|" & row.Cells(GTIME.Index).Value.ToString
                        WANO = WANO & "|" & Val(row.Cells(GNO.Index).Value)


                    End If
                End If
            Next


            alparaval.Add(GRIDSRNO)
            alparaval.Add(TYPE)
            alparaval.Add(SCHEDULER)
            alparaval.Add(SCHDATE)
            alparaval.Add(MONDAY)
            alparaval.Add(TUESDAY)
            alparaval.Add(WEDNESDAY)
            alparaval.Add(THURSDAY)
            alparaval.Add(FRIDAY)
            alparaval.Add(SATURDAY)
            alparaval.Add(SUNDAY)
            alparaval.Add(TIME)
            alparaval.Add(WANO)
            alparaval.Add(CmpId)
            alparaval.Add(Userid)

            Dim OBJAUTOWA As New ClsAUTOWHATSAPP
            OBJAUTOWA.alParaval = alparaval

            DTTABLE = OBJAUTOWA.SAVE()
            MessageBox.Show("Day & Time Details Added")

            AutoWhatsapp_Load(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDAUTOWA_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDAUTOWA.CellClick
        Try
            If e.RowIndex >= 0 Then
                TXTTYPE.Text = GRIDAUTOWA.CurrentRow.Cells(GTYPE.Index).Value

                Dim OBJCMN As New ClsCommon
                Dim DTLEDGER As DataTable = OBJCMN.SEARCH(" CASE WHEN AUTOWHATSAPP_DESC.AUTOWA_LEDGERID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS CHK, LEDGERS.ACC_CMPNAME AS NAME, ISNULL(CITYMASTER.city_name, '') AS CITY ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.ACC_GROUPID = GROUPMASTER.GROUP_ID LEFT JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN AUTOWHATSAPP_DESC ON LEDGERS.ACC_ID = AUTOWHATSAPP_DESC.AUTOWA_LEDGERID AND AUTOWHATSAPP_DESC.AUTOWA_TYPE = '" & GRIDAUTOWA.CurrentRow.Cells(GTYPE.Index).Value & "' AND AUTOWHATSAPP_DESC.AUTOWA_CMPID = " & CmpId, " And (GroupMaster.GROUP_SECONDARY = 'SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS') AND LEDGERS.Acc_TYPE = 'ACCOUNTS' AND LEDGERS.Acc_YEARID = " & YearId & " ORDER BY LEDGERS.ACC_CMPNAME ")
                GRIDLEDGERDETAILS.DataSource = DTLEDGER


                Dim DTAGENT As DataTable = OBJCMN.SEARCH(" CASE WHEN AUTOWHATSAPP_AGENTDESC.AUTOWA_AGENTID IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS CHK, LEDGERS.ACC_CMPNAME AS NAME, ISNULL(CITYMASTER.city_name, '') AS CITY ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.ACC_GROUPID = GROUPMASTER.GROUP_ID LEFT JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN AUTOWHATSAPP_AGENTDESC ON LEDGERS.ACC_ID = AUTOWHATSAPP_AGENTDESC.AUTOWA_AGENTID AND AUTOWHATSAPP_AGENTDESC.AUTOWA_TYPE = '" & GRIDAUTOWA.CurrentRow.Cells(GTYPE.Index).Value & "' AND AUTOWHATSAPP_AGENTDESC.AUTOWA_CMPID = " & CmpId, " And GroupMaster.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.Acc_TYPE = 'AGENT' AND LEDGERS.Acc_YEARID = " & YearId & " ORDER BY LEDGERS.ACC_CMPNAME ")
                GRIDAGENTDETAILS.DataSource = DTAGENT

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPDATE_Click(sender As Object, e As EventArgs) Handles CMDUPDATE.Click
        Try

            If GRIDAUTOWA.RowCount = 0 Then
                MsgBox("First Save the Day & Time Entry Then Update the Ledgers", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Val(GRIDAUTOWA.CurrentRow.Cells(GNO.Index).Value) = 0 Then
                MsgBox("First Save the Day & Time Entry Then Update the Ledgers", MsgBoxStyle.Critical)
                Exit Sub
            End If

            GRIDLEDGER.ClearColumnsFilter()
            GRIDAGENT.ClearColumnsFilter()

            UPDATELEDGERS()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub UPDATELEDGERS()
        Try

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList
            Dim OBJSM As New ClsAUTOWHATSAPP

            'GRID REPORT
            Dim NAME As String = ""
            For i As Integer = 0 To GRIDLEDGER.RowCount - 1
                Dim dtrow As DataRow = GRIDLEDGER.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If NAME = "" Then
                        NAME = dtrow("NAME")
                    Else
                        NAME = NAME & "|" & dtrow("NAME")
                    End If
                End If
            Next

            Dim AGENTNAME As String = ""
            For i As Integer = 0 To GRIDAGENT.RowCount - 1
                Dim dtrow As DataRow = GRIDAGENT.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If AGENTNAME = "" Then
                        AGENTNAME = dtrow("NAME")
                    Else
                        AGENTNAME = AGENTNAME & "|" & dtrow("NAME")
                    End If
                End If
            Next

            ALPARAVAL.Add(Val(GRIDAUTOWA.CurrentRow.Cells(GNO.Index).Value))
            ALPARAVAL.Add(TXTTYPE.Text.Trim)
            ALPARAVAL.Add(NAME)
            ALPARAVAL.Add(AGENTNAME)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            OBJSM.alParaval = ALPARAVAL

            Dim INT As Integer = OBJSM.UPDATELEDGERS()

            MsgBox("Ledgers Details Updated")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkagentall_CheckedChanged(sender As Object, e As EventArgs) Handles chkagentall.CheckedChanged
        Try
            For i As Integer = 0 To GRIDAGENT.RowCount - 1
                Dim dtrow As DataRow = GRIDAGENT.GetDataRow(i)
                dtrow("CHK") = chkagentall.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkall_CheckedChanged(sender As Object, e As EventArgs) Handles chkall.CheckedChanged
        Try
            For i As Integer = 0 To GRIDLEDGER.RowCount - 1
                Dim dtrow As DataRow = GRIDLEDGER.GetDataRow(i)
                dtrow("CHK") = chkall.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class