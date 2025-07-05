
Imports BL

Public Class SalaryFilter

    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String

    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, DTFROM.Value.Date)
        a2 = DatePart(DateInterval.Month, DTFROM.Value.Date)
        a3 = DatePart(DateInterval.Year, DTFROM.Value.Date)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, DTTO.Value.Date)
        a12 = DatePart(DateInterval.Month, DTTO.Value.Date)
        a13 = DatePart(DateInterval.Year, DTTO.Value.Date)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub

    Private Sub SalaryFilter_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'Dim OBJCMN As New ClsCommon
            'Dim DT As DataTable = OBJCMN.search(" DISTINCT CAST (0 AS BIT) AS CHK, LEDGERS.Acc_cmpname AS NAME ", "", " SALARYMASTER INNER JOIN LEDGERS ON SAL_DEBITLEDGERID = ACC_ID ", " AND SALARYMASTER.SAL_YEARID = " & YearId & " ORDER BY LEDGERS.Acc_cmpname ")
            'gridbilldetails.DataSource = DT
            'If DT.Rows.Count > 0 Then
            '    gridbill.FocusedRowHandle = gridbill.RowCount - 1
            '    gridbill.TopRowIndex = gridbill.RowCount - 15
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdShowReport_Click(sender As Object, e As EventArgs) Handles cmdShowReport.Click
        Try
            If RBSALARYSHEET.Checked = True And CMBMONTH.Text.Trim <> "Annually" Then

                'FOR PARTYNAME
                Dim NAMECLAUSE As String = ""
                gridbill.ClearColumnsFilter()
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then
                            NAMECLAUSE = " AND (LEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                        Else
                            NAMECLAUSE = NAMECLAUSE & " OR LEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                        End If
                    End If
                Next
                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                End If

                Dim MONTHNAME As String = UCase(CMBMONTH.Text.Trim)
                If CMBMONTH.Text.Trim = "JANUARY" Or CMBMONTH.Text.Trim = "FEBRUARY" Or CMBMONTH.Text.Trim = "MARCH" Then MONTHNAME = MONTHNAME & " - " & AccTo.Date.Year Else MONTHNAME = MONTHNAME & " - " & AccFrom.Date.Year

                Dim OBJRPT As New clsReportDesigner("Salary Report", System.AppDomain.CurrentDomain.BaseDirectory & "Salary Report.xlsx", 2)
                If CHKDATE.CheckState = CheckState.Checked Then
                    'OBJRPT.SALARYREPORT_EXCEL(CmpId, YearId, DTFROM.Value.Date, DTTO.Value.Date, NAMECLAUSE, UCase(CMBMONTH.Text.Trim), MONTHNAME)
                Else
                    'OBJRPT.SALARYREPORT_EXCEL(CmpId, YearId, AccFrom, AccTo, NAMECLAUSE, UCase(CMBMONTH.Text.Trim), MONTHNAME)
                End If
                Exit Sub
            End If



            'ANNUAL SALARY SHEET
            If RBSALARYSHEET.Checked = True And CMBMONTH.Text.Trim = "Annually" Then

                'FOR PARTYNAME
                Dim NAMECLAUSE As String = ""
                gridbill.ClearColumnsFilter()
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If NAMECLAUSE = "" Then
                            NAMECLAUSE = " AND (LEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                        Else
                            NAMECLAUSE = NAMECLAUSE & " OR LEDGERS.ACC_CMPNAME = '" & dtrow("NAME") & "'"
                        End If
                    End If
                Next
                If NAMECLAUSE <> "" Then
                    NAMECLAUSE = NAMECLAUSE & ")"
                End If
                Dim HEADER As String = "APRIL-" & AccFrom.Date.Year & " TILL MARCH-" & AccTo.Date.Year
                Dim OBJRPT As New clsReportDesigner("Annual Salary Report", System.AppDomain.CurrentDomain.BaseDirectory & "Annual Salary Report.xlsx", 2)
                'OBJRPT.ANNUALSALARYREPORT_EXCEL(CmpId, YearId, AccFrom, AccTo, NAMECLAUSE, HEADER)

                Exit Sub
            End If


            If RBBANKSHEET.Checked = True Then

                Dim OBJSAL As New SalarySlipDesign
                OBJSAL.WHERECLAUSE = " {SALARYMASTER.SAL_YEARID} = " & YearId

                If CMBMONTH.Text.Trim <> "" Then OBJSAL.WHERECLAUSE = OBJSAL.WHERECLAUSE & " AND {SALARYMASTER.SAL_MONTH} = '" & CMBMONTH.Text.Trim & "'"

                OBJSAL.FRMSTRING = "SALSTATEMENT"
                OBJSAL.MdiParent = MDIMain
                OBJSAL.Show()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class