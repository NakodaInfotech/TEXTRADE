

Imports System.ComponentModel
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web.UI.WebControls
Imports BL
Imports DevExpress.Charts.Native
Imports DevExpress.CodeParser
Imports DevExpress.DashboardCommon.Native
Imports DevExpress.DashboardWin.Native
Imports DevExpress.UIAutomation
Imports DevExpress.XtraGauges.Core.Model
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPivotGrid.Design
Imports DevExpress.XtraRichEdit.Commands
Imports DevExpress.XtraRichEdit.Model

Public Class DesignCardMaster

    Public EDIT As Boolean              'Used for edit
    Public tempdesignno As Integer           'Used for edit name
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

            If Not errorvalid() Then
                Exit Sub
            End If
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
            alParaval.Add(TXTTHREADPERDENT.Text.Trim)
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
            alParaval.Add(TXTCOVERFACTOR.Text.Trim)        ' Cover Factor
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
                If row.Cells(2).Value <> "" Then
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
                If row.Cells(2).Value <> "" Then
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
            alParaval.Add(TXTGWIDTH.Text.Trim)
            alParaval.Add(TXTGWIDTHCM.Text.Trim)
            alParaval.Add(TXTFWIDTHCM.Text.Trim)
            alParaval.Add(TXTGLM.Text.Trim)
            alParaval.Add(TXTBLENDPER.Text.Trim)
            alParaval.Add(txtfinishmethod.Text.Trim)
            alParaval.Add(CMBQUALITIES.Text.Trim)
            alParaval.Add(CMBQUALITYTYPE.Text.Trim)
            alParaval.Add(TXTWARPWASTAGE.Text.Trim)
            alParaval.Add(TXTWASTAGEPER.Text.Trim)
            alParaval.Add(TXTSHRINKAGEPER.Text.Trim)
            alParaval.Add(TXTWPP.Text.Trim)
            alParaval.Add(TXTWEAVECOST.Text.Trim)
            alParaval.Add(TXTGFABCOST.Text.Trim)
            alParaval.Add(TXTFFABCOST.Text.Trim)
            alParaval.Add(TXTPRODDAY.Text.Trim)
            alParaval.Add(TXTPCSL.Text.Trim)
            alParaval.Add(Val(TXTREEDSPACECM.Text.Trim))


            '*************************************************************************
            'GRID PEGING
            Dim PEGSrNo As String = ""
            Dim PEGEnds As String = ""
            Dim PEGREPEATMARK As String = ""
            Dim PEGREPEATS As String = ""
            Dim PEGREPEATMARK1 As String = ""
            Dim PEGREPEATS1 As String = ""
            Dim PEGREPEATMARK2 As String = ""
            Dim PEGREPEATS2 As String = ""
            Dim PEGSYM As String = ""
            For Each row As Windows.Forms.DataGridViewRow In GRIDPEG.Rows
                If row.Cells(PPSRNO.Index).Value IsNot Nothing AndAlso row.Cells(PPENDS.Index).Value IsNot Nothing Then
                    If PEGSrNo = "" Then
                        PEGSrNo = Val(row.Cells(PPSRNO.Index).Value)
                        PEGEnds = row.Cells(PPENDS.Index).Value.ToString()
                        PEGREPEATMARK = row.Cells(PPRM.Index).Value
                        PEGREPEATS = Val(row.Cells(PPR.Index).Value)
                        PEGREPEATMARK1 = row.Cells(PPRM1.Index).Value
                        PEGREPEATS1 = Val(row.Cells(PPR1.Index).Value)
                        PEGREPEATMARK2 = row.Cells(PPRM2.Index).Value
                        PEGREPEATS2 = Val(row.Cells(PPR2.Index).Value)
                        PEGSYM = row.Cells(PPSYM.Index).Value
                    Else
                        PEGSrNo = PEGSrNo & "|" & Val(row.Cells(PPSRNO.Index).Value)
                        PEGEnds = PEGEnds & "|" & row.Cells(PPENDS.Index).Value.ToString()
                        PEGREPEATMARK = PEGREPEATMARK & "|" & row.Cells(PPRM.Index).Value
                        PEGREPEATS = PEGREPEATS & "|" & Val(row.Cells(PPR.Index).Value)
                        PEGREPEATMARK1 = PEGREPEATMARK1 & "|" & row.Cells(PPRM1.Index).Value
                        PEGREPEATS1 = PEGREPEATS1 & "|" & Val(row.Cells(PPR1.Index).Value)
                        PEGREPEATMARK2 = PEGREPEATMARK2 & "|" & row.Cells(PPRM2.Index).Value
                        PEGREPEATS2 = PEGREPEATS2 & "|" & Val(row.Cells(PPR2.Index).Value)
                        PEGSYM = PEGSYM & "|" & row.Cells(PPSYM.Index).Value
                    End If
                End If
            Next
            alParaval.Add(PEGSrNo)
            alParaval.Add(PEGEnds)
            alParaval.Add(PEGREPEATMARK)
            alParaval.Add(PEGREPEATS)
            alParaval.Add(PEGREPEATMARK1)
            alParaval.Add(PEGREPEATS1)
            alParaval.Add(PEGREPEATMARK2)
            alParaval.Add(PEGREPEATS2)
            alParaval.Add(PEGSYM)

            alParaval.Add(TXTTOTALENDS.Text.Trim)
            alParaval.Add(TXTENDPERINCH.Text.Trim)
            alParaval.Add(TXTTOTALPEG.Text.Trim)


            Dim objDESIGN As New ClsDesignCardMaster
            objDESIGN.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objDESIGN.SAVE()
                'txtcardno.Text = IntResult.ToString()
                MsgBox("Details Added")
                'tempdesignno = txtcardno.Text.Trim
                PRINTREPORT(txtcardno.Text.Trim)
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempdesignno)
                IntResult = objDESIGN.UPDATE()
                MsgBox("Details Updated")
                PRINTREPORT(tempdesignno)
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
        txtfinishmethod.Clear()
        CMBQUALITIES.Text = ""
        CMBQUALITYTYPE.Text = ""
        DTDATE.Text = Now.Date
        CMBDESIGNNO.Text = ""
        CMBITEMNAME.Text = ""
        TXTREED.Clear()
        TXTREEDSPACE.Text = "65"
        TXTREEDSPACECM.Clear()
        TXTPICKS.Clear()
        TXTMAINRS.Clear()
        TXTTHREADPERDENT.Clear()
        TXTFEPI.Clear()
        TXTFWIDTH.Text = "58"
        TXTFPPI.Clear()
        TXTFWT.Clear()
        TXTDENTS.Clear()
        TXTTOTALDENTSMAIN.Clear()
        TXTTOTALSELVEDGEDENTS.Clear()
        TXTTOTALDENTS.Clear()
        TXTWARPTL.Text = "108"
        TXTWEFTTL.Text = "102"
        TXTGSM.Clear()
        CMBWEAVE.Text = ""
        TXTTOTALWT.Clear()
        TXTGWIDTH.Clear()
        TXTGWIDTHCM.Clear()
        TXTFWIDTHCM.Clear()
        TXTSHRINKAGEPER.Clear()
        TXTWARPWASTAGE.Clear()
        TXTWASTAGEPER.Clear()
        TXTWPP.Clear()
        TXTPRODDAY.Clear()
        TXTPCSL.Clear()
        TXTWEAVECOST.Clear()
        TXTGFABCOST.Clear()
        TXTFFABCOST.Clear()
        TXTCOPYCARDNO.Enabled = True
        TXTGLM.Clear()
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
        TXTORDERNO.Clear()
        ORDERDATE.Text = Now.Date
        CMBGREY.Text = ""
        TXTBLENDPER.Clear()
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
        CMBSELGSYM.Text = ""
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
        GRIDWARPDESC.RowCount = 0
        'GRID WARP PATTERN
        GRIDWARPPATTERN.RowCount = 1
        GRIDWEFTDESC.RowCount = 0
        'GRID SLEVAGE
        GRIDSELVEDGE.RowCount = 0
        GRIDSELDESC.RowCount = 0

        GRIDSELVEDGEPATTERN.RowCount = 1
        'GRID WEFT
        GRIDWEFT.RowCount = 0
        'GRID WEFT PATTERN
        GRIDWEFTPATTERN.RowCount = 1
        'GRID DRAWING
        GRIDDRAWING.RowCount = 1
        'GRIDPEG PLAN
        GRIDPEG.RowCount = 1
        TXTTOTALPEG.Clear()
        'GRID PEGPLAN 
        GRIDPEGPLAN.RowCount = 0
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
        TXTCOPYCARDNO.Clear()
        GBWARP.Visible = False
        GBWEFT.Visible = False
        GBSSHADEDETAILS.Visible = False
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
        If TXTREED.Text.Trim.Length = 0 Then
            Ep.SetError(TXTREED, "Fill Reed ")
            bln = False
        End If
        If TXTTHREADPERDENT.Text.Trim.Length = 0 Then
            Ep.SetError(TXTTHREADPERDENT, "Fill E.P.D")
            bln = False
        End If
        If TXTPICKS.Text.Trim.Length = 0 Then
            Ep.SetError(TXTPICKS, "Fill PICKS ON LOOM ")
            bln = False
        End If
        If TXTREEDSPACE.Text.Trim.Length = 0 Then
            Ep.SetError(TXTREEDSPACE, "Fill Reed Space ")
            bln = False
        End If
        If TXTWEFTTL.Text.Trim.Length = 0 Then
            Ep.SetError(TXTWEFTTL, "Fill WEFT TL ")
            bln = False
        End If
        If TXTWARPTL.Text.Trim.Length = 0 Then
            Ep.SetError(TXTWARPTL, "Fill WARP TL ")
            bln = False
        End If
        If TXTLEFTSEL.Text.Trim.Length = 0 Then
            Ep.SetError(TXTLEFTSEL, "Fill Selvedge Size ")
            bln = False
        End If
        If TXTLEFTSELENDS.Text.Trim.Length = 0 Then
            Ep.SetError(TXTLEFTSELENDS, "Fill Selvedge E P Dent ")
            bln = False
        End If
        If TXTSHRINKAGEPER.Text.Trim.Length = 0 Then
            Ep.SetError(TXTSHRINKAGEPER, "Fill Shrinkage Percent. ")
            bln = False
        End If
        If Not CheckGridsForBlankOrNull(GRIDWARPPATTERN, "WPENDS", "WPSYM") Then
            Ep.SetError(cmdok, "Check Warp Pattern Grid. ")
            bln = False ' If validation fails, set bln to False
        End If

        ' Check for blank/null in FPENDS and FPSYM columns for GRIDWEFTPATTERN
        If Not CheckGridsForBlankOrNull(GRIDWEFTPATTERN, "FPENDS", "FPSYM") Then
            Ep.SetError(cmdok, "Check Weft Pattern Grid. ")
            bln = False ' If validation fails, set bln to False
        End If

        ' Check for blank/null in SPENDS and SPSYM columns for GRIDSELVEDGEPATTERN
        If Not CheckGridsForBlankOrNull(GRIDSELVEDGEPATTERN, "SPENDS", "SPSYM") Then
            Ep.SetError(cmdok, "Check Selvedge Pattern Grid. ")
            bln = False ' If validation fails, set bln to False
        End If
        Return bln
    End Function
    Public Function CheckGridsForBlankOrNull(grid As DataGridView, endColumn As String, symColumn As String) As Boolean
        ' Loop through each row in the grid
        For Each row As DataGridViewRow In grid.Rows
            ' Skip the new row (this row is just for adding new data and does not contain real data yet)
            If row.IsNewRow Then Continue For

            ' Check endColumn for null or blank
            Dim endValue As String = If(row.Cells(endColumn).Value Is DBNull.Value, "", row.Cells(endColumn).Value.ToString().Trim())
            If String.IsNullOrWhiteSpace(endValue) Then
                MessageBox.Show(endColumn & " cannot be left blank or null in row " & row.Index + 1, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                row.Cells(endColumn).Style.BackColor = Color.Red ' Highlight the invalid cell in red
                grid.CurrentCell = row.Cells(endColumn) ' Set focus to the invalid cell
                Return False ' Return False when validation fails
            End If

            ' Check symColumn for null or blank
            Dim symValue As String = If(row.Cells(symColumn).Value Is DBNull.Value, "", row.Cells(symColumn).Value.ToString().Trim())
            If String.IsNullOrWhiteSpace(symValue) Then
                MessageBox.Show(symColumn & " cannot be left blank or null in row " & row.Index + 1, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                row.Cells(symColumn).Style.BackColor = Color.Red ' Highlight the invalid cell in red
                grid.CurrentCell = row.Cells(symColumn) ' Set focus to the invalid cell
                Return False ' Return False when validation fails
            End If
        Next
        Return True ' Return True if all validations pass
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
            FILLPEGPLAN()

            If EDIT = True Then
                SHOWDATA()
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
    Sub SHOWDATA(Optional ByVal CARDNO As Integer = -1)
        Try
            clear()
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJCMN As New ClsCommon
            Dim objclsGRN As New ClsDesignCardMaster()
            Dim dttable As New DataTable
            If CARDNO > 0 Then tempdesignno = If(CARDNO = -1, -1, CARDNO)
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
                    TXTFWT.Text = Format(Val(dr("FWT").ToString), "0.000")
                    TXTDENTS.Text = dr("DENTS").ToString
                    TXTTOTALDENTSMAIN.Text = Val(dr("TOTALDENTSMAIN"))
                    TXTTOTALSELVEDGEDENTS.Text = Val(dr("TOTALSELVEDGEDENTS"))
                    TXTTOTALDENTS.Text = Val(dr("TOTALDENTS"))

                    TXTWARPTL.Text = dr("WARPTTL").ToString
                    TXTWEFTTL.Text = dr("WEFTTTL").ToString
                    TXTGSM.Text = dr("GSM").ToString
                    CMBWEAVE.Text = Convert.ToString(dr("WEAVE").ToString)
                    CMBSHAFTS.Text = Convert.ToString(dr("SHAFTS").ToString)
                    TXTTOTALWT.Text = Format(Val(dr("TOTALWT")), "0.000")
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

                    TXTTOTALWARPPE.Text = Val(dr("TOTALWARPPE"))
                    TXTTOTALWARPBE.Text = Val(dr("TOTALWARPBE"))
                    TXTTOTALWARPTE.Text = Val(dr("TOTALWARPTE"))
                    TXTTOTALWARPWT.Text = Format(Val(dr("TOTALWARPWT")), "0.000")
                    TXTTOTALWARPCONS.Text = Val(dr("TOTALWARPCONS"))
                    TXTTOTALWARPRATE.Text = Val(dr("TOTALWARPRATE"))
                    TXTTOTALWARPCOST.Text = Val(dr("TOTALWARPCOST"))
                    TXTTOTALWARPGRIDPE.Text = Val(dr("TOTALWARPGRIDPE"))
                    ' Total Selvedge

                    TXTTOTALSELPE.Text = Val(dr("TOTALSELVEDGEPE"))
                    TXTTOTALSELBE.Text = Val(dr("TOTALSELVEDGEBE"))
                    TXTTOTALSELTE.Text = Val(dr("TOTALSELVEDGETE"))
                    TXTTOTALSELWT.Text = Format(Val(dr("TOTALSELVEDGEWT")), "0.000")
                    TXTTOTALSELCONS.Text = Val(dr("TOTALSELVEDGECONS"))
                    TXTSELTOTALRATE.Text = Val(dr("TOTALSELVEDGERATE"))
                    TXTSELTOTALCOST.Text = Val(dr("TOTALSELVEDGECOST"))
                    TXTTOTALSELGPE.Text = Val(dr("TOTALSELVEDGEGRIDPE"))
                    ' Total Weft

                    TXTTOTALWEFTPE.Text = Val(dr("TOTALWEFTPE"))
                    TXTTOTALWEFTBE.Text = Val(dr("TOTALWEFTBE"))
                    TXTTOTALWEFTTE.Text = Val(dr("TOTALWEFTTE"))
                    TXTTOTALWEFTWT.Text = Format(Val(dr("TOTALWEFTWT")), "0.000")
                    TXTTOTALWEFTCONS.Text = Val(dr("TOTALWEFTCONS"))
                    TXTTOTALWEFTRATE.Text = Val(dr("TOTALWEFTRATE"))
                    TXTTOTALWEFTCOST.Text = Val(dr("TOTALWEFTCOST"))
                    TXTTOTALWEFTGRIDPE.Text = Val(dr("TOTALWEFTGRIDPE"))
                    ' Total DRAWING 

                    TXTTOTALDRAWENDS.Text = Val(dr("TOTALDRAWENDS"))
                    TXTTOTALDRAWDENTS.Text = Val(dr("TOTALDRAWDENTS"))
                    TXTTOTALPEG.Text = Val(dr("TOTALPEG"))




                    TXTFINISHWT.Text = Format(Val(dr("TOTALFINISHWT")), "0.000")
                    TXTGWIDTH.Text = Val(dr("GREYWIDTH"))
                    TXTGWIDTHCM.Text = Val(dr("GREYWIDTHCM"))
                    TXTFWIDTHCM.Text = Val(dr("FINISHWIDTHCM"))
                    TXTWARPWASTAGE.Text = Val(dr("WARPWASTAGE"))
                    TXTWASTAGEPER.Text = Val(dr("WASTAGEPER"))
                    TXTSHRINKAGEPER.Text = Val(dr("SHRINKAGEPER"))
                    TXTWPP.Text = Val(dr("WPP"))
                    TXTWEAVECOST.Text = Val(dr("WEAVECOST"))
                    TXTGFABCOST.Text = Val(dr("GREYFABCOST"))
                    TXTFFABCOST.Text = Val(dr("FINISHFABCOST"))
                    TXTPRODDAY.Text = Val(dr("PRODDAY"))
                    TXTPCSL.Text = Val(dr("PCSL"))
                    TXTREEDSPACECM.Text = Val(dr("REEDSPACECM"))
                    txtfinishmethod.Text = Convert.ToString(dr("FINISHMETHOD").ToString)
                    CMBQUALITIES.Text = Convert.ToString(dr("QUALITY").ToString)
                    CMBQUALITYTYPE.Text = Convert.ToString(dr("QUALITYTYPE").ToString)
                    TXTBLENDPER.Text = dr("BLENDPER")
                    TXTGLM.Text = Format(Val(dr("GREYLOOMMTR")), "0.000")
                    TXTENDPERINCH.Text = dr("ENDPERINCH")
                    TXTTOTALENDS.Text = dr("TOTALENDS")
                Next
                'cmbtype.Enabled = False

                'TOTAL()

                'warp gridmatching data serializations
                Dim dttable1 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSRNO, 0) As WARPGRIDSRNO, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSYM, '') AS WARPGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WARPYARNQUALITY, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPDENIER, 0) AS WARPDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WARPMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WARPSHADE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPPE, 0) AS WARPPE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPBE, 0) AS WARPBE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPTE, 0) AS WARPTE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPWT, 0.000) AS WARPWT, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCONS, 0) AS WARPCONS, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPRATE, 0) AS WARPRATE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCOST, 0) AS WARPCOST ", "", " DESIGNCARD_WARPMATCHING INNER JOIN YARNQUALITYMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_WARPYARNQUALITYID = YARNQUALITYMASTER.YARN_ID AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = YARNQUALITYMASTER.YARN_YEARID LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = MILLMASTER.MILL_YEARID AND MILLMASTER.MILL_ID = DESIGNCARD_WARPMATCHING.DESIGN_WARPMILLID LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = COLORMASTER.COLOR_yearid AND COLORMASTER.COLOR_id = DESIGNCARD_WARPMATCHING.DESIGN_WARPCOLORID  ", " AND  DESIGNCARD_WARPMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WARPGRIDSRNO")
                If dttable1.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable1.Rows
                        GRIDWARP.Rows.Add(Val(DTR("WARPGRIDSRNO")), DTR("WARPGRIDSYM").ToString, DTR("WARPYARNQUALITY").ToString, Format(DTR("WARPDENIER"), "0.00"), DTR("WARPMILLNAME").ToString, DTR("WARPSHADE").ToString, Format(DTR("WARPPE"), "0.00"), Format(DTR("WARPBE"), "0.00"), Format(DTR("WARPTE"), "0.00"), Format(DTR("WARPWT"), "0.000"), Format(DTR("WARPCONS"), "0.00"), Format(DTR("WARPRATE"), "0.00"), Format(DTR("WARPCOST"), "0.00"))
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
                Dim dttableWARPshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_WDSRNO, 0) AS WDSRNO, ISNULL(COLORMASTER.COLOR_name,'') AS WDSHADE, ISNULL(DESIGN_WDMAINSRNO, 0) AS WDMAINSRNO", "", " DESIGNCARD_WARPSHADE LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WARPSHADE.DESIGN_WDSHADE = COLORMASTER.COLOR_id AND DESIGNCARD_WARPSHADE.DESIGN_YEARID = COLORMASTER.COLOR_yearid  ", " AND  DESIGNCARD_WARPSHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPSHADE.DESIGN_YEARID = " & YearId & " ORDER BY WDSRNO")
                If dttableWARPshade.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttableWARPshade.Rows
                        DT_WARPDETAILS.Rows.Add(DTR("WDSRNO"), DTR("WDSHADE"), DTR("WDMAINSRNO"))
                    Next
                    POPULATEGRID()
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

                Dim dttableshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_sdSRNO, 0) AS SDSRNO,ISNULL(COLORMASTER.COLOR_name,'') AS  SDSHADE, ISNULL(DESIGN_sdMAINSRNO, 0) AS SDMAINSRNO", "", " DESIGNCARD_SELVEDGESHADE LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_SELVEDGESHADE.DESIGN_SDSHADE = COLORMASTER.COLOR_id AND DESIGNCARD_SELVEDGESHADE.DESIGN_YEARID = COLORMASTER.COLOR_yearid   ", " AND  DESIGNCARD_SELVEDGESHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_SELVEDGESHADE.DESIGN_YEARID = " & YearId & " ORDER BY SDSRNO")
                If dttableshade.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttableshade.Rows
                        DT_SELDETAILS.Rows.Add(Val(DTR("SDSRNO")), DTR("SDSHADE").ToString, Val(DTR("SDMAINSRNO")))
                    Next
                    POPULATESELGRID()
                End If

                ' Weft Grid data serialization
                Dim dttable5 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSRNO, 0) AS WEFTGRIDSRNO, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSYM, '') AS WEFTGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WEFTYARNQUALITY, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTDENIER, 0) AS WEFTDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WEFTMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WEFTSHADE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTPE, 0) AS WEFTPE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTBE, 0) AS WEFTBE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTTE, 0) AS WEFTTE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTWT, 0) AS WEFTWT, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCONS, 0) AS WEFTCONS, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTRATE, 0) AS WEFTRATE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCOST, 0) AS WEFTCOST", "", " DESIGNCARD_WEFTMATCHING LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCOLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTMILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTYARNQUALITYID = YARNQUALITYMASTER.YARN_ID   ", " AND  DESIGNCARD_WEFTMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WEFTMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WEFTGRIDSRNO")
                If dttable5.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable5.Rows
                        GRIDWEFT.Rows.Add(DTR("WEFTGRIDSRNO"), DTR("WEFTGRIDSYM").ToString, DTR("WEFTYARNQUALITY").ToString, Format(DTR("WEFTDENIER"), "0.00"), DTR("WEFTMILLNAME").ToString, DTR("WEFTSHADE").ToString, Format(DTR("WEFTPE"), "0.00"), Format(DTR("WEFTBE"), "0.00"), Format(DTR("WEFTTE"), "0.00"), Format(DTR("WEFTWT"), "0.000"), Format(DTR("WEFTCONS"), "0.00"), Format(DTR("WEFTRATE"), "0.00"), Format(DTR("WEFTCOST"), "0.00"))
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

                Dim dttableWEFTshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_FDSRNO, 0) AS FDSRNO,ISNULL(COLORMASTER.COLOR_name,'') AS FDSHADE, ISNULL(DESIGN_FDMAINSRNO, 0) AS FDMAINSRNO", "", " DESIGNCARD_WEFTSHADE LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WEFTSHADE.DESIGN_FDSHADE = COLORMASTER.COLOR_id AND DESIGNCARD_WEFTSHADE.DESIGN_YEARID = COLORMASTER.COLOR_yearid   ", " AND  DESIGNCARD_WEFTSHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WEFTSHADE.DESIGN_YEARID = " & YearId & " ORDER BY FDSRNO")
                If dttableWEFTshade.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttableWEFTshade.Rows
                        DT_WEFTDETAILS.Rows.Add(Val(DTR("FDSRNO")), DTR("FDSHADE").ToString, Val(DTR("FDMAINSRNO")))
                    Next
                    POPULATEWEFTGRID()
                End If
                'DRAWING FIELD
                Dim dttable7 As DataTable = OBJCMN.SEARCH("  ISNULL(DESIGN_DRAWINGSRNO, 0) AS DRAWINGSRNO, ISNULL(DESIGN_DRAWINGENDS, 0) AS DRAWINGENDS, ISNULL(DESIGN_DRAWINGREPEATMARK, '') AS DRAWINGREPEATMARK, ISNULL(DESIGN_DRAWINGREPEAT, 0) AS DRAWINGREPEAT, ISNULL(DESIGN_DRAWINGREPEATMARK1, '') AS DRAWINGGRIDREPEATMARK1, ISNULL(DESIGN_DRAWINGREPEAT1, 0) AS DRAWINGREPEAT1, ISNULL(DESIGN_DRAWINGREPEATMARK2, '') AS DRAWINGREPEATMARK2, ISNULL(DESIGN_DRAWINGREPEAT2, 0) AS DRAWINGREPEAT2 ", "", " DESIGNCARD_DRAWING  ", " AND  DESIGNCARD_DRAWING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_DRAWING.DESIGN_YEARID = " & YearId & " ORDER BY DRAWINGSRNO")
                If dttable7.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable7.Rows
                        GRIDDRAWING.Rows.Add(DTR("DRAWINGSRNO"), DTR("DRAWINGENDS").ToString, DTR("DRAWINGREPEATMARK").ToString, DTR("DRAWINGREPEAT"), DTR("DRAWINGGRIDREPEATMARK1").ToString, DTR("DRAWINGREPEAT1"), DTR("DRAWINGREPEATMARK2").ToString, DTR("DRAWINGREPEAT2"))
                    Next

                End If
                'PEGPLAN FIELD
                Dim dttable8 As DataTable = OBJCMN.SEARCH("  ISNULL(DESIGN_PPSRNO, 0) AS PPSRNO, ISNULL(DESIGN_PPENDS, 0) AS PPENDS, ISNULL(DESIGN_PPREPEATMARK, '') AS PPREPEATMARK, ISNULL(DESIGN_PPREPEAT, 0) AS PPREPEAT, ISNULL(DESIGN_PPREPEATMARK1, '') AS PPGRIDREPEATMARK1, ISNULL(DESIGN_PPREPEAT1, 0) AS PPREPEAT1, ISNULL(DESIGN_PPREPEATMARK2, '') AS PPREPEATMARK2, ISNULL(DESIGN_PPREPEAT2, 0) AS PPREPEAT2, ISNULL(DESIGN_PPSYM, '') AS PPSYM ", "", " DESIGNCARD_PEGPLAN  ", " AND  DESIGNCARD_PEGPLAN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_PEGPLAN.DESIGN_YEARID = " & YearId & " ORDER BY PPSRNO")
                If dttable8.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable8.Rows
                        GRIDPEG.Rows.Add(DTR("PPSRNO"), DTR("PPENDS").ToString, DTR("PPREPEATMARK").ToString, DTR("PPREPEAT"), 0, 0, DTR("PPGRIDREPEATMARK1").ToString, DTR("PPREPEAT1"), 0, 0, DTR("PPREPEATMARK2").ToString, DTR("PPREPEAT2"), 0, 0, DTR("PPSYM").ToString)
                    Next
                    ' GRIDPEG_CellValidating(Nothing, Nothing)
                End If
                cmdbtn1_Click(Nothing, Nothing, GRIDPEG)
                cmdbtn1_Click(Nothing, Nothing, GRIDDRAWING)
                TOTAL()
                CALC()
                FILLPEGPLAN()
                pegplan()
                'GRIDDRAWING_CellValidating(Nothing, Nothing)
                srno(GRIDWARP, TXTWARPSRNO)
                srno(GRIDSELVEDGE, TXTSELSRNO)
                srno(GRIDWEFT, TXTWEFTSRNO)
                srno(GRIDWEFTDESC, TXTFDSRNO)
                srno(GRIDWARPDESC, TXTWDSRNO)
                srno(GRIDSELDESC, TXTSDNO)
                fillMATCHINGcmb()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillMATCHINGcmb()
        Dim OBJCMN As New ClsCommon
        Dim dttable8 As DataTable = OBJCMN.SEARCH(" DISTINCT DESIGN_WARPSYM AS WARPSYM", "", " DESIGNCARD_WARPPATTERN  ", " AND  DESIGNCARD_WARPPATTERN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPPATTERN.DESIGN_YEARID = " & YearId & " ORDER BY DESIGN_WARPSYM")
        If dttable8.Rows.Count > 0 Then
            ' Clear the ComboBox first to avoid appending to any previous items
            CMBGRIDSYM.Items.Clear()
            For Each DTR As DataRow In dttable8.Rows
                ' Add each value from the DataRow to the ComboBox
                CMBGRIDSYM.Items.Add(DTR("WARPSYM").ToString())
            Next
        End If
        Dim dttable1 As DataTable = OBJCMN.SEARCH(" DISTINCT DESIGN_WARPSYM AS WEFTSYM", "", " DESIGNCARD_WEFTPATTERN  ", " AND  DESIGNCARD_WEFTPATTERN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WEFTPATTERN.DESIGN_YEARID = " & YearId & " ORDER BY DESIGN_WARPSYM")
        If dttable1.Rows.Count > 0 Then
            ' Clear the ComboBox first to avoid appending to any previous items
            CMBWEFTGRIDSYMBOL.Items.Clear()
            For Each DTR As DataRow In dttable1.Rows
                ' Add each value from the DataRow to the ComboBox
                CMBWEFTGRIDSYMBOL.Items.Add(DTR("WEFTSYM").ToString())
            Next
        End If
        Dim dttable2 As DataTable = OBJCMN.SEARCH(" DISTINCT DESIGN_SELVEDGESYM AS SELSYM", "", " DESIGNCARD_SELVEDGEPATTERN  ", " AND  DESIGNCARD_SELVEDGEPATTERN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_SELVEDGEPATTERN.DESIGN_YEARID = " & YearId & " ORDER BY DESIGN_SELVEDGESYM")
        If dttable2.Rows.Count > 0 Then
            ' Clear the ComboBox first to avoid appending to any previous items
            CMBSELGSYM.Items.Clear()
            For Each DTR As DataRow In dttable2.Rows
                ' Add each value from the DataRow to the ComboBox
                CMBSELGSYM.Items.Add(DTR("SELSYM").ToString())
            Next
        End If
    End Sub
    Public Sub srno(grid As DataGridView, txtBox As System.Windows.Forms.TextBox)
        If grid Is Nothing OrElse txtBox Is Nothing Then Exit Sub

        If grid.RowCount > 0 Then
            Dim lastValue As Integer = Val(grid.Rows(grid.RowCount - 1).Cells(0).Value)
            txtBox.Text = (lastValue + 1).ToString()
        Else
            txtBox.Text = "1"
        End If
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
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                Toolprevious_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                PrintToolStripButton_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.D Then
                cmddelete_Click(sender, e)
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
        FILLLOOM(CMBLOOM, CMBNAME.Text.Trim, EDIT)
        FILLWEAVE(CMBWEAVE, EDIT)
        If CMBAGENTNAME.Text.Trim = "" Then FILLNAME(CMBAGENTNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
        If CMBDELAT.Text.Trim = "" Then FILLNAME(CMBDELAT, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        If CMBGREYDELAT.Text.Trim = "" Then FILLNAME(CMBGREYDELAT, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE='ACCOUNTS'")
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
            GRIDWARP.Rows.Add(Val(TXTWARPSRNO.Text.Trim), CMBGRIDSYM.Text.Trim, CMBWARPQUALITY.Text.Trim, TXTWARPDENIER.Text.Trim, CMBWARPMILLNAME.Text.Trim, CMBWARPSHADE.Text.Trim, Val(TXTWARPPE.Text.Trim), Val(TXTWARPBE.Text.Trim), Val(TXTWARPTE.Text.Trim), Format(Val(TXTWARPWT.Text.Trim), "0.000"), Val(TXTWARPCONS.Text.Trim), Val(TXTWARPRATE.Text.Trim), Val(TXTWARPCOST.Text.Trim))
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
            GRIDWARP.Item(WWT.Index, TEMPROW).Value = Format((TXTWARPWT.Text.Trim), "0.000")
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
        'If String.IsNullOrWhiteSpace(CMBGRIDSYM.Text) Then
        '    ' Set to the first item in the list (top alphabet)
        '    If CMBGRIDSYM.Items.Count > 0 Then
        '        CMBGRIDSYM.Text = CMBGRIDSYM.Items(0).ToString()
        '    End If
        'Else
        '    CMBGRIDSYM.Text = IncrementAlphabet(CMBGRIDSYM.Text, CMBGRIDSYM)
        'End If
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

        CALC()
        POPULATEGRID()
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
    Sub POPULATEGRID()
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
    Sub fillselvedgegrid()
        If GRIDSELDOUBLECLICK = False Then
            GRIDSELVEDGE.Rows.Add(Val(TXTSELSRNO.Text.Trim), CMBSELGSYM.Text.Trim, CMBSELYARNQUALITY.Text.Trim, TXTSELDEN.Text.Trim, CMBSELMILLNAME.Text.Trim, CMBSELSHADE.Text.Trim, Val(TXTSELPE.Text.Trim), Val(TXTSELBE.Text.Trim), Val(TXTSELTE.Text.Trim), Val(TXTSELWT.Text.Trim), Val(TXTSELCONS.Text.Trim), Val(TXTSELRATE.Text.Trim), Val(TXTSELCOST.Text.Trim))

            getsrno(GRIDSELVEDGE)
        ElseIf GRIDSELDOUBLECLICK = True Then
            GRIDSELVEDGE.Item(SSRNO.Index, TEMPSELROW).Value = Val(TXTSELSRNO.Text.Trim)
            GRIDSELVEDGE.Item(SSYM.Index, TEMPSELROW).Value = CMBSELGSYM.Text.Trim
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
        If String.IsNullOrWhiteSpace(CMBSELGSYM.Text) Then
            ' Set to the first item in the list (top alphabet)
            If CMBSELGSYM.Items.Count > 0 Then
                CMBSELGSYM.Text = CMBSELGSYM.Items(0).ToString()
            End If
        Else
            CMBSELGSYM.Text = IncrementAlphabet(CMBSELGSYM.Text, CMBSELGSYM)
        End If

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
        POPULATESELGRID()
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
        CMBSELGSYM.Focus()

    End Sub
    Function IncrementAlphabet(currentSym As String, cmb As ComboBox) As String
        Dim idx As Integer = cmb.Items.IndexOf(currentSym)
        If idx <> -1 AndAlso idx + 1 < cmb.Items.Count Then
            Return cmb.Items(idx + 1).ToString()
        ElseIf idx = -1 AndAlso cmb.Items.Count > 0 Then
            Return cmb.Items(0).ToString() ' fallback to first item
        Else
            Return currentSym ' if already last, return current
        End If
    End Function
    Sub POPULATESELGRID()
        Dim maxShadeCount As Integer = 0
        For Each dr As DataRow In DT_SELDETAILS.Rows
            Dim srno As Object = dr("SDMAINSRNO")
            Dim shadeRows As DataRow() = DT_SELDETAILS.Select("SDMAINSRNO = '" & srno & "'")
            If shadeRows.Length > maxShadeCount Then maxShadeCount = shadeRows.Length
        Next
        For i As Integer = 1 To maxShadeCount
            Dim colName As String = "SEL" & i
            If Not GRIDSELVEDGE.Columns.Contains(colName) Then
                GRIDSELVEDGE.Columns.Add(colName, colName)
            End If
        Next
        For Each gridRow As DataGridViewRow In GRIDSELVEDGE.Rows
            If gridRow.IsNewRow Then Continue For
            Dim fsrno As Object = gridRow.Cells("SSRNO").Value
            Dim matchedRows As DataRow() = DT_SELDETAILS.Select("SDMAINSRNO = '" & fsrno & "'")
            For shadeIdx As Integer = 0 To matchedRows.Length - 1
                Dim shadeValue As Object = matchedRows(shadeIdx)("SDSHADE")
                gridRow.Cells("SEL" & (shadeIdx + 1)).Value = shadeValue
            Next
        Next
    End Sub
    Sub COPYSELSYM()
        CMBSELGSYM.Items.Clear()

        Dim symSet As New HashSet(Of String)
        For Each row As DataGridViewRow In GRIDSELVEDGEPATTERN.Rows
            If row.Cells(SPSYM.Index) IsNot Nothing AndAlso
   row.Cells(SPSYM.Index).Value IsNot Nothing AndAlso
   Not IsDBNull(row.Cells(SPSYM.Index).Value) AndAlso
   Not String.IsNullOrWhiteSpace(row.Cells(SPSYM.Index).Value.ToString()) Then

                symSet.Add(row.Cells(SPSYM.Index).Value.ToString)
            End If
        Next

        For Each symVal As String In symSet
            CMBSELGSYM.Items.Add(symVal)
        Next
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
        If String.IsNullOrWhiteSpace(CMBWEFTGRIDSYMBOL.Text) Then
            ' Set to the first item in the list (top alphabet)
            If CMBWEFTGRIDSYMBOL.Items.Count > 0 Then
                CMBWEFTGRIDSYMBOL.Text = CMBWEFTGRIDSYMBOL.Items(0).ToString()
            End If
        Else
            CMBWEFTGRIDSYMBOL.Text = IncrementAlphabet(CMBWEFTGRIDSYMBOL.Text, CMBWEFTGRIDSYMBOL)
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

        POPULATEWEFTGRID()

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
    Sub POPULATEWEFTGRID()
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
            Dim fsrno1 As Object = gridRow.Cells(FSRNO.Index).Value
            Dim matchedRows As DataRow() = DT_WEFTDETAILS.Select("FDMAINSRNO = '" & fsrno1 & "'")
            For shadeIdx As Integer = 0 To matchedRows.Length - 1
                Dim shadeValue As Object = matchedRows(shadeIdx)("FDSHADE")
                gridRow.Cells("WEFT" & (shadeIdx + 1)).Value = shadeValue
            Next
        Next
    End Sub

    Private Sub CopyGridWithSubDetails(sourceGrid As DataGridView, targetGrid As DataGridView,
                                   sourceDetails As DataTable, targetDetails As DataTable)

        'Copy main grid
        CopyGridEntries(sourceGrid, targetGrid)

        'Clear target details
        targetDetails.Rows.Clear()

        'Copy sub-grid (DT_WARPDETAILS)
        For Each srcRow As DataRow In sourceDetails.Rows
            Dim newRow As DataRow = targetDetails.NewRow()

            newRow("FDSRNO") = srcRow("WDSRNO")
            newRow("FDSHADE") = srcRow("WDSHADE")
            newRow("FDMAINSRNO") = srcRow("WDMAINSRNO")

            targetDetails.Rows.Add(newRow)
        Next

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
            If CMBDESIGNNO.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGNNO, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSELSHADE_Enter(sender As Object, e As EventArgs) Handles CMBSELSHADE.Enter
        Try
            If CMBSELSHADE.Text.Trim = "" Then FILLCOLOR(CMBSELSHADE, "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPSHADE_Enter(sender As Object, e As EventArgs) Handles CMBWARPSHADE.Enter
        Try
            If CMBWARPSHADE.Text.Trim = "" Then FILLCOLOR(CMBWARPSHADE, "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbweftshade_Enter(sender As Object, e As EventArgs) Handles cmbweftshade.Enter
        Try
            If cmbweftshade.Text.Trim = "" Then FILLCOLOR(cmbweftshade, "", "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbweftshade_Validating(sender As Object, e As CancelEventArgs) Handles cmbweftshade.Validating
        Try
            If cmbweftshade.Text.Trim <> "" Then COLORVALIDATE(cmbweftshade, e, Me, "", "")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBWARPSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBWARPSHADE.Validating
        Try
            If CMBWARPSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBWARPSHADE, e, Me, "", "")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSELSHADE_Validating(sender As Object, e As CancelEventArgs) Handles CMBSELSHADE.Validating
        Try
            If CMBSELSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSELSHADE, e, Me, "", "")
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
            If CMBLOOM.Text.Trim = "" Then FILLLOOM(CMBLOOM, CMBNAME.Text.Trim, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMBLOOM_Validating(sender As Object, e As CancelEventArgs) Handles CMBLOOM.Validating
        Try
            If CMBLOOM.Text.Trim <> "" Then LOOMVALIDATE(CMBLOOM, CMBNAME.Text.Trim, e, Me)
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
        TXTENDPERINCH.Text = 0
        txttotaldentsrepeat.Text = 0.00
        TXTTOTALENDS.Text = 0.00
        TXTTOTALMAINENDS.Text = 0.00
        txtxvalue.Text = 0.00

        ''================ MAIN CALCULATIONS =================
        'If TXTLEFTSEL.Text <> "" And TXTREEDSPACE.Text <> "" And TXTRIGHTSEL.Text <> "" Then
        '    TXTMAINRS.Text = Format(Val(TXTREEDSPACE.Text) - Val(TXTLEFTSEL.Text) - Val(TXTRIGHTSEL.Text), "0.00")
        'End If
        'If TXTREED.Text <> "" Then
        '    TXTDENTS.Text = Format(Val(TXTREED.Text) / 2, "0.00")
        'End If
        'If TXTDENTS.Text <> "" And TXTMAINRS.Text <> "" Then
        '    TXTTOTALDENTSMAIN.Text = Format(Val(TXTDENTS.Text) * Val(TXTMAINRS.Text), "0.00")
        'End If
        ''================ SELVEDGE DENTS =================
        'If TXTLEFTSEL.Text <> "" And TXTDENTS.Text <> "" Then
        '    TXTLEFTSELDENTS.Text = Format(Val(TXTLEFTSEL.Text) * Val(TXTDENTS.Text), "0.00")
        'End If
        'If TXTRIGHTSEL.Text <> "" And TXTDENTS.Text <> "" Then
        '    TXTRIGHTSELDENTS.Text = Format(Val(TXTRIGHTSEL.Text) * Val(TXTDENTS.Text), "0.00")
        'End If
        'If TXTLEFTSELDENTS.Text <> "" And TXTRIGHTSELDENTS.Text <> "" Then
        '    TXTTOTALSELVEDGEDENTS.Text = Format(Val(TXTLEFTSELDENTS.Text) + Val(TXTRIGHTSELDENTS.Text), "0.00")
        'End If
        ''================ TOTAL DENTS =================
        'If TXTTOTALDENTSMAIN.Text <> "" And TXTTOTALSELVEDGEDENTS.Text <> "" Then
        '    TXTTOTALDENTS.Text = Format(Val(TXTTOTALDENTSMAIN.Text) + Val(TXTTOTALSELVEDGEDENTS.Text), "0.00")
        'End If
        ''================ SELVEDGE ENDS =================
        'If TXTTHREADPERDENT.Text <> "" And TXTLEFTSELDENTS.Text <> "" Then
        '    TXTLEFTSELTOTALENDS.Text =
        'Format(Val(TXTTHREADPERDENT.Text) * Val(TXTLEFTSELDENTS.Text), "0.00")
        'End If
        'If TXTTHREADPERDENT.Text <> "" And TXTRIGHTSELDENTS.Text <> "" Then
        '    TXTRIGHTSELTOTALENDS.Text =
        'Format(Val(TXTTHREADPERDENT.Text) * Val(TXTRIGHTSELDENTS.Text), "0.00")
        'End If
        'If TXTLEFTSELTOTALENDS.Text <> "" And TXTRIGHTSELTOTALENDS.Text <> "" Then
        '    TXTTOTALSELENDS.Text =
        'Format(Val(TXTLEFTSELTOTALENDS.Text) + Val(TXTRIGHTSELTOTALENDS.Text), "0.00")
        'End If
        ''================ FINAL TOTAL ENDS (CORRECT) =================
        'If TXTTOTALDENTSMAIN.Text <> "" And TXTTHREADPERDENT.Text <> "" And TXTTOTALSELENDS.Text <> "" Then
        '    Dim mainDents As Double = Val(TXTTOTALDENTSMAIN.Text)
        '    Dim endsPerDent As Double = Val(TXTTHREADPERDENT.Text)   ' eg: 4
        '    Dim selvedgeEnds As Double = Val(TXTTOTALSELENDS.Text)
        '    Dim mainEnds As Double = mainDents * endsPerDent
        '    Dim totalEnds As Double = Math.Ceiling(mainEnds + selvedgeEnds)
        '    TXTTOTALENDS.Text = totalEnds.ToString()
        'End If
        If TXTLEFTSEL.Text <> "" And TXTREEDSPACE.Text <> "" Then TXTMAINRS.Text = Format(Val(TXTREEDSPACE.Text) - (Val(TXTLEFTSEL.Text) + Val(TXTRIGHTSEL.Text)), "0.00")
        If TXTREED.Text <> "" Then TXTDENTS.Text = Format(Val(TXTREED.Text) / 2, "0.00")
        If TXTDENTS.Text <> "" And TXTMAINRS.Text <> "" Then TXTTOTALDENTSMAIN.Text = Format(Val(TXTDENTS.Text) * Val(TXTMAINRS.Text), "0.00")
        If TXTLEFTSEL.Text <> "" And TXTDENTS.Text <> "" Then TXTLEFTSELDENTS.Text = Format(Val(TXTLEFTSEL.Text) * Val(TXTDENTS.Text), "0.00")
        If TXTDENTS.Text <> "" And TXTRIGHTSEL.Text <> "" Then TXTRIGHTSELDENTS.Text = Format(Val(TXTRIGHTSEL.Text) * Val(TXTDENTS.Text), "0.00")
        If TXTRIGHTSELDENTS.Text <> "" And TXTLEFTSELDENTS.Text <> "" Then TXTTOTALSELVEDGEDENTS.Text = Format(Val(TXTLEFTSELDENTS.Text) + Val(TXTRIGHTSELDENTS.Text), "0.00")
        If TXTTOTALDENTSMAIN.Text <> "" And TXTTOTALSELVEDGEDENTS.Text <> "" Then TXTTOTALDENTS.Text = Format(Val(TXTTOTALDENTSMAIN.Text) + Val(TXTTOTALSELVEDGEDENTS.Text), "0.00")
        'If TXTLEFTSELENDS.Text <> "" And TXTLEFTSELDENTS.Text <> "" Then TXTLEFTSELTOTALENDS.Text = Format(Val(TXTTOTALDRAWENDS.Text) * Val(TXTLEFTSELDENTS.Text), "0.00")
        'AS PER RANJAN
        If TXTLEFTSELENDS.Text <> "" And TXTLEFTSELDENTS.Text <> "" Then TXTLEFTSELTOTALENDS.Text = Format(Val(TXTLEFTSELENDS.Text) * Val(TXTLEFTSELDENTS.Text), "0.00")

        'If TXTRIGHTSELENDS.Text <> "" And TXTRIGHTSELDENTS.Text <> "" Then TXTRIGHTSELTOTALENDS.Text = Format(Val(TXTTOTALDRAWENDS.Text) * Val(TXTRIGHTSELDENTS.Text), "0.00")
        'RANJAN
        If TXTRIGHTSELENDS.Text <> "" And TXTRIGHTSELDENTS.Text <> "" Then TXTRIGHTSELTOTALENDS.Text = Format(Val(TXTRIGHTSELENDS.Text) * Val(TXTRIGHTSELDENTS.Text), "0.00")

        If TXTLEFTSELTOTALENDS.Text <> "" And TXTRIGHTSELTOTALENDS.Text <> "" Then TXTTOTALSELENDS.Text = Format(Val(TXTLEFTSELTOTALENDS.Text) + Val(TXTRIGHTSELTOTALENDS.Text), "0.00")
        If TXTTOTALDRAWDENTS.Text <> "" And TXTTOTALDENTS.Text <> "" Then txttotaldentsrepeat.Text = Format(Val(TXTTOTALDENTS.Text) / Val(TXTTOTALDRAWDENTS.Text), "0.00")
        'new code 
        'If TXTTOTALDENTSMAIN.Text <> "" And TXTTHREADPERDENT.Text <> "" Then TXTTOTALDENTSMAIN.Text = Format(Val(TXTTOTALDENTSMAIN.Text) * Val(TXTTHREADPERDENT.Text), "0.00")
        'If TXTTOTALDENTSMAIN.Text <> "" And TXTTOTALSELENDS.Text <> "" Then TXTTOTALENDS.Text = Format(Val(TXTTOTALDENTSMAIN.Text) + Val(TXTTOTALSELENDS.Text), "0.00")

        If txttotaldentsrepeat.Text <> "" And TXTTOTALDRAWENDS.Text <> "" Then
            Dim totalDents As Double = Val(txttotaldentsrepeat.Text)
            Dim totalDrawEnds As Double = Val(TXTTOTALDRAWENDS.Text)
            Dim result As Double = Format(Val(totalDents) * Val(totalDrawEnds), "0.00")
            TXTTOTALENDS.Text = Format((result), "0.00")
        End If
        ' If TXTTOTALENDS.Text <> "" And TXTTOTALENDS.Text > 0 And TXTREEDSPACE.Text <> "" Then TXTENDPERINCH.Text = Format(Val(TXTTOTALENDS.Text) / Val(TXTREEDSPACE.Text), "0")
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
                        row.Cells(FWT.Index).Value = Format(((Val(TXTPICKS.Text) / Val(TXTTOTALWEFTPE.Text.Trim)) * Val(row.Cells(FPE.Index).Value) * Val(TXTREEDSPACE.Text.Trim) * Val(row.Cells(FDENIER.Index).Value) * Val(TXTWEFTTL.Text)) / 9000000, "0.000")
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
                    row.Cells(FENDS.Index).Value = Format(((Val(TXTREEDSPACE.Text) * Val(TXTPICKS.Text)) / Val(TXTTOTALWEFTPE.Text.Trim)) * Val(row.Cells(FPE.Index).Value), "0.00")
                Next
            End If
        End If
        TXTFWT.Text = 0.000
        TXTFINISHWT.Text = 0.000
        TXTGSM.Text = 0
        TXTGLM.Text = 0.000
        TXTFWT.Text = Format(Val(TXTTOTALWARPWT.Text) + Val(TXTTOTALWEFTWT.Text) + Val(TXTTOTALSELWT.Text), "0.000")
        If TXTSHRINKAGEPER.Text <> "" Then TXTFINISHWT.Text = Format(Val(TXTFWT.Text) * (1 + (Val(TXTSHRINKAGEPER.Text) / 100) * 0.6), "0.000")
        If TXTNOOFPCS.Text <> "" And TXTPCSL.Text <> "" Then
            Dim pcs As Double = Val(TXTNOOFPCS.Text)
            Dim pcsl As Double = Val(TXTPCSL.Text)
            Dim result As Double = pcs * pcsl
            TXTBEAMMTRS.Text = Format(Val(result), "0.00")
        End If
        If TXTFWT.Text <> "" And Val(TXTFWIDTH.Text) > 0 Then
            TXTGSM.Text = Format(((Val(TXTFWT.Text) * 39.37) / (Val(TXTFWIDTH.Text) * 10)) * 100, "0")
        End If
        If TXTGSM.Text <> "" Then
            TXTGLM.Text = Format((Val(TXTGSM.Text) * Val(TXTFWT.Text)) / (39.37 / 10), "0.000")
            TXTGLM.Text = TXTFINISHWT.Text
        End If
        '************* EPI ******************
        If Val(TXTTOTALDRAWDENTS.Text) > 0 And TXTREED.Text <> "" Then
            Dim x As Decimal = TXTREED.Text.Trim / 2
            Dim I As Decimal = Format(Val(x / TXTTOTALDRAWDENTS.Text.Trim), "0.00")
            TXTENDPERINCH.Text = Format(Val(I * TXTTOTALDRAWENDS.Text.Trim), "0.00")
            If TXTREEDSPACE.Text.Trim <> "" And TXTFWIDTH.Text.Trim <> "" Then
                Dim y As Decimal = TXTENDPERINCH.Text.Trim * TXTREEDSPACE.Text.Trim
                TXTFEPI.Text = Format(Val(y / TXTFWIDTH.Text.Trim), "0.00")
            End If
        End If
        '************* PPI ******************
        If TXTSHRINKAGEPER.Text <> "" Then
            Dim X As Decimal = TXTPICKS.Text.Trim * (TXTSHRINKAGEPER.Text.Trim / 100)
            TXTFPPI.Text = Format(Val(X + TXTPICKS.Text.Trim), "0")
        End If
        GETSELPE()
        GETWARPPE()
        GETWEFTPE()
        BLENDPERCENTAGE(GRIDWARP, WQUALITY.Index, WWT.Index, GRIDWEFT, FQUALITY.Index, FWT.Index)
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
            If row.Cells(WPTR2.Index).EditedFormattedValue IsNot DBNull.Value Then
                PE = PE + Val(row.Cells(WPTR2.Index).EditedFormattedValue)
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
            If row.Cells(SPTR2.Index).EditedFormattedValue IsNot DBNull.Value Then
                PE = PE + Val(row.Cells(SPTR2.Index).EditedFormattedValue)
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
            If row.Cells(FPTR2.Index).EditedFormattedValue IsNot DBNull.Value Then
                PE = PE + Val(row.Cells(FPTR2.Index).EditedFormattedValue)
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
    'Sub EDITWARPPATTERNROW()
    '    If GRIDWARPPATTERN.CurrentRow IsNot Nothing Then
    '        If GRIDWARPPATTERN.CurrentRow.Index >= 0 Then
    '            TEMPWPROW = GRIDWARPPATTERN.CurrentRow.Index
    '            TXTWARPGSRNO.Text = GRIDWARPPATTERN.Item(WPSRNO.Index, TEMPWPROW).Value
    '            TXTGRIDPE.Text = GRIDWARPPATTERN.Item(WPENDS.Index, TEMPWPROW).Value
    '            CMBGRIDSYM.Text = GRIDWARPPATTERN.Item(WPSYM.Index, TEMPWPROW).Value
    '            GRIDWPDOUBLECLICK = True
    '            TXTGRIDPE.Focus()
    '        End If
    '    End If
    'End Sub
    Sub EDITSELVEDGEROW()
        If GRIDSELVEDGE.CurrentRow IsNot Nothing Then
            If GRIDSELVEDGE.CurrentRow.Index >= 0 Then
                TEMPSELROW = GRIDSELVEDGE.CurrentRow.Index
                TXTSELSRNO.Text = GRIDSELVEDGE.Item(SSRNO.Index, TEMPSELROW).Value
                CMBSELGSYM.Text = GRIDSELVEDGE.Item(SSYM.Index, TEMPSELROW).Value
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
                CMBSELGSYM.Focus()
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
    'Sub EDITWEFTPATTERNROW()
    '    If GRIDWEFTPATTERN.CurrentRow IsNot Nothing Then
    '        If GRIDWEFTPATTERN.CurrentRow.Index >= 0 Then
    '            TEMPWEFTPROW = GRIDWEFTPATTERN.CurrentRow.Index
    '            TXTWEFTGRIDSRNO.Text = GRIDWEFTPATTERN.Item(FPSRNO.Index, TEMPWEFTPROW).Value
    '            TXTWEFTGRIDPE.Text = GRIDWEFTPATTERN.Item(FPENDS.Index, TEMPWEFTPROW).Value
    '            CMBWEFTGRIDSYMBOL.Text = GRIDWEFTPATTERN.Item(FPSYM.Index, TEMPWEFTPROW).Value
    '            GRIDWEFTPDOUBLECLICK = True
    '            TXTWEFTGRIDPE.Focus()
    '        End If
    '    End If
    'End Sub
    'Sub EDITSELVEDGEPATTERNROW()
    '    If GRIDSELVEDGEPATTERN.CurrentRow IsNot Nothing Then
    '        If GRIDSELVEDGEPATTERN.CurrentRow.Index >= 0 Then
    '            TEMPSELPROW = GRIDSELVEDGEPATTERN.CurrentRow.Index
    '            TXTSELGSRNO.Text = GRIDSELVEDGEPATTERN.Item(SPSRNO.Index, TEMPSELPROW).Value
    '            TXTSELGPE.Text = GRIDSELVEDGEPATTERN.Item(SPENDS.Index, TEMPSELPROW).Value
    '            CMBSELGSYM.Text = GRIDSELVEDGEPATTERN.Item(SPSYM.Index, TEMPSELPROW).Value.ToString
    '            GRIDSELPDOUBLECLICK = True
    '            TXTSELGPE.Focus()
    '        End If
    '    End If
    'End Sub


    'Private Sub GRIDWARPPATTERN_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWARPPATTERN.CellDoubleClick
    '    Try
    '        EDITWARPPATTERNROW()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

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
    'Private Sub GRIDWEFTPATTERN_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWEFTPATTERN.CellDoubleClick
    '    EDITWARPPATTERNROW()
    'End Sub
    Private Sub GRIDSELVEDGE_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDSELVEDGE.CellDoubleClick
        EDITSELVEDGEROW()
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
                    Dim cellValue As String = e.FormattedValue.ToString()
                    ' Allow empty value if needed
                    If String.IsNullOrWhiteSpace(e.FormattedValue.ToString()) Then Return

                    'THIS GIVE ERROR ON VALIDATION
                    '' Validate numeric input
                    'Dim val As Decimal
                    'If Not Decimal.TryParse(e.FormattedValue.ToString(), val) Then
                    '    MessageBox.Show("Please enter a valid numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    '    e.Cancel = True
                    'End If
                    'If Not IsNumericOrZero(cellValue) Then
                    '    MessageBox.Show("Please enter a valid number (0 is allowed).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    '    e.Cancel = True
                    '    Return
                    'End If
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
            cmdbtn1_Click(sender, e, GRIDDRAWING)
            ' TOTALDRAWDENTS(GRIDDRAWING)
            CALC()
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub GRIDSELVEDGEPATTERN_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
    '    EDITSELVEDGEPATTERNROW()
    'End Sub

    Private Sub CMBWEFTGRIDSYMBOL_Validated(sender As Object, e As EventArgs) Handles CMBWEFTGRIDSYMBOL.Validated
        Try
            CMBWEFTYARNQUALITY.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMBWARPQUALITY_Validated(sender As Object, e As EventArgs) Handles CMBWARPQUALITY.Validated
        Try
            If GRIDDOUBLECLICK = False Then
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
            End If
            'If CMBGRIDSYM.Text <> "" Then
            '    For Each symRow As DataGridViewRow In GRIDWARP.Rows
            '        If symRow.IsNewRow Then Continue For
            '        Dim symValue As String = symRow.Cells(WSYM.Index).Value?.ToString()
            '        If symValue = CMBGRIDSYM.Text.Trim And GRIDDOUBLECLICK = False Then
            '            MessageBox.Show("Multiple Sym Not Allowed.")
            '            CMBGRIDSYM.Focus()
            '        End If
            '    Next
            'End If

            If CMBWARPQUALITY.Text <> "" And CMBGRIDSYM.Text <> "" Then
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
                If GRIDSELDOUBLECLICK = True Then
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles cmdcopypattern.Click
        Try
            If MsgBox("Wish to Copy Weft Pattern Grid?", MsgBoxStyle.YesNo) = vbYes Then
                CopyGridPatternEntries(GRIDWARPPATTERN, GRIDWEFTPATTERN)
                TOTALWEFTPATTERN()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CopyGridPatternEntries(sourceGrid As DataGridView, targetGrid As DataGridView)
        ' Clear existing rows in target if needed
        targetGrid.Rows.Clear()

        ' Ensure the target grid has the same number of columns as the source
        If targetGrid.Columns.Count < sourceGrid.Columns.Count Then
            ' Add missing columns to target grid
            For i As Integer = targetGrid.Columns.Count To sourceGrid.Columns.Count - 1
                targetGrid.Columns.Add(sourceGrid.Columns(i).Name, sourceGrid.Columns(i).HeaderText)
            Next
        End If

        ' Loop through each non-new row in source
        For Each srcRow As DataGridViewRow In sourceGrid.Rows
            If Not srcRow.IsNewRow Then
                ' Create a new row in target grid
                Dim targetRowIndex As Integer = targetGrid.Rows.Add()
                Dim targetRow As DataGridViewRow = targetGrid.Rows(targetRowIndex)

                ' Copy cell values from source to target
                For i As Integer = 0 To sourceGrid.Columns.Count - 1
                    If targetRow.Cells.Count > i Then
                        targetRow.Cells(i).Value = srcRow.Cells(i).Value
                    End If
                Next
            End If
        Next
    End Sub
    Private Sub CopyGridEntries(sourceGrid As DataGridView, targetGrid As DataGridView)

        ' Clear existing rows
        targetGrid.Rows.Clear()

        ' Clear columns but keep structure simple if needed
        targetGrid.Columns.Clear()

        ' Add ONLY non-WARP columns
        For Each col As DataGridViewColumn In sourceGrid.Columns
            If Not col.Name.StartsWith("WARP", StringComparison.OrdinalIgnoreCase) Then
                targetGrid.Columns.Add(col.Name, col.HeaderText)
            End If
        Next

        ' Copy ONLY non-WARP column data
        For Each srcRow As DataGridViewRow In sourceGrid.Rows
            If Not srcRow.IsNewRow Then
                Dim newRowIndex As Integer = targetGrid.Rows.Add()
                Dim trgRow As DataGridViewRow = targetGrid.Rows(newRowIndex)

                For Each col As DataGridViewColumn In sourceGrid.Columns
                    If Not col.Name.StartsWith("WARP", StringComparison.OrdinalIgnoreCase) Then
                        trgRow.Cells(col.Name).Value = srcRow.Cells(col.Name).Value
                    End If
                Next

            End If
        Next

    End Sub




    Private Sub GRIDWARPPATTERN_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWARPPATTERN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDWARPPATTERN.CurrentRow.Cells(WPENDS.Index).Value <> "" Then
                If GRIDWPDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDWARPPATTERN.Rows.RemoveAt(GRIDWARPPATTERN.CurrentRow.Index)
                TOTALWARPPATTERN()
                TOTALWARP()
                getsrno(GRIDWARPPATTERN)
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
            If e.KeyCode = Keys.Delete And GRIDWEFTPATTERN.CurrentRow.Cells(FPENDS.Index).Value <> "" Then
                If GRIDWEFTPDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDWEFTPATTERN.Rows.RemoveAt(GRIDWEFTPATTERN.CurrentRow.Index)
                TOTALWEFTPATTERN()
                TOTALWEFT()
                getsrno(GRIDWEFTPATTERN)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSELYARNQUALITY_Validated(sender As Object, e As EventArgs) Handles CMBSELYARNQUALITY.Validated
        Try
            If GRIDSELDOUBLECLICK = False Then
                If CMBSELYARNQUALITY.Text.Trim <> "" And CMBSELGSYM.Text.Trim <> "" Then
                    For Each symRow As DataGridViewRow In GRIDSELVEDGE.Rows
                        If symRow.IsNewRow Then Continue For
                        Dim symValue As String = symRow.Cells(SSYM.Index).Value?.ToString()
                        If symValue = CMBSELGSYM.Text.Trim And GRIDDOUBLECLICK = False Then
                            MessageBox.Show("Multiple Sym Not Allowed.")
                            CMBSELGSYM.Focus()
                            Exit Sub
                        End If
                    Next
                Else
                    MsgBox("Fill Yarn Quality OR Symbol")
                End If
            End If
            If CMBSELYARNQUALITY.Text <> "" And CMBSELGSYM.Text.Trim <> "" Then
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
                'DesignCardMaster_Load(sender, e)
                SHOWDATA()
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
                'DesignCardMaster_Load(sender, e)
                SHOWDATA()
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
        If MsgBox("Wish to Print Label Print For This Design Card ?", MsgBoxStyle.YesNo) = vbYes Then
            Dim OBJCARD As New DesignCardDesign
            OBJCARD.MdiParent = MDIMain
            OBJCARD.FRMSTRING = "DESIGNCARDLBL"
            OBJCARD.FORMULA = "{DESIGNCARD_LBLPRINT.DESIGN_CARDNO}=" & Val(txtcardno.Text.Trim) & " and {DESIGNCARD_LBLPRINT.DESIGN_YEARID}=" & YearId
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


            If CMBWEFTYARNQUALITY.Text <> "" And CMBWEFTGRIDSYMBOL.Text <> "" Then
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


            If e.ColumnIndex = FPSYM.Index Then
                If e.FormattedValue IsNot Nothing Then
                    GRIDWEFTPATTERN.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = e.FormattedValue.ToString().ToUpper()
                End If
            End If
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
            If GRIDWEFTPATTERN.Columns(e.ColumnIndex).Name = "FPENDS" OrElse GRIDWEFTPATTERN.Columns(e.ColumnIndex).Name = "FPSYM" Then

                Dim cellValue As String = e.FormattedValue.ToString()

                'If GRIDWEFTPATTERN.Columns(e.ColumnIndex).Name = "FPENDS" Then
                '    ' Allow empty value if needed
                '    If String.IsNullOrWhiteSpace(e.FormattedValue.ToString()) Then Return

                '    ' Validate numeric input
                '    Dim val As Decimal
                '    If Not Decimal.TryParse(e.FormattedValue.ToString(), val) Then
                '        MessageBox.Show("Please enter a valid numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '        e.Cancel = True
                '    End If
                'End If
                If GRIDWEFTPATTERN.Columns(e.ColumnIndex).Name = "FPSYM" Then
                    ' Allow empty values (if you want that), or you can set this to prevent empty values
                    If String.IsNullOrWhiteSpace(e.FormattedValue.ToString()) Then Return

                    ' Validate that the value contains only alphabetic characters
                    If Not IsAlphaOnly(e.FormattedValue.ToString()) Then
                        MessageBox.Show("Please enter only alphabetic characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        e.Cancel = True
                    End If
                End If

            End If
            Button2_Click(sender, e)
            COPYWEFTSYM()
            CALC()
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLOSESEL_Click_1(sender As Object, e As EventArgs) Handles CMDCLOSESEL.Click
        Try
            If GRIDSELVEDGE.RowCount >= 0 And CMBSELYARNQUALITY.Text <> "" And CMBSELGSYM.Text <> "" Then
                fillselvedgegrid()
            End If
            CMBSELGSYM.Focus()
            GBSSHADEDETAILS.Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWARPPATTERN_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDWARPPATTERN.CellValidating
        Try
            Dim dgv As DataGridView = CType(sender, DataGridView)

            If e.ColumnIndex = WPSYM.Index Then
                If e.FormattedValue IsNot Nothing Then
                    GRIDWARPPATTERN.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = e.FormattedValue.ToString().ToUpper()
                End If
            End If
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
            If GRIDWARPPATTERN.Columns(e.ColumnIndex).Name = "WPENDS" OrElse GRIDWARPPATTERN.Columns(e.ColumnIndex).Name = "WPSYM" Then

                Dim cellValue As String = e.FormattedValue.ToString().Trim()

                ' If the value is empty or just spaces, show an error
                'If String.IsNullOrWhiteSpace(cellValue) Then
                '    MessageBox.Show("This field cannot be left blank.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    e.Cancel = True
                '    Return
                'End If

                If GRIDWARPPATTERN.Columns(e.ColumnIndex).Name = "WPSYM" Then
                    ' Allow empty values (if you want that), or you can set this to prevent empty values
                    If String.IsNullOrWhiteSpace(e.FormattedValue.ToString()) Then Return

                    ' Validate that the value contains only alphabetic characters
                    If Not IsAlphaOnly(e.FormattedValue.ToString()) Then
                        MessageBox.Show("Please enter only alphabetic characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        e.Cancel = True
                    End If
                End If

            End If
            COPYSYM()
            CALC()
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function IsAlphaOnly(value As String) As Boolean
        ' Returns True if all characters in the string are letters (alphabetic)
        Return value.All(Function(c) Char.IsLetter(c))
    End Function

    Public Sub CalculateTotalsForGrid(dgv As DataGridView,
                                      endsCol As String, repeatsCol As String,
                                      repeats1Col As String, repeats2Col As String,
                                      totalRepeatCol As String, totalRepeat1Col As String, totalRepeat2Col As String)

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

    Private Sub cmdbtn1_Click(sender As Object, e As EventArgs, Optional GDV As DataGridView = Nothing) Handles cmdbtn1.Click
        If GDV Is GRIDDRAWING Then
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
        ElseIf GDV Is GRIDPEG Then
            CalculateTotalsForGrid(GRIDPEG, "PPENDS", "PPR", "PPR1", "PPR2", "PPTR", "PPTR1", "PPTR2")

            Dim totalDentsCount As Integer = CalculateTotalDents(GRIDPEG, "PPENDS", "PPR", "PPR1", "PPR2", "PPTDR", "PPTDR1", "PPTDR2")
            'TXTTOTALDRAWDENTS.Text = totalDentsCount.ToString()  ' Set total dents from function

            ' Reset TextBoxes before summing to avoid accumulation
            TXTTOTALPEG.Text = "0"
            'TXTTOTALDRAWDENTS.Text = totalDentsCount.ToString()  ' Or keep/reset accordingly

            For Each row As DataGridViewRow In GRIDPEG.Rows
                If row.IsNewRow Then Continue For

                'Dim totalRepeat2Val = If(IsDBNull(row.Cells("PPTDR2").Value), 0, Convert.ToDecimal(row.Cells("PPTDR2").Value))
                'TXTTOTALPEG.Text = (Convert.ToDecimal(TXTTOTALPEG.Text) + totalRepeat2Val).ToString()

                Dim totalDentRepeat2Val = If(IsDBNull(row.Cells("PPTDR2").Value), 0, Convert.ToDecimal(row.Cells("PPTDR2").Value))
                TXTTOTALPEG.Text = (Convert.ToDecimal(TXTTOTALPEG.Text) + totalDentRepeat2Val).ToString()
            Next

        End If
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
        Try
            If GRIDSELDOUBLECLICK = False Then
                If CMBSELGSYM.Text.Trim <> "" And CMBSELYARNQUALITY.Text.Trim <> "" Then
                    GBSSHADEDETAILS.Visible = True
                    For Each symRow As DataGridViewRow In GRIDSELVEDGE.Rows
                        If symRow.IsNewRow Then Continue For
                        Dim symValue As String = symRow.Cells(SSYM.Index).Value?.ToString()
                        If symValue = CMBSELGSYM.Text.Trim And GRIDDOUBLECLICK = False Then
                            MessageBox.Show("Multiple Sym Not Allowed.")
                            CMBSELGSYM.Focus()
                            GBSSHADEDETAILS.Visible = False
                            Exit Sub
                        End If
                    Next
                Else
                    MessageBox.Show("Please Enter Details Properly.")
                    CMBSELGSYM.Focus()
                End If

                GRIDSELDESC.RowCount = 0
                GRIDSELDESCDOUBLECLICK = False
            Else
                If GRIDSELVEDGE.Rows.Count > 0 Then
                    GRIDSELDESC.RowCount = 0
                    GRIDSELDESCDOUBLECLICK = False
                    For i As Integer = 0 To DT_SELDETAILS.Rows.Count - 1
                        If DT_SELDETAILS.Rows(i).Item("SDMAINSRNO") = Val(GRIDSELVEDGE.CurrentRow.Cells(SSRNO.Index).Value) Then
                            GRIDSELDESC.Rows.Add(DT_SELDETAILS.Rows(i).Item("SDSRNO"), DT_SELDETAILS.Rows(i).Item("SDSHADE"), DT_SELDETAILS.Rows(i).Item("SDMAINSRNO"))
                        End If
                    Next
                    GBSSHADEDETAILS.Visible = True
                    CMBSELSHADE.Focus()
                End If
            End If
            TXTSDNO.Text = GRIDSELDESC.RowCount + 1
            CMBSELSHADE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TXTREED_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTREED.KeyPress, TXTSHRINKAGEPER.KeyPress, TXTPICKS.KeyPress, TXTREEDSPACE.KeyPress, TXTWARPTL.KeyPress, TXTWEFTTL.KeyPress, TXTLEFTSELENDS.KeyPress, TXTFWIDTH.KeyPress, TXTWARPWASTAGE.KeyPress, TXTWASTAGEPER.KeyPress, TXTWPP.KeyPress, TXTNOOFPCS.KeyPress, TXTPCSL.KeyPress
        Try
            numkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLOSESEL_Validated(sender As Object, e As EventArgs) Handles CMDCLOSESEL.Validated
        GBSSHADEDETAILS.Visible = False
        TXTSELBE.Focus()
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
        If CMBSELSHADE.Text <> "" Then FILLGRIDSELDESC()
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
    Private Sub CMBWARPMILLNAME_Validated(sender As Object, e As EventArgs) Handles CMBWARPMILLNAME.Validated
        Try
            If GRIDDOUBLECLICK = False Then
                If CMBGRIDSYM.Text.Trim <> "" And CMBWARPQUALITY.Text.Trim <> "" Then
                    GBWARP.Visible = True
                    For Each symRow As DataGridViewRow In GRIDWARP.Rows
                        If symRow.IsNewRow Then Continue For
                        Dim symValue As String = symRow.Cells(WSYM.Index).Value?.ToString()
                        If symValue = CMBGRIDSYM.Text.Trim And GRIDDOUBLECLICK = False Then
                            MessageBox.Show("Multiple Sym Not Allowed.")
                            GBWARP.Visible = False
                            Exit Sub
                        End If
                    Next
                End If
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
                    GBWARP.Visible = True
                    CMBWARPSHADE.Focus()
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
                If GRIDWARPDESC.RowCount > 0 And GRIDWARP.RowCount > 0 Then
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
                Else
                    GRIDWARPDESC.Rows.RemoveAt(GRIDWARPDESC.CurrentRow.Index)
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
            TXTFDSRNO.Text = GRIDWEFTDESC.RowCount + 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWARPSHADE_Validated(sender As Object, e As EventArgs) Handles CMBWARPSHADE.Validated
        If CMBWARPSHADE.Text <> "" Then FILLGRIDWARPDESC() Else CMDWARPCLOSE.Focus()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles cmdcopymatching.Click
        Try
            If MsgBox("Wish to Copy Weft Pattern Grid?", MsgBoxStyle.YesNo) = vbYes Then
                CopyGridEntries(GRIDWARP, GRIDWEFT)
                CopyGridWithSubDetails(GRIDWARPDESC, GRIDWEFTDESC, DT_WARPDETAILS, DT_WEFTDETAILS)
                POPULATEWEFTGRID()
            End If
        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub GRIDWARPDESC_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDWARPDESC.CellDoubleClick
        Try
            EDITGRIDWARPDESCROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBWEFTMILLNAME_Validated(sender As Object, e As EventArgs) Handles CMBWEFTMILLNAME.Validated
        Try
            If GRIDWEFTDOUBLECLICK = False Then
                If CMBWEFTGRIDSYMBOL.Text <> "" And CMBWEFTYARNQUALITY.Text.Trim <> "" Then
                    GBWEFT.Visible = True
                    cmbweftshade.Focus()
                    For Each symRow As DataGridViewRow In GRIDWEFT.Rows
                        If symRow.IsNewRow Then Continue For
                        Dim symValue As String = symRow.Cells(FSYM.Index).Value?.ToString()
                        If symValue = CMBWEFTGRIDSYMBOL.Text.Trim And GRIDWEFTDOUBLECLICK = False Then
                            MessageBox.Show("Multiple Sym Not Allowed.")
                            GBWEFT.Visible = False
                            CMBWEFTGRIDSYMBOL.Focus()
                            Exit Sub
                        End If
                    Next
                    'Else
                    '    MsgBox("Fill Yarn Quality OR Symbol")
                End If

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
                    GBWEFT.Visible = True
                    cmbweftshade.Focus()
                End If
            End If
            TXTFDSRNO.Text = GRIDWEFTDESC.RowCount + 1
            'CMBWEFTMILLNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFTDESC_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDWEFTDESC.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim del As Boolean = False
                If GRIDWEFTDESC.RowCount > 0 And GRIDWEFT.RowCount > 0 Then
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
                Else
                    GRIDWEFTDESC.Rows.RemoveAt(GRIDWEFTDESC.CurrentRow.Index)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbweftshade_Validated(sender As Object, e As EventArgs) Handles cmbweftshade.Validated
        If cmbweftshade.Text <> "" Then FILLGRIDWEFTDESC()
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


    Private Sub CMDWARPCLOSE_Click(sender As Object, e As EventArgs) Handles CMDWARPCLOSE.Click
        Try

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

            If GRIDWEFT.RowCount >= 0 And CMBWEFTYARNQUALITY.Text <> "" And CMBWEFTGRIDSYMBOL.Text <> "" Then
                FILLWEFTGRID()
            End If
            GBWEFT.Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdbtn1_Click(sender As Object, e As EventArgs) Handles cmdbtn1.Click

    End Sub

    Private Sub TXTLEFTSELENDS_Validated(sender As Object, e As EventArgs) Handles TXTLEFTSELENDS.Validated
        Try
            If TXTLEFTSELENDS.Text <> "" Then TXTRIGHTSELENDS.Text = Val(TXTLEFTSELENDS.Text.Trim)
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLEFTSEL_Validated(sender As Object, e As EventArgs) Handles TXTLEFTSEL.Validated, TXTSHRINKAGEPER.Validated, TXTFWIDTH.Validated, TXTNOOFPCS.Validated, TXTREEDSPACE.Validated
        Try
            If TXTLEFTSEL.Text.Trim <> "" Then TXTRIGHTSEL.Text = TXTLEFTSEL.Text
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
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

    Sub BLENDPERCENTAGE(gridWarp As DataGridView, warpQualityIdx As Integer, warpWeightIdx As Integer, gridWeft As DataGridView, weftQualityIdx As Integer, weftWeightIdx As Integer)
        Try

            Dim fiberTotals As New Dictionary(Of String, Double)
            Dim totalWeight As Double = 0
            Dim processGrid = Sub(g As DataGridView, qualityIdx As Integer, weightIdx As Integer)
                                  For Each row As DataGridViewRow In g.Rows
                                      Dim yarnName As String = row.Cells(qualityIdx).Value.ToString()
                                      Dim yarnWeight As Double = Convert.ToDouble(row.Cells(weightIdx).Value)

                                      Dim OBJCLS As New ClsCommon()
                                      Dim DT2 As DataTable = OBJCLS.SEARCH(
                                          "YARNQUALITYMASTER.YARN_NAME AS YARNNAME, ISNULL(YARNQUALITYMASTER_COMPOSITION.YARN_PER,0) AS  YARN_PER, isnull(YARNQUALITYMASTER_1.YARN_REMARK,'') as YARNCOMPOSITIONNAME",
                                          "",
                                          "YARNQUALITYMASTER AS YARNQUALITYMASTER_1 RIGHT OUTER JOIN
                         YARNQUALITYMASTER_COMPOSITION ON YARNQUALITYMASTER_1.YARN_ID = YARNQUALITYMASTER_COMPOSITION.YARN_YARNQUALITYID RIGHT OUTER JOIN
                         YARNQUALITYMASTER ON YARNQUALITYMASTER_COMPOSITION.YARN_YEARID = YARNQUALITYMASTER.YARN_YEARID AND YARNQUALITYMASTER_COMPOSITION.YARN_ID = YARNQUALITYMASTER.YARN_ID
						 ",
                                          "And YARNQUALITYMASTER.YARN_NAME = '" & yarnName & "' AND YARNQUALITYMASTER.YARN_YEARID = " & YearId
                                      )
                                      If DT2.Rows.Count > 0 Then
                                          For Each compRow As DataRow In DT2.Rows
                                              Dim fiberName As String = compRow("YARNCOMPOSITIONNAME").ToString()
                                              Dim fiberPercent As Double = Convert.ToDouble(compRow("YARN_PER")) / 100
                                              Dim fiberWeight As Double = yarnWeight * fiberPercent

                                              If Not fiberTotals.ContainsKey(fiberName) Then
                                                  fiberTotals(fiberName) = 0
                                              End If
                                              fiberTotals(fiberName) += fiberWeight
                                          Next
                                      End If
                                      totalWeight += yarnWeight
                                  Next
                              End Sub
            processGrid(gridWarp, warpQualityIdx, warpWeightIdx)
            processGrid(gridWeft, weftQualityIdx, weftWeightIdx)

            ' Output combined result
            TXTBLENDPER.Text = ""
            For Each fiberName In fiberTotals.Keys
                Dim blendPercent As Double = (fiberTotals(fiberName) / totalWeight) * 100
                Dim blendname As String
                If blendname = "" Then
                    blendname = fiberName '& ":" & blendPercent.ToString("0")
                Else
                    blendname = blendname + " / " + fiberName ' & ":" & blendPercent.ToString("0")
                End If
                Dim blendpercentvalue As String
                If blendpercentvalue = "" Then
                    blendpercentvalue = blendPercent.ToString("0")
                Else
                    blendpercentvalue = blendpercentvalue + " / " + blendPercent.ToString("0")
                End If
                TXTBLENDPER.Text = blendname & ":" & blendpercentvalue
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCOPYCARDNO_Validated(sender As Object, e As EventArgs) Handles TXTCOPYCARDNO.Validated
        Try
            SHOWDATA(TXTCOPYCARDNO.Text.Trim)
            getmax_SO_no()
            TXTCOPYCARDNO.Enabled = False
            COPYSYM()
            COPYWEFTSYM()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDDRAWING_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDDRAWING.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDDRAWING.RowCount > 0 Then

                GRIDDRAWING.Rows.RemoveAt(GRIDDRAWING.CurrentRow.Index)
                TOTALWARP()
                getsrno(GRIDDRAWING)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWEFT_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDWEFT.CellValidating
        Try
            CALC()
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDWARP_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDWARP.CellValidating
        Try
            CALC()
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSELVEDGEPATTERN_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDSELVEDGEPATTERN.CellValidating
        Try
            Dim dgv As DataGridView = CType(sender, DataGridView)

            If e.ColumnIndex = WPSYM.Index Then
                If e.FormattedValue IsNot Nothing Then
                    GRIDSELVEDGEPATTERN.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = e.FormattedValue.ToString().ToUpper()
                End If
            End If
            If e.ColumnIndex = SPREPEAT.Index OrElse e.ColumnIndex = SPREPEAT1.Index Then ' For both repeats columns if needed
                Dim value = Convert.ToString(e.FormattedValue)
                If value IsNot Nothing AndAlso value.Trim() <> "" Then
                    Dim repeatCount As Integer
                    If Not Integer.TryParse(value, repeatCount) OrElse repeatCount < 1 Then
                        MessageBox.Show("Please enter a positive integer for repeats.")
                        e.Cancel = True
                    End If
                End If
            End If
            If GRIDSELVEDGEPATTERN.Columns(e.ColumnIndex).Name = "SPENDS" OrElse GRIDSELVEDGEPATTERN.Columns(e.ColumnIndex).Name = "SPSYM" Then

                Dim cellValue As String = e.FormattedValue.ToString()

                '' If the value is empty or just spaces, show an error
                'If String.IsNullOrWhiteSpace(cellValue) Then
                '    MessageBox.Show("This field cannot be left blank.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    e.Cancel = True
                '    Return
                'End If

                If GRIDSELVEDGEPATTERN.Columns(e.ColumnIndex).Name = "SPSYM" Then
                    ' Allow empty values (if you want that), or you can set this to prevent empty values
                    If String.IsNullOrWhiteSpace(e.FormattedValue.ToString()) Then Return

                    ' Validate that the value contains only alphabetic characters
                    If Not IsAlphaOnly(e.FormattedValue.ToString()) Then
                        MessageBox.Show("Please enter only alphabetic characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        e.Cancel = True
                    End If
                End If

            End If
            Button1_Click(sender, e)
            COPYSELSYM()
            CALC()
            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)


            If e.ColumnIndex = SPSYM.Index Then
                If e.FormattedValue IsNot Nothing Then
                    GRIDSELVEDGEPATTERN.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = e.FormattedValue.ToString().ToUpper()
                End If
            End If

            TOTALSELVEDGEPATTERN()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function IsNumericOrZero(value As String) As Boolean
        ' Check if the value is either numeric or exactly "0"
        Dim number As Decimal
        Return (Decimal.TryParse(value, number) AndAlso number <> 0) OrElse value = "0"
    End Function

    Private Sub GRIDPEG_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles GRIDPEG.CellValidating
        Try
            If GRIDPEG.RowCount > 1 Then
                'If GRIDPEG.Columns(e.ColumnIndex).Name = "PPENDS" OrElse GRIDPEG.Columns(e.ColumnIndex).Name = "SPSYM" Then

                '    Dim cellValue As String = e.FormattedValue.ToString()
                '    If GRIDPEG.Columns(e.ColumnIndex).Name = "PPENDS" Then
                '        ' Allow empty value if needed
                '        If String.IsNullOrWhiteSpace(e.FormattedValue.ToString()) Then Return

                '        ' Validate numeric input
                '        'ADDim cellValue As String = e.FormattedValue.ToString()

                '        ' Split the input based on periods
                '        Dim parts As String() = cellValue.Split(".")

                '        ' Validate each part
                '        For Each part As String In parts
                '            If Not IsNumeric(part) Then
                '                MessageBox.Show("Please enter valid numbers separated by periods (e.g., 1.2.3).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '                e.Cancel = True
                '                Return
                '            End If
                '        Next
                '    End If
                'End If
                ' Assume Shaft value is in a control called numShafts (or you can store it in a variable)
                Dim maxShaft As Integer = 0
                If Integer.TryParse(CMBSHAFTS.Text.Trim(), maxShaft) Then
                    ' maxShaft will hold the correct integer value
                Else
                    MessageBox.Show("Please select a valid shaft number.", "Error")
                    Exit Sub
                End If ' or use Integer.Parse(txtShafts.Text)

                ' Check if editing the "Ends" column by column name or index
                If GRIDPEG.Columns(e.ColumnIndex).Name = "PPENDS" Then
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
                    Dim inputValues As String = Convert.ToString(e.FormattedValue).Trim()
                    If inputValues <> "" Then
                        ' If GRIDPEGPLAN row count is LESS than GRIDPEG row count, add a row!
                        If GRIDPEGPLAN.RowCount < GRIDPEG.Rows.Count Then
                            GRIDPEGPLAN.Rows.Add()
                            ' Set SrNo for the new vertical row:
                            GRIDPEGPLAN.Rows(GRIDPEGPLAN.RowCount - 1).Cells(0).Value = GRIDPEGPLAN.RowCount.ToString()
                        End If
                    End If
                End If
                If e.ColumnIndex = PPR.Index OrElse e.ColumnIndex = PPR1.Index Then ' For both repeats columns if needed
                    Dim value = Convert.ToString(e.FormattedValue)
                    If value IsNot Nothing AndAlso value.Trim() <> "" Then
                        Dim repeatCount As Integer
                        If Not Integer.TryParse(value, repeatCount) OrElse repeatCount < 1 Then
                            MessageBox.Show("Please enter a positive Integer For repeats.")
                            e.Cancel = True
                        End If
                    End If
                End If
                cmdbtn1_Click(sender, e, GRIDPEG)
                ' TOTALDRAWDENTS(GRIDPEG)
                CALC()
                TOTAL()
                pegplan()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDPEG_DefaultValuesNeeded(sender As Object, e As DataGridViewRowEventArgs) Handles GRIDPEG.DefaultValuesNeeded
        e.Row.Cells("PPSRNO").Value = GRIDPEG.Rows.Count
    End Sub

    Private Sub GRIDPEG_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDPEG.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDPEG.RowCount > 0 Then

                GRIDPEG.Rows.RemoveAt(GRIDPEG.CurrentRow.Index)
                TOTALWARP()
                getsrno(GRIDPEG)
                GRIDPEGPLAN.RowCount = 0
                FILLPEGPLAN()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub pegplan()
        Try
            Dim planRow As Integer = -1   ' row index for GRIDPEGPLAN

            For srcRow As Integer = 0 To GRIDPEG.Rows.Count - 1

                Dim srcDgvRow As DataGridViewRow = GRIDPEG.Rows(srcRow)
                If srcDgvRow.IsNewRow Then Continue For

                ' ----- READ PPENDS SAFELY -----
                Dim pickStr As String = String.Empty
                Dim cellPP As DataGridViewCell = srcDgvRow.Cells("PPENDS")

                If cellPP IsNot Nothing AndAlso
               cellPP.Value IsNot Nothing AndAlso
               Not IsDBNull(cellPP.Value) Then

                    pickStr = cellPP.Value.ToString().Trim()
                End If

                ' Agar PPENDS blank hai, to ye row ignore kar do
                If String.IsNullOrWhiteSpace(pickStr) Then Continue For

                ' Ye GRIDPEGPLAN ki next row hai (sirf non-blank PPENDS rows ke liye)
                planRow += 1
                If planRow < 0 OrElse planRow >= GRIDPEGPLAN.RowCount Then Exit For

                ' ----- PEG LOGIC (same as before) -----
                Dim closingBracketIndex As Integer = pickStr.IndexOfAny(New Char() {")"c, "}"c, "]"c})
                If closingBracketIndex >= 0 Then
                    pickStr = pickStr.Substring(0, closingBracketIndex)
                End If

                pickStr = pickStr.Replace("(", "").Replace("{", "").Replace("[", "").Trim()

                Dim picks() As String = pickStr.Split("."c)

                For Each pickVal As String In picks
                    Dim pickNum As Integer
                    If Integer.TryParse(pickVal, pickNum) Then
                        If pickNum > 0 AndAlso pickNum < GRIDPEGPLAN.ColumnCount Then
                            GRIDPEGPLAN.Rows(planRow).Cells(pickNum).Style.BackColor = Color.Green
                            GRIDPEGPLAN.Rows(planRow).Cells(pickNum).Value = pickNum.ToString()
                        End If
                    End If
                Next

                ' ----- COPY SYM -> PPSYM (LAST COLUMN) -----
                Dim symText As String = String.Empty
                ' yaha column ka naam jo tumne GRIDPEG me rakha hai, use karo: "SYM" / "PPSYM"
                Dim symCell As DataGridViewCell = srcDgvRow.Cells(14)

                If symCell IsNot Nothing AndAlso
               symCell.Value IsNot Nothing AndAlso
               Not IsDBNull(symCell.Value) Then

                    symText = symCell.Value.ToString().Trim()
                End If

                ' PPSYM (last column index 25)
                GRIDPEGPLAN.Rows(planRow).Cells(25).Value = symText

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "pegplan error")
            Throw
        End Try
    End Sub


    Sub FILLPEGPLAN()
        Try
            ' Now 26 columns (1 Sr + 24 picks + PPSYM)
            GRIDPEGPLAN.ColumnCount = 26

            ' Determine rows based on PPENDS non-blank values
            Dim desiredRows As Integer =
            GRIDPEG.Rows.Cast(Of DataGridViewRow)().
            Count(Function(r) Not r.IsNewRow AndAlso Not String.IsNullOrWhiteSpace(Convert.ToString(r.Cells("PPENDS").Value)))

            GRIDPEGPLAN.RowCount = desiredRows

            ' Column headers
            GRIDPEGPLAN.Columns(0).HeaderText = "Sr"
            GRIDPEGPLAN.Columns(0).Width = 35

            For col As Integer = 1 To 24
                GRIDPEGPLAN.Columns(col).HeaderText = col.ToString()
                GRIDPEGPLAN.Columns(col).Width = 28
            Next

            ' NEW PPSYM column (Last column)
            GRIDPEGPLAN.Columns(25).HeaderText = "PPSYM"
            GRIDPEGPLAN.Columns(25).Width = 30

            ' Row numbers
            GRIDPEGPLAN.RowTemplate.Height = 30
            For row As Integer = 0 To GRIDPEGPLAN.RowCount - 1
                GRIDPEGPLAN.Rows(row).Cells(0).Value = (row + 1).ToString()
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDSELVEDGEPATTERN_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDSELVEDGEPATTERN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSELVEDGEPATTERN.CurrentRow.Cells(SPENDS.Index).Value <> "" Then
                If GRIDWPDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                If GRIDSELVEDGEPATTERN.RowCount > 1 Then GRIDSELVEDGEPATTERN.Rows.RemoveAt(GRIDSELVEDGEPATTERN.CurrentRow.Index)
                TOTALSELVEDGEPATTERN()
                TOTALSELVEDGE()
                getsrno(GRIDSELVEDGEPATTERN)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(sender As Object, e As EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE='ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry debtors'", "Sundry debtors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(sender As Object, e As EventArgs) Handles CMBNAME.Validated
        Try
            If CMBNAME.Text.Trim <> "" Then
                'GET  AGENCT 
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(LEDGERS_1.ACC_CMPNAME,'') AS TRANSNAME, ISNULL(LEDGERS_2.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(REGISTER_NAME,'') AS REGISTERNAME, ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(LEDGERS.ACC_EXMILLLESS,0) AS EXMILLLESS,  ISNULL(LEDGERS.ACC_DISC,0) AS DISCPER,  ISNULL(LEDGERS.ACC_CDPER,0) AS CDPER, isnull(LEDGERS.ACC_CRDAYS,0) AS CRDAYS, ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO, ISNULL(TERMMASTER.TERM_NAME,'') AS TERM, ISNULL(LEDGERS.ACC_AGENTCOMM,'') AS AGENTCOMM, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME, ISNULL(LEDGERS.ACC_OVERSEAS,0) AS OVERSEAS, ISNULL(LEDGERS.ACC_TCS,0) AS TCS, ISNULL(LEDGERS.ACC_PARTYTDS,0) AS PARTYTDS, ISNULL(LEDGERS.ACC_WARNING,'') AS WARNINGTEXT, ISNULL(LEDGERS.ACC_RD,0) AS RATEDIFF, ISNULL(SALESMANMASTER.SALESMAN_NAME, '') AS SALESMAN ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMANMASTER.SALESMAN_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN TERMMASTER ON LEDGERS.ACC_TERMID = TERM_ID  LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_DELIVERYATID = CITY_ID ", " and LEDGERS.acc_cmpname = '" & CMBNAME.Text.Trim & "' and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    If CMBAGENTNAME.Text.Trim = "" Then CMBAGENTNAME.Text = DT.Rows(0).Item("AGENTNAME")
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLEFTSEL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTLEFTSEL.KeyPress, TXTNOOFPCS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTGWIDTH_Validated(sender As Object, e As EventArgs) Handles TXTGWIDTH.Validated
        Try
            If TXTGWIDTH.Text <> "" Then TXTGWIDTHCM.Text = Format(Val(TXTGWIDTH.Text.Trim) * 2.54, "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class