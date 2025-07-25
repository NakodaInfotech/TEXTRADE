﻿
Imports BL

Public Class TDSChallan


    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdshowdetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshowdetails.Click
        fillgrid()
    End Sub

    Sub fillgrid()

        Dim dt As New DataTable()
        Dim ALPARAVAL As New ArrayList

        Dim objregister As New ClsTDSChallan

        ALPARAVAL.Add(cmbname.Text.Trim)
        If chkdate.Checked = True Then
            ALPARAVAL.Add(dtfrom.Value.Date)
            ALPARAVAL.Add(dtto.Value.Date)
        Else
            ALPARAVAL.Add(AccFrom)
            ALPARAVAL.Add(AccTo)
        End If

        ALPARAVAL.Add(CmpId)
        ALPARAVAL.Add(Locationid)
        ALPARAVAL.Add(YearId)

        objregister.alParaval = ALPARAVAL
        If CHKUNPAID.Checked = True Then
            dt = objregister.GETUNPAID
        ElseIf CHKUNPAID.Checked = False Then
            dt = objregister.GETALL
        End If

        griddetails.DataSource = dt

        If cmbname.Text.Trim = "" Then GTDSLEDGER.Visible = True Else GTDSLEDGER.Visible = False

    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, False, " AND LEDGERS.ACC_TDSAC = 'TRUE' AND GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' and LEDGERS.ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND LEDGERS.ACC_TDSAC = 'TRUE' AND GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' and LEDGERS.ACC_YEARID = " & YearId
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBACCCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then ledgervalidate(cmbname, CMBACCCODE, e, Me, TXTADD, " AND LEDGERS.ACC_TDSAC = 'TRUE' AND GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' and ledgers.acc_cmpname = '" & cmbname.Text.Trim & "'  AND LEDGERS.ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TDSChallan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.D And e.Alt = True Then
                cmdshowdetails_Click(sender, e)
            ElseIf e.KeyCode = Keys.S And e.Alt = True Then
                cmdok_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TDSChallan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillname(cmbname, False, " AND LEDGERS.ACC_TDSAC = 'TRUE' and LEDGERS.ACC_YEARID = " & YearId)
            'fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdate.CheckedChanged
        Try
            dtfrom.Enabled = chkdate.CheckState
            dtto.Enabled = chkdate.CheckState
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkdetails_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKUNPAID.CheckedChanged
        Try
            'fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            If ISLOCKYEAR = True Then
                MsgBox("Unable to Make changes, Year is Locked", MsgBoxStyle.Critical)
                Exit Sub
            End If


            If cmbname.Text.Trim = "" Then Exit Sub


            Dim ALPARAVAL As New ArrayList
            Dim OBJTDS As New ClsTDSChallan

            Dim INTRESULT As Integer

            Dim BILLINITIALS As String = ""
            Dim BILLDATE As String = ""
            Dim CHALLANNO As String = ""
            Dim CHALLANDATE As String = ""
            Dim CHQNO As String = ""
            Dim BANKNAME As String = ""

            If gridregister.FilterPanelText <> "" Then gridregister.ActiveFilterEnabled = False

            For i As Integer = 0 To gridregister.DataRowCount - 1
                Dim dtrow As DataRow = gridregister.GetDataRow(i)
                If dtrow("CHALLANNO").ToString.Trim <> "" Then

                    If BILLINITIALS = "" Then
                        BILLINITIALS = dtrow("BILLINITIALS").ToString
                        BILLDATE = Format(Convert.ToDateTime(dtrow("DATE")).Date, "MM/dd/yyyy")
                        CHALLANNO = dtrow("CHALLANNO").ToString
                        CHALLANDATE = Format(Convert.ToDateTime(dtrow("CHALLANDATE")).Date, "MM/dd/yyyy")
                        CHQNO = dtrow("CHQNO").ToString
                        BANKNAME = dtrow("BANKNAME").ToString
                    Else
                        BILLINITIALS = BILLINITIALS & "," & dtrow("BILLINITIALS").ToString
                        BILLDATE = BILLDATE & "," & Format(Convert.ToDateTime(dtrow("DATE")).Date, "MM/dd/yyyy")
                        CHALLANNO = CHALLANNO & "," & dtrow("CHALLANNO").ToString
                        CHALLANDATE = CHALLANDATE & "," & Format(Convert.ToDateTime(dtrow("CHALLANDATE")).Date, "MM/dd/yyyy")
                        CHQNO = CHQNO & "," & dtrow("CHQNO").ToString
                        BANKNAME = BANKNAME & "," & dtrow("BANKNAME").ToString
                    End If

                End If
            Next

            ALPARAVAL.Add(cmbname.Text.Trim)
            If chkdate.Checked = True Then
                ALPARAVAL.Add(dtfrom.Value.Date)
                ALPARAVAL.Add(dtto.Value.Date)
            Else
                ALPARAVAL.Add(AccFrom)
                ALPARAVAL.Add(AccTo)
            End If

            ALPARAVAL.Add(BILLINITIALS)
            ALPARAVAL.Add(BILLDATE)
            ALPARAVAL.Add(CHALLANNO)
            ALPARAVAL.Add(CHALLANDATE)
            ALPARAVAL.Add(CHQNO)
            ALPARAVAL.Add(BANKNAME)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)
            ALPARAVAL.Add(CHKUNPAID.Checked)
            ALPARAVAL.Add(TXTINTAMOUNT.Text.Trim)


            OBJTDS.alParaval = ALPARAVAL
            INTRESULT = OBJTDS.SAVE()
            MsgBox("Details Saved")

            fillgrid()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Challan Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = ""
            If chkdate.Checked = False Then
                PERIOD = AccFrom & " - " & AccTo
            Else
                PERIOD = dtfrom.Value.Date & " - " & dtto.Value.Date
            End If

            opti.SheetName = "Challan Details"
            griddetails.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Challan Details", gridregister.VisibleColumns.Count + gridregister.GroupCount, cmbname.Text.Trim, PERIOD)

        Catch ex As Exception
            MsgBox("TDS Challan Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub griddetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles griddetails.KeyDown
        Try
            If e.KeyCode = Keys.F12 And gridregister.FocusedRowHandle - 1 >= 0 Then
                'COPY DATA FROM UPPER LINE

                Dim DTROWUP As DataRow = gridregister.GetDataRow(gridregister.FocusedRowHandle - 1)
                Dim DTROW As DataRow = gridregister.GetFocusedDataRow
                DTROW("CHALLANNO") = DTROWUP("CHALLANNO")
                DTROW("CHALLANDATE") = DTROWUP("CHALLANDATE")
                DTROW("CHQNO") = DTROWUP("CHQNO")
                DTROW("BANKNAME") = DTROWUP("BANKNAME")

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDAPPLY_Click(sender As Object, e As EventArgs) Handles CMDAPPLY.Click
        Try

            If TXTCHNO.Text.Trim <> "" And TXTBSRCODE.Text.Trim <> "" Then
                If MsgBox("Apply Data for all records?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                For i As Integer = 0 To gridregister.DataRowCount - 1
                    Dim dtrow As DataRow = gridregister.GetDataRow(i)
                    dtrow("CHALLANNO") = TXTCHNO.Text.Trim
                    dtrow("CHALLANDATE") = CHDATE.Value.Date
                    dtrow("CHQNO") = TXTCHQNO.Text.Trim
                    dtrow("BANKNAME") = TXTBSRCODE.Text.Trim
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTINTAMOUNT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTINTAMOUNT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub
End Class