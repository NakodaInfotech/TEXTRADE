Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms
Imports BL
Imports DevExpress.DataProcessing.InMemoryDataProcessor
Imports DevExpress.XtraRichEdit.Model
Imports iTextSharp
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

        txtsrno.Clear()
        cmbaccname.Text = ""
        cmbname.Text = ""
        TXTCHQNO.Clear()
        txtremarks.Clear()
        CMBBUYER.Text = ""
        CMBSELLER.Text = ""
        LBLTOTALAMT.Text = 0.0
        GRIDISSUE.RowCount = 0
        GRIDDOUBLECLICK = False
        txtinwords.Clear()
        If ClientName = "ABHEE" Then getmaxgrid_no()
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(CHQ_NO),0) + 1 ", " CHQENTERIES ", " AND CHQ_cmpid=" & CmpId & " and CHQ_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTENTERYNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Function errorvalid() As Boolean
        Try
            Dim bln As Boolean = True

            If GRIDISSUE.RowCount = 0 Then
                EP.SetError(GRIDISSUE, "Fill Item Details")
                bln = False
            End If
            If CMBSELLER.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, " Please Fill Seller Name ")
                bln = False
            End If
            If CMBBUYER.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, " Please Fill Buyer Name ")
                bln = False
            End If

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
            If CMBSELLER.Text.Trim = "" Then FILLNAME(CMBSELLER, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
            If CMBBUYER.Text.Trim = "" Then FILLNAME(CMBBUYER, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
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

                    Dim billNos() As String = ROW.Cells(GAMOUNT.Index).Value.ToString().Split("|"c)
                    For i As Integer = 0 To billNos.Length - 1
                        If ROW.Cells(GAMOUNT.Index).Value.ToString() <> Nothing Then
                            If pgridsrno = "" Then

                                pgridsrno = 1
                                paytype = ROW.Cells(GPAYTYPE.Index).Value.ToString()
                                ' billINITIALS = ""
                                narr = ""
                                'amt = Val(row.Cells(gamt.Index).Value)
                                AMTPAID = ""
                                EXTRAAMT = "" ' row.Cells(GEXTRAAMT.Index).Value
                                RETURNAMT = "" 'row.Cells(GRETURN.Index).Value
                                BALANCE = "" 'row.Cells(GBALANCE.Index).Value
                            Else
                                pgridsrno = pgridsrno & "|" & pgridsrno + 1
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

                    alparaval.Add(pgridsrno)
                    alparaval.Add(paytype)
                    alparaval.Add(ROW.Cells(GBILLNO.Index).Value.ToString())
                    alparaval.Add(narr)
                    alparaval.Add(ROW.Cells(GAMOUNT.Index).Value.ToString())
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
            Next

            SAVERECIEPT()
            CLEAR()
            DTENTERYDATE.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub getmaxgrid_no()
        Dim DTTABLE As New DataTable
        If CMBBUYER.Text = "RECEIPT" Then
            DTTABLE = getmax(" isnull(max(RECEIPT_no),0) + 1 ", "RECEIPTMASTER", " AND RECEIPT_cmpid=" & CmpId & " and RECEIPT_locationid=" & Locationid & " and RECEIPT_yearid=" & YearId)
            If DTTABLE.Rows.Count > 0 Then
                txtsrno.Text = DTTABLE.Rows(0).Item(0)
            End If
        ElseIf CMBBUYER.Text = "PAYMENT" Then
            DTTABLE = getmax(" isnull(max(PAYMENT_no),0) + 1 ", "PAYMENTMASTER", " AND PAYMENT_cmpid=" & CmpId & " and PAYMENT_locationid=" & Locationid & " and PAYMENT_yearid=" & YearId)
            If DTTABLE.Rows.Count > 0 Then
                txtsrno.Text = DTTABLE.Rows(0).Item(0)
            End If
        End If
    End Sub
    Sub SAVERECIEPT()
        'WE NEED TO CREATE THE SAME ORDER IN ABHEE FABRICS LLP COMPANY
        'IF BUYER IS ABHEE FABRICS LLP THEN WE NEED TO CREATE PURCHASE INVOICE IN THE NAME OF SELLER IN ABHEE FABRICS LLP COMPANY
        Dim OBJCMN As New ClsCommon
        Dim TEMPYEARID, TEMPCMPID, TEMPLEDGERID, TEMPITEMID As Integer
        Dim DTNAME As DataTable = OBJCMN.SEARCH("ISNULL(ACC_NAME,'') AS NAME", "", " LEDGERS", " AND LEDGERS.ACC_CMPNAME = '" & CMBBUYER.Text.Trim & "' AND LEDGERS.ACC_YEARID = " & YearId)
        If DTNAME.Rows.Count > 0 AndAlso DTNAME.Rows(0).Item("NAME") = "ABHEE FABRICS LLP" Then

            'CREATE PURCHASE INVOICE IN ABHEE FABRICS LLP
            'FIRST GET THE CMPID AND YEARID OF ABHEE FABRICS LLP
            Dim TEMPDT As DataTable = OBJCMN.SEARCH(" TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMPMASTER.CMP_DISPLAYEDNAME = 'ABHEE FABRICS LLP' ORDER BY YEAR_STARTDATE DESC")
            If TEMPDT.Rows.Count > 0 Then
                TEMPCMPID = TEMPDT.Rows(0).Item("CMPID")
                TEMPYEARID = TEMPDT.Rows(0).Item("YEARID")
            Else
                GoTo NEXTLINE
            End If

            'CHECK WHETHER SELLER NAME IS PRESENT OR NOT, IF NOT PRESENT THEN ADD NEW 
            TEMPDT = OBJCMN.SEARCH("ACC_ID AS LEDGERID ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & CMBSELLER.Text.Trim & "' AND ACC_YEARID = " & TEMPYEARID)
            If TEMPDT.Rows.Count > 0 Then TEMPLEDGERID = TEMPDT.Rows(0).Item("LEDGERID") Else CREATELEDGER(CMBSELLER.Text.Trim, TEMPCMPID, TEMPYEARID)


            'CHECKING WHETHER ITEM IS PRESENT IN CURRENT YEAR OR NOT, IF NOT PRESENT THEN ADD NEW ITEM
            TEMPDT = OBJCMN.SEARCH("ITEM_ID AS ITEMID", "", " ITEMMASTER ", " AND ITEM_NAME = '" & row.Cells(gitemname.Index).Value & "' AND ITEM_YEARID = " & TEMPYEARID)
            If TEMPDT.Rows.Count > 0 Then TEMPITEMID = TEMPDT.Rows(0).Item("ITEMID") Else CREATEITEM(row.Cells(gitemname.Index).Value, TEMPCMPID, TEMPYEARID)

            GENERATEPI(Val(row.Index), TEMPCMPID, TEMPYEARID)
        End If
        '******************** END OF PO GENERATION CODE ***************************

NEXTLINE:
        MessageBox.Show("Details Added")
        CLEAR()
        Try
            If CMBBUYER.Text = "ABHEE FABRICS LLP" Then
                Dim DTTABLE1 As DataTable
                Dim alparaval1 As New ArrayList()

                ' Create a HashSet to track unique entries
                Dim addedEntries1 As New HashSet(Of String)

                For Each ROW As DataGridViewRow In GRIDISSUE.Rows
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
                        alparaval1.Add(ROW.Cells(GPARTYNAME.Index).Value.ToString())
                        alparaval1.Add(ROW.Cells(GCHQAMT.Index).Value)
                        alparaval1.Add(ROW.Cells(GCHQNO.Index).Value.ToString())
                        alparaval1.Add(txtremarks.Text.Trim())
                        alparaval1.Add("") 'TXTBILLREMARKS.Text.Trim()
                        alparaval1.Add("") 'TXTOURREMARKS.Text.Trim()
                        alparaval1.Add("") 'txtinwords.Text.Trim()
                        alparaval1.Add("") 'CHKRECO
                        alparaval1.Add(CmpId)
                        alparaval1.Add(Locationid)
                        alparaval1.Add(Userid)
                        alparaval1.Add(YearId)
                        alparaval1.Add(0)
                        alparaval1.Add("1") 'pgridsrno
                        alparaval1.Add("On Account") 'paytype
                        alparaval1.Add("") 'billINITIALS
                        alparaval1.Add("") '(narr)
                        alparaval1.Add(ROW.Cells(GCHQAMT.Index).Value)
                        alparaval1.Add("") 'AMTPAID
                        alparaval1.Add("") 'EXTRAAMT
                        alparaval1.Add("") 'RETURNAMT
                        alparaval1.Add("") 'BALANCE
                        alparaval1.Add("") 'dgridsrno
                        alparaval1.Add("") 'descledgername
                        alparaval1.Add("") 'descnarration
                        alparaval1.Add("") 'descamount
                        alparaval1.Add("") 'DESCPAYGRIDSRNO
                        alparaval1.Add("") 'DESCPAYBILLINITIALS
                        alparaval1.Add("") 'TXTSPECIALREMARKS.Text.Trim()

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
                Next
            End If
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
            GRIDISSUE.Rows.Add(Val(txtsrno.Text.Trim), cmbaccname.Text.Trim, cmbname.Text.Trim, TXTCHQNO.Text.Trim, Format(DTCHQDATE.Value.Date, "dd/MM/yyyy"), Format(Val(txtamt.Text.Trim), "0.00"), cmbpaytype.Text.Trim, TXTBANKNAME.Text.Trim, TXTBILLNO.Text.Trim, TXTADJAMOUNT.Text.Trim)
            getsrno(GRIDISSUE)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDISSUE.Item(GSRNO.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDISSUE.Item(GACCNAME.Index, TEMPROW).Value = cmbaccname.Text.Trim
            GRIDISSUE.Item(GPARTYNAME.Index, TEMPROW).Value = cmbname.Text.Trim
            GRIDISSUE.Item(GCHQNO.Index, TEMPROW).Value = TXTCHQNO.Text.Trim
            GRIDISSUE.Item(GCHQDATE.Index, TEMPROW).Value = Format(DTCHQDATE.Value.Date, "dd/MM/yyyy")
            GRIDISSUE.Item(GCHQAMT.Index, TEMPROW).Value = Format(Val(txtamt.Text.Trim), "0.00")
            GRIDISSUE.Item(GBANKNAME.Index, TEMPROW).Value = TXTBANKNAME.Text.Trim
            GRIDISSUE.Item(GPAYTYPE.Index, TEMPROW).Value = cmbpaytype.Text.Trim
            GRIDISSUE.Item(GBILLNO.Index, TEMPROW).Value = TXTBILLNO.Text.Trim
            GRIDISSUE.Item(GAMOUNT.Index, TEMPROW).Value = TXTADJAMOUNT.Text.Trim


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

        'txtPartyMtrs.Clear()
        'txtCheckPcs.Clear()
        'TXTBARCODE.Clear()
        If GRIDISSUE.RowCount > 0 Then
            txtsrno.Text = Val(GRIDISSUE.Rows(GRIDISSUE.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDINVOICE.RowCount) + 1
        Else
            txtsrno.Text = 1
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
                TXTCHQNO.Text = GRIDISSUE.Item(GCHQNO.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                DTCHQDATE.Text = GRIDISSUE.Item(GCHQDATE.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                txtamt.Text = GRIDISSUE.Item(GCHQAMT.Index, GRIDISSUE.CurrentRow.Index).Value
                cmbpaytype.Text = GRIDISSUE.Item(GPAYTYPE.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTBANKNAME.Text = GRIDISSUE.Item(GBANKNAME.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTBILLNO.Text = GRIDISSUE.Item(GBILLNO.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTADJAMOUNT.Text = GRIDISSUE.Item(GAMOUNT.Index, GRIDISSUE.CurrentRow.Index).Value

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
                OBJSELECTBILL.CMPNAME = cmbname.Text.Trim
                OBJSELECTBILL.AMOUNT = txtamt.Text.Trim
                OBJSELECTBILL.ShowDialog()
                Dim DTBILLS As DataTable = OBJSELECTBILL.DTBILLS
                Dim SELECTEDBILLNO As String = ""
                Dim SELECTEDAMOUNT As String = ""
                For Each DTROW As DataRow In DTBILLS.Rows
                    If SELECTEDBILLNO = "" Then
                        SELECTEDBILLNO = DTROW("BILLNO")
                        SELECTEDAMOUNT = Val(DTROW("ADJUSTAMT"))
                    Else
                        SELECTEDBILLNO = DTROW("BILLNO") & "|" & SELECTEDBILLNO
                        SELECTEDAMOUNT = SELECTEDAMOUNT & "|" & Val(DTROW("ADJUSTAMT"))
                    End If
                Next
                TXTBILLNO.Text = SELECTEDBILLNO
                TXTADJAMOUNT.Text = SELECTEDAMOUNT
                If OBJSELECTBILL.BILLNO <> "" Then SELECTEDBILLNO = OBJSELECTBILL.BILLNO

            Else
                MsgBox("Select Name")
                cmbname.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELLER_Enter(sender As Object, e As EventArgs) Handles CMBSELLER.Enter
        Try
            If CMBSELLER.Text.Trim = "" Then FILLNAME(CMBSELLER, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELLER_Validating(sender As Object, e As CancelEventArgs) Handles CMBSELLER.Validating
        Try
            If CMBSELLER.Text.Trim <> "" Then NAMEVALIDATE(CMBSELLER, CMBACCCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBUYER_Validating(sender As Object, e As CancelEventArgs) Handles CMBBUYER.Validating
        Try
            If CMBBUYER.Text.Trim = "" Then FILLNAME(CMBBUYER, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBUYER_Enter(sender As Object, e As EventArgs) Handles CMBBUYER.Enter
        Try
            If CMBBUYER.Text.Trim <> "" Then NAMEVALIDATE(CMBBUYER, CMBACCCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELLER_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBSELLER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = "  And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')   AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBSELLER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBBUYER_KeyDown(sender As Object, e As KeyEventArgs) Handles CMBBUYER.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then CMBBUYER.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class