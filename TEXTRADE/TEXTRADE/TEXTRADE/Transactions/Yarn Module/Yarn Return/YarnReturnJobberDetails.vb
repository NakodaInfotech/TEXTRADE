

Imports BL
Imports System.IO
Imports System.Windows.Forms

Public Class YarnReturnJobberDetails

    Public EDIT As Boolean
    Dim temppreqno As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub GRNDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub GRNDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            fillgrid(" and dbo.YARNKNITTINGRETURN.YARNRET_yearid=" & YearId & " order by dbo.YARNKNITTINGRETURN.YARNRET_NO ")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" ISNULL(YARNKNITTINGRETURN.YARNRET_NO, 0) AS SRNO, ISNULL(YARNKNITTINGRETURN.YARNRET_date, GETDATE()) AS DATE, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(YARNKNITTINGRETURN.YARNRET_CHALLANNO, '') AS CHALLANNO, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, ISNULL(YARNKNITTINGRETURN.YARNRET_TOTALBAGS, 0) AS TOTALBAGS, ISNULL(YARNKNITTINGRETURN.YARNRET_TOTALWT, 0) AS TOTALWT, ISNULL(YARNKNITTINGRETURN.YARNRET_TOTALCONES, 0) AS TOTALCONES, ISNULL(YARNKNITTINGRETURN.YARNRET_remarks, '') AS REMARKS, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(PROCESSMASTER.PROCESS_NAME, '') AS PROCESS, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS YARNQUALITY, ISNULL(MILLMASTER.MILL_NAME, '') AS MILL, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(YARNKNITTINGRETURN_DESC.YARNRET_BARCODE, '') AS BARCODE, ISNULL(YARNKNITTINGRETURN_DESC.YARNRET_LOTNO, '') AS LOTNO, ISNULL(YARNKNITTINGRETURN_DESC.YARNRET_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(YARNKNITTINGRETURN_DESC.YARNRET_LRNO,'') AS LRNO , ISNULL(YARNKNITTINGRETURN_DESC.YARNRET_BAGS, 0) AS BAGS, ISNULL(YARNKNITTINGRETURN_DESC.YARNRET_WT, 0) AS WT, ISNULL(YARNKNITTINGRETURN_DESC.YARNRET_CONES, 0) AS CONES, ISNULL(YARNKNITTINGRETURN.YARNRET_VEHICLENO, '') AS VEHICLENO, ISNULL(RACKMASTER.RACK_NAME, '') AS RACK   ", "", " YARNKNITTINGRETURN INNER JOIN GODOWNMASTER ON YARNKNITTINGRETURN.YARNRET_GODOWNID = GODOWNMASTER.GODOWN_id INNER JOIN LEDGERS ON YARNKNITTINGRETURN.YARNRET_LEDGERID = LEDGERS.Acc_id INNER JOIN PROCESSMASTER ON YARNKNITTINGRETURN.YARNRET_PROCESSID = PROCESSMASTER.PROCESS_ID INNER JOIN YARNKNITTINGRETURN_DESC ON YARNKNITTINGRETURN.YARNRET_NO = YARNKNITTINGRETURN_DESC.YARNRET_NO AND  YARNKNITTINGRETURN.YARNRET_yearid = YARNKNITTINGRETURN_DESC.YARNRET_YEARID INNER JOIN YARNQUALITYMASTER ON YARNKNITTINGRETURN_DESC.YARNRET_YARNQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN RACKMASTER ON YARNKNITTINGRETURN_DESC.YARNRET_RACKID = RACKMASTER.RACK_ID LEFT OUTER JOIN MILLMASTER ON YARNKNITTINGRETURN_DESC.YARNRET_MILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN COLORMASTER ON YARNKNITTINGRETURN_DESC.YARNRET_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON YARNKNITTINGRETURN_DESC.YARNRET_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON YARNKNITTINGRETURN.YARNRET_transledgerid = TRANSLEDGERS.Acc_id   ", TEMPCONDITION)
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
                Dim objGRN As New YarnReturnJobber
                objGRN.MdiParent = MDIMain
                objGRN.EDIT = editval
                objGRN.TEMPYARNKNITTINGRETNO = SRNO
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

    Private Sub CMDSAVELAYOUT_Click(sender As Object, e As EventArgs) Handles CMDSAVELAYOUT.Click
        Try
            Dim layoutFileName As String = $"{Me.Name}"
            Dim layoutPath As String = System.IO.Path.Combine(Application.StartupPath, layoutFileName)
            gridbill.SaveLayoutToXml(layoutPath)
            'MessageBox.Show("Layout saved as: " & layoutFileName)




            ' Prompt user for filename
            Dim userFileName As String = InputBox("Enter a name for the layout file (without extension):", "Save Layout", Me.Name)

            ' Exit if the user cancels or enters nothing
            If String.IsNullOrWhiteSpace(userFileName) Then
                MessageBox.Show("Save cancelled.")
                Exit Sub
            End If

            ' Add .xml extension and construct path
            Dim FileName As String = $"{userFileName}.xml"

            ' Save layout to file
            gridbill.SaveLayoutToXml(layoutPath)
            MessageBox.Show("Layout saved as: " & FileName)

            ' Read file content
            Dim xmlContent As String = File.ReadAllText(layoutPath)



            Dim OBJSELECTSG As New SelectCustomLayout
            OBJSELECTSG.FORMNAMES = layoutFileName
            OBJSELECTSG.FILENAME = FileName
            OBJSELECTSG.FILES = xmlContent
            OBJSELECTSG.ShowDialog()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid(" and dbo.YARNKNITTINGRETURN.YARNRET_yearid=" & YearId & " order by dbo.YARNKNITTINGRETURN.YARNRET_NO ")
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

            Dim PATH As String = Application.StartupPath & "\Yarn Return Knitting Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Yarn Return Knitting Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Return Knitting Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn Return Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub YarnReturnJobberDetails_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If ClientName = "SWPL" Then
            gridbill.Columns("LRNO").Caption = "Box No"
        End If
    End Sub
End Class