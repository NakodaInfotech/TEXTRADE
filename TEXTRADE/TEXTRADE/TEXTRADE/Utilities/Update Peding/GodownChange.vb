Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports System.ComponentModel

Public Class GodownChange
    Public EDIT As Boolean          'used for editing
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub FinishJobDocketBatch_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub FinishJobDocketBatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As New DataTable
            If RBPENDING.Checked = True Then
                'dt = objclsCMST.search(" ISNULL(JOBBATCHMASTER.job_orderno, 0) AS ORDERNO, ISNULL(JOBBATCHMASTER.job_no, 0) AS JOBDOCKETNO, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(SUBITEMMASTER.item_name, '') AS SUBITEM, ISNULL(JOBBATCHMASTER.JOB_JOBQTY, 0) AS QTY, ISNULL(JOBBATCHMASTER.JOB_PRODFINISHQTY, 0) AS PRODUCTIONQTY, ISNULL(JOBBATCHMASTER.JOB_CLOSE, 0) AS JOBCLOSE ", "", " ITEMMASTER AS SUBITEMMASTER RIGHT OUTER JOIN JOBBATCHMASTER ON SUBITEMMASTER.item_id = JOBBATCHMASTER.JOB_SUBITEMID LEFT OUTER JOIN ITEMMASTER ON JOBBATCHMASTER.JOB_MAINITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS ON JOBBATCHMASTER.JOB_LEDGERID = LEDGERS.Acc_id ", " AND JOBBATCHMASTER.JOB_CLOSE = 0 AND JOBBATCHMASTER.JOB_FINISHED = 0 and JOBBATCHMASTER.JOB_YEARID=" & YearId & "  ORDER BY JOBBATCHMASTER.job_orderno")
                dt = objclsCMST.search("ISNULL(GRN.grn_no, 0) AS SRNO, GRN.grn_date AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(GRN.grn_challanno, '') AS CHALLAN, ISNULL(GRN.GRN_PLOTNO, '') AS LOTNO, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(GRN.GRN_TOTALQTY, 0) AS TOTALQTY, ISNULL(GRN.GRN_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(GRN.GRN_TYPE, '') AS TYPE ", "", " GRN LEFT OUTER JOIN GODOWNMASTER ON GRN.grn_yearid = GODOWNMASTER.GODOWN_yearid AND GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id  ", "  AND GRN.GRN_GODOWNCHECKED = 0 AND GRN.grn_yearid = " & YearId & "  ORDER BY SRNO ")
            Else
                'dt = objclsCMST.search(" ISNULL(JOBBATCHMASTER.job_orderno, 0) As ORDERNO, ISNULL(JOBBATCHMASTER.job_no, 0) As JOBDOCKETNO, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(SUBITEMMASTER.item_name, '') AS SUBITEM, ISNULL(JOBBATCHMASTER.JOB_JOBQTY, 0) AS QTY, ISNULL(JOBBATCHMASTER.JOB_PRODFINISHQTY, 0) AS PRODUCTIONQTY , ISNULL(JOBBATCHMASTER.JOB_CLOSE, 0) AS JOBCLOSE ", "", " ITEMMASTER AS SUBITEMMASTER RIGHT OUTER JOIN JOBBATCHMASTER ON SUBITEMMASTER.item_id = JOBBATCHMASTER.JOB_SUBITEMID LEFT OUTER JOIN ITEMMASTER ON JOBBATCHMASTER.JOB_MAINITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS ON JOBBATCHMASTER.JOB_LEDGERID = LEDGERS.Acc_id ", " AND JOBBATCHMASTER.JOB_CLOSE = 1 and JOBBATCHMASTER.JOB_YEARID=" & YearId & "  ORDER BY JOBBATCHMASTER.job_orderno")
                dt = objclsCMST.search("ISNULL(GRN.grn_no, 0) AS SRNO, GRN.grn_date AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME, ISNULL(GRN.grn_challanno, '') AS CHALLAN, ISNULL(GRN.GRN_PLOTNO, '') AS LOTNO, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(GRN.GRN_TOTALQTY, 0) AS TOTALQTY, ISNULL(GRN.GRN_TOTALMTRS, 0) AS TOTALMTRS, ISNULL(GRN.GRN_TYPE, '') AS TYPE ", "", " GRN LEFT OUTER JOIN GODOWNMASTER ON GRN.grn_yearid = GODOWNMASTER.GODOWN_yearid AND GRN.GRN_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN LEDGERS ON GRN.grn_ledgerid = LEDGERS.Acc_id  ", "  AND GRN.GRN_GODOWNCHECKED = 1 AND GRN.grn_yearid = " & YearId & "  ORDER BY SRNO ")

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

    Private Sub PrinToExcel_Click(sender As Object, e As EventArgs) Handles PrinToExcel.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Finish Job Docket Batch Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Close Godown Change Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Close Godown Change Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Close Job Docket Batch Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click

        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            'PENDING
            If RBPENDING.Checked = True Then
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                    DT = OBJCMN.Execute_Any_String(" UPDATE GRN SET GRN_GODOWNCHECKED = 1, GRN_GODOWNID = (SELECT GODOWN_id FROM GODOWNMASTER WHERE GODOWN_name = '" & CMBGODOWN.Text.Trim & "' AND GODOWN_yearid = " & YearId & ")FROM GRN WHERE GRN.grn_no = " & Val(DTROW("SRNO")) & " AND grn_yearid = " & YearId & " AND GRN_TYPE = '" & DTROW("TYPE") & "'", "", "")
                Next
                MsgBox("Details Updated Successfully")
                FILLGRID()
                gridbill.Focus()
            End If

            'ENTERED
            If RBENTERED.Checked = True Then
                If MsgBox("You have trying to Re-Open Closed GRN, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                    DT = OBJCMN.Execute_Any_String("UPDATE GRN set GRN_GODOWNCHECKED = 0 WHERE grn_no = " & Val(DTROW("SRNO")) & " AND grn_yearid = " & YearId & " AND GRN_TYPE = '" & DTROW("TYPE") & "'", "", "")
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

    Private Sub RBPENDING_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBPENDING.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBENTERED_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBENTERED.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillcmb()
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(sender As Object, e As EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    'Private Sub CMBGODOWN_Validating(sender As Object, e As CancelEventArgs) Handles CMBGODOWN.Validating
    '    Try
    '        If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
End Class