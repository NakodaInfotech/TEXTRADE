
Imports System.ComponentModel
Imports BL

Public Class StoresReorderLevel
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CLEAR()
        Try
            TXTNO.Clear()
            TXTQTY.Clear()
            TXTSRNO.Clear()
            CMBITEMNAME.Text = ""
            GRIDDOUBLECLICK = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If CMBITEMNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBITEMNAME, "Enter Item Details")
            bln = False
        End If

        If Val(TXTQTY.Text.Trim.Length) = 0 Then
            EP.SetError(TXTQTY, "Enter Qty")
            bln = False
        End If

        Dim OBJCMN As New ClsCommon
        Dim WHERECLAUSE As String = ""
        Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(STORESREORDERLEVEL.REORDER_NO, 0) AS NO ", "", " STORESREORDERLEVEL INNER JOIN STOREITEMMASTER ON STORESREORDERLEVEL.REORDER_ITEMID = STOREITEMMASTER.STOREITEM_ID", " AND STOREITEMMASTER.STOREITEM_NAME = '" & CMBITEMNAME.Text.Trim & "'  AND REORDER_YEARID = " & YearId & WHERECLAUSE)
        If DT.Rows.Count > 0 Then
            If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And Val(TXTNO.Text) <> Val(DT.Rows(0).Item("NO"))) Then
                EP.SetError(TXTQTY, "ITEM ALREADY PRESENT WITH THIS ORDER LEVEL.....")
                bln = False
            End If
        End If

        Return bln
    End Function
    Private Sub StoresReorderLevel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Try
                Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
                USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                fillcmb()
                CLEAR()

                fillgrid()
            Catch ex As Exception
                If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub SAVE()
        Try
            Dim ALPARAVAL As New ArrayList
            Dim OBJSM As New ClsStoresReorderLevel


            ALPARAVAL.Add(TXTSRNO.Text.Trim)
            ALPARAVAL.Add(CMBITEMNAME.Text.Trim)
            ALPARAVAL.Add(Val(TXTQTY.Text.Trim))

            ALPARAVAL.Add(CmpId)

            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJSM.alParaval = ALPARAVAL
            If GRIDDOUBLECLICK = False Then
                Dim DT As DataTable = OBJSM.SAVE()
                If DT.Rows.Count > 0 Then TXTNO.Text = DT.Rows(0).Item(0)

            Else
                ALPARAVAL.Add(TXTNO.Text.Trim)
                Dim INTRES As Integer = OBJSM.UPDATE()

            End If
            GRIDDOUBLECLICK = False
            CMBITEMNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
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
    Sub EDITROW()
        Try

            If gridbill.GetFocusedRowCellValue("NO") > 0 Then
                GRIDDOUBLECLICK = True
                TXTNO.Text = Val(gridbill.GetFocusedRowCellValue("NO"))
                CMBITEMNAME.Text = gridbill.GetFocusedRowCellValue("ITEMNAME")
                TXTQTY.Text = Val(gridbill.GetFocusedRowCellValue("QTY"))
                TXTSRNO.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub gridbilldetails_DoubleClick(sender As Object, e As EventArgs) Handles gridbilldetails.DoubleClick
        EDITROW()
    End Sub
    Private Sub CMBITEMNAME_Enter(sender As Object, e As EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then FILLSTOREITEMNAME(CMBITEMNAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then STOREITEMVALIDATE(CMBITEMNAME, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub fillcmb()
        Try
            If CMBITEMNAME.Text.Trim = "" Then FILLSTOREITEMNAME(CMBITEMNAME)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub fillgrid()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(STORESREORDERLEVEL.REORDER_NO, 0) AS NO, ISNULL(STOREITEMMASTER.STOREITEM_NAME, '') AS ITEMNAME, ISNULL(STORESREORDERLEVEL.REORDER_QTY, 0) AS QTY", "", " STORESREORDERLEVEL INNER JOIN STOREITEMMASTER ON STORESREORDERLEVEL.REORDER_ITEMID = STOREITEMMASTER.STOREITEM_ID", " AND REORDER_YEARID = " & YearId & " order by dbo.STORESREORDERLEVEL.REORDER_NO desc ")
            gridbilldetails.DataSource = DT
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub StoresReorderLevel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.F5 Then
                gridbilldetails.Focus()
            ElseIf e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMBPRINT_Click(sender As Object, e As EventArgs) Handles CMBPRINT.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Re Order Entry.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Re Order Entry"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Re Order Entry", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Re Order Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMBPRINT_Validating(sender As Object, e As CancelEventArgs) Handles CMBPRINT.Validating
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            If CMBITEMNAME.Text.Trim <> "" And TXTQTY.Text.Trim <> "" Then
                SAVE()
                CLEAR()
                fillgrid()
            Else
                MsgBox("Enter Proper Details")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
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


            'DELETE FROM TABLE
            Dim OBJSM As New ClsStoresReorderLevel
            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(gridbill.GetFocusedRowCellValue("NO"))
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(YearId)

            OBJSM.alParaval = ALPARAVAL
            Dim INTRES As Integer = OBJSM.DELETE()
            CLEAR()
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TXTQTY_Validating(sender As Object, e As CancelEventArgs) Handles TXTQTY.Validating
        Try
            If CMBITEMNAME.Text.Trim.Length <> 0 And Val(TXTQTY.Text.Trim.Length) <> 0 Then
                EP.Clear()
            End If


            If Not errorvalid() Then
                Exit Sub
            End If

            If CMBITEMNAME.Text.Trim <> "" Then
                SAVE()
                CLEAR()
                fillgrid()
            Else
                MsgBox("Enter Proper Details")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class