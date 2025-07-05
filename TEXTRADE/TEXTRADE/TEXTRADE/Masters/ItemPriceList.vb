
Imports BL
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class ItemPriceList

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public DT As New DataTable
    Public EDIT As Boolean

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub ItemPriceList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Sub CLEAR()
        Try
            cmbcategory.Text = ""
            GRIDBILLDETAILS.DataSource = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcategory_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcategory.Enter
        Try
            If cmbcategory.Text.Trim = "" Then fillCATEGORY(cmbcategory, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcategory_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcategory.Validated
        Try

            'FOR ITEMNAME
            Dim ITEMCLAUSE As String = ""
            Dim WHERECLAUSE As String = ""
            GRIDITEM.ClearColumnsFilter()
            For i As Integer = 0 To GRIDITEM.RowCount - 1
                Dim dtrow As DataRow = GRIDITEM.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If ITEMCLAUSE = "" Then
                        ITEMCLAUSE = " AND (ITEMMASTER.ITEM_NAME = '" & dtrow("ITEMNAME") & "'"
                    Else
                        ITEMCLAUSE = ITEMCLAUSE & " OR ITEMMASTER.ITEM_NAME = '" & dtrow("ITEMNAME") & "'"
                    End If
                End If
            Next
            If ITEMCLAUSE <> "" Then
                ITEMCLAUSE = ITEMCLAUSE & ")"
                WHERECLAUSE = WHERECLAUSE & ITEMCLAUSE
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(ITEMPRICELIST.RATE1, 0) AS RATE1, ISNULL(ITEMPRICELIST.RATE2, 0) AS RATE2, ISNULL(ITEMPRICELIST.RATE3, 0) AS RATE3, ISNULL(ITEMPRICELIST.RATE4, 0) AS RATE4, ISNULL(ITEMPRICELIST.RATE5, 0) AS RATE5, ISNULL(ITEMPRICELIST.RATE6, 0) AS RATE6, ISNULL(ITEMPRICELIST.RATE7, 0) AS RATE7, ISNULL(ITEMPRICELIST.RATE8, 0) AS RATE8, ISNULL(ITEMPRICELIST.RATE9, 0) AS RATE9, ISNULL(ITEMPRICELIST.RATE10, 0) AS RATE10, ISNULL(ITEMPRICELIST.RATE11, 0) AS RATE11, ISNULL(ITEMPRICELIST.RATE12, 0) AS RATE12, ISNULL(ITEMPRICELIST.RATE13, 0) AS RATE13, ISNULL(ITEMPRICELIST.RATE14, 0) AS RATE14, ISNULL(ITEMPRICELIST.RATE15, 0) AS RATE15 ", "", "ITEMMASTER LEFT OUTER JOIN ITEMPRICELIST ON ITEMMASTER.item_id = ITEMPRICELIST.ITEMID LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id  ", " AND ISNULL(CATEGORYMASTER.category_name,'') = '" & cmbcategory.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId & WHERECLAUSE & " ORDER BY ITEMNAME ASC ")
            GRIDBILLDETAILS.DataSource = DT
            If DT.Rows.Count > 0 Then
                GRIDBILL.FocusedRowHandle = GRIDBILL.RowCount - 1
                GRIDBILL.TopRowIndex = GRIDBILL.RowCount - 15
            End If

            'CHANGE LABELS OF ALL RATES FROM RATETYPEMASTER
            Dim DTRATE As DataTable = OBJCMN.Execute_Any_String("SELECT COLNAME, RATENAME FROM RATETYPEMASTER WHERE CMPID = " & CmpId, "", "")
            For Each DTROW As DataRow In DTRATE.Rows
                If DTROW("COLNAME") = "RATE01" Then GRATE1.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE02" Then GRATE2.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE03" Then GRATE3.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE04" Then GRATE4.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE05" Then GRATE5.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE06" Then GRATE6.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE07" Then GRATE7.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE08" Then GRATE8.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE09" Then GRATE9.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE10" Then GRATE10.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE11" Then GRATE11.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE12" Then GRATE12.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE13" Then GRATE13.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE14" Then GRATE14.Caption = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE15" Then GRATE15.Caption = DTROW("RATENAME")

            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcategory_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcategory.Validating
        Try
            If cmbcategory.Text.Trim <> "" Then CATEGORYVALIDATE(cmbcategory, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        GRIDBILL.ClearColumnsFilter()

        Dim ALPARAVAL As New ArrayList
        Dim OBJCONFIG As New ClsItemPriceList

        ALPARAVAL.Add(cmbcategory.Text.Trim)

        Dim ITEMNAME As String = ""
        Dim RATE1 As String = ""
        Dim RATE2 As String = ""
        Dim RATE3 As String = ""
        Dim RATE4 As String = ""

        Dim RATE5 As String = ""
        Dim RATE6 As String = ""
        Dim RATE7 As String = ""
        Dim RATE8 As String = ""
        Dim RATE9 As String = ""
        Dim RATE10 As String = ""
        Dim RATE11 As String = ""
        Dim RATE12 As String = ""
        Dim RATE13 As String = ""
        Dim RATE14 As String = ""
        Dim RATE15 As String = ""

        For I As Integer = 0 To GRIDBILL.RowCount - 1
            Dim ROW As DataRow = GRIDBILL.GetDataRow(I)
            If ITEMNAME = "" Then
                ITEMNAME = ROW("ITEMNAME")
                RATE1 = Val(ROW("RATE1"))
                RATE2 = Val(ROW("RATE2"))
                RATE3 = Val(ROW("RATE3"))
                RATE4 = Val(ROW("RATE4"))
                RATE5 = Val(ROW("RATE5"))
                RATE6 = Val(ROW("RATE6"))
                RATE7 = Val(ROW("RATE7"))
                RATE8 = Val(ROW("RATE8"))
                RATE9 = Val(ROW("RATE9"))
                RATE10 = Val(ROW("RATE10"))
                RATE11 = Val(ROW("RATE11"))
                RATE12 = Val(ROW("RATE12"))
                RATE13 = Val(ROW("RATE13"))
                RATE14 = Val(ROW("RATE14"))
                RATE15 = Val(ROW("RATE15"))

            Else
                ITEMNAME = ITEMNAME & "|" & ROW("ITEMNAME")
                RATE1 = RATE1 & "|" & Val(ROW("RATE1"))
                RATE2 = RATE2 & "|" & Val(ROW("RATE2"))
                RATE3 = RATE3 & "|" & Val(ROW("RATE3"))
                RATE4 = RATE4 & "|" & Val(ROW("RATE4"))
                RATE5 = RATE5 & "|" & Val(ROW("RATE5"))
                RATE6 = RATE6 & "|" & Val(ROW("RATE6"))
                RATE7 = RATE7 & "|" & Val(ROW("RATE7"))
                RATE8 = RATE8 & "|" & Val(ROW("RATE8"))
                RATE9 = RATE9 & "|" & Val(ROW("RATE9"))
                RATE10 = RATE10 & "|" & Val(ROW("RATE10"))
                RATE11 = RATE11 & "|" & Val(ROW("RATE11"))
                RATE12 = RATE12 & "|" & Val(ROW("RATE12"))
                RATE13 = RATE13 & "|" & Val(ROW("RATE13"))
                RATE14 = RATE14 & "|" & Val(ROW("RATE14"))
                RATE15 = RATE15 & "|" & Val(ROW("RATE15"))
            End If
        Next

        ALPARAVAL.Add(ITEMNAME)
        ALPARAVAL.Add(RATE1)
        ALPARAVAL.Add(RATE2)
        ALPARAVAL.Add(RATE3)
        ALPARAVAL.Add(RATE4)
        ALPARAVAL.Add(RATE5)
        ALPARAVAL.Add(RATE6)
        ALPARAVAL.Add(RATE7)
        ALPARAVAL.Add(RATE8)
        ALPARAVAL.Add(RATE9)
        ALPARAVAL.Add(RATE10)
        ALPARAVAL.Add(RATE11)
        ALPARAVAL.Add(RATE12)
        ALPARAVAL.Add(RATE13)
        ALPARAVAL.Add(RATE14)
        ALPARAVAL.Add(RATE15)

        ALPARAVAL.Add(CmpId)
        ALPARAVAL.Add(Userid)
        ALPARAVAL.Add(YearId)
        OBJCONFIG.alParaval = ALPARAVAL

        Dim DT As DataTable = OBJCONFIG.save()
        MsgBox("Details Added")
        CLEAR()
        cmbcategory.Focus()
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim OBJPRINT As New ItemPriceListPrint
            OBJPRINT.WHERECLAUSE = " {ITEMPRICELIST.YEARID} = " & YearId
            If cmbcategory.Text.Trim <> "" Then OBJPRINT.WHERECLAUSE = OBJPRINT.WHERECLAUSE & " AND {CATEGORYMASTER.CATEGORY_NAME} = '" & cmbcategory.Text.Trim & "'"

            'FOR ITEMNAME
            Dim ITEMCLAUSE As String = ""
            GRIDITEM.ClearColumnsFilter()
            For i As Integer = 0 To GRIDITEM.RowCount - 1
                Dim dtrow As DataRow = GRIDITEM.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If ITEMCLAUSE = "" Then
                        ITEMCLAUSE = " AND ({ITEMMASTER.ITEM_NAME} = '" & dtrow("ITEMNAME") & "'"
                    Else
                        ITEMCLAUSE = ITEMCLAUSE & " OR {ITEMMASTER.ITEM_NAME} = '" & dtrow("ITEMNAME") & "'"
                    End If
                End If
            Next
            If ITEMCLAUSE <> "" Then
                ITEMCLAUSE = ITEMCLAUSE & ")"
                OBJPRINT.WHERECLAUSE = OBJPRINT.WHERECLAUSE & ITEMCLAUSE
            End If


            OBJPRINT.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            fillCATEGORY(cmbcategory, EDIT)
            Dim OBJCMN As New ClsCommon
            Dim DTITEM As DataTable = OBJCMN.SEARCH(" DISTINCT CAST (0 AS BIT) AS CHK, ITEMMASTER.ITEM_NAME AS ITEMNAME ", " ", " ITEMMASTER ", " AND ITEMMASTER.ITEM_YEARID = " & YearId & " ORDER BY ITEMMASTER.ITEM_NAME")
            GRIDITEMDETAILS.DataSource = DTITEM
            If DTITEM.Rows.Count > 0 Then
                GRIDITEM.FocusedRowHandle = GRIDITEM.RowCount - 1
                GRIDITEM.TopRowIndex = GRIDITEM.RowCount - 15
            End If

            Dim DT As DataTable = OBJCMN.Execute_Any_String("SELECT COLNAME AS COLNAME, RATENAME, HEADERNAME FROM RATETYPEMASTER  WHERE CMPID = " & CmpId, "", "")
            For Each DTROW As DataRow In DT.Rows
                If DTROW("COLNAME") = "RATE1" Then TXTRATE1.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE2" Then TXTRATE2.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE3" Then TXTRATE3.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE4" Then TXTRATE4.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE5" Then TXTRATE5.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE6" Then TXTRATE6.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE7" Then TXTRATE7.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE8" Then TXTRATE8.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE9" Then TXTRATE9.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE10" Then TXTRATE10.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE11" Then TXTRATE11.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE12" Then TXTRATE12.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE13" Then TXTRATE13.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE14" Then TXTRATE14.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE15" Then TXTRATE15.Text = DTROW("HEADERNAME")

            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ItemPriceList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FILLCMB()
            CMBRATEFROM.SelectedIndex = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRATEPER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTRATEPER.Validating
        Try
            If Val(TXTRATEPER.Text.Trim) <> 0 And CMBRATETYPE.Text.Trim <> "" Then
                For I As Integer = 0 To GRIDBILL.RowCount - 1
                    Dim DTROW As DataRow = GRIDBILL.GetDataRow(I)
                    DTROW(CMBRATETYPE.Text.Trim) = Format(Val(DTROW(CMBRATEFROM.Text.Trim)) + ((Val(DTROW(CMBRATEFROM.Text.Trim)) * Val(TXTRATEPER.Text.Trim)) / 100), "0")
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ItemPriceList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "INDRAPUJAIMPEX" Then GPITEMNAME.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class