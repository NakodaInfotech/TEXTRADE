
Imports BL

Public Class LockPendingLotsForProgram

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LockPendingLotsForProgram_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub LockPendingLotsForProgram_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCMN As New ClsCommonMaster
            Dim DT As New DataTable

            If RBPENDING.Checked = True Then
                If LOTSTATUSONMTRS = False Then
                    DT = OBJCMN.search("* , (LOT_VIEW.TOTALMTRS - LOT_VIEW.PROGRAMMTRS) AS PBALMTRS ", "", " LOT_VIEW ", " AND LOT_VIEW.YEARID=" & YearId & " AND (LOT_VIEW.TOTALPCS - LOT_VIEW.PROGRAMMTRS) > 0 AND LOT_VIEW.PROGRAMDONE = 'FALSE' AND LOT_VIEW.LOTCOMPLETED = 'FALSE' AND LOT_VIEW.GRNTYPE <> 'JOBOUT' ORDER BY JOBBERNAME, LOTNO")
                Else
                    DT = OBJCMN.search("* , (LOT_VIEW.TOTALMTRS - LOT_VIEW.PROGRAMMTRS) AS PBALMTRS ", "", " LOT_VIEW ", " AND LOT_VIEW.YEARID=" & YearId & " AND (LOT_VIEW.TOTALMTRS - LOT_VIEW.PROGRAMMTRS) > 0 AND LOT_VIEW.PROGRAMDONE = 'FALSE' AND LOT_VIEW.LOTCOMPLETED = 'FALSE' AND LOT_VIEW.GRNTYPE <> 'JOBOUT' ORDER BY JOBBERNAME, LOTNO")
                End If
            Else
                DT = OBJCMN.search("* , (LOT_VIEW.TOTALMTRS - LOT_VIEW.PROGRAMMTRS) AS PBALMTRS ", "", " LOT_VIEW ", " AND LOT_VIEW.YEARID=" & YearId & " AND LOT_VIEW.PROGRAMDONE = 'TRUE' AND LOT_VIEW.GRNTYPE <> 'JOBOUT' ORDER BY JOBBERNAME, LOTNO")
            End If
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
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
            PATH = Application.StartupPath & "\Lock-Unlock Pending Lots.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Lock-Unlock Pending Lots"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Lock-Unlock Pending Lots", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Pending Lot Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                If RBPENDING.Checked = True Then
                    If DTROW("GRNTYPE") = "OPENING" Then DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET SM_PROGRAMDONE = 1 WHERE SM_LEDGERIDTO = " & Val(DTROW("JOBBERLEDGERID")) & " AND SM_LOTNO = '" & DTROW("LOTNO") & "' AND SM_YEARID = " & YearId, "", "")
                    If DTROW("GRNTYPE") = "CHECKING" Then DT = OBJCMN.Execute_Any_String(" UPDATE GRN SET GRN_PROGRAMDONE = 1 WHERE GRN_NO = " & Val(DTROW("GRNNO")) & " AND GRN_PLOTNO = '" & DTROW("LOTNO") & "' AND GRN_TYPE = 'Job Work' AND GRN_YEARID = " & YearId, "", "")
                Else
                    If DTROW("GRNTYPE") = "OPENING" Then DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET SM_PROGRAMDONE = 0 WHERE SM_LEDGERIDTO = " & Val(DTROW("JOBBERLEDGERID")) & " AND SM_LOTNO = '" & DTROW("LOTNO") & "' AND SM_YEARID = " & YearId, "", "")
                    If DTROW("GRNTYPE") = "CHECKING" Then DT = OBJCMN.Execute_Any_String(" UPDATE GRN SET GRN_PROGRAMDONE = 0 WHERE GRN_NO = " & Val(DTROW("GRNNO")) & " AND GRN_PLOTNO = '" & DTROW("LOTNO") & "' AND GRN_TYPE = 'Job Work' AND GRN_YEARID = " & YearId, "", "")
                End If
            Next
            MsgBox("Details Updated Successfully")
            fillgrid()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBPENDING_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBPENDING.Click, RBENTERED.Click, CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class