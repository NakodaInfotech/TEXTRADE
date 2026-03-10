
Imports System.ComponentModel
Imports BL

Public Class YarnJobOrder

    Public EDIT As Boolean              'Used for edit
    Public TEMPJONO As Integer           'Used for edit name
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()
        getmaxno()

        DTDATE.Text = Now.Date
        CMBITEMNAME.Text = ""
        CMBNAME.Text = ""
        CMBDESIGNNO.Text = ""
        CMBSHADE.Text = ""

        TXTREED.Clear()
        TXTREEDSPACE.Clear()
        TXTPICKS.Clear()
        TXTTOTALENDS.Clear()
        TXTREFNO.Clear()
        TXTTOTALMTRS.Clear()
        txtremarks.Clear()
        tstxtbillno.Clear()

        GRIDWARP.RowCount = 0
        GRIDWEFT.RowCount = 0
        CMBITEMNAME.Enabled = True
        Ep.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

        TXTWEFTPE.Clear()
        TXTWEFTBE.Clear()
        TXTWEFTTE.Clear()
        TXTWEFTWT.Clear()
        TXTWEFTCONS.Clear()
        TXTWEFTRATE.Clear()
        TXTWEFTCOST.Clear()

    End Sub

    Private Sub JobOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'YARN JOBORDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            Cursor.Current = Cursors.WaitCursor
            'fillcmb()
            clear()

            If EDIT = True Then
                SHOWDATA()
            Else
                EDIT = False
                clear()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAXNO()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(JOB_no),0) + 1 ", " JOBORDER ", " and JOB_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTJONO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Function ERRORVALID() As Boolean

        Dim bln As Boolean = True

        If DTDATE.Text = "__/__/____" Then
            Ep.SetError(DTDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTDATE.Text) Then
                Ep.SetError(DTDATE, "Date not in Accounting Year")
                bln = False
            End If

            If Convert.ToDateTime(DTDATE.Text).Date < SALEBLOCKDATE.Date Then
                Ep.SetError(DTDATE, "Date is Blocked, Please make entries after " & Format(SALEBLOCKDATE.Date, "dd/MM/yyyy"))
                bln = False
            End If
        End If

        If CMBITEMNAME.Text.Trim = "" Then
            Ep.SetError(CMBITEMNAME, "Please select Item Name")
            bln = False
        End If

        If Val(TXTTOTALMTRS.Text.Trim) = 0 Then
            Ep.SetError(TXTTOTALMTRS, "Please Enter Mtrs")
            bln = False
        End If

        If lbllocked.Visible = True Then
            Ep.SetError(lbllocked, " Entry Locked  !!!")
            bln = False
        End If

        Return bln
    End Function

    Sub SHOWDATA(Optional ByVal CARDNO As Integer = -1)
        Try
            clear()
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJCMN As New ClsCommon
            Dim objclsGRN As New ClsJobOrder()

            Dim dttable As New DataTable
            dttable = objclsGRN.SelectYarnJob(TEMPJONO, YearId)

            If dttable.Rows.Count > 0 Then

                For Each dr As DataRow In dttable.Rows
                    TXTJONO.Text = TEMPJONO
                    TXTJONO.ReadOnly = True
                    DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")

                    CMBITEMNAME.Text = Convert.ToString(dr("ITEMNAME").ToString)
                    CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                    CMBDESIGNNO.Text = Convert.ToString(dr("DESIGNNO").ToString)

                    TXTREED.Text = dr("REED").ToString
                    TXTREEDSPACE.Text = dr("REEDSPACE").ToString
                    TXTPICKS.Text = dr("PICKS").ToString

                    ' Reference and names
                    TXTREFNO.Text = dr("REFNO").ToString
                    TXTTOTALMTRS.Text = Val(dr("TOTALMTRS"))
                    TXTTOTALENDS.Text = Val(dr("TOTALENDS"))
                    txtremarks.Text = dr("REMARKS").ToString

                    If Val(dr("OUTMTRS")) > 0 Then
                        lbllocked.Visible = True
                        PBlock.Visible = True
                    End If

                    If Convert.ToBoolean(dr("DONE")) = True Then
                        LBLCLOSED.Visible = True
                        PBlock.Visible = True
                    End If

                Next

                'warp gridmatching data serializations
                Dim dttable1 As DataTable = OBJCMN.SEARCH(" ISNULL(JOBORDER_WARPMATCHING.JOB_WARPSRNO, 0) As WARPGRIDSRNO, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPSYM, '') AS WARPGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WARPYARNQUALITY, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPDENIER, 0) AS WARPDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WARPMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WARPSHADE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPPE, 0) AS WARPPE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPBE, 0) AS WARPBE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPTE, 0) AS WARPTE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPWT, 0.000) AS WARPWT, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPCONS, 0) AS WARPCONS, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPRATE, 0) AS WARPRATE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPCOST, 0) AS WARPCOST ", "", " JOBORDER_WARPMATCHING INNER JOIN YARNQUALITYMASTER ON JOBORDER_WARPMATCHING.JOB_WARPYARNQUALITYID = YARNQUALITYMASTER.YARN_ID AND JOBORDER_WARPMATCHING.JOB_YEARID = YARNQUALITYMASTER.YARN_YEARID LEFT OUTER JOIN MILLMASTER ON JOBORDER_WARPMATCHING.JOB_YEARID = MILLMASTER.MILL_YEARID AND MILLMASTER.MILL_ID = JOBORDER_WARPMATCHING.JOB_WARPMILLID LEFT OUTER JOIN COLORMASTER ON JOBORDER_WARPMATCHING.JOB_YEARID = COLORMASTER.COLOR_yearid AND COLORMASTER.COLOR_id = JOBORDER_WARPMATCHING.JOB_WARPCOLORID  ", " AND  JOBORDER_WARPMATCHING.JOB_NO = " & TEMPJONO & " AND JOBORDER_WARPMATCHING.JOB_YEARID = " & YearId & " ORDER BY WARPGRIDSRNO")
                If dttable1.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable1.Rows
                        GRIDWARP.Rows.Add(Val(DTR("WARPGRIDSRNO")), DTR("WARPGRIDSYM").ToString, DTR("WARPYARNQUALITY").ToString, Format(DTR("WARPDENIER"), "0.00"), DTR("WARPMILLNAME").ToString, DTR("WARPSHADE").ToString, Format(DTR("WARPPE"), "0.00"), Format(DTR("WARPBE"), "0.00"), Format(DTR("WARPTE"), "0.00"), Format(DTR("WARPWT"), "0.000"), Format(DTR("WARPCONS"), "0.00"), Format(DTR("WARPRATE"), "0.00"), Format(DTR("WARPCOST"), "0.00"))
                    Next
                End If

                ' Weft Grid data serialization
                Dim dttable5 As DataTable = OBJCMN.SEARCH(" ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTSRNO, 0) AS WEFTGRIDSRNO, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTSYM, '') AS WEFTGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WEFTYARNQUALITY, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTDENIER, 0) AS WEFTDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WEFTMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WEFTSHADE, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTPE, 0) AS WEFTPE, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTBE, 0) AS WEFTBE, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTTE, 0) AS WEFTTE, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTWT, 0) AS WEFTWT, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTCONS, 0) AS WEFTCONS, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTRATE, 0) AS WEFTRATE, ISNULL(JOBORDER_WEFTMATCHING.JOB_WEFTCOST, 0) AS WEFTCOST", "", " JOBORDER_WEFTMATCHING LEFT OUTER JOIN COLORMASTER ON JOBORDER_WEFTMATCHING.JOB_WEFTCOLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN MILLMASTER ON JOBORDER_WEFTMATCHING.JOB_WEFTMILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON JOBORDER_WEFTMATCHING.JOB_WEFTYARNQUALITYID = YARNQUALITYMASTER.YARN_ID   ", " AND  JOBORDER_WEFTMATCHING.JOB_NO = " & TEMPJONO & " AND JOBORDER_WEFTMATCHING.JOB_YEARID = " & YearId & " ORDER BY WEFTGRIDSRNO")
                If dttable5.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable5.Rows
                        GRIDWEFT.Rows.Add(DTR("WEFTGRIDSRNO"), DTR("WEFTGRIDSYM").ToString, DTR("WEFTYARNQUALITY").ToString, Format(DTR("WEFTDENIER"), "0.00"), DTR("WEFTMILLNAME").ToString, DTR("WEFTSHADE").ToString, Format(DTR("WEFTPE"), "0.00"), Format(DTR("WEFTBE"), "0.00"), Format(DTR("WEFTTE"), "0.00"), Format(DTR("WEFTWT"), "0.000"), Format(DTR("WEFTCONS"), "0.00"), Format(DTR("WEFTRATE"), "0.00"), Format(DTR("WEFTCOST"), "0.00"))
                    Next
                End If

                CMBITEMNAME.Enabled = False

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Ep.Clear()

            If Not ERRORVALID() Then
                Exit Sub
            End If
            Dim IntResult As Integer

            Dim alParaval As New ArrayList
            alParaval.Add(Val(TXTJONO.Text.Trim))
            alParaval.Add(Format(Convert.ToDateTime(DTDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBITEMNAME.Text.Trim)
            alParaval.Add(CMBDESIGNNO.Text.Trim)
            alParaval.Add(CMBSHADE.Text.Trim)
            alParaval.Add(Val(TXTREED.Text.Trim))
            alParaval.Add(Val(TXTREEDSPACE.Text.Trim))
            alParaval.Add(Val(TXTPICKS.Text.Trim))



            'party and other ledgers
            alParaval.Add(TXTREFNO.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)

            alParaval.Add(Val(TXTTOTALMTRS.Text.Trim))
            alParaval.Add(Val(TXTTOTALENDS.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)


            '*************************************************************************
            'GRID WARP

            Dim WARPSrNo As String = ""
            Dim WARPSym As String = ""
            Dim WARPYarnQuality As String = ""
            Dim WARPDenier As String = ""
            Dim WARPMillName As String = ""
            Dim WARPShade As String = ""
            Dim WARPPE As String = ""
            Dim WARPBE As String = ""
            Dim WARPTE As String = ""
            Dim WARPWt As String = ""
            Dim WARPCons As String = ""
            Dim WARPRate As String = ""
            Dim WARPCost As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDWARP.Rows
                If row.Cells(0).Value IsNot Nothing Then
                    If WARPSrNo = "" Then
                        WARPSrNo = Val(row.Cells(WSRNO.Index).Value)
                        WARPSym = row.Cells(WSYM.Index).Value.ToString
                        WARPYarnQuality = row.Cells(WQUALITY.Index).Value.ToString
                        WARPDenier = Val(row.Cells(WDENIER.Index).Value)
                        WARPMillName = row.Cells(WMILL.Index).Value.ToString
                        WARPShade = row.Cells(WSHADE.Index).Value.ToString
                        WARPPE = Val(row.Cells(WPE.Index).Value)
                        WARPBE = Val(row.Cells(WBE.Index).Value)
                        WARPTE = Val(row.Cells(WENDS.Index).Value)
                        WARPWt = Val(row.Cells(WWT.Index).Value)
                        WARPCons = Val(row.Cells(WCONS.Index).Value)
                        WARPRate = Val(row.Cells(WRATE.Index).Value)
                        WARPCost = Val(row.Cells(WCOST.Index).Value)
                    Else
                        WARPSrNo = WARPSrNo & "|" & Val(row.Cells(WSRNO.Index).Value)
                        WARPSym = WARPSym & "|" & row.Cells(WSYM.Index).Value.ToString
                        WARPYarnQuality = WARPYarnQuality & "|" & row.Cells(WQUALITY.Index).Value.ToString
                        WARPDenier = WARPDenier & "|" & Val(row.Cells(WDENIER.Index).Value)
                        WARPMillName = WARPMillName & "|" & row.Cells(WMILL.Index).Value.ToString
                        WARPShade = WARPShade & "|" & row.Cells(WSHADE.Index).Value.ToString
                        WARPPE = WARPPE & "|" & Val(row.Cells(WPE.Index).Value)
                        WARPBE = WARPBE & "|" & Val(row.Cells(WBE.Index).Value)
                        WARPTE = WARPTE & "|" & Val(row.Cells(WENDS.Index).Value)
                        WARPWt = WARPWt & "|" & Val(row.Cells(WWT.Index).Value)
                        WARPCons = WARPCons & "|" & Val(row.Cells(WCONS.Index).Value)
                        WARPRate = WARPRate & "|" & Val(row.Cells(WRATE.Index).Value)
                        WARPCost = WARPCost & "|" & Val(row.Cells(WCOST.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(WARPSrNo)
            alParaval.Add(WARPSym)
            alParaval.Add(WARPYarnQuality)
            alParaval.Add(WARPDenier)
            alParaval.Add(WARPMillName)
            alParaval.Add(WARPShade)
            alParaval.Add(WARPPE)
            alParaval.Add(WARPBE)
            alParaval.Add(WARPTE)
            alParaval.Add(WARPWt)
            alParaval.Add(WARPCons)
            alParaval.Add(WARPRate)
            alParaval.Add(WARPCost)



            '*************************************************************************
            'GRID WEFT
            ' Initialize variables for pipe-separated strings
            Dim WEFTSrNo As String = ""
            Dim WEFTSym As String = ""
            Dim WEFTYarnQuality As String = ""
            Dim WEFTDenier As String = ""
            Dim WEFTMillName As String = ""
            Dim WEFTShade As String = ""
            Dim WEFTPE As String = ""
            Dim WEFTBE As String = ""
            Dim WEFTTE As String = ""
            Dim WEFTWt As String = ""
            Dim WEFTCons As String = ""
            Dim WEFTRate As String = ""
            Dim WEFTCost As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDWEFT.Rows
                If row.Cells(FSRNO.Index).Value IsNot Nothing Then
                    If WEFTSrNo = "" Then
                        WEFTSrNo = Val(row.Cells(FSRNO.Index).Value)
                        WEFTSym = row.Cells(FSYM.Index).Value.ToString
                        WEFTYarnQuality = row.Cells(FQUALITY.Index).Value.ToString
                        WEFTDenier = Val(row.Cells(FDENIER.Index).Value)
                        WEFTMillName = row.Cells(FMILL.Index).Value.ToString
                        WEFTShade = row.Cells(FSHADE.Index).Value.ToString
                        WEFTPE = Val(row.Cells(FPE.Index).Value)
                        WEFTBE = Val(row.Cells(FBE.Index).Value)
                        WEFTTE = Val(row.Cells(FENDS.Index).Value)
                        WEFTWt = Val(row.Cells(FWT.Index).Value)
                        WEFTCons = Val(row.Cells(FCONS.Index).Value)
                        WEFTRate = Val(row.Cells(FRATE.Index).Value)
                        WEFTCost = Val(row.Cells(FCOST.Index).Value)
                    Else
                        WEFTSrNo = WEFTSrNo & "|" & row.Cells(FSRNO.Index).Value
                        WEFTSym = WEFTSym & "|" & row.Cells(FSYM.Index).Value.ToString
                        WEFTYarnQuality = WEFTYarnQuality & "|" & row.Cells(FQUALITY.Index).Value.ToString
                        WEFTDenier = WEFTDenier & "|" & Val(row.Cells(FDENIER.Index).Value)
                        WEFTMillName = WEFTMillName & "|" & row.Cells(FMILL.Index).Value.ToString
                        WEFTShade = WEFTShade & "|" & row.Cells(FSHADE.Index).Value.ToString
                        WEFTPE = WEFTPE & "|" & Val(row.Cells(FPE.Index).Value)
                        WEFTBE = WEFTBE & "|" & Val(row.Cells(FBE.Index).Value)
                        WEFTTE = WEFTTE & "|" & Val(row.Cells(FENDS.Index).Value)
                        WEFTWt = WEFTWt & "|" & Val(row.Cells(FWT.Index).Value)
                        WEFTCons = WEFTCons & "|" & Val(row.Cells(FCONS.Index).Value)
                        WEFTRate = WEFTRate & "|" & Val(row.Cells(FRATE.Index).Value)
                        WEFTCost = WEFTCost & "|" & Val(row.Cells(FCOST.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(WEFTSrNo)
            alParaval.Add(WEFTSym)
            alParaval.Add(WEFTYarnQuality)
            alParaval.Add(WEFTDenier)
            alParaval.Add(WEFTMillName)
            alParaval.Add(WEFTShade)
            alParaval.Add(WEFTPE)
            alParaval.Add(WEFTBE)
            alParaval.Add(WEFTTE)
            alParaval.Add(WEFTWt)
            alParaval.Add(WEFTCons)
            alParaval.Add(WEFTRate)
            alParaval.Add(WEFTCost)


            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)
            alParaval.Add(TXTOUTMTRS.Text.Trim)



            '*************************************************************************




            Dim objDESIGN As New ClsJobOrder
            objDESIGN.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objDESIGN.SAVE()
                'txtcardno.Text = IntResult.ToString()
                MsgBox("Details Added")
                'TEMPJONO = txtcardno.Text.Trim
                'PRINTREPORT(txtcardno.Text.Trim)
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPJONO)
                IntResult = objDESIGN.UPDATE()
                MsgBox("Details Updated")
                'PRINTREPORT(TEMPJONO)
            End If
            EDIT = False

            clear()
            EDIT = False
            CMBDESIGNNO.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validated(sender As Object, e As EventArgs) Handles CMBITEMNAME.Validated
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor

LINE1:
            If CMBITEMNAME.Text <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.SEARCH(" DESIGNCARD.DESIGN_CARDNO AS CARDNO, ISNULL(DESIGNCARD.DESIGN_FEPI, 0) AS FEPI, ISNULL(DESIGNCARD.DESIGN_FWIDTH, 0) AS FWIDTH, ISNULL(DESIGNCARD.DESIGN_FPPI, 0) AS FPPI, ISNULL(DESIGNCARD.DESIGN_FWT, 0) AS FWT, ISNULL(DESIGNCARD.DESIGN_DENTS, 0) AS DENTS, ISNULL(DESIGNCARD.DESIGN_TOTALDENTSMAIN, 0) AS TOTALDENTSMAIN, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEDENTS, 0) AS TOTALSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_TOTALDENTS, 0) AS TOTALDENTS, ISNULL(DESIGNCARD.DESIGN_WARPTTL, 0) AS WARPTTL,                           ISNULL(DESIGNCARD.DESIGN_WEFTTTL, 0) AS WEFTTTL, ISNULL(DESIGNCARD.DESIGN_GSM, 0) AS GSM, ISNULL(DESIGNCARD.DESIGN_SHAFTS, 0) AS SHAFTS, ISNULL(DESIGNCARD.DESIGN_TOTALWT, 0) AS TOTALWT, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGE, 0) AS LEFTSELVEDGE, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGE, 0) AS RIGHTSELVEDGE, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGEEND, 0) AS LEFTSELVEDGEEND, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGEEND, 0) AS RIGHTSELVEDGEEND, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGEDENTS, 0) AS LEFTSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGEDENTS, 0) AS RIGHTSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGETOTALEND, 0) AS LEFTSELVEDGETOTALEND, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGETOTALEND, 0) AS RIGHTSELVEDGETOTALEND, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEENDS, 0) AS TOTALSELVEDGEENDS, ISNULL(DESIGNCARD.DESIGN_REFNO, '') AS REFNO, ISNULL(DESIGNCARD.DESIGN_GREY, '') AS GREY, ISNULL(DESIGNCARD.DESIGN_ORDERNO, 0) AS ORDERNO, ISNULL(DESIGNCARD.DESIGN_DELDATE, '') AS DELDATE, ISNULL(DESIGNCARD.DESIGN_ORDERDATE, '') AS ORDERDATE, ISNULL(DESIGNCARD.DESIGN_MTRS, 0) AS MTRS, ISNULL(DESIGNCARD.DESIGN_NOOFPCS, 0) AS NOOFPCS, ISNULL(DESIGNCARD.DESIGN_LOOM, '') AS LOOM, ISNULL(DESIGNCARD.DESIGN_BEAMMTRS, 0) AS BEAMMTRS, ISNULL(DESIGNCARD.DESIGN_COVERFACTOR, '') AS COVERFACTOR, ISNULL(DESIGNCARD.DESIGN_EFFICIENCY, '') AS EFFICIENCY, ISNULL(DESIGNCARD.DESIGN_LOOMPROD, 0) AS LOOMPROD, ISNULL(DESIGNCARD.DESIGN_RPM, '') AS RPM, ISNULL(DESIGNCARD.DESIGN_GREYDELDATE, '') AS GREYDELDATE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPPE, 0) AS TOTALWARPPE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPBE, 0) AS TOTALWARPBE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPTE, 0) AS TOTALWARPTE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPWT, 0) AS TOTALWARPWT, ISNULL(DESIGNCARD.DESIGN_TOTALWARPCONS, 0) AS TOTALWARPCONS, ISNULL(DESIGNCARD.DESIGN_TOTALWARPRATE, 0) AS TOTALWARPRATE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPCOST, 0) AS TOTALWARPCOST, ISNULL(DESIGNCARD.DESIGN_TOTALWARPGRIDPE, 0) AS TOTALWARPGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEPE, 0) AS TOTALSELVEDGEPE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEBE, 0) AS TOTALSELVEDGEBE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGETE, 0) AS TOTALSELVEDGETE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEWT, 0) AS TOTALSELVEDGEWT, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGECONS, 0) AS TOTALSELVEDGECONS, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGERATE, 0) AS TOTALSELVEDGERATE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGECOST, 0) AS TOTALSELVEDGECOST, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEGRIDPE, 0) AS TOTALSELVEDGEGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTPE, 0) AS TOTALWEFTPE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTBE, 0) AS TOTALWEFTBE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTTE, 0) AS TOTALWEFTTE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTWT, 0) AS TOTALWEFTWT, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTCONS, 0) AS TOTALWEFTCONS, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTRATE, 0) AS TOTALWEFTRATE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTCOST, 0) AS TOTALWEFTCOST, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTGRIDPE, 0) AS TOTALWEFTGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALDRAWENDS, 0) AS TOTALDRAWENDS, ISNULL(DESIGNCARD.DESIGN_TOTALDRAWDENTS, 0) AS TOTALDRAWDENTS, ISNULL(DESIGNMASTER.DESIGN_NO, 0) AS DESIGNNO, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(DELATLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(GDELATLEDGERS.Acc_cmpname, '') AS GREYDELIVERYAT, DESIGNCARD.DESIGN_DATE AS DATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNCARD.DESIGN_REED, 0) AS REED, ISNULL(DESIGNCARD.DESIGN_REEDSPACE, 0) AS REEDSPACE, ISNULL(DESIGNCARD.DESIGN_PICKS, 0) AS PICKS, ISNULL(DESIGNCARD.DESIGN_MAINRS, 0) AS MAINRS, ISNULL(DESIGNCARD.DESIGN_THREADPERDENT, '') AS THREADPERDENT, ISNULL(WEAVEMASTER.WEAVE_name, '') AS WEAVE, ISNULL(DESIGNCARD.DESIGN_TOTALFINISHWT, 0) AS TOTALFINISHWT, ISNULL(DESIGNCARD.DESIGN_GREYWIDTH, 0) AS GREYWIDTH, ISNULL(DESIGNCARD.DESIGN_GREYWIDTHCM,0) AS GREYWIDTHCM, ISNULL(DESIGNCARD.DESIGN_FINISHWIDTHCM,0) AS FINISHWIDTHCM, ISNULL(DESIGNCARD.DESIGN_GREYLOOMMTR,0) AS GREYLOOMMTR, ISNULL(DESIGNCARD.DESIGN_BLENDPERCENTAGE,0) AS BLENDPER, ISNULL(DESIGNCARD.DESIGN_FINISHMETHOD,'') AS FINISHMETHOD, ISNULL(DESIGNCARD.DESIGN_QUALITIES,'') AS QUALITY, ISNULL(DESIGNCARD.DESIGN_QUALITYTYPE,'') AS QUALITYTYPE, ISNULL(DESIGNCARD.DESIGN_WARPWASTAGE,0) AS WARPWASTAGE, ISNULL(DESIGNCARD.DESIGN_WASTAGEPERCENTAGE,0) AS WASTAGEPER, ISNULL(DESIGNCARD.DESIGN_SHRINKAGEPERCENTAGE,0) AS SHRINKAGEPER, ISNULL(DESIGNCARD.DESIGN_WPP,0) AS WPP, ISNULL(DESIGNCARD.DESIGN_WEAVECOST,0) AS WEAVECOST, ISNULL(DESIGNCARD.DESIGN_GREYFABRICCOST,0) AS GREYFABCOST, ISNULL(DESIGNCARD.DESIGN_FINISHFABRICCOST,0) AS FINISHFABCOST, ISNULL(DESIGNCARD.DESIGN_PRODUCTIONPERDAY,0) AS PRODDAY, ISNULL(DESIGNCARD.DESIGN_PCSL,0) AS PCSL, ISNULL(DESIGNCARD.DESIGN_REEDSPACECM,0) AS REEDSPACECM,ISNULL(DESIGNCARD.DESIGN_TOTALENDS,0) AS TOTALENDS ,ISNULL(DESIGNCARD.DESIGN_ENDPERINCH,0) AS ENDPERINCH, ISNULL(DESIGNCARD.DESIGN_TOTALPEG,0) AS TOTALPEG, ISNULL(COLORMASTER.COLOR_name,'') AS SHADE ", "", " DESIGNCARD LEFT OUTER JOIN WEAVEMASTER ON DESIGNCARD.DESIGN_YEARID = WEAVEMASTER.WEAVE_yearid AND DESIGNCARD.DESIGN_WEAVEID = WEAVEMASTER.WEAVE_id LEFT OUTER JOIN LEDGERS AS GDELATLEDGERS ON DESIGNCARD.DESIGN_YEARID = GDELATLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_GREYDELATID = GDELATLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DELATLEDGERS ON DESIGNCARD.DESIGN_YEARID = DELATLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_DELIVERYATID = DELATLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON DESIGNCARD.DESIGN_YEARID = AGENTLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON DESIGNCARD.DESIGN_YEARID = LEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN ITEMMASTER ON DESIGNCARD.DESIGN_YEARID = ITEMMASTER.item_yearid AND DESIGNCARD.DESIGN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON DESIGNCARD.DESIGN_YEARID = DESIGNMASTER.DESIGN_yearid AND DESIGNCARD.DESIGN_ID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON DESIGNCARD.DESIGN_SHADEID = COLORMASTER.COLOR_id AND DESIGNCARD.DESIGN_YEARID = COLORMASTER.COLOR_yearid   ", " AND  (ITEMMASTER.item_name = '" & CMBITEMNAME.Text.Trim & "') AND (DESIGNCARD.DESIGN_YEARID = " & YearId & ") ")
                If dttable.Rows.Count > 0 Then
                    Dim cardno As Integer
                    For Each dr As DataRow In dttable.Rows
                        cardno = Val(dr("CARDNO"))

                        DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBITEMNAME.Text = Convert.ToString(dr("ITEMNAME").ToString)
                        CMBDESIGNNO.Text = Convert.ToString(dr("DESIGNNO").ToString)
                        TXTREED.Text = dr("REED").ToString
                        TXTREEDSPACE.Text = dr("REEDSPACE").ToString
                        TXTPICKS.Text = dr("PICKS").ToString

                        ' Reference and names
                        TXTREFNO.Text = dr("REFNO").ToString
                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)


                        ' Total Weft
                        TXTTOTALWEFTPE.Text = Val(dr("TOTALWEFTPE"))
                        TXTTOTALWEFTBE.Text = Val(dr("TOTALWEFTBE"))
                        TXTTOTALWEFTTE.Text = Val(dr("TOTALWEFTTE"))
                        TXTTOTALWEFTWT.Text = Format(Val(dr("TOTALWEFTWT")), "0.000")
                        TXTTOTALWEFTCONS.Text = Val(dr("TOTALWEFTCONS"))
                        TXTTOTALWEFTRATE.Text = Val(dr("TOTALWEFTRATE"))
                        TXTTOTALWEFTCOST.Text = Val(dr("TOTALWEFTCOST"))

                        TXTTOTALENDS.Text = dr("TOTALENDS")
                    Next

                    'warp gridmatching data serializations
                    Dim dttable1 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSRNO, 0) As WARPGRIDSRNO, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSYM, '') AS WARPGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WARPYARNQUALITY, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPDENIER, 0) AS WARPDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WARPMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WARPSHADE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPPE, 0) AS WARPPE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPBE, 0) AS WARPBE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPTE, 0) AS WARPTE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPWT, 0.000) AS WARPWT, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCONS, 0) AS WARPCONS, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPRATE, 0) AS WARPRATE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCOST, 0) AS WARPCOST ", "", " DESIGNCARD_WARPMATCHING INNER JOIN YARNQUALITYMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_WARPYARNQUALITYID = YARNQUALITYMASTER.YARN_ID AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = YARNQUALITYMASTER.YARN_YEARID LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = MILLMASTER.MILL_YEARID AND MILLMASTER.MILL_ID = DESIGNCARD_WARPMATCHING.DESIGN_WARPMILLID LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = COLORMASTER.COLOR_yearid AND COLORMASTER.COLOR_id = DESIGNCARD_WARPMATCHING.DESIGN_WARPCOLORID  ", " AND  DESIGNCARD_WARPMATCHING.DESIGN_CARDNO = " & cardno & " AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WARPGRIDSRNO")
                    If dttable1.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable1.Rows
                            GRIDWARP.Rows.Add(Val(DTR("WARPGRIDSRNO")), DTR("WARPGRIDSYM").ToString, DTR("WARPYARNQUALITY").ToString, Format(DTR("WARPDENIER"), "0.00"), DTR("WARPMILLNAME").ToString, DTR("WARPSHADE").ToString, Format(DTR("WARPPE"), "0.00"), Format(DTR("WARPBE"), "0.00"), Format(DTR("WARPTE"), "0.00"), Format(DTR("WARPWT"), "0.000"), Format(DTR("WARPCONS"), "0.00"), Format(DTR("WARPRATE"), "0.00"), Format(DTR("WARPCOST"), "0.00"))
                        Next
                    End If


                    ' Weft Grid data serialization
                    Dim dttable5 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSRNO, 0) AS WEFTGRIDSRNO, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSYM, '') AS WEFTGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WEFTYARNQUALITY, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTDENIER, 0) AS WEFTDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WEFTMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WEFTSHADE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTPE, 0) AS WEFTPE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTBE, 0) AS WEFTBE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTTE, 0) AS WEFTTE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTWT, 0) AS WEFTWT, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCONS, 0) AS WEFTCONS, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTRATE, 0) AS WEFTRATE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCOST, 0) AS WEFTCOST", "", " DESIGNCARD_WEFTMATCHING LEFT OUTER JOIN DESIGNCARD_WEFTSHADE ON DESIGNCARD_WEFTMATCHING.DESIGN_CARDNO = DESIGNCARD_WEFTSHADE.DESIGN_CARDNO AND DESIGNCARD_WEFTMATCHING.DESIGN_YEARID = DESIGNCARD_WEFTSHADE.DESIGN_YEARID AND DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSRNO = DESIGNCARD_WEFTSHADE.DESIGN_FDMAINSRNO LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WEFTSHADE.DESIGN_FDSHADE = COLORMASTER.COLOR_id LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTMILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTYARNQUALITYID = YARNQUALITYMASTER.YARN_ID    ", " AND  DESIGNCARD_WEFTMATCHING.DESIGN_CARDNO = " & cardno & " AND DESIGNCARD_WEFTMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WEFTGRIDSRNO")
                    If dttable5.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable5.Rows
                            GRIDWEFT.Rows.Add(DTR("WEFTGRIDSRNO"), DTR("WEFTGRIDSYM").ToString, DTR("WEFTYARNQUALITY").ToString, Format(DTR("WEFTDENIER"), "0.00"), DTR("WEFTMILLNAME").ToString, DTR("WEFTSHADE").ToString, Format(DTR("WEFTPE"), "0.00"), Format(DTR("WEFTBE"), "0.00"), Format(DTR("WEFTTE"), "0.00"), Format(DTR("WEFTWT"), "0.000"), Format(DTR("WEFTCONS"), "0.00"), Format(DTR("WEFTRATE"), "0.00"), Format(DTR("WEFTCOST"), "0.00"))
                        Next
                    End If
                End If

                CMBITEMNAME.Enabled = False
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(sender As Object, e As EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False

    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor


LINE1:
            'temptypename = cmbtype.Text.Trim
            TEMPJONO = Val(TXTJONO.Text) - 1
            If TEMPJONO > 0 Then
                EDIT = True
                'DesignCardMaster_Load(sender, e)
                SHOWDATA()
            Else
                clear()
                EDIT = False
            End If
            If GRIDWARP.RowCount = 0 And TEMPJONO > 1 Then
                TXTJONO.Text = TEMPJONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
LINE1:
            TEMPJONO = Val(TXTJONO.Text) + 1
            'temptypename = cmbtype.Text.Trim
            getmaxno()
            Dim MAXNO As Integer = TXTJONO.Text.Trim
            clear()
            If Val(TXTJONO.Text) - 1 >= TEMPJONO Then
                EDIT = True
                'DesignCardMaster_Load(sender, e)
                SHOWDATA()
            Else
                clear()
                EDIT = False
            End If
            If GRIDWARP.RowCount = 0 And TEMPJONO < MAXNO Then
                TXTJONO.Text = TEMPJONO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE='ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJJO As New YarnJobOrderDetails
            OBJJO.MdiParent = MDIMain
            OBJJO.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Try
            Call cmdok_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT()
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

    Private Sub tooldelete_Click(sender As Object, e As EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If lbllocked.Visible = True Or LBLCLOSED.Visible = True Then
                    MsgBox("Entry Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Delete Job Order ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim alParaval As New ArrayList
                alParaval.Add(TEMPJONO)
                alParaval.Add(CmpId)
                alParaval.Add(0)
                alParaval.Add(YearId)

                Dim clspo As New ClsJobOrder()
                clspo.alParaval = alParaval
                Dim IntResult As Integer = clspo.DELETE()
                MsgBox("Job Order Deleted")
                CLEAR()
                EDIT = False

            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validated(sender As Object, e As EventArgs) Handles tstxtbillno.Validated
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDWARP.RowCount = 0
                GRIDWEFT.RowCount = 0
                TEMPJONO = Val(tstxtbillno.Text)
                If TEMPJONO > 0 Then
                    EDIT = True
                    JobOrder_Load(sender, e)
                Else
                    clear()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTTOTALMTRS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTTOTALMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub
End Class