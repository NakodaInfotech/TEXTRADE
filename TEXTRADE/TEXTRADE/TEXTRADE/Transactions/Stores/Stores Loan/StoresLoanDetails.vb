Imports BL
Imports System.Windows.Forms
Public Class StoresLoanDetails
    Public edit As Boolean
    Dim TEMPLoanNO As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub PRequisitionDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                cmdok_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PRequisitionDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DTROW() As DataRow
        DTROW = USERRIGHTS.Select("FormName = 'STORES'")
        USERADD = DTROW(0).Item(1)
        USEREDIT = DTROW(0).Item(2)
        USERVIEW = DTROW(0).Item(3)
        USERDELETE = DTROW(0).Item(4)

        If USEREDIT = False And USERVIEW = False Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        fillgrid(" and dbo.STORESLOAN.STORLOAN_YEARID=" & YearId & " order by dbo.STORESLOAN.STORLOAN_NO ")
    End Sub

    Sub fillgrid(ByVal tepmcondition)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclsCMST.search("     ISNULL(STORESLOAN.STORLOAN_no,0 )AS SRNO ,STORESLOAN.STORLOAN_date AS LOANDATE, ISNULL(STORESLOAN.STORLOAN_TYPE,'') AS LOAN, ISNULL(STORESLOAN.STORLOAN_TOTALQTY,0) AS TOTALQTY, ISNULL(STORESLOAN.STORLOAN_remarks, '') AS REMARKS, ISNULL(STORESLOAN_DESC.STORLOAN_GRIDSRNO,0) AS GRIDSRNO, ISNULL(STOREITEMMASTER.STOREITEM_NAME,'') AS ITEMNAME, ISNULL(STORESLOAN_DESC.STORLOAN_DESC, '') AS [DESC], STORESLOAN_DESC.STORLOAN_QTY AS QTY, ISNULL(UNITMASTER.unit_abbr, '') AS QTYUNIT, LEDGERS.Acc_cmpname AS NAME, STORESLOAN.STORLOAN_TOTALAMT AS TOTALAMT, STORESLOAN_DESC.STORLOAN_AMT AS AMT ", "", "  STORESLOAN INNER JOIN STORESLOAN_DESC ON STORESLOAN.STORLOAN_cmpid = STORESLOAN_DESC.STORLOAN_cmpid AND STORESLOAN.STORLOAN_locationid = STORESLOAN_DESC.STORLOAN_locationid AND STORESLOAN.STORLOAN_yearid = STORESLOAN_DESC.STORLOAN_yearid AND STORESLOAN.STORLOAN_no = STORESLOAN_DESC.STORLOAN_no INNER JOIN STOREITEMMASTER ON STORESLOAN_DESC.STORLOAN_ITEMID = STOREITEMMASTER.STOREITEM_ID AND STORESLOAN_DESC.STORLOAN_cmpid = STOREITEMMASTER.STOREITEM_CMPID AND STORESLOAN_DESC.STORLOAN_yearid = STOREITEMMASTER.STOREITEM_YEARID INNER JOIN LEDGERS ON STORESLOAN.STORLOAN_NAMEid = LEDGERS.Acc_id AND STORESLOAN.STORLOAN_cmpid = LEDGERS.Acc_cmpid AND STORESLOAN.STORLOAN_locationid = LEDGERS.Acc_locationid AND STORESLOAN.STORLOAN_yearid = LEDGERS.Acc_yearid LEFT OUTER JOIN UNITMASTER ON STORESLOAN_DESC.STORLOAN_yearid = UNITMASTER.unit_yearid AND STORESLOAN_DESC.STORLOAN_locationid = UNITMASTER.unit_locationid AND STORESLOAN_DESC.STORLOAN_cmpid = UNITMASTER.unit_cmpid AND STORESLOAN_DESC.STORLOAN_UNITID = UNITMASTER.unit_id", tepmcondition)
            If dt.Rows.Count > 0 Then
                gridbilldetails.DataSource = dt
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal LoanNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objREQ As New StoresLoan
                objREQ.MdiParent = MDIMain
                objREQ.edit = editval
                objREQ.TEMPloanNO = LoanNO
                objREQ.Show()
                'Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            'If USERADD = False Then
            '    MsgBox("Insufficient Rights")
            '    Exit Sub
            'End If
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Loan Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Loan Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Loan Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class