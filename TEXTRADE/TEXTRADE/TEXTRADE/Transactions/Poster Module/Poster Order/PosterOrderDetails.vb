Imports BL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DevExpress.XtraGrid.Views.Grid

Public Class PosterOrderDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public TEMPPONO As String
    Dim PARTYPOSTERORDERREPORT As Boolean = False

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub PosterOrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, 0)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
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

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            fillgrid(" and dbo.POSTERORDER.POSTER_yearid=" & YearId & " order by dbo.POSTERORDER.POSTER_no, POSTERORDER_DESC.POSTER_GRIDSRNO ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLEXCEL_Click(sender As Object, e As EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Poster Order Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Poster Order Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Poster Order Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Poster Order Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub


    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub PRINTTOOLSTRIPBUTTON_CLICK()
        Try
            If MsgBox("wish to Print Reprot?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJPO As New PosterOrderDesign
            OBJPO.FRMSTRING = "POSTER"
            OBJPO.WHERECLAUSE = "{POSTERORDER.POSTER_NO} = " & Val(TEMPPONO) & " AND {POSTERORDER.POSTER_YEARID} = " & YearId
            OBJPO.MdiParent = MDIMain
            OBJPO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal SOno As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objPO As New PosterOrder
                objPO.MdiParent = MDIMain
                objPO.EDIT = editval
                objPO.TEMPPONO = SOno
                objPO.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PosterOrderDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'POSTER ORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid(" and dbo.POSTERORDER.POSTER_yearid=" & YearId & " order by dbo.POSTERORDER.POSTER_no, POSTERORDER_DESC.POSTER_GRIDSRNO ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("  CAST(0 AS BIT) AS CHK, ISNULL(POSTERORDER.POSTER_no, 0) AS TEMPPONO, POSTERORDER.POSTER_date AS DATE, ISNULL(POSTERORDER.POSTER_DELIVERAT, '') AS DELIVERYAT, ISNULL(POSTERORDER.POSTER_COL1, '') AS COL1, ISNULL(POSTERORDER.POSTER_COL2, '') AS COL2, ISNULL(POSTERORDER.POSTER_COL3, '') AS COL3, ISNULL(POSTERORDER.POSTER_COL4, '') AS COL4, ISNULL(POSTERORDER.POSTER_COL5, '') AS COL5,  ISNULL(POSTERORDER.POSTER_TOTAL12X18, 0) AS TOTAL12X18, ISNULL(POSTERORDER.POSTER_TOTAL12X24, 0) AS TOTAL12X24, ISNULL(POSTERORDER.POSTER_TOTAL8X10, 0) AS TOTAL8X10, ISNULL(POSTERORDER.POSTER_TOTAL6X8, 0) AS TOTAL6X8, ISNULL(POSTERORDER.POSTER_TOTAL4X6, 0) AS TOTAL4X6, ISNULL(POSTERORDER.POSTER_TOTALCOL1, 0) AS TOTALCOL1,  ISNULL(POSTERORDER.POSTER_TOTALCOL2, 0) AS TOTALCOL2, ISNULL(POSTERORDER.POSTER_TOTALCOL3, 0) AS TOTALCOL3, ISNULL(POSTERORDER.POSTER_TOTALCOL4, 0) AS TOTALCOL4, ISNULL(POSTERORDER.POSTER_TOTALCOL5, 0) AS TOTALCOL5, ISNULL(POSTERORDER.POSTER_REMARKS, '') AS REMARKS, ISNULL(JOBBERLEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.Acc_cmpname, '')  AS PARTYNAME, ISNULL(POSTERORDER_DESC.POSTER_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(POSTERORDER_DESC.POSTER_DESC, '') AS [DESC], ISNULL(POSTERORDER_DESC.POSTER_D12X18, 0) AS D12X18, ISNULL(POSTERORDER_DESC.POSTER_D12X24, 0) AS D12X24, ISNULL(POSTERORDER_DESC.POSTER_D8X10, 0) AS D8X10, ISNULL(POSTERORDER_DESC.POSTER_D6X8, 0) AS D6X8,  ISNULL(POSTERORDER_DESC.POSTER_D4X6, 0) AS D4X6, ISNULL(POSTERORDER_DESC.POSTER_COLUMN1, 0) AS COLUMN1, ISNULL(POSTERORDER_DESC.POSTER_COLUMN2, 0) AS COLUMN2, ISNULL(POSTERORDER_DESC.POSTER_COLUMN3, 0) AS COLUMN3, ISNULL(POSTERORDER_DESC.POSTER_COLUMN4, 0) AS COLUMN4, ISNULL(POSTERORDER_DESC.POSTER_COLUMN5, 0) AS COLUMN5,  ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR    ", "", " LEDGERS AS JOBBERLEDGERS RIGHT OUTER JOIN LEDGERS RIGHT OUTER JOIN DESIGNMASTER RIGHT OUTER JOIN POSTERORDER LEFT OUTER JOIN POSTERORDER_DESC ON POSTERORDER.POSTER_YEARID = POSTERORDER_DESC.POSTER_YEARID AND POSTERORDER.POSTER_no = POSTERORDER_DESC.POSTER_NO LEFT OUTER JOIN COLORMASTER ON POSTERORDER_DESC.POSTER_COLORID = COLORMASTER.COLOR_id ON DESIGNMASTER.DESIGN_id = POSTERORDER_DESC.POSTER_DESIGNID LEFT OUTER JOIN ITEMMASTER ON POSTERORDER_DESC.POSTER_ITEMID = ITEMMASTER.item_id ON LEDGERS.Acc_id = POSTERORDER.POSTER_LEDGERid ON JOBBERLEDGERS.Acc_id = POSTERORDER.POSTER_JOBBERid ", TEMPCONDITION)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click_1(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Poster Nos", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                If MsgBox("Wish to Print Poster from " & TXTFROM.Text.Trim & " To " & TXTTO.Text.Trim & " ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPDIRECT()
                End If
            Else
                If MsgBox("Wish to Print Selected Poster Note ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLMAIL_Click(sender As Object, e As EventArgs) Handles TOOLMAIL.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Poster Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Mail Poster from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(True)
                End If
            Else
                If MsgBox("Wish to Mail Selected Poster ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If (Val(TXTFROM.Text.Trim) = 0 Or Val(TXTTO.Text.Trim) = 0 Or Val(TXTCOPIES.Text.Trim) = 0) AndAlso gridbill.SelectedRowsCount = 0 Then Exit Sub
            'IF WE HAVE SELECTED FROM AND TO THEN WORK WITH THE CURRENT CODE ELSE GO FOR SELECTED ENTRIES CODE
            If Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0 Then
                If Val(TXTFROM.Text.Trim) > Val(TXTTO.Text.Trim) Then
                    MsgBox("Enter Proper Poster Nos", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    If MsgBox("Wish to Whatsapp Poster from " & Val(TXTFROM.Text.Trim) & " To " & Val(TXTTO.Text.Trim) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                    SERVERPROPDIRECT(False, True)
                End If
            Else
                If MsgBox("Wish to Whatsapp Selected Poster ?", MsgBoxStyle.YesNo) = vbYes Then
                    SERVERPROPSELECTED(False, True)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub SERVERPROPDIRECT(Optional ByVal POSTERMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try
            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList
            If POSTERMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            For I As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)
                Dim OBJJV As New PosterOrderDesign
                OBJJV.MdiParent = MDIMain
                OBJJV.DIRECTPRINT = True
                OBJJV.FRMSTRING = "POSTER"
                OBJJV.DIRECTMAIL = POSTERMAIL
                OBJJV.DIRECTWHATSAPP = WHATSAPP
                'OBJJV.REGNAME = cmbregister.Text.Trim
                OBJJV.PRINTSETTING = PRINTDIALOG
                OBJJV.BILLNO = Val(I)
                OBJJV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                OBJJV.STRSEARCH = "{POSTERORDER.POSTER_NO} = " & Val(I) & " AND {POSTERORDER.POSTER_YEARID} = " & YearId


                'If ClientName = "NAKODAINFOTECH" Then
                '    'PASSING DR NAME AS PARTNAME
                '    Dim OBJCMN As New ClsCommon
                '    Dim DTLEDGER As DataTable = OBJCMN.SEARCH("TOP 1 LEDGERS.ACC_CMPNAME AS PARTYNAME", "", " JOURNALMASTER INNER JOIN LEDGERS ON JOURNAL_LEDGERID = LEDGERS.ACC_ID ", " AND JOURNAL_NO = " & Val(I) & " AND JOURNAL_TYPE = 'Dr' AND JOURNAL_YEARID = " & YearId)
                '    If DTLEDGER.Rows.Count > 0 Then OBJJV.PARTYNAME = DTLEDGER.Rows(0).Item("PARTYNAME")
                'End If


                OBJJV.Show()
                OBJJV.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\POSTERORDER_" & I & ".pdf")
                FILENAME.Add("POSTERORDER_" & I & ".pdf")
            Next

            If POSTERMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "POSTER ORDER"
                OBJMAIL.ShowDialog()
            End If

            If WHATSAPP = True Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SERVERPROPSELECTED(Optional ByVal POSTERMAIL As Boolean = False, Optional ByVal WHATSAPP As Boolean = False)
        Try

            Dim ALATTACHMENT As New ArrayList
            Dim FILENAME As New ArrayList

            If POSTERMAIL = False And WHATSAPP = False Then
                If PRINTDIALOG.ShowDialog = DialogResult.OK Then PRINTDOC.PrinterSettings = PRINTDIALOG.PrinterSettings Else Exit Sub
            End If
            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(gridbill.RowCount - 1)
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If ROW("CHK") = True Then

                    Dim OBJJV As New PosterOrderDesign
                    OBJJV.MdiParent = MDIMain
                    OBJJV.DIRECTPRINT = True
                    OBJJV.FRMSTRING = "POSTER"
                    OBJJV.DIRECTMAIL = POSTERMAIL
                    OBJJV.DIRECTWHATSAPP = WHATSAPP
                    'OBJJV.REGNAME = cmbregister.Text.Trim
                    OBJJV.PRINTSETTING = PRINTDIALOG
                    OBJJV.BILLNO = Val(ROW("TEMPPONO"))
                    OBJJV.NOOFCOPIES = Val(TXTCOPIES.Text.Trim)
                    OBJJV.STRSEARCH = "{POSTERORDER.POSTER_NO} = " & Val(ROW("TEMPPONO")) & " AND {POSTERORDER.POSTER_YEARID} = " & YearId

                    'If ClientName = "NAKODAINFOTECH" Then
                    '    'PASSING DR NAME AS PARTNAME
                    '    Dim OBJCMN As New ClsCommon
                    '    Dim DTLEDGER As DataTable = OBJCMN.SEARCH("TOP 1 LEDGERS.ACC_CMPNAME AS PARTYNAME", "", " JOURNALMASTER INNER JOIN LEDGERS ON JOURNAL_LEDGERID = LEDGERS.ACC_ID ", " AND JOURNAL_NO = " & Val(ROW("SRNO")) & " AND JOURNAL_TYPE = 'Dr' AND JOURNAL_YEARID = " & YearId)
                    '    If DTLEDGER.Rows.Count > 0 Then OBJJV.PARTYNAME = DTLEDGER.Rows(0).Item("PARTYNAME")
                    'End If


                    OBJJV.Show()
                    OBJJV.Close()
                    ALATTACHMENT.Add(Application.StartupPath & "\POSTERORDER_" & Val(ROW("TEMPPONO")) & ".pdf")
                    FILENAME.Add("POSTERORDER_" & Val(ROW("TEMPPONO")) & ".pdf")
                End If
            Next

            If POSTERMAIL Then
                Dim OBJMAIL As New SendMail
                OBJMAIL.ALATTACHMENT = ALATTACHMENT
                OBJMAIL.subject = "POSTER ORDER"
                OBJMAIL.ShowDialog()
            End If

            If WHATSAPP = True Then
                Dim OBJWHATSAPP As New SendWhatsapp
                OBJWHATSAPP.PATH = ALATTACHMENT
                OBJWHATSAPP.FILENAME = FILENAME
                OBJWHATSAPP.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(sender As Object, e As EventArgs) Handles gridbilldetails.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("TEMPPONO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class