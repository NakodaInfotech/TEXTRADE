
Imports System.ComponentModel
Imports BL

Public Class StateWiseMRP

    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub GETSRNO()
        Try
            For I As Integer = 0 To gridbill.RowCount - 1
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                ROW("SRNO") = I + 1
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim BLN As Boolean = True
        If CMBSTATENAME.Text.Trim = "" Then
            EP.SetError(CMBSTATENAME, "Fill Statename")
        End If

        If CMBITEMNAME.Text.Trim = "" Then
                EP.SetError(CMBITEMNAME, "Fill ItemName")
            End If

        Dim OBJCMN As New ClsCommon
        Dim DT As DataTable = OBJCMN.search(" STATEWISEMRP.MRP_NO AS NO, ISNULL(STATEMASTER.state_name, '') AS STATENAME, ISNULL(ITEMMASTER.ITEM_NAME, '') AS ITEMNAME  ", "", " STATEMASTER INNER JOIN STATEWISEMRP ON STATEMASTER.state_id = STATEWISEMRP.MRP_STATEID INNER JOIN ITEMMASTER ON STATEWISEMRP.MRP_ITEMID = ITEMMASTER.ITEM_ID ", " AND STATEMASTER.STATE_name = '" & CMBSTATENAME.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "'  AND STATEWISEMRP.MRP_YEARID = " & YearId)
        If DT.Rows.Count > 0 Then
            If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And Val(TXTNO.Text.Trim) <> Val(DT.Rows(0).Item("NO"))) Then
                EP.SetError(CMBSTATENAME, "ITEM ALREADY PRESENT IN STATE")
                BLN = False
            End If
        End If
        Return BLN
    End Function

    Sub EDITROW()
        Try
            If gridbill.GetFocusedRowCellValue("NO") > 0 Then
                GRIDDOUBLECLICK = True
                CMBITEMNAME.Text = gridbill.GetFocusedRowCellValue("ITEMNAME")
                CMBSTATENAME.Text = gridbill.GetFocusedRowCellValue("STATENAME")
                TXTMRP.Text = gridbill.GetFocusedRowCellValue("MRP")
                TXTNO.Text = gridbill.GetFocusedRowCellValue("NO")
                CMBSTATENAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(sender As Object, e As EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, "AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSTATENAME_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBSTATENAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "MERCHANT"
                OBJItem.STRSEARCH = " and ITEM_cmpid = " & CmpId & " and ITEM_LOCATIONid = " & Locationid & " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then CMBITEMNAME.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSTATENAME_Enter(sender As Object, e As EventArgs) Handles CMBSTATENAME.Enter
        Try
            If CMBSTATENAME.Text.Trim = "" Then fillSTATE(CMBSTATENAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSTATENAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBSTATENAME.Validating
        Try
            If CMBSTATENAME.Text.Trim <> "" Then STATEVALIDATE(CMBSTATENAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT' ")
            If CMBSTATENAME.Text.Trim = "" Then fillSTATE(CMBSTATENAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" ISNULL(STATEWISEMRP.MRP_NO, 0) AS NO, ISNULL(STATEWISEMRP.MRP_GRIDSRNO, 0) AS SRNO, ISNULL(STATEMASTER.state_name, '') AS STATENAME, ISNULL(STATEWISEMRP.MRP_MRP, 0) AS MRP, ISNULL(ITEMMASTER.ITEM_NAME, '') AS ITEMNAME ", "", " STATEWISEMRP INNER JOIN STATEMASTER ON STATEWISEMRP.MRP_STATEID = STATEMASTER.state_id INNER JOIN ITEMMASTER ON STATEWISEMRP.MRP_ITEMID = ITEMMASTER.item_id ", " AND STATEWISEMRP.MRP_YEARID = " & YearId & " ORDER BY STATEMASTER.STATE_NAME ")
            gridbilldetails.DataSource = DT
            GETSRNO()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StateWiseMRP_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.F5 Then
                gridbilldetails.Focus()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            TXTNO.Clear()
            txtsrno.Text = gridbill.RowCount + 1
            CMBITEMNAME.Text = ""
            CMBSTATENAME.Text = ""
            TXTMRP.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StateWiseMRP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FILLCMB()
            FILLGRID()
            CLEAR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDADD_Click(sender As Object, e As EventArgs) Handles CMDADD.Click
        Try
            If CMBSTATENAME.Text.Trim <> "" And CMBITEMNAME.Text.Trim <> "" Then
                EP.Clear()
                If Not ERRORVALID() Then
                    Exit Sub
                End If
                SAVE()
                FILLGRID()
                CMBITEMNAME.Focus()
            Else
                MsgBox("Enter Proper Deatails", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SAVE()
        Try
            Dim ALPARAVAL As New ArrayList
            Dim OBJMRP As New ClsStateWiseMRP

            ALPARAVAL.Add(Val(txtsrno.Text.Trim))
            ALPARAVAL.Add(CMBITEMNAME.Text.Trim)
            ALPARAVAL.Add(CMBSTATENAME.Text.Trim)
            ALPARAVAL.Add(TXTMRP.Text.Trim)

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJMRP.ALPARAVAL = ALPARAVAL
            If GRIDDOUBLECLICK = False Then
                Dim DT As DataTable = OBJMRP.SAVE()
                If DT.Rows.Count > 0 Then TXTNO.Text = Val(DT.Rows(0).Item(0))
            Else
                ALPARAVAL.Add(Val(TXTNO.Text.Trim))
                Dim INTRES As Integer = OBJMRP.UPDATE()
                GRIDDOUBLECLICK = False
            End If
            CLEAR()
            CMBSTATENAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(sender As Object, e As EventArgs) Handles gridbilldetails.DoubleClick
        EDITROW()
    End Sub

End Class