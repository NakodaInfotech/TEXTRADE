
Imports System.ComponentModel
Imports BL

Public Class YarnJobOrder
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPJONO As Integer           'Used for edit name
    Dim ALLOWMANUALJOBORDER As Boolean = False
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Sub FILLCMB()
        Try
            FILLNAME(CMBNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE='ACCOUNTS'")
            FILLNAME(CMBPARTYNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE='ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()
        If ALLOWMANUALJOBORDER = True Then
            TXTJONO.ReadOnly = False
            TXTJONO.BackColor = Color.LemonChiffon
        Else
            TXTJONO.ReadOnly = True
            TXTJONO.BackColor = Color.Linen
        End If
        GETMAXNO()
        getsrno(GRIDBEAM)
        TXTSRNO.Text = 1
        CMBSHADE.Text = ""
        TXTDESCRIPTION.Clear()
        TXTPONO.Clear()
        DTDATE.Text = Now.Date
        CMBITEMNAME.Text = ""
        CMBNAME.Text = ""
        CMBPARTYNAME.Text = ""
        TXTTOTALMTRS.Clear()
        TXTREED.Clear()
        TXTREEDSPACE.Clear()
        TXTPICKS.Clear()
        TXTTOTALENDS.Clear()
        TXTREFNO.Clear()
        TXTMTRS.Clear()
        txtremarks.Clear()
        tstxtbillno.Clear()
        CMDSELECTSO.Enabled = True

        GRIDBEAM.RowCount = 0

        CMBITEMNAME.Enabled = True
        Ep.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False


        CMBDESIGN.Text = ""

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
            CLEAR()
            If ClientName = "SWPL" Then ALLOWMANUALJOBORDER = True

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
        DTTABLE = getmax(" isnull(max(YJOB_no),0) + 1 ", " YARNJOBORDER ", " and YJOB_yearid=" & YearId)
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

        Dim OBJCMN As New ClsCommon
        If ALLOWMANUALJOBORDER = True Then
            If TXTJONO.Text <> "" And CMBNAME.Text.Trim <> "" And EDIT = False Then
                Dim dttable As DataTable = OBJCMN.SEARCH(" ISNULL(YARNJOBORDER.YJOB_no,0) AS JONO ", "", " YARNJOBORDER ", "  AND YARNJOBORDER.YJOB_no=" & TXTJONO.Text.Trim & " AND YARNJOBORDER.YJOB_yearid = " & YearId)
                If dttable.Rows.Count > 0 Then
                    Ep.SetError(TXTJONO, "Job Order No Already Exist")
                    bln = False
                End If
            End If
        End If

        If lbllocked.Visible = True And UserName <> "Admin" Then
            Ep.SetError(lbllocked, " Entry Locked  !!!")
            bln = False
        End If


        If CMBNAME.Text.Trim = "" Then
            Ep.SetError(CMBNAME, "Select Jobber Name")
            bln = False
        End If

        If TXTPONO.Text.Trim = "" And ClientName = "SWPL" Then
            Ep.SetError(TXTPONO, "Enter PO No")
            bln = False
        End If

        If CMBPARTYNAME.Text.Trim = "" And ClientName = "SWPL" Then
            Ep.SetError(CMBPARTYNAME, "Select Party Name")
            bln = False
        End If

        If GRIDBEAM.RowCount = 0 Then
            Ep.SetError(CMBNAME, "Fill Packing Slip Details")
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
            Dim objclsGRN As New ClsYarnJobOrder()
            Dim dttable As DataTable = objclsGRN.SelectYarnJob(TEMPJONO, YearId)
            If dttable.Rows.Count > 0 Then
                For Each dr As DataRow In dttable.Rows
                    TXTJONO.Text = TEMPJONO
                    TXTJONO.ReadOnly = True
                    DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                    CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                    TXTPONO.Text = dr("PONO")
                    CMBPARTYNAME.Text = Convert.ToString(dr("PARTYNAME").ToString)
                    TXTTOTALMTRS.Text = Val(dr("TOTALMTRS"))
                    txtremarks.Text = dr("REMARKS").ToString
                Next

                Dim OBJCMN As New ClsCommon
                Dim dttable1 As DataTable = OBJCMN.SEARCH("ISNULL(YARNJOBORDER_DESC.YJOB_SRNO, 0) AS GRIDSRNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(YARNJOBORDER_DESC.YJOB_PARENTITEM, '') AS PARENTITEM, ISNULL(YARNJOBORDER_DESC.YJOB_REFNO, '') AS REFNO, ISNULL(YARNJOBORDER_DESC.YJOB_REED, 0) AS REED, ISNULL(YARNJOBORDER_DESC.YJOB_PICKS, 0) AS PICKS, ISNULL(YARNJOBORDER_DESC.YJOB_REEDSPACE, 0) AS REEDSPACE, ISNULL(YARNJOBORDER_DESC.YJOB_ENDS, 0) AS ENDS, ISNULL(YARNJOBORDER_DESC.YJOB_MTRS, 0) AS MTRS, ISNULL(YARNJOBORDER_DESC.YJOB_DESCRIPTION, '') AS DESCRIPTION, ISNULL(YARNJOBORDER_DESC.YJOB_OUTMTRS, 0) AS OUTMTRS, ISNULL(YARNJOBORDER_DESC.YJOB_DONE, 0) AS DONE, ISNULL(YARNJOBORDER_DESC.YJOB_CLOSED, 0) AS CLOSED, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO ", "", " YARNJOBORDER_DESC LEFT OUTER JOIN DESIGNMASTER ON YARNJOBORDER_DESC.YJOB_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON YARNJOBORDER_DESC.YJOB_SHADEID = COLORMASTER.COLOR_id LEFT OUTER JOIN ITEMMASTER ON YARNJOBORDER_DESC.YJOB_ITEMID = ITEMMASTER.item_id ", " AND  YARNJOBORDER_DESC.YJOB_NO = " & TEMPJONO & " AND YARNJOBORDER_DESC.YJOB_YEARID = " & YearId & " ORDER BY GRIDSRNO")
                If dttable1.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable1.Rows
                        GRIDBEAM.Rows.Add(Val(DTR("GRIDSRNO")), DTR("ITEMNAME").ToString, DTR("DESIGNNO").ToString, DTR("COLOR").ToString, DTR("PARENTITEM").ToString, DTR("REFNO").ToString, Format(DTR("REED"), "0.00"), Format(DTR("PICKS"), "0.00"), Format(DTR("REEDSPACE"), "0.00"), Format(DTR("ENDS"), "0.000"), Format(DTR("MTRS"), "0.00"), DTR("DESCRIPTION").ToString, Format(DTR("OUTMTRS"), "0.00"), Val(DTR("DONE")), Val(DTR("CLOSED")))

                        If Convert.ToBoolean(DTR("DONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                            GRIDBEAM.Rows(GRIDBEAM.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                        End If

                        If Val(DTR("OUTMTRS")) > 0 Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                            GRIDBEAM.Rows(GRIDBEAM.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                        End If


                        If Convert.ToBoolean(DTR("CLOSED")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                            GRIDBEAM.Rows(GRIDBEAM.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                        End If
                    Next
                End If

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
            alParaval.Add(CMBPARTYNAME.Text.Trim)
            alParaval.Add(Val(TXTTOTALMTRS.Text.Trim))
            alParaval.Add(txtremarks.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            '*************************************************************************
            'GRID WARP

            Dim SrNo As String = ""
            Dim ItemName As String = ""
            Dim DESIGN As String = ""
            Dim Shade As String = ""
            Dim OtherItemName As String = ""
            Dim RefNo As String = ""
            Dim Reed As String = ""
            Dim Picks As String = ""
            Dim RS As String = ""
            Dim Ends As String = ""
            Dim Mtrs As String = ""
            Dim Description As String = ""
            Dim OUTMTRS As String = ""
            Dim DONE As String = ""
            Dim CLOSED As String = ""




            For Each row As Windows.Forms.DataGridViewRow In GRIDBEAM.Rows
                If row.IsNewRow Then Continue For
                If row.Cells(0).Value IsNot Nothing Then
                    If SrNo = "" Then
                        SrNo = Val(row.Cells(GSRNO.Index).Value)
                        ItemName = row.Cells(GITEMNAME.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        Shade = row.Cells(GSHADE.Index).Value.ToString
                        OtherItemName = row.Cells(GPARENTITEM.Index).Value.ToString
                        RefNo = row.Cells(GREFNO.Index).Value.ToString
                        Reed = Val(row.Cells(GREED.Index).Value)
                        Picks = Val(row.Cells(GPICKS.Index).Value)
                        RS = Val(row.Cells(GREEDSPACE.Index).Value)
                        Ends = Val(row.Cells(GENDS.Index).Value)
                        Mtrs = Val(row.Cells(GMTRS.Index).Value)
                        Description = row.Cells(GDESC.Index).Value.ToString
                        OUTMTRS = Val(row.Cells(GOUTMTRS.Index).Value)
                        DONE = Val(row.Cells(GDONE.Index).Value)
                        CLOSED = Val(row.Cells(GCLOSED.Index).Value)

                    Else
                        SrNo = SrNo & "|" & Val(row.Cells(GSRNO.Index).Value)
                        ItemName = ItemName & "|" & row.Cells(GITEMNAME.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        Shade = Shade & "|" & row.Cells(GSHADE.Index).Value.ToString
                        OtherItemName = OtherItemName & "|" & row.Cells(GPARENTITEM.Index).Value.ToString
                        RefNo = RefNo & "|" & row.Cells(GREFNO.Index).Value.ToString
                        Reed = Reed & "|" & Val(row.Cells(GREED.Index).Value)
                        Picks = Picks & "|" & Val(row.Cells(GPICKS.Index).Value)
                        RS = RS & "|" & Val(row.Cells(GREEDSPACE.Index).Value)
                        Ends = Ends & "|" & Val(row.Cells(GENDS.Index).Value)
                        Mtrs = Mtrs & "|" & Val(row.Cells(GMTRS.Index).Value)
                        Description = Description & "|" & row.Cells(GDESC.Index).Value.ToString
                        OUTMTRS = OUTMTRS & "|" & Val(row.Cells(GOUTMTRS.Index).Value)
                        DONE = DONE & "|" & Val(row.Cells(GDONE.Index).Value)
                        CLOSED = CLOSED & "|" & Val(row.Cells(GCLOSED.Index).Value)


                    End If
                End If
            Next

            alParaval.Add(SrNo)
            alParaval.Add(ItemName)
            alParaval.Add(DESIGN)
            alParaval.Add(Shade)
            alParaval.Add(OtherItemName)
            alParaval.Add(RefNo)
            alParaval.Add(Reed)
            alParaval.Add(Picks)
            alParaval.Add(RS)
            alParaval.Add(Ends)
            alParaval.Add(Mtrs)
            alParaval.Add(Description)
            alParaval.Add(OUTMTRS)
            alParaval.Add(DONE)
            alParaval.Add(CLOSED)




            '*************************************************************************




            Dim objDESIGN As New ClsYarnJobOrder
            objDESIGN.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objDESIGN.SAVE()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPJONO)
                IntResult = objDESIGN.UPDATE()
                MsgBox("Details Updated")
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
            If CMBITEMNAME.Text <> "" And ClientName = "SWPL" Then
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.SEARCH(" DESIGNCARD.DESIGN_CARDNO AS CARDNO, ISNULL(DESIGNCARD.DESIGN_FEPI, 0) AS FEPI, ISNULL(DESIGNCARD.DESIGN_FWIDTH, 0) AS FWIDTH, ISNULL(DESIGNCARD.DESIGN_FPPI, 0) AS FPPI, ISNULL(DESIGNCARD.DESIGN_FWT, 0) AS FWT, ISNULL(DESIGNCARD.DESIGN_DENTS, 0) AS DENTS, ISNULL(DESIGNCARD.DESIGN_TOTALDENTSMAIN, 0) AS TOTALDENTSMAIN, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEDENTS, 0) AS TOTALSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_TOTALDENTS, 0) AS TOTALDENTS, ISNULL(DESIGNCARD.DESIGN_WARPTTL, 0) AS WARPTTL, ISNULL(DESIGNCARD.DESIGN_WEFTTTL, 0) AS WEFTTTL, ISNULL(DESIGNCARD.DESIGN_GSM, 0) AS GSM, ISNULL(DESIGNCARD.DESIGN_SHAFTS, 0) AS SHAFTS, ISNULL(DESIGNCARD.DESIGN_TOTALWT, 0) AS TOTALWT, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGE, 0) AS LEFTSELVEDGE, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGE, 0) AS RIGHTSELVEDGE, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGEEND, 0) AS LEFTSELVEDGEEND, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGEEND, 0) AS RIGHTSELVEDGEEND, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGEDENTS, 0) AS LEFTSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGEDENTS, 0) AS RIGHTSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGETOTALEND, 0) AS LEFTSELVEDGETOTALEND, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGETOTALEND, 0) AS RIGHTSELVEDGETOTALEND, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEENDS, 0) AS TOTALSELVEDGEENDS, ISNULL(DESIGNCARD.DESIGN_REFNO, '') AS REFNO, ISNULL(DESIGNCARD.DESIGN_GREY, '') AS GREY, ISNULL(DESIGNCARD.DESIGN_ORDERNO, 0) AS ORDERNO, ISNULL(DESIGNCARD.DESIGN_DELDATE, '') AS DELDATE, ISNULL(DESIGNCARD.DESIGN_ORDERDATE, '') AS ORDERDATE, ISNULL(DESIGNCARD.DESIGN_MTRS, 0) AS MTRS, ISNULL(DESIGNCARD.DESIGN_NOOFPCS, 0) AS NOOFPCS, ISNULL(DESIGNCARD.DESIGN_LOOM, '') AS LOOM, ISNULL(DESIGNCARD.DESIGN_BEAMMTRS, 0) AS BEAMMTRS, ISNULL(DESIGNCARD.DESIGN_COVERFACTOR, '') AS COVERFACTOR, ISNULL(DESIGNCARD.DESIGN_EFFICIENCY, '') AS EFFICIENCY, ISNULL(DESIGNCARD.DESIGN_LOOMPROD, 0) AS LOOMPROD, ISNULL(DESIGNCARD.DESIGN_RPM, '') AS RPM, ISNULL(DESIGNCARD.DESIGN_GREYDELDATE, '') AS GREYDELDATE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPPE, 0) AS TOTALWARPPE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPBE, 0) AS TOTALWARPBE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPTE, 0) AS TOTALWARPTE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPWT, 0) AS TOTALWARPWT, ISNULL(DESIGNCARD.DESIGN_TOTALWARPCONS, 0) AS TOTALWARPCONS, ISNULL(DESIGNCARD.DESIGN_TOTALWARPRATE, 0) AS TOTALWARPRATE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPCOST, 0) AS TOTALWARPCOST, ISNULL(DESIGNCARD.DESIGN_TOTALWARPGRIDPE, 0) AS TOTALWARPGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEPE, 0) AS TOTALSELVEDGEPE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEBE, 0) AS TOTALSELVEDGEBE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGETE, 0) AS TOTALSELVEDGETE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEWT, 0) AS TOTALSELVEDGEWT, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGECONS, 0) AS TOTALSELVEDGECONS, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGERATE, 0) AS TOTALSELVEDGERATE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGECOST, 0) AS TOTALSELVEDGECOST, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEGRIDPE, 0) AS TOTALSELVEDGEGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTPE, 0) AS TOTALWEFTPE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTBE, 0) AS TOTALWEFTBE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTTE, 0) AS TOTALWEFTTE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTWT, 0) AS TOTALWEFTWT, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTCONS, 0) AS TOTALWEFTCONS, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTRATE, 0) AS TOTALWEFTRATE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTCOST, 0) AS TOTALWEFTCOST, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTGRIDPE, 0) AS TOTALWEFTGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALDRAWENDS, 0) AS TOTALDRAWENDS, ISNULL(DESIGNCARD.DESIGN_TOTALDRAWDENTS, 0) AS TOTALDRAWDENTS, ISNULL(DESIGNMASTER.DESIGN_NO, 0) AS DESIGNNO, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(DELATLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(GDELATLEDGERS.Acc_cmpname, '') AS GREYDELIVERYAT, DESIGNCARD.DESIGN_DATE AS DATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNCARD.DESIGN_REED, 0) AS REED, ISNULL(DESIGNCARD.DESIGN_REEDSPACE, 0) AS REEDSPACE, ISNULL(DESIGNCARD.DESIGN_PICKS, 0) AS PICKS, ISNULL(DESIGNCARD.DESIGN_MAINRS, 0) AS MAINRS, ISNULL(DESIGNCARD.DESIGN_THREADPERDENT, '') AS THREADPERDENT, ISNULL(WEAVEMASTER.WEAVE_name, '') AS WEAVE, ISNULL(DESIGNCARD.DESIGN_TOTALFINISHWT, 0) AS TOTALFINISHWT, ISNULL(DESIGNCARD.DESIGN_GREYWIDTH, 0) AS GREYWIDTH, ISNULL(DESIGNCARD.DESIGN_GREYWIDTHCM,0) AS GREYWIDTHCM, ISNULL(DESIGNCARD.DESIGN_FINISHWIDTHCM,0) AS FINISHWIDTHCM, ISNULL(DESIGNCARD.DESIGN_GREYLOOMMTR,0) AS GREYLOOMMTR, ISNULL(DESIGNCARD.DESIGN_BLENDPERCENTAGE,0) AS BLENDPER, ISNULL(DESIGNCARD.DESIGN_FINISHMETHOD,'') AS FINISHMETHOD, ISNULL(DESIGNCARD.DESIGN_QUALITIES,'') AS QUALITY, ISNULL(DESIGNCARD.DESIGN_QUALITYTYPE,'') AS QUALITYTYPE, ISNULL(DESIGNCARD.DESIGN_WARPWASTAGE,0) AS WARPWASTAGE, ISNULL(DESIGNCARD.DESIGN_WASTAGEPERCENTAGE,0) AS WASTAGEPER, ISNULL(DESIGNCARD.DESIGN_SHRINKAGEPERCENTAGE,0) AS SHRINKAGEPER, ISNULL(DESIGNCARD.DESIGN_WPP,0) AS WPP, ISNULL(DESIGNCARD.DESIGN_WEAVECOST,0) AS WEAVECOST, ISNULL(DESIGNCARD.DESIGN_GREYFABRICCOST,0) AS GREYFABCOST, ISNULL(DESIGNCARD.DESIGN_FINISHFABRICCOST,0) AS FINISHFABCOST, ISNULL(DESIGNCARD.DESIGN_PRODUCTIONPERDAY,0) AS PRODDAY, ISNULL(DESIGNCARD.DESIGN_PCSL,0) AS PCSL, ISNULL(DESIGNCARD.DESIGN_REEDSPACECM,0) AS REEDSPACECM,ISNULL(DESIGNCARD.DESIGN_TOTALENDS,0) AS TOTALENDS ,ISNULL(DESIGNCARD.DESIGN_ENDPERINCH,0) AS ENDPERINCH, ISNULL(DESIGNCARD.DESIGN_TOTALPEG,0) AS TOTALPEG, ISNULL(COLORMASTER.COLOR_name,'') AS SHADE , ISNULL(ITEMMASTER.ITEM_SELVEDGE,'') AS PARENTITEM ", "", " DESIGNCARD LEFT OUTER JOIN WEAVEMASTER ON DESIGNCARD.DESIGN_YEARID = WEAVEMASTER.WEAVE_yearid AND DESIGNCARD.DESIGN_WEAVEID = WEAVEMASTER.WEAVE_id LEFT OUTER JOIN LEDGERS AS GDELATLEDGERS ON DESIGNCARD.DESIGN_YEARID = GDELATLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_GREYDELATID = GDELATLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DELATLEDGERS ON DESIGNCARD.DESIGN_YEARID = DELATLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_DELIVERYATID = DELATLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON DESIGNCARD.DESIGN_YEARID = AGENTLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON DESIGNCARD.DESIGN_YEARID = LEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN ITEMMASTER ON DESIGNCARD.DESIGN_YEARID = ITEMMASTER.item_yearid AND DESIGNCARD.DESIGN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON DESIGNCARD.DESIGN_YEARID = DESIGNMASTER.DESIGN_yearid AND DESIGNCARD.DESIGN_ID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN COLORMASTER ON DESIGNCARD.DESIGN_SHADEID = COLORMASTER.COLOR_id AND DESIGNCARD.DESIGN_YEARID = COLORMASTER.COLOR_yearid   ", " AND  (ITEMMASTER.item_name = '" & CMBITEMNAME.Text.Trim & "') AND (DESIGNCARD.DESIGN_YEARID = " & YearId & ") ")
                If dttable.Rows.Count > 0 Then
                    Dim cardno As Integer
                    For Each dr As DataRow In dttable.Rows
                        cardno = Val(dr("CARDNO"))

                        TXTREED.Text = Val(dr("REED"))
                        TXTREEDSPACE.Text = Val(dr("REEDSPACE"))
                        TXTPICKS.Text = Val(dr("PICKS"))
                        CMBSHADE.Text = dr("SHADE")
                        TXTOTHERITEMNAME.Text = dr("PARENTITEM")
                        TXTTOTALENDS.Text = Val(dr("TOTALENDS"))
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
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE='ACCOUNTS'")
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
                OBJLEDGER.STRSEARCH = " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " and (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Enter(sender As Object, e As EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then FILLNAME(CMBPARTYNAME, EDIT, " and (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE='ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBPARTYNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND (GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY ='SUNDRY DEBTORS') AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBPARTYNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBPARTYNAME, CMBCODE, e, Me, TXTADD, " and (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' OR GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')", "Sundry Creditors", "ACCOUNTS")
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
            If MsgBox("Wish to Print Yarn Job order?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJPUR As New YarnDesign
                OBJPUR.MdiParent = MDIMain
                OBJPUR.FRMSTRING = "YARNJOBORDER"
                OBJPUR.WHERECLAUSE = "{YARNJOBORDER.YJOB_NO}=" & Val(TXTJONO.Text.Trim) & " and {YARNJOBORDER.YJOB_YEARID}=" & YearId
                OBJPUR.Show()
            End If
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
                alParaval.Add(YearId)

                Dim clspo As New ClsYarnJobOrder()
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

    Private Sub TXTDESCRIPTION_Validated(sender As Object, e As EventArgs) Handles TXTDESCRIPTION.Validated
        Try
            If CMBITEMNAME.Text.Trim <> "" And Val(TXTMTRS.Text) <> 0 Then
                FILLGRID()
            Else
                MsgBox("Please fill Proper Details")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        If GRIDDOUBLECLICK = False Then

            GRIDBEAM.Rows.Add(Val(TXTSRNO.Text.Trim), CMBITEMNAME.Text.Trim, CMBDESIGN.Text.Trim, CMBSHADE.Text.Trim, TXTOTHERITEMNAME.Text.Trim, TXTREFNO.Text.Trim, Format(Val(TXTREED.Text.Trim), "0.00"), Format(Val(TXTPICKS.Text.Trim), "0.00"), Format(Val(TXTREEDSPACE.Text.Trim), "0.00"), Format(Val(TXTTOTALENDS.Text.Trim), "0.00"), Format(Val(TXTMTRS.Text.Trim), "0.00"), TXTDESCRIPTION.Text.Trim)
            getsrno(GRIDBEAM)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDBEAM.Item(GSRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDBEAM.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
            GRIDBEAM.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDBEAM.Item(GSHADE.Index, TEMPROW).Value = CMBSHADE.Text.Trim
            GRIDBEAM.Item(GPARENTITEM.Index, TEMPROW).Value = TXTOTHERITEMNAME.Text.Trim
            GRIDBEAM.Item(GREFNO.Index, TEMPROW).Value = TXTREFNO.Text.Trim
            GRIDBEAM.Item(GREED.Index, TEMPROW).Value = Format(Val(TXTREED.Text.Trim), "0.00")
            GRIDBEAM.Item(GPICKS.Index, TEMPROW).Value = Format(Val(TXTPICKS.Text.Trim), "0.00")
            GRIDBEAM.Item(GREEDSPACE.Index, TEMPROW).Value = Format(Val(TXTREEDSPACE.Text.Trim), "0.00")
            GRIDBEAM.Item(GENDS.Index, TEMPROW).Value = Format(Val(TXTTOTALENDS.Text.Trim), "0.00")
            GRIDBEAM.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDBEAM.Item(GDESC.Index, TEMPROW).Value = TXTDESCRIPTION.Text.Trim
            GRIDDOUBLECLICK = False
        End If

        TOTAL()

        CMBITEMNAME.Text = ""
        CMBSHADE.Text = ""
        TXTOTHERITEMNAME.Clear()
        TXTREED.Clear()
        TXTREEDSPACE.Clear()
        TXTPICKS.Clear()
        TXTTOTALENDS.Clear()
        TXTREFNO.Clear()
        TXTMTRS.Clear()
        TXTDESCRIPTION.Clear()

        CMBITEMNAME.Enabled = True
        CMBITEMNAME.Focus()
        CMBDESIGN.Text = ""


        GRIDBEAM.FirstDisplayedScrollingRowIndex = GRIDBEAM.RowCount - 1
        TXTSRNO.Text = GRIDBEAM.RowCount + 1
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

    Private Sub GRIDBEAM_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDBEAM.CellDoubleClick
        EDITROW()
    End Sub

    Sub EDITROW()
        Try
            If GRIDBEAM.CurrentRow.Index >= 0 And GRIDBEAM.Item(GSRNO.Index, GRIDBEAM.CurrentRow.Index).Value <> Nothing Then
                If (Convert.ToBoolean(GRIDBEAM.Rows(GRIDBEAM.CurrentRow.Index).Cells(GDONE.Index).Value) = True Or (GRIDBEAM.Rows(GRIDBEAM.CurrentRow.Index).Cells(GOUTMTRS.Index).Value) > 0 Or (GRIDBEAM.Rows(GRIDBEAM.CurrentRow.Index).Cells(GCLOSED.Index).Value) = True) And UserName <> "Admin" Then
                    MsgBox("Item Locked. Item Used !!")
                    Exit Sub
                End If

                GRIDDOUBLECLICK = True

                TXTSRNO.Text = GRIDBEAM.Item(GSRNO.Index, GRIDBEAM.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = GRIDBEAM.Item(GITEMNAME.Index, GRIDBEAM.CurrentRow.Index).Value.ToString
                CMBDESIGN.Text = GRIDBEAM.Item(GDESIGN.Index, GRIDBEAM.CurrentRow.Index).Value.ToString
                CMBSHADE.Text = GRIDBEAM.Item(GSHADE.Index, GRIDBEAM.CurrentRow.Index).Value.ToString
                TXTOTHERITEMNAME.Text = GRIDBEAM.Item(GPARENTITEM.Index, GRIDBEAM.CurrentRow.Index).Value.ToString
                TXTREFNO.Text = GRIDBEAM.Item(GREFNO.Index, GRIDBEAM.CurrentRow.Index).Value.ToString
                TXTREED.Text = GRIDBEAM.Item(GREED.Index, GRIDBEAM.CurrentRow.Index).Value.ToString
                TXTPICKS.Text = GRIDBEAM.Item(GPICKS.Index, GRIDBEAM.CurrentRow.Index).Value.ToString
                TXTREEDSPACE.Text = GRIDBEAM.Item(GREEDSPACE.Index, GRIDBEAM.CurrentRow.Index).Value.ToString
                TXTTOTALENDS.Text = GRIDBEAM.Item(GENDS.Index, GRIDBEAM.CurrentRow.Index).Value.ToString
                TXTMTRS.Text = GRIDBEAM.Item(GMTRS.Index, GRIDBEAM.CurrentRow.Index).Value.ToString
                TXTDESCRIPTION.Text = GRIDBEAM.Item(GDESC.Index, GRIDBEAM.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDBEAM.CurrentRow.Index

                If ClientName = "SWPL" Then
                    TXTREFNO.Focus()
                Else
                    CMBITEMNAME.Focus()
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDBEAM_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDBEAM.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDBEAM.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbMERCHANT.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block


                'DONT ALLOW TO DELETE ANY ROW IF LOCKED IS VISIBLE
                If lbllocked.Visible = True Then
                    MessageBox.Show("Unable to Delete Row, Sale Order is Locked")
                    Exit Sub
                End If


                GRIDBEAM.Rows.RemoveAt(GRIDBEAM.CurrentRow.Index)
                TOTAL()
                getsrno(GRIDBEAM)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Keys.F12 And GRIDBEAM.RowCount > 0 Then
                If GRIDBEAM.CurrentRow.Cells(GITEMNAME.Index).Value <> "" Then GRIDBEAM.Rows.Add(CloneWithValues(GRIDBEAM.CurrentRow))
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub TOTAL()
        If GRIDBEAM.RowCount > 0 Then
            TXTTOTALMTRS.Text = "0.00"
            For Each row As DataGridViewRow In GRIDBEAM.Rows
                If Val(row.Cells(GMTRS.Index).EditedFormattedValue) > 0 Then TXTTOTALMTRS.Text = Format(Val(TXTTOTALMTRS.Text) + Val(row.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
            Next
        End If

    End Sub

    Private Sub TXTCOPYSONONO_Validated(sender As Object, e As EventArgs) Handles TXTCOPYSONO.Validated
        Try
            If Val(TXTCOPYSONO.Text.Trim) = 0 Then Exit Sub

            Dim OBJCMN As New ClsCommon
            Dim dttable2 As DataTable = OBJCMN.SEARCH(" SALEORDER_DESC.SO_GRIDSRNO AS GRIDSRNO , ISNULL(QUALITYMASTER.QUALITY_name,'') AS QUALITY, ISNULL(COLORMASTER.COLOR_name,'') AS COLOR, ISNULL(ITEMMASTER.item_name,'') AS ITEMNAME, ISNULL(SALEORDER_DESC.SO_MTRS,0) AS MTRS ", "", " SALEORDER_DESC LEFT OUTER JOIN QUALITYMASTER ON SALEORDER_DESC.SO_QUALITYID = QUALITYMASTER.QUALITY_id AND SALEORDER_DESC.SO_YEARID = QUALITYMASTER.QUALITY_yearid LEFT OUTER JOIN COLORMASTER ON SALEORDER_DESC.SO_YEARID = COLORMASTER.COLOR_yearid AND SALEORDER_DESC.SO_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON SALEORDER_DESC.SO_DESIGNID = DESIGNMASTER.DESIGN_ID AND SALEORDER_DESC.SO_YEARID = DESIGNMASTER.DESIGN_YEARID  LEFT OUTER JOIN ITEMMASTER ON SALEORDER_DESC.SO_YEARID = ITEMMASTER.item_yearid AND SALEORDER_DESC.SO_ITEMID = ITEMMASTER.item_id  ", " AND SALEORDER_DESC.SO_NO = " & Val(TXTCOPYSONO.Text.Trim) & " AND SALEORDER_DESC.SO_YEARID = " & YearId & " ORDER BY GRIDSRNO")
            If dttable2.Rows.Count = 0 Then
                MsgBox("Sale Order Not Found", MsgBoxStyle.Critical, "TEXTRADE")
                TXTCOPYSONO.Clear()
                Exit Sub
            End If

            If MsgBox("Copy data from Sale Order No. " & TXTCOPYSONO.Text.Trim & "?", MsgBoxStyle.YesNo, "TEXPRO") = MsgBoxResult.No Then
                TXTCOPYSONO.Clear()
                Exit Sub
            End If
            If dttable2.Rows.Count > 0 Then
                GRIDBEAM.RowCount = 0   ' Clear existing grid rows

                For Each DTR As DataRow In dttable2.Rows
                    GRIDBEAM.Rows.Add(
                        Val(DTR("GRIDSRNO")),
                        DTR("ITEMNAME").ToString,
                        "",'DESIGN
                        DTR("COLOR").ToString,
                        DTR("QUALITY").ToString,
                        "",'DTR("REFNO").ToString,
                         "0.00",'Format(Val(DTR("REED")), "0.00"),
                        "0.00",'Format(Val(DTR("PICKS")), "0.00"),
                        "0.00",'Format(Val(DTR("REEDSPACE")), "0.00"),
                        "0.00",'Format(Val(DTR("ENDS")), "0.000"),
                        Format(Val(DTR("MTRS")), "0.00"),
                        "")
                Next
                getsrno(GRIDBEAM)
                TOTAL()
            Else
                MsgBox("No detail lines found in this Sale Order", MsgBoxStyle.Information, "TEXTRADE")
            End If

            TXTCOPYSONO.Clear()
            CMBNAME.Focus()

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

    Private Sub TXTJONO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTJONO.KeyPress, TXTPONO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub YarnJobOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1) Then       'for scheduling
                ' TabControl1.SelectedIndex = (0)
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2) Then       'for ITEM DETAILS
                ' TabControl1.SelectedIndex = (1)
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)

            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Private Sub YarnJobOrder_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'If ClientName = "MMC" Then
            '    CMBSHADE.BackColor = Color.White
            '    TXTOTHERITEMNAME.ReadOnly = False
            '    TXTOTHERITEMNAME.BackColor = Color.White
            '    TXTREED.ReadOnly = False
            '    TXTREED.BackColor = Color.White
            '    TXTREEDSPACE.ReadOnly = False
            '    TXTREEDSPACE.BackColor = Color.White
            '    TXTPICKS.ReadOnly = False
            '    TXTPICKS.BackColor = Color.White
            '    TXTTOTALENDS.ReadOnly = False
            '    TXTTOTALENDS.BackColor = Color.White
            '    CMBITEMNAME.Enabled = True
            'End If
            If ClientName = "MMC" Then
                CMDSELECTSO.Visible = True
                TXTCOPYSONO.Enabled = False
                CMBDESIGN.TabStop = True
                CMBDESIGN.Enabled = True
            End If

            If ClientName = "SWPL" Then
                CMBSHADE.Enabled = False
                CMBSHADE.TabStop = False

                CMBSHADE.BackColor = Color.Linen
                TXTOTHERITEMNAME.ReadOnly = True
                TXTOTHERITEMNAME.Enabled = False
                TXTOTHERITEMNAME.TabStop = False
                TXTOTHERITEMNAME.BackColor = Color.Linen
                TXTREED.ReadOnly = True
                TXTREED.Enabled = False
                TXTREED.TabStop = False
                TXTREED.BackColor = Color.Linen
                TXTREEDSPACE.ReadOnly = True
                TXTREEDSPACE.Enabled = False
                TXTREEDSPACE.TabStop = False
                TXTREEDSPACE.BackColor = Color.Linen
                TXTPICKS.ReadOnly = True
                TXTPICKS.Enabled = False
                TXTPICKS.TabStop = False
                TXTPICKS.BackColor = Color.Linen
                TXTTOTALENDS.ReadOnly = True
                TXTTOTALENDS.Enabled = False
                TXTTOTALENDS.TabStop = False
                TXTTOTALENDS.BackColor = Color.Linen
                CMBITEMNAME.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validating(sender As Object, e As CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(sender As Object, e As EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSO_Click(sender As Object, e As EventArgs) Handles CMDSELECTSO.Click
        Try
            If ClientName = "MMC" Then
                If CMBNAME.Text.Trim = "" Then
                    MsgBox("Select Party Name", MsgBoxStyle.Critical)
                    CMBNAME.Focus()
                    Exit Sub
                End If
                Dim OBJCMN As New ClsCommon
                'Dim DT1 As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS LASTRATE", "", " INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN INVOICEMASTER ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO  AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN LEDGERS ON ACC_ID = INVOICE_LEDGERID", " AND LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & GRIDINVOICE.Item(GITEMNAME.Index, GRIDINVOICE.CurrentRow.Index).Value & "' AND INVOICEMASTER.INVOICE_DATE < '" & Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy") & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER.INVOICE_NO DESC")
                'If DT1.Rows.Count > 0 Then LBLRATE.Text = Format(Val(DT1.Rows(0).Item("LASTRATE")), "0.00")

                Dim DTSO As New DataTable
                Dim OBJSELECTSO As New SelectSO
                OBJSELECTSO.PARTYNAME = CMBNAME.Text.Trim
                OBJSELECTSO.ShowDialog()
                DTSO = OBJSELECTSO.DT

                If ClientName = "MMC" Then
                    Dim rowsToDelete As New List(Of DataRow)
                    For Each dr As DataRow In DTSO.Rows
                        If dr("TYPE").ToString = "YARNSALEORDER" Or dr("TYPE").ToString = "OPENINGYARNSALEORDER" Then
                            rowsToDelete.Add(dr)
                        End If
                    Next
                    For Each dr As DataRow In rowsToDelete
                        DTSO.Rows.Remove(dr)
                    Next
                End If

                If DTSO.Rows.Count > 0 Then

                    ''  GETTING DISTINCT SONO NO IN TEXTBOX
                    Dim DV As DataView = DTSO.DefaultView
                    Dim NEWDT As DataTable = DV.ToTable(True, "SONO")

                    'txtremarks.Text = DTSO.Rows(0).Item("REMARKS")


                    'BEFORE ADDING THE ROW IN ORDERDER GRID CHECK WHETHER SAME ORDERNO AN SRNO IS PRESENT IN GRID OR NOT
                    For Each DTROW As DataRow In DTSO.Rows
                        'For Each ROW As DataGridViewRow In GRIDORDER.Rows
                        '    If Val(ROW.Cells(OFROMNO.Index).Value) = Val(DTROW("SONO")) And Val(ROW.Cells(OFROMSRNO.Index).Value) = Val(DTROW("GRIDSRNO")) And ROW.Cells(OFROMTYPE.Index).Value = DTROW("TYPE") Then GoTo NEXTLINE
                        'Next


                        GRIDBEAM.Rows.Add(0, DTROW("ITEMNAME"), DTROW("DESIGN"), DTROW("COLOR"), "", "", 0, 0, 0, 0, Format(Val(DTROW("MTRS")), "0.00"), "")

NEXTLINE:
                    Next
                    getsrno(GRIDBEAM)
                    'getsrno(GRIDINVOICE)
                    CMDSELECTSO.Enabled = False

                    TOTAL()
                    GRIDBEAM.FirstDisplayedScrollingRowIndex = GRIDBEAM.RowCount - 1
                    If GRIDBEAM.RowCount > 0 Then
                        GRIDBEAM.Focus()
                        GRIDBEAM.CurrentCell = GRIDBEAM.Rows(0).Cells(GMTRS.Index)
                    End If
                    'If ClientName = "ABHEE" Then
                    '    GRIDBEAM.RowCount = 0
                    'End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Enter(sender As Object, e As EventArgs) Handles CMBSHADE.Enter
        Try
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSHADE, e, Me, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTREED_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTREED.KeyPress, TXTPICKS.KeyPress, TXTREEDSPACE.KeyPress, TXTTOTALENDS.KeyPress, TXTTOTALMTRS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub
End Class