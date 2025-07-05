

Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Public Class SampleOrderClose

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            If RBENTERED.Checked = True Then
                If MsgBox("You have trying to Re-Open Closed Orders, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            For I As Integer = 0 To gridbill.RowCount - 1
                Dim DTROW As DataRow = gridbill.GetDataRow(I)
                If RBPENDING.Checked = True Then
                    If Convert.ToBoolean(DTROW("CLOSED")) = True Then
                        If DTROW("TYPE") = "SAMPLEORDER" Then Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE SAMPLEORDER_DESC SET SO_CLOSED = 1 WHERE SO_NO = " & Val(DTROW("SRNO")) & " AND SO_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND SO_YEARID = " & YearId, "", "") Else Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE OPENINGSAMPLEORDER_DESC SET OSO_CLOSED = 1 WHERE OSO_NO = " & Val(DTROW("SRNO")) & " AND OSO_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND OSO_YEARID = " & YearId, "", "")
                    End If
                Else
                    If Convert.ToBoolean(DTROW("CLOSED")) = True Then
                        If DTROW("TYPE") = "SAMPLEORDER" Then Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE SAMPLEORDER_DESC SET SO_CLOSED = 0 WHERE SO_NO = " & Val(DTROW("SRNO")) & " AND SO_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND SO_YEARID = " & YearId, "", "") Else Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE OPENINGSAMPLEORDER_DESC SET OSO_CLOSED = 0 WHERE OSO_NO = " & Val(DTROW("SRNO")) & " AND OSO_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND OSO_YEARID = " & YearId, "", "")
                    End If
                End If

            Next
            MsgBox("Entries Updated")
            fillgrid("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CloseMultipleSampleOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            fillgrid("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Sample Order Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sample Order Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sample Order Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Sample Order Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As New DataTable
            If RBPENDING.Checked = True Then
                dt = objclsCMST.search("*", "", " (SELECT ISNULL(SAMPLEORDER.SO_NO, 0) AS SRNO,SAMPLEORDER.SO_DATE AS DATE, ISNULL(SAMPLEORDER.SO_TOTALNOOFBOOKLET, 0) AS NOOFBOOKLET, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENT_LEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(SAMPLEORDER_DESC.SO_GRIDSRNO, '0') AS GRIDSRNO, ISNULL(SAMPLEORDER_DESC.SO_OUTQTY, '') AS OUTQTY,(SAMPLEORDER.SO_TOTALNOOFBOOKLET - SAMPLEORDER_DESC.SO_OUTQTY) AS BALQTY, ISNULL(SAMPLEORDER_DESC.SO_CLOSED, 0) AS CLOSED, 'SAMPLEORDER' AS TYPE FROM ITEMMASTER LEFT OUTER JOIN COLORMASTER RIGHT OUTER JOIN SAMPLEORDER_DESC ON COLORMASTER.COLOR_id = SAMPLEORDER_DESC.SO_COLORID LEFT OUTER JOIN DESIGNMASTER ON SAMPLEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON SAMPLEORDER_DESC.SO_QUALITYID = QUALITYMASTER.QUALITY_id RIGHT OUTER JOIN SAMPLEORDER LEFT OUTER JOIN LEDGERS AS AGENT_LEDGERS ON SAMPLEORDER.SO_AGENTID = AGENT_LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON SAMPLEORDER.SO_LEDGERID = LEDGERS.Acc_id ON SAMPLEORDER_DESC.SO_NO = SAMPLEORDER.SO_NO ON ITEMMASTER.item_id = SAMPLEORDER_DESC.SO_ITEMID  WHERE SAMPLEORDER_DESC.SO_CLOSED = 'FALSE' and (SAMPLEORDER_DESC.SO_NOOFBOOKLET-SAMPLEORDER_DESC.SO_OUTQTY)>0 AND dbo.SAMPLEORDER.SO_yearid=" & YearId & " UNION ALL  SELECT  ISNULL(OPENINGSAMPLEORDER.OSO_NO, 0) AS SRNO,OPENINGSAMPLEORDER.OSO_DATE AS DATE, ISNULL(OPENINGSAMPLEORDER.OSO_TOTALNOOFBOOKLET, 0) AS NOOFBOOKLET, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '0') AS DESIGN,  ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(OPENINGSAMPLEORDER_DESC.OSO_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(OPENINGSAMPLEORDER_DESC.OSO_OUTQTY, 0) AS OUTQTY,(OPENINGSAMPLEORDER.OSO_TOTALNOOFBOOKLET-OPENINGSAMPLEORDER_DESC.OSO_OUTQTY)AS BALQTY,  ISNULL(OPENINGSAMPLEORDER_DESC.OSO_CLOSED, 0) AS CLOSED, 'OPENING' AS TYPE FROM COLORMASTER RIGHT OUTER JOIN OPENINGSAMPLEORDER_DESC ON COLORMASTER.COLOR_id = OPENINGSAMPLEORDER_DESC.OSO_COLORID LEFT OUTER JOIN DESIGNMASTER ON OPENINGSAMPLEORDER_DESC.OSO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGSAMPLEORDER_DESC.OSO_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGSAMPLEORDER_DESC.OSO_ITEMID = ITEMMASTER.item_id RIGHT OUTER JOIN OPENINGSAMPLEORDER LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON OPENINGSAMPLEORDER.OSO_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON OPENINGSAMPLEORDER.OSO_LEDGERID = LEDGERS.Acc_id ON OPENINGSAMPLEORDER_DESC.OSO_NO = OPENINGSAMPLEORDER.OSO_NO  WHERE OPENINGSAMPLEORDER_DESC.OSO_CLOSED = 'FALSE' and (OPENINGSAMPLEORDER_DESC.OSO_NOOFBOOKLET-OPENINGSAMPLEORDER_DESC.OSO_OUTQTY)>0 AND dbo.OPENINGSAMPLEORDER_DESC.OSO_YEARID=" & YearId & ") AS T", "  ORDER BY  GRIDSRNO , SRNO")
            Else
                dt = objclsCMST.search("*", "", " (SELECT ISNULL(SAMPLEORDER.SO_NO, 0) AS SRNO,SAMPLEORDER.SO_DATE AS DATE, ISNULL(SAMPLEORDER.SO_TOTALNOOFBOOKLET, 0) AS NOOFBOOKLET, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENT_LEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(SAMPLEORDER_DESC.SO_GRIDSRNO, '0') AS GRIDSRNO, ISNULL(SAMPLEORDER_DESC.SO_OUTQTY, '') AS OUTQTY,(SAMPLEORDER.SO_TOTALNOOFBOOKLET - SAMPLEORDER_DESC.SO_OUTQTY) AS BALQTY, ISNULL(SAMPLEORDER_DESC.SO_CLOSED, 0) AS CLOSED, 'SAMPLEORDER' AS TYPE FROM ITEMMASTER LEFT OUTER JOIN COLORMASTER RIGHT OUTER JOIN SAMPLEORDER_DESC ON COLORMASTER.COLOR_id = SAMPLEORDER_DESC.SO_COLORID LEFT OUTER JOIN DESIGNMASTER ON SAMPLEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON SAMPLEORDER_DESC.SO_QUALITYID = QUALITYMASTER.QUALITY_id RIGHT OUTER JOIN SAMPLEORDER LEFT OUTER JOIN LEDGERS AS AGENT_LEDGERS ON SAMPLEORDER.SO_AGENTID = AGENT_LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON SAMPLEORDER.SO_LEDGERID = LEDGERS.Acc_id ON SAMPLEORDER_DESC.SO_NO = SAMPLEORDER.SO_NO ON ITEMMASTER.item_id = SAMPLEORDER_DESC.SO_ITEMID  WHERE SAMPLEORDER_DESC.SO_CLOSED = 'TRUE' and (SAMPLEORDER_DESC.SO_NOOFBOOKLET-SAMPLEORDER_DESC.SO_OUTQTY)>0 AND dbo.SAMPLEORDER.SO_yearid=" & YearId & " UNION ALL  SELECT  ISNULL(OPENINGSAMPLEORDER.OSO_NO, 0) AS SRNO,OPENINGSAMPLEORDER.OSO_DATE AS DATE, ISNULL(OPENINGSAMPLEORDER.OSO_TOTALNOOFBOOKLET, 0) AS NOOFBOOKLET, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENT, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '0') AS DESIGN,  ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(OPENINGSAMPLEORDER_DESC.OSO_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(OPENINGSAMPLEORDER_DESC.OSO_OUTQTY, 0) AS OUTQTY, (OPENINGSAMPLEORDER.OSO_TOTALNOOFBOOKLET-OPENINGSAMPLEORDER_DESC.OSO_OUTQTY)AS BALQTY,  ISNULL(OPENINGSAMPLEORDER_DESC.OSO_CLOSED, 0) AS CLOSED, 'OPENING' AS TYPE FROM COLORMASTER RIGHT OUTER JOIN OPENINGSAMPLEORDER_DESC ON COLORMASTER.COLOR_id = OPENINGSAMPLEORDER_DESC.OSO_COLORID LEFT OUTER JOIN DESIGNMASTER ON OPENINGSAMPLEORDER_DESC.OSO_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON OPENINGSAMPLEORDER_DESC.OSO_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON OPENINGSAMPLEORDER_DESC.OSO_ITEMID = ITEMMASTER.item_id RIGHT OUTER JOIN OPENINGSAMPLEORDER LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON OPENINGSAMPLEORDER.OSO_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON OPENINGSAMPLEORDER.OSO_LEDGERID = LEDGERS.Acc_id ON OPENINGSAMPLEORDER_DESC.OSO_NO = OPENINGSAMPLEORDER.OSO_NO  WHERE OPENINGSAMPLEORDER_DESC.OSO_CLOSED = 'TRUE' and (OPENINGSAMPLEORDER_DESC.OSO_NOOFBOOKLET-OPENINGSAMPLEORDER_DESC.OSO_OUTQTY)>0 AND dbo.OPENINGSAMPLEORDER_DESC.OSO_YEARID=" & YearId & ") AS T", "  ORDER BY  GRIDSRNO , SRNO")
            End If
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CloseMultipleSampleOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Space And e.Control = True Then
                'SELECT ALL DATA
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CLOSED") = Not Convert.ToBoolean(dtrow("CLOSED"))
                Next
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    'Private Sub gridbill_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gridbill.RowStyle
    '    Try
    '        If e.RowHandle >= 0 Then
    '            Dim View As GridView = sender
    '            If View.GetRowCellDisplayText(e.RowHandle, View.Columns("CLOSED")) = "Checked" Then
    '                e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
    '                e.Appearance.BackColor = Color.Yellow
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub CHKSELECTALL_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            If gridbilldetails.Visible = True Then
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CLOSED") = CHKSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class