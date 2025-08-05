
Imports System.ComponentModel
Imports System.IO
Imports BL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class GDN

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK, GRIDSTOREDOUBLECLICK As Boolean
    Public EDIT As Boolean          'used for editing
    Public TEMPGDNNO As Integer     'used for poation no while editing
    Public TEMPPROFORMANO As Integer = 0
    Dim TEMPROW, TEMPSTOREROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer
    Dim TEMPMTRS As Double = 0.0
    Dim DTNEW As New DataTable
    Dim PRESENT As Boolean = False
    Dim PARTYCHALLANNO As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        FILLCMB()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
    Sub CLEAR()

        EP.Clear()
        'txtgdnno.ReadOnly = False
        txtgdnno.Clear()
        GDNDATE.Text = Now.Date
        CMBCONTRACTOR.Text = ""
        TXTDYEINGRECNO.Clear()

        If ALLOWMANUALGDNNO = True Then
            txtgdnno.ReadOnly = False
            txtgdnno.BackColor = Color.LemonChiffon
        Else
            txtgdnno.ReadOnly = True
            txtgdnno.BackColor = Color.Linen
        End If

        TXTTYPECHALLANNO.Clear()
        If CMBTYPE.Items.Count > 0 Then
            CMBTYPE.Enabled = True
            CMBTYPE.SelectedIndex = 0
            GETMAXTYPECHALLANNO()
        End If
        TabControl1.SelectedIndex = 0

        tstxtbillno.Clear()
        TXTSONO.Enabled = True
        TXTMULTISONO.Clear()
        TXTMERCHANT.Clear()
        If ClientName <> "ALENCOT" And ClientName <> "AMAN" Then cmbname.Text = ""
        If USERGODOWN <> "" Then CMBGODOWN.Text = USERGODOWN Else CMBGODOWN.Text = ""
        cmbname.Enabled = True
        txtadd.Clear()
        TXTCONSIGNEE.Clear()
        CMBHASTE.Text = ""
        CMBAGENT.Text = ""
        TXTDELIVERYADDRESS.Clear()
        txtpono.Clear()
        podate.Value = Now.Date
        TXTSONO.Clear()
        SODATE.Value = Now.Date

        CHKHOLD.Checked = False
        CMBTRANS.Text = ""
        TXTLRNO.Clear()
        LRDATE.Value = Now.Date
        CMBDISPATCHTO.Text = ""
        TXTDISPATCHADDRESS.Clear()

        If ClientName = "PARAS" Then
            LBLCHALLAN.Text = "Fold"
            txttransref.Text = "100CMS"
        Else
            txttransref.Clear()
        End If
        TXTTRANSREMARKS.Clear()

        cmbcity.Text = ""
        If ClientName = "SOFTAS" Or ClientName = "AVIS" Or ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
            TXTBALENOFROM.Text = 1
        Else
            TXTBALENOFROM.Clear()
            CMBPIECETYPE.Text = ""
        End If

        TXTBALENOTO.Clear()
        txtremarks.Clear()
        CMDSELECTSTOCK.Enabled = True
        lbllocked.Visible = False
        PBlock.Visible = False
        SALELOCK.Visible = False

        'clearing itemgrid textboxes and combos
        GRIDGDN.RowCount = 0
        GRIDORDER.RowCount = 0
        cmdselectOrder.Enabled = True
        cmdselectps.Enabled = True
        GRIDDOUBLECLICK = False
        GRIDSTOREDOUBLECLICK = False
        GETMAXNO()

        LBLTOTALBALES.Text = 0
        LBLTOTALMTRS.Text = 0
        LBLTOTALPCS.Text = 0
        LBLAMOUNT.Text = 0

        txtsrno.Text = GRIDGDN.RowCount + 1
        CMBITEMNAME.Text = ""
        If ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
            CMBQUALITY.Text = "FINISHED"
            CMBPIECETYPE.Text = "FRESH"
        Else
            CMBQUALITY.Text = ""
        End If

        If ClientName = "SMS" Or ClientName = "KREEVE" Or ClientName = "LEEFABRICO" Or ClientName = "BALAJI" Then CMBPIECETYPE.Text = "FRESH"

        TXTDESCRIPTION.Clear()
        CMBDESIGN.Text = ""
        CMBCOLOR.Text = ""
        TXTBALENO.Clear()
        TXTPCS.Clear()
        TXTCUT.Clear()
        TXTMTRS.Clear()
        TXTRATE.Clear()
        If ClientName <> "MOMAI" And ClientName <> "KREEVE" Then CMBPER.SelectedIndex = 0
        TXTAMOUNT.Clear()
        TXTCOPIES.Text = 2
        TXTMOBILENO.Clear()
        If ClientName <> "SMS" And ClientName <> "SHUBHI" And ClientName <> "SUBHLAXMI" And ClientName <> "KOTHARI" And ClientName <> "KOTHARINEW" Then CHKHIDEPCS.CheckState = CheckState.Unchecked
        LBLWHATSAPP.Visible = False
        TXTGRIDLOTNO.Clear()
        TXTSTOCK.Clear()

        GRIDCONSUME.RowCount = 0
        TXTSTORESRNO.Text = GRIDCONSUME.RowCount + 1
        CMBSTOREITEMNAME.Text = ""
        TXTSTOREQTY.Clear()
        CMBSTOREUNIT.Text = ""

    End Sub

    Sub TOTAL()
        Try
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALPCS.Text = 0.0
            LBLAMOUNT.Text = 0.0

            If GRIDGDN.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDGDN.Rows
                    If Val(row.Cells(GCUT.Index).EditedFormattedValue) > 0 And Val(row.Cells(Gpcs.Index).EditedFormattedValue) > 0 Then row.Cells(Gmtrs.Index).Value = Format(Val(row.Cells(Gpcs.Index).EditedFormattedValue) * Val(row.Cells(GCUT.Index).EditedFormattedValue), "0.00")
                    If row.Cells(GPER.Index).EditedFormattedValue = "Pcs" Then
                        row.Cells(GAMOUNT.Index).Value = Format(Val(row.Cells(GRATE.Index).EditedFormattedValue) * Val(row.Cells(Gpcs.Index).EditedFormattedValue))
                    Else
                        row.Cells(GAMOUNT.Index).Value = Format(Val(row.Cells(GRATE.Index).EditedFormattedValue) * Val(row.Cells(Gmtrs.Index).EditedFormattedValue))
                    End If
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(row.Cells(Gmtrs.Index).EditedFormattedValue), "0.00")
                    LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(row.Cells(Gpcs.Index).EditedFormattedValue), "0.00")
                    LBLAMOUNT.Text = Format(Val(LBLAMOUNT.Text) + Val(row.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
                Next
            End If
            BALECOUNT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub BALECOUNT()
        Try
            LBLTOTALBALES.Text = 0
            Dim dic As New Dictionary(Of String, Integer)()
            Dim cellValue As String
            For i = 0 To GRIDGDN.Rows.Count - 1
                If Not GRIDGDN.Rows(i).IsNewRow Then
                    cellValue = GRIDGDN(GBALENO.Index, i).EditedFormattedValue.ToString()
                    If cellValue <> "" Then
                        If Not dic.ContainsKey(cellValue) Then
                            dic.Add(cellValue, 1)
                        Else
                            dic(cellValue) += 1
                        End If
                    End If
                End If
            Next
            LBLTOTALBALES.Text = Val(dic.Count)
            If ClientName = "PARAS" Or ClientName = "DILIP" Or ClientName = "DILIPNEW" Or ClientName = "VINIT" Then TXTBALENOFROM.Text = Val(LBLTOTALBALES.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        cmbname.Focus()
        TEMPPROFORMANO = 0
    End Sub

    Sub GETMAXNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(gdn_no),0) + 1 ", " GDN ", " and gdn_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then txtgdnno.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True
            Dim OBJCMN As New ClsCommon

            If Val(TXTBALENOTO.Text.Trim) > 0 And Val(TXTBALENOFROM.Text.Trim) > 0 Then
                If Val(TXTBALENOFROM.Text.Trim) > Val(TXTBALENOTO.Text.Trim) Then
                    EP.SetError(TXTBALENOTO, " From Bale No Cannot Be Greater Than To Bale No ")
                    bln = False
                End If
            End If


            If ClientName = "SONU" Then
                If CMBKPL.Text = "" Then CMBKPL.SelectedIndex = 0
                txttransref.Text = CMBKPL.Text.Trim
            End If

            If ClientName = "SNCM" Then
                If CMBAGENT.Text.Trim.Length = 0 Then
                    EP.SetError(CMBAGENT, " Please Fill Agent Name ")
                    bln = False
                End If
                If CMBTRANS.Text.Trim.Length = 0 Then
                    EP.SetError(CMBTRANS, " Please Fill Transporter Name ")
                    bln = False
                End If
                If CMBDISPATCHTO.Text.Trim.Length = 0 Then
                    EP.SetError(CMBDISPATCHTO, " Please Fill Dispatch To Name ")
                    bln = False
                End If
                If cmbcity.Text.Trim.Length = 0 Then
                    EP.SetError(cmbcity, " Please Fill City Name ")
                    bln = False
                End If

                'WE HAVE TO GIVE POPUP OF REMARKS
                If txtremarks.Text.Trim <> "" Then MsgBox(txtremarks.Text.Trim, MsgBoxStyle.Critical)
            End If


            'IF ALLOWPACKINGSLIP IS TRUE THEN FETCH NOOFBALES AUTO FROM LBLTOTALBALES\
            If (ALLOWPACKINGSLIP = True Or ClientName = "MONOGRAM" Or ClientName = "SNCM" Or ClientName = "BIGAPPLE") And Val(TXTBALENOFROM.Text.Trim) = 0 Then TXTBALENOFROM.Text = LBLTOTALBALES.Text.Trim
            If Val(TXTBALENOFROM.Text.Trim) = 0 And ClientName <> "SONU" And ClientName <> "NTC" And ClientName <> "MANSI" Then TXTBALENOFROM.Text = 1

            If cmbname.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, " Please Fill Party Name ")
                bln = False
            End If

            If CMBGODOWN.Text.Trim.Length = 0 Then
                EP.SetError(CMBGODOWN, " Please Select Godown")
                bln = False
            End If

            If (ClientName = "KOTHARI" Or ClientName = "KOTHARINEW") And TXTSONO.Text.Trim = "" Then
                EP.SetError(TXTSONO, " Enter SO No.")
                bln = False
            End If

            If ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                If CMBTYPE.Text.Trim = "" Then
                    EP.SetError(CMBTYPE, " Please Select Challan Type")
                    bln = False
                End If

                If Val(TXTTYPECHALLANNO.Text.Trim) = 0 Then
                    EP.SetError(CMBTYPE, " Please Select Challan Type")
                    bln = False
                End If
            End If

            If Val(txtgdnno.Text.Trim) = 0 Then
                EP.SetError(cmbname, "Invalid Challan No")
                bln = False
            End If

            If ClientName <> "SANGHVI" And ClientName <> "TINUMINU" And ClientName <> "KDFAB" And ClientName <> "DJIMPEX" And ALLOWMANUALGDNNO = True Then
                If Val(txtgdnno.Text.Trim) <> 0 And EDIT = False Then
                    Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(GDN.GDN_NO,0)  AS GDNNO", "", " GDN ", "  AND GDN.GDN_NO=" & txtgdnno.Text.Trim & " AND GDN.GDN_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        EP.SetError(txtgdnno, "Challan No Already Exists")
                        bln = False
                    End If
                End If
            End If

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, "Invoice Raised, Delete Invoice First")
                bln = False
            End If

            If SALELOCK.Visible = True Then
                EP.SetError(lbllocked, "Sale Return Raised, Delete Sale Return First")
                bln = False
            End If

            If GRIDGDN.RowCount = 0 Then
                EP.SetError(cmbname, "Fill Packing Slip Details")
                bln = False
            End If

            Dim POMANDATE As Boolean = False
            If cmbname.Text.Trim <> "" Then
                Dim DTLEDGER As DataTable = OBJCMN.SEARCH("ISNULL(ACC_POMANDATE,0) AS POMANDATE", "", "LEDGERS", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)
                POMANDATE = Convert.ToBoolean(DTLEDGER.Rows(0).Item("POMANDATE"))
            End If
            For Each ROW As DataGridViewRow In GRIDGDN.Rows
                'ALLOW USER TO KEEP PCS 0 COZ, THIS WILL HELP THEM TO MAINTAIN PCS WISE STOCK
                'If Val(ROW.Cells(Gpcs.Index).Value) = 0 Then
                '    EP.SetError(cmbname, "Pcs Cannot be 0")
                '    bln = False
                'End If


                'CHECK IF POMANDATE AND PARTYPO IS BLANK
                If POMANDATE = True And ROW.Cells(GPARTYPONO.Index).Value = "" Then
                    EP.SetError(cmbname, "Enter Party PO No in Sale Order")
                    bln = False
                    Exit For
                End If



                'CLEAR THE BACKCOLOR
                ROW.DefaultCellStyle.BackColor = Color.Empty


                If ClientName <> "MOMAI" And ClientName <> "AXIS" And ClientName <> "GELATO" And ClientName <> "KREEVE" Then
                    If Val(ROW.Cells(Gmtrs.Index).Value) = 0 Then
                        EP.SetError(cmbname, "Mtrs Cannot be 0")
                        bln = False
                    End If
                End If

                If Val(ROW.Cells(GSALEDONE.Index).Value) = 1 Then
                    lbllocked.Visible = True
                    EP.SetError(lbllocked, "Sale Return Raised, Delete Sale Return First")
                    bln = False
                End If


                'THIS CODE IS OF NO USE.. AS THEY HAVE SHIFTED TO BARCODESTOCK
                ''NEGATIVE STOCK NOT ALLLOWED
                'If ClientName = "RUCHITA" Then
                '    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL((SUM(PCS)-SUM(ISSPCS)),0) AS TOTALPCS, ISNULL((SUM(MTRS)-SUM(ISSMTRS)),0) AS TOTALMTRS ", "", " STOCKREGISTER ", " AND ITEMNAME = '" & ROW.Cells(GITEMNAME.Index).Value & "' AND DESIGNNO = '" & ROW.Cells(GDESIGN.Index).Value & "' AND COLOR = '" & ROW.Cells(GSHADE.Index).Value & "' and GODOWN = '" & CMBGODOWN.Text.Trim & "' AND YEARID = " & YearId)
                '    If DT.Rows.Count > 0 Then
                '        If Val(DT.Rows(0).Item("TOTALMTRS")) <= 0 Then
                '            ROW.DefaultCellStyle.BackColor = Color.LightGreen
                '            If MsgBox("Mtrs not Present, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                '                EP.SetError(cmbname, "Mtrs Not Present")
                '                bln = False
                '            End If
                '        End If
                '    Else
                '        ROW.DefaultCellStyle.BackColor = Color.LightGreen
                '        If MsgBox("Mtrs not Present, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                '            EP.SetError(cmbname, "Mtrs Not Present")
                '            bln = False
                '        End If
                '    End If
                'End If

                'CHECK WHETHER BARCODE IS USED OR NOT
                If EDIT = False And ROW.Cells(GBARCODE.Index).Value <> "" And ALLOWBARCODEPRINT = True And CHECKBARCODEERRORVALID = True Then
                    Dim DT As DataTable = OBJCMN.SEARCH("*", "", "OUTBARCODESTOCK", " AND BARCODE = '" & ROW.Cells(GBARCODE.Index).Value & "' AND YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        EP.SetError(cmbname, "Barcode Already Used")
                        bln = False
                    End If
                End If
            Next


            If (ClientName = "KARAN" Or ClientName = "AMAN" Or ClientName = "AARYA" Or ClientName = "BALAJI") And txttransref.Text.Trim = "" Then
                EP.SetError(txttransref, "Enter Challan No")
                bln = False
            End If



            'FOR ORDER CHECKING, FIRST REMOVE GDNQTY
            Dim TEMPORDERROWNO As Integer = -1
            Dim TEMPORDERMATCH As Boolean = False
            If GRIDORDER.RowCount > 0 Then

                For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                    ORDROW.Cells(OGDNQTY.Index).Value = 0
                    ORDROW.Cells(OGDNMTRS.Index).Value = 0
                Next


                For Each CHROW As DataGridViewRow In GRIDGDN.Rows
                    CHROW.Cells(GSONO.Index).Value = 0
                    CHROW.Cells(GSOSRNO.Index).Value = 0
                Next

                'GET MULTISONO
                Dim MULTISONO() As String = (From row As DataGridViewRow In GRIDORDER.Rows.Cast(Of DataGridViewRow)() Where Not row.IsNewRow Select CStr(row.Cells(OFROMNO.Index).Value)).Distinct.ToArray
                TXTMULTISONO.Clear()
                For Each a As String In MULTISONO
                    If TXTMULTISONO.Text = "" Then
                        TXTMULTISONO.Text = a
                    Else
                        TXTMULTISONO.Text = TXTMULTISONO.Text & "," & a
                    End If
                Next


                If SALEORDERONMTRS = False Then
                    For Each ROW As DataGridViewRow In GRIDGDN.Rows
                        For Each ORDROW As DataGridViewRow In GRIDORDER.Rows
                            If ROW.Cells(GITEMNAME.Index).Value = ORDROW.Cells(OITEMNAME.Index).Value And ROW.Cells(GDESIGN.Index).Value = ORDROW.Cells(ODESIGN.Index).Value And ROW.Cells(GSHADE.Index).Value = ORDROW.Cells(OCOLOR.Index).Value Then
                                TEMPORDERMATCH = True
                                'IF ITEM / DESIGN / SHADE IS MATCHED BUT THE QTY IS FULL THEN WE NEED TO KEEP THIS ROWNO IN TEMP AND NEED TO CHECK FURTHER ALSO
                                'IF WE GET ANY NEW MATHING THEN WE NEED TO INSERT THERE
                                'IF NO MATCHING IS FOUND IN FURTHER ROWS THEN WE NEED TO ADD QTY IN THIS TEMPROW
                                If Val(ORDROW.Cells(OGDNQTY.Index).Value) >= Val(ORDROW.Cells(OPCS.Index).Value) Then
                                    TEMPORDERROWNO = ORDROW.Index

                                    'DONT ALLOW EXCESS PCS
                                    If ClientName = "MAHAVIRPOLYCOT" Or ClientName = "SIDDHGIRI" Then
                                        TEMPORDERROWNO = -1
                                        TEMPORDERMATCH = False
                                        ROW.DefaultCellStyle.BackColor = Color.LightGreen
                                    End If

                                    GoTo CHECKNEXTLINE
                                End If
                                ORDROW.Cells(OGDNQTY.Index).Value = Val(ORDROW.Cells(OGDNQTY.Index).Value) + Val(ROW.Cells(Gpcs.Index).Value)
                                ROW.Cells(GRATE.Index).Value = Val(ORDROW.Cells(ORATE.Index).Value)
                                ROW.Cells(GSONO.Index).Value = Val(ORDROW.Cells(OFROMNO.Index).Value)
                                ROW.Cells(GSOSRNO.Index).Value = Val(ORDROW.Cells(OFROMSRNO.Index).Value)
                                ROW.Cells(GPARTYPONO.Index).Value = ORDROW.Cells(OPARTYPONO.Index).Value
                                TEMPORDERROWNO = -1
                                Exit For
CHECKNEXTLINE:
                            End If
                        Next
                        'IF NO FURTHER MACHING IS FOUND BUT WE HAVE TEMPORDERROWNO THEN ADD VALUE IN THAT ROW
                        If TEMPORDERROWNO >= 0 Then
                            GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNQTY.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNQTY.Index).Value) + Val(ROW.Cells(Gpcs.Index).Value)
                            ROW.Cells(GRATE.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(ORATE.Index).Value)
                            ROW.Cells(GSONO.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OFROMNO.Index).Value)
                            ROW.Cells(GSOSRNO.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OFROMSRNO.Index).Value)
                            ROW.Cells(GPARTYPONO.Index).Value = GRIDORDER.Rows(TEMPORDERROWNO).Cells(OPARTYPONO.Index).Value
                            TEMPORDERROWNO = -1
                        End If
                        If TEMPORDERMATCH = False Then
                            ROW.DefaultCellStyle.BackColor = Color.LightGreen

                            'SALEORDER MANDATORY 
                            If ClientName = "AVIS" Or ClientName = "NAYRA" Or ClientName = "SIDDHGIRI" Or ClientName = "SUPRIYA" Then
                                EP.SetError(cmbname, "There are Items which are not Present in Selected Order")
                                bln = False
                            Else
                                If MsgBox("There are Items which are not Present in Selected Order, Wish to Proceed", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    EP.SetError(cmbname, "There are Items which are not Present in Selected Order")
                                    bln = False
                                End If
                            End If


                        End If
                        TEMPORDERMATCH = False
                    Next

                Else

                    'FOR SALEORDER ON MTRS
                    For Each ROW As DataGridViewRow In GRIDGDN.Rows
                        For Each ORDROW As DataGridViewRow In GRIDORDER.Rows

                            'FOR SNCM WE ARE MATCHING ONLY ITEM AND DESIGN
                            If (ROW.Cells(GITEMNAME.Index).Value = ORDROW.Cells(OITEMNAME.Index).Value And ROW.Cells(GDESIGN.Index).Value = ORDROW.Cells(ODESIGN.Index).Value And ClientName = "SNCM") Or (ROW.Cells(GITEMNAME.Index).Value = ORDROW.Cells(OITEMNAME.Index).Value And ROW.Cells(GDESIGN.Index).Value = ORDROW.Cells(ODESIGN.Index).Value And ROW.Cells(GSHADE.Index).Value = ORDROW.Cells(OCOLOR.Index).Value) Then
                                TEMPORDERMATCH = True
                                'IF ITEM / DESIGN / SHADE IS MATCHED BUT THE QTY IS FULL THEN WE NEED TO KEEP THIS ROWNO IN TEMP AND NEED TO CHECK FURTHER ALSO
                                'IF WE GET ANY NEW MATHING THEN WE NEED TO INSERT THERE
                                'IF NO MATCHING IS FOUND IN FURTHER ROWS THEN WE NEED TO ADD QTY IN THIS TEMPROW
                                If Val(ORDROW.Cells(OGDNMTRS.Index).Value) >= Val(ORDROW.Cells(OMTRS.Index).Value) Then
                                    TEMPORDERROWNO = ORDROW.Index
                                    GoTo CHECKNEXTLINEMTRS
                                End If
                                ORDROW.Cells(OGDNMTRS.Index).Value = Val(ORDROW.Cells(OGDNMTRS.Index).Value) + Val(ROW.Cells(Gmtrs.Index).Value)
                                ROW.Cells(GRATE.Index).Value = Val(ORDROW.Cells(ORATE.Index).Value)
                                ROW.Cells(GSONO.Index).Value = Val(ORDROW.Cells(OFROMNO.Index).Value)
                                ROW.Cells(GSOSRNO.Index).Value = Val(ORDROW.Cells(OFROMSRNO.Index).Value)
                                ROW.Cells(GPARTYPONO.Index).Value = ORDROW.Cells(OPARTYPONO.Index).Value
                                TEMPORDERROWNO = -1
                                Exit For
CHECKNEXTLINEMTRS:
                            End If
                        Next
                        'IF NO FURTHER MACHING IS FOUND BUT WE HAVE TEMPORDERROWNO THEN ADD VALUE IN THAT ROW
                        If TEMPORDERROWNO >= 0 Then
                            GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNMTRS.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OGDNMTRS.Index).Value) + Val(ROW.Cells(Gmtrs.Index).Value)
                            ROW.Cells(GRATE.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(ORATE.Index).Value)
                            ROW.Cells(GSONO.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OFROMNO.Index).Value)
                            ROW.Cells(GSOSRNO.Index).Value = Val(GRIDORDER.Rows(TEMPORDERROWNO).Cells(OFROMSRNO.Index).Value)
                            ROW.Cells(GPARTYPONO.Index).Value = GRIDORDER.Rows(TEMPORDERROWNO).Cells(OPARTYPONO.Index).Value
                            TEMPORDERROWNO = -1
                        End If
                        If TEMPORDERMATCH = False Then
                            ROW.DefaultCellStyle.BackColor = Color.LightGreen

                            'SALEORDER MANDATORY 
                            If ClientName = "AVIS" Or ClientName = "NAYRA" Or ClientName = "SIDDHGIRI" Or ClientName = "SUPRIYA" Or ClientName = "SNCM" Then
                                EP.SetError(cmbname, "There are Items which are not Present in Selected Order")
                                bln = False
                            Else
                                If MsgBox("There are Items which are not Present in Selected Order, Wish to Proceed", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    EP.SetError(cmbname, "There are Items which are not Present in Selected Order")
                                    bln = False
                                End If
                            End If
                        End If
                        TEMPORDERMATCH = False
                    Next
                End If

            End If

            If (ClientName = "AVIS" Or ClientName = "SUPEEMA" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "NAYRA" Or ClientName = "SIDDHGIRI" Or ClientName = "SUPRIYA" Or ClientName = "SNCM") And GRIDORDER.RowCount = 0 And CHALLANWITHOUTSO = False Then
                EP.SetError(cmbname, "Please Select Sale Order")
                bln = False
            End If


            If ClientName = "MAHAVIRPOLYCOT" And GRIDORDER.RowCount = 0 Then
                If MsgBox("Sale Order Not Selected, Wish to Proceed without Order?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    EP.SetError(cmbname, "Please Select Sale Order")
                    bln = False
                End If
            End If


            If txttransref.Text.Trim <> "" And ClientName <> "KCRAYON" And ClientName <> "DJIMPEX" And ClientName <> "PARAS" And ClientName <> "SONU" And ClientName <> "AMAN" And ClientName <> "AARYA" And ClientName <> "RAJKRIPA" Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(txttransref.Text.Trim)) Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    Dim DT As DataTable = objclscommon.SEARCH(" GDN_TRANSREFNO", "", " GDN", " and GDN_TRANSREFNO= '" & txttransref.Text.Trim & "' AND GDN_YEARID =" & YearId)
                    If DT.Rows.Count > 0 Then
                        EP.SetError(txttransref, "Challan No. Already Exists")
                        bln = False
                    End If
                End If
            End If


            If GDNDATE.Text = "__/__/____" Then
                EP.SetError(GDNDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(GDNDATE.Text) Then
                    EP.SetError(GDNDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            If ALLOWMANUALGDNNO = True Then
                If txtgdnno.Text <> "" And cmbname.Text.Trim <> "" And EDIT = False Then
                    Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(GDN.GDN_NO,0)  AS GDNNO", "", " GDN ", "  AND GDN.GDN_NO=" & txtgdnno.Text.Trim & " AND GDN.GDN_YEARID = " & YearId)

                    If dttable.Rows.Count > 0 Then
                        EP.SetError(txtgdnno, "Challan No Already Exist")
                        bln = False
                    End If
                End If
            End If

            If (ClientName = "KOTHARI" Or ClientName = "KOTHARINEW") And Val(TXTBALENOFROM.Text.Trim) = 0 Then
                EP.SetError(TXTBALENOFROM, " Enter No of Bales")
                bln = False
            End If

            If TXTSONO.Text.Trim <> "" And TXTMULTISONO.Text.Trim = "" Then TXTMULTISONO.Text = TXTSONO.Text.Trim


            'call total here
            TOTAL()

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub cmdselectOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdselectOrder.Click
        Try
            If cmbname.Text.Trim = "" Then
                MsgBox("Select Party Name", MsgBoxStyle.Critical)
                cmbname.Focus()
                Exit Sub
            End If

            Dim DTSO As New DataTable
            Dim OBJSELECTSO As New SelectSO
            OBJSELECTSO.PARTYNAME = cmbname.Text.Trim
            OBJSELECTSO.ShowDialog()
            DTSO = OBJSELECTSO.DT

            If DTSO.Rows.Count > 0 Then

                ''  GETTING DISTINCT CHALLAN NO IN TEXTBOX
                Dim DV As DataView = DTSO.DefaultView
                Dim NEWDT As DataTable = DV.ToTable(True, "SONO")
                For Each DTR As DataRow In NEWDT.Rows
                    If TXTMULTISONO.Text.Trim = "" Then
                        TXTMULTISONO.Text = DTR("SONO").ToString
                    Else
                        TXTMULTISONO.Text = TXTMULTISONO.Text & "," & DTR("SONO").ToString
                    End If
                Next

                TXTSONO.Text = DTSO.Rows(0).Item("SONO")
                TXTSOGRIDSRNO.Text = DTSO.Rows(0).Item("GRIDSRNO")
                SODATE.Value = DTSO.Rows(0).Item("DATE")


                ''  GETTING DISTINCT PONO NO IN TEXTBOX
                DV = DTSO.DefaultView
                NEWDT = DV.ToTable(True, "PONO")
                For Each DTR As DataRow In NEWDT.Rows
                    If txtpono.Text.Trim = "" Then
                        txtpono.Text = DTR("PONO").ToString
                        TXTTRANSREMARKS.Text = DTR("PONO").ToString
                    Else
                        txtpono.Text = txtpono.Text & "," & DTR("PONO").ToString
                        TXTTRANSREMARKS.Text = TXTTRANSREMARKS.Text & "," & DTR("PONO").ToString
                    End If
                Next

                'WE NEED DISTINCT PONO WITH , SO WE HAVE WRITTEN THE ABOVE CODE
                'txtpono.Text = DTSO.Rows(0).Item("PONO")
                'TXTTRANSREMARKS.Text = DTSO.Rows(0).Item("PONO")



                CMBAGENT.Text = DTSO.Rows(0).Item("AGENTNAME")
                If DTSO.Rows(0).Item("TRANSNAME") <> "" Then CMBTRANS.Text = DTSO.Rows(0).Item("TRANSNAME")
                cmbcity.Text = DTSO.Rows(0).Item("CITYNAME")
                CMBDISPATCHTO.Text = DTSO.Rows(0).Item("DELIVERYAT")
                If ClientName = "MOHAN" Then
                    txttransref.Text = DTSO.Rows(0).Item("REFNO")
                    TXTTRANSREMARKS.Text = DTSO.Rows(0).Item("PONO")
                End If
                If ClientName = "RAJKRIPA" Then txttransref.Text = DTSO.Rows(0).Item("PACKINGTYPE")
                If ClientName = "SIDDHGIRI" Or ClientName = "SNCM" Then txtremarks.Text = DTSO.Rows(0).Item("REMARKS")
                TXTSONO.Enabled = False


                Dim CUT As Double = 0.00
                Dim PER As String = "Pcs"

                'BEFORE ADDING THE ROW IN ORDERDER GRID CHECK WHETHER SAME ORDERNO AN SRNO IS PRESENT IN GRID OR NOT
                For Each DTROW As DataRow In DTSO.Rows
                    For Each ROW As DataGridViewRow In GRIDORDER.Rows
                        If Val(ROW.Cells(OFROMNO.Index).Value) = Val(DTROW("SONO")) And Val(ROW.Cells(OFROMSRNO.Index).Value) = Val(DTROW("GRIDSRNO")) And ROW.Cells(OFROMTYPE.Index).Value = DTROW("TYPE") Then GoTo NEXTLINE
                    Next
                    GRIDORDER.Rows.Add(0, DTROW("ITEMNAME"), DTROW("DESIGN"), DTROW("COLOR"), DTROW("QTY"), DTROW("MTRS"), DTROW("SONO"), DTROW("GRIDSRNO"), DTROW("TYPE"), 0, 0, Val(DTROW("RATE")), DTROW("GRIDPARTYPONO"))
                    'ADD DATA OF SALE ORDER IN GDN GRID ALSO
                    If ClientName <> "SIDDHGIRI" Then CUT = Format(Val(DTROW("MTRS")) / Val(DTROW("QTY")), "0.00")
                    'GET PER FROM ITEMMASTER
                    If ClientName = "SIDDHGIRI" Then
                        PER = "Mtrs"
                        Dim OBJCMN As New ClsCommon
                        Dim DTITEM As DataTable = OBJCMN.SEARCH(" ISNULL(UNIT_ABBR,'') AS UNIT", "", " ITEMMASTER LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID ", " AND ITEM_NAME = '" & DTROW("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                        If DTITEM.Rows.Count > 0 AndAlso LCase(DTITEM.Rows(0).Item("UNIT")) = "pcs" Then PER = "Pcs"
                    End If




                    If ClientName = "KREEVE" Or ClientName = "SMS" Then GRIDGDN.Rows.Add(0, "FRESH", DTROW("ITEMNAME"), "", "", DTROW("DESIGN"), DTROW("COLOR"), DTROW("GRIDPARTYPONO"), "", Format(Val(DTROW("QTY")), "0"), PER, CUT, Format(Val(DTROW("MTRS")), "0.00"), Format(Val(DTROW("RATE")), "0.00"), PER, 0, "", 0, 0, "", 0, DTROW("SONO"), DTROW("GRIDSRNO"), "")

                    If ClientName = "SNCM" Then
                        Dim OBJCMN As New ClsCommon
                        Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(so_no, 0) AS SONO, ISNULL(so_remarks, '') AS REMARKS", "", " SALEORDER", " AND SO_NO = '" & DTROW("SONO") & "' AND SO_YEARID = " & YearId)
                        If DT.Rows.Count > 0 Then
                            If DT.Rows(0).Item("REMARKS") <> "" Then MsgBox(DT.Rows(0).Item("REMARKS"), MsgBoxStyle.Critical)
                        End If
                    End If


NEXTLINE:
                Next
                getsrno(GRIDORDER)
                getsrno(GRIDGDN)

            End If

            cmdselectps.Enabled = True
            TOTAL()
            If ClientName = "AFW" Then CMBTRANS.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub RECOSAVE()
        Try

            Dim alParaval As New ArrayList

            alParaval.Add(0)    'manualNO
            alParaval.Add(Format(Convert.ToDateTime(GDNDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBGODOWN.Text.Trim)


            alParaval.Add("")    'TRANS
            alParaval.Add("")    'CHALLAN

            alParaval.Add(Val(LBLTOTALPCS.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(0)    'INPCS
            alParaval.Add(0)    'INMTRS

            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)


            Dim gridsrno As String = ""
            Dim PIECETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim LOTNO As String = ""
            Dim PCS As String = ""
            Dim MTRS As String = ""

            Dim BARCODE As String = "" 'BARCODE ADDED
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""
            Dim RATE As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""



            For Each row As Windows.Forms.DataGridViewRow In GRIDGDN.Rows
                Dim objclscommon As New ClsCommonMaster
                Dim dt1 As DataTable = objclscommon.search(" ISNULL(MTRS,0) AS MTRS ", "", " BARCODESTOCK", " AND GODOWN='" & CMBGODOWN.Text.Trim & "' AND FROMNO= " & Val(row.Cells(GFROMNO.Index).Value) & " AND FROMSRNO= " & Val(row.Cells(GFROMSRNO.Index).Value) & " AND TYPE='" & row.Cells(GFROMTYPE.Index).Value & "' AND Yearid = " & YearId)
                If dt1.Rows.Count > 0 Then
                    If Val(dt1.Rows(0).Item(0)) <= 3 Then
                        PRESENT = True
                        TEMPMTRS = Val(dt1.Rows(0).Item(0))
                        If gridsrno = "" Then
                            gridsrno = row.Cells(GSRNO.Index).Value.ToString
                            PIECETYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                            ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                            QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                            DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                            COLOR = row.Cells(GSHADE.Index).Value.ToString
                            LOTNO = row.Cells(GGRIDLOTNO.Index).Value.ToString
                            PCS = row.Cells(Gpcs.Index).Value.ToString
                            MTRS = TEMPMTRS
                            RATE = row.Cells(GRATE.Index).Value
                            PER = row.Cells(GPER.Index).Value
                            AMOUNT = row.Cells(GAMOUNT.Index).Value
                            BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                            FROMNO = row.Cells(GFROMNO.Index).Value.ToString
                            FROMSRNO = row.Cells(GFROMSRNO.Index).Value.ToString
                            FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString

                        Else
                            gridsrno = gridsrno & "," & row.Cells(GSRNO.Index).Value.ToString
                            PIECETYPE = PIECETYPE & "," & row.Cells(GPIECETYPE.Index).Value.ToString
                            ITEMNAME = ITEMNAME & "," & row.Cells(GITEMNAME.Index).Value.ToString
                            QUALITY = QUALITY & "," & row.Cells(GQUALITY.Index).Value.ToString
                            DESIGN = DESIGN & "," & row.Cells(GDESIGN.Index).Value.ToString
                            COLOR = COLOR & "," & row.Cells(GSHADE.Index).Value.ToString
                            LOTNO = LOTNO & "," & row.Cells(GGRIDLOTNO.Index).Value.ToString
                            PCS = PCS & "," & row.Cells(Gpcs.Index).Value.ToString
                            MTRS = MTRS & "," & TEMPMTRS
                            RATE = RATE & "|" & row.Cells(GRATE.Index).Value
                            PER = PER & "|" & row.Cells(GPER.Index).Value
                            AMOUNT = AMOUNT & "|" & row.Cells(GAMOUNT.Index).Value
                            BARCODE = BARCODE & "," & row.Cells(GBARCODE.Index).Value.ToString
                            FROMNO = FROMNO & "," & row.Cells(GFROMNO.Index).Value.ToString
                            FROMSRNO = FROMSRNO & "," & row.Cells(GFROMSRNO.Index).Value.ToString
                            FROMTYPE = FROMTYPE & "," & row.Cells(GFROMTYPE.Index).Value.ToString

                        End If

                    End If
                End If
            Next


            alParaval.Add(gridsrno)
            alParaval.Add(PIECETYPE)
            alParaval.Add(ITEMNAME)
            alParaval.Add(QUALITY)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(LOTNO)
            alParaval.Add(PCS)
            alParaval.Add(MTRS)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMOUNT)
            alParaval.Add(BARCODE)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)


            Dim INGRIDSRNO As String = ""
            Dim INPIECETYPE As String = ""
            Dim INITEMNAME As String = ""
            Dim INQUALITY As String = ""
            Dim INBALENO As String = ""
            Dim INGRIDDESC As String = ""
            Dim INLOTNO As String = ""
            Dim INDESIGN As String = ""
            Dim INCOLOR As String = ""
            Dim INCUT As String = ""
            Dim INPCS As String = ""
            Dim INQTYUNIT As String = ""
            Dim INMTRS As String = ""
            Dim INRACK As String = ""
            Dim INSHELF As String = ""
            Dim INBARCODE As String = ""
            Dim INDONE As String = ""
            Dim INOUTPCS As String = ""
            Dim INOUTMTRS As String = ""

            alParaval.Add(INGRIDSRNO)
            alParaval.Add(INPIECETYPE)
            alParaval.Add(INITEMNAME)
            alParaval.Add(INQUALITY)
            alParaval.Add(INBALENO)
            alParaval.Add(INGRIDDESC)
            alParaval.Add(INLOTNO)
            alParaval.Add(INDESIGN)
            alParaval.Add(INCOLOR)
            alParaval.Add(INCUT)
            alParaval.Add(INPCS)
            alParaval.Add(INQTYUNIT)
            alParaval.Add(INMTRS)
            alParaval.Add(0)      'INMTRS
            alParaval.Add("Mtrs")      'INPER
            alParaval.Add(0)      'INAMOUNT
            alParaval.Add(INRACK)
            alParaval.Add(INSHELF)
            alParaval.Add(INBARCODE)
            alParaval.Add(INDONE)
            alParaval.Add(INOUTPCS)
            alParaval.Add(INOUTMTRS)

            alParaval.Add("")   'NAME



            alParaval.Add(LBLAMOUNT.Text.Trim)
            alParaval.Add(0)
            alParaval.Add(0)   'AVGRATE

            Dim objclsPurord As New ClsStockAdjustment()
            objclsPurord.alParaval = alParaval

            If PRESENT = True Then Dim DT As DataTable = objclsPurord.SAVE()
            MsgBox("Reco done Successfully!")
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

    Private Sub GDN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) And ClientName <> "RUCHITA" Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNoCancel)
                    If tempmsg = vbCancel Then Exit Sub
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call PrintToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.OemQuotes Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F5 Then
                GRIDGDN.Focus()
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolPREVIOUS_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_CLICK(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.D1 Then
                TabControl1.SelectedIndex = 0
            ElseIf e.Alt = True And e.KeyCode = Keys.D2 Then
                TabControl1.SelectedIndex = 1
            ElseIf e.Alt = True And e.KeyCode = Keys.D3 Then
                TabControl1.SelectedIndex = 2
            ElseIf e.Alt = True And e.KeyCode = Keys.D4 Then
                TabControl1.SelectedIndex = 3
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
            If CMBAGENT.Text.Trim = "" Then fillagentledger(CMBAGENT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
            If CMBTRANS.Text.Trim = "" Then filltransname(CMBTRANS, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
            If cmbcity.Text.Trim = "" Then fillCITY(cmbcity, EDIT)
            If CMBHASTE.Text.Trim = "" Then filljobbername(CMBHASTE, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")

            If ClientName = "AVIS" Then
                If CMBDISPATCHTO.Text.Trim = "" Then FILLNAME(CMBDISPATCHTO, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')  AND GROUP_NAME = 'HASTE DEBTORS'  AND ACC_TYPE = 'ACCOUNTS'")
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUP_NAME <> 'HASTE DEBTORS'")
            Else
                If CMBDISPATCHTO.Text.Trim = "" Then FILLNAME(CMBDISPATCHTO, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')  AND ACC_TYPE = 'ACCOUNTS'")
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            End If
            FILLCHALLANTYPE(CMBTYPE)
            FILLCONTRACT(CMBCONTRACTOR)
            If CMBSTOREITEMNAME.Text.Trim = "" Then FILLSTOREITEMNAME(CMBSTOREITEMNAME)


            'GRID COMBO FIELDS
            If SHOWGDNCOLUMNS = True Then
                fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
                fillQUALITY(CMBQUALITY, EDIT)
                FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
                FILLCOLOR(CMBCOLOR, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
                fillPIECETYPE(CMBPIECETYPE)
                If CMBSTOREUNIT.Text.Trim = "" Then fillunit(CMBSTOREUNIT)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSTOREUNIT_Enter(sender As Object, e As EventArgs) Handles CMBSTOREUNIT.Enter
        Try
            If CMBSTOREUNIT.Text.Trim = "" Then fillunit(CMBSTOREUNIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSTOREUNIT_Validating(sender As Object, e As CancelEventArgs) Handles CMBSTOREUNIT.Validating
        Try
            If CMBSTOREUNIT.Text.Trim <> "" Then unitvalidate(CMBSTOREUNIT, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBUNIT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSTOREUNIT.Validated
        Try
            If CMBSTOREITEMNAME.Text.Trim <> "" And Val(TXTSTOREQTY.Text.Trim) > 0 And CMBSTOREUNIT.Text.Trim <> "" Then
                FILLSTOREGRID()
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSTOREGRID()
        Try
            If GRIDSTOREDOUBLECLICK = False Then
                GRIDCONSUME.Rows.Add(0, CMBSTOREITEMNAME.Text.Trim, Val(TXTSTOREQTY.Text.Trim), CMBSTOREUNIT.Text.Trim)


                'WE WILL CHECK STOREITEMMASTER_DESC FOR MULTIPLE ITEMS REGARDING THIS ITEM, IF PRESENT THEN ADD THOSE ITEMS ALSO
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" GRIDSTOREITEMMASTER.STOREITEM_NAME AS STOREITEMNAME, STOREITEMMASTER_DESC.STOREITEM_QTY AS STOREQTY ", "", " STOREITEMMASTER_DESC INNER JOIN STOREITEMMASTER ON STOREITEMMASTER_DESC.STOREITEM_ID = STOREITEMMASTER.STOREITEM_ID INNER JOIN STOREITEMMASTER AS GRIDSTOREITEMMASTER ON STOREITEMMASTER_DESC.STOREITEM_ITEMID = GRIDSTOREITEMMASTER.STOREITEM_ID ", " AND STOREITEMMASTER.STOREITEM_NAME = '" & CMBSTOREITEMNAME.Text.Trim & "' AND STOREITEMMASTER.STOREITEM_YEARID = " & YearId)
                For Each DTROW As DataRow In DT.Rows
                    GRIDCONSUME.Rows.Add(0, DTROW("STOREITEMNAME"), Val(DTROW("STOREQTY")), CMBSTOREUNIT.Text.Trim)
                Next


            ElseIf GRIDSTOREDOUBLECLICK = True Then
                GRIDCONSUME.Item(SSTOREITEMNAME.Index, TEMPROW).Value = CMBSTOREITEMNAME.Text.Trim
                GRIDCONSUME.Item(SSTOREQTY.Index, TEMPROW).Value = Val(TXTSTOREQTY.Text.Trim)
                GRIDCONSUME.Item(SSTOREUNIT.Index, TEMPROW).Value = CMBSTOREUNIT.Text.Trim
                GRIDSTOREDOUBLECLICK = False
            End If

            getsrno(GRIDCONSUME)
            GRIDCONSUME.FirstDisplayedScrollingRowIndex = GRIDCONSUME.RowCount - 1

            CMBSTOREITEMNAME.Focus()
            CMBSTOREITEMNAME.Text = ""
            TXTSTOREQTY.Clear()
            CMBSTOREUNIT.Text = ""

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCONSUME_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCONSUME.CellDoubleClick
        Try
            If GRIDCONSUME.CurrentRow.Index >= 0 And GRIDCONSUME.Item(SSTOREITEMNAME.Index, GRIDCONSUME.CurrentRow.Index).Value <> Nothing Then

                GRIDSTOREDOUBLECLICK = True
                CMBSTOREITEMNAME.Text = GRIDCONSUME.Item(SSTOREITEMNAME.Index, GRIDCONSUME.CurrentRow.Index).Value.ToString
                TXTSTOREQTY.Text = GRIDCONSUME.Item(SSTOREQTY.Index, GRIDCONSUME.CurrentRow.Index).Value.ToString
                CMBSTOREUNIT.Text = GRIDCONSUME.Item(SSTOREUNIT.Index, GRIDCONSUME.CurrentRow.Index).Value.ToString

                TEMPSTOREROW = GRIDCONSUME.CurrentRow.Index
                CMBSTOREITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCONSUME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCONSUME.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCONSUME.RowCount > 0 Then
                If GRIDSTOREDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDCONSUME.Rows.RemoveAt(GRIDCONSUME.CurrentRow.Index)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETDATA()
        Try
            Dim OBJCLSPROFORMA As New ClsProforma()
            Dim dttable As DataTable = OBJCLSPROFORMA.SELECTPROFORMA(TEMPPROFORMANO, CmpId, Locationid, YearId)
            If dttable.Rows.Count > 0 Then
                For Each dr As DataRow In dttable.Rows
                    GRIDGDN.Rows.Add(GRIDGDN.RowCount + 1, dr("PIECETYPE"), dr("ITEMNAME").ToString, dr("QUALITY"), dr("PRINTDESC"), dr("DESIGN"), dr("COLOR"), dr("BALENO"), "", Format(Val(dr("PCS")), "0"), "", Format(Val(dr("CUT")), "0.00"), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER"), Format(Val(dr("AMOUNT")), "0.00"), dr("BARCODE"), dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"), 0, 0, 0, "")
                Next
                txtremarks.Text = "Proforma No - " & Val(TEMPPROFORMANO)
                TOTAL()
                GRIDGDN.FirstDisplayedScrollingRowIndex = GRIDGDN.RowCount - 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GDN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow

            DTROW = USERRIGHTS.Select("FormName = 'GDN'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor



            CLEAR()

            If TEMPPROFORMANO > 0 Then GETDATA()

            If ClientName = "MABHAY" Or ClientName = "SVS" Then Gpcs.ReadOnly = True


            If EDIT = True Then
                SHOWDATA()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub SHOWDATA()
        Try

            CLEAR()
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJCMN As New ClsCommon
            Dim objclsGDN As New ClsGDN()
            Dim dttable As New DataTable

            dttable = objclsGDN.SELECTGDN(TEMPGDNNO, CmpId, Locationid, YearId)
            If dttable.Rows.Count > 0 Then

                For Each dr As DataRow In dttable.Rows

                    txtgdnno.Text = TEMPGDNNO
                    txtgdnno.ReadOnly = True

                    CMBTYPE.Text = Convert.ToString(dr("CHALLANTYPE").ToString)
                    CMBTYPE.Enabled = False
                    TXTTYPECHALLANNO.Text = Val(dr("TYPECHALLANNO"))

                    CHKHOLD.Checked = Convert.ToBoolean(dr("HOLDFORAPPROVAL"))

                    GDNDATE.Text = Format(Convert.ToDateTime(dr("DATE")), "dd/MM/yyyy")
                    CMBGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                    cmbname.Text = Convert.ToString(dr("NAME").ToString)

                    TXTMOBILENO.Text = dr("MOBILENO")

                    'If ClientName <> "DAKSH" And ClientName <> "SHALIBHADRA" And ClientName <> "KARAN" And ClientName <> "" Then cmdselectOrder.Enabled = False

                    TXTCONSIGNEE.Text = Convert.ToString(dr("CONSIGNEE").ToString)
                    CMBHASTE.Text = Convert.ToString(dr("JOBBERNAME").ToString)
                    CMBAGENT.Text = Convert.ToString(dr("AGENT").ToString)

                    txtpono.Text = Convert.ToString(dr("PONO").ToString)
                    podate.Text = Format(Convert.ToDateTime(dr("PODATE")).Date, "dd/MM/yyyy")
                    TXTSONO.Text = Convert.ToString(dr("SONO").ToString)
                    TXTMULTISONO.Text = Convert.ToString(dr("MULTISONO").ToString)
                    SODATE.Text = Format(Convert.ToDateTime(dr("SODATE")).Date, "dd/MM/yyyy")


                    CMBTRANS.Text = dr("TRANSNAME").ToString
                    cmbcity.Text = dr("CITY").ToString
                    TXTLRNO.Text = dr("LRNO").ToString
                    LRDATE.Text = Format(Convert.ToDateTime(dr("LRDATE")).Date, "dd/MM/yyyy")
                    CMBRISK.Text = dr("RISK").ToString
                    TXTBALENOFROM.Text = Val(dr("BALENOFROM"))
                    TXTBALENOTO.Text = Val(dr("BALENOTO"))

                    CMBDISPATCHTO.Text = dr("DISPATCHTO").ToString
                    TXTDISPATCHADDRESS.Text = dr("DISPATCHADD").ToString
                    txttransref.Text = dr("transrefno").ToString
                    If txttransref.Text.Trim <> "" And ClientName = "SONU" Then CMBKPL.Text = txttransref.Text.Trim
                    PARTYCHALLANNO = txttransref.Text.Trim

                    TXTTRANSREMARKS.Text = dr("transremarks").ToString
                    CMBCONTRACTOR.Text = dr("CONTRACTOR")

                    txtremarks.Text = Convert.ToString(dr("REMARKS").ToString)
                    TXTMERCHANT.Text = Convert.ToString(dr("ITEMNAME").ToString)
                    GRIDGDN.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE"), dr("ITEMNAME").ToString, dr("QUALITY"), dr("PRINTDESC"), dr("DESIGN"), dr("COLOR"), dr("BALENO"), dr("GRIDLOTNO"), Format(Val(dr("PCS")), "0"), dr("UNIT"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("RATE")), "0.00"), dr("PER"), Format(Val(dr("AMOUNT")), "0.00"), dr("BARCODE"), dr("FROMNO"), dr("FROMSRNO"), dr("FROMTYPE"), dr("SALEDONE"), dr("GRIDSONO"), dr("GRIDSOSRNO"), dr("GRIDPARTYPONO"))

                    If Convert.ToBoolean(dr("DONE")) = True Or Val(dr("OUTMTRS")) > 0 Or Val(dr("OUTPCS")) > 0 Then
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If
                    If Val(dr("GRIDSONO")) = 0 And ClientName = "MAHAVIRPOLYCOT" Then GRIDGDN.Rows(GRIDGDN.RowCount - 1).DefaultCellStyle.BackColor = Color.LightGreen

                    If Convert.ToBoolean(dr("SMSSEND")) = True Then LBLSMS.Visible = True
                    If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True

                    If Convert.ToBoolean(dr("SALERETURN")) = True Then
                        lbllocked.Visible = True
                        SALELOCK.Visible = True
                    End If
                    If Convert.ToBoolean(dr("CHANGEADD")) = False Then CHKCHANGEADD.Checked = False Else CHKCHANGEADD.Checked = True
                    TXTDELIVERYADDRESS.Text = dr("DELIVERYADD")

                Next


                'ORDER GRID
                'Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.SEARCH(" GDN_SODETAILS.GDN_GRIDSRNO AS GRIDSRNO, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, GDN_SODETAILS.GDN_ORDERPCS AS ORDERQTY, ISNULL(GDN_SODETAILS.GDN_ORDERMTRS,0) AS ORDERMTRS, GDN_SODETAILS.GDN_FROMNO AS FROMNO, GDN_SODETAILS.GDN_FROMSRNO AS FROMSRNO, GDN_SODETAILS.GDN_FROMTYPE AS FROMTYPE, GDN_SODETAILS.GDN_PCS AS GDNQTY, ISNULL(GDN_SODETAILS.GDN_MTRS,0) AS GDNMTRS, ISNULL(GDN_SODETAILS.GDN_RATE,0) AS RATE, ISNULL(GDN_SODETAILS.GDN_PARTYPONO,'') AS PARTYPONO ", "", " GDN_SODETAILS INNER JOIN ITEMMASTER ON GDN_SODETAILS.GDN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON GDN_SODETAILS.GDN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON GDN_SODETAILS.GDN_DESIGNID = DESIGNMASTER.DESIGN_id  ", " AND GDN_SODETAILS.GDN_NO = " & TEMPGDNNO & " AND GDN_SODETAILS.GDN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        GRIDORDER.Rows.Add(Val(DTR("GRIDSRNO")), DTR("ITEMNAME"), DTR("DESIGNNO"), DTR("COLOR"), Val(DTR("ORDERQTY")), Val(DTR("ORDERMTRS")), Val(DTR("FROMNO")), Val(DTR("FROMSRNO")), DTR("FROMTYPE"), Val(DTR("GDNQTY")), Val(DTR("GDNMTRS")), Val(DTR("RATE")), DTR("PARTYPONO"))
                    Next
                End If
                getsrno(GRIDORDER)



                'STORE GRID
                dttable = OBJCMN.SEARCH("  GDN_STOREDETAILS.GDN_STORESRNO AS GRIDSTORESRNO, STOREITEMMASTER.STOREITEM_NAME AS STOREITEMNAME, GDN_STOREDETAILS.GDN_STOREQTY AS STOREQTY, UNITMASTER.unit_abbr AS STOREUNIT ", "", " GDN_STOREDETAILS INNER JOIN STOREITEMMASTER ON GDN_STOREDETAILS.GDN_STOREITEMID = STOREITEMMASTER.STOREITEM_ID INNER JOIN UNITMASTER ON GDN_STOREDETAILS.GDN_STOREUNITID = UNITMASTER.unit_id ", " AND GDN_STOREDETAILS.GDN_NO = " & TEMPGDNNO & " AND GDN_STOREDETAILS.GDN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        GRIDCONSUME.Rows.Add(Val(DTR("GRIDSTORESRNO")), DTR("STOREITEMNAME"), Val(DTR("STOREQTY")), DTR("STOREUNIT"))
                    Next
                End If
                getsrno(GRIDCONSUME)


                TOTAL()
                GRIDGDN.FirstDisplayedScrollingRowIndex = GRIDGDN.RowCount - 1
            Else
                EDIT = False
                CLEAR()
            End If
            chkchange.CheckState = CheckState.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If


            Dim alParaval As New ArrayList

            If txtgdnno.ReadOnly = False Then
                alParaval.Add(Val(txtgdnno.Text.Trim))
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(Format(Convert.ToDateTime(GDNDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBGODOWN.Text.Trim)
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(TXTCONSIGNEE.Text.Trim)
            alParaval.Add(CMBHASTE.Text.Trim)
            alParaval.Add(CMBAGENT.Text.Trim)

            alParaval.Add(txtpono.Text.Trim)
            alParaval.Add(podate.Value.Date)
            alParaval.Add(Val(TXTSONO.Text.Trim))
            alParaval.Add(Val(TXTSOGRIDSRNO.Text.Trim))
            alParaval.Add(SODATE.Value.Date)
            alParaval.Add(TXTMULTISONO.Text.Trim)

            alParaval.Add(CMBTRANS.Text.Trim)
            alParaval.Add(cmbcity.Text.Trim)
            alParaval.Add(TXTLRNO.Text.Trim)
            alParaval.Add(LRDATE.Value)
            alParaval.Add(CMBRISK.Text.Trim)
            alParaval.Add(TXTBALENOFROM.Text.Trim)
            alParaval.Add(TXTBALENOTO.Text.Trim)

            alParaval.Add(CMBDISPATCHTO.Text.Trim)
            alParaval.Add(txttransref.Text.Trim)
            alParaval.Add(TXTTRANSREMARKS.Text.Trim)

            alParaval.Add(Val(LBLTOTALBALES.Text))
            alParaval.Add(Val(LBLTOTALPCS.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(Val(LBLAMOUNT.Text))

            alParaval.Add(CHKHOLD.CheckState)
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)
            alParaval.Add(ClientName)


            Dim GRIDSRNO As String = ""
            Dim PIECETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim PRINTDESC As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim BALENO As String = ""

            Dim PCS As String = ""
            Dim UNIT As String = ""
            Dim CUT As String = ""
            Dim MTRS As String = ""
            Dim RATE As String = ""
            Dim PER As String = ""
            Dim AMOUNT As String = ""
            Dim BARCODE As String = ""

            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""
            Dim SALEDONE As String = ""

            Dim GRIDSONO As String = ""
            Dim GRIDSOSRNO As String = ""
            Dim GRIDPARTYPONO As String = ""
            Dim GRIDLOTNO As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDGDN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(row.Cells(GSRNO.Index).Value)
                        PIECETYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        If row.Cells(GPRINTDESC.Index).Value <> Nothing Then PRINTDESC = row.Cells(GPRINTDESC.Index).Value.ToString Else PRINTDESC = ""
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(GSHADE.Index).Value.ToString
                        If ClientName = "RMANILAL" Or ClientName = "SUNCOTT" Then
                            BALENO = Val(txtgdnno.Text.Trim)
                        Else
                            If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = row.Cells(GBALENO.Index).Value.ToString Else BALENO = ""
                        End If
                        PCS = Val(row.Cells(Gpcs.Index).Value)
                        UNIT = row.Cells(GUNIT.Index).Value.ToString

                        If CHALLANCUTWHEN1 = True Then
                            If Val(row.Cells(GCUT.Index).Value) = 0 And Val(row.Cells(Gpcs.Index).Value) = 1 Then CUT = Format(Val(row.Cells(Gmtrs.Index).Value), "0.00") Else CUT = Val(row.Cells(GCUT.Index).Value)
                        ElseIf CHALLANCUT = True Then
                            CUT = Val(row.Cells(GCUT.Index).Value)
                        Else
                            If Val(row.Cells(GCUT.Index).Value) = 0 Then CUT = Format(Val(row.Cells(Gmtrs.Index).Value) / Val(row.Cells(Gpcs.Index).Value), "0.00") Else CUT = Val(row.Cells(GCUT.Index).Value)
                        End If

                        MTRS = Val(row.Cells(Gmtrs.Index).Value)
                        RATE = Val(row.Cells(GRATE.Index).Value)
                        PER = row.Cells(GPER.Index).Value.ToString
                        AMOUNT = Val(row.Cells(GAMOUNT.Index).Value)
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString
                        SALEDONE = row.Cells(GSALEDONE.Index).Value.ToString

                        GRIDSONO = Val(row.Cells(GSONO.Index).Value)
                        GRIDSOSRNO = Val(row.Cells(GSOSRNO.Index).Value)
                        GRIDPARTYPONO = row.Cells(GPARTYPONO.Index).Value.ToString
                        GRIDLOTNO = row.Cells(GGRIDLOTNO.Index).Value.ToString

                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
                        PIECETYPE = PIECETYPE & "|" & row.Cells(GPIECETYPE.Index).Value.ToString
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        If row.Cells(GPRINTDESC.Index).Value <> Nothing Then PRINTDESC = PRINTDESC & "|" & row.Cells(GPRINTDESC.Index).Value.ToString Else PRINTDESC = PRINTDESC & "|" & ""
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GSHADE.Index).Value.ToString
                        If ClientName = "RMANILAL" Or ClientName = "SUNCOTT" Then
                            BALENO = BALENO & "|" & Val(txtgdnno.Text.Trim)
                        Else
                            If row.Cells(GBALENO.Index).Value <> Nothing Then BALENO = BALENO & "|" & row.Cells(GBALENO.Index).Value.ToString Else BALENO = BALENO & "|" & ""
                        End If
                        PCS = PCS & "|" & Val(row.Cells(Gpcs.Index).Value)
                        UNIT = UNIT & "|" & row.Cells(GUNIT.Index).Value.ToString

                        If CHALLANCUTWHEN1 = True Then
                            If Val(row.Cells(GCUT.Index).Value) = 0 And Val(row.Cells(Gpcs.Index).Value) = 1 Then CUT = CUT & "|" & Format(Val(row.Cells(Gmtrs.Index).Value), "0.00") Else CUT = CUT & "|" & Val(row.Cells(GCUT.Index).Value)
                        ElseIf CHALLANCUT = True Then
                            CUT = CUT & "|" & Val(row.Cells(GCUT.Index).Value)
                        Else
                            If Val(row.Cells(GCUT.Index).Value) = 0 Then CUT = CUT & "|" & Format(Val(row.Cells(Gmtrs.Index).Value) / Val(row.Cells(Gpcs.Index).Value), "0.00") Else CUT = CUT & "|" & Val(row.Cells(GCUT.Index).Value)
                        End If

                        MTRS = MTRS & "|" & Val(row.Cells(Gmtrs.Index).Value)
                        RATE = RATE & "|" & Val(row.Cells(GRATE.Index).Value)
                        PER = PER & "|" & row.Cells(GPER.Index).Value.ToString
                        AMOUNT = AMOUNT & "|" & Val(row.Cells(GAMOUNT.Index).Value)
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString
                        SALEDONE = SALEDONE & "|" & row.Cells(GSALEDONE.Index).Value.ToString

                        GRIDSONO = GRIDSONO & "|" & Val(row.Cells(GSONO.Index).Value)
                        GRIDSOSRNO = GRIDSOSRNO & "|" & Val(row.Cells(GSOSRNO.Index).Value)
                        GRIDPARTYPONO = GRIDPARTYPONO & "|" & row.Cells(GPARTYPONO.Index).Value.ToString
                        GRIDLOTNO = GRIDLOTNO & "|" & row.Cells(GGRIDLOTNO.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(PIECETYPE)
            alParaval.Add(ITEMNAME)
            alParaval.Add(QUALITY)
            alParaval.Add(PRINTDESC)
            alParaval.Add(DESIGN)
            alParaval.Add(COLOR)
            alParaval.Add(BALENO)
            alParaval.Add(PCS)
            alParaval.Add(UNIT)
            alParaval.Add(CUT)
            alParaval.Add(MTRS)
            alParaval.Add(RATE)
            alParaval.Add(PER)
            alParaval.Add(AMOUNT)
            alParaval.Add(BARCODE)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)
            alParaval.Add(SALEDONE)
            alParaval.Add(GRIDSONO)
            alParaval.Add(GRIDSOSRNO)
            alParaval.Add(GRIDPARTYPONO)
            alParaval.Add(GRIDLOTNO)



            Dim ORDERGRIDSRNO As String = ""
            Dim ORDERITEMNAME As String = ""
            Dim ORDERDESIGN As String = ""
            Dim ORDERCOLOR As String = ""
            Dim ORDERPCS As String = ""
            Dim ORDERMTRS As String = ""
            Dim ORDERFROMNO As String = ""
            Dim ORDERFROMSRNO As String = ""
            Dim ORDERFROMTYPE As String = ""
            Dim ORDERGDNPCS As String = ""
            Dim ORDERGDNMTRS As String = ""
            Dim ORDERRATE As String = ""
            Dim ORDERPARTYPONO As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDORDER.Rows
                If row.Cells(0).Value <> Nothing AndAlso (Val(row.Cells(OGDNQTY.Index).Value) > 0 Or Val(row.Cells(OGDNMTRS.Index).Value) > 0) Then

                    If ORDERGRIDSRNO = "" Then
                        ORDERGRIDSRNO = Val(row.Cells(OSRNO.Index).Value)
                        ORDERITEMNAME = row.Cells(OITEMNAME.Index).Value.ToString
                        ORDERDESIGN = row.Cells(ODESIGN.Index).Value.ToString
                        ORDERCOLOR = row.Cells(OCOLOR.Index).Value.ToString
                        ORDERPCS = Val(row.Cells(OPCS.Index).Value)
                        ORDERMTRS = Val(row.Cells(OMTRS.Index).Value)
                        ORDERFROMNO = Val(row.Cells(OFROMNO.Index).Value)
                        ORDERFROMSRNO = Val(row.Cells(OFROMSRNO.Index).Value)
                        ORDERFROMTYPE = row.Cells(OFROMTYPE.Index).Value.ToString
                        ORDERGDNPCS = Val(row.Cells(OGDNQTY.Index).Value)
                        ORDERGDNMTRS = Val(row.Cells(OGDNMTRS.Index).Value)
                        ORDERRATE = Val(row.Cells(ORATE.Index).Value)
                        ORDERPARTYPONO = row.Cells(OPARTYPONO.Index).Value
                    Else
                        ORDERGRIDSRNO = ORDERGRIDSRNO & "|" & Val(row.Cells(OSRNO.Index).Value)
                        ORDERITEMNAME = ORDERITEMNAME & "|" & row.Cells(OITEMNAME.Index).Value.ToString
                        ORDERDESIGN = ORDERDESIGN & "|" & row.Cells(ODESIGN.Index).Value.ToString
                        ORDERCOLOR = ORDERCOLOR & "|" & row.Cells(OCOLOR.Index).Value.ToString
                        ORDERPCS = ORDERPCS & "|" & Val(row.Cells(OPCS.Index).Value)
                        ORDERMTRS = ORDERMTRS & "|" & Val(row.Cells(OMTRS.Index).Value)
                        ORDERFROMNO = ORDERFROMNO & "|" & Val(row.Cells(OFROMNO.Index).Value)
                        ORDERFROMSRNO = ORDERFROMSRNO & "|" & Val(row.Cells(OFROMSRNO.Index).Value)
                        ORDERFROMTYPE = ORDERFROMTYPE & "|" & row.Cells(OFROMTYPE.Index).Value.ToString
                        ORDERGDNPCS = ORDERGDNPCS & "|" & Val(row.Cells(OGDNQTY.Index).Value)
                        ORDERGDNMTRS = ORDERGDNMTRS & "|" & Val(row.Cells(OGDNMTRS.Index).Value)
                        ORDERRATE = ORDERRATE & "|" & Val(row.Cells(ORATE.Index).Value)
                        ORDERPARTYPONO = ORDERPARTYPONO & "|" & row.Cells(OPARTYPONO.Index).Value
                    End If
                End If
            Next

            alParaval.Add(ORDERGRIDSRNO)
            alParaval.Add(ORDERITEMNAME)
            alParaval.Add(ORDERDESIGN)
            alParaval.Add(ORDERCOLOR)
            alParaval.Add(ORDERPCS)
            alParaval.Add(ORDERMTRS)
            alParaval.Add(ORDERFROMNO)
            alParaval.Add(ORDERFROMSRNO)
            alParaval.Add(ORDERFROMTYPE)
            alParaval.Add(ORDERGDNPCS)
            alParaval.Add(ORDERGDNMTRS)
            alParaval.Add(ORDERRATE)
            alParaval.Add(ORDERPARTYPONO)


            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(Val(TXTTYPECHALLANNO.Text.Trim))



            Dim GRIDSTORESRNO As String = ""
            Dim STOREITEMNAME As String = ""
            Dim STOREQTY As String = ""
            Dim STOREUNIT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCONSUME.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSTORESRNO = "" Then
                        GRIDSTORESRNO = Val(row.Cells(SSTORESRNO.Index).Value)
                        STOREITEMNAME = row.Cells(SSTOREITEMNAME.Index).Value.ToString
                        STOREQTY = Val(row.Cells(SSTOREQTY.Index).Value)
                        STOREUNIT = row.Cells(SSTOREUNIT.Index).Value.ToString
                    Else
                        GRIDSTORESRNO = GRIDSTORESRNO & "|" & Val(row.Cells(SSTORESRNO.Index).Value)
                        STOREITEMNAME = STOREITEMNAME & "|" & row.Cells(SSTOREITEMNAME.Index).Value
                        STOREQTY = STOREQTY & "|" & Val(row.Cells(SSTOREQTY.Index).Value)
                        STOREUNIT = STOREUNIT & "|" & row.Cells(SSTOREUNIT.Index).Value
                    End If
                End If
            Next

            alParaval.Add(GRIDSTORESRNO)
            alParaval.Add(STOREITEMNAME)
            alParaval.Add(STOREQTY)
            alParaval.Add(STOREUNIT)

            alParaval.Add(CMBCONTRACTOR.Text.Trim)
            If CHKCHANGEADD.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            alParaval.Add(TXTDELIVERYADDRESS.Text.Trim)

            Dim objclsgdn As New ClsGDN()
            objclsgdn.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTT As DataTable = objclsgdn.SAVE()
                txtgdnno.Text = DTT.Rows(0).Item(0)
                MsgBox("Details Added")

                TEMPGDNNO = txtgdnno.Text.Trim
                If ClientName = "RMANILAL" Then SENDDIRECTMAIL()

                If ClientName = "SVS" Then
                    If MsgBox("Wish To Stock Reco Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        RECOSAVE()
                    End If
                End If


            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                alParaval.Add(TEMPGDNNO)
                IntResult = objclsgdn.UPDATE()
                MsgBox("Details Updated")

            End If

            EDIT = False

            'THEY DONT NEED PRINT
            If ClientName <> "SNCM" And ClientName <> "AFW" Then
                If ClientName = "SUPRIYA" Then SERVERPROP() Else PRINTREPORT(txtgdnno.Text.Trim)
            End If

            If ClientName = "AFW" And EDIT = False Then
                Dim OBJSALE As New InvoiceMaster
                OBJSALE.DIRECTINVOICE = True
                OBJSALE.TEMPPARTYNAME = cmbname.Text.Trim
                OBJSALE.TEMPPURNO = Val(TEMPGDNNO)
                OBJSALE.MdiParent = MDIMain
                OBJSALE.Show()
            End If

            TEMPPROFORMANO = 0
            CLEAR()
            If ClientName = "LEEFABRICO" Then GDNDATE.Focus() Else cmbname.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub SERVERPROP()
        Dim OBJ As New Object

        If MsgBox("Wish to Print Challan?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

        If ClientName = "SUPRIYA" Then
            OBJ = New GDNReport_SUPRIYA
        ElseIf ClientName = "SUPEEMA" Then
            OBJ = New GDNReport_SUPEEMA
        End If

        '**************** SET SERVER ************************
        Dim crtableLogonInfo As New TableLogOnInfo
        Dim crConnecttionInfo As New ConnectionInfo
        Dim crTables As Tables
        Dim crTable As Table

        With crConnecttionInfo
            .ServerName = SERVERNAME
            .DatabaseName = DatabaseName
            .UserID = DBUSERNAME
            .Password = Dbpassword
            .IntegratedSecurity = Dbsecurity
        End With

        crTables = OBJ.Database.Tables
        For Each crTable In crTables
            crtableLogonInfo = crTable.LogOnInfo
            crtableLogonInfo.ConnectionInfo = crConnecttionInfo
            crTable.ApplyLogOnInfo(crtableLogonInfo)
        Next


        Dim OBJCMN As New ClsCommon
        Dim DT As New DataTable
        'OBJ.PrintOptions.PaperSize = PaperSize.

        'Dim c As Integer
        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        'doctoprint.PrinterSettings.PrinterName = "EPSON fx-2175"
        'Dim rawKind As Integer
        'For c = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
        '    If doctoprint.PrinterSettings.PaperSizes(c).PaperName = "10x6" Then
        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(c).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(c)))
        '        Exit For
        '    End If
        'Next

        'OBJ.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
        If ClientName = "SUPRIYA" Then OBJ.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
        OBJ.RecordSelectionFormula = "{GDN.GDN_no}=" & Val(txtgdnno.Text.Trim) & " and {GDN.GDN_yearid}=" & YearId
        OBJ.PrintToPrinter(1, True, 0, 0)
    End Sub

    Private Sub toolPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDGDN.RowCount = 0
LINE1:
            TEMPGDNNO = Val(txtgdnno.Text) - 1

            'IF WE DONT HAVE ANY PREVIOUS RECORDS THEN EXIT
            If ClientName = "MIDAS" Then
                Dim OBJCMN As New ClsCommon
                Dim DTTABLE As DataTable = OBJCMN.SEARCH("GDN_NO AS GDNNO", "", "GDN", " AND GDN_NO <" & Val(txtgdnno.Text.Trim) & " AND GDN_YEARID =" & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    CLEAR()
                    EDIT = False
                    Exit Sub
                End If
            End If


            If TEMPGDNNO > 0 Then
                EDIT = True
                'GDN_Load(sender, e)
                SHOWDATA()
            Else
                CLEAR()
                EDIT = False
                TEMPPROFORMANO = 0
            End If
            If GRIDGDN.RowCount = 0 And TEMPGDNNO > 1 Then
                txtgdnno.Text = TEMPGDNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_CLICK(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDGDN.RowCount = 0
LINE1:
            TEMPGDNNO = Val(txtgdnno.Text) + 1
            GETMAXNO()
            Dim MAXNO As Integer = txtgdnno.Text.Trim
            CLEAR()



            If Val(txtgdnno.Text) - 1 >= TEMPGDNNO Then
                EDIT = True
                'GDN_Load(sender, e)
                SHOWDATA()
            Else
                CLEAR()
                EDIT = False
                TEMPPROFORMANO = 0
            End If
            If GRIDGDN.RowCount = 0 And TEMPGDNNO < MAXNO Then
                txtgdnno.Text = TEMPGDNNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDGDN.RowCount = 0
                TEMPGDNNO = Val(tstxtbillno.Text)
                If TEMPGDNNO > 0 Then
                    EDIT = True
                    'GDN_Load(sender, e)
                    SHOWDATA()
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal GDNNO As Integer)
        Try
            If ClientName = "SUPRIYA" Then
                SERVERPROP()
                Exit Sub
            End If

            If ClientName = "MOMAI" Then
                If MsgBox("Wish to Print Label?", MsgBoxStyle.YesNo) = vbYes Then PRINTBARCODE()
            End If


            If MsgBox("Wish to Print Challan?", MsgBoxStyle.YesNo) = vbYes Then

                Dim OBJGDN As New GDNDESIGN
                OBJGDN.MdiParent = MDIMain
                OBJGDN.FRMSTRING = "GDN"
                OBJGDN.FORMULA = "{GDN.GDN_no}=" & Val(GDNNO) & " and {GDN.GDN_yearid}=" & YearId
                OBJGDN.vendorname = cmbname.Text.Trim
                OBJGDN.agentname = CMBAGENT.Text.Trim
                If (ClientName = "RAJKRIPA" Or ClientName = "MANISH" Or ClientName = "SSC" Or ClientName = "SNCM") AndAlso MsgBox("Wish to Print Rate?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJGDN.PRINTRATE = True
                If ClientName = "MYCOT" Or ClientName = "SHUBHI" Then OBJGDN.PRINTRATE = True
                If (ClientName = "MANSI" Or ClientName = "CHINTAN") AndAlso MsgBox("Print Challan for Garments?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJGDN.FRMSTRING = "GDNGARMENT"
                OBJGDN.WHITELABEL = CHKWHITELABEL.Checked
                OBJGDN.HIDEPCSDETAILS = CHKHIDEPCS.Checked
                OBJGDN.PRINTINYARDS = CHKYARD.Checked
                If CHKCHANGEADD.CheckState = CheckState.Checked Then OBJGDN.PARTYCHANGEADD = TXTDELIVERYADDRESS.Text.Trim
                OBJGDN.Show()
            End If

            If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "INDRAPUJAFABRICS" Or ClientName = "MSANCHITKUMAR" Then
                Dim TEMPMSG2 As Integer = MsgBox("Wish to Print Challan Banner?", MsgBoxStyle.YesNo)
                If TEMPMSG2 = vbYes Then
                    Dim OBJGDN As New GDNDESIGN
                    OBJGDN.FORMULA = "{GDN.GDN_no}=" & Val(GDNNO) & " and {GDN.GDN_yearid}=" & YearId
                    OBJGDN.FRMSTRING = "GDNBANNER"
                    OBJGDN.MdiParent = MDIMain
                    OBJGDN.Show()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTBARCODE()
        Try

            'PRINT BARCODE

            'GET FRESH DATA FROM DATABASE (ONLY GRID)
            'THIS IS DONE COZ FOR MULTIUSER THE NOS WILL BE SAME
            'SO WE WILL ADD BARCODE IN SP AND THEN FETCH THAT DATA HERE AFTER THAT WE WILL PRINT BARCODES
            'GRIDGDN.RowCount = 0
            'Dim OBJGDN As New ClsGDN
            'Dim dttable As DataTable = OBJGDN.SELECTGDN(Val(txtgdnno.Text.Trim), CmpId, Locationid, YearId)
            'For Each dr As DataRow In dttable.Rows
            '    GRIDGDN.Rows.Add(dr("GRIDSRNO").ToString, dr("PIECETYPE").ToString, dr("ITEMNAME").ToString, dr("QUALITY").ToString, dr("BALENO").ToString, dr("DESIGNNO").ToString, dr("DESC").ToString, dr("COLOR"), Format(dr("qty"), "0.00"), dr("QTYUNIT").ToString, Format(dr("CUT"), "0.00"), Format(dr("MTRS"), "0.00"), dr("RACK"), dr("SHELF"), Format(dr("WT"), "0.00"), Format(dr("PURRATE"), "0.00"), Format(dr("SALERATE"), "0.00"), Format(dr("WHOLESALERATE"), "0.00"), dr("BARCODE").ToString, dr("DONE").ToString, Val(dr("OUTPCS")), Val(dr("OUTMTRS")), dr("GRIDPONO").ToString, dr("POGRIDSRNO").ToString)
            'Next

            Dim PRINTMRP As Integer
            If ClientName = "MOMAI" Then
                PRINTMRP = MsgBox("Wish to Print MRP?", MsgBoxStyle.YesNo)
            End If

            For Each ROW As DataGridViewRow In GRIDGDN.Rows

                For I As Integer = 0 To Val(ROW.Cells(Gpcs.Index).Value) - 1

                    Dim dirresults As String = ""
                    Dim oWrite As System.IO.StreamWriter
                    oWrite = File.CreateText(Application.StartupPath & "\Barcode.txt")

                    If ClientName = "MOMAI" Then

                        oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>SIZE 47.5 mm, 25 mm")
                        oWrite.WriteLine("GAP 3 mm, 0 mm")
                        oWrite.WriteLine("DIRECTION 0,0")
                        oWrite.WriteLine("REFERENCE 0,0")
                        oWrite.WriteLine("OFFSET 0 mm")
                        oWrite.WriteLine("SET PEEL OFF")
                        oWrite.WriteLine("SET CUTTER OFF")
                        oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                        oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>SET TEAR ON")
                        oWrite.WriteLine("CLS")
                        oWrite.WriteLine("CODEPAGE 1252")
                        oWrite.WriteLine("TEXT 365,188,""0"",180,14,14,""" & ROW.Cells(GITEMNAME.Index).Value & """")
                        oWrite.WriteLine("TEXT 365,146,""0"",180,14,14,""" & ROW.Cells(GDESIGN.Index).Value & """")
                        oWrite.WriteLine("TEXT 365,102,""0"",180,9,9,""" & ROW.Cells(GSHADE.Index).Value & """")
                        oWrite.WriteLine("TEXT 172,101,""0"",180,8,8,""MRP""")


                        Dim TEMPMRP As String = ""
                        Dim OBJCMN As New ClsCommon
                        'FIRST CHECK WITH LEDGER NAME IN WHERECLAUSE IF NO RECORD IS FOUND THEN GET DATA WITHOUT PARTY NAME
                        Dim WHERECLAUSE As String = " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & cmbname.Text.Trim & "'"
                        Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(PL_RATE,0) AS RATE", "", "PRICELISTMASTER LEFT OUTER JOIN ITEMMASTER ON PL_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN DESIGNMASTER ON PL_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON PL_COLORID = COLORMASTER.COLOR_ID LEFT OUTER JOIN LEDGERS ON PL_LEDGERID = LEDGERS.ACC_ID ", " AND ITEMMASTER.ITEM_NAME = '" & ROW.Cells(GITEMNAME.Index).Value & "' AND DESIGNMASTER.DESIGN_NO = '" & ROW.Cells(GDESIGN.Index).Value & "' AND PL_YEARID = " & YearId & WHERECLAUSE)
                        If DT.Rows.Count > 0 Then
                            TEMPMRP = Val(DT.Rows(0).Item("RATE"))
                        Else
                            DT = OBJCMN.SEARCH("ISNULL(PL_RATE,0) AS RATE", "", "PRICELISTMASTER LEFT OUTER JOIN ITEMMASTER ON PL_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN DESIGNMASTER ON PL_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON PL_COLORID = COLORMASTER.COLOR_ID  LEFT OUTER JOIN LEDGERS ON PL_LEDGERID = LEDGERS.ACC_ID ", " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '' AND ITEMMASTER.ITEM_NAME = '" & ROW.Cells(GITEMNAME.Index).Value & "' AND DESIGNMASTER.DESIGN_NO = '" & ROW.Cells(GDESIGN.Index).Value & "' AND PL_YEARID = " & YearId)
                            If DT.Rows.Count > 0 Then TEMPMRP = Val(DT.Rows(0).Item("RATE"))
                        End If




                        ''AS PER NEW MODIFICATIOSN WE HAVE TO PRINT MRP STATWISE
                        ''GET STATENAME FROM LEDGERS
                        'Dim DTSTATE As DataTable = OBJCMN.search("ISNULL(STATE_NAME,'') AS STATENAME", "", " LEDGERS INNER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATEMASTER.STATE_ID ", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                        'Dim DT As DataTable = OBJCMN.search("ISNULL(MRP_MRP,0) AS RATE", "", "STATEWISEMRP INNER JOIN STATEMASTER ON MRP_STATEID = STATEMASTER.STATE_ID INNER JOIN ITEMMASTER ON MRP_ITEMID = ITEMMASTER.ITEM_ID", " AND ITEMMASTER.ITEM_NAME = '" & ROW.Cells(GITEMNAME.Index).Value & "' AND STATEMASTER.STATE_NAME = '" & DTSTATE.Rows(0).Item("STATENAME") & "' AND MRP_YEARID = " & YearId)
                        'If DT.Rows.Count > 0 Then
                        '    TEMPMRP = Val(DT.Rows(0).Item("RATE"))
                        'End If



                        If PRINTMRP = vbNo Then TEMPMRP = ""

                        oWrite.WriteLine("TEXT 119,107,""0"",180,13,13, """ & TEMPMRP & """")
                        oWrite.WriteLine("TEXT 98,71,""0"",180,4,4,""(Inc. of all Taxes)""")
                        oWrite.WriteLine("TEXT 68,138,""0"",180,7,7,""1PCS""")
                        oWrite.WriteLine("BARCODE 365,72,""128M"",52,0,180,1,2, """ & ROW.Cells(GBARCODE.Index).Value & """") 'BARCODE
                        oWrite.WriteLine("TEXT 325,17,""0"",180,6,6, """ & ROW.Cells(GBARCODE.Index).Value & """")

                        oWrite.WriteLine("PRINT 1,1")
                        oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                        oWrite.Dispose()


                    End If

                    'Printing Barcode
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = "cmd.exe"
                    psi.RedirectStandardInput = False
                    psi.RedirectStandardOutput = True
                    psi.Arguments = "/c print " & Application.StartupPath & "\Barcode.txt"    ' specify your command
                    psi.UseShellExecute = False

                    Dim proc As Process
                    proc = Process.Start(psi)
                    dirresults = proc.StandardOutput.ReadToEnd() ' // read from stdout
                    proc.WaitForExit()
                    proc.Dispose()
NEXTLINE:
                    'THIS LINE IS WRITTEN TO DISPOSE THE BARCODE NOTEPAD OBJECT, WHEN CURSOR COMES DIRECTLY ON NEXTLINE CODE
                    oWrite.Dispose()
                Next
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPGDNNO)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objpodtls As New GDNDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
            objpodtls.BringToFront()
            'Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If ClientName = "AVIS" Then
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND GROUP_NAME <> 'HASTE DEBTORS'")
            Else
                If cmbname.Text.Trim = "" Then FILLNAME(cmbname, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then
                NAMEVALIDATE(cmbname, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS", CMBTRANS.Text, CMBAGENT.Text)
                'Dim OBJCMN As New ClsCommon
                'Dim DT As DataTable = OBJCMN.search(" ISNULL(ACC_TINNO,'') AS TINNO", "", " LEDGERS", " AND ACC_CMPNAME = '" & cmbname.Text.Trim & "'  AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId)
                'If DT.Rows.Count > 0 Then LBLTINNO.Text = DT.Rows(0).Item("TINNO")
                'If ClientName <> "KOTHARI" Then TXTMOBILENO.Text = DT.Rows(0).Item("MOBILENO")

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then FILLNAME(CMBTRANS, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANS.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'"
                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBTRANS.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then NAMEVALIDATE(CMBTRANS, CMBCODE, e, Me, TXTTRANSADD, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcity_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcity.Enter
        Try
            If cmbcity.Text.Trim = "" Then fillCITY(cmbcity, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcity_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcity.Validating
        Try
            If cmbcity.Text.Trim <> "" Then CITYVALIDATE(cmbcity, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGDN_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDGDN.CellValidating
        Try
            Dim colNum As Integer = GRIDGDN.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum
                Case Gpcs.Index, Gmtrs.Index, GRATE.Index, GCUT.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)
                    If bValid Then
                        If GRIDGDN.CurrentCell.Value = Nothing Then GRIDGDN.CurrentCell.Value = "0.00"
                        GRIDGDN.CurrentCell.Value = Convert.ToDecimal(GRIDGDN.Item(colNum, e.RowIndex).Value)
                        '' everything is good
                        TOTAL()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If
                Case GPER.Index
                    TOTAL()
                Case GBALENO.Index
                    BALECOUNT()
            End Select


            'FOR RATE AND DESCRIPTION WITH RESPECT TO ITEMNAME
            If ClientName = "SBA" Or ClientName = "SONU" Or ClientName = "GELATO" Then
                Dim TEMPITEM As String = ""
                Dim TEMPDESIGNNO As String = ""
                For I As Integer = GRIDGDN.CurrentRow.Index + 1 To GRIDGDN.RowCount - 1
                    If I = GRIDGDN.CurrentRow.Index + 1 Then TEMPITEM = GRIDGDN.Item(GITEMNAME.Index, I - 1).Value
                    If I = GRIDGDN.CurrentRow.Index + 1 Then TEMPDESIGNNO = GRIDGDN.Item(GDESIGN.Index, I - 1).Value
                    If ClientName = "SBA" And GRIDGDN.Item(GITEMNAME.Index, I).Value = TEMPITEM Then GRIDGDN.Item(GRATE.Index, I).Value = GRIDGDN.Item(GRATE.Index, I - 1).EditedFormattedValue
                    If ClientName = "SONU" And GRIDGDN.Item(GITEMNAME.Index, I).Value = TEMPITEM Then GRIDGDN.Item(GPRINTDESC.Index, I).Value = GRIDGDN.Item(GPRINTDESC.Index, I - 1).EditedFormattedValue
                    If ClientName = "GELATO" And GRIDGDN.Item(GITEMNAME.Index, I).Value = TEMPITEM And GRIDGDN.Item(GDESIGN.Index, I).Value = TEMPDESIGNNO Then GRIDGDN.Item(GRATE.Index, I).Value = GRIDGDN.Item(GRATE.Index, I - 1).EditedFormattedValue
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGDN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDGDN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDGDN.RowCount > 0 Then GRIDGDN.Rows.RemoveAt(GRIDGDN.CurrentRow.Index) ''Removing Row From Grid
            'If GRIDGDN.CurrentRow.Index > 0 Then If e.KeyCode = Keys.F12 Then If GRIDGDN.Rows(GRIDGDN.CurrentRow.Index - 1).Cells(GLRNO.Index).Value <> "" Then GRIDGDN.Rows(GRIDGDN.CurrentRow.Index).Cells(GLRNO.Index).Value = GRIDGDN.Rows(GRIDGDN.CurrentRow.Index - 1).Cells(GLRNO.Index).Value
            If e.KeyCode = Keys.F12 And GRIDGDN.RowCount > 0 Then
                'THIS IS DONE FOR DAKSH, COZ WHEN WE FETCH DATA FROM PACKING WE DONT HAVE BARCODE AND WE NEED TO RUN THIS CODE
                'If GRIDGDN.CurrentRow.Cells(GBARCODE.Index).Value <> "" Then GRIDGDN.Rows.Insert(GRIDGDN.CurrentRow.Index, CloneWithValues(GRIDGDN.CurrentRow))
                GRIDGDN.Rows.Insert(GRIDGDN.CurrentRow.Index, CloneWithValues(GRIDGDN.CurrentRow))
                GRIDGDN.Rows(GRIDGDN.RowCount - 1).Selected = True
            End If

            getsrno(GRIDGDN)
            TOTAL()
            If e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Keys.F8 Then
                If GRIDGDN.CurrentRow.DefaultCellStyle.BackColor = Color.Linen Then GRIDGDN.CurrentRow.DefaultCellStyle.BackColor = Color.Empty Else GRIDGDN.CurrentRow.DefaultCellStyle.BackColor = Color.Linen
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Public Function CloneWithValues(ByVal row As DataGridViewRow) As DataGridViewRow
        CloneWithValues = CType(row.Clone(), DataGridViewRow)
        For index As Int32 = 0 To row.Cells.Count - 1
            CloneWithValues.Cells(index).Value = row.Cells(index).Value
        Next
    End Function

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Sale Invoice / Sale Return Made, Delete it First", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                TEMPMSG = MsgBox("Wish to Delete Challan?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub


                'FIRST MAIL THE CHALLAN 
                If ClientName = "AVIS" And CmpName <> "AVIS IMPEX" Then SENDDIRECTMAIL()



                'DONE BY GULKIT
                'BEFORE UPDATING REVERSE THE ENTRY IN SCHEDULEMASTER_DESC
                'If ClientName = "SAFFRON" Then
                '    Dim OBJCMN As New ClsCommon
                '    Dim LOOPTABLE As DataTable = OBJCMN.search("  GDN.GDN_ledgerid AS LEDGERID, GDN_DESC.GDN_ITEMID AS ITEMID, GDN_DESC.GDN_COLORID AS COLORID, GDN_DESC.GDN_PCS AS PCS ", "", " GDN_DESC INNER JOIN GDN ON GDN_DESC.GDN_NO = GDN.GDN_NO AND GDN_DESC.GDN_YEARID = GDN.GDN_YEARID ", " AND GDN.GDN_NO = " & TEMPGDNNO & " AND GDN.GDN_YEARID = " & YearId)
                '    For Each LOOPROW As DataRow In LOOPTABLE.Rows
                '        Dim DTTEMP As DataTable = OBJCMN.search(" TOP 1 SCH_NO , SCH_GRIDSCHNO ", "", " SCHEDULEMASTER_DESC INNER JOIN LEDGERS ON SCHEDULEMASTER_DESC.SCH_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON SCHEDULEMASTER_DESC.SCH_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON SCHEDULEMASTER_DESC.SCH_COLORID = COLORMASTER.COLOR_id  ", " AND LEDGERS.ACC_ID = " & Val(LOOPROW("LEDGERID")) & " AND ISNULL(ITEM_ID, 0) = " & Val(LOOPROW("ITEMID")) & " AND ISNULL(COLORMASTER.COLOR_ID,0) = " & Val(LOOPROW("COLORID")) & " AND SCH_YEARID = " & YearId & " AND  SCHEDULEMASTER_DESC.SCH_RECDQTY > 0 ORDER BY SCH_NO , SCH_GRIDSCHNO DESC")
                '        If DTTEMP.Rows.Count > 0 Then
                '            Dim NEWTEMP As DataTable = OBJCMN.Execute_Any_String(" update SCHEDULEMASTER_DESC set  SCH_RECDQTY = SCH_RECDQTY - " & Val(LOOPROW("PCS")) & " WHERE SCH_NO = " & Val(DTTEMP.Rows(0).Item(0)) & " AND SCH_GRIDSCHNO = " & Val(DTTEMP.Rows(0).Item(1)) & " AND SCH_yearid= " & YearId, "", "")
                '        End If
                '    Next
                'End If

                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPGDNNO)
                ALPARAVAL.Add(TXTSONO.Text.Trim)

                ALPARAVAL.Add(TXTSOGRIDSRNO.Text.Trim)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(ClientName)

                Dim OBJGDN As New ClsGDN
                OBJGDN.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJGDN.Delete
                MsgBox("GDN Deleted")

                CLEAR()
                EDIT = False
                cmbname.Focus()
                TEMPPROFORMANO = 0

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SENDDIRECTMAIL()
        Try

            Dim ALATTACHMENT As New ArrayList
            '**************** SET SERVER ************************
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table

            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With

            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions

            Dim TOMAIL As String = ""
            Dim SUBJECT As String = ""
            Dim OBJ As New Object

            If ClientName = "AVIS" Then
                OBJ = New GDNReport_AVIS
                TOMAIL = "infoavisindustries@gmail.com"
                SUBJECT = "Deleted Challan"


            ElseIf ClientName = "RMANILAL" Then
                OBJ = New GDNReport_COMMON
                'CHECK WHETHER EMAILID IS PRESENT IN LEDGER OR NOT, IF NOT THEN EXIT SUB WITH MESSAGE
                Dim OBJCMN As New ClsCommon
                Dim DTEMAIL As DataTable = OBJCMN.SEARCH("ACC_EMAIL AS EMAILID", "", "LEDGERS", "AND ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DTEMAIL.Rows.Count > 0 AndAlso DTEMAIL.Rows(0).Item("EMAILID") = "" Then
                    MsgBox("Add E-Mail id in Ledger", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    TOMAIL = DTEMAIL.Rows(0).Item("EMAILID")
                    SUBJECT = "Challan No. " & Val(TEMPGDNNO)
                End If

            End If

            crTables = OBJ.Database.Tables
            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = "{GDN.GDN_no}=" & Val(TEMPGDNNO) & " and {GDN.GDN_yearid}=" & YearId

            oDfDopt.DiskFileName = Application.StartupPath & "\GDN_" & TEMPGDNNO & ".pdf"
            expo = OBJ.ExportOptions
            expo.ExportDestinationType = ExportDestinationType.DiskFile
            expo.ExportFormatType = ExportFormatType.PortableDocFormat
            expo.DestinationOptions = oDfDopt
            OBJ.Export()
            ALATTACHMENT.Add(oDfDopt.DiskFileName)

            sendemail(TOMAIL, ALATTACHMENT(0), "", SUBJECT, ALATTACHMENT, ALATTACHMENT.Count, "", "", "", "", "")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub GDN_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "AXIS" Then GDESIGN.HeaderText = "Design/Size"
            If ClientName = "AKASHDEEP" Then TXTSTOCK.Visible = True
            If ClientName = "KARAN" Then
                LBLCHALLAN.Text = "Challan No"
                txttransref.BackColor = Color.LemonChiffon
            End If

            If ClientName = "SOFTAS" And UserName <> "Admin" Then
                ORATE.Visible = False
                GRATE.Visible = False
                GAMOUNT.Visible = False
            End If

            If ClientName = "MAFATLAL" Then CHKHIDEPCS.Text = "Hide Mtrs"


            If ClientName = "SONU" Then
                LBLKPL.Visible = True
                CMBKPL.Visible = True
            End If

            If ClientName <> "SVS" Then
                GQUALITY.Visible = False
                GITEMNAME.Visible = True
                GCUT.Visible = True
            End If

            If ClientName = "AVIS" Then
                LBLCITY.Text = "Delivery To"
                LBLCONSIGNEE.Visible = False
                TXTCONSIGNEE.Visible = False
                LBLPACKING.Visible = False
                CMBHASTE.Visible = False
                LBLCHALLAN.Visible = False
                txttransref.Visible = False
                LBLDYEINGRECNO.Visible = True
                TXTDYEINGRECNO.Visible = True
            End If

            If ClientName = "LEEFABRICO" Or ClientName = "BALAJI" Or ClientName = "MANISH" Then
                TXTBARCODE.TabStop = False
                TXTCONSIGNEE.TabStop = False
                CMBHASTE.TabStop = False
                CMBQUALITY.TabStop = False
                TXTDESCRIPTION.TabStop = False
                TXTGRIDLOTNO.TabStop = False
                TXTCUT.TabStop = False
            End If

            If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                CHKRETAIL.Visible = True
            End If

            If ClientName = "SUPEEMA" Then
                LBLCONTRACTOR.Visible = True
                CMBCONTRACTOR.Visible = True
            End If

            If ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then CHKHIDEPCS.CheckState = CheckState.Checked

            If SHOWGDNCOLUMNS = True Then
                'LBLSRNO.Text = "Bale No"
                txtsrno.Visible = True
                CMBPIECETYPE.Visible = True
                CMBITEMNAME.Visible = True
                CMBQUALITY.Visible = True
                TXTDESCRIPTION.Visible = True
                CMBDESIGN.Visible = True
                CMBCOLOR.Visible = True
                TXTBALENO.Visible = True
                TXTPCS.Visible = True
                TXTCUT.Visible = True
                TXTMTRS.Visible = True
                TXTRATE.Visible = True
                CMBPER.Visible = True
                TXTAMOUNT.Visible = True
                GQUALITY.Visible = True
                GPARTYPONO.ReadOnly = False
                TXTGRIDLOTNO.Visible = True
            End If

            If ClientName = "MOHAN" Then LBLCHALLAN.Text = "Indent No."
            If ClientName = "KENCOT" Then
                GITEMNAME.Width = 250
                LBLCHALLAN.Text = "Total Wt."
            End If

            If ClientName = "MSANCHITKUMAR" Then TXTBALENO.BackColor = Color.LemonChiffon



            LBLBALEFROM.Text = "Bls/Bundle"
            LBLBALETO.Visible = False
            TXTBALENOTO.Visible = False
            TXTMULTISONO.ReadOnly = False
            TXTMULTISONO.Visible = True


            If ClientName = "KCRAYON" Then
                LBLCHALLAN.Text = "Patta"
                LBLCONSIGNEE.Text = "Exp. To"
                TXTSONO.TabStop = True
                TXTMULTISONO.Visible = False
                SODATE.TabStop = True
            End If


            If ClientName = "INDRAPUJAFABRICS" Or ClientName = "INDRAPUJAIMPEX" Or ClientName = "SONU" Or ClientName = "LEEFABRICO" Or ClientName = "BALAJI" Then
                TXTMULTISONO.TabStop = True
                TXTMULTISONO.Visible = True
                TXTMULTISONO.TabIndex = TXTSONO.TabIndex
                TXTSONO.Visible = False
                SODATE.TabStop = True
            End If


            If ClientName = "SVS" Then
                GPRINTDESC.Visible = False
                GBALENO.Visible = False
                GBARCODE.Visible = False
            End If

            If ClientName = "SAFFRON" Or ClientName = "SAFFRONOFF" Then
                LBLTYPE.Visible = True
                CMBTYPE.Visible = True
                txtgdnno.Visible = False
                TXTTYPECHALLANNO.Visible = True
                LBLSRNO.Text = "Challan No"
                LBLCHALLAN.Text = "Ref No"
                toolprevious.Visible = False
                toolnext.Visible = False
                tstxtbillno.Visible = False
                'cmdselectOrder.Text = "View Schedules"
                TXTMULTISONO.Visible = True
            End If

            If ClientName = "SOFTAS" Or ClientName = "AMAN" Or ClientName = "AARYA" Or ClientName = "VINTAGEINDIA" Then
                CMBPIECETYPE.Text = "FRESH"
                CMBPIECETYPE.TabStop = False
                CMBQUALITY.TabStop = False

                If ClientName = "AMAN" Or ClientName = "AARYA" Or ClientName = "VINTAGEINDIA" Then
                    txtpono.TabStop = False
                    LBLCONSIGNEE.Text = "Vehicle No"
                    LBLCHALLAN.Text = "P Challan No"
                Else
                    txttransref.TabStop = False
                    TXTCONSIGNEE.TabStop = False
                End If

                CMBDESIGN.TabStop = False
                CMBCOLOR.TabStop = False
                TXTBALENO.TabStop = False
                TXTPCS.Text = 1

                CMBAGENT.TabStop = False
                CMBHASTE.TabStop = False
                TXTTRANSREMARKS.TabStop = False
                TXTBARCODE.TabStop = False
                TXTMOBILENO.TabStop = False
            End If

            If ClientName = "KDFAB" Then
                Label10.Left = Label10.Left - 100
                LBLTOTALBALES.Left = Label10.Left + Label10.Width
                LBLTOTALPCS.Left = LBLTOTALBALES.Left + LBLTOTALBALES.Width
                LBLTOTALMTRS.Left = LBLTOTALPCS.Left + LBLTOTALPCS.Width + LBLTOTALPCS.Width
                LBLAMOUNT.Visible = False
            End If

            If ClientName = "SBA" Or ClientName = "MANSI" Then GQUALITY.Visible = True
            If ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                CHKHIDEPCS.CheckState = CheckState.Checked
                'If ClientName = "KOTHARI" Then CHKWHITELABEL.CheckState = CheckState.Checked
            End If

            If ClientName = "DJIMPEX" Then
                LBLCHALLAN.Text = "Gross Wt"
                CHKYARD.Visible = True
                CHKYARD.CheckState = CheckState.Checked
                LBLBALETO.Visible = True
                TXTBALENOTO.Visible = True
            End If

            If ClientName = "MOMAI" Or ClientName = "KREEVE" Or ClientName = "MANSI" Then
                If ClientName = "MOMAI" Then CMBQUALITY.TabStop = False Else GQUALITY.HeaderText = "Size"
                TXTDESCRIPTION.TabStop = False
                TXTBALENO.TabStop = False
                TXTCUT.TabStop = False
                TXTMTRS.TabStop = False
                CMBPER.Text = "Pcs"
                TXTMTRS.BackColor = Color.White


            End If

            If ClientName = "MANSI" Then
                LBLSODATE.Visible = False
                SODATE.Visible = False
            End If

            If ClientName = "GELATO" Then
                GSHADE.HeaderText = "Size"
            End If

            If ClientName = "SMS" Then
                GBALENO.HeaderText = "Packing"
                TXTGRIDLOTNO.TabStop = False
            End If

            If ClientName = "YASHVI" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "MBB" Then
                Gpcs.ReadOnly = True
                GCUT.ReadOnly = True
                Gmtrs.ReadOnly = True
            End If

            If ClientName = "MANISH" Then
                GPRINTDESC.HeaderText = "Party Design No"
                GBALENO.HeaderText = "Batch No"
            End If


            If ClientName = "AFW" Then
                CMBGODOWN.TabStop = False
                cmbcity.TabStop = False
            End If

            If ClientName = "MAHAVIRPOLYCOT" And UserName <> "Admin" Then CMDSELECTSTOCK.Visible = False

            If ClientName = "RADHA" Then GBALENO.HeaderText = "Party Ch No"
            If ClientName = "DILIP" Or ClientName = "DILIPNEW" Then CMBDISPATCHTO.TabStop = False
            If ClientName = "SNCM" Then
                CMBAGENT.BackColor = Color.LemonChiffon
                CMBTRANS.BackColor = Color.LemonChiffon
                CMBDISPATCHTO.BackColor = Color.LemonChiffon
                cmbcity.BackColor = Color.LemonChiffon
                If UserName <> "Admin" Then GRIDGDN.ReadOnly = True
                If UserName <> "Admin" Then CMDSELECTSTOCK.Enabled = False
                End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSONO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTSONO.KeyPress
        numkeypress(e, TXTSONO, Me)
    End Sub

    Private Sub TXTSONO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTSONO.Validating
        Try
            If TXTSONO.Text.Trim <> "" Then
                Dim objclspreq As New ClsCommon()
                Dim DT As DataTable = objclspreq.SEARCH(" SALEORDER.so_no AS SONO, SALEORDER.so_date AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(ADDRESSMASTER.ADDRESS_ALIAS, '') AS Consignee, ISNULL(AGENTLEDGER.Acc_cmpname, '') AS Agent, SALEORDER.so_pono AS PONO, SALEORDER.so_DUEdate AS DUEDATE, SALEORDER_DESC.SO_GRIDSRNO AS GRIDSRNO, ISNULL(ITEMMASTER.item_name,'') AS MERCHANT   ", "", "  SALEORDER INNER JOIN LEDGERS ON SALEORDER.so_ledgerid = LEDGERS.Acc_id AND SALEORDER.SO_CMPID = LEDGERS.Acc_cmpid AND SALEORDER.SO_LOCATIONID = LEDGERS.Acc_locationid AND SALEORDER.SO_YEARID = LEDGERS.Acc_yearid INNER JOIN SALEORDER_DESC ON SALEORDER.so_no = SALEORDER_DESC.SO_NO AND SALEORDER.SO_CMPID = SALEORDER_DESC.SO_CMPID AND SALEORDER.SO_LOCATIONID = SALEORDER_DESC.SO_LOCATIONID AND SALEORDER.SO_YEARID = SALEORDER_DESC.SO_YEARID LEFT OUTER JOIN ITEMMASTER ON SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id AND SALEORDER_DESC.SO_CMPID = ITEMMASTER.item_cmpid AND SALEORDER_DESC.SO_LOCATIONID = ITEMMASTER.item_locationid AND SALEORDER_DESC.SO_YEARID = ITEMMASTER.item_yearid LEFT OUTER JOIN LEDGERS AS AGENTLEDGER ON SALEORDER.so_Agentid = AGENTLEDGER.Acc_id AND SALEORDER.SO_CMPID = AGENTLEDGER.Acc_cmpid AND SALEORDER.SO_LOCATIONID = AGENTLEDGER.Acc_locationid AND SALEORDER.SO_YEARID = AGENTLEDGER.Acc_yearid LEFT OUTER JOIN ADDRESSMASTER ON SALEORDER.SO_YEARID = ADDRESSMASTER.ADDRESS_yearid AND SALEORDER.SO_LOCATIONID = ADDRESSMASTER.ADDRESS_locationid AND SALEORDER.SO_CMPID = ADDRESSMASTER.ADDRESS_cmpid AND SALEORDER.so_HASTEID = ADDRESSMASTER.ADDRESS_ID ", " AND SALEORDER.SO_NO = " & Val(TXTSONO.Text.Trim) & " and (SALEORDER_DESC.SO_QTY - SALEORDER_DESC.SO_RECDQTY) > 0 and SALEORDER.so_CMPID = " & CmpId & " AND SALEORDER.so_LOCATIONID = " & Locationid & " AND SALEORDER.so_YEARID = " & YearId & " ORDER BY SALEORDER_DESC.SO_GRIDSRNO")
                If DT.Rows.Count > 0 Then
                    cmbname.Enabled = False
                    cmdselectOrder.Enabled = False
                    TXTSONO.Text = DT.Rows(0).Item("SONO")
                    TXTSOGRIDSRNO.Text = DT.Rows(0).Item("GRIDSRNO")
                    SODATE.Value = DT.Rows(0).Item("DATE")
                    txtpono.Text = DT.Rows(0).Item("PONO")
                    podate.Value = DT.Rows(0).Item("DUEDATE")
                    TXTCONSIGNEE.Text = DT.Rows(0).Item("Consignee")
                    CMBAGENT.Text = DT.Rows(0).Item("AGENT")
                    TXTMERCHANT.Text = DT.Rows(0).Item("MERCHANT")
                    TXTSONO.Enabled = False
                Else
                    If ClientName <> "KEMLINO" Then
                        If MsgBox("Invalid SO No ,Wish To Proceed ?", MsgBoxStyle.YesNo) = vbNo Then
                            TXTSONO.Clear()
                            e.Cancel = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTBARCODE.Validating
        Try
            If TXTBARCODE.Text.Trim <> "" And CHECKBARCODEERRORVALID = True Then
                'CHECKING WHETHER IS IS GONE OUT OR NOT
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("TOP 1 TYPE, FROMNO", "", " OUTBARCODESTOCK ", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "' AND CMPID = " & CmpId & " AND LOCATIONID = " & Locationid & " AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    MsgBox("Barcode Already Used in " & DT.Rows(0).Item("TYPE") & " Sr No " & DT.Rows(0).Item("FROMNO"))
                    TXTBARCODE.Clear()
                    e.Cancel = True
                    'Else
                    '    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            If CMBGODOWN.Text.Trim = "" And CMBHASTE.Text.Trim = "" Then
                MsgBox("Please Select Godown First", MsgBoxStyle.Critical)
                CMBGODOWN.Focus()
                Exit Sub
            End If

            Dim DTGDN As New DataTable
            If CMBHASTE.Text.Trim = "" Then
                Dim OBJSELECTGDN As New SelectStockGDN
                If ClientName <> "SANGHVI" And ClientName <> "TINUMINU" Then OBJSELECTGDN.GODOWN = CMBGODOWN.Text.Trim
                If ALLOWPACKINGSLIP = True Then OBJSELECTGDN.FILTER = " AND BARCODE = ''"
                If ALLOWPACKINGSLIP = True And cmbname.Text.Trim <> "" Then OBJSELECTGDN.FILTER = OBJSELECTGDN.FILTER & " AND JOBBERNAME = '" & cmbname.Text.Trim & "'"
                If ClientName = "RADHA" Or ClientName = "VINTAGEINDIA" Or ClientName = "AARYA" And cmbname.Text.Trim <> "" Then OBJSELECTGDN.FILTER = OBJSELECTGDN.FILTER & " AND PURNAME = '" & cmbname.Text.Trim & "'"
                OBJSELECTGDN.ShowDialog()
                DTGDN = OBJSELECTGDN.DT
            Else
                Dim OBJSELECTGDN As New SelectStockJobber
                OBJSELECTGDN.TYPE = " PACKTOJOB"
                OBJSELECTGDN.JOBBER = CMBHASTE.Text.Trim
                OBJSELECTGDN.ShowDialog()
                DTGDN = OBJSELECTGDN.DT
            End If
            If DTGDN.Rows.Count > 0 Then
                For Each DTROWPS As DataRow In DTGDN.Rows

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    If ALLOWBARCODEPRINT = True Or ALLOWPACKINGSLIP = True Then
                        For Each ROW As DataGridViewRow In GRIDGDN.Rows
                            If DTROWPS("BARCODE") <> "" And LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(DTROWPS("BARCODE")) Or (DTROWPS("BARCODE") = "" And Val(ROW.Cells(GFROMNO.Index).Value) = Val(DTROWPS("FROMNO")) And Val(ROW.Cells(GFROMSRNO.Index).Value) = Val(DTROWPS("FROMSRNO"))) Then GoTo LINE1
                        Next
                    End If

                    If Val(DTROWPS("PCS")) = 0 Then DTROWPS("PCS") = 1
                    If (ClientName <> "SAKARIA" And ClientName <> "ALENCOT" And ClientName <> "AVIS" And ClientName <> "MARKIN" And ClientName <> "DILIP" And ClientName <> "DILIPNEW" And ClientName <> "SHUBHI" And ClientName <> "SUBHLAXMI" And ClientName <> "SSC" And ClientName <> "RUCHITA" And ClientName <> "SARAYU" And ClientName <> "VALIANT" And ClientName <> "MBB" And ClientName <> "RADHA" And ClientName <> "MONOGRAM" And ClientName <> "SNCM" And ClientName <> "SHAILESHTRADING" And ClientName <> "CHINTAN") AndAlso Val(DTROWPS("CUT")) = 0 Then DTROWPS("CUT") = Val(DTROWPS("MTRS"))

                    Dim PER As String = "Mtrs"
                    Dim CCRATE As Double = 0
                    Dim CUT As Double = 0

                    If ClientName = "SOFTAS" Or ClientName = "DEVEN" Or ClientName = "DILIP" Or ClientName = "DILIPNEW" Or ClientName = "VINIT" Or ClientName = "CHINTAN" Then CUT = 0 Else CUT = Format(Val(DTROWPS("CUT")), "0.00")

                    Dim OBJCMN As New ClsCommon
                    If ClientName = "CC" Or ClientName = "C3" Then
                        Dim DTRATE As DataTable = OBJCMN.SEARCH("ISNULL(DESIGN_SALERATE,0) AS SALERATE, ISNULL(DESIGN_WRATE,0) AS WRATE", "", "DESIGNMASTER", " AND DESIGN_NO = '" & DTROWPS("DESIGNNO") & "' AND DESIGN_YEARID = " & YearId)
                        If CHKRETAIL.CheckState = CheckState.Checked Then CCRATE = DTRATE.Rows(0).Item("SALERATE") Else CCRATE = DTRATE.Rows(0).Item("WRATE")
                        PER = "Pcs"
                    End If

                    If ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then PER = "Pcs"

                    'GET RATE FROM PRICELIST
                    If ClientName = "MYCOT" Or ClientName = "SBA" Or ClientName = "DEVEN" Then
                        Dim DTPRICELIST As DataTable = OBJCMN.SEARCH(" ISNULL(LEDGERS.ACC_PRICELISTCOLUMN, '') AS COLNAME", "", " LEDGERS ", " AND ledgers.acc_cmpname = '" & cmbname.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                        If DTPRICELIST.Rows.Count > 0 AndAlso DTPRICELIST.Rows(0).Item("COLNAME") <> "" Then
                            Dim DTRATE As DataTable = OBJCMN.SEARCH(DTPRICELIST.Rows(0).Item("COLNAME") & " AS RATE", "", "ITEMPRICELIST INNER JOIN ITEMMASTER ON ITEMPRICELIST.ITEMID = ITEMMASTER.item_id ", " AND ITEMMASTER.ITEM_NAME = '" & DTROWPS("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                            If DTRATE.Rows.Count > 0 Then CCRATE = Val(DTRATE.Rows(0).Item("RATE"))
                        End If
                    End If

                    'getting per AND RATE from itemmaster, PER FOR AXIS AND RATE FOR MAN
                    If ClientName = "AXIS" Or ClientName = "MANS" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "SIDDHGIRI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "REALCORPORATION" Or ClientName = "SUPEEMA" Then
                        Dim DTPER As DataTable = OBJCMN.SEARCH("ISNULL(UNIT_ABBR,'Mtrs') AS PER, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE", "", " ITEMMASTER LEFT OUTER JOIN UNITMASTER ON item_unitid = UNIT_ID ", " AND ITEMMASTER.ITEM_NAME = '" & DTROWPS("ITEMNAME") & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                        If DTPER.Rows.Count > 0 Then
                            If ClientName = "AXIS" Or ClientName = "SIDDHGIRI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "REALCORPORATION" Or ClientName = "SUPEEMA" Then If DTPER.Rows(0).Item("PER") = "Pcs" Then PER = "Pcs"
                            If ClientName = "MANS" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "REALCORPORATION" Then CCRATE = Val(DTPER.Rows(0).Item("RATE"))
                        End If
                    End If


                    If ClientName = "ANOX" Then DTROWPS("BALENO") = ""
                    If ClientName = "RADHA" Then DTROWPS("BALENO") = DTROWPS("CHALLANNO")

                    If ClientName = "VINTAGEINDIA" Or ClientName = "AARYA" Then
                        txttransref.Text = DTROWPS("CHALLANNO")
                        cmbname.Text = DTROWPS("PURNAME")
                    End If

                    If ClientName = "SSC" And DTROWPS("TYPE") = "PACKING" Then DTROWPS("BALENO") = DTROWPS("GRIDREMARKS")


                    If ALLOWPACKINGSLIP = True Then
                        'WE NEED TO FETCH ALL THE DATA FROM BARCODESTOCK WITH RESPECT TO SELECTED FROMNO AND YEARID
                        'FOR MARKING WE NEED DETAILS AND FOR OTHERS WE WILL FETCH SUMMARY
                        Dim DTPS As New DataTable
                        If ClientName = "KOTHARI" Then
                            DTPS = OBJCMN.SEARCH(" ITEMNAME, QUALITY, DESIGNNO, COLOR, GODOWN, JOBBERNAME, UNIT, SUM(PCS) AS PCS, CUT, SUM(MTRS) AS MTRS, BARCODE, LOTNO, FROMNO, FROMSRNO, TYPE, PIECETYPE, BALENO ", "", " BARCODESTOCK ", " GROUP BY ITEMNAME, QUALITY, DESIGNNO, COLOR, GODOWN, JOBBERNAME, UNIT, CUT, BARCODE, LOTNO, FROMNO, FROMSRNO, TYPE, PIECETYPE, BALENO, YEARID HAVING ROUND(SUM(MTRS),2) >0 AND GODOWN = '" & CMBGODOWN.Text.Trim & "' AND BARCODE = '' AND FROMNO = " & Val(DTROWPS("FROMNO")) & " AND YEARID = " & YearId)
                        Else
                            DTPS = OBJCMN.SEARCH(" ITEMNAME, QUALITY, DESIGNNO, COLOR, GODOWN, JOBBERNAME, UNIT, SUM(PCS) AS PCS, CUT, SUM(MTRS) AS MTRS, BARCODE, LOTNO, FROMNO, FROMSRNO, TYPE, PIECETYPE, BALENO ", "", " BARCODESTOCK ", " GROUP BY ITEMNAME, QUALITY, DESIGNNO, COLOR, GODOWN, JOBBERNAME, UNIT, CUT, BARCODE, LOTNO, FROMNO, FROMSRNO, TYPE, PIECETYPE, BALENO, YEARID HAVING ROUND(SUM(MTRS),2) >0 AND GODOWN = '" & CMBGODOWN.Text.Trim & "' AND BARCODE = '' AND FROMNO = " & Val(DTROWPS("FROMNO")) & " AND FROMSRNO = " & Val(DTROWPS("FROMSRNO")) & "   AND YEARID = " & YearId)
                        End If
                        For Each DTROWBARCODE As DataRow In DTPS.Rows
                            GRIDGDN.Rows.Add(0, DTROWBARCODE("PIECETYPE"), DTROWBARCODE("ITEMNAME"), DTROWBARCODE("QUALITY"), "", DTROWBARCODE("DESIGNNO"), DTROWBARCODE("COLOR"), DTROWBARCODE("BALENO"), DTROWBARCODE("LOTNO"), Val(DTROWBARCODE("PCS")), DTROWBARCODE("UNIT"), Format(Val(DTROWBARCODE("CUT")), "0.00"), Format(Val(DTROWBARCODE("MTRS")), "0.00"), CCRATE, PER, 0, DTROWBARCODE("BARCODE"), DTROWBARCODE("FROMNO"), DTROWBARCODE("FROMSRNO"), DTROWBARCODE("TYPE"), 0, 0, 0, "")
                        Next

                    Else
                        If ClientName = "SVS" Then
                            GRIDGDN.Rows.Add(0, DTROWPS("PIECETYPE"), DTROWPS("ITEMNAME"), DTROWPS("QUALITY"), "", DTROWPS("DESIGNNO"), DTROWPS("COLOR"), "", DTROWPS("LOTNO"), 1, DTROWPS("UNIT"), Format(Val(DTROWPS("CUT")), "0.00"), Format(Val(DTROWPS("MTRS")), "0.00"), CCRATE, PER, 0, DTROWPS("BARCODE"), DTROWPS("FROMNO"), DTROWPS("FROMSRNO"), DTROWPS("TYPE"), 0, 0, 0, "")
                        Else
                            Dim GRIDDESC As String = ""
                            If ClientName = "AVIS" Then GRIDDESC = DTROWPS("LOTNO")
                            If ClientName = "KRFABRICS" Then
                                GRIDDESC = DTROWPS("BALENO")
                                DTROWPS("BALENO") = ""
                            End If

                            If ClientName = "AARYA" Then
                                GRIDDESC = Val(DTROWPS("MTRS"))
                                CUT = 0
                                DTROWPS("MTRS") = 0
                            End If
                            If ClientName = "KENCOT" Or ClientName = "SAFFRON" Or ClientName = "NTC" Or ClientName = "SOFTAS" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "SHREENAKODA" Or ClientName = "RAJKRIPA" Or ClientName = "MANSI" Then GRIDDESC = DTROWPS("GRIDREMARKS")

                            GRIDGDN.Rows.Add(0, DTROWPS("PIECETYPE"), DTROWPS("ITEMNAME"), DTROWPS("QUALITY"), GRIDDESC, DTROWPS("DESIGNNO"), DTROWPS("COLOR"), DTROWPS("BALENO"), DTROWPS("LOTNO"), Val(DTROWPS("PCS")), DTROWPS("UNIT"), CUT, Format(Val(DTROWPS("MTRS")), "0.00"), CCRATE, PER, 0, DTROWPS("BARCODE"), DTROWPS("FROMNO"), DTROWPS("FROMSRNO"), DTROWPS("TYPE"), 0, 0, 0, "")
                        End If
                    End If



LINE1:
                Next
                CMDSELECTSTOCK.Enabled = True
                getsrno(GRIDGDN)
                TOTAL()
                GRIDGDN.FirstDisplayedScrollingRowIndex = GRIDGDN.RowCount - 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBAGENT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT' "
                OBJLEDGER.ShowDialog()
                'If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBHASTE.Enter
        Try
            If CMBHASTE.Text.Trim = "" Then filljobbername(CMBHASTE, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBHASTE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBHASTE.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJOBBER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBHASTE.Validating
        Try
            If CMBHASTE.Text.Trim <> "" Then namevalidate(CMBHASTE, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'", "Sundry Debtors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        If CMBAGENT.Text.Trim = "" Then fillagentledger(CMBAGENT, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
    End Sub

    Private Sub txtremarks_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtremarks.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then txtremarks.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcity.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCITY As New SelectCity
                OBJCITY.FRMSTRING = "CITY"
                OBJCITY.ShowDialog()
                If OBJCITY.TEMPNAME <> "" Then cmbcity.Text = OBJCITY.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTTRANSREMARKS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTTRANSREMARKS.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then TXTTRANSREMARKS.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GDNDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles GDNDATE.GotFocus
        GDNDATE.SelectionStart = 0
    End Sub

    Private Sub GDNDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GDNDATE.Validating
        Try
            If GDNDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(GDNDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else

                    'SAME DATE FOR CHALLANDATE / LRDATE 
                    If ClientName = "LEEFABRICO" Then
                        SODATE.Value = GDNDATE.Text
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBITEMNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "MERCHANT"
                OBJItem.STRSEARCH = " and ITEM_cmpid = " & CmpId & " and ITEM_LOCATIONid = " & Locationid & " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then CMBITEMNAME.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBQUALITY.Enter
        Try
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJQ As New SelectQuality
                OBJQ.FRMSTRING = "QUALITY"
                OBJQ.ShowDialog()
                If OBJQ.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQ.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBQUALITY.Validating
        Try
            If CMBQUALITY.Text.Trim <> "" Then QUALITYVALIDATE(CMBQUALITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDESIGN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJD As New SelectDesign
                OBJD.ShowDialog()
                If OBJD.TEMPNAME <> "" Then CMBDESIGN.Text = OBJD.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCOLOR.Enter
        Try
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCOLOR.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCOLOR As New SelectShade
                OBJCOLOR.ShowDialog()
                If OBJCOLOR.TEMPNAME <> "" Then CMBCOLOR.Text = OBJCOLOR.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCOLOR.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPIECETYPE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPIECETYPE.Enter
        Try
            If CMBPIECETYPE.Text.Trim = "" Then fillPIECETYPE(CMBPIECETYPE)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPIECETYPE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPIECETYPE.Validating
        Try
            If CMBPIECETYPE.Text.Trim <> "" Then PIECETYPEvalidate(CMBPIECETYPE, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTAMOUNT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTAMOUNT.Validating
        Try
            If ClientName = "SKF" And CMBQUALITY.Text.Trim = "" Then
                MsgBox("Enter Proper Details")
                Exit Sub
            End If


            If CMBPIECETYPE.Text.Trim <> "" And CMBITEMNAME.Text.Trim <> "" Then
                If ClientName <> "MOMAI" And ClientName <> "KREEVE" And Val(TXTMTRS.Text.Trim) = 0 Then Exit Sub
                If ClientName <> "AMAN" And Val(TXTPCS.Text.Trim) = 0 Then Exit Sub
                FILLGRID()
                TOTAL()
            Else
                MsgBox("Enter Proper Details")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        GRIDGDN.Enabled = True
        If GRIDDOUBLECLICK = False Then
            GRIDGDN.Rows.Add(Val(txtsrno.Text.Trim), CMBPIECETYPE.Text.Trim, CMBITEMNAME.Text.Trim, CMBQUALITY.Text.Trim, TXTDESCRIPTION.Text.Trim, CMBDESIGN.Text.Trim, CMBCOLOR.Text.Trim, TXTBALENO.Text.Trim, TXTGRIDLOTNO.Text.Trim, Format(Val(TXTPCS.Text.Trim), "0.00"), "", Format(Val(TXTCUT.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), CMBPER.Text.Trim, Format(Val(TXTAMOUNT.Text.Trim), "0.00"), "", 0, 0, 0, 0, 0, 0, "")
            getsrno(GRIDGDN)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDGDN.Item(GSRNO.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDGDN.Item(GPIECETYPE.Index, TEMPROW).Value = CMBPIECETYPE.Text.Trim
            GRIDGDN.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
            GRIDGDN.Item(GQUALITY.Index, TEMPROW).Value = CMBQUALITY.Text.Trim
            GRIDGDN.Item(GPRINTDESC.Index, TEMPROW).Value = TXTDESCRIPTION.Text.Trim
            GRIDGDN.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDGDN.Item(GSHADE.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
            GRIDGDN.Item(GBALENO.Index, TEMPROW).Value = TXTBALENO.Text.Trim
            GRIDGDN.Item(GGRIDLOTNO.Index, TEMPROW).Value = TXTGRIDLOTNO.Text.Trim
            GRIDGDN.Item(Gpcs.Index, TEMPROW).Value = Format(Val(TXTPCS.Text.Trim), "0.00")
            GRIDGDN.Item(GCUT.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0.00")
            GRIDGDN.Item(Gmtrs.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDGDN.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.00")
            GRIDGDN.Item(GPER.Index, TEMPROW).Value = CMBPER.Text.Trim
            GRIDGDN.Item(GAMOUNT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")
            GRIDGDN.Item(GGRIDLOTNO.Index, TEMPROW).Value = TXTGRIDLOTNO.Text.Trim

            GRIDDOUBLECLICK = False

        End If

        CALC()
        TOTAL()

        GRIDGDN.FirstDisplayedScrollingRowIndex = GRIDGDN.RowCount - 1

        If ClientName <> "SKF" And ClientName <> "MANIBHADRA" And ClientName <> "ALENCOT" And ClientName <> "MNARESH" And ClientName <> "SOFTAS" And ClientName <> "MANISH" And ClientName <> "MVIKASKUMAR" And ClientName <> "MOMAI" And ClientName <> "SMS" And ClientName <> "SHREEVALLABH" And ClientName <> "RAJDEEP" And ClientName <> "KREEVE" And ClientName <> "MSSYNTHETICS" And ClientName <> "AMAN" And ClientName <> "BALAJI" And ClientName <> "VINAYAK" And ClientName <> "MAFATLAL" And ClientName <> "NAMOCOT" Then

            CMBITEMNAME.Text = ""
            TXTDESCRIPTION.Clear()
            CMBDESIGN.Text = ""
            CMBCOLOR.Text = ""
            TXTBALENO.Clear()
            TXTPCS.Clear()
            TXTCUT.Clear()
            TXTMTRS.Clear()
            TXTRATE.Clear()
            CMBPER.SelectedItem = Nothing
            TXTAMOUNT.Clear()
            CMBPIECETYPE.Focus()
        Else
            If ClientName <> "SKF" Then TXTMTRS.Clear()
            If ClientName = "LEEFABRICO" Then TXTPCS.Clear()
            If ClientName = "RUCHITA" Then TXTDESCRIPTION.Clear()
            If ClientName = "MOMAI" Then
                CMBDESIGN.Text = ""
                TXTPCS.Clear()
                CMBDESIGN.Focus()
            ElseIf ClientName = "SKF" Or ClientName = "BALAJI" Or ClientName = "NAMOCOT" Then
                CMBITEMNAME.Focus()
            ElseIf ClientName = "MANISH" Or ClientName = "MAFATLAL" Then
                CMBCOLOR.Focus()
            Else
                TXTMTRS.Focus()
            End If
        End If
        txtsrno.Text = Val(GRIDGDN.RowCount) + 1

    End Sub

    Private Sub GRIDGDN_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDGDN.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDGDN.CurrentRow.Index >= 0 And GRIDGDN.Item(GSRNO.Index, GRIDGDN.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True

                txtsrno.Text = GRIDGDN.Item(GSRNO.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                CMBPIECETYPE.Text = GRIDGDN.Item(GPIECETYPE.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = GRIDGDN.Item(GITEMNAME.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                CMBQUALITY.Text = GRIDGDN.Item(GQUALITY.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                TXTDESCRIPTION.Text = GRIDGDN.Item(GPRINTDESC.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDGDN.Item(GDESIGN.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                CMBCOLOR.Text = GRIDGDN.Item(GSHADE.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                TXTBALENO.Text = GRIDGDN.Item(GBALENO.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                TXTPCS.Text = GRIDGDN.Item(Gpcs.Index, GRIDGDN.CurrentRow.Index).Value.ToString

                TXTCUT.Text = GRIDGDN.Item(GCUT.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                TXTMTRS.Text = GRIDGDN.Item(Gmtrs.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                TXTRATE.Text = GRIDGDN.Item(GRATE.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                CMBPER.Text = GRIDGDN.Item(GPER.Index, GRIDGDN.CurrentRow.Index).Value.ToString
                TXTAMOUNT.Text = GRIDGDN.Item(GAMOUNT.Index, GRIDGDN.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDGDN.CurrentRow.Index
                CMBPIECETYPE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPCS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPCS.KeyPress, TXTMTRS.KeyPress, TXTRATE.KeyPress, TXTCUT.KeyPress, TXTSTOREQTY.KeyPress, TXTDYEINGRECNO.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Sub CALC()
        Try
            TXTAMOUNT.Text = 0.0
            If Val(TXTRATE.Text.Trim) > 0 And Val(TXTPCS.Text.Trim) > 0 And Val(TXTMTRS.Text.Trim) > 0 Then
                If CMBPER.Text = "Mtrs" Then
                    TXTAMOUNT.Text = Format(Val(TXTMTRS.Text) * Val(TXTRATE.Text), "0.00")
                Else
                    TXTAMOUNT.Text = Format(Val(TXTPCS.Text) * Val(TXTRATE.Text), "0.00")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBPER.Validated
        Try
            CALC()
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txttransref_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txttransref.Validating
        Try
            Dim OBJCMN As New ClsCommon()
            Dim dt As New DataTable
            If txttransref.Text.Trim.Length > 0 And ClientName <> "KCRAYON" And ClientName <> "DJIMPEX" And ClientName <> "PARAS" And ClientName <> "SONU" And ClientName <> "AMAN" And ClientName <> "AARYA" And ClientName <> "RAJKRIPA" Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(txttransref.Text.Trim)) Then
                    dt = OBJCMN.SEARCH(" GDN.GDN_TRANSREFNO AS CHALLANNO ", "", " GDN ", " and GDN_TRANSREFNO = '" & txttransref.Text.Trim & "' AND GDN_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Challan No. Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                        e.Cancel = True
                    End If
                End If
            End If


            'FOR AMAN WE NEED TO CHECK WHETHER THIS PARTYCHALLAN IS CORRECT OR NOT, IF CORRECT THEN CHECK WHETHER WE HAVE STOCK FOR THIS CHALLAN OR NOT
            If (ClientName = "AMAN" Or ClientName = "AARYA") And txttransref.Text.Trim <> "" Then
                Dim BALPCS As Double = 0
                dt = OBJCMN.SEARCH(" CHALLANNO, SUM(PCS) AS INPCS, SUM(MTRS) AS INMTRS, SUM(PCS)-SUM(ISSPCS) AS BALPCS, SUM(MTRS)-SUM(ISSMTRS) AS BALMTRS ", "", " STOCKREGISTER ", " AND NAME = '" & cmbname.Text.Trim & "' AND CHALLANNO = '" & txttransref.Text.Trim & "' AND YEARID = " & YearId & " GROUP BY NAME, CHALLANNO ")
                If dt.Rows.Count > 0 Then

                    'CHECK THE STOCK
                    BALPCS = Val(dt.Rows(0).Item("BALPCS"))

                    'ON EDIT MODE ADD CHALLANPCS ALSO
                    If EDIT = True Then
                        Dim DTGDN As DataTable = OBJCMN.Execute_Any_String("SELECT GDN_TOTALPCS AS TOTALPCS FROM GDN WHERE GDN_NO = " & Val(txtgdnno.Text.Trim) & " AND GDN_YEARID = " & YearId, "", "")
                        If DTGDN.Rows.Count > 0 Then BALPCS += Val(DTGDN.Rows(0).Item("TOTALPCS"))
                    End If


                    If Val(BALPCS) <= 0 Then
                        MsgBox("Challan No not in Stock", MsgBoxStyle.Critical, "TEXTRADE")
                        e.Cancel = True
                    End If

                    'IF PCS ARE IN STOCK AND WE HAVE LOCKED CHALLAN THEN ALSO VALIDATE
                    'WE HAVE GIVE AN OPTION IN GRN TO LOCK THIS CHALLAN MANUALLY IF WE WANT TO CLOSE THIS
                    'IF WE CLOSE THIS CHALLAN NO IN GRN THEN CHECK IT AND DONT ALLOW TO ENTER THAT CHALLANNO
                    'WE HAVE USED LOTREADY CHECK BOX IN GRN FOR THIS PURPOSE
                    Dim DTGRN As DataTable = OBJCMN.SEARCH("ISNULL(GRN_LOTREADY,0) AS CHALLANLOCK", "", " GRN INNER JOIN LEDGERS ON GRN_LEDGERID = LEDGERS.ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND GRN_CHALLANNO = '" & txttransref.Text.Trim & "' AND GRN.GRN_YEARID = " & YearId)
                    If DTGRN.Rows.Count > 0 Then
                        If Convert.ToBoolean(DTGRN.Rows(0).Item("CHALLANLOCK")) = True Then
                            MsgBox("Invalid Challan No., Challan has been Locked ", MsgBoxStyle.Critical, "TEXTRADE")
                            e.Cancel = True
                        End If
                    End If
                Else
                    MsgBox("Invalid Challan No. ", MsgBoxStyle.Critical, "TEXTRADE")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtgdnno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtgdnno.KeyPress
        numkeypress(e, txtgdnno, Me)
    End Sub

    Private Sub txtgdnno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtgdnno.Validating
        Try
            If Val(txtgdnno.Text.Trim) <> 0 And EDIT = False Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(GDN.GDN_NO,0)  AS GDNNO", "", " GDN ", "  AND GDN.GDN_NO=" & txtgdnno.Text.Trim & " AND GDN.GDN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    MsgBox("Challan No Already Exist")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTFROM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCOPIES.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub CMBTYPE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTYPE.Validated
        Try
            GETMAXTYPECHALLANNO()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAXTYPECHALLANNO()
        Try
            'GET MAX NO WITH RESPECT TO SELECTED CHALLANTYPE
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("isnull(max(GDN_TYPENO),0) + 1", "", "GDN INNER JOIN  CHALLANTYPEMASTER ON GDN_TYPEID = CHALLANTYPE_ID", " AND CHALLANTYPE_NAME = '" & CMBTYPE.Text.Trim & "' AND GDN_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then TXTTYPECHALLANNO.Text = Val(DT.Rows(0).Item(0))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Validated
        Try
            If cmbname.Text.Trim <> "" And CMBDISPATCHTO.Text.Trim = "" Then CMBDISPATCHTO.Text = cmbname.Text.Trim
            If ClientName = "MVIKASKUMAR" And cmbname.Text.Trim <> "" Then
                CMBDISPATCHTO.Text = cmbname.Text.Trim
                CMBDISPATCHTO_Validated(sender, e)
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("  ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(ACC_MOBILE,'') AS MOBILENO, ISNULL(ACC_HOLDFORAPPROVAL,0) AS HOLDFORAPPROVAL, ISNULL(LEDGERS.ACC_WARNING,'') AS WARNINGTEXT ", "", "CITYMASTER RIGHT OUTER JOIN LEDGERS ON CITYMASTER.city_id = LEDGERS.ACC_DELIVERYATID ", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                If cmbname.Text.Trim = CMBDISPATCHTO.Text.Trim And ((ClientName = "MANISH" And cmbcity.Text.Trim = "") Or ClientName <> "MANISH") Then cmbcity.Text = DT.Rows(0).Item(0)
                TXTMOBILENO.Text = DT.Rows(0).Item("MOBILENO")
                If EDIT = False Then CHKHOLD.Checked = Convert.ToBoolean(DT.Rows(0).Item("HOLDFORAPPROVAL"))
                If DT.Rows(0).Item("WARNINGTEXT") <> "" Then MsgBox(DT.Rows(0).Item("WARNINGTEXT"), MsgBoxStyle.Critical)
            End If
            If ClientName = "SNCM" Then
                If cmbname.Text.Trim <> "" Then
                    cmdselectOrder.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDISPATCHTO_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDISPATCHTO.Enter
        Try
            If ClientName = "AVIS" Then
                If CMBDISPATCHTO.Text.Trim = "" Then fillname(CMBDISPATCHTO, EDIT, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')  AND GROUP_NAME = 'HASTE DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBDISPATCHTO.Text.Trim = "" Then fillname(CMBDISPATCHTO, EDIT, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')   AND ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDISPATCHTO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDISPATCHTO.Validating
        Try
            'If CMBDISPATCHTO.Text.Trim <> "" Then namevalidate(CMBDISPATCHTO, CMBCODE, e, Me, TXTDISPATCHADDRESS, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS")

            'If CMBDISPATCHTO.Text.Trim <> "" And ClientName = "DAKSH" And ClientName = "PURVITEX" And ClientName = "SHALIBHADRA" Then
            If CHKCHANGEADD.Checked = True Then
                If CMBDISPATCHTO.Text.Trim <> "" Then NAMEVALIDATE(CMBDISPATCHTO, CMBCODE, e, Me, TXTDISPATCHADDRESS, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry CREDITORS", "ACCOUNTS")
            Else
                If CMBDISPATCHTO.Text.Trim <> "" Then NAMEVALIDATE(CMBDISPATCHTO, CMBCODE, e, Me, TXTDELIVERYADDRESS, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry CREDITORS", "ACCOUNTS")
            End If
            'Else
            'namevalidate(CMBDISPATCHTO, CMBCODE, e, Me, TXTDISPATCHADDRESS, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS")
            'End If
            ' If TXTDISPATCHADDRESS.Text.Trim <> "" Then TXTDELIVERYADDRESS.Text = TXTDISPATCHADDRESS.Text.Trim

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBALENOFROM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTBALENOFROM.KeyPress
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBALENOTO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTBALENOTO.KeyPress
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txttransref_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttransref.KeyPress
        If ClientName = "KENCOT" Then numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TOOLSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLSMS.Click
        If EDIT = False Then Exit Sub
        SMSCODE()
    End Sub

    Sub SMSCODE()
        If ALLOWSMS = True Then
            If TXTMOBILENO.Text.Trim = "" Then Exit Sub
            If ClientName = "KOTHARI" And CMBDISPATCHTO.Text.Trim = "" Then Exit Sub

            If MsgBox("Send SMS?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim MSG As String = ""
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            MSG = MSG & CMBDISPATCHTO.Text.Trim & " - " & cmbcity.Text.Trim & "\n"
            MSG = MSG & "GOODS READY" & "\n"
            MSG = MSG & "BALE NO." & txtgdnno.Text.Trim & " X " & TXTBALENOFROM.Text.Trim & "\n"
            For Each ROW As DataGridViewRow In GRIDGDN.Rows
                If ROW.Cells(GPER.Index).Value = "Mtrs" Then MSG = MSG & Val(ROW.Cells(GSRNO.Index).Value) & ") " & ROW.Cells(GITEMNAME.Index).Value & "-" & ROW.Cells(GPRINTDESC.Index).Value & " " & Format(Val(ROW.Cells(Gmtrs.Index).Value), "0.00") & " " & ROW.Cells(GPER.Index).Value & "\n" Else MSG = MSG & Val(ROW.Cells(GSRNO.Index).Value) & ") " & ROW.Cells(GITEMNAME.Index).Value & "-" & ROW.Cells(GPRINTDESC.Index).Value & "-" & Format(Val(ROW.Cells(Gpcs.Index).Value), "0") & " " & ROW.Cells(GPER.Index).Value & " " & "-" & " " & Format(Val(ROW.Cells(Gmtrs.Index).Value), "0.00") & " " & "MTRS" & "\n"
            Next

            If SENDMSG(MSG, TXTMOBILENO.Text.Trim) = "1701" Then
                MsgBox("Message Sent")
                DT = OBJCMN.Execute_Any_String("UPDATE GDN SET GDN_SMSSEND = 1 WHERE GDN_NO = " & TEMPGDNNO & " AND GDN_YEARID = " & YearId, "", "")
                LBLSMS.Visible = True
            Else
                MsgBox("Error Sending Message")
            End If
        End If
    End Sub

    Private Sub TXTMTRS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTMTRS.Validating
        Try
            If ClientName = "MANIBHADRA" Or ClientName = "AMAN" Or ClientName = "AARYA" Or ClientName = "MAFATLAL" Or ClientName = "MASHOK" Then TXTAMOUNT_Validating(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDORDER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDORDER.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDORDER.RowCount > 0 Then GRIDORDER.Rows.RemoveAt(GRIDORDER.CurrentRow.Index)
            getsrno(GRIDORDER)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_Validated(sender As Object, e As EventArgs) Handles TXTCUT.Validated, TXTPCS.Validated
        Try
            If Val(TXTCUT.Text.Trim) > 0 And Val(TXTPCS.Text.Trim) > 0 Then TXTMTRS.Text = Val(TXTPCS.Text.Trim) * Val(TXTCUT.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If EDIT = True Then SENDWHATSAPP(TEMPGDNNO)
            DT = OBJCMN.Execute_Any_String("UPDATE GDN SET GDN_SENDWHATSAPP = 1 WHERE GDN_NO = " & TEMPGDNNO & " AND GDN_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(GDNNO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJGDN As New GDNDESIGN
            OBJGDN.MdiParent = MDIMain
            OBJGDN.DIRECTPRINT = True
            OBJGDN.FRMSTRING = "GDN"
            If ClientName = "MANSI" AndAlso MsgBox("Print Challan for Garments?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then OBJGDN.FRMSTRING = "GDNGARMENT"
            OBJGDN.DIRECTMAIL = True
            OBJGDN.vendorname = cmbname.Text.Trim
            OBJGDN.agentname = CMBAGENT.Text.Trim
            OBJGDN.FORMULA = "{GDN.GDN_no}=" & Val(GDNNO) & " and {GDN.GDN_yearid}=" & YearId
            OBJGDN.JONO = GDNNO
            OBJGDN.NOOFCOPIES = 1
            OBJGDN.WHITELABEL = CHKWHITELABEL.Checked
            OBJGDN.HIDEPCSDETAILS = CHKHIDEPCS.Checked
            OBJGDN.PRINTINYARDS = CHKYARD.Checked
            OBJGDN.Show()
            OBJGDN.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = cmbname.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBAGENT.Text.Trim
            If cmbname.Text.Trim <> CMBDISPATCHTO.Text.Trim Then OBJWHATSAPP.OTHERNAME1 = CMBDISPATCHTO.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & cmbname.Text.Trim & "GDN_" & Val(GDNNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add(cmbname.Text.Trim & "GDN_" & Val(GDNNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validated(sender As Object, e As EventArgs) Handles CMBITEMNAME.Validated
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If ClientName = "SOFTAS" And CMBITEMNAME.Text.Trim <> "" Then TXTDESCRIPTION.Text = CMBITEMNAME.Text.Trim
            If ClientName = "SMS" Or ClientName = "KREEVE" Or ClientName = "MVIKASKUMAR" Or ClientName = "MANISH" Or ClientName = "SIDDHPOLYCOT" Or ClientName = "SHAILESHTRADING" Then
                DT = OBJCMN.Execute_Any_String(" SELECT ISNULL(ITEMMASTER.ITEM_REORDER,0) AS CUT, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT FROM ITEMMASTER LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNIT_ID WHERE ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "' AND ITEM_YEARID = " & YearId, "", "")
                If DT.Rows.Count > 0 Then
                    If ClientName = "SMS" Then TXTCUT.Text = Val(DT.Rows(0).Item("CUT"))
                    If ClientName = "KREEVE" Or ClientName = "MANISH" Or ClientName = "SIDDHPOLYCOT" Then TXTRATE.Text = Val(DT.Rows(0).Item("RATE"))
                    If ClientName = "MVIKASKUMAR" Or ClientName = "SIDDHPOLYCOT" Or ClientName = "SHAILESHTRADING" Then
                        If LCase(DT.Rows(0).Item("UNIT")) = "pcs" Or LCase(DT.Rows(0).Item("UNIT")) = "mtrs" Or LCase(DT.Rows(0).Item("UNIT")) = "yards" Then CMBPER.Text = DT.Rows(0).Item("UNIT")
                    End If
                End If
            End If

            'SHOW STOCK
            If ClientName = "AKASHDEEP" And CMBITEMNAME.Text.Trim <> "" Then
                DT = OBJCMN.Execute_Any_String(" SELECT ROUND(SUM(MTRS)-SUM(ISSMTRS),2) AS MTRS FROM STOCKREGISTER WHERE ITEMNAME = '" & CMBITEMNAME.Text.Trim & "' AND YEARID = " & YearId, "", "")
                If DT.Rows.Count > 0 Then TXTSTOCK.Text = DT.Rows(0).Item("MTRS")
            End If
            If ClientName = "BARKHA" Then
                DT = OBJCMN.SEARCH(" ISNULL(PARTYITEMWISECHART.PAR_STAMPING, '') AS STAMPING", "", " PARTYITEMWISECHART INNER JOIN LEDGERS ON PARTYITEMWISECHART.PAR_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON PARTYITEMWISECHART.PAR_ITEMID = ITEMMASTER.item_id ", " AND ledgers.acc_cmpname = '" & cmbname.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & CMBITEMNAME.Text.Trim & " ' AND PARTYITEMWISECHART.PAR_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    For Each DTROW As DataRow In DT.Rows
                        TXTDESCRIPTION.Text = (DT.Rows(0).Item("STAMPING"))
                    Next
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDISPATCHTO_Validated(sender As Object, e As EventArgs) Handles CMBDISPATCHTO.Validated
        Try
            If CMBDISPATCHTO.Text.Trim <> "" And (EDIT = False Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "MVIKASKUMAR" Or ClientName = "SUPEEMA") Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("  ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(ACC_MOBILE,'') AS MOBILENO, ISNULL(ACC_HOLDFORAPPROVAL,0) AS HOLDFORAPPROVAL, ISNULL(LEDGERS.ACC_WARNING,'') AS WARNINGTEXT ", "", "CITYMASTER RIGHT OUTER JOIN LEDGERS ON CITYMASTER.city_id = LEDGERS.ACC_DELIVERYATID", " AND LEDGERS.ACC_CMPNAME = '" & CMBDISPATCHTO.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If (ClientName = "MANISH" And cmbcity.Text.Trim = "") Or ClientName <> "MANISH" Then cmbcity.Text = DT.Rows(0).Item("CITY")
                    TXTMOBILENO.Text = DT.Rows(0).Item("MOBILENO")
                    CHKHOLD.Checked = Convert.ToBoolean(DT.Rows(0).Item("HOLDFORAPPROVAL"))
                    If DT.Rows(0).Item("WARNINGTEXT") <> "" Then MsgBox(DT.Rows(0).Item("WARNINGTEXT"), MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If ALLOWPACKINGSLIP = True Then Exit Sub

            If TXTBARCODE.Text.Trim.Length > 0 Then

                If CMBGODOWN.Text.Trim = "" Then
                    MsgBox("Select Godown First", MsgBoxStyle.Critical)
                    Exit Sub
                End If


                TXTBARCODE.Text = TXTBARCODE.Text.Replace(" TRIAL", "")


                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 * ", "", "BARCODESTOCK", " AND BARCODE = '" & TXTBARCODE.Text.Trim & "' AND DONE = 0 AND CMPID = " & CmpId & " AND LOCATIONID  = " & Locationid & " AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then

                    'VALIDATE GODOWN
                    If DT.Rows(0).Item("GODOWN") <> CMBGODOWN.Text.Trim And ClientName <> "SANGHVI" And ClientName <> "TINUMINU" Then
                        MsgBox("Item Not in Selected Godown", MsgBoxStyle.Critical)
                        TXTBARCODE.Clear()
                        Exit Sub
                    End If

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    For Each ROW As DataGridViewRow In GRIDGDN.Rows
                        If LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(TXTBARCODE.Text.Trim) Then GoTo LINE1
                    Next

                    Dim PER As String = "Mtrs"
                    Dim GRIDREMARKS As String = ""
                    Dim RATE As Double = 0
                    Dim PCS As Double = 0

                    If ClientName = "TCOT" Or ClientName = "KCRAYON" Or ClientName = "SBA" Or ClientName = "AXIS" Or ClientName = "KRISHNA" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "SSC" Or ClientName = "VINIT" Or ClientName = "RUCHITA" Or ClientName = "SARAYU" Or ClientName = "VALIANT" Or ClientName = "MBB" Or ClientName = "SIDDHGIRI" Or ClientName = "SNCM" Or ClientName = "CHINTAN" Or ClientName = "SHAILESHTRADING" Then PCS = Val(DT.Rows(0).Item("PCS")) Else PCS = 1

                    If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                        Dim DTRATE As DataTable = OBJCMN.SEARCH("ISNULL(DESIGN_SALERATE,0) AS SALERATE, ISNULL(DESIGN_WRATE,0) AS WRATE", "", "DESIGNMASTER", " AND DESIGN_NO = '" & DT.Rows(0).Item("DESIGNNO") & "' AND DESIGN_YEARID = " & YearId)
                        If CHKRETAIL.CheckState = CheckState.Checked Then RATE = DTRATE.Rows(0).Item("SALERATE") Else RATE = DTRATE.Rows(0).Item("WRATE")
                        PER = "Pcs"
                    End If


                    'getting per AND RATE from itemmaster, PER FOR AXIS AND RATE FOR MAN
                    If ClientName = "AXIS" Or ClientName = "MANS" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "SIDDHGIRI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "SHAILESHTRADING" Or ClientName = "REALCORPORATION" Or ClientName = "SUPEEMA" Then
                        Dim DTPER As DataTable = OBJCMN.SEARCH("ISNULL(UNIT_ABBR,'Mtrs') AS PER, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE", "", " ITEMMASTER LEFT OUTER JOIN UNITMASTER ON item_unitid = UNIT_ID ", " AND ITEMMASTER.ITEM_NAME = '" & DT.Rows(0).Item("ITEMNAME") & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                        If DTPER.Rows.Count > 0 Then
                            If ClientName = "AXIS" Or ClientName = "SIDDHGIRI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "SHAILESHTRADING" Or ClientName = "REALCORPORATION" Or ClientName = "SUPEEMA" Then If DTPER.Rows(0).Item("PER") = "Pcs" Then PER = "Pcs"
                            If ClientName = "MANS" Or ClientName = "MAHAVIRPOLYCOT" Or ClientName = "REALCORPORATION" Then RATE = Val(DTPER.Rows(0).Item("RATE"))
                        End If
                    End If


                    'GET RATE
                    'FOR THIS WE NEED TO GET THE COLUMN NAME FROM RATETYPEMASREER 
                    If ClientName = "SBA" Or ClientName = "MYCOT" Or ClientName = "DEVEN" Then
                        Dim DTRATE As DataTable = OBJCMN.SEARCH(" ISNULL(LEDGERS.ACC_PRICELISTCOLUMN, '') AS COLNAME", "", " LEDGERS ", " AND ledgers.acc_cmpname = '" & cmbname.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
                        If DTRATE.Rows.Count > 0 AndAlso DTRATE.Rows(0).Item("COLNAME") <> "" Then
                            DTRATE = OBJCMN.SEARCH(DTRATE.Rows(0).Item("COLNAME") & " AS RATE", "", "ITEMPRICELIST INNER JOIN ITEMMASTER ON ITEMPRICELIST.ITEMID = ITEMMASTER.item_id ", " AND ITEMMASTER.ITEM_NAME = '" & DT.Rows(0).Item("ITEMNAME") & "' AND ITEM_YEARID = " & YearId)
                            If DTRATE.Rows.Count > 0 Then RATE = Val(DTRATE.Rows(0).Item("RATE"))
                        End If
                    End If

                    'FOR ANOX GET RATES FROM ORDERGRID WHILE SCANNING BARCODES, REMOVE BALE NO
                    If ClientName = "ANOX" Then
                        For Each ROW As DataGridViewRow In GRIDORDER.Rows
                            If ROW.Cells(OITEMNAME.Index).Value = DT.Rows(0).Item("ITEMNAME") And ROW.Cells(ODESIGN.Index).Value = DT.Rows(0).Item("DESIGNNO") And ROW.Cells(OCOLOR.Index).Value = DT.Rows(0).Item("COLOR") Then
                                RATE = Val(ROW.Cells(ORATE.Index).Value)
                                Exit For
                            End If
                        Next
                        DT.Rows(0).Item("BALENO") = ""
                    End If



                    If ClientName = "KENCOT" Or ClientName = "SAFFRON" Or ClientName = "NTC" Or ClientName = "SOFTAS" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "SHREENAKODA" Or ClientName = "RAJKRIPA" Or ClientName = "MANSI" Then GRIDREMARKS = DT.Rows(0).Item("GRIDREMARKS")
                    If ClientName = "KRFABRICS" Then
                        GRIDREMARKS = DT.Rows(0).Item("BALENO")
                        DT.Rows(0).Item("BALENO") = ""
                    End If

                    If ClientName = "RADHA" Then
                        DT.Rows(0).Item("BALENO") = DT.Rows(0).Item("CHALLANNO")
                    End If


                    If ClientName = "SSC" And (DT.Rows(0).Item("TYPE") = "PACKING" Or DT.Rows(0).Item("TYPE") = "OPENING") Then DT.Rows(0).Item("BALENO") = DT.Rows(0).Item("GRIDREMARKS")




                    'GET RATE FROM PRICELISTMASTER
                    If ClientName = "MOMAI" Then
                        Dim WHERECLAUSE As String = ""
                        If DT.Rows(0).Item("ITEMNAME") <> "" Then WHERECLAUSE = WHERECLAUSE & " AND ITEMMASTER.ITEM_NAME = '" & DT.Rows(0).Item("ITEMNAME") & "'"
                        If DT.Rows(0).Item("DESIGNNO") <> "" Then WHERECLAUSE = WHERECLAUSE & " AND DESIGNMASTER.DESIGN_NO = '" & DT.Rows(0).Item("DESIGNNO") & "'"

                        Dim DTRATE As DataTable = OBJCMN.SEARCH(" TOP 1  ISNULL(PL_RATE,0) AS RATE", "", "PRICELISTMASTER LEFT OUTER JOIN ITEMMASTER ON PL_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN DESIGNMASTER ON PL_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON PL_COLORID = COLORMASTER.COLOR_ID LEFT OUTER JOIN LEDGERS ON PL_LEDGERID = LEDGERS.ACC_ID ", WHERECLAUSE & " AND (LEDGERS.Acc_cmpname = '" & cmbname.Text.Trim & "' OR ISNULL(LEDGERS.Acc_cmpname,'') = '') AND PL_YEARID = " & YearId & " ORDER BY LEDGERS.ACC_CMPNAME DESC")
                        If DTRATE.Rows.Count > 0 Then RATE = Val(DTRATE.Rows(0).Item("RATE"))
                    End If

                    If ClientName = "GELATO" Then PER = "Pcs"

                    GRIDGDN.Rows.Add(GRIDGDN.RowCount + 1, DT.Rows(0).Item("PIECETYPE"), DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), GRIDREMARKS, DT.Rows(0).Item("DESIGNNO"), DT.Rows(0).Item("COLOR"), DT.Rows(0).Item("BALENO"), DT.Rows(0).Item("LOTNO"), PCS, DT.Rows(0).Item("UNIT"), Format(Val(DT.Rows(0).Item("CUT")), "0.00"), Format(Val(DT.Rows(0).Item("MTRS")), "0.00"), RATE, PER, 0, DT.Rows(0).Item("BARCODE"), DT.Rows(0).Item("FROMNO"), DT.Rows(0).Item("FROMSRNO"), DT.Rows(0).Item("TYPE"), 0, 0, 0, "")
                    TOTAL()
                    GRIDGDN.FirstDisplayedScrollingRowIndex = GRIDGDN.RowCount - 1


LINE1:
                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()
                Else
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    TXTBARCODE.Clear()
                    TXTBARCODE.Focus()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSTOREITEMNAME_Enter(sender As Object, e As EventArgs) Handles CMBSTOREITEMNAME.Enter
        Try
            If CMBSTOREITEMNAME.Text.Trim = "" Then FILLSTOREITEMNAME(CMBSTOREITEMNAME)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSTOREITEMNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBSTOREITEMNAME.Validating
        Try
            If CMBSTOREITEMNAME.Text.Trim <> "" Then STOREITEMVALIDATE(CMBSTOREITEMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCONTRACTOR_Enter(sender As Object, e As EventArgs) Handles CMBCONTRACTOR.Enter
        Try
            If CMBCONTRACTOR.Text.Trim = "" Then FILLCONTRACT(CMBCONTRACTOR)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCONTRACTOR_Validating(sender As Object, e As CancelEventArgs) Handles CMBCONTRACTOR.Validating
        Try
            If CMBCONTRACTOR.Text.Trim <> "" Then CONTRACTVALIDATE(CMBCONTRACTOR, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTDYEINGRECNO_Validated(sender As Object, e As EventArgs) Handles TXTDYEINGRECNO.Validated
        Try
            If ClientName = "AVIS" And Val(TXTDYEINGRECNO.Text.Trim) > 0 And EDIT = False Then
                Dim OBJMATREC As New ClsMaterialReceipt()
                Dim dttable As DataTable = OBJMATREC.SELECTMATREC(Val(TXTDYEINGRECNO.Text.Trim), CmpId, Locationid, YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DR As DataRow In dttable.Rows
                        If Val(DR("OUTMTRS")) = 0 And Convert.ToBoolean(DR("INHOUSECHECKDONE")) = False Then
                            GRIDGDN.Rows.Add(Val(DR("GRIDSRNO")), "FRESH", DR("ITEMNAME"), DR("QUALITY"), "", DR("DESIGN"), DR("COLOR"), DR("BALENO"), DR("GRIDLOTNO"), Val(DR("QTY")), DR("UNIT"), Val(DR("CUT")), Format(Val(DR("RECDMTRS")), "0.00"), 0, "Mtrs", 0, DR("BARCODE"), Val(TXTDYEINGRECNO.Text.Trim), DR("GRIDSRNO"), "MATREC", 0, 0, 0, "")
                        End If
                    Next
                    getsrno(GRIDGDN)
                    TOTAL()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CHKCHANGEADD_CheckedChanged(sender As Object, e As EventArgs) Handles CHKCHANGEADD.CheckedChanged
        Try
            TXTDELIVERYADDRESS.Enabled = CHKCHANGEADD.Checked
            TXTDELIVERYADDRESS.ReadOnly = Not CHKCHANGEADD.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
