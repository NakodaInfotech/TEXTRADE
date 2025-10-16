
Imports BL
Imports DevExpress.Diagram.Core

Public Class UpdateHoldforIntCalc

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateHoldforIntCalc_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub UpdateHoldforIntCalc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
                GNEWDATE.Visible = False
                dt = objclsCMST.search("*", "", " HOLDFORINTCALCVIEW ", " AND HOLDINTCALC = 'FALSE' AND HOLDFORINTCALCVIEW.YEARID=" & YearId & " ORDER BY ENTRYTYPE, DATE, ENTRYNO")
            Else
                GNEWDATE.Visible = True
                dt = objclsCMST.search("*, CAST(NULL AS DATE) AS NEWDATE", "", " HOLDFORINTCALCVIEW ", " AND HOLDINTCALC = 'TRUE' AND HOLDFORINTCALCVIEW.YEARID=" & YearId & " ORDER BY ENTRYTYPE, DATE, ENTRYNO")
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
            PATH = Application.StartupPath & "\Hold-Unhold For Int Calc.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Hold-Unhold For Int Calc"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Hold-Unhold For Int Calc", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Hold-Unhold For Int Calc Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
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

                    If DTROW("ENTRYTYPE") = "INVOICE" Then DT = OBJCMN.Execute_Any_String(" UPDATE INVOICEMASTER SET INVOICE_HOLDINTCALC = 1 FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(DTROW("ENTRYNO")) & " AND REGISTER_NAME = '" & DTROW("REGNAME") & "' AND INVOICE_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "PURCHASE" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER SET BILL_HOLDINTCALC = 1 FROM PURCHASEMASTER INNER JOIN REGISTERMASTER ON BILL_REGISTERID = REGISTER_ID WHERE BILL_NO = " & Val(DTROW("ENTRYNO")) & " AND REGISTER_NAME = '" & DTROW("REGNAME") & "' AND BILL_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "CREDITNOTE" Then DT = OBJCMN.Execute_Any_String(" UPDATE CREDITNOTEMASTER SET CN_HOLDINTCALC = 1 FROM CREDITNOTEMASTER INNER JOIN REGISTERMASTER ON CN_REGISTERID = REGISTER_ID WHERE CN_NO = " & Val(DTROW("ENTRYNO")) & " AND REGISTER_NAME = '" & DTROW("REGNAME") & "' AND CN_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "DEBITNOTE" Then DT = OBJCMN.Execute_Any_String(" UPDATE DEBITNOTEMASTER SET DN_HOLDINTCALC = 1 FROM DEBITNOTEMASTER INNER JOIN REGISTERMASTER ON DN_REGISTERID = REGISTER_ID WHERE DN_NO = " & Val(DTROW("ENTRYNO")) & " AND REGISTER_NAME = '" & DTROW("REGNAME") & "' AND DN_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "SALERETURN" Then DT = OBJCMN.Execute_Any_String(" UPDATE SALERETURN SET SALRET_HOLDINTCALC = 1 WHERE SALRET_NO = " & Val(DTROW("ENTRYNO")) & " AND SALRET_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "PURCHASERETURN" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASERETURN SET PR_HOLDINTCALC = 1 WHERE PR_NO = " & Val(DTROW("ENTRYNO")) & " AND PR_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "OPENING" Then DT = OBJCMN.Execute_Any_String(" UPDATE OPENINGBILL SET BILL_HOLDINTCALC = 1 FROM OPENINGBILL INNER JOIN REGISTERMASTER ON BILL_REGISTERID = REGISTER_ID INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.ACC_ID WHERE BILL_NO = " & Val(DTROW("ENTRYNO")) & " AND LEDGERS.ACC_CMPNAME = '" & DTROW("NAME") & "' AND BILL_YEARID = " & YearId, "", "")


                Next
                MsgBox("Details Updated Successfully")
                FILLGRID()
                gridbill.Focus()
            End If

            'ENTERED
            If RBENTERED.Checked = True Then
                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                    If IsDBNull(DTROW("NEWDATE")) = True Then GoTo SKIPLINE

                    If DTROW("ENTRYTYPE") = "INVOICE" Then DT = OBJCMN.Execute_Any_String(" UPDATE INVOICEMASTER SET INVOICE_HOLDINTCALC = 0, INVOICE_CRDAYS = INVOICE_CRDAYS + DATEDIFF(DAY, INVOICE_DATE, '" & Format(Convert.ToDateTime(DTROW("NEWDATE")), "yyyy-MM-dd") & "'), INVOICE_DUEDATE = DATEADD(DAY,INVOICE_CRDAYS + DATEDIFF(DAY, INVOICE_DATE, '" & Format(Convert.ToDateTime(DTROW("NEWDATE")), "yyyy-MM-dd") & "'), INVOICE_DATE) FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(DTROW("ENTRYNO")) & " AND REGISTER_NAME = '" & DTROW("REGNAME") & "' AND INVOICE_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "PURCHASE" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER SET BILL_HOLDINTCALC = 0 FROM PURCHASEMASTER INNER JOIN REGISTERMASTER ON BILL_REGISTERID = REGISTER_ID WHERE BILL_NO = " & Val(DTROW("ENTRYNO")) & " AND REGISTER_NAME = '" & DTROW("REGNAME") & "' AND BILL_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "CREDITNOTE" Then DT = OBJCMN.Execute_Any_String(" UPDATE CREDITNOTEMASTER SET CN_HOLDINTCALC = 0 FROM CREDITNOTEMASTER INNER JOIN REGISTERMASTER ON CN_REGISTERID = REGISTER_ID WHERE CN_NO = " & Val(DTROW("ENTRYNO")) & " AND REGISTER_NAME = '" & DTROW("REGNAME") & "' AND CN_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "DEBITNOTE" Then DT = OBJCMN.Execute_Any_String(" UPDATE DEBITNOTEMASTER SET DN_HOLDINTCALC = 0 FROM DEBITNOTEMASTER INNER JOIN REGISTERMASTER ON DN_REGISTERID = REGISTER_ID WHERE DN_NO = " & Val(DTROW("ENTRYNO")) & " AND REGISTER_NAME = '" & DTROW("REGNAME") & "' AND DN_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "SALERETURN" Then DT = OBJCMN.Execute_Any_String(" UPDATE SALERETURN SET SALRET_HOLDINTCALC = 0 WHERE SALRET_NO = " & Val(DTROW("ENTRYNO")) & " AND SALRET_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "PURCHASERETURN" Then DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASERETURN SET PR_HOLDINTCALC = 0 WHERE PR_NO = " & Val(DTROW("ENTRYNO")) & " AND PR_YEARID = " & YearId, "", "")
                    If DTROW("ENTRYTYPE") = "OPENING" Then DT = OBJCMN.Execute_Any_String(" UPDATE OPENINGBILL SET BILL_HOLDINTCALC = 0 FROM OPENINGBILL INNER JOIN REGISTERMASTER ON BILL_REGISTERID = REGISTER_ID INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.ACC_ID WHERE BILL_NO = " & Val(DTROW("ENTRYNO")) & " AND LEDGERS.ACC_CMPNAME = '" & DTROW("NAME") & "' AND BILL_YEARID = " & YearId, "", "")
SKIPLINE:
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

    Private Sub RBPENDING_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBPENDING.Click, RBENTERED.Click, CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class