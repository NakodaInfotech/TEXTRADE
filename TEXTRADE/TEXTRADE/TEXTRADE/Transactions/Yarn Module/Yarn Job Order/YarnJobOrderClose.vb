Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles
Imports BL
Imports DevExpress.XtraGrid.Views.Grid

Public Class YarnJobOrderClose

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub YarnJobOrderClose_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            fillgrid(" and yearid=" & YearId)
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

                    If DTROW("TYPE") = "PURCHASE" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER SET BILL_TEMPSOLD = 1 WHERE BILL_NO = " & Val(DTROW("BILLNO")) & " AND  BILL_YEARID = " & YearId, "", "")
                    If DTROW("TYPE") = "OPENINGPUR" Then DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET SM_TEMPSOLD = 1 WHERE SM_NO = " & Val(DTROW("BILLNO")) & " AND  SM_YEARID = " & YearId, "", "")

                Next
                MsgBox("Details Updated Successfully")
                fillgrid(" and yearid=" & YearId)
                gridbill.Focus()
            End If

            'ENTERED
            If RBENTERED.Checked = True Then
                If MsgBox("You have trying to Re-Open Close Job Docket Batch, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                    If DTROW("TYPE") = "PURCHASE" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER SET BILL_TEMPSOLD = 0 WHERE BILL_NO = " & Val(DTROW("BILLNO")) & " AND  BILL_YEARID = " & YearId, "", "")
                    If DTROW("TYPE") = "OPENINGPUR" Then DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET SM_TEMPSOLD = 0 WHERE SM_NO = " & Val(DTROW("BILLNO")) & " AND  SM_YEARID = " & YearId, "", "")
                Next
                MsgBox("Details Updated Successfully")
                fillgrid(" and yearid=" & YearId)
                gridbill.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid(" and yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim OBJCMN As New ClsCommonMaster
            Dim dt As New DataTable
            If RBPENDING.Checked = True Then

                dt = OBJCMN.search(" * ", "", "  PURCHASELRSTOCK ", TEMPCONDITION & "and TEMPSOLD = 0")
            Else
                dt = OBJCMN.search(" * ", "", "  PURCHASELRSTOCK ", TEMPCONDITION & "and TEMPSOLD = 1")

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
            fillgrid(" and yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBENTERED_CheckedChanged(sender As Object, e As EventArgs) Handles RBENTERED.CheckedChanged
        Try
            fillgrid(" and yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class