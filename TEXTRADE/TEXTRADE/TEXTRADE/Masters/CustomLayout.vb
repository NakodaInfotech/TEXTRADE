Imports BL
Imports DevExpress.CodeParser
Imports DevExpress.Entity.ProjectModel

Public Class CustomLayout
    Private Sub CustomLayout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLGRID()
        Dim dttable As New DataTable
        Dim objClsCommon As New ClsCommonMaster

        If UserName = "Admin" Then
            dttable = objClsCommon.search("0 AS SRNO, CUSTOMLAYOUTS.CL_FormName AS FORMNAME,CUSTOMLAYOUTS.CL_FILENAME AS FILENAME, CUSTOMLAYOUTS.CL_LayoutContent AS CONTENT, USERMASTER.User_Name AS USERNAME ", "", " CUSTOMLAYOUTS INNER JOIN CMPMASTER ON CUSTOMLAYOUTS.CL_CMPID = CMPMASTER.cmp_id LEFT OUTER JOIN USERMASTER ON CUSTOMLAYOUTS.CL_UserId = USERMASTER.User_id ", "  and CL_CMPID = " & CmpId & " order by FORMNAME")
        Else
            dttable = objClsCommon.search("0 AS SRNO, CUSTOMLAYOUTS.CL_FormName AS FORMNAME,CUSTOMLAYOUTS.CL_FILENAME AS FILENAME, CUSTOMLAYOUTS.CL_LayoutContent AS CONTENT, USERMASTER.User_Name AS USERNAME ", "", " CUSTOMLAYOUTS LEFT OUTER JOIN USERMASTER ON CUSTOMLAYOUTS.CL_UserId = USERMASTER.User_id ", " and CL_CMPID = " & CmpId & " and USERMASTER.User_Name= '" & UserName & "' order by FORMNAME")
        End If
        GRIDNAME.DataSource = dttable
    End Sub
    Sub showform(ByVal editval As Boolean, ByVal FORMNAME As String)
        Try
            'If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
            '    MsgBox("Insufficient Rights")
            '    Exit Sub
            'End If

            If (editval = False) Or (editval = True And gridledger.RowCount > 0) Then
                Dim objItemmaster As New CustomLayout
                objItemmaster.MdiParent = MDIMain
                'objItemmaster.tempFORMName = FORMNAME
                objItemmaster.Show()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDNAME_DoubleClick(sender As Object, e As EventArgs) Handles GRIDNAME.DoubleClick
        Try
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(GRIDNAME.FocusedView, DevExpress.XtraGrid.Views.Grid.GridView)
            Dim rowHandle As Integer = view.FocusedRowHandle
            If rowHandle < 0 Then Exit Sub

            Dim fILEName As String = view.GetRowCellValue(rowHandle, "FILENAME").ToString()
            Dim formName As String = view.GetRowCellValue(rowHandle, "FORMNAME").ToString()
            Dim layoutXml As String = view.GetRowCellValue(rowHandle, "CONTENT").ToString()

            ' Load the form by name dynamically
            Dim frm As Form = LoadFormByName(formName)

            If frm IsNot Nothing Then
                frm.MdiParent = MDIMain
                frm.Show()

                Application.DoEvents()

                ' Find the grid control (change name if not GridControl1)
                Dim grid As DevExpress.XtraGrid.GridControl = TryCast(frm.Controls.Find("gridbilldetails", True).FirstOrDefault(), DevExpress.XtraGrid.GridControl)

                If grid IsNot Nothing Then
                    Using ms As New IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(layoutXml))
                        grid.MainView.RestoreLayoutFromStream(ms)
                    End Using
                Else
                    MsgBox("GridControl not found inside form '" & fILEName & "'.", MsgBoxStyle.Critical)
                End If
            Else
                MsgBox("Form '" & formName & "' not found in the project.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Public Function LoadFormByName(formName As String) As Form
        Try
            ' Get the current assembly
            Dim asm As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly()

            ' Try to find the type with the matching name (case-sensitive)
            Dim formType As Type = asm.GetTypes().FirstOrDefault(Function(t) t.Name.ToUpper() = formName.ToUpper() AndAlso t.IsSubclassOf(GetType(Form)))

            If formType IsNot Nothing Then
                Dim frm As Form = CType(Activator.CreateInstance(formType), Form)
                Return frm
            End If

            Return Nothing
        Catch ex As Exception
            MsgBox("Error loading form by name: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub GRIDNAME_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDNAME.KeyDown
        Try
            If e.KeyCode <> Keys.Delete Then Exit Sub
            'If UserName = "Admin" Then
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView =
                CType(GRIDNAME.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                If view.SelectedRowsCount = 0 Then Exit Sub

                If MsgBox("Delete selected record(s)?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub

                Dim objClsCommon As New ClsCommon

                For Each rowHandle In view.GetSelectedRows()
                    If rowHandle < 0 Then Continue For

                    Dim FILENAME As String = view.GetRowCellValue(rowHandle, "FILENAME")

                    ' DELETE QUERY
                    Dim sql As String = "DELETE FROM CUSTOMLAYOUTS WHERE CL_FILENAME = '" & FILENAME & "'"

                    objClsCommon.Execute_Any_String(sql, "", "") ' <-- You must have this function
                Next

                ' Refresh grid
                FILLGRID()
            'End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class