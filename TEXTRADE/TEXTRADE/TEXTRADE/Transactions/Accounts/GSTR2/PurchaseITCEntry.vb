
Imports BL

Public Class PurchaseITCEntry

    Public TEMPENTRYNO As Integer          'used for editing
    Public EDIT As Boolean          'used for editing

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseITCEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDSAVE_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PurchaseITCEntry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            CLEAR()
            FILLGRID()

            If EDIT = True Then

                Dim OBJITC As New ClsPurchaseITCEntry()
                Dim dttable As DataTable = OBJITC.SELECTPURCHASEITCENTRY(TEMPENTRYNO, YearId)
                If dttable.Rows.Count > 0 Then
                    TXTENTRYNO.Text = TEMPENTRYNO
                    DTENTRYDATE.Value = Format(Convert.ToDateTime(dttable.Rows(0).Item("DATE")).Date, "dd/MM/yyyy")
                    CMBMONTH.Text = dttable.Rows(0).Item("MONTH").ToString
                    txtremarks.Text = Convert.ToString(dttable.Rows(0).Item("REMARKS").ToString)
                    GRIDBILLDETAILS.DataSource = dttable
                    If dttable.Rows.Count > 0 Then
                        GRIDBILL.FocusedRowHandle = GRIDBILL.RowCount - 1
                        GRIDBILL.TopRowIndex = GRIDBILL.RowCount - 15
                    End If
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()

        EP.Clear()
        DTENTRYDATE.Value = Now.Date
        tstxtbillno.Clear()

        txtremarks.Clear()
        CMBMONTH.Text = MonthName(Now.Month)


        GRIDBILLDETAILS.DataSource = Nothing
        GETMAXNO()
        FILLGRID()

    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True

            If GRIDBILL.RowCount = 0 Then
                EP.SetError(CMBMONTH, "Select Entries")
                bln = False
            End If

            If Not datecheck(DTENTRYDATE.Text) Then
                EP.SetError(DTENTRYDATE, "Date not in Accounting Year")
                bln = False
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        Try
            CLEAR()
            EDIT = False
            DTENTRYDATE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAXNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(PURITC_no),0) + 1 ", " PurchaseITCEntry ", " AND PURITC_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTENTRYNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub CMDSAVE_Click(sender As Object, e As EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            alParaval.Add(Format(DTENTRYDATE.Value.Date, "MM/dd/yyyy"))
            alParaval.Add(CMBMONTH.Text.Trim)
            alParaval.Add(Val(GTAXABLEAMTBOOKS.SummaryText))
            alParaval.Add(Val(GCGSTAMTBOOKS.SummaryText))
            alParaval.Add(Val(GSGSTAMTBOOKS.SummaryText))
            alParaval.Add(Val(GIGSTAMTBOOKS.SummaryText))
            alParaval.Add(Val(GGTOTALBOOKS.SummaryText))
            alParaval.Add(Val(GTAXABLEAMTPORTAL.SummaryText))
            alParaval.Add(Val(GCGSTAMTPORTAL.SummaryText))
            alParaval.Add(Val(GSGSTAMTPORTAL.SummaryText))
            alParaval.Add(Val(GIGSTAMTPORTAL.SummaryText))
            alParaval.Add(Val(GGTOTALPORTAL.SummaryText))
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim BILLNO As String = ""
            Dim TYPE As String = ""
            Dim GSTIN As String = ""
            Dim NAME As String = ""
            Dim INVNOBOOKS As String = ""
            Dim INVDATEBOOKS As String = ""
            Dim INVNOPORTAL As String = ""
            Dim INVDATEPORTAL As String = ""
            Dim TAXABLEAMTBOOKS As String = ""
            Dim CGSTAMTBOOKS As String = ""
            Dim SGSTAMTBOOKS As String = ""
            Dim IGSTAMTBOOKS As String = ""
            Dim GTOTALBOOKS As String = ""
            Dim TAXABLEAMTPORTAL As String = ""
            Dim CGSTAMTPORTAL As String = ""
            Dim SGSTAMTPORTAL As String = ""
            Dim IGSTAMTPORTAL As String = ""
            Dim GTOTALPORTAL As String = ""
            Dim GSTRATE As String = ""
            Dim GRIDREMARKS As String = ""
            Dim ITCRECDLASTYEAR As String = ""
            Dim ITCREVERSEDLASTYEAR As String = ""

            For I As Integer = 0 To GRIDBILL.RowCount - 1
                Dim ROW As DataRow = GRIDBILL.GetDataRow(I)
                If BILLNO = "" Then
                    BILLNO = Val(ROW("BILLNO"))
                    TYPE = ROW("TYPE")
                    GSTIN = ROW("GSTIN")
                    NAME = ROW("NAME")
                    INVNOBOOKS = ROW("INVNOBOOKS")
                    INVDATEBOOKS = Format(Convert.ToDateTime(ROW("INVDATEBOOKS")).Date, "MM/dd/yyyy")
                    INVNOPORTAL = ROW("INVNOPORTAL")
                    INVDATEPORTAL = Format(Convert.ToDateTime(ROW("INVDATEPORTAL")).Date, "MM/dd/yyyy")
                    TAXABLEAMTBOOKS = Val(ROW("TAXABLEAMTBOOKS"))
                    CGSTAMTBOOKS = Val(ROW("CGSTAMTBOOKS"))
                    SGSTAMTBOOKS = Val(ROW("SGSTAMTBOOKS"))
                    IGSTAMTBOOKS = Val(ROW("IGSTAMTBOOKS"))
                    GTOTALBOOKS = Val(ROW("GRANDTOTALBOOKS"))
                    TAXABLEAMTPORTAL = Val(ROW("TAXABLEAMTPORTAL"))
                    CGSTAMTPORTAL = Val(ROW("CGSTAMTPORTAL"))
                    SGSTAMTPORTAL = Val(ROW("SGSTAMTPORTAL"))
                    IGSTAMTPORTAL = Val(ROW("IGSTAMTPORTAL"))
                    GTOTALPORTAL = Val(ROW("GRANDTOTALPORTAL"))
                    GSTRATE = Val(ROW("GSTRATE"))
                    GRIDREMARKS = ROW("GRIDREMARKS")
                    ITCRECDLASTYEAR = Convert.ToBoolean(ROW("ITCRECD"))
                    ITCREVERSEDLASTYEAR = Convert.ToBoolean(ROW("ITCREVERSED"))
                Else
                    BILLNO = BILLNO & "|" & Val(ROW("BILLNO"))
                    TYPE = TYPE & "|" & ROW("TYPE")
                    GSTIN = GSTIN & "|" & ROW("GSTIN")
                    NAME = NAME & "|" & ROW("NAME")
                    INVNOBOOKS = INVNOBOOKS & "|" & ROW("INVNOBOOKS")
                    INVDATEBOOKS = INVDATEBOOKS & "|" & Format(Convert.ToDateTime(ROW("INVDATEBOOKS")).Date, "MM/dd/yyyy")
                    INVNOPORTAL = INVNOPORTAL & "|" & ROW("INVNOPORTAL")
                    INVDATEPORTAL = INVDATEPORTAL & "|" & Format(Convert.ToDateTime(ROW("INVDATEPORTAL")).Date, "MM/dd/yyyy")
                    TAXABLEAMTBOOKS = TAXABLEAMTBOOKS & "|" & Val(ROW("TAXABLEAMTBOOKS"))
                    CGSTAMTBOOKS = CGSTAMTBOOKS & "|" & Val(ROW("CGSTAMTBOOKS"))
                    SGSTAMTBOOKS = SGSTAMTBOOKS & "|" & Val(ROW("SGSTAMTBOOKS"))
                    IGSTAMTBOOKS = IGSTAMTBOOKS & "|" & Val(ROW("IGSTAMTBOOKS"))
                    GTOTALBOOKS = GTOTALBOOKS & "|" & Val(ROW("GRANDTOTALBOOKS"))
                    TAXABLEAMTPORTAL = TAXABLEAMTPORTAL & "|" & Val(ROW("TAXABLEAMTPORTAL"))
                    CGSTAMTPORTAL = CGSTAMTPORTAL & "|" & Val(ROW("CGSTAMTPORTAL"))
                    SGSTAMTPORTAL = SGSTAMTPORTAL & "|" & Val(ROW("SGSTAMTPORTAL"))
                    IGSTAMTPORTAL = IGSTAMTPORTAL & "|" & Val(ROW("IGSTAMTPORTAL"))
                    GTOTALPORTAL = GTOTALPORTAL & "|" & Val(ROW("GRANDTOTALPORTAL"))
                    GSTRATE = GSTRATE & "|" & Val(ROW("GSTRATE"))
                    GRIDREMARKS = GRIDREMARKS & "|" & ROW("GRIDREMARKS")
                    ITCRECDLASTYEAR = ITCRECDLASTYEAR & "|" & Convert.ToBoolean(ROW("ITCRECD"))
                    ITCREVERSEDLASTYEAR = ITCREVERSEDLASTYEAR & "|" & Convert.ToBoolean(ROW("ITCREVERSED"))

                End If
            Next

            alParaval.Add(BILLNO)
            alParaval.Add(TYPE)
            alParaval.Add(GSTIN)
            alParaval.Add(NAME)
            alParaval.Add(INVNOBOOKS)
            alParaval.Add(INVDATEBOOKS)
            alParaval.Add(INVNOPORTAL)
            alParaval.Add(INVDATEPORTAL)
            alParaval.Add(TAXABLEAMTBOOKS)
            alParaval.Add(CGSTAMTBOOKS)
            alParaval.Add(SGSTAMTBOOKS)
            alParaval.Add(IGSTAMTBOOKS)
            alParaval.Add(GTOTALBOOKS)
            alParaval.Add(TAXABLEAMTPORTAL)
            alParaval.Add(CGSTAMTPORTAL)
            alParaval.Add(SGSTAMTPORTAL)
            alParaval.Add(IGSTAMTPORTAL)
            alParaval.Add(GTOTALPORTAL)
            alParaval.Add(GSTRATE)
            alParaval.Add(GRIDREMARKS)
            alParaval.Add(ITCRECDLASTYEAR)
            alParaval.Add(ITCREVERSEDLASTYEAR)

            Dim OBJITC As New ClsPurchaseITCEntry()
            OBJITC.alParaval = alParaval
            If EDIT = False Then
                Dim DTTABLE As DataTable = OBJITC.SAVE()
                TXTENTRYNO.Text = DTTABLE.Rows(0).Item(0)
                TEMPENTRYNO = DTTABLE.Rows(0).Item(0)
                MsgBox("Details Added")

            ElseIf EDIT = True Then
                alParaval.Add(TEMPENTRYNO)
                Dim IntResult As Integer = OBJITC.UPDATE()
                MsgBox("Details Updated")
                EDIT = False
            End If

            CLEAR()
            DTENTRYDATE.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            'THIS CODE IS WRITTEN BY GULKIT
            'WE HAVE PASSED YEARID=0, DONT CHANGE THIS CODE
            'THIS IS DONE AS WE NEED DATASOURCE TO BE LINKED WITH GRID
            Dim objclsCMST As New ClsCommon
            Dim dt As DataTable = objclsCMST.Execute_Any_String(" Select BILLNO, TYPE, '' AS GSTIN, NAME, PARTYBILLNO AS INVNOBOOKS, PARTYBILLDATE AS INVDATEBOOKS, '' AS INVNOPORTAL, PARTYBILLDATE AS INVDATEPORTAL , 0.0 AS TAXABLEAMTBOOKS, 0.0 AS CGSTAMTBOOKS, 0.0 AS SGSTAMTBOOKS, 0.0 AS IGSTAMTBOOKS, 0.0 AS GRANDTOTALBOOKS, 0.0 AS TAXABLEAMTPORTAL, 0.0 AS CGSTAMTPORTAL, 0.0 AS SGSTAMTPORTAL, 0.0 AS IGSTAMTPORTAL, 0.0 AS GRANDTOTALPORTAL, 0.0 AS GSTRATE, '' AS GRIDREMARKS, CAST(0 as BIT) AS ITCRECD, CAST(0 AS BIT) AS ITCREVERSED FROM GSTR2BILLS WHERE YEARID = 0 ", "", "")
            GRIDBILLDETAILS.DataSource = dt
            If dt.Rows.Count > 0 Then
                GRIDBILL.FocusedRowHandle = GRIDBILL.RowCount - 1
                GRIDBILL.TopRowIndex = GRIDBILL.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            GRIDBILLDETAILS.DataSource = Nothing
LINE1:
            TEMPENTRYNO = Val(TXTENTRYNO.Text) - 1
            If TEMPENTRYNO > 0 Then
                EDIT = True
                PurchaseITCEntry_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDBILL.RowCount = 0 And TEMPENTRYNO > 1 Then
                TXTENTRYNO.Text = TEMPENTRYNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
LINE1:
            TEMPENTRYNO = Val(TXTENTRYNO.Text) + 1
            GETMAXNO()
            Dim MAXNO As Integer = TXTENTRYNO.Text.Trim
            CLEAR()
            If Val(TXTENTRYNO.Text) - 1 >= TEMPENTRYNO Then
                EDIT = True
                PurchaseITCEntry_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDBILL.RowCount = 0 And TEMPENTRYNO < MAXNO Then
                TXTENTRYNO.Text = TEMPENTRYNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDBILLDETAILS.DataSource = Nothing
                TEMPENTRYNO = Val(tstxtbillno.Text)
                If TEMPENTRYNO > 0 Then
                    EDIT = True
                    PurchaseITCEntry_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDBILLDETAILS.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDBILL.RowCount > 0 Then
                Dim DT As DataTable = GRIDBILLDETAILS.DataSource
                Dim ROW As DataRow = GRIDBILL.GetFocusedDataRow
                DT.Rows.Remove(ROW)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then
                If MsgBox("Wish to Delete Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBJITC As New ClsPurchaseITCEntry

                ALPARAVAL.Add(TEMPENTRYNO)
                ALPARAVAL.Add(YearId)
                OBJITC.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJITC.DELETE()
                MsgBox("Entry Deleted Succesfully")
                CLEAR()
                EDIT = False
                DTENTRYDATE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtremarks_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtremarks.KeyDown
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

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJITC As New PurchaseITCDetails
            OBJITC.MdiParent = MDIMain
            OBJITC.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Try
            Call CMDSAVE_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub CMDSELECTBILLS_Click(sender As Object, e As EventArgs) Handles CMDSELECTBILLS.Click
        Try
            Dim DT As New DataTable
            Dim OBJITC As New SelectBillsforITC
            OBJITC.ShowDialog()
            DT = OBJITC.DT

            'CHECK WHETHER SAME BILLNO AND TYPE IS PRESENT IN GRID OR NOT, IF NOT PRESENT THEN ADD IN GRID
            For Each DTROW As DataRow In DT.Rows
                For I As Integer = 0 To GRIDBILL.RowCount - 1
                    Dim GRIDROW As DataRow = GRIDBILL.GetDataRow(I)
                    If DTROW("INVNOBOOKS") = GRIDROW("INVNOBOOKS") And DTROW("GSTIN") = GRIDROW("GSTIN") And DTROW("INVNOPORTAL") = GRIDROW("INVNOPORTAL") Then GoTo SKIPLINE
                Next
                Dim DTGRID As DataTable = GRIDBILLDETAILS.DataSource
                DTGRID.ImportRow(DTROW)
SKIPLINE:
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class