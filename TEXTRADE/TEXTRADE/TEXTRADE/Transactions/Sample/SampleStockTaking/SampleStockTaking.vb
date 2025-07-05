
Imports System.ComponentModel
Imports BL

Public Class SampleStockTaking

    Public TEMPSTOCKNO As Integer          'used for editing
    Public EDIT As Boolean          'used for editing
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Opening_Stock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub StockTaking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GDN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillGODOWN(CMBGODOWN, False)
            CMBGODOWN.Text = USERGODOWN
            CLEAR()


            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objSTOCK As New ClsSampleStockTaking()
                Dim dttable As DataTable = objSTOCK.SELECTSAMPLESTOCKTAKING(TEMPSTOCKNO, CmpId, YearId)
                If dttable.Rows.Count > 0 Then

                    TXTSTOCKNO.Text = TEMPSTOCKNO
                    STOCKDATE.Value = Format(Convert.ToDateTime(dttable.Rows(0).Item("STOCKDATE")).Date, "dd/MM/yyyy")
                    CMBGODOWN.Text = dttable.Rows(0).Item("STOCKGODOWN")
                    TXTREMARKS.Text = dttable.Rows(0).Item("STOCKREMARKS")

                    gridbilldetails.DataSource = dttable
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
        STOCKDATE.Value = Now.Date
        tstxtbillno.Clear()
        TXTBARCODE.Clear()
        TXTREMARKS.Clear()

        gridbilldetails.DataSource = Nothing
        GETMAXNO()


    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True

            If CMBGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBGODOWN, " Please Fill Godown")
                bln = False
            End If

            If gridbill.RowCount = 0 Then
                EP.SetError(gridbilldetails, "Fill Item Details")
                bln = False
            End If

            If Not datecheck(STOCKDATE.Text) Then
                EP.SetError(STOCKDATE, "Date not in Accounting Year")
                bln = False
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        STOCKDATE.Focus()
    End Sub

    Sub GETMAXNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(NO),0) + 1 ", " SAMPLESTOCKTAKING ", " AND YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSTOCKNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try

            If gridbill.FilterPanelText <> "" Then gridbill.ActiveFilterEnabled = False
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(Format(STOCKDATE.Value.Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBGODOWN.Text.Trim)

            ALPARAVAL.Add(TXTREMARKS.Text.Trim)

            Dim SAMPLETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim DESIGNNO As String = ""
            Dim COLOR As String = ""
            Dim NOOFBOOKLET As String = ""
            Dim BARCODE As String = ""


            For I As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(I)
                If BARCODE = "" Then
                    SAMPLETYPE = dtrow("SAMPLETYPE")
                    ITEMNAME = dtrow("ITEMNAME")
                    DESIGNNO = dtrow("DESIGNNO")
                    COLOR = dtrow("COLOR")
                    NOOFBOOKLET = Val(dtrow("NOOFBOOKLET"))
                    BARCODE = dtrow("BARCODE")

                Else

                    SAMPLETYPE = SAMPLETYPE & "|" & dtrow("SAMPLETYPE")
                    ITEMNAME = ITEMNAME & "|" & dtrow("ITEMNAME")
                    DESIGNNO = DESIGNNO & "|" & dtrow("DESIGNNO")
                    COLOR = COLOR & "|" & dtrow("COLOR")
                    NOOFBOOKLET = NOOFBOOKLET & "|" & Val(dtrow("NOOFBOOKLET"))
                    BARCODE = BARCODE & "|" & dtrow("BARCODE")

                End If
            Next
            ALPARAVAL.Add(SAMPLETYPE)
            ALPARAVAL.Add(ITEMNAME)
            ALPARAVAL.Add(DESIGNNO)
            ALPARAVAL.Add(COLOR)
            ALPARAVAL.Add(NOOFBOOKLET)
            ALPARAVAL.Add(BARCODE)

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim objSTOCK As New ClsSampleStockTaking()
            objSTOCK.alParaval = ALPARAVAL
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = objSTOCK.SAVE()
                MsgBox("Details Added")
                TXTSTOCKNO.Text = DTTABLE.Rows(0).Item(0)
                TEMPSTOCKNO = DTTABLE.Rows(0).Item(0)

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                ALPARAVAL.Add(TEMPSTOCKNO)
                Dim IntResult As Integer = objSTOCK.UPDATE()
                MsgBox("Details Updated")
                EDIT = False
            End If

            CLEAR()
            STOCKDATE.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
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


    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            gridbilldetails.DataSource = Nothing
LINE1:
            TEMPSTOCKNO = Val(TXTSTOCKNO.Text) - 1
            If TEMPSTOCKNO > 0 Then
                EDIT = True
                StockTaking_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If gridbill.RowCount = 0 And TEMPSTOCKNO > 1 Then
                TXTSTOCKNO.Text = TEMPSTOCKNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
LINE1:
            TEMPSTOCKNO = Val(TXTSTOCKNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTSTOCKNO.Text.Trim
            CLEAR()
            If Val(TXTSTOCKNO.Text) - 1 >= TEMPSTOCKNO Then
                EDIT = True
                StockTaking_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If gridbill.RowCount = 0 And TEMPSTOCKNO < MAXNO Then
                TXTSTOCKNO.Text = TEMPSTOCKNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                gridbilldetails.DataSource = Nothing
                TEMPSTOCKNO = Val(tstxtbillno.Text)
                If TEMPSTOCKNO > 0 Then
                    EDIT = True
                    StockTaking_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridbill_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridbill.KeyDown
        Try
            If gridbill.SelectedRowsCount > 0 Then
                If e.KeyCode = Keys.Delete Then
                    Dim DT As DataTable = gridbilldetails.DataSource
                    DT.Rows.RemoveAt(gridbill.FocusedRowHandle)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then
                If MsgBox("Wish to Delete Stock Taking?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBSTOCK As New ClsSampleStockTaking

                ALPARAVAL.Add(TEMPSTOCKNO)
                ALPARAVAL.Add(YearId)
                OBSTOCK.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBSTOCK.DELETE()
                MsgBox("Stock Taking Deleted Succesfully")
                CLEAR()
                EDIT = False
                STOCKDATE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtremarks_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTREMARKS.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then TXTREMARKS.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJstock As New SampleStockTakingDetails
            OBJstock.MdiParent = MDIMain
            OBJstock.Show()
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

    Private Sub PrintToolStripButton_Click_1(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click

    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Sample Stock Taking.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Sample Stock Taking"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sample Stock Taking", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Sample Stock Taking Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(sender As Object, e As EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            Dim OBJCMN As New ClsCommon
            'THIS CODE IS JUST WRITTEN TO MATCH THE COLUMNS IN DT
            Dim DT As DataTable = OBJCMN.SEARCH("BOOKLETSTOCK.SAMPLETYPE, BOOKLETSTOCK.ITEMNAME, BOOKLETSTOCK.DESIGNNO, ISNULL(BOOKLETSTOCK.SHADE, '') AS COLOR, ISNULL(BOOKLETSTOCK.NOOFBOOKLETS, 0) AS NOOFBOOKLET, SB_BARCODE AS BARCODE ", "", " SAMPLEBARCODE INNER JOIN ITEMMASTER ON ITEM_ID = SB_ITEMID LEFT OUTER JOIN DESIGNMASTER ON SB_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON SB_COLORID = COLOR_ID INNER JOIN BOOKLETSTOCK ON ITEMMASTER.ITEM_NAME = BOOKLETSTOCK.ITEMNAME AND ISNULL(DESIGN_NO,'') = BOOKLETSTOCK.DESIGNNO AND ISNULL(COLOR_NAME,'') = BOOKLETSTOCK.SHADE AND BOOKLETSTOCK.YEARID = " & YearId, " AND GODOWN = '" & CMBGODOWN.Text.Trim & "' AND CMPID = 0 AND NOOFBOOKLETS <> 0 ORDER BY ITEMNAME, DESIGNNO, SHADE, GODOWN")
            If gridbill.RowCount = 0 Then gridbilldetails.DataSource = DT


            Dim OBJSELECTGDN As New SelectSampleStock
            OBJSELECTGDN.GODOWN = CMBGODOWN.Text.Trim
            OBJSELECTGDN.ShowDialog()

            DT = OBJSELECTGDN.DT
            If DT.Rows.Count > 0 Then

                For Each DTROWPS As DataRow In DT.Rows

                    'CHECK WHETHER BARCODE Is ALREADY PRESENT IN GRID Or Not
                    'If DTROWPS("BARCODE") <> "" Then
                    '    For I As Integer = 0 To gridbill.RowCount - 1
                    '        Dim DR As DataRow = gridbill.GetDataRow(I)
                    '        If LCase(DR("BARCODE")) = LCase(DTROWPS("BARCODE")) Then GoTo LINE1
                    '    Next
                    'End If

                    Dim DTGRID As DataTable = gridbilldetails.DataSource
                    DTGRID.ImportRow(DTROWPS)
                    Dim DTROW As DataRow = gridbill.GetDataRow(gridbill.DataRowCount - 1)
                    gridbill.MakeRowVisible(gridbill.DataRowCount - 1, True)

LINE1:
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub StockTaking_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    If ClientName = "SVS" Then GITEMNAME.Visible = False
    '    If ClientName = "SAFFRON" Then
    '        GITEMCODE.Visible = True
    '        GITEMCODE.VisibleIndex = 0
    '        GCATEGORY.Visible = True
    '        GCATEGORY.VisibleIndex = 2
    '    End If
    'End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" BOOKLETSTOCK.SAMPLETYPE, BOOKLETSTOCK.ITEMNAME, BOOKLETSTOCK.DESIGNNO, ISNULL(BOOKLETSTOCK.SHADE, '') AS COLOR, ISNULL(BOOKLETSTOCK.NOOFBOOKLETS, 0) AS NOOFBOOKLET, SB_BARCODE AS BARCODE ", "", " SAMPLEBARCODE INNER JOIN ITEMMASTER ON ITEM_ID = SB_ITEMID LEFT OUTER JOIN DESIGNMASTER ON SB_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON SB_COLORID = COLOR_ID INNER JOIN BOOKLETSTOCK ON ITEMMASTER.ITEM_NAME = BOOKLETSTOCK.ITEMNAME AND ISNULL(DESIGN_NO,'') = BOOKLETSTOCK.DESIGNNO AND ISNULL(COLOR_NAME,'') = BOOKLETSTOCK.SHADE AND BOOKLETSTOCK.YEARID = " & YearId, " And SB_BARCODE = '" & TXTBARCODE.Text.Trim & "' AND BOOKLETSTOCK.GODOWN = '" & CMBGODOWN.Text.Trim & "' AND SB_YEARID = " & YearId)

                If DT.Rows.Count > 0 Then

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT, if YES THEN GIVE A MESSAGE THAT BARCODE EXISTS
                    'ALSO CHECK WHETHER BARCODE IS PRESENT IN OTHER STOCKTAKING ENTRY
                    Dim DTROW As DataRow
                    For I As Integer = 0 To gridbill.RowCount - 1
                        DTROW = gridbill.GetDataRow(I)
                        If LCase(DTROW("BARCODE")) = LCase(TXTBARCODE.Text.Trim) Then
                            MsgBox("Barcode Already Exist in Stock..", MsgBoxStyle.Critical)
                            GoTo LINE1
                        End If
                    Next

                    Dim DTCHECK As DataTable = OBJCMN.Execute_Any_String("SELECT NO FROM STOCKTAKING_DESC WHERE BARCODE = '" & TXTBARCODE.Text.Trim & "' AND CMPID = " & CmpId & " AND YEARID = " & YearId & " AND NO <> " & Val(TXTSTOCKNO.Text.Trim), "", "")
                    If DTCHECK.Rows.Count > 0 Then
                        MsgBox("Barcode Already Exist in Entry No " & DTCHECK.Rows(0).Item(0), MsgBoxStyle.Critical)
                        GoTo LINE1
                    End If

                    If gridbill.RowCount = 0 Then
                        gridbilldetails.DataSource = DT
                    Else
                        Dim DTGRID As DataTable = gridbilldetails.DataSource
                        DTGRID.ImportRow(DT.Rows(0))
                        DTROW = gridbill.GetDataRow(gridbill.DataRowCount - 1)
                        gridbill.MakeRowVisible(gridbill.DataRowCount - 1, True)
                    End If
LINE1:
                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()
                Else
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    TXTBARCODE.Clear()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class