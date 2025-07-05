
Imports BL

Public Class SelectLedger

    Public STRSEARCH As String
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public TEMPNAME, TEMPCODE, TEMPTRANS, TEMPAGENT As String

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub SelectLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            FILLGRID("")
            cmbname.Text = "Name"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID(ByVal WHERECLAUSE As String)
        Try
            Dim OBJCMN As New ClsCommon
            'Dim DT As DataTable = OBJCMN.search("  ISNULL(LEDGERS.Acc_cmpname,'') AS LEDGERNAME, ISNULL(LEDGERS.Acc_code,'') AS CODE, ISNULL(GROUPMASTER.group_secondary,'') AS [GROUP], ISNULL(CITYMASTER.city_name,'') AS CITY, ISNULL(LEDGERS_1.Acc_cmpname,'') AS TRANSNAME, ISNULL(LEDGERS_2.Acc_cmpname,'') AS AGENT ", "", "    LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid AND LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_1.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_1.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_1.Acc_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_2.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_2.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_2.Acc_yearid LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id AND LEDGERS.Acc_cmpid = CITYMASTER.city_cmpid AND LEDGERS.Acc_locationid = CITYMASTER.city_locationid AND LEDGERS.Acc_yearid = CITYMASTER.city_yearid ", STRSEARCH & WHERECLAUSE & " AND LEDGERS.ACC_CMPID = " & CmpId & " AND LEDGERS.ACC_LOCATIONID = " & Locationid & " AND LEDGERS.ACC_YEARID = " & YearId & " order by LEDGERS.Acc_cmpname")
            Dim DT As DataTable = OBJCMN.SEARCH("  ISNULL(LEDGERS.Acc_cmpname, '') AS LEDGERNAME, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(AREAMASTER.area_name, '') AS AREA, ISNULL(LEDGERS.Acc_add, '') AS ADDRESS , ISNULL(GROUPMASTER.group_NAME, '') AS [GROUP], ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENT, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(COUNTRY_NAME,'') AS COUNTRY", "", "    LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN AREAMASTER ON LEDGERS.Acc_areaid = AREAMASTER.area_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON LEDGERS.ACC_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN COUNTRYMASTER ON LEDGERS.Acc_COUNTRYid = COUNTRYMASTER.COUNTRY_id ", STRSEARCH & WHERECLAUSE & " AND LEDGERS.ACC_YEARID = " & YearId & " order by LEDGERS.Acc_cmpname")

            GRIDLEDGER.DataSource = DT

            GRIDLEDGER.Columns(0).HeaderText = "Name"
            GRIDLEDGER.Columns(0).Width = 260

            GRIDLEDGER.Columns(1).HeaderText = "City"
            GRIDLEDGER.Columns(1).Width = 100

            GRIDLEDGER.Columns(2).HeaderText = "Area"
            GRIDLEDGER.Columns(2).Width = 100

            GRIDLEDGER.Columns(3).HeaderText = "Address"
            GRIDLEDGER.Columns(3).Width = 340

            GRIDLEDGER.Columns(4).HeaderText = "Group"
            GRIDLEDGER.Columns(4).Width = 150

            GRIDLEDGER.Columns(5).HeaderText = "Agent"
            GRIDLEDGER.Columns(5).Width = 150

            GRIDLEDGER.Columns(6).HeaderText = "Transport"
            GRIDLEDGER.Columns(6).Width = 150

            GRIDLEDGER.Columns(7).HeaderText = "Country"
            GRIDLEDGER.Columns(7).Width = 100



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            If GRIDLEDGER.SelectedRows.Count > 0 Then
                TEMPNAME = GRIDLEDGER.Rows(GRIDLEDGER.SelectedRows(0).Index).Cells(0).Value
                TEMPAGENT = GRIDLEDGER.Rows(GRIDLEDGER.SelectedRows(0).Index).Cells(2).Value
                TEMPTRANS = GRIDLEDGER.Rows(GRIDLEDGER.SelectedRows(0).Index).Cells(3).Value
            Else
                TEMPNAME = ""
                TEMPCODE = ""
                TEMPTRANS = ""
                TEMPAGENT = ""
            End If
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDLEDGER_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDLEDGER.CellDoubleClick
        Try
            If GRIDLEDGER.Rows.Count > 0 Then
                CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        Try
            showform(False, "")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        Try
            showform(True, GRIDLEDGER.Item(0, GRIDLEDGER.CurrentRow.Index).Value)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal name As String)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And GRIDLEDGER.RowCount > 0) Then
                Dim objaccountsmaster As New AccountsMaster
                objaccountsmaster.MdiParent = MDIMain
                objaccountsmaster.EDIT = editval
                objaccountsmaster.tempAccountsName = TXTNAME.Text.Trim()
                objaccountsmaster.tempAccountsCode = TEMPCODE
                objaccountsmaster.frmstring = "ACCOUNTS"
                'objaccountsmaster.tempAccountsName = name
                objaccountsmaster.Show()
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            FILLGRID("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNAME_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTNAME.TextChanged
        Try
            If TXTNAME.Text.Trim <> "" And cmbname.Text.Trim <> "" Then
                If rbstart.Checked = True Then
                    If cmbname.Text = "Name" Then
                        FILLGRID(" AND LEDGERS.ACC_CMPNAME like '%" & TXTNAME.Text.Trim & "'")
                    ElseIf cmbname.Text = "Code" Then
                        FILLGRID(" AND LEDGERS.ACC_CODE like '%" & TXTNAME.Text.Trim & "'")
                    ElseIf cmbname.Text = "Group" Then
                        FILLGRID(" AND GROUPMASTER.GROUP_NAME like '%" & TXTNAME.Text.Trim & "'")
                    ElseIf cmbname.Text = "City" Then
                        FILLGRID(" AND CITYMASTER.CITY_NAME  like '%" & TXTNAME.Text.Trim & "'")
                    End If
                ElseIf rbpart.Checked = True Then
                    If cmbname.Text = "Name" Then
                        FILLGRID(" AND LEDGERS.ACC_CMPNAME LIKE '%" & TXTNAME.Text.Trim & "%'")
                    ElseIf cmbname.Text = "Code" Then
                        FILLGRID(" AND LEDGERS.ACC_CODE LIKE '%" & TXTNAME.Text.Trim & "%'")
                    ElseIf cmbname.Text = "Group" Then
                        FILLGRID(" AND GROUPMASTER.GROUP_NAME LIKE '%" & TXTNAME.Text.Trim & "%'")
                    ElseIf cmbname.Text = "City" Then
                        FILLGRID(" AND CITYMASTER.CITY_NAME  LIKE '%" & TXTNAME.Text.Trim & "%'")
                    End If

                End If
            Else
                FILLGRID("")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDLEDGER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDLEDGER.KeyDown
        Try
            If e.KeyCode = Keys.Enter And GRIDLEDGER.RowCount > 0 Then
                Call CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class