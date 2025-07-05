
Imports BL

Public Class CategoryDetails

    Public FRMSTRING As String      'Used for form Category or GRade

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CategoryDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        End If
    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        Try
            showform(True, gridledger.GetFocusedRowCellValue("NAME"), gridledger.GetFocusedRowCellValue("ID"))
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CategoryDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If frmstring = "CATEGORY" Then
                Me.Text = "Category Master"
            ElseIf frmstring = "BASE" Then
                Me.Text = "Base Master"
            ElseIf frmstring = "MATERIAL TYPE" Then
                Me.Text = "Material Type Master"
            ElseIf frmstring = "COLOR" Then
                Me.Text = "Color Master"
            ElseIf frmstring = "DISTRICT" Then
                Me.Text = "District Master"
            ElseIf frmstring = "DEPARTMENT" Then
                Me.Text = "Department Master"
            ElseIf frmstring = "PIECE TYPE" Then
                Me.Text = "Piece Type Master"
            ElseIf frmstring = "RATE TYPE" Then
                Me.Text = "Rate Type Master"
            ElseIf frmstring = "NARRATION" Then
                Me.Text = "Narration Master"
            ElseIf frmstring = "PARTYBANK" Then
                Me.Text = "Bank Name Master"
            ElseIf frmstring = "PROCESS" Then
                Me.Text = "Process Master"
            ElseIf frmstring = "QUALITY" Then
                Me.Text = "Quality Master"
            ElseIf frmstring = "GODOWN" Then
                Me.Text = "Godown Master"
            ElseIf FRMSTRING = "SAMPLETYPE" Then
                Me.Text = "Sample Type Master"
            ElseIf FRMSTRING = "DYEDTYPE" Then
                Me.Text = "Dyed Type Master"
            ElseIf FRMSTRING = "COSTCENTER" Then
                Me.Text = "Cost Center Details"
            End If
            fillgrid()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Dim dttable As New DataTable
        Dim objClsCommon As New ClsCommonMaster

        If frmstring = "CATEGORY" Then
            dttable = objClsCommon.search(" category_name AS NAME, category_id AS ID", "", "categorymaster", " and category_Yearid = " & YearId & " ORDER BY CATEGORY_NAME")
        ElseIf frmstring = "BASE" Then
            dttable = objClsCommon.search(" BASE_name AS NAME, BASE_id AS ID", "", "BASEmaster", " and BASE_Yearid = " & YearId & " ORDER BY BASE_NAME")
        ElseIf frmstring = "MATERIAL TYPE" Then
            dttable = objClsCommon.search(" material_name AS NAME, material_id AS ID", "", "materialtypemaster", " and material_Yearid = " & YearId & " ORDER Byte MATERIAL_NAME")
        ElseIf frmstring = "COLOR" Then
            dttable = objClsCommon.search(" COLOR_name AS NAME, COLOR_id AS ID", "", "COLORmaster", " and COLOR_Yearid = " & YearId & " ORDER BY COLOR_NAME")
        ElseIf frmstring = "DISTRICT" Then
            dttable = objClsCommon.search(" DISTRICT_name AS NAME, DISTRICT_id AS ID", "", "DISTRICTmaster", " and DISTRICT_Yearid = " & YearId & " ORDER BY DISTRICT_NAME")
        ElseIf frmstring = "DEPARTMENT" Then
            dttable = objClsCommon.search(" DEPARTMENT_name AS NAME, DEPARTMENT_id AS ID", "", "DEPARTMENTmaster", " and DEPARTMENT_Yearid = " & YearId & " ORDER BY DEPARTMENT_NAME")
        ElseIf frmstring = "PIECE TYPE" Then
            dttable = objClsCommon.search(" PIECETYPE_name AS NAME, PIECETYPE_id AS ID", "", "PIECEtypemaster", " and PIECETYPE_Yearid = " & YearId & " ORDER BY PIECETYPE_NAME")
        ElseIf frmstring = "NARRATION" Then
            dttable = objClsCommon.search(" NARRATION_name AS NAME, NARRATION_id AS ID", "", "NARRATIONmaster", " AND NARRATION_Yearid = " & YearId & " ORDER BY NARRATION_NAME")
        ElseIf frmstring = "PARTYBANK" Then
            dttable = objClsCommon.search(" PARTYBANK_name AS NAME, PARTYBANK_id AS ID", "", "PARTYBANKmaster", " and PARTYBANK_Yearid = " & YearId & " ORDER BY PARTYBANK_NAME")
        ElseIf frmstring = "PROCESS" Then
            dttable = objClsCommon.search(" PROCESS_name AS NAME, PROCESS_id AS ID", "", "PROCESSmaster", " and PROCESS_Yearid = " & YearId & " ORDER BY PROCESS_NAME")
        ElseIf frmstring = "GODOWN" Then
            dttable = objClsCommon.search(" GODOWN_name AS NAME, GODOWN_id AS ID", "", "GODOWNmaster", " and GODOWN_Yearid = " & YearId & " ORDER BY GODOWN_NAME")
        ElseIf frmstring = "QUALITY" Then
            dttable = objClsCommon.search(" QUALITY_name AS NAME, QUALITY_id AS ID", "", "QUALITYmaster", " and QUALITY_Yearid = " & YearId & " ORDER BY QUALITY_NAME")
        ElseIf frmstring = "SAMPLETYPE" Then
            dttable = objClsCommon.search(" SAMPLETYPE_name AS NAME, SAMPLETYPE_id AS ID", "", "SAMPLETYPEmaster", " and SAMPLETYPE_Yearid = " & YearId & " ORDER BY SAMPLETYPE_NAME")
        ElseIf frmstring = "DYEDTYPE" Then
            dttable = objClsCommon.search(" DYEDTYPE_name AS NAME, DYEDTYPE_id AS ID", "", "DYEDTYPEmaster", " and DYEDTYPE_Yearid = " & YearId & " ORDER BY DYEDTYPE_NAME")
        ElseIf frmstring = "COSTCENTER" Then
            dttable = objClsCommon.search(" COSTCENTER_name AS NAME, COSTCENTER_id AS ID", "", "COSTCENTERmaster", " and COSTCENTER_Yearid = " & YearId & " ORDER BY COSTCENTER_NAME")
        End If

        gridname.DataSource = dttable
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal name As String, ByVal id As Integer)
        Try
            Dim objCategorymaster As New CategoryMaster
            objCategorymaster.EDIT = editval
            objCategorymaster.MdiParent = MDIMain
            objCategorymaster.frmString = FRMSTRING
            objCategorymaster.TempName = name
            objCategorymaster.TempID = id
            objCategorymaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        Try
            showform(False, "", 0)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridname_DoubleClick(sender As Object, e As EventArgs) Handles gridname.DoubleClick
        Try
            showform(True, gridledger.GetFocusedRowCellValue("NAME"), gridledger.GetFocusedRowCellValue("ID"))
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class