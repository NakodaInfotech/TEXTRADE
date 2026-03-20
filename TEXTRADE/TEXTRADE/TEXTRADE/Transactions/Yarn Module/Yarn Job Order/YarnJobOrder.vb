
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
        GETMAXNO()

        DTDATE.Text = Now.Date
        CMBITEMNAME.Text = ""
        CMBNAME.Text = ""

        TXTREED.Clear()
        TXTREEDSPACE.Clear()
        TXTPICKS.Clear()
        TXTTOTALENDS.Clear()
        TXTREFNO.Clear()
        TXTMTRS.Clear()
        txtremarks.Clear()
        tstxtbillno.Clear()

        GRIDBEAM.RowCount = 0

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
            CLEAR()

            If EDIT = True Then
                SHOWDATA()
            Else
                EDIT = False
                CLEAR()
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

        If Val(TXTMTRS.Text.Trim) = 0 Then
            Ep.SetError(TXTMTRS, "Please Enter Mtrs")
            bln = False
        End If

        If lbllocked.Visible = True Then
            Ep.SetError(lbllocked, " Entry Locked  !!!")
            bln = False
        End If

        Return bln
    End Function

    Sub SHOWDATA()
        Try
            CLEAR()
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim objclsGRN As New ClsJobOrder()
            Dim dttable As DataTable = objclsGRN.SelectYarnJob(TEMPJONO, YearId)
            If dttable.Rows.Count > 0 Then
                For Each dr As DataRow In dttable.Rows
                    TXTJONO.Text = TEMPJONO
                    TXTJONO.ReadOnly = True
                    DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")

                    CMBITEMNAME.Text = Convert.ToString(dr("ITEMNAME").ToString)
                    CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                    TXTSHADE.Text = dr("COLOR")

                    TXTREED.Text = dr("REED").ToString
                    TXTREEDSPACE.Text = dr("REEDSPACE").ToString
                    TXTPICKS.Text = dr("PICKS").ToString

                    ' Reference and names
                    TXTREFNO.Text = dr("REFNO").ToString
                    TXTMTRS.Text = Val(dr("TOTALMTRS"))
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
                Dim OBJCMN As New ClsCommon
                Dim dttable1 As DataTable = OBJCMN.SEARCH(" ISNULL(JOBORDER_WARPMATCHING.JOB_WARPSRNO, 0) As WARPGRIDSRNO, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPSYM, '') AS WARPGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WARPYARNQUALITY, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPDENIER, 0) AS WARPDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WARPMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WARPSHADE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPPE, 0) AS WARPPE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPBE, 0) AS WARPBE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPTE, 0) AS WARPTE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPWT, 0.000) AS WARPWT, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPCONS, 0) AS WARPCONS, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPRATE, 0) AS WARPRATE, ISNULL(JOBORDER_WARPMATCHING.JOB_WARPCOST, 0) AS WARPCOST ", "", " JOBORDER_WARPMATCHING INNER JOIN YARNQUALITYMASTER ON JOBORDER_WARPMATCHING.JOB_WARPYARNQUALITYID = YARNQUALITYMASTER.YARN_ID AND JOBORDER_WARPMATCHING.JOB_YEARID = YARNQUALITYMASTER.YARN_YEARID LEFT OUTER JOIN MILLMASTER ON JOBORDER_WARPMATCHING.JOB_YEARID = MILLMASTER.MILL_YEARID AND MILLMASTER.MILL_ID = JOBORDER_WARPMATCHING.JOB_WARPMILLID LEFT OUTER JOIN COLORMASTER ON JOBORDER_WARPMATCHING.JOB_YEARID = COLORMASTER.COLOR_yearid AND COLORMASTER.COLOR_id = JOBORDER_WARPMATCHING.JOB_WARPCOLORID  ", " AND  JOBORDER_WARPMATCHING.JOB_NO = " & TEMPJONO & " AND JOBORDER_WARPMATCHING.JOB_YEARID = " & YearId & " ORDER BY WARPGRIDSRNO")
                If dttable1.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable1.Rows
                        GRIDBEAM.Rows.Add(Val(DTR("WARPGRIDSRNO")), DTR("WARPGRIDSYM").ToString, DTR("WARPYARNQUALITY").ToString, Format(DTR("WARPDENIER"), "0.00"), DTR("WARPMILLNAME").ToString, DTR("WARPSHADE").ToString, Format(DTR("WARPPE"), "0.00"), Format(DTR("WARPBE"), "0.00"), Format(DTR("WARPTE"), "0.00"), Format(DTR("WARPWT"), "0.000"), Format(DTR("WARPCONS"), "0.00"), Format(DTR("WARPRATE"), "0.00"), Format(DTR("WARPCOST"), "0.00"))
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
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(Val(TXTPONO.Text.Trim))
            alParaval.Add(Val(TXTTOTALMTRS.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            '*************************************************************************
            'GRID WARP

            Dim SrNo As String = ""
            Dim ItemName As String = ""
            Dim Shade As String = ""
            Dim OtherItemName As String = ""
            Dim RefNo As String = ""
            Dim Reed As String = ""
            Dim Picks As String = ""
            Dim RS As String = ""
            Dim Ends As String = ""
            Dim Mtrs As String = ""
            Dim Description As String = ""
            Dim OutMtrs As String = ""
            Dim Done As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDBEAM.Rows
                If row.IsNewRow Then Continue For
                If row.Cells(0).Value IsNot Nothing Then
                    If SrNo = "" Then
                        SrNo = Val(row.Cells(GSRNO.Index).Value)
                        ItemName = row.Cells(GITEMNAME.Index).Value.ToString
                        Shade = row.Cells(GSHADE.Index).Value.ToString
                        OtherItemName = row.Cells(GPARENTITEM.Index).Value.ToString
                        RefNo = row.Cells(GREFNO.Index).Value.ToString
                        Reed = Val(row.Cells(GREED.Index).Value)
                        Picks = Val(row.Cells(GPICKS.Index).Value)
                        RS = Val(row.Cells(GREEDSPACE.Index).Value)
                        Ends = Val(row.Cells(GENDS.Index).Value)
                        Mtrs = Val(row.Cells(GMTRS.Index).Value)
                        Description = row.Cells(GDESC.Index).Value.ToString
                        OutMtrs = Val(row.Cells(GOUTMTRS.Index).Value)
                        Done = row.Cells(GDONE.Index).Value.ToString
                    Else
                        SrNo = SrNo & "|" & Val(row.Cells(GSRNO.Index).Value)
                        ItemName = ItemName & "|" & row.Cells(GITEMNAME.Index).Value.ToString
                        Shade = Shade & "|" & row.Cells(GSHADE.Index).Value.ToString
                        OtherItemName = OtherItemName & "|" & row.Cells(GPARENTITEM.Index).Value.ToString
                        RefNo = RefNo & "|" & row.Cells(GREFNO.Index).Value.ToString
                        Reed = Reed & "|" & Val(row.Cells(GREED.Index).Value)
                        Picks = Picks & "|" & Val(row.Cells(GPICKS.Index).Value)
                        RS = RS & "|" & Val(row.Cells(GREEDSPACE.Index).Value)
                        Ends = Ends & "|" & Val(row.Cells(GENDS.Index).Value)
                        Mtrs = Mtrs & "|" & Val(row.Cells(GMTRS.Index).Value)
                        Description = Description & "|" & row.Cells(GDESC.Index).Value.ToString
                        OutMtrs = OutMtrs & "|" & Val(row.Cells(GOUTMTRS.Index).Value)
                        Done = Done & "|" & row.Cells(GDONE.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(SrNo)
            alParaval.Add(ItemName)
            alParaval.Add(Shade)
            alParaval.Add(OtherItemName)
            alParaval.Add(RefNo)
            alParaval.Add(Reed)
            alParaval.Add(Picks)
            alParaval.Add(RS)
            alParaval.Add(Ends)
            alParaval.Add(Mtrs)
            alParaval.Add(Description)
            alParaval.Add(OutMtrs)
            alParaval.Add(Done)

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

            CLEAR()
            EDIT = False
            CMBNAME.Focus()

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
                Dim dttable As DataTable = OBJCMN.SEARCH(" DESIGNCARD.DESIGN_CARDNO AS CARDNO, ISNULL(DESIGNCARD.DESIGN_FEPI, 0) AS FEPI, ISNULL(DESIGNCARD.DESIGN_FWIDTH, 0) AS FWIDTH, ISNULL(DESIGNCARD.DESIGN_FPPI, 0) AS FPPI, ISNULL(DESIGNCARD.DESIGN_FWT, 0) AS FWT, ISNULL(DESIGNCARD.DESIGN_DENTS, 0) AS DENTS, ISNULL(DESIGNCARD.DESIGN_TOTALDENTSMAIN, 0) AS TOTALDENTSMAIN, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEDENTS, 0) AS TOTALSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_TOTALDENTS, 0) AS TOTALDENTS, ISNULL(DESIGNCARD.DESIGN_WARPTTL, 0) AS WARPTTL, ISNULL(DESIGNCARD.DESIGN_WEFTTTL, 0) AS WEFTTTL, ISNULL(DESIGNCARD.DESIGN_GSM, 0) AS GSM, ISNULL(DESIGNCARD.DESIGN_SHAFTS, 0) AS SHAFTS, ISNULL(DESIGNCARD.DESIGN_TOTALWT, 0) AS TOTALWT, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGE, 0) AS LEFTSELVEDGE, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGE, 0) AS RIGHTSELVEDGE, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGEEND, 0) AS LEFTSELVEDGEEND, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGEEND, 0) AS RIGHTSELVEDGEEND, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGEDENTS, 0) AS LEFTSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGEDENTS, 0) AS RIGHTSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGETOTALEND, 0) AS LEFTSELVEDGETOTALEND, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGETOTALEND, 0) AS RIGHTSELVEDGETOTALEND, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEENDS, 0) AS TOTALSELVEDGEENDS, ISNULL(DESIGNCARD.DESIGN_REFNO, '') AS REFNO, ISNULL(DESIGNCARD.DESIGN_GREY, '') AS GREY, ISNULL(DESIGNCARD.DESIGN_ORDERNO, 0) AS ORDERNO, ISNULL(DESIGNCARD.DESIGN_DELDATE, '') AS DELDATE, ISNULL(DESIGNCARD.DESIGN_ORDERDATE, '') AS ORDERDATE, ISNULL(DESIGNCARD.DESIGN_MTRS, 0) AS MTRS, ISNULL(DESIGNCARD.DESIGN_NOOFPCS, 0) AS NOOFPCS, ISNULL(DESIGNCARD.DESIGN_LOOM, '') AS LOOM, ISNULL(DESIGNCARD.DESIGN_BEAMMTRS, 0) AS BEAMMTRS, ISNULL(DESIGNCARD.DESIGN_COVERFACTOR, '') AS COVERFACTOR, ISNULL(DESIGNCARD.DESIGN_EFFICIENCY, '') AS EFFICIENCY, ISNULL(DESIGNCARD.DESIGN_LOOMPROD, 0) AS LOOMPROD, ISNULL(DESIGNCARD.DESIGN_RPM, '') AS RPM, ISNULL(DESIGNCARD.DESIGN_GREYDELDATE, '') AS GREYDELDATE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPPE, 0) AS TOTALWARPPE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPBE, 0) AS TOTALWARPBE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPTE, 0) AS TOTALWARPTE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPWT, 0) AS TOTALWARPWT, ISNULL(DESIGNCARD.DESIGN_TOTALWARPCONS, 0) AS TOTALWARPCONS, ISNULL(DESIGNCARD.DESIGN_TOTALWARPRATE, 0) AS TOTALWARPRATE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPCOST, 0) AS TOTALWARPCOST, ISNULL(DESIGNCARD.DESIGN_TOTALWARPGRIDPE, 0) AS TOTALWARPGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEPE, 0) AS TOTALSELVEDGEPE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEBE, 0) AS TOTALSELVEDGEBE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGETE, 0) AS TOTALSELVEDGETE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEWT, 0) AS TOTALSELVEDGEWT, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGECONS, 0) AS TOTALSELVEDGECONS, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGERATE, 0) AS TOTALSELVEDGERATE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGECOST, 0) AS TOTALSELVEDGECOST, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEGRIDPE, 0) AS TOTALSELVEDGEGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTPE, 0) AS TOTALWEFTPE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTBE, 0) AS TOTALWEFTBE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTTE, 0) AS TOTALWEFTTE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTWT, 0) AS TOTALWEFTWT, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTCONS, 0) AS TOTALWEFTCONS, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTRATE, 0) AS TOTALWEFTRATE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTCOST, 0) AS TOTALWEFTCOST, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTGRIDPE, 0) AS TOTALWEFTGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALDRAWENDS, 0) AS TOTALDRAWENDS, ISNULL(DESIGNCARD.DESIGN_TOTALDRAWDENTS, 0) AS TOTALDRAWDENTS, ISNULL(DESIGNMASTER.DESIGN_NO, 0) AS DESIGNNO, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(DELATLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(GDELATLEDGERS.Acc_cmpname, '') AS GREYDELIVERYAT, DESIGNCARD.DESIGN_DATE AS DATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNCARD.DESIGN_REED, 0) AS REED, ISNULL(DESIGNCARD.DESIGN_REEDSPACE, 0) AS REEDSPACE, ISNULL(DESIGNCARD.DESIGN_PICKS, 0) AS PICKS, ISNULL(DESIGNCARD.DESIGN_MAINRS, 0) AS MAINRS, ISNULL(DESIGNCARD.DESIGN_THREADPERDENT, '') AS THREADPERDENT, ISNULL(WEAVEMASTER.WEAVE_name, '') AS WEAVE, ISNULL(DESIGNCARD.DESIGN_TOTALFINISHWT, 0) AS TOTALFINISHWT, ISNULL(DESIGNCARD.DESIGN_GREYWIDTH, 0) AS GREYWIDTH, ISNULL(DESIGNCARD.DESIGN_GREYWIDTHCM,0) AS GREYWIDTHCM, ISNULL(DESIGNCARD.DESIGN_FINISHWIDTHCM,0) AS FINISHWIDTHCM, ISNULL(DESIGNCARD.DESIGN_GREYLOOMMTR,0) AS GREYLOOMMTR, ISNULL(DESIGNCARD.DESIGN_BLENDPERCENTAGE,0) AS BLENDPER, ISNULL(DESIGNCARD.DESIGN_FINISHMETHOD,'') AS FINISHMETHOD, ISNULL(DESIGNCARD.DESIGN_QUALITIES,'') AS QUALITY, ISNULL(DESIGNCARD.DESIGN_QUALITYTYPE,'') AS QUALITYTYPE, ISNULL(DESIGNCARD.DESIGN_WARPWASTAGE,0) AS WARPWASTAGE, ISNULL(DESIGNCARD.DESIGN_WASTAGEPERCENTAGE,0) AS WASTAGEPER, ISNULL(DESIGNCARD.DESIGN_SHRINKAGEPERCENTAGE,0) AS SHRINKAGEPER, ISNULL(DESIGNCARD.DESIGN_WPP,0) AS WPP, ISNULL(DESIGNCARD.DESIGN_WEAVECOST,0) AS WEAVECOST, ISNULL(DESIGNCARD.DESIGN_GREYFABRICCOST,0) AS GREYFABCOST, ISNULL(DESIGNCARD.DESIGN_FINISHFABRICCOST,0) AS FINISHFABCOST, ISNULL(DESIGNCARD.DESIGN_PRODUCTIONPERDAY,0) AS PRODDAY, ISNULL(DESIGNCARD.DESIGN_PCSL,0) AS PCSL, ISNULL(DESIGNCARD.DESIGN_REEDSPACECM,0) AS REEDSPACECM,ISNULL(DESIGNCARD.DESIGN_TOTALENDS,0) AS TOTALENDS ,ISNULL(DESIGNCARD.DESIGN_ENDPERINCH,0) AS ENDPERINCH, ISNULL(DESIGNCARD.DESIGN_TOTALPEG,0) AS TOTALPEG, ISNULL(COLORMASTER.COLOR_name,'') AS SHADE ", "", " DESIGNCARD LEFT OUTER JOIN WEAVEMASTER ON DESIGNCARD.DESIGN_YEARID = WEAVEMASTER.WEAVE_yearid AND DESIGNCARD.DESIGN_WEAVEID = WEAVEMASTER.WEAVE_id LEFT OUTER JOIN LEDGERS AS GDELATLEDGERS ON DESIGNCARD.DESIGN_YEARID = GDELATLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_GREYDELATID = GDELATLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DELATLEDGERS ON DESIGNCARD.DESIGN_YEARID = DELATLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_DELIVERYATID = DELATLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON DESIGNCARD.DESIGN_YEARID = AGENTLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON DESIGNCARD.DESIGN_YEARID = LEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN ITEMMASTER ON DESIGNCARD.DESIGN_YEARID = ITEMMASTER.item_yearid AND DESIGNCARD.DESIGN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON DESIGNCARD.DESIGN_YEARID = DESIGNMASTER.DESIGN_yearid AND DESIGNCARD.DESIGN_ID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON DESIGNCARD.DESIGN_SHADEID = COLORMASTER.COLOR_id AND DESIGNCARD.DESIGN_YEARID = COLORMASTER.COLOR_yearid   ", " AND  (ITEMMASTER.item_name = '" & CMBITEMNAME.Text.Trim & "') AND (DESIGNCARD.DESIGN_YEARID = " & YearId & ") ")
                If dttable.Rows.Count > 0 Then
                    Dim cardno As Integer
                    For Each dr As DataRow In dttable.Rows
                        cardno = Val(dr("CARDNO"))

                        TXTREED.Text = dr("REED").ToString
                        TXTREEDSPACE.Text = dr("REEDSPACE").ToString
                        TXTPICKS.Text = dr("PICKS").ToString
                        TXTSHADE.Text = dr("SHADE")
                    Next



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
        CLEAR()
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
                CLEAR()
                EDIT = False
            End If
            If GRIDBEAM.RowCount = 0 And TEMPJONO > 1 Then
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
            GETMAXNO()
            Dim MAXNO As Integer = TXTJONO.Text.Trim
            CLEAR()
            If Val(TXTJONO.Text) - 1 >= TEMPJONO Then
                EDIT = True
                'DesignCardMaster_Load(sender, e)
                SHOWDATA()
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDBEAM.RowCount = 0 And TEMPJONO < MAXNO Then
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
                Dim IntResult As Integer = clspo.Delete()
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
                GRIDBEAM.RowCount = 0
                TEMPJONO = Val(tstxtbillno.Text)
                If TEMPJONO > 0 Then
                    EDIT = True
                    JobOrder_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTTOTALMTRS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub
End Class