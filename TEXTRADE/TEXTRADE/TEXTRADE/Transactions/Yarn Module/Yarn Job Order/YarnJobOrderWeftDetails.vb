
Imports System.IO
Imports System.Runtime.InteropServices
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class YarnJobOrderWeftDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public multi As Boolean = False


    Private Sub InvoiceGridDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon

            Dim dt As DataTable = OBJCMN.SEARCH(" ISNULL(JOBORDER_WEFTMATCHING.JOB_NO, 0)AS JOBNO,ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTSRNO, 0) AS WEFTGRIDSRNO, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTSYM, '') AS WEFTGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WEFTYARNQUALITY, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTDENIER, 0) AS WEFTDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WEFTMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WEFTSHADE, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTPE, 0) AS WEFTPE, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTBE, 0) AS WEFTBE, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTTE, 0) AS WEFTTE, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTWT, 0) AS WEFTWT, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTCONS, 0) AS WEFTCONS, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTRATE, 0) AS WEFTRATE, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTCOST, 0) AS WEFTCOST  ", "", " JOBORDER_WEFTMATCHING LEFT OUTER JOIN COLORMASTER ON JOBORDER_WEFTMATCHING.JOB_WEFTCOLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN MILLMASTER ON JOBORDER_WEFTMATCHING.JOB_WEFTMILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON JOBORDER_WEFTMATCHING.JOB_WEFTYARNQUALITYID = YARNQUALITYMASTER.YARN_ID  ", " AND JOBORDER_WEFTMATCHING.JOB_YEARID = " & YearId & " ORDER BY JOBNO,WEFTGRIDSRNO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal billno As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJBILL As New YarnJobOrder
                OBJBILL.MdiParent = MDIMain
                OBJBILL.EDIT = editval
                OBJBILL.tempdesignno = billno
                OBJBILL.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("JOBNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        fillgrid()
    End Sub

    Private Sub gridINVOICE_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("JOBNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InvoiceGridDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Yarn Job Order Weft Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Yarn Job Order Weft Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Job Order Weft Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn Job Order Weft Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub



End Class