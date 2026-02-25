Imports BL


Public Class MasterTransfer
    Dim INTRES As Integer
    Dim OBJTRF As New ClsYearTransfer
    Public FRMSTRING As String = ""
    'Sub fillcmp()
    '    Try
    '        Dim objclscommon As New ClsCommonMaster
    '        Dim dt As DataTable
    '        Dim whereclause As String = ""
    '        dt = objclscommon.search(" distinct user_cmpid", "", "UserMaster", " and User_Name = '" & UserName & "'")
    '        For Each DTROW As DataRow In dt.Rows
    '            If whereclause = "" Then
    '                whereclause = " AND CMP_ID IN (" & DTROW(0)
    '            Else
    '                whereclause = whereclause & "," & DTROW(0)
    '            End If
    '        Next
    '        whereclause = whereclause & ")"

    '        If SHOWHIDDENCMP = False Then whereclause = whereclause & " AND CMPMASTER.CMP_PASSWORD <> 'Infosys@123'"
    '        'dt = objclscommon.search("CMP_NAME, year_dbname, year_cmpid, year_startdate, year_enddate, year_id", "", "YearMaster INNER JOIN cmpmaster on cmp_id = year_cmpid", whereclause)
    '        dt = objclscommon.search("CMP_NAME, CMP_id", "", "cmpmaster", whereclause)
    '        If dt.Rows.Count > 0 Then
    '            dt.DefaultView.Sort = "cmp_name"
    '            gridcmp.DataSource = dt
    '            gridcmp.Columns(1).Visible = False
    '            'gridcmp.Columns(2).Visible = False
    '            'gridcmp.Columns(3).Visible = False
    '            'gridcmp.Columns(4).Visible = False
    '            'gridcmp.Columns(5).Visible = False
    '            gridcmp.Columns(0).HeaderText = "Company Name"
    '            gridcmp.Columns(0).Width = 270
    '        End If
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub
    Sub FILLCOMPANY(ByRef CMBCOMPANY As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBCOMPANY.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" CMP_ID AS ID , CMP_NAME AS NAME ", "", "CMPMASTER", " And CMP_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "NAME"
                    CMBCOMPANY.DisplayMember = "NAME"
                    CMBCOMPANY.ValueMember = "ID"
                    CMBCOMPANY.SelectedItem = Nothing
                End If
                CMBCOMPANY.DataSource = dt
                CMBCOMPANY.SelectedIndex = -1
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Sub FILLCMB()
        If CMBOLDCMP.Text.Trim = "" Then FILLCOMPANY(CMBOLDCMP)
        If CMBNEWCMP.Text.Trim = "" Then FILLCOMPANY(CMBNEWCMP)
    End Sub


    Private Sub MasterTransfer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBOLDCMP_Enter(sender As Object, e As EventArgs) Handles CMBOLDCMP.Enter
        Try
            If CMBOLDCMP.Text.Trim = "" Then FILLCOMPANY(CMBOLDCMP)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNEWCMP_Enter(sender As Object, e As EventArgs) Handles CMBNEWCMP.Enter
        Try
            If CMBNEWCMP.Text.Trim = "" Then FILLCOMPANY(CMBNEWCMP)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()

    End Sub

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try



            'INTIMATE IF USER HAS SELECTED WRONG YEAR
            If CMBOLDCMP.Text.Trim = CMBNEWCMP.Text.Trim Then
                MsgBox("You have selected the Wrong Company.")
                Exit Sub
            End If


            Dim SELECTEDCMP As String = ""
            Dim TEMPMSG As Integer = MsgBox("Transfer Data from Selected Company?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                TEMPMSG = MsgBox("Are you sure, wish to Proceed?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" CMP_ID AS CMPID  ", "", " CMPMASTER", " AND CMP_NAME = '" & CMBOLDCMP.Text & "'")
                    If DT.Rows.Count > 0 Then
                        For Each DTROW As DataRow In DT.Rows

                            SELECTEDCMP = DTROW("CMPID")


                            If CHKLEDGER.Checked = True Then
                                TRANSFERGROUP(SELECTEDCMP)

                                TRANSFERTRANSPORT(SELECTEDCMP)
                                TRANSFERAGENTS(SELECTEDCMP)

                                TRANSFERACCOUNTS(SELECTEDCMP)
                                TRANSFEREMPLOYEES(SELECTEDCMP)
                                MsgBox("Masters Transferred Successfully")

                            End If
                        Next
                    End If



                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub TRANSFERGROUP(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERGROUP()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub TRANSFERTRANSPORT(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERTRANSPORT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub TRANSFERAGENTS(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERAGENTS()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub TRANSFERACCOUNTS(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERACCOUNTS()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub TRANSFEREMPLOYEES(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFEREMPLOYEES()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class