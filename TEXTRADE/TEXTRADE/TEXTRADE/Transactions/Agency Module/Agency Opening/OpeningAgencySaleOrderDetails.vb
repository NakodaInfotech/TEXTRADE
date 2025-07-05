Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class OpeningAgencySaleOrderDetails

    Public EDIT As Boolean
    Dim temppreqno As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub OpeningAgencySaleOrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub OpeningAgencySaleOrderDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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

            fillgrid(" and dbo.OPENINGAGENCYSALEORDER.OPASO_yearid=" & YearId & " order by dbo.OPENINGAGENCYSALEORDER.OPASO_no, OPENINGAGENCYSALEORDER_DESC.OPASO_GRIDSRNO  ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" OPENINGAGENCYSALEORDER.OPASO_no AS OPSONO, OPENINGAGENCYSALEORDER.OPASO_date AS OPSODATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, OPENINGAGENCYSALEORDER.OPASO_pono AS PARTYPONO, OPENINGAGENCYSALEORDER.OPASO_DUEDATE AS DELDATE, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(OPENINGAGENCYSALEORDER_DESC.OPASO_PARTYPONO, '') AS GRIDPARTYPONO, ISNULL(OPENINGAGENCYSALEORDER_DESC.OPASO_GRIDREMARKS, '') AS GRIDREMARKS, OPENINGAGENCYSALEORDER_DESC.OPASO_QTY AS PCS, OPENINGAGENCYSALEORDER_DESC.OPASO_CUT AS CUT, OPENINGAGENCYSALEORDER_DESC.OPASO_RATE AS RATE, OPENINGAGENCYSALEORDER_DESC.OPASO_QTY - OPENINGAGENCYSALEORDER_DESC.OPASO_RECDQTY AS BALPCS, OPENINGAGENCYSALEORDER.OPASO_remarks AS REMARKS, ISNULL(OPENINGAGENCYSALEORDER_DESC.OPASO_CLOSED, 0) AS CLOSED, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS PACKING ", "", " OPENINGAGENCYSALEORDER_DESC INNER JOIN OPENINGAGENCYSALEORDER ON OPENINGAGENCYSALEORDER_DESC.OPASO_NO = OPENINGAGENCYSALEORDER.OPASO_no AND OPENINGAGENCYSALEORDER_DESC.OPASO_YEARID = OPENINGAGENCYSALEORDER.OPASO_YEARID INNER JOIN LEDGERS ON OPENINGAGENCYSALEORDER.OPASO_ledgerid = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON OPENINGAGENCYSALEORDER_DESC.OPASO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON OPENINGAGENCYSALEORDER.OPASO_PACKINGID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN COLORMASTER ON OPENINGAGENCYSALEORDER_DESC.OPASO_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON OPENINGAGENCYSALEORDER_DESC.OPASO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON OPENINGAGENCYSALEORDER.OPASO_Agentid = AGENTLEDGERS.Acc_id  ", TEMPCONDITION)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal SOno As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New OpeningAgencySaleOrder
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPOPSONO = SOno
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
            showform(True, gridbill.GetFocusedRowCellValue("OPSONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("OPSONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("CLOSED")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.LightSkyBlue
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Sale Order Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sale Order Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sale Order Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("SO Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub
End Class


