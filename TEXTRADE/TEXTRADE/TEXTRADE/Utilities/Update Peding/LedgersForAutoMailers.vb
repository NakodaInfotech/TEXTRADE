Imports BL
Public Class LedgersForAutoMailers
    Private Sub LedgersForAutoMailers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FILLCMB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLCMB()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DTUNIT As DataTable = OBJCMN.SEARCH(" CASE WHEN ISNULL(LEDGERSFORAUTOMAILER.LFAM_LEDGERID, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS CHK, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME  ", " ", "LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.ACC_GROUPID = GROUPMASTER.GROUP_ID LEFT OUTER JOIN  LEDGERSFORAUTOMAILER ON LEDGERS.Acc_id = LEDGERSFORAUTOMAILER.LFAM_LEDGERID", " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS' AND LEDGERS.Acc_YEARID= " & YearId & " ORDER BY LEDGERS.ACC_CMPNAME")
            gridbilldetailsunit.DataSource = DTUNIT
            If DTUNIT.Rows.Count > 0 Then
                gridbillunit.FocusedRowHandle = gridbillunit.RowCount - 1
                gridbillunit.TopRowIndex = gridbillunit.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DefaultUnit_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLCMB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CHKSELECTUNIT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTUNIT.CheckedChanged
        Try
            For i As Integer = 0 To gridbillunit.RowCount - 1
                Dim dtrow As DataRow = gridbillunit.GetDataRow(i)
                dtrow("CHK") = CHKSELECTUNIT.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Dim ALPARAVAL As New ArrayList

            Dim OBJLFAM As New ClsLedgersForAutoMailers

            Dim PARTYNAME As String = ""
            gridbillunit.ClearColumnsFilter()
            For i As Integer = 0 To gridbillunit.RowCount - 1
                Dim dtrow As DataRow = gridbillunit.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If PARTYNAME = "" Then
                        PARTYNAME = dtrow("PARTYNAME")
                    Else
                        PARTYNAME = PARTYNAME & "|" & dtrow("PARTYNAME")
                    End If
                End If
            Next

            OBJLFAM.alParaval.Add(PARTYNAME)
            OBJLFAM.alParaval.Add(CmpId)
            OBJLFAM.alParaval.Add(YearId)
            Dim DT As Integer = OBJLFAM.LEDGERSFORAUTOMAILERS()
            Me.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class