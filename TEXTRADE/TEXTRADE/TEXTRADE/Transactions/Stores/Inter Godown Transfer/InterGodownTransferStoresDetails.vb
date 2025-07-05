Imports BL

Public Class InterGodownTransferStoresDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub PROFORMADetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Sub fillgrid()

        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("  CAST(0 AS BIT) AS CHK,  ISNULL(INTERGODOWNTRANSFERSTORES.TRANSFER_NO, 0) AS GODOWNNO, INTERGODOWNTRANSFERSTORES.TRANSFER_DATE AS DATE, ISNULL(INTERGODOWNTRANSFERSTORES.TRANSFER_MODEOFSHIPMENT, '') AS MODEOFSHIP, INTERGODOWNTRANSFERSTORES.TRANSFER_ISSUEDBY AS ISSUEBY, INTERGODOWNTRANSFERSTORES.TRANSFER_REMARKS AS REMARKS, ISNULL(INTERGODOWNTRANSFERSTORES.TRANSFER_TOTALQTY, 0) AS TOTALQTY, INTERGODOWNTRANSFERSTORES.TRANSFER_CMPID AS CMPID, INTERGODOWNTRANSFERSTORES.TRANSFER_USERID AS USERID, INTERGODOWNTRANSFERSTORES.TRANSFER_YEARID AS YEARID,  ISNULL(INTERGODOWNTRANSFERSTORES_DESC.TRANSFER_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(INTERGODOWNTRANSFERSTORES_DESC.TRANSFER_DESC, '') AS DESCRIPTION, ISNULL(INTERGODOWNTRANSFERSTORES_DESC.TRANSFER_QTY, 0) AS QTY, ISNULL(FROMGODOWNMASTER.GODOWN_name, '') AS FROMGODOWN, ISNULL(GODOWNMASTER.GODOWN_name, '') AS TOGODOWN, ISNULL(UNITMASTER.unit_name, '') AS UNIT, INTERGODOWNTRANSFERSTORES_DESC.TRANSFER_ITEMID, ISNULL(STOREITEMMASTER.STOREITEM_NAME, '') AS ITEMNAME   ", "", "   INTERGODOWNTRANSFERSTORES_DESC LEFT OUTER JOIN STOREITEMMASTER ON INTERGODOWNTRANSFERSTORES_DESC.TRANSFER_ITEMID = STOREITEMMASTER.STOREITEM_ID LEFT OUTER JOIN UNITMASTER ON INTERGODOWNTRANSFERSTORES_DESC.TRANSFER_UNITID = UNITMASTER.unit_id RIGHT OUTER JOIN INTERGODOWNTRANSFERSTORES LEFT OUTER JOIN GODOWNMASTER ON INTERGODOWNTRANSFERSTORES.TRANSFER_TOGODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN GODOWNMASTER AS FROMGODOWNMASTER ON INTERGODOWNTRANSFERSTORES.TRANSFER_FROMGODOWNID = FROMGODOWNMASTER.GODOWN_id ON  INTERGODOWNTRANSFERSTORES_DESC.TRANSFER_NO = INTERGODOWNTRANSFERSTORES.TRANSFER_NO", " and  INTERGODOWNTRANSFERSTORES.TRANSFER_YEARID=" & YearId)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub showform(ByVal editval As Boolean, ByVal TEMPGODOWNNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objgdn As New InterGodownTransferStores
                objgdn.MdiParent = MDIMain
                objgdn.EDIT = editval
                objgdn.TEMPGODOWNNO = TEMPGODOWNNO
                objgdn.Show()
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

    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("GODOWNNO"))
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

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click

        Try
            showform(True, gridbill.GetFocusedRowCellValue("GODOWNNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Store Transfer Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Store Transfer Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Store Transfer Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Store Transfer Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub InterGodownTransferStoresDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
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
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class