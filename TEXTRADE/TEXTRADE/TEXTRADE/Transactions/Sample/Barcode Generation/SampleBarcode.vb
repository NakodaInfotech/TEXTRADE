
Imports System.ComponentModel
Imports System.IO
Imports BL

Public Class SampleBarcode

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
        If CMBMERCHANT.Text.Trim = "" Then
            EP.SetError(CMBMERCHANT, " Please Fill Item Name ")
            bln = False
        End If

        'CHECK WHETHER SAME ITEMNAME WITH SAME DESIGN AND SHADE IS ENTERED OR NOT
        Dim OBJCMN As New ClsCommon
        Dim DT As DataTable = OBJCMN.search(" SB_NO AS NO, COLORMASTER.COLOR_name AS SHADE, DESIGNMASTER.DESIGN_NO AS DESIGN ", "", " SAMPLEBARCODE INNER JOIN ITEMMASTER ON SB_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON SB_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON SB_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN QUALITYMASTER ON SB_QUALITYID = QUALITYMASTER.QUALITY_ID ", " AND ITEMMASTER.item_name = '" & CMBMERCHANT.Text.Trim & "' AND ISNULL(QUALITYMASTER.QUALITY_NAME,'') = '" & CMBQUALITY.Text.Trim & "' AND isnull(COLORMASTER.COLOR_name,'') = '" & CMBCOLOR.Text.Trim & "' AND isnull(DESIGNMASTER.DESIGN_NO,'') = '" & CMBDESIGNNO.Text.Trim & "' AND SB_YEARID = " & YearId)
        If DT.Rows.Count > 0 Then
            If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And Val(TXTNO.Text) <> Val(DT.Rows(0).Item(0))) Then
                EP.SetError(TXTREMARKS, "ITEM ALREADY PRESENT")
                bln = False
            End If
        End If

        Return bln
    End Function

    Sub EDITROW()
        Try
            If gridbill.GetFocusedRowCellValue("NO") > 0 Then
                GRIDDOUBLECLICK = True
                TXTNO.Text = Val(gridbill.GetFocusedRowCellValue("NO"))
                txtsrno.Text = Val(gridbill.GetFocusedRowCellValue("SRNO"))
                CMBMERCHANT.Text = gridbill.GetFocusedRowCellValue("ITEMNAME")
                CMBQUALITY.Text = gridbill.GetFocusedRowCellValue("QUALITY")
                CMBDESIGNNO.Text = gridbill.GetFocusedRowCellValue("DESIGNNO")
                CMBCOLOR.Text = gridbill.GetFocusedRowCellValue("SHADE")
                TXTREMARKS.Text = gridbill.GetFocusedRowCellValue("REMARKS")
                TXTBARCODE.Text = gridbill.GetFocusedRowCellValue("BARCODE")
                TXTCATEGORY.Text = gridbill.GetFocusedRowCellValue("CATEGORY")
                CMBMERCHANT.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbMERCHANT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBMERCHANT.Enter
        Try
            If CMBMERCHANT.Text.Trim = "" Then fillitemname(CMBMERCHANT, " AND ITEMMASTER.ITEM_FRMSTRING IN ('MERCHANT')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbMERCHANT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMERCHANT.Validating
        Try
            If CMBMERCHANT.Text.Trim <> "" Then
                itemvalidate(CMBMERCHANT, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT' ", "MERCHANT")

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

    Private Sub CMBDESIGNNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGNNO.Validating
        Try
            If CMBDESIGNNO.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGNNO, e, Me, CMBMERCHANT.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCOLOR.Enter
        Try
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR, CMBDESIGNNO.Text.Trim, CMBMERCHANT.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCOLOR.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me, CMBDESIGNNO.Text.Trim, CMBMERCHANT.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            If CMBMERCHANT.Text.Trim = "" Then fillitemname(CMBMERCHANT, " And ITEMMASTER.ITEM_FRMSTRING IN ('ITEMNAME')")
            fillQUALITY(CMBQUALITY, EDIT)
            FILLDESIGN(CMBDESIGNNO, CMBMERCHANT.Text.Trim)
            FILLCOLOR(CMBCOLOR, CMBDESIGNNO.Text.Trim, CMBMERCHANT.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
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

    Private Sub SampleBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Sub CLEAR()
        Try
            TXTNO.Clear()
            txtsrno.Clear()
            CMBMERCHANT.Text = ""
            CMBQUALITY.Text = ""
            CMBDESIGNNO.Text = ""
            CMBCOLOR.Text = ""
            TXTREMARKS.Clear()
            TXTFROM.Clear()
            TXTTO.Clear()
            TXTCATEGORY.Clear()
            GRIDDOUBLECLICK = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SampleBarcode_Load(sender As Object, e As EventArgs) Handles Me.Load
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

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub SAVE()
        Try
            Dim ALPARAVAL As New ArrayList
            Dim OBJSM As New ClsSampleBarcode

            ALPARAVAL.Add(Val(txtsrno.Text.Trim))
            ALPARAVAL.Add(CMBMERCHANT.Text.Trim)
            ALPARAVAL.Add(CMBQUALITY.Text.Trim)
            ALPARAVAL.Add(CMBDESIGNNO.Text.Trim)
            ALPARAVAL.Add(CMBCOLOR.Text.Trim)
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(TXTBARCODE.Text.Trim)


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
                BARCODE()
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

    Sub BARCODE()
        Try
            'GET BARCODE NO FROM DATABASE
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" SB_BARCODE AS BARCODE ", "", " SAMPLEBARCODE ", " AND SB_NO = " & TXTNO.Text.Trim & " AND SB_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then TXTBARCODE.Text = DT.Rows(0).Item("BARCODE")
            'PRINTBARCODE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTBARCODE()
        Try
            If ALLOWBARCODEPRINT = True Then


                Dim TEMPMSG As Integer = MsgBox("Wish to Print Bar Code?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub


                Dim TEMPHEADER As String = ""
                If ClientName = "GELATO" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR MRP" & Chr(13) & "3 FOR WSP")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
                End If

                If ClientName = "RAJKRIPA" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR DHANNA SHALI" & Chr(13) & "2 FOR COLOR CRAFT" & Chr(13) & "3 FOR C. H. L." & Chr(13) & "4 FOR BLANK")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" And TEMPHEADER <> "4" Then Exit Sub
                End If

                If ClientName = "SUPRIYA" Or ClientName = "MNARESH" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR SAMPLE STICKER" & Chr(13) & "2 FOR REGISTER STICKER")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                If ClientName = "YASHVI" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR DESIGN STICKER" & Chr(13) & "2 FOR PRE PRINTED STICKER" & Chr(13) & "3 FOR SMALL SHADE STICKER")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
                End If


                If ClientName = "KDFAB" Then
                    If MsgBox("Print Sticker With Rate?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then TEMPHEADER = "1"
                End If


                If CHKPRINT.CheckState = CheckState.Checked Then
                    Dim dirresults As String = ""

                    'Writing in file
                    Dim oWrite As System.IO.StreamWriter
                    oWrite = File.CreateText(Application.StartupPath & "\Barcode.txt")

                    For J As Integer = 1 To Val(TXTCOPIES.Text.Trim)


                        If ClientName = "ANOX" Then

                            oWrite.WriteLine("CT~~CD,~CC^~CT~")
                            oWrite.WriteLine("^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR6,6~SD10^JUS^LRN^CI0^XZ")
                            oWrite.WriteLine("^XA")
                            oWrite.WriteLine("^MMT")
                            oWrite.WriteLine("^PW400")
                            oWrite.WriteLine("^LL0200")
                            oWrite.WriteLine("^LS0")
                            oWrite.WriteLine("^FT95,50^A0N,45,52^FH\^FD" & CMBMERCHANT.Text.Trim & "^FS")
                            oWrite.WriteLine("^FT27,94^A0N,39,40^FH\^FDD. NO. :^FS")
                            oWrite.WriteLine("^FT173,94^A0N,39,43^FH\^FD" & CMBDESIGNNO.Text.Trim & "^FS")
                            oWrite.WriteLine("^BY1,3,51^FT92,156^BCN,,N,N")
                            oWrite.WriteLine("^FD>:" & TXTBARCODE.Text.Trim & "^FS")
                            oWrite.WriteLine("^FT24,178^A0N,23,24^FH\^FDE-mail - anoxlifestyle@gmail.com^FS")
                            oWrite.WriteLine("^FT330,94^A0N,39,38^FH\^FD" & CMBCOLOR.Text.Trim & "^FS")
                            oWrite.WriteLine("^FT309,94^A0N,39,38^FH\^FD/^FS")
                            oWrite.WriteLine("^PQ1,0,1,Y^XZ")
                            oWrite.Dispose()

                        ElseIf ClientName = "AVIS" Then

                            oWrite.WriteLine("n")
                            oWrite.WriteLine("M0500")
                            oWrite.WriteLine("O0214")
                            oWrite.WriteLine("V0")
                            oWrite.WriteLine("t1")
                            oWrite.WriteLine("Kf0070")
                            oWrite.WriteLine("L")
                            oWrite.WriteLine("D11")
                            oWrite.WriteLine("ySPM")
                            oWrite.WriteLine("A2")
                            oWrite.WriteLine("1911A1201200006" & CMBMERCHANT.Text.Trim)
                            oWrite.WriteLine("1911A1200930006" & CMBDESIGNNO.Text.Trim)
                            oWrite.WriteLine("1W1D66000002601092,LA," & TXTBARCODE.Text.Trim)
                            oWrite.WriteLine("1911A1200660006WIDTH:")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String = ""
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                            oWrite.WriteLine("1911A1200660071" & TEMPWIDTH)
                            oWrite.WriteLine("1911A0800330006" & TXTBARCODE.Text.Trim)
                            oWrite.WriteLine("Q0001")
                            oWrite.WriteLine("E")
                            oWrite.Dispose()


                        ElseIf ClientName = "GELATO" Then

                            oWrite.WriteLine("<xpml><page quantity='0' pitch='70.1 mm'></xpml>G0")
                            oWrite.WriteLine("n")
                            oWrite.WriteLine("M0690")
                            oWrite.WriteLine("O0214")
                            oWrite.WriteLine("V0")
                            oWrite.WriteLine("t1")
                            oWrite.WriteLine("Kf0070")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='70.1 mm'></xpml>L")
                            oWrite.WriteLine("D11")
                            oWrite.WriteLine("ySPM")
                            oWrite.WriteLine("A2")
                            oWrite.WriteLine("4911C1400060021D.NO")
                            oWrite.WriteLine("4911C1400750021:")
                            oWrite.WriteLine("4911C1400930021" & CMBDESIGNNO.Text.Trim)

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPMRP As Double = 0
                            Dim TEMPWSP As Double = 0
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search(" ISNULL(DESIGNMASTER.DESIGN_SALERATE, 0) AS MRP, ISNULL(DESIGNMASTER.DESIGN_WRATE,0) AS WSP", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & CMBDESIGNNO.Text.Trim & "' AND DESIGN_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then
                                TEMPMRP = Val(DT.Rows(0).Item("MRP"))
                                TEMPWSP = Val(DT.Rows(0).Item("WSP"))
                            End If

                            oWrite.WriteLine("4911C1400060065STYLE")
                            oWrite.WriteLine("4911C1400750065:")
                            oWrite.WriteLine("4911C1400930065" & CMBMERCHANT.Text.Trim)
                            oWrite.WriteLine("4911C1400060041SIZE")
                            oWrite.WriteLine("4911C1400750041:")
                            oWrite.WriteLine("4911C1400930041" & CMBCOLOR.Text.Trim)
                            oWrite.WriteLine("4911C1400060089QTY")
                            oWrite.WriteLine("4911C1400750089:")
                            oWrite.WriteLine("4911C14009300891")

                            If TEMPHEADER = "2" Then
                                oWrite.WriteLine("4911C1401290091MRP")
                                oWrite.WriteLine("4911C1401750091:")
                                oWrite.WriteLine("4911C1401900091" & Val(TEMPMRP))
                                oWrite.WriteLine("4911C0801700106(BOYS / KIDS)")
                            ElseIf TEMPHEADER = "3" Then
                                oWrite.WriteLine("4911C1401290091WSP")
                                oWrite.WriteLine("4911C1401750091:")
                                oWrite.WriteLine("4911C1401900091" & Val(TEMPWSP))
                                oWrite.WriteLine("4911C0801700106(BOYS / KIDS)")
                            End If

                            oWrite.WriteLine("4e4203200090140B" & TXTBARCODE.Text.Trim)
                            oWrite.WriteLine("4911A0800100109" & TXTBARCODE.Text.Trim)
                            oWrite.WriteLine("Q0001")
                            oWrite.WriteLine("E")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                            oWrite.Dispose()

                        ElseIf ClientName = "INDRAPUJAFABRICS" Then

                            oWrite.WriteLine("SIZE 80.1 mm, 50 mm")
                            oWrite.WriteLine("GAP 3 mm, 0 mm")
                            oWrite.WriteLine("DIRECTION 0,0")
                            oWrite.WriteLine("REFERENCE 0,0")
                            oWrite.WriteLine("OFFSET 0 mm")
                            oWrite.WriteLine("SET PEEL OFFT")
                            oWrite.WriteLine("SET CUTTER OFF")
                            oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                            oWrite.WriteLine("SET TEAR ON")
                            oWrite.WriteLine("ON")
                            oWrite.WriteLine("CLS")
                            oWrite.WriteLine("CODEPAGE 1252")
                            oWrite.WriteLine("TEXT 614,379,""ROMAN.TTF"",180,1,18,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("BAR 23,317, 596, 3")
                            oWrite.WriteLine("TEXT 614,301,""ROMAN.TTF"",180,1,18,""D.NO""")
                            oWrite.WriteLine("TEXT 439,301,""ROMAN.TTF"",180,1,18,"":""")
                            oWrite.WriteLine("TEXT 411,301,""ROMAN.TTF"",180,1,18,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("TEXT 614,237,""ROMAN.TTF"",180,1,18,""SHADE""")
                            oWrite.WriteLine("TEXT 439,237,""ROMAN.TTF"",180,1,18,"":""")
                            oWrite.WriteLine("TEXT 411,237,""ROMAN.TTF"",180,1,18,""" & CMBCOLOR.Text.Trim & """")
                            oWrite.WriteLine("TEXT 614,167,""ROMAN.TTF"",180,1,18,""WIDTH""")
                            oWrite.WriteLine("TEXT 439,167,""ROMAN.TTF"",180,1,18,"":""")


                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String = ""
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                            oWrite.WriteLine("TEXT 411,167,""ROMAN.TTF"",180,1,18,""" & TEMPWIDTH & """")
                            oWrite.WriteLine("BARCODE 619,99,""128M"",59,0,180,3,6,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("TEXT 401,34,""ROMAN.TTF"",180,1,8,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("PRINT 1,1")
                            oWrite.Dispose()


                        ElseIf ClientName = "INDRAPUJAIMPEX" Then

                            oWrite.WriteLine("<xpml><page quantity='0' pitch='38.0 mm'></xpml>SIZE 37.10 mm, 38 mm")
                            oWrite.WriteLine("DIRECTION 0,0")
                            oWrite.WriteLine("REFERENCE 0,0")
                            oWrite.WriteLine("OFFSET 0 mm")
                            oWrite.WriteLine("SET PEEL OFFT")
                            oWrite.WriteLine("SET CUTTER OFF")
                            oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='38.0 mm'></xpml>SET TEAR ON")
                            oWrite.WriteLine("CLS")
                            oWrite.WriteLine("CODEPAGE 1252")
                            oWrite.WriteLine("TEXT 283,288,""ROMAN.TTF"",180,1,16,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("TEXT 289,197,""ROMAN.TTF"",180,1,16,""D.NO""")
                            oWrite.WriteLine("BAR 7,239, 276, 3")
                            oWrite.WriteLine("TEXT 191,197,""ROMAN.TTF"",180,1,16,"":""")
                            oWrite.WriteLine("TEXT 172,197,""ROMAN.TTF"",180,1,16,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("TEXT 216,233,""ROMAN.TTF"",180,1,8,""" & TXTREMARKS.Text.Trim & """")
                            oWrite.WriteLine("BARCODE 285,99,""128M"",62,0,180,2,4,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("TEXT 195,32,""ROMAN.TTF"",180,1,8,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("TEXT 289,140,""ROMAN.TTF"",180,1,12,""WIDTH""")
                            oWrite.WriteLine("TEXT 191,145,""ROMAN.TTF"",180,1,14,"":""")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String = ""
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                            oWrite.WriteLine("TEXT 172,139,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")
                            oWrite.WriteLine("PRINT 1,1")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                            oWrite.Dispose()


                        ElseIf ClientName = "KCRAYON" Then

                            oWrite.WriteLine("SIZE 47.5 mm, 50 mm")
                            oWrite.WriteLine("DIRECTION 0,0")
                            oWrite.WriteLine("REFERENCE 0,0")
                            oWrite.WriteLine("OFFSET 0 mm")
                            oWrite.WriteLine("SET PEEL OFFT")
                            oWrite.WriteLine("SET CUTTER OFF")
                            oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                            oWrite.WriteLine("SET TEAR ON")
                            oWrite.WriteLine("CLS")
                            oWrite.WriteLine("CODEPAGE 1252")
                            oWrite.WriteLine("TEXT 373,244,""ROMAN.TTF"",180,1,18,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("TEXT 373,174,""ROMAN.TTF"",180,1,12,""SHADE""")
                            oWrite.WriteLine("TEXT 275,174,""ROMAN.TTF"",180,1,12,"":""")
                            oWrite.WriteLine("TEXT 259,174,""ROMAN.TTF"",180,1,12,""" & CMBCOLOR.Text.Trim & """")
                            oWrite.WriteLine("TEXT 373,113,""ROMAN.TTF"",180,1,12,""WIDTH""")
                            oWrite.WriteLine("TEXT 275,113,""ROMAN.TTF"",180,1,12,"":""")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String = ""
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                            oWrite.WriteLine("TEXT 259,113,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """")
                            oWrite.WriteLine("QRCODE 113,134,L,5,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("TEXT 373,52,""ROMAN.TTF"",180,1,12,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("PRINT 1,1")
                            oWrite.Dispose()


                        ElseIf ClientName = "KARAN" Then

                            oWrite.WriteLine("SIZE 47.5 mm, 25 mm")
                            oWrite.WriteLine("GAP 0 mm, 0 mm")
                            oWrite.WriteLine("DIRECTION 0,0")
                            oWrite.WriteLine("REFERENCE 0,0")
                            oWrite.WriteLine("OFFSET 0 mm")
                            oWrite.WriteLine("SET PEEL OFFT")
                            oWrite.WriteLine("SET CUTTER OFF")
                            oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                            oWrite.WriteLine("SET TEAR ON")
                            oWrite.WriteLine("CLS")
                            oWrite.WriteLine("CODEPAGE 1252")
                            oWrite.WriteLine("TEXT 367,180,""ROMAN.TTF"",180,1,9,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("TEXT 367,130,""ROMAN.TTF"",180,1,14,""D.NO""")
                            oWrite.WriteLine("TEXT 271,130,""ROMAN.TTF"",180,1,14,"":""")
                            oWrite.WriteLine("TEXT 258,130,""ROMAN.TTF"",180,1,14,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("TEXT 367,66,""ROMAN.TTF"",180,1,14,""S.NO""")
                            oWrite.WriteLine("TEXT 271,66,""ROMAN.TTF"",180,1,14,"":""")
                            oWrite.WriteLine("TEXT 258,66,""ROMAN.TTF"",180,1,14,""" & CMBCOLOR.Text.Trim & """")
                            oWrite.WriteLine("PRINT 1,1")
                            oWrite.Dispose()


                        ElseIf ClientName = "KDFAB" Then

                            oWrite.WriteLine("I8,A")
                            oWrite.WriteLine("ZN")
                            oWrite.WriteLine("q401")
                            oWrite.WriteLine("O")
                            oWrite.WriteLine("JF")
                            oWrite.WriteLine("ZT")
                            oWrite.WriteLine("Q304,25")
                            oWrite.WriteLine("N")
                            oWrite.WriteLine("A379,200,2,1,2,2,N,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("A379,161,2,1,2,2,N,""WIDTH""")
                            oWrite.WriteLine("A272,161,2,1,2,2,N,"":""")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String = ""
                            Dim TEMPRATE As Double = 0
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                            oWrite.WriteLine("A246,161,2,1,2,2,N,""" & TEMPWIDTH & """")

                            oWrite.WriteLine("A379,126,2,1,2,2,N,""D.NO""")
                            oWrite.WriteLine("A272,126,2,1,2,2,N,"":""")
                            oWrite.WriteLine("A246,126,2,1,2,2,N,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("A379,91,2,1,2,2,N,""RATE""")
                            oWrite.WriteLine("A272,91,2,1,2,2,N,"":""")


                            If TEMPHEADER = "1" Then
                                'GET RATE
                                Dim WHERECLAUSE As String = ""
                                'If CMBDESIGNNO.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(DESIGNMASTER.DESIGN_NO,'') = '" & CMBDESIGNNO.Text.Trim & "'"
                                'If CMBCOLOR.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(COLORMASTER.COLOR_NAME,'') = '" & CMBCOLOR.Text.Trim & "'"
                                DT = OBJCMN.search("PRICELISTMASTER.PL_RATE AS SALERATE ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON PRICELISTMASTER.PL_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON PRICELISTMASTER.PL_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON PRICELISTMASTER.PL_QUALITYID = QUALITYMASTER.QUALITY_id ", " AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & CMBMERCHANT.Text.Trim & "'" & WHERECLAUSE & " AND PL_YEARID = " & YearId)
                                If DT.Rows.Count > 0 Then TEMPRATE = Val(DT.Rows(0).Item("SALERATE"))
                                If TXTREMARKS.Text.Trim = "" Then oWrite.WriteLine("A246,91,2,1,2,2,N,""" & Format(Val(TEMPRATE), "0.00") & """") Else oWrite.WriteLine("A246,91,2,1,2,2,N,""" & Format(Val(TEMPRATE), "0.00") & TXTREMARKS.Text.Trim & """")

                            End If


                            oWrite.WriteLine("B380,57,2,1,2,4,37,N,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("A261,18,2,1,1,1,N,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("P1")
                            oWrite.Dispose()


                        ElseIf ClientName = "KOTHARI" Then

                            oWrite.WriteLine("<xpml><page quantity='0' pitch='38.0 mm'></xpml>I8,A")
                            oWrite.WriteLine("ZN")
                            oWrite.WriteLine("q636")
                            oWrite.WriteLine("S3")
                            oWrite.WriteLine("O")
                            oWrite.WriteLine("JF")
                            oWrite.WriteLine("KIZZQ0")
                            oWrite.WriteLine("KI9+0.0")
                            oWrite.WriteLine("D14")
                            oWrite.WriteLine("ZT")
                            oWrite.WriteLine("Q304,B25")
                            oWrite.WriteLine("Arglabel 380 31")
                            oWrite.WriteLine("exit")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='38.0 mm'></xpml>N")
                            oWrite.WriteLine("b151,108,Q,m2,s5,eL,iA,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("A24,77,0,4,1,1,N,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("A24,165,0,4,1,1,N,""" & CMBCOLOR.Text.Trim & """")
                            oWrite.WriteLine("A24,123,0,4,1,1,N,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("A24,211,0,2,1,1,N,""" & TXTREMARKS.Text.Trim & """")
                            oWrite.WriteLine("A265,225,3,1,1,1,N,""" & TXTBARCODE.Text.Trim & """")
                            oWrite.WriteLine("b487,108,Q,m2,s5,eL,iA,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("A360,77,0,4,1,1,N,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("A360,165,0,4,1,1,N,""" & CMBCOLOR.Text.Trim & """")
                            oWrite.WriteLine("A360,123,0,4,1,1,N,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("A360,211,0,2,1,1,N,""" & TXTREMARKS.Text.Trim & """")
                            oWrite.WriteLine("A602,225,3,1,1,1,N,""" & TXTBARCODE.Text.Trim & """")
                            oWrite.WriteLine("P1")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                            oWrite.Dispose()



                        ElseIf ClientName = "KRFABRICS" Then

                            oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                            oWrite.WriteLine("GAP 3 mm, 0 mm")
                            oWrite.WriteLine("DIRECTION 0,0")
                            oWrite.WriteLine("REFERENCE 0,0")
                            oWrite.WriteLine("OFFSET 0 mm")
                            oWrite.WriteLine("SET PEEL OFF")
                            oWrite.WriteLine("SET CUTTER OFF")
                            oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                            oWrite.WriteLine("SET TEAR ON")
                            oWrite.WriteLine("CLS")
                            oWrite.WriteLine("CODEPAGE 1252")
                            oWrite.WriteLine("TEXT 573,375,""ROMAN.TTF"",180,1,22,""" & CmpName & """")
                            oWrite.WriteLine("BAR 204,318, 369, 3")
                            oWrite.WriteLine("TEXT 746,286,""ROMAN.TTF"",180,1,14,""QUALITY""")
                            oWrite.WriteLine("TEXT 578,286,""ROMAN.TTF"",180,1,14,"":""")
                            oWrite.WriteLine("TEXT 551,286,""ROMAN.TTF"",180,1,14,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("TEXT 746,231,""ROMAN.TTF"",180,1,14,""D.NO""")
                            oWrite.WriteLine("TEXT 578,231,""ROMAN.TTF"",180,1,14,"":""")
                            oWrite.WriteLine("TEXT 551,231,""ROMAN.TTF"",180,1,14,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("TEXT 746,174,""ROMAN.TTF"",180,1,14,""SHADE""")
                            oWrite.WriteLine("TEXT 578,174,""ROMAN.TTF"",180,1,14,"":""")
                            oWrite.WriteLine("TEXT 551,174,""ROMAN.TTF"",180,1,14,""" & CMBCOLOR.Text.Trim & """")
                            oWrite.WriteLine("TEXT 746,123,""ROMAN.TTF"",180,1,14,""SERIES""")
                            oWrite.WriteLine("TEXT 578,123,""ROMAN.TTF"",180,1,14,"":""")
                            oWrite.WriteLine("TEXT 551,128,""ROMAN.TTF"",180,1,18,""" & TXTREMARKS.Text.Trim & """")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String = ""
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then
                                TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                            End If

                            oWrite.WriteLine("TEXT 746,68,""ROMAN.TTF"",180,1,14,""WIDTH""")
                            oWrite.WriteLine("TEXT 578,68,""ROMAN.TTF"",180,1,14,"":""")
                            oWrite.WriteLine("TEXT 551,68,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")
                            oWrite.WriteLine("QRCODE 248,231,L,9,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("TEXT 248,39,""ROMAN.TTF"",180,1,9,""" & TXTBARCODE.Text.Trim & """")
                            oWrite.WriteLine("PRINT 1,1")
                            oWrite.Dispose()


                        ElseIf ClientName = "KRISHNA" Then

                            oWrite.WriteLine("SIZE 72.4 mm, 36.2 mm")
                            oWrite.WriteLine("GAP 3 mm, 0 mm")
                            oWrite.WriteLine("DIRECTION 0,0")
                            oWrite.WriteLine("REFERENCE 0,0")
                            oWrite.WriteLine("OFFSET 0 mm")
                            oWrite.WriteLine("SET PEEL OFF")
                            oWrite.WriteLine("SET CUTTER OFF")
                            oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                            oWrite.WriteLine("SET TEAR ON")
                            oWrite.WriteLine("CLS")
                            oWrite.WriteLine("CODEPAGE 1252")
                            oWrite.WriteLine("TEXT 392,279,""ROMAN.TTF"",180,1,16,""SUNWARE""")
                            oWrite.WriteLine("BAR 186,239, 206, 2")
                            oWrite.WriteLine("TEXT 560,225,""ROMAN.TTF"",180,1,12,""DESIGN""")
                            oWrite.WriteLine("TEXT 560,184,""ROMAN.TTF"",180,1,12,""SHADE""")
                            oWrite.WriteLine("TEXT 560,144,""ROMAN.TTF"",180,1,12,""WIDTH""")
                            oWrite.WriteLine("TEXT 439,225,""ROMAN.TTF"",180,1,12,"":""")
                            oWrite.WriteLine("TEXT 439,184,""ROMAN.TTF"",180,1,12,"":""")
                            oWrite.WriteLine("TEXT 439,144,""ROMAN.TTF"",180,1,12,"":""")
                            oWrite.WriteLine("TEXT 418,225,""ROMAN.TTF"",180,1,12,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("TEXT 418,184,""ROMAN.TTF"",180,1,12,""" & CMBCOLOR.Text.Trim & """")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then
                                TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                            End If

                            oWrite.WriteLine("TEXT 418,144,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """")
                            oWrite.WriteLine("BARCODE 560,93,""128M"",51,0,180,2,4,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("TEXT 447,38,""ROMAN.TTF"",180,1,8,""" & TXTBARCODE.Text.Trim & """")
                            oWrite.WriteLine("PRINT 1,2")
                            oWrite.Dispose()

                        ElseIf ClientName = "MAHAVIRPOLYCOT" Then

                            If CMBCOLOR.Text.Trim = "" Then
                                oWrite.WriteLine("I8,A")
                                oWrite.WriteLine("ZN")
                                oWrite.WriteLine("q779")
                                oWrite.WriteLine("S3")
                                oWrite.WriteLine("O")
                                oWrite.WriteLine("JF")
                                oWrite.WriteLine("D8")
                                oWrite.WriteLine("ZT")
                                oWrite.WriteLine("Q800,25")
                                oWrite.WriteLine("KI81")
                                oWrite.WriteLine("N")
                                oWrite.WriteLine("A742,379,2,2,2,2,N,""Width""")
                                oWrite.WriteLine("A742,603,2,2,2,2,N,""Item""")
                                oWrite.WriteLine("b80,367,Q,m2,s5,eL,iA,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("A205,352,2,1,1,1,N,""" & TXTBARCODE.Text.Trim & """") 'BARCODE

                                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                Dim TEMPWIDTH As String = ""
                                Dim TEMPHSN As String = ""
                                Dim TEMPRATE As String = ""
                                Dim TEMPQUALITY As String = ""
                                Dim TEMPREMARKS As String = ""
                                Dim OBJCMN As New ClsCommon
                                Dim DT As DataTable = OBJCMN.search(" ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE, ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_SELVEDGE, '') AS SELVEDGE, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(ITEMMASTER.ITEM_RATE, 0) AS RATE  ", "", " ITEMMASTER LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.item_HSNCODEID = HSNMASTER.HSN_ID ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                                If DT.Rows.Count > 0 Then
                                    TEMPHSN = DT.Rows(0).Item("HSNCODE")
                                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    TEMPQUALITY = DT.Rows(0).Item("SELVEDGE")
                                    TEMPRATE = (Val(DT.Rows(0).Item("RATE")) + 18) & "000"
                                End If

                                DT = OBJCMN.search(" ISNULL(CAST(DESIGNMASTER.DESIGN_REMARK AS VARCHAR(1000)), '') AS REMARKS ", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & CMBDESIGNNO.Text.Trim & "' AND DESIGN_YEARID = " & YearId)
                                If DT.Rows.Count > 0 Then
                                    TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                End If

                                oWrite.WriteLine("A548,379,2,2,2,2,N,""" & TEMPWIDTH & """")
                                oWrite.WriteLine("A548,603,2,2,2,2,N,""" & CMBMERCHANT.Text.Trim & """")
                                oWrite.WriteLine("A742,552,2,2,2,2,N,""D.No""")
                                oWrite.WriteLine("A580,603,2,2,2,2,N,"":""")
                                oWrite.WriteLine("A548,552,2,2,2,2,N,""" & CMBDESIGNNO.Text.Trim & """")
                                oWrite.WriteLine("A580,552,2,2,2,2,N,"":""")
                                oWrite.WriteLine("A580,379,2,2,2,2,N,"":""")
                                oWrite.WriteLine("A548,314,2,4,1,1,N,""" & TEMPQUALITY & """")
                                oWrite.WriteLine("A742,491,2,2,2,2,N,""Series""")
                                oWrite.WriteLine("A548,491,2,2,2,2,N,""" & TEMPRATE & """")
                                oWrite.WriteLine("A580,491,2,2,2,2,N,"":""")
                                oWrite.WriteLine("A742,317,2,2,2,2,N,""Quality""")
                                oWrite.WriteLine("A580,317,2,2,2,2,N,"":""")
                                oWrite.WriteLine("A742,435,2,2,2,2,N,""HSN""")
                                oWrite.WriteLine("A548,435,2,2,2,2,N,""" & TEMPHSN & """")
                                oWrite.WriteLine("A580,435,2,2,2,2,N,"":""")
                                oWrite.WriteLine("A611,178,2,4,1,1,N,""" & CMBMERCHANT.Text.Trim & """")
                                oWrite.WriteLine("A611,117,2,4,1,1,N,""" & CMBDESIGNNO.Text.Trim & """")
                                oWrite.WriteLine("A185,184,2,3,2,2,N,""" & TEMPREMARKS & """")
                                oWrite.WriteLine("A217,23,1,2,1,1,N,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("b52,24,Q,m2,s5,eL,iA,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("P1")
                                oWrite.Dispose()
                            Else
                                oWrite.WriteLine("I8,A")
                                oWrite.WriteLine("ZN")
                                oWrite.WriteLine("q300")
                                oWrite.WriteLine("O")
                                oWrite.WriteLine("JF")
                                oWrite.WriteLine("ZT")
                                oWrite.WriteLine("Q120,25")
                                oWrite.WriteLine("KI80")
                                oWrite.WriteLine("N")
                                oWrite.WriteLine("A293,109,2,3,1,1,N,""" & CMBMERCHANT.Text.Trim & """")
                                oWrite.WriteLine("A293,76,2,1,1,1,N,""" & CMBDESIGNNO.Text.Trim & """")
                                oWrite.WriteLine("LO5,83,289,2")
                                oWrite.WriteLine("LO80,9,2,73")
                                oWrite.WriteLine("A215,49,2,3,1,1,N,""" & CMBCOLOR.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("b10,11,Q,m2,s3,eL,iA,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("LO80,52,203,2")
                                oWrite.WriteLine("A266,23,2,1,1,1,N,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("P1")
                                oWrite.Dispose()
                            End If


                        ElseIf ClientName = "MANMANDIR" Then

                            oWrite.WriteLine("SIZE 35.5 mm, 50 mm")
                            oWrite.WriteLine("DIRECTION 0,0")
                            oWrite.WriteLine("REFERENCE 0,0")
                            oWrite.WriteLine("OFFSET 0 mm")
                            oWrite.WriteLine("SET PEEL OFF")
                            oWrite.WriteLine("SET CUTTER OFF")
                            oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                            oWrite.WriteLine("SET TEAR ON")
                            oWrite.WriteLine("CLS")
                            oWrite.WriteLine("CODEPAGE 1252")
                            oWrite.WriteLine("TEXT 5,363,""ROMAN.TTF"",270,12,12,""" & CMBMERCHANT.Text.Trim & """")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String
                            Dim TEMPCATEGORY As String
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then
                                TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                            End If

                            oWrite.WriteLine("TEXT 42,290,""ROMAN.TTF"",270,1,8,""" & TEMPCATEGORY & """")
                            oWrite.WriteLine("TEXT 76,363,""ROMAN.TTF"",270,1,12,""WIDTH""")
                            oWrite.WriteLine("TEXT 76,256,""ROMAN.TTF"",270,1,12,"":""")
                            oWrite.WriteLine("TEXT 76,236,""ROMAN.TTF"",270,1,12,""" & TEMPWIDTH & """")
                            oWrite.WriteLine("TEXT 121,363,""ROMAN.TTF"",270,1,12,""SHADE""")
                            oWrite.WriteLine("TEXT 121,256,""ROMAN.TTF"",270,1,12,"":""")
                            oWrite.WriteLine("TEXT 121,236,""ROMAN.TTF"",270,1,12,""" & CMBCOLOR.Text.Trim & """")
                            oWrite.WriteLine("BARCODE 155,372,""128M"",89,0,270,2,4,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("TEXT 249,265,""ROMAN.TTF"",270,1,8,""" & TXTBARCODE.Text.Trim & """")
                            oWrite.WriteLine("PRINT 1,1")
                            oWrite.Dispose()

                        ElseIf ClientName = "MNARESH" Then

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String = ""
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then
                                TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                            End If

                            If TEMPHEADER = "1" Then

                                oWrite.WriteLine("SIZE 97.5 mm, 50 mm
DIRECTION 0,0
REFERENCE 0,0
OFFSET 0 mm
SET PEEL OFF
SET CUTTER OFF
SET PARTIAL_CUTTER OFF
SET TEAR ON
CLS
CODEPAGE 1252
TEXT 755,379,""ROMAN.TTF"",180,1,16,""" & CMBMERCHANT.Text.Trim & """
BAR 409,324, 355, 3
TEXT 755,307,""ROMAN.TTF"",180,1,14,""LOT NO""
TEXT 614,307,""ROMAN.TTF"",180,1,14,"":""
TEXT 583,307,""ROMAN.TTF"",180,1,14,""" & TXTREMARKS.Text.Trim & """
TEXT 755,253,""ROMAN.TTF"",180,1,14,""D. NO""
TEXT 614,253,""ROMAN.TTF"",180,1,14,"":""
TEXT 583,253,""ROMAN.TTF"",180,1,14,""" & CMBDESIGNNO.Text.Trim & """
TEXT 755,199,""ROMAN.TTF"",180,1,14,""SHADE""
TEXT 614,199,""ROMAN.TTF"",180,1,14,"":""
TEXT 583,199,""ROMAN.TTF"",180,1,14,""" & CMBCOLOR.Text.Trim & """
TEXT 755,145,""ROMAN.TTF"",180,1,14,""WIDTH""
TEXT 614,145,""ROMAN.TTF"",180,1,14,"":""
TEXT 583,145,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """
BARCODE 764,101,""39"",60,0,180,2,5,""" & TXTBARCODE.Text.Trim & """
TEXT 631,36,""ROMAN.TTF"",180,1,8,""" & TXTBARCODE.Text.Trim & """
TEXT 384,379,""ROMAN.TTF"",180,1,16,""" & CMBMERCHANT.Text.Trim & """
BAR 37,324, 355, 3
TEXT 384,307,""ROMAN.TTF"",180,1,14,""LOT NO""
TEXT 243,307,""ROMAN.TTF"",180,1,14,"":""
TEXT 211,307,""ROMAN.TTF"",180,1,14,""" & TXTREMARKS.Text.Trim & """
TEXT 384,253,""ROMAN.TTF"",180,1,14,""D. NO""
TEXT 243,253,""ROMAN.TTF"",180,1,14,"":""
TEXT 211,253,""ROMAN.TTF"",180,1,14,""" & CMBDESIGNNO.Text.Trim & """
TEXT 384,199,""ROMAN.TTF"",180,1,14,""SHADE""
TEXT 243,199,""ROMAN.TTF"",180,1,14,"":""
TEXT 211,199,""ROMAN.TTF"",180,1,14,""" & CMBCOLOR.Text.Trim & """
TEXT 384,145,""ROMAN.TTF"",180,1,14,""WIDTH""
TEXT 243,145,""ROMAN.TTF"",180,1,14,"":""
TEXT 211,145,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """
BARCODE 390,101,""39"",60,0,180,2,5,""" & TXTBARCODE.Text.Trim & """
TEXT 258,36,""ROMAN.TTF"",180,1,8,""" & TXTBARCODE.Text.Trim & """
PRINT 1,1")
                                oWrite.Dispose()

                            Else

                                oWrite.WriteLine("SIZE 67.5 mm, 50 mm
DIRECTION 0,0
REFERENCE 0,0
OFFSET 0 mm
SET PEEL OFF
SET CUTTER OFF
SET PARTIAL_CUTTER OFF
SET TEAR ON
CLS
CODEPAGE 1252
TEXT 523,385,""ROMAN.TTF"",180,1,12,""" & CMBMERCHANT.Text.Trim & """
BAR 19,337, 510, 3
TEXT 517,314,""ROMAN.TTF"",180,1,12,""LOT NO""
TEXT 379,314,""ROMAN.TTF"",180,1,12,"":""
TEXT 358,314,""ROMAN.TTF"",180,1,12,""" & TXTREMARKS.Text.Trim & """
TEXT 517,259,""ROMAN.TTF"",180,1,12,""D. NO""
TEXT 379,259,""ROMAN.TTF"",180,1,12,"":""
TEXT 358,259,""ROMAN.TTF"",180,1,12,""" & CMBDESIGNNO.Text.Trim & """
TEXT 517,204,""ROMAN.TTF"",180,1,12,""SHADE""
TEXT 379,204,""ROMAN.TTF"",180,1,12,"":""
TEXT 358,204,""ROMAN.TTF"",180,1,12,""" & CMBCOLOR.Text.Trim & """
BARCODE 523,101,""39"",65,0,180,2,5,""" & TXTBARCODE.Text.Trim & """
TEXT 366,31,""0"",180,7,7,""" & TXTBARCODE.Text.Trim & """
TEXT 517,149,""ROMAN.TTF"",180,1,12,""WIDTH""
TEXT 379,149,""ROMAN.TTF"",180,1,12,"":""
TEXT 358,149,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """
PRINT 1,1")
                                oWrite.Dispose()

                            End If


                        ElseIf ClientName = "MNIKHIL" Then

                            oWrite.WriteLine("SIZE 50 mm, 25 mm")
                            oWrite.WriteLine("DIRECTION 0,0")
                            oWrite.WriteLine("REFERENCE 0,0")
                            oWrite.WriteLine("OFFSET 0 mm")
                            oWrite.WriteLine("SET PEEL OFF")
                            oWrite.WriteLine("SET CUTTER OFF")
                            oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                            oWrite.WriteLine("SET TEAR ON")
                            oWrite.WriteLine("CLS")
                            oWrite.WriteLine("CODEPAGE 1252")
                            oWrite.WriteLine("TEXT 375,183,""ROMAN.TTF"",180,1,18,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("TEXT 375,110,""ROMAN.TTF"",180,1,12,""Design""")
                            oWrite.WriteLine("TEXT 270,110,""ROMAN.TTF"",180,1,12,"":""")
                            oWrite.WriteLine("TEXT 253,110,""ROMAN.TTF"",180,1,12,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("TEXT 375,65,""ROMAN.TTF"",180,1,12,""Width""")
                            oWrite.WriteLine("TEXT 270,65,""ROMAN.TTF"",180,1,12,"":""")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then
                                TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                            End If

                            oWrite.WriteLine("TEXT 253,65,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """")
                            oWrite.WriteLine("BAR 30,128, 344, 3")
                            oWrite.WriteLine("PRINT 1,1")
                            oWrite.Dispose()

                        ElseIf ClientName = "RAJKRIPA" Then

                            oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>^XA")
                            oWrite.WriteLine("^MCY^PMN")
                            oWrite.WriteLine("^PW380")
                            oWrite.WriteLine("~JSN^MMT")
                            oWrite.WriteLine("^JZY")
                            oWrite.WriteLine("^LH0,0^LRN")
                            oWrite.WriteLine("^XZ")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='0' pitch='50.0 mm'></xpml>~DGR:SSGFX000.GRF,190,5,:Z64:eJxtjrENgDAMBA9RpGQBRNagQLBSSgqKjJZRMkJKuvAxVCiydG7s/4Mb8DXBUTNUdzMULlxmx0UmjfYCY5phSJuQTz2VIIQfFMHaQ+kcW8oqZAudW0crilapcv9qmJDUyidpuk38AVIpK8E=:119B")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>^XA")
                            oWrite.WriteLine("^FT23,103")
                            oWrite.WriteLine("^CI0")
                            If TEMPHEADER = "1" Then
                                oWrite.WriteLine("^A0N,39,52^FDDHANNA SHALI^FS")
                            ElseIf TEMPHEADER = "2" Then
                                oWrite.WriteLine("^A0N,39,52^FDCOLOR CRAFT^FS")
                            ElseIf TEMPHEADER = "3" Then
                                oWrite.WriteLine("^A0N,39,52^FDC. H. L.^FS")
                            End If
                            oWrite.WriteLine("^FT331,55")

                            oWrite.WriteLine("^AAN,27,15^FDR^FS")
                            oWrite.WriteLine("^FT54,145")
                            oWrite.WriteLine("^A0N,23,30^FDEXCLUSIVE SHIRTING^FS")
                            oWrite.WriteLine("^FT9,197")

                            'GET ITEMDISPLAYNAME FROM ITEMMASTER
                            Dim TEMPITEMNAME As String = ""
                            Dim TEMPWIDTH As String = ""
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.SEARCH("  ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS ITEMDISPLAYNAME, ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then
                                TEMPITEMNAME = DT.Rows(0).Item("ITEMDISPLAYNAME")
                                TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                            End If

                            oWrite.WriteLine("^A0N,23,30^FD" & TEMPITEMNAME & "^FS")
                            oWrite.WriteLine("^FT10,235")
                            oWrite.WriteLine("^A0N,23,30^FDWIDTH^FS")
                            oWrite.WriteLine("^FT112,235")
                            oWrite.WriteLine("^A0N,23,30^FD:^FS")
                            oWrite.WriteLine("^FT136,235")
                            oWrite.WriteLine("^A0N,23,30^FD" & TEMPWIDTH & "^FS")
                            oWrite.WriteLine("^FT10,275")
                            oWrite.WriteLine("^A0N,23,30^FDSMP ^FS")
                            oWrite.WriteLine("^FT112,275")
                            oWrite.WriteLine("^A0N,23,30^FD:^FS")
                            oWrite.WriteLine("^FT136,275")
                            oWrite.WriteLine("^A0N,23,30^FD" & TXTBARCODE.Text.Trim & "^FS") 'BARCODE
                            oWrite.WriteLine("^FO12,289")
                            oWrite.WriteLine("^BY2^BCN,80,N,N,,A^FD" & TXTBARCODE.Text.Trim & "^FS") 'BARCODE
                            oWrite.WriteLine("^FO322,26")
                            oWrite.WriteLine("^XGR:SSGFX000.GRF,1,1^FS")
                            oWrite.WriteLine("^PQ1,0,1,Y")
                            oWrite.WriteLine("^XZ")
                            oWrite.WriteLine("<xpml></page></xpml>^XA")
                            oWrite.WriteLine("^IDR:SSGFX000.GRF^XZ")
                            oWrite.WriteLine("<xpml><end/></xpml>")
                            oWrite.Dispose()

                        ElseIf ClientName = "REALCORPORATION" Then

                            If CMBCOLOR.Text.Trim = "" Then
                                oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>SIZE 47.5 mm, 25 mm")
                                oWrite.WriteLine("GAP 3 mm, 0 mm")
                                oWrite.WriteLine("DIRECTION 0,0")
                                oWrite.WriteLine("REFERENCE 0,0")
                                oWrite.WriteLine("OFFSET 0 mm")
                                oWrite.WriteLine("SET PEEL OFFT")
                                oWrite.WriteLine("SET CUTTER OFF")
                                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>SET TEAR ON")
                                oWrite.WriteLine("ON")
                                oWrite.WriteLine("CLS")
                                oWrite.WriteLine("CODEPAGE 1252")
                                oWrite.WriteLine("TEXT 364,187,""0"",180,10,10,""" & CMBMERCHANT.Text.Trim & """")
                                oWrite.WriteLine("BAR 9,152, 357, 3")
                                oWrite.WriteLine("TEXT 364,141,""0"",180,10,10,""D.NO""")
                                oWrite.WriteLine("TEXT 275,141,""0"",180,10,10,"":""")
                                oWrite.WriteLine("TEXT 261,141,""0"",180,10,10,""" & CMBDESIGNNO.Text.Trim & """")
                                oWrite.WriteLine("TEXT 364,95,""0"",180,10,10,""WIDTH""")
                                oWrite.WriteLine("TEXT 275,95,""0"",180,10,10,"":""")



                                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                Dim TEMPREMARKS As String = ""
                                Dim TEMPWIDTH As String = ""
                                Dim OBJCMN As New ClsCommon
                                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(ITEMMASTER.ITEM_WIDTH,'') AS WIDTH", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                                If DT.Rows.Count > 0 Then
                                    TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                End If

                                oWrite.WriteLine("TEXT 261,95,""0"",180,10,10,""" & TEMPWIDTH & """")
                                oWrite.WriteLine("TEXT 364,48,""0"",180,10,10,""" & TEMPREMARKS & """")
                                oWrite.WriteLine("QRCODE 121,106,L,4,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("TEXT 26,14,""0"",90,6,6,""" & TXTBARCODE.Text.Trim & """")
                                oWrite.WriteLine("PRINT 1,1")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                oWrite.Dispose()
                            Else
                                oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>SIZE 47.5 mm, 25 mm")
                                oWrite.WriteLine("GAP 3 mm, 0 mm")
                                oWrite.WriteLine("DIRECTION 0,0")
                                oWrite.WriteLine("REFERENCE 0,0")
                                oWrite.WriteLine("OFFSET 0 mm")
                                oWrite.WriteLine("SET PEEL OFFT")
                                oWrite.WriteLine("SET CUTTER OFF")
                                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>SET TEAR ON")
                                oWrite.WriteLine("ON")
                                oWrite.WriteLine("CLS")
                                oWrite.WriteLine("CODEPAGE 1252")
                                oWrite.WriteLine("TEXT 364,187,""0"",180,10,10,""" & CMBMERCHANT.Text.Trim & """")
                                oWrite.WriteLine("BAR 9,152, 357, 3")
                                oWrite.WriteLine("TEXT 364,144,""0"",180,10,10,""D.NO""")
                                oWrite.WriteLine("TEXT 275,144,""0"",180,10,10,"":""")
                                oWrite.WriteLine("TEXT 261,144,""0"",180,10,10,""" & CMBDESIGNNO.Text.Trim & """")
                                oWrite.WriteLine("TEXT 364,74,""0"",180,10,10,""WIDTH""")
                                oWrite.WriteLine("TEXT 275,74,""0"",180,10,10,"":""")



                                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                Dim TEMPREMARKS As String = ""
                                Dim TEMPWIDTH As String = ""
                                Dim OBJCMN As New ClsCommon
                                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(ITEMMASTER.ITEM_WIDTH,'') AS WIDTH", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                                If DT.Rows.Count > 0 Then
                                    TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                End If

                                oWrite.WriteLine("TEXT 261,74,""0"",180,10,10,""" & TEMPWIDTH & """")
                                oWrite.WriteLine("TEXT 364,40,""0"",180,10,10,""" & TEMPREMARKS & """")
                                oWrite.WriteLine("QRCODE 121,106,L,4,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("TEXT 26,14,""0"",90,6,6,""" & TXTBARCODE.Text.Trim & """")
                                oWrite.WriteLine("TEXT 364,109,""0"",180,10,10,""SHADE""")
                                oWrite.WriteLine("TEXT 275,109,""0"",180,10,10,"":""")
                                oWrite.WriteLine("TEXT 261,109,""0"",180,10,10,""" & CMBCOLOR.Text.Trim & """")
                                oWrite.WriteLine("PRINT 1,1")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                oWrite.Dispose()
                            End If

                        ElseIf ClientName = "RMANILAL" Then

                            oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 47.5 mm, 50 mm")
                            oWrite.WriteLine("GAP 3 mm, 0 mm")
                            oWrite.WriteLine("SET RIBBON OFF")
                            oWrite.WriteLine("DIRECTION 0,0")
                            oWrite.WriteLine("REFERENCE 0,0")
                            oWrite.WriteLine("OFFSET 0 mm")
                            oWrite.WriteLine("SET PEEL OFFT")
                            oWrite.WriteLine("SET CUTTER OFF")
                            oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
                            oWrite.WriteLine("CLS")
                            oWrite.WriteLine("CODEPAGE 1252")
                            oWrite.WriteLine("TEXT 365,373,""0"",180,16,16,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("TEXT 365,316,""0"",180,12,12,""D.NO""")
                            oWrite.WriteLine("TEXT 264,316,""0"",180,12,12,"":""")
                            oWrite.WriteLine("TEXT 242,316,""0"",180,12,12,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("TEXT 365,269,""0"",180,12,12,""CH.NO""")
                            oWrite.WriteLine("TEXT 264,269,""0"",180,12,12,"":""")
                            oWrite.WriteLine("TEXT 242,269,""0"",180,12,12,""" & CMBCOLOR.Text.Trim & """")
                            oWrite.WriteLine("QRCODE 266,169,L,6,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("TEXT 169,269,""0"",180,12,12,""" & TXTREMARKS.Text.Trim & """")
                            oWrite.WriteLine("TEXT 365,219,""0"",180,12,12,""BASE""")
                            oWrite.WriteLine("TEXT 264,219,""0"",180,12,12,"":""")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPREMARKS As String = ""
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then TEMPREMARKS = DT.Rows(0).Item("REMARKS")

                            oWrite.WriteLine("TEXT 242,219,""0"",180,12,12,""" & TEMPREMARKS & """")
                            oWrite.WriteLine("PRINT 1,1")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                            oWrite.Dispose()

                        ElseIf ClientName = "SANGHVI" Then

                            oWrite.WriteLine("I8,A")
                            oWrite.WriteLine("ZN")
                            oWrite.WriteLine("q428")
                            oWrite.WriteLine("O")
                            oWrite.WriteLine("JF")
                            oWrite.WriteLine("ZT")
                            oWrite.WriteLine("Q400,25")
                            oWrite.WriteLine("N")
                            oWrite.WriteLine("A392,273,2,4,1,1,N,""COLOR""")
                            oWrite.WriteLine("A392,231,2,4,1,1,N,""RATE""")
                            oWrite.WriteLine("A306,273,2,4,1,1,N,"":""")
                            oWrite.WriteLine("A306,231,2,4,1,1,N,"":""")
                            If ClientName = "SANGHVI" Then oWrite.WriteLine("A368,355,2,4,1,1,N,""TINU MINU EMBROIDERY""")
                            oWrite.WriteLine("A157,315,2,4,1,1,N,""WIDTH""")
                            oWrite.WriteLine("A283,273,2,4,1,1,N,""" & CMBCOLOR.Text.Trim & """")


                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String = ""
                            Dim TEMPRATE As Double = 0
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                            'GET RATE
                            Dim WHERECLAUSE As String = ""
                            If CMBDESIGNNO.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(DESIGNMASTER.DESIGN_NO,'') = '" & CMBDESIGNNO.Text.Trim & "'"
                            If CMBCOLOR.Text.Trim <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(COLORMASTER.COLOR_NAME,'') = '" & CMBCOLOR.Text.Trim & "'"
                            DT = OBJCMN.search("PRICELISTMASTER.PL_RATE AS SALERATE ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON PRICELISTMASTER.PL_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON PRICELISTMASTER.PL_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON PRICELISTMASTER.PL_QUALITYID = QUALITYMASTER.QUALITY_id ", " AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & CMBMERCHANT.Text.Trim & "'" & WHERECLAUSE & " AND PL_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then TEMPRATE = Val(DT.Rows(0).Item("SALERATE"))


                            oWrite.WriteLine("A283,231,2,4,1,1,N,""" & Format(Val(TEMPRATE), "0.00") & """")
                            oWrite.WriteLine("A57,315,2,4,1,1,N,""" & TEMPWIDTH & """")
                            oWrite.WriteLine("A72,315,2,4,1,1,N,"":""")
                            oWrite.WriteLine("B396,190,2,1,2,4,63,N,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("A319,121,2,4,1,1,N,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("A392,315,2,4,1,1,N,""D.NO""")
                            oWrite.WriteLine("A306,315,2,4,1,1,N,"":""")
                            oWrite.WriteLine("A283,315,2,4,1,1,N,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("A151,155,2,4,1,1,N,""" & TXTREMARKS.Text.Trim & """")
                            oWrite.WriteLine("P1")
                            oWrite.Dispose()

                        ElseIf ClientName = "SHREENAKODA" Then

                            oWrite.WriteLine("I8,A")
                            oWrite.WriteLine("ZN")
                            oWrite.WriteLine("q380")
                            oWrite.WriteLine("O")
                            oWrite.WriteLine("JF")
                            oWrite.WriteLine("ZT")
                            oWrite.WriteLine("Q400,B25")
                            oWrite.WriteLine("KI80")
                            oWrite.WriteLine("N")

                            'GET PACKINGTYPE FROM PARTYMASTER AND SHOW HERE, WHEN WEAVERNAME IS PRESENT
                            Dim TEMPPACKINGTYPE As String = ""
                            Dim TEMPWIDTH As String = ""
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search(" ISNULL(PACKINGTYPE_NAME, '') AS PACKINGTYPE ", "", " LEDGERS LEFT OUTER JOIN PACKINGTYPEMASTER ON LEDGERS.ACC_PACKINGTYPEID = PACKINGTYPEMASTER.PACKINGTYPE_id ", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then
                                TEMPPACKINGTYPE = DT.Rows(0).Item("PACKINGTYPE")
                            End If

                            oWrite.WriteLine("A238,397,2,4,2,2,N,""" & TEMPPACKINGTYPE & """")
                            oWrite.WriteLine("LO20,324,339,3")
                            oWrite.WriteLine("A364,314,2,4,1,1,N,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("A364,256,2,4,1,1,N,""D.NO""")
                            oWrite.WriteLine("A279,256,2,4,1,1,N,"":""")
                            oWrite.WriteLine("A253,256,2,4,1,1,N,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("b23,37,Q,m2,s5,eL,iA,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("A364,192,2,4,1,1,N,""SERIES""")
                            oWrite.WriteLine("A279,192,2,4,1,1,N,"":""")
                            oWrite.WriteLine("A253,192,2,4,1,1,N,""" & TXTREMARKS.Text.Trim & """")
                            oWrite.WriteLine("A364,129,2,4,1,1,N,""SHADE""")
                            oWrite.WriteLine("A279,129,2,4,1,1,N,"":""")
                            oWrite.WriteLine("A253,129,2,4,1,1,N,""" & CMBCOLOR.Text.Trim & """")
                            oWrite.WriteLine("A364,66,2,4,1,1,N,""WIDTH""")
                            oWrite.WriteLine("A279,66,2,4,1,1,N,"":""")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            DT = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then
                                TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                            End If
                            oWrite.WriteLine("A253,66,2,4,1,1,N,""" & TEMPWIDTH & """")
                            oWrite.WriteLine("P1")
                            oWrite.Dispose()

                        ElseIf ClientName = "SNCM" Then

                            oWrite.WriteLine("SIZE 72.5 mm, 50 mm")
                            oWrite.WriteLine("GAP 3 mm, 0 mm")
                            oWrite.WriteLine("SPEED 5")
                            oWrite.WriteLine("DENSITY 10")
                            oWrite.WriteLine("DIRECTION 0,0")
                            oWrite.WriteLine("REFERENCE 0,0")
                            oWrite.WriteLine("OFFSET 0 mm")
                            oWrite.WriteLine("SET PEEL OFFT")
                            oWrite.WriteLine("SET CUTTER OFF")
                            oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                            oWrite.WriteLine("SET TEAR ON")
                            oWrite.WriteLine("ON")
                            oWrite.WriteLine("CLS")
                            oWrite.WriteLine("CODEPAGE 1252")
                            oWrite.WriteLine("TEXT 557,267,""0"",180,16,28,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("BAR 45,193, 512, 4")
                            oWrite.WriteLine("TEXT 557,175,""ROMAN.TTF"",180,1,12,""D. NO""")
                            oWrite.WriteLine("TEXT 425,175,""ROMAN.TTF"",180,1,12,"":""")
                            oWrite.WriteLine("TEXT 404,175,""ROMAN.TTF"",180,1,12,""" & CMBDESIGNNO.Text.Trim & """")


                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String = ""
                            Dim TEMPREMARKS As String = ""
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS,'') AS REMARKS ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                            oWrite.WriteLine("TEXT 557,113,""ROMAN.TTF"",180,1,12,""WIDTH""")
                            oWrite.WriteLine("TEXT 425,113,""ROMAN.TTF"",180,1,12,"":""")
                            oWrite.WriteLine("TEXT 404,113,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """")
                            oWrite.WriteLine("TEXT 557,52,""ROMAN.TTF"",180,1,12,""QLTY""")
                            oWrite.WriteLine("TEXT 425,52,""ROMAN.TTF"",180,1,12,"":""")
                            oWrite.WriteLine("TEXT 404,52,""ROMAN.TTF"",180,1,12,""" & TEMPREMARKS & """")

                            oWrite.WriteLine("QRCODE 167,175,L,5,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("PRINT 1,1")
                            oWrite.Dispose()

                        ElseIf ClientName = "SONU" Then

                            oWrite.WriteLine("<xpml><page quantity='0' pitch='45.0 mm'></xpml>I8,A")
                            oWrite.WriteLine("ZN")
                            oWrite.WriteLine("q600")
                            oWrite.WriteLine("O")
                            oWrite.WriteLine("JF")
                            oWrite.WriteLine("ZT")
                            oWrite.WriteLine("Q360,B25")
                            oWrite.WriteLine("KI80")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='45.0 mm'></xpml>N")
                            oWrite.WriteLine("B590,133,2,1,2,4,82,N,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("A510,42,2,4,1,1,N,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("A590,345,2,3,2,2,N,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("A590,288,2,2,2,2,N,""D.NO""")
                            oWrite.WriteLine("A590,237,2,2,2,2,N,""SHADE""")
                            oWrite.WriteLine("A590,186,2,2,2,2,N,""WIDTH""")
                            oWrite.WriteLine("A464,288,2,2,2,2,N,"":""")
                            oWrite.WriteLine("A464,237,2,2,2,2,N,"":""")
                            oWrite.WriteLine("A464,186,2,2,2,2,N,"":""")
                            oWrite.WriteLine("A439,288,2,2,2,2,N,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("A439,242,2,3,2,2,N,""" & CMBCOLOR.Text.Trim & """")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then
                                TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                            End If

                            oWrite.WriteLine("A439,186,2,2,2,2,N,""" & TEMPWIDTH & """")
                            oWrite.WriteLine("P1")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                            oWrite.Dispose()



                        ElseIf ClientName = "SPCORP" Then

                            oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>I8,A")
                            oWrite.WriteLine("ZN")
                            oWrite.WriteLine("q380")
                            oWrite.WriteLine("O")
                            oWrite.WriteLine("JF")
                            oWrite.WriteLine("ZT")
                            oWrite.WriteLine("Q400,25")
                            oWrite.WriteLine("KI80")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>N")
                            oWrite.WriteLine("A362,287,2,4,1,1,N,""" & CMBMERCHANT.Text.Trim & """")
                            oWrite.WriteLine("LO12,243,355,3")
                            oWrite.WriteLine("A362,222,2,1,2,2,N,""" & CMBDESIGNNO.Text.Trim & """")
                            oWrite.WriteLine("B367,119,2,1,2,4,71,N,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("A293,42,2,4,1,1,N,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                            oWrite.WriteLine("A362,171,2,4,1,1,N,""WIDTH""")
                            oWrite.WriteLine("A266,171,2,4,1,1,N,"":""")

                            'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                            Dim TEMPWIDTH As String
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then
                                TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                            End If

                            oWrite.WriteLine("A241,171,2,4,1,1,N,""" & TEMPWIDTH & """")
                            'oWrite.WriteLine("A279,385,2,5,1,1,N,""SEHCO""")
                            oWrite.WriteLine("LO12,297,355,3")
                            oWrite.WriteLine("P1")
                            oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                            oWrite.Dispose()

                        ElseIf ClientName = "SUPRIYA" Then

                            'FOR SAMELE STICKER
                            If TEMPHEADER = "1" Then
                                oWrite.WriteLine("G0")
                                oWrite.WriteLine("n")
                                oWrite.WriteLine("M0500")
                                oWrite.WriteLine("O0214")
                                oWrite.WriteLine("V0")
                                oWrite.WriteLine("t1")
                                oWrite.WriteLine("Kf0070")
                                oWrite.WriteLine("L")
                                oWrite.WriteLine("D11")
                                oWrite.WriteLine("A2")
                                oWrite.WriteLine("1W1D66000003301172,LA," & TXTBARCODE.Text.Trim)
                                oWrite.WriteLine("ySPM")
                                oWrite.WriteLine("1911A0600200122" & TXTBARCODE.Text.Trim)
                                oWrite.WriteLine("1911C0801280010" & CmpName)
                                oWrite.WriteLine("1911A1000990010" & CMBMERCHANT.Text.Trim)
                                oWrite.WriteLine("1911A0800820010D.NO")
                                oWrite.WriteLine("1911A0800820048:")
                                oWrite.WriteLine("1911A1000800056" & CMBDESIGNNO.Text.Trim)
                                oWrite.WriteLine("1911A0800640010S.NO")
                                oWrite.WriteLine("1911A0800640048:")
                                oWrite.WriteLine("1911A0800640056" & CMBCOLOR.Text.Trim)
                                oWrite.WriteLine("1911A0800450010WIDTH")
                                oWrite.WriteLine("1911A0800450048:")


                                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                Dim TEMPWIDTH As String = ""
                                Dim TEMPREMARKS As String = ""
                                Dim OBJCMN As New ClsCommon
                                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                                If DT.Rows.Count > 0 Then
                                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                End If

                                oWrite.WriteLine("1911A0800450056" & TEMPWIDTH)
                                oWrite.WriteLine("1X1100001240006L176001")
                                oWrite.WriteLine("1911A0800280010" & TEMPREMARKS)
                                If CmpName = "SUPRIYA SILK INDUSTRIES" Then oWrite.WriteLine("1911C0800040061DEEP BLUE") Else oWrite.WriteLine("1911C0800040061ROYAL TEX")
                                oWrite.WriteLine("Q0001")
                                oWrite.WriteLine("E")
                                oWrite.Dispose()

                            Else
                                'FOR REGISTER STICKER
                                oWrite.WriteLine("<xpml><page quantity='0' pitch='22.0 mm'></xpml>G0")
                                oWrite.WriteLine("n")
                                oWrite.WriteLine("M0500")
                                oWrite.WriteLine("O0214")
                                oWrite.WriteLine("V0")
                                oWrite.WriteLine("t1")
                                oWrite.WriteLine("Kf0070")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='22.0 mm'></xpml>L")
                                oWrite.WriteLine("D11")
                                oWrite.WriteLine("A2")
                                oWrite.WriteLine("1W1D44000002101352,LA," & TXTBARCODE.Text.Trim)
                                oWrite.WriteLine("ySPM")
                                oWrite.WriteLine("1911A0600100130" & TXTBARCODE.Text.Trim)
                                oWrite.WriteLine("1911C1000670006" & CMBMERCHANT.Text.Trim)
                                oWrite.WriteLine("1911C1000490006D.NO")
                                oWrite.WriteLine("1911C1000490044:")
                                oWrite.WriteLine("1911C1000490050" & CMBDESIGNNO.Text.Trim)
                                oWrite.WriteLine("1911C1000330006S.NO")
                                oWrite.WriteLine("1911C1000330044:")
                                oWrite.WriteLine("1911C1000330050" & CMBCOLOR.Text.Trim)
                                oWrite.WriteLine("Q0001")
                                oWrite.WriteLine("E")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                oWrite.Dispose()
                            End If


                        ElseIf ClientName = "YASHVI" Then

                            If TEMPHEADER = "1" Then
                                oWrite.WriteLine("SIZE 72.5 mm, 50 mm")
                                oWrite.WriteLine("GAP 3 mm, 0 mm")
                                oWrite.WriteLine("DIRECTION 0,0")
                                oWrite.WriteLine("REFERENCE 0,0")
                                oWrite.WriteLine("OFFSET 0 mm")
                                oWrite.WriteLine("SET PEEL OFFT")
                                oWrite.WriteLine("SET CUTTER OFF")
                                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                oWrite.WriteLine("SET TEAR ON")
                                oWrite.WriteLine("CLS")
                                oWrite.WriteLine("CODEPAGE 1252")
                                oWrite.WriteLine("TEXT 495,252,""ROMAN.TTF"",180,1,11,""QUALITY""")
                                oWrite.WriteLine("TEXT 495,212,""ROMAN.TTF"",180,1,11,""DESIGN""")
                                oWrite.WriteLine("TEXT 495,129,""ROMAN.TTF"",180,1,11,""WIDTH""")
                                oWrite.WriteLine("TEXT 374,252,""ROMAN.TTF"",180,1,11,"":""")
                                oWrite.WriteLine("TEXT 374,212,""ROMAN.TTF"",180,1,11,"":""")
                                oWrite.WriteLine("TEXT 374,129,""ROMAN.TTF"",180,1,11,"":""")
                                oWrite.WriteLine("TEXT 354,252,""ROMAN.TTF"",180,1,11,""" & CMBMERCHANT.Text.Trim & """")
                                oWrite.WriteLine("TEXT 354,212,""ROMAN.TTF"",180,1,11,""" & CMBDESIGNNO.Text.Trim & """")

                                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                Dim TEMPWIDTH As String = ""
                                Dim TEMPREMARKS As String = ""
                                Dim OBJCMN As New ClsCommon
                                Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                                If DT.Rows.Count > 0 Then
                                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                End If

                                oWrite.WriteLine("TEXT 354,129,""ROMAN.TTF"",180,1,11,""" & TEMPWIDTH & """")
                                oWrite.WriteLine("TEXT 495,83,""ROMAN.TTF"",180,1,9,""" & TEMPREMARKS & """")
                                oWrite.WriteLine("QRCODE 161,169,L,7,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("TEXT 495,43,""ROMAN.TTF"",180,1,9,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("TEXT 495,170,""ROMAN.TTF"",180,1,11,""SHADE""")
                                oWrite.WriteLine("TEXT 374,170,""ROMAN.TTF"",180,1,11,"":""")
                                oWrite.WriteLine("TEXT 354,170,""ROMAN.TTF"",180,1,11,""" & CMBCOLOR.Text.Trim & """")
                                oWrite.WriteLine("PRINT 1,1")
                                oWrite.Dispose()

                            ElseIf TEMPHEADER = "2" Then

                                oWrite.WriteLine("<xpml><page quantity='0' pitch='100.1 mm'></xpml>SIZE 99.10 mm, 100.1 mm")
                                oWrite.WriteLine("GAP 3 mm, 0 mm")
                                oWrite.WriteLine("DIRECTION 0,0")
                                oWrite.WriteLine("REFERENCE 0,0")
                                oWrite.WriteLine("OFFSET 0 mm")
                                oWrite.WriteLine("SET PEEL OFF")
                                oWrite.WriteLine("SET CUTTER OFF")
                                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='100.1 mm'></xpml>SET TEAR ON")
                                oWrite.WriteLine("CLS")
                                oWrite.WriteLine("CODEPAGE 1252")
                                oWrite.WriteLine("TEXT 710,591,""0"",180,16,16,""ITEM""")
                                oWrite.WriteLine("TEXT 710,532,""0"",180,16,16,""DESIGN""")
                                oWrite.WriteLine("TEXT 710,465,""0"",180,16,16,""WIDTH""")
                                oWrite.WriteLine("TEXT 555,591,""0"",180,16,16,"":""")
                                oWrite.WriteLine("TEXT 555,532,""0"",180,16,16,"":""")
                                oWrite.WriteLine("TEXT 555,465,""0"",180,16,16,"":""")
                                oWrite.WriteLine("TEXT 525,591,""0"",180,16,16,""" & CMBMERCHANT.Text.Trim & """")
                                oWrite.WriteLine("TEXT 525,532,""0"",180,16,16,""" & CMBDESIGNNO.Text.Trim & """")

                                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
                                Dim OBJCMN As New ClsCommon
                                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & CMBMERCHANT.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                                If DT.Rows.Count > 0 Then
                                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                                    TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                End If

                                oWrite.WriteLine("TEXT 525,465,""0"",180,16,16,""" & TEMPWIDTH & """")
                                oWrite.WriteLine("QRCODE 281,521,L,10,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("TEXT 276,306,""0"",180,10,10,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("TEXT 583,265,""0"",180,14,14,""" & TEMPREMARKS & """")


                                oWrite.WriteLine("TEXT 515,177,""0"",180,11,11,""ITEM""")
                                oWrite.WriteLine("TEXT 515,137,""0"",180,11,11,""DESIGN""")
                                oWrite.WriteLine("TEXT 409,177,""0"",180,11,11,"":""")
                                oWrite.WriteLine("TEXT 409,137,""0"",180,11,11,"":""")
                                oWrite.WriteLine("TEXT 390,177,""0"",180,11,11,""" & CMBMERCHANT.Text.Trim & """")
                                oWrite.WriteLine("TEXT 390,137,""0"",180,11,11,""" & CMBDESIGNNO.Text.Trim & """")
                                oWrite.WriteLine("QRCODE 216,127,L,5,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("TEXT 91,26,""0"",90,8,8,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("PRINT 1,1")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                oWrite.Dispose()


                            Else
                                oWrite.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 57.5 mm, 15 mm")
                                oWrite.WriteLine("GAP 3 mm, 0 mm")
                                oWrite.WriteLine("DIRECTION 0,0")
                                oWrite.WriteLine("REFERENCE 0,0")
                                oWrite.WriteLine("OFFSET 0 mm")
                                oWrite.WriteLine("SET PEEL OFFT")
                                oWrite.WriteLine("SET CUTTER OFF")
                                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>SET TEAR ON")
                                oWrite.WriteLine("ON")
                                oWrite.WriteLine("CLS")
                                oWrite.WriteLine("CODEPAGE 1252")
                                oWrite.WriteLine("TEXT 448,66,""0"",180,18,18,""" & CMBCOLOR.Text.Trim & """")
                                oWrite.WriteLine("QRCODE 381,100,L,3,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("TEXT 445,24,""0"",180,5,5,""" & CMBMERCHANT.Text.Trim & """")
                                oWrite.WriteLine("TEXT 376,24,""0"",180,5,5,""" & CMBDESIGNNO.Text.Trim & """")

                                oWrite.WriteLine("TEXT 295,66,""0"",180,19,18,""" & CMBCOLOR.Text.Trim & """")
                                oWrite.WriteLine("QRCODE 227,100,L,3,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("TEXT 295,24,""0"",180,5,5,""" & CMBMERCHANT.Text.Trim & """")
                                oWrite.WriteLine("TEXT 227,24,""0"",180,5,5,""" & CMBDESIGNNO.Text.Trim & """")

                                oWrite.WriteLine("TEXT 138,66,""0"",180,18,18,""" & CMBCOLOR.Text.Trim & """")
                                oWrite.WriteLine("QRCODE 70,100,L,3,A,180,M2,S7,""" & TXTBARCODE.Text.Trim & """") 'BARCODE
                                oWrite.WriteLine("TEXT 138,24,""0"",180,5,5,""" & CMBMERCHANT.Text.Trim & """")
                                oWrite.WriteLine("TEXT 70,24,""0"",180,5,5,""" & CMBDESIGNNO.Text.Trim & """")
                                oWrite.WriteLine("PRINT 1,1")
                                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                oWrite.Dispose()
                            End If


                        End If

                        'Printing Barcode
                        Dim psi As New ProcessStartInfo()
                        psi.FileName = "cmd.exe"
                        psi.RedirectStandardInput = False
                        psi.RedirectStandardOutput = True
                        'psi.Arguments = "/c print " & Application.StartupPath & "\Barcode.txt"    ' specify your command
                        psi.Arguments = "/c print D:\Barcode.txt"    ' specify your command
                        psi.UseShellExecute = False

                        Dim proc As Process
                        proc = Process.Start(psi)
                        dirresults = proc.StandardOutput.ReadToEnd() ' // read from stdout
                        '// do something with result stream
                        proc.WaitForExit()
                        proc.Dispose()

                    Next

                Else
                    If (Val(TXTTO.Text.Trim) > 0 And Val(TXTFROM.Text.Trim) > 0) Or CHKPRINTSELECTED.CheckState = CheckState.Checked Then
                        If CHKPRINTSELECTED.Checked = False Then
                            If (Val(TXTTO.Text.Trim) < Val(TXTFROM.Text.Trim)) Or (Val(TXTFROM.Text.Trim) > gridbill.RowCount) Or (Val(TXTTO.Text.Trim) > gridbill.RowCount) Then
                                MsgBox("Invalid No Entered", MsgBoxStyle.Critical)
                                TXTFROM.Focus()
                                Exit Sub
                            End If
                        End If



                        If CHKPRINTSELECTED.Checked = True Then
                            TXTFROM.Text = 1
                            TXTTO.Text = gridbill.RowCount
                        End If

                        For i As Integer = Val(TXTFROM.Text.Trim) To Val(TXTTO.Text.Trim)

                            For J As Integer = 1 To Val(TXTCOPIES.Text.Trim)


                                Dim ROW As DataRow = gridbill.GetDataRow(i - 1)

                                If CHKPRINTSELECTED.CheckState = CheckState.Checked And Convert.ToBoolean(ROW("CHK")) = False Then GoTo NEXTLINE
                                If Convert.ToBoolean(ROW("ITEMBLOCKED")) = True Or Convert.ToBoolean(ROW("DESIGNBLOCKED")) = True Or Convert.ToBoolean(ROW("COLORBLOCKED")) = True Then GoTo NEXTLINE

                                Dim dirresults As String = ""
                                'Writing in file
                                Dim oWrite As System.IO.StreamWriter
                                oWrite = File.CreateText(Application.StartupPath & "\Barcode.txt")

                                If ClientName = "ANOX" Then

                                    oWrite.WriteLine("CT~~CD,~CC^~CT~")
                                    oWrite.WriteLine("^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR6,6~SD10^JUS^LRN^CI0^XZ")
                                    oWrite.WriteLine("^XA")
                                    oWrite.WriteLine("^MMT")
                                    oWrite.WriteLine("^PW400")
                                    oWrite.WriteLine("^LL0200")
                                    oWrite.WriteLine("^LS0")
                                    oWrite.WriteLine("^FT95,50^A0N,45,52^FH\^FD" & ROW("ITEMNAME") & "^FS")
                                    oWrite.WriteLine("^FT27,94^A0N,39,40^FH\^FDD. NO. :^FS")
                                    oWrite.WriteLine("^FT173,94^A0N,39,43^FH\^FD" & ROW("DESIGNNO") & "^FS")
                                    oWrite.WriteLine("^BY1,3,51^FT92,156^BCN,,N,N")
                                    oWrite.WriteLine("^FD>:" & ROW("BARCODE") & "^FS")
                                    oWrite.WriteLine("^FT24,178^A0N,23,24^FH\^FDE-mail - anoxlifestyle@gmail.com^FS")
                                    oWrite.WriteLine("^FT330,94^A0N,39,38^FH\^FD" & ROW("SHADE") & "^FS")
                                    oWrite.WriteLine("^FT309,94^A0N,39,38^FH\^FD/^FS")
                                    oWrite.WriteLine("^PQ1,0,1,Y^XZ")
                                    oWrite.Dispose()

                                ElseIf ClientName = "AVIS" Then

                                    oWrite.WriteLine("n")
                                    oWrite.WriteLine("M0500")
                                    oWrite.WriteLine("O0214")
                                    oWrite.WriteLine("V0")
                                    oWrite.WriteLine("t1")
                                    oWrite.WriteLine("Kf0070")
                                    oWrite.WriteLine("L")
                                    oWrite.WriteLine("D11")
                                    oWrite.WriteLine("ySPM")
                                    oWrite.WriteLine("A2")
                                    oWrite.WriteLine("1911A1201200006" & ROW("ITEMNAME"))
                                    oWrite.WriteLine("1911A1200930006" & ROW("DESIGNNO"))
                                    oWrite.WriteLine("1W1D66000002601092,LA," & ROW("BARCODE"))
                                    oWrite.WriteLine("1911A1200660006WIDTH:")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String = ""
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("BARCODE") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                                    oWrite.WriteLine("1911A1200660071" & TEMPWIDTH)
                                    oWrite.WriteLine("1911A0800330006" & ROW("BARCODE"))
                                    oWrite.WriteLine("Q0001")
                                    oWrite.WriteLine("E")
                                    oWrite.Dispose()

                                ElseIf ClientName = "GELATO" Then

                                    oWrite.WriteLine("<xpml><page quantity='0' pitch='70.1 mm'></xpml>G0")
                                    oWrite.WriteLine("n")
                                    oWrite.WriteLine("M0690")
                                    oWrite.WriteLine("O0214")
                                    oWrite.WriteLine("V0")
                                    oWrite.WriteLine("t1")
                                    oWrite.WriteLine("Kf0070")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='70.1 mm'></xpml>L")
                                    oWrite.WriteLine("D11")
                                    oWrite.WriteLine("ySPM")
                                    oWrite.WriteLine("A2")
                                    oWrite.WriteLine("4911C1400060021D.NO")
                                    oWrite.WriteLine("4911C1400750021:")
                                    oWrite.WriteLine("4911C1400930021" & ROW("DESIGNNO"))

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPMRP As Double = 0
                                    Dim TEMPWSP As Double = 0
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.search(" ISNULL(DESIGNMASTER.DESIGN_SALERATE, 0) AS MRP, ISNULL(DESIGNMASTER.DESIGN_WRATE,0) AS WSP", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & ROW("DESIGNNO") & "' AND DESIGN_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then
                                        TEMPMRP = Val(DT.Rows(0).Item("MRP"))
                                        TEMPWSP = Val(DT.Rows(0).Item("WSP"))
                                    End If

                                    oWrite.WriteLine("4911C1400060065STYLE")
                                    oWrite.WriteLine("4911C1400750065:")
                                    oWrite.WriteLine("4911C1400930065" & ROW("ITEMNAME"))
                                    oWrite.WriteLine("4911C1400060041SIZE")
                                    oWrite.WriteLine("4911C1400750041:")
                                    oWrite.WriteLine("4911C1400930041" & ROW("SHADE"))
                                    oWrite.WriteLine("4911C1400060089QTY")
                                    oWrite.WriteLine("4911C1400750089:")
                                    oWrite.WriteLine("4911C14009300891")

                                    If TEMPHEADER = "2" Then
                                        oWrite.WriteLine("4911C1401290091MRP")
                                        oWrite.WriteLine("4911C1401750091:")
                                        oWrite.WriteLine("4911C1401900091" & Val(TEMPMRP))
                                        oWrite.WriteLine("4911C0801700106(BOYS / KIDS)")
                                    ElseIf TEMPHEADER = "3" Then
                                        oWrite.WriteLine("4911C1401290091WSP")
                                        oWrite.WriteLine("4911C1401750091:")
                                        oWrite.WriteLine("4911C1401900091" & Val(TEMPWSP))
                                        oWrite.WriteLine("4911C0801700106(BOYS / KIDS)")
                                    End If

                                    oWrite.WriteLine("4e4203200090140B" & ROW("BARCODE"))
                                    oWrite.WriteLine("4911A0800100109" & ROW("BARCODE"))
                                    oWrite.WriteLine("Q0001")
                                    oWrite.WriteLine("E")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                    oWrite.Dispose()


                                ElseIf ClientName = "INDRAPUJAFABRICS" Then

                                    oWrite.WriteLine("SIZE 80.1 mm, 50 mm")
                                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                                    oWrite.WriteLine("DIRECTION 0,0")
                                    oWrite.WriteLine("REFERENCE 0,0")
                                    oWrite.WriteLine("OFFSET 0 mm")
                                    oWrite.WriteLine("SET PEEL OFFT")
                                    oWrite.WriteLine("SET CUTTER OFF")
                                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                    oWrite.WriteLine("SET TEAR ON")
                                    oWrite.WriteLine("ON")
                                    oWrite.WriteLine("CLS")
                                    oWrite.WriteLine("CODEPAGE 1252")
                                    oWrite.WriteLine("TEXT 614,379,""ROMAN.TTF"",180,1,18,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("BAR 23,317, 596, 3")
                                    oWrite.WriteLine("TEXT 614,301,""ROMAN.TTF"",180,1,18,""D.NO""")
                                    oWrite.WriteLine("TEXT 439,301,""ROMAN.TTF"",180,1,18,"":""")
                                    oWrite.WriteLine("TEXT 411,301,""ROMAN.TTF"",180,1,18,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("TEXT 614,237,""ROMAN.TTF"",180,1,18,""SHADE""")
                                    oWrite.WriteLine("TEXT 439,237,""ROMAN.TTF"",180,1,18,"":""")
                                    oWrite.WriteLine("TEXT 411,237,""ROMAN.TTF"",180,1,18,""" & ROW("SHADE") & """")
                                    oWrite.WriteLine("TEXT 614,167,""ROMAN.TTF"",180,1,18,""WIDTH""")
                                    oWrite.WriteLine("TEXT 439,167,""ROMAN.TTF"",180,1,18,"":""")


                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String = ""
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                                    oWrite.WriteLine("TEXT 411,167,""ROMAN.TTF"",180,1,18,""" & TEMPWIDTH & """")
                                    oWrite.WriteLine("BARCODE 619,99,""128M"",59,0,180,3,6,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("TEXT 401,34,""ROMAN.TTF"",180,1,8,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("PRINT 1,1")
                                    oWrite.Dispose()


                                ElseIf ClientName = "INDRAPUJAIMPEX" Then

                                    oWrite.WriteLine("<xpml><page quantity='0' pitch='38.0 mm'></xpml>SIZE 37.10 mm, 38 mm")
                                    oWrite.WriteLine("DIRECTION 0,0")
                                    oWrite.WriteLine("REFERENCE 0,0")
                                    oWrite.WriteLine("OFFSET 0 mm")
                                    oWrite.WriteLine("SET PEEL OFFT")
                                    oWrite.WriteLine("SET CUTTER OFF")
                                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='38.0 mm'></xpml>SET TEAR ON")
                                    oWrite.WriteLine("CLS")
                                    oWrite.WriteLine("CODEPAGE 1252")
                                    oWrite.WriteLine("TEXT 283,288,""ROMAN.TTF"",180,1,16,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("TEXT 289,197,""ROMAN.TTF"",180,1,16,""D.NO""")
                                    oWrite.WriteLine("BAR 7,239, 276, 3")
                                    oWrite.WriteLine("TEXT 191,197,""ROMAN.TTF"",180,1,16,"":""")
                                    oWrite.WriteLine("TEXT 172,197,""ROMAN.TTF"",180,1,16,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("TEXT 216,233,""ROMAN.TTF"",180,1,8,""" & ROW("REMARKS") & """")
                                    oWrite.WriteLine("BARCODE 285,99,""128M"",62,0,180,2,4,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("TEXT 195,32,""ROMAN.TTF"",180,1,8,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("TEXT 289,140,""ROMAN.TTF"",180,1,12,""WIDTH""")
                                    oWrite.WriteLine("TEXT 191,145,""ROMAN.TTF"",180,1,14,"":""")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String = ""
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                                    oWrite.WriteLine("TEXT 172,139,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")
                                    oWrite.WriteLine("PRINT 1,1")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                    oWrite.Dispose()



                                ElseIf ClientName = "KCRAYON" Then

                                    oWrite.WriteLine("SIZE 47.5 mm, 50 mm")
                                    oWrite.WriteLine("DIRECTION 0,0")
                                    oWrite.WriteLine("REFERENCE 0,0")
                                    oWrite.WriteLine("OFFSET 0 mm")
                                    oWrite.WriteLine("SET PEEL OFFT")
                                    oWrite.WriteLine("SET CUTTER OFF")
                                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                    oWrite.WriteLine("SET TEAR ON")
                                    oWrite.WriteLine("CLS")
                                    oWrite.WriteLine("CODEPAGE 1252")
                                    oWrite.WriteLine("TEXT 373,244,""ROMAN.TTF"",180,1,18,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("TEXT 373,174,""ROMAN.TTF"",180,1,12,""SHADE""")
                                    oWrite.WriteLine("TEXT 275,174,""ROMAN.TTF"",180,1,12,"":""")
                                    oWrite.WriteLine("TEXT 259,174,""ROMAN.TTF"",180,1,12,""" & ROW("SHADE") & """")
                                    oWrite.WriteLine("TEXT 373,113,""ROMAN.TTF"",180,1,12,""WIDTH""")
                                    oWrite.WriteLine("TEXT 275,113,""ROMAN.TTF"",180,1,12,"":""")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String = ""
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                                    oWrite.WriteLine("TEXT 259,113,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """")
                                    oWrite.WriteLine("QRCODE 113,134,L,5,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("TEXT 373,52,""ROMAN.TTF"",180,1,12,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("PRINT 1,1")
                                    oWrite.Dispose()

                                ElseIf ClientName = "KARAN" Then

                                    oWrite.WriteLine("SIZE 47.5 mm, 25 mm")
                                    oWrite.WriteLine("GAP 0 mm, 0 mm")
                                    oWrite.WriteLine("DIRECTION 0,0")
                                    oWrite.WriteLine("REFERENCE 0,0")
                                    oWrite.WriteLine("OFFSET 0 mm")
                                    oWrite.WriteLine("SET PEEL OFFT")
                                    oWrite.WriteLine("SET CUTTER OFF")
                                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                    oWrite.WriteLine("SET TEAR ON")
                                    oWrite.WriteLine("CLS")
                                    oWrite.WriteLine("CODEPAGE 1252")
                                    oWrite.WriteLine("TEXT 367,180,""ROMAN.TTF"",180,1,9,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("TEXT 367,130,""ROMAN.TTF"",180,1,14,""D.NO""")
                                    oWrite.WriteLine("TEXT 271,130,""ROMAN.TTF"",180,1,14,"":""")
                                    oWrite.WriteLine("TEXT 258,130,""ROMAN.TTF"",180,1,14,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("TEXT 367,66,""ROMAN.TTF"",180,1,14,""S.NO""")
                                    oWrite.WriteLine("TEXT 271,66,""ROMAN.TTF"",180,1,14,"":""")
                                    oWrite.WriteLine("TEXT 258,66,""ROMAN.TTF"",180,1,14,""" & ROW("SHADE") & """")
                                    oWrite.WriteLine("PRINT 1,1")
                                    oWrite.Dispose()

                                ElseIf ClientName = "KDFAB" Then

                                    oWrite.WriteLine("I8,A")
                                    oWrite.WriteLine("ZN")
                                    oWrite.WriteLine("q401")
                                    oWrite.WriteLine("O")
                                    oWrite.WriteLine("JF")
                                    oWrite.WriteLine("ZT")
                                    oWrite.WriteLine("Q304,25")
                                    oWrite.WriteLine("N")
                                    oWrite.WriteLine("A379,200,2,1,2,2,N,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("A379,161,2,1,2,2,N,""WIDTH""")
                                    oWrite.WriteLine("A272,161,2,1,2,2,N,"":""")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String = ""
                                    Dim TEMPRATE As Double = 0
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    oWrite.WriteLine("A246,161,2,1,2,2,N,""" & TEMPWIDTH & """")

                                    oWrite.WriteLine("A379,126,2,1,2,2,N,""D.NO""")
                                    oWrite.WriteLine("A272,126,2,1,2,2,N,"":""")
                                    oWrite.WriteLine("A246,126,2,1,2,2,N,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("A379,91,2,1,2,2,N,""RATE""")
                                    oWrite.WriteLine("A272,91,2,1,2,2,N,"":""")


                                    If TEMPHEADER = "1" Then
                                        'GET RATE
                                        Dim WHERECLAUSE As String = ""
                                        'If ROW("DESIGNNO") <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(DESIGNMASTER.DESIGN_NO,'') = '" & ROW("DESIGNNO") & "'"
                                        'If ROW("SHADE") <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(COLORMASTER.COLOR_NAME,'') = '" & ROW("SHADE") & "'"
                                        DT = OBJCMN.search("PRICELISTMASTER.PL_RATE AS SALERATE ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON PRICELISTMASTER.PL_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON PRICELISTMASTER.PL_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON PRICELISTMASTER.PL_QUALITYID = QUALITYMASTER.QUALITY_id ", " AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & ROW("ITEMNAME") & "'" & WHERECLAUSE & " AND PL_YEARID = " & YearId)
                                        If DT.Rows.Count > 0 Then TEMPRATE = Val(DT.Rows(0).Item("SALERATE"))
                                        If ROW("REMARKS") = "" Then oWrite.WriteLine("A246,91,2,1,2,2,N,""" & Format(Val(TEMPRATE), "0.00") & """") Else oWrite.WriteLine("A246,91,2,1,2,2,N,""" & Format(Val(TEMPRATE), "0.00") & "  " & ROW("REMARKS") & """")
                                    End If


                                    oWrite.WriteLine("B380,57,2,1,2,4,37,N,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("A261,18,2,1,1,1,N,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("P1")
                                    oWrite.Dispose()


                                ElseIf ClientName = "KOTHARI" Then

                                    oWrite.WriteLine("<xpml><page quantity='0' pitch='38.0 mm'></xpml>I8,A")
                                    oWrite.WriteLine("ZN")
                                    oWrite.WriteLine("q636")
                                    oWrite.WriteLine("S3")
                                    oWrite.WriteLine("O")
                                    oWrite.WriteLine("JF")
                                    oWrite.WriteLine("KIZZQ0")
                                    oWrite.WriteLine("KI9+0.0")
                                    oWrite.WriteLine("D14")
                                    oWrite.WriteLine("ZT")
                                    oWrite.WriteLine("Q304,B25")
                                    oWrite.WriteLine("Arglabel 380 31")
                                    oWrite.WriteLine("exit")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='38.0 mm'></xpml>N")
                                    oWrite.WriteLine("b151,108,Q,m2,s5,eL,iA,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("A24,77,0,4,1,1,N,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("A24,165,0,4,1,1,N,""" & ROW("SHADE") & """")
                                    oWrite.WriteLine("A24,123,0,4,1,1,N,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("A24,211,0,2,1,1,N,""" & ROW("REMARKS") & """")
                                    oWrite.WriteLine("A265,225,3,1,1,1,N,""" & ROW("BARCODE") & """")
                                    oWrite.WriteLine("b487,108,Q,m2,s5,eL,iA,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("A360,77,0,4,1,1,N,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("A360,165,0,4,1,1,N,""" & ROW("SHADE") & """")
                                    oWrite.WriteLine("A360,123,0,4,1,1,N,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("A360,211,0,2,1,1,N,""" & ROW("REMARKS") & """")
                                    oWrite.WriteLine("A602,225,3,1,1,1,N,""" & ROW("BARCODE") & """")
                                    oWrite.WriteLine("P1")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                    oWrite.Dispose()


                                ElseIf ClientName = "KRFABRICS" Then

                                    oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                                    oWrite.WriteLine("DIRECTION 0,0")
                                    oWrite.WriteLine("REFERENCE 0,0")
                                    oWrite.WriteLine("OFFSET 0 mm")
                                    oWrite.WriteLine("SET PEEL OFF")
                                    oWrite.WriteLine("SET CUTTER OFF")
                                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                    oWrite.WriteLine("SET TEAR ON")
                                    oWrite.WriteLine("CLS")
                                    oWrite.WriteLine("CODEPAGE 1252")
                                    oWrite.WriteLine("TEXT 573,375,""ROMAN.TTF"",180,1,22,""" & CmpName & """")
                                    oWrite.WriteLine("BAR 204,318, 369, 3")
                                    oWrite.WriteLine("TEXT 746,286,""ROMAN.TTF"",180,1,14,""QUALITY""")
                                    oWrite.WriteLine("TEXT 578,286,""ROMAN.TTF"",180,1,14,"":""")
                                    oWrite.WriteLine("TEXT 551,286,""ROMAN.TTF"",180,1,14,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("TEXT 746,231,""ROMAN.TTF"",180,1,14,""D.NO""")
                                    oWrite.WriteLine("TEXT 578,231,""ROMAN.TTF"",180,1,14,"":""")
                                    oWrite.WriteLine("TEXT 551,231,""ROMAN.TTF"",180,1,14,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("TEXT 746,174,""ROMAN.TTF"",180,1,14,""SHADE""")
                                    oWrite.WriteLine("TEXT 578,174,""ROMAN.TTF"",180,1,14,"":""")
                                    oWrite.WriteLine("TEXT 551,174,""ROMAN.TTF"",180,1,14,""" & ROW("SHADE") & """")
                                    oWrite.WriteLine("TEXT 746,123,""ROMAN.TTF"",180,1,14,""SERIES""")
                                    oWrite.WriteLine("TEXT 578,123,""ROMAN.TTF"",180,1,14,"":""")
                                    oWrite.WriteLine("TEXT 551,128,""ROMAN.TTF"",180,1,18,""" & ROW("REMARKS") & """")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String = ""
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then
                                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    End If

                                    oWrite.WriteLine("TEXT 746,68,""ROMAN.TTF"",180,1,14,""WIDTH""")
                                    oWrite.WriteLine("TEXT 578,68,""ROMAN.TTF"",180,1,14,"":""")
                                    oWrite.WriteLine("TEXT 551,68,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")
                                    oWrite.WriteLine("QRCODE 248,231,L,9,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("TEXT 248,39,""ROMAN.TTF"",180,1,9,""" & ROW("BARCODE") & """")
                                    oWrite.WriteLine("PRINT 1,1")
                                    oWrite.Dispose()


                                ElseIf ClientName = "KRISHNA" Then

                                    oWrite.WriteLine("SIZE 72.4 mm, 36.2 mm")
                                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                                    oWrite.WriteLine("DIRECTION 0,0")
                                    oWrite.WriteLine("REFERENCE 0,0")
                                    oWrite.WriteLine("OFFSET 0 mm")
                                    oWrite.WriteLine("SET PEEL OFF")
                                    oWrite.WriteLine("SET CUTTER OFF")
                                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                    oWrite.WriteLine("SET TEAR ON")
                                    oWrite.WriteLine("CLS")
                                    oWrite.WriteLine("CODEPAGE 1252")
                                    oWrite.WriteLine("TEXT 392,279,""ROMAN.TTF"",180,1,16,""SUNWARE""")
                                    oWrite.WriteLine("BAR 186,239, 206, 2")
                                    oWrite.WriteLine("TEXT 560,225,""ROMAN.TTF"",180,1,12,""DESIGN""")
                                    oWrite.WriteLine("TEXT 560,184,""ROMAN.TTF"",180,1,12,""SHADE""")
                                    oWrite.WriteLine("TEXT 560,144,""ROMAN.TTF"",180,1,12,""WIDTH""")
                                    oWrite.WriteLine("TEXT 439,225,""ROMAN.TTF"",180,1,12,"":""")
                                    oWrite.WriteLine("TEXT 439,184,""ROMAN.TTF"",180,1,12,"":""")
                                    oWrite.WriteLine("TEXT 439,144,""ROMAN.TTF"",180,1,12,"":""")
                                    oWrite.WriteLine("TEXT 418,225,""ROMAN.TTF"",180,1,12,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("TEXT 418,184,""ROMAN.TTF"",180,1,12,""" & ROW("SHADE") & """")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then
                                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    End If

                                    oWrite.WriteLine("TEXT 418,144,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """")
                                    oWrite.WriteLine("BARCODE 560,93,""128M"",51,0,180,2,4,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("TEXT 447,38,""ROMAN.TTF"",180,1,8,""" & ROW("BARCODE") & """")
                                    oWrite.WriteLine("PRINT 1,2")
                                    oWrite.Dispose()


                                ElseIf ClientName = "MAHAVIRPOLYCOT" Then

                                    If ROW("SHADE") = "" Then

                                        oWrite.WriteLine("I8,A")
                                        oWrite.WriteLine("ZN")
                                        oWrite.WriteLine("q779")
                                        oWrite.WriteLine("S3")
                                        oWrite.WriteLine("O")
                                        oWrite.WriteLine("JF")
                                        oWrite.WriteLine("D8")
                                        oWrite.WriteLine("ZT")
                                        oWrite.WriteLine("Q800,25")
                                        oWrite.WriteLine("KI81")
                                        oWrite.WriteLine("N")
                                        oWrite.WriteLine("A742,379,2,2,2,2,N,""Width""")
                                        oWrite.WriteLine("A742,603,2,2,2,2,N,""Item""")
                                        oWrite.WriteLine("b80,367,Q,m2,s5,eL,iA,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("A205,352,2,1,1,1,N,""" & ROW("BARCODE") & """") 'BARCODE

                                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                        Dim TEMPWIDTH As String = ""
                                        Dim TEMPHSN As String = ""
                                        Dim TEMPRATE As String = ""
                                        Dim TEMPQUALITY As String = ""
                                        Dim TEMPREMARKS As String = ""
                                        Dim OBJCMN As New ClsCommon
                                        Dim DT As DataTable = OBJCMN.search(" ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE, ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_SELVEDGE, '') AS SELVEDGE, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(ITEMMASTER.ITEM_RATE, 0) AS RATE  ", "", " ITEMMASTER LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.item_HSNCODEID = HSNMASTER.HSN_ID ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                        If DT.Rows.Count > 0 Then
                                            TEMPHSN = DT.Rows(0).Item("HSNCODE")
                                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                            TEMPQUALITY = DT.Rows(0).Item("SELVEDGE")
                                            TEMPRATE = (Val(DT.Rows(0).Item("RATE")) + 18) & "000"
                                        End If

                                        DT = OBJCMN.search(" ISNULL(CAST(DESIGNMASTER.DESIGN_REMARK AS VARCHAR(1000)), '') AS REMARKS ", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & ROW("DESIGNNO") & "' AND DESIGN_YEARID = " & YearId)
                                        If DT.Rows.Count > 0 Then
                                            TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                        End If

                                        oWrite.WriteLine("A548,379,2,2,2,2,N,""" & TEMPWIDTH & """")
                                        oWrite.WriteLine("A548,603,2,2,2,2,N,""" & ROW("ITEMNAME") & """")
                                        oWrite.WriteLine("A742,552,2,2,2,2,N,""D.No""")
                                        oWrite.WriteLine("A580,603,2,2,2,2,N,"":""")
                                        oWrite.WriteLine("A548,552,2,2,2,2,N,""" & ROW("DESIGNNO") & """")
                                        oWrite.WriteLine("A580,552,2,2,2,2,N,"":""")
                                        oWrite.WriteLine("A580,379,2,2,2,2,N,"":""")
                                        oWrite.WriteLine("A548,314,2,4,1,1,N,""" & TEMPQUALITY & """")
                                        oWrite.WriteLine("A742,491,2,2,2,2,N,""Series""")
                                        oWrite.WriteLine("A548,491,2,2,2,2,N,""" & TEMPRATE & """")
                                        oWrite.WriteLine("A580,491,2,2,2,2,N,"":""")
                                        oWrite.WriteLine("A742,317,2,2,2,2,N,""Quality""")
                                        oWrite.WriteLine("A580,317,2,2,2,2,N,"":""")
                                        oWrite.WriteLine("A742,435,2,2,2,2,N,""HSN""")
                                        oWrite.WriteLine("A548,435,2,2,2,2,N,""" & TEMPHSN & """")
                                        oWrite.WriteLine("A580,435,2,2,2,2,N,"":""")
                                        oWrite.WriteLine("A611,178,2,4,1,1,N,""" & ROW("ITEMNAME") & """")
                                        oWrite.WriteLine("A611,117,2,4,1,1,N,""" & ROW("DESIGNNO") & """")
                                        oWrite.WriteLine("A185,184,2,3,2,2,N,""" & TEMPREMARKS & """")
                                        oWrite.WriteLine("A217,23,1,2,1,1,N,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("b52,24,Q,m2,s5,eL,iA,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("P1")
                                        oWrite.Dispose()

                                    Else
                                        oWrite.WriteLine("I8,A")
                                        oWrite.WriteLine("ZN")
                                        oWrite.WriteLine("q300")
                                        oWrite.WriteLine("O")
                                        oWrite.WriteLine("JF")
                                        oWrite.WriteLine("ZT")
                                        oWrite.WriteLine("Q120,25")
                                        oWrite.WriteLine("KI80")
                                        oWrite.WriteLine("N")
                                        oWrite.WriteLine("A293,109,2,3,1,1,N,""" & ROW("ITEMNAME") & """")
                                        oWrite.WriteLine("A293,76,2,1,1,1,N,""" & ROW("DESIGNNO") & """")
                                        oWrite.WriteLine("LO5,83,289,2")
                                        oWrite.WriteLine("LO80,9,2,73")
                                        oWrite.WriteLine("A215,49,2,3,1,1,N,""" & ROW("SHADE") & """") 'BARCODE
                                        oWrite.WriteLine("b10,11,Q,m2,s3,eL,iA,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("LO80,52,203,2")
                                        oWrite.WriteLine("A266,23,2,1,1,1,N,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("P1")
                                        oWrite.Dispose()
                                    End If


                                ElseIf ClientName = "MANMANDIR" Then

                                    oWrite.WriteLine("SIZE 35.5 mm, 50 mm")
                                    oWrite.WriteLine("DIRECTION 0,0")
                                    oWrite.WriteLine("REFERENCE 0,0")
                                    oWrite.WriteLine("OFFSET 0 mm")
                                    oWrite.WriteLine("SET PEEL OFF")
                                    oWrite.WriteLine("SET CUTTER OFF")
                                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                    oWrite.WriteLine("SET TEAR ON")
                                    oWrite.WriteLine("CLS")
                                    oWrite.WriteLine("CODEPAGE 1252")
                                    oWrite.WriteLine("TEXT 5,363,""ROMAN.TTF"",270,12,12,""" & ROW("ITEMNAME") & """")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String
                                    Dim TEMPCATEGORY As String
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then
                                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                        TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                                    End If

                                    oWrite.WriteLine("TEXT 42,290,""ROMAN.TTF"",270,1,8,""" & TEMPCATEGORY & """")
                                    oWrite.WriteLine("TEXT 76,363,""ROMAN.TTF"",270,1,12,""WIDTH""")
                                    oWrite.WriteLine("TEXT 76,256,""ROMAN.TTF"",270,1,12,"":""")
                                    oWrite.WriteLine("TEXT 76,236,""ROMAN.TTF"",270,1,12,""" & TEMPWIDTH & """")
                                    oWrite.WriteLine("TEXT 121,363,""ROMAN.TTF"",270,1,12,""SHADE""")
                                    oWrite.WriteLine("TEXT 121,256,""ROMAN.TTF"",270,1,12,"":""")
                                    oWrite.WriteLine("TEXT 121,236,""ROMAN.TTF"",270,1,12,""" & ROW("SHADE") & """")
                                    oWrite.WriteLine("BARCODE 155,372,""128M"",89,0,270,2,4,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("TEXT 249,265,""ROMAN.TTF"",270,1,8,""" & ROW("BARCODE") & """")
                                    oWrite.WriteLine("PRINT 1,1")
                                    oWrite.Dispose()

                                ElseIf ClientName = "MNARESH" Then

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String = ""
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then
                                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    End If

                                    If TEMPHEADER = "1" Then

                                        oWrite.WriteLine("SIZE 97.5 mm, 50 mm
DIRECTION 0,0
REFERENCE 0,0
OFFSET 0 mm
SET PEEL OFF
SET CUTTER OFF
SET PARTIAL_CUTTER OFF
SET TEAR ON
CLS
CODEPAGE 1252
TEXT 755,379,""ROMAN.TTF"",180,1,16,""" & ROW("ITEMNAME") & """
BAR 409,324, 355, 3
TEXT 755,307,""ROMAN.TTF"",180,1,14,""LOT NO""
TEXT 614,307,""ROMAN.TTF"",180,1,14,"":""
TEXT 583,307,""ROMAN.TTF"",180,1,14,""" & ROW("REMARKS") & """
TEXT 755,253,""ROMAN.TTF"",180,1,14,""D. NO""
TEXT 614,253,""ROMAN.TTF"",180,1,14,"":""
TEXT 583,253,""ROMAN.TTF"",180,1,14,""" & ROW("DESIGNNO") & """
TEXT 755,199,""ROMAN.TTF"",180,1,14,""SHADE""
TEXT 614,199,""ROMAN.TTF"",180,1,14,"":""
TEXT 583,199,""ROMAN.TTF"",180,1,14,""" & ROW("SHADE") & """
TEXT 755,145,""ROMAN.TTF"",180,1,14,""WIDTH""
TEXT 614,145,""ROMAN.TTF"",180,1,14,"":""
TEXT 583,145,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """
BARCODE 764,101,""39"",60,0,180,2,5,""" & ROW("BARCODE") & """
TEXT 631,36,""ROMAN.TTF"",180,1,8,""" & ROW("BARCODE") & """
TEXT 384,379,""ROMAN.TTF"",180,1,16,""" & ROW("ITEMNAME") & """
BAR 37,324, 355, 3
TEXT 384,307,""ROMAN.TTF"",180,1,14,""LOT NO""
TEXT 243,307,""ROMAN.TTF"",180,1,14,"":""
TEXT 211,307,""ROMAN.TTF"",180,1,14,""" & ROW("REMARKS") & """
TEXT 384,253,""ROMAN.TTF"",180,1,14,""D. NO""
TEXT 243,253,""ROMAN.TTF"",180,1,14,"":""
TEXT 211,253,""ROMAN.TTF"",180,1,14,""" & ROW("DESIGNNO") & """
TEXT 384,199,""ROMAN.TTF"",180,1,14,""SHADE""
TEXT 243,199,""ROMAN.TTF"",180,1,14,"":""
TEXT 211,199,""ROMAN.TTF"",180,1,14,""" & ROW("SHADE") & """
TEXT 384,145,""ROMAN.TTF"",180,1,14,""WIDTH""
TEXT 243,145,""ROMAN.TTF"",180,1,14,"":""
TEXT 211,145,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """
BARCODE 390,101,""39"",60,0,180,2,5,""" & ROW("BARCODE") & """
TEXT 258,36,""ROMAN.TTF"",180,1,8,""" & ROW("BARCODE") & """
PRINT 1,1")
                                        oWrite.Dispose()

                                    Else

                                        oWrite.WriteLine("SIZE 67.5 mm, 50 mm
DIRECTION 0,0
REFERENCE 0,0
OFFSET 0 mm
SET PEEL OFF
SET CUTTER OFF
SET PARTIAL_CUTTER OFF
SET TEAR ON
CLS
CODEPAGE 1252
TEXT 523,385,""ROMAN.TTF"",180,1,12,""" & ROW("ITEMNAME") & """
BAR 19,337, 510, 3
TEXT 517,314,""ROMAN.TTF"",180,1,12,""LOT NO""
TEXT 379,314,""ROMAN.TTF"",180,1,12,"":""
TEXT 358,314,""ROMAN.TTF"",180,1,12,""" & ROW("REMARKS") & """
TEXT 517,259,""ROMAN.TTF"",180,1,12,""D. NO""
TEXT 379,259,""ROMAN.TTF"",180,1,12,"":""
TEXT 358,259,""ROMAN.TTF"",180,1,12,""" & ROW("DESIGNNO") & """
TEXT 517,204,""ROMAN.TTF"",180,1,12,""SHADE""
TEXT 379,204,""ROMAN.TTF"",180,1,12,"":""
TEXT 358,204,""ROMAN.TTF"",180,1,12,""" & ROW("SHADE") & """
BARCODE 523,101,""39"",65,0,180,2,5,""" & ROW("BARCODE") & """
TEXT 366,31,""0"",180,7,7,""" & ROW("BARCODE") & """
TEXT 517,149,""ROMAN.TTF"",180,1,12,""WIDTH""
TEXT 379,149,""ROMAN.TTF"",180,1,12,"":""
TEXT 358,149,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """
PRINT 1,1")
                                        oWrite.Dispose()

                                    End If


                                ElseIf ClientName = "MNIKHIL" Then

                                    oWrite.WriteLine("SIZE 50 mm, 25 mm")
                                    oWrite.WriteLine("DIRECTION 0,0")
                                    oWrite.WriteLine("REFERENCE 0,0")
                                    oWrite.WriteLine("OFFSET 0 mm")
                                    oWrite.WriteLine("SET PEEL OFF")
                                    oWrite.WriteLine("SET CUTTER OFF")
                                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                    oWrite.WriteLine("SET TEAR ON")
                                    oWrite.WriteLine("CLS")
                                    oWrite.WriteLine("CODEPAGE 1252")
                                    oWrite.WriteLine("TEXT 375,183,""ROMAN.TTF"",180,1,18,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("TEXT 375,110,""ROMAN.TTF"",180,1,12,""Design""")
                                    oWrite.WriteLine("TEXT 270,110,""ROMAN.TTF"",180,1,12,"":""")
                                    oWrite.WriteLine("TEXT 253,110,""ROMAN.TTF"",180,1,12,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("TEXT 375,65,""ROMAN.TTF"",180,1,12,""Width""")
                                    oWrite.WriteLine("TEXT 270,65,""ROMAN.TTF"",180,1,12,"":""")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then
                                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    End If

                                    oWrite.WriteLine("TEXT 253,65,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """")
                                    oWrite.WriteLine("BAR 30,128, 344, 3")
                                    oWrite.WriteLine("PRINT 1,1")
                                    oWrite.Dispose()

                                ElseIf ClientName = "RAJKRIPA" Then

                                    oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>^XA")
                                    oWrite.WriteLine("^MCY^PMN")
                                    oWrite.WriteLine("^PW380")
                                    oWrite.WriteLine("~JSN^MMT")
                                    oWrite.WriteLine("^JZY")
                                    oWrite.WriteLine("^LH0,0^LRN")
                                    oWrite.WriteLine("^XZ")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='0' pitch='50.0 mm'></xpml>~DGR:SSGFX000.GRF,190,5,:Z64:eJxtjrENgDAMBA9RpGQBRNagQLBSSgqKjJZRMkJKuvAxVCiydG7s/4Mb8DXBUTNUdzMULlxmx0UmjfYCY5phSJuQTz2VIIQfFMHaQ+kcW8oqZAudW0crilapcv9qmJDUyidpuk38AVIpK8E=:119B")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>^XA")
                                    oWrite.WriteLine("^FT23,103")
                                    oWrite.WriteLine("^CI0")
                                    If TEMPHEADER = "1" Then
                                        oWrite.WriteLine("^A0N,39,52^FDDHANNA SHALI^FS")
                                    ElseIf TEMPHEADER = "2" Then
                                        oWrite.WriteLine("^A0N,39,52^FDCOLOR CRAFT^FS")
                                    ElseIf TEMPHEADER = "3" Then
                                        oWrite.WriteLine("^A0N,39,52^FDC. H. L.^FS")
                                    End If
                                    oWrite.WriteLine("^FT331,55")

                                    oWrite.WriteLine("^AAN,27,15^FDR^FS")
                                    oWrite.WriteLine("^FT54,145")
                                    oWrite.WriteLine("^A0N,23,30^FDEXCLUSIVE SHIRTING^FS")
                                    oWrite.WriteLine("^FT9,197")

                                    'GET ITEMDISPLAYNAME FROM ITEMMASTER
                                    Dim TEMPITEMNAME As String = ""
                                    Dim TEMPWIDTH As String = ""
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.SEARCH("  ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS ITEMDISPLAYNAME, ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then
                                        TEMPITEMNAME = DT.Rows(0).Item("ITEMDISPLAYNAME")
                                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    End If

                                    oWrite.WriteLine("^A0N,23,30^FD" & TEMPITEMNAME & "^FS")
                                    oWrite.WriteLine("^FT10,235")
                                    oWrite.WriteLine("^A0N,23,30^FDWIDTH^FS")
                                    oWrite.WriteLine("^FT112,235")
                                    oWrite.WriteLine("^A0N,23,30^FD:^FS")
                                    oWrite.WriteLine("^FT136,235")
                                    oWrite.WriteLine("^A0N,23,30^FD" & TEMPWIDTH & "^FS")
                                    oWrite.WriteLine("^FT10,275")
                                    oWrite.WriteLine("^A0N,23,30^FDSMP ^FS")
                                    oWrite.WriteLine("^FT112,275")
                                    oWrite.WriteLine("^A0N,23,30^FD:^FS")
                                    oWrite.WriteLine("^FT136,275")
                                    oWrite.WriteLine("^A0N,23,30^FD" & ROW("BARCODE") & "^FS") 'BARCODE
                                    oWrite.WriteLine("^FO12,289")
                                    oWrite.WriteLine("^BY2^BCN,80,N,N,,A^FD" & ROW("BARCODE") & "^FS") 'BARCODE
                                    oWrite.WriteLine("^FO322,26")
                                    oWrite.WriteLine("^XGR:SSGFX000.GRF,1,1^FS")
                                    oWrite.WriteLine("^PQ1,0,1,Y")
                                    oWrite.WriteLine("^XZ")
                                    oWrite.WriteLine("<xpml></page></xpml>^XA")
                                    oWrite.WriteLine("^IDR:SSGFX000.GRF^XZ")
                                    oWrite.WriteLine("<xpml><end/></xpml>")
                                    oWrite.Dispose()


                                ElseIf ClientName = "REALCORPORATION" Then

                                    If CMBCOLOR.Text.Trim = "" Then
                                        oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>SIZE 47.5 mm, 25 mm")
                                        oWrite.WriteLine("GAP 3 mm, 0 mm")
                                        oWrite.WriteLine("DIRECTION 0,0")
                                        oWrite.WriteLine("REFERENCE 0,0")
                                        oWrite.WriteLine("OFFSET 0 mm")
                                        oWrite.WriteLine("SET PEEL OFFT")
                                        oWrite.WriteLine("SET CUTTER OFF")
                                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>SET TEAR ON")
                                        oWrite.WriteLine("ON")
                                        oWrite.WriteLine("CLS")
                                        oWrite.WriteLine("CODEPAGE 1252")
                                        oWrite.WriteLine("TEXT 364,187,""0"",180,10,10,""" & ROW("ITEMNAME") & """")
                                        oWrite.WriteLine("BAR 9,152, 357, 3")
                                        oWrite.WriteLine("TEXT 364,141,""0"",180,10,10,""D.NO""")
                                        oWrite.WriteLine("TEXT 275,141,""0"",180,10,10,"":""")
                                        oWrite.WriteLine("TEXT 261,141,""0"",180,10,10,""" & ROW("DESIGNNO") & """")
                                        oWrite.WriteLine("TEXT 364,95,""0"",180,10,10,""WIDTH""")
                                        oWrite.WriteLine("TEXT 275,95,""0"",180,10,10,"":""")



                                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                        Dim TEMPREMARKS As String = ""
                                        Dim TEMPWIDTH As String = ""
                                        Dim OBJCMN As New ClsCommon
                                        Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(ITEMMASTER.ITEM_WIDTH,'') AS WIDTH", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                        If DT.Rows.Count > 0 Then
                                            TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                        End If

                                        oWrite.WriteLine("TEXT 261,95,""0"",180,10,10,""" & TEMPWIDTH & """")
                                        oWrite.WriteLine("TEXT 364,48,""0"",180,10,10,""" & TEMPREMARKS & """")
                                        oWrite.WriteLine("QRCODE 121,106,L,4,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("TEXT 26,14,""0"",90,6,6,""" & ROW("BARCODE") & """")
                                        oWrite.WriteLine("PRINT 1,1")
                                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                        oWrite.Dispose()
                                    Else
                                        oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>SIZE 47.5 mm, 25 mm")
                                        oWrite.WriteLine("GAP 3 mm, 0 mm")
                                        oWrite.WriteLine("DIRECTION 0,0")
                                        oWrite.WriteLine("REFERENCE 0,0")
                                        oWrite.WriteLine("OFFSET 0 mm")
                                        oWrite.WriteLine("SET PEEL OFFT")
                                        oWrite.WriteLine("SET CUTTER OFF")
                                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>SET TEAR ON")
                                        oWrite.WriteLine("ON")
                                        oWrite.WriteLine("CLS")
                                        oWrite.WriteLine("CODEPAGE 1252")
                                        oWrite.WriteLine("TEXT 364,187,""0"",180,10,10,""" & ROW("ITEMNAME") & """")
                                        oWrite.WriteLine("BAR 9,152, 357, 3")
                                        oWrite.WriteLine("TEXT 364,144,""0"",180,10,10,""D.NO""")
                                        oWrite.WriteLine("TEXT 275,144,""0"",180,10,10,"":""")
                                        oWrite.WriteLine("TEXT 261,144,""0"",180,10,10,""" & ROW("DESIGNNO") & """")
                                        oWrite.WriteLine("TEXT 364,74,""0"",180,10,10,""WIDTH""")
                                        oWrite.WriteLine("TEXT 275,74,""0"",180,10,10,"":""")



                                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                        Dim TEMPREMARKS As String = ""
                                        Dim TEMPWIDTH As String = ""
                                        Dim OBJCMN As New ClsCommon
                                        Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(ITEMMASTER.ITEM_WIDTH,'') AS WIDTH", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                        If DT.Rows.Count > 0 Then
                                            TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                        End If

                                        oWrite.WriteLine("TEXT 261,74,""0"",180,10,10,""" & TEMPWIDTH & """")
                                        oWrite.WriteLine("TEXT 364,40,""0"",180,10,10,""" & TEMPREMARKS & """")
                                        oWrite.WriteLine("QRCODE 121,106,L,4,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("TEXT 26,14,""0"",90,6,6,""" & ROW("BARCODE") & """")
                                        oWrite.WriteLine("TEXT 364,109,""0"",180,10,10,""SHADE""")
                                        oWrite.WriteLine("TEXT 275,109,""0"",180,10,10,"":""")
                                        oWrite.WriteLine("TEXT 261,109,""0"",180,10,10,""" & ROW("SHADE") & """")
                                        oWrite.WriteLine("PRINT 1,1")
                                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                        oWrite.Dispose()
                                    End If


                                ElseIf ClientName = "RMANILAL" Then

                                    oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 47.5 mm, 50 mm")
                                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                                    oWrite.WriteLine("SET RIBBON OFF")
                                    oWrite.WriteLine("DIRECTION 0,0")
                                    oWrite.WriteLine("REFERENCE 0,0")
                                    oWrite.WriteLine("OFFSET 0 mm")
                                    oWrite.WriteLine("SET PEEL OFFT")
                                    oWrite.WriteLine("SET CUTTER OFF")
                                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
                                    oWrite.WriteLine("CLS")
                                    oWrite.WriteLine("CODEPAGE 1252")
                                    oWrite.WriteLine("TEXT 365,373,""0"",180,16,16,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("TEXT 365,316,""0"",180,12,12,""D.NO""")
                                    oWrite.WriteLine("TEXT 264,316,""0"",180,12,12,"":""")
                                    oWrite.WriteLine("TEXT 242,316,""0"",180,12,12,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("TEXT 365,269,""0"",180,12,12,""CH.NO""")
                                    oWrite.WriteLine("TEXT 264,269,""0"",180,12,12,"":""")
                                    oWrite.WriteLine("TEXT 242,269,""0"",180,12,12,""" & ROW("SHADE") & """")
                                    oWrite.WriteLine("QRCODE 266,169,L,6,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("TEXT 169,269,""0"",180,12,12,""" & ROW("REMARKS") & """")
                                    oWrite.WriteLine("TEXT 365,219,""0"",180,12,12,""BASE""")
                                    oWrite.WriteLine("TEXT 264,219,""0"",180,12,12,"":""")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPREMARKS As String = ""
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then TEMPREMARKS = DT.Rows(0).Item("REMARKS")

                                    oWrite.WriteLine("TEXT 242,219,""0"",180,12,12,""" & TEMPREMARKS & """")
                                    oWrite.WriteLine("PRINT 1,1")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                    oWrite.Dispose()

                                ElseIf ClientName = "SANGHVI" Then

                                    oWrite.WriteLine("I8,A")
                                    oWrite.WriteLine("ZN")
                                    oWrite.WriteLine("q428")
                                    oWrite.WriteLine("O")
                                    oWrite.WriteLine("JF")
                                    oWrite.WriteLine("ZT")
                                    oWrite.WriteLine("Q400,25")
                                    oWrite.WriteLine("N")
                                    oWrite.WriteLine("A392,273,2,4,1,1,N,""COLOR""")
                                    oWrite.WriteLine("A392,231,2,4,1,1,N,""RATE""")
                                    oWrite.WriteLine("A306,273,2,4,1,1,N,"":""")
                                    oWrite.WriteLine("A306,231,2,4,1,1,N,"":""")
                                    If ClientName = "SANGHVI" Then oWrite.WriteLine("A368,355,2,4,1,1,N,""TINU MINU EMBROIDERY""")
                                    oWrite.WriteLine("A157,315,2,4,1,1,N,""WIDTH""")
                                    oWrite.WriteLine("A283,273,2,4,1,1,N,""" & ROW("SHADE") & """")


                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String = ""
                                    Dim TEMPRATE As Double = 0
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                                    'GET RATE
                                    Dim WHERECLAUSE As String = ""
                                    If ROW("DESIGNNO") <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(DESIGNMASTER.DESIGN_NO,'') = '" & ROW("DESIGNNO") & "'"
                                    If ROW("SHADE") <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ISNULL(COLORMASTER.COLOR_NAME,'') = '" & ROW("SHADE") & "'"
                                    DT = OBJCMN.search("PRICELISTMASTER.PL_RATE AS SALERATE ", "", "PRICELISTMASTER INNER JOIN ITEMMASTER ON PRICELISTMASTER.PL_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON PRICELISTMASTER.PL_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON PRICELISTMASTER.PL_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON PRICELISTMASTER.PL_QUALITYID = QUALITYMASTER.QUALITY_id ", " AND ISNULL(ITEMMASTER.ITEM_NAME,'') = '" & ROW("ITEMNAME") & "'" & WHERECLAUSE & " AND PL_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then TEMPRATE = Val(DT.Rows(0).Item("SALERATE"))


                                    oWrite.WriteLine("A283,231,2,4,1,1,N,""" & Format(Val(TEMPRATE), "0.00") & """")
                                    oWrite.WriteLine("A57,315,2,4,1,1,N,""" & TEMPWIDTH & """")
                                    oWrite.WriteLine("A72,315,2,4,1,1,N,"":""")
                                    oWrite.WriteLine("B396,190,2,1,2,4,63,N,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("A319,121,2,4,1,1,N,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("A392,315,2,4,1,1,N,""D.NO""")
                                    oWrite.WriteLine("A306,315,2,4,1,1,N,"":""")
                                    oWrite.WriteLine("A283,315,2,4,1,1,N,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("A151,155,2,4,1,1,N,""" & ROW("REMARKS") & """")
                                    oWrite.WriteLine("P1")
                                    oWrite.Dispose()

                                ElseIf ClientName = "SHREENAKODA" Then

                                    oWrite.WriteLine("I8,A")
                                    oWrite.WriteLine("ZN")
                                    oWrite.WriteLine("q380")
                                    oWrite.WriteLine("O")
                                    oWrite.WriteLine("JF")
                                    oWrite.WriteLine("ZT")
                                    oWrite.WriteLine("Q400,B25")
                                    oWrite.WriteLine("KI80")
                                    oWrite.WriteLine("N")

                                    'GET PACKINGTYPE FROM PARTYMASTER AND SHOW HERE, WHEN WEAVERNAME IS PRESENT
                                    Dim TEMPPACKINGTYPE As String = ""
                                    Dim TEMPWIDTH As String = ""
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.search(" ISNULL(PACKINGTYPE_NAME, '') AS PACKINGTYPE ", "", " LEDGERS LEFT OUTER JOIN PACKINGTYPEMASTER ON LEDGERS.ACC_PACKINGTYPEID = PACKINGTYPEMASTER.PACKINGTYPE_id ", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then
                                        TEMPPACKINGTYPE = DT.Rows(0).Item("PACKINGTYPE")
                                    End If

                                    oWrite.WriteLine("A238,397,2,4,2,2,N,""" & TEMPPACKINGTYPE & """")
                                    oWrite.WriteLine("LO20,324,339,3")
                                    oWrite.WriteLine("A364,314,2,4,1,1,N,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("A364,256,2,4,1,1,N,""D.NO""")
                                    oWrite.WriteLine("A279,256,2,4,1,1,N,"":""")
                                    oWrite.WriteLine("A253,256,2,4,1,1,N,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("b23,37,Q,m2,s5,eL,iA,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("A364,192,2,4,1,1,N,""SERIES""")
                                    oWrite.WriteLine("A279,192,2,4,1,1,N,"":""")
                                    oWrite.WriteLine("A253,192,2,4,1,1,N,""" & ROW("REMARKS") & """")
                                    oWrite.WriteLine("A364,129,2,4,1,1,N,""SHADE""")
                                    oWrite.WriteLine("A279,129,2,4,1,1,N,"":""")
                                    oWrite.WriteLine("A253,129,2,4,1,1,N,""" & ROW("SHADE") & """")
                                    oWrite.WriteLine("A364,66,2,4,1,1,N,""WIDTH""")
                                    oWrite.WriteLine("A279,66,2,4,1,1,N,"":""")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    DT = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then
                                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    End If

                                    oWrite.WriteLine("A253,66,2,4,1,1,N,""" & TEMPWIDTH & """")
                                    oWrite.WriteLine("P1")
                                    oWrite.Dispose()

                                ElseIf ClientName = "SNCM" Then

                                    oWrite.WriteLine("SIZE 72.5 mm, 50 mm")
                                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                                    oWrite.WriteLine("SPEED 5")
                                    oWrite.WriteLine("DENSITY 10")
                                    oWrite.WriteLine("DIRECTION 0,0")
                                    oWrite.WriteLine("REFERENCE 0,0")
                                    oWrite.WriteLine("OFFSET 0 mm")
                                    oWrite.WriteLine("SET PEEL OFFT")
                                    oWrite.WriteLine("SET CUTTER OFF")
                                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                    oWrite.WriteLine("SET TEAR ON")
                                    oWrite.WriteLine("ON")
                                    oWrite.WriteLine("CLS")
                                    oWrite.WriteLine("CODEPAGE 1252")
                                    oWrite.WriteLine("TEXT 557,267,""0"",180,16,28,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("BAR 45,193, 512, 4")
                                    oWrite.WriteLine("TEXT 557,175,""ROMAN.TTF"",180,1,12,""D. NO""")
                                    oWrite.WriteLine("TEXT 425,175,""ROMAN.TTF"",180,1,12,"":""")
                                    oWrite.WriteLine("TEXT 404,175,""ROMAN.TTF"",180,1,12,""" & ROW("DESIGNNO") & """")


                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String = ""
                                    Dim TEMPREMARKS As String = ""
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS,'') AS REMARKS ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                                    oWrite.WriteLine("TEXT 557,113,""ROMAN.TTF"",180,1,12,""WIDTH""")
                                    oWrite.WriteLine("TEXT 425,113,""ROMAN.TTF"",180,1,12,"":""")
                                    oWrite.WriteLine("TEXT 404,113,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """")
                                    oWrite.WriteLine("TEXT 557,52,""ROMAN.TTF"",180,1,12,""QLTY""")
                                    oWrite.WriteLine("TEXT 425,52,""ROMAN.TTF"",180,1,12,"":""")
                                    oWrite.WriteLine("TEXT 404,52,""ROMAN.TTF"",180,1,12,""" & TEMPREMARKS & """")

                                    oWrite.WriteLine("QRCODE 167,175,L,5,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("PRINT 1,1")
                                    oWrite.Dispose()

                                ElseIf ClientName = "SONU" Then

                                    oWrite.WriteLine("<xpml><page quantity='0' pitch='45.0 mm'></xpml>I8,A")
                                    oWrite.WriteLine("ZN")
                                    oWrite.WriteLine("q600")
                                    oWrite.WriteLine("O")
                                    oWrite.WriteLine("JF")
                                    oWrite.WriteLine("ZT")
                                    oWrite.WriteLine("Q360,B25")
                                    oWrite.WriteLine("KI80")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='45.0 mm'></xpml>N")
                                    oWrite.WriteLine("B590,133,2,1,2,4,82,N,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("A510,42,2,4,1,1,N,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("A590,345,2,3,2,2,N,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("A590,288,2,2,2,2,N,""D.NO""")
                                    oWrite.WriteLine("A590,237,2,2,2,2,N,""SHADE""")
                                    oWrite.WriteLine("A590,186,2,2,2,2,N,""WIDTH""")
                                    oWrite.WriteLine("A464,288,2,2,2,2,N,"":""")
                                    oWrite.WriteLine("A464,237,2,2,2,2,N,"":""")
                                    oWrite.WriteLine("A464,186,2,2,2,2,N,"":""")
                                    oWrite.WriteLine("A439,288,2,2,2,2,N,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("A439,242,2,3,2,2,N,""" & ROW("SHADE") & """")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then
                                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    End If

                                    oWrite.WriteLine("A439,186,2,2,2,2,N,""" & TEMPWIDTH & """")
                                    oWrite.WriteLine("P1")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                    oWrite.Dispose()


                                ElseIf ClientName = "SPCORP" Then

                                    oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>I8,A")
                                    oWrite.WriteLine("ZN")
                                    oWrite.WriteLine("q380")
                                    oWrite.WriteLine("O")
                                    oWrite.WriteLine("JF")
                                    oWrite.WriteLine("ZT")
                                    oWrite.WriteLine("Q400,25")
                                    oWrite.WriteLine("KI80")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>N")
                                    oWrite.WriteLine("A362,287,2,4,1,1,N,""" & ROW("ITEMNAME") & """")
                                    oWrite.WriteLine("LO12,243,355,3")
                                    oWrite.WriteLine("A362,222,2,1,2,2,N,""" & ROW("DESIGNNO") & """")
                                    oWrite.WriteLine("B367,119,2,1,2,4,71,N,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("A293,42,2,4,1,1,N,""" & ROW("BARCODE") & """") 'BARCODE
                                    oWrite.WriteLine("A362,171,2,4,1,1,N,""WIDTH""")
                                    oWrite.WriteLine("A266,171,2,4,1,1,N,"":""")

                                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                    Dim TEMPWIDTH As String
                                    Dim OBJCMN As New ClsCommon
                                    Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                    If DT.Rows.Count > 0 Then
                                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                    End If

                                    oWrite.WriteLine("A241,171,2,4,1,1,N,""" & TEMPWIDTH & """")
                                    'oWrite.WriteLine("A279,385,2,5,1,1,N,""SEHCO""")
                                    oWrite.WriteLine("LO12,297,355,3")
                                    oWrite.WriteLine("P1")
                                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                    oWrite.Dispose()

                                ElseIf ClientName = "SUPRIYA" Then

                                    'FOR SAMPLE STICKER
                                    If TEMPHEADER = "1" Then
                                        oWrite.WriteLine("G0")
                                        oWrite.WriteLine("n")
                                        oWrite.WriteLine("M0500")
                                        oWrite.WriteLine("O0214")
                                        oWrite.WriteLine("V0")
                                        oWrite.WriteLine("t1")
                                        oWrite.WriteLine("Kf0070")
                                        oWrite.WriteLine("L")
                                        oWrite.WriteLine("D11")
                                        oWrite.WriteLine("A2")
                                        oWrite.WriteLine("1W1D66000003301172,LA," & ROW("BARCODE"))
                                        oWrite.WriteLine("ySPM")
                                        oWrite.WriteLine("1911A0600200122" & ROW("BARCODE"))
                                        oWrite.WriteLine("1911C0801280010" & CmpName)
                                        oWrite.WriteLine("1911A1000990010" & ROW("ITEMNAME"))
                                        oWrite.WriteLine("1911A0800820010D.NO")
                                        oWrite.WriteLine("1911A0800820048:")
                                        oWrite.WriteLine("1911A1000800056" & ROW("DESIGNNO"))
                                        oWrite.WriteLine("1911A0800640010S.NO")
                                        oWrite.WriteLine("1911A0800640048:")
                                        oWrite.WriteLine("1911A0800640056" & ROW("SHADE"))
                                        oWrite.WriteLine("1911A0800450010WIDTH")
                                        oWrite.WriteLine("1911A0800450048:")


                                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                        Dim TEMPWIDTH As String = ""
                                        Dim TEMPREMARKS As String = ""
                                        Dim OBJCMN As New ClsCommon
                                        Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                        If DT.Rows.Count > 0 Then
                                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                            TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                        End If

                                        oWrite.WriteLine("1911A0800450056" & TEMPWIDTH)
                                        oWrite.WriteLine("1X1100001240006L176001")
                                        oWrite.WriteLine("1911A0800280010" & TEMPREMARKS)
                                        If CmpName = "SUPRIYA SILK INDUSTRIES" Then oWrite.WriteLine("1911C0800040061DEEP BLUE") Else oWrite.WriteLine("1911C0800040061ROYAL TEX")
                                        oWrite.WriteLine("Q0001")
                                        oWrite.WriteLine("E")
                                        oWrite.Dispose()
                                    Else
                                        'FOR REGISTER STICKER
                                        oWrite.WriteLine("<xpml><page quantity='0' pitch='22.0 mm'></xpml>G0")
                                        oWrite.WriteLine("n")
                                        oWrite.WriteLine("M0500")
                                        oWrite.WriteLine("O0214")
                                        oWrite.WriteLine("V0")
                                        oWrite.WriteLine("t1")
                                        oWrite.WriteLine("Kf0070")
                                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='22.0 mm'></xpml>L")
                                        oWrite.WriteLine("D11")
                                        oWrite.WriteLine("A2")
                                        oWrite.WriteLine("1W1D44000002101352,LA," & ROW("BARCODE"))
                                        oWrite.WriteLine("ySPM")
                                        oWrite.WriteLine("1911A0600100130" & ROW("BARCODE"))
                                        oWrite.WriteLine("1911C1000670006" & ROW("ITEMNAME"))
                                        oWrite.WriteLine("1911C1000490006D.NO")
                                        oWrite.WriteLine("1911C1000490044:")
                                        oWrite.WriteLine("1911C1000490050" & ROW("DESIGNNO"))
                                        oWrite.WriteLine("1911C1000330006S.NO")
                                        oWrite.WriteLine("1911C1000330044:")
                                        oWrite.WriteLine("1911C1000330050" & ROW("SHADE"))
                                        oWrite.WriteLine("Q0001")
                                        oWrite.WriteLine("E")
                                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                        oWrite.Dispose()
                                    End If

                                ElseIf ClientName = "YASHVI" Then

                                    If TEMPHEADER = "1" Then
                                        oWrite.WriteLine("SIZE 72.5 mm, 50 mm")
                                        oWrite.WriteLine("GAP 3 mm, 0 mm")
                                        oWrite.WriteLine("DIRECTION 0,0")
                                        oWrite.WriteLine("REFERENCE 0,0")
                                        oWrite.WriteLine("OFFSET 0 mm")
                                        oWrite.WriteLine("SET PEEL OFFT")
                                        oWrite.WriteLine("SET CUTTER OFF")
                                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                        oWrite.WriteLine("SET TEAR ON")
                                        oWrite.WriteLine("CLS")
                                        oWrite.WriteLine("CODEPAGE 1252")
                                        oWrite.WriteLine("TEXT 495,252,""ROMAN.TTF"",180,1,11,""QUALITY""")
                                        oWrite.WriteLine("TEXT 495,212,""ROMAN.TTF"",180,1,11,""DESIGN""")
                                        oWrite.WriteLine("TEXT 495,129,""ROMAN.TTF"",180,1,11,""WIDTH""")
                                        oWrite.WriteLine("TEXT 374,252,""ROMAN.TTF"",180,1,11,"":""")
                                        oWrite.WriteLine("TEXT 374,212,""ROMAN.TTF"",180,1,11,"":""")
                                        oWrite.WriteLine("TEXT 374,129,""ROMAN.TTF"",180,1,11,"":""")
                                        oWrite.WriteLine("TEXT 354,252,""ROMAN.TTF"",180,1,11,""" & ROW("ITEMNAME") & """")
                                        oWrite.WriteLine("TEXT 354,212,""ROMAN.TTF"",180,1,11,""" & ROW("DESIGNNO") & """")

                                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                        Dim TEMPWIDTH As String = ""
                                        Dim TEMPREMARKS As String = ""
                                        Dim OBJCMN As New ClsCommon
                                        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                        If DT.Rows.Count > 0 Then
                                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                            TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                        End If

                                        oWrite.WriteLine("TEXT 354,129,""ROMAN.TTF"",180,1,11,""" & TEMPWIDTH & """")
                                        oWrite.WriteLine("TEXT 495,83,""ROMAN.TTF"",180,1,9,""" & TEMPREMARKS & """")
                                        oWrite.WriteLine("QRCODE 161,169,L,7,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("TEXT 495,43,""ROMAN.TTF"",180,1,9,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("TEXT 495,170,""ROMAN.TTF"",180,1,11,""SHADE""")
                                        oWrite.WriteLine("TEXT 374,170,""ROMAN.TTF"",180,1,11,"":""")
                                        oWrite.WriteLine("TEXT 354,170,""ROMAN.TTF"",180,1,11,""" & ROW("SHADE") & """")
                                        oWrite.WriteLine("PRINT 1,1")
                                        oWrite.Dispose()


                                    ElseIf TEMPHEADER = "2" Then

                                        oWrite.WriteLine("<xpml><page quantity='0' pitch='100.1 mm'></xpml>SIZE 99.10 mm, 100.1 mm")
                                        oWrite.WriteLine("GAP 3 mm, 0 mm")
                                        oWrite.WriteLine("DIRECTION 0,0")
                                        oWrite.WriteLine("REFERENCE 0,0")
                                        oWrite.WriteLine("OFFSET 0 mm")
                                        oWrite.WriteLine("SET PEEL OFF")
                                        oWrite.WriteLine("SET CUTTER OFF")
                                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='100.1 mm'></xpml>SET TEAR ON")
                                        oWrite.WriteLine("CLS")
                                        oWrite.WriteLine("CODEPAGE 1252")
                                        oWrite.WriteLine("TEXT 710,591,""0"",180,16,16,""ITEM""")
                                        oWrite.WriteLine("TEXT 710,532,""0"",180,16,16,""DESIGN""")
                                        oWrite.WriteLine("TEXT 710,465,""0"",180,16,16,""WIDTH""")
                                        oWrite.WriteLine("TEXT 555,591,""0"",180,16,16,"":""")
                                        oWrite.WriteLine("TEXT 555,532,""0"",180,16,16,"":""")
                                        oWrite.WriteLine("TEXT 555,465,""0"",180,16,16,"":""")
                                        oWrite.WriteLine("TEXT 525,591,""0"",180,16,16,""" & ROW("ITEMNAME") & """")
                                        oWrite.WriteLine("TEXT 525,532,""0"",180,16,16,""" & ROW("DESIGNNO") & """")

                                        'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                                        Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
                                        Dim OBJCMN As New ClsCommon
                                        Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                                        If DT.Rows.Count > 0 Then
                                            TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                                            TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                                            TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                                        End If

                                        oWrite.WriteLine("TEXT 525,465,""0"",180,16,16,""" & TEMPWIDTH & """")
                                        oWrite.WriteLine("QRCODE 281,521,L,10,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("TEXT 276,306,""0"",180,10,10,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("TEXT 583,265,""0"",180,14,14,""" & TEMPREMARKS & """")


                                        oWrite.WriteLine("TEXT 515,177,""0"",180,11,11,""ITEM""")
                                        oWrite.WriteLine("TEXT 515,137,""0"",180,11,11,""DESIGN""")
                                        oWrite.WriteLine("TEXT 409,177,""0"",180,11,11,"":""")
                                        oWrite.WriteLine("TEXT 409,137,""0"",180,11,11,"":""")
                                        oWrite.WriteLine("TEXT 390,177,""0"",180,11,11,""" & ROW("ITEMNAME") & """")
                                        oWrite.WriteLine("TEXT 390,137,""0"",180,11,11,""" & ROW("DESIGNNO") & """")
                                        oWrite.WriteLine("QRCODE 216,127,L,5,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("TEXT 91,26,""0"",90,8,8,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("PRINT 1,1")
                                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                        oWrite.Dispose()


                                    Else

                                        oWrite.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 57.5 mm, 15 mm")
                                        oWrite.WriteLine("GAP 3 mm, 0 mm")
                                        oWrite.WriteLine("DIRECTION 0,0")
                                        oWrite.WriteLine("REFERENCE 0,0")
                                        oWrite.WriteLine("OFFSET 0 mm")
                                        oWrite.WriteLine("SET PEEL OFFT")
                                        oWrite.WriteLine("SET CUTTER OFF")
                                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>SET TEAR ON")
                                        oWrite.WriteLine("ON")
                                        oWrite.WriteLine("CLS")
                                        oWrite.WriteLine("CODEPAGE 1252")
                                        oWrite.WriteLine("TEXT 448,66,""0"",180,18,18,""" & ROW("SHADE") & """")
                                        oWrite.WriteLine("QRCODE 381,100,L,3,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("TEXT 445,24,""0"",180,5,5,""" & ROW("ITEMNAME") & """")
                                        oWrite.WriteLine("TEXT 376,24,""0"",180,5,5,""" & ROW("DESIGNNO") & """")

                                        oWrite.WriteLine("TEXT 295,66,""0"",180,19,18,""" & ROW("SHADE") & """")
                                        oWrite.WriteLine("QRCODE 227,100,L,3,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("TEXT 295,24,""0"",180,5,5,""" & ROW("ITEMNAME") & """")
                                        oWrite.WriteLine("TEXT 227,24,""0"",180,5,5,""" & ROW("DESIGNNO") & """")

                                        oWrite.WriteLine("TEXT 138,66,""0"",180,18,18,""" & ROW("SHADE") & """")
                                        oWrite.WriteLine("QRCODE 70,100,L,3,A,180,M2,S7,""" & ROW("BARCODE") & """") 'BARCODE
                                        oWrite.WriteLine("TEXT 138,24,""0"",180,5,5,""" & ROW("ITEMNAME") & """")
                                        oWrite.WriteLine("TEXT 70,24,""0"",180,5,5,""" & ROW("DESIGNNO") & """")
                                        oWrite.WriteLine("PRINT 1,1")
                                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                                        oWrite.Dispose()

                                    End If


                                End If


                                'Printing Barcode
                                Dim psi As New ProcessStartInfo()
                                psi.FileName = "cmd.exe"
                                psi.RedirectStandardInput = False
                                psi.RedirectStandardOutput = True
                                psi.Arguments = "/c print " & Application.StartupPath & "\Barcode.txt"    ' specify your command
                                'psi.Arguments = "/c print D:\Barcode.txt"    ' specify your command
                                psi.UseShellExecute = False

                                Dim proc As Process
                                proc = Process.Start(psi)
                                dirresults = proc.StandardOutput.ReadToEnd() ' // read from stdout
                                '// do something with result stream
                                proc.WaitForExit()

NEXTLINE:
                            Next
                        Next

                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        EDITROW()
    End Sub

    Private Sub CMBDESIGNNO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDESIGNNO.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJDESIGN As New SelectDesign
                OBJDESIGN.FRMSTRING = "DESIGN"
                OBJDESIGN.ShowDialog()
                If OBJDESIGN.TEMPNAME <> "" Then CMBDESIGNNO.Text = OBJDESIGN.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbITEMNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBMERCHANT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "ITEMNAME"
                OBJItem.STRSEARCH = " and ITEM_cmpid = " & CmpId & " and ITEM_LOCATIONid = " & Locationid & " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then CMBMERCHANT.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCOLOR.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCOLOR As New SelectShade
                OBJCOLOR.FRMSTRING = "COLOR"
                OBJCOLOR.ShowDialog()
                If OBJCOLOR.TEMPNAME <> "" Then CMBCOLOR.Text = OBJCOLOR.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTREMARKS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTREMARKS.Validating
        Try
            If CMBMERCHANT.Text.Trim <> "" Then
                EP.Clear()
                If Not errorvalid() Then
                    Exit Sub
                End If
                SAVE()
                FILLGRID()
                If ClientName = "KDFAB" Then
                    CMBDESIGNNO.Focus()
                    CMBCOLOR.Text = ""
                    CMBDESIGNNO.Text = ""
                    TXTREMARKS.Clear()
                Else
                    CMBMERCHANT.Focus()
                End If
            Else
                MsgBox("Enter Item Name", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTTO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTTO.KeyPress, TXTFROM.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTTO_Validating(sender As Object, e As CancelEventArgs) Handles TXTTO.Validating
        PRINTBARCODE()
    End Sub

    Private Sub SampleBarcode_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "KARAN" Then
                GREMARKS.Caption = "Rate"
                TXTREMARKS.TextAlign = HorizontalAlignment.Right
            End If

            If ClientName = "SHREENAKODA" Then
                LBLPARTYNAME.Visible = True
                CMBNAME.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTREMARKS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTREMARKS.KeyPress
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

                'DELETE FROM SAMPLEBARCODE
                Dim OBJSM As New ClsSampleBarcode
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

    Private Sub CMBQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            For I As Integer = 0 To gridbill.RowCount - 1
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                ROW("CHK") = CHKSELECTALL.Checked
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPRINT_Click(sender As Object, e As EventArgs) Handles CMDPRINT.Click
        Try
            PRINTBARCODE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, False, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS'", "SUNDRY DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNNO_Enter(sender As Object, e As EventArgs) Handles CMBDESIGNNO.Enter
        Try
            If CMBDESIGNNO.Text.Trim = "" Then FILLDESIGN(CMBDESIGNNO, CMBMERCHANT.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBMERCHANT_Validated(sender As Object, e As EventArgs) Handles CMBMERCHANT.Validated
        Try
            CMBDESIGNNO.Text = ""
            CMBCOLOR.Text = ""

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class