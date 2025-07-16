
Imports BL
Imports System.ComponentModel
Imports System.IO
Public Class CoverNote
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPVERIFIED As Boolean = False  'THIS IS USED TO CHECK WHETHER THE ORDER WAS ALREADY VERIFIED OR NOT, ON EDIT MODE, USED ON ERRORVALID FOR SNCM
    Public TEMPCOVERNO As String
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        fillcmb()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub CMDSELECTINV_Click(sender As Object, e As EventArgs) Handles CMDSELECTINV.Click
        Try

            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If CMBNAME.Text = "" AndAlso CMBAGENT.Text = "" Then
                MsgBox("Select Party Name First !", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim DTTABLE As DataTable
            Dim OBJSELECTPO As New SelectInvoice
            OBJSELECTPO.PARTYNAME = CMBNAME.Text.Trim
            OBJSELECTPO.AGENTNAME = CMBAGENT.Text.Trim
            OBJSELECTPO.FRMSTRING = "SELECTINVOICE"
            OBJSELECTPO.ShowDialog()

            DTTABLE = OBJSELECTPO.DT1

            Dim TEMPINVNO As String = ""
            Dim TEMPINVINITIALS As String = ""
            Dim i As Integer = 0
            If DTTABLE.Rows.Count > 0 Then

                ''  GETTING DISTINCT CHALLAN NO IN TEXTBOX
                Dim DV As DataView = DTTABLE.DefaultView
                Dim NEWDT As DataTable = DV.ToTable(True, "INVNO", "PRINTINITIALS")
                For Each DTR As DataRow In NEWDT.Rows
                    If TEMPINVNO.Trim = "" Then
                        TEMPINVNO = DTR("INVNO").ToString
                    Else
                        TEMPINVNO = TEMPINVNO & "," & DTR("INVNO").ToString
                    End If
                Next
                For Each DTR As DataRow In NEWDT.Rows
                    If TEMPINVINITIALS.Trim = "" Then
                        TEMPINVINITIALS = "'" & DTR("PRINTINITIALS").ToString & "'"
                    Else
                        TEMPINVINITIALS = TEMPINVINITIALS & "," & "'" & DTR("PRINTINITIALS").ToString & "'"
                    End If
                Next
                Dim OBJCMN As New ClsCommon()
                Dim DT1 As New DataTable
                If DTTABLE.Rows.Count > 0 Then
                    DT1 = OBJCMN.SEARCH(" INVOICEMASTER.INVOICE_NO AS INVNO, INVOICEMASTER.INVOICE_INITIALS AS INVINITIALS, INVOICEMASTER.INVOICE_DATE AS INVDATE, INVOICEMASTER.INVOICE_PRINTINITIALS AS PRINTINITIALS,  INVOICEMASTER.INVOICE_LRNO AS LRNO, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, INVOICEMASTER.INVOICE_LRDATE AS LRDATE, REGISTERMASTER.register_name AS REGNAME, INVOICEMASTER.INVOICE_TOTALPCS AS TOTALPCS, INVOICEMASTER.INVOICE_TOTALMTRS AS TOTALMTRS, INVOICEMASTER.INVOICE_GRANDTOTAL AS GRANDTOTAL, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME  ", "", " INVOICEMASTER LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id AND INVOICEMASTER.INVOICE_YEARID = AGENTLEDGERS.Acc_yearid LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON INVOICEMASTER.INVOICE_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON INVOICEMASTER.INVOICE_YEARID = LEDGERS.Acc_yearid AND INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_YEARID = REGISTERMASTER.register_yearid AND INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id  ", "  AND INVOICEMASTER.INVOICE_NO in ( " & TEMPINVNO & ") AND INVOICEMASTER.INVOICE_INITIALS in ( " & TEMPINVINITIALS & ") AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                    If DT1.Rows.Count > 0 Then
                        For Each dr As DataRow In DT1.Rows
                            GRIDCOVER.Rows.Add(0, dr("INVNO"), dr("REGNAME"), dr("INVINITIALS"), dr("PRINTINITIALS"), dr("NAME"), dr("AGENTNAME"), Format(Convert.ToDateTime(dr("INVDATE")), "dd/MM/yyyy"), dr("LRNO"), Format(Convert.ToDateTime(dr("LRDATE")), "dd/MM/yyyy"), dr("TRANSPORT"), Val(dr("TOTALMTRS")), Val(dr("TOTALPCS")), Val(dr("GRANDTOTAL")))
                        Next

                        GRIDCOVER.FirstDisplayedScrollingRowIndex = GRIDCOVER.RowCount - 1
                        getsrno(GRIDCOVER)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub GETMAXNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(COVER_no),0) + 1 ", " COVERNOTE ", " and COVER_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTCOVERNO.Text = DTTABLE.Rows(0).Item(0)
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

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTCOVERDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBAGENT.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(YearId)
            alParaval.Add(Userid)
            alParaval.Add(TXTCOURIERNAME.Text.Trim)
            alParaval.Add(TXTCOURIERDOCKETNO.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(COURIERDATE.Text).Date, "MM/dd/yyyy"))


            Dim SRNO As String = ""
            Dim INVNO As String = ""
            Dim REGNAME As String = ""
            Dim INVINITIALS As String = ""
            Dim PRINTINITIALS As String = ""
            Dim PARTYNAME As String = ""
            Dim AGENTNAME As String = ""
            Dim INVDATE As String = ""
            Dim LRNO As String = ""
            Dim LRDATE As String = ""
            Dim TRANSPORT As String = ""
            Dim TOTALMTRS As String = ""
            Dim TOTALPCS As String = ""
            Dim GRANDTOTAL As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCOVER.Rows
                If row.Cells(0).Value <> Nothing Then
                    If SRNO = "" Then

                        SRNO = row.Cells(GSRNO.Index).Value.ToString
                        INVNO = row.Cells(GINVNO.Index).Value.ToString
                        REGNAME = row.Cells(GREGNAME.Index).Value.ToString
                        INVINITIALS = row.Cells(GINVINITIALS.Index).Value.ToString
                        PRINTINITIALS = row.Cells(GPRINTINITIALS.Index).Value.ToString
                        PARTYNAME = row.Cells(GPARTYNAME.Index).Value.ToString
                        AGENTNAME = row.Cells(GAGENTNAME.Index).Value.ToString
                        INVDATE = row.Cells(GINVDATE.Index).Value
                        LRNO = row.Cells(GLRNO.Index).Value.ToString
                        LRDATE = row.Cells(GLRDATE.Index).Value
                        TRANSPORT = row.Cells(GTRANSPORT.Index).Value.ToString
                        TOTALMTRS = row.Cells(GTOTALMTRS.Index).Value
                        TOTALPCS = row.Cells(GTOTALPCS.Index).Value
                        GRANDTOTAL = row.Cells(GGRANDTOTAL.Index).Value
                    Else

                        SRNO = SRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
                        INVNO = INVNO & "|" & row.Cells(GINVNO.Index).Value
                        REGNAME = REGNAME & "|" & row.Cells(GREGNAME.Index).Value.ToString
                        INVINITIALS = INVINITIALS & "|" & row.Cells(GINVINITIALS.Index).Value.ToString
                        PRINTINITIALS = PRINTINITIALS & "|" & row.Cells(GPRINTINITIALS.Index).Value.ToString
                        PARTYNAME = PARTYNAME & "|" & row.Cells(GPARTYNAME.Index).Value.ToString
                        AGENTNAME = AGENTNAME & "|" & row.Cells(GAGENTNAME.Index).Value.ToString
                        INVDATE = INVDATE & "|" & row.Cells(GINVDATE.Index).Value
                        LRNO = LRNO & "|" & row.Cells(GLRNO.Index).Value.ToString
                        TRANSPORT = TRANSPORT & "|" & row.Cells(GTRANSPORT.Index).Value.ToString
                        LRDATE = LRDATE & "|" & row.Cells(GLRDATE.Index).Value
                        TOTALMTRS = TOTALMTRS & "|" & row.Cells(GTOTALMTRS.Index).Value
                        TOTALPCS = TOTALPCS & "|" & row.Cells(GTOTALPCS.Index).Value
                        GRANDTOTAL = GRANDTOTAL & "|" & row.Cells(GGRANDTOTAL.Index).Value

                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(INVNO)
            alParaval.Add(REGNAME)
            alParaval.Add(INVINITIALS)
            alParaval.Add(PRINTINITIALS)
            alParaval.Add(PARTYNAME)
            alParaval.Add(AGENTNAME)
            alParaval.Add(INVDATE)
            alParaval.Add(LRNO)
            alParaval.Add(LRDATE)
            alParaval.Add(TRANSPORT)
            alParaval.Add(TOTALMTRS)
            alParaval.Add(TOTALPCS)
            alParaval.Add(GRANDTOTAL)


            Dim OBJCLSCOVERNOTE As New ClsCoverNote()
            OBJCLSCOVERNOTE.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = OBJCLSCOVERNOTE.SAVE()
                MsgBox("Details Added")
            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPCOVERNO)
                alParaval.Add(YearId)
                Dim IntResult As Integer = OBJCLSCOVERNOTE.UPDATE()
                MsgBox("Details Updated")
            End If

            EDIT = False
            CLEAR()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If CMBNAME.Text.Trim.Length = 0 AndAlso CMBAGENT.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, " Please Fill Party Name Or Agent Name ")
            bln = False
        End If

        If GRIDCOVER.RowCount = 0 Then
            EP.SetError(GRIDCOVER, " Please Select Invoice ")
            bln = False
        End If
        Return bln
    End Function

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If MsgBox("Wish to Delete Cover Note ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPCOVERNO)
                ALPARAVAL.Add(YearId)

                Dim OBJPRO As New ClsCoverNote
                OBJPRO.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJPRO.DELETE
                MsgBox("Cover Note Deleted")

                CLEAR()
                EDIT = False
                CMBNAME.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLPRIVIOUS_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            GRIDCOVER.RowCount = 0
LINE1:
            TEMPCOVERNO = Val(TXTCOVERNO.Text) - 1
            If TEMPCOVERNO > 0 Then
                EDIT = True
                CoverNote_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDCOVER.RowCount = 0 And TEMPCOVERNO > 1 Then
                TXTCOVERNO.Text = TEMPCOVERNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


    Private Sub CoverNote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            CLEAR()
            fillcmb()
            CMBNAME.Enabled = True
            'If EDIT = True Then
            '    SHOWDATA()
            'End If

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJCMN As New ClsCommon
                Dim OBJFP As New ClsCoverNote()
                OBJFP.alParaval.Add(TEMPCOVERNO)
                OBJFP.alParaval.Add(YearId)
                Dim DT As DataTable = OBJFP.SELECTNOTE()
                If DT.Rows.Count > 0 Then

                    For Each dr As DataRow In DT.Rows

                        TXTCOVERNO.Text = TEMPCOVERNO
                        TXTCOVERNO.ReadOnly = True
                        DTCOVERDATE.Text = Format(Convert.ToDateTime(dr("COVERDATE")), "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(dr("NAME"))
                        CMBAGENT.Text = Convert.ToString(dr("AGENT"))
                        txtremarks.Text = Convert.ToString(dr("REMARKS"))
                        TXTCOURIERNAME.Text = Convert.ToString(dr("COURIERNAME"))
                        TXTCOURIERDOCKETNO.Text = Convert.ToString(dr("COURIERDOCKETNO"))
                        COURIERDATE.Text = Format(Convert.ToDateTime(dr("COURIERDATE")), "dd/MM/yyyy")


                        GRIDCOVER.Rows.Add(dr("SRNO"), dr("INVNO"), dr("REGNAME"), dr("INVINITIALS"), dr("PRINTINITIALS"), dr("PARTYNAME"), dr("AGENTNAME"), Format(Convert.ToDateTime(dr("INVDATE")), "dd/MM/yyyy").ToString, dr("LRNO"), dr("LRDATE"), dr("TRANSPORT"), Val(dr("TOTALMTRS")), Val(dr("TOTALPCS")), Val(dr("GRANDTOTAL")))

                    Next

                    'TOTAL()
                    GRIDCOVER.FirstDisplayedScrollingRowIndex = GRIDCOVER.RowCount - 1
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Sub fillcmb()
        Try
            If CMBAGENT.Text.Trim = "" Then fillagentledger(CMBAGENT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND ACC_TYPE='AGENT'")
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub CLEAR()
        Try
            CMBNAME.Text = ""
            CMBAGENT.Text = ""
            GRIDCOVER.RowCount = 0
            txtremarks.Clear()
            TXTCOURIERDOCKETNO.Clear()
            TXTCOURIERNAME.Clear()
            COURIERDATE.Value = Now.Date
            GETMAXNO()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMBAGENT_Enter(sender As Object, e As EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillledger(CMBAGENT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(sender As Object, e As CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then NAMEVALIDATE(CMBAGENT, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUPMASTER.GROUP_NAME <> 'HASTE DEBTORS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCOVER_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDCOVER.CellDoubleClick
        'Try
        '    EDITROW()
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDCOVER.CurrentRow.Index >= 0 And GRIDCOVER.Item(GSRNO.Index, GRIDCOVER.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True

                GRIDCOVER.Item(GSRNO.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()
                GRIDCOVER.Item(GINVNO.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()
                GRIDCOVER.Item(GREGNAME.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()
                GRIDCOVER.Item(GINVINITIALS.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()
                GRIDCOVER.Item(GPRINTINITIALS.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()
                GRIDCOVER.Item(GINVDATE.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()
                GRIDCOVER.Item(GLRNO.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()
                GRIDCOVER.Item(GLRDATE.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()
                GRIDCOVER.Item(GTRANSPORT.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()
                GRIDCOVER.Item(GTOTALMTRS.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()
                GRIDCOVER.Item(GTOTALPCS.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()
                GRIDCOVER.Item(GGRANDTOTAL.Index, GRIDCOVER.CurrentRow.Index).Value.ToString()


                TEMPROW = GRIDCOVER.CurrentRow.Index
                CMBNAME.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCOVER_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDCOVER.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCOVER.RowCount > 0 Then

                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                GRIDCOVER.Rows.RemoveAt(GRIDCOVER.CurrentRow.Index)
                'total()
                getsrno(GRIDCOVER)
            ElseIf e.KeyCode = Keys.F12 And GRIDCOVER.RowCount > 0 Then
                If GRIDCOVER.CurrentRow.Cells(GREGNAME.Index).Value <> "" Then GRIDCOVER.Rows.Add(CloneWithValues(GRIDCOVER.CurrentRow))
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs)
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If MsgBox("Wish to Delete Cover Note ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPCOVERNO)
                ALPARAVAL.Add(YearId)

                Dim OBJPRO As New ClsCoverNote
                OBJPRO.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJPRO.DELETE
                MsgBox("Cover Note Deleted")

                CLEAR()
                EDIT = False
                CMBNAME.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)
        Try
            If EDIT = True Then
                PRINTREPORT(TEMPCOVERNO)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal CoverNote As Integer)
        Try

            Dim TEMPMSG2 As Integer = MsgBox("Wish to Print Cover Note ?", MsgBoxStyle.YesNo)
            If CMBNAME.Text.Trim <> "" And TEMPMSG2 = vbYes Then
                Dim OBJINV As New SaleInvoiceDesign
                OBJINV.MdiParent = MDIMain
                OBJINV.FRMSTRING = "MAINCOVERNOTE"
                OBJINV.WHERECLAUSE = "{COVERNOTE.COVER_NO}=" & Val(TXTCOVERNO.Text) & " and {COVERNOTE.COVER_YEARID}=" & YearId
                'OBJINV.COVERNOTENO = CoverNote
                OBJINV.PARTYNAME = CMBNAME.Text.Trim
                OBJINV.AGENTNAME = CMBAGENT.Text.Trim
                OBJINV.Show()

            End If

            Dim TEMPMSG As Integer = MsgBox("Wish to Print AGENT Cover Note ?", MsgBoxStyle.YesNo)
            If CMBAGENT.Text.Trim <> "" And TEMPMSG = vbYes Then
                Dim OBJAGENT As New SaleInvoiceDesign
                OBJAGENT.MdiParent = MDIMain
                OBJAGENT.FRMSTRING = "MAINAGENTCOVERNOTE"
                OBJAGENT.WHERECLAUSE = "{COVERNOTE.COVER_NO}=" & Val(TXTCOVERNO.Text) & " and {COVERNOTE.COVER_YEARID}=" & YearId
                OBJAGENT.PARTYNAME = CMBNAME.Text.Trim
                OBJAGENT.AGENTNAME = CMBAGENT.Text.Trim
                OBJAGENT.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPCOVERNO)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            GRIDCOVER.RowCount = 0
LINE1:
            TEMPCOVERNO = Val(TXTCOVERNO.Text) + 1
            GETMAXNO()
            Dim MAXNO As Integer = TXTCOVERNO.Text.Trim
            CLEAR()
            If Val(TXTCOVERNO.Text) - 1 >= TEMPCOVERNO Then
                EDIT = True
                CoverNote_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDCOVER.RowCount = 0 And TEMPCOVERNO < MAXNO Then
                TXTCOVERNO.Text = TEMPCOVERNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objpodtls As New CoverNoteDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
            objpodtls.BringToFront()
            'Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Try
            cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(sender As Object, e As EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            CloneWithValues.Cells(index).Value = row.Cells(index).Value
        Next
    End Function
    Private Sub GRIDCOVER_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDCOVER.CellValidating
        '''  CODE FOR NUMERIC CHECK ONLY
        'Dim colNum As Integer = GRIDCOVER.Columns(e.ColumnIndex).Index
        'If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

        'Select Case colNum

        '    Case gQty.Index, GMTRS.Index

        '        Dim dDebit As Decimal
        '        Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

        '        If bValid Then
        '            If GRIDCOVER.CurrentCell.Value = Nothing Then GRIDCOVER.CurrentCell.Value = "0.00"
        '            GRIDCOVER.CurrentCell.Value = Convert.ToDecimal(GRIDCOVER.Item(colNum, e.RowIndex).Value)
        '            '' everything is good

        '        Else
        '            MessageBox.Show("Invalid Number Entered")
        '            e.Cancel = True
        '            Exit Sub
        '        End If
        '        total()

        'End Select
    End Sub

    Private Sub CoverNote_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNoCancel)
                    If tempmsg = vbCancel Then Exit Sub
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()

            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call PrintToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                TOOLPRIVIOUS_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F5 Then
                GRIDCOVER.Focus()
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(sender As Object, e As CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDCOVER.RowCount = 0
                TEMPCOVERNO = Val(tstxtbillno.Text)
                If TEMPCOVERNO > 0 Then
                    EDIT = True
                    CoverNote_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class