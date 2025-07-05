

Imports BL

Public Class BlanketRepresentationDetails

    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public TEMPBLANKETREPNO As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub BlanketRepresentationDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub BlanketRepresentationDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")
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
            'Dim dt As DataTable = objclsCMST.search("ISNULL(BLANKETREPRESENT.BLANKET_NO, 0) AS TEMPBLANKETREPNO, BLANKETREPRESENT.BLANKET_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(BLANKETREPRESENT_DESC.BLANKET_SRNO, 0) AS BLANKETSRNO, ISNULL(BLANKETMASTER.BLANKET_NAME, '') AS BLANKETNAME, ISNULL(BLANKETREPRESENT.BLANKET_REMARKS, '') AS REMARKS, ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN ", "", " BLANKETREPRESENT INNER JOIN BLANKETREPRESENT_DESC ON BLANKETREPRESENT.BLANKET_NO = BLANKETREPRESENT_DESC.BLANKET_NO AND BLANKETREPRESENT.BLANKET_YEARID = BLANKETREPRESENT_DESC.BLANKET_YEARID LEFT OUTER JOIN SALESMANMASTER ON BLANKETREPRESENT.BLANKET_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON BLANKETREPRESENT.BLANKET_YEARID = AGENTLEDGERS.Acc_yearid AND BLANKETREPRESENT.BLANKET_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON BLANKETREPRESENT.BLANKET_YEARID = LEDGERS.Acc_yearid AND BLANKETREPRESENT.BLANKET_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN BLANKETMASTER ON BLANKETREPRESENT_DESC.BLANKET_YEARID = BLANKETMASTER.BLANKET_YEARID AND BLANKETREPRESENT_DESC.BLANKET_BLANKETID = BLANKETMASTER.BLANKET_ID ", "  and (BLANKETREPRESENT.BLANKET_YEARID= '" & YearId & "') ORDER BY BLANKETREPRESENT.BLANKET_NO")
            Dim dt As DataTable = objclsCMST.search("ISNULL(BLANKETREPRESENT.BLANKET_NO, 0) AS TEMPBLANKETREPNO, BLANKETREPRESENT.BLANKET_DATE AS DATE, ISNULL(BLANKETREPRESENT_DESC.BLANKET_SRNO, 0) AS SRNO, ISNULL(BLANKETMASTER.BLANKET_NAME, '') AS BLANKETNAME, ISNULL(BLANKETREPRESENT_DESIGNDESC.BLANKET_SRNO, 0) AS DESIGNSRNO, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(BLANKETREPRESENT_DESIGNDESC.BLANKET_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(BLANKETREPRESENT_DESIGNDESC.BLANKET_APPROVAL, '') AS APPROVAL, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN ", "", " BLANKETREPRESENT INNER JOIN BLANKETREPRESENT_DESC ON BLANKETREPRESENT.BLANKET_NO = BLANKETREPRESENT_DESC.BLANKET_NO AND BLANKETREPRESENT.BLANKET_YEARID = BLANKETREPRESENT_DESC.BLANKET_YEARID INNER JOIN BLANKETREPRESENT_DESIGNDESC ON BLANKETREPRESENT_DESC.BLANKET_NO = BLANKETREPRESENT_DESIGNDESC.BLANKET_NO AND BLANKETREPRESENT_DESC.BLANKET_YEARID = BLANKETREPRESENT_DESIGNDESC.BLANKET_YEARID AND BLANKETREPRESENT_DESC.BLANKET_SRNO = BLANKETREPRESENT_DESIGNDESC.BLANKET_MAINSRNO INNER JOIN BLANKETMASTER ON BLANKETREPRESENT_DESC.BLANKET_BLANKETID = BLANKETMASTER.BLANKET_ID INNER JOIN DESIGNMASTER ON BLANKETREPRESENT_DESIGNDESC.BLANKET_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN SALESMANMASTER ON BLANKETREPRESENT.BLANKET_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON BLANKETREPRESENT.BLANKET_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON BLANKETREPRESENT.BLANKET_LEDGERID = LEDGERS.Acc_id  ", "  and (BLANKETREPRESENT.BLANKET_YEARID= '" & YearId & "') ORDER BY TEMPBLANKETREPNO, SRNO, DESIGNSRNO")
            GRIDBILLDETAILS.DataSource = dt
            If dt.Rows.Count > 0 Then
                GRIDBILL.FocusedRowHandle = GRIDBILL.RowCount - 1
                GRIDBILL.TopRowIndex = GRIDBILL.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub showform(ByVal editval As Boolean, ByVal BLANKETREPNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And GRIDBILL.RowCount > 0) Then
                Dim objPO As New BlanketRepresentation
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPBLANKETREPNO = BLANKETREPNO
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

    Private Sub CMDEDIT_Click(sender As Object, e As EventArgs) Handles CMDEDIT.Click
        Try
            showform(True, GRIDBILL.GetFocusedRowCellValue("TEMPBLANKETREPNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDADDNEW_Click(sender As Object, e As EventArgs) Handles CMDADDNEW.Click
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

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(sender As Object, e As EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Blanket Repersentation Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Blanket Repersentation Details"
            GRIDBILL.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Blanket Details", GRIDBILL.VisibleColumns.Count + GRIDBILL.GroupCount)
        Catch ex As Exception
            MsgBox("Blanket Repersentation Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GRIDBILL.DoubleClick
        Try
            showform(True, GRIDBILL.GetFocusedRowCellValue("TEMPBLANKETREPNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TXTFROM_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TXTTO_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class