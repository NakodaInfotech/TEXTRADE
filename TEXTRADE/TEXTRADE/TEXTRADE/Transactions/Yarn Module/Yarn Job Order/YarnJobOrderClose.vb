Imports BL

Public Class YarnJobOrderClose

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub YarnJobOrderClose_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'YARN JOBORDER'")
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

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            'PENDING
            If RBPENDING.Checked = True Then
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                    If DTROW("TYPE") = "JOBORDER" Then DT = OBJCMN.Execute_Any_String(" UPDATE JOBORDER_DESC SET JOB_CLOSED = 1 WHERE JOB_NO = " & Val(DTROW("JOBNO")) & "  AND JOB_SRNO = " & Val(DTROW("JOBSRNO")) & " AND  JOB_YEARID = " & YearId, "", "")
                    If DTROW("TYPE") = "OPJOBORDER" Then DT = OBJCMN.Execute_Any_String(" UPDATE OPENINGYARNJOBORDER_DESC SET OYJOB_CLOSED = 1 WHERE OYJOB_NO = " & Val(DTROW("JOBNO")) & "  AND OYJOB_SRNO = " & Val(DTROW("JOBSRNO")) & " AND  OYJOB_YEARID = " & YearId, "", "")

                Next
                MsgBox("Details Updated Successfully")
                fillgrid()
                gridbill.Focus()
            End If

            'ENTERED
            If RBENTERED.Checked = True Then
                If MsgBox("You have trying to Re-Open Close Yarn Job Order, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                    If DTROW("TYPE") = "JOBORDER" Then DT = OBJCMN.Execute_Any_String(" UPDATE JOBORDER_DESC SET JOB_CLOSED = 0 WHERE JOB_NO = " & Val(DTROW("JOBNO")) & "  AND JOB_SRNO = " & Val(DTROW("JOBSRNO")) & " AND  JOB_YEARID = " & YearId, "", "")
                    If DTROW("TYPE") = "OPJOBORDER" Then DT = OBJCMN.Execute_Any_String(" UPDATE OPENINGYARNJOBORDER_DESC SET OYJOB_CLOSED = 0 WHERE OYJOB_NO = " & Val(DTROW("JOBNO")) & "  AND OYJOB_SRNO = " & Val(DTROW("JOBSRNO")) & " AND  OYJOB_YEARID = " & YearId, "", "")
                Next
                MsgBox("Details Updated Successfully")
                fillgrid()
                gridbill.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As New DataTable
            If RBPENDING.Checked = True Then

                dt = OBJCMN.search(" ALLJOBORDER.JOB_NO AS JOBNO,ALLJOBORDER.JOB_DATE AS DATE, ITEMMASTER.item_name AS ITEMNAME, LEDGERS.Acc_CMPname AS NAME, COLORMASTER.COLOR_name AS COLOR, ISNULL(ALLJOBORDER_DESC.JOB_MTRS - ALLJOBORDER_DESC.JOB_OUTMTRS,  0) AS TOTALMTRS, ALLJOBORDER_DESC.JOB_SRNO AS JOBSRNO ,ALLJOBORDER.TYPE, ISNULL(ALLJOBORDER.JOB_REMARKS, '') AS REMARKS  ", "", "  ALLJOBORDER INNER JOIN  ALLJOBORDER_DESC ON ALLJOBORDER.JOB_NO = ALLJOBORDER_DESC.JOB_NO AND ALLJOBORDER.TYPE = ALLJOBORDER_DESC.TYPE AND ALLJOBORDER.JOB_YEARID = ALLJOBORDER_DESC.JOB_YEARID INNER JOIN ITEMMASTER ON ALLJOBORDER_DESC.JOB_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS ON ALLJOBORDER.JOB_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN COLORMASTER ON ALLJOBORDER_DESC.JOB_SHADEID = COLORMASTER.COLOR_id   ", " AND ALLJOBORDER.JOB_YEARID =  " & YearId & " and JOB_CLOSED = 0 AND (ALLJOBORDER_DESC.JOB_MTRS - ALLJOBORDER_DESC.JOB_OUTMTRS > 0) ORDER BY  ALLJOBORDER.JOB_NO ")
            Else
                dt = OBJCMN.search("ALLJOBORDER.JOB_NO AS JOBNO,ALLJOBORDER.JOB_DATE AS DATE ,ITEMMASTER.item_name AS ITEMNAME, LEDGERS.Acc_CMPname AS NAME, COLORMASTER.COLOR_name AS COLOR, ISNULL(ALLJOBORDER_DESC.JOB_MTRS - ALLJOBORDER_DESC.JOB_OUTMTRS,  0) AS TOTALMTRS, ALLJOBORDER_DESC.JOB_SRNO AS JOBSRNO ,ALLJOBORDER.TYPE, ISNULL(ALLJOBORDER.JOB_REMARKS, '') AS REMARKS  ", "", "  ALLJOBORDER INNER JOIN  ALLJOBORDER_DESC ON ALLJOBORDER.JOB_NO = ALLJOBORDER_DESC.JOB_NO AND ALLJOBORDER.TYPE = ALLJOBORDER_DESC.TYPE AND ALLJOBORDER.JOB_YEARID = ALLJOBORDER_DESC.JOB_YEARID INNER JOIN ITEMMASTER ON ALLJOBORDER_DESC.JOB_ITEMID = ITEMMASTER.item_id INNER JOIN LEDGERS ON ALLJOBORDER.JOB_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN COLORMASTER ON ALLJOBORDER_DESC.JOB_SHADEID = COLORMASTER.COLOR_id   ", " AND ALLJOBORDER.JOB_YEARID =  " & YearId & " and JOB_CLOSED = 1 AND (ALLJOBORDER_DESC.JOB_MTRS - ALLJOBORDER_DESC.JOB_OUTMTRS > 0) ORDER BY  ALLJOBORDER.JOB_NO ")

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

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Yarn Job Order Close Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Yarn Job Order Close Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Yarn Job Order Close Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Yarn Job Order Close Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub RBPENDING_CheckedChanged(sender As Object, e As EventArgs) Handles RBPENDING.CheckedChanged
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBENTERED_CheckedChanged(sender As Object, e As EventArgs) Handles RBENTERED.CheckedChanged
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class