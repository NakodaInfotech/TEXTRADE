


Imports BL
    Imports System.Windows.Forms

Public Class CoverNoteDetails

    Public EDIT As Boolean
    Dim TEMPCOVERNO As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CoverNoteDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub CoverNoteDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            fillgrid(" and dbo.COVERNOTE.COVER_YEARID=" & YearId & " order by dbo.COVERNOTE.COVER_NO ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclsCMST.search("COVERNOTE.COVER_NO AS TEMPCOVERNO, COVERNOTE.COVER_COVERDATE AS COVERDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(AGENTMASTER.Acc_cmpname, '') AS AGENT,  ISNULL(COVERNOTE.COVER_REMARKS, '') AS REMARKS, COVERNOTE_DESC.COVER_SRNO AS SRNO, ISNULL(COVERNOTE_DESC.COVER_INVNO, 0) AS INVNO, ISNULL(REGISTERMASTER.register_name, '') AS REGNAME, ISNULL(COVERNOTE_DESC.COVER_INVINITIALS, '') AS INVINITIALS, ISNULL(COVERNOTE_DESC.COVER_PRINTINITIALS, '') AS PRINTINITIALS, ISNULL(COVERNOTE_DESC.COVER_INVDATE, '') AS INVDATE, ISNULL(COVERNOTE_DESC.COVER_LRNO, '') AS LRNO, ISNULL(COVERNOTE_DESC.COVER_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(COVERNOTE_DESC.COVER_TOTALPCS, 0) AS TOTALPCS, ISNULL(COVERNOTE_DESC.COVER_GRANDTOTAL, 0) AS GRANDTOTAL, ISNULL(COVERNOTE_DESC.COVER_LRDATE, '') AS LRDATE, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(GRIDLEDGERS.Acc_cmpname, '') AS GRIDNAME, ISNULL(GRIDAGENTLEDGERS.Acc_cmpname, '') AS GRIDAGENTNAME ", "", " COVERNOTE INNER JOIN COVERNOTE_DESC ON COVERNOTE.COVER_NO = COVERNOTE_DESC.COVER_NO AND COVERNOTE.COVER_YEARID = COVERNOTE_DESC.COVER_YEARID LEFT OUTER JOIN LEDGERS AS GRIDAGENTLEDGERS ON COVERNOTE_DESC.COVER_AGENTNAMEID = GRIDAGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS GRIDLEDGERS ON COVERNOTE_DESC.COVER_PARTYNAMEID = GRIDLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON COVERNOTE_DESC.COVER_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN REGISTERMASTER ON COVERNOTE_DESC.COVER_REGID = REGISTERMASTER.register_id LEFT OUTER JOIN LEDGERS AS AGENTMASTER ON COVERNOTE.COVER_AGENTID = AGENTMASTER.Acc_id LEFT OUTER JOIN LEDGERS ON COVERNOTE.COVER_LEDGERID = LEDGERS.Acc_id  ", TEMPCONDITION)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal TEMPCOVERNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New CoverNote
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPCOVERNO = TEMPCOVERNO
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

    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("TEMPCOVERNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid(" and dbo.COVERNOTE.COVER_YEARID=" & YearId & " order by dbo.COVERNOTE.COVER_NO ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("TEMPCOVERNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        'Try
        '    Dim DT As DataTable = gridbilldetails.DataSource
        '    If e.RowHandle >= 0 Then
        '        Dim ROW As DataRow = DT.Rows(e.RowHandle)
        '        If ROW("CLOSED") = "TRUE" Then
        '            e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Regular)
        '            e.Appearance.BackColor = Color.LightSkyBlue
        '        End If
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Cover Note Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Cover Note Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Cover Note Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class