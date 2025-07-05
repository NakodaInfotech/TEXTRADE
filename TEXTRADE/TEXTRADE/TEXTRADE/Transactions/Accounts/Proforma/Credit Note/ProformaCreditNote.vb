﻿
Imports BL

Public Class ProformaCreditNote

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public edit As Boolean
    Dim temprefno As String
    Dim CNREGABBR, CNREGINITIAL As String
    Dim CNREGID As Integer

    Dim TYPE As String  'USED FOR FORMTYPE WHILE RETRIVING DATA FROM GETDATA FUNCTION AND PASSING IN TO SP
    Public FRMSTRING As String  ' USER FOR BOOKTYPE 
    Public TEMPCNNO As Integer
    Public BILLNO As String
    Dim TEMPPARTYBILLNO As String
    Public TEMPREGNAME As String
    Dim GRIDCHGSDOUBLECLICK As Boolean
    Dim TEMPCHGSROW As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Try
            clear()
            edit = False
            cmbregister.Enabled = True
            cmbregister.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub clear()
        Try
            tstxtbillno.Clear()

            If ALLOWMANUALCNDN = True Then
                TXTCNNO.ReadOnly = False
                TXTCNNO.BackColor = Color.LemonChiffon
            Else
                TXTCNNO.ReadOnly = True
                TXTCNNO.BackColor = Color.Linen
            End If

            EP.Clear()

            TXTBILLNO.Enabled = True
            TXTBILLNO.Clear()

            CMBNAME.Text = ""
            CMBNAME.Enabled = True
            CMBAGENT.Text = ""
            TXTSTATECODE.Clear()
            TXTPARTYBILLNO.Clear()
            CMBDEBITLEDGER.Text = ""
            CMBDEBITLEDGER.Enabled = True
            CMBPACKING.Text = ""

            txtremarks.Clear()
            txtinwords.Clear()

            CNREGABBR = ""
            CNREGINITIAL = ""
            TXTCHARGES.Text = 0.0
            TXTTOTALTAXAMT.Clear()
            TXTTOTALOTHERCHGSAMT.Clear()
            CNDATE.Text = Now.Date

            CHKTDS.CheckState = CheckState.Unchecked
            CHKRCM.CheckState = CheckState.Unchecked
            CHKMANUAL.CheckState = CheckState.Unchecked

            TXTCHGSSRNO.Clear()
            CMBCHARGES.Text = ""
            TXTCHGSPER.Clear()
            TXTCHGSAMT.Clear()
            GRIDCHGS.RowCount = 0


            TXTAMTREC.Clear()
            TXTRETURN.Clear()
            TXTEXTRAAMT.Clear()
            TXTBAL.Clear()
            TXTTDSAMT.Clear()

            TXTTOTALSALEAMT.Text = 0.0
            TXTCGSTPER.Text = 0.0
            TXTCGSTAMT.Text = 0.0
            TXTSGSTPER.Text = 0.0
            TXTSGSTAMT.Text = 0.0
            TXTIGSTPER.Text = 0.0
            TXTIGSTAMT.Text = 0.0

            txtsubtotal.Text = 0.0
            txtroundoff.Text = 0.0
            txtgrandtotal.Text = 0.0

            lbllocked.Visible = False
            PBlock.Visible = False
            PBPAID.Visible = False
            PBTDS.Visible = False
            CMDSHOWDETAILS.Visible = False
            LBLWHATSAPP.Visible = False

            getmaxno_CN()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            txtsubtotal.Text = 0.0
            If CHKMANUALROUND.CheckState = CheckState.Unchecked Then txtroundoff.Text = 0
            txtgrandtotal.Text = 0.0
            TXTCHARGES.Text = 0.0
            TXTTOTALTAXAMT.Clear()
            TXTTOTALOTHERCHGSAMT.Clear()

            If GRIDCHGS.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDCHGS.Rows
                    If Val(row.Cells(EPER.Index).Value) Then row.Cells(EAMT.Index).Value = Format((Val(row.Cells(EPER.Index).Value * Val(TXTTOTALSALEAMT.Text.Trim)) / 100), "0.00")
                    TXTCHARGES.Text = Format(Val(TXTCHARGES.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
                    If Val(row.Cells(ETAXID.Index).Value) > 0 Then TXTTOTALTAXAMT.Text = Format(Val(TXTTOTALTAXAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00") Else TXTTOTALOTHERCHGSAMT.Text = Format(Val(TXTTOTALOTHERCHGSAMT.Text) + Val(row.Cells(EAMT.Index).Value), "0.00")
                Next
            End If
            txtsubtotal.Text = Format(Val(TXTTOTALSALEAMT.Text) + Val(TXTCHARGES.Text.Trim), "0.00")

            If CHKMANUAL.CheckState = CheckState.Unchecked Then
                TXTCGSTAMT.Text = Format((Val(txtsubtotal.Text.Trim) * Val(TXTCGSTPER.Text.Trim)) / 100, "0.00")
                TXTSGSTAMT.Text = Format((Val(txtsubtotal.Text.Trim) * Val(TXTSGSTPER.Text.Trim)) / 100, "0.00")
                TXTIGSTAMT.Text = Format((Val(txtsubtotal.Text.Trim) * Val(TXTIGSTPER.Text.Trim)) / 100, "0.00")
            End If

            If CHKMANUALROUND.Checked = False Then
                txtgrandtotal.Text = Format(Val(txtsubtotal.Text) + Val(TXTCGSTAMT.Text.Trim) + Val(TXTSGSTAMT.Text.Trim) + Val(TXTIGSTAMT.Text.Trim), "0")
                txtroundoff.Text = Format(Val(txtgrandtotal.Text) - (Val(txtsubtotal.Text) + Val(TXTCGSTAMT.Text.Trim) + Val(TXTSGSTAMT.Text.Trim) + Val(TXTIGSTAMT.Text.Trim)), "0.00")
            Else
                txtgrandtotal.Text = Format(Val(txtsubtotal.Text) + Val(TXTCGSTAMT.Text.Trim) + Val(TXTSGSTAMT.Text.Trim) + Val(TXTIGSTAMT.Text.Trim) + Val(txtroundoff.Text.Trim), "0.00")
            End If
            txtgrandtotal.Text = Format(Val(txtgrandtotal.Text), "0.00")

            If Val(txtgrandtotal.Text) > 0 Then txtinwords.Text = CurrencyToWord(txtgrandtotal.Text)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub getmaxno_CN()
        Try
            Dim DTTABLE As New DataTable
            DTTABLE = getmax(" isnull(max(CN_NO),0) + 1 ", "PROFORMACREDITNOTEMASTER INNER JOIN REGISTERMASTER ON CN_REGISTERID = REGISTER_ID ", " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND CN_YEARID = " & YearId)
            If DTTABLE.Rows.Count > 0 Then TXTCNNO.Text = DTTABLE.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CREDITNOTEdate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CNDATE.Validating
        Try
            If CNDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CNDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.Enter
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'CREDITNOTE'")
            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'CREDITNOTE' and register_YEARid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If
            getmaxno_CN()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            If TXTBILLNO.Text.Trim <> "" And TYPE = Nothing Then
                TYPE = "INVOICE"
            ElseIf TXTBILLNO.Text.Trim = "" Then
                TYPE = ""
            End If



            Dim alParaval As New ArrayList

            If ALLOWMANUALCNDN = True Then
                alParaval.Add(Val(TXTCNNO.Text.Trim))
            Else
                alParaval.Add(0)
            End If


            alParaval.Add(cmbregister.Text.Trim)
            alParaval.Add(TYPE)
            alParaval.Add(Format(Convert.ToDateTime(CNDATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(TXTBILLNO.Text.Trim)
            alParaval.Add(TXTPARTYBILLNO.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBAGENT.Text.Trim)
            alParaval.Add(TXTSACCODE.Text.Trim)
            alParaval.Add(CMBDEBITLEDGER.Text.Trim)
            alParaval.Add(CMBPACKING.Text.Trim)

            alParaval.Add(Val(TXTTOTALSALEAMT.Text.Trim))
            alParaval.Add(Val(TXTTOTALTAXAMT.Text.Trim))
            alParaval.Add(Val(TXTTOTALOTHERCHGSAMT.Text.Trim))
            alParaval.Add(Val(TXTCHARGES.Text.Trim))

            If CHKRCM.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            If CHKMANUAL.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            If CHKMANUALROUND.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)
            If CHKNOGSTR1.Checked = True Then alParaval.Add(1) Else alParaval.Add(0)

            alParaval.Add(Val(txtsubtotal.Text.Trim))

            alParaval.Add(Val(TXTCGSTPER.Text.Trim))
            alParaval.Add(Val(TXTCGSTAMT.Text.Trim))
            alParaval.Add(Val(TXTSGSTPER.Text.Trim))
            alParaval.Add(Val(TXTSGSTAMT.Text.Trim))
            alParaval.Add(Val(TXTIGSTPER.Text.Trim))
            alParaval.Add(Val(TXTIGSTAMT.Text.Trim))

            alParaval.Add(Val(txtroundoff.Text.Trim))
            alParaval.Add(Val(txtgrandtotal.Text.Trim))

            alParaval.Add(Val(TXTAMTREC.Text.Trim))
            alParaval.Add(Val(TXTEXTRAAMT.Text.Trim))
            alParaval.Add(Val(TXTRETURN.Text.Trim))
            alParaval.Add(Val(TXTBAL.Text.Trim))

            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(txtinwords.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)


            Dim CSRNO As String = ""
            Dim CCHGS As String = ""
            Dim CPER As String = ""
            Dim CAMT As String = ""
            Dim CTAXID As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCHGS.Rows
                If row.Cells(0).Value <> Nothing Then
                    If CSRNO = "" Then
                        CSRNO = row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = row.Cells(ECHARGES.Index).Value.ToString
                        CPER = row.Cells(EPER.Index).Value.ToString
                        CAMT = Val(row.Cells(EAMT.Index).Value)
                        CTAXID = Val(row.Cells(ETAXID.Index).Value)
                    Else
                        CSRNO = CSRNO & "|" & row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = CCHGS & "|" & row.Cells(ECHARGES.Index).Value.ToString
                        CPER = CPER & "|" & row.Cells(EPER.Index).Value.ToString
                        CAMT = CAMT & "|" & Val(row.Cells(EAMT.Index).Value)
                        CTAXID = CTAXID & "|" & Val(row.Cells(ETAXID.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(CSRNO)
            alParaval.Add(CCHGS)
            alParaval.Add(CPER)
            alParaval.Add(CAMT)
            alParaval.Add(CTAXID)


            Dim objclsCNmaster As New ClsProformaCreditNote()
            objclsCNmaster.alParaval = alParaval
            Dim DTTABLE As DataTable

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                DTTABLE = objclsCNmaster.SAVE()
                MessageBox.Show("Details Added")
                PRINTREPORT(DTTABLE.Rows(0).Item(0))
                TXTCNNO.Text = Val(DTTABLE.Rows(0).Item(0))

                'If CHKTDS.CheckState = CheckState.Checked Then
                '    Dim OBJTDS As New DeductTDS
                '    OBJTDS.BILLNO = Val(DTTABLE.Rows(0).Item(0))
                '    OBJTDS.REGISTER = cmbregister.Text.Trim
                '    OBJTDS.ShowDialog()
                'End If

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPCNNO)
                Dim INTRES As Integer = objclsCNmaster.UPDATE()
                MsgBox("Details Updated")
                PRINTREPORT(TEMPCNNO)
                edit = False
            End If



            If ClientName = "SAKARIA" Then SENDDIRECTMAIL()


            'SHOW NEXT BILL ON EDIT MODE DONT CLEAR
            'clear()
            Call toolnext_Click(sender, e)
            CNDATE.Focus()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SENDDIRECTMAIL()
        Try
            If MsgBox("Wish To Mail Credit Note?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim ALATTACHMENT As New ArrayList

            'CHECK WHETHER EMAILID IS PRESENT IN LEDGER OR NOT, IF NOT THEN EXIT SUB WITH MESSAGE
            Dim OBJCMN As New ClsCommon
            Dim DTEMAIL As DataTable = OBJCMN.search("ACC_EMAIL AS EMAILID", "", "LEDGERS", "AND ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If DTEMAIL.Rows.Count > 0 AndAlso DTEMAIL.Rows(0).Item("EMAILID") <> "" Then

                Dim OBJCN As New CrDrNoteDesign
                OBJCN.MdiParent = MDIMain
                OBJCN.DIRECTPRINT = True
                OBJCN.FRMSTRING = "PROFORMACREDIT"
                OBJCN.DIRECTMAIL = True
                OBJCN.REGNAME = cmbregister.Text.Trim
                OBJCN.PRINTSETTING = PRINTDIALOG
                OBJCN.BILLNO = Val(TXTCNNO.Text.Trim)
                OBJCN.NOOFCOPIES = 1
                OBJCN.Show()
                OBJCN.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\CN_" & Val(TXTCNNO.Text.Trim) & ".pdf")
                sendemail(DTEMAIL.Rows(0).Item("EMAILID"), ALATTACHMENT(0), "", "Credit Note", ALATTACHMENT, ALATTACHMENT.Count, "", "", "", "", "")
                MsgBox("Mail Sent")
            Else
                MsgBox("Add E-Mail id in Ledger")
                Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If CNDATE.Text = "__/__/____" Then
                EP.SetError(CNDATE, " Please Enter Proper Date")
                bln = False
                Return bln
                Exit Function
            Else
                If Not datecheck(CNDATE.Text) Then
                    EP.SetError(CNDATE, "Date not in Accounting Year")
                    bln = False
                End If

                If Convert.ToDateTime(CNDATE.Text).Date < CNBLOCKDATE.Date Then
                    EP.SetError(CNDATE, "Date is Blocked, Please make entries after " & Format(CNBLOCKDATE.Date, "dd/MM/yyyy"))
                    bln = False
                End If
            End If


            If Convert.ToDateTime(CNDATE.Text).Date >= "01/07/2017" Then
                If TXTSTATECODE.Text.Trim.Length = 0 Then
                    EP.SetError(TXTSTATECODE, "Please enter the state code")
                    bln = False
                End If
            End If

            If cmbregister.Text.Trim.Length = 0 Then
                EP.SetError(cmbregister, "Select Register Name")
                bln = False
            End If

            'CHEKCING IN HOTELBOOKINGS WHETHER CN IS MADE OR NOT
            'If TXTBILLNO.Text.Trim <> "" And edit = False Then
            '    Dim OBJCMN As New ClsCommon
            '    Dim DT As DataTable = OBJCMN.search("*", "", " (SELECT INVOICE_RETURN AS SALERETURN FROM INVOICEMASTER WHERE INVOICE_INITIALS = '" & TXTBILLNO.Text.Trim & "' AND INVOICE_YEARID = " & YearId & " UNION ALL SELECT BILL_RETURN FROM OPENINGBILL WHERE OPENINGBILL.BILL_INITIALS = '" & TXTBILLNO.Text.Trim & "' AND BILL_YEARID = " & YearId & ") AS T", "")
            '    If DT.Rows.Count > 0 Then
            '        If Val(DT.Rows(0).Item("SALERETURN")) > 0 Then
            '            EP.SetError(TXTBILLNO, "Credit Note Already Raised")
            '            bln = False
            '        End If
            '    End If
            'End If

            'IF INVOICENO IS NOT BLANK THEN CHECK THAT FIGURES CANNOT BE GREATER THEN BALANCEAMT
            If TXTBILLNO.Text.Trim <> "" Then
                Dim BALANCE As Double = 0
                Dim DT As New DataTable
                Dim OBJCMN As New ClsCommon
                If TYPE = "INVOICE" Then
                    DT = OBJCMN.search("ISNULL(INVOICE_BALANCE,0) AS INVBAL", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_INITIALS = '" & TXTBILLNO.Text.Trim & "' AND INVOICE_YEARID = " & YearId)
                Else
                    DT = OBJCMN.search("ISNULL(BILL_BALANCE,0) AS INVBAL", "", "OPENINGBILL INNER JOIN REGISTERMASTER ON BILL_REGISTERID = REGISTER_ID", " AND BILL_INITIALS = '" & TXTBILLNO.Text.Trim & "' AND BILL_YEARID = " & YearId)
                End If
                BALANCE = Val(DT.Rows(0).Item("INVBAL"))
                If edit = True Then
                    Dim DT1 As DataTable = OBJCMN.search("CN_GTOTAL AS RETTOTAL", "", "PROFORMACREDITNOTEMASTER", " AND CN_NO = " & Val(TEMPCNNO) & " AND CN_YEARID = " & YearId)
                    BALANCE += Val(DT1.Rows(0).Item("RETTOTAL"))
                End If
                If Val(txtgrandtotal.Text.Trim) > Val(BALANCE) Then
                    EP.SetError(txtgrandtotal, "Amount Greater then Balance Amt, only " & Val(BALANCE) & " can be Used")
                    bln = False
                End If
            End If






            If CMBNAME.Text.Trim = "" Then
                EP.SetError(CMBNAME, "Select Invoice")
                bln = False
            End If

            If TXTPARTYBILLNO.Text.Trim = "" And Convert.ToDateTime(CNDATE.Text).Date >= "01/07/2017" Then
                EP.SetError(TXTPARTYBILLNO, "Enter Party Bill No")
                bln = False
            End If


            If CMBDEBITLEDGER.Text.Trim = "" Then
                EP.SetError(CMBDEBITLEDGER, "Select Debit Ledger")
                bln = False
            End If

            If txtgrandtotal.Text.Trim = "" Then
                EP.SetError(txtgrandtotal, "Enter Amount")
                bln = False
            End If

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, "Sale Return Made , Delete Sale Return First")
                bln = False
            End If

            If Not datecheck(CNDATE.Text) Then
                EP.SetError(CNDATE, "Date Not in Current Accounting Year")
                bln = False
            End If

            If TXTPARTYBILLNO.Text.Trim.Length = 0 Then
                EP.SetError(TXTPARTYBILLNO, "Enter Party Bill No")
                bln = False
            End If

            If TXTPARTYBILLNO.Text.Trim <> "" Then
                If (edit = False) Or (edit = True And TEMPPARTYBILLNO <> TXTPARTYBILLNO.Text.Trim) Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" CN_INITIALS AS BILLNO", "", " PROFORMACREDITNOTEMASTER INNER JOIN LEDGERS ON PROFORMACREDITNOTEMASTER.CN_LEDGERID = LEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND CN_PARTYREFNO = '" & TXTPARTYBILLNO.Text.Trim & "' AND CN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        MsgBox("Party Bill No Already Exists in Entry No " & DT.Rows(0).Item("BILLNO"))
                        bln = False
                    End If
                End If
            End If



            If ALLOWMANUALCNDN = True Then
                If TXTCNNO.Text <> "" And CMBNAME.Text.Trim <> "" And edit = False Then
                    Dim OBJCMN As New ClsCommon
                    Dim dttable As DataTable = OBJCMN.search(" ISNULL(PROFORMACREDITNOTEMASTER.CN_no, 0) AS BILLNO, ISNULL(REGISTERMASTER.register_name,'') AS REGNAME", "", " REGISTERMASTER INNER JOIN PROFORMACREDITNOTEMASTER ON REGISTERMASTER.register_id = PROFORMACREDITNOTEMASTER.CN_REGISTERID AND REGISTERMASTER.register_cmpid = PROFORMACREDITNOTEMASTER.CN_CMPID AND REGISTERMASTER.register_yearid = PROFORMACREDITNOTEMASTER.CN_YEARID AND REGISTERMASTER.register_locationid = PROFORMACREDITNOTEMASTER.CN_LOCATIONID", "  AND PROFORMACREDITNOTEMASTER.CN_NO=" & TXTCNNO.Text.Trim & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND PROFORMACREDITNOTEMASTER.CN_cmpid = " & CmpId & " AND PROFORMACREDITNOTEMASTER.CN_yearid = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        EP.SetError(TXTCNNO, "Bill No Already Exist")
                        bln = False
                    End If
                End If
            End If

            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub ProformaCreditNote_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.F2 Then
                tstxtbillno.Focus()
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTPARTYBILLNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTPARTYBILLNO.Validating
        Try
            If TXTPARTYBILLNO.Text.Trim <> "" Then
                If (edit = False) Or (edit = True And TEMPPARTYBILLNO <> TXTPARTYBILLNO.Text.Trim) Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" CN_INITIALS AS BILLNO", "", " PROFORMACREDITNOTEMASTER INNER JOIN LEDGERS ON PROFORMACREDITNOTEMASTER.CN_LEDGERID = LEDGERS.Acc_id", " AND LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND CN_PARTYREFNO = '" & TXTPARTYBILLNO.Text.Trim & "' AND CN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        MsgBox("Party Bill No Already Exists in Entry No " & DT.Rows(0).Item("BILLNO"))
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 And edit = False Then
                'clear()
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'CREDITNOTE' and register_YEARid = " & YearId)
                If dt.Rows.Count > 0 Then
                    CNREGABBR = dt.Rows(0).Item(0).ToString
                    CNREGINITIAL = dt.Rows(0).Item(1).ToString
                    CNREGID = dt.Rows(0).Item(2)
                    getmaxno_CN()
                    cmbregister.Enabled = False
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            fillregister(cmbregister, " and register_type = 'CREDITNOTE' ")
            If ClientName = "AVIS" Then
                If CMBPACKING.Text.Trim = "" Then fillname(CMBPACKING, edit, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND GROUP_NAME = 'HASTE DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') AND GROUP_NAME <> 'HASTE DEBTORS'  AND ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBPACKING.Text.Trim = "" Then fillname(CMBPACKING, edit, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
                If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, "  AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
            End If
            If CMBDEBITLEDGER.Text.Trim = "" Then fillname(CMBDEBITLEDGER, edit, "")
            If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, edit, " AND (GROUPMASTER.GROUP_SECONDARY ='Indirect Income' OR GROUPMASTER.GROUP_SECONDARY ='Indirect Expenses' or GROUPMASTER.GROUP_SECONDARY ='Direct Income' OR GROUPMASTER.GROUP_SECONDARY ='Direct Expenses' OR GROUPMASTER.GROUP_SECONDARY ='Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' or GROUPMASTER.GROUP_SECONDARY = 'Sales A/C')")
            If CMBSACDESC.Text.Trim = "" Then FILLHSNITEMDESC(CMBSACDESC)
            If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPACKING.Enter
        Try
            If ClientName = "AVIS" Then
                If CMBPACKING.Text.Trim = "" Then fillname(CMBPACKING, edit, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')  AND GROUP_NAME = 'HASTE DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
            Else
                If CMBPACKING.Text.Trim = "" Then fillname(CMBPACKING, edit, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')   AND ACC_TYPE = 'ACCOUNTS'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPACKING.Validating
        Try
            If CMBPACKING.Text.Trim <> "" Then namevalidate(CMBPACKING, CMBCODE, e, Me, TXTADD, " AND  (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBACCCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(LEDGERS_1.ACC_CMPNAME,'') AS TRANSNAME,ISNULL(LEDGERS_2.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(REGISTER_NAME,'') AS REGISTERNAME, ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(LEDGERS.ACC_DISC,0) AS DISCPER, ISNULL(LEDGERS.ACC_WARNING,'') AS WARNINGTEXT ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid AND LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_1.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_1.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_1.Acc_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_2.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_2.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_2.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.Acc_cmpid = REGISTERMASTER.register_cmpid AND LEDGERS.Acc_locationid = REGISTERMASTER.register_locationid AND LEDGERS.Acc_yearid = REGISTERMASTER.register_yearid AND LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id", " and LEDGERS.acc_cmpname = '" & CMBNAME.Text.Trim & "' and (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS') and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    TXTSTATECODE.Text = DT.Rows(0).Item("STATECODE")
                    If DT.Rows(0).Item("WARNINGTEXT") <> "" Then MsgBox(DT.Rows(0).Item("WARNINGTEXT"), MsgBoxStyle.Critical)
                End If

                'GET TDSAPPLICABLE
                DT = OBJCMN.search("ISNULL(ACC_TDSPER,0) AS TDSPER ", "", " LEDGERS INNER JOIN ACCOUNTSMASTER_TDS ON LEDGERS.ACC_ID = ACCOUNTSMASTER_TDS.ACC_ID", " and LEDGERS.acc_cmpname = '" & CMBNAME.Text.Trim & "' and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    If Val(DT.Rows(0).Item("TDSPER")) > 0 Then CHKTDS.CheckState = CheckState.Checked
                End If


                CMBNAME.Enabled = False
                GETHSNCODE()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBACCCODE, e, Me, TXTOTHERCHGSADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS')", "SUNDRY DEBTORS", "ACCOUNTS", "", CMBAGENT.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDEBITLEDGER_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDEBITLEDGER.Enter
        Try
            If CMBDEBITLEDGER.Text.Trim = "" Then fillname(CMBDEBITLEDGER, edit, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDEBITLEDGER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDEBITLEDGER.Validating
        Try
            If CMBDEBITLEDGER.Text.Trim <> "" Then namevalidate(CMBDEBITLEDGER, CMBACCCODE, e, Me, TXTOTHERCHGSADD, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ProformaCreditNote_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'CREDIT NOTE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            FILLCMB()
            clear()

            If BILLNO <> "" And edit = False Then
                TXTBILLNO.Text = BILLNO
                TXTBILLNO.Enabled = False
                GETDATA()
            End If


            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim dt As New DataTable
                Dim objclsCN As New ClsProformaCreditNote()
                dt = objclsCN.selectCREDITNOTE_edit(TEMPCNNO, TEMPREGNAME, CmpId, YearId)

                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows

                        TXTCNNO.Text = TEMPCNNO
                        TXTCNNO.ReadOnly = True
                        CMBNAME.Enabled = False


                        cmbregister.Text = TEMPREGNAME
                        TYPE = dt.Rows(0).Item("TYPE")
                        TXTSTATECODE.Text = dt.Rows(0).Item("STATECODE")
                        CNDATE.Text = Format(Convert.ToDateTime(dr("CNDATE")), "dd/MM/yyyy")
                        TXTBILLNO.Text = dt.Rows(0).Item("BILLNO")
                        If TXTBILLNO.Text.Trim <> "" Then TXTBILLNO.Enabled = False
                        TXTPARTYBILLNO.Text = Convert.ToString(dr("PARTYREFNO"))
                        TEMPPARTYBILLNO = Convert.ToString(dr("PARTYREFNO"))

                        CMBNAME.Text = dt.Rows(0).Item("NAME")
                        CMBAGENT.Text = Convert.ToString(dr("AGENT"))
                        CMBSACDESC.Text = dt.Rows(0).Item("ITEMDESC")
                        TXTSACCODE.Text = dt.Rows(0).Item("HSNCODE")
                        CMBDEBITLEDGER.Text = dt.Rows(0).Item("DEBITNAME")
                        CMBPACKING.Text = dt.Rows(0).Item("DELIVERYAT")

                        TXTTOTALSALEAMT.Text = dt.Rows(0).Item("PURAMT")
                        TXTTOTALTAXAMT.Text = dt.Rows(0).Item("TOTALTAXAMT")
                        TXTTOTALOTHERCHGSAMT.Text = dt.Rows(0).Item("TOTALOTHERCHGSAMT")
                        TXTCHARGES.Text = dt.Rows(0).Item("TOTALCHARGES")

                        If Convert.ToBoolean(dr("RCM")) = False Then CHKRCM.Checked = False Else CHKRCM.Checked = True
                        If Convert.ToBoolean(dr("MANUALGST")) = False Then CHKMANUAL.Checked = False Else CHKMANUAL.Checked = True
                        If Convert.ToBoolean(dr("MANUALROUNDOFF")) = False Then CHKMANUALROUND.Checked = False Else CHKMANUALROUND.Checked = True
                        If Convert.ToBoolean(dr("NOGSTR1")) = False Then CHKNOGSTR1.Checked = False Else CHKNOGSTR1.Checked = True


                        txtsubtotal.Text = dt.Rows(0).Item("SUBTOTAL")
                        TXTCGSTPER.Text = dt.Rows(0).Item("CGSTPER")
                        TXTCGSTAMT.Text = dt.Rows(0).Item("CGSTAMT")
                        TXTSGSTPER.Text = dt.Rows(0).Item("SGSTPER")
                        TXTSGSTAMT.Text = dt.Rows(0).Item("SGSTAMT")
                        TXTIGSTPER.Text = dt.Rows(0).Item("IGSTPER")
                        TXTIGSTAMT.Text = dt.Rows(0).Item("IGSTAMT")

                        txtroundoff.Text = dt.Rows(0).Item("ROUNDOFF")
                        txtgrandtotal.Text = dt.Rows(0).Item("GTOTAL")

                        TXTAMTREC.Text = Val(dr("AMTREC"))
                        TXTEXTRAAMT.Text = Val(dr("EXTRAAMT"))
                        TXTRETURN.Text = Val(dr("CNRETURN"))
                        TXTBAL.Text = Val(dr("BALANCE"))

                        txtremarks.Text = Convert.ToString(dr("REMARKS"))
                        txtinwords.Text = Convert.ToString(dr("INWORDS"))
                        If Convert.ToBoolean(dr("SENDWHATSAPP")) = True Then LBLWHATSAPP.Visible = True


                        If Convert.ToBoolean(dr("DONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        'CHECKING WHETHER TDS IS DEDUCTED OR NOT
                        Dim OBJCMNTDS As New ClsCommon
                        Dim DTTDS As DataTable = OBJCMNTDS.search(" ISNULL(SUM(JOURNALMASTER.journal_credit),0) AS TDS", "", " JOURNALMASTER INNER JOIN PROFORMACREDITNOTEMASTER ON JOURNALMASTER.journal_refno = PROFORMACREDITNOTEMASTER.CN_INITIALS AND  JOURNALMASTER.journal_yearid = PROFORMACREDITNOTEMASTER.CN_YEARID INNER JOIN LEDGERS ON JOURNALMASTER.journal_ledgerid = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON PROFORMACREDITNOTEMASTER.CN_REGISTERID = REGISTERMASTER.register_id ", " AND (LEDGERS.ACC_TDSAC = 'True') AND CN_NO = " & TEMPCNNO & " AND REGISTER_NAME = '" & TEMPREGNAME & "' AND CN_YEARID = " & YearId)
                        If DTTDS.Rows.Count > 0 Then
                            If Val(DTTDS.Rows(0).Item("TDS")) > 0 Then
                                TXTTDSAMT.Text = Format(Val(DTTDS.Rows(0).Item("TDS")), "0.00")
                                CMDSHOWDETAILS.Visible = True
                                PBTDS.Visible = True
                                lbllocked.Visible = True
                                PBlock.Visible = True
                            End If
                        End If

                        If PBTDS.Visible = False Then
                            If Val(dr("AMTREC")) > 0 Or Val(dr("EXTRAAMT")) > 0 Then
                                CMDSHOWDETAILS.Visible = True
                                PBPAID.Visible = True
                                lbllocked.Visible = True
                                PBlock.Visible = True
                            End If
                        End If

                    Next

                    'CHARGES GRID
                    Dim OBJCM2 As New ClsCommon
                    Dim dt2 As DataTable = OBJCM2.search("  PROFORMACREDITNOTEMASTER_CHGS.CN_gridsrno AS GRIDSRNO, ISNULL(LEDGERS.Acc_cmpname, '') AS CHARGES, ISNULL(PROFORMACREDITNOTEMASTER_CHGS.CN_PER, 0) AS PER, ISNULL(PROFORMACREDITNOTEMASTER_CHGS.CN_AMT, 0) AS AMT, ISNULL(TAXMASTER.tax_id, 0) AS TAXID", "", "  PROFORMACREDITNOTEMASTER INNER JOIN PROFORMACREDITNOTEMASTER_CHGS ON PROFORMACREDITNOTEMASTER.CN_no = PROFORMACREDITNOTEMASTER_CHGS.CN_no AND PROFORMACREDITNOTEMASTER.CN_registerid = PROFORMACREDITNOTEMASTER_CHGS.CN_REGISTERID AND PROFORMACREDITNOTEMASTER.CN_yearid = PROFORMACREDITNOTEMASTER_CHGS.CN_yearid INNER JOIN REGISTERMASTER ON PROFORMACREDITNOTEMASTER.CN_registerid = REGISTERMASTER.register_id AND PROFORMACREDITNOTEMASTER.CN_yearid = REGISTERMASTER.register_yearid LEFT OUTER JOIN TAXMASTER ON PROFORMACREDITNOTEMASTER_CHGS.CN_TAXID = TAXMASTER.tax_id AND PROFORMACREDITNOTEMASTER_CHGS.CN_yearid = TAXMASTER.tax_yearid LEFT OUTER JOIN LEDGERS ON PROFORMACREDITNOTEMASTER_CHGS.CN_CHARGESID = LEDGERS.Acc_id AND PROFORMACREDITNOTEMASTER_CHGS.CN_yearid = LEDGERS.Acc_yearid", " AND REGISTERMASTER.REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND REGISTER_TYPE = 'CREDITNOTE' AND PROFORMACREDITNOTEMASTER_CHGS.CN_NO = " & TEMPCNNO & " AND PROFORMACREDITNOTEMASTER_CHGS.CN_YEARID = " & YearId)
                    If dt2.Rows.Count > 0 Then
                        For Each DTR As DataRow In dt2.Rows
                            GRIDCHGS.Rows.Add(DTR("GRIDSRNO"), DTR("CHARGES"), DTR("PER"), DTR("AMT"), DTR("TAXID"))
                        Next
                    End If


                    dt2 = OBJCM2.search(" register_abbr, register_initials, register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'CREDITNOTE' and register_YEARid = " & YearId)
                    If dt2.Rows.Count > 0 Then
                        CNREGABBR = dt2.Rows(0).Item(0).ToString
                        CNREGINITIAL = dt2.Rows(0).Item(1).ToString
                        CNREGID = dt2.Rows(0).Item(2)
                    End If

                    TOTAL()
                    cmbregister.Enabled = False
                    TXTTOTALSALEAMT.Focus()
                Else
                    edit = False
                    clear()
                    TXTCNNO.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating

    End Sub

    Private Sub TXTBILLNO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTBILLNO.Validating
        Try
            'If TXTBILLNO.Text.Trim <> "" And edit = False Then
            If TXTBILLNO.Text.Trim <> "" Then
                GETDATA()
                If Val(txtgrandtotal.Text.Trim) = 0 Then
                    MsgBox("Invalid Voucher No")
                    e.Cancel = True
                    Exit Sub
                End If
                TXTBILLNO.Enabled = False
            Else
                If edit = True Then TXTBILLNO.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETDATA()
        Try
            Dim OBJCMN As New ClsCommon
            'Dim DT As DataTable = OBJCMN.search("  INVOICEMASTER.INVOICE_AMTREC AS RECDAMT, INVOICEMASTER.INVOICE_NO AS BILL, INVOICEMASTER.INVOICE_INITIALS AS BILLNO, INVOICEMASTER.INVOICE_CMPID, INVOICEMASTER.INVOICE_LOCATIONID, INVOICEMASTER.INVOICE_YEARID, LEDGERS.Acc_cmpname AS NAME, INVOICEMASTER.INVOICE_AMOUNT AS SALEAMT,ISNULL(INVOICEMASTER.INVOICE_TOTALTAXAMT, 0) AS TOTALTAXAMT,ISNULL(INVOICEMASTER.INVOICE_TOTALCHGSAMT, 0) AS TOTALCHGSAMT,  ISNULL(INVOICEMASTER.INVOICE_CHARGES, 0) AS TOTALCHARGES, INVOICEMASTER.INVOICE_ROUNDOFF AS ROUNDOFF, INVOICEMASTER.INVOICE_GRANDTOTAL AS GTOTAL, 'INVOICE' AS TYPE, ISNULL(INVOICEMASTER.INVOICE_ORDERNO,'') AS ORDERNO ", "", "  INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id AND INVOICEMASTER.INVOICE_YEARID = LEDGERS.Acc_yearid ", " AND INVOICEMASTER.INVOICE_INITIALS = '" & TXTBILLNO.Text.Trim & "' AND INVOICEMASTER.INVOICE_CMPID = " & CmpId & " AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
            'THIS QUERY RETRIEVES DATA FROM INVOICEMASTER ONLY, NEW QUERY WILL RETRIEVE DATA DATA FROM OPENING ALSO
            'Dim DT As DataTable = OBJCMN.search("  INVOICEMASTER.INVOICE_AMTREC AS RECDAMT, INVOICEMASTER.INVOICE_NO AS BILL, INVOICEMASTER.INVOICE_INITIALS AS BILLNO, INVOICEMASTER.INVOICE_CMPID, INVOICEMASTER.INVOICE_LOCATIONID, INVOICEMASTER.INVOICE_YEARID, LEDGERS.Acc_cmpname AS NAME, INVOICEMASTER.INVOICE_AMOUNT AS SALEAMT, 0 AS TOTALTAXAMT, 0 AS TOTALCHGSAMT,  ISNULL(INVOICEMASTER.INVOICE_CHARGES, 0) AS TOTALCHARGES, INVOICEMASTER.INVOICE_ROUNDOFF AS ROUNDOFF, INVOICEMASTER.INVOICE_GRANDTOTAL AS GTOTAL, 'INVOICE' AS TYPE ", "", "  INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id AND INVOICEMASTER.INVOICE_YEARID = LEDGERS.Acc_yearid ", " AND INVOICEMASTER.INVOICE_INITIALS = '" & TXTBILLNO.Text.Trim & "' AND INVOICEMASTER.INVOICE_CMPID = " & CmpId & " AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
            'Dim DT As DataTable = OBJCMN.search("*", "", " (SELECT INVOICEMASTER.INVOICE_AMTREC AS RECDAMT, INVOICEMASTER.INVOICE_NO AS BILL, INVOICEMASTER.INVOICE_INITIALS AS BILLNO, INVOICEMASTER.INVOICE_CMPID, INVOICEMASTER.INVOICE_LOCATIONID, INVOICEMASTER.INVOICE_YEARID, LEDGERS.Acc_cmpname AS NAME, INVOICEMASTER.INVOICE_AMOUNT AS SALEAMT, 0 AS TOTALTAXAMT, 0 AS TOTALCHGSAMT,  ISNULL(INVOICEMASTER.INVOICE_CHARGES, 0) AS TOTALCHARGES, INVOICEMASTER.INVOICE_ROUNDOFF AS ROUNDOFF, INVOICEMASTER.INVOICE_GRANDTOTAL AS GTOTAL, 'INVOICE' AS TYPE, REGLEDGERS.ACC_CMPNAME AS DEBITLEDGER FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN LEDGERS AS REGLEDGERS ON REGISTERMASTER.register_abbr = REGLEDGERS.Acc_cmpname AND REGISTERMASTER.register_yearid = REGLEDGERS.Acc_yearid WHERE INVOICEMASTER.INVOICE_INITIALS = '" & TXTBILLNO.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " UNION ALL SELECT OPENINGBILL.BILL_AMTPAIDREC AS RECDAMT, OPENINGBILL.BILL_NO AS BILL, OPENINGBILL.BILL_INITIALS AS BILLNO, OPENINGBILL.BILL_CMPID, OPENINGBILL.BILL_LOCATIONID, OPENINGBILL.BILL_YEARID, LEDGERS.Acc_cmpname AS NAME, OPENINGBILL.BILL_AMT AS SALEAMT, 0 AS TOTALTAXAMT, 0 AS TOTALCHGSAMT,  0 AS TOTALCHARGES, 0 AS ROUNDOFF, OPENINGBILL.BILL_AMT AS GTOTAL, 'OPENING' AS TYPE, REGLEDGERS.ACC_CMPNAME AS DEBITLEDGER FROM OPENINGBILL INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON OPENINGBILL.BILL_REGISTERID = REGISTERMASTER.register_id INNER JOIN LEDGERS AS REGLEDGERS ON REGISTERMASTER.register_abbr = REGLEDGERS.Acc_cmpname AND REGISTERMASTER.register_yearid = REGLEDGERS.Acc_yearid WHERE OPENINGBILL.BILL_INITIALS = '" & TXTBILLNO.Text.Trim & "' AND OPENINGBILL.BILL_YEARID = " & YearId & ") AS T", "")
            Dim DT As DataTable = OBJCMN.search("*", "", " (SELECT INVOICEMASTER.INVOICE_AMTREC AS RECDAMT, INVOICEMASTER.INVOICE_NO AS BILL, INVOICEMASTER.INVOICE_DATE AS DATE, INVOICEMASTER.INVOICE_INITIALS AS BILLNO, INVOICEMASTER.INVOICE_CMPID, INVOICEMASTER.INVOICE_LOCATIONID, INVOICEMASTER.INVOICE_YEARID, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, INVOICEMASTER.INVOICE_AMOUNT AS SALEAMT, 0 AS TOTALTAXAMT, 0 AS TOTALCHGSAMT, ISNULL(INVOICEMASTER.INVOICE_CHARGES, 0) AS TOTALCHARGES, INVOICEMASTER.INVOICE_ROUNDOFF AS ROUNDOFF, INVOICEMASTER.INVOICE_GRANDTOTAL AS GTOTAL, 'INVOICE' AS TYPE, REGLEDGERS.Acc_cmpname AS DEBITLEDGER, ISNULL(INVOICEMASTER.INVOICE_CGSTPER, 0) AS CGSTPER, ISNULL(INVOICEMASTER.INVOICE_TOTALCGSTAMT, 0) AS CGSTAMT, ISNULL(INVOICEMASTER.INVOICE_SGSTPER, 0) AS SGSTPER, ISNULL(INVOICEMASTER.INVOICE_TOTALSGSTAMT, 0) AS SGSTAMT, ISNULL(INVOICEMASTER.INVOICE_IGSTPER, 0) AS IGSTPER, ISNULL(INVOICEMASTER.INVOICE_TOTALIGSTAMT, 0) AS IGSTAMT, ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR), '') AS STATECODE, ISNULL(DELIVERYLEDGERS.ACC_CMPNAME,'') AS DELIVERYAT FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DELIVERYLEDGERS ON INVOICEMASTER.INVOICE_PACKINGID = DELIVERYLEDGERS.Acc_id  INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id INNER JOIN LEDGERS AS REGLEDGERS ON REGISTERMASTER.register_abbr = REGLEDGERS.Acc_cmpname AND REGISTERMASTER.register_yearid = REGLEDGERS.Acc_yearid LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id  LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.ACC_ID WHERE INVOICEMASTER.INVOICE_INITIALS = '" & TXTBILLNO.Text.Trim & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " UNION ALL SELECT OPENINGBILL.BILL_AMTPAIDREC AS RECDAMT, OPENINGBILL.BILL_NO AS BILL, OPENINGBILL.BILL_DATE AS DATE, OPENINGBILL.BILL_INITIALS AS BILLNO, OPENINGBILL.BILL_CMPID, OPENINGBILL.BILL_LOCATIONID, OPENINGBILL.BILL_YEARID, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, OPENINGBILL.BILL_AMT AS SALEAMT, 0 AS TOTALTAXAMT, 0 AS TOTALCHGSAMT, 0 AS TOTALCHARGES, 0 AS ROUNDOFF, OPENINGBILL.BILL_AMT AS GTOTAL, 'OPENING' AS TYPE, REGLEDGERS.Acc_cmpname AS DEBITLEDGER, 0 AS CGSTPER, 0 AS CGSTAMT, 0 AS SGSTPER, 0 AS SGSTAMT, 0 AS IGSTPER, 0 AS IGSTAMT, ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR), '') AS STATECODE, '' AS DELIVERYAT FROM  OPENINGBILL INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON OPENINGBILL.BILL_REGISTERID = REGISTERMASTER.register_id INNER JOIN LEDGERS AS REGLEDGERS ON REGISTERMASTER.register_abbr = REGLEDGERS.Acc_cmpname AND REGISTERMASTER.register_yearid = REGLEDGERS.Acc_yearid LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id  LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON OPENINGBILL.BILL_AGENTID = AGENTLEDGERS.ACC_ID WHERE OPENINGBILL.BILL_INITIALS = '" & TXTBILLNO.Text.Trim & "' AND OPENINGBILL.BILL_YEARID = " & YearId & ") AS T", "")
            If DT.Rows.Count > 0 Then

                For Each dr As DataRow In DT.Rows

                    'If Convert.ToDateTime(DT.Rows(0).Item("DATE")).Date >= "01/07/2017" Then
                    '    MsgBox("Credit Note Cannot be Raised for This Invoice, Create Sale Return", MsgBoxStyle.Critical)
                    '    Exit Sub
                    'End If

                    'If DT.Rows(0).Item("RECDAMT") > 0 Then
                    '    MsgBox("Receipt Made, Delete Receipt First", MsgBoxStyle.Critical)
                    '    Exit Sub
                    'End If

                    BILLNO = DT.Rows(0).Item("BILL")
                    TYPE = DT.Rows(0).Item("TYPE")
                    TXTSTATECODE.Text = DT.Rows(0).Item("STATECODE")


                    'GET TOP HSN FROM INVOICEMASTER_DESC
                    If TYPE = "INVOICE" Then
                        Dim DTHSN As DataTable = OBJCMN.search(" TOP 1 ISNULL(HSNMASTER.HSN_ITEMDESC, '') AS HSNITEMDESC, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE ", "", " INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID LEFT OUTER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID ", " AND INVOICEMASTER.INVOICE_INITIALS = '" & TXTBILLNO.Text.Trim & "' AND INVOICEMASTER_DESC.INVOICE_YEARID = " & YearId)
                        If DTHSN.Rows.Count > 0 Then
                            CMBSACDESC.Text = DTHSN.Rows(0).Item("HSNITEMDESC")
                            TXTSACCODE.Text = DTHSN.Rows(0).Item("HSNCODE")
                            GETHSNCODE()
                        End If
                    End If

                    CMBNAME.Text = DT.Rows(0).Item("NAME")
                    If DT.Rows(0).Item("AGENTNAME") <> "" Then CMBAGENT.Text = DT.Rows(0).Item("AGENTNAME")
                    CMBDEBITLEDGER.Text = DT.Rows(0).Item("DEBITLEDGER")
                    CMBPACKING.Text = DT.Rows(0).Item("DELIVERYAT")

                    TXTTOTALSALEAMT.Text = DT.Rows(0).Item("SALEAMT")
                    TXTTOTALTAXAMT.Text = DT.Rows(0).Item("TOTALTAXAMT")
                    TXTTOTALOTHERCHGSAMT.Text = DT.Rows(0).Item("TOTALCHGSAMT")
                    TXTCHARGES.Text = DT.Rows(0).Item("TOTALCHARGES")
                    TXTCGSTPER.Text = DT.Rows(0).Item("CGSTPER")
                    TXTCGSTAMT.Text = DT.Rows(0).Item("CGSTAMT")
                    TXTSGSTPER.Text = DT.Rows(0).Item("SGSTPER")
                    TXTSGSTAMT.Text = DT.Rows(0).Item("SGSTAMT")
                    TXTIGSTPER.Text = DT.Rows(0).Item("IGSTPER")
                    TXTIGSTAMT.Text = DT.Rows(0).Item("IGSTAMT")
                    txtroundoff.Text = DT.Rows(0).Item("ROUNDOFF")
                    txtgrandtotal.Text = DT.Rows(0).Item("GTOTAL")
                    If txtremarks.Text = "" Then txtremarks.Text = DT.Rows(0).Item("BILLNO")

                Next

                CMBNAME.Enabled = False

            End If


            'CHARGES GRID
            'Dim OBJCMN As New ClsCommon
            If TYPE = "INVOICE" And ClientName <> "SOFTAS" Then
                Dim dttable As DataTable = OBJCMN.search(" ISNULL(INVOICEMASTER_CHGS.INVOICE_GRIDSRNO,0) AS GRIDSRNO, ISNULL(LEDGERS.Acc_CMPname, '') AS CHARGES, ISNULL(INVOICEMASTER_CHGS.INVOICE_PER, 0) AS PER, ISNULL(INVOICEMASTER_CHGS.INVOICE_AMT, 0) AS AMT, ISNULL(TAXMASTER.TAX_ID, 0) AS TAXID ", "", " LEDGERS RIGHT OUTER JOIN INVOICEMASTER_CHGS LEFT OUTER JOIN TAXMASTER ON INVOICEMASTER_CHGS.INVOICE_yearid = TAXMASTER.tax_yearid AND INVOICEMASTER_CHGS.INVOICE_locationid = TAXMASTER.tax_locationid AND INVOICEMASTER_CHGS.INVOICE_cmpid = TAXMASTER.tax_cmpid AND INVOICEMASTER_CHGS.INVOICE_TAXID = TAXMASTER.tax_id ON LEDGERS.Acc_yearid = INVOICEMASTER_CHGS.INVOICE_yearid AND LEDGERS.Acc_locationid = INVOICEMASTER_CHGS.INVOICE_locationid AND LEDGERS.Acc_cmpid = INVOICEMASTER_CHGS.INVOICE_cmpid AND LEDGERS.Acc_id = INVOICEMASTER_CHGS.INVOICE_CHARGESID RIGHT OUTER JOIN REGISTERMASTER INNER JOIN INVOICEMASTER ON REGISTERMASTER.register_id = INVOICEMASTER.INVOICE_REGISTERID AND REGISTERMASTER.register_cmpid = INVOICEMASTER.INVOICE_CMPID AND REGISTERMASTER.register_locationid = INVOICEMASTER.INVOICE_LOCATIONID AND REGISTERMASTER.register_yearid = INVOICEMASTER.INVOICE_YEARID ON INVOICEMASTER_CHGS.INVOICE_no = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_CHGS.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_CHGS.INVOICE_cmpid = INVOICEMASTER.INVOICE_CMPID AND INVOICEMASTER_CHGS.INVOICE_locationid = INVOICEMASTER.INVOICE_LOCATIONID AND INVOICEMASTER_CHGS.INVOICE_yearid = INVOICEMASTER.INVOICE_YEARID", " AND INVOICEMASTER.INVOICE_INITIALS = '" & TXTBILLNO.Text.Trim & "'  AND INVOICEMASTER.INVOICE_CMPID = " & CmpId & " AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        GRIDCHGS.Rows.Add(DTR("GRIDSRNO"), DTR("CHARGES"), DTR("PER"), DTR("AMT"), DTR("TAXID"))
                    Next
                End If
            End If

            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub TXTTOTALSALEAMT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTTOTALSALEAMT.Validated
        TOTAL()
    End Sub

    Sub PRINTREPORT(ByVal BILLNO As Integer)
        Try
            Dim TEMPMSG As Integer = MsgBox("Wish to Print Credit Note?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                Dim OBJCN As New CrDrNoteDesign
                OBJCN.BILLNO = BILLNO
                OBJCN.REGNAME = cmbregister.Text.Trim
                OBJCN.MdiParent = MDIMain
                OBJCN.PARTYNAME = CMBNAME.Text.Trim
                OBJCN.AGENTNAME = CMBAGENT.Text.Trim
                OBJCN.FRMSTRING = "PROFORMACREDIT"
                OBJCN.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSHOWDETAILS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSHOWDETAILS.Click
        Try
            Dim OBJRECPAY As New ShowRecPay
            OBJRECPAY.MdiParent = MDIMain

            OBJRECPAY.PURBILLINITIALS = CNREGINITIAL & "-" & TEMPCNNO
            OBJRECPAY.SALEBILLINITIALS = CNREGINITIAL & "-" & TEMPCNNO
            OBJRECPAY.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If edit = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Then
                    MsgBox("Sale Return Made , Delete Sale Return First")
                    Exit Sub
                End If

                If Convert.ToDateTime(CNDATE.Text).Date < CNBLOCKDATE.Date Then
                    MsgBox("Date is Blocked, Please Delete entries after " & Format(CNBLOCKDATE.Date, "dd/MM/yyyy"), MsgBoxStyle.Critical)
                    Exit Sub
                End If

                Dim tempmsg As Integer = MsgBox("Delete Credit Note Permanently?", MsgBoxStyle.YesNo, "TEXPRO")
                If tempmsg = vbYes Then

                    Dim OBJCN As New ClsProformaCreditNote
                    Dim ALPARAVAL As New ArrayList

                    ALPARAVAL.Add(TYPE)
                    ALPARAVAL.Add(TEMPCNNO)
                    ALPARAVAL.Add(TEMPREGNAME)
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(YearId)

                    OBJCN.alParaval = ALPARAVAL
                    Dim DT As DataTable = OBJCN.Delete()
                    MsgBox(DT.Rows(0).Item(0).ToString)

                    edit = False
                    clear()

                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHGS_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCHGS.CellDoubleClick
        EDITCHGSROW()
    End Sub

    Private Sub GRIDCHGS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHGS.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCHGS.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbMERCHANT.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDCHGSDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDCHGS.Rows.RemoveAt(GRIDCHGS.CurrentRow.Index)
                TOTAL()
                getsrno(GRIDCHGS)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITCHGSROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub EDITCHGSROW()
        Try
            TXTCHGSPER.ReadOnly = False
            TXTCHGSAMT.ReadOnly = False
            If GRIDCHGS.CurrentRow.Index >= 0 And GRIDCHGS.Item(ESRNO.Index, GRIDCHGS.CurrentRow.Index).Value <> Nothing Then
                GRIDCHGSDOUBLECLICK = True
                TXTCHGSSRNO.Text = GRIDCHGS.Item(ESRNO.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                CMBCHARGES.Text = GRIDCHGS.Item(ECHARGES.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSPER.Text = GRIDCHGS.Item(EPER.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSAMT.Text = GRIDCHGS.Item(EAMT.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTTAXID.Text = GRIDCHGS.Item(ETAXID.Index, GRIDCHGS.CurrentRow.Index).Value.ToString

                If Val(TXTCHGSPER.Text.Trim) > 0 Then TXTCHGSAMT.ReadOnly = True

                TEMPCHGSROW = GRIDCHGS.CurrentRow.Index
                CMBCHARGES.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHARGES_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHARGES.Validating
        TOTAL()
    End Sub

    Private Sub CMBCHARGES_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Enter
        Try
            If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, edit, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' or GROUPMASTER.GROUP_SECONDARY = 'Sales A/C')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCHARGES.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' or GROUPMASTER.GROUP_SECONDARY = 'Sales A/C')"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBCHARGES.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AMOUNTNUMDOTKYEPRESS(ByVal han As KeyPressEventArgs, ByVal sen As Control, ByVal frm As System.Windows.Forms.Form)
        Try
            Dim mypos As Integer

            If AscW(han.KeyChar) >= 48 And AscW(han.KeyChar) <= 57 Or AscW(han.KeyChar) = 8 Or AscW(han.KeyChar) = 45 Then
                han.KeyChar = han.KeyChar
            ElseIf AscW(han.KeyChar) = 46 Or AscW(han.KeyChar) = 45 Then
                mypos = InStr(1, sen.Text, ".")
                If mypos = 0 Then
                    han.KeyChar = han.KeyChar
                Else
                    han.KeyChar = ""
                End If
            Else
                han.KeyChar = ""
            End If

            If AscW(han.KeyChar) = Keys.Escape Then
                frm.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub filltax()
        Try
            TXTCHGSPER.ReadOnly = False
            TXTCHGSAMT.ReadOnly = False
            TXTTAXID.Text = 0
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable = objclscommon.search(" ISNULL(tax_tax, 0) as TAX, TAX_ID AS TAXID ", "", " TAXMASTER", " AND tax_name = '" & CMBCHARGES.Text & "'  AND tax_cmpid=" & CmpId & " AND tax_LOCATIONID = " & Locationid & " AND tax_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                TXTCHGSPER.Text = dt.Rows(0).Item("TAX")
                TXTTAXID.Text = Val(dt.Rows(0).Item("TAXID"))
                If Val(TXTCHGSPER.Text.Trim) > 0 Then TXTCHGSAMT.ReadOnly = True
                TXTCHGSPER.ReadOnly = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Validated
        Try
            If CMBCHARGES.Text.Trim <> "" Then
                filltax()

                'GET ADDLESS DONE BY GULKIT
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(LEDGERS.ACC_ADDLESS,'ADD') AS ADDLESS ", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBCHARGES.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If UCase(DT.Rows(0).Item("ADDLESS")) = "LESS" Then
                        If Val(TXTCHGSPER.Text.Trim) = 0 Then TXTCHGSPER.Text = "-"
                        If Val(TXTCHGSAMT.Text.Trim) = 0 Then TXTCHGSAMT.Text = "-"
                        TXTCHGSPER.Select(TXTCHGSPER.Text.Length, 0)
                    End If
                End If
            Else
                TXTCHGSPER.Clear()
                TXTCHGSAMT.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCHARGES.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" Then namevalidate(CMBCHARGES, CMBACCCODE, e, Me, TXTOTHERCHGSADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' or GROUPMASTER.GROUP_SECONDARY = 'Purchase A/C' or GROUPMASTER.GROUP_SECONDARY = 'Sales A/C' )")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSAMT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtroundoff.KeyPress, TXTCHGSPER.KeyPress, TXTCHGSAMT.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKVALIDATE() As Boolean
        Try
            Dim BLN As Boolean = True
            For Each ROW As DataGridViewRow In GRIDCHGS.Rows
                If GRIDCHGSDOUBLECLICK = False Or (GRIDCHGSDOUBLECLICK = True And TEMPCHGSROW <> ROW.Index) Then
                    If CMBCHARGES.Text.Trim = ROW.Cells(ECHARGES.Index).Value Then
                        BLN = False
                    End If
                End If
            Next
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub TXTCHGSAMT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHGSAMT.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" And Val(TXTCHGSAMT.Text.Trim) <> 0 Then
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(TXTCHGSAMT.Text.Trim, dDebit)
                If bValid Then
                    TXTCHGSAMT.Text = Convert.ToDecimal(Val(TXTCHGSAMT.Text))
                    ' everything is good
                    'CHECK WHETHER IT IS ALREADY PRESENT IN GRID OR NOT
                    If Not CHECKVALIDATE() Then
                        MsgBox("Charges Already Present Below", MsgBoxStyle.Critical)
                        CMBCHARGES.Focus()
                        Exit Sub
                    End If
                    fillchgsgrid()
                    TOTAL()
                Else
                    MessageBox.Show("Invalid Number Entered")
                    'e.Cancel = True
                    TXTCHGSAMT.Clear()
                    TXTCHGSAMT.Focus()
                    Exit Sub
                End If
            Else
                If CMBCHARGES.Text.Trim = "" Then
                    MsgBox("Please Fill Charges Name ")
                    CMBCHARGES.Focus()
                    Exit Sub
                ElseIf Val(TXTCHGSPER.Text.Trim) = 0 And Val(TXTCHGSAMT.Text.Trim) = 0 Then
                    MsgBox("Amount can not be zero")
                    TXTCHGSAMT.Clear()
                    TXTCHGSAMT.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillchgsgrid()
        Try
            If GRIDCHGSDOUBLECLICK = False Then
                GRIDCHGS.Rows.Add(Val(TXTCHGSSRNO.Text.Trim), CMBCHARGES.Text.Trim, Val(TXTCHGSPER.Text.Trim), Val(TXTCHGSAMT.Text.Trim), Val(TXTTAXID.Text.Trim))
                getsrno(GRIDCHGS)
            ElseIf GRIDCHGSDOUBLECLICK = True Then
                GRIDCHGS.Item(ESRNO.Index, TEMPCHGSROW).Value = Val(TXTCHGSSRNO.Text.Trim)
                GRIDCHGS.Item(ECHARGES.Index, TEMPCHGSROW).Value = CMBCHARGES.Text.Trim
                GRIDCHGS.Item(EPER.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSPER.Text.Trim), "0.00")
                GRIDCHGS.Item(EAMT.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSAMT.Text.Trim), "0.00")
                GRIDCHGS.Item(ETAXID.Index, TEMPCHGSROW).Value = Format(Val(TXTTAXID.Text.Trim))

                GRIDCHGSDOUBLECLICK = False

            End If
            TOTAL()

            GRIDCHGS.FirstDisplayedScrollingRowIndex = GRIDCHGS.RowCount - 1

            TXTCHGSSRNO.Text = GRIDCHGS.RowCount + 1
            CMBCHARGES.Text = ""
            TXTCHGSPER.Clear()
            TXTCHGSAMT.Clear()
            If TXTCHGSPER.ReadOnly = True Then TXTCHGSPER.ReadOnly = False
            CMBCHARGES.Focus()
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

    Private Sub ProformaCreditNote_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            If ClientName = "SVS" Then Me.Close()
            TXTCNNO.TabStop = ALLOWMANUALINVNO
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Try
            Call cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim objCNdtls As New ProformaCreditNoteDetails
            objCNdtls.MdiParent = MDIMain
            objCNdtls.Show()
            objCNdtls.BringToFront()
            'Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
LINE1:
            TEMPCNNO = Val(TXTCNNO.Text) + 1
            TEMPREGNAME = cmbregister.Text.Trim
            getmaxno_CN()
            Dim MAXNO As Integer = TXTCNNO.Text.Trim
            clear()
            If Val(TXTCNNO.Text) - 1 >= TEMPCNNO Then
                edit = True
                ProformaCreditNote_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            'If txtgrandtotal.Text > 0 And TEMPCNNO < MAXNO Then
            '    TXTCNNO.Text = TEMPCNNO
            '    ' GoTo LINE1
            'End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
LINE1:
            TEMPCNNO = Val(TXTCNNO.Text) - 1
            TEMPREGNAME = cmbregister.Text.Trim
            If TEMPCNNO > 0 Then
                edit = True
                ProformaCreditNote_Load(sender, e)
            Else
                clear()
                edit = False
            End If

            'If txtgrandtotal.Text > 0 And TEMPCNNO > 1 Then
            '    TXTCNNO.Text = TEMPCNNO
            '    ' GoTo LINE1
            'End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If edit = True Then PRINTREPORT(TEMPCNNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSPER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTCHGSPER.Validated
        CALC()
    End Sub

    Sub CALC()
        Try
            If CMBCHARGES.Text.Trim <> "" And Val(TXTCHGSPER.Text.Trim) <> 0 Then
                TXTCHGSAMT.Text = ((Val(TXTTOTALSALEAMT.Text.Trim) * Val(TXTCHGSPER.Text.Trim)) / 100)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLHSNITEMDESC(ByRef CMBHSNITEMDESC As ComboBox)
        Try
            Dim objclscommon As New ClsCommon
            Dim dt As DataTable

            dt = objclscommon.search(" ISNULL(HSN_ITEMDESC, '') AS HSNITEMDESC ", "", " HSNMASTER ", " AND HSN_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "HSNITEMDESC"
                CMBHSNITEMDESC.DataSource = dt
                CMBHSNITEMDESC.DisplayMember = "HSNITEMDESC"
            End If
            CMBSACDESC.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBHSNITEMDESC_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSACDESC.Enter
        Try
            If CMBSACDESC.Text.Trim = "" Then FILLHSNITEMDESC(CMBSACDESC)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETHSNCODE()
        Try
            TXTSACCODE.Clear()
            TXTCGSTPER.Clear()
            TXTCGSTAMT.Clear()
            TXTSGSTPER.Clear()
            TXTSGSTAMT.Clear()
            TXTIGSTPER.Clear()
            TXTIGSTAMT.Clear()

            If CMBSACDESC.Text.Trim <> "" And Convert.ToDateTime(CNDATE.Text).Date >= "01/07/2017" Then
                Dim OBJCMN As New ClsCommon
                'Dim DT As DataTable = OBJCMN.search("  ISNULL(HSN_CODE, '') AS HSNCODE, ISNULL(HSN_CGST, 0) AS CGSTPER, ISNULL(HSN_SGST, 0) AS SGSTPER, ISNULL(HSN_IGST, 0) AS IGSTPER", "", " HSNMASTER ", " AND HSNMASTER.HSN_ITEMDESC = '" & CMBSACDESC.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
                Dim DT As DataTable = OBJCMN.search(" TOP 1 ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER_DESC.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER_DESC.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER_DESC.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER_DESC.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER_DESC.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN HSNMASTER_DESC ON HSNMASTER.HSN_ID = HSNMASTER_DESC.HSN_ID ", " AND HSNMASTER_DESC.HSN_WEFDATE <= '" & Format(Convert.ToDateTime(CNDATE.Text).Date, "MM/dd/yyyy") & "' AND HSNMASTER.HSN_ITEMDESC = '" & CMBSACDESC.Text.Trim & "' AND HSNMASTER.HSN_YEARID=" & YearId & " ORDER BY HSNMASTER_DESC.HSN_WEFDATE DESC")
                If DT.Rows.Count > 0 Then
                    TXTSACCODE.Text = DT.Rows(0).Item("HSNCODE")
                    If CMBNAME.Text.Trim <> "" Then
                        If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                            TXTIGSTPER.Text = 0
                            TXTCGSTPER.Text = Val(DT.Rows(0).Item("CGSTPER"))
                            TXTSGSTPER.Text = Val(DT.Rows(0).Item("SGSTPER"))
                        Else
                            TXTCGSTPER.Text = 0
                            TXTSGSTPER.Text = 0
                            TXTIGSTPER.Text = Val(DT.Rows(0).Item("IGSTPER"))
                        End If
                    End If
                End If
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub CMBHSNITEMDESC_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSACDESC.Validated
        Try
            GETHSNCODE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDVIEW_Click(sender As Object, e As EventArgs) Handles CMDVIEW.Click
        Try
            If CMBNAME.Text.Trim <> "" Then

                Dim OBJSELECTBILL As New SelectBills
                OBJSELECTBILL.CMPNAME = CMBNAME.Text.Trim
                OBJSELECTBILL.ShowDialog()
                If OBJSELECTBILL.BILLNO <> "" Then
                    TXTBILLNO.Text = OBJSELECTBILL.BILLNO
                    TXTBILLNO.Focus()
                End If

            Else
                MsgBox("Select Name")
                CMBNAME.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKMANUALROUND_CheckedChanged(sender As Object, e As EventArgs) Handles CHKMANUALROUND.CheckedChanged
        Try
            If CHKMANUALROUND.Checked = True Then
                txtroundoff.ReadOnly = False
                txtroundoff.TabStop = True
            Else
                txtroundoff.ReadOnly = True
                txtroundoff.TabStop = False
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCGSTAMT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTSGSTAMT.KeyPress, TXTIGSTAMT.KeyPress, TXTCGSTAMT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTCGSTAMT_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTSGSTAMT.Validated, txtroundoff.Validated, TXTIGSTAMT.Validated, TXTCGSTAMT.Validated
        TOTAL()
    End Sub

    Private Sub CHKMANUAL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKMANUAL.CheckedChanged
        Try
            If CHKMANUAL.Checked = True Then
                TXTCGSTAMT.ReadOnly = False
                TXTCGSTAMT.TabStop = True
                TXTCGSTAMT.BackColor = Color.LemonChiffon
                TXTSGSTAMT.ReadOnly = False
                TXTSGSTAMT.TabStop = True
                TXTSGSTAMT.BackColor = Color.LemonChiffon
                TXTIGSTAMT.ReadOnly = False
                TXTIGSTAMT.TabStop = True
                TXTIGSTAMT.BackColor = Color.LemonChiffon
            Else
                TXTCGSTAMT.ReadOnly = True
                TXTCGSTAMT.TabStop = False
                TXTCGSTAMT.BackColor = Color.Linen
                TXTSGSTAMT.ReadOnly = True
                TXTSGSTAMT.TabStop = False
                TXTSGSTAMT.BackColor = Color.Linen
                TXTIGSTAMT.ReadOnly = True
                TXTIGSTAMT.TabStop = False
                TXTIGSTAMT.BackColor = Color.Linen
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If edit = True Then SENDWHATSAPP(TEMPCNNO)
            DT = OBJCMN.Execute_Any_String("UPDATE PROFORMACREDITNOTEMASTER SET CN_SENDWHATSAPP = 1 WHERE CN_NO = " & TEMPCNNO & " AND CN_YEARID = " & YearId, "", "")
            LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(CNNO As Integer)
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim WHATSAPPNO As String = ""
            Dim OBJCN As New CrDrNoteDesign
            OBJCN.MdiParent = MDIMain
            OBJCN.DIRECTPRINT = True
            OBJCN.FRMSTRING = "PROFORMACREDIT"
            OBJCN.DIRECTMAIL = False
            OBJCN.DIRECTWHATSAPP = True
            OBJCN.REGNAME = cmbregister.Text.Trim
            OBJCN.PRINTSETTING = PRINTDIALOG
            OBJCN.BILLNO = Val(CNNO)
            OBJCN.NOOFCOPIES = 1
            OBJCN.Show()
            OBJCN.Close()

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = CMBNAME.Text.Trim
            OBJWHATSAPP.AGENTNAME = CMBAGENT.Text.Trim
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\CN_" & Val(CNNO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add("CN_" & Val(CNNO) & ".pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCNNO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCNNO.Validating
        Try
            If ALLOWMANUALCNDN = True Then
                If (Val(TXTCNNO.Text.Trim) <> 0 And cmbregister.Text.Trim <> "" And edit = False) Or (edit = True And TEMPCNNO <> Val(TXTCNNO.Text.Trim)) Then
                    Dim OBJCMN As New ClsCommon
                    Dim dttable As DataTable = OBJCMN.search(" ISNULL(PROFORMACREDITNOTEMASTER.CN_no, 0) AS CNNO, ISNULL(REGISTERMASTER.register_name,'') AS REGNAME", "", " REGISTERMASTER INNER JOIN PROFORMACREDITNOTEMASTER ON REGISTERMASTER.register_id = PROFORMACREDITNOTEMASTER.CN_REGISTERID ", "  AND PROFORMACREDITNOTEMASTER.CN_NO=" & TXTCNNO.Text.Trim & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND PROFORMACREDITNOTEMASTER.CN_yearid = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        MsgBox("CreditNote No Already Exist")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        If CMBAGENT.Text.Trim = "" Then fillledger(CMBAGENT, edit, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
    End Sub

    Private Sub CMBAGENT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBAGENT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT' "
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

End Class