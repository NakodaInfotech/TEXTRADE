Imports System.ComponentModel
Imports BL

Public Class LotTagging
    Dim OBJCMN As New ClsCommon
    Public EDIT As Boolean          'used for editing
    Public LOTTAGNO, TEMPCHGSROW As Integer     'used for poation no while editing
    Dim ALLOWMANUALPROGNO As Boolean = False
    Dim TEMPROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
    End Sub

    Private Sub LotTagging_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'GRN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            'If ClientName = "AVIS" Then ALLOWMANUALPROGNO = True
            FILLCMB()
            CLEAR()

            If EDIT = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJCMN1 As New ClsCommon()
                Dim DTA As DataTable = OBJCMN1.SEARCH("CAST(0 AS BIT) AS CHK, ISNULL(LOTTAGGING_DESC.LOTTAG_GRIDSRNO, 0) AS SRNO, ISNULL(LOTTAGGING_DESC.LOTTAG_CHK, 0) AS CHK, ISNULL(LOTTAGGING_DESC.LOTTAG_LRNO, '')  AS LRNO, ISNULL(LOTTAGGING_DESC.LOTTAG_LOTNO, '') AS LOTNO,  LOTTAGGING_desc.LOTTAG_LOTDATE AS DATE,  ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS ITEMNAME,  ISNULL(LOTTAGGING_DESC.LOTTAG_MTRS, 0) AS TOTALMTRS, ISNULL(LOTTAGGING_DESC.LOTTAG_BALMTRS, 0) AS BALMTRS, ISNULL(LOTTAGGING_DESC.LOTTAG_ADJMTRS, 0) AS ADJMTRS, ISNULL(LOTTAGGING_DESC.LOTTAG_FROMNO, 0) AS FROMNO,ISNULL(LOTTAGGING_DESC.LOTTAG_FROMTYPE, '') AS FROMTYPE,  ISNULL(LOTTAGGING.LOTTAG_NO, '') AS LOTTAGNO,  LOTTAGGING_DESC.LOTTAG_LOTDATE AS LOTDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME,  ISNULL(LOTTAGGING.LOTTAG_LBLPROGTOTALADJMTRS, 0) AS PROGTOTALADJMTRS, ISNULL(LOTTAGGING.LOTTAG_LBLPROGTOTALBALMTRS, 0) AS PROGTOTALBALMTRS,  ISNULL(LOTTAGGING.LOTTAG_LBLPROGTOTALMTRS, 0) AS PROGTOTALMTRS, ISNULL(LOTTAGGING.LOTTAG_LBLLOTTOTALADJMTRS, 0) AS LOTTOTALADJMTRS, ISNULL(LOTTAGGING.LOTTAG_LBLLOTTOTALBALMTRS, 0)  AS LOTTOTALBALMTRS, ISNULL(LOTTAGGING.LOTTAG_LBLLOTTOTALMTRS, 0) AS LOTTOTALMTRS , ISNULL(LOTTAGGING.LOTTAG_PENDINGLOT, 0) AS PENDINGLOT ", "", " LOTTAGGING INNER JOIN LOTTAGGING_DESC ON LOTTAGGING.LOTTAG_NO = LOTTAGGING_DESC.LOTTAG_NO AND LOTTAGGING.LOTTAG_YEARID = LOTTAGGING_DESC.LOTTAG_YEARID LEFT OUTER JOIN ITEMMASTER ON LOTTAGGING_DESC.LOTTAG_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS ON LOTTAGGING.LOTTAG_LEDGERID = LEDGERS.Acc_id ", " AND LOTTAGGING.LOTTAG_NO =  " & LOTTAGNO & " AND LOTTAGGING.LOTTAG_YEARID = " & YearId & " ORDER BY LOTTAG_GRIDSRNO ")
                If DTA.Rows.Count > 0 Then
                    GRIDLOTDETAILS.DataSource = DTA
                    GRIDLOT.FocusedRowHandle = GRIDLOT.RowCount - 1

                    TXTLOTTAGNO.ReadOnly = True
                    TXTLOTTAGNO.Text = Val(DTA.Rows(0).Item("LOTTAGNO"))
                    LOTTAGDATE.Value = Format(Convert.ToDateTime(DTA.Rows(0).Item("DATE")).Date, "dd/MM/yyyy")
                    CMBNAME.Text = DTA.Rows(0).Item("NAME")
                    CMBNAME.Enabled = False
                    If Val(DTA.Rows(0).Item("PENDINGLOT")) = True Then CHKPENDINGLOT.CheckState = CheckState.Checked Else CHKPENDINGLOT.CheckState = CheckState.Unchecked

                    'GRIDLOT.DataSource.Add(Val(("GSRNO")), ("CHK"), ("LRNO"), ("LOTNO"), Format(Convert.ToDateTime(("LOTDATE")).Date, "dd/MM/yyyy"), ("ITEMNAME"), Format(Val(("MTRS")), "0.00"), Format(Val(("BALMTRS")), "0.00"), Format(Val(("ADJMTRS")), "0.00"), ("FROMNO"), ("FROMTYPE"))

                End If

                Dim OBJCMN2 As New ClsCommon
                Dim DT2 As DataTable = OBJCMN2.SEARCH(" CAST(0 AS BIT) AS CHK,ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PSRNO, 0) AS SRNO, ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PCHK, 0) AS CHK,ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE, ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PMTRS, 0) AS MTRS, ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PBALMTRS, 0) AS BALMTRS, ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PADJMTRS, 0) AS ADJMTRS, ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PFROMNO, 0) AS FROMNO, ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PFROMSRNO, 0) AS FROMSRNO, ISNULL(LOTTAGGING_ADJDESC.LOTTAG_PFROMTYPE, '') AS FROMTYPE , LOTTAGGING_ADJDESC.LOTTAG_PROGDATE AS PROGDATE ", "", "  LOTTAGGING_ADJDESC LEFT OUTER JOIN  COLORMASTER ON LOTTAGGING_ADJDESC.LOTTAG_PCOLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON LOTTAGGING_ADJDESC.LOTTAG_PDESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON LOTTAGGING_ADJDESC.LOTTAG_PITEMID = ITEMMASTER.item_id ", "AND LOTTAGGING_ADJDESC.LOTTAG_NO = " & LOTTAGNO & " AND LOTTAGGING_ADJDESC.LOTTAG_YEARID = " & YearId)
                If DT2.Rows.Count > 0 Then
                    GRIDPROGRAMDETAILS.DataSource = DT2
                    GRIDPROGRAM.FocusedRowHandle = GRIDPROGRAM.RowCount - 1
                    'GRIDPROGRAM.DataSource.Rows.Add(Val(("PSRNO")), ("PCHK"), ("PITEMNAME"), ("DESIGN"), ("COLOR"), Format(Val(("PMTRS")), "0.00"), Format(Val(("PBALMTRS")), "0.00"), Format(Val(("PADJMTRS")), "0.00"), ("PFROMNO"), ("PFROMSRNO"), ("PFROMTYPE"))
                End If

                TOTAL()
            Else
                EDIT = False
                CLEAR()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " And GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        EP.Clear()
        CMBNAME.Text = ""
        CMBNAME.Enabled = True

        LOTTAGDATE.Value = Now.Date
        GRIDLOT.ClearColumnsFilter()
        GRIDPROGRAM.ClearColumnsFilter()

        GRIDLOTDETAILS.DataSource = Nothing
        GRIDPROGRAMDETAILS.DataSource = Nothing
        CHKPENDINGLOT.CheckState = CheckState.Unchecked
        GETMAXNO()
        LBLTOTALMTRS.Text = "0.00"
        LBLTOTALBALMTRS.Text = "0.00"
        LBLTOTALADJMTRS.Text = "0.00"
        LBLTOTALLOTMTRS.Text = "0.00"
        LBLTOTALLOTBALMTRS.Text = "0.00"
        LBLTOTALLOTADJMTRS.Text = "0.00"

        If ALLOWMANUALPROGNO = True Then
            TXTLOTTAGNO.ReadOnly = False
            TXTLOTTAGNO.BackColor = Color.LemonChiffon
        Else
            TXTLOTTAGNO.ReadOnly = True
            TXTLOTTAGNO.BackColor = Color.Linen
        End If
        CMBNAME.Focus()
    End Sub

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try
            Dim DTTABLE As DataTable

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alparaval As New ArrayList

            If TXTLOTTAGNO.ReadOnly = False Then
                alparaval.Add(Val(TXTLOTTAGNO.Text.Trim))
            Else
                alparaval.Add(0)
            End If
            alparaval.Add(LOTTAGDATE.Value.Date)
            alparaval.Add(CMBNAME.Text.Trim)
            alparaval.Add(Val(LBLTOTALLOTMTRS.Text))
            alparaval.Add(Val(LBLTOTALLOTBALMTRS.Text))
            alparaval.Add(Val(LBLTOTALLOTADJMTRS.Text))
            alparaval.Add(Val(LBLTOTALMTRS.Text))
            alparaval.Add(Val(LBLTOTALBALMTRS.Text))
            alparaval.Add(Val(LBLTOTALADJMTRS.Text))

            alparaval.Add(CmpId)
            alparaval.Add(Userid)
            alparaval.Add(YearId)


            Dim GRIDSRNO As String = ""
            Dim CHK As String = ""
            Dim LRNO As String = ""
            Dim LOTNO As String = ""
            Dim LOTDATE As String = ""
            Dim ITEMNAME As String = ""
            Dim TOTALMTRS As String = ""
            Dim BALMTRS As String = ""
            Dim ADJMTRS As String = ""
            Dim FROMNO As String = ""
            Dim FROMTYPE As String = ""

            For I As Integer = 0 To GRIDLOT.RowCount - 1
                Dim dtrow As DataRow = GRIDLOT.GetDataRow(I)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If GRIDSRNO = "" Then

                        GRIDSRNO = Val(dtrow("SRNO"))
                        CHK = dtrow("CHK")
                        LRNO = Val(dtrow("LRNO"))
                        LOTNO = dtrow("LOTNO")
                        LOTDATE = Format(dtrow("DATE"), "MM/dd/yyyy")
                        ITEMNAME = dtrow("ITEMNAME")
                        TOTALMTRS = Val(dtrow("TOTALMTRS"))
                        BALMTRS = Val(dtrow("BALMTRS"))
                        ADJMTRS = Val(dtrow("ADJMTRS"))
                        FROMNO = Val(dtrow("FROMNO"))
                        FROMTYPE = dtrow("FROMTYPE")

                    Else

                        GRIDSRNO = GRIDSRNO & "|" & Val(dtrow("SRNO"))
                        CHK = CHK & "|" & dtrow("CHK")
                        LRNO = LRNO & "|" & Val(dtrow("LRNO"))
                        LOTNO = LOTNO & "|" & dtrow("LOTNO")
                        LOTDATE = LOTDATE & "|" & Format(dtrow("DATE"), "MM/dd/yyyy")
                        ITEMNAME = ITEMNAME & "|" & dtrow("ITEMNAME")
                        TOTALMTRS = TOTALMTRS & "|" & Val(dtrow("TOTALMTRS"))
                        BALMTRS = BALMTRS & "|" & Val(dtrow("BALMTRS"))
                        ADJMTRS = ADJMTRS & "|" & Val(dtrow("ADJMTRS"))
                        FROMNO = FROMNO & "|" & Val(dtrow("FROMNO"))
                        FROMTYPE = FROMTYPE & "|" & dtrow("FROMTYPE")

                    End If
                End If
            Next
            alparaval.Add(GRIDSRNO)
            alparaval.Add(CHK)
            alparaval.Add(LRNO)
            alparaval.Add(LOTNO)
            alparaval.Add(LOTDATE)
            alparaval.Add(ITEMNAME)
            alparaval.Add(TOTALMTRS)
            alparaval.Add(BALMTRS)
            alparaval.Add(ADJMTRS)
            alparaval.Add(FROMNO)
            alparaval.Add(FROMTYPE)

            Dim PSRNO As String = ""
            Dim PCHK As String = ""
            Dim PROGDATE As String = ""
            Dim PITEMNAME As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim PMTRS As String = ""
            Dim PBALMTRS As String = ""
            Dim PADJMTRS As String = ""
            Dim PFROMNO As String = ""
            Dim PFROMSRNO As String = ""
            Dim PFROMTYPE As String = ""

            For I As Integer = 0 To GRIDPROGRAM.RowCount - 1
                Dim dtrow As DataRow = GRIDPROGRAM.GetDataRow(I)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If PSRNO = "" Then

                        PSRNO = Val(dtrow("SRNO"))
                        PCHK = dtrow("CHK")
                        PROGDATE = Format(dtrow("PROGDATE"), "MM/dd/yyyy")
                        PITEMNAME = dtrow("ITEMNAME")
                        DESIGN = dtrow("DESIGN")
                        COLOR = dtrow("SHADE")
                        PMTRS = Val(dtrow("MTRS"))
                        PBALMTRS = Val(dtrow("BALMTRS"))
                        PADJMTRS = Val(dtrow("ADJMTRS"))
                        PFROMNO = Val(dtrow("FROMNO"))
                        PFROMSRNO = Val(dtrow("FROMSRNO"))
                        PFROMTYPE = dtrow("FROMTYPE")

                    Else

                        PSRNO = PSRNO & "|" & Val(dtrow("SRNO"))
                        PCHK = PCHK & "|" & dtrow("CHK")
                        PROGDATE = PROGDATE & "|" & Format(dtrow("PROGDATE"), "MM/dd/yyyy")
                        PITEMNAME = PITEMNAME & "|" & dtrow("ITEMNAME")
                        DESIGN = DESIGN & "|" & dtrow("DESIGN")
                        COLOR = COLOR & "|" & dtrow("SHADE")
                        PMTRS = PMTRS & "|" & Val(dtrow("MTRS"))
                        PBALMTRS = PBALMTRS & "|" & Val(dtrow("BALMTRS"))
                        PADJMTRS = PADJMTRS & "|" & Val(dtrow("ADJMTRS"))
                        PFROMNO = PFROMNO & "|" & Val(dtrow("FROMNO"))
                        PFROMSRNO = PFROMSRNO & "|" & Val(dtrow("FROMSRNO"))
                        PFROMTYPE = PFROMTYPE & "|" & dtrow("FROMTYPE")

                    End If
                End If
            Next
            alparaval.Add(PSRNO)
            alparaval.Add(PCHK)
            alparaval.Add(PROGDATE)
            alparaval.Add(PITEMNAME)
            alparaval.Add(DESIGN)
            alparaval.Add(COLOR)
            alparaval.Add(PMTRS)
            alparaval.Add(PBALMTRS)
            alparaval.Add(PADJMTRS)
            alparaval.Add(PFROMNO)
            alparaval.Add(PFROMSRNO)
            alparaval.Add(PFROMTYPE)

            If CHKPENDINGLOT.CheckState = CheckState.Checked Then
                alparaval.Add(1)
            Else
                alparaval.Add(0)
            End If

            Dim OBJMATCH As New ClsLotTagging
            OBJMATCH.alParaval = alparaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                DTTABLE = OBJMATCH.SAVE()
                MessageBox.Show("Details Added")
                TXTLOTTAGNO.Text = DTTABLE.Rows(0).Item(0)
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alparaval.Add(LOTTAGNO)
                Dim IntResult As Integer = OBJMATCH.UPDATE()
                MsgBox("Details Updated")
            End If
            CLEAR()
            EDIT = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True
            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, " Please Select Name ")
                bln = False
            End If

            If GRIDLOT.RowCount = 0 Then
                EP.SetError(CMBNAME, " Please Select Lot")
                bln = False
            End If

            If GRIDPROGRAM.RowCount = 0 Then
                EP.SetError(CMBNAME, " Please Select Program")
                bln = False
            End If

            For I As Integer = 0 To GRIDLOT.RowCount - 1
                Dim LOTROW As DataRow = GRIDLOT.GetDataRow(I)
                LOTROW("ADJMTRS") = 0.0
            Next
            Dim PROGRAMUNADJMTRS As Double = 0
            Dim MAXALLOWEDADJMTRS As Double = 0

            For I As Integer = 0 To GRIDPROGRAM.RowCount - 1
                Dim ROW As DataRow = GRIDPROGRAM.GetDataRow(I)
                If Convert.ToBoolean(ROW("CHK")) = True Then
                    ROW("ADJMTRS") = 0.0
                    PROGRAMUNADJMTRS = Val(ROW("BALMTRS"))
                    For J As Integer = 0 To GRIDLOT.RowCount - 1
                        Dim LOTROW As DataRow = GRIDLOT.GetDataRow(J)
                        If Convert.ToBoolean(LOTROW("CHK")) = True Then
                            If Val(LOTROW("BALMTRS")) - Val(LOTROW("ADJMTRS")) > 0 Then
                                MAXALLOWEDADJMTRS = Format(Val(LOTROW("BALMTRS")) - Val(LOTROW("ADJMTRS")), "0.00")
                                If PROGRAMUNADJMTRS > MAXALLOWEDADJMTRS Then
                                    LOTROW("ADJMTRS") = Format(Val(LOTROW("ADJMTRS")) + Val(MAXALLOWEDADJMTRS), "0.00")
                                    PROGRAMUNADJMTRS = PROGRAMUNADJMTRS - MAXALLOWEDADJMTRS
                                    ROW("ADJMTRS") = Format(Val(ROW("ADJMTRS")) + Val(MAXALLOWEDADJMTRS), "0.00")
                                Else
                                    LOTROW("ADJMTRS") = Format(Val(LOTROW("ADJMTRS")) + Val(PROGRAMUNADJMTRS), "0.00")
                                    ROW("ADJMTRS") = Format(Val(ROW("ADJMTRS")) + Val(PROGRAMUNADJMTRS), "0.00")
                                    PROGRAMUNADJMTRS = 0
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                End If
            Next

            TOTAL()

            If Val(LBLTOTALLOTADJMTRS.Text.Trim) = 0 Then
                EP.SetError(CMBNAME, " Please Adjust Lot")
                bln = False
            End If

            If Val(LBLTOTALADJMTRS.Text.Trim) = 0 Then
                EP.SetError(CMBNAME, " Please Adjust Program")
                bln = False
            End If

            If Not datecheck(LOTTAGDATE.Value) Then
                EP.SetError(LOTTAGDATE, "Date Not In Current Accounting Year")
                bln = False
            End If
            Return bln

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub GETMAXNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(LOTTAG_no),0) + 1 ", "LOTTAGGING", " AND LOTTAG_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTLOTTAGNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub TOOLPREVIOUS_Click(sender As Object, e As EventArgs) Handles TOOLPREVIOUS.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            'GRIDLOT.RowCount = 0
            GRIDLOTDETAILS.DataSource = Nothing
            GRIDPROGRAMDETAILS.DataSource = Nothing
            LOTTAGNO = Val(TXTLOTTAGNO.Text) - 1
            If LOTTAGNO > 0 Then
                EDIT = True
                LotTagging_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub TOOLNEXT_Click(sender As Object, e As EventArgs) Handles TOOLNEXT.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            LOTTAGNO = Val(TXTLOTTAGNO.Text) + 1
            GETMAXNO()
            CLEAR()
            If Val(TXTLOTTAGNO.Text) - 1 >= LOTTAGNO Then
                EDIT = True
                LotTagging_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(sender As Object, e As EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" And EDIT = False Then FILLLOTNOGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLLOTNOGRID()
        Try
            'CHECKING NWAS MANDATE FOR THIS QUERY
            'Dim DT As DataTable = OBJCMN.SEARCH("*", "", " (SELECT 0 AS SRNO, CAST(0 AS BIT) AS CHK, CHECKINGMASTER.CHECK_LOTNO AS LOTNO , LRNO , ITEMNAME ,DESIGN,SHADE , TOTALMTRS,(TOTALMTRS - ISNULL(GRN.GRN_PROGRAMMTRS,0)) AS BALMTRS, DATE ,GRNNO AS FROMNO , GRNTYPE AS FROMTYPE , ISNULL(GRN.GRN_PROGRAMMTRS,0) AS PROGRAMMTRS, 0.0 AS ADJMTRS FROM CHECKINGMASTER INNER JOIN LEDGERS ON CHECKINGMASTER.CHECK_LEDGERID = LEDGERS.Acc_id INNER JOIN GRN ON GRN.GRN_NO = CHECKINGMASTER.CHECK_GRNNO AND GRN.GRN_TYPE = CHECKINGMASTER.CHECK_TYPE AND GRN.GRN_YEARID = CHECKINGMASTER.CHECK_YEARID INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = CHECKINGMASTER.CHECK_LOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = CHECKINGMASTER.CHECK_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0 WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(GRN.GRN_PROGRAMDONE,0) = 0 AND (LOT_VIEW.TOTALMTRS - LOT_VIEW.PROGRAMMTRS)>0 AND GRN.GRN_PLOTNO <> '' AND GRN.GRN_YEARID =  " & YearId & " UNION ALL  SELECT 0 AS SRNO, CAST(0 AS BIT) AS CHK, STOCKMASTER.SM_LOTNO AS LOTNO , LRNO , ITEMNAME ,DESIGN,SHADE , TOTALMTRS,(TOTALMTRS- ISNULL(STOCKMASTER.SM_PROGRAMMTRS,0)) AS  BALMTRS, DATE ,GRNNO AS FROMNO , GRNTYPE AS FROMTYPE  , ISNULL(STOCKMASTER.SM_PROGRAMMTRS,0) AS PROGRAMMTRS, 0 AS ADJMTRS FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = STOCKMASTER.SM_LOTNO AND LOT_VIEW.LRNO = CAST(STOCKMASTER.SM_REMARKS  AS VARCHAR(100)) AND LOT_VIEW.JOBBERLEDGERID = LEDGERS.ACC_ID AND LOT_VIEW.YEARID = STOCKMASTER.SM_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTCOMPLETED = 0  WHERE LEDGERS.ACC_CMPNAME ='" & CMBNAME.Text.Trim & "' AND ISNULL(STOCKMASTER.SM_PROGRAMDONE,0) = 0 AND SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_LOTNO <> '' AND (LOT_VIEW.TOTALMTRS - LOT_VIEW.PROGRAMMTRS)>0 AND STOCKMASTER.SM_YEARID = " & YearId & ") AS T", "")
            Dim DT As DataTable = OBJCMN.SEARCH("*", "", " (SELECT 0 AS SRNO, CAST(0 AS BIT) AS CHK, GRN.GRN_PLOTNO AS LOTNO , LRNO , ITEMNAME ,DESIGN,SHADE , TOTALMTRS, (TOTALMTRS - ISNULL(GRN.GRN_PROGRAMMTRS,0)) AS BALMTRS, DATE , GRNNO AS FROMNO , GRNTYPE AS FROMTYPE , ISNULL(GRN.GRN_PROGRAMMTRS,0) AS PROGRAMMTRS, 0.0 AS ADJMTRS FROM GRN INNER JOIN LEDGERS ON GRN.GRN_TOLEDGERID = LEDGERS.Acc_id INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = GRN.GRN_PLOTNO AND LOT_VIEW.JOBBERNAME = LEDGERS.ACC_CMPNAME AND LOT_VIEW.YEARID = GRN.GRN_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTNO <> '' AND LOT_VIEW.LOTCOMPLETED = 0  WHERE LEDGERS.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ISNULL(GRN.GRN_PROGRAMDONE,0) = 0 AND (LOT_VIEW.TOTALMTRS - LOT_VIEW.PROGRAMMTRS)>0 AND GRN.GRN_PLOTNO <> '' AND GRN.GRN_YEARID =  " & YearId & " UNION ALL  SELECT 0 AS SRNO, CAST(0 AS BIT) AS CHK, STOCKMASTER.SM_LOTNO AS LOTNO , LRNO , ITEMNAME ,DESIGN,SHADE , TOTALMTRS,(TOTALMTRS- ISNULL(STOCKMASTER.SM_PROGRAMMTRS,0)) AS  BALMTRS, DATE ,GRNNO AS FROMNO , GRNTYPE AS FROMTYPE  , ISNULL(STOCKMASTER.SM_PROGRAMMTRS,0) AS PROGRAMMTRS, 0 AS ADJMTRS FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO = LEDGERS.Acc_id INNER JOIN LOT_VIEW ON LOT_VIEW.LOTNO = STOCKMASTER.SM_LOTNO AND LOT_VIEW.LRNO = CAST(STOCKMASTER.SM_REMARKS  AS VARCHAR(100)) AND LOT_VIEW.JOBBERLEDGERID = LEDGERS.ACC_ID AND LOT_VIEW.YEARID = STOCKMASTER.SM_YEARID AND LOT_VIEW.BALMTRS > 0 AND LOT_VIEW.LOTCOMPLETED = 0  WHERE LEDGERS.ACC_CMPNAME ='" & CMBNAME.Text.Trim & "' AND ISNULL(STOCKMASTER.SM_PROGRAMDONE,0) = 0 AND SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_LOTNO <> '' AND (LOT_VIEW.TOTALMTRS - LOT_VIEW.PROGRAMMTRS)>0 AND STOCKMASTER.SM_YEARID = " & YearId & ") AS T", "")
            GRIDLOTDETAILS.DataSource = DT
            If DT.Rows.Count > 0 Then
                GRIDLOT.FocusedRowHandle = GRIDLOT.RowCount - 1
                GRIDLOT.TopRowIndex = GRIDLOT.RowCount - 15
            End If
            GETSRNO(GRIDLOT)

            Dim DT1 As DataTable = OBJCMN.SEARCH("0 AS SRNO, CAST(0 AS BIT) AS CHK,ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_TYPE, '') AS FROMTYPE , ALLPROGRAMMASTER.PROGRAM_DATE AS PROGDATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_NO, 0) AS FROMNO, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_GRIDSRNO, 0) AS FROMSRNO, ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS SHADE ,ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS,0) AS MTRS , ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS - ALLPROGRAMMASTER_DESC.PROGRAM_ADJMTRS,0) AS BALMTRS, 0.0 AS ADJMTRS ", "", "   ALLPROGRAMMASTER_DESC INNER JOIN ALLPROGRAMMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_NO = ALLPROGRAMMASTER.PROGRAM_NO  AND ALLPROGRAMMASTER_DESC.PROGRAM_TYPE = ALLPROGRAMMASTER.PROGRAM_TYPE AND ALLPROGRAMMASTER_DESC.PROGRAM_YEARID = ALLPROGRAMMASTER.PROGRAM_YEARID INNER JOIN LEDGERS ON ALLPROGRAMMASTER.PROGRAM_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN COLORMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_ITEMID = ITEMMASTER.item_id ", " AND LEDGERS.Acc_cmpname= '" & CMBNAME.Text.Trim & "' and ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS - ALLPROGRAMMASTER_DESC.PROGRAM_RECDPCS,0)  > 0 and ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS - ALLPROGRAMMASTER_DESC.PROGRAM_ADJMTRS,0)  > 0  AND (ALLPROGRAMMASTER_DESC.PROGRAM_STATUS = '' or ALLPROGRAMMASTER_DESC.PROGRAM_STATUS = '0') AND ALLPROGRAMMASTER.PROGRAM_YEARID = " & YearId & " ORDER BY FROMNO")
            GRIDPROGRAMDETAILS.DataSource = DT1
            If DT1.Rows.Count > 0 Then
                GRIDPROGRAM.FocusedRowHandle = GRIDPROGRAM.RowCount - 1
                GRIDPROGRAM.TopRowIndex = GRIDPROGRAM.RowCount - 15
            End If
            GETSRNO(GRIDPROGRAM)
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub CMDDELETE_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Dim IntResult As Integer
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Lot Tagging?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim alParaval As New ArrayList
                alParaval.Add(LOTTAGNO)
                alParaval.Add(YearId)

                Dim OBJPROGRAM As New ClsLotTagging()
                OBJPROGRAM.alParaval = alParaval
                IntResult = OBJPROGRAM.DELETE()
                MsgBox("Lot Tagging Deleted")
                EDIT = False
                CLEAR()

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub GETSRNO(ByRef GRID As DevExpress.XtraGrid.Views.Grid.GridView)
        Try
            For I As Integer = 0 To GRID.RowCount - 1
                Dim DTROW As DataRow = GRID.GetDataRow(I)
                DTROW("SRNO") = I + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PRINTTOOLSTRIP_Click(sender As Object, e As EventArgs) Handles PRINTTOOLSTRIP.Click
        Try
            PRINTREPORT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALLOTMTRS.Text = 0.0
            LBLTOTALLOTADJMTRS.Text = 0.0
            LBLTOTALLOTBALMTRS.Text = 0.0
            LBLTOTALMTRS.Text = 0.0
            LBLTOTALBALMTRS.Text = 0.0
            LBLTOTALADJMTRS.Text = 0.0

            For I As Integer = 0 To GRIDLOT.RowCount - 1
                Dim DTROW As DataRow = GRIDLOT.GetDataRow(I)
                ' If Convert.ToBoolean(DTROW("CHK")) = True Then
                'DTROW("BALMTRS") = 0.00
                LBLTOTALLOTMTRS.Text = Format(Val(LBLTOTALLOTMTRS.Text) + Val(DTROW("TOTALMTRS")), "0.00")
                    LBLTOTALLOTBALMTRS.Text = Format(Val(LBLTOTALLOTBALMTRS.Text) + Val(DTROW("BALMTRS")), "0.00")
                    LBLTOTALLOTADJMTRS.Text = Format(Val(LBLTOTALLOTADJMTRS.Text) + Val(DTROW("ADJMTRS")), "0.00")
                'DTROW("BALMTRS") = Format(Val(DTROW("TOTALMTRS")) - Val(DTROW("ADJMTRS")), "0.00")
                ' End If
            Next
            'For I As Integer = 0 To GRIDLOT.RowCount - 1
            '    Dim DTROW As DataRow = GRIDLOT.GetDataRow(I)
            '    DTROW("BALMTRS") = 0.00
            '    DTROW("BALMTRS") = Format(Val(DTROW("TOTALMTRS")) - Val(DTROW("ADJMTRS")), "0.00")
            'Next
            For I As Integer = 0 To GRIDPROGRAM.RowCount - 1
                Dim DTROW As DataRow = GRIDPROGRAM.GetDataRow(I)
                'If Convert.ToBoolean(DTROW("CHK")) = True Then
                LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(DTROW("MTRS")), "0.00")
                    LBLTOTALBALMTRS.Text = Format(Val(LBLTOTALBALMTRS.Text) + Val(DTROW("BALMTRS")), "0.00")
                    LBLTOTALADJMTRS.Text = Format(Val(LBLTOTALADJMTRS.Text) + Val(DTROW("ADJMTRS")), "0.00")
                ' End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objINVDTLS As New LotTaggingDetails
            objINVDTLS.MdiParent = MDIMain
            objINVDTLS.Show()
            objINVDTLS.BringToFront()
            'Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDPROGRAM_KeyDown(sender As Object, e As KeyEventArgs)
        'Try
        '    If e.KeyCode = Keys.Delete And GRIDPROGRAM.RowCount > 0 Then

        '        'end of block
        '        GRIDPROGRAM.Rows.RemoveAt(GRIDPROGRAM.CurrentRow.Index)
        '        GETSRNO(GRIDPROGRAM)
        '        TOTAL()
        '    End If
        'Catch ex As Exception
        '    If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        'End Try
        Try
            If GRIDPROGRAM.SelectedRowsCount > 0 Then
                If e.KeyCode = Keys.Delete Then
                    Dim DT As DataTable = GRIDPROGRAMDETAILS.DataSource
                    DT.Rows.RemoveAt(GRIDPROGRAM.FocusedRowHandle)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GRIDLOT_KeyDown(sender As Object, e As KeyEventArgs)
        'Try
        '    If e.KeyCode = Keys.Delete And GRIDLOT.RowCount > 0 Then

        '        'end of block
        '        GRIDLOT.Rows.RemoveAt(GRIDLOT.CurrentRow.Index)
        '        GETSRNO(GRIDLOT)
        '        TOTAL()
        '    End If
        'Catch ex As Exception
        '    If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        'End Try

        Try
            If GRIDLOT.SelectedRowsCount > 0 Then
                If e.KeyCode = Keys.Delete Then
                    Dim DT As DataTable = GRIDLOTDETAILS.DataSource
                    DT.Rows.RemoveAt(GRIDLOT.FocusedRowHandle)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LotTagging_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call PRINTTOOLSTRIP_Click(sender, e)
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                TOOLPREVIOUS_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                TOOLNEXT_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(sender As Object, e As CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDLOTDETAILS.DataSource = Nothing
                GRIDPROGRAMDETAILS.DataSource = Nothing
                LOTTAGNO = Val(tstxtbillno.Text)
                If LOTTAGNO > 0 Then
                    EDIT = True
                    LotTagging_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class