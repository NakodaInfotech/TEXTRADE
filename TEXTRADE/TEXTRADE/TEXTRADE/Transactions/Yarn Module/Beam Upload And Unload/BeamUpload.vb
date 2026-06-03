Imports System.ComponentModel
Imports BL
Imports DevExpress.XtraGrid.Views.Base
Public Class BeamUpload
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Dim GRIDDOUBLECLICK As Boolean
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Public TEMPGREYNO As Integer
    Dim TEMPROW As Integer
    Dim TEMPUPLOADROW As Integer
    Dim IntResult As Integer
    Dim TEMPMSG As Integer

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        Try
            clear()
            EDIT = False
            CMBNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub clear()
        EP.Clear()
        CMBNAME.Text = ""
        txtadd.Clear()
        GREYDATE.Text = Now.Date
        tstxtbillno.Clear()
        txtremarks.Clear()
        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False
        getmaxno()
        CMBBEAM.Text = ""
        CMBLOOM.Text = ""
        CMBGODOWN.Text = ""
        GRIDLOOMBEAM.RowCount = 0
        txtbeamname.Clear()
    End Sub
    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(BEAMUPLOAD_NO),0) + 1 ", " BEAMUPLOAD ", " AND BEAMUPLOAD_cmpid=" & CmpId & " and BEAMUPLOAD_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTGREYNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub
    Private Sub CMBGODOWN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(GREYDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim) 'WEAVER NAME 
            alParaval.Add(CMBLOOM.Text.Trim)
            alParaval.Add(CMBBEAM.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(txtbeamname.Text.Trim)

            Dim objCUTTING As New ClsBeamUpload()
            objCUTTING.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = objCUTTING.SAVE()
                MsgBox("Details Added")
                TXTGREYNO.Text = DTTABLE.Rows(0).Item(0)
                'PRINTREPORT(DTTABLE.Rows(0).Item(0))

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                alParaval.Add(TEMPGREYNO)
                IntResult = objCUTTING.UPDATE()
                MsgBox("Details Updated")

                EDIT = False
            End If
            clear()
            GREYDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If CMBGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBGODOWN, " Please Fill Godown")
                bln = False
            End If

            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, " Please Fill Name")
                bln = False
            End If

            If CMBLOOM.Text.Trim.Length = 0 Then
                EP.SetError(CMBLOOM, " Please Fill Loom")
                bln = False
            End If

            If CMBBEAM.Text.Trim.Length = 0 Then
                EP.SetError(CMBBEAM, " Please Fill Beam")
                bln = False
            End If


            If GREYDATE.Text = "__/__/____" Then
                EP.SetError(GREYDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(GREYDATE.Text) Then
                    EP.SetError(GREYDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function
    Sub fillcmb()
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub CMBNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then

                Dim TEMPMSG As Integer = MsgBox("Wish to Delete This Beam Upload Entry ...?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBJEMB As New ClsBeamUpload

                ALPARAVAL.Add(TEMPGREYNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(YearId)
                OBJEMB.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJEMB.Delete()
                MsgBox("Beam Upload Entry Deleted Succesfully")
                clear()
                EDIT = False
                CMBNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            fillcmb()
            clear()

            'If ClientName = "SVS" Then GPCS.ReadOnly = True



            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objJO As New ClsBeamUpload()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPGREYNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(YearId)
                objJO.alParaval = ALPARAVAL
                Dim dttable As DataTable = objJO.selectGREY(TEMPGREYNO, CmpId, YearId)

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTGREYNO.Text = TEMPGREYNO
                        GREYDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                        CMBGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)

                        CMBLOOM.Text = Convert.ToString(dr("LOOM").ToString)
                        CMBBEAM.Text = Convert.ToString(dr("BEAM").ToString)
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        txtbeamname.Text = Convert.ToString(dr("BEAMNAME").ToString)
                    Next
                Else
                    EDIT = False
                    'clear()
                End If
                LoadGridLoomBeam()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BeamUpload_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If errorvalid() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.KeyCode = Keys.F5 Then     'grid focus
            YarnRecd.Focus()
        End If
    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor

LINE1:
            TEMPGREYNO = Val(TXTGREYNO.Text) - 1
            If TEMPGREYNO > 0 Then
                EDIT = True
                BeamUpload_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
LINE1:
            TEMPGREYNO = Val(TXTGREYNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTGREYNO.Text.Trim
            clear()
            If Val(TXTGREYNO.Text) - 1 >= TEMPGREYNO Then
                EDIT = True
                BeamUpload_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(sender As Object, e As EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" Then
                LoadLoomsByWeaver(CMBNAME.Text.Trim)
                LoadGridLoomBeam()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LoadLoomsByWeaver(weaverAccId As String)
        Try
            Dim WHERECLAUSE As String = ""
            If CMBLOOM.Text <> "" Then
                WHERECLAUSE = " AND LOOM_NO = " & CMBLOOM.Text
            Else
                CMBBEAM.Items.Clear()
                CMBBEAM.Text = ""
                CMBLOOM.Items.Clear()
                CMBLOOM.Text = ""
            End If

            Dim dttable As DataTable
            Dim OBJCMN As New ClsCommon
            dttable = OBJCMN.SEARCH("LOOM_NO", "", "BEAMLOOMSTATUS", "AND LOOM_STATUS = 'FREE' " & "AND WEAVER_NAME = '" & CMBNAME.Text.Trim & "' " & WHERECLAUSE & " " & "ORDER BY LOOM_NO;")
            If dttable.Rows.Count > 0 Then
                For Each row As DataRow In dttable.Rows
                    If Not IsDBNull(row("LOOM_NO")) Then
                        CMBLOOM.Items.Add(row("LOOM_NO").ToString())
                    End If
                Next
                'Else
                '    CMBLOOM.Focus()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBLOOM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBLOOM.SelectedIndexChanged
        Try
            If CMBLOOM.Text.Trim = "" Then Exit Sub

            CMBBEAM.Items.Clear()
            CMBBEAM.Text = ""

            Dim dttable As DataTable
            Dim OBJCMN As New ClsCommon

            dttable = OBJCMN.SEARCH(" b.BEAMNO AS BEAMNO , b.BEAMNAME AS BEAMNAME", "", " BEAMSTOCKATJOBBER b ", "And Not EXISTS(SELECT 1 FROM BEAMUPLOAD u WHERE u.BEAMUPLOAD_BEAMNAME = b.BEAMNO ) And DONE = 'FALSE'   AND YEARID = " & YearId & " ORDER BY DATE DESC ")
            If dttable.Rows.Count > 0 Then
                For Each row As DataRow In dttable.Rows
                    If Not IsDBNull(row("BEAMNO")) AndAlso row("BEAMNO").ToString().Trim <> "" Then
                        CMBBEAM.Items.Add(row("BEAMNO").ToString().Trim)
                    End If
                Next
                If CMBBEAM.Items.Count > 0 Then
                    CMBBEAM.SelectedIndex = 0
                End If
                txtbeamname.Text = dttable.Rows(0)("BEAMNAME").ToString().Trim
            Else
                MsgBox("No Beams found for selected Loom.", MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBLOOM_Validating(sender As Object, e As CancelEventArgs) Handles CMBLOOM.Validating
        Try
            LoadLoomsByWeaver(CMBNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub LoadGridLoomBeam()
        Try
            Dim dt As DataTable
            Dim OBJCMN As New ClsCommon

            dt = OBJCMN.SEARCH(
            " BEAMUPLOAD.BEAMUPLOAD_LOOMNO AS LOOMNO, BEAMUPLOAD.BEAMUPLOAD_BEAMNO AS BEAMNO ",
            "",
            " BEAMUPLOAD INNER JOIN LEDGERS ON BEAMUPLOAD.BEAMUPLOAD_LEDGERID = LEDGERS.Acc_id AND BEAMUPLOAD.BEAMUPLOAD_yearid = LEDGERS.Acc_yearid ",
            " AND LEDGERS.Acc_cmpname = '" & CMBNAME.Text.Trim & "'"
        )

            GRIDLOOMBEAM.Rows.Clear()

            If dt.Rows.Count > 0 Then
                Dim SNO As Integer = 0
                For Each DTROWPS As DataRow In dt.Rows
                    SNO += 1
                    GRIDLOOMBEAM.Rows.Add(SNO, DTROWPS("LOOMNO"), DTROWPS("BEAMNO"))
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtremarks_KeyDown(sender As Object, e As KeyEventArgs) Handles txtremarks.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then txtremarks.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class