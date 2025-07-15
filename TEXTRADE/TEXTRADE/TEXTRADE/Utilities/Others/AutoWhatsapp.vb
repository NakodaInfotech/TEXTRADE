Imports BL
Public Class AutoWhatsapp
    Dim GRIDDOUBLECLICK As Boolean
    Dim EDIT As Boolean
    Dim TEMPROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If CMBTYPE.Text.Trim.Length = 0 Then
            EP.SetError(CMBTYPE, "Select Type")
            bln = False
        End If
        Return bln
    End Function
    Sub SAVE()
        Try
            If ISLOCKYEAR = True Then
                MsgBox("Unable to Make changes, Year is Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            Dim OBJSM As New ClsAUTOWHATSAPP
            If EDIT = False Then
                alParaval.Add(CMBTYPE.Text.Trim)
                If CHKMONDAY.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                If CHKTUESDAY.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                If CHKWEDNESDAY.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                If CHKTHURSDAY.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                If CHKFRIDAY.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                If CHKSATURDAY.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
                If CHKSUNDAY.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)

                alParaval.Add(TXTTIME.Text.Trim)
                alParaval.Add(CmpId)
                alParaval.Add(Userid)
            Else
                'GRID REPORT
                Dim NAME As String = ""

                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAME = "" Then
                            NAME = dtrow("NAME")
                        Else
                            NAME = NAME & "|" & dtrow("NAME")
                        End If
                    End If
                Next
                alParaval.Add(NAME)

                Dim AGENTNAME As String = ""

                For i As Integer = 0 To GridView1.RowCount - 1
                    Dim dtrow As DataRow = GridView1.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If AGENTNAME = "" Then
                            AGENTNAME = dtrow("AGENTNAME")
                        Else
                            AGENTNAME = AGENTNAME & "|" & dtrow("AGENTNAME")
                        End If
                    End If
                Next
                alParaval.Add(AGENTNAME)
            End If
            OBJSM.alParaval = alParaval

            If GRIDDOUBLECLICK = False Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = OBJSM.SAVE()
                If DT.Rows.Count > 0 Then TXTSRNO.Text = DT.Rows(0).Item(0)
            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                'alParaval.Add(TXTSRNO.Text.Trim)
                Dim INTRES As Integer = OBJSM.UPDATE()
            End If
            'OBJCONFIG.alParaval = alParaval
            'IntResult = OBJCONFIG.SAVE()
            MsgBox("Details Added")
            CLEAR()
            CMBTYPE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLGRID()

        GRIDAUTOWA.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDAUTOWA.Rows.Add(Val(TXTSRNO.Text.Trim), CMBTYPE.Text.Trim, CHKMONDAY.Checked, CHKTUESDAY.Checked, CHKWEDNESDAY.Checked, CHKTHURSDAY.Checked, CHKFRIDAY.Checked, CHKSATURDAY.Checked, CHKSUNDAY.Checked, TXTTIME.Text.Trim)
            getsrno(GRIDAUTOWA)
            GRIDAUTOWA.FirstDisplayedScrollingRowIndex = GRIDAUTOWA.RowCount - 1
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDAUTOWA.Item(GSRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDAUTOWA.Item(GTYPE.Index, TEMPROW).Value = CMBTYPE.Text
            GRIDAUTOWA.Item(GMON.Index, TEMPROW).Value = CHKMONDAY.Checked
            GRIDAUTOWA.Item(GTUE.Index, TEMPROW).Value = CHKTUESDAY.Checked
            GRIDAUTOWA.Item(GWED.Index, TEMPROW).Value = CHKWEDNESDAY.Checked
            GRIDAUTOWA.Item(GTHU.Index, TEMPROW).Value = CHKTHURSDAY.Checked
            GRIDAUTOWA.Item(GFRI.Index, TEMPROW).Value = CHKFRIDAY.Checked
            GRIDAUTOWA.Item(GSAT.Index, TEMPROW).Value = CHKSATURDAY.Checked
            GRIDAUTOWA.Item(GSUN.Index, TEMPROW).Value = CHKSUNDAY.Checked
            GRIDAUTOWA.Item(GTIME.Index, TEMPROW).Value = TXTTIME.Text.Trim
            GRIDDOUBLECLICK = False
        End If
        getsrno(GRIDAUTOWA)
        GRIDAUTOWA.FirstDisplayedScrollingRowIndex = GRIDAUTOWA.RowCount - 1

        '   TXTSRNO.Text = GRIDINSURANCE.RowCount + 1

        TXTSRNO.Text = GRIDAUTOWA.RowCount + 1
        TXTTIME.Clear()
        CMBTYPE.Text = ""
        CHKMONDAY.CheckState = CheckState.Unchecked
        CHKTUESDAY.CheckState = CheckState.Unchecked
        CHKWEDNESDAY.CheckState = CheckState.Unchecked
        CHKTHURSDAY.CheckState = CheckState.Unchecked
        CHKFRIDAY.CheckState = CheckState.Unchecked
        CHKSATURDAY.CheckState = CheckState.Unchecked
        CHKSUNDAY.CheckState = CheckState.Unchecked


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

    Private Sub AutoWhatsapp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'OPENING'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            CLEAR()

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.Execute_Any_String(" SELECT   AUTOWA_TYPE AS TYPE, ISNULL(AUTOWHATSAPP.AUTOWA_MON,0) AS MON, ISNULL(AUTOWHATSAPP.AUTOWA_TUE,0) AS TUE, ISNULL(AUTOWHATSAPP.AUTOWA_WED,0) AS WED, ISNULL(AUTOWHATSAPP.AUTOWA_THU,0) AS THU, ISNULL(AUTOWHATSAPP.AUTOWA_FRI,0) AS FRI, ISNULL(AUTOWHATSAPP.AUTOWA_SAT,0) AS SAT, ISNULL(AUTOWHATSAPP.AUTOWA_SUN,0)  AS SUN, AUTOWA_TIME AS TIME FROM AUTOWHATSAPP WHERE AUTOWA_CMPID = " & CmpId & "", "", "")
            If dttable.Rows.Count > 0 Then
                GRIDAUTOWA.RowCount = 0
                For Each DR As DataRow In dttable.Rows
                    GRIDAUTOWA.Rows.Add(0, DR("TYPE"), DR("MON"), DR("TUE"), DR("WED"), DR("THU"), DR("FRI"), DR("SAT"), DR("SUN"), DR("TIME"))
                Next
                getsrno(GRIDAUTOWA)
                GRIDAUTOWA.FirstDisplayedScrollingRowIndex = GRIDAUTOWA.RowCount - 1
            End If
            FILLCMB()
            If GRIDAUTOWA.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDAUTOWA.Rows(GRIDAUTOWA.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTSRNO.Text = 1
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTTIME_Validated(sender As Object, e As EventArgs) Handles TXTTIME.Validated
        Try
            If CMBTYPE.Text <> "" And TXTTIME.Text.Trim <> "" Then
                'SAVE()
                FILLGRID()
                CLEAR()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CLEAR()
        Try
            TXTSRNO.Text = GRIDAUTOWA.RowCount + 1
            TXTTIME.Clear()
            CMBTYPE.Text = ""
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
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row Is In Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                Dim TEMPMSG As Integer = MsgBox("Wish To Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                'DELETE FROM TRANSPORT INSURANCE
                Dim OBJSM As New ClsTransInsurance
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(GRIDAUTOWA.Rows(GRIDAUTOWA.CurrentRow.Index).Cells(GSRNO.Index).Value)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)

                OBJSM.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJSM.DELETE()

                GRIDAUTOWA.Rows.RemoveAt(GRIDAUTOWA.CurrentRow.Index)
                getsrno(GRIDAUTOWA)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub GRIDAUTOWA_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDAUTOWA.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub CMDSAVE_Click(sender As Object, e As EventArgs) Handles CMDSAVE.Click
        Try
            Dim DTTABLE As DataTable
            If ISLOCKYEAR = True Then
                MsgBox("Unable to Make changes, Year is Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim INTRESULT As Integer

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alparaval As New ArrayList

            Dim GRIDSRNO As String = ""
            Dim TYPE As String = ""
            Dim MONDAY As String = ""
            Dim TUESDAY As String = ""
            Dim WEDNESDAY As String = ""
            Dim THURSDAY As String = ""
            Dim FRIDAY As String = ""
            Dim SATURDAY As String = ""
            Dim SUNDAY As String = ""
            Dim TIME As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDAUTOWA.Rows
                If row.Cells(GSRNO.Index).Value <> Nothing Then
                    If GRIDSRNO = "" Then

                        GRIDSRNO = Val(row.Cells(GSRNO.Index).Value)
                        TYPE = row.Cells(GTYPE.Index).Value.ToString
                        MONDAY = row.Cells(GMON.Index).Value
                        TUESDAY = row.Cells(GTUE.Index).Value
                        WEDNESDAY = row.Cells(GWED.Index).Value
                        THURSDAY = row.Cells(GTHU.Index).Value
                        FRIDAY = row.Cells(GFRI.Index).Value
                        SATURDAY = row.Cells(GSAT.Index).Value
                        SUNDAY = row.Cells(GSUN.Index).Value
                        TIME = row.Cells(GTIME.Index).Value.ToString

                    Else

                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
                        TYPE = TYPE & "|" & row.Cells(GTYPE.Index).Value.ToString
                        MONDAY = MONDAY & "|" & row.Cells(GMON.Index).Value
                        TUESDAY = TUESDAY & "|" & row.Cells(GTUE.Index).Value
                        WEDNESDAY = WEDNESDAY & "|" & row.Cells(GWED.Index).Value
                        THURSDAY = THURSDAY & "|" & row.Cells(GTHU.Index).Value
                        FRIDAY = FRIDAY & "|" & row.Cells(GFRI.Index).Value
                        SATURDAY = SATURDAY & "|" & row.Cells(GSAT.Index).Value
                        SUNDAY = SUNDAY & "|" & row.Cells(GSUN.Index).Value
                        TIME = TIME & "|" & row.Cells(GTIME.Index).Value.ToString


                    End If
                End If
            Next


            alparaval.Add(GRIDSRNO)
            alparaval.Add(TYPE)
            alparaval.Add(MONDAY)
            alparaval.Add(TUESDAY)
            alparaval.Add(WEDNESDAY)
            alparaval.Add(THURSDAY)
            alparaval.Add(FRIDAY)
            alparaval.Add(SATURDAY)
            alparaval.Add(SUNDAY)
            alparaval.Add(TIME)
            alparaval.Add(CmpId)
            alparaval.Add(Userid)


            Dim CHK As String = ""
            Dim NAME As String = ""
            Dim CITY As String = ""

            For I As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(I)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If NAME = "" Then
                        CHK = dtrow("CHK")
                        NAME = dtrow("NAME")
                        CITY = dtrow("CITY")
                    Else
                        CHK = CHK & "|" & If(Convert.ToBoolean(dtrow("CHK")), "1", "0")
                        NAME = NAME & "|" & dtrow("NAME")
                        CITY = CITY & "|" & dtrow("CITY")
                    End If
                End If
            Next

            alparaval.Add(CHK)
            alparaval.Add(NAME)
            alparaval.Add(CITY)

            Dim ACHK As String = ""
            Dim ANAME As String = ""
            Dim ACITY As String = ""

            For I As Integer = 0 To GridView1.RowCount - 1
                Dim dtrow As DataRow = GridView1.GetDataRow(I)
                If Convert.ToBoolean(dtrow("AGENTCHK")) = True Then
                    If ANAME = "" Then
                        ACHK = dtrow("AGENTCHK")
                        ANAME = dtrow("AGENTNAME")
                        ACITY = dtrow("AGENTCITY")
                    Else
                        ACHK = ACHK & "|" & If(Convert.ToBoolean(dtrow("AGENTCHK")), "1", "0")
                        ANAME = ANAME & "|" & dtrow("AGENTNAME")
                        ACITY = ACITY & "|" & dtrow("AGENTCITY")
                    End If
                End If
            Next

            alparaval.Add(ACHK)
            alparaval.Add(ANAME)
            alparaval.Add(ACITY)

            Dim OBJAUTOWA As New ClsAUTOWHATSAPP
            OBJAUTOWA.alParaval = alparaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                DTTABLE = OBJAUTOWA.SAVE()
                MessageBox.Show("Details Added")
                TXTSRNO.Text = DTTABLE.Rows(0).Item(0)
            Else
                'If USEREDIT = False Then
                '    MsgBox("Insufficient Rights")
                '    Exit Sub
                'End If
                'alparaval.Add(LOTTAGNO)
                'Dim IntResult As Integer = OBJAUTOWA.UPDATE()
                'MsgBox("Details Updated")
            End If
            CLEAR()
            EDIT = False


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDAUTOWA.CurrentRow.Index >= 0 And GRIDAUTOWA.Item(GSRNO.Index, GRIDAUTOWA.CurrentRow.Index).Value <> Nothing Then


                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDAUTOWA.Item(GSRNO.Index, GRIDAUTOWA.CurrentRow.Index).Value.ToString
                CMBTYPE.Text = GRIDAUTOWA.Item(GTYPE.Index, GRIDAUTOWA.CurrentRow.Index).Value.ToString
                CHKMONDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GMON.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKTUESDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GTUE.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKWEDNESDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GWED.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKTHURSDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GTHU.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKFRIDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GFRI.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKSATURDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GSAT.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                CHKSUNDAY.Checked = Convert.ToBoolean(GRIDAUTOWA.Item(GSUN.Index, GRIDAUTOWA.CurrentRow.Index).Value)
                TXTTIME.Text = GRIDAUTOWA.Item(GTIME.Index, GRIDAUTOWA.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDAUTOWA.CurrentRow.Index
                TXTSRNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLCMB()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DTUNIT As DataTable = OBJCMN.SEARCH("CASE When ISNULL(LEDGERS.Acc_cmpname,'') = '' THEN  CAST (0 AS BIT) ELSE  CAST (0 AS BIT) END AS CHK, LEDGERS.Acc_cmpname AS NAME , ISNULL(CITYMASTER.city_name,'') AS CITY ", " ", " LEDGERS LEFT OUTER JOIN  CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id ", " AND  Acc_TYPE = 'ACCOUNTS' AND LEDGERS.ACC_YEARID = '" & YearId & "' ORDER BY LEDGERS.Acc_cmpname")
            gridbilldetails.DataSource = DTUNIT
            If DTUNIT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
            Dim DT As DataTable = OBJCMN.SEARCH("CASE When ISNULL(LEDGERS.Acc_cmpname,'') = '' THEN  CAST (0 AS BIT) ELSE  CAST (0 AS BIT) END AS AGENTCHK, LEDGERS.Acc_cmpname AS AGENTNAME , ISNULL(CITYMASTER.city_name,'') AS AGENTCITY ", " ", " LEDGERS LEFT OUTER JOIN  CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id ", " AND  Acc_TYPE = 'AGENT' AND LEDGERS.ACC_YEARID = '" & YearId & "' ORDER BY LEDGERS.Acc_cmpname")
            GridControl1.DataSource = DT
            If DT.Rows.Count > 0 Then
                GridView1.FocusedRowHandle = GridView1.RowCount - 1
                GridView1.TopRowIndex = GridView1.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTYPE_Validated(sender As Object, e As EventArgs) Handles CMBTYPE.Validated
        If CMBTYPE.Text <> "" Then
            TXTTYPE1.Text = CMBTYPE.Text
            TXTTYPE2.Text = CMBTYPE.Text
        End If
    End Sub
End Class