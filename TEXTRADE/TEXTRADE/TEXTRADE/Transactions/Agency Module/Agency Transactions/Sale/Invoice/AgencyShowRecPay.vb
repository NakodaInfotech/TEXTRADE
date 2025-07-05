Imports BL

Public Class AgencyShowRecPay



    Public PURBILLINITIALS As String
    Public SALEBILLINITIALS As String

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub AgencyShowRecPay_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt = True And e.KeyCode = Windows.Forms.Keys.O Then       'for Saving
                Call CMDOK_Click(sender, e)
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AgencyShowRecPay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" T.[Bill No] , T.Amt , T.BILLINITIALS,T.TYPE, T.Date ", "", " (SELECT AGENCYRECEIPTMASTER_DESC.Areceipt_initials AS [Bill No], SUM(AGENCYRECEIPTMASTER_DESC.Areceipt_amt) AS Amt, AGENCYRECEIPTMASTER_DESC.Areceipt_no AS BILLINITIALS, 'RECEIPT' AS TYPE, AGENCYRECEIPTMASTER.Areceipt_date AS Date FROM AGENCYRECEIPTMASTER_DESC INNER JOIN AGENCYRECEIPTMASTER ON AGENCYRECEIPTMASTER.Areceipt_no = AGENCYRECEIPTMASTER_DESC.Areceipt_no WHERE (AGENCYRECEIPTMASTER.Areceipt_cmpid = " & CmpId & ") AND (AGENCYRECEIPTMASTER.Areceipt_locationid = " & Locationid & ") AND (AGENCYRECEIPTMASTER.Areceipt_YEARID = " & YearId & ") AND (AGENCYRECEIPTMASTER_DESC.Areceipt_BILLINITIALS = '" & SALEBILLINITIALS & "') GROUP BY AGENCYRECEIPTMASTER_DESC.Areceipt_no, AGENCYRECEIPTMASTER_DESC.Areceipt_initials, AGENCYRECEIPTMASTER.Areceipt_date UNION ALL SELECT AGENCYCREDITNOTEMASTER.ACN_initials AS [Bill No], SUM(AGENCYCREDITNOTEMASTER_BILLDESC.ACN_amt) AS Amt, AGENCYCREDITNOTEMASTER_BILLDESC.ACN_no AS BILLINITIALS, 'CREDITNOTE' AS TYPE, AGENCYCREDITNOTEMASTER.ACN_date AS Date FROM AGENCYCREDITNOTEMASTER_BILLDESC INNER JOIN AGENCYCREDITNOTEMASTER ON AGENCYCREDITNOTEMASTER.ACN_no = AGENCYCREDITNOTEMASTER_BILLDESC.ACN_no  WHERE AGENCYCREDITNOTEMASTER.ACN_YEARID = " & YearId & " AND (AGENCYCREDITNOTEMASTER_BILLDESC.ACN_BILLINITIALS = '" & PURBILLINITIALS & "') GROUP BY AGENCYCREDITNOTEMASTER_BILLDESC.ACN_no, AGENCYCREDITNOTEMASTER.ACN_initials ,AGENCYCREDITNOTEMASTER.ACN_date UNION ALL SELECT AGENCYCREDITNOTEMASTER.ACN_initials AS [Bill No], AGENCYCREDITNOTEMASTER.ACN_GTOTAL AS Amt, AGENCYCREDITNOTEMASTER.ACN_no AS BILLINITIALS, 'CREDITNOTE' AS TYPE, AGENCYCREDITNOTEMASTER.ACN_date AS Date FROM AGENCYCREDITNOTEMASTER  WHERE (AGENCYCREDITNOTEMASTER.ACN_YEARID = " & YearId & ") AND (AGENCYCREDITNOTEMASTER.ACN_BILLNO = '" & SALEBILLINITIALS & "') UNION ALL SELECT 'SR-' + CAST(ASALRET_NO AS VARCHAR(20)) AS [Bill No], AGENCYSALERETURN.ASALRET_GRANDTOTAL AS Amt, AGENCYSALERETURN.ASALRET_no AS BILLINITIALS, 'SALERETURN' AS TYPE, AGENCYSALERETURN.ASALRET_date AS Date FROM  AGENCYSALERETURN  WHERE (AGENCYSALERETURN.ASALRET_YEARID = " & YearId & ")  AND AGENCYSALERETURN.ASALRET_INVOICEINITIALS = '" & SALEBILLINITIALS & "' UNION ALL SELECT 'SR-' + CAST(AGENCYSALERETURN.ASALRET_NO AS VARCHAR(20)) AS [Bill No], AGENCYSALERETURN_BILLDESC.ASALRET_AMT AS Amt, AGENCYSALERETURN.ASALRET_no AS BILLINITIALS,'SALERETURN ' AS TYPE, AGENCYSALERETURN.ASALRET_date AS Date FROM  AGENCYSALERETURN INNER JOIN AGENCYSALERETURN_BILLDESC ON AGENCYSALERETURN.ASALRET_NO = AGENCYSALERETURN_BILLDESC.ASALRET_NO AND AGENCYSALERETURN.ASALRET_yearid = AGENCYSALERETURN_BILLDESC.ASALRET_YEARID WHERE (AGENCYSALERETURN.ASALRET_YEARID = " & YearId & ")  AND  ASALRET_BILLINITIALS = '" & SALEBILLINITIALS & "') AS T ", "")


            If DT.Rows.Count > 0 Then
                GRIDRECPAY.DataSource = DT
                GRIDRECPAY.Columns(0).Width = 150
                GRIDRECPAY.Columns(1).Width = 150
                GRIDRECPAY.Columns(2).Visible = False
                GRIDRECPAY.Columns(3).Visible = False
                GRIDRECPAY.Columns(4).Width = 85

                GRIDRECPAY.Columns(1).DefaultCellStyle.Format = "N2"
                GRIDRECPAY.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                GRIDRECPAY.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                TXTTOTAL.Clear()
                For Each ROW As DataGridViewRow In GRIDRECPAY.Rows
                    TXTTOTAL.Text = Format(Val(TXTTOTAL.Text.Trim) + Val(ROW.Cells(1).Value), "0.00")
                Next

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            ' SHOWFORM()
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SHOWFORM()
        Try
            For Each ROW As DataGridViewRow In GRIDRECPAY.SelectedRows



                If ROW.Cells(4).Value.ToString = "RECEIPT" Then

                    Dim OBJREC As New AgencyReceipt
                    OBJREC.MdiParent = MDIMain
                    OBJREC.EDIT = True
                    OBJREC.TEMPARECEIPTNO = ROW.Cells(2).Value.ToString
                    OBJREC.TEMPREGNAME = ROW.Cells(3).Value.ToString
                    OBJREC.Show()


                ElseIf ROW.Cells(4).Value.ToString = "CREDITNOTE" Then

                    Dim OBJCN As New AgencyCreditNote
                    OBJCN.MdiParent = MDIMain
                    OBJCN.edit = True
                    OBJCN.TEMPCNNO = ROW.Cells(2).Value.ToString
                    OBJCN.TEMPREGNAME = ROW.Cells(3).Value.ToString
                    OBJCN.Show()



                ElseIf ROW.Cells(4).Value.ToString = "SALERETURN" Then

                    Dim OBJSR As New AgencySaleReturn
                    OBJSR.MdiParent = MDIMain
                    OBJSR.EDIT = True
                    OBJSR.TEMPSALRETNO = Val(ROW.Cells(2).Value)
                    OBJSR.Show()

                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDRECPAY_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDRECPAY.CellDoubleClick
        Try
            SHOWFORM()
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class

