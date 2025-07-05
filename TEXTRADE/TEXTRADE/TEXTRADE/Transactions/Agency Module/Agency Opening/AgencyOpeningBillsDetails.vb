

Imports BL

Public Class AgencyOpeningBillsDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String = ""

    Private Sub OpeningBillDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub AgencyOpeningBillsDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            'If FRMSTRING = "AGENCYOPENINGBILLS" Then
            '    lbl.Text = "Agency Opening Bill"
            '    Me.Text = "Agency Opening Bill"
            'Else
            '    lbl.Text = "Agency Opening Bill (Interest)"
            '    Me.Text = "Agency Opening Bill (Interest)"
            'End If
            fillgridname()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgridname(Optional ByVal whereclause As String = "")
        Try
            Dim objClsCommon As New ClsCommonMaster
            Dim dttable As DataTable
            'If FRMSTRING = "AGENCYOPENINGBILLS" Then
            dttable = objClsCommon.search(" DISTINCT LEDGERS.ACC_CMPNAME as NAME", "", " AGENCYOPENINGBILL INNER JOIN LEDGERS ON AGENCYOPENINGBILL.ABILL_LEDGERID = LEDGERS.Acc_id AND AGENCYOPENINGBILL.ABILL_CMPID = LEDGERS.Acc_cmpid AND AGENCYOPENINGBILL.ABILL_LOCATIONID = LEDGERS.Acc_locationid AND AGENCYOPENINGBILL.ABILL_YEARID = LEDGERS.Acc_yearid ", whereclause & " and Acc_cmpid = " & CmpId & " and Acc_locationid = " & Locationid & " and Acc_yearid = " & YearId & " order by Acc_cmpname")
            'Else
            '    dttable = objClsCommon.search(" DISTINCT LEDGERS.ACC_CMPNAME as NAME", "", " AGENCYOPENINGBILL_INT INNER JOIN LEDGERS ON AGENCYOPENINGBILL_INT.ABILL_LEDGERID = LEDGERS.Acc_id AND AGENCYOPENINGBILL_INT.ABILL_CMPID = LEDGERS.Acc_cmpid AND AGENCYOPENINGBILL_INT.ABILL_LOCATIONID = LEDGERS.Acc_locationid AND AGENCYOPENINGBILL_INT.ABILL_YEARID = LEDGERS.Acc_yearid ", whereclause & " and Acc_cmpid = " & CmpId & " and Acc_locationid = " & Locationid & " and Acc_yearid = " & YearId & " order by Acc_cmpname")
            'End If
            griduserdetails.DataSource = dttable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getdetails(ByRef name As String)
        Dim OBJCMN As New ClsCommon
        Dim dttable As DataTable
        'If FRMSTRING = "AGENCYOPENINGBILLS" Then
        dttable = OBJCMN.SEARCH(" LEDGERS.Acc_cmpname AS NAME, AGENCYOPENINGBILL.ABILL_GRIDSRNO AS SRNO, AGENCYOPENINGBILL.ABILL_TYPE AS TYPE, AGENCYOPENINGBILL.ABILL_NO AS BILLNO, AGENCYOPENINGBILL.ABILL_YEAR AS YEAR, AGENCYOPENINGBILL.ABILL_DATE AS DATE, AGENCYOPENINGBILL.ABILL_DUEDATE AS DUEDATE,AGENCYOPENINGBILL.ABILL_AMT AS AMT", "", " AGENCYOPENINGBILL INNER JOIN LEDGERS ON AGENCYOPENINGBILL.ABILL_LEDGERID = LEDGERS.Acc_id AND AGENCYOPENINGBILL.ABILL_CMPID = LEDGERS.Acc_cmpid AND AGENCYOPENINGBILL.ABILL_LOCATIONID = LEDGERS.Acc_locationid AND AGENCYOPENINGBILL.ABILL_YEARID = LEDGERS.Acc_yearid", " AND LEDGERS.ACC_CMPNAME = '" & name & "' AND ABILL_CMPID = " & CmpId & " AND ABILL_LOCATIONID = " & Locationid & " AND ABILL_YEARID = " & YearId & " ORDER BY ABILL_GRIDSRNO")
        'Else
        '    dttable = OBJCMN.SEARCH(" LEDGERS.Acc_cmpname AS NAME, AGENCYOPENINGBILL_INT.ABILL_GRIDSRNO AS SRNO, AGENCYOPENINGBILL_INT.ABILL_TYPE AS TYPE, AGENCYOPENINGBILL_INT.ABILL_NO AS BILLNO, AGENCYOPENINGBILL_INT.ABILL_YEAR AS YEAR, AGENCYOPENINGBILL_INT.ABILL_DATE AS DATE, AGENCYOPENINGBILL_INT.ABILL_DUEDATE AS DUEDATE,AGENCYOPENINGBILL_INT.ABILL_AMT AS AMT", "", " AGENCYOPENINGBILL_INT INNER JOIN LEDGERS ON AGENCYOPENINGBILL_INT.ABILL_LEDGERID = LEDGERS.Acc_id AND AGENCYOPENINGBILL_INT.ABILL_CMPID = LEDGERS.Acc_cmpid AND AGENCYOPENINGBILL_INT.ABILL_LOCATIONID = LEDGERS.Acc_locationid AND AGENCYOPENINGBILL_INT.ABILL_YEARID = LEDGERS.Acc_yearid", " AND LEDGERS.ACC_CMPNAME = '" & name & "' AND ABILL_CMPID = " & CmpId & " AND ABILL_LOCATIONID = " & Locationid & " AND ABILL_YEARID = " & YearId & " ORDER BY ABILL_GRIDSRNO")
        'End If
        griddetails.DataSource = dttable
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEDIT.Click
        Try
            showform(True, GRIDUSERNAME.GetFocusedRowCellValue("NAME"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal name As String)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And GRIDUSERNAME.RowCount > 0) Then
                Dim OBJOP As New AgencyOpeningBills
                OBJOP.MdiParent = MDIMain
                OBJOP.FRMSTRING = FRMSTRING
                OBJOP.TEMPNAME = name
                OBJOP.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(False, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDUSERNAME_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GRIDUSERNAME.Click
        Try
            getdetails(GRIDUSERNAME.GetFocusedRowCellValue("NAME"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDUSERNAME_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GRIDUSERNAME.DoubleClick
        Try
            showform(True, GRIDUSERNAME.GetFocusedRowCellValue("NAME"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDUSERNAME_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GRIDUSERNAME.FocusedRowChanged
        Try
            getdetails(GRIDUSERNAME.GetFocusedRowCellValue("NAME"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgridname()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class