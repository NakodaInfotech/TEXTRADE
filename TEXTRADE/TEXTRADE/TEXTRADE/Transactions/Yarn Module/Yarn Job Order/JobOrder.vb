
Imports BL
Imports System.IO
Imports System.Net
Imports System.ComponentModel
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
    Sub SHOWDATA(Optional ByVal CARDNO As Integer = -1)
        '    Try
        '        clear()
        '        If USEREDIT = False And USERVIEW = False Then
        '            MsgBox("Insufficient Rights")
        '            Exit Sub
        '        End If
        '        Dim OBJCMN As New ClsCommon
        '        Dim objclsGRN As New ClsDesignCardMaster()
        '        Dim dttable As New DataTable
        '        If CARDNO > 0 Then tempdesignno = If(CARDNO = -1, -1, CARDNO)
        '        dttable = objclsGRN.SelectDesignCard(tempdesignno, YearId)

        '        If dttable.Rows.Count > 0 Then

        '            For Each dr As DataRow In dttable.Rows

        '                txtcardno.Text = tempdesignno
        '                txtcardno.ReadOnly = True

        '                DTDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
        '                CMBITEMNAME.Text = Convert.ToString(dr("ITEMNAME").ToString)
        '                CMBDESIGNNO.Text = Convert.ToString(dr("DESIGNNO").ToString)
        '                '  CMBWEAVER.Text = Convert.ToString(dr("WEAVER").ToString)
        '                TXTREED.Text = dr("REED").ToString
        '                TXTREEDSPACE.Text = dr("REEDSPACE").ToString
        '                TXTPICKS.Text = dr("PICKS").ToString
        '                TXTMAINRS.Text = dr("MAINRS").ToString
        '                TXTTHREADPERDENT.Text = dr("THREADPERDENT").ToString
        '                TXTFEPI.Text = dr("FEPI").ToString
        '                TXTFWIDTH.Text = dr("FWIDTH").ToString
        '                TXTFPPI.Text = dr("FPPI").ToString
        '                TXTFWT.Text = Format(Val(dr("FWT").ToString), "0.000")
        '                TXTDENTS.Text = dr("DENTS").ToString
        '                TXTTOTALDENTSMAIN.Text = Val(dr("TOTALDENTSMAIN"))
        '                TXTTOTALSELVEDGEDENTS.Text = Val(dr("TOTALSELVEDGEDENTS"))
        '                TXTTOTALDENTS.Text = Val(dr("TOTALDENTS"))

        '                TXTWARPTL.Text = dr("WARPTTL").ToString
        '                TXTWEFTTL.Text = dr("WEFTTTL").ToString
        '                TXTGSM.Text = dr("GSM").ToString
        '                CMBWEAVE.Text = Convert.ToString(dr("WEAVE").ToString)
        '                CMBSHAFTS.Text = Convert.ToString(dr("SHAFTS").ToString)
        '                TXTTOTALWT.Text = Format(Val(dr("TOTALWT")), "0.000")
        '                ' Selvedge fields
        '                TXTLEFTSEL.Text = dr("LEFTSELVEDGE").ToString
        '                TXTRIGHTSEL.Text = dr("RIGHTSELVEDGE").ToString
        '                TXTLEFTSELENDS.Text = dr("LEFTSELVEDGEEND").ToString
        '                TXTRIGHTSELENDS.Text = dr("RIGHTSELVEDGEEND").ToString
        '                TXTLEFTSELDENTS.Text = dr("LEFTSELVEDGEDENTS").ToString
        '                TXTRIGHTSELDENTS.Text = dr("RIGHTSELVEDGEDENTS").ToString
        '                TXTLEFTSELTOTALENDS.Text = dr("LEFTSELVEDGETOTALEND").ToString
        '                TXTRIGHTSELTOTALENDS.Text = dr("RIGHTSELVEDGETOTALEND").ToString
        '                TXTTOTALSELENDS.Text = dr("TOTALSELVEDGEENDS").ToString
        '                ' Reference and names
        '                TXTREFNO.Text = dr("REFNO").ToString
        '                CMBNAME.Text = Convert.ToString(dr("NAME").ToString)
        '                CMBAGENTNAME.Text = Convert.ToString(dr("AGENTNAME").ToString)
        '                CMBDELAT.Text = Convert.ToString(dr("DELIVERYAT").ToString)
        '                CMBGREY.Text = Convert.ToString(dr("GREY").ToString)
        '                TXTORDERNO.Text = dr("ORDERNO").ToString
        '                DELDATE.Text = Format(Convert.ToDateTime(dr("DELDATE")).Date, "dd/MM/yyyy")
        '                ORDERDATE.Text = Format(Convert.ToDateTime(dr("ORDERDATE")).Date, "dd/MM/yyyy")
        '                ' Other details
        '                TXTMTRS.Text = Val(dr("MTRS"))
        '                TXTNOOFPCS.Text = Val(dr("NOOFPCS"))
        '                CMBLOOM.Text = Convert.ToString(dr("LOOM").ToString)
        '                TXTBEAMMTRS.Text = dr("BEAMMTRS").ToString
        '                TXTCOVERFACTOR.Text = dr("COVERFACTOR").ToString
        '                TXTEFFICIENCY.Text = dr("EFFICIENCY").ToString
        '                TXTLOOMPROD.Text = dr("LOOMPROD").ToString
        '                TXTRPM.Text = dr("RPM").ToString
        '                CMBGREYDELAT.Text = Convert.ToString(dr("GREYDELIVERYAT").ToString)
        '                GREYDELDATE.Text = Format(Convert.ToDateTime(dr("GREYDELDATE")).Date, "dd/MM/yyyy")
        '                ' Total Warp

        '                TXTTOTALWARPPE.Text = Val(dr("TOTALWARPPE"))
        '                TXTTOTALWARPBE.Text = Val(dr("TOTALWARPBE"))
        '                TXTTOTALWARPTE.Text = Val(dr("TOTALWARPTE"))
        '                TXTTOTALWARPWT.Text = Format(Val(dr("TOTALWARPWT")), "0.000")
        '                TXTTOTALWARPCONS.Text = Val(dr("TOTALWARPCONS"))
        '                TXTTOTALWARPRATE.Text = Val(dr("TOTALWARPRATE"))
        '                TXTTOTALWARPCOST.Text = Val(dr("TOTALWARPCOST"))
        '                TXTTOTALWARPGRIDPE.Text = Val(dr("TOTALWARPGRIDPE"))
        '                ' Total Selvedge

        '                TXTTOTALSELPE.Text = Val(dr("TOTALSELVEDGEPE"))
        '                TXTTOTALSELBE.Text = Val(dr("TOTALSELVEDGEBE"))
        '                TXTTOTALSELTE.Text = Val(dr("TOTALSELVEDGETE"))
        '                TXTTOTALSELWT.Text = Format(Val(dr("TOTALSELVEDGEWT")), "0.000")
        '                TXTTOTALSELCONS.Text = Val(dr("TOTALSELVEDGECONS"))
        '                TXTSELTOTALRATE.Text = Val(dr("TOTALSELVEDGERATE"))
        '                TXTSELTOTALCOST.Text = Val(dr("TOTALSELVEDGECOST"))
        '                TXTTOTALSELGPE.Text = Val(dr("TOTALSELVEDGEGRIDPE"))
        '                ' Total Weft

        '                TXTTOTALWEFTPE.Text = Val(dr("TOTALWEFTPE"))
        '                TXTTOTALWEFTBE.Text = Val(dr("TOTALWEFTBE"))
        '                TXTTOTALWEFTTE.Text = Val(dr("TOTALWEFTTE"))
        '                TXTTOTALWEFTWT.Text = Format(Val(dr("TOTALWEFTWT")), "0.000")
        '                TXTTOTALWEFTCONS.Text = Val(dr("TOTALWEFTCONS"))
        '                TXTTOTALWEFTRATE.Text = Val(dr("TOTALWEFTRATE"))
        '                TXTTOTALWEFTCOST.Text = Val(dr("TOTALWEFTCOST"))
        '                TXTTOTALWEFTGRIDPE.Text = Val(dr("TOTALWEFTGRIDPE"))
        '                ' Total DRAWING 

        '                TXTTOTALDRAWENDS.Text = Val(dr("TOTALDRAWENDS"))
        '                TXTTOTALDRAWDENTS.Text = Val(dr("TOTALDRAWDENTS"))
        '                TXTTOTALPEG.Text = Val(dr("TOTALPEG"))




        '                TXTFINISHWT.Text = Format(Val(dr("TOTALFINISHWT")), "0.000")
        '                TXTGWIDTH.Text = Val(dr("GREYWIDTH"))
        '                TXTGWIDTHCM.Text = Val(dr("GREYWIDTHCM"))
        '                TXTFWIDTHCM.Text = Val(dr("FINISHWIDTHCM"))
        '                TXTWARPWASTAGE.Text = Val(dr("WARPWASTAGE"))
        '                TXTWASTAGEPER.Text = Val(dr("WASTAGEPER"))
        '                TXTSHRINKAGEPER.Text = Val(dr("SHRINKAGEPER"))
        '                TXTWPP.Text = Val(dr("WPP"))
        '                TXTWEAVECOST.Text = Val(dr("WEAVECOST"))
        '                TXTGFABCOST.Text = Val(dr("GREYFABCOST"))
        '                TXTFFABCOST.Text = Val(dr("FINISHFABCOST"))
        '                TXTPRODDAY.Text = Val(dr("PRODDAY"))
        '                TXTPCSL.Text = Val(dr("PCSL"))
        '                TXTREEDSPACECM.Text = Val(dr("REEDSPACECM"))
        '                txtfinishmethod.Text = Convert.ToString(dr("FINISHMETHOD").ToString)
        '                CMBQUALITIES.Text = Convert.ToString(dr("QUALITY").ToString)
        '                CMBQUALITYTYPE.Text = Convert.ToString(dr("QUALITYTYPE").ToString)
        '                TXTBLENDPER.Text = dr("BLENDPER")
        '                TXTGLM.Text = Format(Val(dr("GREYLOOMMTR")), "0.000")
        '                TXTENDPERINCH.Text = dr("ENDPERINCH")
        '                TXTTOTALENDS.Text = dr("TOTALENDS")
        '            Next
        '            'cmbtype.Enabled = False

        '            'TOTAL()

        '            'warp gridmatching data serializations
        '            Dim dttable1 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSRNO, 0) As WARPGRIDSRNO, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPSYM, '') AS WARPGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WARPYARNQUALITY, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPDENIER, 0) AS WARPDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WARPMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WARPSHADE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPPE, 0) AS WARPPE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPBE, 0) AS WARPBE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPTE, 0) AS WARPTE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPWT, 0.000) AS WARPWT, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCONS, 0) AS WARPCONS, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPRATE, 0) AS WARPRATE, ISNULL(DESIGNCARD_WARPMATCHING.DESIGN_WARPCOST, 0) AS WARPCOST ", "", " DESIGNCARD_WARPMATCHING INNER JOIN YARNQUALITYMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_WARPYARNQUALITYID = YARNQUALITYMASTER.YARN_ID AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = YARNQUALITYMASTER.YARN_YEARID LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = MILLMASTER.MILL_YEARID AND MILLMASTER.MILL_ID = DESIGNCARD_WARPMATCHING.DESIGN_WARPMILLID LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WARPMATCHING.DESIGN_YEARID = COLORMASTER.COLOR_yearid AND COLORMASTER.COLOR_id = DESIGNCARD_WARPMATCHING.DESIGN_WARPCOLORID  ", " AND  DESIGNCARD_WARPMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WARPGRIDSRNO")
        '            If dttable1.Rows.Count > 0 Then
        '                For Each DTR As DataRow In dttable1.Rows
        '                    GRIDWARP.Rows.Add(Val(DTR("WARPGRIDSRNO")), DTR("WARPGRIDSYM").ToString, DTR("WARPYARNQUALITY").ToString, Format(DTR("WARPDENIER"), "0.00"), DTR("WARPMILLNAME").ToString, DTR("WARPSHADE").ToString, Format(DTR("WARPPE"), "0.00"), Format(DTR("WARPBE"), "0.00"), Format(DTR("WARPTE"), "0.00"), Format(DTR("WARPWT"), "0.000"), Format(DTR("WARPCONS"), "0.00"), Format(DTR("WARPRATE"), "0.00"), Format(DTR("WARPCOST"), "0.00"))
        '                Next
        '            End If
        '            ' Warp Gridpattern data serializations
        '            Dim dttable2 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_SRNO, 0) AS WARPPATTERNGRIDSRNO, ISNULL(DESIGN_WARPPE, '') AS WARPPATTERNGRIDPE, ISNULL(DESIGN_WARPSYM, '') AS WARPPATTERNGRIDSYM", "", " DESIGNCARD_WARPPATTERN  ", " AND  DESIGNCARD_WARPPATTERN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPPATTERN.DESIGN_YEARID = " & YearId & " ORDER BY WARPPATTERNGRIDSRNO")
        '            If dttable2.Rows.Count > 0 Then
        '                For Each DTR As DataRow In dttable2.Rows
        '                    GRIDWARPPATTERN.Rows.Add(DTR("WARPPATTERNGRIDSRNO"), DTR("WARPPATTERNGRIDPE"), DTR("WARPPATTERNGRIDSYM").ToString)
        '                Next
        '            End If
        '            'WARP grid shade data serializations
        '            Dim dttableWARPshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_WDSRNO, 0) AS WDSRNO, ISNULL(COLORMASTER.COLOR_name,'') AS WDSHADE, ISNULL(DESIGN_WDMAINSRNO, 0) AS WDMAINSRNO", "", " DESIGNCARD_WARPSHADE LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WARPSHADE.DESIGN_WDSHADE = COLORMASTER.COLOR_id AND DESIGNCARD_WARPSHADE.DESIGN_YEARID = COLORMASTER.COLOR_yearid  ", " AND  DESIGNCARD_WARPSHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WARPSHADE.DESIGN_YEARID = " & YearId & " ORDER BY WDSRNO")
        '            If dttableWARPshade.Rows.Count > 0 Then
        '                For Each DTR As DataRow In dttableWARPshade.Rows
        '                    DT_WARPDETAILS.Rows.Add(DTR("WDSRNO"), DTR("WDSHADE"), DTR("WDMAINSRNO"))
        '                Next
        '                POPULATEGRID()
        '            End If

        '            ' Selvedge Grid data serialization
        '            Dim dttable3 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGESRNO, 0) AS SELVEDGEGRIDSRNO, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGESYM, '') AS SELVEDGEGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS SELVEDGEYARNQUALITY, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEDENIER, 0) AS SELVEDGEDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS SELVEDGEMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS SELVEDGESHADE, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEPE, 0) AS SELVEDGEPE, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEBE, 0) AS SELVEDGEBE, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEDTE, 0) AS SELVEDGETE, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEWT, 0) AS SELVEDGEWT, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGECONS, 0) AS SELVEDGECONS, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGERATE, 0) AS SELVEDGERATE, ISNULL(DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGECOST, 0) AS SELVEDGECOST ", "", " DESIGNCARD_SELVEDGEMATCHING LEFT OUTER JOIN YARNQUALITYMASTER ON DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEYARNQUALITYID = YARNQUALITYMASTER.YARN_ID LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGEMILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_SELVEDGEMATCHING.DESIGN_SELVEDGECOLORID = COLORMASTER.COLOR_id   ", " AND  DESIGNCARD_SELVEDGEMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_SELVEDGEMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY SELVEDGEGRIDSRNO")
        '            If dttable3.Rows.Count > 0 Then
        '                For Each DTR As DataRow In dttable3.Rows
        '                    GRIDSELVEDGE.Rows.Add(DTR("SELVEDGEGRIDSRNO"), DTR("SELVEDGEGRIDSYM").ToString, DTR("SELVEDGEYARNQUALITY").ToString, Format(DTR("SELVEDGEDENIER"), "0.00"), DTR("SELVEDGEMILLNAME").ToString, DTR("SELVEDGESHADE").ToString, Format(DTR("SELVEDGEPE"), "0.00"), Format(DTR("SELVEDGEBE"), "0.00"), Format(DTR("SELVEDGETE"), "0.00"), Format(DTR("SELVEDGEWT"), "0.00"), Format(DTR("SELVEDGECONS"), "0.00"), Format(DTR("SELVEDGERATE"), "0.00"), Format(DTR("SELVEDGECOST"), "0.00"))
        '                Next
        '            End If
        '            ' Selvedge Gridpattern data serializations
        '            Dim dttable4 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_SRNO, 0) AS SELVEDGEPATTERNGRIDSRNO, ISNULL(DESIGN_SELVEDGEPE, '') AS SELVEDGEPATTERNGRIDPE, ISNULL(DESIGN_SELVEDGESYM, '') AS SELVEDGEPATTERNGRIDSYM", "", " DESIGNCARD_SELVEDGEPATTERN  ", " AND  DESIGNCARD_SELVEDGEPATTERN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_SELVEDGEPATTERN.DESIGN_YEARID = " & YearId & " ORDER BY SELVEDGEPATTERNGRIDSRNO")
        '            If dttable4.Rows.Count > 0 Then
        '                For Each DTR As DataRow In dttable4.Rows
        '                    GRIDSELVEDGEPATTERN.Rows.Add(DTR("SELVEDGEPATTERNGRIDSRNO"), DTR("SELVEDGEPATTERNGRIDPE"), DTR("SELVEDGEPATTERNGRIDSYM").ToString)
        '                Next
        '            End If

        '            'selvedge grid shade data serializations

        '            Dim dttableshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_sdSRNO, 0) AS SDSRNO,ISNULL(COLORMASTER.COLOR_name,'') AS  SDSHADE, ISNULL(DESIGN_sdMAINSRNO, 0) AS SDMAINSRNO", "", " DESIGNCARD_SELVEDGESHADE LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_SELVEDGESHADE.DESIGN_SDSHADE = COLORMASTER.COLOR_id AND DESIGNCARD_SELVEDGESHADE.DESIGN_YEARID = COLORMASTER.COLOR_yearid   ", " AND  DESIGNCARD_SELVEDGESHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_SELVEDGESHADE.DESIGN_YEARID = " & YearId & " ORDER BY SDSRNO")
        '            If dttableshade.Rows.Count > 0 Then
        '                For Each DTR As DataRow In dttableshade.Rows
        '                    DT_SELDETAILS.Rows.Add(Val(DTR("SDSRNO")), DTR("SDSHADE").ToString, Val(DTR("SDMAINSRNO")))
        '                Next
        '                POPULATESELGRID()
        '            End If

        '            ' Weft Grid data serialization
        '            Dim dttable5 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSRNO, 0) AS WEFTGRIDSRNO, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTSYM, '') AS WEFTGRIDSYM, ISNULL(YARNQUALITYMASTER.YARN_NAME, '') AS WEFTYARNQUALITY, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTDENIER, 0) AS WEFTDENIER, ISNULL(MILLMASTER.MILL_NAME, '') AS WEFTMILLNAME, ISNULL(COLORMASTER.COLOR_name, '') AS WEFTSHADE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTPE, 0) AS WEFTPE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTBE, 0) AS WEFTBE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTTE, 0) AS WEFTTE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTWT, 0) AS WEFTWT, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCONS, 0) AS WEFTCONS, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTRATE, 0) AS WEFTRATE, ISNULL(DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCOST, 0) AS WEFTCOST", "", " DESIGNCARD_WEFTMATCHING LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTCOLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN MILLMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTMILLID = MILLMASTER.MILL_ID LEFT OUTER JOIN YARNQUALITYMASTER ON DESIGNCARD_WEFTMATCHING.DESIGN_WEFTYARNQUALITYID = YARNQUALITYMASTER.YARN_ID   ", " AND  DESIGNCARD_WEFTMATCHING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WEFTMATCHING.DESIGN_YEARID = " & YearId & " ORDER BY WEFTGRIDSRNO")
        '            If dttable5.Rows.Count > 0 Then
        '                For Each DTR As DataRow In dttable5.Rows
        '                    GRIDWEFT.Rows.Add(DTR("WEFTGRIDSRNO"), DTR("WEFTGRIDSYM").ToString, DTR("WEFTYARNQUALITY").ToString, Format(DTR("WEFTDENIER"), "0.00"), DTR("WEFTMILLNAME").ToString, DTR("WEFTSHADE").ToString, Format(DTR("WEFTPE"), "0.00"), Format(DTR("WEFTBE"), "0.00"), Format(DTR("WEFTTE"), "0.00"), Format(DTR("WEFTWT"), "0.000"), Format(DTR("WEFTCONS"), "0.00"), Format(DTR("WEFTRATE"), "0.00"), Format(DTR("WEFTCOST"), "0.00"))
        '                Next
        '            End If
        '            ' Weft GridPattern data serialization
        '            Dim dttable6 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_SRNO, 0) AS WEFTPATTERNGRIDSRNO, ISNULL(DESIGN_WEFTPE, '') AS WEFTPATTERNGRIDPE, ISNULL(DESIGN_WARPSYM, '') AS WEFTPATTERNGRIDSYM", "", " DESIGNCARD_WEFTPATTERN  ", " AND  DESIGNCARD_WEFTPATTERN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WEFTPATTERN.DESIGN_YEARID = " & YearId & " ORDER BY WEFTPATTERNGRIDSRNO")
        '            If dttable6.Rows.Count > 0 Then
        '                For Each DTR As DataRow In dttable6.Rows
        '                    GRIDWEFTPATTERN.Rows.Add(DTR("WEFTPATTERNGRIDSRNO"), DTR("WEFTPATTERNGRIDPE"), DTR("WEFTPATTERNGRIDSYM").ToString)
        '                Next
        '            End If
        '            'WEFT grid shade data serializations

        '            Dim dttableWEFTshade As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_FDSRNO, 0) AS FDSRNO,ISNULL(COLORMASTER.COLOR_name,'') AS FDSHADE, ISNULL(DESIGN_FDMAINSRNO, 0) AS FDMAINSRNO", "", " DESIGNCARD_WEFTSHADE LEFT OUTER JOIN COLORMASTER ON DESIGNCARD_WEFTSHADE.DESIGN_FDSHADE = COLORMASTER.COLOR_id AND DESIGNCARD_WEFTSHADE.DESIGN_YEARID = COLORMASTER.COLOR_yearid   ", " AND  DESIGNCARD_WEFTSHADE.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_WEFTSHADE.DESIGN_YEARID = " & YearId & " ORDER BY FDSRNO")
        '            If dttableWEFTshade.Rows.Count > 0 Then
        '                For Each DTR As DataRow In dttableWEFTshade.Rows
        '                    DT_WEFTDETAILS.Rows.Add(Val(DTR("FDSRNO")), DTR("FDSHADE").ToString, Val(DTR("FDMAINSRNO")))
        '                Next
        '                POPULATEWEFTGRID()
        '            End If
        '            'DRAWING FIELD
        '            Dim dttable7 As DataTable = OBJCMN.SEARCH("  ISNULL(DESIGN_DRAWINGSRNO, 0) AS DRAWINGSRNO, ISNULL(DESIGN_DRAWINGENDS, 0) AS DRAWINGENDS, ISNULL(DESIGN_DRAWINGREPEATMARK, '') AS DRAWINGREPEATMARK, ISNULL(DESIGN_DRAWINGREPEAT, 0) AS DRAWINGREPEAT, ISNULL(DESIGN_DRAWINGREPEATMARK1, '') AS DRAWINGGRIDREPEATMARK1, ISNULL(DESIGN_DRAWINGREPEAT1, 0) AS DRAWINGREPEAT1, ISNULL(DESIGN_DRAWINGREPEATMARK2, '') AS DRAWINGREPEATMARK2, ISNULL(DESIGN_DRAWINGREPEAT2, 0) AS DRAWINGREPEAT2 ", "", " DESIGNCARD_DRAWING  ", " AND  DESIGNCARD_DRAWING.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_DRAWING.DESIGN_YEARID = " & YearId & " ORDER BY DRAWINGSRNO")
        '            If dttable7.Rows.Count > 0 Then
        '                For Each DTR As DataRow In dttable7.Rows
        '                    GRIDDRAWING.Rows.Add(DTR("DRAWINGSRNO"), DTR("DRAWINGENDS").ToString, DTR("DRAWINGREPEATMARK").ToString, DTR("DRAWINGREPEAT"), DTR("DRAWINGGRIDREPEATMARK1").ToString, DTR("DRAWINGREPEAT1"), DTR("DRAWINGREPEATMARK2").ToString, DTR("DRAWINGREPEAT2"))
        '                Next

        '            End If
        '            'PEGPLAN FIELD
        '            Dim dttable8 As DataTable = OBJCMN.SEARCH("  ISNULL(DESIGN_PPSRNO, 0) AS PPSRNO, ISNULL(DESIGN_PPENDS, 0) AS PPENDS, ISNULL(DESIGN_PPREPEATMARK, '') AS PPREPEATMARK, ISNULL(DESIGN_PPREPEAT, 0) AS PPREPEAT, ISNULL(DESIGN_PPREPEATMARK1, '') AS PPGRIDREPEATMARK1, ISNULL(DESIGN_PPREPEAT1, 0) AS PPREPEAT1, ISNULL(DESIGN_PPREPEATMARK2, '') AS PPREPEATMARK2, ISNULL(DESIGN_PPREPEAT2, 0) AS PPREPEAT2, ISNULL(DESIGN_PPSYM, '') AS PPSYM ", "", " DESIGNCARD_PEGPLAN  ", " AND  DESIGNCARD_PEGPLAN.DESIGN_CARDNO = " & tempdesignno & " AND DESIGNCARD_PEGPLAN.DESIGN_YEARID = " & YearId & " ORDER BY PPSRNO")
        '            If dttable8.Rows.Count > 0 Then
        '                For Each DTR As DataRow In dttable8.Rows
        '                    GRIDPEG.Rows.Add(DTR("PPSRNO"), DTR("PPENDS").ToString, DTR("PPREPEATMARK").ToString, DTR("PPREPEAT"), 0, 0, DTR("PPGRIDREPEATMARK1").ToString, DTR("PPREPEAT1"), 0, 0, DTR("PPREPEATMARK2").ToString, DTR("PPREPEAT2"), 0, 0, DTR("PPSYM").ToString)
        '                Next
        '                ' GRIDPEG_CellValidating(Nothing, Nothing)
        '            End If
        '            cmdbtn1_Click(Nothing, Nothing, GRIDPEG)
        '            cmdbtn1_Click(Nothing, Nothing, GRIDDRAWING)
        '            TOTAL()
        '            CALC()
        '            FILLPEGPLAN()
        '            pegplan()
        '            'GRIDDRAWING_CellValidating(Nothing, Nothing)
        '            srno(GRIDWARP, TXTWARPSRNO)
        '            srno(GRIDSELVEDGE, TXTSELSRNO)
        '            srno(GRIDWEFT, TXTWEFTSRNO)
        '            srno(GRIDWEFTDESC, TXTFDSRNO)
        '            srno(GRIDWARPDESC, TXTWDSRNO)
        '            srno(GRIDSELDESC, TXTSDNO)
        '            fillMATCHINGcmb()
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
    End Sub

End Class