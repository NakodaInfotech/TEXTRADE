

Imports BL
Imports System.Windows.Forms

Public Class RecFromPackingDetails
    Public EDIT As Boolean
    Public TYPE As String
    Dim TEMPPONO As Integer
    Public Where As String = ""
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub IssueToPackingDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                cmdok_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub IssueToPackingDetails_LOAD(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DTROW() As DataRow
        DTROW = USERRIGHTS.Select("FormName = 'JOB OUT'")
        USERADD = DTROW(0).Item(1)
        USEREDIT = DTROW(0).Item(2)
        USERVIEW = DTROW(0).Item(3)
        USERDELETE = DTROW(0).Item(4)

        If USEREDIT = False And USERVIEW = False Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        fillgrid(" AND dbo.RECPACKING.REC_yearid=" & YearId & " order by dbo.RECPACKING.REC_no ")
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            If ClientName = "SVS" Then
                lbl.Text = "For Issue Packing Details"
                Me.Text = "For Issue Packing Details"
            End If

            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, ISNULL(RECPACKING.REC_NO, 0) AS SRNO, ISNULL(RECPACKING.REC_DATE, GETDATE()) AS DATE, ISNULL(RECPACKING.REC_REFNO, '') AS REFNO, ISNULL(RECPACKING.REC_LOTNO, '') AS LOTNO, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(RECPACKING_DESC.REC_QTY, 0) AS PCS, ISNULL(RECPACKING_DESC.REC_MTRS, 0) AS MTRS, ISNULL(RECPACKING.REC_REMARKS, '') AS REMARKS, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(RECPACKING_DESC.REC_BARCODE, '') AS BARCODE, ISNULL(RECPACKING_DESC.REC_MTRS, 0) - ISNULL(RECPACKING_DESC.REC_OUTMTRS, 0) AS BALMTRS, ISNULL(RECPACKING.REC_FROMNO, 0) AS FROMNO, ISNULL(RECPACKING.REC_OUTBARCODE, '') AS OUTBARCODE, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISSUEPACKING.ISS_date AS ISSDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(RECPACKING_DESC.REC_RATE, 0) AS RATE, ISNULL(RECPACKING_DESC.REC_PER, '') AS PER, ISNULL(RECPACKING_DESC.REC_AMOUNT, 0) AS AMOUNT, ISNULL(LEDGERS.ACC_WHATSAPPNO, '') AS PARTYWHATSAAP, ISNULL(LEDGERS.Acc_email, '') AS PARTYEMAIL, ISNULL(CONTRACTMASTER.CONTRACT_NAME, '') AS CONTRACTOR, ISNULL(USERMASTER.User_Name, '') AS USERNAME", "", " RECPACKING INNER JOIN GODOWNMASTER ON RECPACKING.REC_GODOWNID = GODOWNMASTER.GODOWN_id INNER JOIN RECPACKING_DESC ON RECPACKING.REC_NO = RECPACKING_DESC.REC_NO AND RECPACKING.REC_YEARID = RECPACKING_DESC.REC_YEARID INNER JOIN ITEMMASTER ON RECPACKING_DESC.REC_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON RECPACKING_DESC.REC_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id LEFT OUTER JOIN DESIGNMASTER ON RECPACKING_DESC.REC_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON RECPACKING_DESC.REC_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN UNITMASTER ON RECPACKING_DESC.REC_QTYUNITID = UNITMASTER.unit_id INNER JOIN ISSUEPACKING ON RECPACKING.REC_FROMNO = ISSUEPACKING.ISS_no AND RECPACKING.REC_YEARID = ISSUEPACKING.ISS_yearid LEFT OUTER JOIN USERMASTER ON RECPACKING.REC_USERID = USERMASTER.User_id LEFT OUTER JOIN LEDGERS ON RECPACKING.REC_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN CONTRACTMASTER ON ISSUEPACKING.ISS_CONTRACTID = CONTRACTMASTER.CONTRACT_ID", Where & TEMPCONDITION)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal ISSUENO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objREQ As New RecFromPacking
                objREQ.MdiParent = MDIMain
                objREQ.EDIT = editval
                objREQ.TEMPRECNO = ISSUENO
                objREQ.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(sender As Object, e As EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Rec From Packing Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Rec From Packing Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Rec From Packing Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Rec Packing Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub

            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Entry Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Wish to Print Rec Report from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    PRINTREPORT()
                End If
                If MsgBox("Wish to Print Barcode from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    PRINTBARCODE()
                End If
            Else
                If MsgBox("Wish to Print Selected Rec Report?", MsgBoxStyle.YesNo) = vbYes Then
                    PRINTREPORT()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            Dim OBJMATREC As New MATRECDesign
            OBJMATREC.MdiParent = MDIMain
            OBJMATREC.FRMSTRING = "RECPACK"

            If Val(TXTFROM.Text.Trim) > 0 Then
                For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                    If OBJMATREC.WHERECLAUSE = "" Then
                        OBJMATREC.WHERECLAUSE = "({RECPACKING.REC_no}=" & Val(I)
                    Else
                        OBJMATREC.WHERECLAUSE = OBJMATREC.WHERECLAUSE & " OR {RECPACKING.REC_no}=" & Val(I)
                    End If
                Next
            Else
                For I As Integer = 0 To Val(gridbill.RowCount - 1)
                    Dim ROW As DataRow = gridbill.GetDataRow(I)
                    If ROW("CHK") = True Then
                        If OBJMATREC.WHERECLAUSE = "" Then
                            OBJMATREC.WHERECLAUSE = "({RECPACKING.REC_no}=" & Val(ROW("SRNO"))
                        Else
                            OBJMATREC.WHERECLAUSE = OBJMATREC.WHERECLAUSE & " OR {RECPACKING.REC_no}=" & Val(ROW("SRNO"))
                        End If
                    End If
                Next
            End If
            OBJMATREC.WHERECLAUSE = OBJMATREC.WHERECLAUSE & ") AND {RECPACKING.REC_yearid}=" & YearId
            OBJMATREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid(" AND dbo.RECPACKING.REC_yearid=" & YearId & " order by dbo.RECPACKING.REC_no ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECT_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECT.CheckedChanged
        Try
            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                dtrow("CHK") = CHKSELECT.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
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

    Sub PRINTBARCODE()
        Try
            If ALLOWBARCODEPRINT Then

                For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)


                    'GET FRESH DATA FROM DATABASE (ONLY GRID)
                    'THIS IS DONE COZ FOR MULTIUSER THE NOS WILL BE SAME
                    'SO WE WILL ADD BARCODE IN SP AND THEN FETCH THAT DATA HERE AFTER THAT WE WILL PRINT BARCODES
                    GRIDREC.RowCount = 0
                    Dim OBJREC As New ClsRecFromPacking()
                    OBJREC.alParaval.Add(I)
                    OBJREC.alParaval.Add(YearId)
                    Dim dttable As DataTable = OBJREC.SELECTRECPACKING()
                    Dim TEMPLOTNO As String = ""
                    Dim TEMPNAME As String = ""
                    For Each dr As DataRow In dttable.Rows
                        GRIDREC.Rows.Add(Val(dr("GRIDSRNO")), dr("PIECETYPE"), dr("ITEM").ToString, dr("QUALITY").ToString, dr("DESIGN").ToString, dr("GRIDREMARKS").ToString, dr("COLOR"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("qty")), "0.00"), dr("UNIT").ToString, Format(Val(dr("MTRS")), "0.00"), dr("RACK"), dr("SHELF"), dr("BARCODE"), 0, dr("OUTPCS"), dr("OUTMTRS"), Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("FROMTYPE"))
                        TEMPNAME = dr("NAME")
                        TEMPLOTNO = dr("LOTNO")
                        getsrno(GRIDREC)
                    Next


                    Dim WHOLESALEBARCODE As Integer = 0
                    If ClientName = "CC"  Or ClientName = "C3" Or ClientName = "SHREEDEV" Then WHOLESALEBARCODE = MsgBox("Wish to Print Wholesale Barcode?", MsgBoxStyle.YesNo)


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

                    If ClientName = "DAKSH" Or ClientName = "MANSI" Then
                        TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED")
                        If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                    End If

                    If ClientName = "RAJKRIPA" Then
                        TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR LUMP" & Chr(13) & "2 FOR CUTPACK")
                        If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                    End If

                    If ClientName = "MANS" Then
                        TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR SALVATROE" & Chr(13) & "2 FOR DONBION")
                        If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                    End If


                    If ClientName = "KRISHNA" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                        TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR BOX STICKER")
                        If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                    End If


                    If ClientName = "SOFTAS" And CHKPRINTSERIES.Checked = True Then
                        TEMPHEADER = "PRINTSERIES"
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

                    For Each ROW As DataGridViewRow In GRIDREC.Rows
                        BARCODEPRINTING(ROW.Cells(GBARCODE.Index).Value, ROW.Cells(GPIECETYPE.Index).Value, ROW.Cells(GITEMNAME.Index).Value, ROW.Cells(GQUALITY.Index).Value, ROW.Cells(GDESIGN.Index).Value, ROW.Cells(GCOLOR.Index).Value, ROW.Cells(gqtyunit.Index).Value, TEMPLOTNO, "", ROW.Cells(gdesc.Index).Value, Val(ROW.Cells(GMTRS.Index).Value), Val(ROW.Cells(gQty.Index).Value), Val(ROW.Cells(gcut.Index).Value), ROW.Cells(GRACK.Index).Value, TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, "", TEMPNAME)
                    Next

                Next

            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RecFromPackingDetails_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "SOFTAS" Then
                CHKPRINTSERIES.Visible = True
                If CmpName = "SIDDHIM COTFAB LLP" Then CHKPRINTSERIES.CheckState = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class