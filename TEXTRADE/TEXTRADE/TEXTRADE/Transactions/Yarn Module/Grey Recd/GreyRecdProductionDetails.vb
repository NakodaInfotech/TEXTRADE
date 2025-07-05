
Imports BL
Imports System.Windows.Forms

Public Class GreyRecdProductionDetails
    Public EDIT As Boolean
    Dim TEMPGREYNO As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GREYRECDPRODUCTIONDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub GREYRECDPRODUCTIONDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid(" and dbo.GREYRECDPRODUCTION.GREY_yearid=" & YearId & " order by dbo.GREYRECDPRODUCTION.GREY_no ")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("ISNULL(GREYRECDPRODUCTION.GREY_no, 0) AS SRNO, ISNULL(GREYRECDPRODUCTION.GREY_date, GETDATE()) AS DATE, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(MACHINE_NAME, '') AS MACHINENAME, ISNULL(GREYRECDPRODUCTION.GREY_challanno, '') AS CHALLANNO, ISNULL(GREYRECDPRODUCTION.GREY_TOTALQTY, 0) AS TOTALQTY, ISNULL(GREYRECDPRODUCTION.GREY_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(GREYRECDPRODUCTION.GREY_TOTALWT, 0) AS TOTALWT, ISNULL(GREYRECDPRODUCTION.GREY_remarks, '') AS REMARKS, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(GREYRECDPRODUCTION_DESC.GREY_ROLLNO, '') AS ROLLNO, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS YARNQUALITY, ISNULL(GREYRECDPRODUCTION_DESC.GREY_QTY, 0) AS QTY, ISNULL(GREYRECDPRODUCTION_DESC.GREY_MTRS, 0) AS MTRS, ISNULL(GREYRECDPRODUCTION_DESC.GREY_WT, 0) AS WT", "", " GODOWNMASTER INNER JOIN GREYRECDPRODUCTION ON GODOWNMASTER.GODOWN_id = GREYRECDPRODUCTION.GREY_GODOWNID INNER JOIN MACHINEMASTER ON GREYRECDPRODUCTION.GREY_MACHINEID = MACHINEMASTER.MACHINE_ID INNER JOIN GREYRECDPRODUCTION_DESC ON GREYRECDPRODUCTION.GREY_NO = GREYRECDPRODUCTION_DESC.GREY_NO AND GREYRECDPRODUCTION.GREY_yearid = GREYRECDPRODUCTION_DESC.GREY_YEARID INNER JOIN ITEMMASTER ON GREYRECDPRODUCTION_DESC.GREY_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN YARNQUALITYMASTER ON GREYRECDPRODUCTION_DESC.GREY_QUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN COLORMASTER ON GREYRECDPRODUCTION_DESC.GREY_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GREYRECDPRODUCTION_DESC.GREY_DESIGNID = DESIGNMASTER.DESIGN_id  ", TEMPCONDITION)
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
                Dim objGRN As New GreyRecdProduction
                objGRN.MdiParent = MDIMain
                objGRN.EDIT = editval
                objGRN.TEMPGREYNO = SRNO
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
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid(" and dbo.GREYRECDPRODUCTION.GREY_yearid=" & YearId & " order by dbo.GREYRECDPRODUCTION.GREY_no ")
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

            Dim PATH As String = Application.StartupPath & "\Grey Recd Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Grey Recd Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Grey Recd Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Grey Recd Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid(" and dbo.GREYRECDPRODUCTION.GREY_yearid=" & YearId & " order by dbo.GREYRECDPRODUCTION.GREY_no ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class