
Imports BL

Public Class SamplePriceListDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SAMPLENOTEMASTERDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub SamplePriceListDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SAMPLE MODULE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid(" and dbo.SAMPLEPRICELIST.SPL_yearid=" & YearId & " order by dbo.SAMPLEPRICELIST.SPL_NO ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("  SAMPLEPRICELIST.SPL_NO AS SPLNO, SAMPLEPRICELIST.SPL_DATE AS DATE, SAMPLEPRICELIST.SPL_MODE AS MODE, SAMPLEPRICELIST.SPL_TOTALMTRS, SAMPLEPRICELIST.SPL_TOTALAMT,   SAMPLEPRICELIST.SPL_REMARKS AS REMARKS, SAMPLEPRICELIST.SPL_REFNO AS REFNO, SAMPLEPRICELIST.SPL_CREATED, SAMPLEPRICELIST.SPL_MODIFIED, SAMPLEPRICELIST.SPL_MODIFIEDBY,   SAMPLEPRICELIST.SPL_TOTALNOOFBOOKLET, CMPMASTER.cmp_name, USERMASTER.User_Name, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(LEDGERS.Acc_cmpname, '') AS AGENT ", "", "   SAMPLEPRICELIST LEFT OUTER JOIN  LEDGERS ON SAMPLEPRICELIST.SPL_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN  GODOWNMASTER ON SAMPLEPRICELIST.SPL_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN USERMASTER ON SAMPLEPRICELIST.SPL_USERID = USERMASTER.User_id LEFT OUTER JOIN  YEARMASTER ON SAMPLEPRICELIST.SPL_YEARID = YEARMASTER.year_id LEFT OUTER JOIN  CMPMASTER ON SAMPLEPRICELIST.SPL_CMPID = CMPMASTER.cmp_id", TEMPCONDITION)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal SPLNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New SamplePriceList
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPSPLNO = SPLNO
                objPO.Show()
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
            showform(True, gridbill.GetFocusedRowCellValue("SPLNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJSPG As New SamplePriceListGridDetails
            OBJSPG.MdiParent = MDIMain
            OBJSPG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SPLNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Sample Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sample Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sample Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Sample Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid(" and dbo.SAMPLEPRICELIST.SPL_yearid=" & YearId & " order by dbo.SAMPLEPRICELIST.SPL_NO, SAMPLEPRICELIST_DESC.SMP_GRIDSRNO ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class