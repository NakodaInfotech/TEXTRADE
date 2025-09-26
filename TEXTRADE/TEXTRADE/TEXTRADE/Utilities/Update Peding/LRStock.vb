Imports BL
Imports DevExpress.CodeParser

Public Class LRStock
    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LRStock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub LRStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
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
                dt = objclsCMST.search(" CAST(0 AS BIT) AS CHK, *", "", " PURCHASELRSTOCK ", " AND PURCHASELRSTOCK.YEARID=" & YearId & " AND PURCHASELRSTOCK.SOLD =  0 ORDER BY TYPE, BILLNO")
            Else
                dt = objclsCMST.search(" CAST(0 AS BIT) AS CHK, *", "", " PURCHASELRSTOCK ", " AND PURCHASELRSTOCK.YEARID=" & YearId & " AND PURCHASELRSTOCK.SOLD =  1 ORDER BY TYPE, BILLNO")
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

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\LR Stock Entries.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "LR Stock Entries"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "LR Stock Entries", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("LR Stock Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            gridbill.ClearColumnsFilter()

            'PENDING
            If RBPENDING.Checked = True Then
                For I As Integer = 0 To gridbill.RowCount - 1
                    Dim DTROW As DataRow = gridbill.GetDataRow(I)
                    If DTROW("CHK") = True Then
                        If DTROW("TYPE") = "PURCHASE" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER SET BILL_SOLD = 1 WHERE BILL_NO = " & DTROW("BILLNO") & " AND  BILL_REGISTERID = " & DTROW("REGID") & " AND BILL_YEARID = " & YearId, "", "")
                        If DTROW("TYPE") = "OPENING" Then DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET SM_SOLD = 1 WHERE SM_LEDGERID = " & DTROW("LEDGERID") & "  AND SM_LEDGERIDTO = " & DTROW("TRANSID") & "  AND SM_BALENO = '" & DTROW("LRNO") & "' AND SM_YEARID = " & YearId, "", "")
                    End If
                Next
            End If

            'ENTERED
            If RBENTERED.Checked = True Then
                If MsgBox("You have trying to Re-Open LR Stock, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                For I As Integer = 0 To gridbill.RowCount - 1
                    Dim DTROW As DataRow = gridbill.GetDataRow(I)
                    If DTROW("CHK") = True Then
                        If DTROW("TYPE") = "PURCHASE" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER SET BILL_SOLD = 0 WHERE BILL_NO = " & DTROW("BILLNO") & "  AND BILL_REGISTERID = " & DTROW("REGID") & " AND BILL_YEARID = " & YearId, "", "")
                        If DTROW("TYPE") = "OPENING" Then DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET SM_SOLD = 0 WHERE SM_LEDGERID = " & DTROW("LEDGERID") & "  AND SM_LEDGERIDTO = " & DTROW("TRANSID") & "  AND SM_BALENO = '" & DTROW("LRNO") & "' AND SM_YEARID = " & YearId, "", "")
                    End If
                Next
            End If

            MsgBox("Details Updated Successfully")
            FILLGRID()
            gridbill.Focus()

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

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
