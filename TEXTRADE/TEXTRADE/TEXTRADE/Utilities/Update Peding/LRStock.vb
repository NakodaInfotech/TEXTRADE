Imports BL

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

                dt = objclsCMST.search("*", "", " PURCHASELRSTOCK ", " AND PURCHASELRSTOCK.YEARID=" & YearId & " AND PURCHASELRSTOCK.SOLD =  0 ORDER BY TYPE, BILLNO")
            Else
                '  dt = objclsCMST.search(" BILLNO, BILLDATE,BILLINITIALS, NAME ,PARTYBILLNO , LRNO ,TOTALQTY, TOTALMTRS ,TYPE, REGID, LEDGERID, LEDGERIDTO, CMPID, YEARID ", "", "   (SELECT PURCHASEMASTER.BILL_NO AS BILLNO,dbo.PURCHASEMASTER.BILL_date AS DATE, CAST(dbo.PURCHASEMASTER.BILL_NO AS VARCHAR(20)) AS INITIALS, LEDGERS.Acc_cmpname AS NAME, ISNULL(PURCHASEMASTER.BILL_PARTYBILLNO, '') AS PARTYBILLNO, ISNULL(PURCHASEMASTER.BILL_LRNO, '') AS LRNO, dbo.PURCHASEMASTER.BILL_TOTALQTY AS TOTALQTY, dbo.PURCHASEMASTER.BILL_TOTALMTRS AS TOTALMTRS, 'PURCHASE' AS TYPE, PURCHASEMASTER.BILL_REGISTERID AS REGID, dbo.LEDGERS.Acc_ID AS LEDGERID, ISNULL(TRANSLEDGERS.Acc_ID, '')  AS LEDGERIDTO, dbo.PURCHASEMASTER.BILL_CMPID AS CMPID,  dbo.PURCHASEMASTER.BILL_YEARID AS YEARID FROM dbo.PURCHASEMASTER INNER JOIN  dbo.LEDGERS ON dbo.PURCHASEMASTER.BILL_ledgerid = dbo.LEDGERS.Acc_id LEFT OUTER JOIN dbo.LEDGERS AS TRANSLEDGERS ON dbo.PURCHASEMASTER.BILL_TRANSNAMEID = TRANSLEDGERS.Acc_id WHERE PURCHASEMASTER.BILL_SOLD = 1 AND PURCHASEMASTER.BILL_YEARID = " & YearId & " UNION ALL SELECT   STOCKMASTER.SM_NO AS BILLNO, STOCKMASTER.SM_DATE AS DATE, CAST(STOCKMASTER.SM_NO AS VARCHAR(20)) AS INITIALS, LEDGERS.Acc_cmpname AS NAME, '' AS PARTYBILLNO, ISNULL(STOCKMASTER.SM_BALENO, '') AS LRNO, STOCKMASTER.SM_PCS AS TOTALQTY, STOCKMASTER.SM_MTRS AS TOTALMTRS ,'OPENING' AS TYPE, '' AS REGID , LEDGERS.Acc_id AS LEDGERID, ISNULL(TRANSLEDGERS.Acc_id, '') AS LEDGERIDTO , STOCKMASTER.SM_CMPID AS CMPID, STOCKMASTER.SM_YEARID AS YEARID FROM  STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS TRANSLEDGERS ON STOCKMASTER.SM_LEDGERIDTO = TRANSLEDGERS.Acc_id WHERE (STOCKMASTER.SM_SOLD = 1) AND STOCKMASTER.SM_YEARID = " & YearId & ")  AS T ", " ORDER BY TYPE, DATE, BILLNO")
                dt = objclsCMST.search("*", "", " PURCHASELRSTOCK ", " AND PURCHASELRSTOCK.YEARID=" & YearId & " AND PURCHASELRSTOCK.SOLD =  1 ORDER BY TYPE, BILLNO")

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

            'PENDING
            If RBPENDING.Checked = True Then
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                    If DTROW("TYPE") = "PURCHASE" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER SET BILL_SOLD = 1 WHERE BILL_NO = " & DTROW("BILLNO") & " AND  BILL_REGISTERID = " & DTROW("REGID") & " AND BILL_YEARID = " & YearId, "", "")
                    If DTROW("TYPE") = "OPENING" Then DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET SM_SOLD = 1 WHERE SM_LEDGERID = " & DTROW("LEDGERID") & "  AND SM_LEDGERIDTO = " & DTROW("TRANSID") & "  AND SM_BALENO = '" & DTROW("LRNO") & "' AND SM_YEARID = " & YearId, "", "")

                Next
                MsgBox("Details Updated Successfully")
                FILLGRID()
                gridbill.Focus()
            End If

            'ENTERED
            If RBENTERED.Checked = True Then
                If MsgBox("You have trying to Re-Open LR Stock, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                    If DTROW("TYPE") = "PURCHASE" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER SET BILL_SOLD = 0 WHERE BILL_NO = " & DTROW("BILLNO") & "  AND BILL_REGISTERID = " & DTROW("REGID") & " AND BILL_YEARID = " & YearId, "", "")
                    If DTROW("TYPE") = "OPENING" Then DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET SM_SOLD = 0 WHERE SM_LEDGERID = " & DTROW("LEDGERID") & "  AND SM_LEDGERIDTO = " & DTROW("TRANSID") & "  AND SM_BALENO = '" & DTROW("LRNO") & "' AND SM_YEARID = " & YearId, "", "")


                Next
                MsgBox("Details Updated Successfully")
                FILLGRID()
                gridbill.Focus()
            End If


            'DT = OBJCMN.Execute_Any_String("UPDATE CHALLAN SET CHALLAN.CHALLAN_SIGNRECD = 1 WHERE CHALLAN.CHALLAN_NO = " & Val(dtrow("CHALLANNO")) & "  AND CHALLAN.CHALLAN_YEARID = " & YearId, "", "")


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
