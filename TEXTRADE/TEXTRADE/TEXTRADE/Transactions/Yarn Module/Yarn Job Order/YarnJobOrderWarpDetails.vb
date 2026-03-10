
Imports System.IO
Imports System.Runtime.InteropServices
Imports BL
Imports DevExpress.XtraGrid.Views.Grid
Public Class YarnJobOrderWarpDetails

    Dim SALEREGID As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public multi As Boolean = False
    Public fromno, tono As Integer
    Public PARTYNAME As String

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

            Dim dt As DataTable = OBJCMN.SEARCH(" ISNULL(JOBORDER_WARPMATCHING.JOB_NO, 0)AS JOBNO,ISNULL(JOBORDER_WARPMATCHING.JOB_WARPSRNO, 0) As WARPGRIDSRNO, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPSYM, '') AS WARPGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WARPYARNQUALITY, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPDENIER, 0) AS WARPDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WARPMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WARPSHADE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPPE, 0) AS WARPPE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPBE, 0) AS WARPBE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPTE, 0) AS WARPTE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPWT, 0.000) AS WARPWT, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPCONS, 0) AS WARPCONS, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPRATE, 0) AS WARPRATE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPCOST, 0) AS WARPCOST ", "", " JOBORDER_WARPMATCHING INNER JOIN YARNQUALITYMASTER ON JOBORDER_WARPMATCHING.JOB_WARPYARNQUALITYID = YARNQUALITYMASTER.YARN_ID AND JOBORDER_WARPMATCHING.JOB_YEARID = YARNQUALITYMASTER.YARN_YEARID LEFT OUTER JOIN MILLMASTER ON JOBORDER_WARPMATCHING.JOB_YEARID = MILLMASTER.MILL_YEARID AND MILLMASTER.MILL_ID = JOBORDER_WARPMATCHING.JOB_WARPMILLID LEFT OUTER JOIN COLORMASTER ON JOBORDER_WARPMATCHING.JOB_YEARID = COLORMASTER.COLOR_yearid AND COLORMASTER.COLOR_id = JOBORDER_WARPMATCHING.JOB_WARPCOLORID ", "AND JOBORDER_WARPMATCHING.JOB_YEARID = " & YearId & " ORDER BY JOBNO,WARPGRIDSRNO")
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
                OBJBILL.TEMPJONO = billno
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

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        fillgrid()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("JOBNO"))
        Catch ex As Exception
            Throw ex
        End Try
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
            DTROW = USERRIGHTS.Select("FormName = 'YARN JOBORDER'")
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
            Dim PATH As String = Application.StartupPath & "\Yarn Job Order Warp Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Yarn Job Order Warp Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Job Order Warp Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn Job Order Warp Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub



End Class