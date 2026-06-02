Imports BL
Imports System.Windows.Forms

Public Class YarnLoanMasterDetails

    Public EDIT As Boolean
    Dim TEMPLOANNO As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

        Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
            Try
                Me.Close()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    Private Sub YarnLoanMasterDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub YarnLoanMasterDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'YARN RECD'")
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

    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclsCMST.search("  YARNLOAN.YARNLOAN_no AS YARNNO, YARNLOAN.YARNLOAN_date AS DATE, YARNLOAN.YARNLOAN_TYPE AS TYPE, ISNULL(YARNLOAN.YARNLOAN_remarks, '') AS REMARKS, LEDGERS.Acc_cmpname AS PARTYNAME, GODOWNMASTER.GODOWN_name AS GODOWN, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, YARNLOAN_DESC.YARNLOAN_GRIDSRNO AS GRIDSRNO, YARNQUALITYMASTER.YARN_NAME AS YARNNAME, ISNULL(MILLMASTER.MILL_NAME, '') AS MILLNAME, ISNULL(YARNLOAN_DESC.YARNLOAN_LOTNO, '') AS LOTNO, ISNULL(YARNLOAN_DESC.YARNLOAN_BAGS, '') AS BAGS, ISNULL(YARNLOAN_DESC.YARNLOAN_WT, 0) AS WT, ISNULL(YARNLOAN_DESC.YARNLOAN_CONES, 0) AS CONES, ISNULL(YARNLOAN_DESC.YARNLOAN_LRNO, '') AS LRNO, YARNLOAN_DESC.YARNLOAN_LRDATE AS LRDATE, ISNULL(YARNLOAN_DESC.YARNLOAN_DONE, 0) AS DONE, ISNULL(YARNLOAN_DESC.YARNLOAN_FROMNO, 0) AS FROMNO, ISNULL(YARNLOAN_DESC.YARNLOAN_FROMSRNO, 0) AS FROMSRNO, ISNULL(YARNLOAN_DESC.YARNLOAN_BARCODE, 0) AS BARCODE, ISNULL(RACKMASTER.RACK_NAME, '') AS RACK, ISNULL(YARNLOAN_DESC.YARNLOAN_OUTBAGS, 0) AS OUTBAGS, ISNULL(YARNLOAN_DESC.YARNLOAN_OUTWT, 0) AS OUTWT ", "", " LEDGERS AS TRANSLEDGERS RIGHT OUTER JOIN GODOWNMASTER INNER JOIN YARNLOAN INNER JOIN YARNLOAN_DESC ON YARNLOAN.YARNLOAN_no = YARNLOAN_DESC.YARNLOAN_NO AND YARNLOAN.YARNLOAN_yearid = YARNLOAN_DESC.YARNLOAN_YEARID INNER JOIN YARNQUALITYMASTER ON YARNLOAN_DESC.YARNLOAN_YARNQUALITYID = YARNQUALITYMASTER.YARN_ID AND YARNLOAN_DESC.YARNLOAN_YEARID = YARNQUALITYMASTER.YARN_YEARID INNER JOIN LEDGERS ON YARNLOAN.YARNLOAN_yearid = LEDGERS.Acc_yearid AND YARNLOAN.YARNLOAN_NAMEid = LEDGERS.Acc_id ON GODOWNMASTER.GODOWN_yearid = YARNLOAN.YARNLOAN_yearid AND  GODOWNMASTER.GODOWN_id = YARNLOAN.YARNLOAN_GODOWNID LEFT OUTER JOIN RACKMASTER ON YARNLOAN_DESC.YARNLOAN_RACKID = RACKMASTER.RACK_ID AND YARNLOAN_DESC.YARNLOAN_YEARID = RACKMASTER.RACK_YEARID LEFT OUTER JOIN MILLMASTER ON YARNLOAN_DESC.YARNLOAN_MILLID = MILLMASTER.MILL_ID AND YARNLOAN_DESC.YARNLOAN_YEARID = MILLMASTER.MILL_YEARID ON  TRANSLEDGERS.Acc_yearid = YARNLOAN.YARNLOAN_yearid AND TRANSLEDGERS.Acc_id = YARNLOAN.YARNLOAN_TRANSID   ", " AND YARNLOAN.YARNLOAN_yearid = '" & YearId & "' ORDER BY YARNLOAN.YARNLOAN_no")
            gridbilldetails.DataSource = dt

            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal SRNO As Integer)
            Try
                If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objGRN As New YarnLoanMaster
                objGRN.MdiParent = MDIMain
                    objGRN.EDIT = editval
                objGRN.TEMPLOANNO = SRNO
                objGRN.Show()
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
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

        Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
            Try
            showform(True, gridbill.GetFocusedRowCellValue("YARNNO"))
        Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
            Try
            fillgrid()
        Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
            Try
            showform(True, gridbill.GetFocusedRowCellValue("YARNNO"))
        Catch ex As Exception
                Throw ex
            End Try
        End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Yarn loan Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Grey Recd Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Loan Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn loan Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        fillgrid()
    End Sub
    End Class




