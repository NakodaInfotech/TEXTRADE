Imports BL
Public Class ChangeBarcodeDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub PROFORMADetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Sub fillgrid()

        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("  CAST(0 AS BIT) AS CHK,  ISNULL(CHANGEBARCODE.CB_NO, 0) AS NO, CHANGEBARCODE.CB_DATE AS DATE, ISNULL(ITEMMASTER_1.item_name, '') AS NEWITEMNAME, ISNULL(QUALITYMASTER_1.QUALITY_name, '') AS NEWQUALITY, ISNULL(DESIGNMASTER_1.DESIGN_NO, '') AS NEWDESIGN, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, ISNULL(PIECETYPEMASTER_1.PIECETYPE_name, '') AS NEWPEICETYPE, ISNULL(COLORMASTER_1.COLOR_name, '') AS NEWSHADE, ISNULL(UNITMASTER_1.unit_name, '') AS NEWUNIT, ISNULL(CHANGEBARCODE.CB_STAMPING, '') AS STAMPING, ISNULL(CHANGEBARCODE.CB_DESCRIPTION, '')  AS DESCRIPTION, ISNULL(RACKMASTER.RACK_NAME, '') AS NEWRACK, ISNULL(CHANGEBARCODE.CB_REMARKS, '') AS REMARKS, ISNULL(CHANGEBARCODE_DESC.CB_GRIDSRNO, 0) AS GRIDSRNO, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PEICETYPE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(CHANGEBARCODE_DESC.CB_GRIDDESC, '') AS GRIDDESC, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNO, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(UNITMASTER.unit_name, '') AS UNIT, ISNULL(CHANGEBARCODE_DESC.CB_LOTNO, '0') AS LOTNO, ISNULL(CHANGEBARCODE_DESC.CB_CUT, 0) AS CUT, ISNULL(CHANGEBARCODE_DESC.CB_MTRS, 0) AS MTRS, ISNULL(RACKMASTER_1.RACK_NAME, '')  AS RACKNAME, ISNULL(CHANGEBARCODE_DESC.CB_BARCODE, '') AS BARCODE, ISNULL(CHANGEBARCODE_DESC.CB_FROMNO, 0) AS FROMNO, ISNULL(CHANGEBARCODE_DESC.CB_FROMSRNO, 0) AS FROMSRNO, ISNULL(CHANGEBARCODE_DESC.CB_FROMTYPE, '') AS FROMTYPE, ISNULL(SHELFMASTER_1.SHELF_NAME, '') AS SHELF  ", "", "    GODOWNMASTER RIGHT OUTER JOIN UNITMASTER AS UNITMASTER_1 RIGHT OUTER JOIN RACKMASTER RIGHT OUTER JOIN DESIGNMASTER RIGHT OUTER JOIN SHELFMASTER AS SHELFMASTER_1 RIGHT OUTER JOIN CHANGEBARCODE_DESC ON SHELFMASTER_1.SHELF_ID = CHANGEBARCODE_DESC.CB_SHELFID LEFT OUTER JOIN RACKMASTER AS RACKMASTER_1 ON CHANGEBARCODE_DESC.CB_RACKID = RACKMASTER_1.RACK_ID LEFT OUTER JOIN UNITMASTER ON CHANGEBARCODE_DESC.CB_UNITID = UNITMASTER.unit_id LEFT OUTER JOIN COLORMASTER ON CHANGEBARCODE_DESC.CB_COLORID = COLORMASTER.COLOR_id ON DESIGNMASTER.DESIGN_id = CHANGEBARCODE_DESC.CB_DESIGNID LEFT OUTER JOIN QUALITYMASTER ON CHANGEBARCODE_DESC.CB_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN ITEMMASTER ON CHANGEBARCODE_DESC.CB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN PIECETYPEMASTER ON CHANGEBARCODE_DESC.CB_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id RIGHT OUTER JOIN CHANGEBARCODE ON CHANGEBARCODE_DESC.CB_NO = CHANGEBARCODE.CB_NO ON RACKMASTER.RACK_ID = CHANGEBARCODE.CB_NEWRACKID ON  UNITMASTER_1.unit_id = CHANGEBARCODE.CB_NEWUNITID LEFT OUTER JOIN COLORMASTER AS COLORMASTER_1 ON CHANGEBARCODE.CB_NEWCOLORID = COLORMASTER_1.COLOR_id LEFT OUTER JOIN PIECETYPEMASTER AS PIECETYPEMASTER_1 ON CHANGEBARCODE.CB_NEWPIECETYPEID = PIECETYPEMASTER_1.PIECETYPE_id ON GODOWNMASTER.GODOWN_id = CHANGEBARCODE.CB_GODOWNID LEFT OUTER JOIN DESIGNMASTER AS DESIGNMASTER_1 ON CHANGEBARCODE.CB_NEWDESIGNID = DESIGNMASTER_1.DESIGN_id LEFT OUTER JOIN QUALITYMASTER AS QUALITYMASTER_1 ON CHANGEBARCODE.CB_NEWQUALITYID = QUALITYMASTER_1.QUALITY_id LEFT OUTER JOIN ITEMMASTER AS ITEMMASTER_1 ON CHANGEBARCODE.CB_NEWITEMID = ITEMMASTER_1.item_id", " and  CHANGEBARCODE.CB_YEARID=" & YearId)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal TEMPGODOWNNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objgdn As New ChangeBarcode
                objgdn.MdiParent = MDIMain
                objgdn.EDIT = editval
                objgdn.TEMPNO = TEMPGODOWNNO
                objgdn.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Change Barcode Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Change Barcode Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Change Barcode Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Change Barcode Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub


    Private Sub ChangeBarcodeDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'CHANGE BARCODE'")
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
End Class