
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class StoreStockRecoInDetails
    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public TEMPRECONO As Integer

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub StoreStockRecoInDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'STORESTOCKRECO'")
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

    Private Sub StoreStockRecoInDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Sub FILLGRID()
        Try
            'Dim OBJSTOCKRECO As New ClsStoreStockAdjustment
            Dim dt As New DataTable
            'dt = OBJSTOCKRECO.SELECTSTORESTOCKADJUSTMENT(TEMPRECONO, CmpId, Locationid, YearId)
            Dim OBJCMN As New ClsCommon
            dt = OBJCMN.SEARCH(" STORESTOCKADJUSTMENT.SA_NO AS SANO, STORESTOCKADJUSTMENT_INDESC.SA_INGRIDSRNO AS INGRIDSRNO, STORESTOCKADJUSTMENT_INDESC.SA_INDESC AS INDESC,  STORESTOCKADJUSTMENT_INDESC.SA_INQTY AS INQTY, UNITMASTER.unit_name AS INUNIT, STORESTOCKADJUSTMENT_INDESC.SA_INRATE AS INRATE, STOREITEMMASTER.STOREITEM_NAME AS INITEMNAME ", "", " STORESTOCKADJUSTMENT INNER JOIN STORESTOCKADJUSTMENT_INDESC ON STORESTOCKADJUSTMENT.SA_NO = STORESTOCKADJUSTMENT_INDESC.SA_NO AND  STORESTOCKADJUSTMENT.SA_yearid = STORESTOCKADJUSTMENT_INDESC.SA_YEARID INNER JOIN  UNITMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_INUNITID = UNITMASTER.unit_id INNER JOIN  STOREITEMMASTER ON STORESTOCKADJUSTMENT_INDESC.SA_INITEMID = STOREITEMMASTER.STOREITEM_ID ", "  AND (STORESTOCKADJUSTMENT_INDESC.SA_yearid= " & YearId & ") ORDER BY dbo.STORESTOCKADJUSTMENT_INDESC.SA_NO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub showform(ByVal editval As Boolean, ByVal SANO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New StoreStockReco
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPRECONO = SANO
                objPO.Show()
            End If
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
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(sender As Object, e As EventArgs) Handles gridbilldetails.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SANO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SANO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Store Stock In Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Store Stock In Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Store Stock In Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Store Stock In Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub
End Class