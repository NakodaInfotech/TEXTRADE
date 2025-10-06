
Imports System.ComponentModel
Imports System.IO
Imports System.Text.RegularExpressions
Imports BL
Imports DevExpress.Charts.Native
Imports DevExpress.CodeParser
Imports DevExpress.DashboardCommon.Native
Imports DevExpress.UIAutomation
Imports DevExpress.XtraGauges.Core.Model
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPivotGrid.Design
Imports DevExpress.XtraRichEdit.Commands
Imports DevExpress.XtraRichEdit.Model
Public Class DesignCardMaster
    Public EDIT As Boolean              'Used for edit
    Public tempdesignno As String           'Used for edit name
    Public tempid As Integer            'Used for edit id
    Dim GRIDDOUBLECLICK, GRIDWPDOUBLECLICK, GRIDSELDOUBLECLICK, GRIDSELPDOUBLECLICK, GRIDWEFTDOUBLECLICK, GRIDWEFTPDOUBLECLICK, GRIDDRAWDOUBLECLICK, GRIDSELDESCDOUBLECLICK, GRIDWARPDESCDOUBLECLICK, GRIDWEFTDESCDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPPROW, TEMPWPROW, TEMPSELROW, TEMPSELPROW, TEMPWEFTROW, TEMPWEFTPROW, TEMPDRAWROW, TEMPSELDESCROW, TEMPWARPDESCROW, TEMPWEFTDESCROW As Integer
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPUPLOADROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public FRMSTRING As String
    Dim TEMPMSG As Integer
    Dim DT_SELDETAILS As New DataTable
    Dim DT_WARPDETAILS As New DataTable
    Dim DT_WEFTDETAILS As New DataTable




    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Ep.Clear()
            'If Not errorvalid() Then
            '    Exit Sub
            'End If
            Dim IntResult As Integer

            Dim alParaval As New ArrayList
            alParaval.Add(Val(txtcardno.Text.Trim))
            alParaval.Add(Format(Convert.ToDateTime(DTDATE.Text).Date, "MM/dd/yyyy"))
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
            alParaval.Add(Val(TXTTOTALSELGPE.Text.Trim))
            'Weft Total
            alParaval.Add(Val(TXTTOTALWEFTPE.Text.Trim))        ' P.E. (Weft)
            alParaval.Add(Val(TXTTOTALWEFTBE.Text.Trim))        ' B.E. (Weft)
            alParaval.Add(Val(TXTTOTALWEFTTE.Text.Trim))        ' T.E. (Weft)
            alParaval.Add(Val(TXTTOTALWEFTWT.Text.Trim))        ' Wt (Weft Weight)
            alParaval.Add(Val(TXTTOTALWEFTCONS.Text.Trim))      ' Cons (Weft Consumption)
            alParaval.Add(Val(TXTTOTALWEFTRATE.Text.Trim))      ' Rate (Weft Rate)
            alParaval.Add(Val(TXTTOTALWEFTCOST.Text.Trim))      ' Cost (Weft Cost)
            alParaval.Add(Val(TXTTOTALWEFTGRIDPE.Text.Trim))        ' P.E. (Repeated for field order continuity)
            'DRAWING TOTAL  
            alParaval.Add(Val(TXTTOTALDRAWENDS.Text.Trim))
            alParaval.Add(Val(TXTTOTALDRAWDENTS.Text.Trim))

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

            For Each row As Windows.Forms.DataGridViewRow In GRIDWARPPATTERN.Rows
                If row.Cells(2).Value <> "" Then
                    If WARPGRIDSRNO = "" Then
                        WARPGRIDSRNO = Val(row.Cells(WPSRNO.Index).Value)
                        WARPGRIDPE = row.Cells(WPENDS.Index).Value.ToString
                        WARPGRIDSYM = row.Cells(WPSYM.Index).Value.ToString
                    Else
                        WARPGRIDSRNO = WARPGRIDSRNO & "|" & Val(row.Cells(WPSRNO.Index).Value)
                        WARPGRIDPE = WARPGRIDPE & "|" & row.Cells(WPENDS.Index).Value.ToString
                        WARPGRIDSYM = WARPGRIDSYM & "|" & row.Cells(WPSYM.Index).Value.ToString
                    End If
                End If
            Next


            alParaval.Add(WARPGRIDSRNO)
            alParaval.Add(WARPGRIDPE)
            alParaval.Add(WARPGRIDSYM)

            Dim WDSRNO As String = ""
            Dim WDMTRS As String = ""
            Dim WDMAINSRNO As String = ""

            For i As Integer = 0 To DT_WARPDETAILS.Rows.Count - 1
                If DT_WARPDETAILS.Rows(i).Item(0) <> Nothing Then
                    If WDSRNO = "" Then
                        WDSRNO = Val(DT_WARPDETAILS.Rows(i).Item("WDSRNO"))
                        WDMTRS = DT_WARPDETAILS.Rows(i).Item("WDSHADE")
                        WDMAINSRNO = Val(DT_WARPDETAILS.Rows(i).Item("WDMAINSRNO"))
                    Else
                        WDSRNO = WDSRNO & "|" & Val(DT_WARPDETAILS.Rows(i).Item("WDSRNO"))
                        WDMTRS = WDMTRS & "|" & DT_WARPDETAILS.Rows(i).Item("WDSHADE")
                        WDMAINSRNO = WDMAINSRNO & "|" & Val(DT_WARPDETAILS.Rows(i).Item("WDMAINSRNO"))
                    End If
                End If
            Next


            alParaval.Add(WDSRNO)
            alParaval.Add(WDMTRS)
            alParaval.Add(WDMAINSRNO)
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
            'GRID SELVEDGE PATTERN

            Dim ALOTRSrNo As String = ""
            Dim ALOTRPE As String = ""
            Dim ALOTRSym As String = ""
            For Each row As Windows.Forms.DataGridViewRow In GRIDSELVEDGEPATTERN.Rows
                If row.Cells(SPSYM.Index).Value IsNot Nothing Then
                    If ALOTRSrNo = "" Then
                        ALOTRSrNo = Val(row.Cells(SPSRNO.Index).Value)
                        ALOTRPE = row.Cells(SPENDS.Index).Value.ToString
                        ALOTRSym = row.Cells(SPSYM.Index).Value.ToString
                    Else
                        ALOTRSrNo = ALOTRSrNo & "|" & Val(row.Cells(SPSRNO.Index).Value)
                        ALOTRPE = ALOTRPE & "|" & row.Cells(SPENDS.Index).Value.ToString
                        ALOTRSym = ALOTRSym & "|" & row.Cells(SPSYM.Index).Value.ToString
                    End If
                End If
            Next
            alParaval.Add(ALOTRSrNo)
            alParaval.Add(ALOTRPE)
            alParaval.Add(ALOTRSym)



            Dim SDSRNO As String = ""
            Dim SDMTRS As String = ""
            Dim SDMAINSRNO As String = ""

            For i As Integer = 0 To DT_SELDETAILS.Rows.Count - 1
                If DT_SELDETAILS.Rows(i).Item(0) <> Nothing Then
                    If SDSRNO = "" Then
                        SDSRNO = Val(DT_SELDETAILS.Rows(i).Item("SDSRNO"))
                        SDMTRS = DT_SELDETAILS.Rows(i).Item("SDSHADE")
                        SDMAINSRNO = Val(DT_SELDETAILS.Rows(i).Item("SDMAINSRNO"))
                    Else
                        SDSRNO = SDSRNO & "|" & Val(DT_SELDETAILS.Rows(i).Item("SDSRNO"))
                        SDMTRS = SDMTRS & "|" & DT_SELDETAILS.Rows(i).Item("SDSHADE")
                        SDMAINSRNO = SDMAINSRNO & "|" & Val(DT_SELDETAILS.Rows(i).Item("SDMAINSRNO"))
                    End If
                End If
            Next


            alParaval.Add(SDSRNO)
            alParaval.Add(SDMTRS)
            alParaval.Add(SDMAINSRNO)
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
                If row.Cells(FPSRNO.Index).Value IsNot Nothing AndAlso row.Cells(FPSYM.Index).Value IsNot Nothing Then
                    If WEFTTRSrNo = "" Then
                        WEFTTRSrNo = Val(row.Cells(FPSRNO.Index).Value)
                        WEFTTRPE = row.Cells(FPENDS.Index).Value.ToString
                        WEFTTRSym = row.Cells(FPSYM.Index).Value.ToString
                    Else
                        WEFTTRSrNo = WEFTTRSrNo & "|" & Val(row.Cells(FPSRNO.Index).Value)
                        WEFTTRPE = WEFTTRPE & "|" & row.Cells(FPENDS.Index).Value.ToString
                        WEFTTRSym = WEFTTRSym & "|" & row.Cells(FPSYM.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(WEFTTRSrNo)
            alParaval.Add(WEFTTRPE)
            alParaval.Add(WEFTTRSym)


            Dim FDSRNO As String = ""
            Dim FDMTRS As String = ""
            Dim FDMAINSRNO As String = ""

            For i As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                If DT_WEFTDETAILS.Rows(i).Item(0) <> Nothing Then
                    If FDSRNO = "" Then
                        FDSRNO = Val(DT_WEFTDETAILS.Rows(i).Item("FDSRNO"))
                        FDMTRS = DT_WEFTDETAILS.Rows(i).Item("FDSHADE")
                        FDMAINSRNO = Val(DT_WEFTDETAILS.Rows(i).Item("FDMAINSRNO"))
                    Else
                        FDSRNO = FDSRNO & "|" & Val(DT_WEFTDETAILS.Rows(i).Item("FDSRNO"))
                        FDMTRS = FDMTRS & "|" & DT_WEFTDETAILS.Rows(i).Item("FDSHADE")
                        FDMAINSRNO = FDMAINSRNO & "|" & Val(DT_WEFTDETAILS.Rows(i).Item("FDMAINSRNO"))
                    End If
                End If
            Next


            alParaval.Add(FDSRNO)
            alParaval.Add(FDMTRS)
            alParaval.Add(FDMAINSRNO)

            '*************************************************************************
            'GRID DRAWING
            Dim DRAWSrNo As String = ""
            Dim DRAWEnds As String = ""
            Dim DRAWREPEATMARK As String = ""
            Dim DRAWREPEATS As String = ""
            Dim DRAWREPEATMARK1 As String = ""
            Dim DRAWREPEATS1 As String = ""
            Dim DRAWREPEATMARK2 As String = ""
            Dim DRAWREPEATS2 As String = ""
            For Each row As Windows.Forms.DataGridViewRow In GRIDDRAWING.Rows
                If row.Cells(DSRNO.Index).Value IsNot Nothing AndAlso row.Cells(DENDS.Index).Value IsNot Nothing Then
                    If DRAWSrNo = "" Then
                        DRAWSrNo = Val(row.Cells(DSRNO.Index).Value)
                        DRAWEnds = row.Cells(DENDS.Index).Value.ToString()
                        DRAWREPEATMARK = row.Cells(DREPEATMARK.Index).Value
                        DRAWREPEATS = Val(row.Cells(DREPEAT.Index).Value)
                        DRAWREPEATMARK1 = row.Cells(DREPEATMARK1.Index).Value
                        DRAWREPEATS1 = Val(row.Cells(DREPEATS1.Index).Value)
                        DRAWREPEATMARK2 = row.Cells(DREPEATMARK2.Index).Value
                        DRAWREPEATS2 = Val(row.Cells(DREPEATS2.Index).Value)
                    Else
                        DRAWSrNo = DRAWSrNo & "|" & Val(row.Cells(DSRNO.Index).Value)
                        DRAWEnds = DRAWEnds & "|" & row.Cells(DENDS.Index).Value.ToString()
                        DRAWREPEATMARK = DRAWREPEATMARK & "|" & row.Cells(DREPEATMARK.Index).Value
                        DRAWREPEATS = DRAWREPEATS & "|" & Val(row.Cells(DREPEAT.Index).Value)
                        DRAWREPEATMARK1 = DRAWREPEATMARK1 & "|" & row.Cells(DREPEATMARK1.Index).Value
                        DRAWREPEATS1 = DRAWREPEATS1 & "|" & Val(row.Cells(DREPEATS1.Index).Value)
                        DRAWREPEATMARK2 = DRAWREPEATMARK2 & "|" & row.Cells(DREPEATMARK2.Index).Value
                        DRAWREPEATS2 = DRAWREPEATS2 & "|" & Val(row.Cells(DREPEATS2.Index).Value)
                    End If
                End If
            Next
            alParaval.Add(DRAWSrNo)
            alParaval.Add(DRAWEnds)
            alParaval.Add(DRAWREPEATMARK)
            alParaval.Add(DRAWREPEATS)
            alParaval.Add(DRAWREPEATMARK1)
            alParaval.Add(DRAWREPEATS1)
            alParaval.Add(DRAWREPEATMARK2)
            alParaval.Add(DRAWREPEATS2)

            '*************************************************************************



            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)
            alParaval.Add(TXTFINISHWT.Text.Trim)




            Dim objDESIGN As New ClsDesignCardMaster
            objDESIGN.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objDESIGN.SAVE()
                txtcardno.Text = IntResult.ToString()
                MsgBox("Details Added")
                tempdesignno = txtcardno.Text.Trim

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempdesignno)
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
        DTDATE.Text = Now.Date
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
        TXTTOTALSELGPE.Clear()
        'Weft Total
        TXTTOTALWEFTPE.Clear()       ' P.E. (Weft)
        TXTTOTALWEFTBE.Clear()       ' B.E. (Weft)
        TXTTOTALWEFTTE.Clear()       ' T.E. (Weft)
        TXTTOTALWEFTWT.Clear()       ' Wt (Weft Weight)
        TXTTOTALWEFTCONS.Clear()     ' Cons (Weft Consumption)
        TXTTOTALWEFTRATE.Clear()     ' Rate (Weft Rate)
        TXTTOTALWEFTCOST.Clear()     ' Cost (Weft Cost)
        TXTTOTALWEFTGRIDPE.Clear()       ' P.E. (Repeated for field order continuity)
        'drawing total
        TXTTOTALDRAWDENTS.Clear()
        TXTTOTALDRAWENDS.Clear()
        TXTFINISHWT.Clear()
        'WARPMATCHING TEXTBOXES
        TXTGRIDPE.Clear()
        CMBGRIDSYM.Text = ""
        TXTWARPSRNO.Text = 1
        CMBGRIDSYM.Text = ""
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
        'SELVMATCHING TEXTBOXES
        TXTSELSRNO.Text = 1
        TXTSELSYMBOL.Text = ""
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
        TXTSELGSRNO.Clear()
        TXTSELGPE.Clear()
        'WEFTMATCHING TEXTBOXES
        TXTWEFTSRNO.Text = 1
        CMBWEFTGRIDSYMBOL.Text = ""
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
        txttotaldentsrepeat.Clear()
        TXTTOTALENDS.Clear()
        TXTENDPERINCH.Clear()
        TXTTOTALMAINENDS.Clear()
        txtxvalue.Clear()
        'DRAWING TEXTBOXES
        TXTDRAWSRNO.Clear()
        TXTDRAWENDS.Clear()
        'GRID WARP
        GRIDWARP.RowCount = 0
        'GRID WARP PATTERN
        GRIDWARPPATTERN.RowCount = 1
        'GRID SLEVAGE
        GRIDSELVEDGE.RowCount = 0

        GRIDSELVEDGEPATTERN.RowCount = 1
        'GRID WEFT
        GRIDWEFT.RowCount = 0
        'GRID WEFT PATTERN
        GRIDWEFTPATTERN.RowCount = 1
        'GRID DRAWING
        GRIDDRAWING.RowCount = 1
        'DT TABLE FOR SELVEDGE 
        DT_SELDETAILS.Reset()
        DT_SELDETAILS.Columns.Add("SDSRNO")
        DT_SELDETAILS.Columns.Add("SDSHADE")
        DT_SELDETAILS.Columns.Add("SDMAINSRNO")
        'DT TABLE FOR WARP
        DT_WARPDETAILS.Reset()
        DT_WARPDETAILS.Columns.Add("WDSRNO")
        DT_WARPDETAILS.Columns.Add("WDSHADE")
        DT_WARPDETAILS.Columns.Add("WDMAINSRNO")
        'DT TABLE FOR WEFT
        DT_WEFTDETAILS.Reset()
        DT_WEFTDETAILS.Columns.Add("FDSRNO")
        DT_WEFTDETAILS.Columns.Add("FDSHADE")
        DT_WEFTDETAILS.Columns.Add("FDMAINSRNO")

        Ep.Clear()
        GBSELVIEW.Visible = False

    End Sub
    Private Function errorvalid() As Boolean

        Dim bln As Boolean = True

        If CMBDESIGNNO.Text.Trim.Length = 0 Then
            Ep.SetError(CMBDESIGNNO, "Fill Design No")
            bln = False
        End If
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
                Dim OBJCMN As New ClsCommon
                Dim objclsGRN As New ClsDesignCardMaster()
                Dim dttable As New DataTable

                dttable = objclsGRN.SelectDesignCard(tempdesignno, YearId)

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        txtcardno.Text = tempdesignno
                        txtcardno.ReadOnly = True

                        DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBITEMNAME.Text = Convert.ToString(dr("ITEMNAME").ToString)
                        CMBDESIGNNO.Text = Convert.ToString(dr("DESIGNNO").ToString)
                        '  CMBWEAVER.Text = Convert.ToString(dr("WEAVER").ToString)
                        TXTREED.Text = dr("REED").ToString
                        TXTREEDSPACE.Text = dr("REEDSPACE").ToString
                        TXTPICKS.Text = dr("PICKS").ToString
                        TXTMAINRS.Text = dr("MAINRS").ToString
                        TXTTHREADPERDENT.Text = dr("THREADPERDENT").ToString
                        TXTFEPI.Text = dr("FEPI").ToString
                        TXTFWIDTH.Text = dr("FWIDTH").ToString
                        TXTFPPI.Text = dr("FPPI").ToString
                        TXTFWT.Text = dr("FWT").ToString
                        TXTDENTS.Text = dr("DENTS").ToString
                        TXTTOTALDENTSMAIN.Text = Val(dr("TOTALDENTSMAIN"))
                        TXTTOTALSELVEDGEDENTS.Text = Val(dr("TOTALSELVEDGEDENTS"))
                        TXTTOTALDENTS.Text = Val(dr("TOTALDENTS"))

                        TXTWARPTL.Text = dr("WARPTTL").ToString
                        TXTWEFTTL.Text = dr("WEFTTTL").ToString
                        TXTGSM.Text = dr("GSM").ToString
                        CMBWEAVE.Text = Convert.ToString(dr("WEAVE").ToString)
                        CMBSHAFTS.Text = Convert.ToString(dr("SHAFTS").ToString)
                        TXTTOTALWT.Text = Val(dr("TOTALWT"))
                        ' Selvedge fields
                        TXTLEFTSEL.Text = dr("LEFTSELVEDGE").ToString
                        TXTRIGHTSEL.Text = dr("RIGHTSELVEDGE").ToString
                        TXTLEFTSELENDS.Text = dr("LEFTSELVEDGEEND").ToString
                        TXTRIGHTSELENDS.Text = dr("RIGHTSELVEDGEEND").ToString
                        TXTLEFTSELDENTS.Text = dr("LEFTSELVEDGEDENTS").ToString
                        TXTRIGHTSELDENTS.Text = dr("RIGHTSELVEDGEDENTS").ToString
                        TXTLEFTSELTOTALENDS.Text = dr("LEFTSELVEDGETOTALEND").ToString
                        TXTRIGHTSELTOTALENDS.Text = dr("RIGHTSELVEDGETOTALEND").ToString
                        TXTTOTALSELENDS.Text = dr("TOTALSELVEDGEENDS").ToString
                        ' Reference and names
                        TXTREFNO.Text = dr("REFNO").ToString
                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
                        CMBAGENTNAME.Text = Convert.ToString(dr("AGENTNAME").ToString)
                        CMBDELAT.Text = Convert.ToString(dr("DELIVERYAT").ToString)
                        CMBGREY.Text = Convert.ToString(dr("GREY").ToString)
                        TXTORDERNO.Text = dr("ORDERNO").ToString
                        DELDATE.Text = Format(Convert.ToDateTime(dr("DELDATE")).Date, "dd/MM/yyyy")
                        ORDERDATE.Text = Format(Convert.ToDateTime(dr("ORDERDATE")).Date, "dd/MM/yyyy")
                        ' Other details
                        TXTMTRS.Text = Val(dr("MTRS"))
                        TXTNOOFPCS.Text = Val(dr("NOOFPCS"))
                        CMBLOOM.Text = Convert.ToString(dr("LOOM").ToString)
                        TXTBEAMMTRS.Text = dr("BEAMMTRS").ToString
                        TXTCOVERFACTOR.Text = dr("COVERFACTOR").ToString
                        TXTEFFICIENCY.Text = dr("EFFICIENCY").ToString
                        TXTLOOMPROD.Text = dr("LOOMPROD").ToString
                        TXTRPM.Text = dr("RPM").ToString
                        CMBGREYDELAT.Text = Convert.ToString(dr("GREYDELIVERYAT").ToString)
                        GREYDELDATE.Text = Format(Convert.ToDateTime(dr("GREYDELDATE")).Date, "dd/MM/yyyy")
                        ' Total Warp

                        'TXTTOTALWARPPE.Text = Val(dr("TOTALWARPPE"))
                        'TXTTOTALWARPBE.Text = Val(dr("TOTALWARPBE"))
                        'TXTTOTALWARPTE.Text = Val(dr("TOTALWARPTE"))
                        'TXTTOTALWARPWT.Text = Val(dr("TOTALWARPWT"))
                        'TXTTOTALWARPCONS.Text = Val(dr("TOTALWARPCONS"))
                        'TXTTOTALWARPRATE.Text = Val(dr("TOTALWARPRATE"))
                        'TXTTOTALWARPCOST.Text = Val(dr("TOTALWARPCOST"))
                        'TXTTOTALWARPGRIDPE.Text = Val(dr("TOTALWARPGRIDPE"))
                        '' Total Selvedge

                        'TXTTOTALSELPE.Text = Val(dr("TOTALSELVEDGEPE"))
                        'TXTTOTALSELBE.Text = Val(dr("TOTALSELVEDGEBE"))
                        'TXTTOTALSELTE.Text = Val(dr("TOTALSELVEDGETE"))
                        'TXTTOTALSELWT.Text = Val(dr("TOTALSELVEDGEWT"))
                        'TXTTOTALSELCONS.Text = Val(dr("TOTALSELVEDGECONS"))
                        'TXTSELTOTALRATE.Text = Val(dr("TOTALSELVEDGERATE"))
                        'TXTSELTOTALCOST.Text = Val(dr("TOTALSELVEDGECOST"))
                        'TXTTOTALSELGPE.Text = Val(dr("TOTALSELVEDGEGRIDPE"))
                        '' Total Weft

                        'TXTTOTALWEFTPE.Text = Val(dr("TOTALWEFTPE"))
                        'TXTTOTALWEFTBE.Text = Val(dr("TOTALWEFTBE"))
                        'TXTTOTALWEFTTE.Text = Val(dr("TOTALWEFTTE"))
                        'TXTTOTALWEFTWT.Text = Val(dr("TOTALWEFTWT"))
                        'TXTTOTALWEFTCONS.Text = Val(dr("TOTALWEFTCONS"))
                        'TXTTOTALWEFTRATE.Text = Val(dr("TOTALWEFTRATE"))
                        'TXTTOTALWEFTCOST.Text = Val(dr("TOTALWEFTCOST"))
                        'TXTTOTALWEFTGRIDPE.Text = Val(dr("TOTALWEFTGRIDPE"))
                        '' Total DRAWING 

                        'TXTTOTALDRAWENDS.Text = Val(dr("TOTALDRAWENDS"))
                        'TXTTOTALDRAWDENTS.Text = Val(dr("TOTALDRAWDENTS"))




                        TXTFINISHWT.Text = Val(dr("TOTALFINISHWT"))
                    Next
                    'cmbtype.Enabled = False

                    'TOTAL()

                    'warp gridmatching data serializations
                    Dim dttable1 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSRNO, 0) AS WARPGRIDSRNO, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSYM, '') AS WARPGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WARPYARNQUALITY, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPDENIER, 0) AS WARPDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WARPMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WARPSHADE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPPE, 0) AS WARPPE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPBE, 0) AS WARPBE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPTE, 0) AS WARPTE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPWT, 0) AS WARPWT, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCONS, 0) AS WARPCONS, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPRATE, 0) AS WARPRATE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCOST, 0) AS WARPCOST ", "", " DESIGNCARD_WARPMATCHING INNER JOIN YARNQUALITYMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_WARPYARNQUALITYID = YARNQUALITYMASTER.YARN_ID AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = YARNQUALITYMASTER.YARN_YEARID LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = MILLMASTER.MILL_YEARID AND MILLMASTER.MILL_ID = DESIGNCARD_WARPMATCHING.DESIGN_WARPMILLID LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = COLORMASTER.COLOR_yearid AND COLORMASTER.COLOR_id = DESIGNCARD_WARPMATCHING.DESIGN_WARPCOLORID  ", " AND  DESIGNCARD_WARPMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WARPGRIDSRNO")
                    If dttable1.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable1.Rows
                            GRIDWARP.Rows.Add(Val(DTR("WARPGRIDSRNO")), DTR("WARPGRIDSYM").ToString, DTR("WARPYARNQUALITY").ToString, Format(DTR("WARPDENIER"), "0.00"), DTR("WARPMILLNAME").ToString, DTR("WARPSHADE").ToString, Format(DTR("WARPPE"), "0.00"), Format(DTR("WARPBE"), "0.00"), Format(DTR("WARPTE"), "0.00"), Format(DTR("WARPWT"), "0.00"), Format(DTR("WARPCONS"), "0.00"), Format(DTR("WARPRATE"), "0.00"), Format(DTR("WARPCOST"), "0.00"))
                        Next
                    End If
                    ' Warp Gridpattern data serializations
                    Dim dttable2 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_SRNO, 0) AS WARPPATTERNGRIDSRNO, ISNULL(DESIGN_WARPPE, '') AS WARPPATTERNGRIDPE, ISNULL(DESIGN_WARPSYM, '') AS WARPPATTERNGRIDSYM", "", " DESIGNCARD_WARPPATTERN  ", " AND  DESIGNCARD_WARPPATTERN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPPATTERN.DESIGN_YEARID = " & YearId & " ORDER BY WARPPATTERNGRIDSRNO")
                    If dttable2.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable2.Rows
                            GRIDWARPPATTERN.Rows.Add(DTR("WARPPATTERNGRIDSRNO"), DTR("WARPPATTERNGRIDPE"), DTR("WARPPATTERNGRIDSYM").ToString)
                        Next
                    End If
                    'WARP grid shade data serializations
                    Dim dttableWARPshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_sdSRNO, 0) AS WDSRNO, ISNULL(DESIGN_sdSHADE, '') AS WDSHADE, ISNULL(DESIGN_sdMAINSRNO, 0) AS WDMAINSRNO", "", " DESIGNCARD_WARPSHADE  ", " AND  DESIGNCARD_WARPSHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPSHADE.DESIGN_YEARID = " & YearId & " ORDER BY WDSRNO")
                    If dttableWARPshade.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttableWARPshade.Rows
                            DT_WARPDETAILS.Rows.Add(DTR("WDSRNO"), DTR("WDSHADE"), DTR("WDMAINSRNO"))
                        Next
                    End If

                    ' Selvedge Grid data serialization
                    Dim dttable3 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGESRNO, 0) AS SELVEDGEGRIDSRNO, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGESYM, '') AS SELVEDGEGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS SELVEDGEYARNQUALITY, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEDENIER, 0) AS SELVEDGEDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS SELVEDGEMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS SELVEDGESHADE, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEPE, 0) AS SELVEDGEPE, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEBE, 0) AS SELVEDGEBE, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEDTE, 0) AS SELVEDGETE, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEWT, 0) AS SELVEDGEWT, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGECONS, 0) AS SELVEDGECONS, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGERATE, 0) AS SELVEDGERATE, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGECOST, 0) AS SELVEDGECOST ", "", " DESIGNCARD_SELVEDGEMATCHING LEFT OUTER JOIN YARNQUALITYMASTER ON DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEYARNQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEMILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGECOLORID = COLORMASTER.COLOR_id   ", " AND  DESIGNCARD_SELVEDGEMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_SELVEDGEMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY SELVEDGEGRIDSRNO")
                    If dttable3.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable3.Rows
                            GRIDSELVEDGE.Rows.Add(DTR("SELVEDGEGRIDSRNO"), DTR("SELVEDGEGRIDSYM").ToString, DTR("SELVEDGEYARNQUALITY").ToString, Format(DTR("SELVEDGEDENIER"), "0.00"), DTR("SELVEDGEMILLNAME").ToString, DTR("SELVEDGESHADE").ToString, Format(DTR("SELVEDGEPE"), "0.00"), Format(DTR("SELVEDGEBE"), "0.00"), Format(DTR("SELVEDGETE"), "0.00"), Format(DTR("SELVEDGEWT"), "0.00"), Format(DTR("SELVEDGECONS"), "0.00"), Format(DTR("SELVEDGERATE"), "0.00"), Format(DTR("SELVEDGECOST"), "0.00"))
                        Next
                    End If
                    ' Selvedge Gridpattern data serializations
                    Dim dttable4 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_SRNO, 0) AS SELVEDGEPATTERNGRIDSRNO, ISNULL(DESIGN_SELVEDGEPE, '') AS SELVEDGEPATTERNGRIDPE, ISNULL(DESIGN_SELVEDGESYM, '') AS SELVEDGEPATTERNGRIDSYM", "", " DESIGNCARD_SELVEDGEPATTERN  ", " AND  DESIGNCARD_SELVEDGEPATTERN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_SELVEDGEPATTERN.DESIGN_YEARID = " & YearId & " ORDER BY SELVEDGEPATTERNGRIDSRNO")
                    If dttable4.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable4.Rows
                            GRIDSELVEDGEPATTERN.Rows.Add(DTR("SELVEDGEPATTERNGRIDSRNO"), DTR("SELVEDGEPATTERNGRIDPE"), DTR("SELVEDGEPATTERNGRIDSYM").ToString)
                        Next
                    End If

                    'selvedge grid shade data serializations

                    Dim dttableshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_sdSRNO, 0) AS SDSRNO, ISNULL(DESIGN_sdSHADE, '') AS SDSHADE, ISNULL(DESIGN_sdMAINSRNO, 0) AS SDMAINSRNO", "", " DESIGNCARD_SELVEDGESHADE  ", " AND  DESIGNCARD_SELVEDGESHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_SELVEDGESHADE.DESIGN_YEARID = " & YearId & " ORDER BY SDSRNO")
                    If dttableshade.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttableshade.Rows
                            DT_SELDETAILS.Rows.Add(Val(DTR("SDSRNO")), DTR("SDSHADE").ToString, Val(DTR("SDMAINSRNO")))
                        Next
                    End If

                    ' Weft Grid data serialization
                    Dim dttable5 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSRNO, 0) AS WEFTGRIDSRNO, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSYM, '') AS WEFTGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WEFTYARNQUALITY, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTDENIER, 0) AS WEFTDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WEFTMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WEFTSHADE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTPE, 0) AS WEFTPE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTBE, 0) AS WEFTBE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTTE, 0) AS WEFTTE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTWT, 0) AS WEFTWT, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCONS, 0) AS WEFTCONS, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTRATE, 0) AS WEFTRATE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCOST, 0) AS WEFTCOST", "", " DESIGNCARD_WEFTMATCHING LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCOLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTMILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTYARNQUALITYID = YARNQUALITYMASTER.YARN_ID   ", " AND  DESIGNCARD_WEFTMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WEFTMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WEFTGRIDSRNO")
                    If dttable5.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable5.Rows
                            GRIDWEFT.Rows.Add(DTR("WEFTGRIDSRNO"), DTR("WEFTGRIDSYM").ToString, DTR("WEFTYARNQUALITY").ToString, Format(DTR("WEFTDENIER"), "0.00"), DTR("WEFTMILLNAME").ToString, DTR("WEFTSHADE").ToString, Format(DTR("WEFTPE"), "0.00"), Format(DTR("WEFTBE"), "0.00"), Format(DTR("WEFTTE"), "0.00"), Format(DTR("WEFTWT"), "0.00"), Format(DTR("WEFTCONS"), "0.00"), Format(DTR("WEFTRATE"), "0.00"), Format(DTR("WEFTCOST"), "0.00"))
                        Next
                    End If
                    ' Weft GridPattern data serialization
                    Dim dttable6 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_SRNO, 0) AS WEFTPATTERNGRIDSRNO, ISNULL(DESIGN_WEFTPE, '') AS WEFTPATTERNGRIDPE, ISNULL(DESIGN_WARPSYM, '') AS WEFTPATTERNGRIDSYM", "", " DESIGNCARD_WEFTPATTERN  ", " AND  DESIGNCARD_WEFTPATTERN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WEFTPATTERN.DESIGN_YEARID = " & YearId & " ORDER BY WEFTPATTERNGRIDSRNO")
                    If dttable6.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable6.Rows
                            GRIDWEFTPATTERN.Rows.Add(DTR("WEFTPATTERNGRIDSRNO"), DTR("WEFTPATTERNGRIDPE"), DTR("WEFTPATTERNGRIDSYM").ToString)
                        Next
                    End If
                    'WEFT grid shade data serializations

                    Dim dttableWEFTshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_FDSRNO, 0) AS FDSRNO, ISNULL(DESIGN_FDSHADE, '') AS FDSHADE, ISNULL(DESIGN_FDMAINSRNO, 0) AS FDMAINSRNO", "", " DESIGNCARD_WEFTSHADE  ", " AND  DESIGNCARD_WEFTSHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WEFTSHADE.DESIGN_YEARID = " & YearId & " ORDER BY FDSRNO")
                    If dttableWEFTshade.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttableWEFTshade.Rows
                            DT_WEFTDETAILS.Rows.Add(Val(DTR("FDSRNO")), DTR("FDSHADE").ToString, Val(DTR("FDMAINSRNO")))
                        Next
                    End If
                    'DRAWING FIELD
                    Dim dttable7 As DataTable = OBJCMN.SEARCH("  ISNULL(DESIGN_DRAWINGSRNO, 0) AS DRAWINGSRNO, ISNULL(DESIGN_DRAWINGENDS, 0) AS DRAWINGENDS, ISNULL(DESIGN_DRAWINGREPEATMARK, '') AS DRAWINGREPEATMARK, ISNULL(DESIGN_DRAWINGREPEAT, 0) AS DRAWINGREPEAT, ISNULL(DESIGN_DRAWINGREPEATMARK1, '') AS DRAWINGGRIDREPEATMARK1, ISNULL(DESIGN_DRAWINGREPEAT1, 0) AS DRAWINGREPEAT1, ISNULL(DESIGN_DRAWINGREPEATMARK2, '') AS DRAWINGREPEATMARK2, ISNULL(DESIGN_DRAWINGREPEAT2, 0) AS DRAWINGREPEAT2 ", "", " DESIGNCARD_DRAWING  ", " AND  DESIGNCARD_DRAWING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_DRAWING.DESIGN_YEARID = " & YearId & " ORDER BY DRAWINGSRNO")
                    If dttable7.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable7.Rows
                            GRIDDRAWING.Rows.Add(DTR("DRAWINGSRNO"), DTR("DRAWINGENDS").ToString, DTR("DRAWINGREPEATMARK").ToString, DTR("DRAWINGREPEAT"), DTR("DRAWINGGRIDREPEATMARK1").ToString, DTR("DRAWINGREPEAT1"), DTR("DRAWINGREPEATMARK2").ToString, DTR("DRAWINGREPEAT2"))

                        Next
                    End If
                    TOTAL()
                    CALC()

                End If
            End If

            'If GRIDSELVEDGE.RowCount > 0 Then
            '    txtcardno.Text = Val(GRIDSELVEDGE.Rows(GRIDSELVEDGE.RowCount - 1).Cells(0).Value) + 1
            'Else
            '    txtcardno.Text = 1
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub TOTAL()
        TOTALSELVEDGE()
        TOTALSELVEDGEPATTERN()
        GETSELPE()
        GETWARPPE()
        GETWEFTPE()
        cmdbtn1_Click(Nothing, Nothing)
        Button2_Click(Nothing, Nothing)
        Button1_Click(Nothing, Nothing)
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
            GRIDWARP.Rows.Add(Val(TXTWARPSRNO.Text.Trim), CMBGRIDSYM.Text.Trim, CMBWARPQUALITY.Text.Trim, TXTWARPDENIER.Text.Trim, CMBWARPMILLNAME.Text.Trim, CMBWARPSHADE.Text.Trim, Val(TXTWARPPE.Text.Trim), Val(TXTWARPBE.Text.Trim), Val(TXTWARPTE.Text.Trim), Val(TXTWARPWT.Text.Trim), Val(TXTWARPCONS.Text.Trim), Val(TXTWARPRATE.Text.Trim), Val(TXTWARPCOST.Text.Trim))
            getsrno(GRIDWARP)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDWARP.Item(WSRNO.Index, TEMPROW).Value = Val(TXTWARPSRNO.Text.Trim)
            GRIDWARP.Item(WSYM.Index, TEMPROW).Value = CMBGRIDSYM.Text.Trim
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

            'WE WILL REMOVE THE DATA AND REINSERT, THIS IS CODE FOR REMOAL, FOR INSERTING WE HAVE ENTERED CODE BELOW
            If EDIT = False Then
LINE1:
                For I As Integer = 0 To DT_WARPDETAILS.Rows.Count - 1
                    If GRIDWARP.Rows(GRIDWARP.CurrentRow.Index).Cells(WSRNO.Index).Value = Val(DT_WARPDETAILS.Rows(I).Item("WDMAINSRNO")) Then
                        DT_WARPDETAILS.Rows.RemoveAt(I)
                        GoTo LINE1
                    End If
                Next
            End If
        End If

        GRIDWARPDESC.EndEdit() '


        For Each MTRSROW1 As DataGridViewRow In GRIDWARPDESC.Rows
            Dim currentMainSrNo As Object = MTRSROW1.Cells(WDMAINSRNO.Index).Value
            For i As Integer = DT_WARPDETAILS.Rows.Count - 1 To 0 Step -1
                If DT_WARPDETAILS.Rows(i)("WDMAINSRNO") = currentMainSrNo Then
                    DT_WARPDETAILS.Rows.RemoveAt(i)
                End If
            Next

            ' Now add new rows for this entry as usual
            For Each MTRSROW As DataGridViewRow In GRIDWARPDESC.Rows
                If Not MTRSROW.IsNewRow Then
                    Dim newRow As DataRow = DT_WARPDETAILS.NewRow()
                    newRow("WDSRNO") = MTRSROW.Cells(WDSRNO.Index).Value
                    newRow("WDSHADE") = MTRSROW.Cells(WDSHADE.Index).Value
                    newRow("WDMAINSRNO") = currentMainSrNo
                    DT_WARPDETAILS.Rows.Add(newRow)
                End If
            Next
        Next
        Dim maxShadeCount As Integer = 0
        For Each dr As DataRow In DT_WARPDETAILS.Rows
            Dim srno As Object = dr("WDMAINSRNO")
            Dim shadeRows As DataRow() = DT_WARPDETAILS.Select("WDMAINSRNO = '" & srno & "'")
            If shadeRows.Length > maxShadeCount Then maxShadeCount = shadeRows.Length
        Next
        For i As Integer = 1 To maxShadeCount
            Dim colName As String = "WARP" & i
            If Not GRIDWARP.Columns.Contains(colName) Then
                GRIDWARP.Columns.Add(colName, colName)
            End If
        Next

        For Each gridRow As DataGridViewRow In GRIDWARP.Rows
            If gridRow.IsNewRow Then Continue For
            Dim fsrno As Object = gridRow.Cells("WSRNO").Value
            Dim matchedRows As DataRow() = DT_WARPDETAILS.Select("WDMAINSRNO = '" & fsrno & "'")
            For shadeIdx As Integer = 0 To matchedRows.Length - 1
                Dim shadeValue As Object = matchedRows(shadeIdx)("WDSHADE")
                gridRow.Cells("WARP" & (shadeIdx + 1)).Value = shadeValue
            Next
        Next

        GRIDWARP.ClearSelection()
        CMBGRIDSYM.Focus()
        clearwarp()
        TOTALWARP()

        If GRIDWARP.RowCount > 0 Then
            TXTWARPSRNO.Text = Val(GRIDWARP.Rows(GRIDWARP.RowCount - 1).Cells(0).Value) + 1
        Else
            TXTWARPSRNO.Text = 1
        End If
    End Sub
    Sub COPYSYM()
        CMBGRIDSYM.Items.Clear()

        Dim symSet As New HashSet(Of String)
        If GRIDWARPPATTERN.RowCount > 0 Then
            For Each row As DataGridViewRow In GRIDWARPPATTERN.Rows
                Dim cellValue = row.Cells(WPSYM.Index).Value
                If cellValue IsNot Nothing AndAlso Not IsDBNull(cellValue) AndAlso Not String.IsNullOrWhiteSpace(cellValue.ToString) Then
                    symSet.Add(cellValue.ToString)
                End If
            Next
        End If
        For Each symVal As String In symSet
            CMBGRIDSYM.Items.Add(symVal)
        Next
    End Sub
    Sub clearwarp()
        'TXTWARPSRNO.Clear()
        'CMBGRIDSYM.Clear()
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
        TOTALWARPPATTERN()
        TXTGRIDPE.Focus()
        If GRIDWARPPATTERN.RowCount > 0 Then
            TXTWARPGSRNO.Text = Val(GRIDWARPPATTERN.Rows(GRIDWARPPATTERN.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDSELVEDGE.RowCount) + 1
        Else
            TXTWARPGSRNO.Text = 1
        End If
        Button1_Click(errorvalid, New EventArgs)
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
            'WE WILL REMOVE THE DATA AND REINSERT, THIS IS CODE FOR REMOAL, FOR INSERTING WE HAVE ENTERED CODE BELOW
            If EDIT = False Then
LINE1:
                For I As Integer = 0 To DT_SELDETAILS.Rows.Count - 1
                    If GRIDSELVEDGE.Rows(GRIDSELVEDGE.CurrentRow.Index).Cells(SSRNO.Index).Value = Val(DT_SELDETAILS.Rows(I).Item("SDMAINSRNO")) Then
                        DT_SELDETAILS.Rows.RemoveAt(I)
                        GoTo LINE1
                    End If
                Next
            End If
        End If
        'If String.IsNullOrEmpty(TXTSELSYMBOL.Text) Then
        '    TXTSELSYMBOL.Text = "A"
        'Else
        '    TXTSELSYMBOL.Text = IncrementAlphabet(TXTSELSYMBOL.Text)
        'End If

        GRIDSELDESC.EndEdit() '
        ' Remove all rows for the current entry before adding new ones
        For Each MTRSROW1 As DataGridViewRow In GRIDSELDESC.Rows
            Dim currentMainSrNo As Object = MTRSROW1.Cells(SDMAINSRNO.Index).Value
            For i As Integer = DT_SELDETAILS.Rows.Count - 1 To 0 Step -1
                If DT_SELDETAILS.Rows(i)("SDMAINSRNO") = currentMainSrNo Then
                    DT_SELDETAILS.Rows.RemoveAt(i)
                End If
            Next

            ' Now add new rows for this entry as usual
            For Each MTRSROW As DataGridViewRow In GRIDSELDESC.Rows
                If Not MTRSROW.IsNewRow Then
                    Dim newRow As DataRow = DT_SELDETAILS.NewRow()
                    newRow("SDSRNO") = MTRSROW.Cells(SDSRNO.Index).Value
                    newRow("SDSHADE") = MTRSROW.Cells(SDSHADE.Index).Value
                    newRow("SDMAINSRNO") = currentMainSrNo
                    DT_SELDETAILS.Rows.Add(newRow)
                End If
            Next
        Next


        GRIDSELVEDGE.ClearSelection()
        CLEARSELVEDGE()
        COPYSELSYM()
        TOTALSELVEDGE()
        If GRIDSELVEDGE.RowCount > 0 Then
            TXTSELSRNO.Text = Val(GRIDSELVEDGE.Rows(GRIDSELVEDGE.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDSELVEDGE.RowCount) + 1
        Else
            TXTSELSRNO.Text = 1
        End If
        CMBSELYARNQUALITY.Focus()
    End Sub
    Sub COPYSELSYM()
        CMBSELGSYM.Items.Clear()

        Dim symSet As New HashSet(Of String)
        For Each row As DataGridViewRow In GRIDSELVEDGE.Rows
            If Not IsDBNull(row.Cells(SSYM.Index).Value) AndAlso Not String.IsNullOrWhiteSpace(row.Cells(SSYM.Index).Value.ToString) Then
                symSet.Add(row.Cells(SSYM.Index).Value.ToString)
            End If
        Next

        For Each symVal As String In symSet
            CMBSELGSYM.Items.Add(symVal)
        Next


    End Sub
    Sub FILLSELPATTERNGRID()
        If GRIDSELPDOUBLECLICK = False Then
            GRIDSELVEDGEPATTERN.Rows.Add(Val(TXTSELGSRNO.Text.Trim), TXTSELGPE.Text.Trim, CMBSELGSYM.Text.Trim)
            getsrno(GRIDSELVEDGEPATTERN)
        ElseIf GRIDSELPDOUBLECLICK = True Then
            GRIDSELVEDGEPATTERN.Item(SPSRNO.Index, TEMPSELPROW).Value = Val(TXTSELGSRNO.Text.Trim)
            GRIDSELVEDGEPATTERN.Item(SPENDS.Index, TEMPSELPROW).Value = Val(TXTSELGPE.Text.Trim)
            GRIDSELVEDGEPATTERN.Item(SPSYM.Index, TEMPSELPROW).Value = CMBSELGSYM.Text.Trim
            TXTSELGSRNO.Focus()
            GRIDSELPDOUBLECLICK = False
        End If
        GRIDSELVEDGEPATTERN.ClearSelection()
        TXTSELGPE.Clear()
        CMBSELGSYM.Text = ""
        TXTSELGPE.Focus()
        If GRIDSELVEDGEPATTERN.RowCount > 0 Then
            TXTSELGSRNO.Text = Val(GRIDSELVEDGEPATTERN.Rows(GRIDSELVEDGEPATTERN.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDSELVEDGE.RowCount) + 1
        Else
            TXTSELGSRNO.Text = 1
        End If
        TOTALSELVEDGEPATTERN()
    End Sub
    Sub CLEARSELVEDGE()
        TXTSELSRNO.Clear()
        'TXTSELSYMBOL.Clear()
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
            GRIDWEFT.Rows.Add(Val(TXTWEFTSRNO.Text.Trim), CMBWEFTGRIDSYMBOL.Text.Trim, CMBWEFTYARNQUALITY.Text.Trim, Val(TXTWEFTDEN.Text.Trim), CMBWEFTMILLNAME.Text.Trim, cmbweftshade.Text.Trim, Val(TXTWEFTPE.Text.Trim), Val(TXTWEFTBE.Text.Trim), Val(TXTWEFTTE.Text.Trim), Val(TXTWEFTWT.Text.Trim), Val(TXTWEFTCONS.Text.Trim), Val(TXTWEFTRATE.Text.Trim), Val(TXTWEFTCOST.Text.Trim))
            getsrno(GRIDWEFT)
        ElseIf GRIDWEFTDOUBLECLICK = True Then
            GRIDWEFT.Item(FSRNO.Index, TEMPWEFTROW).Value = Val(TXTWEFTSRNO.Text.Trim)
            GRIDWEFT.Item(FSYM.Index, TEMPWEFTROW).Value = CMBWEFTGRIDSYMBOL.Text.Trim
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

            'WE WILL REMOVE THE DATA And REINSERT, THIS Is CODE For REMOAL, FOR INSERTING WE HAVE ENTERED CODE BELOW
            If EDIT = False Then
LINE1:
                For I As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                    If GRIDWEFT.Rows(GRIDWEFT.CurrentRow.Index).Cells(FSRNO.Index).Value = Val(DT_WEFTDETAILS.Rows(I).Item("FDMAINSRNO")) Then
                        DT_WEFTDETAILS.Rows.RemoveAt(I)
                        GoTo LINE1
                    End If
                Next
            End If
        End If

        GRIDWEFTDESC.EndEdit()

        For Each MTRSROW1 As DataGridViewRow In GRIDWEFTDESC.Rows
            Dim currentMainSrNo As Object = MTRSROW1.Cells(FDMAINSRNO.Index).Value
            For i As Integer = DT_WEFTDETAILS.Rows.Count - 1 To 0 Step -1
                If DT_WEFTDETAILS.Rows(i)("FDMAINSRNO") = currentMainSrNo Then
                    DT_WEFTDETAILS.Rows.RemoveAt(i)
                End If
            Next

            ' Now add new rows for this entry as usual
            For Each MTRSROW As DataGridViewRow In GRIDWEFTDESC.Rows
                If Not MTRSROW.IsNewRow Then
                    Dim newRow As DataRow = DT_WEFTDETAILS.NewRow()
                    newRow("FDSRNO") = MTRSROW.Cells(FDSRNO.Index).Value
                    newRow("FDSHADE") = MTRSROW.Cells(FDSHADE.Index).Value
                    newRow("FDMAINSRNO") = currentMainSrNo
                    DT_WEFTDETAILS.Rows.Add(newRow)
                End If
            Next
        Next

        Dim maxShadeCount As Integer = 0
        For Each dr As DataRow In DT_WEFTDETAILS.Rows
            Dim srno As Object = dr("FDMAINSRNO")
            Dim shadeRows As DataRow() = DT_WEFTDETAILS.Select("FDMAINSRNO = '" & srno & "'")
            If shadeRows.Length > maxShadeCount Then maxShadeCount = shadeRows.Length
        Next
        For i As Integer = 1 To maxShadeCount
            Dim colName As String = "WEFT" & i
            If Not GRIDWEFT.Columns.Contains(colName) Then
                GRIDWEFT.Columns.Add(colName, colName)
            End If
        Next

        For Each gridRow As DataGridViewRow In GRIDWEFT.Rows
            If gridRow.IsNewRow Then Continue For
            Dim fsrno As Object = gridRow.Cells("FSRNO").Value
            Dim matchedRows As DataRow() = DT_WEFTDETAILS.Select("FDMAINSRNO = '" & fsrno & "'")
            For shadeIdx As Integer = 0 To matchedRows.Length - 1
                Dim shadeValue As Object = matchedRows(shadeIdx)("FDSHADE")
                gridRow.Cells("WEFT" & (shadeIdx + 1)).Value = shadeValue
            Next
        Next

        GRIDWEFT.ClearSelection()
        CLEARWEFT()
        CMBWEFTGRIDSYMBOL.Focus()
        If GRIDWEFT.RowCount > 0 Then
            TXTWEFTSRNO.Text = Val(GRIDWEFT.Rows(GRIDWEFT.RowCount - 1).Cells(0).Value) + 1
        Else
            TXTWEFTSRNO.Text = 1
        End If
        GRIDWEFTDESC.RowCount = 0
        TXTFDSRNO.Text = GRIDWEFTDESC.RowCount + 1
    End Sub
    Sub COPYWEFTSYM()
        CMBWEFTGRIDSYMBOL.Items.Clear()

        Dim symSet As New HashSet(Of String)
        If GRIDWEFTPATTERN.RowCount > 0 Then
            For Each row As DataGridViewRow In GRIDWEFTPATTERN.Rows
                Dim cellValue = row.Cells(FPSYM.Index).Value
                If cellValue IsNot Nothing AndAlso Not IsDBNull(cellValue) AndAlso Not String.IsNullOrWhiteSpace(cellValue.ToString) Then
                    symSet.Add(cellValue.ToString)
                End If
            Next
        End If
        For Each symVal As String In symSet
            CMBWEFTGRIDSYMBOL.Items.Add(symVal)
        Next
    End Sub
    Sub CLEARWEFT()
        'TXTWEFTSRNO.Clear()
        'CMBWEFTGRIDSYMBOL.Clear()
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
        TOTALWEFTPATTERN()
        If GRIDWEFTPATTERN.RowCount > 0 Then
            TXTWEFTGRIDSRNO.Text = Val(GRIDWEFTPATTERN.Rows(GRIDWEFTPATTERN.RowCount - 1).Cells(0).Value) + 1
            ' TXTSRNO.Text = Val(GRIDSELVEDGE.RowCount) + 1
        Else
            TXTWEFTGRIDSRNO.Text = 1
        End If
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
        TXTENDPERINCH.Text = 0.00
        txttotaldentsrepeat.Text = 0.00
        TXTTOTALENDS.Text = 0.00
        TXTTOTALMAINENDS.Text = 0.00
        txtxvalue.Text = 0.00

        If TXTLEFTSEL.Text <> "" And TXTREEDSPACE.Text <> "" Then TXTMAINRS.Text = Format(Val(TXTREEDSPACE.Text) - Val(TXTLEFTSEL.Text) - Val(TXTRIGHTSEL.Text), "0.00")
        If TXTREED.Text <> "" Then TXTDENTS.Text = Format(Val(TXTREED.Text) / 2, "0.00")
        If TXTDENTS.Text <> "" And TXTMAINRS.Text <> "" Then TXTTOTALDENTSMAIN.Text = Format(Val(TXTDENTS.Text) * Val(TXTMAINRS.Text), "0.00")
        If TXTLEFTSEL.Text <> "" And TXTDENTS.Text <> "" Then TXTLEFTSELDENTS.Text = Format(Val(TXTLEFTSEL.Text) * Val(TXTDENTS.Text), "0.00")
        If TXTDENTS.Text <> "" And TXTRIGHTSEL.Text <> "" Then TXTRIGHTSELDENTS.Text = Format(Val(TXTRIGHTSEL.Text) * Val(TXTDENTS.Text), "0.00")
        If TXTRIGHTSELDENTS.Text <> "" And TXTLEFTSELDENTS.Text <> "" Then TXTTOTALSELVEDGEDENTS.Text = Format(Val(TXTLEFTSELDENTS.Text) + Val(TXTRIGHTSELDENTS.Text), "0.00")
        If TXTTOTALDENTSMAIN.Text <> "" And TXTTOTALSELVEDGEDENTS.Text <> "" Then TXTTOTALDENTS.Text = Format(Val(TXTTOTALDENTSMAIN.Text) + Val(TXTTOTALSELVEDGEDENTS.Text), "0.00")
        If TXTLEFTSELENDS.Text <> "" And TXTLEFTSELDENTS.Text <> "" Then TXTLEFTSELTOTALENDS.Text = Format(Val(TXTLEFTSELENDS.Text) * Val(TXTLEFTSELDENTS.Text), "0.00")
        If TXTRIGHTSELENDS.Text <> "" And TXTRIGHTSELDENTS.Text <> "" Then TXTRIGHTSELTOTALENDS.Text = Format(Val(TXTRIGHTSELENDS.Text) * Val(TXTRIGHTSELDENTS.Text), "0.00")
        If TXTLEFTSELTOTALENDS.Text <> "" And TXTRIGHTSELTOTALENDS.Text <> "" Then TXTTOTALSELENDS.Text = Format(Val(TXTLEFTSELTOTALENDS.Text) + Val(TXTRIGHTSELTOTALENDS.Text), "0.00")
        If TXTTOTALDRAWDENTS.Text <> "" And TXTTOTALDENTS.Text <> "" Then txttotaldentsrepeat.Text = Format(Val(TXTTOTALDENTS.Text) / Val(TXTTOTALDRAWDENTS.Text), "0.00")
        If txttotaldentsrepeat.Text <> "" And TXTTOTALDRAWENDS.Text <> "" Then
            Dim totalDents As Double = Val(txttotaldentsrepeat.Text)
            Dim totalDrawEnds As Double = Val(TXTTOTALDRAWENDS.Text)
            Dim result As Double = totalDents * totalDrawEnds
            TXTTOTALENDS.Text = Math.Ceiling(result).ToString()
        End If
        If TXTTOTALENDS.Text <> "" And TXTREEDSPACE.Text <> "" Then TXTENDPERINCH.Text = Format(Val(TXTTOTALENDS.Text) / Val(TXTREEDSPACE.Text), "0.00")
        If TXTTOTALENDS.Text <> "" And TXTTOTALSELENDS.Text <> "" Then TXTTOTALMAINENDS.Text = Format(Val(TXTTOTALENDS.Text) - Val(TXTTOTALSELENDS.Text), "0.00")
        If TXTTOTALMAINENDS.Text <> "" And TXTTOTALWARPGRIDPE.Text <> "" Then
            Dim totalMainEnds As Double = Val(TXTTOTALMAINENDS.Text)
            Dim pcs As Double = Val(TXTTOTALWARPGRIDPE.Text)
            Dim result As Double = totalMainEnds / pcs
            txtxvalue.Text = Math.Ceiling(result).ToString()
        End If
        If TXTTOTALSELENDS.Text <> "" Then TXTSELTE.Text = Format(Val(TXTTOTALSELENDS.Text), "0.00")
        If txtxvalue.Text <> "" Then
            'WARP ENDS IN GRID
            For Each row As DataGridViewRow In GRIDWARP.Rows
                If row.Cells(WPE.Index).Value IsNot DBNull.Value Then
                    row.Cells(WENDS.Index).Value = Format(Val(txtxvalue.Text) * Val(row.Cells(WPE.Index).Value), "0")
                End If
            Next
            'WARP WT IN GRID
            If TXTWARPTL.Text <> "" Then
                For Each row As DataGridViewRow In GRIDWARP.Rows
                    If row.Cells(WENDS.Index).Value IsNot DBNull.Value And row.Cells(WDENIER.Index).Value IsNot DBNull.Value Then
                        row.Cells(WWT.Index).Value = Format(Val(row.Cells(WENDS.Index).Value) * Val(row.Cells(WDENIER.Index).Value) * Val(TXTWARPTL.Text) / 9000000, "0.000")
                    End If
                Next
            End If
            'WEFT WT IN GRID
            If TXTWEFTTL.Text <> "" And TXTREEDSPACE.Text <> "" And TXTPICKS.Text <> "" Then
                For Each row As DataGridViewRow In GRIDWEFT.Rows
                    If row.Cells(FDENIER.Index).Value IsNot DBNull.Value Then
                        row.Cells(FWT.Index).Value = Format(Val(TXTREEDSPACE.Text) * Val(TXTPICKS.Text) * Val(row.Cells(FDENIER.Index).Value) * Val(TXTWEFTTL.Text) / 9000000, "0.000")
                    End If
                Next
            End If
            'SELVEDGE WT IN GRID
            If TXTTOTALSELENDS.Text <> "" And TXTWARPTL.Text <> "" Then
                For Each row As DataGridViewRow In GRIDSELVEDGE.Rows
                    If row.Cells(SDENIER.Index).Value IsNot DBNull.Value Then
                        row.Cells(SWT.Index).Value = Format(Val(row.Cells(SDENIER.Index).Value) * Val(TXTWARPTL.Text) * Val(TXTTOTALSELENDS.Text) / 9000000, "0.000")
                    End If
                Next
            End If
            'WEFT ENDS IN GRID
            If TXTPICKS.Text <> "" And TXTREEDSPACE.Text <> "" Then
                For Each row As DataGridViewRow In GRIDWEFT.Rows
                    row.Cells(FENDS.Index).Value = Format(Val(TXTREEDSPACE.Text) * Val(TXTPICKS.Text), "0.00")
                Next
            End If
        End If
        TXTFWT.Text = 0.000
        TXTFINISHWT.Text = 0.000
        TXTFWT.Text = Format(Val(TXTTOTALWARPWT.Text) + Val(TXTTOTALWEFTWT.Text) + Val(TXTTOTALSELWT.Text), "0.000")
        If TXTSHRINKAGEPER.Text <> "" Then TXTFINISHWT.Text = Format(Val(TXTFWT.Text) + (1 + (Val(TXTSHRINKAGEPER.Text) / 100)), "0.000")
        If TXTNOOFPCS.Text <> "" And TXTPCSL.Text <> "" Then
            Dim pcs As Double = Val(TXTNOOFPCS.Text)
            Dim pcsl As Double = Val(TXTPCSL.Text)
            Dim result As Double = pcs * pcsl
            TXTBEAMMTRS.Text = Format(Val(TXTFINISHWT.Text) * result, "0.00")
        End If
        GETSELPE()
        GETWARPPE()
        GETWEFTPE()
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

        TXTTOTALWARPPE.Text = Format(PE, "0.00")
        TXTTOTALWARPBE.Text = Format(BE, "0.00")
        TXTTOTALWARPTE.Text = Format(TE, "0.00")
        TXTTOTALWARPWT.Text = Format(WT, "0.000")
        TXTTOTALWARPCONS.Text = Format(CONS, "0.00")
        TXTTOTALWARPRATE.Text = Format(RATE, "0.00")
        TXTTOTALWARPCOST.Text = Format(COST, "0.00")
    End Sub
    Sub TOTALWARPPATTERN()
        CalculateTotalsForGridPATTERN(GRIDWARPPATTERN, "WPENDS", "WPR", "WPR1", "WPR2", "WPTR", "WPTR1", "WPTR2")

        Dim PE As Double
        PE = 0.00
        For Each row As DataGridViewRow In GRIDWARPPATTERN.Rows
            If row.Cells(WPTR2.Index).Value IsNot DBNull.Value Then
                PE = PE + Val(row.Cells(WPTR2.Index).Value)
            End If
        Next
        TXTTOTALWARPGRIDPE.Text = Format(PE, "0.00")
        If GRIDWARP.RowCount > 0 Then GETWARPPE()
    End Sub
    Sub TOTALSELVEDGE()
        Dim PE, BE, TE, WT, CONS, RATE, COST, GRIDSPE As Double
        PE = 0.00
        BE = 0.00
        TE = 0.00
        WT = 0.00
        CONS = 0.00
        RATE = 0.00
        COST = 0.00
        For Each row As DataGridViewRow In GRIDSELVEDGE.Rows
            If row.Cells(SPE.Index).Value IsNot DBNull.Value Then
                PE = PE + Val(row.Cells(SPE.Index).Value)
            End If
            If row.Cells(SBE.Index).Value IsNot DBNull.Value Then
                BE = BE + Val(row.Cells(SBE.Index).Value)
            End If
            If row.Cells(SENDS.Index).Value IsNot DBNull.Value Then
                TE = TE + Val(row.Cells(SENDS.Index).Value)
            End If
            If row.Cells(SWT.Index).Value IsNot DBNull.Value Then
                WT = WT + Val(row.Cells(SWT.Index).Value)
            End If
            If row.Cells(SCONS.Index).Value IsNot DBNull.Value Then
                CONS = CONS + Val(row.Cells(SCONS.Index).Value)
            End If
            If row.Cells(SRATE.Index).Value IsNot DBNull.Value Then
                RATE = RATE + Val(row.Cells(SRATE.Index).Value)
            End If
            If row.Cells(SCOST.Index).Value IsNot DBNull.Value Then
                COST = COST + Val(row.Cells(SCOST.Index).Value)
            End If

        Next
        TXTTOTALSELPE.Text = Format(PE, "0.00")
        TXTTOTALSELBE.Text = Format(BE, "0.00")
        TXTTOTALSELTE.Text = Format(TE, "0.00")
        TXTTOTALSELWT.Text = Format(WT, "0.000")
        TXTTOTALSELCONS.Text = Format(CONS, "0.00")
        TXTSELTOTALRATE.Text = Format(RATE, "0.00")
        TXTSELTOTALCOST.Text = Format(COST, "0.00")
    End Sub
    Sub TOTALSELVEDGEPATTERN()
        CalculateTotalsForGridPATTERN(GRIDSELVEDGEPATTERN, "SPENDS", "SPREPEAT", "SPREPEAT1", "SPREPEAT2", "SPTR", "SPTR1", "SPTR2")
        Dim PE As Double
        PE = 0.00
        For Each row As DataGridViewRow In GRIDSELVEDGEPATTERN.Rows
            If row.Cells(SPTR2.Index).Value IsNot DBNull.Value Then
                PE = PE + Val(row.Cells(SPTR2.Index).Value)
            End If
        Next
        TXTTOTALSELGPE.Text = Format(PE, "0.00")
        ' Call GETSELPE() only if the grid exists and has data rows
        If GRIDSELVEDGE IsNot Nothing AndAlso GRIDSELVEDGE.RowCount > 0 Then
            GETSELPE()
        End If
    End Sub
    Sub TOTALWEFT()
        Dim PE, BE, TE, WT, CONS, RATE, COST, GRIDPE As Double
        PE = 0.00
        BE = 0.00
        TE = 0.00
        WT = 0.00
        CONS = 0.00
        RATE = 0.00
        COST = 0.00
        GRIDPE = 0.00
        For Each row As DataGridViewRow In GRIDWEFT.Rows
            If row.Cells(FPE.Index).Value IsNot DBNull.Value Then
                PE = PE + Val(row.Cells(FPE.Index).Value)
            End If
            If row.Cells(FBE.Index).Value IsNot DBNull.Value Then
                BE = BE + Val(row.Cells(FBE.Index).Value)
            End If
            If row.Cells(FENDS.Index).Value IsNot DBNull.Value Then
                TE = TE + Val(row.Cells(FENDS.Index).Value)
            End If
            If row.Cells(FWT.Index).Value IsNot DBNull.Value Then
                WT = WT + Val(row.Cells(FWT.Index).Value)
            End If
            If row.Cells(FCONS.Index).Value IsNot DBNull.Value Then
                CONS = CONS + Val(row.Cells(FCONS.Index).Value)
            End If
            If row.Cells(FRATE.Index).Value IsNot DBNull.Value Then
                RATE = RATE + Val(row.Cells(FRATE.Index).Value)
            End If
            If row.Cells(FCOST.Index).Value IsNot DBNull.Value Then
                COST = COST + Val(row.Cells(FCOST.Index).Value)
            End If
        Next
        TXTTOTALWEFTPE.Text = Format(PE, "0.00")
        TXTTOTALWEFTBE.Text = Format(BE, "0.00")
        TXTTOTALWEFTTE.Text = Format(TE, "0.00")
        TXTTOTALWEFTWT.Text = Format(WT, "0.000")
        TXTTOTALWEFTCONS.Text = Format(CONS, "0.00")
        TXTTOTALWEFTRATE.Text = Format(RATE, "0.00")
        TXTTOTALWEFTCOST.Text = Format(COST, "0.00")
    End Sub
    Sub TOTALWEFTPATTERN()
        CalculateTotalsForGridPATTERN(GRIDWEFTPATTERN, "FPENDS", "FPR", "FPR1", "FPR2", "FPTR", "FPTR1", "FPTR2")
        Dim PE As Double
        PE = 0.00
        For Each row As DataGridViewRow In GRIDWEFTPATTERN.Rows
            If row.Cells(FPTR2.Index).Value IsNot DBNull.Value Then
                PE = PE + Val(row.Cells(FPTR2.Index).Value)
            End If
        Next
        TXTTOTALWEFTGRIDPE.Text = Format(PE, "0.00")
        If GRIDWEFT.RowCount > 0 Then GETWEFTPE()
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
                CMBGRIDSYM.Text = GRIDWARP.Item(WSYM.Index, TEMPROW).Value
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
                CMBGRIDSYM.Focus()
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
                CMBSELYARNQUALITY.Focus()
            End If
        End If
    End Sub

    Sub EDITWEFTROW()
        If GRIDWEFT.CurrentRow IsNot Nothing Then
            If GRIDWEFT.CurrentRow.Index >= 0 Then
                TEMPWEFTROW = GRIDWEFT.CurrentRow.Index
                TXTWEFTSRNO.Text = GRIDWEFT.Item(FSRNO.Index, TEMPWEFTROW).Value
                CMBWEFTGRIDSYMBOL.Text = GRIDWEFT.Item(FSYM.Index, TEMPWEFTROW).Value
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
                CMBWEFTYARNQUALITY.Focus()
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
    Sub EDITSELVEDGEPATTERNROW()
        If GRIDSELVEDGEPATTERN.CurrentRow IsNot Nothing Then
            If GRIDSELVEDGEPATTERN.CurrentRow.Index >= 0 Then
                TEMPSELPROW = GRIDSELVEDGEPATTERN.CurrentRow.Index
                TXTSELGSRNO.Text = GRIDSELVEDGEPATTERN.Item(SPSRNO.Index, TEMPSELPROW).Value
                TXTSELGPE.Text = GRIDSELVEDGEPATTERN.Item(SPENDS.Index, TEMPSELPROW).Value
                CMBSELGSYM.Text = GRIDSELVEDGEPATTERN.Item(SPSYM.Index, TEMPSELPROW).Value.ToString
                GRIDSELPDOUBLECLICK = True
                TXTSELGPE.Focus()
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

    Private Sub CMDPHOTOUPLOAD_Click(sender As Object, e As EventArgs) Handles CMDPHOTOUPLOAD.Click
        If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png;*.pdf)|*.bmp;*.jpeg;*.png;*.pdf"
        OpenFileDialog1.ShowDialog()

        OpenFileDialog1.AddExtension = True
        TXTFILENAME.Text = OpenFileDialog1.SafeFileName
        txtimgpath.Text = OpenFileDialog1.FileName
        TXTNEWIMGPATH.Text = Application.StartupPath & "\UPLOADDOCS\" & txtcardno.Text.Trim & TXTFILENAME.Text.Trim
        On Error Resume Next

        If txtimgpath.Text.Trim.Length <> 0 Then
            PBPHOTO.ImageLocation = txtimgpath.Text.Trim
            PBPHOTO.Load(txtimgpath.Text.Trim)
        End If
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

    Private Sub CMBGRIDSYM_Validated(sender As Object, e As EventArgs) Handles CMBGRIDSYM.Validated
        Try
            If CMBGRIDSYM.Text <> "" Then
                For Each symRow As DataGridViewRow In GRIDWARP.Rows
                    If symRow.IsNewRow Then Continue For
                    Dim symValue As String = symRow.Cells(WSYM.Index).Value?.ToString()
                    If symValue = CMBGRIDSYM.Text.Trim Then
                        MessageBox.Show("Multiple Sym Not Allowed.")
                        'CMBGRIDSYM.Focus()
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub GETWARPPE()
        ' --- Step 1: Create a dictionary to sum P.E. per Sym from warppattern grid ---
        Dim peSumBySym As New Dictionary(Of String, Double)

        For Each row As DataGridViewRow In GRIDWARPPATTERN.Rows
            If row.IsNewRow Then Continue For
            Dim symVal As String = row.Cells(WPSYM.Index).Value?.ToString()
            Dim peVal As Double = 0
            Double.TryParse(row.Cells(WPTR2.Index).Value?.ToString(), peVal)
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
                row.Cells(WPE.Index).Value = peSumBySym(symVal)
            End If
        Next
        TOTALWARP()
    End Sub

    Sub GETWEFTPE()
        ' --- Step 1: Create a dictionary to sum P.E. per Sym from warppattern grid ---
        Dim peSumBySym As New Dictionary(Of String, Double)

        For Each row As DataGridViewRow In GRIDWEFTPATTERN.Rows
            If row.IsNewRow Then Continue For
            Dim symVal As String = row.Cells(FPSYM.Index).Value?.ToString()
            Dim peVal As Double = 0
            Double.TryParse(row.Cells(FPTR2.Index).Value?.ToString(), peVal)
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
                row.Cells(FPE.Index).Value = peSumBySym(symVal)
            End If
        Next
        TOTALWEFT()
    End Sub
    Sub GETSELPE()
        ' --- Step 1: Create a dictionary to sum P.E. per Sym from warppattern grid ---
        Dim peSumBySym As New Dictionary(Of String, Double)

        For Each row As DataGridViewRow In GRIDSELVEDGEPATTERN.Rows
            If row.IsNewRow Then Continue For
            Dim symVal As String = row.Cells(SPSYM.Index).Value?.ToString()
            Dim peVal As Double = 0
            Double.TryParse(row.Cells(SPENDS.Index).Value?.ToString(), peVal)
            If Not String.IsNullOrWhiteSpace(symVal) Then
                If Not peSumBySym.ContainsKey(symVal) Then
                    peSumBySym(symVal) = 0
                End If
                peSumBySym(symVal) += peVal
            End If
        Next

        ' --- Step 2: Write the sum into the matching Sym row's P.E. cell in the GRIDWARP ---
        For Each row As DataGridViewRow In GRIDSELVEDGE.Rows
            If row.IsNewRow Then Continue For
            Dim symVal As String = row.Cells(SSYM.Index).Value?.ToString()
            If Not String.IsNullOrWhiteSpace(symVal) AndAlso peSumBySym.ContainsKey(symVal) Then
                row.Cells(SPE.Index).Value = peSumBySym(symVal)
            End If
        Next
        TOTALSELVEDGE()

    End Sub
    Private Sub TXTDRAWENDS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTDRAWENDS.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = Convert.ToChar(".") Or e.KeyChar = Convert.ToChar(Keys.Back)) Then
            e.Handled = True
        End If
    End Sub

    Sub FILLDRAWGRID()
        If TXTDRAWENDS.Text.Trim = "" Then
            Exit Sub
        End If
        If GRIDDRAWDOUBLECLICK = False Then
            GRIDDRAWING.Rows.Add(Val(TXTDRAWSRNO.Text.Trim), TXTDRAWENDS.Text.Trim)
            getsrno(GRIDDRAWING)
        ElseIf GRIDDRAWDOUBLECLICK = True Then
            GRIDDRAWING.Item(DSRNO.Index, TEMPDRAWROW).Value = Val(TXTDRAWSRNO.Text.Trim)
            GRIDDRAWING.Item(DENDS.Index, TEMPDRAWROW).Value = TXTDRAWENDS.Text.Trim
            TXTDRAWSRNO.Focus()
            GRIDDRAWDOUBLECLICK = False
        End If
        GRIDDRAWING.ClearSelection()
        TXTDRAWENDS.Clear()
        TXTDRAWENDS.Focus()
        If GRIDDRAWING.RowCount > 0 Then
            TXTDRAWSRNO.Text = Val(GRIDDRAWING.Rows(GRIDDRAWING.RowCount - 1).Cells(0).Value) + 1
        Else
            TXTDRAWSRNO.Text = 1
        End If
        'TOTALDRAWDENTS(GRIDDRAWING)
    End Sub

    Private Sub TXTDRAWENDS_Validated(sender As Object, e As EventArgs) Handles TXTDRAWENDS.Validated
        Try
            FILLDRAWGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GRIDDRAWING_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDDRAWING.CellValidating
        Try
            ' Assume Shaft value is in a control called numShafts (or you can store it in a variable)
            Dim maxShaft As Integer = 0
            If Integer.TryParse(CMBSHAFTS.Text.Trim(), maxShaft) Then
                ' maxShaft will hold the correct integer value
            Else
                MessageBox.Show("Please select a valid shaft number.", "Error")
                Exit Sub
            End If ' or use Integer.Parse(txtShafts.Text)

            ' Check if editing the "Ends" column by column name or index
            If GRIDDRAWING.Columns(e.ColumnIndex).Name = "DENDS" Then
                Dim inputValue As String = e.FormattedValue.ToString().Trim()
                If inputValue <> "" Then
                    Dim nums = inputValue.Split("."c)
                    For Each n In nums
                        Dim value As Integer
                        If Integer.TryParse(n.Trim(), value) Then
                            If value > maxShaft Then
                                MessageBox.Show($"The largest number allowed is {maxShaft}.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                e.Cancel = True
                                Return
                            End If
                        End If
                    Next
                End If
            End If
            If e.ColumnIndex = DREPEAT.Index OrElse e.ColumnIndex = DREPEATS1.Index Then ' For both repeats columns if needed
                Dim value = Convert.ToString(e.FormattedValue)
                If value IsNot Nothing AndAlso value.Trim() <> "" Then
                    Dim repeatCount As Integer
                    If Not Integer.TryParse(value, repeatCount) OrElse repeatCount < 1 Then
                        MessageBox.Show("Please enter a positive Integer For repeats.")
                        e.Cancel = True
                    End If
                End If
            End If
            cmdbtn1_Click(sender, e)
            ' TOTALDRAWDENTS(GRIDDRAWING)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GRIDSELVEDGEPATTERN_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSELVEDGEPATTERN.CellDoubleClick
        EDITSELVEDGEPATTERNROW()
    End Sub

    Private Sub CMBWEFTGRIDSYMBOL_Validated(sender As Object, e As EventArgs) Handles CMBWEFTGRIDSYMBOL.Validated
        Try
            CMBWEFTYARNQUALITY.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMBWARPQUALITY_Validated(sender As Object, e As EventArgs) Handles CMBWARPQUALITY.Validated
        Try
            If CMBWARPQUALITY.Text <> "" Then
                Dim OBJCLS As New ClsCommon()
                Dim DT2 As New DataTable
                DT2 = OBJCLS.SEARCH("ISNULL(YARN_DENIER, 0) As DENIER, ISNULL(MILLMASTER.MILL_NAME, '') As MILLNAME", "", "  YARNQUALITYMASTER LEFT OUTER JOIN MILLMASTER ON YARNQUALITYMASTER.YARN_YEARID = MILLMASTER.MILL_YEARID AND YARNQUALITYMASTER.YARN_MILLID = MILLMASTER.MILL_ID  ", "  And YARN_NAME ='" & CMBWARPQUALITY.Text.Trim & "'  AND YARN_YEARID = " & YearId)
                If DT2.Rows.Count > 0 Then
                    TXTWARPDENIER.Text = DT2.Rows(0).Item("DENIER")
                    CMBWARPMILLNAME.Text = DT2.Rows(0).Item("MILLNAME")
                End If
                CMBWARPMILLNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSELVEDGE_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDSELVEDGE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSELVEDGE.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
LINE1:
                For I As Integer = 0 To DT_SELDETAILS.Rows.Count - 1
                    If GRIDSELVEDGE.Rows(GRIDSELVEDGE.CurrentRow.Index).Cells(SSRNO.Index).Value = Val(DT_SELDETAILS.Rows(I).Item("SDMAINSRNO")) Then
                        DT_SELDETAILS.Rows.RemoveAt(I)
                        GoTo LINE1
                    End If
                Next
                For I As Integer = 0 To DT_SELDETAILS.Rows.Count - 1
                    If GRIDSELVEDGE.Rows(GRIDSELVEDGE.CurrentRow.Index).Cells(SSRNO.Index).Value < Val(DT_SELDETAILS.Rows(I).Item("SDMAINSRNO")) Then
                        DT_SELDETAILS.Rows(I).Item("SDMAINSRNO") = Val(DT_SELDETAILS.Rows(I).Item("SDMAINSRNO")) - 1
                    End If
                Next
                GRIDSELVEDGE.Rows.RemoveAt(GRIDSELVEDGE.CurrentRow.Index)
                TOTALSELVEDGE()
                getsrno(GRIDSELVEDGE)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITSELVEDGEROW()
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDSELVEDGEPATTERN_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDSELVEDGEPATTERN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSELVEDGEPATTERN.RowCount > 0 Then
                If GRIDSELDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDSELVEDGEPATTERN.Rows.RemoveAt(GRIDSELVEDGEPATTERN.CurrentRow.Index)
                TOTALSELVEDGE()
                getsrno(GRIDSELVEDGEPATTERN)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITSELVEDGEPATTERNROW()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWARP_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWARP.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDWARP.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                ' Store SYM value of the current row before deletion
                Dim deletedSym As String = ""
                If GRIDWARP.CurrentRow.Cells("WSYM").Value IsNot Nothing Then
                    deletedSym = GRIDWARP.CurrentRow.Cells("WSYM").Value.ToString().Trim()
                End If



                ' Remove matching SYM rows from small grid GRIDWARPPATTERN
                For i As Integer = GRIDWARPPATTERN.Rows.Count - 1 To 0 Step -1
                    Dim row As DataGridViewRow = GRIDWARPPATTERN.Rows(i)
                    If Not row.IsNewRow AndAlso row.Cells("WPSYM").Value IsNot Nothing Then
                        If row.Cells("WPSYM").Value.ToString().Trim() = deletedSym Then
                            GRIDWARPPATTERN.Rows.RemoveAt(i)
                        End If
                    End If
                Next

LINE1:
                For I As Integer = 0 To DT_WARPDETAILS.Rows.Count - 1
                    If GRIDWARP.Rows(GRIDWARP.CurrentRow.Index).Cells(WSRNO.Index).Value = Val(DT_WARPDETAILS.Rows(I).Item("WDMAINSRNO")) Then
                        DT_WARPDETAILS.Rows.RemoveAt(I)
                        GoTo LINE1
                    End If
                Next
                For I As Integer = 0 To DT_WARPDETAILS.Rows.Count - 1
                    If GRIDWARP.Rows(GRIDWARP.CurrentRow.Index).Cells(WSRNO.Index).Value < Val(DT_WARPDETAILS.Rows(I).Item("WDMAINSRNO")) Then
                        DT_WARPDETAILS.Rows(I).Item("WDMAINSRNO") = Val(DT_WARPDETAILS.Rows(I).Item("WDMAINSRNO")) - 1
                    End If
                Next
                ' Remove row from main grid
                GRIDWARP.Rows.RemoveAt(GRIDWARP.CurrentRow.Index)
                ' Refresh totals and serial numbers
                TOTALWARP()
                TOTALWARPPATTERN()
                getsrno(GRIDWARP)
                getsrno(GRIDWARPPATTERN)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITWARPROW()
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If MsgBox("Wish to Copy Warp Matching Grid?", MsgBoxStyle.YesNo) = vbYes Then
                CopyGridEntries(GRIDWARP, GRIDWEFT)
            ElseIf MsgBox("Wish to Copy Weft Pattern Grid?", MsgBoxStyle.YesNo) = vbYes Then
                CopyGridEntries(GRIDWARPPATTERN, GRIDWEFTPATTERN)
            End If
            'CopyGridEntries(GRIDWEFTPATTERN, GRIDWEFTPATTERNCOPY)
            'CopyGridEntries(GRIDSELVEDGEPATTERN, GRIDSELVEDGEPATTERNCOPY)
            MsgBox("Copied Successfully")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CopyGridEntries(sourceGrid As DataGridView, targetGrid As DataGridView)
        ' Clear existing rows in target if needed
        targetGrid.Rows.Clear()

        ' Loop through each non-new row in source
        For Each srcRow As DataGridViewRow In sourceGrid.Rows
            If Not srcRow.IsNewRow Then
                ' Create a new row in target grid
                Dim targetRowIndex As Integer = targetGrid.Rows.Add()
                Dim targetRow As DataGridViewRow = targetGrid.Rows(targetRowIndex)

                ' Copy cell values from source to target
                For i As Integer = 0 To sourceGrid.Columns.Count - 1
                    targetRow.Cells(i).Value = srcRow.Cells(i).Value
                Next
            End If
        Next
    End Sub


    Private Sub GRIDWARPPATTERN_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWARPPATTERN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDWARPPATTERN.RowCount > 0 Then
                If GRIDWPDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDWARPPATTERN.Rows.RemoveAt(GRIDWARPPATTERN.CurrentRow.Index)
                TOTALWARP()
                getsrno(GRIDWARPPATTERN)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITWARPPATTERNROW()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFT_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWEFT.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDWEFT.RowCount > 0 Then
                If GRIDWEFTDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

LINE1:
                For I As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                    If GRIDWEFT.Rows(GRIDWEFT.CurrentRow.Index).Cells(FSRNO.Index).Value = Val(DT_WEFTDETAILS.Rows(I).Item("FDMAINSRNO")) Then
                        DT_WEFTDETAILS.Rows.RemoveAt(I)
                        GoTo LINE1
                    End If
                Next
                For I As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                    If GRIDWEFT.Rows(GRIDWEFT.CurrentRow.Index).Cells(FSRNO.Index).Value < Val(DT_WEFTDETAILS.Rows(I).Item("FDMAINSRNO")) Then
                        DT_WEFTDETAILS.Rows(I).Item("FDMAINSRNO") = Val(DT_WEFTDETAILS.Rows(I).Item("FDMAINSRNO")) - 1
                    End If
                Next

                GRIDWEFT.Rows.RemoveAt(GRIDWEFT.CurrentRow.Index)

                TOTALWEFT()
                getsrno(GRIDWEFT)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITWEFTROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFTPATTERN_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWEFTPATTERN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDWEFTPATTERN.RowCount > 0 Then
                If GRIDWEFTPDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDWEFTPATTERN.Rows.RemoveAt(GRIDWEFTPATTERN.CurrentRow.Index)
                TOTALWEFT()
                getsrno(GRIDWEFTPATTERN)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITWEFTPATTERNROW()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELGSYM_Validated(sender As Object, e As EventArgs) Handles CMBSELGSYM.Validated
        'Try
        '    If CMBSELGSYM.Text <> "" And TXTSELGPE.Text.Trim <> "" Then
        '        FILLSELPATTERNGRID()
        '        GETSELPE()
        '    Else
        '        MsgBox("Please Enter Symbol and P.E.")
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub CMBSELYARNQUALITY_Validated(sender As Object, e As EventArgs) Handles CMBSELYARNQUALITY.Validated
        Try
            If CMBSELYARNQUALITY.Text <> "" Then
                Dim OBJCLS As New ClsCommon()
                Dim DT2 As New DataTable
                DT2 = OBJCLS.SEARCH("ISNULL(YARN_DENIER, 0) AS DENIER", "", "  YARNQUALITYMASTER  ", "  and YARN_NAME ='" & CMBSELYARNQUALITY.Text.Trim & "'  AND YARN_YEARID = " & YearId)
                If DT2.Rows.Count > 0 Then
                    TXTSELDEN.Text = DT2.Rows(0).Item("DENIER")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Toolprevious_Click(sender As Object, e As EventArgs) Handles Toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor

            GRIDSELVEDGE.RowCount = 0
LINE1:
            'temptypename = cmbtype.Text.Trim
            tempdesignno = Val(txtcardno.Text) - 1
            If tempdesignno > 0 Then
                EDIT = True
                DesignCardMaster_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDSELVEDGE.RowCount = 0 And tempdesignno > 1 Then
                txtcardno.Text = tempdesignno
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
            GRIDSELVEDGE.RowCount = 0
LINE1:
            tempdesignno = Val(txtcardno.Text) + 1
            'temptypename = cmbtype.Text.Trim
            getmaxno()
            Dim MAXNO As Integer = txtcardno.Text.Trim
            clear()
            If Val(txtcardno.Text) - 1 >= tempdesignno Then
                EDIT = True
                DesignCardMaster_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDSELVEDGE.RowCount = 0 And tempdesignno < MAXNO Then
                txtcardno.Text = tempdesignno
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(DESIGN_CARDNO),0) + 1 ", "DESIGNCARD", " AND  DESIGN_CMPID=" & CmpId & " and DESIGN_LOCATIONID=" & Locationid & " and DESIGN_YEARID=" & YearId)
        If DTTABLE.Rows.Count > 0 Then
            txtcardno.Text = DTTABLE.Rows(0).Item(0)
        End If
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then
                PRINTREPORT(tempdesignno)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub PRINTREPORT(ByVal CARDNO As Integer)

        If MsgBox("Wish to Print Design Card ?", MsgBoxStyle.YesNo) = vbYes Then
            Dim OBJCARD As New DesignCardDesign
            OBJCARD.MdiParent = MDIMain
            OBJCARD.FRMSTRING = "DESIGNCARD"
            OBJCARD.FORMULA = "{DESIGNCARD.DESIGN_CARDNO}=" & Val(txtcardno.Text.Trim) & " and {DESIGNCARD.DESIGN_YEARID}=" & YearId
            OBJCARD.Show()
        End If
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        cmdok_Click(sender, e)
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objgrndetails As New DesignCardMasterDetails
            objgrndetails.MdiParent = MDIMain
            objgrndetails.FRMSTRING = FRMSTRING
            objgrndetails.Show()
            objgrndetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TOOLDELETE_Click(sender As Object, e As EventArgs) Handles TOOLDELETE.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try

            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                TEMPMSG = MsgBox("Delete Design Card ?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(txtcardno.Text.Trim)
                    alParaval.Add(CmpId)
                    alParaval.Add(Locationid)
                    alParaval.Add(YearId)

                    Dim Clsgrn As New ClsDesignCardMaster()
                    Clsgrn.alParaval = alParaval
                    IntResult = Clsgrn.Delete()
                    MsgBox("Design Card Deleted")
                    clear()
                    EDIT = False
                End If
            Else
                MsgBox("Delete Is only In Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTYARNQUALITY_Validated(sender As Object, e As EventArgs) Handles CMBWEFTYARNQUALITY.Validated
        Try
            If CMBWEFTYARNQUALITY.Text <> "" Then
                Dim OBJCLS As New ClsCommon()
                Dim DT2 As New DataTable
                DT2 = OBJCLS.SEARCH("ISNULL(YARN_DENIER, 0) AS DENIER,ISNULL(MILLMASTER.MILL_NAME, '') AS MILLNAME", "", "  YARNQUALITYMASTER LEFT OUTER JOIN MILLMASTER ON YARNQUALITYMASTER.YARN_YEARID = MILLMASTER.MILL_YEARID AND YARNQUALITYMASTER.YARN_MILLID = MILLMASTER.MILL_ID  ", "  and YARN_NAME ='" & CMBWEFTYARNQUALITY.Text.Trim & "'  AND YARN_YEARID = " & YearId)
                If DT2.Rows.Count > 0 Then
                    TXTWEFTDEN.Text = DT2.Rows(0).Item("DENIER")
                    CMBWEFTMILLNAME.Text = DT2.Rows(0).Item("MILLNAME")
                End If
                CMBWEFTMILLNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GRIDWEFTPATTERN_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDWEFTPATTERN.CellValidating
        Try
            Dim dgv As DataGridView = CType(sender, DataGridView)

            '' Proceed only if the column being edited is "WPSYM"
            'If dgv.Columns(e.ColumnIndex).Name = "FPSYM" Then
            '    Dim inputValue As String = e.FormattedValue.ToString().Trim()
            '    If inputValue <> "" Then
            '        ' Flag to track if match is found
            '        Dim matchFound As Boolean = False

            '        ' Loop through rows of main grid to check for matching "WSYM" value
            '        For Each row As DataGridViewRow In GRIDWEFT.Rows
            '            If Not row.IsNewRow AndAlso row.Cells("FSYM").Value IsNot Nothing Then
            '                Dim symValue As String = row.Cells("FSYM").Value.ToString().Trim()

            '                If String.Equals(inputValue, symValue, StringComparison.OrdinalIgnoreCase) Then
            '                    matchFound = True
            '                    Exit For
            '                End If
            '            End If
            '        Next

            '        ' If no match found, show warning and cancel editing
            '        If Not matchFound Then
            '            MessageBox.Show("SYM must match a SYM from the main grid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '            e.Cancel = True  ' Cancels the edit
            '        End If
            '    End If
            'End If

            If e.ColumnIndex = FPR.Index OrElse e.ColumnIndex = FPR1.Index Then ' For both repeats columns if needed
                Dim value = Convert.ToString(e.FormattedValue)
                If value IsNot Nothing AndAlso value.Trim() <> "" Then
                    Dim repeatCount As Integer
                    If Not Integer.TryParse(value, repeatCount) OrElse repeatCount < 1 Then
                        MessageBox.Show("Please enter a positive integer for repeats.")
                        e.Cancel = True
                    End If
                End If
            End If
            Button2_Click(sender, e)
            COPYWEFTSYM()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLOSESEL_Click_1(sender As Object, e As EventArgs) Handles CMDCLOSESEL.Click
        Try
            If CMBSELYARNQUALITY.Text.Trim <> "" And TXTSELSYMBOL.Text.Trim <> "" Then
                fillselvedgegrid()
            Else
                MsgBox("Fill Yarn Quality OR Symbol")
            End If
            GBSSHADEDETAILS.Visible = False
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GRIDWARPPATTERN_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDWARPPATTERN.CellValidating
        Try
            Dim dgv As DataGridView = CType(sender, DataGridView)

            '' Proceed only if the column being edited is "WPSYM"
            'If dgv.Columns(e.ColumnIndex).Name = "WPSYM" Then
            '    Dim inputValue As String = e.FormattedValue.ToString().Trim()
            '    If inputValue <> "" Then
            '        ' Flag to track if match is found
            '        Dim matchFound As Boolean = False

            '        ' Loop through rows of main grid to check for matching "WSYM" value
            '        For Each row As DataGridViewRow In GRIDWARP.Rows
            '            If Not row.IsNewRow AndAlso row.Cells("WSYM").Value IsNot Nothing Then
            '                Dim symValue As String = row.Cells("WSYM").Value.ToString().Trim()

            '                If String.Equals(inputValue, symValue, StringComparison.OrdinalIgnoreCase) Then
            '                    matchFound = True
            '                    Exit For
            '                End If
            '            End If
            '        Next

            '        ' If no match found, show warning and cancel editing
            '        If Not matchFound Then
            '            MessageBox.Show("SYM must match a SYM from the main grid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '            e.Cancel = True  ' Cancels the edit
            '        End If
            '    End If
            'End If
            If e.ColumnIndex = WPR.Index OrElse e.ColumnIndex = WPR1.Index Then ' For both repeats columns if needed
                Dim value = Convert.ToString(e.FormattedValue)
                If value IsNot Nothing AndAlso value.Trim() <> "" Then
                    Dim repeatCount As Integer
                    If Not Integer.TryParse(value, repeatCount) OrElse repeatCount < 1 Then
                        MessageBox.Show("Please enter a positive integer for repeats.")
                        e.Cancel = True
                    End If
                End If
            End If
            Button1_Click(sender, e)
            COPYSYM()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub CalculateTotalsForGrid(dgv As DataGridView,
                                      endsCol As String, repeatsCol As String,
                                      repeats1Col As String, repeats2Col As String,
                                      totalRepeatCol As String, totalRepeat1Col As String, totalRepeat2Col As String)
        'For Each row As DataGridViewRow In dgv.Rows
        '    If row.IsNewRow Then Continue For

        '    Dim ends As Integer = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(endsCol).Value)), 1, Convert.ToInt32(row.Cells(endsCol).Value))
        '    Dim repeats As Integer = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeatsCol).Value)), 1, Convert.ToInt32(row.Cells(repeatsCol).Value))
        '    Dim repeats1 As Integer = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeats1Col).Value)), 1, Convert.ToInt32(row.Cells(repeats1Col).Value))
        '    Dim repeats2 As Integer = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeats2Col).Value)), 1, Convert.ToInt32(row.Cells(repeats2Col).Value))

        '    Dim totalRepeat As Integer = ends * repeats
        '    Dim totalRepeat1 As Integer = totalRepeat * repeats1
        '    Dim totalRepeat2 As Integer = totalRepeat1 * repeats2

        '    row.Cells(totalRepeatCol).Value = totalRepeat
        '    row.Cells(totalRepeat1Col).Value = totalRepeat1
        '    row.Cells(totalRepeat2Col).Value = totalRepeat2
        'Next

        ' --- Group State Variables ---
        Dim inGroupParen As Boolean = False, groupStartParen As Integer = -1, repeatValueParen As Integer = 1
        Dim inGroupSquare As Boolean = False, groupStartSquare As Integer = -1, repeatValueSquare As Integer = 1
        Dim inGroupCurly As Boolean = False, groupStartCurly As Integer = -1, repeatValueCurly As Integer = 1
        Dim totalEndsInGroupParen As Integer = 0
        ' --- Main Pass: Assign Repeats Based on Group Patterns ---
        For i As Integer = 0 To dgv.Rows.Count - 1
            If dgv.Rows(i).IsNewRow Then Continue For
            Dim endsVal As String = Convert.ToString(dgv.Rows(i).Cells(endsCol).Value)

            ' ( ) brackets for repeatsCol
            If endsVal.Contains("(") Then inGroupParen = True : groupStartParen = i
            If inGroupParen And endsVal.Contains(")") Then
                totalEndsInGroupParen = ExtractValuesInsideBrackets(groupStartParen, dgv, endsCol)
                Dim match = System.Text.RegularExpressions.Regex.Match(endsVal, "\)(\d+)")
                repeatValueParen = If(match.Success, Convert.ToInt32(match.Groups(1).Value), 1)
                For j As Integer = groupStartParen To i
                    dgv.Rows(j).Cells(repeatsCol).Value = repeatValueParen
                Next
                inGroupParen = False : groupStartParen = -1
            ElseIf Not inGroupParen Then
                dgv.Rows(i).Cells(repeatsCol).Value = 1
            End If

            ' [ ] brackets for repeats1Col
            If endsVal.Contains("[") Then inGroupSquare = True : groupStartSquare = i
            If inGroupSquare And endsVal.Contains("]") Then
                totalEndsInGroupParen = ExtractValuesInsideBrackets(groupStartParen, dgv, endsCol)
                Dim match = System.Text.RegularExpressions.Regex.Match(endsVal, "\](\d+)")
                repeatValueSquare = If(match.Success, Convert.ToInt32(match.Groups(1).Value), 1)
                For j As Integer = groupStartSquare To i
                    dgv.Rows(j).Cells(repeats1Col).Value = repeatValueSquare
                Next
                inGroupSquare = False : groupStartSquare = -1
            ElseIf Not inGroupSquare Then
                dgv.Rows(i).Cells(repeats1Col).Value = 1
            End If

            ' { } brackets for repeats2Col
            If endsVal.Contains("{") Then inGroupCurly = True : groupStartCurly = i
            If inGroupCurly And endsVal.Contains("}") Then
                totalEndsInGroupParen = ExtractValuesInsideBrackets(groupStartParen, dgv, endsCol)
                Dim match = System.Text.RegularExpressions.Regex.Match(endsVal, "\}(\d+)")
                repeatValueCurly = If(match.Success, Convert.ToInt32(match.Groups(1).Value), 1)
                For j As Integer = groupStartCurly To i
                    dgv.Rows(j).Cells(repeats2Col).Value = repeatValueCurly
                Next
                inGroupCurly = False : groupStartCurly = -1
            ElseIf Not inGroupCurly Then
                dgv.Rows(i).Cells(repeats2Col).Value = 1
            End If
        Next

        ' --- Handle any unclosed groups ---
        If inGroupParen And groupStartParen <> -1 Then
            For j As Integer = groupStartParen To dgv.Rows.Count - 1
                If dgv.Rows(j).IsNewRow Then Continue For
                dgv.Rows(j).Cells(repeatsCol).Value = repeatValueParen
            Next
        End If
        If inGroupSquare And groupStartSquare <> -1 Then
            For j As Integer = groupStartSquare To dgv.Rows.Count - 1
                If dgv.Rows(j).IsNewRow Then Continue For
                dgv.Rows(j).Cells(repeats1Col).Value = repeatValueSquare
            Next
        End If
        If inGroupCurly And groupStartCurly <> -1 Then
            For j As Integer = groupStartCurly To dgv.Rows.Count - 1
                If dgv.Rows(j).IsNewRow Then Continue For
                dgv.Rows(j).Cells(repeats2Col).Value = repeatValueCurly
            Next
        End If

        ' --- Final Calculation: Totals using assigned repeats ---
        For Each row As DataGridViewRow In dgv.Rows
            If row.IsNewRow Then Continue For
            Dim endsStr As String = Convert.ToString(row.Cells(endsCol).Value)
            Dim ends As Integer = 1, repeatsFromEnds As Integer = 1
            ExtractEndsAndRepeatation(endsStr, ends, repeatsFromEnds)
            Dim repeats = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeatsCol).Value)), repeatsFromEnds, Convert.ToInt32(row.Cells(repeatsCol).Value))
            Dim repeats1 = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeats1Col).Value)), 1, Convert.ToInt32(row.Cells(repeats1Col).Value))
            Dim repeats2 = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeats2Col).Value)), 1, Convert.ToInt32(row.Cells(repeats2Col).Value))
            Dim totalRepeat = ends * repeats
            Dim totalRepeat1 = totalRepeat * repeats1
            Dim totalRepeat2 = totalRepeat1 * repeats2
            row.Cells(totalRepeatCol).Value = totalRepeat
            row.Cells(totalRepeat1Col).Value = totalRepeat1
            row.Cells(totalRepeat2Col).Value = totalRepeat2
        Next

    End Sub
    Private Function ExtractValuesInsideBrackets(startIndex As Integer, dgv As DataGridView, endsCol As String) As Integer
        Dim valuesList As New List(Of String)
        For i As Integer = startIndex To dgv.Rows.Count - 1
            If i < 0 OrElse i >= dgv.Rows.Count Then Exit For
            If dgv.Rows(i).IsNewRow Then Exit For
            If dgv.Rows(i).Cells(endsCol).Value IsNot Nothing Then
                Dim cellValue As String = dgv.Rows(i).Cells(endsCol).Value.ToString()
                If Not String.IsNullOrWhiteSpace(cellValue) Then
                    valuesList.Add(cellValue)
                    If cellValue.Contains("]") Then Exit For
                End If
            End If
        Next
        Dim joinedValues As String = String.Join("", valuesList).
        Replace("(", "").Replace(")", "").
        Replace("[", "").Replace("]", "").
        Replace("{", "").Replace("}", "")
        If String.IsNullOrWhiteSpace(joinedValues) Then
            Return 0
        End If
        Dim splitVals As String() = joinedValues.Split("."c)
        Return splitVals.Count(Function(x) Not String.IsNullOrWhiteSpace(x))
    End Function

    Private Sub ExtractEndsAndRepeatation(input As String, ByRef endsValue As Integer, ByRef repeatationValue As Integer)
        endsValue = 1
        repeatationValue = 1
        If String.IsNullOrWhiteSpace(input) Then
            endsValue = 0
            repeatationValue = 1
            Return
        End If
        ' Extract repeatation: number after closing bracket, e.g. )5 or ]5 or }5
        Dim repeatMatch As Match = Regex.Match(input, "[)\]\}]\s*(\d+)")
        If repeatMatch.Success Then
            Integer.TryParse(repeatMatch.Groups(1).Value, repeatationValue)
        End If
        ' Extract count of dot-separated numbers BEFORE the closing bracket
        Dim beforeCloseBracket As String = input.Split({")", "]", "}"}, StringSplitOptions.None)(0)
        Dim core As String = beforeCloseBracket.Replace("(", "").Replace("[", "").Replace("{", "")
        If String.IsNullOrWhiteSpace(core) Then
            endsValue = 0
            Return
        End If
        Dim valsInside As String() = core.Split("."c)
        ' If any part is exactly "0", endsValue should be 0 and exit
        If valsInside.Any(Function(x) x.Trim() = "0") Then
            endsValue = 0
        Else
            endsValue = valsInside.Count(Function(x) Not String.IsNullOrWhiteSpace(x))
        End If
    End Sub

    Public Function CalculateTotalDents(dgv As DataGridView,
                                      endsCol As String, repeatsCol As String,
                                      repeats1Col As String, repeats2Col As String,
                                      totalRepeatCol As String, totalRepeat1Col As String, totalRepeat2Col As String)
        Try
            For Each row As DataGridViewRow In dgv.Rows
                If row.IsNewRow Then Continue For
                Dim endsStr As String = Convert.ToString(row.Cells(endsCol).Value)
                Dim ends As Integer = 1, repeatsFromEnds As Integer = 1
                ExtractEndsAndRepeatation(endsStr, ends, repeatsFromEnds)
                Dim repeats = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeatsCol).Value)), repeatsFromEnds, Convert.ToInt32(row.Cells(repeatsCol).Value))
                Dim repeats1 = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeats1Col).Value)), 1, Convert.ToInt32(row.Cells(repeats1Col).Value))
                Dim repeats2 = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeats2Col).Value)), 1, Convert.ToInt32(row.Cells(repeats2Col).Value))
                Dim totalRepeat = 1 * repeats
                Dim totalRepeat1 = totalRepeat * repeats1
                Dim totalRepeat2 = totalRepeat1 * repeats2
                row.Cells(totalRepeatCol).Value = totalRepeat
                row.Cells(totalRepeat1Col).Value = totalRepeat1
                row.Cells(totalRepeat2Col).Value = totalRepeat2
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmdbtn1_Click(sender As Object, e As EventArgs) Handles cmdbtn1.Click
        CalculateTotalsForGrid(GRIDDRAWING, "DENDS", "DREPEAT", "DREPEATS1", "DREPEATS2", "DTOTALREPEAT", "DTOTALREPEAT1", "DTOTALREPEAT2")

        Dim totalDentsCount As Integer = CalculateTotalDents(GRIDDRAWING, "DENDS", "DREPEAT", "DREPEATS1", "DREPEATS2", "DTOTALDENTREPEAT", "DTOTALDENTREPEAT1", "DTOTALDENTREPEAT2")
        TXTTOTALDRAWDENTS.Text = totalDentsCount.ToString()  ' Set total dents from function

        ' Reset TextBoxes before summing to avoid accumulation
        TXTTOTALDRAWENDS.Text = "0"
        TXTTOTALDRAWDENTS.Text = totalDentsCount.ToString()  ' Or keep/reset accordingly

        For Each row As DataGridViewRow In GRIDDRAWING.Rows
            If row.IsNewRow Then Continue For

            Dim totalRepeat2Val = If(IsDBNull(row.Cells("DTOTALREPEAT2").Value), 0, Convert.ToDecimal(row.Cells("DTOTALREPEAT2").Value))
            TXTTOTALDRAWENDS.Text = (Convert.ToDecimal(TXTTOTALDRAWENDS.Text) + totalRepeat2Val).ToString()

            Dim totalDentRepeat2Val = If(IsDBNull(row.Cells("DTOTALDENTREPEAT2").Value), 0, Convert.ToDecimal(row.Cells("DTOTALDENTREPEAT2").Value))
            TXTTOTALDRAWDENTS.Text = (Convert.ToDecimal(TXTTOTALDRAWDENTS.Text) + totalDentRepeat2Val).ToString()
        Next
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'CalculateTotalsForGrid(GRIDWARPPATTERN, "WPENDS", "WPR", "WPR1", "WPR2", "WPTR", "WPTR1", "WPTR2")
        TOTALWARPPATTERN()
        TOTALWARP()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'CalculateTotalsForGrid(GRIDWEFTPATTERN, "FPENDS", "FPR", "FPR1", "FPR2", "FPTR", "FPTR1", "FPTR2")
        TOTALWEFTPATTERN()
        TOTALWEFT()
    End Sub

    Private Sub GRIDDRAWING_DefaultValuesNeeded(ByVal sender As Object, ByVal e As DataGridViewRowEventArgs) Handles GRIDDRAWING.DefaultValuesNeeded
        e.Row.Cells("DSRNO").Value = GRIDDRAWING.Rows.Count
    End Sub

    Private Sub GRIDSELVEDGEPATTERN_DefaultValuesNeeded(sender As Object, e As DataGridViewRowEventArgs) Handles GRIDSELVEDGEPATTERN.DefaultValuesNeeded
        e.Row.Cells("SPSRNO").Value = GRIDSELVEDGEPATTERN.Rows.Count
    End Sub

    Private Sub GRIDWARPPATTERN_DefaultValuesNeeded(sender As Object, e As DataGridViewRowEventArgs) Handles GRIDWARPPATTERN.DefaultValuesNeeded
        e.Row.Cells("WPSRNO").Value = GRIDWARPPATTERN.Rows.Count
    End Sub

    Private Sub GRIDWEFTPATTERN_DefaultValuesNeeded(sender As Object, e As DataGridViewRowEventArgs) Handles GRIDWEFTPATTERN.DefaultValuesNeeded
        e.Row.Cells("FPSRNO").Value = GRIDWEFTPATTERN.Rows.Count
    End Sub


    Private Sub GRIDSELVEDGEPATTERN_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDSELVEDGEPATTERN.CellValidating
        Try
            Dim dgv As DataGridView = CType(sender, DataGridView)

            ' Proceed only if the column being edited is "WPSYM"
            If dgv.Columns(e.ColumnIndex).Name = "SPSYM" Then
                Dim inputValue As String = e.FormattedValue.ToString().Trim()
                If inputValue <> "" Then
                    ' Flag to track if match is found
                    Dim matchFound As Boolean = False

                    ' Loop through rows of main grid to check for matching "WSYM" value
                    For Each row As DataGridViewRow In GRIDSELVEDGE.Rows
                        If Not row.IsNewRow AndAlso row.Cells("SSYM").Value IsNot Nothing Then
                            Dim symValue As String = row.Cells("SSYM").Value.ToString().Trim()

                            If String.Equals(inputValue, symValue, StringComparison.OrdinalIgnoreCase) Then
                                matchFound = True
                                Exit For
                            End If
                        End If
                    Next

                    ' If no match found, show warning and cancel editing
                    If Not matchFound Then
                        MessageBox.Show("SYM must match a SYM from the main grid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        e.Cancel = True  ' Cancels the edit
                    End If
                End If
            End If
            'If e.ColumnIndex = SPR.Index OrElse e.ColumnIndex = SPR1.Index Then ' For both repeats columns if needed
            '    Dim value = Convert.ToString(e.FormattedValue)
            '    If value IsNot Nothing AndAlso value.Trim() <> "" Then
            '        Dim repeatCount As Integer
            '        If Not Integer.TryParse(value, repeatCount) OrElse repeatCount < 1 Then
            '            MessageBox.Show("Please enter a positive integer for repeats.")
            '            e.Cancel = True
            '        End If
            '    End If
            'End If
            TOTALSELVEDGEPATTERN()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub CalculateTotalsForGridPATTERN(dgv As DataGridView,
                                    endsCol As String, repeatsCol As String,
                                    repeats1Col As String, repeats2Col As String,
                                    totalRepeatCol As String, totalRepeat1Col As String, totalRepeat2Col As String)
        'For Each row As DataGridViewRow In dgv.Rows
        '    If row.IsNewRow Then Continue For

        '    Dim ends As Integer = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(endsCol).Value)), 1, Convert.ToInt32(row.Cells(endsCol).Value))
        '    Dim repeats As Integer = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeatsCol).Value)), 1, Convert.ToInt32(row.Cells(repeatsCol).Value))
        '    Dim repeats1 As Integer = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeats1Col).Value)), 1, Convert.ToInt32(row.Cells(repeats1Col).Value))
        '    Dim repeats2 As Integer = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeats2Col).Value)), 1, Convert.ToInt32(row.Cells(repeats2Col).Value))

        '    Dim totalRepeat As Integer = ends * repeats
        '    Dim totalRepeat1 As Integer = totalRepeat * repeats1
        '    Dim totalRepeat2 As Integer = totalRepeat1 * repeats2

        '    row.Cells(totalRepeatCol).Value = totalRepeat
        '    row.Cells(totalRepeat1Col).Value = totalRepeat1
        '    row.Cells(totalRepeat2Col).Value = totalRepeat2
        'Next

        ' --- Group State Variables ---
        Dim inGroupParen As Boolean = False, groupStartParen As Integer = -1, repeatValueParen As Integer = 1
        Dim inGroupSquare As Boolean = False, groupStartSquare As Integer = -1, repeatValueSquare As Integer = 1
        Dim inGroupCurly As Boolean = False, groupStartCurly As Integer = -1, repeatValueCurly As Integer = 1
        Dim totalEndsInGroupParen As Integer = 0
        ' --- Main Pass: Assign Repeats Based on Group Patterns ---
        For i As Integer = 0 To dgv.Rows.Count - 1
            If dgv.Rows(i).IsNewRow Then Continue For
            Dim endsVal As String = Convert.ToString(dgv.Rows(i).Cells(endsCol).Value)

            ' ( ) brackets for repeatsCol
            If endsVal.Contains("(") Then inGroupParen = True : groupStartParen = i
            If inGroupParen And endsVal.Contains(")") Then
                totalEndsInGroupParen = ExtractValuesInsideBrackets(groupStartParen, dgv, endsCol)
                Dim match = System.Text.RegularExpressions.Regex.Match(endsVal, "\)(\d+)")
                repeatValueParen = If(match.Success, Convert.ToInt32(match.Groups(1).Value), 1)
                For j As Integer = groupStartParen To i
                    dgv.Rows(j).Cells(repeatsCol).Value = repeatValueParen
                Next
                inGroupParen = False : groupStartParen = -1
            ElseIf Not inGroupParen Then
                dgv.Rows(i).Cells(repeatsCol).Value = 1
            End If

            ' [ ] brackets for repeats1Col
            If endsVal.Contains("[") Then inGroupSquare = True : groupStartSquare = i
            If inGroupSquare And endsVal.Contains("]") Then
                totalEndsInGroupParen = ExtractValuesInsideBrackets(groupStartParen, dgv, endsCol)
                Dim match = System.Text.RegularExpressions.Regex.Match(endsVal, "\](\d+)")
                repeatValueSquare = If(match.Success, Convert.ToInt32(match.Groups(1).Value), 1)
                For j As Integer = groupStartSquare To i
                    dgv.Rows(j).Cells(repeats1Col).Value = repeatValueSquare
                Next
                inGroupSquare = False : groupStartSquare = -1
            ElseIf Not inGroupSquare Then
                dgv.Rows(i).Cells(repeats1Col).Value = 1
            End If

            ' { } brackets for repeats2Col
            If endsVal.Contains("{") Then inGroupCurly = True : groupStartCurly = i
            If inGroupCurly And endsVal.Contains("}") Then
                totalEndsInGroupParen = ExtractValuesInsideBrackets(groupStartParen, dgv, endsCol)
                Dim match = System.Text.RegularExpressions.Regex.Match(endsVal, "\}(\d+)")
                repeatValueCurly = If(match.Success, Convert.ToInt32(match.Groups(1).Value), 1)
                For j As Integer = groupStartCurly To i
                    dgv.Rows(j).Cells(repeats2Col).Value = repeatValueCurly
                Next
                inGroupCurly = False : groupStartCurly = -1
            ElseIf Not inGroupCurly Then
                dgv.Rows(i).Cells(repeats2Col).Value = 1
            End If
        Next

        ' --- Handle any unclosed groups ---
        If inGroupParen And groupStartParen <> -1 Then
            For j As Integer = groupStartParen To dgv.Rows.Count - 1
                If dgv.Rows(j).IsNewRow Then Continue For
                dgv.Rows(j).Cells(repeatsCol).Value = repeatValueParen
            Next
        End If
        If inGroupSquare And groupStartSquare <> -1 Then
            For j As Integer = groupStartSquare To dgv.Rows.Count - 1
                If dgv.Rows(j).IsNewRow Then Continue For
                dgv.Rows(j).Cells(repeats1Col).Value = repeatValueSquare
            Next
        End If
        If inGroupCurly And groupStartCurly <> -1 Then
            For j As Integer = groupStartCurly To dgv.Rows.Count - 1
                If dgv.Rows(j).IsNewRow Then Continue For
                dgv.Rows(j).Cells(repeats2Col).Value = repeatValueCurly
            Next
        End If

        ' --- Final Calculation: Totals using assigned repeats ---
        For Each row As DataGridViewRow In dgv.Rows
            If row.IsNewRow Then Continue For
            Dim endsStr As String = Convert.ToString(row.Cells(endsCol).Value)
            Dim ends As Integer = 1, repeatsFromEnds As Integer = 1
            ExtractEndsAndRepeatationPATTERN(endsStr, ends, repeatsFromEnds)
            Dim repeats = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeatsCol).Value)), repeatsFromEnds, Convert.ToInt32(row.Cells(repeatsCol).Value))
            Dim repeats1 = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeats1Col).Value)), 1, Convert.ToInt32(row.Cells(repeats1Col).Value))
            Dim repeats2 = If(String.IsNullOrWhiteSpace(Convert.ToString(row.Cells(repeats2Col).Value)), 1, Convert.ToInt32(row.Cells(repeats2Col).Value))
            Dim totalRepeat = ends * repeats
            Dim totalRepeat1 = totalRepeat * repeats1
            Dim totalRepeat2 = totalRepeat1 * repeats2
            row.Cells(totalRepeatCol).Value = totalRepeat
            row.Cells(totalRepeat1Col).Value = totalRepeat1
            row.Cells(totalRepeat2Col).Value = totalRepeat2
        Next

    End Sub
    Private Sub ExtractEndsAndRepeatationPATTERN(input As String, ByRef endsValue As Integer, ByRef repeatationValue As Integer)
        endsValue = 1
        repeatationValue = 1
        If String.IsNullOrWhiteSpace(input) Then
            endsValue = 0
            repeatationValue = 1
            Return
        End If
        ' Extract repeatation: number after closing bracket, e.g. )5 or ]5 or }5
        Dim repeatMatch As Match = Regex.Match(input, "[)\]\}]\s*(\d+)")
        If repeatMatch.Success Then
            Integer.TryParse(repeatMatch.Groups(1).Value, repeatationValue)
        End If
        ' Extract count of dot-separated numbers BEFORE the closing bracket
        Dim beforeCloseBracket As String = input.Split({")", "]", "}"}, StringSplitOptions.None)(0)
        Dim core As String = beforeCloseBracket.Replace("(", "").Replace("[", "").Replace("{", "")
        If String.IsNullOrWhiteSpace(core) Then
            endsValue = 0
            Return
        End If
        Dim valsInside As String() = core.Split("."c)
        endsValue = valsInside.Where(Function(x) Not String.IsNullOrWhiteSpace(x)).
                    Select(Function(x)
                               Dim v As Integer = 0
                               Integer.TryParse(x, v)
                               Return v
                           End Function).Sum()

    End Sub

    Private Sub CMBSELMILLNAME_Validated(sender As Object, e As EventArgs) Handles CMBSELMILLNAME.Validated
        GBSSHADEDETAILS.Visible = True


        If GRIDSELDOUBLECLICK = False Then
            'TEMPDTMTRS.Clear()
            GRIDSELDESC.RowCount = 0
            GRIDSELDESCDOUBLECLICK = False
            'Dim i As Integer = 0
            'While i < TEMPDTMTRS.Rows.Count
            '    If TEMPDTMTRS.Rows(i).Item("SDMAINSRNO") = Val(txtsrno.Text.Trim) Then
            '        TEMPDTMTRS.Rows.RemoveAt(i)
            '        'GRIDMTRS.Rows.RemoveAt(GRIDMTRS.CurrentRow.Index)
            '    Else
            '        i += 1 ' Only increment if no row is removed
            '    End If
            'End While
        Else
            If GRIDSELVEDGE.Rows.Count > 0 Then
                GRIDSELDESC.RowCount = 0
                GRIDSELDESCDOUBLECLICK = False
                For i As Integer = 0 To DT_SELDETAILS.Rows.Count - 1
                    If DT_SELDETAILS.Rows(i).Item("SDMAINSRNO") = Val(GRIDSELVEDGE.CurrentRow.Cells(SSRNO.Index).Value) Then
                        GRIDSELDESC.Rows.Add(DT_SELDETAILS.Rows(i).Item("SDSRNO"), DT_SELDETAILS.Rows(i).Item("SDSHADE"), DT_SELDETAILS.Rows(i).Item("SDMAINSRNO"))
                    End If
                Next
            End If
        End If
        TXTSDNO.Text = GRIDSELDESC.RowCount + 1
        CMBSELSHADE.Focus()
    End Sub
    Private Sub TXTREED_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTREED.KeyPress
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLOSESEL_Validated(sender As Object, e As EventArgs) Handles CMDCLOSESEL.Validated
        'GBSSHADEDETAILS.Visible = False
        'TXTSELBE.Focus()
    End Sub

    Private Sub GRIDSELDESC_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDSELDESC.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim del As Boolean = False
                If GRIDSELDESC.RowCount > 0 Then
                    Dim row As Integer = GRIDSELVEDGE.Rows(GRIDSELVEDGE.CurrentRow.Index).Cells(SSRNO.Index).Value
                    For I As Integer = 0 To DT_SELDETAILS.Rows.Count - 1
                        If GRIDSELVEDGE.Rows(GRIDSELVEDGE.CurrentRow.Index).Cells(SSRNO.Index).Value = Val(DT_SELDETAILS.Rows(I).Item("SDMAINSRNO")) And GRIDSELDESC.Rows(GRIDSELDESC.CurrentRow.Index).Cells(SDSRNO.Index).Value = Val(DT_SELDETAILS.Rows(I).Item("SDSRNO")) Then
                            If del = False Then
                                DT_SELDETAILS.Rows.RemoveAt(I)
                                GRIDSELDESC.Rows.RemoveAt(GRIDSELDESC.CurrentRow.Index)
                                del = True
                                GoTo line1
                            End If
                        End If
                    Next
line1:
                    For I As Integer = 0 To DT_SELDETAILS.Rows.Count - 1
                        If GRIDSELVEDGE.Rows(GRIDSELVEDGE.CurrentRow.Index).Cells(SSRNO.Index).Value = Val(DT_SELDETAILS.Rows(I).Item("SDMAINSRNO")) And del = True And row < Val(DT_SELDETAILS.Rows(I).Item(SSRNO.Index)) Then
                            DT_SELDETAILS.Rows(I).Item("SDSRNO") = Val(DT_SELDETAILS.Rows(I).Item("SDSRNO")) - 1
                        End If
                    Next
                    getsrno(GRIDSELDESC)
                    TXTSDNO.Text = GRIDSELDESC.RowCount + 1
                    'CMBSELSHADE.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLGRIDSELDESC()
        Try
            If GRIDSELDESCDOUBLECLICK = False Then
                GRIDSELDESC.Rows.Add(Val(TXTSDNO.Text.Trim), CMBSELSHADE.Text.Trim, Val(TXTSELSRNO.Text.Trim))
                getsrno(GRIDSELDESC)
            ElseIf GRIDSELDESCDOUBLECLICK = True Then
                GRIDSELDESC.Item(DSRNO.Index, TEMPSELDESCROW).Value = Val(TXTSDNO.Text.Trim)
                GRIDSELDESC.Item(SDSHADE.Index, TEMPSELDESCROW).Value = CMBSELSHADE.Text.Trim
                GRIDSELDESC.Item(SDMAINSRNO.Index, TEMPSELDESCROW).Value = Val(TXTSELSRNO.Text.Trim)
                GRIDSELDESCDOUBLECLICK = False
            End If
            TXTSDMAINSRNO.Clear()
            CMBSELSHADE.Text = ""
            CMBSELSHADE.Focus()
            TXTSDNO.Text = GRIDSELDESC.RowCount + 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELSHADE_Validated(sender As Object, e As EventArgs) Handles CMBSELSHADE.Validated
        If CMBSELSHADE.Text <> "" Then FILLGRIDSELDESC() Else CMDCLOSESEL.Focus()
    End Sub
    Sub EDITGRIDSELDESCROW()

        Try
            If GRIDSELDESC.CurrentRow IsNot Nothing Then
                TEMPSELDESCROW = GRIDSELDESC.CurrentRow.Index
                TXTSDNO.Text = GRIDSELDESC.Item(SDSRNO.Index, TEMPSELDESCROW).Value.ToString()
                CMBSELSHADE.Text = GRIDSELDESC.Item(SDSHADE.Index, TEMPSELDESCROW).Value.ToString()
                TXTSDMAINSRNO.Text = GRIDSELDESC.Item(SDMAINSRNO.Index, TEMPSELDESCROW).Value.ToString()
                GRIDSELDESCDOUBLECLICK = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSELDESC_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSELDESC.CellDoubleClick
        Try
            EDITGRIDSELDESCROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub GRIDTEMPVIEW(mainGrid As DataGridView,
    DataTable As DataTable,
    mainSrnoColName As String,
    dataSrnoColName As String,
    dataShadeColName As String,
    dataMainSrnoColName As String,
    Optional rowNo As Integer = -1
)
        Try
            GBSELVIEW.Visible = True
            If mainGrid.Rows.Count > 0 Then
                If rowNo = -1 Then rowNo = mainGrid.CurrentRow.Index
                GRIDSELVIEWS.Rows.Clear()

                Dim mainSrnoValue As Integer = Val(mainGrid.Rows(rowNo).Cells(mainSrnoColName).Value)
                For i As Integer = 0 To DataTable.Rows.Count - 1
                    If Val(DataTable.Rows(i).Item(dataMainSrnoColName)) = mainSrnoValue Then
                        GRIDSELVIEWS.Rows.Add(
                        DataTable.Rows(i).Item(dataSrnoColName),
                        DataTable.Rows(i).Item(dataShadeColName),
                        DataTable.Rows(i).Item(dataMainSrnoColName)
                    )
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GRIDSELVEDGE_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSELVEDGE.RowEnter
        'Try
        '    If GRIDSELVEDGE.RowCount > 0 Then GRIDTEMPVIEW(GRIDSELVEDGE, DT_SELDETAILS, "SSRNO", "SDSRNO", "SDSHADE", "SDMAINSRNO")
        'Catch ex As Exception
        '    Throw ex
        'End Try

    End Sub


    Private Sub CMBWARPMILLNAME_Validated(sender As Object, e As EventArgs) Handles CMBWARPMILLNAME.Validated
        Try
            GBWARP.Visible = True


            If GRIDDOUBLECLICK = False Then
                'TEMPDTMTRS.Clear()
                GRIDWARPDESC.RowCount = 0
                GRIDWARPDESCDOUBLECLICK = False

            Else
                If GRIDWARP.Rows.Count > 0 Then
                    GRIDWARPDESC.RowCount = 0
                    GRIDWARPDESCDOUBLECLICK = False
                    For i As Integer = 0 To DT_WARPDETAILS.Rows.Count - 1
                        If DT_WARPDETAILS.Rows(i).Item("WDMAINSRNO") = Val(GRIDWARP.CurrentRow.Cells(WSRNO.Index).Value) Then
                            GRIDWARPDESC.Rows.Add(DT_WARPDETAILS.Rows(i).Item("WDSRNO"), DT_WARPDETAILS.Rows(i).Item("WDSHADE"), DT_WARPDETAILS.Rows(i).Item("WDMAINSRNO"))
                        End If
                    Next
                End If
            End If
            TXTSDNO.Text = GRIDWARPDESC.RowCount + 1
            CMBWARPSHADE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GRIDWARPDESC_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWARPDESC.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim del As Boolean = False
                If GRIDWARPDESC.RowCount > 0 Then
                    Dim row As Integer = GRIDWARP.Rows(GRIDWARP.CurrentRow.Index).Cells(WSRNO.Index).Value
                    For I As Integer = 0 To DT_WARPDETAILS.Rows.Count - 1
                        If GRIDWARP.Rows(GRIDWARP.CurrentRow.Index).Cells(WSRNO.Index).Value = Val(DT_WARPDETAILS.Rows(I).Item("WDMAINSRNO")) And GRIDWARPDESC.Rows(GRIDWARPDESC.CurrentRow.Index).Cells(WDSRNO.Index).Value = Val(DT_WARPDETAILS.Rows(I).Item("WDSRNO")) Then
                            If del = False Then
                                DT_WARPDETAILS.Rows.RemoveAt(I)
                                GRIDWARPDESC.Rows.RemoveAt(GRIDWARPDESC.CurrentRow.Index)
                                del = True
                                GoTo line1
                            End If
                        End If
                    Next
line1:
                    For I As Integer = 0 To DT_WARPDETAILS.Rows.Count - 1
                        If GRIDWARP.Rows(GRIDWARP.CurrentRow.Index).Cells(WSRNO.Index).Value = Val(DT_WARPDETAILS.Rows(I).Item("WDMAINSRNO")) And del = True And row < Val(DT_WARPDETAILS.Rows(I).Item(WSRNO.Index)) Then
                            DT_WARPDETAILS.Rows(I).Item("WDSRNO") = Val(DT_WARPDETAILS.Rows(I).Item("WDSRNO")) - 1
                        End If
                    Next
                    getsrno(GRIDWARPDESC)
                    TXTWDSRNO.Text = GRIDWARPDESC.RowCount + 1
                    'CMBWARPSHADE.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLGRIDWARPDESC()
        Try
            If GRIDWARPDESCDOUBLECLICK = False Then
                GRIDWARPDESC.Rows.Add(Val(TXTWDSRNO.Text.Trim), CMBWARPSHADE.Text.Trim, Val(TXTWARPSRNO.Text.Trim))
                getsrno(GRIDWARPDESC)
            ElseIf GRIDWARPDESCDOUBLECLICK = True Then
                GRIDWARPDESC.Item(WDSRNO.Index, TEMPWARPDESCROW).Value = Val(TXTWDSRNO.Text.Trim)
                GRIDWARPDESC.Item(WDSHADE.Index, TEMPWARPDESCROW).Value = CMBWARPSHADE.Text.Trim
                GRIDWARPDESC.Item(WDMAINSRNO.Index, TEMPWARPDESCROW).Value = Val(TXTWARPSRNO.Text.Trim)
                GRIDWARPDESCDOUBLECLICK = False
            End If
            TXTWDMAINSRNO.Clear()
            CMBWARPSHADE.Text = ""
            CMBWARPSHADE.Focus()
            TXTWDSRNO.Text = GRIDWARPDESC.RowCount + 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FILLGRIDWEFTDESC()
        Try
            If GRIDWEFTDESCDOUBLECLICK = False Then
                GRIDWEFTDESC.Rows.Add(Val(TXTFDSRNO.Text.Trim), cmbweftshade.Text.Trim, Val(TXTWEFTSRNO.Text.Trim))
                getsrno(GRIDWEFTDESC)
            ElseIf GRIDWEFTDESCDOUBLECLICK = True Then
                GRIDWEFTDESC.Item(FDSRNO.Index, TEMPWEFTDESCROW).Value = Val(TXTFDSRNO.Text.Trim)
                GRIDWEFTDESC.Item(FDSHADE.Index, TEMPWEFTDESCROW).Value = cmbweftshade.Text.Trim
                GRIDWEFTDESC.Item(FDMAINSRNO.Index, TEMPWEFTDESCROW).Value = Val(TXTWEFTSRNO.Text.Trim)
                GRIDWEFTDESCDOUBLECLICK = False
            End If
            TXTFDMAINSRNO.Clear()
            cmbweftshade.Text = ""
            cmbweftshade.Focus()
            TXTWDSRNO.Text = GRIDWEFTDESC.RowCount + 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPSHADE_Validated(sender As Object, e As EventArgs) Handles CMBWARPSHADE.Validated
        If CMBWARPSHADE.Text <> "" Then FILLGRIDWARPDESC() Else CMDWARPCLOSE.Focus()
    End Sub
    Sub EDITGRIDWARPDESCROW()

        Try
            If GRIDWARPDESC.CurrentRow IsNot Nothing Then
                TEMPWARPDESCROW = GRIDWARPDESC.CurrentRow.Index
                TXTWDSRNO.Text = GRIDWARPDESC.Item(WDSRNO.Index, TEMPWARPDESCROW).Value.ToString()
                CMBWARPSHADE.Text = GRIDWARPDESC.Item(WDSHADE.Index, TEMPWARPDESCROW).Value.ToString()
                TXTWDMAINSRNO.Text = GRIDWARPDESC.Item(WDMAINSRNO.Index, TEMPWARPDESCROW).Value.ToString()
                GRIDWARPDESCDOUBLECLICK = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWARPDESC_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWARPDESC.CellDoubleClick
        Try
            EDITGRIDWARPDESCROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GRIDSELVEDGE_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSELVEDGE.CellClick
        Try
            If GRIDSELVEDGE.RowCount > 0 Then GRIDTEMPVIEW(GRIDSELVEDGE, DT_SELDETAILS, "SSRNO", "SDSRNO", "SDSHADE", "SDMAINSRNO")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWARP_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWARP.CellClick
        Try
            If GRIDWARP.RowCount > 0 Then GRIDTEMPVIEW(GRIDWARP, DT_WARPDETAILS, "WSRNO", "WDSRNO", "WDSHADE", "WDMAINSRNO")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTMILLNAME_Validated(sender As Object, e As EventArgs) Handles CMBWEFTMILLNAME.Validated
        Try
            GBWEFT.Visible = True
            If GRIDWEFTDOUBLECLICK = False Then
                'TEMPDTMTRS.Clear()
                GRIDWEFTDESC.RowCount = 0
                GRIDWEFTDESCDOUBLECLICK = False
            Else
                If GRIDWEFT.Rows.Count > 0 Then
                    GRIDWEFTDESC.RowCount = 0
                    GRIDWEFTDESCDOUBLECLICK = False
                    For i As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                        If DT_WEFTDETAILS.Rows(i).Item("FDMAINSRNO") = Val(GRIDWEFT.CurrentRow.Cells(FSRNO.Index).Value) Then
                            GRIDWEFTDESC.Rows.Add(DT_WEFTDETAILS.Rows(i).Item("FDSRNO"), DT_WEFTDETAILS.Rows(i).Item("FDSHADE"), DT_WEFTDETAILS.Rows(i).Item("FDMAINSRNO"))
                        End If
                    Next
                End If
            End If
            TXTFDSRNO.Text = GRIDWEFTDESC.RowCount + 1
            cmbweftshade.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFTDESC_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWEFTDESC.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim del As Boolean = False
                If GRIDWEFTDESC.RowCount > 0 Then
                    Dim row As Integer = GRIDWEFT.Rows(GRIDWEFT.CurrentRow.Index).Cells(FSRNO.Index).Value
                    For I As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                        If GRIDWEFT.Rows(GRIDWEFT.CurrentRow.Index).Cells(FSRNO.Index).Value = Val(DT_WEFTDETAILS.Rows(I).Item("FDMAINSRNO")) And GRIDWEFTDESC.Rows(GRIDWEFTDESC.CurrentRow.Index).Cells(FDSRNO.Index).Value = Val(DT_WEFTDETAILS.Rows(I).Item("FDSRNO")) Then
                            If del = False Then
                                DT_WEFTDETAILS.Rows.RemoveAt(I)
                                GRIDWEFTDESC.Rows.RemoveAt(GRIDWEFTDESC.CurrentRow.Index)
                                del = True
                                GoTo line1
                            End If
                        End If
                    Next
line1:
                    For I As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
                        If GRIDWEFT.Rows(GRIDWEFT.CurrentRow.Index).Cells(FSRNO.Index).Value = Val(DT_WEFTDETAILS.Rows(I).Item("FDMAINSRNO")) And del = True And row < Val(DT_WEFTDETAILS.Rows(I).Item(FSRNO.Index)) Then
                            DT_WEFTDETAILS.Rows(I).Item("FDSRNO") = Val(DT_WEFTDETAILS.Rows(I).Item("FDSRNO")) - 1
                        End If
                    Next
                    getsrno(GRIDWEFTDESC)
                    TXTFDSRNO.Text = GRIDWEFTDESC.RowCount + 1
                    'CMBWARPSHADE.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbweftshade_Validated(sender As Object, e As EventArgs) Handles cmbweftshade.Validated
        If cmbweftshade.Text <> "" Then FILLGRIDWEFTDESC() Else CMDWEFTCLOSE.Focus()
    End Sub
    Sub EDITGRIDWEFTDESCROW()
        Try
            If GRIDWEFTDESC.CurrentRow IsNot Nothing Then
                TEMPWEFTDESCROW = GRIDWEFTDESC.CurrentRow.Index
                TXTFDSRNO.Text = GRIDWEFTDESC.Item(FDSRNO.Index, TEMPWEFTDESCROW).Value.ToString()
                cmbweftshade.Text = GRIDWEFTDESC.Item(FDSHADE.Index, TEMPWEFTDESCROW).Value.ToString()
                TXTFDMAINSRNO.Text = GRIDWEFTDESC.Item(FDMAINSRNO.Index, TEMPWEFTDESCROW).Value.ToString()
                GRIDWEFTDESCDOUBLECLICK = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFTDESC_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWEFTDESC.CellDoubleClick
        Try
            EDITGRIDWEFTDESCROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFT_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWEFT.CellClick
        Try
            If GRIDWEFT.RowCount > 0 Then GRIDTEMPVIEW(GRIDWEFT, DT_WEFTDETAILS, "FSRNO", "FDSRNO", "FDSHADE", "FDMAINSRNO")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDWARPCLOSE_Click(sender As Object, e As EventArgs) Handles CMDWARPCLOSE.Click
        Try
            If CMBGRIDSYM.Text <> "" And CMBWARPQUALITY.Text.Trim <> "" Then
                For Each symRow As DataGridViewRow In GRIDWARP.Rows
                    If symRow.IsNewRow Then Continue For
                    Dim symValue As String = symRow.Cells(WSYM.Index).Value?.ToString()
                    If symValue = CMBGRIDSYM.Text.Trim And GRIDDOUBLECLICK = False Then
                        MessageBox.Show("Multiple Sym Not Allowed.")
                        Exit Sub
                    End If
                Next
            Else
                MsgBox("Fill Yarn Quality OR Symbol")
            End If
            If GRIDWARP.RowCount >= 0 And CMBWARPQUALITY.Text <> "" And CMBGRIDSYM.Text <> "" Then
                fillwarpgrid()
            End If
            GBWARP.Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMDWEFTCLOSE_Click(sender As Object, e As EventArgs) Handles CMDWEFTCLOSE.Click
        Try
            If CMBWEFTGRIDSYMBOL.Text <> "" And CMBWEFTYARNQUALITY.Text.Trim <> "" Then
                For Each symRow As DataGridViewRow In GRIDWEFT.Rows
                    If symRow.IsNewRow Then Continue For
                    Dim symValue As String = symRow.Cells(FSYM.Index).Value?.ToString()
                    If symValue = CMBWEFTGRIDSYMBOL.Text.Trim And GRIDWEFTDOUBLECLICK = False Then
                        MessageBox.Show("Multiple Sym Not Allowed.")
                        Exit Sub
                        'ElseIf symValue <> CMBWEFTGRIDSYMBOL.Text.Trim Then
                        '    FILLWEFTGRID()
                        '    'GRIDWEFTDOUBLECLICK = True
                    End If
                Next

            Else
                MsgBox("Fill Yarn Quality OR Symbol")
            End If
            If GRIDWEFT.RowCount >= 0 And CMBWEFTYARNQUALITY.Text <> "" And CMBWEFTGRIDSYMBOL.Text <> "" Then
                FILLWEFTGRID()
            End If
            GBWEFT.Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TXTLEFTSELENDS_Validated(sender As Object, e As EventArgs) Handles TXTLEFTSELENDS.Validated
        Try
            If TXTLEFTSELENDS.Text <> "" Then TXTRIGHTSELENDS.Text = Val(TXTLEFTSELENDS.Text.Trim)
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLEFTSEL_Validated(sender As Object, e As EventArgs) Handles TXTLEFTSEL.Validated
        Try
            If TXTLEFTSEL.Text.Trim <> "" Then TXTRIGHTSEL.Text = TXTLEFTSEL.Text
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCALC_Click(sender As Object, e As EventArgs) Handles CMDCALC.Click
        CALC()
    End Sub

    Private Sub TXTFWIDTH_Validated(sender As Object, e As EventArgs) Handles TXTFWIDTH.Validated
        Try
            If TXTFWIDTH.Text <> "" Then TXTFWIDTHCM.Text = Format(Val(TXTFWIDTH.Text.Trim) * 2.54, "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTREEDSPACE_Validated(sender As Object, e As EventArgs) Handles TXTREEDSPACE.Validated
        Try
            If TXTREEDSPACE.Text <> "" Then TXTREEDSPACECM.Text = Format(Val(TXTREEDSPACE.Text.Trim) * 2.54, "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub blendpercentcalc()
        Try
            For Each row As DataGridViewRow In GRIDWARP.Rows
                Dim OBJCLS As New ClsCommon()
                Dim DT2 As New DataTable
                DT2 = OBJCLS.SEARCH("ISNULL(YARN_DENIER, 0) As DENIER, ISNULL(MILLMASTER.MILL_NAME, '') As MILLNAME", "", "  YARNQUALITYMASTER LEFT OUTER JOIN MILLMASTER ON YARNQUALITYMASTER.YARN_YEARID = MILLMASTER.MILL_YEARID AND YARNQUALITYMASTER.YARN_MILLID = MILLMASTER.MILL_ID  ", "  And YARN_NAME ='" & row.Cells(WQUALITY.Index).Value.ToString & "'  AND YARN_YEARID = " & YearId)
                If DT2.Rows.Count > 0 Then

                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class