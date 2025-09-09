
Imports System.ComponentModel
Imports System.IO
Imports BL
Imports DevExpress.DashboardCommon.Native
Imports DevExpress.UIAutomation
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPivotGrid.Design
Public Class DesignCardMaster
    Public EDIT As Boolean              'Used for edit
    Public tempdesignno As String           'Used for edit name
    Public tempid As Integer            'Used for edit id
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPPROW As Integer
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPUPLOADROW As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If
            Dim IntResult As Integer

            Dim alParaval As New ArrayList
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
            alParaval.Add(Val(TXTREFNO.Text.Trim))
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(CMBAGENTNAME.Text.Trim)
            alParaval.Add(CMBDELAT.Text.Trim)
            alParaval.Add(Format(Convert.ToDateTime(DELDATE.Text).Date, "MM/dd/yyyy"))
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
            alParaval.Add(Format(Convert.ToDateTime(GREYDELDATE.Text).Date, "MM/dd/yyyy"))
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
                        WARPSrNo = row.Cells(WSRNO.Index).Value.ToString
                        WARPSym = row.Cells(WSYM.Index).Value.ToString
                        WARPYarnQuality = row.Cells(WQUALITY.Index).Value.ToString
                        WARPDenier = row.Cells(WDENIER.Index).Value.ToString
                        WARPMillName = row.Cells(WMILL.Index).Value.ToString
                        WARPShade = row.Cells(WSHADE.Index).Value.ToString
                        WARPPE = row.Cells(WPE.Index).Value.ToString
                        WARPBE = row.Cells(WBE.Index).Value.ToString
                        WARPTE = row.Cells(WENDS.Index).Value.ToString
                        WARPWt = row.Cells(WWT.Index).Value.ToString
                        WARPCons = row.Cells(WCONS.Index).Value.ToString
                        WARPRate = row.Cells(WRATE.Index).Value.ToString
                        WARPCost = row.Cells(WCOST.Index).Value.ToString
                    Else
                        WARPSrNo &= "|" & row.Cells(WSRNO.Index).Value.ToString
                        WARPSym &= "|" & row.Cells(WSYM.Index).Value.ToString
                        WARPYarnQuality &= "|" & row.Cells(WQUALITY.Index).Value.ToString
                        WARPDenier &= "|" & row.Cells(WDENIER.Index).Value.ToString
                        WARPMillName &= "|" & row.Cells(WMILL.Index).Value.ToString
                        WARPShade &= "|" & row.Cells(WSHADE.Index).Value.ToString
                        WARPPE &= "|" & row.Cells(WPE.Index).Value.ToString
                        WARPBE &= "|" & row.Cells(WBE.Index).Value.ToString
                        WARPTE &= "|" & row.Cells(WENDS.Index).Value.ToString
                        WARPWt &= "|" & row.Cells(WWT.Index).Value.ToString
                        WARPCons &= "|" & row.Cells(WCONS.Index).Value.ToString
                        WARPRate &= "|" & row.Cells(WRATE.Index).Value.ToString
                        WARPCost &= "|" & row.Cells(WCOST.Index).Value.ToString
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
                        WARPGRIDSRNO = row.Cells(WPSRNO.Index).Value.ToString
                        WARPGRIDPE = row.Cells(WPENDS.Index).Value.ToString
                        WARPGRIDSYM = row.Cells(WPSYM.Index).Value.ToString
                    Else
                        WARPGRIDSRNO &= "|" & row.Cells(WPSRNO.Index).Value.ToString
                        WARPGRIDPE &= "|" & row.Cells(WPENDS.Index).Value.ToString
                        WARPGRIDSYM &= "|" & row.Cells(WPSYM.Index).Value.ToString
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
                        ALOSrNo = row.Cells(SSRNO.Index).Value.ToString
                        ALOSym = row.Cells(SSYM.Index).Value.ToString
                        ALOYarnQuality = row.Cells(SQUALITY.Index).Value.ToString
                        ALODenier = row.Cells(SDENIER.Index).Value.ToString
                        ALOMillName = row.Cells(SMILL.Index).Value.ToString
                        ALOShade = row.Cells(SSHADE.Index).Value.ToString
                        ALOPE = row.Cells(SPE.Index).Value.ToString
                        ALOBE = row.Cells(SBE.Index).Value.ToString
                        ALOTE = row.Cells(SENDS.Index).Value.ToString
                        ALOWt = row.Cells(SWT.Index).Value.ToString
                        ALOCons = row.Cells(SCONS.Index).Value.ToString
                        ALORate = row.Cells(SRATE.Index).Value.ToString
                        ALOCost = row.Cells(SCOST.Index).Value.ToString
                    Else
                        ALOSrNo &= "|" & row.Cells(SSRNO.Index).Value.ToString
                        ALOSym &= "|" & row.Cells(SSYM.Index).Value.ToString
                        ALOYarnQuality &= "|" & row.Cells(SQUALITY.Index).Value.ToString
                        ALODenier &= "|" & row.Cells(SDENIER.Index).Value.ToString
                        ALOMillName &= "|" & row.Cells(SMILL.Index).Value.ToString
                        ALOShade &= "|" & row.Cells(SSHADE.Index).Value.ToString
                        ALOPE &= "|" & row.Cells(SPE.Index).Value.ToString
                        ALOBE &= "|" & row.Cells(SBE.Index).Value.ToString
                        ALOTE &= "|" & row.Cells(SENDS.Index).Value.ToString
                        ALOWt &= "|" & row.Cells(SWT.Index).Value.ToString
                        ALOCons &= "|" & row.Cells(SCONS.Index).Value.ToString
                        ALORate &= "|" & row.Cells(SRATE.Index).Value.ToString
                        ALOCost &= "|" & row.Cells(SCOST.Index).Value.ToString
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
                        WEFTSrNo = row.Cells(FSRNO.Index).Value.ToString
                        WEFTSym = row.Cells(FSYM.Index).Value.ToString
                        WEFTYarnQuality = row.Cells(FQUALITY.Index).Value.ToString
                        WEFTDenier = row.Cells(FDENIER.Index).Value.ToString
                        WEFTMillName = row.Cells(FMILL.Index).Value.ToString
                        WEFTShade = row.Cells(FSHADE.Index).Value.ToString
                        WEFTPE = row.Cells(FPE.Index).Value.ToString
                        WEFTBE = row.Cells(FBE.Index).Value.ToString
                        WEFTTE = row.Cells(FENDS.Index).Value.ToString
                        WEFTWt = row.Cells(FWT.Index).Value.ToString
                        WEFTCons = row.Cells(FCONS.Index).Value.ToString
                        WEFTRate = row.Cells(FRATE.Index).Value.ToString
                        WEFTCost = row.Cells(FCOST.Index).Value.ToString
                    Else
                        WEFTSrNo &= "|" & row.Cells(FSRNO.Index).Value.ToString
                        WEFTSym &= "|" & row.Cells(FSYM.Index).Value.ToString
                        WEFTYarnQuality &= "|" & row.Cells(FQUALITY.Index).Value.ToString
                        WEFTDenier &= "|" & row.Cells(FDENIER.Index).Value.ToString
                        WEFTMillName &= "|" & row.Cells(FMILL.Index).Value.ToString
                        WEFTShade &= "|" & row.Cells(FSHADE.Index).Value.ToString
                        WEFTPE &= "|" & row.Cells(FPE.Index).Value.ToString
                        WEFTBE &= "|" & row.Cells(FBE.Index).Value.ToString
                        WEFTTE &= "|" & row.Cells(FENDS.Index).Value.ToString
                        WEFTWt &= "|" & row.Cells(FWT.Index).Value.ToString
                        WEFTCons &= "|" & row.Cells(FCONS.Index).Value.ToString
                        WEFTRate &= "|" & row.Cells(FRATE.Index).Value.ToString
                        WEFTCost &= "|" & row.Cells(FCOST.Index).Value.ToString
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
                        WEFTTRSrNo = row.Cells(FSRNO.Index).Value.ToString
                        WEFTTRPE = row.Cells(FPE.Index).Value.ToString
                        WEFTTRSym = row.Cells(FPSYM.Index).Value.ToString
                    Else
                        WEFTTRSrNo &= "|" & row.Cells(FSRNO.Index).Value.ToString
                        WEFTTRPE &= "|" & row.Cells(FPE.Index).Value.ToString
                        WEFTTRSym &= "|" & row.Cells(FPSYM.Index).Value.ToString
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
    Sub clear()

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
        'GRID WARP
        GRIDWARP.Rows.Clear()
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
        Dim DT As DataTable = OBJCMN.SEARCH("DESIGN_NO", "", " DESIGNMASTER ", " and DESIGN_cmpid = " & CmpId & " and DESIGN_locationid = " & Locationid & " and DESIGN_yearid = " & YearId)
        If DT.Rows.Count > 0 Then
            DT.DefaultView.Sort = "DESIGN_NO"
            CMBDESIGNNO.DataSource = DT
            CMBDESIGNNO.DisplayMember = "DESIGN_NO"
            CMBDESIGNNO.Text = tempdesignno
        End If
        FILLDESIGN(CMBCOPYDESIGN, EDIT)
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
        If CMBAGENTNAME.Text.Trim = "" Then FILLNAME(CMBAGENTNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
        If CMBDELAT.Text.Trim = "" Then FILLNAME(CMBDELAT, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        If CMBGREYDELAT.Text.Trim = "" Then FILLNAME(CMBGREYDELAT, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        If CMBNAME.Text.Trim = "" Then FILLNAME(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Debtors' AND LEDGERS.ACC_TYPE<>'ACCOUNTS'")
    End Sub
    Sub fillgrid()

        If GRIDDOUBLECLICK = False Then
            'GRIDWARP.Rows.Add(Val(TXTWARPSRNO.Text.Trim), TXTWARPSYMBOL.Text.Trim, CMBWARPQUALITY.Text.Trim, TXTWARPDENIER.Text.Trim, CMBWARPMILLNAME.Text.Trim, TXTWARP.Text.Trim, Val(TXTWARPPE.Text.Trim), Val(TXTWARPBE.Text.Trim), Val(TXTWARPTE.Text.Trim), Val(TXTWARPWT.Text.Trim), Val(TXTWARPCONS.Text.Trim), Val(TXTWARPRATE.Text.Trim), Val(TXTWARPCOST.Text.Trim))

            'getsrno(GRIDSHADE)
        End If
    End Sub
End Class