Imports System.Runtime.Remoting.Metadata.W3cXsd2001
Imports System.Windows.Forms
Imports System.ComponentModel
Imports BL
Public Class SecurityInwardOutward
    'following two variables is only for used in edit mode....
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim gridDoubleClick As Boolean
    Dim tempRow As Integer

    Public edit As Boolean
    Public TEMPSECNO As String
    Public tempMsg As Integer

    Private Sub SecurityInwardOutward_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE INVOICE'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            clear()
            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objSTOCK As New ClsSecurityInwardOutward()
                Dim dttable As DataTable = objSTOCK.SELECTSECURITYINOUT(TEMPSECNO, CmpId, Locationid, YearId)
                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows
                        TXTSECNO.Text = TEMPSECNO
                        TXTSECNO.ReadOnly = True
                        DTSECDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        TXTVEHICLENO.Text = dr("VEHICLENO")
                        TXTWT.Text = dr("WT")
                        cmbname.Text = dr("NAME")
                        TXTMATRERIAL.Text = dr("MATERIAL")
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        TXTQUANTITY.Text = dr("QUANTITY")



                    Next

                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_INGRIDSRNO, 0) AS GRIDSRNO,  ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_INDESC, '') AS INDESC, ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_INQTY, 0) AS INQTY, ISNULL(UNITMASTER.unit_abbr, '') AS INUNIT,  ISNULL(STORESTOCKADJUSTMENT_INDESC.SA_INRATE, 0) AS INRATE,  ISNULL(STOREITEMMASTER.STOREITEM_NAME, '') AS INITEMNAME ", "", " STORESTOCKADJUSTMENT LEFT OUTER JOIN STORESTOCKADJUSTMENT_INDESC ON STORESTOCKADJUSTMENT.SA_no = STORESTOCKADJUSTMENT_INDESC.SA_NO AND STORESTOCKADJUSTMENT.SA_yearid = STORESTOCKADJUSTMENT_INDESC.SA_YEARID LEFT OUTER JOIN UNITMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_INUNITID = UNITMASTER.unit_id LEFT OUTER JOIN STOREITEMMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_INITEMID = STOREITEMMASTER.STOREITEM_ID  ", " AND STORESTOCKADJUSTMENT.SA_NO = " & TEMPSECNO & " AND STORESTOCKADJUSTMENT_INDESC.SA_YEARID = " & YearId & " ORDER BY STORESTOCKADJUSTMENT_INDESC.SA_INGRIDSRNO")

                    For Each DR As DataRow In DT.Rows
                        'Item Grid
                        gridupload.Rows.Add(DR("GRIDSRNO").ToString, DR("INITEMNAME").ToString, DR("INDESC").ToString, Format(Val(DR("INQTY")), "0.00"), DR("INUNIT"), Format(Val(DR("INRATE")), "0.00"))


                        TabControl1.SelectedIndex = 1

                    Next
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



    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
    Sub clear()
        cmbname.Text = ""
        TXTWT.Clear()
        TXTMATRERIAL.Clear()
        TXTVEHICLENO.Clear()
        DTSECDATE.Text = Now.Date
        TXTQUANTITY.Clear()
        txtremarks.Clear()
        EP.Clear()

        TXTUPLOADSRNO.Text = 1
        txtuploadname.Clear()
        PBIMG.ImageLocation = ""
        TXTPHOTOIMAGEUPLOADPATH.Clear()
        gridupload.RowCount = 0
        gridDoubleClick = False
        getmaxno()


        'EP.Clear()
        ''txtsrno.Clear()
        ''cmbitemname.Text = ""
        ''txtgridremarks.Clear()
        'TXTQTY.Clear()
        ''cmbqtyunit.Text = ""

        'txtremarks.Clear()
        'cmbname.Text = ""
        ''gridloan.RowCount = 0
        ''lbltotalqty.Text = 0.0


        'gridDoubleClick = False
        ''txtadd.Clear()


        'getmax_loan_no() 'this function is for to get max value from the Purchase loanuisition table

        'If gridloan.RowCount > 0 Then
        '    txtsrno.Text = Val(gridloan.Rows(gridloan.RowCount - 1).Cells(gsrno.Index).Value) + 1
        'Else
        '    txtsrno.Text = 1
        'End If

    End Sub

    Private Sub cmbname_Enter(sender As Object, e As EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        clear()
        edit = False
        DTSECDATE.Focus()
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(SA_no),0) + 1 ", " STORESTOCKADJUSTMENT ", " AND SA_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSECNO.Text = DTTABLE.Rows(0).Item(0)
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

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            If TXTSECNO.ReadOnly = False Then
                alParaval.Add(Val(TXTSECNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(Format(Convert.ToDateTime(DTSECDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(TXTWT.Text.Trim)
            alParaval.Add(TXTMATRERIAL.Text.Trim)
            alParaval.Add(TXTVEHICLENO.Text.Trim)
            alParaval.Add(TXTQUANTITY.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)



        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try


    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            gridupload.RowCount = 0
            GRIDUPLOADDESC.RowCount = 0
LINE1:
            TEMPSECNO = Val(TXTSECNO.Text) - 1
            If TEMPSECNO > 0 Then
                edit = True
                SecurityInwardOutward_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If gridupload.RowCount = 0 And GRIDUPLOADDESC.RowCount = 0 And TEMPSECNO > 1 Then
                TXTSECNO.Text = TEMPSECNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
LINE1:
            TEMPSECNO = Val(TXTSECNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTSECNO.Text.Trim
            clear()
            If Val(TXTSECNO.Text) - 1 >= TEMPSECNO Then
                edit = True
                SecurityInwardOutward_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If gridupload.RowCount = 0 And GRIDUPLOADDESC.RowCount = 0 And TEMPSECNO < MAXNO Then
                TXTSECNO.Text = TEMPSECNO
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

            Dim OBJstock As New SecurityInwardOutwardDetails
            OBJstock.MdiParent = MDIMain
            OBJstock.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        If cmbname.Text.Trim = "" Then FILLNAME(cmbname, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')")
    End Sub

    Private Sub SecurityInwardOutward_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If ERRORVALID() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1 Then       'for Delete
            TabControl1.SelectedIndex = (0)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2 Then       'for Delete
            TabControl1.SelectedIndex = (1)
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
        ElseIf e.KeyCode = Keys.F5 Then     'grid focus
            gridupload.Focus()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        End If
    End Sub


    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True

            If cmbname.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, " Please Fill Party Name")
                bln = False
            End If



            If gridupload.RowCount = 0 And GRIDUPLOADDESC.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If
            'CHEKC BARCODE IS PRESENT IN DATABASE OR NOT

            If Not datecheck(DTSECDATE.Text) Then
                EP.SetError(DTSECDATE, "Date not in Accounting Year")
                bln = False
            End If

            If TXTVEHICLENO.Text.Trim.Length = 0 Then
                EP.SetError(TXTVEHICLENO, "Please Fill Vehicle No")
                bln = False
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function
End Class