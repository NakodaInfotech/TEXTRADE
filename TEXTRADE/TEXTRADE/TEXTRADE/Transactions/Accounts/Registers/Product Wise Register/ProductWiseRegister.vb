Imports BL

Public Class ProductWiseRegister

    Dim fromD
    Dim toD
    Dim a1, a2, a3, a4 As String
    Dim a11, a12, a13, a14 As String
    Public TEMPNAME As String = ""

    Sub getFromToDate()
        a1 = DatePart(DateInterval.Day, dtfrom.Value.Date)
        a2 = DatePart(DateInterval.Month, dtfrom.Value.Date)
        a3 = DatePart(DateInterval.Year, dtfrom.Value.Date)
        fromD = "(" & a3 & "," & a2 & "," & a1 & ")"

        a11 = DatePart(DateInterval.Day, dtto.Value.Date)
        a12 = DatePart(DateInterval.Month, dtto.Value.Date)
        a13 = DatePart(DateInterval.Year, dtto.Value.Date)
        toD = "(" & a13 & "," & a12 & "," & a11 & ")"
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdshowdetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshowdetails.Click
        fillgrid()
    End Sub

    Sub fillgrid()


        Dim dt As New DataTable()

        Dim OBJCOMMON As New ClsCommonMaster
        If chkdate.CheckState = CheckState.Unchecked Then

            dt = OBJCOMMON.search("*", "", " REGISTERPRODUCTWISE ", "  and CMPID = " & CmpId & " and YEARID = " & YearId)
        ElseIf chkdate.CheckState = CheckState.Checked Then
            dt = OBJCOMMON.search("*", "", " REGISTERPRODUCTWISE ", " and DATE >'" & Format(dtfrom.Value.Date, "MM/dd/yyyy") & "'  and DATE <'" & Format(dtto.Value.Date, "MM/dd/yyyy") & "'  and CMPID = " & CmpId & "  and YEARID = " & YearId)

        ElseIf LSTCMP.CheckedItems.Count > 1 Then

            'GET ALL YEARID FROM SELECTED COMPANY WITH SAME STARTYEAR
            Dim OBJCMN As New ClsCommon
            'Dim DT As New DataTable
            Dim CMPCLAUSE As String = ""
            Dim CHECKED_CMP As CheckedListBox.CheckedItemCollection = LSTCMP.CheckedItems
            For Each item As Object In CHECKED_CMP
                If CMPCLAUSE = "" Then
                    CMPCLAUSE = "'" & item.ToString() & "'"
                Else
                    CMPCLAUSE = CMPCLAUSE & ",'" & item.ToString() & "'"
                End If
            Next item
            dt = OBJCMN.SEARCH("cmp_id AS CMPID ,year_id AS YEARID", "", " CMPMASTER inner join YEARMASTER ON YEAR_CMPID = CMP_ID", " AND YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND CMP_NAME IN (" & CMPCLAUSE & ")")
            CMPCLAUSE = ""
            For Each DTROW As DataRow In dt.Rows
                If CMPCLAUSE = "" Then CMPCLAUSE = DTROW("YEARID") Else CMPCLAUSE = CMPCLAUSE & "," & DTROW("YEARID")
            Next
            'OBJOUTSTAND.selfor_ss = " {@YEARID} in [" & CMPCLAUSE & "]"
            ' If RBOUTSTANDINGBILLS.Checked = True Then OBJOUTSTAND.selfor_ss = " {OUTSTANDINGREPORT_ALL.YEARID} in [" & CMPCLAUSE & "]" Else OBJOUTSTAND.selfor_ss = " {OUTSTANDINGREPORT_DETAILS.YEARID} in [" & CMPCLAUSE & "]"
            ' If LSTCMP.CheckedItems.Count > 1 Then OBJOUTSTAND.MULTICMP = 1 Else OBJOUTSTAND.MULTICMP = 0

        End If
        griddetails.DataSource = dt



    End Sub


    Private Sub ProductWiseRegister_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.S And e.Alt = True Then
                cmdshowdetails_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ProductWiseRegister_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable = OBJCMN.SEARCH("CMP_NAME", "", "CMPMASTER", "")
            For Each DROW As DataRow In dt.Rows
                LSTCMP.Items.Add(DROW(0).ToString)
                If DROW(0) = CmpName Then LSTCMP.SetItemChecked(LSTCMP.Items.Count - 1, True)
            Next

            ' fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdate.CheckedChanged
        Try
            dtfrom.Enabled = chkdate.CheckState
            dtto.Enabled = chkdate.CheckState
            'fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub





    Private Sub gridregister_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridregister.DoubleClick
        Try
            showform()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform()
        Try
            If gridregister.RowCount > 0 Then
                Dim dtrow As DataRow = gridregister.GetFocusedDataRow
                If dtrow("TYPE") = "OPENINGBILL" Or dtrow("TYPE") = "OPENINGSTOCK" Then Exit Sub
                VIEWFORM(dtrow("TYPE"), True, dtrow("BILL"), dtrow("REGNAME"))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub





    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)

            PATH = Application.StartupPath & "\Register Wise Product.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim PERIOD As String = ""
            If chkdate.Checked = False Then
                PERIOD = AccFrom & " - " & AccTo
            Else
                PERIOD = dtfrom.Value.Date & " - " & dtto.Value.Date
            End If


            opti.SheetName = "Ledger Book Summary"
                griddetails.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Register Wise Product", gridregister.VisibleColumns.Count + gridregister.GroupCount, PERIOD)

        Catch ex As Exception
            MsgBox("Register Wise Product Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub


End Class


