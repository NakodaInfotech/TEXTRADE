Imports System.ComponentModel
Imports BL
Imports DevExpress.XtraReports.UI

Public Class ClientDetails

    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Sub getsrno()
        Try
            For I As Integer = 0 To gridbill.RowCount - 1
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                ROW("SRNO") = I + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If CMBPARTYNAME.Text.Trim = "" Then
            EP.SetError(CMBPARTYNAME, " Please Fill Party Name ")
            bln = False
        End If

        ''CHECK WHETHER SAME ITEMNAME WITH SAME DESIGN AND SHADE IS ENTERED OR NOT
        'Dim OBJCMN As New ClsCommon
        'Dim DT As DataTable = OBJCMN.SEARCH(" SB_NO AS NO, COLORMASTER.COLOR_name AS SHADE, DESIGNMASTER.DESIGN_NO AS DESIGN ", "", " SAMPLEBARCODE INNER JOIN ITEMMASTER ON SB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON SB_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON SB_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON SB_QUALITYID = QUALITYMASTER.QUALITY_ID ", " AND ITEMMASTER.item_name = '" & CMBPARTYNAME.Text.Trim & "' AND ISNULL(QUALITYMASTER.QUALITY_NAME,'') = '" & CMBCLIENTNAME.Text.Trim & "' AND isnull(COLORMASTER.COLOR_name,'') = '" & CMBPROJECTNAME.Text.Trim & "' AND SB_YEARID = " & YearId)
        'If DT.Rows.Count > 0 Then
        '    If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And Val(TXTNO.Text) <> Val(DT.Rows(0).Item(0))) Then
        '        EP.SetError(TXTLOCATION, "LOCATION ALREADY PRESENT")
        '        bln = False
        '    End If
        'End If

        Return bln
    End Function

    Sub EDITROW()
        Try
            If gridbill.GetFocusedRowCellValue("NO") > 0 Then
                GRIDDOUBLECLICK = True
                TXTNO.Text = Val(gridbill.GetFocusedRowCellValue("NO"))
                txtsrno.Text = Val(gridbill.GetFocusedRowCellValue("SRNO"))
                CMBPARTYNAME.Text = gridbill.GetFocusedRowCellValue("PARTYNAME")
                CMBCLIENTNAME.Text = gridbill.GetFocusedRowCellValue("CLIENTNAME")
                CMBPROJECTNAME.Text = gridbill.GetFocusedRowCellValue("PROJECTNAME")
                DTAMCDATE.Text = gridbill.GetFocusedRowCellValue("AMCDATE")
                DTEWAYDATE.Text = gridbill.GetFocusedRowCellValue("EWAYDATE")
                DTEINVOICEDATE.Text = gridbill.GetFocusedRowCellValue("EINVOICEDATE")
                DTWHATSAPPDATE.Text = gridbill.GetFocusedRowCellValue("WHATSAPPDATE")
                TXTLOCATION.Text = gridbill.GetFocusedRowCellValue("LOCATION")
                DTMOBILEDATE.Text = gridbill.GetFocusedRowCellValue("MOBILEDATE")
                TXTMOBILELIC.Text = gridbill.GetFocusedRowCellValue("MOBILELIC")
                CMBPARTYNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbPARTYNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillitemname(CMBPARTYNAME, " AND ITEMMASTER.ITEM_FRMSTRING IN ('PARTYNAME')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbPARTYNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then
                itemvalidate(CMBPARTYNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'PARTYNAME' ", "PARTYNAME")

                'THIS CODE IS FOR SAVING ALL ITEMS AND DESIGNS ONCE IN THE SAMPLE ENTRY
                Dim OBJCMN As New ClsCommon

                'THIS CODE IS TO FETCH DATA FROM BARCODE STOCK
                'Dim DT As DataTable = OBJCMN.search("DISTINCT ITEMNAME ", "", " BARCODESTOCK ", " AND YEARID =" & YearId & " ORDER BY ITEMNAME")
                'For Each DTROW As DataRow In DT.Rows
                '    Dim DTDESIGN As DataTable = OBJCMN.search(" DISTINCT DESIGNNO ", "", " BARCODESTOCK ", " AND ITEMNAME = '" & DTROW("ITEMNAME") & "' AND YEARID = " & YearId & " ORDER BY DESIGNNO")
                '    For Each DRDESIGN As DataRow In DTDESIGN.Rows
                '        Dim DTCOLOR As DataTable = OBJCMN.search(" DISTINCT COLOR ", "", " BARCODESTOCK ", " AND ITEMNAME = '" & DTROW("ITEMNAME") & "' AND DESIGNNO = '" & DRDESIGN("DESIGNNO") & "' AND YEARID = " & YearId & " ORDER BY COLOR")
                '        For Each DRCOLOR As DataRow In DTCOLOR.Rows
                '            CMBMERCHANT.Text = DTROW("ITEMNAME")
                '            CMBDESIGNNO.Text = DRDESIGN("DESIGNNO")
                '            CMBCOLOR.Text = DRCOLOR("COLOR")

                '            Dim DTCHECK As DataTable = OBJCMN.search(" SB_BARCODE AS BARCODE ", "", " SAMPLEBARCODE LEFT OUTER JOIN ITEMMASTER ON SB_ITEMID = ITEM_ID LEFT OUTER JOIN DESIGNMASTER ON SB_DESIGNID = DESIGN_ID", " AND ITEMMASTER.ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND isnull(DESIGN_NO, '') = '" & CMBDESIGNNO.Text.Trim & "' AND SB_YEARID = " & YearId)
                '            If DTCHECK.Rows.Count = 0 Then Call TXTREMARKS_Validating(sender, e)
                '        Next
                '    Next
                'Next


                'THIS IS TO FETCH DATA FROM DESIGNMASER_COLOR
                'Dim DTCOLOR As DataTable = OBJCMN.search(" DISTINCT ITEMMASTER.item_name AS ITEMNAME, DESIGNMASTER.DESIGN_NO AS DESIGNNO, COLORMASTER.COLOR_name AS COLOR ", "", " DESIGNMASTER INNER JOIN ITEMMASTER ON DESIGNMASTER.DESIGN_ITEMID = ITEMMASTER.ITEM_ID INNER JOIN DESIGNMASTER_COLOR ON DESIGNMASTER.DESIGN_ID = DESIGNMASTER_COLOR.DESIGN_ID INNER JOIN  COLORMASTER ON DESIGNMASTER_COLOR.DESIGN_COLORID = COLORMASTER.COLOR_ID ", " AND DESIGNMASTER.DESIGN_YEARID = " & YearId & " ORDER BY ITEMMASTER.ITEM_NAME, DESIGNMASTER.DESIGN_NO, COLORMASTER.COLOR_NAME")
                'For Each DRCOLOR As DataRow In DTCOLOR.Rows
                '    CMBMERCHANT.Text = DRCOLOR("ITEMNAME")
                '    CMBDESIGNNO.Text = DRCOLOR("DESIGNNO")
                '    CMBCOLOR.Text = DRCOLOR("COLOR")

                '    Dim DTCHECK As DataTable = OBJCMN.search(" SB_BARCODE AS BARCODE ", "", " SAMPLEBARCODE INNER JOIN ITEMMASTER ON SB_ITEMID = ITEM_ID INNER JOIN DESIGNMASTER ON SB_DESIGNID = DESIGN_ID INNER JOIN COLORMASTER ON SB_COLORID = COLOR_ID", " AND ITEMMASTER.ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND isnull(DESIGN_NO, '') = '" & CMBDESIGNNO.Text.Trim & "' AND isnull(COLOR_NAME, '') = '" & CMBCOLOR.Text.Trim & "'  AND SB_YEARID = " & YearId)
                '    If DTCHECK.Rows.Count = 0 Then Call TXTREMARKS_Validating(sender, e)
                'Next

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPROJECTNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPROJECTNAME.Validating
        Try
            If CMBPROJECTNAME.Text.Trim <> "" Then DESIGNVALIDATE(CMBPROJECTNAME, e, Me, CMBPARTYNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ClientDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.F5 Then
                gridbilldetails.Focus()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ClientDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SAMPLE MODULE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillcmb()
            CLEAR()

            FILLGRID()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()

    End Sub

    Sub SAVE()
        Try
            Dim ALPARAVAL As New ArrayList
            Dim OBJSM As New ClsClientDetails

            ALPARAVAL.Add(Val(txtsrno.Text.Trim))
            ALPARAVAL.Add(CMBPARTYNAME.Text.Trim)
            ALPARAVAL.Add(CMBCLIENTNAME.Text.Trim)
            ALPARAVAL.Add(CMBPROJECTNAME.Text.Trim)
            ALPARAVAL.Add(DTAMCDATE.Text.Trim)
            ALPARAVAL.Add(DTEWAYDATE.Text.Trim)
            ALPARAVAL.Add(DTEINVOICEDATE.Text.Trim)
            ALPARAVAL.Add(DTWHATSAPPDATE.Text.Trim)
            ALPARAVAL.Add(TXTLOCATION.Text.Trim)
            ALPARAVAL.Add(DTMOBILEDATE.Text.Trim)
            ALPARAVAL.Add(TXTMOBILELIC.Text.Trim)


            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)
            ALPARAVAL.Add(0)

            OBJSM.ALPARAVAL = ALPARAVAL
            If GRIDDOUBLECLICK = False Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = OBJSM.SAVE()
                If DT.Rows.Count > 0 Then TXTNO.Text = Val(DT.Rows(0).Item(0))
            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                ALPARAVAL.Add(Val(TXTNO.Text.Trim))
                Dim INTRES As Integer = OBJSM.UPDATE()
                GRIDDOUBLECLICK = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            If CMBPARTYNAME.Text.Trim = "" Then FILLNAME(CMBPARTYNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUP_NAME <> 'HASTE DEBTORS'")
            If CMBCLIENTNAME.Text.Trim = "" Then FILLNAME(CMBCLIENTNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUP_NAME <> 'HASTE DEBTORS'")
            If CMBPROJECTNAME.Text.Trim = "" Then FILLNAME(CMBPROJECTNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUP_NAME <> 'HASTE DEBTORS'")

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            txtsrno.Clear()
            CMBPARTYNAME.Text = ""
            CMBCLIENTNAME.Text = ""
            CMBPROJECTNAME.Text = ""
            DTAMCDATE.Text = ""
            DTEWAYDATE.Clear()
            DTEINVOICEDATE.Clear()
            DTWHATSAPPDATE.Clear()
            TXTLOCATION.Clear()
            DTMOBILEDATE.Clear()
            TXTMOBILELIC.Clear()
            GRIDDOUBLECLICK = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT CAST(0 AS BIT) AS CHK, SAMPLEBARCODE.SB_NO AS NO, SAMPLEBARCODE.SB_GRIDSRNO AS SRNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITYNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(SAMPLEBARCODE.SB_REMARKS, '') AS REMARKS, SAMPLEBARCODE.SB_BARCODE AS BARCODE, ISNULL(CATEGORYMASTER.category_name, '') AS CATEGORY, ISNULL(ITEMMASTER.ITEM_BLOCKED,0) AS ITEMBLOCKED, ISNULL(DESIGNMASTER.DESIGN_BLOCKED,0) AS DESIGNBLOCKED, ISNULL(DESIGNMASTER_COLOR.DESIGN_BLOCKED,0) AS COLORBLOCKED FROM SAMPLEBARCODE INNER JOIN ITEMMASTER ON SAMPLEBARCODE.SB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN COLORMASTER ON SAMPLEBARCODE.SB_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON SAMPLEBARCODE.SB_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON SAMPLEBARCODE.SB_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN DESIGNMASTER_COLOR ON DESIGNMASTER.DESIGN_ID = DESIGNMASTER_COLOR.DESIGN_ID AND COLORMASTER.COLOR_ID = DESIGNMASTER_COLOR.DESIGN_COLORID WHERE SAMPLEBARCODE.SB_YEARID = " & YearId & " ORDER BY SAMPLEBARCODE.SB_NO", "", "")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
            getsrno()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        EDITROW()
    End Sub
    Private Sub CMBPROJECTNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBPROJECTNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJDESIGN As New SelectDesign
                OBJDESIGN.FRMSTRING = "PROJECT"
                OBJDESIGN.ShowDialog()
                If OBJDESIGN.TEMPNAME <> "" Then CMBPROJECTNAME.Text = OBJDESIGN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbPARTYNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBPARTYNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "PARTYNAME"
                OBJItem.STRSEARCH = " and PARTY_cmpid = " & CmpId & " and PARTY_LOCATIONid = " & Locationid & " and PARTY_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then CMBPARTYNAME.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLOCATION_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTLOCATION.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then
                EP.Clear()
                If Not errorvalid() Then
                    Exit Sub
                End If
                SAVE()
                FILLGRID()
                If ClientName = "KDFAB" Then
                    CMBPROJECTNAME.Focus()
                    CMBCLIENTNAME.Text = ""
                    CMBPROJECTNAME.Text = ""
                    TXTLOCATION.Clear()
                Else
                    CMBPARTYNAME.Focus()
                End If
            Else
                MsgBox("Enter Party Name", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTMOBILELIC_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTMOBILELIC.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTLOCATION_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTLOCATION.KeyPress
        Try
            If ClientName = "KARAN" Then numdotkeypress(sender, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbilldetails.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                Dim ROW As DataRow = gridbill.GetFocusedDataRow()

                Dim TEMPMSG As Integer = MsgBox("Wish To Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                'DELETE FROM CLIENTDETAILS
                Dim OBJSM As New ClsClientDetails
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(ROW("NO"))
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)

                OBJSM.ALPARAVAL = ALPARAVAL
                Dim INTRES As Integer = OBJSM.DELETE()

                FILLGRID()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCLIENTNAME_Enter(sender As Object, e As EventArgs) Handles CMBCLIENTNAME.Enter
        Try
            If CMBCLIENTNAME.Text.Trim = "" Then fillQUALITY(CMBCLIENTNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCLIENTNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBCLIENTNAME.Validating
        Try
            If CMBCLIENTNAME.Text.Trim <> "" Then QUALITYVALIDATE(CMBCLIENTNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPROJECTNAME_Enter(sender As Object, e As EventArgs) Handles CMBPROJECTNAME.Enter
        Try
            If CMBPROJECTNAME.Text.Trim = "" Then FILLDESIGN(CMBPROJECTNAME, CMBPARTYNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validated(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Validated
        Try
            CMBPROJECTNAME.Text = ""
            CMBCLIENTNAME.Text = ""

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class