Imports BL
Imports System.Windows.Forms
Public Class SelectYarnStockGdnTransfer
    Dim tempindex, i As Integer
    Public SELECTIONFORMULA As String = ""
    Dim ADDCOL As Boolean = False
    Dim col As New DataGridViewCheckBoxColumn
    Public DT As New DataTable
    Public GODOWN As String = ""
    Public FILTER As String = ""
    Public FRMSTRING As String = ""
    Public ITEMNAME As String = ""

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectYarnStockGdnTransfer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectYarnStockGdnTransfer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Top = 100

        Dim objclspreq As New ClsCommon()
        DT.Columns.Add("YARNQUALITY")
        DT.Columns.Add("MILLNAME")
        DT.Columns.Add("DESIGNNO")
        DT.Columns.Add("PARTYLOTNO")
        DT.Columns.Add("PARTYCOLOR")
        DT.Columns.Add("COLOR")
        DT.Columns.Add("LOTNO")
        DT.Columns.Add("BAGS")
        DT.Columns.Add("WT")
        DT.Columns.Add("CONES")
        DT.Columns.Add("LRNO")
        DT.Columns.Add("LIFTINGDATE")

        FILLLRNO()
        fillCATEGORY(CMBCATEGORY, False)
        CMBPIECETYPE.SelectedItem = Nothing
        CMBLOTNO.SelectedItem = Nothing

        fillcheckboxlist()
        cmbselect.SelectedIndex = (1)
        txtsearch.Focus()

        If ClientName = "AVIS" Or ClientName = "ANOX" Then cmbselect.Text = "Design"

        FILLGRID("")
    End Sub


    Sub FILLLRNO()
        Try
            If CMBBALENO.Text.Trim = "" Then
                Dim WHERECLAUSE As String = ""
                If GODOWN <> "" Then WHERECLAUSE = " AND GODOWN = '" & GODOWN & "' "
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" DISTINCT LRNO ", "", " YARNSTOCKVIEW ", WHERECLAUSE & " AND YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "LRNO"
                    CMBBALENO.DataSource = dt
                    CMBBALENO.DisplayMember = "LRNO"
                    CMBBALENO.Text = ""
                End If
                CMBBALENO.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLLOTNO()
        Try
            If CMBLOTNO.Text.Trim = "" Then
                Dim WHERECLAUSE As String = ""
                If GODOWN <> "" Then WHERECLAUSE = " AND GODOWN = '" & GODOWN & "' "
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" DISTINCT LOTNO ", "", " YARNSTOCKVIEW ", WHERECLAUSE & " AND YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "LOTNO"
                    CMBLOTNO.DataSource = dt
                    CMBLOTNO.DisplayMember = "LOTNO"
                    CMBLOTNO.Text = ""
                End If
                CMBLOTNO.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Sub FILLGRID(ByVal WHERE As String)
        Try
            Cursor.Current = Cursors.WaitCursor

            If GODOWN <> "" Then WHERE = WHERE & " AND GODOWN = '" & GODOWN & "'"
            'If ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then WHERE = WHERE & " AND GODOWN = '" & GODOWN & "'"

            If FILTER <> "" Then WHERE = WHERE & FILTER

            Dim OBJCMN As New ClsCommon()
            Dim DT As New DataTable
            DT = OBJCMN.Execute_Any_String("SELECT YARNQUALITY, MILLNAME, DESIGNNO, COLOR, LOTNO, SUM(BAGS) AS BAGS, SUM (WT) AS WT,SUM(CONES) AS CONES,  LRNO ,  CAST(GETDATE() AS DATE) AS LIFTINGDATE,GODOWN  FROM YARNSTOCKVIEW WHERE YEARID = " & YearId & WHERE & " AND (BAGS > 0 OR CONES > 0 OR WT > 0)  GROUP BY YARNQUALITY, MILLNAME, DESIGNNO, COLOR, GODOWN, LOTNO,CATEGORY, LRNO", "", "")

            gridwo.DataSource = DT
            If DT.Rows.Count > 0 Then

                If ADDCOL = False Then
                    gridwo.Columns.Insert(0, col)
                    ADDCOL = True
                End If
                Dim I As Integer = 0
                gridwo.Columns(I).Width = 40 'CHECK BOK
                I = I + 1
                gridwo.Columns(I).Width = 200 'YARNQUALITY
                I = I + 1
                gridwo.Columns(I).Width = 150 'MILLNAME
                If ClientName <> "SVS" Then gridwo.Columns(I).Visible = False  'DESIGNNO
                I = I + 1

                If ClientName = "SPCORP" Then gridwo.Columns(I).Width = 200 Else gridwo.Columns(I).Width = 100 'DESIGNNO
                gridwo.Columns(I).DefaultCellStyle.Font = New Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point)
                I = I + 1
                gridwo.Columns(I).Width = 80 'SHADE
                I = I + 1
                gridwo.Columns(I).Width = 100 'GODOWN
                If ClientName = "KOCHAR" Then gridwo.Columns(I).Visible = False
                I = I + 1


                'gridwo.Columns(I).Width = 150 'JOBBERNAME
                'gridwo.Columns(I).Visible = False
                'I = I + 1
                'gridwo.Columns(I).Width = 60 'UNIT
                'I = I + 1

                gridwo.Columns(I).Width = 60 'BAGS
                gridwo.Columns(I).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                gridwo.Columns(I).DefaultCellStyle.Format = "N2"
                I = I + 1

                gridwo.Columns(I).Width = 60 'WT
                gridwo.Columns(I).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                gridwo.Columns(I).DefaultCellStyle.Format = "N2"
                I = I + 1

                gridwo.Columns(I).Width = 80 'CONES
                gridwo.Columns(I).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                gridwo.Columns(I).DefaultCellStyle.Format = "N2"
                gridwo.Columns(I).DefaultCellStyle.Font = New Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point)
                I = I + 1

                'gridwo.Columns(I).Width = 80 'BARCODE
                'I = I + 1
                'gridwo.Columns(I).Width = 70 'LOTNO
                'I = I + 1
                'gridwo.Columns(I).Visible = False 'FROMNO
                'I = I + 1
                'gridwo.Columns(I).Visible = False 'FROMSRNO
                'I = I + 1
                'gridwo.Columns(I).Width = 60 'TYPE
                'I = I + 1
                'gridwo.Columns(I).Width = 70 'PIECETYPE
                'I = I + 1

                'gridwo.Columns(I).Width = 80 'BALENO
                'If ClientName = "INDRANI" Then gridwo.Columns(I).HeaderText = "SO No"
                'If ClientName = "AMAN" Or ClientName = "AARYA" Then gridwo.Columns(I).HeaderText = "Challan No"
                'I = I + 1


                'gridwo.Columns(I).Width = 80 'GRIDREMARKS
                'I = I + 1
                gridwo.Columns(I).Width = 100 'LRNO
                I = I + 1

                gridwo.Columns(I).Width = 80 'LIFTINGDATE
                I = I + 1
                gridwo.Columns(I).Width = 150 'GODOWN
                I = I + 1
                'gridwo.Columns(I).Width = 80 'SHELF
                'I = I + 1
                'gridwo.Columns(I).Width = 120 'CHALLANNO
                'I = I + 1
                'gridwo.Columns(I).Width = 80 'PURRATE
                'I = I + 1


                gridwo.FirstDisplayedScrollingRowIndex = gridwo.RowCount - 1
            End If
            chkall.CheckState = CheckState.Unchecked
            LBLTOTALPCS.Text = 0
            LBLTOTALMTRS.Text = 0.00
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub txtsearch_Validated(sender As Object, e As EventArgs) Handles txtsearch.Validated
        Dim rowno, b As Integer
        Dim obj As New CheckedListBox
        rowno = 0

        If txtsearch.Text.Trim <> "" Then
            If cmbselect.Text = "Mill" Then
                obj = CLB_Mill
            ElseIf cmbselect.Text = "Design" Then
                obj = CLB_Design
            ElseIf cmbselect.Text = "Shade" Then
                obj = CLB_Shade
            ElseIf cmbselect.Text = "YarnQuality" Then
                obj = CLB_YarnQuality
            End If

            For b = 1 To obj.Items.Count

                txttempname.Text = obj.Items(rowno)
                txttempname.SelectionStart = 0
                txttempname.SelectionLength = txtsearch.TextLength
                If LCase(txtsearch.Text.Trim) = LCase(txttempname.SelectedText.Trim) Then
                    obj.SelectedIndex = rowno
                    obj.Focus()
                Else
                    rowno = rowno + 1
                End If
            Next
        End If
    End Sub

    Private Sub gridwo_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles gridwo.CellClick
        If e.RowIndex >= 0 Then
            With gridwo.Rows(e.RowIndex).Cells(0)
                If .Value = True Then
                    .Value = False
                Else
                    .Value = True
                End If
                TOTAL()
            End With
        End If
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            For Each ROW As DataGridViewRow In gridwo.Rows
                If ROW.Cells(0).Value = True Then
                    Dim DTROW() As DataRow = DT.Select("YARNQUALITY='" & ROW.Cells(2).Value & "'")
                    If DTROW.Length = 0 Then
                        'DT.Rows.Add(ROW.Cells(16).Value, ROW.Cells(1).Value, ROW.Cells(2).Value, ROW.Cells(3).Value, ROW.Cells(4).Value, ROW.Cells(5).Value, ROW.Cells(6).Value, ROW.Cells(7).Value, ROW.Cells(8).Value, ROW.Cells(9).Value, ROW.Cells(10).Value, ROW.Cells(11).Value, ROW.Cells(12).Value, ROW.Cells(13).Value, ROW.Cells(14).Value, ROW.Cells(15).Value, ROW.Cells(17).Value, ROW.Cells(18).Value, ROW.Cells(21).Value, ROW.Cells(22).Value, ROW.Cells(23).Value, ROW.Cells(24).Value, Val(ROW.Cells(25).Value))

                        DT.Rows.Add(ROW.Cells(1).Value, ROW.Cells(2).Value, ROW.Cells(3).Value, "", "", ROW.Cells(4).Value, ROW.Cells(5).Value, ROW.Cells(6).Value, ROW.Cells(7).Value, ROW.Cells(8).Value, ROW.Cells(9).Value, ROW.Cells(10).Value)
                    End If
                End If
            Next

            'Dim tempmsg As Integer = MsgBox("Wish To Continue?", MsgBoxStyle.YesNo)
            'If tempmsg = vbYes Then
            '    fillgrid("")
            'Else
            '    Me.Close()
            'End If

            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDSEARCH_Click(sender As Object, e As EventArgs) Handles CMDSEARCH.Click
        Dim WHERE As String = ""

        '***************search for QUALITY ****************

        Dim checked_QUALITY As CheckedListBox.CheckedItemCollection = CLB_Mill.CheckedItems
        Dim QUALITY As String = "" '"     Selected Students:" & vbCrLf
        For Each item As Object In checked_QUALITY
            If QUALITY = "" Then
                QUALITY = "'" & item.ToString() & "'"
            Else
                QUALITY = QUALITY & ",'" & item.ToString() & "'"
            End If
        Next item

        If QUALITY <> "" Then
            WHERE = WHERE & " and QUALITY IN (" & QUALITY & ")"
        End If
        '*********end of current Block ********

        '***************search for ITEM ****************

        Dim checked_ITEM As CheckedListBox.CheckedItemCollection = CLB_YarnQuality.CheckedItems
        Dim ITEMNAME As String = "" '"     Selected Students:" & vbCrLf
        For Each item As Object In checked_ITEM
            If ITEMNAME = "" Then
                ITEMNAME = "'" & item.ToString() & "'"
            Else
                ITEMNAME = ITEMNAME & ",'" & item.ToString() & "'"
            End If
        Next item

        If ITEMNAME <> "" Then
            WHERE = WHERE & " and ITEMNAME IN (" & ITEMNAME & ")"
        End If
        '*********end of current Block ********


        '***************search for DESIGN NO ****************
        Dim checked_DESIGN As CheckedListBox.CheckedItemCollection = CLB_Design.CheckedItems
        Dim DESIGN As String = "" '"     Selected Students:" & vbCrLf
        For Each item As Object In checked_DESIGN
            If DESIGN = "" Then
                DESIGN = "'" & item.ToString() & "'"
            Else
                DESIGN = DESIGN & ",'" & item.ToString() & "'"
            End If
        Next item
        If DESIGN <> "" Then WHERE = WHERE & " and DESIGNNO IN (" & DESIGN & ")"
        '*********end of current Block ********


        '***************search for SHADE ****************
        Dim checked_SHADE As CheckedListBox.CheckedItemCollection = CLB_Shade.CheckedItems
        Dim SHADE As String = "" '"     Selected Students:" & vbCrLf
        For Each item As Object In checked_SHADE
            If SHADE = "" Then
                SHADE = "'" & item.ToString() & "'"
            Else
                SHADE = SHADE & ",'" & item.ToString() & "'"
            End If
        Next item
        If SHADE <> "" Then WHERE = WHERE & " and COLOR IN (" & SHADE & ")"
        '*********end of current Block ********

        If CMBPIECETYPE.Text.Trim <> "" Then WHERE = WHERE & " and PIECETYPE = '" & CMBPIECETYPE.Text.Trim & "'"
        If CMBLOTNO.Text.Trim <> "" Then WHERE = WHERE & " and LOTNO = '" & CMBLOTNO.Text.Trim & "'"
        If CMBBALENO.Text.Trim <> "" Then
            If ClientName = "AMAN" Or ClientName = "AARYA" Then WHERE = WHERE & " and CHALLANNO = '" & CMBBALENO.Text.Trim & "'" Else WHERE = WHERE & " and BALENO = '" & CMBBALENO.Text.Trim & "'"
        End If
        If CMBUNIT.Text.Trim <> "" Then WHERE = WHERE & " and UNIT = '" & CMBUNIT.Text.Trim & "'"
        If CMBCATEGORY.Text.Trim <> "" Then WHERE = WHERE & " and CATEGORY = '" & CMBCATEGORY.Text.Trim & "'"
        If CMBBALENO.Text.Trim <> "" Then WHERE = WHERE & " and CHALLANNO = '" & CMBBALENO.Text.Trim & "'"
        If TXTBARCODE.Text.Trim <> "" Then WHERE = WHERE & " and BARCODE = '" & TXTBARCODE.Text.Trim & "'"
        If Val(TXTENTRYNO.Text.Trim) <> 0 Then WHERE = WHERE & " and FROMNO = " & Val(TXTENTRYNO.Text.Trim)

        FILLGRID(WHERE)
        chkall.Focus()
    End Sub

    Private Sub CMDCLEARSEARCH_Click(sender As Object, e As EventArgs) Handles CMDCLEARSEARCH.Click
        If ClientName = "SVS" Then
            CMBQUALITY.SelectedItem = Nothing
            CMBDESIGN.DataSource = Nothing
            CMBSHADE.DataSource = Nothing
            CMBQUALITY.Focus()
        Else
            fillcheckboxlist()
        End If
        chkall.CheckState = CheckState.Unchecked
        LBLTOTALPCS.Text = 0
        LBLTOTALMTRS.Text = 0.00
        CMBBALENO.Text = ""
        CMBLOTNO.Text = ""
        CMBBALENO.Text = ""
        txtsearch.Clear()
        TXTBARCODE.Clear()
        FILLGRID("")
    End Sub
    Sub fillcheckboxlist()
        Dim dt As DataTable
        Dim objclscomm As New ClsCommon()

        'Fill QUALITY
        dt = objclscomm.Execute_Any_String("SELECT DISTINCT MILL_NAME FROM MILLMASTER ", " ", " WHERE MILLMASTER.MILL_Yearid=" & YearId & " ORDER BY MILLMASTER.MILL_NAME ")
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                CLB_Mill.Items.Add(Convert.ToString(dr(0)), False)
            Next
        End If

        'Fill ITEMNAME
        dt = objclscomm.Execute_Any_String("SELECT DISTINCT YARN_NAME FROM YARNQUALITYMASTER ", " ", " WHERE YARNQUALITYMASTER.YARN_Yearid=" & YearId & " ORDER BY YARNQUALITYMASTER.YARN_NAME ")
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                CLB_YarnQuality.Items.Add(Convert.ToString(dr(0)), False)
            Next
        End If

        'Fill DESIGN
        dt = objclscomm.Execute_Any_String(" SELECT DISTINCT DESIGN_NO FROM DESIGNMASTER ", " ", " WHERE DESIGNMASTER.DESIGN_yearid = " & YearId & " ORDER BY DESIGNMASTER.DESIGN_NO")
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                CLB_Design.Items.Add(Convert.ToString(dr(0)), False)
            Next
        End If

        'Fill COLOR
        dt = objclscomm.Execute_Any_String("SELECT DISTINCT COLOR_NAME FROM COLORMASTER", " ", " WHERE COLORMASTER.COLOR_Yearid=" & YearId & " ORDER BY COLORMASTER.COLOR_NAME")
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                CLB_Shade.Items.Add(Convert.ToString(dr(0)), False)
            Next
        End If

    End Sub

    Private Sub chkall_CheckedChanged(sender As Object, e As EventArgs) Handles chkall.CheckedChanged
        Try
            Dim N As String = ""
            Dim M As String = ""

            Dim tempindex As Integer
            Dim i As Integer
            Dim USERROWS As Integer = gridwo.RowCount - 1
            If Val(TXTQTY.Text.Trim) > 0 AndAlso gridwo.RowCount >= Val(TXTQTY.Text.Trim) Then USERROWS = Val(TXTQTY.Text.Trim)


            If gridwo.RowCount > 0 Then
                If chkall.Checked = True Then
                    For i = 0 To USERROWS
                        With gridwo.Rows(i).Cells(0)
                            .Value = True
                        End With
                    Next
                Else
                    For i = 0 To USERROWS
                        With gridwo.Rows(i).Cells(0)
                            .Value = False
                        End With
                    Next
                End If
            End If
            TOTAL()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub TOTAL()
        Try
            LBLTOTALMTRS.Text = 0.00
            LBLTOTALPCS.Text = 0
            For Each ROW As DataGridViewRow In gridwo.Rows
                If Convert.ToBoolean(ROW.Cells(0).Value) = True Then
                    LBLTOTALMTRS.Text = Val(LBLTOTALMTRS.Text.Trim) + Val(ROW.Cells("WT").Value)
                    LBLTOTALPCS.Text = Val(LBLTOTALPCS.Text.Trim) + Val(ROW.Cells("BAGS").Value)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkDesign_CheckedChanged(sender As Object, e As EventArgs) Handles chkDesign.CheckedChanged
        If chkDesign.Checked = True Then
            For i As Integer = 0 To CLB_Design.Items.Count - 1
                CLB_Design.SetItemChecked(i, True)
            Next
        Else
            For i As Integer = 0 To CLB_Design.Items.Count - 1
                CLB_Design.SetItemChecked(i, False)
            Next
        End If
    End Sub
    Sub FILLCLBCOLOR()
        Try
            Dim checked_DESIGN As CheckedListBox.CheckedItemCollection = CLB_Design.CheckedItems
            Dim DESIGN As String = "" '"     Selected Students:" & vbCrLf
            Dim WHERE As String = ""
            For Each item As Object In checked_DESIGN
                If DESIGN = "" Then
                    DESIGN = "'" & item.ToString() & "'"
                Else
                    DESIGN = DESIGN & ",'" & item.ToString() & "'"
                End If
            Next item
            If DESIGN <> "" Then WHERE = WHERE & " and ISNULL(DESIGNMASTER.DESIGN_NO,'') IN (" & DESIGN & ")"


            'Fill COLOR
            CLB_Shade.Items.Clear()
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String("SELECT DISTINCT COLOR_NAME FROM DESIGNMASTER INNER JOIN DESIGNMASTER_COLOR ON DESIGNMASTER.DESIGN_id = DESIGNMASTER_COLOR.DESIGN_ID RIGHT OUTER JOIN COLORMASTER ON DESIGNMASTER_COLOR.DESIGN_COLORID = COLORMASTER.COLOR_id WHERE COLOR_yearid = " & YearId & WHERE & " ORDER BY COLORMASTER.COLOR_NAME", "", "")
            If DT.Rows.Count > 0 Then
                For Each dr As DataRow In DT.Rows
                    CLB_Shade.Items.Add(Convert.ToString(dr(0)), False)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkQuality_CheckedChanged(sender As Object, e As EventArgs) Handles chkQuality.CheckedChanged
        If chkQuality.Checked = True Then
            For i As Integer = 0 To CLB_Mill.Items.Count - 1
                CLB_Mill.SetItemChecked(i, True)
            Next
        Else
            For i As Integer = 0 To CLB_Mill.Items.Count - 1
                CLB_Mill.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub CHKShade_CheckedChanged(sender As Object, e As EventArgs) Handles CHKShade.CheckedChanged
        If CHKShade.Checked = True Then
            For i As Integer = 0 To CLB_Shade.Items.Count - 1
                CLB_Shade.SetItemChecked(i, True)
            Next
        Else
            For i As Integer = 0 To CLB_Shade.Items.Count - 1
                CLB_Shade.SetItemChecked(i, False)
            Next
        End If
    End Sub
    Sub fillnewquality(ByRef CMBQUALITY As ComboBox)
        Try
            Dim objclscommon As New ClsCommonMaster
            Dim DT As DataTable = objclscommon.search(" DISTINCT QUALITY ", "", " BARCODESTOCK", " AND ROUND(MTRS,0) > 0 AND CMPID = " & CmpId & " AND LOCATIONID  = " & Locationid & " AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                DT.DefaultView.Sort = "QUALITY"
                CMBQUALITY.DataSource = DT
                CMBQUALITY.DisplayMember = "QUALITY"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLNEWPIECETYPE(ByRef CMBQUALITY As ComboBox)
        Try
            Dim objclscommon As New ClsCommonMaster
            Dim DT As DataTable = objclscommon.search(" DISTINCT QUALITY ", "", " BARCODESTOCK", " AND ROUND(MTRS,0) > 0 AND CMPID = " & CmpId & " AND LOCATIONID  = " & Locationid & " AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                DT.DefaultView.Sort = "QUALITY"
                CMBQUALITY.DataSource = DT
                CMBQUALITY.DisplayMember = "QUALITY"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillnewItem(ByRef CMBITEMNAME As ComboBox)
        Try
            Dim objclscommon As New ClsCommonMaster
            Dim DT As DataTable = objclscommon.search(" DISTINCT ITEM ", "", " BARCODESTOCK", " AND ROUND(MTRS,0) > 0 AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                DT.DefaultView.Sort = "ITEM"
                CMBITEMNAME.DataSource = DT
                CMBITEMNAME.DisplayMember = "ITEM"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillnewdesignno(ByRef CMBDESIGN As ComboBox)
        Try
            Dim objclscommon As New ClsCommonMaster
            Dim DT As DataTable = objclscommon.search(" DISTINCT DESIGNNO ", "", " BARCODESTOCK", " AND ROUND(MTRS,0) > 0 AND QUALITY = '" & CMBQUALITY.Text.Trim & "' AND CMPID = " & CmpId & " AND LOCATIONID  = " & Locationid & " AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                DT.DefaultView.Sort = "DESIGNNO"
                CMBDESIGN.DataSource = DT
                CMBDESIGN.DisplayMember = "DESIGNNO"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillnewCOLOR(ByRef CMBSHADE As ComboBox)
        Try
            Dim objclscommon As New ClsCommonMaster
            Dim DT As DataTable = objclscommon.search(" DISTINCT COLOR ", "", " BARCODESTOCK", " AND ROUND(MTRS,0) > 0 AND QUALITY = '" & CMBQUALITY.Text.Trim & "' AND DESIGNNO = '" & CMBDESIGN.Text.Trim & "' AND CMPID = " & CmpId & " AND LOCATIONID  = " & Locationid & " AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                DT.DefaultView.Sort = "COLOR"
                CMBSHADE.DataSource = DT
                CMBSHADE.DisplayMember = "COLOR"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try

            If CMBQUALITY.Text.Trim <> "" Then
                fillnewdesignno(CMBDESIGN)
                CMBDESIGN.SelectedItem = Nothing
                CMBDESIGN.Focus()
                FILLGRID(" AND QUALITY = '" & CMBQUALITY.Text.Trim & "'")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then
                fillnewCOLOR(CMBSHADE)
                CMBSHADE.SelectedItem = Nothing
                CMBSHADE.Focus()
                FILLGRID(" AND QUALITY = '" & CMBQUALITY.Text.Trim & "' AND DESIGNNO = '" & CMBDESIGN.Text.Trim & "'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then FILLGRID(" AND QUALITY = '" & CMBQUALITY.Text.Trim & "' AND DESIGNNO = '" & CMBDESIGN.Text.Trim & "' AND COLOR = '" & CMBSHADE.Text.Trim & "'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKItem_CheckedChanged(sender As Object, e As EventArgs) Handles CHKItem.CheckedChanged
        If CHKItem.Checked = True Then
            For i As Integer = 0 To CLB_YarnQuality.Items.Count - 1
                CLB_YarnQuality.SetItemChecked(i, True)
            Next
        Else
            For i As Integer = 0 To CLB_YarnQuality.Items.Count - 1
                CLB_YarnQuality.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub CLB_Design_Click(sender As Object, e As EventArgs) Handles CLB_Design.Click
        Try
            If ClientName = "AVIS" Then FILLCLBCOLOR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CLB_Design_SelectedValueChanged(sender As Object, e As EventArgs) Handles CLB_Design.SelectedValueChanged
        Try
            If ClientName = "AVIS" Then FILLCLBCOLOR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTCUT.KeyPress
        numdotkeypress(e, sender, Me)

    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim <> "" Then CMDSEARCH_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectYarnStockGdnTransfer_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ALLOWBARCODEPRINT = False Then
                LBLENTRYNO.Visible = False
                TXTENTRYNO.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class