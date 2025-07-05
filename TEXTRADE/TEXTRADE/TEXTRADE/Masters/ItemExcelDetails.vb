
Imports BL

Public Class ItemExcelDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String


    Private Sub LedgerDetailsReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim OBJCMN As New ClsCommonMaster
            Dim DT As DataTable = OBJCMN.search(" ITEMMASTER.item_id AS ITEMID, MATERIALTYPEMASTER.material_name AS MATERIALTYPE, ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, ITEMMASTER.item_name AS ITEMNAME,  ISNULL(ITEMMASTER.item_code, '') AS ITEMCODE, ISNULL(ITEMMASTER.ITEM_BLOCKED, 0) AS BLOCKED, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(DEPARTMENTMASTER.DEPARTMENT_name, '')  AS DEPARTMENT, ITEMMASTER.item_reorder AS REORDER, ITEMMASTER.ITEM_FOLD AS FOLD, ITEMMASTER.ITEM_RATE AS RATE, ITEMMASTER.ITEM_TRANSRATE AS TRANSPORTRATE, ITEMMASTER.ITEM_CHECKRATE AS CHECKINGRATE, ITEMMASTER.ITEM_PACKRATE AS PACKINGRATE, ITEMMASTER.ITEM_DESIGNRATE AS DESIGNRATE, ITEMMASTER.item_upper AS UPPER,  ITEMMASTER.item_lower AS LOWER, ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_GREYWIDTH, '') AS GREYWIDTH, ISNULL(ITEMMASTER.ITEM_SHRINKFROM, 0) AS SHRINKFROM, ISNULL(ITEMMASTER.ITEM_SHRINKTO, 0) AS SHRINKTO, ISNULL(ITEMMASTER.ITEM_SELVEDGE, '') AS SELVEDGE, ISNULL(ITEMMASTER.item_remarks, '') AS REMARKS, ITEMMASTER.ITEM_PHOTO AS IMGPATH, ISNULL(ITEMMASTER.ITEM_WARP, '') AS WARP, ISNULL(ITEMMASTER.ITEM_WEFT, '') AS WEFT, ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS DISPLAYNAME, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(ITEMMASTER.ITEM_BLOCKED, 0) AS BLOCKED, ISNULL(ITEMMASTER.ITEM_RATE, 0) AS RATE, ISNULL(ITEMMASTER.ITEM_VALUATIONRATE, 0) AS VALUATIONRATE , ITEMMASTER.ITEM_CREATED AS CREATEDDATE, ISNULL(HSNMASTER_DESC.HSN_CGST,0) AS CGSTPER, ISNULL(HSNMASTER_DESC.HSN_SGST,0) AS SGSTPER, ISNULL(HSNMASTER_DESC.HSN_IGST,0) AS IGSTPER ", "", " ITEMMASTER INNER JOIN  MATERIALTYPEMASTER ON ITEMMASTER.item_materialtypeid = MATERIALTYPEMASTER.material_id LEFT OUTER JOIN  HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN  UNITMASTER ON ITEMMASTER.item_unitid = UNITMASTER.unit_id LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN DEPARTMENTMASTER ON ITEMMASTER.item_departmentid = DEPARTMENTMASTER.DEPARTMENT_id CROSS APPLY (SELECT TOP 1 * FROM HSNMASTER_DESC WHERE ITEMMASTER.ITEM_HSNCODEID = HSNMASTER_DESC.HSN_ID) AS HSNMASTER_DESC", " AND ITEMMASTER.ITEM_YEARID = " & YearId & " order by ITEMNAME")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then gridbill.FocusedRowHandle = gridbill.RowCount - 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub cmdadd_Click(sender As Object, e As EventArgs) Handles cmdadd.Click
        Try
            showform(False, "")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal ITEMNAME As String)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objaccountsmaster As New ItemMaster
                objaccountsmaster.MdiParent = MDIMain
                objaccountsmaster.EDIT = editval
                objaccountsmaster.tempItemName = ITEMNAME
                objaccountsmaster.frmstring = "MERCHANT"
                objaccountsmaster.Show()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPRINT.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Item Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim PERIOD As String = AccFrom & " - " & AccTo
            opti.SheetName = "Item Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Item Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Item Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub cmdedit_Click(sender As Object, e As EventArgs) Handles cmdedit.Click
        showform(True, gridbill.GetFocusedRowCellValue("ITEMNAME"))
    End Sub

    Private Sub LedgerDetailsReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
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

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("ITEMNAME"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ItemExcelDetails_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "SANGHVI" Then GSELVEDGE.Caption = "Tally Item Name"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class




