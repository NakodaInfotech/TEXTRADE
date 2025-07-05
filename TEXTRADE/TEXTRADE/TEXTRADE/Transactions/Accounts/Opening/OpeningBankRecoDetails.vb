Imports BL
Public Class OpeningBankRecoDetails
    Public FRMSTRING As String = ""
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean


    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
    Private Sub OpeningBankRecoDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'OPENING'")
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

    Sub fillgrid()
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" ISNULL(OPENINGBANKRECO_DESC.OPENING_SRNO, 0) AS SRNO , ISNULL(REGISTERMASTER.register_name, '') AS REGISTER, ISNULL(LEDGERS.Acc_cmpname, '') AS PARTYNAME,  ISNULL(OPENINGBANKRECO_DESC.OPENING_TYPE, '') AS TYPE, ISNULL(OPENINGBANKRECO_DESC.OPENING_ENTRYNO, 0) AS ENTRYNO, ISNULL(OPENINGBANKRECO_DESC.OPENING_AMOUNT, 0) AS AMOUNT, OPENINGBANKRECO_DESC.OPENING_RECODATE AS RECODATE, ISNULL(OPENINGBANKRECO_DESC.OPENING_CHQNO, '0') AS CHQNO, OPENINGBANKRECO.OPENING_DATE AS DATE, ISNULL(OPENINGBANKRECO.OPENING_CHEQUEDEPOSITEDTOTAL, 0) AS CHEQUEDEPOSITEDTOTAL, ISNULL(OPENINGBANKRECO.OPENING_CHEQUEISSUEDTOTAL, 0) AS CHEQUEISSUEDTOTAL, ISNULL(REGISTERMASTER.register_name, '') AS REGISTERNAME, ISNULL(BANKLEDGERS.Acc_cmpname, '') AS BANKNAME ", "", " OPENINGBANKRECO_DESC INNER JOIN OPENINGBANKRECO INNER  JOIN LEDGERS AS BANKLEDGERS ON OPENINGBANKRECO.OPENING_BANKID = BANKLEDGERS.Acc_id ON OPENINGBANKRECO_DESC.OPENING_SRNO = OPENINGBANKRECO.OPENING_SRNO LEFT OUTER JOIN REGISTERMASTER ON OPENINGBANKRECO_DESC.OPENING_REGISTERID = REGISTERMASTER.register_id INNER JOIN LEDGERS ON OPENINGBANKRECO_DESC.OPENING_LEDGERID = LEDGERS.Acc_id", "AND OPENINGBANKRECO.OPENING_YEARID = " & YearId)
            griddetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                griduserrights.FocusedRowHandle = griduserrights.RowCount - 1
                griduserrights.TopRowIndex = griduserrights.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub showform(ByVal editval As Boolean, ByVal SRNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And griduserrights.RowCount > 0) Then
                Dim OBJBANK As New OpeningBankReco
                OBJBANK.MdiParent = MDIMain
                OBJBANK.EDIT = editval
                OBJBANK.TEMPBANKRECONO = SRNO
                OBJBANK.Show()
                'Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub griddetails_DoubleClick(sender As Object, e As EventArgs) Handles griddetails.DoubleClick
        Try
            showform(True, griduserrights.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class