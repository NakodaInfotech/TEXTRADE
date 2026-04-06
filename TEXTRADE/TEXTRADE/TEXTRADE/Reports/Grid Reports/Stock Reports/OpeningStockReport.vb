
Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class OpeningStockReport

    Public EDIT As Boolean
    Dim temppreqno As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Opening_Stock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Control = True Then
                showform(False, cmbtype)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Opening_Stock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal cmbtype As ComboBox)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then

                Dim OBJ As New Object
                If FRMSTRING = "GREY" Then
                    OBJ = New OpeningStockGrey
                Else
                    OBJ = New OpeningStock
                End If
                OBJ.FRMSTRING = cmbtype.Text
                OBJ.MdiParent = MDIMain
                OBJ.edit = editval
                OBJ.Show()
                Me.Close()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gridbill.RowStyle
        Try
            If e.RowHandle >= 0 Then
                Dim View As GridView = sender
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("DONE")) = "Checked" Then
                    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                    e.Appearance.BackColor = Color.Yellow
                End If
                'If View.GetRowCellDisplayText(e.RowHandle, View.Columns("OUTPCS")) > 0 Or View.GetRowCellDisplayText(e.RowHandle, View.Columns("OUTMTRS")) > 0 Then
                '    e.Appearance.Font = New System.Drawing.Font("CALIBRI", 9.0F, System.Drawing.FontStyle.Bold)
                '    e.Appearance.BackColor = Color.Yellow
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try

            If cmbtype.Text <> "" Then
                Dim WHERECLAUSE As String = ""
                Dim OBJCMN As New ClsCommonMaster
                Dim DT As DataTable

                If FRMSTRING = "GREY" Then
                    Me.Text = "Opening Stock Report - Grey"
                    WHERECLAUSE = WHERECLAUSE & " And STOCKMASTER_GREY.SMGREY_TYPE ='" & cmbtype.Text & "' and dbo.STOCKMASTER_GREY.SMGREY_yearid=" & YearId & " order by dbo.STOCKMASTER_GREY.SMGREY_NO "
                    DT = OBJCMN.search(" STOCKMASTER_GREY.SMGREY_GRIDSRNO AS SRNO, STOCKMASTER_GREY.SMGREY_BARCODE AS BARCODE, STOCKMASTER_GREY.SMGREY_DATE AS DATE, ISNULL(STOCKMASTER_GREY.SMGREY_LOTNO, '') AS LOT, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(PROCESSMASTER.PROCESS_NAME, '') AS PROCESS, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERSTO.Acc_cmpname, '') AS TONAME, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(STOCKMASTER_GREY.SMGREY_CUT, 0) AS CUT, ISNULL(STOCKMASTER_GREY.SMGREY_WT, 0) AS WT, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(STOCKMASTER_GREY.SMGREY_PCS, 0) AS PCS, ISNULL(STOCKMASTER_GREY.SMGREY_MTRS, 0) AS MTRS, ISNULL(STOCKMASTER_GREY.SMGREY_OUTPCS, 0) AS OUTPCS, ISNULL(STOCKMASTER_GREY.SMGREY_OUTMTRS, 0) AS OUTMTRS, CAST(STOCKMASTER_GREY.SMGREY_DONE AS BIT) AS DONE, ISNULL(STOCKMASTER_GREY.SMGREY_RATE, 0) AS RATE, ISNULL(STOCKMASTER_GREY.SMGREY_BILLNO, 0) AS BILLNO, ISNULL(STOCKMASTER_GREY.SMGREY_PER, '') AS PER, ISNULL(STOCKMASTER_GREY.SMGREY_AMOUNT, 0) AS AMOUNT, ISNULL(STOCKMASTER_GREY.SMGREY_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(STOCKMASTER_GREY.SMGREY_BALENO, '') AS BALENO, ISNULL(STOCKMASTER_GREY.SMGREY_PARTYCHALLANNO, '') AS PARTYCHALLANNO, ISNULL(STOCKMASTER_GREY.SMGREY_DYEINGJOB, '') AS DYEINGJOB, ISNULL(SHELFMASTER.SHELF_NAME, '') AS SHELF, ISNULL(RACKMASTER.RACK_NAME, '') AS RACK, ISNULL(STOCKMASTER_GREY.SMGREY_LOTCOMPLETED, 0) AS LOTCOMPLETED, STOCKMASTER_GREY.SMGREY_REMARKS AS REMARKS ", "", "  STOCKMASTER_GREY LEFT OUTER JOIN RACKMASTER ON STOCKMASTER_GREY.SMGREY_RACKID = RACKMASTER.RACK_ID LEFT OUTER JOIN SHELFMASTER ON STOCKMASTER_GREY.SMGREY_SHELFID = SHELFMASTER.SHELF_ID LEFT OUTER JOIN LEDGERS ON STOCKMASTER_GREY.SMGREY_LEDGERID = LEDGERS.Acc_id AND STOCKMASTER_GREY.SMGREY_CMPID = LEDGERS.Acc_cmpid AND STOCKMASTER_GREY.SMGREY_LOCATIONID = LEDGERS.Acc_locationid AND STOCKMASTER_GREY.SMGREY_YEARID = LEDGERS.Acc_yearid LEFT OUTER JOIN UNITMASTER ON STOCKMASTER_GREY.SMGREY_YEARID = UNITMASTER.unit_yearid AND STOCKMASTER_GREY.SMGREY_LOCATIONID = UNITMASTER.unit_locationid AND STOCKMASTER_GREY.SMGREY_CMPID = UNITMASTER.unit_cmpid AND STOCKMASTER_GREY.SMGREY_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN GODOWNMASTER ON STOCKMASTER_GREY.SMGREY_YEARID = GODOWNMASTER.GODOWN_yearid AND STOCKMASTER_GREY.SMGREY_LOCATIONID = GODOWNMASTER.GODOWN_locationid AND STOCKMASTER_GREY.SMGREY_CMPID = GODOWNMASTER.GODOWN_cmpid AND STOCKMASTER_GREY.SMGREY_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN LEDGERS AS LEDGERSTO ON STOCKMASTER_GREY.SMGREY_YEARID = LEDGERSTO.Acc_yearid AND STOCKMASTER_GREY.SMGREY_LOCATIONID = LEDGERSTO.Acc_locationid AND STOCKMASTER_GREY.SMGREY_CMPID = LEDGERSTO.Acc_cmpid AND STOCKMASTER_GREY.SMGREY_LEDGERIDTO = LEDGERSTO.Acc_id LEFT OUTER JOIN PROCESSMASTER ON STOCKMASTER_GREY.SMGREY_YEARID = PROCESSMASTER.PROCESS_YEARID AND STOCKMASTER_GREY.SMGREY_LOCATIONID = PROCESSMASTER.PROCESS_LOCATIONID AND STOCKMASTER_GREY.SMGREY_CMPID = PROCESSMASTER.PROCESS_CMPID AND STOCKMASTER_GREY.SMGREY_PROCESSID = PROCESSMASTER.PROCESS_ID LEFT OUTER JOIN COLORMASTER ON STOCKMASTER_GREY.SMGREY_YEARID = COLORMASTER.COLOR_yearid AND STOCKMASTER_GREY.SMGREY_LOCATIONID = COLORMASTER.COLOR_locationid AND STOCKMASTER_GREY.SMGREY_CMPID = COLORMASTER.COLOR_cmpid AND STOCKMASTER_GREY.SMGREY_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON STOCKMASTER_GREY.SMGREY_YEARID = DESIGNMASTER.DESIGN_yearid AND STOCKMASTER_GREY.SMGREY_LOCATIONID = DESIGNMASTER.DESIGN_locationid AND STOCKMASTER_GREY.SMGREY_CMPID = DESIGNMASTER.DESIGN_cmpid AND STOCKMASTER_GREY.SMGREY_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON STOCKMASTER_GREY.SMGREY_YEARID = QUALITYMASTER.QUALITY_yearid AND STOCKMASTER_GREY.SMGREY_LOCATIONID = QUALITYMASTER.QUALITY_locationid AND STOCKMASTER_GREY.SMGREY_CMPID = QUALITYMASTER.QUALITY_cmpid AND STOCKMASTER_GREY.SMGREY_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON STOCKMASTER_GREY.SMGREY_YEARID = ITEMMASTER.item_yearid AND STOCKMASTER_GREY.SMGREY_LOCATIONID = ITEMMASTER.item_locationid AND STOCKMASTER_GREY.SMGREY_CMPID = ITEMMASTER.item_cmpid AND STOCKMASTER_GREY.SMGREY_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON STOCKMASTER_GREY.SMGREY_YEARID = PIECETYPEMASTER.PIECETYPE_yearid AND STOCKMASTER_GREY.SMGREY_LOCATIONID = PIECETYPEMASTER.PIECETYPE_locationid AND STOCKMASTER_GREY.SMGREY_CMPID = PIECETYPEMASTER.PIECETYPE_cmpid AND STOCKMASTER_GREY.SMGREY_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id", WHERECLAUSE)
                Else
                    Me.Text = "Opening Stock Report - Finish"
                    WHERECLAUSE = WHERECLAUSE & " and STOCKMASTER.SM_type='" & cmbtype.Text & "' and dbo.STOCKMASTER.SM_yearid=" & YearId & " order by dbo.STOCKMASTER.SM_no "
                    DT = OBJCMN.search(" STOCKMASTER.SM_GRIDSRNO AS SRNO, STOCKMASTER.SM_BARCODE AS BARCODE, STOCKMASTER.SM_DATE AS DATE, ISNULL(STOCKMASTER.SM_LOTNO, '') AS LOT, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(PROCESSMASTER.PROCESS_NAME, '') AS PROCESS, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERSTO.Acc_cmpname, '') AS TONAME, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(STOCKMASTER.SM_CUT, 0) AS CUT, ISNULL(STOCKMASTER.SM_WT, 0) AS WT, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(STOCKMASTER.SM_PCS, 0) AS PCS, ISNULL(STOCKMASTER.SM_MTRS, 0) AS MTRS, ISNULL(STOCKMASTER.SM_OUTPCS, 0) AS OUTPCS, ISNULL(STOCKMASTER.SM_OUTMTRS, 0) AS OUTMTRS, CAST(STOCKMASTER.SM_DONE AS BIT) AS DONE, ISNULL(STOCKMASTER.SM_RATE, 0) AS RATE, ISNULL(STOCKMASTER.SM_BILLNO, 0) AS BILLNO, ISNULL(STOCKMASTER.SM_PER, '') AS PER, ISNULL(STOCKMASTER.SM_AMOUNT, 0) AS AMOUNT, ISNULL(STOCKMASTER.SM_GRIDREMARKS, '') AS GRIDREMARKS, ISNULL(STOCKMASTER.SM_BALENO, '') AS BALENO, ISNULL(STOCKMASTER.SM_PARTYCHALLANNO, '') AS PARTYCHALLANNO, ISNULL(STOCKMASTER.SM_DYEINGJOB, '') AS DYEINGJOB, ISNULL(SHELFMASTER.SHELF_NAME, '') AS SHELF, ISNULL(RACKMASTER.RACK_NAME, '') AS RACK, ISNULL(STOCKMASTER.SM_LOTCOMPLETED, 0) AS LOTCOMPLETED, STOCKMASTER.SM_REMARKS AS REMARKS ", "", "  STOCKMASTER LEFT OUTER JOIN RACKMASTER ON STOCKMASTER.SM_RACKID = RACKMASTER.RACK_ID LEFT OUTER JOIN SHELFMASTER ON STOCKMASTER.SM_SHELFID = SHELFMASTER.SHELF_ID LEFT OUTER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERID = LEDGERS.Acc_id AND STOCKMASTER.SM_CMPID = LEDGERS.Acc_cmpid AND STOCKMASTER.SM_LOCATIONID = LEDGERS.Acc_locationid AND STOCKMASTER.SM_YEARID = LEDGERS.Acc_yearid LEFT OUTER JOIN UNITMASTER ON STOCKMASTER.SM_YEARID = UNITMASTER.unit_yearid AND STOCKMASTER.SM_LOCATIONID = UNITMASTER.unit_locationid AND STOCKMASTER.SM_CMPID = UNITMASTER.unit_cmpid AND STOCKMASTER.SM_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN GODOWNMASTER ON STOCKMASTER.SM_YEARID = GODOWNMASTER.GODOWN_yearid AND STOCKMASTER.SM_LOCATIONID = GODOWNMASTER.GODOWN_locationid AND STOCKMASTER.SM_CMPID = GODOWNMASTER.GODOWN_cmpid AND STOCKMASTER.SM_GODOWNID = GODOWNMASTER.GODOWN_id LEFT OUTER JOIN LEDGERS AS LEDGERSTO ON STOCKMASTER.SM_YEARID = LEDGERSTO.Acc_yearid AND STOCKMASTER.SM_LOCATIONID = LEDGERSTO.Acc_locationid AND STOCKMASTER.SM_CMPID = LEDGERSTO.Acc_cmpid AND STOCKMASTER.SM_LEDGERIDTO = LEDGERSTO.Acc_id LEFT OUTER JOIN PROCESSMASTER ON STOCKMASTER.SM_YEARID = PROCESSMASTER.PROCESS_YEARID AND STOCKMASTER.SM_LOCATIONID = PROCESSMASTER.PROCESS_LOCATIONID AND STOCKMASTER.SM_CMPID = PROCESSMASTER.PROCESS_CMPID AND STOCKMASTER.SM_PROCESSID = PROCESSMASTER.PROCESS_ID LEFT OUTER JOIN COLORMASTER ON STOCKMASTER.SM_YEARID = COLORMASTER.COLOR_yearid AND STOCKMASTER.SM_LOCATIONID = COLORMASTER.COLOR_locationid AND STOCKMASTER.SM_CMPID = COLORMASTER.COLOR_cmpid AND STOCKMASTER.SM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON STOCKMASTER.SM_YEARID = DESIGNMASTER.DESIGN_yearid AND STOCKMASTER.SM_LOCATIONID = DESIGNMASTER.DESIGN_locationid AND STOCKMASTER.SM_CMPID = DESIGNMASTER.DESIGN_cmpid AND STOCKMASTER.SM_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON STOCKMASTER.SM_YEARID = QUALITYMASTER.QUALITY_yearid AND STOCKMASTER.SM_LOCATIONID = QUALITYMASTER.QUALITY_locationid AND STOCKMASTER.SM_CMPID = QUALITYMASTER.QUALITY_cmpid AND STOCKMASTER.SM_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON STOCKMASTER.SM_YEARID = ITEMMASTER.item_yearid AND STOCKMASTER.SM_LOCATIONID = ITEMMASTER.item_locationid AND STOCKMASTER.SM_CMPID = ITEMMASTER.item_cmpid AND STOCKMASTER.SM_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON STOCKMASTER.SM_YEARID = PIECETYPEMASTER.PIECETYPE_yearid AND STOCKMASTER.SM_LOCATIONID = PIECETYPEMASTER.PIECETYPE_locationid AND STOCKMASTER.SM_CMPID = PIECETYPEMASTER.PIECETYPE_cmpid AND STOCKMASTER.SM_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id", WHERECLAUSE)
                End If

                gridbilldetails.DataSource = DT

                If DT.Rows.Count > 0 Then
                    gridbill.FocusedRowHandle = gridbill.RowCount - 1
                    gridbill.TopRowIndex = gridbill.RowCount - 15
                End If

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
            showform(False, cmbtype)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        showform(False, cmbtype)
    End Sub

    Private Sub cmbtype_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtype.Validating
        Try
            If cmbtype.Text.Trim = "INHOUSE" Then
                If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    GLOTNO.Visible = False
                    GPIECETYPE.Visible = False
                    GITEM.Visible = False
                    GCOLOR.Visible = False
                    GTONAME.Visible = False
                    GPCS.Visible = True
                End If
                If ClientName = "SAKARIA" Then
                    GLOTNO.Visible = True
                    GLOTNO.Caption = "Bale No"
                    GPIECETYPE.Visible = False
                    GNAME.Visible = True
                    GNAME.VisibleIndex = 7
                    GPCS.Visible = True
                    GPCS.VisibleIndex = 10
                End If
                gridbill.Columns("GODOWN").Visible = True
                gridbill.Columns("BARCODE").Visible = True
            Else
                gridbill.Columns("GODOWN").Visible = False
                gridbill.Columns("PCS").Visible = True
                gridbill.Columns("PCS").VisibleIndex = gridbill.Columns("MTRS").VisibleIndex - 1

            End If
            FILLGRID()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Opening Stock Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Opening Stock Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Opening Stock Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Opening Stock Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

End Class