
Imports BL

Public Class UnCheckedStock
    Private Sub UnCheckedStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try

            Dim TEMPCONDITION As String = ""
            If ClientName = "YAMUNESH" Or ClientName = "MOMAI" Or ClientName = "AXIS" Then
                TEMPCONDITION = TEMPCONDITION & " AND ROUND(PCS,0) > 0 "
            ElseIf ClientName = "MOHAN" Then
                TEMPCONDITION = TEMPCONDITION & " AND ROUND(MTRS,2) > 0 "
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("CAST(0 AS BIT) AS CHK , BARCODESTOCK.*", "", " BARCODESTOCK ", " AND BARCODESTOCK.BARCODE NOT IN (SELECT BARCODE FROM STOCKTAKING_DESC WHERE YEARID = " & YearId & ") AND BARCODESTOCK.YEARID = " & YearId & TEMPCONDITION)
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UnCheckedStock_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\UnChecked Stock.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "UnChecked Stock"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "UnChecked Stock", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("UnChecked Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CMDADJUST_Click(sender As Object, e As EventArgs) Handles CMDADJUST.Click
        Try
            If UserName <> "Admin" Then MsgBox("Only Admin can Adjust Unchecked Stock", MsgBoxStyle.Critical)
            If gridbill.RowCount > 0 Then
                If MsgBox("Wish To Adjust Unchecked Stock?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub
                Dim BARCODE As String = ""
                Dim count As Integer = 1


                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If BARCODE = "" Then
                            BARCODE = "'" + dtrow("BARCODE") + "'"
                        ElseIf count < 500 Then
                            BARCODE = BARCODE + "," + "'" & dtrow("BARCODE") & "'"
                            count = count + 1
                        End If
                    End If
                Next

                Dim OBJRECO As New StockReco
                OBJRECO.MdiParent = MDIMain
                OBJRECO.UNCHECKEDSTOCK = True
                OBJRECO.BARCODE = BARCODE
                OBJRECO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            If gridbilldetails.Visible = True Then
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREPRINT_Click(sender As Object, e As EventArgs) Handles CMDREPRINT.Click
        Try
            Dim TEMPMSG As Integer = MsgBox("Wish to Print Barcode?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbNo Then Exit Sub

            Dim WHOLESALEBARCODE As Integer = 0
            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then WHOLESALEBARCODE = MsgBox("Wish to Print Wholesale Barcode?", MsgBoxStyle.YesNo)

            Dim TEMPHEADER As String = ""
            If ClientName = "YASHVI" Then
                TEMPHEADER = InputBox("Enter Sticker Type (M/N/Y/B)")
                If TEMPHEADER <> "M" And TEMPHEADER <> "N" And TEMPHEADER <> "Y" And TEMPHEADER <> "B" Then Exit Sub
                If TEMPHEADER = "M" Then TEMPHEADER = "MAFATLAL"
                If TEMPHEADER = "N" Then TEMPHEADER = ""
            End If


            If ClientName = "GELATO" Then
                TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR MRP" & Chr(13) & "3 FOR WSP")
                If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
            End If

            If ClientName = "DAKSH" Or ClientName = "KUNAL" Or ClientName = "VALIANT" Then
                TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED")
                If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
            End If

            If ClientName = "SST" Then
                TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED" & Chr(13) & "3 FOR MRP")
                If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
            End If

            If ClientName = "MANSI" Then
                TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED" & Chr(13) & "3 FOR MRP" & Chr(13) & "4 FOR 100 X 50")
                If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" And TEMPHEADER <> "4" Then Exit Sub
            End If

            If ClientName = "RAJKRIPA" Or ClientName = "MOHATUL" Then
                TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR LUMP" & Chr(13) & "2 FOR CUTPACK")
                If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
            End If

            If ClientName = "MANS" Then
                TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR SALVATROE" & Chr(13) & "2 FOR DONBION" & Chr(13) & "2 FOR OCM")
                If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
            End If

            If ClientName = "AXIS" Then
                TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR PCS" & Chr(13) & "2 FOR MTRS")
                If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
            End If

            If ClientName = "KRISHNA" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "SIMPLEX" Then
                TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR BOX STICKER")
                If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
            End If


            Dim SUPRIYAHEADER As String = ""
            If ClientName = "SUPRIYA" Then
                TEMPHEADER = InputBox("Enter Sticker Type (1/2/3/4/5/6/7)")
                If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" And TEMPHEADER <> "4" And TEMPHEADER <> "5" And TEMPHEADER <> "6" And TEMPHEADER <> "7" Then Exit Sub
                If TEMPHEADER = "1" Or TEMPHEADER = "6" Then SUPRIYAHEADER = "ROYAL TEX"
                If TEMPHEADER = "2" Or TEMPHEADER = "7" Then SUPRIYAHEADER = "DEEP BLUE"
                If TEMPHEADER = "3" Then SUPRIYAHEADER = ""
                If TEMPHEADER = "4" Then SUPRIYAHEADER = "KAMDHENU"
                If TEMPHEADER = "5" Then SUPRIYAHEADER = "5"
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    BARCODEPRINTING(dtrow("BARCODE"), dtrow("PIECETYPE"), dtrow("ITEMNAME"), dtrow("QUALITY"), dtrow("DESIGNNO"), dtrow("COLOR"), dtrow("UNIT"), dtrow("LOTNO"), dtrow("BALENO"), "", dtrow("MTRS"), 1, dtrow("CUT"), dtrow("RACK"), TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, "", "", dtrow("SHELF"))
                End If
            Next

            'For Each ROW As DataGridViewRow In GRIDREPRINT.Rows
            '    If ClientName = "AVIS" And ROW.Cells(GPIECETYPE.Index).Value = "SECOND" Then ROW.Cells(GPIECETYPE.Index).Value = "FRESH"
            '    For I As Integer = 1 To Val(txtcopies.Text.Trim)
            '        BARCODEPRINTING(ROW.Cells(GBARCODE.Index).Value, ROW.Cells(GPIECETYPE.Index).Value, ROW.Cells(GITEMNAME.Index).Value, ROW.Cells(GQUALITY.Index).Value, ROW.Cells(GDESIGN.Index).Value, ROW.Cells(GSHADE.Index).Value, ROW.Cells(GUNIT.Index).Value, ROW.Cells(GLOTNO.Index).Value, ROW.Cells(GBALENO.Index).Value, ROW.Cells(GPRINTDESC.Index).Value, Val(ROW.Cells(GMTRS.Index).Value), 1, Val(ROW.Cells(GCUT.Index).Value), ROW.Cells(GRACK.Index).Value, TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, "", "", ROW.Cells(GSHELF.Index).Value)
            '    Next
            'Next
LINE1:
            'clear()
            'txtbarcode.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class