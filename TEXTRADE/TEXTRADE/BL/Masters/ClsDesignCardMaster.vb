Imports DB
Public Class ClsDesignCardMaster
    Private objDBOperation As DBOperation
    Public alParaval As New ArrayList
    Dim intResult As Integer

#Region "Constructor"
    Public Sub New()
        Try
            objDBOperation = New DBOperation()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Functions"
    Public Function SAVE() As Integer
        Try
            Dim strCommand As String = "SP_MASTER_DESIGNCARDMASTER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                ' Add parameters in the exact order of alParaval
                .Add(New SqlClient.SqlParameter("@CARDNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESIGNNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REED", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REEDSPACE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PICKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MAINRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@THREADPERDENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FEPI", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FWIDTH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FPPI", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DENTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALDENTSMAIN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEDENTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALDENTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPTTL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTTTL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GSM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEAVE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHAFTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWT", alParaval(I)))
                I += 1
                ' Selvedge fields
                .Add(New SqlClient.SqlParameter("@LEFTSELVEDGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RIGHTSELVEDGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LEFTSELVEDGEEND", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RIGHTSELVEDGEEND", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LEFTSELVEDGEDENTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RIGHTSELVEDGEDENTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LEFTSELVEDGETOTALEND", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RIGHTSELVEDGETOTALEND", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEENDS", alParaval(I)))
                I += 1


                ' Reference and names
                .Add(New SqlClient.SqlParameter("@REFNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@AGENTNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DELIVERYAT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ORDERNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DELDATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ORDERDATE", alParaval(I)))
                I += 1
                ' Other details
                .Add(New SqlClient.SqlParameter("@MTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NOOFPCS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOOM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BEAMMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@COVERFACTOR", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@EFFICIENCY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOOMPROD", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RPM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYDELIVERYAT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYDELDATE", alParaval(I)))
                I += 1
                ' Total Warp
                .Add(New SqlClient.SqlParameter("@TOTALWARPPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPCOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPGRIDPE", alParaval(I)))
                I += 1
                ' Total Selvedge
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGETE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGECONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGERATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGECOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEGRIDPE", alParaval(I)))
                I += 1
                ' Total Weft
                .Add(New SqlClient.SqlParameter("@TOTALWEFTPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTCOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTGRIDPE", alParaval(I)))
                I += 1
                ' Total DRAWING 
                .Add(New SqlClient.SqlParameter("@TOTALDRAWENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALDRAWDENTS", alParaval(I)))
                I += 1
                'warp gridmatching data serializations
                .Add(New SqlClient.SqlParameter("@WARPGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPGRIDSYM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPYARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPDENIER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPMILLNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPCOST", alParaval(I)))
                I += 1
                ' Warp Gridpattern data serializations
                .Add(New SqlClient.SqlParameter("@WARPPATTERNGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPPATTERNGRIDPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPPATTERNGRIDSYM", alParaval(I)))
                I += 1
                ' WARP Grid shade data serializations
                .Add(New SqlClient.SqlParameter("@WDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WDSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WDMAINSRNO", alParaval(I)))
                I += 1
                ' Selvedge Grid data serialization
                .Add(New SqlClient.SqlParameter("@SELVEDGEGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEGRIDSYM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEYARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEDENIER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEMILLNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGESHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGETE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGECONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGERATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGECOST", alParaval(I)))
                I += 1
                ' Warp Gridpattern data serializations
                .Add(New SqlClient.SqlParameter("@SELVEDGEPATTERNGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEPATTERNGRIDPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEPATTERNGRIDSYM", alParaval(I)))
                I += 1

                ' selvedge Grid shade data serializations
                .Add(New SqlClient.SqlParameter("@SDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SDSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SDMAINSRNO", alParaval(I)))
                I += 1

                ' Weft Grid data serialization
                .Add(New SqlClient.SqlParameter("@WEFTGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTGRIDSYM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTYARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTDENIER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTMILLNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTCOST", alParaval(I)))
                I += 1
                ' Weft GridPattern data serialization
                .Add(New SqlClient.SqlParameter("@WEFTPATTERNGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTPATTERNGRIDPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTPATTERNGRIDSYM", alParaval(I)))
                I += 1
                ' WEFT Grid shade data serializations
                .Add(New SqlClient.SqlParameter("@FDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FDSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FDMAINSRNO", alParaval(I)))
                I += 1
                'DRAWING FIELD
                .Add(New SqlClient.SqlParameter("@DRAWINGSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEATMARK", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEAT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEATMARK1", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEAT1", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEATMARK2", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEAT2", alParaval(I)))
                I += 1




                ' Company and user details
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOCATIONID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1
                ' Additional flags or reserved parameter
                .Add(New SqlClient.SqlParameter("@TRANSFER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALFINISHWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYWIDTH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYWIDTHCM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FINISHWIDTHCM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYLOOMMTR", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLENDPERCENTAGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FINISHMETHOD", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@QUALITIES", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@QUALITYTYPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPWASTAGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WASTAGEPERCENTAGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHRINKAGEPERCENTAGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WPP", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEAVECOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYFABRICCOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FINISHFABRICCOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PRODUCTIONPERDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PCSL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REEDSPACECM", alParaval(I)))
                I += 1



            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function UPDATE() As Integer
        Dim intResult As Integer

        Try
            Dim strCommand As String = "SP_MASTER_DESIGNCARDMASTER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@CARDNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESIGNNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REED", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REEDSPACE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PICKS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MAINRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@THREADPERDENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FEPI", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FWIDTH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FPPI", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DENTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALDENTSMAIN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEDENTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALDENTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPTTL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTTTL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GSM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEAVE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHAFTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWT", alParaval(I)))
                I += 1
                ' Selvedge fields
                .Add(New SqlClient.SqlParameter("@LEFTSELVEDGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RIGHTSELVEDGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LEFTSELVEDGEEND", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RIGHTSELVEDGEEND", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LEFTSELVEDGEDENTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RIGHTSELVEDGEDENTS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LEFTSELVEDGETOTALEND", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RIGHTSELVEDGETOTALEND", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEENDS", alParaval(I)))
                I += 1


                ' Reference and names
                .Add(New SqlClient.SqlParameter("@REFNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@AGENTNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DELIVERYAT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ORDERNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DELDATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ORDERDATE", alParaval(I)))
                I += 1
                ' Other details
                .Add(New SqlClient.SqlParameter("@MTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NOOFPCS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOOM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BEAMMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@COVERFACTOR", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@EFFICIENCY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOOMPROD", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RPM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYDELIVERYAT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYDELDATE", alParaval(I)))
                I += 1
                ' Total Warp
                .Add(New SqlClient.SqlParameter("@TOTALWARPPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPCOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPGRIDPE", alParaval(I)))
                I += 1
                ' Total Selvedge
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGETE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGECONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGERATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGECOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALSELVEDGEGRIDPE", alParaval(I)))
                I += 1
                ' Total Weft
                .Add(New SqlClient.SqlParameter("@TOTALWEFTPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTCOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTGRIDPE", alParaval(I)))
                I += 1
                ' Total DRAWING 
                .Add(New SqlClient.SqlParameter("@TOTALDRAWENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALDRAWDENTS", alParaval(I)))
                I += 1
                'warp gridmatching data serializations
                .Add(New SqlClient.SqlParameter("@WARPGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPGRIDSYM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPYARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPDENIER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPMILLNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPCOST", alParaval(I)))
                I += 1
                ' Warp Gridpattern data serializations
                .Add(New SqlClient.SqlParameter("@WARPPATTERNGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPPATTERNGRIDPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPPATTERNGRIDSYM", alParaval(I)))
                I += 1
                ' WARP Grid shade data serializations
                .Add(New SqlClient.SqlParameter("@WDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WDSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WDMAINSRNO", alParaval(I)))
                I += 1
                ' Selvedge Grid data serialization
                .Add(New SqlClient.SqlParameter("@SELVEDGEGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEGRIDSYM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEYARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEDENIER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEMILLNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGESHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGETE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGECONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGERATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGECOST", alParaval(I)))
                I += 1
                ' Warp Gridpattern data serializations
                .Add(New SqlClient.SqlParameter("@SELVEDGEPATTERNGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEPATTERNGRIDPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGEPATTERNGRIDSYM", alParaval(I)))
                I += 1

                ' selvedge Grid shade data serializations
                .Add(New SqlClient.SqlParameter("@SDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SDSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SDMAINSRNO", alParaval(I)))
                I += 1

                ' Weft Grid data serialization
                .Add(New SqlClient.SqlParameter("@WEFTGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTGRIDSYM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTYARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTDENIER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTMILLNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTCONS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTCOST", alParaval(I)))
                I += 1
                ' Weft GridPattern data serialization
                .Add(New SqlClient.SqlParameter("@WEFTPATTERNGRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTPATTERNGRIDPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTPATTERNGRIDSYM", alParaval(I)))
                I += 1
                ' WEFT Grid shade data serializations
                .Add(New SqlClient.SqlParameter("@FDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FDSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FDMAINSRNO", alParaval(I)))
                I += 1

                'DRAWING FIELD
                .Add(New SqlClient.SqlParameter("@DRAWINGSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEATMARK", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEAT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEATMARK1", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEAT1", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEATMARK2", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DRAWINGREPEAT2", alParaval(I)))
                I += 1




                ' Company and user details
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LOCATIONID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1
                ' Additional flags or reserved parameter
                .Add(New SqlClient.SqlParameter("@TRANSFER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALFINISHWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYWIDTH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYWIDTHCM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FINISHWIDTHCM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYLOOMMTR", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLENDPERCENTAGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FINISHMETHOD", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@QUALITIES", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@QUALITYTYPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPWASTAGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WASTAGEPERCENTAGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHRINKAGEPERCENTAGE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WPP", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEAVECOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYFABRICCOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FINISHFABRICCOST", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PRODUCTIONPERDAY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PCSL", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REEDSPACECM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TEMPDESIGNNO", alParaval(I))) ' Adjust if needed
                I += 1



            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function Delete() As Integer
        Dim intResult As Integer
        Try
            Dim strCommand As String = "SP_DESIGN_CARD_MASTER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@CARDNO", alParaval(0))) ' Or correct index
                .Add(New SqlClient.SqlParameter("@CmpId", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@LocationId", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@YearId", alParaval(3)))
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
            'Dim DT As DataTable = objDBOperation.execute(strCommand, alParameter).Tables(0)
            'Return DT
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function SelectDesignCard(ByVal designNo As String, ByVal Itemname As String, ByVal cmpId As Integer, ByVal locationId As Integer, ByVal yearId As Integer) As DataTable
    '    Try
    '        Dim strCommand As String = "SP_SELECT_DESIGN_CARD_FOR_EDIT"
    '        Dim alParameter As New ArrayList
    '        With alParameter
    '            .Add(New SqlClient.SqlParameter("@DesignNo", designNo))
    '            .Add(New SqlClient.SqlParameter("@Itemname", Itemname))
    '            .Add(New SqlClient.SqlParameter("@CmpId", cmpId))
    '            .Add(New SqlClient.SqlParameter("@LocationId", locationId))
    '            .Add(New SqlClient.SqlParameter("@YearId", yearId))
    '        End With
    '        Dim dtTable As DataTable = objDBOperation.execute(strCommand, alParameter).Tables(0)
    '        Return dtTable
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Public Function SelectDesignCard(ByVal CARDNO As String, ByVal yearId As Integer) As DataTable
        Try
            Dim strCommand As String = "SP_MASTER_SELECT_DESIGNCARD_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@CARDNO", CARDNO))
                .Add(New SqlClient.SqlParameter("@YearId", yearId))
            End With
            Dim dtTable As DataTable = objDBOperation.execute(strCommand, alParameter).Tables(0)
            Return dtTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class
