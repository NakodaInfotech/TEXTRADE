
Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports BL
Imports DevExpress.XtraGrid.Drawing
Public Class JobOrder
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

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
    Sub clear()
        'getmax_SO_no()
        'txtfinishmethod.Clear()
        'CMBQUALITIES.Text = ""
        'CMBQUALITYTYPE.Text = ""
        DTDATE.Text = Now.Date
        CMBDESIGNNO.Text = ""
        CMBITEMNAME.Text = ""
        TXTREED.Clear()
        TXTREEDSPACE.Text = "65"
        'TXTREEDSPACECM.Clear()
        TXTPICKS.Clear()
        'TXTMAINRS.Clear()

        'SLEAVEDGE

        TXTREFNO.Clear()

        'TOTAL
        TXTTOTALWARPPE.Clear()       ' P.E. (Possible: Ends per repeat)
        TXTTOTALWARPBE.Clear()       ' B.E. (Possible: Ends for Border)
        TXTTOTALWARPTE.Clear()      ' T.E. (Possible: Ends for Total)
        TXTTOTALWARPWT.Clear()      ' Wt (Warp Weight)
        TXTTOTALWARPCONS.Clear()    ' Cons (Warp Consumption)
        TXTTOTALWARPRATE.Clear()    ' Rate (Rate per unit)
        TXTTOTALWARPCOST.Clear()    ' Cost (Warp Cost)

        'Weft Total
        TXTTOTALWEFTPE.Clear()       ' P.E. (Weft)
        TXTTOTALWEFTBE.Clear()       ' B.E. (Weft)
        TXTTOTALWEFTTE.Clear()       ' T.E. (Weft)
        TXTTOTALWEFTWT.Clear()       ' Wt (Weft Weight)
        TXTTOTALWEFTCONS.Clear()     ' Cons (Weft Consumption)
        TXTTOTALWEFTRATE.Clear()     ' Rate (Weft Rate)
        TXTTOTALWEFTCOST.Clear()     ' Cost (Weft Cost)

        'WARPMATCHING TEXTBOXES
        TXTWARPSRNO.Text = 1
        CMBGRIDSYM.Text = ""
        CMBWARPQUALITY.Text = ""
        TXTWARPDENIER.Clear()
        CMBWARPMILLNAME.Text = ""


        'WEFTMATCHING TEXTBOXES
        TXTWEFTSRNO.Text = 1
        CMBWEFTGRIDSYMBOL.Text = ""
        CMBWEFTYARNQUALITY.Text = ""
        TXTWEFTDEN.Clear()
        CMBWEFTMILLNAME.Text = ""
        TXTWEFTPE.Clear()
        TXTWEFTBE.Clear()
        TXTWEFTTE.Clear()
        TXTWEFTWT.Clear()
        TXTWEFTCONS.Clear()
        TXTWEFTRATE.Clear()
        TXTWEFTCOST.Clear()
        TXTTOTALENDS.Clear()

        'GRID WARP
        GRIDWARP.RowCount = 0


        'GRID WEFT
        GRIDWEFT.RowCount = 0


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
        'TXTCOPYCARDNO.Clear()

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
            dttable = objclsGRN.SelectDesignCard(CMBITEMNAME.Text, YearId)

            If dttable.Rows.Count > 0 Then

                For Each dr As DataRow In dttable.Rows

                    DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                    CMBITEMNAME.Text = Convert.ToString(dr("ITEMNAME").ToString)
                    CMBDESIGNNO.Text = Convert.ToString(dr("DESIGNNO").ToString)
                    '  CMBWEAVER.Text = Convert.ToString(dr("WEAVER").ToString)
                    TXTREED.Text = dr("REED").ToString
                    TXTREEDSPACE.Text = dr("REEDSPACE").ToString
                    TXTPICKS.Text = dr("PICKS").ToString
                    'TXTMAINRS.Text = dr("MAINRS").ToString

                    ' Reference and names
                    TXTREFNO.Text = dr("REFNO").ToString
                    CMBNAME.Text = Convert.ToString(dr("NAME").ToString)

                    ' Total Warp

                    TXTTOTALWARPPE.Text = Val(dr("TOTALWARPPE"))
                    TXTTOTALWARPBE.Text = Val(dr("TOTALWARPBE"))
                    TXTTOTALWARPTE.Text = Val(dr("TOTALWARPTE"))
                    TXTTOTALWARPWT.Text = Format(Val(dr("TOTALWARPWT")), "0.000")
                    TXTTOTALWARPCONS.Text = Val(dr("TOTALWARPCONS"))
                    TXTTOTALWARPRATE.Text = Val(dr("TOTALWARPRATE"))
                    TXTTOTALWARPCOST.Text = Val(dr("TOTALWARPCOST"))

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
                'cmbtype.Enabled = False

                'TOTAL()

                'warp gridmatching data serializations
                Dim dttable1 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSRNO, 0) As WARPGRIDSRNO, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSYM, '') AS WARPGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WARPYARNQUALITY, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPDENIER, 0) AS WARPDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WARPMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WARPSHADE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPPE, 0) AS WARPPE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPBE, 0) AS WARPBE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPTE, 0) AS WARPTE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPWT, 0.000) AS WARPWT, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCONS, 0) AS WARPCONS, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPRATE, 0) AS WARPRATE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCOST, 0) AS WARPCOST ", "", " DESIGNCARD_WARPMATCHING INNER JOIN YARNQUALITYMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_WARPYARNQUALITYID = YARNQUALITYMASTER.YARN_ID AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = YARNQUALITYMASTER.YARN_YEARID LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = MILLMASTER.MILL_YEARID AND MILLMASTER.MILL_ID = DESIGNCARD_WARPMATCHING.DESIGN_WARPMILLID LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = COLORMASTER.COLOR_yearid AND COLORMASTER.COLOR_id = DESIGNCARD_WARPMATCHING.DESIGN_WARPCOLORID  ", " AND  DESIGNCARD_WARPMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WARPGRIDSRNO")
                If dttable1.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable1.Rows
                        GRIDWARP.Rows.Add(Val(DTR("WARPGRIDSRNO")), DTR("WARPGRIDSYM").ToString, DTR("WARPYARNQUALITY").ToString, Format(DTR("WARPDENIER"), "0.00"), DTR("WARPMILLNAME").ToString, DTR("WARPSHADE").ToString, Format(DTR("WARPPE"), "0.00"), Format(DTR("WARPBE"), "0.00"), Format(DTR("WARPTE"), "0.00"), Format(DTR("WARPWT"), "0.000"), Format(DTR("WARPCONS"), "0.00"), Format(DTR("WARPRATE"), "0.00"), Format(DTR("WARPCOST"), "0.00"))
                    Next
                End If
                '' Warp Gridpattern data serializations
                'Dim dttable2 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_SRNO, 0) AS WARPPATTERNGRIDSRNO, ISNULL(DESIGN_WARPPE, '') AS WARPPATTERNGRIDPE, ISNULL(DESIGN_WARPSYM, '') AS WARPPATTERNGRIDSYM", "", " DESIGNCARD_WARPPATTERN  ", " AND  DESIGNCARD_WARPPATTERN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPPATTERN.DESIGN_YEARID = " & YearId & " ORDER BY WARPPATTERNGRIDSRNO")
                'If dttable2.Rows.Count > 0 Then
                '    For Each DTR As DataRow In dttable2.Rows
                '        GRIDWARPPATTERN.Rows.Add(DTR("WARPPATTERNGRIDSRNO"), DTR("WARPPATTERNGRIDPE"), DTR("WARPPATTERNGRIDSYM").ToString)
                '    Next
                'End If
                ''WARP grid shade data serializations
                'Dim dttableWARPshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_WDSRNO, 0) AS WDSRNO, ISNULL(COLORMASTER.COLOR_name,'') AS WDSHADE, ISNULL(DESIGN_WDMAINSRNO, 0) AS WDMAINSRNO", "", " DESIGNCARD_WARPSHADE LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WARPSHADE.DESIGN_WDSHADE = COLORMASTER.COLOR_id AND DESIGNCARD_WARPSHADE.DESIGN_YEARID = COLORMASTER.COLOR_yearid  ", " AND  DESIGNCARD_WARPSHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPSHADE.DESIGN_YEARID = " & YearId & " ORDER BY WDSRNO")
                'If dttableWARPshade.Rows.Count > 0 Then
                '    For Each DTR As DataRow In dttableWARPshade.Rows
                '        DT_WARPDETAILS.Rows.Add(DTR("WDSRNO"), DTR("WDSHADE"), DTR("WDMAINSRNO"))
                '    Next
                '    POPULATEGRID()
                'End If

                ''selvedge grid shade data serializations

                'Dim dttableshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_sdSRNO, 0) AS SDSRNO,ISNULL(COLORMASTER.COLOR_name,'') AS  SDSHADE, ISNULL(DESIGN_sdMAINSRNO, 0) AS SDMAINSRNO", "", " DESIGNCARD_SELVEDGESHADE LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_SELVEDGESHADE.DESIGN_SDSHADE = COLORMASTER.COLOR_id AND DESIGNCARD_SELVEDGESHADE.DESIGN_YEARID = COLORMASTER.COLOR_yearid   ", " AND  DESIGNCARD_SELVEDGESHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_SELVEDGESHADE.DESIGN_YEARID = " & YearId & " ORDER BY SDSRNO")
                'If dttableshade.Rows.Count > 0 Then
                '    For Each DTR As DataRow In dttableshade.Rows
                '        DT_SELDETAILS.Rows.Add(Val(DTR("SDSRNO")), DTR("SDSHADE").ToString, Val(DTR("SDMAINSRNO")))
                '    Next
                '    POPULATESELGRID()
                'End If

                ' Weft Grid data serialization
                Dim dttable5 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSRNO, 0) AS WEFTGRIDSRNO, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSYM, '') AS WEFTGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WEFTYARNQUALITY, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTDENIER, 0) AS WEFTDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WEFTMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WEFTSHADE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTPE, 0) AS WEFTPE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTBE, 0) AS WEFTBE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTTE, 0) AS WEFTTE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTWT, 0) AS WEFTWT, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCONS, 0) AS WEFTCONS, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTRATE, 0) AS WEFTRATE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCOST, 0) AS WEFTCOST", "", " DESIGNCARD_WEFTMATCHING LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCOLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTMILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTYARNQUALITYID = YARNQUALITYMASTER.YARN_ID   ", " AND  DESIGNCARD_WEFTMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WEFTMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WEFTGRIDSRNO")
                If dttable5.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable5.Rows
                        GRIDWEFT.Rows.Add(DTR("WEFTGRIDSRNO"), DTR("WEFTGRIDSYM").ToString, DTR("WEFTYARNQUALITY").ToString, Format(DTR("WEFTDENIER"), "0.00"), DTR("WEFTMILLNAME").ToString, DTR("WEFTSHADE").ToString, Format(DTR("WEFTPE"), "0.00"), Format(DTR("WEFTBE"), "0.00"), Format(DTR("WEFTTE"), "0.00"), Format(DTR("WEFTWT"), "0.000"), Format(DTR("WEFTCONS"), "0.00"), Format(DTR("WEFTRATE"), "0.00"), Format(DTR("WEFTCOST"), "0.00"))
                    Next
                End If


                'TOTAL()
                'CALC()
                'FILLPEGPLAN()
                'pegplan()
                ''GRIDDRAWING_CellValidating(Nothing, Nothing)
                'srno(GRIDWARP, TXTWARPSRNO)
                'srno(GRIDSELVEDGE, TXTSELSRNO)
                'srno(GRIDWEFT, TXTWEFTSRNO)
                'srno(GRIDWEFTDESC, TXTFDSRNO)
                'srno(GRIDWARPDESC, TXTWDSRNO)
                'srno(GRIDSELDESC, TXTSDNO)
                'fillMATCHINGcmb()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            Ep.Clear()

            'If Not errorvalid() Then
            '    Exit Sub
            'End If
            Dim IntResult As Integer

            Dim alParaval As New ArrayList
            alParaval.Add(Val(TXTJONO.Text.Trim))
            alParaval.Add(Format(Convert.ToDateTime(DTDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBITEMNAME.Text.Trim)
            alParaval.Add(CMBDESIGNNO.Text.Trim)

            alParaval.Add(Val(TXTREED.Text.Trim))
            alParaval.Add(Val(TXTREEDSPACE.Text.Trim))
            alParaval.Add(Val(TXTPICKS.Text.Trim))



            'party and other ledgers
            alParaval.Add(TXTREFNO.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)

            'alParaval.Add(Val(TXTORDERNO.Text.Trim))

            'TOTAL
            alParaval.Add(Val(TXTTOTALWARPPE.Text.Trim))        ' P.E. (Possible: Ends per repeat)
            alParaval.Add(Val(TXTTOTALWARPBE.Text.Trim))        ' B.E. (Possible: Ends for Border)
            alParaval.Add(Val(TXTTOTALWARPTE.Text.Trim))       ' T.E. (Possible: Ends for Total)
            alParaval.Add(Val(TXTTOTALWARPWT.Text.Trim))       ' Wt (Warp Weight)
            alParaval.Add(Val(TXTTOTALWARPCONS.Text.Trim))     ' Cons (Warp Consumption)
            alParaval.Add(Val(TXTTOTALWARPRATE.Text.Trim))     ' Rate (Rate per unit)
            alParaval.Add(Val(TXTTOTALWARPCOST.Text.Trim))     ' Cost (Warp Cost)

            'Weft Total
            alParaval.Add(Val(TXTTOTALWEFTPE.Text.Trim))        ' P.E. (Weft)
            alParaval.Add(Val(TXTTOTALWEFTBE.Text.Trim))        ' B.E. (Weft)
            alParaval.Add(Val(TXTTOTALWEFTTE.Text.Trim))        ' T.E. (Weft)
            alParaval.Add(Val(TXTTOTALWEFTWT.Text.Trim))        ' Wt (Weft Weight)
            alParaval.Add(Val(TXTTOTALWEFTCONS.Text.Trim))      ' Cons (Weft Consumption)
            alParaval.Add(Val(TXTTOTALWEFTRATE.Text.Trim))      ' Rate (Weft Rate)
            alParaval.Add(Val(TXTTOTALWEFTCOST.Text.Trim))      ' Cost (Weft Cost)
            'alParaval.Add(Val(TXTTOTALWEFTGRIDPE.Text.Trim))        ' P.E. (Repeated for field order continuity)


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




            'Dim WDSRNO As String = ""
            'Dim WDMTRS As String = ""
            'Dim WDMAINSRNO As String = ""

            'For i As Integer = 0 To DT_WARPDETAILS.Rows.Count - 1
            '    If DT_WARPDETAILS.Rows(i).Item(0) <> Nothing Then
            '        If WDSRNO = "" Then
            '            WDSRNO = Val(DT_WARPDETAILS.Rows(i).Item("WDSRNO"))
            '            WDMTRS = DT_WARPDETAILS.Rows(i).Item("WDSHADE")
            '            WDMAINSRNO = Val(DT_WARPDETAILS.Rows(i).Item("WDMAINSRNO"))
            '        Else
            '            WDSRNO = WDSRNO & "|" & Val(DT_WARPDETAILS.Rows(i).Item("WDSRNO"))
            '            WDMTRS = WDMTRS & "|" & DT_WARPDETAILS.Rows(i).Item("WDSHADE")
            '            WDMAINSRNO = WDMAINSRNO & "|" & Val(DT_WARPDETAILS.Rows(i).Item("WDMAINSRNO"))
            '        End If
            '    End If
            'Next


            'alParaval.Add(WDSRNO)
            'alParaval.Add(WDMTRS)
            'alParaval.Add(WDMAINSRNO)
            '*************************************************************************

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


            'Dim WEFTTRSrNo As String = ""
            'Dim WEFTTRPE As String = ""
            'Dim WEFTTRSym As String = ""

            'For Each row As Windows.Forms.DataGridViewRow In GRIDWEFTPATTERN.Rows
            '    If row.Cells(2).Value <> "" Then
            '        If WEFTTRSrNo = "" Then
            '            WEFTTRSrNo = Val(row.Cells(FPSRNO.Index).Value)
            '            WEFTTRPE = row.Cells(FPENDS.Index).Value.ToString
            '            WEFTTRSym = row.Cells(FPSYM.Index).Value.ToString
            '        Else
            '            WEFTTRSrNo = WEFTTRSrNo & "|" & Val(row.Cells(FPSRNO.Index).Value)
            '            WEFTTRPE = WEFTTRPE & "|" & row.Cells(FPENDS.Index).Value.ToString
            '            WEFTTRSym = WEFTTRSym & "|" & row.Cells(FPSYM.Index).Value.ToString
            '        End If
            '    End If
            'Next

            'alParaval.Add(WEFTTRSrNo)
            'alParaval.Add(WEFTTRPE)
            'alParaval.Add(WEFTTRSym)


            'Dim FDSRNO As String = ""
            'Dim FDMTRS As String = ""
            'Dim FDMAINSRNO As String = ""

            'For i As Integer = 0 To DT_WEFTDETAILS.Rows.Count - 1
            '    If DT_WEFTDETAILS.Rows(i).Item(0) <> Nothing Then
            '        If FDSRNO = "" Then
            '            FDSRNO = Val(DT_WEFTDETAILS.Rows(i).Item("FDSRNO"))
            '            FDMTRS = DT_WEFTDETAILS.Rows(i).Item("FDSHADE")
            '            FDMAINSRNO = Val(DT_WEFTDETAILS.Rows(i).Item("FDMAINSRNO"))
            '        Else
            '            FDSRNO = FDSRNO & "|" & Val(DT_WEFTDETAILS.Rows(i).Item("FDSRNO"))
            '            FDMTRS = FDMTRS & "|" & DT_WEFTDETAILS.Rows(i).Item("FDSHADE")
            '            FDMAINSRNO = FDMAINSRNO & "|" & Val(DT_WEFTDETAILS.Rows(i).Item("FDMAINSRNO"))
            '        End If
            '    End If
            'Next


            'alParaval.Add(FDSRNO)
            'alParaval.Add(FDMTRS)
            'alParaval.Add(FDMAINSRNO)

            '*************************************************************************
            '*************************************************************************



            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)



            '*************************************************************************


            alParaval.Add(TXTTOTALENDS.Text.Trim)


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
                'PRINTREPORT(txtcardno.Text.Trim)
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempdesignno)
                IntResult = objDESIGN.UPDATE()
                MsgBox("Details Updated")
                'PRINTREPORT(tempdesignno)
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
            'temptypename = cmbtype.Text.Trim
            'tempdesignno = Val(txtcardno.Text) - 1
            If CMBITEMNAME.Text <> "" Then
                EDIT = True
                'DesignCardMaster_Load(sender, e)
                ' SHOWDATA()
                Dim OBJCMN As New ClsCommon
                Dim dttable As DataTable = OBJCMN.SEARCH(" DESIGNCARD.DESIGN_CARDNO AS CARDNO, ISNULL(DESIGNCARD.DESIGN_FEPI, 0) AS FEPI, ISNULL(DESIGNCARD.DESIGN_FWIDTH, 0) AS FWIDTH, ISNULL(DESIGNCARD.DESIGN_FPPI, 0) AS FPPI, ISNULL(DESIGNCARD.DESIGN_FWT, 0) AS FWT, ISNULL(DESIGNCARD.DESIGN_DENTS, 0) AS DENTS, ISNULL(DESIGNCARD.DESIGN_TOTALDENTSMAIN, 0) AS TOTALDENTSMAIN, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEDENTS, 0) AS TOTALSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_TOTALDENTS, 0) AS TOTALDENTS, ISNULL(DESIGNCARD.DESIGN_WARPTTL, 0) AS WARPTTL,                           ISNULL(DESIGNCARD.DESIGN_WEFTTTL, 0) AS WEFTTTL, ISNULL(DESIGNCARD.DESIGN_GSM, 0) AS GSM, ISNULL(DESIGNCARD.DESIGN_SHAFTS, 0) AS SHAFTS, ISNULL(DESIGNCARD.DESIGN_TOTALWT, 0) AS TOTALWT, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGE, 0) AS LEFTSELVEDGE, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGE, 0) AS RIGHTSELVEDGE, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGEEND, 0) AS LEFTSELVEDGEEND, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGEEND, 0) AS RIGHTSELVEDGEEND, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGEDENTS, 0) AS LEFTSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGEDENTS, 0) AS RIGHTSELVEDGEDENTS, ISNULL(DESIGNCARD.DESIGN_LEFTSELVEDGETOTALEND, 0) AS LEFTSELVEDGETOTALEND, ISNULL(DESIGNCARD.DESIGN_RIGHTSELVEDGETOTALEND, 0) AS RIGHTSELVEDGETOTALEND, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEENDS, 0) AS TOTALSELVEDGEENDS, ISNULL(DESIGNCARD.DESIGN_REFNO, '') AS REFNO, ISNULL(DESIGNCARD.DESIGN_GREY, '') AS GREY, ISNULL(DESIGNCARD.DESIGN_ORDERNO, 0) AS ORDERNO, ISNULL(DESIGNCARD.DESIGN_DELDATE, '') AS DELDATE, ISNULL(DESIGNCARD.DESIGN_ORDERDATE, '') AS ORDERDATE, ISNULL(DESIGNCARD.DESIGN_MTRS, 0) AS MTRS, ISNULL(DESIGNCARD.DESIGN_NOOFPCS, 0) AS NOOFPCS, ISNULL(DESIGNCARD.DESIGN_LOOM, '') AS LOOM, ISNULL(DESIGNCARD.DESIGN_BEAMMTRS, 0) AS BEAMMTRS, ISNULL(DESIGNCARD.DESIGN_COVERFACTOR, '') AS COVERFACTOR, ISNULL(DESIGNCARD.DESIGN_EFFICIENCY, '') AS EFFICIENCY, ISNULL(DESIGNCARD.DESIGN_LOOMPROD, 0) AS LOOMPROD, ISNULL(DESIGNCARD.DESIGN_RPM, '') AS RPM, ISNULL(DESIGNCARD.DESIGN_GREYDELDATE, '') AS GREYDELDATE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPPE, 0) AS TOTALWARPPE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPBE, 0) AS TOTALWARPBE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPTE, 0) AS TOTALWARPTE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPWT, 0) AS TOTALWARPWT, ISNULL(DESIGNCARD.DESIGN_TOTALWARPCONS, 0) AS TOTALWARPCONS, ISNULL(DESIGNCARD.DESIGN_TOTALWARPRATE, 0) AS TOTALWARPRATE, ISNULL(DESIGNCARD.DESIGN_TOTALWARPCOST, 0) AS TOTALWARPCOST, ISNULL(DESIGNCARD.DESIGN_TOTALWARPGRIDPE, 0) AS TOTALWARPGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEPE, 0) AS TOTALSELVEDGEPE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEBE, 0) AS TOTALSELVEDGEBE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGETE, 0) AS TOTALSELVEDGETE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEWT, 0) AS TOTALSELVEDGEWT, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGECONS, 0) AS TOTALSELVEDGECONS, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGERATE, 0) AS TOTALSELVEDGERATE, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGECOST, 0) AS TOTALSELVEDGECOST, ISNULL(DESIGNCARD.DESIGN_TOTALSELVEDGEGRIDPE, 0) AS TOTALSELVEDGEGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTPE, 0) AS TOTALWEFTPE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTBE, 0) AS TOTALWEFTBE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTTE, 0) AS TOTALWEFTTE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTWT, 0) AS TOTALWEFTWT, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTCONS, 0) AS TOTALWEFTCONS, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTRATE, 0) AS TOTALWEFTRATE, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTCOST, 0) AS TOTALWEFTCOST, ISNULL(DESIGNCARD.DESIGN_TOTALWEFTGRIDPE, 0) AS TOTALWEFTGRIDPE, ISNULL(DESIGNCARD.DESIGN_TOTALDRAWENDS, 0) AS TOTALDRAWENDS, ISNULL(DESIGNCARD.DESIGN_TOTALDRAWDENTS, 0) AS TOTALDRAWDENTS, ISNULL(DESIGNMASTER.DESIGN_NO, 0) AS DESIGNNO, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(AGENTLEDGERS.Acc_cmpname, '') AS AGENTNAME, ISNULL(DELATLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(GDELATLEDGERS.Acc_cmpname, '') AS GREYDELIVERYAT, DESIGNCARD.DESIGN_DATE AS DATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNCARD.DESIGN_REED, 0) AS REED, ISNULL(DESIGNCARD.DESIGN_REEDSPACE, 0) AS REEDSPACE, ISNULL(DESIGNCARD.DESIGN_PICKS, 0) AS PICKS, ISNULL(DESIGNCARD.DESIGN_MAINRS, 0) AS MAINRS, ISNULL(DESIGNCARD.DESIGN_THREADPERDENT, '') AS THREADPERDENT, ISNULL(WEAVEMASTER.WEAVE_name, '') AS WEAVE, ISNULL(DESIGNCARD.DESIGN_TOTALFINISHWT, 0) AS TOTALFINISHWT, ISNULL(DESIGNCARD.DESIGN_GREYWIDTH, 0) AS GREYWIDTH, ISNULL(DESIGNCARD.DESIGN_GREYWIDTHCM,0) AS GREYWIDTHCM, ISNULL(DESIGNCARD.DESIGN_FINISHWIDTHCM,0) AS FINISHWIDTHCM, ISNULL(DESIGNCARD.DESIGN_GREYLOOMMTR,0) AS GREYLOOMMTR, ISNULL(DESIGNCARD.DESIGN_BLENDPERCENTAGE,0) AS BLENDPER, ISNULL(DESIGNCARD.DESIGN_FINISHMETHOD,'') AS FINISHMETHOD, ISNULL(DESIGNCARD.DESIGN_QUALITIES,'') AS QUALITY, ISNULL(DESIGNCARD.DESIGN_QUALITYTYPE,'') AS QUALITYTYPE, ISNULL(DESIGNCARD.DESIGN_WARPWASTAGE,0) AS WARPWASTAGE, ISNULL(DESIGNCARD.DESIGN_WASTAGEPERCENTAGE,0) AS WASTAGEPER, ISNULL(DESIGNCARD.DESIGN_SHRINKAGEPERCENTAGE,0) AS SHRINKAGEPER, ISNULL(DESIGNCARD.DESIGN_WPP,0) AS WPP, ISNULL(DESIGNCARD.DESIGN_WEAVECOST,0) AS WEAVECOST, ISNULL(DESIGNCARD.DESIGN_GREYFABRICCOST,0) AS GREYFABCOST, ISNULL(DESIGNCARD.DESIGN_FINISHFABRICCOST,0) AS FINISHFABCOST, ISNULL(DESIGNCARD.DESIGN_PRODUCTIONPERDAY,0) AS PRODDAY, ISNULL(DESIGNCARD.DESIGN_PCSL,0) AS PCSL, ISNULL(DESIGNCARD.DESIGN_REEDSPACECM,0) AS REEDSPACECM,ISNULL(DESIGNCARD.DESIGN_TOTALENDS,0) AS TOTALENDS ,ISNULL(DESIGNCARD.DESIGN_ENDPERINCH,0) AS ENDPERINCH, ISNULL(DESIGNCARD.DESIGN_TOTALPEG,0) AS TOTALPEG ", "", " DESIGNCARD LEFT OUTER JOIN WEAVEMASTER ON DESIGNCARD.DESIGN_YEARID = WEAVEMASTER.WEAVE_yearid AND DESIGNCARD.DESIGN_WEAVEID = WEAVEMASTER.WEAVE_id LEFT OUTER JOIN LEDGERS AS GDELATLEDGERS ON DESIGNCARD.DESIGN_YEARID = GDELATLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_GREYDELATID = GDELATLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DELATLEDGERS ON DESIGNCARD.DESIGN_YEARID = DELATLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_DELIVERYATID = DELATLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON DESIGNCARD.DESIGN_YEARID = AGENTLEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS ON DESIGNCARD.DESIGN_YEARID = LEDGERS.Acc_yearid AND DESIGNCARD.DESIGN_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN ITEMMASTER ON DESIGNCARD.DESIGN_YEARID = ITEMMASTER.item_yearid AND DESIGNCARD.DESIGN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN DESIGNMASTER ON DESIGNCARD.DESIGN_YEARID = DESIGNMASTER.DESIGN_yearid AND DESIGNCARD.DESIGN_ID = DESIGNMASTER.DESIGN_id  ", " AND  (DESIGNCARD.DESIGN_ITEMID = @ITEM) AND (DESIGNCARD.DESIGN_YEARID = " & YearId & ") ")
                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBITEMNAME.Text = Convert.ToString(dr("ITEMNAME").ToString)
                        CMBDESIGNNO.Text = Convert.ToString(dr("DESIGNNO").ToString)
                        '  CMBWEAVER.Text = Convert.ToString(dr("WEAVER").ToString)
                        TXTREED.Text = dr("REED").ToString
                        TXTREEDSPACE.Text = dr("REEDSPACE").ToString
                        TXTPICKS.Text = dr("PICKS").ToString
                        'TXTMAINRS.Text = dr("MAINRS").ToString

                        ' Reference and names
                        TXTREFNO.Text = dr("REFNO").ToString
                        CMBNAME.Text = Convert.ToString(dr("NAME").ToString)

                        ' Total Warp

                        TXTTOTALWARPPE.Text = Val(dr("TOTALWARPPE"))
                        TXTTOTALWARPBE.Text = Val(dr("TOTALWARPBE"))
                        TXTTOTALWARPTE.Text = Val(dr("TOTALWARPTE"))
                        TXTTOTALWARPWT.Text = Format(Val(dr("TOTALWARPWT")), "0.000")
                        TXTTOTALWARPCONS.Text = Val(dr("TOTALWARPCONS"))
                        TXTTOTALWARPRATE.Text = Val(dr("TOTALWARPRATE"))
                        TXTTOTALWARPCOST.Text = Val(dr("TOTALWARPCOST"))

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
                    'cmbtype.Enabled = False

                    'TOTAL()

                    'warp gridmatching data serializations
                    Dim dttable1 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSRNO, 0) As WARPGRIDSRNO, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSYM, '') AS WARPGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WARPYARNQUALITY, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPDENIER, 0) AS WARPDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WARPMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WARPSHADE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPPE, 0) AS WARPPE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPBE, 0) AS WARPBE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPTE, 0) AS WARPTE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPWT, 0.000) AS WARPWT, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCONS, 0) AS WARPCONS, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPRATE, 0) AS WARPRATE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCOST, 0) AS WARPCOST ", "", " DESIGNCARD_WARPMATCHING INNER JOIN YARNQUALITYMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_WARPYARNQUALITYID = YARNQUALITYMASTER.YARN_ID AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = YARNQUALITYMASTER.YARN_YEARID LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = MILLMASTER.MILL_YEARID AND MILLMASTER.MILL_ID = DESIGNCARD_WARPMATCHING.DESIGN_WARPMILLID LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = COLORMASTER.COLOR_yearid AND COLORMASTER.COLOR_id = DESIGNCARD_WARPMATCHING.DESIGN_WARPCOLORID  ", " AND  DESIGNCARD_WARPMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WARPGRIDSRNO")
                    If dttable1.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable1.Rows
                            GRIDWARP.Rows.Add(Val(DTR("WARPGRIDSRNO")), DTR("WARPGRIDSYM").ToString, DTR("WARPYARNQUALITY").ToString, Format(DTR("WARPDENIER"), "0.00"), DTR("WARPMILLNAME").ToString, DTR("WARPSHADE").ToString, Format(DTR("WARPPE"), "0.00"), Format(DTR("WARPBE"), "0.00"), Format(DTR("WARPTE"), "0.00"), Format(DTR("WARPWT"), "0.000"), Format(DTR("WARPCONS"), "0.00"), Format(DTR("WARPRATE"), "0.00"), Format(DTR("WARPCOST"), "0.00"))
                        Next
                    End If
                    '' Warp Gridpattern data serializations
                    'Dim dttable2 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_SRNO, 0) AS WARPPATTERNGRIDSRNO, ISNULL(DESIGN_WARPPE, '') AS WARPPATTERNGRIDPE, ISNULL(DESIGN_WARPSYM, '') AS WARPPATTERNGRIDSYM", "", " DESIGNCARD_WARPPATTERN  ", " AND  DESIGNCARD_WARPPATTERN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPPATTERN.DESIGN_YEARID = " & YearId & " ORDER BY WARPPATTERNGRIDSRNO")
                    'If dttable2.Rows.Count > 0 Then
                    '    For Each DTR As DataRow In dttable2.Rows
                    '        GRIDWARPPATTERN.Rows.Add(DTR("WARPPATTERNGRIDSRNO"), DTR("WARPPATTERNGRIDPE"), DTR("WARPPATTERNGRIDSYM").ToString)
                    '    Next
                    'End If
                    ''WARP grid shade data serializations
                    'Dim dttableWARPshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_WDSRNO, 0) AS WDSRNO, ISNULL(COLORMASTER.COLOR_name,'') AS WDSHADE, ISNULL(DESIGN_WDMAINSRNO, 0) AS WDMAINSRNO", "", " DESIGNCARD_WARPSHADE LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WARPSHADE.DESIGN_WDSHADE = COLORMASTER.COLOR_id AND DESIGNCARD_WARPSHADE.DESIGN_YEARID = COLORMASTER.COLOR_yearid  ", " AND  DESIGNCARD_WARPSHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPSHADE.DESIGN_YEARID = " & YearId & " ORDER BY WDSRNO")
                    'If dttableWARPshade.Rows.Count > 0 Then
                    '    For Each DTR As DataRow In dttableWARPshade.Rows
                    '        DT_WARPDETAILS.Rows.Add(DTR("WDSRNO"), DTR("WDSHADE"), DTR("WDMAINSRNO"))
                    '    Next
                    '    POPULATEGRID()
                    'End If

                    ''selvedge grid shade data serializations

                    'Dim dttableshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_sdSRNO, 0) AS SDSRNO,ISNULL(COLORMASTER.COLOR_name,'') AS  SDSHADE, ISNULL(DESIGN_sdMAINSRNO, 0) AS SDMAINSRNO", "", " DESIGNCARD_SELVEDGESHADE LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_SELVEDGESHADE.DESIGN_SDSHADE = COLORMASTER.COLOR_id AND DESIGNCARD_SELVEDGESHADE.DESIGN_YEARID = COLORMASTER.COLOR_yearid   ", " AND  DESIGNCARD_SELVEDGESHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_SELVEDGESHADE.DESIGN_YEARID = " & YearId & " ORDER BY SDSRNO")
                    'If dttableshade.Rows.Count > 0 Then
                    '    For Each DTR As DataRow In dttableshade.Rows
                    '        DT_SELDETAILS.Rows.Add(Val(DTR("SDSRNO")), DTR("SDSHADE").ToString, Val(DTR("SDMAINSRNO")))
                    '    Next
                    '    POPULATESELGRID()
                    'End If

                    ' Weft Grid data serialization
                    Dim dttable5 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSRNO, 0) AS WEFTGRIDSRNO, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSYM, '') AS WEFTGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WEFTYARNQUALITY, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTDENIER, 0) AS WEFTDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WEFTMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WEFTSHADE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTPE, 0) AS WEFTPE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTBE, 0) AS WEFTBE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTTE, 0) AS WEFTTE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTWT, 0) AS WEFTWT, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCONS, 0) AS WEFTCONS, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTRATE, 0) AS WEFTRATE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCOST, 0) AS WEFTCOST", "", " DESIGNCARD_WEFTMATCHING LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCOLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTMILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTYARNQUALITYID = YARNQUALITYMASTER.YARN_ID   ", " AND  DESIGNCARD_WEFTMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WEFTMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WEFTGRIDSRNO")
                    If dttable5.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable5.Rows
                            GRIDWEFT.Rows.Add(DTR("WEFTGRIDSRNO"), DTR("WEFTGRIDSYM").ToString, DTR("WEFTYARNQUALITY").ToString, Format(DTR("WEFTDENIER"), "0.00"), DTR("WEFTMILLNAME").ToString, DTR("WEFTSHADE").ToString, Format(DTR("WEFTPE"), "0.00"), Format(DTR("WEFTBE"), "0.00"), Format(DTR("WEFTTE"), "0.00"), Format(DTR("WEFTWT"), "0.000"), Format(DTR("WEFTCONS"), "0.00"), Format(DTR("WEFTRATE"), "0.00"), Format(DTR("WEFTCOST"), "0.00"))
                        Next
                    End If


                    'TOTAL()
                    'CALC()
                    'FILLPEGPLAN()
                    'pegplan()
                    ''GRIDDRAWING_CellValidating(Nothing, Nothing)
                    'srno(GRIDWARP, TXTWARPSRNO)
                    'srno(GRIDSELVEDGE, TXTSELSRNO)
                    'srno(GRIDWEFT, TXTWEFTSRNO)
                    'srno(GRIDWEFTDESC, TXTFDSRNO)
                    'srno(GRIDWARPDESC, TXTWDSRNO)
                    'srno(GRIDSELDESC, TXTSDNO)
                    'fillMATCHINGcmb()
                End If

            End If
            'If GRIDSELVEDGE.RowCount = 0 And tempdesignno > 1 Then
            '    txtcardno.Text = tempdesignno
            '    GoTo LINE1
            'End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
End Class