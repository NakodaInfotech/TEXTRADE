Imports BL
Imports System.IO
Imports DevExpress.XtraGrid.Views.Base


Public Class IssueToDesignerDetails


    Dim USERADD, USEREDIT, USERDELETE, USERVIEW As Boolean
    Public TEMPMANUALNO As Integer

        Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
            Me.Close()
        End Sub
        Private Sub ManualEntryDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
            Try
                If e.KeyCode = Windows.Forms.Keys.Escape Then
                    Me.Close()
                ElseIf e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemPipe Then
                    e.SuppressKeyPress = True
                ElseIf e.KeyCode = Keys.E And e.Alt = True Then
                    Call PrintToolStripButton_Click(sender, e)
                ElseIf e.KeyCode = Keys.R And e.Alt = True Then
                    Call ToolStripRefresh_Click(sender, e)
                ElseIf e.KeyCode = Keys.N And e.Control = True Then
                    showform(False, 0)
                End If
            Catch ex As Exception
                If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
            End Try
        End Sub

        Private Sub ManualEntryDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Dim DTROW() As DataRow
        DTROW = USERRIGHTS.Select("FormName = 'SALE ORDER'")
        USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid()
        End Sub

        Sub fillgrid()
            Try
                Dim objclsCMST As New ClsCommonMaster
                Dim dt As DataTable
            dt = objclsCMST.search(" ISSUETODESIGNER.ISS_NO AS TEMPISSNO, ISSUETODESIGNER.ISS_DATE AS DATE, ISNULL(DESIGNERMASTER.DESIGNER_NAME, '') AS DESIGNERNAME, ISNULL(ISSUETODESIGNER.ISS_TOTALMTRS, 0) AS TOTALMTRS,  ISNULL(ISSUETODESIGNER.ISS_REMARKS, '') AS REMARKS, ISSUETODESIGNER_DESC.ISS_GRIDSRNO AS GRIDSRNO, ISNULL(ISSUETODESIGNER_DESC.ISS_ORDERNO, 0) AS ORDERNO, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(ISSUETODESIGNER_DESC.ISS_MTRS, 0)  AS MTRS, ISNULL(ISSUETODESIGNER_DESC.ISS_ORDERSRNO, 0) AS ORDERSRNO, ISNULL(ISSUETODESIGNER_DESC.ISS_ORDERTYPE, '') AS ORDERTYPE", "", " ISSUETODESIGNER INNER JOIN ISSUETODESIGNER_DESC ON ISSUETODESIGNER.ISS_NO = ISSUETODESIGNER_DESC.ISS_NO AND ISSUETODESIGNER.ISS_YEARID = ISSUETODESIGNER_DESC.ISS_YEARID INNER JOIN DESIGNERMASTER ON ISSUETODESIGNER.ISS_DESIGNERID = DESIGNERMASTER.DESIGNER_ID INNER JOIN LEDGERS ON ISSUETODESIGNER_DESC.ISS_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON ISSUETODESIGNER_DESC.ISS_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON ISSUETODESIGNER_DESC.ISS_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ISSUETODESIGNER_DESC.ISS_DESIGNID = DESIGNMASTER.DESIGN_id   ", " AND ISSUETODESIGNER.ISS_YEARID = '" & YearId & "' ORDER BY ISSUETODESIGNER.ISS_NO")
            gridbilldetails.DataSource = dt


            If dt.Rows.Count > 0 Then
                    GRIDBILL.FocusedRowHandle = GRIDBILL.RowCount - 1
                    GRIDBILL.TopRowIndex = GRIDBILL.RowCount - 15
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub



    Sub showform(ByVal editval As Boolean, ByVal SCHNO As Integer)
            Try
                If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If (editval = False) Or (editval = True And GRIDBILL.RowCount > 0) Then
                Dim objjob As New IssueToDesigner
                objjob.MdiParent = MDIMain
                    objjob.EDIT = editval
                objjob.TEMPISSNO = SCHNO
                objjob.Show()
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Private Sub ADDNEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ADDNEW.Click
            Try
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                showform(False, 0)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
            Try
            showform(True, GRIDBILL.GetFocusedRowCellValue("TEMPISSNO"))
        Catch ex As Exception
                Throw ex
            End Try
        End Sub



    Private Sub GRIDJOB_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRIDBILL.DoubleClick
            Try
            showform(True, GRIDBILL.GetFocusedRowCellValue("TEMPISSNO"))
        Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Issue To Designer Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Issue To Designer Details"
            GRIDBILL.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Issue To Designer Details", GRIDBILL.VisibleColumns.Count + GRIDBILL.GroupCount)
        Catch ex As Exception
            MsgBox("Issue To Designer Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

        Private Sub ToolStripRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripRefresh.Click
            Try
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                fillgrid()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub




End Class






