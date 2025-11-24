Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms
Imports BL
Imports DevExpress.CodeParser
Imports DevExpress.DataProcessing.InMemoryDataProcessor
Imports DevExpress.XtraRichEdit.Model
Imports iTextSharp
Imports iTextSharp.text.pdf.qrcode
Imports Org.BouncyCastle.Asn1
Public Class MagicBoxForRecPay

    Public EDIT As Boolean          'used for editing
    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Public TEMPCHQENTNO As Integer          'used for editing
    Dim TEMPROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer

    Sub CLEAR()
        EP.Clear()
        DTENTERYDATE.Text = Now.Date
        getmaxno()
        cmbaccname.Text = ""
        cmbname.Text = ""
        TXTCHQNO.Clear()
        txtremarks.Clear()
        CMBSELLERNAME.Text = ""
        'CMBSELLER.Text = ""
        LBLTOTALAMT.Text = 0.0
        GRIDISSUE.RowCount = 0
        GRIDDOUBLECLICK = False
        txtinwords.Clear()
        'If ClientName = "ABHEE" Then getmaxgrid_no()
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(Areceipt_NO),0) + 1 ", " AGENCYRECEIPTMASTER ", " AND Areceipt_cmpid=" & CmpId & " and Areceipt_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then txtsrno.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If GRIDISSUE.RowCount = 0 Then
                EP.SetError(GRIDISSUE, "Fill Item Details")
                bln = False
            End If
            'If CMBSELLERNAME.Text.Trim.Length = 0 Then
            '    EP.SetError(CMBSELLERNAME, " Please Fill Seller Name ")
            '    bln = False
            'End If
            'If cmbname.Text.Trim.Length = 0 Then
            '    EP.SetError(cmbname, " Please Fill Buyer Name ")
            '    bln = False
            'End If

            If DTENTERYDATE.Text = "__/__/____" Then
                EP.SetError(DTENTERYDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(DTENTERYDATE.Text) Then
                    EP.SetError(DTENTERYDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If

            'If ALLOWMANUALJONO = True Then
            '    If TXTJONO.Text <> "" And CMBNAME.Text.Trim <> "" And EDIT = False Then
            '        Dim OBJCMN As New ClsCommon
            '        Dim dttable As DataTable = OBJCMN.search(" ISNULL(JOBOUT.JO_NO,0)  AS JONO", "", " JOBOUT ", "  AND JOBOUT.JO_NO=" & TXTJONO.Text.Trim & " AND JOBOUT.JO_CMPID = " & CmpId & " AND JOBOUT.JO_LOCATIONID = " & Locationid & " AND JOBOUT.JO_YEARID = " & YearId)
            '        If dttable.Rows.Count > 0 Then
            '            EP.SetError(TXTJONO, "Job Out No Already Exist")
            '            bln = False
            '        End If
            '    End If
            'End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub ChqEnteries_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If errorvalid() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then CMDOK_Click(sender, e)
            End If
            Me.Close()

        ElseIf e.KeyCode = Keys.OemPipe Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            'SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            toolprevious_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
            'ElseIf e.KeyCode = Keys.F5 Then     'grid focus
            '    Me.Focus()
        ElseIf e.KeyCode = Keys.P And e.Alt = True Then
            Call PrintToolStripButton_Click(sender, e)
        End If
    End Sub

    Private Sub ChqEnteries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'RECEIPT'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            'getmaxno_receiptmaster()
            fillledger(cmbname, EDIT, " and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
            fillledger(cmbaccname, EDIT, " and (groupmaster.group_secondary = 'BANK A/C' OR groupmaster.group_secondary = 'BANK OD A/C' OR groupmaster.group_secondary = 'CASH IN HAND') and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
            If CMBSELLERNAME.Text.Trim = "" Then FILLNAME(CMBSELLERNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            'If CMBSELLERNAME.Text.Trim = "" Then FILLNAME(CMBSELLERNAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
            CLEAR()
            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objJO As New ClsChqEntries()
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TEMPCHQENTNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(YearId)
                objJO.alParaval = ALPARAVAL
                Dim dttable As DataTable = objJO.selectCHQENT(TEMPCHQENTNO, CmpId, YearId)

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows
                        TXTENTERYNO.Text = TEMPCHQENTNO
                        TXTENTERYNO.ReadOnly = True
                        DTENTERYDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        LBLTOTALAMT.Text = Val(dr("TOTALAMT"))
                        txtremarks.Text = Convert.ToString(dr("REMARKS").ToString)
                        txtinwords.Text = Convert.ToString(dr("INWORDS").ToString)

                        GRIDISSUE.Rows.Add(dr("GRIDSRNO").ToString, dr("ACCNAME").ToString, dr("NAME").ToString, dr("CHQNO").ToString, Format(Convert.ToDateTime(dr("CHQDATE")).Date, "dd/MM/yyyy"), Format(dr("AMT"), "0.00"), dr("BANKNAME").ToString)



                    Next
                    total()
                    'GRIDISSUE.FirstDisplayedScrollingRowIndex = GRIDISSUE.RowCount - 1
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        DTENTERYDATE.Focus()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim IntResult As Integer

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim DTTABLE As DataTable
            Dim alparaval As New ArrayList()

            ' Create a HashSet to track unique entries
            Dim addedEntries As New HashSet(Of String)

            For Each ROW As DataGridViewRow In GRIDISSUE.Rows
                If ROW.Cells(0).Value IsNot Nothing Then
                    ' Generate a unique key based on some values in the row (e.g., GSRNO and GACCNAME)
                    Dim entryKey As String = ROW.Cells(GSRNO.Index).Value.ToString() &
                                     ROW.Cells(GACCNAME.Index).Value.ToString() &
                                     ROW.Cells(GPARTYNAME.Index).Value.ToString()

                    ' If the entry has already been added, skip it
                    If addedEntries.Contains(entryKey) Then
                        Continue For
                    End If

                    ' Add this entry to the HashSet to prevent duplicates
                    addedEntries.Add(entryKey)

                    ' Add the row values to alparaval
                    alparaval.Clear()
                    alparaval.Add(ROW.Cells(GSRNO.Index).Value.ToString())
                    alparaval.Add("RECEIPT")
                    alparaval.Add(Format(Convert.ToDateTime(DTENTERYDATE.Text).Date, "MM/dd/yyyy"))
                    alparaval.Add(ROW.Cells(GACCNAME.Index).Value.ToString())
                    alparaval.Add(ROW.Cells(GPARTYNAME.Index).Value.ToString())
                    alparaval.Add(ROW.Cells(GCHQAMT.Index).Value)
                    alparaval.Add(ROW.Cells(GCHQNO.Index).Value.ToString())
                    alparaval.Add(txtremarks.Text.Trim())
                    alparaval.Add("") 'TXTBILLREMARKS.Text.Trim()
                    alparaval.Add("") 'TXTOURREMARKS.Text.Trim()
                    alparaval.Add("") 'txtinwords.Text.Trim()
                    alparaval.Add(0) 'CHKPDC
                    alparaval.Add("") 'CHKRECO
                    alparaval.Add(CmpId)
                    alparaval.Add(Locationid)
                    alparaval.Add(Userid)
                    alparaval.Add(YearId)
                    alparaval.Add(0)

                    Dim pgridsrno As String = ""
                    Dim paytype As String = ""
                    Dim billINITIALS As String = ""
                    Dim narr As String = ""
                    Dim amt As String = ""
                    Dim AMTPAID As String = ""
                    Dim EXTRAAMT As String = ""
                    Dim RETURNAMT As String = ""
                    Dim BALANCE As String = ""
                    Dim serialCounter As Integer = 1
                    Dim billNos() As String = ROW.Cells(GAMOUNT.Index).Value.ToString().Split("|"c)

                    For i As Integer = 0 To billNos.Length - 1
                        If ROW.Cells(GBILLNO.Index).Value.ToString() <> Nothing Then
                            If pgridsrno = "" Then
                                pgridsrno = serialCounter.ToString
                                paytype = ROW.Cells(GPAYTYPE.Index).Value.ToString()
                                ' billINITIALS = ""
                                narr = ""
                                'amt = Val(row.Cells(gamt.Index).Value)
                                AMTPAID = ""
                                EXTRAAMT = "" ' row.Cells(GEXTRAAMT.Index).Value
                                RETURNAMT = "" 'row.Cells(GRETURN.Index).Value
                                BALANCE = "" 'row.Cells(GBALANCE.Index).Value
                            Else
                                serialCounter += 1
                                pgridsrno = pgridsrno & "|" & serialCounter.ToString
                                paytype = paytype & "|" & ROW.Cells(GPAYTYPE.Index).Value.ToString()
                                'billINITIALS = billINITIALS & "|" & row.Cells(GBILLNO.Index).Value.ToString
                                narr = narr & "|" & ""
                                'amt = amt & "|" & Val(row.Cells(gamt.Index).Value)
                                AMTPAID = AMTPAID & "|" & "" 'row.Cells(GAMTPAID.Index).Value
                                EXTRAAMT = EXTRAAMT & "|" & "" 'row.Cells(GEXTRAAMT.Index).Value
                                RETURNAMT = RETURNAMT & "|" & "" 'row.Cells(GRETURN.Index).Value
                                BALANCE = BALANCE & "|" & "" 'row.Cells(GBALANCE.Index).Value
                            End If
                        End If
                    Next
                    If Not IsDBNull(ROW.Cells(gremamt.Index).Value) AndAlso ROW.Cells(gremamt.Index).Value.ToString().Trim() <> "" Then
                        serialCounter += 1
                        pgridsrno = pgridsrno & "|" & serialCounter.ToString
                        paytype = paytype & "|" & "On Account"
                        narr = narr & "|" & ""
                        AMTPAID = AMTPAID & "|" & ""
                        EXTRAAMT = EXTRAAMT & "|" & ""
                        RETURNAMT = RETURNAMT & "|" & ""
                        BALANCE = BALANCE & "|" & ""
                    End If
                    alparaval.Add(pgridsrno)
                    alparaval.Add(paytype)

                    If ROW.Cells(gremamt.Index).Value.ToString() <> "" Then alparaval.Add(ROW.Cells(GBILLNO.Index).Value.ToString() & "|" & "") Else alparaval.Add(ROW.Cells(GBILLNO.Index).Value.ToString())

                    alparaval.Add(narr)
                    If ROW.Cells(gremamt.Index).Value.ToString() <> "" Then alparaval.Add(ROW.Cells(GAMOUNT.Index).Value.ToString() & "|" & ROW.Cells(gremamt.Index).Value.ToString()) Else alparaval.Add(ROW.Cells(GAMOUNT.Index).Value.ToString())
                    alparaval.Add(AMTPAID)
                    alparaval.Add(EXTRAAMT)
                    alparaval.Add(RETURNAMT)
                    alparaval.Add(BALANCE)


                    alparaval.Add("") 'dgridsrno
                    alparaval.Add("") 'descledgername
                    alparaval.Add("") 'descnarration
                    alparaval.Add("") 'descamount
                    alparaval.Add("") 'DESCPAYGRIDSRNO
                    alparaval.Add("") 'DESCPAYBILLINITIALS
                    alparaval.Add("") 'CMBPARTYBANK.Text.Trim
                    alparaval.Add("") 'TXTSPECIALREMARKS.Text.Trim
                    alparaval.Add(Format(Convert.ToDateTime(ROW.Cells(GCHQDATE.Index).Value).Date, "MM/dd/yyyy"))

                    alparaval.Add("")   'COMPLAINT
                    alparaval.Add("")   'COMPLAINTBY
                    alparaval.Add("")   'COMPLAINTDATE
                    alparaval.Add(0)    'HOLDINTCALC
                    ' Initialize the receipt object
                    Dim OBJCLRECEIPT As New ClsAgencyReceiptMaster()
                    OBJCLRECEIPT.alParaval = alparaval

                    ' Only save if not in edit mode
                    If Not EDIT Then
                        If Not USERADD Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        DTTABLE = OBJCLRECEIPT.SAVE()
                    End If
                End If
                GENERATEAGENCYCN(Val(ROW.Index))

                'WE NEED TO CREATE THE SAME ORDER IN ABHEE FABRICS LLP COMPANY
                'IF BUYER IS ABHEE FABRICS LLP THEN WE NEED TO CREATE PAYMENT IN THE NAME OF SELLER IN ABHEE FABRICS LLP COMPANY
                Dim OBJCMN As New ClsCommon
                Dim TEMPYEARID, TEMPCMPID, TEMPLEDGERID, TEMPITEMID As Integer
                Dim DTNAME As DataTable = OBJCMN.SEARCH("ISNULL(ACC_CMPNAME,'') AS NAME", "", " LEDGERS", " AND LEDGERS.ACC_CMPNAME = '" & ROW.Cells(GPARTYNAME.Index).Value & "' AND LEDGERS.ACC_YEARID = " & YearId)
                If DTNAME.Rows.Count > 0 AndAlso DTNAME.Rows(0).Item("NAME") = "ABHEE FABRICS LLP [ BUYER ]" Then

                    'CREATE payment IN ABHEE FABRICS LLP
                    'FIRST GET THE CMPID AND YEARID OF ABHEE FABRICS LLP
                    Dim TEMPDT As DataTable = OBJCMN.SEARCH(" TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMPMASTER.CMP_DISPLAYEDNAME = 'ABHEE FABRICS LLP' ORDER BY YEAR_STARTDATE DESC")
                    If TEMPDT.Rows.Count > 0 Then
                        TEMPCMPID = TEMPDT.Rows(0).Item("CMPID")
                        TEMPYEARID = TEMPDT.Rows(0).Item("YEARID")
                    Else
                        GoTo NEXTLINE
                    End If

                    'CHECK WHETHER SELLER NAME IS PRESENT OR NOT, IF NOT PRESENT THEN ADD NEW 
                    TEMPDT = OBJCMN.SEARCH("ACC_ID AS LEDGERID ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & ROW.Cells(GPARTYNAME.Index).Value & "' AND ACC_YEARID = " & TEMPYEARID)
                    If TEMPDT.Rows.Count > 0 Then TEMPLEDGERID = TEMPDT.Rows(0).Item("LEDGERID") Else CREATELEDGER(ROW.Cells(GPARTYNAME.Index).Value, TEMPCMPID, TEMPYEARID)

                    'CHECK WHETHER bank NAME IS PRESENT OR NOT, IF NOT PRESENT THEN ADD NEW 
                    TEMPDT = OBJCMN.SEARCH("ACC_ID AS LEDGERID ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & ROW.Cells(GACCNAME.Index).Value & "' AND ACC_YEARID = " & TEMPYEARID)
                    If TEMPDT.Rows.Count > 0 Then TEMPLEDGERID = TEMPDT.Rows(0).Item("LEDGERID") Else CREATELEDGER(ROW.Cells(GACCNAME.Index).Value, TEMPCMPID, TEMPYEARID)


                    CREATEPAYMENT(Val(ROW.Index), TEMPCMPID, TEMPYEARID)
                    CREATEJV(Val(ROW.Index), TEMPCMPID, TEMPYEARID)

                End If


                '******************** END OF JV GENERATION CODE ***************************
                Dim DTNAME1 As DataTable = OBJCMN.SEARCH("ISNULL(ACC_CMPNAME,'') AS NAME", "", " LEDGERS", " AND LEDGERS.ACC_CMPNAME = '" & ROW.Cells(GSELLERNAME.Index).Value & "' AND LEDGERS.ACC_YEARID = " & YearId)

                If DTNAME1.Rows.Count > 0 AndAlso DTNAME1.Rows(0).Item("NAME") = "ABHEE FABRICS LLP [ SELLER ]" Then


                    'CREATE reciept IN ABHEE FABRICS LLP
                    'FIRST GET THE CMPID AND YEARID OF ABHEE FABRICS LLP
                    Dim TEMPDT As DataTable = OBJCMN.SEARCH(" TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMPMASTER.CMP_DISPLAYEDNAME = 'ABHEE FABRICS LLP' ORDER BY YEAR_STARTDATE DESC")
                    If TEMPDT.Rows.Count > 0 Then
                        TEMPCMPID = TEMPDT.Rows(0).Item("CMPID")
                        TEMPYEARID = TEMPDT.Rows(0).Item("YEARID")
                    Else
                        GoTo NEXTLINE
                    End If

                    'CHECK WHETHER SELLER NAME IS PRESENT OR NOT, IF NOT PRESENT THEN ADD NEW 
                    TEMPDT = OBJCMN.SEARCH("ACC_ID AS LEDGERID ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & ROW.Cells(GSELLERNAME.Index).Value & "' AND ACC_YEARID = " & TEMPYEARID)
                    If TEMPDT.Rows.Count > 0 Then TEMPLEDGERID = TEMPDT.Rows(0).Item("LEDGERID") Else CREATELEDGER(ROW.Cells(GSELLERNAME.Index).Value, TEMPCMPID, TEMPYEARID)

                    'CHECK WHETHER bank NAME IS PRESENT OR NOT, IF NOT PRESENT THEN ADD NEW 
                    TEMPDT = OBJCMN.SEARCH("ACC_ID AS LEDGERID ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & ROW.Cells(GACCNAME.Index).Value & "' AND ACC_YEARID = " & TEMPYEARID)
                    If TEMPDT.Rows.Count > 0 Then TEMPLEDGERID = TEMPDT.Rows(0).Item("LEDGERID") Else CREATELEDGER(ROW.Cells(GACCNAME.Index).Value, TEMPCMPID, TEMPYEARID)


                    CREATEREC(Val(ROW.Index), TEMPCMPID, TEMPYEARID)
                End If
                '******************** END OF PO GENERATION CODE ***************************

NEXTLINE:
                MessageBox.Show("Details Added")
                CLEAR()
            Next

            CLEAR()
            DTENTERYDATE.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub getmaxgrid_no()
        Dim DTTABLE As New DataTable
        If CMBSELLERNAME.Text = "ABHEE FABRICS LLP" Then
            DTTABLE = getmax(" isnull(max(RECEIPT_no),0) + 1 ", "RECEIPTMASTER", " AND RECEIPT_cmpid=" & CmpId & " and RECEIPT_locationid=" & Locationid & " and RECEIPT_yearid=" & YearId)
            If DTTABLE.Rows.Count > 0 Then
                txtsrno.Text = DTTABLE.Rows(0).Item(0)
            End If
        Else cmbname.Text = "ABHEE FABRICS LLP"
            DTTABLE = getmax(" isnull(max(PAYMENT_no),0) + 1 ", "PAYMENTMASTER", " AND PAYMENT_cmpid=" & CmpId & " and PAYMENT_locationid=" & Locationid & " and PAYMENT_yearid=" & YearId)
            If DTTABLE.Rows.Count > 0 Then
                txtsrno.Text = DTTABLE.Rows(0).Item(0)
            End If
        End If
    End Sub
    Sub CREATEPAYMENT(ROWNO As Integer, TEMPCMPID As Integer, TEMPYEARID As Integer)
        Try
            'If cmbname.Text = "ABHEE FABRICS LLP" Then
            Dim DTTABLE1 As DataTable
            Dim alparaval1 As New ArrayList()

            ' Create a HashSet to track unique entries
            Dim addedEntries1 As New HashSet(Of String)

            For Each ROW As DataGridViewRow In GRIDISSUE.Rows
                If ROW.Cells(GPARTYNAME.Index).Value.ToString() = "ABHEE FABRICS LLP [ BUYER ]" Then

                    If ROW.Cells(GSRNO.Index).Value IsNot Nothing Then
                        ' Generate a unique key based on some values in the row (e.g., GSRNO and GACCNAME)
                        Dim entryKey As String = ROW.Cells(GSRNO.Index).Value.ToString() &
                                 ROW.Cells(GACCNAME.Index).Value.ToString() &
                                 ROW.Cells(GPARTYNAME.Index).Value.ToString()

                        ' If the entry has already been added, skip it
                        If addedEntries1.Contains(entryKey) Then
                            Continue For
                        End If

                        ' Add this entry to the HashSet to prevent duplicates
                        addedEntries1.Add(entryKey)

                        ' Add the row values to alparaval1
                        alparaval1.Clear()
                        alparaval1.Add(ROW.Cells(GSRNO.Index).Value.ToString())
                        alparaval1.Add("PAYMENT")
                        alparaval1.Add(Format(Convert.ToDateTime(DTENTERYDATE.Text).Date, "MM/dd/yyyy"))
                        alparaval1.Add(ROW.Cells(GACCNAME.Index).Value.ToString())
                        alparaval1.Add(ROW.Cells(GSELLERNAME.Index).Value.ToString())
                        alparaval1.Add(ROW.Cells(GCHQAMT.Index).Value)
                        alparaval1.Add(ROW.Cells(GCHQNO.Index).Value.ToString())
                        alparaval1.Add(txtremarks.Text.Trim())
                        alparaval1.Add("") 'TXTBILLREMARKS.Text.Trim()
                        alparaval1.Add("") 'TXTOURREMARKS.Text.Trim()
                        alparaval1.Add("") 'txtinwords.Text.Trim()
                        alparaval1.Add("") 'CHKRECO
                        alparaval1.Add(TEMPCMPID)
                        alparaval1.Add(Locationid)
                        alparaval1.Add(Userid)
                        alparaval1.Add(TEMPYEARID)
                        alparaval1.Add(0)
                        Dim pgridsrno As String = ""
                        Dim paytype As String = ""
                        Dim billINITIALS As String = ""
                        Dim narr As String = ""
                        Dim amt As String = ""
                        Dim AMTPAID As String = ""
                        Dim EXTRAAMT As String = ""
                        Dim RETURNAMT As String = ""
                        Dim BALANCE As String = ""
                        Dim serialCounter As Integer = 1
                        Dim billNos() As String = ROW.Cells(GAMOUNT.Index).Value.ToString().Split("|"c)

                        For i As Integer = 0 To billNos.Length - 1
                            If ROW.Cells(GBILLNO.Index).Value.ToString() <> Nothing Then
                                If pgridsrno = "" Then
                                    pgridsrno = serialCounter.ToString
                                    paytype = ROW.Cells(GPAYTYPE.Index).Value.ToString()
                                    ' billINITIALS = ""
                                    narr = ""
                                    'amt = Val(row.Cells(gamt.Index).Value)
                                    AMTPAID = ""
                                    EXTRAAMT = "" ' row.Cells(GEXTRAAMT.Index).Value
                                    RETURNAMT = "" 'row.Cells(GRETURN.Index).Value
                                    BALANCE = "" 'row.Cells(GBALANCE.Index).Value
                                Else
                                    serialCounter += 1
                                    pgridsrno = pgridsrno & "|" & serialCounter.ToString
                                    paytype = paytype & "|" & ROW.Cells(GPAYTYPE.Index).Value.ToString()
                                    'billINITIALS = billINITIALS & "|" & row.Cells(GBILLNO.Index).Value.ToString
                                    narr = narr & "|" & ""
                                    'amt = amt & "|" & Val(row.Cells(gamt.Index).Value)
                                    AMTPAID = AMTPAID & "|" & "" 'row.Cells(GAMTPAID.Index).Value
                                    EXTRAAMT = EXTRAAMT & "|" & "" 'row.Cells(GEXTRAAMT.Index).Value
                                    RETURNAMT = RETURNAMT & "|" & "" 'row.Cells(GRETURN.Index).Value
                                    BALANCE = BALANCE & "|" & "" 'row.Cells(GBALANCE.Index).Value
                                End If
                            End If
                        Next
                        If Not IsDBNull(ROW.Cells(gremamt.Index).Value) AndAlso ROW.Cells(gremamt.Index).Value.ToString().Trim() <> "" Then
                            serialCounter += 1
                            pgridsrno = pgridsrno & "|" & serialCounter.ToString
                            paytype = paytype & "|" & "On Account"
                            narr = narr & "|" & ""
                            AMTPAID = AMTPAID & "|" & ""
                            EXTRAAMT = EXTRAAMT & "|" & ""
                            RETURNAMT = RETURNAMT & "|" & ""
                            BALANCE = BALANCE & "|" & ""
                        End If
                        alparaval1.Add(pgridsrno)
                        alparaval1.Add(paytype)

                        If ROW.Cells(gremamt.Index).Value.ToString() <> "" Then alparaval1.Add(ROW.Cells(GBILLNO.Index).Value.ToString() & "|" & "") Else alparaval1.Add(ROW.Cells(GBILLNO.Index).Value.ToString())

                        alparaval1.Add(narr)
                        If ROW.Cells(gremamt.Index).Value.ToString() <> "" Then alparaval1.Add(ROW.Cells(GAMOUNT.Index).Value.ToString() & "|" & ROW.Cells(gremamt.Index).Value.ToString()) Else alparaval1.Add(ROW.Cells(GAMOUNT.Index).Value.ToString())
                        alparaval1.Add(AMTPAID)
                        alparaval1.Add(EXTRAAMT)
                        alparaval1.Add(RETURNAMT)
                        alparaval1.Add(BALANCE)

                        alparaval1.Add("") 'dgridsrno
                        alparaval1.Add("") 'descledgername
                        alparaval1.Add("") 'descnarration
                        alparaval1.Add("") 'descamount
                        alparaval1.Add("") 'DESCPAYGRIDSRNO
                        alparaval1.Add("") 'DESCPAYBILLINITIALS
                        alparaval1.Add("") 'TXTSPECIALREMARKS.Text.Trim()
                        alparaval1.Add("") 'infav

                        ' Initialize the payment object
                        Dim OBJCLRECEIPT As New ClsPaymentMaster()
                        OBJCLRECEIPT.alParaval = alparaval1

                        ' Only save if not in edit mode
                        If Not EDIT Then
                            If Not USERADD Then
                                MsgBox("Insufficient Rights")
                                Exit Sub
                            End If
                            DTTABLE1 = OBJCLRECEIPT.SAVE()
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CREATEJV(ROWNO As Integer, TEMPCMPID As Integer, TEMPYEARID As Integer)
        Try
            'If cmbname.Text = "ABHEE FABRICS LLP" Then
            Dim DTTABLE1 As DataTable
            Dim alparaval1 As New ArrayList()

            Dim addedEntries1 As New HashSet(Of String)

            For Each ROW As DataGridViewRow In GRIDISSUE.Rows

                ' --- Split bill numbers ---
                Dim billNos As String = ROW.Cells(GBILLNO.Index).Value.ToString()
                Dim billArray() As String = billNos.Split("|"c)

                ' --- Split amounts  ---
                Dim amtArray() As String = {}
                If ROW.Cells(GTDSAMT.Index).Value IsNot Nothing Then
                    amtArray = ROW.Cells(GTDSAMT.Index).Value.ToString().Split("|"c)
                End If


                For i = 0 To billArray.Length - 1
                    Dim billNo As String = billArray(i).Trim()
                    If String.IsNullOrWhiteSpace(billNo) Then Continue For

                    Dim tdsAmount As Decimal = 0
                    If amtArray.Length > i Then
                        Decimal.TryParse(amtArray(i), tdsAmount)
                    End If

                    ' === NEW CONDITION: SKIP ZERO AMOUNT ENTRIES ===
                    If tdsAmount = 0 Then
                        Continue For    ' <-- SKIP saving this JV entry
                    End If

                    If ROW.Cells(GPARTYNAME.Index).Value.ToString() = "ABHEE FABRICS LLP [ BUYER ]" Then

                        If ROW.Cells(GSRNO.Index).Value IsNot Nothing Then
                            ' Generate a unique key based on some values in the row (e.g., GSRNO and GACCNAME)
                            Dim entryKey As String = ROW.Cells(GSRNO.Index).Value.ToString() &
                                 ROW.Cells(GACCNAME.Index).Value.ToString() &
                                 ROW.Cells(GPARTYNAME.Index).Value.ToString()

                            ' If the entry has already been added, skip it
                            If addedEntries1.Contains(entryKey) Then
                                Continue For
                            End If

                            ' Add this entry to the HashSet to prevent duplicates
                            addedEntries1.Add(entryKey)

                            ' Add the row values to alparaval1
                            alparaval1.Clear()
                            alparaval1.Add(0) 'ROW.Cells(GSRNO.Index).Value.ToString())
                            alparaval1.Add("JOURNAL REGISTER") '"cmbregister.Text.Trim)
                            alparaval1.Add(Format(Convert.ToDateTime(DTENTERYDATE.Text).Date, "MM/dd/yyyy"))
                            alparaval1.Add(0) 'Val(TXTTOTALDR.Text.Trim))
                            alparaval1.Add(0) 'Val(TXTTOTALCR.Text.Trim))
                            alparaval1.Add("") 'txtremarks.Text.Trim)
                            alparaval1.Add("") 'TXTBILLREMARKS.Text.Trim)
                            alparaval1.Add(TEMPCMPID)
                            alparaval1.Add(Locationid)
                            alparaval1.Add(Userid)
                            alparaval1.Add(TEMPYEARID)
                            alparaval1.Add(0)

                            Dim type As String = ""
                            Dim name As String = ""
                            Dim paytype As String = ""
                            Dim refno As String = ""
                            Dim debit As String = ""
                            Dim credit As String = ""
                            Dim gridsrno As String = ""

                            For j As Integer = 0 To 1
                                If type = "" Then
                                    type = "Dr"
                                    name = (ROW.Cells(GSELLERNAME.Index).Value.ToString())
                                    paytype = "Against Bill"
                                    refno = billNo ' TXTINITIALS.Text.Trim
                                    debit = tdsAmount
                                    credit = 0
                                    gridsrno = 1
                                Else
                                    type = type & "|" & "Cr"
                                    name = name & "|" & (ROW.Cells(GSELLERNAME.Index).Value.ToString())
                                    paytype = paytype & "|" & "On Account"
                                    refno = refno & "|" & billNo ' TXTINITIALS.Text.Trim
                                    debit = debit & "|" & 0
                                    credit = credit & "|" & tdsAmount
                                    gridsrno = gridsrno & "|" & 2
                                End If
                            Next

                            alparaval1.Add(type)
                            alparaval1.Add(name)
                            alparaval1.Add(paytype)
                            alparaval1.Add(refno)
                            alparaval1.Add(debit)
                            alparaval1.Add(credit)
                            alparaval1.Add(gridsrno)
                            alparaval1.Add("") 'TXTSPLREMARKS.Text)
                            alparaval1.Add("") 'TXTPARTYBILLNO.Text.Trim)
                            alparaval1.Add("") '"CMBCOSTCENTERNAME.Text.Trim)

                            ' Initialize the payment object
                            Dim OBJCLJV As New ClsJournalMaster()
                            OBJCLJV.alParaval = alparaval1

                            ' Only save if not in edit mode
                            If Not EDIT Then
                                If Not USERADD Then
                                    MsgBox("Insufficient Rights")
                                    Exit Sub
                                End If
                                DTTABLE1 = OBJCLJV.save()
                            End If
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub GENERATEAGENCYCN(ROWNO As Integer)
        Try
            'If cmbname.Text = "ABHEE FABRICS LLP" Then
            Dim DTTABLE1 As DataTable
            Dim alparaval1 As New ArrayList()

            Dim addedEntries1 As New HashSet(Of String)

            For Each ROW As DataGridViewRow In GRIDISSUE.Rows

                ' --- Split bill numbers ---
                Dim billNos As String = ROW.Cells(GBILLNO.Index).Value.ToString()
                Dim billArray = billNos.Split("|"c).Reverse().ToArray()
                Dim amtArray = ROW.Cells(GTDSAMT.Index).Value.ToString().Split("|"c).Reverse().ToArray()



                For i = 0 To billArray.Length - 1
                    Dim billNo As String = billArray(i).Trim()
                    If String.IsNullOrWhiteSpace(billNo) Then Continue For

                    Dim tdsAmount As Decimal = 0
                    If amtArray.Length > i Then
                        Decimal.TryParse(amtArray(i), tdsAmount)
                    End If

                    ' === NEW CONDITION: SKIP ZERO AMOUNT ENTRIES ===
                    If tdsAmount = 0 Then
                        Continue For    ' <-- SKIP saving this JV entry
                    End If

                    If ROW.Cells(GPARTYNAME.Index).Value.ToString() = "ABHEE FABRICS LLP [ BUYER ]" Then

                        If ROW.Cells(GSRNO.Index).Value IsNot Nothing Then
                            ' Generate a unique key based on some values in the row (e.g., GSRNO and GACCNAME)
                            Dim entryKey As String = ROW.Cells(GSRNO.Index).Value.ToString() &
                                 ROW.Cells(GACCNAME.Index).Value.ToString() &
                                 ROW.Cells(GPARTYNAME.Index).Value.ToString()

                            ' If the entry has already been added, skip it
                            If addedEntries1.Contains(entryKey) Then
                                Continue For
                            End If

                            ' Add this entry to the HashSet to prevent duplicates
                            addedEntries1.Add(entryKey)

                            ' Add the row values to alparaval1
                            alparaval1.Clear()

                            alparaval1.Add(0)    'CNNO
                            alparaval1.Add("")   'TYPE
                            alparaval1.Add(Format(Convert.ToDateTime(DTENTERYDATE.Text).Date, "MM/dd/yyyy")) 'CNDATE
                            alparaval1.Add(Format(Convert.ToDateTime(DTENTERYDATE.Text).Date, "MM/dd/yyyy")) 'ACTUALINVDATE

                            alparaval1.Add("")   'BILLNO
                            alparaval1.Add("")  'PARTYBILLNO
                            alparaval1.Add(ROW.Cells(GPARTYNAME.Index).Value.ToString()) 'NAME
                            alparaval1.Add("")   'AGENT
                            alparaval1.Add(0) 'HSNCODE
                            alparaval1.Add(ROW.Cells(GSELLERNAME.Index).Value.ToString()) 'DEBITLEDGER
                            alparaval1.Add(ROW.Cells(GTDSACC.Index).Value.ToString())    'PACKING (add debit to)

                            alparaval1.Add("")   'INVPRINTINITIALS
                            alparaval1.Add(0)    'PCS
                            alparaval1.Add(0)    'MTRS
                            alparaval1.Add(0)    'ACTUALINVAMT
                            alparaval1.Add(0)    'DISCPER


                            alparaval1.Add(Val(tdsAmount))
                            alparaval1.Add(0)    'TOTALTAXAMT
                            alparaval1.Add(0)    'OTHERCHGS
                            alparaval1.Add(0)    'CHARGES

                            alparaval1.Add(0)    'RCM
                            alparaval1.Add(1)    'MANUALGST (KEEP IT TRUE), AS WE NEED 0 GSTAMT
                            alparaval1.Add(0)    'MANUALROUNDOFF
                            alparaval1.Add(1)    'NOGSTR1

                            alparaval1.Add(Val(tdsAmount))

                            alparaval1.Add(0)    'CGSTPER
                            alparaval1.Add(0)    'CGSTAMT
                            alparaval1.Add(0)    'SGSTPER
                            alparaval1.Add(0)    'SGSTAMT
                            alparaval1.Add(0)    'IGSTPER
                            alparaval1.Add(0)    'IGSTAMT

                            alparaval1.Add(Val(tdsAmount)) 'TOTALWITHGST
                            alparaval1.Add(0)    'APPLYTCS
                            alparaval1.Add(0)    'TCSPER
                            alparaval1.Add(0)    'TCSAMT

                            alparaval1.Add(0)    'ROUNDOFF
                            alparaval1.Add(Val(tdsAmount)) 'GTOTAL

                            alparaval1.Add(0)    'RECAMT
                            alparaval1.Add(0)    'EXTRAAMT
                            alparaval1.Add(0)    'RETURN
                            alparaval1.Add(Val(tdsAmount)) 'BAL

                            alparaval1.Add("")   'REMARKS
                            alparaval1.Add("")   'BILLREMARKS
                            alparaval1.Add("")   'INWORDS

                            alparaval1.Add(CmpId)
                            alparaval1.Add(Locationid)
                            alparaval1.Add(Userid)
                            alparaval1.Add(YearId)
                            alparaval1.Add(0)

                            alparaval1.Add("")   'CSRNO)
                            alparaval1.Add("")   'CCHGS)
                            alparaval1.Add("")   'CPER)
                            alparaval1.Add("")   'CAMT)
                            alparaval1.Add("")   'CTAXID)

                            alparaval1.Add("1")  'GRIDSRNO
                            alparaval1.Add("Against Bill")   'PAYTYPE
                            alparaval1.Add(billNo)   'BILLINITIALS
                            alparaval1.Add("")   'NARR
                            alparaval1.Add(Val(tdsAmount)) 'ADJAMT
                            alparaval1.Add(0)    'RECAMT
                            alparaval1.Add(0)    'EXTRAAMT
                            alparaval1.Add(0)    'RETURN
                            alparaval1.Add(Val(tdsAmount)) 'BALANCE

                            alparaval1.Add("")   'IRN
                            alparaval1.Add("")   'ACKNO
                            alparaval1.Add(Format(Convert.ToDateTime(DTENTERYDATE.Text).Date, "MM/dd/yyyy")) 'ACKDATE
                            alparaval1.Add(DBNull.Value) 'QRCODE
                            alparaval1.Add("")   'SPREMARKS
                            alparaval1.Add(0)    'CD
                            alparaval1.Add("")   'COSTCENTRE

                            alparaval1.Add("")   'COMPLAINT
                            alparaval1.Add("")   'COMPLAINTBY
                            alparaval1.Add("")   'COMPLAINTDATE

                            Dim objclsCNmaster As New ClsAgencyCreditNote()
                            objclsCNmaster.alParaval = alparaval1
                            'Dim DTTABLE As DataTable = objclsCNmaster.SAVE()

                            ' Only save if not in edit mode
                            If Not EDIT Then
                                If Not USERADD Then
                                    MsgBox("Insufficient Rights")
                                    Exit Sub
                                End If
                                DTTABLE1 = objclsCNmaster.SAVE()
                            End If
                        End If
                    End If
                Next
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub CREATEREC(ROWNO As Integer, TEMPCMPID As Integer, TEMPYEARID As Integer)
        Try
            Dim DTTABLE As DataTable
            Dim alparaval As New ArrayList()

            ' Create a HashSet to track unique entries
            Dim addedEntries As New HashSet(Of String)

            For Each ROW As DataGridViewRow In GRIDISSUE.Rows
                If ROW.Cells(0).Value IsNot Nothing Then
                    ' Generate a unique key based on some values in the row (e.g., GSRNO and GACCNAME)
                    Dim entryKey As String = ROW.Cells(GSRNO.Index).Value.ToString() &
                                     ROW.Cells(GACCNAME.Index).Value.ToString() &
                                     ROW.Cells(GPARTYNAME.Index).Value.ToString()

                    ' If the entry has already been added, skip it
                    If addedEntries.Contains(entryKey) Then
                        Continue For
                    End If

                    ' Add this entry to the HashSet to prevent duplicates
                    addedEntries.Add(entryKey)

                    ' Add the row values to alparaval
                    alparaval.Clear()
                    alparaval.Add(ROW.Cells(GSRNO.Index).Value.ToString())
                    alparaval.Add("RECEIPT")
                    alparaval.Add(Format(Convert.ToDateTime(DTENTERYDATE.Text).Date, "MM/dd/yyyy"))
                    alparaval.Add(ROW.Cells(GACCNAME.Index).Value.ToString())
                    alparaval.Add(ROW.Cells(GPARTYNAME.Index).Value.ToString())
                    alparaval.Add(ROW.Cells(GCHQAMT.Index).Value)
                    alparaval.Add(ROW.Cells(GCHQNO.Index).Value.ToString())
                    alparaval.Add(txtremarks.Text.Trim())
                    alparaval.Add("") 'TXTBILLREMARKS.Text.Trim()
                    alparaval.Add("") 'TXTOURREMARKS.Text.Trim()
                    alparaval.Add("") 'txtinwords.Text.Trim()
                    alparaval.Add(0) 'CHKPDC
                    alparaval.Add("") 'CHKRECO
                    alparaval.Add(TEMPCMPID)
                    alparaval.Add(Locationid)
                    alparaval.Add(Userid)
                    alparaval.Add(TEMPYEARID)
                    alparaval.Add(0)

                    Dim pgridsrno As String = ""
                    Dim paytype As String = ""
                    Dim billINITIALS As String = ""
                    Dim narr As String = ""
                    Dim amt As String = ""
                    Dim AMTPAID As String = ""
                    Dim EXTRAAMT As String = ""
                    Dim RETURNAMT As String = ""
                    Dim BALANCE As String = ""
                    Dim serialCounter As Integer = 1
                    Dim billNos() As String = ROW.Cells(GAMOUNT.Index).Value.ToString().Split("|"c)

                    For i As Integer = 0 To billNos.Length - 1
                        If ROW.Cells(GBILLNO.Index).Value.ToString() <> Nothing Then
                            If pgridsrno = "" Then
                                pgridsrno = serialCounter.ToString
                                paytype = ROW.Cells(GPAYTYPE.Index).Value.ToString()
                                ' billINITIALS = ""
                                narr = ""
                                'amt = Val(row.Cells(gamt.Index).Value)
                                AMTPAID = ""
                                EXTRAAMT = "" ' row.Cells(GEXTRAAMT.Index).Value
                                RETURNAMT = "" 'row.Cells(GRETURN.Index).Value
                                BALANCE = "" 'row.Cells(GBALANCE.Index).Value
                            Else
                                serialCounter += 1
                                pgridsrno = pgridsrno & "|" & serialCounter.ToString
                                paytype = paytype & "|" & ROW.Cells(GPAYTYPE.Index).Value.ToString()
                                'billINITIALS = billINITIALS & "|" & row.Cells(GBILLNO.Index).Value.ToString
                                narr = narr & "|" & ""
                                'amt = amt & "|" & Val(row.Cells(gamt.Index).Value)
                                AMTPAID = AMTPAID & "|" & "" 'row.Cells(GAMTPAID.Index).Value
                                EXTRAAMT = EXTRAAMT & "|" & "" 'row.Cells(GEXTRAAMT.Index).Value
                                RETURNAMT = RETURNAMT & "|" & "" 'row.Cells(GRETURN.Index).Value
                                BALANCE = BALANCE & "|" & "" 'row.Cells(GBALANCE.Index).Value
                            End If
                        End If
                    Next
                    If Not IsDBNull(ROW.Cells(gremamt.Index).Value) AndAlso ROW.Cells(gremamt.Index).Value.ToString().Trim() <> "" Then
                        serialCounter += 1
                        pgridsrno = pgridsrno & "|" & serialCounter.ToString
                        paytype = paytype & "|" & "On Account"
                        narr = narr & "|" & ""
                        AMTPAID = AMTPAID & "|" & ""
                        EXTRAAMT = EXTRAAMT & "|" & ""
                        RETURNAMT = RETURNAMT & "|" & ""
                        BALANCE = BALANCE & "|" & ""
                    End If
                    alparaval.Add(pgridsrno)
                    alparaval.Add(paytype)

                    If ROW.Cells(gremamt.Index).Value.ToString() <> "" Then alparaval.Add(ROW.Cells(GBILLNO.Index).Value.ToString() & "|" & "") Else alparaval.Add(ROW.Cells(GBILLNO.Index).Value.ToString())

                    alparaval.Add(narr)
                    If ROW.Cells(gremamt.Index).Value.ToString() <> "" Then alparaval.Add(ROW.Cells(GAMOUNT.Index).Value.ToString() & "|" & ROW.Cells(gremamt.Index).Value.ToString()) Else alparaval.Add(ROW.Cells(GAMOUNT.Index).Value.ToString())
                    alparaval.Add(AMTPAID)
                    alparaval.Add(EXTRAAMT)
                    alparaval.Add(RETURNAMT)
                    alparaval.Add(BALANCE)


                    alparaval.Add("") 'dgridsrno
                    alparaval.Add("") 'descledgername
                    alparaval.Add("") 'descnarration
                    alparaval.Add("") 'descamount
                    alparaval.Add("") 'DESCPAYGRIDSRNO
                    alparaval.Add("") 'DESCPAYBILLINITIALS
                    alparaval.Add("") 'CMBPARTYBANK.Text.Trim
                    alparaval.Add("") 'TXTSPECIALREMARKS.Text.Trim
                    alparaval.Add(Format(Convert.ToDateTime(ROW.Cells(GCHQDATE.Index).Value).Date, "MM/dd/yyyy"))

                    ' Initialize the receipt object
                    Dim OBJCLRECEIPT As New ClsReceiptMaster()
                    OBJCLRECEIPT.alParaval = alparaval

                    ' Only save if not in edit mode
                    If Not EDIT Then
                        If Not USERADD Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        DTTABLE = OBJCLRECEIPT.SAVE()
                    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CREATELEDGER(NAME As String, TEMPCMPID As Integer, TEMPYEARID As Integer)
        Try

            'ADD IN ACCOUNTSMASTER
            Dim ALPARAVAL As New ArrayList
            Dim OBJSM As New ClsAccountsMaster
            Dim OBJCMN As New ClsCommon
            Dim DTLEDGER As DataTable = OBJCMN.SEARCH(" GROUPMASTER.group_name AS GROUPNAME, ISNULL(LEDGERS.ACC_INTPER, 0) AS INTPER, ISNULL(LEDGERS.Acc_add1,'') AS ADD1, ISNULL(LEDGERS.Acc_add2,'') AS ADD2, ISNULL(AREAMASTER.area_name, '') AS AREA, ISNULL(CITYMASTER.city_name, '') AS CITYNAME, ISNULL(LEDGERS.Acc_zipcode, '') AS PINCODE, ISNULL(STATEMASTER.state_name, '') AS STATE, ISNULL(COUNTRYMASTER.country_name, '') AS COUNTRY, ISNULL(LEDGERS.Acc_crdays, 0) AS CRDAYS, ISNULL(LEDGERS.Acc_crlimit, 0) AS CRLIMIT, ISNULL(LEDGERS.Acc_resino, '') AS RESINO, ISNULL(LEDGERS.Acc_altno, '') AS ALTNO, ISNULL(LEDGERS.Acc_phone, '') 
                         AS PHONENO, ISNULL(LEDGERS.Acc_mobile, '') AS MOBILENO, ISNULL(LEDGERS.ACC_WHATSAPPNO, '') AS WHATSAPPNO, ISNULL(LEDGERS.Acc_fax, '') AS FAX, ISNULL(LEDGERS.Acc_website, '') AS WEBSITE, 
                         ISNULL(LEDGERS.Acc_email, '') AS EMAIL, ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSPORT, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS BROKER, ISNULL(LEDGERS.ACC_AGENTCOMM, 0) AS COMMISSION, 
                         ISNULL(LEDGERS.ACC_DISC, 0) AS DISCOUNT, ISNULL(LEDGERS.ACC_CDPER, 0) AS CASHDISC, ISNULL(LEDGERS.ACC_KMS, 0) AS KMS, ISNULL(LEDGERS.Acc_panno, '') AS PANNO, ISNULL(LEDGERS.ACC_GSTIN, '') 
                         AS GSTIN, ISNULL(LEDGERS.Acc_add, '') AS ADDRESS, ISNULL(LEDGERS.Acc_shippingadd, '') AS SHIPPINGADDRESS, ISNULL(LEDGERS.Acc_remarks, '') AS REMARKS, LEDGERS.Acc_code AS CODE, 
                         ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN, ISNULL(DELIVERYCITYMASTER.city_name, '') AS DELIVERYAT, LEDGERS.Acc_TYPE AS TYPE, ISNULL(LEDGERS.ACC_DELIVERYPINCODE, '') 
                         AS DELIVERYPINNO, ISNULL(LEDGERS.ACC_UPI, '') AS UPI, ISNULL(LEDGERS.ACC_MSMENO, '') AS MSME, ISNULL(LEDGERS.ACC_COMMISSION, 0) AS BROKERAGECOMM, ISNULL(LEDGERS.ACC_WARNING, '') 
                         AS WARNINGTEXT, ISNULL(LEDGERS.ACC_GSTINVERIFIED, 0) AS GSTVERIFIED, ISNULL(LEDGERS.ACC_MSMETYPE, '') AS MSMETYPE, ISNULL(LEDGERS.ACC_EXMILLLESS, 0) AS EXMILLLESS, 
                         ISNULL(LEDGERS.ACC_LOCKDAYS, 0) AS LOCKDAYS ", "", " LEDGERS INNER JOIN
                         GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN
						 SALESMANMASTER ON SALESMANMASTER.SALESMAN_ID = LEDGERS.ACC_SALESMANID LEFT OUTER JOIN
                         CITYMASTER AS DELIVERYCITYMASTER ON LEDGERS.ACC_DELIVERYATID = DELIVERYCITYMASTER.city_id LEFT OUTER JOIN
                         LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN
						 LEDGERS AS TRANSLEDGERS ON TRANSLEDGERS.Acc_id = LEDGERS.ACC_TRANSID LEFT OUTER JOIN
                         COUNTRYMASTER ON LEDGERS.Acc_countryid = COUNTRYMASTER.country_id LEFT OUTER JOIN
                         STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN
                         CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id LEFT OUTER JOIN 
						 AREAMASTER ON AREAMASTER.area_id = LEDGERS.Acc_areaid ", " AND LEDGERS.ACC_CMPNAME = '" & NAME & "' AND LEDGERS.ACC_YEARID = " & YearId)



            Dim DTTABLE As DataTable = OBJCMN.SEARCH("CITY_ID AS CITYID", "", "CITYMASTER ", "AND CITY_NAME = '" & DTLEDGER.Rows(0).Item("CITYNAME") & "' AND CITY_YEARID = " & TEMPYEARID)
            If DTTABLE.Rows.Count = 0 Then
                'ADD NEW CITYNAME
                Dim objyearmaster As New ClsYearMaster
                objyearmaster.savecity(DTLEDGER.Rows(0).Item("CITYNAME"), TEMPCMPID, 0, Userid, TEMPYEARID, " and city_name = '" & DTLEDGER.Rows(0).Item("CITYNAME") & "' AND CITY_YEARID = " & TEMPYEARID)
            End If


            DTTABLE = OBJCMN.SEARCH("CITY_ID AS CITYID", "", "CITYMASTER ", "AND CITY_NAME = '" & DTLEDGER.Rows(0).Item("DELIVERYAT") & "' AND CITY_YEARID = " & TEMPYEARID)
            If DTTABLE.Rows.Count = 0 Then
                'ADD NEW CITYNAME
                Dim objyearmaster As New ClsYearMaster
                objyearmaster.savecity(DTLEDGER.Rows(0).Item("DELIVERYAT"), TEMPCMPID, Locationid, Userid, TEMPYEARID, " and city_name = '" & DTLEDGER.Rows(0).Item("DELIVERYAT") & "' AND CITY_CMPID = " & TEMPCMPID & " AND CITY_LOCATIONID = " & Locationid & " AND CITY_YEARID = " & TEMPYEARID)
            End If


            DTTABLE = OBJCMN.SEARCH("AREA_ID AS AREAID", "", "AREAMASTER ", "AND AREA_NAME = '" & DTLEDGER.Rows(0).Item("AREA") & "' AND AREA_YEARID = " & TEMPYEARID)
            If DTTABLE.Rows.Count = 0 Then
                'ADD NEW AREA
                Dim objyearmaster As New ClsYearMaster
                objyearmaster.savearea(DTLEDGER.Rows(0).Item("AREA"), TEMPCMPID, Locationid, Userid, TEMPYEARID, " and AREA_name = '" & DTLEDGER.Rows(0).Item("AREA") & "' AND AREA_CMPID = " & TEMPCMPID & " AND AREA_LOCATIONID = " & Locationid & " AND AREA_YEARID = " & TEMPYEARID)
            End If


            DTTABLE = OBJCMN.SEARCH("STATE_ID AS STATEID", "", "STATEMASTER ", "AND STATE_NAME = '" & DTLEDGER.Rows(0).Item("STATE") & "' AND STATE_YEARID = " & TEMPYEARID)
            If DTTABLE.Rows.Count = 0 Then
                'ADD NEW STATE
                Dim objyearmaster As New ClsYearMaster
                objyearmaster.savestate(DTLEDGER.Rows(0).Item("STATE"), TEMPCMPID, Locationid, Userid, TEMPYEARID, " and STATE_name = '" & DTLEDGER.Rows(0).Item("STATE") & "' AND STATE_YEARID = " & TEMPYEARID)
            End If


            DTTABLE = OBJCMN.SEARCH("COUNTRY_ID AS COUNTRYID", "", "COUNTRYMASTER ", "AND COUNTRY_NAME = '" & DTLEDGER.Rows(0).Item("COUNTRY") & "' AND COUNTRY_YEARID = " & TEMPYEARID)
            If DTTABLE.Rows.Count = 0 Then
                'ADD NEW COUNTRY
                Dim objyearmaster As New ClsYearMaster
                objyearmaster.savecountry(DTLEDGER.Rows(0).Item("COUNTRY"), TEMPCMPID, Locationid, Userid, TEMPYEARID, " and COUNTRY_name = '" & DTLEDGER.Rows(0).Item("COUNTRY") & "' AND COUNTRY_YEARID = " & TEMPYEARID)
            End If





            ALPARAVAL.Add(NAME)
            ALPARAVAL.Add("")   'NAME
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("GROUPNAME"))
            ALPARAVAL.Add(0)    'OPBAL
            ALPARAVAL.Add("Cr.")
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("INTPER")))    'INTPER
            ALPARAVAL.Add(0)    'PROFITPER
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("ADD1"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("ADD2"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("AREA"))   'AREA
            ALPARAVAL.Add("")   'STD
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("CITYNAME"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("PINCODE"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("STATE"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("COUNTRY"))
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("CRDAYS")))
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("CRLIMIT")))    'CRLIMIT
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("RESINO"))   'RESI
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("ALTNO"))   'ALT
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("PHONENO"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("MOBILENO"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("WHATSAPPNO"))   'WHATSAPPNO
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("FAX"))   'FAX
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("WEBSITE"))   'WEBSITE
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("EMAIL"))   'EMAIL

            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("TRANSPORT"))   'TRANS
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("BROKER"))   'AGENT
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("COMMISSION")))    'AGENTCOM
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("DISCOUNT")))    'DISC
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("CASHDISC")))    'CDPER
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("KMS")))    'KMS

            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("PANNO"))   'PAN
            ALPARAVAL.Add("")   'EXISE
            ALPARAVAL.Add("")   'RANGE
            ALPARAVAL.Add("")   'ADDLESS
            ALPARAVAL.Add("")   'CST
            ALPARAVAL.Add("")   'TIN
            ALPARAVAL.Add("")   'ST
            ALPARAVAL.Add("")   'VAT
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("GSTIN"))
            ALPARAVAL.Add("")   'REGISTER
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("ADDRESS"))
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("SHIPPINGADDRESS"))   'SHIPADD
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("REMARKS"))   'REMARKS
            ALPARAVAL.Add("")   'PARTYBANK
            ALPARAVAL.Add("")   'ACCTYPE
            ALPARAVAL.Add("")   'ACCNO
            ALPARAVAL.Add("")   'IFSCCODE
            ALPARAVAL.Add("")   'BRANCH
            ALPARAVAL.Add("")   'BANKCITY
            ALPARAVAL.Add("")   'GROUPOFCOMPANIES
            ALPARAVAL.Add(0)    'BLOCKED
            ALPARAVAL.Add(0)    'RCM
            ALPARAVAL.Add(0)    'OVERSEAS
            ALPARAVAL.Add(0)    'HOLDFORAPPROVAL
            ALPARAVAL.Add(TEMPCMPID)
            ALPARAVAL.Add(0)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(TEMPYEARID)
            ALPARAVAL.Add(0)    'TRANSFER
            ALPARAVAL.Add(NAME) 'CODE
            ALPARAVAL.Add("")    'PRICELIST
            ALPARAVAL.Add("")    'PACKINGTYPE
            ALPARAVAL.Add("")    'TERM
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("SALESMAN"))    'SALESMAN
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("DELIVERYAT"))    'DELIVERYAT (SAME AS CITY WHILE UPLOADING)



            'TDS
            '*******************************
            ALPARAVAL.Add(0)    'ISTDS
            ALPARAVAL.Add("")   'DEDUCTEETYPER
            ALPARAVAL.Add("")   'TDSFORM
            ALPARAVAL.Add("")   'TDSCOMPANY
            ALPARAVAL.Add(0)    'ISLOWER

            ALPARAVAL.Add("")   'SECTION
            ALPARAVAL.Add(Val(0))   'TDSRATE
            ALPARAVAL.Add(0)    'TDSPER
            ALPARAVAL.Add(0) 'SURCHARGE
            ALPARAVAL.Add(0) 'LIMIT
            '*******************************

            ALPARAVAL.Add(0)    'TDSAC
            ALPARAVAL.Add("NON SEZ")    'SEZTYPE
            ALPARAVAL.Add("")   'NATUREOFPAY
            If DTLEDGER.Rows(0).Item("TYPE") <> "" Then ALPARAVAL.Add(DTLEDGER.Rows(0).Item("TYPE")) Else ALPARAVAL.Add("ACCOUNTS")   'TYPE
            ALPARAVAL.Add("")   'CALC
            ALPARAVAL.Add(0)                        'POMNADTE
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("DELIVERYPINNO"))       'DELIVERYPINCODE (SAME AS PINCODE WHILE UPLOADING)
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("UPI"))   'UPI
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("MSME"))   'MSME
            ALPARAVAL.Add(0)    'TCS
            ALPARAVAL.Add("")   'TDSDEDUCTEDAC
            ALPARAVAL.Add(0)    'TDSONGTOTAL
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("BROKERAGECOMM")))    'COMMISSION
            ALPARAVAL.Add("")   'DISTRICT
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("WARNINGTEXT"))   'WARNING TEXT
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("GSTVERIFIED"))   'GSTINVERIFIED
            ALPARAVAL.Add(0)   'PARTYTDS
            ALPARAVAL.Add(0)   'RD
            ALPARAVAL.Add(DTLEDGER.Rows(0).Item("MSMETYPE"))   'MSME TYPE
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("EXMILLLESS")))   'EXMILL
            ALPARAVAL.Add(0)   'BILLTOID
            ALPARAVAL.Add(Val(DTLEDGER.Rows(0).Item("LOCKDAYS")))   'LOCKDAYS

            'CONTACT DETAILS
            '*******************************
            ALPARAVAL.Add("")   'FOR NAME
            ALPARAVAL.Add(0)   'FOR DESIGNATION
            ALPARAVAL.Add("")   'FOR MOBILE
            ALPARAVAL.Add("")   'FOR EMAIL



            OBJSM.alParaval = ALPARAVAL
            Dim INTRES As Integer = OBJSM.SAVE()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ' Returns an array of bill numbers split by comma
    Function GetBillNumbers(billNos As String) As String()
        If String.IsNullOrEmpty(billNos) Then
            Return New String() {}
        End If
        Return billNos.Split(","c).Select(Function(s) s.Trim()).ToArray()
    End Function


    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDISSUE.RowCount = 0
                TEMPCHQENTNO = Val(tstxtbillno.Text)
                If TEMPCHQENTNO > 0 Then
                    EDIT = True
                    ChqEnteries_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            GRIDISSUE.RowCount = 0
LINE1:
            TEMPCHQENTNO = Val(TXTENTERYNO.Text) - 1
            If TEMPCHQENTNO > 0 Then
                EDIT = True
                ChqEnteries_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDISSUE.RowCount = 0 And TEMPCHQENTNO > 1 Then
                TXTENTERYNO.Text = TEMPCHQENTNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
LINE1:
            TEMPCHQENTNO = Val(TXTENTERYNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTENTERYNO.Text.Trim
            CLEAR()
            If Val(TXTENTERYNO.Text) - 1 >= TEMPCHQENTNO Then
                EDIT = True
                ChqEnteries_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDISSUE.RowCount = 0 And TEMPCHQENTNO < MAXNO Then
                TXTENTERYNO.Text = TEMPCHQENTNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTBANKNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTBANKNAME.Validating
        Try
            If cmbaccname.Text.Trim <> "" And cmbname.Text.Trim <> "" And Val(txtamt.Text.Trim) > 0 Then
                fillgrid()
            ElseIf cmbaccname.Text.Trim = "" Then
                MsgBox("Enter Bank Name", MsgBoxStyle.Critical)
                cmbaccname.Focus()
                Exit Sub
            ElseIf cmbname.Text.Trim = "" Then
                MsgBox("Enter Name", MsgBoxStyle.Critical)
                cmbname.Focus()
                Exit Sub
            ElseIf Val(txtamt.Text.Trim) <= 0 Then
                MsgBox("Enter Amt....", MsgBoxStyle.Critical)
                txtamt.Focus()
                Exit Sub

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()

        GRIDISSUE.Enabled = True


        If GRIDDOUBLECLICK = False Then
            GRIDISSUE.Rows.Add(Val(txtsrno.Text.Trim), cmbaccname.Text.Trim, cmbname.Text.Trim, CMBSELLERNAME.Text.Trim, TXTCHQNO.Text.Trim, Format(DTCHQDATE.Value.Date, "dd/MM/yyyy"), Format(Val(txtamt.Text.Trim), "0.00"), cmbpaytype.Text.Trim, TXTBANKNAME.Text.Trim, TXTBILLNO.Text.Trim, TXTADJAMOUNT.Text.Trim, txtremamount.Text.Trim, TXTTDSACC.Text.Trim, TXTTDSAMT.Text.Trim)
            getmaxno()
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDISSUE.Item(GSRNO.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDISSUE.Item(GACCNAME.Index, TEMPROW).Value = cmbaccname.Text.Trim
            GRIDISSUE.Item(GPARTYNAME.Index, TEMPROW).Value = cmbname.Text.Trim
            GRIDISSUE.Item(GSELLERNAME.Index, TEMPROW).Value = CMBSELLERNAME.Text.Trim
            GRIDISSUE.Item(GCHQNO.Index, TEMPROW).Value = TXTCHQNO.Text.Trim
            GRIDISSUE.Item(GCHQDATE.Index, TEMPROW).Value = Format(DTCHQDATE.Value.Date, "dd/MM/yyyy")
            GRIDISSUE.Item(GCHQAMT.Index, TEMPROW).Value = Format(Val(txtamt.Text.Trim), "0.00")
            GRIDISSUE.Item(GBANKNAME.Index, TEMPROW).Value = TXTBANKNAME.Text.Trim
            GRIDISSUE.Item(GPAYTYPE.Index, TEMPROW).Value = cmbpaytype.Text.Trim
            GRIDISSUE.Item(GBILLNO.Index, TEMPROW).Value = TXTBILLNO.Text.Trim
            GRIDISSUE.Item(GAMOUNT.Index, TEMPROW).Value = TXTADJAMOUNT.Text.Trim
            GRIDISSUE.Item(gremamt.Index, TEMPROW).Value = txtremamount.Text.Trim
            GRIDISSUE.Item(GTDSACC.Index, TEMPROW).Value = TXTTDSACC.Text.Trim
            GRIDISSUE.Item(GTDSAMT.Index, TEMPROW).Value = TXTTDSAMT.Text.Trim


            GRIDDOUBLECLICK = False

        End If
        total()


        GRIDISSUE.FirstDisplayedScrollingRowIndex = GRIDISSUE.RowCount - 1


        TXTCHQNO.Clear()
        txtamt.Clear()
        TXTBANKNAME.Clear()
        cmbaccname.Text = ""
        cmbname.Text = ""
        TXTADJAMOUNT.Clear()
        TXTBILLNO.Clear()
        cmbpaytype.Text = ""
        DTCHQDATE.Value = Now.Date
        CMBSELLERNAME.Text = ""
        txtremamount.Clear()
        TXTTDSACC.Clear()
        TXTTDSAMT.Clear()
        'txtPartyMtrs.Clear()
        'txtCheckPcs.Clear()
        'TXTBARCODE.Clear()
        If GRIDISSUE.RowCount > 0 Then
            txtsrno.Text = Val(GRIDISSUE.Rows(GRIDISSUE.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDINVOICE.RowCount) + 1
        Else
            getmaxno()
        End If
        txtsrno.Focus()
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

    Sub total()
        Try
            'If GRIDISSUE.RowCount > 0 Then
            LBLTOTALAMT.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDISSUE.Rows
                If ROW.Cells(GSRNO.Index).Value <> Nothing Then
                    LBLTOTALAMT.Text = Format(Val(LBLTOTALAMT.Text) + Val(ROW.Cells(GCHQAMT.Index).EditedFormattedValue), "0.00")
                End If
            Next
            txtinwords.Text = CurrencyToWord(LBLTOTALAMT.Text)
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDISSUE.CurrentRow.Index >= 0 And GRIDISSUE.Item(GSRNO.Index, GRIDISSUE.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDISSUE.Item(GSRNO.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                cmbaccname.Text = GRIDISSUE.Item(GACCNAME.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                cmbname.Text = GRIDISSUE.Item(GPARTYNAME.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                CMBSELLERNAME.Text = GRIDISSUE.Item(GSELLERNAME.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTCHQNO.Text = GRIDISSUE.Item(GCHQNO.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                DTCHQDATE.Text = GRIDISSUE.Item(GCHQDATE.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                txtamt.Text = GRIDISSUE.Item(GCHQAMT.Index, GRIDISSUE.CurrentRow.Index).Value
                cmbpaytype.Text = GRIDISSUE.Item(GPAYTYPE.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTBANKNAME.Text = GRIDISSUE.Item(GBANKNAME.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTBILLNO.Text = GRIDISSUE.Item(GBILLNO.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTADJAMOUNT.Text = GRIDISSUE.Item(GAMOUNT.Index, GRIDISSUE.CurrentRow.Index).Value
                txtremamount.Text = GRIDISSUE.Item(gremamt.Index, GRIDISSUE.CurrentRow.Index).Value
                TXTTDSACC.Text = GRIDISSUE.Item(GTDSACC.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTTDSAMT.Text = GRIDISSUE.Item(GTDSAMT.Index, GRIDISSUE.CurrentRow.Index).Value

                TEMPROW = GRIDISSUE.CurrentRow.Index
                txtsrno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDISSUE_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDISSUE.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDISSUE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDISSUE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDISSUE.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbMERCHANT.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDISSUE.Rows.RemoveAt(GRIDISSUE.CurrentRow.Index)
                total()
                getsrno(GRIDISSUE)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtamt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtamt.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try
            If EDIT = True Then

                Dim TEMPMSG As Integer = MsgBox("Wish to Delete Cheque Entry?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                Dim ALPARAVAL As New ArrayList
                Dim OBJEMB As New ClsChqEntries

                ALPARAVAL.Add(TEMPCHQENTNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(YearId)
                OBJEMB.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJEMB.Delete()
                MsgBox("Cheque Entry Deleted Succesfully")
                CLEAR()
                EDIT = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJPAYDTLS As New ChqEnteriesDetail
            OBJPAYDTLS.MdiParent = MDIMain
            OBJPAYDTLS.Show()
            OBJPAYDTLS.BringToFront()
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call CMDOK_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPCHQENTNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal CHQENTNO As Integer)
        Try
            TEMPMSG = MsgBox("Wish to Print Cheque Enteries Register.....?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                Dim OBJPUR As New ChqEnteriesDesign
                OBJPUR.MdiParent = MDIMain
                OBJPUR.FRMSTRING = "CHQENTPRINT"
                OBJPUR.WHERECLAUSE = "{CHQENTERIES.CHQ_NO}=" & Val(CHQENTNO) & " and {CHQENTERIES.CHQ_YEARID}=" & YearId
                OBJPUR.Show()
            End If

            TEMPMSG = MsgBox("Wish to Print Cheque Enteries Pay-Slip.....?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                Dim OBJPUR As New ChqEnteriesDesign
                OBJPUR.MdiParent = MDIMain
                OBJPUR.WHERECLAUSE = "{CHQENTERIES.CHQ_NO}=" & Val(CHQENTNO) & " and {CHQENTERIES.CHQ_YEARID}=" & YearId
                OBJPUR.Show()
            End If
            'Else
            '    Dim OBJPUR As New ChqEnteriesDesign
            '    OBJPUR.MdiParent = MDIMain
            '    'OBJPUR.FRMSTRING = "CHQENTPRINT"
            '    OBJPUR.WHERECLAUSE = "{CHQENTERIES.CHQ_NO}=" & Val(CHQENTNO) & " and {CHQENTERIES.CHQ_YEARID}=" & YearId
            '    OBJPUR.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbaccname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbaccname.Enter
        Try
            'OPEN BANK A/C AND BANK OD A/C
            If cmbaccname.Text.Trim = "" Then fillledger(cmbaccname, EDIT, " and (groupmaster.group_SECONDARY = 'BANK A/C' OR groupmaster.group_SECONDARY = 'BANK OD A/C' OR groupmaster.group_SECONDARY = 'CASH IN HAND') and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbaccname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbaccname.KeyDown
        If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
        If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                'OBJLEDGER.STRSEARCH = " and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBACCCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            'If cmbname.Text.Trim <> "" Then ledgervalidate(cmbname, CMBACCCODE, e, Me, txtadd, " and (groupmaster.group_SECONDARY = 'Sundry Creditors' or groupmaster.group_SECONDARY = 'Indirect Expenses' or groupmaster.group_SECONDARY = 'Direct Expenses') and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)
            If cmbname.Text.Trim <> "" Then ledgervalidate(cmbname, CMBACCCODE, e, Me, txtadd, " and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbaccname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbaccname.Validating
        Try
            If cmbaccname.Text.Trim <> "" Then ledgervalidate(cmbaccname, CMBACCCODE, e, Me, txtadd, " AND (GROUPMASTER.group_SECONDARY = 'BANK A/C' OR GROUPMASTER.group_SECONDARY = 'BANK OD A/C' OR GROUPMASTER.group_SECONDARY = 'CASH IN HAND') AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBANKNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTBANKNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJPARTYBANK As New SelectPartyBank
                OBJPARTYBANK.FRMSTRING = "PARTYBANK"
                OBJPARTYBANK.ShowDialog()
                If OBJPARTYBANK.TEMPNAME <> "" Then TXTBANKNAME.Text = OBJPARTYBANK.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbpaytype_Validated(sender As Object, e As EventArgs) Handles cmbpaytype.Validated
        Try
            If cmbname.Text.Trim <> "" And cmbpaytype.Text.Trim = "Against Bill" Then


                Dim OBJSELECTBILL As New SelectAdjustBills
                If cmbname.Text = "ABHEE FABRICS LLP" Then OBJSELECTBILL.CMPNAME = CMBSELLERNAME.Text.Trim Else OBJSELECTBILL.CMPNAME = cmbname.Text.Trim
                OBJSELECTBILL.AMOUNT = txtamt.Text.Trim
                OBJSELECTBILL.ShowDialog()
                Dim DTBILLS As DataTable = OBJSELECTBILL.DTBILLS
                Dim SELECTEDBILLNO As String = ""
                Dim SELECTEDAMOUNT As String = ""
                Dim SELECTEDTDSAMT As String = ""
                For Each DTROW As DataRow In DTBILLS.Rows
                    If SELECTEDBILLNO = "" Then
                        SELECTEDBILLNO = DTROW("BILLNO")
                        SELECTEDAMOUNT = Val(DTROW("ADJUSTAMT"))
                        SELECTEDTDSAMT = Val(DTROW("TDS"))
                    Else
                        SELECTEDBILLNO = DTROW("BILLNO") & "|" & SELECTEDBILLNO
                        SELECTEDAMOUNT = Val(DTROW("ADJUSTAMT")) & "|" & SELECTEDAMOUNT
                        SELECTEDTDSAMT = Val(DTROW("TDS")) & "|" & SELECTEDTDSAMT
                    End If
                Next
                txtremamount.Text = OBJSELECTBILL.RemAmount

                TXTADJAMOUNT.Text = SELECTEDAMOUNT
                TXTTDSAMT.Text = SELECTEDTDSAMT
                TXTBILLNO.Text = SELECTEDBILLNO
                TXTTDSACC.Text = OBJSELECTBILL.CMBTDSDEDUCTEDAC.Text

            Else
                MsgBox("Select Name")
                cmbname.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELLERNAME_Enter(sender As Object, e As EventArgs)
        Try
            If CMBSELLERNAME.Text.Trim = "" Then FILLNAME(CMBSELLERNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELLERNAME_Validating(sender As Object, e As CancelEventArgs)
        Try
            If CMBSELLERNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBSELLERNAME, CMBACCCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub CMBSELLERNAME_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = "  And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')   AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBSELLERNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub CMBBUYER_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBSELLERNAME.KeyDown
    '    Try
    '        If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
    '        If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

    '        If e.KeyCode = Keys.F1 Then
    '            Dim OBJLEDGER As New SelectLedger
    '            OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'"
    '            OBJLEDGER.ShowDialog()
    '            If OBJLEDGER.TEMPNAME <> "" Then CMBSELLERNAME.Text = OBJLEDGER.TEMPNAME
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
End Class