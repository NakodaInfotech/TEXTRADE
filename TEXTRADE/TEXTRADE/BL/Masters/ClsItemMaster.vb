
Imports DB

Public Class clsItemmaster

    Private objDBOperation As DBOperation
    Public alParaval As New ArrayList
    Dim intResult As Integer

#Region "Constructor"
    Public Sub New()
        Try
            objDBOperation = New DBOperation
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Functions"

    Public Function SAVE() As Integer

        Try

            'save itemdetails
            Dim strCommand As String = "SP_MASTER_ITEMMASTER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@material", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@category", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DISPLAYNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@itemname", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DEPARTMENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@itemcode", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@unit", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FOLD", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@VALUATIONRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TRANSPORTRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CHECKINGRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PACKINGRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESIGNRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REORDER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@upper", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@lower", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@HSNCODE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLOCKED", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@HIDEINDESIGN", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@WIDTH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYWIDTH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHRINKFROM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHRINKTO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGE", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@RATETYPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GRIDRATE", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@YARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PER", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PROCESS", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@remarks", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FRMSTRING", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@IMGPATH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARP", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@WARPSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPAMOUNT", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@WEFTSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTPICK", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTAMOUNT", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@WARPTL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WEFTTL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REED", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REEDSPACE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PICKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WEAVE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GREYCATEGORY", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@ACTWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACTAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DHARAPERCENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DHARAAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WASTAGEPERCENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WASTAGEAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEAVINGCHGS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEAVINGAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GSTPERCENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GSTAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@AMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALGSTPERCENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPTOTALAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTTOTALAMOUNT", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@COLORSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@COLOR", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@VALUELOSSPER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@COSTCENTERNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GSM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PERCENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GARMENT", alParaval(I)))
                I += 1

                'GRID SHADE
                .Add(New SqlClient.SqlParameter("@SHADESRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHADE", alParaval(I)))
                I += 1


                'GRID SHADE ITEMS 
                .Add(New SqlClient.SqlParameter("@ITEMSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEMDESIGN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEMSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEMMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHADEGRIDSRNO", alParaval(I)))
                I += 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function UPDATE() As Integer
        Dim strcommand As String = ""
        Try
            'Update AccountsMaster
            strcommand = "SP_MASTER_ITEMMASTER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@material", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@category", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DISPLAYNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@itemname", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DEPARTMENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@itemcode", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@unit", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FOLD", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@VALUATIONRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TRANSPORTRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CHECKINGRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PACKINGRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DESIGNRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@REORDER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@upper", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@lower", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@HSNCODE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BLOCKED", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@HIDEINDESIGN", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@WIDTH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GREYWIDTH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHRINKFROM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHRINKTO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SELVEDGE", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@RATETYPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GRIDRATE", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@YARNQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PER", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PROCESS", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@remarks", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FRMSTRING", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@IMGPATH", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARP", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@WARPSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPENDS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPAMOUNT", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@WEFTSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTQUALITY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTPICK", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTRATE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTAMOUNT", alParaval(I)))
                I += 1


                .Add(New SqlClient.SqlParameter("@WARPTL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WEFTTL", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REED", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REEDSPACE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PICKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALWARPWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALWEFTWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WEAVE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GREYCATEGORY", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@ACTWT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ACTAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DHARAPERCENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DHARAAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WASTAGEPERCENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WASTAGEAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEAVINGCHGS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEAVINGAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GSTPERCENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GSTAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@AMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALGSTPERCENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TOTALAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPTOTALAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WEFTTOTALAMOUNT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@COLORSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@COLOR", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@VALUELOSSPER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@COSTCENTERNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GSM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@PERCENT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GARMENT", alParaval(I)))
                I += 1

                'GRID SHADE
                .Add(New SqlClient.SqlParameter("@SHADESRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHADE", alParaval(I)))
                I += 1


                'GRID SHADE ITEMS 
                .Add(New SqlClient.SqlParameter("@ITEMSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEMDESIGN", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEMSHADE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ITEMMTRS", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SHADEGRIDSRNO", alParaval(I)))
                I += 1

                .Add(New SqlClient.SqlParameter("@itemid", alParaval(I)))
                I += 1

            End With

            intResult = objDBOperation.executeNonQuery(strcommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function Delete() As Integer
        Dim intResult As Integer
        Try
            Dim strCommand As String = "SP_MASTER_ITEMMASTER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userId", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(3)))
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SAVESTORE() As Integer

        Try

            'save itemdetails
            Dim strCommand As String = "SP_MASTER_ITEMMASTER_STORES_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@ITEMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UNIT", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GRIDSRNO", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@STOREITEMNAME", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@QTY", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@USERID", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(I)))
                I += 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

#End Region

End Class
