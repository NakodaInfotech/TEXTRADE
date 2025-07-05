Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base

Public Class SalesEnquiryClose
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
    Private Sub SalesEnquiryClose_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Private Sub SalesEnquiryClose_Load(sender As Object, e As EventArgs) Handles Me.Load
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

            ''GET REASON
            'Dim OBJCMN As New ClsCommon
            'Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(REASON_NAME,'') AS REASON", "", "REASONMASTER", " AND REASON_YEARID = " & YearId)
            'For Each ROW As DataRow In DT.Rows
            '    CMBREASON.Items.Add(ROW("REASON"))
            '    CMBMAINREASON.Items.Add(ROW("REASON"))
            'Next


            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLGRID()
        'Try
        '    Dim objclsCMST As New ClsCommonMaster
        '    Dim dt As New DataTable
        '    If RBPENDING.Checked = True Then
        '        dt = objclsCMST.search("ISNULL(SALESENQUIRY.ENQ_no, 0) AS ENQNO, ISNULL(SALESENQUIRY_DESC.ENQ_GRIDSRNO, 0) AS GRIDSRNO, SALESENQUIRY.ENQ_date AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN, ISNULL(ITEMMASTER.ITEM_NAME, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(SALESENQUIRY_DESC.ENQ_CLOSED, 0) AS CLOSED   ", "", " SALESENQUIRY INNER JOIN SALESENQUIRY_DESC ON SALESENQUIRY.ENQ_no = SALESENQUIRY_DESC.ENQ_NO AND SALESENQUIRY.ENQ_YEARID = SALESENQUIRY_DESC.ENQ_YEARID LEFT OUTER JOIN DESIGNMASTER ON SALESENQUIRY_DESC.ENQ_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON SALESENQUIRY_DESC.ENQ_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN SALESMANMASTER ON SALESENQUIRY.ENQ_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON SALESENQUIRY.ENQ_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON SALESENQUIRY.ENQ_ledgerid = LEDGERS.Acc_id   ", "  AND SALESENQUIRY_DESC.ENQ_CLOSED = 0")
        '    Else
        '        dt = objclsCMST.search("CAST(0 AS BIT) AS CHK,ISNULL(SALESENQUIRY.ENQ_no, 0) AS ENQNO, ISNULL(SALESENQUIRY_DESC.ENQ_GRIDSRNO, 0) AS GRIDSRNO, SALESENQUIRY.ENQ_date AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN, ISNULL(ITEMMASTER.ITEM_NAME, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(SALESENQUIRY_DESC.ENQ_CLOSED, 0) AS CLOSED   ", "", " SALESENQUIRY INNER JOIN SALESENQUIRY_DESC ON SALESENQUIRY.ENQ_no = SALESENQUIRY_DESC.ENQ_NO AND SALESENQUIRY.ENQ_YEARID = SALESENQUIRY_DESC.ENQ_YEARID LEFT OUTER JOIN DESIGNMASTER ON SALESENQUIRY_DESC.ENQ_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON SALESENQUIRY_DESC.ENQ_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN SALESMANMASTER ON SALESENQUIRY.ENQ_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON SALESENQUIRY.ENQ_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON SALESENQUIRY.ENQ_ledgerid = LEDGERS.Acc_id   ", "  AND SALESENQUIRY_DESC.ENQ_CLOSED = 1")

        '    End If
        '    gridbilldetails.DataSource = dt
        '    If dt.Rows.Count > 0 Then
        '        gridbill.FocusedRowHandle = gridbill.RowCount - 1
        '        gridbill.TopRowIndex = gridbill.RowCount - 15
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try



        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As New DataTable
            If RBPENDING.Checked = True Then
                dt = objclsCMST.search("ISNULL(SALESENQUIRY.ENQ_no, 0) AS ENQNO, ISNULL(SALESENQUIRY_DESC.ENQ_GRIDSRNO, 0) AS GRIDSRNO, SALESENQUIRY.ENQ_date AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN, ISNULL(ITEMMASTER.ITEM_NAME, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(SALESENQUIRY_DESC.ENQ_CLOSED, 0) AS CLOSED   ", "", " SALESENQUIRY INNER JOIN SALESENQUIRY_DESC ON SALESENQUIRY.ENQ_no = SALESENQUIRY_DESC.ENQ_NO AND SALESENQUIRY.ENQ_YEARID = SALESENQUIRY_DESC.ENQ_YEARID LEFT OUTER JOIN DESIGNMASTER ON SALESENQUIRY_DESC.ENQ_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON SALESENQUIRY_DESC.ENQ_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN SALESMANMASTER ON SALESENQUIRY.ENQ_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON SALESENQUIRY.ENQ_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON SALESENQUIRY.ENQ_ledgerid = LEDGERS.Acc_id   ", "  AND SALESENQUIRY_DESC.ENQ_CLOSED = 0")
            Else
                dt = objclsCMST.search("ISNULL(SALESENQUIRY.ENQ_no, 0) AS ENQNO, ISNULL(SALESENQUIRY_DESC.ENQ_GRIDSRNO, 0) AS GRIDSRNO, SALESENQUIRY.ENQ_date AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN, ISNULL(ITEMMASTER.ITEM_NAME, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(SALESENQUIRY_DESC.ENQ_CLOSED, 0) AS CLOSED   ", "", " SALESENQUIRY INNER JOIN SALESENQUIRY_DESC ON SALESENQUIRY.ENQ_no = SALESENQUIRY_DESC.ENQ_NO AND SALESENQUIRY.ENQ_YEARID = SALESENQUIRY_DESC.ENQ_YEARID LEFT OUTER JOIN DESIGNMASTER ON SALESENQUIRY_DESC.ENQ_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON SALESENQUIRY_DESC.ENQ_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN SALESMANMASTER ON SALESENQUIRY.ENQ_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON SALESENQUIRY.ENQ_Agentid = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON SALESENQUIRY.ENQ_ledgerid = LEDGERS.Acc_id   ", "  AND SALESENQUIRY_DESC.ENQ_CLOSED = 1")

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

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        'Try
        '    If RBPENDING.Checked = True Then
        '        For I As Integer = 0 To gridbill.RowCount - 1
        '            Dim DTROW As DataRow = gridbill.GetDataRow(I)
        '            If Convert.ToBoolean(DTROW("CLOSED")) = True Then
        '                'If IsDBNull(DTROW("CLOSEDDATE")) = False AndAlso DTROW("CLOSEDDATE") = "" Then
        '                '    MsgBox("Enter Closed Date", MsgBoxStyle.Critical)
        '                '    Exit Sub
        '                'End If
        '            End If
        '        Next
        '    Else
        '        If MsgBox("You have trying to Re-Open Closed Orders, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        '    End If



        '    Dim OBJCMN As New ClsCommon
        '    For I As Integer = 0 To gridbill.RowCount - 1
        '        Dim DTROW As DataRow = gridbill.GetDataRow(I)
        '        If RBPENDING.Checked = True Then
        '            If Convert.ToBoolean(DTROW("CLOSED")) = True Then 'AndAlso IsDBNull(DTROW("CLOSEDDATE")) = False AndAlso DTROW("CLOSEDDATE") <> "" Then
        '                'Dim TEMP As DateTime
        '                'DateTime.TryParse(DTROW("CLOSEDDATE"), TEMP)
        '                Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE SALESENQUIRY_DESC SET ENQ_CLOSED = 1 WHERE ENQ_NO = " & Val(DTROW("ENQNO")) & " AND ENQ_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND ENQ_YEARID = " & YearId, "", "") 'Else Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE OPENINGSALEENQUIRY_DESC SET OPENQ_CLOSED = 1 WHERE OPENQ_NO = " & Val(DTROW("ENQNO")) & " AND OPENQ_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND OPENQ_YEARID = " & YearId, "", "")
        '            End If
        '        Else
        '            If Convert.ToBoolean(DTROW("CLOSED")) = True Then
        '                Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE SALESENQUIRY_DESC SET ENQ_CLOSED = 0 WHERE ENQ_NO = " & Val(DTROW("ENQNO")) & " AND ENQ_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND ENQ_YEARID = " & YearId, "", "") 'Else Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE OPENINGSALEENQUIRY_DESC SET OPENQ_CLOSED = 0 WHERE OPENQ_NO = " & Val(DTROW("ENQNO")) & " AND OPENQ_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND OPENQ_YEARID = " & YearId, "", "")
        '            End If
        '        End If
        '    Next
        '    FILLGRID()
        '    gridbill.Focus()
        'Catch ex As Exception
        '    Throw ex
        'End Try
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            'PENDING
            If RBPENDING.Checked = True Then
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                    DT = OBJCMN.Execute_Any_String("UPDATE SALESENQUIRY_DESC SET ENQ_CLOSED = 1 WHERE ENQ_NO = " & Val(DTROW("ENQNO")) & " AND ENQ_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND ENQ_YEARID = " & YearId, "", "")
                Next
                MsgBox("Details Updated Successfully")
                FILLGRID()
                gridbill.Focus()
            End If

            'ENTERED
            If RBENTERED.Checked = True Then
                If MsgBox("You have trying to Re-Open Close Job Docket Batch, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                    DT = OBJCMN.Execute_Any_String("UPDATE SALESENQUIRY_DESC SET ENQ_CLOSED = 0 WHERE ENQ_NO = " & Val(DTROW("ENQNO")) & " AND ENQ_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND ENQ_YEARID = " & YearId, "", "")
                Next
                MsgBox("Details Updated Successfully")
                FILLGRID()
                gridbill.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    'Private Sub CMDAPPLY_Click(sender As Object, e As EventArgs)
    '    Try
    '        For I As Integer = 0 To gridbill.RowCount - 1
    '            Dim DTROW As DataRow = gridbill.GetDataRow(I)
    '            If Convert.ToBoolean(DTROW("CLOSED")) = True Then
    '                If IsDBNull(DTROW("CLOSEDDATE")) = False AndAlso DTROW("CLOSEDDATE") = "" Then
    '                    DTROW("CLOSEDDATE") = DTCLOSEDATE.Value.Date.ToString
    '                    DTROW("REASON") = CMBMAINREASON.Text.Trim
    '                End If
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Private Sub gridbill_ValidateRow(sender As Object, e As ValidateRowEventArgs) Handles gridbill.ValidateRow
    '    Try
    '        If IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "CLOSEDDATE")) = False Then

    '            If gridbill.GetRowCellValue(e.RowHandle, "CLOSEDDATE").ToString <> "" Then
    '                'PARSING DATE FORMATS WHETHER THEY ARE CLOSEDDATE OR NOT
    '                Dim TEMP As DateTime
    '                If Not DateTime.TryParse(gridbill.GetRowCellValue(e.RowHandle, "CLOSEDDATE"), TEMP) Then
    '                    e.Valid = False
    '                    gridbill.SetColumnError(GCLOSEDDATE, "Invalid Date")
    '                    Exit Sub
    '                Else
    '                    If gridbill.GetRowCellValue(e.RowHandle, "CLOSEDDATE") < Convert.ToDateTime(gridbill.GetRowCellValue(e.RowHandle, "DATE")).Date Then
    '                        e.Valid = False
    '                        gridbill.SetColumnError(GCLOSEDDATE, "Date must be After Order Date")
    '                        Exit Sub
    '                    End If
    '                End If
    '            End If

    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Sale Enquiry Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Sale Enquiry Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sale Enquiry Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Sale Enquiry Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

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

    Private Sub gridbill_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbill.KeyDown
        Try
            If e.KeyCode = Keys.Space Then
                Dim dtrow As DataRow = gridbill.GetFocusedDataRow()
                dtrow("CLOSED") = Not dtrow("CLOSED")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class