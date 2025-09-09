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
            Dim strCommand As String = "SP_DESIGN_CARD_MASTER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                ' Add parameters in the exact order of alParaval
                .Add(New SqlClient.SqlParameter("@ItemName", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DesignNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Reed", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ReedSpace", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Picks", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MainRs", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ThreadPerDent", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FePi", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FWidth", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FPpi", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FWt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Dents", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalDentsMain", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeDents", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalDents", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WarpTtl", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftTtl", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Gsm", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Weave", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Shafts", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWt", alParaval(I)))
                I += 1

                ' Selvedge fields
                .Add(New SqlClient.SqlParameter("@LeftSelvedge", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RightSelvedge", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LeftSelvedgeEnds", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RightSelvedgeEnds", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LeftSelvedgeDents", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RightSelvedgeDents", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LeftSelvedgeTotalEnds", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RightSelvedgeTotalEnds", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeEnds", alParaval(I)))
                I += 1

                ' Reference and names
                .Add(New SqlClient.SqlParameter("@RefNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Name", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@AgentName", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DelAt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DelDate", alParaval(I)))
                I += 1

                ' Other details
                .Add(New SqlClient.SqlParameter("@Mtrs", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NoOfPcs", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Loom", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BeamMtrs", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CoverFactor", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Efficiency", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LoomProd", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RPM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GreyDelAt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GreyDelDate", alParaval(I)))
                I += 1

                ' Total Warp
                .Add(New SqlClient.SqlParameter("@TotalWarpPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpWt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpCons", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpRate", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpCost", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpPERepeat", alParaval(I)))
                I += 1

                ' Total Selvedge
                .Add(New SqlClient.SqlParameter("@TotalSelvedgePE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeWt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeCons", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeRate", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeCost", alParaval(I)))
                I += 1

                ' Total Weft
                .Add(New SqlClient.SqlParameter("@TotalWeftPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftWt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftCons", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftRate", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftCost", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftPERepeat", alParaval(I)))
                I += 1

                'warp gridmatching data serializations
                .Add(New SqlClient.SqlParameter("@WarpGridSrNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WarpGridSym", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WarpYarnQuality", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPDenier", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPMillName", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPShade", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPWt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPCons", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPRate", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WARPCost", alParaval(I)))
                I += 1

                ' Warp Gridpattern data serializations
                .Add(New SqlClient.SqlParameter("@WarpPatternGridSrNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WarpPatternGridPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WarpPatternGridSym", alParaval(I)))
                I += 1

                ' Selvedge Grid data serialization
                .Add(New SqlClient.SqlParameter("@SelvedgeGridSrNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridSym", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridYarnQuality", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridDenier", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridMillName", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridShade", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridWt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridCons", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridRate", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridCost", alParaval(I)))
                I += 1

                ' Weft Grid data serialization
                .Add(New SqlClient.SqlParameter("@WeftGridSrNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridSym", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridYarnQuality", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridDenier", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridMillName", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridShade", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridWt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridCons", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridRate", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridCost", alParaval(I)))
                I += 1

                ' Weft GridPattern data serialization
                .Add(New SqlClient.SqlParameter("@WeftGridPatternSrNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridPatternPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridPatternSym", alParaval(I)))
                I += 1



                ' Company and user details
                .Add(New SqlClient.SqlParameter("@CmpId", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LocationId", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UserId", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YearId", alParaval(I)))
                I += 1

                ' Additional flags or reserved parameter
                .Add(New SqlClient.SqlParameter("@TRANSFER", alParaval(I)))
                I += 1
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function UPDATE() As Integer
        Try
            Dim strCommand As String = "SP_DESIGN_CARD_MASTER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter
                Dim I As Integer = 0
                .Add(New SqlClient.SqlParameter("@ItemName", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DesignNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Reed", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ReedSpace", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Picks", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@MainRs", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@ThreadPerDent", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FePi", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FWidth", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FPpi", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@FWt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Dents", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalDentsMain", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeDents", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalDents", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WarpTtl", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftTtl", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Gsm", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Weave", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Shafts", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWt", alParaval(I)))
                I += 1

                ' Selvedge fields
                .Add(New SqlClient.SqlParameter("@LeftSelvedge", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RightSelvedge", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LeftSelvedgeEnds", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RightSelvedgeEnds", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LeftSelvedgeDents", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RightSelvedgeDents", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LeftSelvedgeTotalEnds", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RightSelvedgeTotalEnds", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeEnds", alParaval(I)))
                I += 1

                ' Reference and names
                .Add(New SqlClient.SqlParameter("@RefNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Name", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@AgentName", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DelAt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DelDate", alParaval(I)))
                I += 1

                ' Other details
                .Add(New SqlClient.SqlParameter("@Mtrs", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@NoOfPcs", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Loom", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@BeamMtrs", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@CoverFactor", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@Efficiency", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LoomProd", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@RPM", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GreyDelAt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@GreyDelDate", alParaval(I)))
                I += 1

                ' Total Warp
                .Add(New SqlClient.SqlParameter("@TotalWarpPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpWt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpCons", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpRate", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpCost", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWarpPERepeat", alParaval(I)))
                I += 1

                ' Total Selvedge
                .Add(New SqlClient.SqlParameter("@TotalSelvedgePE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeWt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeCons", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeRate", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalSelvedgeCost", alParaval(I)))
                I += 1

                ' Total Weft
                .Add(New SqlClient.SqlParameter("@TotalWeftPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftBE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftTE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftWt", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftCons", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftRate", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftCost", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@TotalWeftPERepeat", alParaval(I)))
                I += 1

                ' Warp Grid data serializations
                .Add(New SqlClient.SqlParameter("@WarpGridSrNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WarpGridPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WarpGridSym", alParaval(I)))
                I += 1

                ' Selvedge Grid data serialization
                .Add(New SqlClient.SqlParameter("@SelvedgeGridSrNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@SelvedgeGridSym", alParaval(I)))
                I += 1

                ' Weft Grid data serialization
                .Add(New SqlClient.SqlParameter("@WeftGridSrNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftGridSym", alParaval(I)))
                I += 1

                ' Weft Repeat Grid data serialization
                .Add(New SqlClient.SqlParameter("@WeftRepeatGridSrNo", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftRepeatGridPE", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@WeftRepeatGridSym", alParaval(I)))
                I += 1

                ' Company and user details
                .Add(New SqlClient.SqlParameter("@CmpId", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@LocationId", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@UserId", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@YearId", alParaval(I)))
                I += 1

                ' Additional flags or reserved parameter
                .Add(New SqlClient.SqlParameter("@TRANSFER", alParaval(I)))
                I += 1
                .Add(New SqlClient.SqlParameter("@DesignCardId", alParaval(I))) ' Adjust if needed
                I += 1

            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function Delete() As DataTable
        Try
            Dim strCommand As String = "SP_DESIGN_CARD_MASTER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@DesignNo", alParaval(0))) ' Or correct index
                .Add(New SqlClient.SqlParameter("@ItemName", alParaval(0))) ' Or correct index
                .Add(New SqlClient.SqlParameter("@CmpId", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@LocationId", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@YearId", alParaval(3)))
            End With
            Dim DT As DataTable = objDBOperation.execute(strCommand, alParameter).Tables(0)
            Return DT
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SelectDesignCard(ByVal designNo As String, ByVal Itemname As String, ByVal cmpId As Integer, ByVal locationId As Integer, ByVal yearId As Integer) As DataTable
        Try
            Dim strCommand As String = "SP_SELECT_DESIGN_CARD_FOR_EDIT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@DesignNo", designNo))
                .Add(New SqlClient.SqlParameter("@Itemname", Itemname))
                .Add(New SqlClient.SqlParameter("@CmpId", cmpId))
                .Add(New SqlClient.SqlParameter("@LocationId", locationId))
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
