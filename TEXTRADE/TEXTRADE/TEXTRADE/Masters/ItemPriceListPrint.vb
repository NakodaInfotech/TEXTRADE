
Imports BL

Public Class ItemPriceListPrint

    Public WHERECLAUSE As String = ""

    Private Sub ItemPriceListPrint_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If ClientName = "YASHVI" Then
                TXTHEADER.Text = "NATURAL FABRICS PRICE LIST EFFECTIVE FROM " & Format(Now.Date, "dd/MM/yyyy")
                TXTHEADERDESC.Text = "Made from mainly Natural Fibre as Linen. Cotton, Bamboo, Ramie, Tancel etc for better Ventilation and High comfort. Skin Friendly and Versatile fabrics."
            ElseIf ClientName = "MAHAVIRPOLYCOT" Then
                TXTHEADER.Text = "ANKIT R. SHAH - 09324145825 - 09773414467 - 022/40115001 " & vbCrLf & " PRICE LIST AS ON " & Format(Now.Date, "dd/MM/yyyy")
                TXTREMARKS.Text = "TERMS & CONDITIONS :- 
*    RATES ARE SUBJECT TO MARKET FLUCTUATION.
*    ALL RATES ARE NET RATE ONLY.
*    NO DEDUCTION IN FINAL BILL AMOUNT.
*    CASH DISCOUNT 3 %, IF PAYMENT MADE WITHIN 30-45 DAYS ONLY.
*    WHITENESS MAY DIFFER FROM LOT TO LOT.
*    RESIDUAL SHRINKAGE OF 2% WILL BE APPLICABLE ON ALL PRODUCTS.
*    NO RATE DIFFERENCE ON ANY PRODUCT.
*    GOODS ONCE ORDERED WONT BE CANCELLED.
*    STRICTLY NO RETURN GOODS."
            End If
            fillcmb()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDPRINT_Click(sender As Object, e As EventArgs) Handles CMDPRINT.Click
        Try
            If CMBPRICELIST.Text.Trim = "" And ClientName <> "MAHAVIRPOLYCOT" Then
                MsgBox("Select Price List to be Printed", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If ClientName = "MAHAVIRPOLYCOT" Then
                'FOR CATEGORY
                Dim ITEMCLAUSE As String = ""
                GRIDITEM.ClearColumnsFilter()
                For i As Integer = 0 To GRIDITEM.RowCount - 1
                    Dim dtrow As DataRow = GRIDITEM.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then
                        If ITEMCLAUSE = "" Then
                            ITEMCLAUSE = " AND ({CATEGORYMASTER.CATEGORY_NAME} = '" & dtrow("CATEGORY") & "'"
                        Else
                            ITEMCLAUSE = ITEMCLAUSE & " OR {CATEGORYMASTER.CATEGORY_NAME} = '" & dtrow("CATEGORY") & "'"
                        End If
                    End If
                Next
                If ITEMCLAUSE <> "" Then
                    ITEMCLAUSE = ITEMCLAUSE & ")"
                    WHERECLAUSE = "{ITEMMASTER.ITEM_BLOCKED} = FALSE AND {ITEMMASTER.ITEM_YEARID} = " & YearId & ITEMCLAUSE
                End If
            End If


            Me.Close()

            Dim OBJPRICELIST As New SaleInvoiceDesign
            OBJPRICELIST.MdiParent = MDIMain
            OBJPRICELIST.FRMSTRING = "PRICELIST"
            OBJPRICELIST.PERIOD = TXTHEADER.Text.Trim
            OBJPRICELIST.LESSPER = Val(TXTLESSPER.Text.Trim)
            OBJPRICELIST.WHERECLAUSE = WHERECLAUSE
            OBJPRICELIST.SELECTEDRATE = CMBPRICELIST.Text.Trim
            OBJPRICELIST.PLDESC = TXTHEADERDESC.Text.Trim
            OBJPRICELIST.PLREMARKS = TXTREMARKS.Text.Trim
            OBJPRICELIST.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("DISTINCT CAST (0 AS BIT) AS CHK, CATEGORYMASTER.CATEGORY_NAME AS CATEGORY ", " ", " CATEGORYMASTER ", " ORDER BY CATEGORYMASTER.CATEGORY_NAME")
            GRIDITEMDETAILS.DataSource = DT
            If DT.Rows.Count > 0 Then
                GRIDITEM.FocusedRowHandle = GRIDITEM.RowCount - 1
                GRIDITEM.TopRowIndex = GRIDITEM.RowCount - 15
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTCATEGORY_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTCATEGORY.CheckedChanged
        Try
            For i As Integer = 0 To GRIDITEM.RowCount - 1
                Dim dtrow As DataRow = GRIDITEM.GetDataRow(i)
                dtrow("CHK") = CHKSELECTCATEGORY.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ItemPriceListPrint_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "MAHAVIRPOLYCOT" Then
                LBLLESSPER.Visible = True
                TXTLESSPER.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLESSPER_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTLESSPER.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class