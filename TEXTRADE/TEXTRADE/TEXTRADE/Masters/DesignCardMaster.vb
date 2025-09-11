
Imports System.ComponentModel
Imports System.IO
Imports BL
Imports DevExpress.Charts.Native
Imports DevExpress.DashboardCommon.Native
Imports DevExpress.UIAutomation
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPivotGrid.Design
Public Class DesignCardMaster
    Public EDIT As Boolean              'Used for edit
    Public tempdesignno As String           'Used for edit name
    Public tempid As Integer            'Used for edit id
    Dim GRIDDOUBLECLICK, GRIDWPDOUBLECLICK, GRIDSELDOUBLECLICK, GRIDWEFTDOUBLECLICK, GRIDWEFTPDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPPROW, TEMPWPROW, TEMPSELROW, TEMPWEFTROW, TEMPWEFTPROW As Integer
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPUPLOADROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Ep.Clear()
            'If Not errorvalid() Then
            '    Exit Sub
            'End If
            Dim IntResult As Integer

            Dim alParaval As New ArrayList
            If txtcardno.ReadOnly = True Then
                alParaval.Add(0)
            Else
                alParaval.Add(Val(txtcardno.Text.Trim))
            End If
            alParaval.Add(CMBITEMNAME.Text.Trim)
            alParaval.Add(CMBDESIGNNO.Text.Trim)

            alParaval.Add(Val(TXTREED.Text.Trim))
            alParaval.Add(Val(TXTREEDSPACE.Text.Trim))
            alParaval.Add(Val(TXTPICKS.Text.Trim))
            alParaval.Add(Val(TXTMAINRS.Text.Trim))
            alParaval.Add(Val(TXTTHREADPERDENT.Text.Trim))
            alParaval.Add(Val(TXTFEPI.Text.Trim))
            alParaval.Add(Val(TXTFWIDTH.Text.Trim))
            alParaval.Add(Val(TXTFPPI.Text.Trim))
            alParaval.Add(Val(TXTFWT.Text.Trim))
            alParaval.Add(Val(TXTDENTS.Text.Trim))
            alParaval.Add(Val(TXTTOTALDENTSMAIN.Text.Trim))
            alParaval.Add(Val(TXTTOTALSELVEDGEDENTS.Text.Trim))
            alParaval.Add(Val(TXTTOTALDENTS.Text.Trim))
            alParaval.Add(Val(TXTWARPTL.Text.Trim))
            alParaval.Add(Val(TXTWEFTTL.Text.Trim))
            alParaval.Add(Val(TXTGSM.Text.Trim))
            alParaval.Add(CMBWEAVE.Text.Trim)
            alParaval.Add(CMBSHAFTS.Text.Trim)
            alParaval.Add(Val(TXTTOTALWT.Text.Trim))
            'SLEAVEDGE
            alParaval.Add(Val(TXTLEFTSEL.Text.Trim))
            alParaval.Add(Val(TXTRIGHTSEL.Text.Trim))
            alParaval.Add(Val(TXTLEFTSELENDS.Text.Trim))
            alParaval.Add(Val(TXTRIGHTSELENDS.Text.Trim))
            alParaval.Add(Val(TXTLEFTSELDENTS.Text.Trim))
            alParaval.Add(Val(TXTRIGHTSELDENTS.Text.Trim))
            alParaval.Add(Val(TXTLEFTSELTOTALENDS.Text.Trim))
            alParaval.Add(Val(TXTRIGHTSELTOTALENDS.Text.Trim))
            alParaval.Add(Val(TXTTOTALSELENDS.Text.Trim))

            'party and other ledgers
            alParaval.Add(TXTREFNO.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBAGENTNAME.Text.Trim)
            alParaval.Add(CMBDELAT.Text.Trim)
            alParaval.Add(CMBGREY.Text.Trim)
            alParaval.Add(Val(TXTORDERNO.Text.Trim))
            If IsDate(DELDATE.Text.Trim) Then
                alParaval.Add(Format(CDate(DELDATE.Text.Trim), "MM/dd/yyyy"))
            Else
                alParaval.Add("")
            End If
            If IsDate(ORDERDATE.Text.Trim) Then
                alParaval.Add(Format(CDate(ORDERDATE.Text.Trim), "MM/dd/yyyy"))
            Else
                alParaval.Add("")
            End If



            'OTHERS
            alParaval.Add(Val(TXTMTRS.Text.Trim))          ' Piece Mtrs
            alParaval.Add(Val(TXTNOOFPCS.Text.Trim))            ' No of Pcs
            alParaval.Add(CMBLOOM.Text.Trim)                    ' Loom (ComboBox)
            alParaval.Add(Val(TXTBEAMMTRS.Text.Trim))           ' Beam Mtrs
            alParaval.Add(Val(TXTCOVERFACTOR.Text.Trim))        ' Cover Factor
            alParaval.Add(Val(TXTEFFICIENCY.Text.Trim))         ' Efficiency
            alParaval.Add(Val(TXTLOOMPROD.Text.Trim))           ' Loom Prod
            alParaval.Add(Val(TXTRPM.Text.Trim))                ' RPM
            alParaval.Add(CMBGREYDELAT.Text.Trim)          ' Grey Delivery At (ComboBox)
            If IsDate(GREYDELDATE.Text.Trim) Then alParaval.Add(Format(Convert.ToDateTime(GREYDELDATE.Text).Date, "MM/dd/yyyy")) Else alParaval.Add("")
            'TOTAL
            alParaval.Add(Val(TXTTOTALWARPPE.Text.Trim))        ' P.E. (Possible: Ends per repeat)
            alParaval.Add(Val(TXTTOTALWARPBE.Text.Trim))        ' B.E. (Possible: Ends for Border)
            alParaval.Add(Val(TXTTOTALWARPTE.Text.Trim))       ' T.E. (Possible: Ends for Total)
            alParaval.Add(Val(TXTTOTALWARPWT.Text.Trim))       ' Wt (Warp Weight)
            alParaval.Add(Val(TXTTOTALWARPCONS.Text.Trim))     ' Cons (Warp Consumption)
            alParaval.Add(Val(TXTTOTALWARPRATE.Text.Trim))     ' Rate (Rate per unit)
            alParaval.Add(Val(TXTTOTALWARPCOST.Text.Trim))     ' Cost (Warp Cost)
            alParaval.Add(Val(TXTTOTALWARPGRIDPE.Text.Trim))
            'Selvedge Total
            alParaval.Add(Val(TXTTOTALSELPE.Text.Trim))        ' P.E. (Selvedge)
            alParaval.Add(Val(TXTTOTALSELBE.Text.Trim))        ' B.E. (Selvedge)
            alParaval.Add(Val(TXTTOTALSELTE.Text.Trim))        ' T.E. (Selvedge)
            alParaval.Add(Val(TXTTOTALSELWT.Text.Trim))        ' Wt (Selvedge)
            alParaval.Add(Val(TXTTOTALSELCONS.Text.Trim))      ' Cons (Selvedge)
            alParaval.Add(Val(TXTSELTOTALRATE.Text.Trim))      ' Rate (Selvedge)
            alParaval.Add(Val(TXTSELTOTALCOST.Text.Trim))      ' Cost (Selvedge)
            'Weft Total
            alParaval.Add(Val(TXTTOTALWEFTPE.Text.Trim))        ' P.E. (Weft)
            alParaval.Add(Val(TXTTOTALWEFTBE.Text.Trim))        ' B.E. (Weft)
            alParaval.Add(Val(TXTTOTALWEFTTE.Text.Trim))        ' T.E. (Weft)
            alParaval.Add(Val(TXTTOTALWEFTWT.Text.Trim))        ' Wt (Weft Weight)
            alParaval.Add(Val(TXTTOTALWEFTCONS.Text.Trim))      ' Cons (Weft Consumption)
            alParaval.Add(Val(TXTTOTALWEFTRATE.Text.Trim))      ' Rate (Weft Rate)
            alParaval.Add(Val(TXTTOTALWEFTCOST.Text.Trim))      ' Cost (Weft Cost)
            alParaval.Add(Val(TXTTOTALWEFTGRIDPE.Text.Trim))        ' P.E. (Repeated for field order continuity)

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


            Dim WARPGRIDSRNO As String = ""
            Dim WARPGRIDPE As String = ""
            Dim WARPGRIDSYM As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDWARP.Rows
                If row.Cells(0).Value IsNot Nothing Then
                    If WARPGRIDSRNO = "" Then
                        WARPGRIDSRNO = Val(row.Cells(WPSRNO.Index).Value)
                        WARPGRIDPE = Val(row.Cells(WPENDS.Index).Value)
                        WARPGRIDSYM = row.Cells(WPSYM.Index).Value.ToString
                    Else
                        WARPGRIDSRNO = WARPGRIDSRNO & "|" & Val(row.Cells(WPSRNO.Index).Value)
                        WARPGRIDPE = WARPGRIDPE & "|" & Val(row.Cells(WPENDS.Index).Value)
                        WARPGRIDSYM = WARPGRIDSYM & "|" & row.Cells(WPSYM.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(WARPGRIDSRNO)
            alParaval.Add(WARPGRIDPE)
            alParaval.Add(WARPGRIDSYM)
            '*************************************************************************
            'GRID SLEVAGE
            Dim ALOSrNo As String = ""
            Dim ALOSym As String = ""
            Dim ALOYarnQuality As String = ""
            Dim ALODenier As String = ""
            Dim ALOMillName As String = ""
            Dim ALOShade As String = ""
            Dim ALOPE As String = ""
            Dim ALOBE As String = ""
            Dim ALOTE As String = ""
            Dim ALOWt As String = ""
            Dim ALOCons As String = ""
            Dim ALORate As String = ""
            Dim ALOCost As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDSELVEDGE.Rows
                If row.Cells(SSRNO.Index).Value IsNot Nothing Then
                    If ALOSrNo = "" Then
                        ALOSrNo = row.Cells(SSRNO.Index).Value
                        ALOSym = row.Cells(SSYM.Index).Value.ToString
                        ALOYarnQuality = row.Cells(SQUALITY.Index).Value.ToString
                        ALODenier = Val(row.Cells(SDENIER.Index).Value)
                        ALOMillName = row.Cells(SMILL.Index).Value.ToString
                        ALOShade = row.Cells(SSHADE.Index).Value.ToString
                        ALOPE = Val(row.Cells(SPE.Index).Value)
                        ALOBE = Val(row.Cells(SBE.Index).Value)
                        ALOTE = Val(row.Cells(SENDS.Index).Value)
                        ALOWt = Val(row.Cells(SWT.Index).Value)
                        ALOCons = Val(row.Cells(SCONS.Index).Value)
                        ALORate = Val(row.Cells(SRATE.Index).Value)
                        ALOCost = Val(row.Cells(SCOST.Index).Value)
                    Else
                        ALOSrNo = ALOSrNo & "|" & row.Cells(SSRNO.Index).Value
                        ALOSym = ALOSym & "|" & row.Cells(SSYM.Index).Value.ToString
                        ALOYarnQuality = ALOYarnQuality & "|" & row.Cells(SQUALITY.Index).Value.ToString
                        ALODenier = ALODenier & "|" & Val(row.Cells(SDENIER.Index).Value)
                        ALOMillName = ALOMillName & "|" & row.Cells(SMILL.Index).Value.ToString
                        ALOShade = ALOShade & "|" & row.Cells(SSHADE.Index).Value.ToString
                        ALOPE = ALOPE & "|" & Val(row.Cells(SPE.Index).Value)
                        ALOBE = ALOBE & "|" & Val(row.Cells(SBE.Index).Value)
                        ALOTE = ALOTE & "|" & Val(row.Cells(SENDS.Index).Value)
                        ALOWt = ALOWt & "|" & Val(row.Cells(SWT.Index).Value)
                        ALOCons = ALOCons & "|" & Val(row.Cells(SCONS.Index).Value)
                        ALORate = ALORate & "|" & Val(row.Cells(SRATE.Index).Value)
                        ALOCost = ALOCost & "|" & Val(row.Cells(SCOST.Index).Value)
                    End If
                End If

            Next

            ' Add these variables to your parameter list (ArrayList, etc.)
            alParaval.Add(ALOSrNo)
            alParaval.Add(ALOSym)
            alParaval.Add(ALOYarnQuality)
            alParaval.Add(ALODenier)
            alParaval.Add(ALOMillName)
            alParaval.Add(ALOShade)
            alParaval.Add(ALOPE)
            alParaval.Add(ALOBE)
            alParaval.Add(ALOTE)
            alParaval.Add(ALOWt)
            alParaval.Add(ALOCons)
            alParaval.Add(ALORate)
            alParaval.Add(ALOCost)
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


            Dim WEFTTRSrNo As String = ""
            Dim WEFTTRPE As String = ""
            Dim WEFTTRSym As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDWEFTPATTERN.Rows
                If row.Cells(FSRNO.Index).Value IsNot Nothing Then
                    If WEFTTRSrNo = "" Then
                        WEFTTRSrNo = Val(row.Cells(FSRNO.Index).Value)
                        WEFTTRPE = Val(row.Cells(FPENDS.Index).Value)
                        WEFTTRSym = row.Cells(FPSYM.Index).Value.ToString
                    Else
                        WEFTTRSrNo = WEFTTRSrNo & "|" & Val(row.Cells(FSRNO.Index).Value)
                        WEFTTRPE = WEFTTRPE & "|" & Val(row.Cells(FPENDS.Index).Value)
                        WEFTTRSym = WEFTTRSym & "|" & row.Cells(FPSYM.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(WEFTTRSrNo)
            alParaval.Add(WEFTTRPE)
            alParaval.Add(WEFTTRSym)


            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            Dim objDESIGN As New ClsDesignCardMaster
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
                alParaval.Add(tempid)
                IntResult = objDESIGN.UPDATE()
                MsgBox("Details Updated")
            End If
            EDIT = False

            clear()
            EDIT = False
            CMBDESIGNNO.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub getmax_SO_no()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(DESIGN_CARDno),0) + 1 ", "DESIGNCARD", " AND DESIGN_cmpid=" & CmpId & " and DESIGN_locationid=" & Locationid & " and DESIGN_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            txtcardno.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub
    Sub clear()
        getmax_SO_no()

        CMBDESIGNNO.Text = ""
        CMBITEMNAME.Text = ""
        TXTREED.Clear()
        TXTREEDSPACE.Clear()
        TXTPICKS.Clear()
        TXTMAINRS.Clear()
        TXTTHREADPERDENT.Clear()
        TXTFEPI.Clear()
        TXTFWIDTH.Clear()
        TXTFPPI.Clear()
        TXTFWT.Clear()
        TXTDENTS.Clear()
        TXTTOTALDENTSMAIN.Clear()
        TXTTOTALSELVEDGEDENTS.Clear()
        TXTTOTALDENTS.Clear()
        TXTWARPTL.Clear()
        TXTWEFTTL.Clear()
        TXTGSM.Clear()
        CMBWEAVE.Text = ""
        CMBSHAFTS.Text = ""
        TXTTOTALWT.Clear()
        'SLEAVEDGE
        TXTLEFTSEL.Clear()
        TXTRIGHTSEL.Clear()
        TXTLEFTSELENDS.Clear()
        TXTRIGHTSELENDS.Clear()
        TXTLEFTSELDENTS.Clear()
        TXTRIGHTSELDENTS.Clear()
        TXTLEFTSELTOTALENDS.Clear()
        TXTRIGHTSELTOTALENDS.Clear()
        TXTTOTALSELENDS.Clear()
        TXTREFNO.Clear()
        CMBNAME.Text = ""
        CMBAGENTNAME.Text = ""
        CMBDELAT.Text = ""
        DELDATE.Text = Now.Date
        'OTHERS
        TXTMTRS.Clear()          ' Piece Mtrs
        TXTNOOFPCS.Clear()            ' No of Pcs
        CMBLOOM.Text = ""                    ' Loom (ComboBox)
        TXTBEAMMTRS.Clear()           ' Beam Mtrs
        TXTCOVERFACTOR.Clear()        ' Cover Factor
        TXTEFFICIENCY.Clear()         ' Efficiency
        TXTLOOMPROD.Clear()           ' Loom Prod
        TXTRPM.Clear()                ' RPM
        CMBGREYDELAT.Text = ""          ' Grey Delivery At (ComboBox)
        GREYDELDATE.Text = Now.Date
        'TOTAL
        TXTTOTALWARPPE.Clear()       ' P.E. (Possible: Ends per repeat)
        TXTTOTALWARPBE.Clear()       ' B.E. (Possible: Ends for Border)
        TXTTOTALWARPTE.Clear()      ' T.E. (Possible: Ends for Total)
        TXTTOTALWARPWT.Clear()      ' Wt (Warp Weight)
        TXTTOTALWARPCONS.Clear()    ' Cons (Warp Consumption)
        TXTTOTALWARPRATE.Clear()    ' Rate (Rate per unit)
        TXTTOTALWARPCOST.Clear()    ' Cost (Warp Cost)
        TXTTOTALWARPGRIDPE.Clear()
        'Selvedge Total
        TXTTOTALSELPE.Clear()       ' P.E. (Selvedge)
        TXTTOTALSELBE.Clear()       ' B.E. (Selvedge)
        TXTTOTALSELTE.Clear()       ' T.E. (Selvedge)
        TXTTOTALSELWT.Clear()       ' Wt (Selvedge)
        TXTTOTALSELCONS.Clear()     ' Cons (Selvedge)
        TXTSELTOTALRATE.Clear()     ' Rate (Selvedge)
        TXTSELTOTALCOST.Clear()     ' Cost (Selvedge)
        'Weft Total
        TXTTOTALWEFTPE.Clear()       ' P.E. (Weft)
        TXTTOTALWEFTBE.Clear()       ' B.E. (Weft)
        TXTTOTALWEFTTE.Clear()       ' T.E. (Weft)
        TXTTOTALWEFTWT.Clear()       ' Wt (Weft Weight)
        TXTTOTALWEFTCONS.Clear()     ' Cons (Weft Consumption)
        TXTTOTALWEFTRATE.Clear()     ' Rate (Weft Rate)
        TXTTOTALWEFTCOST.Clear()     ' Cost (Weft Cost)
        TXTTOTALWEFTGRIDPE.Clear()       ' P.E. (Repeated for field order continuity)
        'WARPMATCHING TEXTBOXES
        TXTGRIDPE.Clear()
        CMBGRIDSYM.Text = ""
        'GRID WARP
        GRIDWARP.Rows.Clear()
        'GRID WARP PATTERN
        GRIDWARPPATTERN.Rows.Clear()
        'GRID SLEVAGE
        GRIDSELVEDGE.Rows.Clear()
        'GRID WEFT
        GRIDWEFT.Rows.Clear()
        'GRID WEFT PATTERN
        GRIDWEFTPATTERN.Rows.Clear()

    End Sub
    Private Function errorvalid() As Boolean

        Dim bln As Boolean = True

        If CMBDESIGNNO.Text.Trim.Length = 0 Then
            Ep.SetError(CMBDESIGNNO, "Fill Design No")
            bln = False
        End If
    End Function

    Private Sub DesignCardMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            Cursor.Current = Cursors.WaitCursor
            fillcmb()
            clear()

            If EDIT = True Then
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub DesignCardMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillcmb()
        Dim OBJCMN As New ClsCommon
        'Dim DT As DataTable = OBJCMN.SEARCH("DESIGN_NO", "", " DESIGNMASTER ", " and DESIGN_cmpid = " & CmpId & " and DESIGN_locationid = " & Locationid & " and DESIGN_yearid = " & YearId)
        'If DT.Rows.Count > 0 Then
        '    DT.DefaultView.Sort = "DESIGN_NO"
        '    CMBDESIGNNO.DataSource = DT
        '    CMBDESIGNNO.DisplayMember = "DESIGN_NO"
        '    CMBDESIGNNO.Text = tempdesignno
        'End If
        FILLDESIGN(CMBDESIGNNO, CMBITEMNAME.Text.Trim)
        FILLCOLOR(CMBWARPSHADE, "", "")
        FILLCOLOR(CMBSELSHADE, "", "")
        FILLCOLOR(cmbweftshade, "", "")
        If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEM_FRMSTRING = 'MERCHANT'")
        FILLMILL(CMBWARPMILLNAME, EDIT)
        FILLMILL(CMBWEFTMILLNAME, EDIT)
        FILLMILL(CMBSELMILLNAME, EDIT)
        fillYARNQUALITY(CMBSELYARNQUALITY, EDIT)
        fillYARNQUALITY(CMBWEFTYARNQUALITY, EDIT)
        fillYARNQUALITY(CMBWARPQUALITY, EDIT)
        FILLLOOM(CMBLOOM, EDIT)
        FILLWEAVE(CMBWEAVE, EDIT)
        If CMBAGENTNAME.Text.Trim = "" Then FILLNAME(CMBAGENTNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
        If CMBDELAT.Text.Trim = "" Then FILLNAME(CMBDELAT, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        If CMBGREYDELAT.Text.Trim = "" Then FILLNAME(CMBGREYDELAT, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE<>'ACCOUNTS'")
    End Sub
    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            'If edit = False Then
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
            'End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub fillwarpgrid()

        If GRIDDOUBLECLICK = False Then
            GRIDWARP.Rows.Add(Val(TXTWARPSRNO.Text.Trim), TXTWARPSYMBOL.Text.Trim, CMBWARPQUALITY.Text.Trim, TXTWARPDENIER.Text.Trim, CMBWARPMILLNAME.Text.Trim, CMBWARPSHADE.Text.Trim, Val(TXTWARPPE.Text.Trim), Val(TXTWARPBE.Text.Trim), Val(TXTWARPTE.Text.Trim), Val(TXTWARPWT.Text.Trim), Val(TXTWARPCONS.Text.Trim), Val(TXTWARPRATE.Text.Trim), Val(TXTWARPCOST.Text.Trim))
            getsrno(GRIDWARP)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDWARP.Item(WSRNO.Index, TEMPROW).Value = Val(TXTWARPSRNO.Text.Trim)
            GRIDWARP.Item(WSYM.Index, TEMPROW).Value = TXTWARPSYMBOL.Text.Trim
            GRIDWARP.Item(WQUALITY.Index, TEMPROW).Value = CMBWARPQUALITY.Text.Trim
            GRIDWARP.Item(WDENIER.Index, TEMPROW).Value = TXTWARPDENIER.Text.Trim
            GRIDWARP.Item(WMILL.Index, TEMPROW).Value = CMBWARPMILLNAME.Text.Trim
            GRIDWARP.Item(WSHADE.Index, TEMPROW).Value = CMBWARPSHADE.Text.Trim
            GRIDWARP.Item(WPE.Index, TEMPROW).Value = Val(TXTWARPPE.Text.Trim)
            GRIDWARP.Item(WBE.Index, TEMPROW).Value = Val(TXTWARPBE.Text.Trim)
            GRIDWARP.Item(WENDS.Index, TEMPROW).Value = Val(TXTWARPTE.Text.Trim)
            GRIDWARP.Item(WWT.Index, TEMPROW).Value = Val(TXTWARPWT.Text.Trim)
            GRIDWARP.Item(WCONS.Index, TEMPROW).Value = Val(TXTWARPCONS.Text.Trim)
            GRIDWARP.Item(WRATE.Index, TEMPROW).Value = Val(TXTWARPRATE.Text.Trim)
            GRIDWARP.Item(WCOST.Index, TEMPROW).Value = Val(TXTWARPCOST.Text.Trim)
            TEMPROW = GRIDWARP.CurrentRow.Index
            TXTWARPSRNO.Focus()
            GRIDDOUBLECLICK = False
        End If
        GRIDWARP.ClearSelection()
        TXTWARPSYMBOL.Focus()
        clearwarp()
        TOTALWARP()
        COPYSYM()
        If GRIDWARP.RowCount > 0 Then
            TXTWARPSRNO.Text = Val(GRIDWARP.Rows(GRIDWARP.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDINVOICE.RowCount) + 1
        Else
            TXTWARPSRNO.Text = 1
        End If
    End Sub
    Sub COPYSYM()
        CMBGRIDSYM.Items.Clear()

        Dim symSet As New HashSet(Of String)
        For Each row As DataGridViewRow In GRIDWARP.Rows
            If Not IsDBNull(row.Cells(WSYM.Name).Value) AndAlso Not String.IsNullOrWhiteSpace(row.Cells(WSYM.Name).Value.ToString) Then
                symSet.Add(row.Cells(WSYM.Name).Value.ToString)
            End If
        Next

        For Each symVal As String In symSet
            CMBGRIDSYM.Items.Add(symVal)
        Next


    End Sub
    Sub clearwarp()
        TXTWARPSRNO.Clear()
        TXTWARPSYMBOL.Clear()
        CMBWARPQUALITY.Text = ""
        TXTWARPDENIER.Clear()
        CMBWARPMILLNAME.Text = ""
        CMBWARPSHADE.Text = ""
        TXTWARPPE.Clear()
        TXTWARPBE.Clear()
        TXTWARPTE.Clear()
        TXTWARPWT.Clear()
        TXTWARPCONS.Clear()
        TXTWARPRATE.Clear()
        TXTWARPCOST.Clear()
    End Sub
    Sub fillwarppatterngrid()
        If GRIDWPDOUBLECLICK = False Then
            GRIDWARPPATTERN.Rows.Add(Val(TXTWARPGSRNO.Text.Trim), TXTGRIDPE.Text.Trim, CMBGRIDSYM.Text.Trim)
            getsrno(GRIDWARPPATTERN)
        ElseIf GRIDWPDOUBLECLICK = True Then
            GRIDWARPPATTERN.Item(WPSRNO.Index, TEMPWPROW).Value = Val(TXTWARPGSRNO.Text.Trim)
            GRIDWARPPATTERN.Item(WPENDS.Index, TEMPWPROW).Value = TXTGRIDPE.Text.Trim
            GRIDWARPPATTERN.Item(WPSYM.Index, TEMPWPROW).Value = CMBGRIDSYM.Text.Trim

            TEMPWPROW = GRIDWARPPATTERN.CurrentRow.Index
            TXTWARPGSRNO.Focus()
            GRIDWPDOUBLECLICK = False
        End If
        GRIDWARPPATTERN.ClearSelection()
        TXTGRIDPE.Clear()
        CMBGRIDSYM.Text = ""
        TOTALWARP()
        TXTGRIDPE.Focus()
        If GRIDWARPPATTERN.RowCount > 0 Then
            TXTWARPGSRNO.Text = Val(GRIDWARPPATTERN.Rows(GRIDWARPPATTERN.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDINVOICE.RowCount) + 1
        Else
            TXTWARPGSRNO.Text = 1
        End If
    End Sub
    Sub fillselvedgegrid()
        If GRIDSELDOUBLECLICK = False Then
            GRIDSELVEDGE.Rows.Add(Val(TXTSELSRNO.Text.Trim), TXTSELSYMBOL.Text.Trim, CMBSELYARNQUALITY.Text.Trim, TXTSELDEN.Text.Trim, CMBSELMILLNAME.Text.Trim, CMBSELSHADE.Text.Trim, Val(TXTSELPE.Text.Trim), Val(TXTSELBE.Text.Trim), Val(TXTSELTE.Text.Trim), Val(TXTSELWT.Text.Trim), Val(TXTSELCONS.Text.Trim), Val(TXTSELRATE.Text.Trim), Val(TXTSELCOST.Text.Trim))

            getsrno(GRIDSELVEDGE)
        ElseIf GRIDSELDOUBLECLICK = True Then
            GRIDSELVEDGE.Item(SSRNO.Index, TEMPSELROW).Value = Val(TXTSELSRNO.Text.Trim)
            GRIDSELVEDGE.Item(SSYM.Index, TEMPSELROW).Value = TXTSELSYMBOL.Text.Trim
            GRIDSELVEDGE.Item(SQUALITY.Index, TEMPSELROW).Value = CMBSELYARNQUALITY.Text.Trim
            GRIDSELVEDGE.Item(SDENIER.Index, TEMPSELROW).Value = TXTSELDEN.Text.Trim
            GRIDSELVEDGE.Item(SMILL.Index, TEMPSELROW).Value = CMBSELMILLNAME.Text.Trim
            GRIDSELVEDGE.Item(SSHADE.Index, TEMPSELROW).Value = CMBSELSHADE.Text.Trim
            GRIDSELVEDGE.Item(SPE.Index, TEMPSELROW).Value = Val(TXTSELPE.Text.Trim)
            GRIDSELVEDGE.Item(SBE.Index, TEMPSELROW).Value = Val(TXTSELBE.Text.Trim)
            GRIDSELVEDGE.Item(SENDS.Index, TEMPSELROW).Value = Val(TXTSELTE.Text.Trim)
            GRIDSELVEDGE.Item(SWT.Index, TEMPSELROW).Value = Val(TXTSELWT.Text.Trim)
            GRIDSELVEDGE.Item(SCONS.Index, TEMPSELROW).Value = Val(TXTSELCONS.Text.Trim)
            GRIDSELVEDGE.Item(SRATE.Index, TEMPSELROW).Value = Val(TXTSELRATE.Text.Trim)
            GRIDSELVEDGE.Item(SCOST.Index, TEMPSELROW).Value = Val(TXTSELCOST.Text.Trim)
            TEMPSELROW = GRIDSELVEDGE.CurrentRow.Index
            GRIDSELDOUBLECLICK = False
        End If
        GRIDSELVEDGE.ClearSelection()
        CLEARSELVEDGE()
        If GRIDSELVEDGE.RowCount > 0 Then
            TXTSELSRNO.Text = Val(GRIDSELVEDGE.Rows(GRIDSELVEDGE.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDINVOICE.RowCount) + 1
        Else
            TXTSELSRNO.Text = 1
        End If
        TXTSELSYMBOL.Focus()
    End Sub
    Sub CLEARSELVEDGE()
        TXTSELSRNO.Clear()
        TXTSELSYMBOL.Clear()
        CMBSELYARNQUALITY.Text = ""
        TXTSELDEN.Clear()
        CMBSELMILLNAME.Text = ""
        CMBSELSHADE.Text = ""
        TXTSELPE.Clear()
        TXTSELBE.Clear()
        TXTSELTE.Clear()
        TXTSELWT.Clear()
        TXTSELCONS.Clear()
        TXTSELRATE.Clear()
        TXTSELCOST.Clear()
    End Sub
    Sub FILLWEFTGRID()
        If GRIDWEFTDOUBLECLICK = False Then
            GRIDWEFT.Rows.Add(Val(TXTWEFTSRNO.Text.Trim), TXTWEFTSYMBOL.Text.Trim, CMBWEFTYARNQUALITY.Text.Trim, Val(TXTWEFTDEN.Text.Trim), CMBWEFTMILLNAME.Text.Trim, cmbweftshade.Text.Trim, Val(TXTWEFTPE.Text.Trim), Val(TXTWEFTBE.Text.Trim), Val(TXTWEFTTE.Text.Trim), Val(TXTWEFTWT.Text.Trim), Val(TXTWEFTCONS.Text.Trim), Val(TXTWEFTRATE.Text.Trim), Val(TXTWEFTCOST.Text.Trim))
            getsrno(GRIDWEFT)
        ElseIf GRIDWEFTDOUBLECLICK = True Then
            GRIDWEFT.Item(FSRNO.Index, TEMPWEFTROW).Value = Val(TXTWEFTSRNO.Text.Trim)
            GRIDWEFT.Item(FSYM.Index, TEMPWEFTROW).Value = TXTWEFTSYMBOL.Text.Trim
            GRIDWEFT.Item(FQUALITY.Index, TEMPWEFTROW).Value = CMBWEFTYARNQUALITY.Text.Trim
            GRIDWEFT.Item(FDENIER.Index, TEMPWEFTROW).Value = TXTWEFTDEN.Text.Trim
            GRIDWEFT.Item(FMILL.Index, TEMPWEFTROW).Value = CMBWEFTMILLNAME.Text.Trim
            GRIDWEFT.Item(FSHADE.Index, TEMPWEFTROW).Value = cmbweftshade.Text.Trim
            GRIDWEFT.Item(FPE.Index, TEMPWEFTROW).Value = Val(TXTWEFTPE.Text.Trim)
            GRIDWEFT.Item(FBE.Index, TEMPWEFTROW).Value = Val(TXTWEFTBE.Text.Trim)
            GRIDWEFT.Item(FENDS.Index, TEMPWEFTROW).Value = Val(TXTWEFTTE.Text.Trim)
            GRIDWEFT.Item(FWT.Index, TEMPWEFTROW).Value = Val(TXTWEFTWT.Text.Trim)
            GRIDWEFT.Item(FCONS.Index, TEMPWEFTROW).Value = Val(TXTWEFTCONS.Text.Trim)
            GRIDWEFT.Item(FRATE.Index, TEMPWEFTROW).Value = Val(TXTWEFTRATE.Text.Trim)
            GRIDWEFT.Item(FCOST.Index, TEMPWEFTROW).Value = Val(TXTWEFTCOST.Text.Trim)
            TEMPWEFTROW = GRIDWEFT.CurrentRow.Index
            TXTSELSRNO.Focus()
            GRIDWEFTDOUBLECLICK = False
        End If
        GRIDWEFT.ClearSelection()
        CLEARWEFT()
        COPYWEFTSYM()
        TXTWEFTSYMBOL.Focus()
        If GRIDWEFT.RowCount > 0 Then
            TXTWEFTSRNO.Text = Val(GRIDWEFT.Rows(GRIDWEFT.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDINVOICE.RowCount) + 1
        Else
            TXTWEFTSRNO.Text = 1
        End If
    End Sub
    Sub COPYWEFTSYM()
        CMBWEFTGRIDSYMBOL.Items.Clear()

        Dim symSet As New HashSet(Of String)
        For Each row As DataGridViewRow In GRIDWEFT.Rows
            If Not IsDBNull(row.Cells(FSYM.Name).Value) AndAlso Not String.IsNullOrWhiteSpace(row.Cells(FSYM.Name).Value.ToString) Then
                symSet.Add(row.Cells(FSYM.Name).Value.ToString)
            End If
        Next

        For Each symVal As String In symSet
            CMBWEFTGRIDSYMBOL.Items.Add(symVal)
        Next


    End Sub
    Sub CLEARWEFT()
        'TXTWEFTSRNO.Clear()
        TXTWEFTSYMBOL.Clear()
        CMBWEFTYARNQUALITY.Text = ""
        TXTWEFTDEN.Clear()
        CMBWEFTMILLNAME.Text = ""
        cmbweftshade.Text = ""
        TXTWEFTPE.Clear()
        TXTWEFTBE.Clear()
        TXTWEFTTE.Clear()
        TXTWEFTWT.Clear()
        TXTWEFTCONS.Clear()
        TXTWEFTRATE.Clear()
        TXTWEFTCOST.Clear()
    End Sub
    Sub FILLWEFTPATTERNGRID()
        If GRIDWEFTPDOUBLECLICK = False Then
            GRIDWEFTPATTERN.Rows.Add(Val(TXTWEFTGRIDSRNO.Text.Trim), TXTWEFTGRIDPE.Text.Trim, CMBWEFTGRIDSYMBOL.Text.Trim)
            getsrno(GRIDWEFTPATTERN)
        ElseIf GRIDWEFTPDOUBLECLICK = True Then
            GRIDWEFTPATTERN.Item(FPSRNO.Index, TEMPWEFTPROW).Value = Val(TXTWEFTGRIDSRNO.Text.Trim)
            GRIDWEFTPATTERN.Item(FPENDS.Index, TEMPWEFTPROW).Value = Val(TXTWEFTGRIDPE.Text.Trim)
            GRIDWEFTPATTERN.Item(FPSYM.Index, TEMPWEFTPROW).Value = Val(CMBWEFTGRIDSYMBOL.Text.Trim)
            TXTWEFTGRIDSRNO.Focus()
            GRIDWEFTPDOUBLECLICK = False
        End If
        GRIDWEFTPATTERN.ClearSelection()
        TXTWEFTGRIDPE.Clear()
        CMBWEFTGRIDSYMBOL.Text = ""
        TXTWEFTGRIDPE.Focus()
        If GRIDWEFTPATTERN.RowCount > 0 Then
            TXTWEFTGRIDSRNO.Text = Val(GRIDWEFTPATTERN.Rows(GRIDWEFTPATTERN.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDINVOICE.RowCount) + 1
        Else
            TXTWEFTGRIDSRNO.Text = 1
        End If
    End Sub

    Private Sub TXTWARPCOST_Validated(sender As Object, e As EventArgs) Handles TXTWARPCOST.Validated
        Try
            fillwarpgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGRIDSYMBOL_Validated(sender As Object, e As EventArgs)
        Try
            fillwarppatterngrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSELCOST_Validated(sender As Object, e As EventArgs) Handles TXTSELCOST.Validated
        Try
            fillselvedgegrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWEFTCOST_Validated(sender As Object, e As EventArgs) Handles TXTWEFTCOST.Validated
        Try
            FILLWEFTGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTGRIDSYMBOL_Validated(sender As Object, e As EventArgs)
        Try
            FILLWEFTPATTERNGRID()
        Catch ex As Exception
            Throw ex
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

    Private Sub CMBDESIGNNO_Enter(sender As Object, e As EventArgs) Handles CMBDESIGNNO.Enter
        Try
            If CMBDESIGNNO.Text.Trim = "" Then FILLDESIGN(CMBDESIGNNO, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNNO_Validating(sender As Object, e As CancelEventArgs) Handles CMBDESIGNNO.Validating
        Try
            If CMBDESIGNNO.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGNNO, e, Me, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSELSHADE_Enter(sender As Object, e As EventArgs) Handles CMBSELSHADE.Enter
        Try
            If CMBSELSHADE.Text.Trim = "" Then FILLCOLOR(CMBSELSHADE, CMBDESIGNNO.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPSHADE_Enter(sender As Object, e As EventArgs) Handles CMBWARPSHADE.Enter
        Try
            If CMBWARPSHADE.Text.Trim = "" Then FILLCOLOR(CMBWARPSHADE, CMBDESIGNNO.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbweftshade_Enter(sender As Object, e As EventArgs) Handles cmbweftshade.Enter
        Try
            If cmbweftshade.Text.Trim = "" Then FILLCOLOR(cmbweftshade, CMBDESIGNNO.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbweftshade_Validating(sender As Object, e As CancelEventArgs) Handles cmbweftshade.Validating
        Try
            If cmbweftshade.Text.Trim <> "" Then COLORVALIDATE(cmbweftshade, e, Me, CMBDESIGNNO.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBWARPSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBWARPSHADE.Validating
        Try
            If CMBWARPSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBWARPSHADE, e, Me, CMBDESIGNNO.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSELSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBSELSHADE.Validating
        Try
            If CMBSELSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSELSHADE, e, Me, CMBDESIGNNO.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSELMILLNAME_Enter(sender As Object, e As EventArgs) Handles CMBSELMILLNAME.Enter
        Try
            If CMBSELMILLNAME.Text.Trim = "" Then FILLMILL(CMBSELMILLNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPMILLNAME_Enter(sender As Object, e As EventArgs) Handles CMBWARPMILLNAME.Enter
        Try
            If CMBWARPMILLNAME.Text.Trim = "" Then FILLMILL(CMBWARPMILLNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTMILLNAME_Enter(sender As Object, e As EventArgs) Handles CMBWEFTMILLNAME.Enter
        Try
            If CMBWEFTMILLNAME.Text.Trim = "" Then FILLMILL(CMBWEFTMILLNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELMILLNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBSELMILLNAME.Validating
        Try
            If CMBSELMILLNAME.Text.Trim <> "" Then MILLVALIDATE(CMBSELMILLNAME, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        clear()
        EDIT = False
    End Sub

    Private Sub CMBWARPMILLNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBWARPMILLNAME.Validating
        Try
            If CMBWARPMILLNAME.Text.Trim <> "" Then MILLVALIDATE(CMBWARPMILLNAME, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTMILLNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBWEFTMILLNAME.Validating
        Try
            If CMBWEFTMILLNAME.Text.Trim <> "" Then MILLVALIDATE(CMBWEFTMILLNAME, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBWARPQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBWARPQUALITY.Enter
        Try
            If CMBWARPQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBWARPQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBWARPQUALITY.Validating
        Try
            If CMBWARPQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBWARPQUALITY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSELYARNQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBSELYARNQUALITY.Validating

        Try
            If CMBSELYARNQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBSELYARNQUALITY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub CMBWEFTYARNQUALITY_Validating(sender As Object, e As CancelEventArgs) Handles CMBWEFTYARNQUALITY.Validating
        Try
            If CMBWEFTYARNQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBWEFTYARNQUALITY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSELYARNQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBSELYARNQUALITY.Enter
        Try
            If CMBSELYARNQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBSELYARNQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMBWEFTYARNQUALITY_Enter(sender As Object, e As EventArgs) Handles CMBWEFTYARNQUALITY.Enter
        Try
            If CMBWEFTYARNQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBWEFTYARNQUALITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMBWEAVE_Validating(sender As Object, e As CancelEventArgs) Handles CMBWEAVE.Validating
        Try
            If CMBWEAVE.Text.Trim <> "" Then WEAVEVALIDATE(CMBWEAVE, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Sub CMBWEAVE_Enter(sender As Object, e As EventArgs) Handles CMBWEAVE.Enter

        Try
            If CMBWEAVE.Text.Trim = "" Then FILLWEAVE(CMBWEAVE, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMBLOOM_Enter(sender As Object, e As EventArgs) Handles CMBLOOM.Enter

        Try
            If CMBLOOM.Text.Trim = "" Then FILLLOOM(CMBLOOM, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMBLOOM_Validating(sender As Object, e As CancelEventArgs) Handles CMBLOOM.Validating
        Try
            If CMBLOOM.Text.Trim <> "" Then LOOMVALIDATE(CMBLOOM, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTreed_Validated(sender As Object, e As EventArgs) Handles TXTREED.Validated
        Try
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CALC()
        TXTMAINRS.Text = 0.00
        TXTDENTS.Text = 0.00
        TXTTOTALDENTSMAIN.Text = 0.00
        TXTTOTALSELVEDGEDENTS.Text = 0.00
        TXTTOTALDENTS.Text = 0.00
        TXTLEFTSELDENTS.Text = 0.00
        TXTRIGHTSELDENTS.Text = 0.00
        TXTLEFTSELTOTALENDS.Text = 0.00
        TXTRIGHTSELTOTALENDS.Text = 0.00
        TXTTOTALSELENDS.Text = 0.00


        If TXTLEFTSEL.Text <> "" And TXTREEDSPACE.Text <> "" Then TXTMAINRS.Text = Val(TXTREEDSPACE.Text) - Val(TXTLEFTSEL.Text) - Val(TXTRIGHTSEL.Text)
        If TXTREED.Text <> "" Then TXTDENTS.Text = Val(TXTREED.Text) / 2
        If TXTDENTS.Text <> "" And TXTMAINRS.Text <> "" Then TXTTOTALDENTSMAIN.Text = Val(TXTDENTS.Text) * Val(TXTMAINRS.Text)
        If TXTLEFTSEL.Text <> "" And TXTDENTS.Text <> "" Then TXTLEFTSELDENTS.Text = Val(TXTLEFTSEL.Text) * Val(TXTDENTS.Text)
        If TXTDENTS.Text <> "" And TXTRIGHTSEL.Text <> "" Then TXTRIGHTSELDENTS.Text = Val(TXTRIGHTSEL.Text) * Val(TXTDENTS.Text)
        If TXTRIGHTSELDENTS.Text <> "" And TXTLEFTSELDENTS.Text <> "" Then TXTTOTALSELVEDGEDENTS.Text = Val(TXTLEFTSELDENTS.Text) + Val(TXTRIGHTSELDENTS.Text)
        If TXTTOTALDENTSMAIN.Text <> "" And TXTTOTALSELVEDGEDENTS.Text <> "" Then TXTTOTALDENTS.Text = Val(TXTTOTALDENTSMAIN.Text) + Val(TXTTOTALSELVEDGEDENTS.Text)
        If TXTLEFTSELENDS.Text <> "" And TXTLEFTSELDENTS.Text <> "" Then TXTLEFTSELTOTALENDS.Text = Val(TXTLEFTSELENDS.Text) * Val(TXTLEFTSELDENTS.Text)
        If TXTRIGHTSELENDS.Text <> "" And TXTRIGHTSELDENTS.Text <> "" Then TXTRIGHTSELTOTALENDS.Text = Val(TXTRIGHTSELENDS.Text) * Val(TXTRIGHTSELDENTS.Text)
        If TXTLEFTSELTOTALENDS.Text <> "" And TXTRIGHTSELTOTALENDS.Text <> "" Then TXTTOTALSELENDS.Text = Val(TXTLEFTSELTOTALENDS.Text) + Val(TXTRIGHTSELTOTALENDS.Text)
    End Sub
    Sub TOTALWARP()
        Dim PE, BE, TE, WT, CONS, RATE, COST, GRIDPE As Double
        PE = 0.00
        BE = 0.00
        TE = 0.00
        WT = 0.00
        CONS = 0.00
        RATE = 0.00
        COST = 0.00
        GRIDPE = 0.00
        For Each row As DataGridViewRow In GRIDWARP.Rows
            If row.Cells(WPE.Index).Value IsNot DBNull.Value Then
                PE = PE + Val(row.Cells(WPE.Index).Value)
            End If
            If row.Cells(WBE.Index).Value IsNot DBNull.Value Then
                BE = BE + Val(row.Cells(WBE.Index).Value)
            End If
            If row.Cells(WENDS.Index).Value IsNot DBNull.Value Then
                TE = TE + Val(row.Cells(WENDS.Index).Value)
            End If
            If row.Cells(WWT.Index).Value IsNot DBNull.Value Then
                WT = WT + Val(row.Cells(WWT.Index).Value)
            End If
            If row.Cells(WCONS.Index).Value IsNot DBNull.Value Then
                CONS = CONS + Val(row.Cells(WCONS.Index).Value)
            End If
            If row.Cells(WRATE.Index).Value IsNot DBNull.Value Then
                RATE = RATE + Val(row.Cells(WRATE.Index).Value)
            End If
            If row.Cells(WCOST.Index).Value IsNot DBNull.Value Then
                COST = COST + Val(row.Cells(WCOST.Index).Value)
            End If
        Next
        For Each row As DataGridViewRow In GRIDWARPPATTERN.Rows
            If row.Cells(WPENDS.Index).Value IsNot DBNull.Value Then
                GRIDPE = GRIDPE + Val(row.Cells(WPENDS.Index).Value)
            End If
        Next
        TXTTOTALWARPPE.Text = Format(PE, "0.00")
        TXTTOTALWARPBE.Text = Format(BE, "0.00")
        TXTTOTALWARPTE.Text = Format(TE, "0.00")
        TXTTOTALWARPWT.Text = Format(WT, "0.00")
        TXTTOTALWARPCONS.Text = Format(CONS, "0.00")
        TXTTOTALWARPRATE.Text = Format(RATE, "0.00")
        TXTTOTALWARPCOST.Text = Format(COST, "0.00")
        TXTTOTALWARPGRIDPE.Text = Format(GRIDPE, "0.00")


    End Sub

    Private Sub GRIDWARP_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWARP.CellDoubleClick
        Try
            EDITWARPROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub EDITWARPROW()
        If GRIDWARP.CurrentRow IsNot Nothing Then
            If GRIDWARP.CurrentRow.Index >= 0 Then
                TEMPROW = GRIDWARP.CurrentRow.Index
                TXTWARPSRNO.Text = GRIDWARP.Item(WSRNO.Index, TEMPROW).Value
                TXTWARPSYMBOL.Text = GRIDWARP.Item(WSYM.Index, TEMPROW).Value
                CMBWARPQUALITY.Text = GRIDWARP.Item(WQUALITY.Index, TEMPROW).Value
                TXTWARPDENIER.Text = GRIDWARP.Item(WDENIER.Index, TEMPROW).Value
                CMBWARPMILLNAME.Text = GRIDWARP.Item(WMILL.Index, TEMPROW).Value
                CMBWARPSHADE.Text = GRIDWARP.Item(WSHADE.Index, TEMPROW).Value
                TXTWARPPE.Text = GRIDWARP.Item(WPE.Index, TEMPROW).Value
                TXTWARPBE.Text = GRIDWARP.Item(WBE.Index, TEMPROW).Value
                TXTWARPTE.Text = GRIDWARP.Item(WENDS.Index, TEMPROW).Value
                TXTWARPWT.Text = GRIDWARP.Item(WWT.Index, TEMPROW).Value
                TXTWARPCONS.Text = GRIDWARP.Item(WCONS.Index, TEMPROW).Value
                TXTWARPRATE.Text = GRIDWARP.Item(WRATE.Index, TEMPROW).Value
                TXTWARPCOST.Text = GRIDWARP.Item(WCOST.Index, TEMPROW).Value
                GRIDDOUBLECLICK = True
                TXTWARPSYMBOL.Focus()
            End If
        End If
    End Sub
    Sub EDITWARPPATTERNROW()
        If GRIDWARPPATTERN.CurrentRow IsNot Nothing Then
            If GRIDWARPPATTERN.CurrentRow.Index >= 0 Then
                TEMPWPROW = GRIDWARPPATTERN.CurrentRow.Index
                TXTWARPGSRNO.Text = GRIDWARPPATTERN.Item(WPSRNO.Index, TEMPWPROW).Value
                TXTGRIDPE.Text = GRIDWARPPATTERN.Item(WPENDS.Index, TEMPWPROW).Value
                CMBGRIDSYM.Text = GRIDWARPPATTERN.Item(WPSYM.Index, TEMPWPROW).Value
                GRIDWPDOUBLECLICK = True
                TXTGRIDPE.Focus()
            End If
        End If
    End Sub
    Sub EDITSELVEDGEROW()
        If GRIDSELVEDGE.CurrentRow IsNot Nothing Then
            If GRIDSELVEDGE.CurrentRow.Index >= 0 Then
                TEMPSELROW = GRIDSELVEDGE.CurrentRow.Index
                TXTSELSRNO.Text = GRIDSELVEDGE.Item(SSRNO.Index, TEMPSELROW).Value
                TXTSELSYMBOL.Text = GRIDSELVEDGE.Item(SSYM.Index, TEMPSELROW).Value
                CMBSELYARNQUALITY.Text = GRIDSELVEDGE.Item(SQUALITY.Index, TEMPSELROW).Value
                TXTSELDEN.Text = GRIDSELVEDGE.Item(SDENIER.Index, TEMPSELROW).Value
                CMBSELMILLNAME.Text = GRIDSELVEDGE.Item(SMILL.Index, TEMPSELROW).Value
                CMBSELSHADE.Text = GRIDSELVEDGE.Item(SSHADE.Index, TEMPSELROW).Value
                TXTSELPE.Text = GRIDSELVEDGE.Item(SPE.Index, TEMPSELROW).Value
                TXTSELBE.Text = GRIDSELVEDGE.Item(SBE.Index, TEMPSELROW).Value
                TXTSELTE.Text = GRIDSELVEDGE.Item(SENDS.Index, TEMPSELROW).Value
                TXTSELWT.Text = GRIDSELVEDGE.Item(SWT.Index, TEMPSELROW).Value
                TXTSELCONS.Text = GRIDSELVEDGE.Item(SCONS.Index, TEMPSELROW).Value
                TXTSELRATE.Text = GRIDSELVEDGE.Item(SRATE.Index, TEMPSELROW).Value
                TXTSELCOST.Text = GRIDSELVEDGE.Item(SCOST.Index, TEMPSELROW).Value
                GRIDSELDOUBLECLICK = True
                TXTSELSYMBOL.Focus()
            End If
        End If
    End Sub
    Sub EDITWEFTROW()
        If GRIDWEFT.CurrentRow IsNot Nothing Then
            If GRIDWEFT.CurrentRow.Index >= 0 Then
                TEMPWEFTROW = GRIDWEFT.CurrentRow.Index
                TXTWEFTSRNO.Text = GRIDWEFT.Item(FSRNO.Index, TEMPWEFTROW).Value
                TXTWEFTSYMBOL.Text = GRIDWEFT.Item(FSYM.Index, TEMPWEFTROW).Value
                CMBWEFTYARNQUALITY.Text = GRIDWEFT.Item(FQUALITY.Index, TEMPWEFTROW).Value
                TXTWEFTDEN.Text = GRIDWEFT.Item(FDENIER.Index, TEMPWEFTROW).Value
                CMBWEFTMILLNAME.Text = GRIDWEFT.Item(FMILL.Index, TEMPWEFTROW).Value
                cmbweftshade.Text = GRIDWEFT.Item(FSHADE.Index, TEMPWEFTROW).Value
                TXTWEFTPE.Text = GRIDWEFT.Item(FPE.Index, TEMPWEFTROW).Value
                TXTWEFTBE.Text = GRIDWEFT.Item(FBE.Index, TEMPWEFTROW).Value
                TXTWEFTTE.Text = GRIDWEFT.Item(FENDS.Index, TEMPWEFTROW).Value
                TXTWEFTWT.Text = GRIDWEFT.Item(FWT.Index, TEMPWEFTROW).Value
                TXTWEFTCONS.Text = GRIDWEFT.Item(FCONS.Index, TEMPWEFTROW).Value
                TXTWEFTRATE.Text = GRIDWEFT.Item(FRATE.Index, TEMPWEFTROW).Value
                TXTWEFTCOST.Text = GRIDWEFT.Item(FCOST.Index, TEMPWEFTROW).Value
                GRIDWEFTDOUBLECLICK = True
                TXTWEFTSYMBOL.Focus()
            End If
        End If
    End Sub
    Sub EDITWEFTPATTERNROW()
        If GRIDWEFTPATTERN.CurrentRow IsNot Nothing Then
            If GRIDWEFTPATTERN.CurrentRow.Index >= 0 Then
                TEMPWEFTPROW = GRIDWEFTPATTERN.CurrentRow.Index
                TXTWEFTGRIDSRNO.Text = GRIDWEFTPATTERN.Item(FPSRNO.Index, TEMPWEFTPROW).Value
                TXTWEFTGRIDPE.Text = GRIDWEFTPATTERN.Item(FPENDS.Index, TEMPWEFTPROW).Value
                CMBWEFTGRIDSYMBOL.Text = GRIDWEFTPATTERN.Item(FPSYM.Index, TEMPWEFTPROW).Value
                GRIDWEFTPDOUBLECLICK = True
                TXTWEFTGRIDPE.Focus()
            End If
        End If
    End Sub

    Private Sub GRIDWARPPATTERN_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWARPPATTERN.CellDoubleClick
        Try
            EDITWARPPATTERNROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWARPSYMBOL_Validated(sender As Object, e As EventArgs) Handles TXTWARPSYMBOL.Validated
        Try
            If TXTWARPSYMBOL.Text = "" Then TXTGRIDPE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGRIDPE_Validated(sender As Object, e As EventArgs) Handles TXTGRIDPE.Validated
        Try
            If TXTGRIDPE.Text = "" Then cmdok.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFT_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWEFT.CellDoubleClick
        EDITWEFTROW()
    End Sub

    Private Sub GRIDWEFTPATTERN_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWEFTPATTERN.CellDoubleClick
        EDITWARPPATTERNROW()
    End Sub
    Private Sub GRIDSELVEDGE_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSELVEDGE.CellDoubleClick
        EDITSELVEDGEROW()
    End Sub

    Private Sub TXTREEDSPACE_Validated(sender As Object, e As EventArgs) Handles TXTREEDSPACE.Validated, TXTRIGHTSEL.Validated, TXTLEFTSEL.Validated, TXTLEFTSELENDS.Validated, TXTRIGHTSELENDS.Validated
        CALC()
    End Sub

    Private Sub CMBGRIDSYM_Validated(sender As Object, e As EventArgs) Handles CMBGRIDSYM.Validated
        fillwarppatterngrid()
        GETWARPPE()
    End Sub
    Sub GETWARPPE()
        ' --- Step 1: Create a dictionary to sum P.E. per Sym from warppattern grid ---
        Dim peSumBySym As New Dictionary(Of String, Double)

        For Each row As DataGridViewRow In GRIDWARPPATTERN.Rows
            If row.IsNewRow Then Continue For
            Dim symVal As String = row.Cells(WPSYM.Index).Value?.ToString()
            Dim peVal As Double = 0
            Double.TryParse(row.Cells(WPENDS.Index).Value?.ToString(), peVal) ' Replace WPE with your PE column Name/variable
            If Not String.IsNullOrWhiteSpace(symVal) Then
                If Not peSumBySym.ContainsKey(symVal) Then
                    peSumBySym(symVal) = 0
                End If
                peSumBySym(symVal) += peVal
            End If
        Next

        ' --- Step 2: Write the sum into the matching Sym row's P.E. cell in the GRIDWARP ---
        For Each row As DataGridViewRow In GRIDWARP.Rows
            If row.IsNewRow Then Continue For
            Dim symVal As String = row.Cells(WSYM.Index).Value?.ToString()
            If Not String.IsNullOrWhiteSpace(symVal) AndAlso peSumBySym.ContainsKey(symVal) Then
                row.Cells(WPE.Index).Value = peSumBySym(symVal) ' Replace WPE with your PE column Name/variable
            End If
        Next

    End Sub
    Sub GETWEFTPE()
        ' --- Step 1: Create a dictionary to sum P.E. per Sym from warppattern grid ---
        Dim peSumBySym As New Dictionary(Of String, Double)

        For Each row As DataGridViewRow In GRIDWEFTPATTERN.Rows
            If row.IsNewRow Then Continue For
            Dim symVal As String = row.Cells(FPSYM.Index).Value?.ToString()
            Dim peVal As Double = 0
            Double.TryParse(row.Cells(FPENDS.Index).Value?.ToString(), peVal) ' Replace WPE with your PE column Name/variable
            If Not String.IsNullOrWhiteSpace(symVal) Then
                If Not peSumBySym.ContainsKey(symVal) Then
                    peSumBySym(symVal) = 0
                End If
                peSumBySym(symVal) += peVal
            End If
        Next

        ' --- Step 2: Write the sum into the matching Sym row's P.E. cell in the GRIDWARP ---
        For Each row As DataGridViewRow In GRIDWEFT.Rows
            If row.IsNewRow Then Continue For
            Dim symVal As String = row.Cells(FSYM.Index).Value?.ToString()
            If Not String.IsNullOrWhiteSpace(symVal) AndAlso peSumBySym.ContainsKey(symVal) Then
                row.Cells(FPE.Index).Value = peSumBySym(symVal) ' Replace WPE with your PE column Name/variable
            End If
        Next

    End Sub

    Private Sub TXTWARPSYMBOL_Validating(sender As Object, e As CancelEventArgs) Handles TXTWARPSYMBOL.Validating
        Try
            If TXTWARPSYMBOL.Text <> "" And GRIDWARP.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDWARP.Rows
                    If TXTWARPSYMBOL.Text = row.Cells(WSYM.Index).Value Then
                        MsgBox("Symbol Already Exists", MsgBoxStyle.Critical)
                        e.Cancel = True
                        TXTWARPSYMBOL.Focus()
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWEFTSYMBOL_Validating(sender As Object, e As CancelEventArgs) Handles TXTWEFTSYMBOL.Validating
        Try
            If TXTWEFTSYMBOL.Text <> "" And GRIDWEFT.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDWEFT.Rows
                    If TXTWEFTSYMBOL.Text = row.Cells(FSYM.Index).Value Then
                        MsgBox("Symbol Already Exists", MsgBoxStyle.Critical)
                        e.Cancel = True
                        TXTWEFTSYMBOL.Focus()
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWEFTSYMBOL_Validated(sender As Object, e As EventArgs) Handles TXTWEFTSYMBOL.Validated
        Try
            If TXTWEFTSYMBOL.Text = "" Then TXTWEFTGRIDPE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWEFTGRIDPE_Validated(sender As Object, e As EventArgs) Handles TXTWEFTGRIDPE.Validated
        Try
            If TXTWEFTGRIDPE.Text = "" Then cmdok.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class