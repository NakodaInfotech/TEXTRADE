
Imports BL

Public Class UpdateItemRates

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String

    Private Sub ItemDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillgriditem()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Sub fillgriditem(Optional ByVal WHERE As String = "")
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objClsCommon As New ClsCommonMaster
            Dim dttable As DataTable = objClsCommon.search(" CAST(0 AS BIT)AS CHK, ITEMMASTER.item_name AS NAME, ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, ISNULL(ITEMMASTER.ITEM_VALUATIONRATE, 0) AS VALUATIONRATE, ISNULL(ITEMMASTER.ITEM_RATE, 0) AS RATE, ISNULL(ITEMMASTER.ITEM_HSNCODEID, 0) AS HSNCODE, ISNULL(CATEGORYMASTER.category_name, '') AS GREYCATEGORY, ISNULL(ITEMMASTER.item_categoryid, 0) AS CATEGORY ", "", " ItemMaster LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.ITEM_CATEGORYID = CATEGORYMASTER.CATEGORY_ID ", " and Item_yearid = " & YearId & " ORDER BY ITEMMASTER.item_name ")
            GRIDITEMDETAILS.DataSource = dttable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPDATE_Click(sender As Object, e As EventArgs) Handles CMDUPDATE.Click
        Try

            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            For I As Integer = 0 To Val(GRIDITEM.RowCount - 1)
                Dim ROW As DataRow = GRIDITEM.GetDataRow(I)
                If ROW("CHK") = True Then
                    If Val(TXTRATE.Text.Trim) > 0 Then DT = OBJCMN.Execute_Any_String(" UPDATE ITEMMASTER SET ITEMMASTER.ITEM_RATE = " & Val(TXTRATE.Text.Trim) & " WHERE ITEM_NAME = '" & ROW("NAME") & "' AND ITEM_YEARID = " & YearId, "", "")
                    If Val(TXTVALUATIONRATE.Text.Trim) > 0 Then DT = OBJCMN.Execute_Any_String(" UPDATE ITEMMASTER SET ITEMMASTER.ITEM_VALUATIONRATE = " & Val(TXTVALUATIONRATE.Text.Trim) & " WHERE ITEM_NAME = '" & ROW("NAME") & "' AND ITEM_YEARID = " & YearId, "", "")
                End If
            Next
            MsgBox("Rate Updated Successfully")
            Me.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            fillgriditem()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal name As String)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And GRIDITEM.RowCount > 0) Then
                Dim objItemmaster As New ItemMaster
                objItemmaster.MdiParent = MDIMain
                objItemmaster.EDIT = editval
                objItemmaster.tempItemName = name
                objItemmaster.frmstring = FRMSTRING
                objItemmaster.Show()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridledger_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GRIDITEM.DoubleClick
        Try
            showform(True, GRIDITEM.GetFocusedRowCellValue("NAME"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class