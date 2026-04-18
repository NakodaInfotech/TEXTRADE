
Imports BL
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.Utils.Gesture
Public Class ReprintGrey
    Public TEMPNO As String

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub cmdprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprint.Click
        Try
            Dim TEMPMSG As Integer = MsgBox("Wish to Print Barcode?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbNo Then Exit Sub


            Dim TEMPHEADER As String = ""
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable


            For Each ROW As DataGridViewRow In GRIDREPRINT.Rows
                For I As Integer = 1 To Val(txtcopies.Text.Trim)
                    BARCODEPRINTING(ROW.Cells(GBARCODE.Index).Value, "FRESH", ROW.Cells(GITEMNAME.Index).Value, ROW.Cells(GQUALITY.Index).Value, ROW.Cells(GDESIGN.Index).Value, ROW.Cells(GSHADE.Index).Value, ROW.Cells(GUNIT.Index).Value, "", ROW.Cells(GBALENO.Index).Value, "", Val(ROW.Cells(GMTRS.Index).Value), Val((ROW.Cells(GQTY.Index).Value)), 0, ROW.Cells(GRACK.Index).Value, TEMPHEADER, "", 0, "", "", ROW.Cells(GSHELF.Index).Value, ROW.Cells(GGREYDATE.Index).Value)
                Next
            Next
LINE1:
            clear()
            txtbarcode.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Labelprint_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Sub clear()
        Try
            txtbarcode.Clear()
            TXTBALENO.Clear()
            txtcopies.Text = 1
            GRIDREPRINT.RowCount = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Labelprint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clear()
    End Sub

    Private Sub txtcopies_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcopies.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Reprint_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then CHKBARCODE.Visible = True
        If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "KDFAB" Or ClientName = "ALENCOT" Then GPRINTDESC.Visible = True
        If ClientName = "DEVEN" Then
            CHKBARCODE.Visible = True
            CHKBARCODE.Text = "Print In Yards"
        End If
        If ClientName = "SNCM" Then
            TXTBALENO.Visible = True
            CMDPRINTPS.Visible = True
            lblbaleno.Visible = True

        End If
    End Sub

    Private Sub txtbarcode_Validated(sender As Object, e As EventArgs) Handles txtbarcode.Validated
        Try
            If txtbarcode.Text.Trim = "" Then Exit Sub
            txtbarcode.Text = txtbarcode.Text.Replace(" TRIAL", "")
            If ClientName = "SHEETAL" And Len(txtbarcode.Text.Trim) > 7 And Char.IsDigit(txtbarcode.Text(0)) = True Then txtbarcode.Text = txtbarcode.Text.Substring(0, txtbarcode.Text.Length - 1)
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("TOP 1 *", "", "GREYBARCODESTOCK", " AND BARCODE = '" & txtbarcode.Text.Trim & "' AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then

                'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                For Each ROW As DataGridViewRow In GRIDREPRINT.Rows
                    If LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(txtbarcode.Text.Trim) Then GoTo LINE1
                Next

                GRIDREPRINT.Rows.Add(0, DT.Rows(0).Item("PIECETYPE"), DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("GRIDREMARKS"), DT.Rows(0).Item("DESIGNNO"), DT.Rows(0).Item("COLOR"), DT.Rows(0).Item("UNIT"), DT.Rows(0).Item("LOTNO"), Format((Val(DT.Rows(0).Item("CUT"))), "0.00"), Format((Val(DT.Rows(0).Item("MTRS"))), "0.00"), Format((Val(DT.Rows(0).Item("PCS"))), "0.00"), DT.Rows(0).Item("BARCODE"), DT.Rows(0).Item("RACK"), DT.Rows(0).Item("SHELF"), Format(DT.Rows(0).Item("DATE"), "dd/MM/yyyy"), DT.Rows(0).Item("BALENO"), "", Val(DT.Rows(0).Item("FROMNO")), Val(DT.Rows(0).Item("FROMSRNO")), DT.Rows(0).Item("TYPE"))
                GRIDREPRINT.FirstDisplayedScrollingRowIndex = GRIDREPRINT.RowCount - 1
                getsrno(GRIDREPRINT)
            Else
                MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
            End If
LINE1:
            txtbarcode.Clear()
            txtbarcode.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(sender As Object, e As EventArgs) Handles CMDSELECTSTOCK.Click
        Try


            Dim DTGDN As New DataTable
            Dim OBJSELECTGDN As New SelectStockGDN
            OBJSELECTGDN.ShowDialog()
            DTGDN = OBJSELECTGDN.DT

            If DTGDN.Rows.Count > 0 Then
                For Each DTROWPS As DataRow In DTGDN.Rows

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    For Each ROW As DataGridViewRow In GRIDREPRINT.Rows
                        If DTROWPS("BARCODE") <> "" And LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(DTROWPS("BARCODE")) Or (DTROWPS("BARCODE") = "" And Val(ROW.Cells(GFROMNO.Index).Value) = Val(DTROWPS("FROMNO")) And Val(ROW.Cells(GFROMSRNO.Index).Value) = Val(DTROWPS("FROMSRNO"))) Then GoTo LINE1
                    Next

                    Dim GRIDDESC As String = ""
                    If ClientName = "AVIS" Then GRIDDESC = DTROWPS("LOTNO")
                    GRIDREPRINT.Rows.Add(0, DTROWPS("PIECETYPE"), DTROWPS("ITEMNAME"), DTROWPS("QUALITY"), DTROWPS("GRIDREMARKS"), DTROWPS("DESIGNNO"), DTROWPS("COLOR"), DTROWPS("UNIT"), DTROWPS("LOTNO"), Format(Val(DTROWPS("CUT")), "0.00"), Format(Val(DTROWPS("MTRS")), "0.00"), DTROWPS("BARCODE"), DTROWPS("RACK"), DTROWPS("SHELF"), DTROWPS("BALENO"), "", Val(DTROWPS("FROMNO")), Val(DTROWPS("FROMSRNO")), DTROWPS("TYPE"))
LINE1:
                Next
                CMDSELECTSTOCK.Enabled = True
                getsrno(GRIDREPRINT)
                GRIDREPRINT.FirstDisplayedScrollingRowIndex = GRIDREPRINT.RowCount - 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDREPRINT_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDREPRINT.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDREPRINT.RowCount > 0 Then
                GRIDREPRINT.Rows.RemoveAt(GRIDREPRINT.CurrentRow.Index)
                getsrno(GRIDREPRINT)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTBALENO_Validated(sender As Object, e As EventArgs) Handles TXTBALENO.Validated
        Try
            If TXTBALENO.Text.Trim = "" Then Exit Sub
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("TOP 1 *", "", "GREYBARCODESTOCK", " AND BALENO = '" & TXTBALENO.Text.Trim & "' AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then

                'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                For Each ROW As DataGridViewRow In GRIDREPRINT.Rows
                    If LCase(ROW.Cells(GBALENO.Index).Value) = LCase(TXTBALENO.Text.Trim) Then GoTo LINE1
                Next

                GRIDREPRINT.Rows.Add(0, DT.Rows(0).Item("PIECETYPE"), DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("GRIDREMARKS"), DT.Rows(0).Item("DESIGNNO"), DT.Rows(0).Item("COLOR"), DT.Rows(0).Item("UNIT"), DT.Rows(0).Item("LOTNO"), Format((Val(DT.Rows(0).Item("CUT"))), "0.00"), Format((Val(DT.Rows(0).Item("MTRS"))), "0.00"), DT.Rows(0).Item("BARCODE"), DT.Rows(0).Item("RACK"), DT.Rows(0).Item("SHELF"), DT.Rows(0).Item("BALENO"), "", Val(DT.Rows(0).Item("FROMNO")), Val(DT.Rows(0).Item("FROMSRNO")), DT.Rows(0).Item("TYPE"))
                GRIDREPRINT.FirstDisplayedScrollingRowIndex = GRIDREPRINT.RowCount - 1
                getsrno(GRIDREPRINT)
            Else
                MsgBox("Invalid Bale No", MsgBoxStyle.Critical)
            End If
LINE1:
            TXTBALENO.Clear()
            TXTBALENO.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub CMDPRINTPS_Click(sender As Object, e As EventArgs) Handles CMDPRINTPS.Click
    '    Try
    '        If ClientName = "SNCM" AndAlso MsgBox("Wish to Print Packing Slip", MsgBoxStyle.YesNo) = vbYes Then SERVERPROPSELECTED()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Sub SERVERPROPSELECTED()
    '    Try

    '        If PrintDialog.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PrintDialog.PrinterSettings

    '        For Each ROW As DataGridViewRow In GRIDREPRINT.Rows
    '            '**************** SET SERVER ************************
    '            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
    '            Dim crParameterFieldDefinition As ParameterFieldDefinition
    '            Dim crParameterValues As New ParameterValues
    '            Dim crParameterDiscreteValue As New ParameterDiscreteValue

    '            Dim crtableLogonInfo As New TableLogOnInfo
    '            Dim crConnecttionInfo As New ConnectionInfo
    '            Dim crTables As Tables
    '            Dim crTable As Table

    '            With crConnecttionInfo
    '                .ServerName = SERVERNAME
    '                .DatabaseName = DatabaseName
    '                .UserID = DBUSERNAME
    '                .Password = Dbpassword
    '                .IntegratedSecurity = Dbsecurity
    '            End With

    '            Dim expo As New ExportOptions
    '            Dim oDfDopt As New DiskFileDestinationOptions


    '            Dim OBJ As New Object
    '            If ROW.Cells(GFROMTYPE.Index).Value = "OPENINGGREY" Then
    '                OBJ = New MatRecReport_SNCM
    '                OBJ.RecordSelectionFormula = "{STOCKMASTER_GREY.SMGREY_BALENO}='" & ROW.Cells(GBALENO.Index).Value.ToString & "' AND {STOCKMASTER_GREY.SMGREY_NO}=" & ROW.Cells(GFROMNO.Index).Value & " AND {STOCKMASTER_GREY.SMGREY_GRIDSRNO}=" & ROW.Cells(GFROMSRNO.Index).Value & " AND {STOCKMASTER_GREY.SMGREY_yearid}=" & YearId

    '            ElseIf ROW.Cells(GFROMTYPE.Index).Value = "KNITTING" Then
    '                OBJ = New JobInReport_SNCM
    '                OBJ.RecordSelectionFormula = "{JOBIN_DESC.JI_BALENO}='" & ROW.Cells(GBALENO.Index).Value.ToString & "' AND {JOBIN_DESC.JI_NO}=" & ROW.Cells(GFROMNO.Index).Value & " AND {JOBIN_DESC.JI_GRIDSRNO}=" & ROW.Cells(GFROMSRNO.Index).Value & " and {JOBIN.JI_yearid}=" & YearId

    '            ElseIf ROW.Cells(GFROMTYPE.Index).Value = "OPENING" Then
    '                OBJ = New OpeningReport_SNCM
    '                OBJ.RecordSelectionFormula = "{STOCKMASTER.SM_BALENO}='" & ROW.Cells(GBALENO.Index).Value.ToString & "' AND {STOCKMASTER.SM_NO}=" & ROW.Cells(GFROMNO.Index).Value & " AND {STOCKMASTER.MATREC_GRIDSRNO}=" & ROW.Cells(GFROMSRNO.Index).Value & " AND {MATERIALRECEIPT.MATREC_yearid}=" & YearId
    '            End If

    '            crTables = OBJ.Database.Tables
    '            For Each crTable In crTables
    '                crtableLogonInfo = crTable.LogOnInfo
    '                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
    '                crTable.ApplyLogOnInfo(crtableLogonInfo)
    '            Next


    '            OBJ.PrintOptions.PrinterName = PrintDialog.PrinterSettings.PrinterName
    '            OBJ.PrintToPrinter(Val(txtcopies.Text.Trim), True, 0, 0)


    '        Next

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub txtbarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbarcode.KeyDown
        Try
            If e.KeyCode = Keys.F1 And ALLOWBARCODEPRINT = True And ALLOWPACKINGSLIP = False Then
                If (ClientName = "MAHAVIRPOLYCOT" Or ClientName = "SNCM") And UserName <> "Admin" Then Exit Sub



                Dim OBJSTOCK As New SelectStockGDNGrid
                OBJSTOCK.WHERECLAUSE = OBJSTOCK.WHERECLAUSE
                OBJSTOCK.FRMSTRING = "GREY"
                OBJSTOCK.ShowDialog()
                Dim DTBARCODE As DataTable = OBJSTOCK.DTBARCODE
                For Each DTROW As DataRow In DTBARCODE.Rows
                    txtbarcode.Text = DTROW("BARCODE")
                    txtbarcode_Validated(sender, e)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class