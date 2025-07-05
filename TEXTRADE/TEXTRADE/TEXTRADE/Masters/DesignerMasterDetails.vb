
Imports BL

Public Class DesignerMasterDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public TEMPID As Integer

    Private Sub DesignerMasterDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
            Try
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("ISNULL(DESIGNERMASTER.DESIGNER_NAME, '') AS DESIGNERNAME, ISNULL(DESIGNERMASTER.DESIGNER_ID,0) AS TEMPID, ISNULL(DESIGNERMASTER.DESIGNER_MOBILENO, '') AS MOBILENO, ISNULL(DESIGNERMASTER.DESIGNER_DCODE, '') AS DCODE, ISNULL(EMAILMASTER.email_id, '') AS EMAIL ", "", " DESIGNERMASTER LEFT OUTER JOIN EMAILMASTER ON DESIGNERMASTER.DESIGNER_EMAILID = EMAILMASTER.email_id ", " and   DESIGNERMASTER.DESIGNER_cmpid = " & CmpId & "  and DESIGNERMASTER.DESIGNER_yearid = " & YearId)
            GRIDBILLDETAILS.DataSource = dt
                If dt.Rows.Count > 0 Then
                    GRIDBILL.FocusedRowHandle = GRIDBILL.RowCount - 1
                    GRIDBILL.TopRowIndex = GRIDBILL.RowCount - 15
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Sub SHOWFORM(ByVal EDITVAL As Boolean, ByVal ID As Integer)
            Try
                If (EDITVAL = True And USEREDIT = False And USERVIEW = False) Or (EDITVAL = False And USERADD = False) Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

            Dim OBJDEPT As New DesignerMaster
            OBJDEPT.MdiParent = MDIMain
                OBJDEPT.EDIT = EDITVAL
                OBJDEPT.TEMPID = ID
                OBJDEPT.Show()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub CMDADDNEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDADDNEW.Click
            Try
                SHOWFORM(False, 0)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub CMDEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
            Try
                SHOWFORM(True, GRIDBILL.GetFocusedRowCellValue("TEMPID"))
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
            Me.Close()
        End Sub

        Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
            Try
                FILLGRID()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            SHOWFORM(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBILL_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GRIDBILL.DoubleClick
        Try
            SHOWFORM(True, GRIDBILL.GetFocusedRowCellValue("TEMPID"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(sender As Object, e As EventArgs) Handles ExcelExport.Click
            Try
            Dim PATH As String = Application.StartupPath & "\Designer Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
                opti.ShowGridLines = True
            opti.SheetName = "Designer Details"
            GRIDBILL.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Designer Details", GRIDBILL.VisibleColumns.Count + GRIDBILL.GroupCount)
        Catch ex As Exception
            MsgBox("Designer Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
        End Sub

    Private Sub DesignerMasterDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class