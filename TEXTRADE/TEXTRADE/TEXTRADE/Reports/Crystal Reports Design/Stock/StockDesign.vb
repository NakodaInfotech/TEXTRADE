﻿
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports System.IO

Public Class StockDesign

    Public WHERECLAUSE As String = ""
    Public FRMSTRING As String = ""
    Public PERIOD As String = ""
    Public MTRS As Double = 0.0
    Public FROMDATE As Date
    Public TODATE As Date
    Public SHOWBALANCESTOCKONLY As Boolean
    Public DIRECTWHATSAPP As Boolean = False
    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False

    Dim RPTITEMSTOCKSUMM As New ItemWiseStockReport
    Dim RPTITEMSTOCKBARCODESUMM As New ItemWiseBarcodeStockReport
    Dim RPTQUALITYSTOCKSUMM As New QualityWiseStockReport
    Dim RPTDESIGNSTOCKSUMM As New DesignWiseStockReport
    Dim RPTSHADESTOCKSUMM As New ShadeWiseStockReport
    Dim RPTITEMSHADESTOCKSUMM As New ItemShadeWiseStockReport
    Dim RPTITEMSHADEGODOWNSTOCKSUMM As New ItemShadeGodownWiseStockReport
    Dim RPTITEMQUALITYSTOCKSUMM As New ItemQualityWiseStockReport
    Dim RPTITEMDESIGNSHADESTOCKSUMM As New ItemDesignShadeWiseStockReport
    Dim RPTITEMDESIGNSHADESTOCKSMALLSUMM As New ItemDesignShadeSmallStockReport
    Dim RPTITEMDESIGNSHADESTOCKNOUNITSMALLSUMM As New ItemDesignShadeWithoutUnitSmallStockReport
    Dim RPTCATEGORYITEMDESIGNSHADESTOCKNOUNITSMALLSUMM As New CategoryItemDesignShadeWithoutUnitSmallStockReport
    Dim RPTBARCODEITEMDESIGNSHADESTOCKSUMM As New BarcodeStockSummReport
    Dim RPTBARCODEGODOWNITEMSTOCKSUMM As New BarcodeGodownItemStockSummReport
    Dim RPTDESIGNSHADESTOCKSUMM As New DesignShadeWiseStockReport
    Dim RPTDESIGNSHADEBRILLANTO As New DesignStockReport_BRILLANTO
    Dim RPTBALESTOCKSUMM As New BaleWiseStockSummReport
    Dim RPTBARCODESTOCKDETAILS As New BarcodeStockDetailsReport
    Dim RPTBARCODESTOCKSUMMIMG As New BarcodeStockSummImageReport
    Dim RPTBARCODEBALESTOCK As New BarcodeBaleWiseStockReport
    Dim RPTITEMDESIGNSHADEBALECOUNT As New ItemDesignShadeBaleCountReport
    Dim RPTNILSTOCKREPORT As New NilStockReport
    Dim RPTITEMWISEDESIGNSTOCK As New ItemWiseDesignStockReport 'ITEMWISEDESIGN ALL COLORS ARE VISIBE, IRRESPECTIVE TO STOCK
    Dim RPTITEMWISEDESIGNSTOCK_DRDRAPES As New ItemWiseDesignStockReport_DRDRAPES 'ITEMWISEDESIGN ALL COLORS ARE VISIBE, IRRESPECTIVE TO STOCK

    Dim RPTGREYSTOCKMONTHLY As New GreyStockMonthlyReport
    Dim RPTDYEINGSTOCKMONTHLY As New GreyStockMonthlyReport
    Dim RPTGODOWNSTOCKMONTHLY As New GreyStockMonthlyReport
    Dim RPTGREYSTOCK As New GreyStockReport

    Private Sub StockDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Sub PRINTDIRECTLYTOPRINTER()
        Try
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table


            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With


            Dim OBJ As New Object
            If FRMSTRING = "NILSTOCKREPORT" Then
                OBJ = New NilStockReport
                OBJ.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
            End If

SKIPINVOICE:
            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = strsearch

            If DIRECTWHATSAPP = True Then
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                oDfDopt.DiskFileName = Application.StartupPath & "\NILSTOCKREPORT.pdf"

                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)

                expo = OBJ.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StockDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTLYTOPRINTER()
                Exit Sub
            End If

            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table


            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With



            If FRMSTRING = "ITEMSTOCKSUMM" Then crTables = RPTITEMSTOCKSUMM.Database.Tables
            If FRMSTRING = "ITEMSTOCKBARCODESUMM" Then crTables = RPTITEMSTOCKBARCODESUMM.Database.Tables
            If FRMSTRING = "QUALITYSTOCKSUMM" Then crTables = RPTQUALITYSTOCKSUMM.Database.Tables
            If FRMSTRING = "DESIGNSTOCKSUMM" Then crTables = RPTDESIGNSTOCKSUMM.Database.Tables
            If FRMSTRING = "SHADESTOCKSUMM" Then crTables = RPTSHADESTOCKSUMM.Database.Tables
            If FRMSTRING = "ITEMSHADESTOCKSUMM" Then crTables = RPTITEMSHADESTOCKSUMM.Database.Tables
            If FRMSTRING = "ITEMSHADEGODOWNSTOCKSUMM" Then crTables = RPTITEMSHADEGODOWNSTOCKSUMM.Database.Tables
            If FRMSTRING = "ITEMQUALITYSTOCKSUMM" Then crTables = RPTITEMQUALITYSTOCKSUMM.Database.Tables
            If FRMSTRING = "ITEMDESIGNSHADESTOCKSUMM" Then crTables = RPTITEMDESIGNSHADESTOCKSUMM.Database.Tables
            If FRMSTRING = "ITEMDESIGNSHADESTOCKSMALLSUMM" Then crTables = RPTITEMDESIGNSHADESTOCKSMALLSUMM.Database.Tables
            If FRMSTRING = "ITEMDESIGNSHADESTOCKNOUNITSMALLSUMM" Then crTables = RPTITEMDESIGNSHADESTOCKNOUNITSMALLSUMM.Database.Tables
            If FRMSTRING = "CATEGORYITEMDESIGNSHADESTOCKNOUNITSMALLSUMM" Then crTables = RPTCATEGORYITEMDESIGNSHADESTOCKNOUNITSMALLSUMM.Database.Tables
            If FRMSTRING = "BARCODEITEMDESIGNSHADESTOCKSUMM" Then crTables = RPTBARCODEITEMDESIGNSHADESTOCKSUMM.Database.Tables
            If FRMSTRING = "BARCODEGODOWNITEMSTOCKSUMM" Then crTables = RPTBARCODEGODOWNITEMSTOCKSUMM.Database.Tables
            If FRMSTRING = "BALESTOCKSUMM" Then crTables = RPTBALESTOCKSUMM.Database.Tables
            If FRMSTRING = "DESIGNSHADESTOCKSUMM" Then crTables = RPTDESIGNSHADESTOCKSUMM.Database.Tables
            If FRMSTRING = "DESIGNSHADEBRILLANTO" Then crTables = RPTDESIGNSHADEBRILLANTO.Database.Tables
            If FRMSTRING = "BARCODESTOCKDETAILS" Then crTables = RPTBARCODESTOCKDETAILS.Database.Tables
            If FRMSTRING = "BARCODESTOCKSUMMIMG" Then crTables = RPTBARCODESTOCKSUMMIMG.Database.Tables
            If FRMSTRING = "BARCODEBALESTOCK" Then crTables = RPTBARCODEBALESTOCK.Database.Tables
            If FRMSTRING = "ITEMDESIGNSHADEBALECOUNT" Then crTables = RPTITEMDESIGNSHADEBALECOUNT.Database.Tables
            If FRMSTRING = "NILSTOCKREPORT" Then crTables = RPTNILSTOCKREPORT.Database.Tables
            If FRMSTRING = "ITEMWISEDESIGNSTOCK" Then
                If ClientName = "DRDRAPES" Then crTables = RPTITEMWISEDESIGNSTOCK_DRDRAPES.Database.Tables Else crTables = RPTITEMWISEDESIGNSTOCK.Database.Tables
            End If

            If FRMSTRING = "GREYSTOCKMONTHLY" Then crTables = RPTGREYSTOCKMONTHLY.Database.Tables
            If FRMSTRING = "DYEINGSTOCKMONTHLY" Then crTables = RPTDYEINGSTOCKMONTHLY.Database.Tables
            If FRMSTRING = "GODOWNSTOCKMONTHLY" Then crTables = RPTGODOWNSTOCKMONTHLY.Database.Tables
            If FRMSTRING = "GREYSTOCK" Then crTables = RPTGREYSTOCK.Database.Tables


            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            crpo.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "ITEMSTOCKSUMM" Then
                RPTITEMSTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ITEM STOCK SUMMARY - " & PERIOD & "'"
                RPTITEMSTOCKSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTITEMSTOCKSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                If SHOWBALANCESTOCKONLY = True Then RPTITEMSTOCKSUMM.DataDefinition.FormulaFields("SHOWBALANCESTOCK").Text = 1
                crpo.ReportSource = RPTITEMSTOCKSUMM
            ElseIf FRMSTRING = "ITEMSTOCKBARCODESUMM" Then
                RPTITEMSTOCKBARCODESUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ITEM STOCK SUMMARY (BARCODE)- " & PERIOD & "'"
                RPTITEMSTOCKBARCODESUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTITEMSTOCKBARCODESUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                crpo.ReportSource = RPTITEMSTOCKBARCODESUMM
            ElseIf FRMSTRING = "QUALITYSTOCKSUMM" Then
                RPTQUALITYSTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' QUALITY STOCK SUMMARY - " & PERIOD & "'"
                RPTQUALITYSTOCKSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTQUALITYSTOCKSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                crpo.ReportSource = RPTQUALITYSTOCKSUMM
            ElseIf FRMSTRING = "DESIGNSTOCKSUMM" Then
                RPTDESIGNSTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' DESIGN STOCK SUMMARY - " & PERIOD & "'"
                RPTDESIGNSTOCKSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTDESIGNSTOCKSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                crpo.ReportSource = RPTDESIGNSTOCKSUMM
            ElseIf FRMSTRING = "SHADESTOCKSUMM" Then
                RPTSHADESTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' SHADE STOCK SUMMARY - " & PERIOD & "'"
                RPTSHADESTOCKSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTSHADESTOCKSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                crpo.ReportSource = RPTSHADESTOCKSUMM
            ElseIf FRMSTRING = "ITEMSHADESTOCKSUMM" Then
                RPTITEMSHADESTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ITEM SHADE STOCK SUMMARY - " & PERIOD & "'"
                RPTITEMSHADESTOCKSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTITEMSHADESTOCKSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                crpo.ReportSource = RPTITEMSHADESTOCKSUMM
            ElseIf FRMSTRING = "ITEMSHADEGODOWNSTOCKSUMM" Then
                RPTITEMSHADEGODOWNSTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ITEM SHADE GODOWN STOCK SUMMARY - " & PERIOD & "'"
                RPTITEMSHADEGODOWNSTOCKSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTITEMSHADEGODOWNSTOCKSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                crpo.ReportSource = RPTITEMSHADEGODOWNSTOCKSUMM
            ElseIf FRMSTRING = "ITEMQUALITYSTOCKSUMM" Then
                RPTITEMQUALITYSTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ITEM QUALITY STOCK SUMMARY - " & PERIOD & "'"
                RPTITEMQUALITYSTOCKSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTITEMQUALITYSTOCKSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                crpo.ReportSource = RPTITEMQUALITYSTOCKSUMM
            ElseIf FRMSTRING = "ITEMDESIGNSHADESTOCKSUMM" Then
                RPTITEMDESIGNSHADESTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ITEM DESIGN SHADE STOCK SUMMARY - " & PERIOD & "'"
                RPTITEMDESIGNSHADESTOCKSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTITEMDESIGNSHADESTOCKSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                crpo.ReportSource = RPTITEMDESIGNSHADESTOCKSUMM
            ElseIf FRMSTRING = "ITEMDESIGNSHADESTOCKSMALLSUMM" Then
                RPTITEMDESIGNSHADESTOCKSMALLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ITEM DESIGN SHADE SMALL STOCK SUMMARY - " & PERIOD & "'"
                RPTITEMDESIGNSHADESTOCKSMALLSUMM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                crpo.ReportSource = RPTITEMDESIGNSHADESTOCKSMALLSUMM
            ElseIf FRMSTRING = "ITEMDESIGNSHADESTOCKNOUNITSMALLSUMM" Then
                RPTITEMDESIGNSHADESTOCKNOUNITSMALLSUMM.DataDefinition.FormulaFields("MTRS").Text = Val(MTRS)
                RPTITEMDESIGNSHADESTOCKNOUNITSMALLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ITEM DESIGN SHADE SMALL STOCK SUMMARY - " & PERIOD & "'"
                RPTITEMDESIGNSHADESTOCKNOUNITSMALLSUMM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                crpo.ReportSource = RPTITEMDESIGNSHADESTOCKNOUNITSMALLSUMM
            ElseIf FRMSTRING = "CATEGORYITEMDESIGNSHADESTOCKNOUNITSMALLSUMM" Then
                RPTCATEGORYITEMDESIGNSHADESTOCKNOUNITSMALLSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ITEM DESIGN SHADE SMALL STOCK SUMMARY - " & PERIOD & "'"
                RPTCATEGORYITEMDESIGNSHADESTOCKNOUNITSMALLSUMM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                crpo.ReportSource = RPTCATEGORYITEMDESIGNSHADESTOCKNOUNITSMALLSUMM
            ElseIf FRMSTRING = "BARCODEITEMDESIGNSHADESTOCKSUMM" Then
                RPTBARCODEITEMDESIGNSHADESTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' ITEM DESIGN SHADE STOCK SUMMARY - " & PERIOD & "'"
                RPTBARCODEITEMDESIGNSHADESTOCKSUMM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                crpo.ReportSource = RPTBARCODEITEMDESIGNSHADESTOCKSUMM
            ElseIf FRMSTRING = "BARCODEGODOWNITEMSTOCKSUMM" Then
                RPTBARCODEGODOWNITEMSTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' GODOWN - ITEM STOCK SUMMARY - " & PERIOD & "'"
                RPTBARCODEGODOWNITEMSTOCKSUMM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                crpo.ReportSource = RPTBARCODEGODOWNITEMSTOCKSUMM
            ElseIf FRMSTRING = "BALESTOCKSUMM" Then
                RPTBALESTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK SUMMARY - " & PERIOD & "'"
                RPTBALESTOCKSUMM.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                crpo.ReportSource = RPTBALESTOCKSUMM
            ElseIf FRMSTRING = "DESIGNSHADESTOCKSUMM" Then
                RPTDESIGNSHADESTOCKSUMM.DataDefinition.FormulaFields("PERIOD").Text = "' DESIGN SHADE STOCK SUMMARY - " & PERIOD & "'"
                RPTDESIGNSHADESTOCKSUMM.DataDefinition.FormulaFields("FROMDATE").Text = "'" & Format(Convert.ToDateTime(FROMDATE).Date, "MM/dd/yyyy") & "'"
                RPTDESIGNSHADESTOCKSUMM.DataDefinition.FormulaFields("TODATE").Text = "'" & Format(Convert.ToDateTime(TODATE).Date, "MM/dd/yyyy") & "'"
                crpo.ReportSource = RPTDESIGNSHADESTOCKSUMM
            ElseIf FRMSTRING = "DESIGNSHADEBRILLANTO" Then
                RPTDESIGNSHADEBRILLANTO.DataDefinition.FormulaFields("PERIOD").Text = "' DESIGN SHADE STOCK SUMMARY - " & PERIOD & "'"
                crpo.ReportSource = RPTDESIGNSHADEBRILLANTO
            ElseIf FRMSTRING = "BARCODESTOCKDETAILS" Then
                RPTBARCODESTOCKDETAILS.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                RPTBARCODESTOCKDETAILS.DataDefinition.FormulaFields("PERIOD").Text = "' BARCODE STOCK DETAILS - " & PERIOD & "'"
                crpo.ReportSource = RPTBARCODESTOCKDETAILS
            ElseIf FRMSTRING = "BARCODESTOCKSUMMIMG" Then
                RPTBARCODESTOCKSUMMIMG.DataDefinition.FormulaFields("PERIOD").Text = "' BARCODE STOCK SUMMARY (IMAGE) - " & PERIOD & "'"
                crpo.ReportSource = RPTBARCODESTOCKSUMMIMG
            ElseIf FRMSTRING = "BARCODEBALESTOCK" Then
                RPTBARCODEBALESTOCK.DataDefinition.FormulaFields("PERIOD").Text = "' BALE STOCK SUMMARY (BARCODE) - " & PERIOD & "'"
                crpo.ReportSource = RPTBARCODEBALESTOCK
            ElseIf FRMSTRING = "ITEMDESIGNSHADEBALECOUNT" Then
                RPTITEMDESIGNSHADEBALECOUNT.DataDefinition.FormulaFields("PERIOD").Text = "' ITEM - DESIGN - SHADE WISE BALE COUNT STOCK - " & PERIOD & "'"
                crpo.ReportSource = RPTITEMDESIGNSHADEBALECOUNT
            ElseIf FRMSTRING = "NILSTOCKREPORT" Then
                RPTNILSTOCKREPORT.DataDefinition.FormulaFields("PERIOD").Text = "' NIL STOCK REPORT - " & PERIOD & "'"
                crpo.ReportSource = RPTNILSTOCKREPORT
            ElseIf FRMSTRING = "ITEMWISEDESIGNSTOCK" Then
                If ClientName = "DRDRAPES" Then
                    RPTITEMWISEDESIGNSTOCK.DataDefinition.FormulaFields("PERIOD").Text = "' NIL STOCK REPORT - " & PERIOD & "'"
                    crpo.ReportSource = RPTITEMWISEDESIGNSTOCK_DRDRAPES
                Else
                    RPTITEMWISEDESIGNSTOCK.DataDefinition.FormulaFields("PERIOD").Text = "' NIL STOCK REPORT - " & PERIOD & "'"
                    crpo.ReportSource = RPTITEMWISEDESIGNSTOCK
                End If


            ElseIf FRMSTRING = "GREYSTOCKMONTHLY" Then
                RPTGREYSTOCKMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                crpo.ReportSource = RPTGREYSTOCKMONTHLY
            ElseIf FRMSTRING = "DYEINGSTOCKMONTHLY" Then
                RPTDYEINGSTOCKMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                crpo.ReportSource = RPTDYEINGSTOCKMONTHLY
            ElseIf FRMSTRING = "GODOWNSTOCKMONTHLY" Then
                RPTGODOWNSTOCKMONTHLY.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                crpo.ReportSource = RPTGODOWNSTOCKMONTHLY
            ElseIf FRMSTRING = "GREYSTOCK" Then
                RPTGREYSTOCK.DataDefinition.FormulaFields("PERIOD").Text = "'" & PERIOD & "'"
                crpo.ReportSource = RPTGREYSTOCK
            End If

            crpo.Zoom(100)
            crpo.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()
            Dim TEMPATTACHMENT As String = "STOCK"
            Dim objmail As New SendMail
            objmail.subject = "STOCK"
            objmail.attachment = TEMPATTACHMENT
            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
            End If

            objmail.attachment = TEMPATTACHMENT
            objmail.attachment = Application.StartupPath & "\" & TEMPATTACHMENT & ".PDF"

            objmail.Show()
            objmail.BringToFront()
            Windows.Forms.Cursor.Current = Cursors.Arrow
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\STOCK.pdf"

            If FRMSTRING = "ITEMSTOCKSUMM" Then
                expo = RPTITEMSTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMSTOCKSUMM.Export()

            ElseIf FRMSTRING = "ITEMSTOCKBARCODESUMM" Then
                expo = RPTITEMSTOCKBARCODESUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMSTOCKBARCODESUMM.Export()

            ElseIf FRMSTRING = "QUALITYSTOCKSUMM" Then
                expo = RPTQUALITYSTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTQUALITYSTOCKSUMM.Export()

            ElseIf FRMSTRING = "DESIGNSTOCKSUMM" Then
                expo = RPTDESIGNSTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDESIGNSTOCKSUMM.Export()

            ElseIf FRMSTRING = "SHADESTOCKSUMM" Then
                expo = RPTSHADESTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSHADESTOCKSUMM.Export()

            ElseIf FRMSTRING = "ITEMSHADESTOCKSUMM" Then
                expo = RPTITEMSHADESTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMSHADESTOCKSUMM.Export()

            ElseIf FRMSTRING = "ITEMSHADEGODOWNSTOCKSUMM" Then
                expo = RPTITEMSHADEGODOWNSTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMSHADEGODOWNSTOCKSUMM.Export()

            ElseIf FRMSTRING = "ITEMQUALITYSTOCKSUMM" Then
                expo = RPTITEMQUALITYSTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMQUALITYSTOCKSUMM.Export()

            ElseIf FRMSTRING = "ITEMDESIGNSHADESTOCKSUMM" Then
                expo = RPTITEMDESIGNSHADESTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMDESIGNSHADESTOCKSUMM.Export()

            ElseIf FRMSTRING = "ITEMDESIGNSHADESTOCKSMALLSUMM" Then
                expo = RPTITEMDESIGNSHADESTOCKSMALLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMDESIGNSHADESTOCKSMALLSUMM.Export()

            ElseIf FRMSTRING = "ITEMDESIGNSHADESTOCKNOUNITSMALLSUMM" Then
                expo = RPTITEMDESIGNSHADESTOCKNOUNITSMALLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMDESIGNSHADESTOCKNOUNITSMALLSUMM.Export()

            ElseIf FRMSTRING = "CATEGORYITEMDESIGNSHADESTOCKNOUNITSMALLSUMM" Then
                expo = RPTCATEGORYITEMDESIGNSHADESTOCKNOUNITSMALLSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCATEGORYITEMDESIGNSHADESTOCKNOUNITSMALLSUMM.Export()

            ElseIf FRMSTRING = "BARCODEITEMDESIGNSHADESTOCKSUMM" Then
                expo = RPTBARCODEITEMDESIGNSHADESTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBARCODEITEMDESIGNSHADESTOCKSUMM.Export()


            ElseIf FRMSTRING = "BARCODEGODOWNITEMSTOCKSUMM" Then
                expo = RPTBARCODEGODOWNITEMSTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBARCODEGODOWNITEMSTOCKSUMM.Export()

            ElseIf FRMSTRING = "BALESTOCKSUMM" Then
                expo = RPTBALESTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBALESTOCKSUMM.Export()

            ElseIf FRMSTRING = "DESIGNSHADESTOCKSUMM" Then
                expo = RPTDESIGNSHADESTOCKSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDESIGNSHADESTOCKSUMM.Export()

            ElseIf FRMSTRING = "DESIGNSHADEBRILLANTO" Then
                expo = RPTDESIGNSHADEBRILLANTO.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDESIGNSHADEBRILLANTO.Export()

            ElseIf FRMSTRING = "BARCODESTOCKDETAILS" Then
                expo = RPTBARCODESTOCKDETAILS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBARCODESTOCKDETAILS.Export()

            ElseIf FRMSTRING = "BARCODESTOCKSUMMIMG" Then
                expo = RPTBARCODESTOCKSUMMIMG.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBARCODESTOCKSUMMIMG.Export()

            ElseIf FRMSTRING = "BARCODEBALESTOCK" Then
                expo = RPTBARCODEBALESTOCK.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTBARCODEBALESTOCK.Export()

            ElseIf FRMSTRING = "ITEMDESIGNSHADEBALECOUNT" Then
                expo = RPTITEMDESIGNSHADEBALECOUNT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTITEMDESIGNSHADEBALECOUNT.Export()

            ElseIf FRMSTRING = "NILSTOCKREPORT" Then
                expo = RPTNILSTOCKREPORT.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTNILSTOCKREPORT.Export()

            ElseIf FRMSTRING = "ITEMWISEDESIGNSTOCK" Then
                If ClientName = "DRDRAPES" Then
                    expo = RPTITEMWISEDESIGNSTOCK_DRDRAPES.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTITEMWISEDESIGNSTOCK_DRDRAPES.Export()
                Else
                    expo = RPTITEMWISEDESIGNSTOCK.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTITEMWISEDESIGNSTOCK.Export()
                End If


            ElseIf FRMSTRING = "GREYSTOCKMONTHLY" Then
                expo = RPTGREYSTOCKMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYSTOCKMONTHLY.Export()

            ElseIf FRMSTRING = "DYEINGSTOCKMONTHLY" Then
                expo = RPTDYEINGSTOCKMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTDYEINGSTOCKMONTHLY.Export()

            ElseIf FRMSTRING = "GODOWNSTOCKMONTHLY" Then
                expo = RPTGODOWNSTOCKMONTHLY.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGODOWNSTOCKMONTHLY.Export()
            ElseIf FRMSTRING = "GREYSTOCK" Then
                expo = RPTGREYSTOCK.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTGREYSTOCK.Export()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()
            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\STOCK.PDF")
            OBJWHATSAPP.FILENAME.Add("STOCK.pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class