
Imports BL
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Windows.Forms
Imports System.IO

Public Class SaleOrderDesign

    Public FORMULA As String

    Dim RPTSO As New SOReport
    Dim RPTSO_GELATO As New SOReport_GELATO
    Dim RPTSO_KENCOT As New SOReport_KENCOT
    Dim RPTSO_SAFFRON As New SOReport_SAFFRON
    Dim RPTSO_CC As New SOReport_CC
    Dim RPTSO_MAHAVIR As New SOReport_MAHAVIR
    Dim RPTSO_YASHVIYUMILONE As New SOReport_YASHVIYUMILONE
    Dim RPTSO_SBA As New SOReport_SBA
    Dim RPTSO_AVISSUP As New SOReport_AVISSUP
    Dim RPTSO_AVIS_CAD As New SOReport_AVIS_CAD
    Dim RPTSO_MAHAVIRPOLYCOT As New SOReport_MAHAVIRPOLYCOT



    Dim RPTSTOCKRECO As New StockRecoReport
    Dim RPTSTOCKRECO_CUTTING As New StockRecoCuttingReport

    'SCHEDULE REPORT
    Dim RPTSCHREPORT As New SCHReport_SAFFRON

    Dim tempattachment As String
    Dim ConInfo As New CrystalDecisions.Shared.TableLogOnInfo
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions
    Public vendorname As String
    'NEWLY ADDED

    Public FRMSTRING As String
    Public FROMDATE As String
    Public TODATE As String
    Public OPENINGDATE As String
    Public selfor_ss As String
    Public PERIOD As String
    Public PARTYNAME As String
    Public AGENTNAME As String
    Public SHIPTONAME As String
    Public SONO As Integer
    Public RATERACK As String = "RATE"
    Public WITHPHOTO As Boolean = False
    Public PARTYSOREPORT As Boolean = False

    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public DIRECTWHATSAPP As Boolean = False
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1

    Private Sub SaleOrderDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SaleOrderDesign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTADVICE()
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor


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

            If FRMSTRING = "SOREPORT" Then
                If ClientName = "AVIS" Or ClientName = "SUPRIYA" Or ClientName = "SHREEVALLABH" Or ClientName = "RAJDEEP" Then
                    crTables = RPTSO_AVISSUP.Database.Tables
                ElseIf ClientName = "GELATO" Then
                    crTables = RPTSO_GELATO.Database.Tables
                ElseIf ClientName = "SAFFRON" Then
                    crTables = RPTSO_SAFFRON.Database.Tables
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    crTables = RPTSO_CC.Database.Tables
                ElseIf ClientName = "MAHAVIR" Then
                    crTables = RPTSO_MAHAVIR.Database.Tables
                ElseIf ClientName = "YASHVI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "KRISHNA" Or ClientName = "KRFABRICS" Or ClientName = "SHREENAKODA" Then
                    crTables = RPTSO_YASHVIYUMILONE.Database.Tables
                ElseIf ClientName = "SBA" Then
                    crTables = RPTSO_SBA.Database.Tables
                ElseIf ClientName = "KENCOT" Or ClientName = "SPCORP" Then
                    crTables = RPTSO_KENCOT.Database.Tables
                ElseIf ClientName = "MAHAVIRPOLYCOT" And PARTYSOREPORT = True Then
                    crTables = RPTSO_MAHAVIRPOLYCOT.Database.Tables
                Else
                    crTables = RPTSO.Database.Tables
                End If
            ElseIf FRMSTRING = "SOCAD" Then
                crTables = RPTSO_AVIS_CAD.Database.Tables
            ElseIf FRMSTRING = "SCHEDULEREPORT" Then
                crTables = RPTSCHREPORT.Database.Tables
            ElseIf FRMSTRING = "STOCKRECO" Then
                If ClientName = "MOHATUL" Then
                    crTables = RPTSTOCKRECO_CUTTING.Database.Tables
                Else
                    crTables = RPTSTOCKRECO.Database.Tables
                End If
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            crpo.SelectionFormula = FORMULA

            If FRMSTRING = "SOREPORT" Then
                If ClientName = "AVIS" Or ClientName = "SUPRIYA" Or ClientName = "SHREEVALLABH" Or ClientName = "RAJDEEP" Then
                    'GET TOTAL NO OF DESIGNS IN SALEORDER
                    Dim OBJCMN As New ClsCommon
                    Dim dt As DataTable = OBJCMN.search("DISTINCT COUNT(SO_DESIGNID) AS DESIGN", "", "SALEORDER_DESC", " AND SO_NO = " & SONO & " and SO_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then RPTSO_AVISSUP.DataDefinition.FormulaFields("TOTALDESIGNCOUNT").Text = Val(dt.Rows(0).Item("DESIGN"))
                    RPTSO_AVISSUP.DataDefinition.FormulaFields("RATERACK").Text = "'" & RATERACK & "'"
                    crpo.ReportSource = RPTSO_AVISSUP
                ElseIf ClientName = "GELATO" Then
                    crpo.ReportSource = RPTSO_GELATO
                    If WITHPHOTO = True Then RPTSO_GELATO.DataDefinition.FormulaFields("WITHPHOTO").Text = 1
                ElseIf ClientName = "SAFFRON" Then
                    crpo.ReportSource = RPTSO_SAFFRON
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    crpo.ReportSource = RPTSO_CC
                ElseIf ClientName = "MAHAVIR" Then
                    crpo.ReportSource = RPTSO_MAHAVIR
                ElseIf ClientName = "YASHVI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "KRISHNA" Or ClientName = "KRFABRICS" Or ClientName = "SHREENAKODA" Then
                    crpo.ReportSource = RPTSO_YASHVIYUMILONE
                    RPTSO_YASHVIYUMILONE.DataDefinition.FormulaFields("RATERACK").Text = "'" & RATERACK & "'"
                ElseIf ClientName = "SBA" Then
                    crpo.ReportSource = RPTSO_SBA
                ElseIf ClientName = "KENCOT" Or ClientName = "SPCORP" Then
                    crpo.ReportSource = RPTSO_KENCOT
                ElseIf ClientName = "MAHAVIRPOLYCOT" And PARTYSOREPORT = True Then
                    crpo.ReportSource = RPTSO_MAHAVIRPOLYCOT
                Else
                    RPTSO.DataDefinition.FormulaFields("CLIENTNAME").Text = "'" & ClientName & "'"
                    crpo.ReportSource = RPTSO
                    If WITHPHOTO = True Then RPTSO.DataDefinition.FormulaFields("WITHPHOTO").Text = 1
                    RPTSO.DataDefinition.FormulaFields("RATERACK").Text = "'" & RATERACK & "'"
                End If
            ElseIf FRMSTRING = "SOCAD" Then
                crpo.ReportSource = RPTSO_AVIS_CAD
            ElseIf FRMSTRING = "SCHEDULEREPORT" Then
                crpo.ReportSource = RPTSCHREPORT
            ElseIf FRMSTRING = "STOCKRECO" Then
                If ClientName = "MOHATUL" Then
                    crpo.ReportSource = RPTSTOCKRECO_CUTTING
                Else
                    crpo.ReportSource = RPTSTOCKRECO
                End If
            End If

            '************************ END *******************
            crpo.Zoom(100)
            crpo.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click

        Dim emailid As String = ""
        Dim emailid1 As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()

        If PARTYNAME <> "" Then
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable = OBJCMN.search("ACC_EMAIL As EMAILID", "", "LEDGERS", " And ACC_CMPNAME = '" & PARTYNAME & "' AND ACC_YEARID=" & YearId)
            If dt.Rows.Count > 0 Then
                emailid = dt.Rows(0).Item(0).ToString
            End If
        End If

        If AGENTNAME <> "" Then
            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable = OBJCMN.search("ACC_EMAIL AS EMAILID", "", "LEDGERS", " and ACC_CMPNAME = '" & AGENTNAME & "' AND ACC_YEARID=" & YearId)
            If dt.Rows.Count > 0 Then
                emailid1 = dt.Rows(0).Item(0).ToString
            End If
        End If

        Dim tempattachment As String

        Dim objmail As New SendMail

        If FRMSTRING = "" Then
            tempattachment = PARTYNAME & "_SOREPORT"
            objmail.subject = "Sale Order"
        ElseIf FRMSTRING = "SOREPORT" Or FRMSTRING = "SOCAD" Then
            tempattachment = PARTYNAME & "_SOREPORT"
            objmail.subject = "Sale Order"

        ElseIf FRMSTRING = "SCHEDULEREPORT" Then
            tempattachment = "SCHEDULEREPORT"
            objmail.subject = "Schedule"

        ElseIf FRMSTRING = "SAMPLENOTE" Then
            tempattachment = "SAMPLENOTE"
            objmail.subject = "Sample Note"
        ElseIf FRMSTRING = "SAMPLEPRICELIST" Then
            tempattachment = "SAMPLEPRICELIST"
            objmail.subject = "Sample Price List"
        End If

        Try
            'Dim objmail As New SendMail
            objmail.attachment = tempattachment
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"
            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
            End If
            If emailid1 <> "" Then
                objmail.cmbsecondadd.Text = emailid1
            End If
            objmail.Show()
            objmail.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
        Windows.Forms.Cursor.Current = Cursors.Arrow
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions

            If FRMSTRING = "" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_SOREPORT.PDF"
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt

            ElseIf FRMSTRING = "SOCAD" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_SOREPORT.PDF"
                expo = RPTSO_AVIS_CAD.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTSO_AVIS_CAD.Export()

            ElseIf FRMSTRING = "SOREPORT" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_SOREPORT.PDF"
                If ClientName = "SAFFRON" Then
                    expo = RPTSO_SAFFRON.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSO_SAFFRON.Export()
                ElseIf ClientName = "GELATO" Then
                    expo = RPTSO_GELATO.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSO_GELATO.Export()
                ElseIf ClientName = "AVIS" Or ClientName = "SUPRIYA" Or ClientName = "SHREEVALLABH" Or ClientName = "RAJDEEP" Then
                    expo = RPTSO_AVISSUP.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSO_AVISSUP.Export()
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    expo = RPTSO_CC.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSO_CC.Export()
                ElseIf ClientName = "MAHAVIR" Then
                    expo = RPTSO_MAHAVIR.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSO_MAHAVIR.Export()
                ElseIf ClientName = "YASHVI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "KRISHNA" Or ClientName = "KRFABRICS" Or ClientName = "SHREENAKODA" Then
                    expo = RPTSO_YASHVIYUMILONE.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSO_YASHVIYUMILONE.Export()
                ElseIf ClientName = "SBA" Then
                    expo = RPTSO_SBA.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSO_SBA.Export()
                ElseIf ClientName = "KENCOT" Or ClientName = "SPCORP" Then
                    expo = RPTSO_KENCOT.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSO_KENCOT.Export()
                ElseIf ClientName = "MAHAVIRPOLYCOT" And PARTYSOREPORT = True Then
                    expo = RPTSO_MAHAVIRPOLYCOT.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSO_MAHAVIRPOLYCOT.Export()
                Else
                    expo = RPTSO.ExportOptions
                    RPTSO.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSO.Export()
                    RPTSO.DataDefinition.FormulaFields("SENDMAIL").Text = "0"

                End If
            ElseIf FRMSTRING = "SCHEDULEREPORT" Then
                If ClientName = "SAFFRON" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\SCHEDULEREPORT.PDF"
                    expo = RPTSCHREPORT.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSCHREPORT.Export()
                End If

            ElseIf FRMSTRING = "STOCKRECO" Then

                If ClientName = "MOHATUL" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\STOCKRECO.PDF"
                    expo = RPTSTOCKRECO_CUTTING.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSTOCKRECO_CUTTING.Export()

                Else
                    oDfDopt.DiskFileName = Application.StartupPath & "\STOCKRECO.PDF"
                    expo = RPTSTOCKRECO.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSTOCKRECO.Export()
                End If

            Else
                oDfDopt.DiskFileName = Application.StartupPath & "\" & PARTYNAME & "_SOREPORT.PDF"
                '  expo = rptssum.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                'rptssum.Export()
            End If



        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Sub PRINTDIRECTADVICE()
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

            If FRMSTRING = "SOREPORT" Then
                If ClientName = "AVIS" Or ClientName = "SUPRIYA" Or ClientName = "SHREEVALLABH" Or ClientName = "RAJDEEP" Then
                    OBJ = New SOReport_AVISSUP
                ElseIf ClientName = "SAFFRON" Then
                    OBJ = New SOReport_SAFFRON
                ElseIf ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    OBJ = New SOReport_CC
                ElseIf ClientName = "MAHAVIR" Then
                    OBJ = New SOReport_MAHAVIR
                ElseIf ClientName = "YASHVI" Or ClientName = "YUMILONE" Or ClientName = "REVAANT" Or ClientName = "KRISHNA" Or ClientName = "KRFABRICS" Or ClientName = "SHREENAKODA" Then
                    OBJ = New SOReport_YASHVIYUMILONE
                ElseIf ClientName = "SBA" Then
                    OBJ = New SOReport_SBA
                ElseIf ClientName = "KENCOT" Or ClientName = "SPCORP" Then
                    OBJ = New SOReport_KENCOT
                ElseIf ClientName = "MAHAVIRPOLYCOT" And PARTYSOREPORT = True Then
                    OBJ = New SOReport_MAHAVIRPOLYCOT
                Else
                    OBJ = New SOReport
                End If
            ElseIf FRMSTRING = "SOCAD" Then
                OBJ = New SOReport_AVIS_CAD
            ElseIf FRMSTRING = "SCHEDULEREPORT" Then
                OBJ = New SCHReport_SAFFRON
            ElseIf FRMSTRING = "SAMPLENOTE" Then
                OBJ = New SampleNoteReport
            ElseIf FRMSTRING = "SAMPLEPRICELIST" Then
                OBJ = New SamplePriceListReport
            ElseIf FRMSTRING = "STOCKRECO" Then
                If ClientName = "MOHATUL" Then
                    OBJ = New StockRecoCuttingReport
                Else
                    OBJ = New StockRecoReport
                End If
            End If


            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = FORMULA

            If DIRECTMAIL = False And DIRECTWHATSAPP = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions


                Dim TEMPATTACHMENT As String = ""
                If FRMSTRING = "" Then
                    TEMPATTACHMENT = PARTYNAME & "_SOREPORT"
                ElseIf FRMSTRING = "SOREPORT" Or FRMSTRING = "SOCAD" Then
                    TEMPATTACHMENT = PARTYNAME & "_SOREPORT"
                ElseIf FRMSTRING = "SCHEDULEREPORT" Then
                    TEMPATTACHMENT = "SCHEDULEREPORT"
                ElseIf FRMSTRING = "SAMPLENOTE" Then
                    TEMPATTACHMENT = "SAMPLENOTE"
                ElseIf FRMSTRING = "SAMPLEPRICELIST" Then
                    TEMPATTACHMENT = "SAMPLEPRICELIST"
                End If


                oDfDopt.DiskFileName = Application.StartupPath & "\" & TEMPATTACHMENT & "_" & SONO & ".pdf"

                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "1"
                expo = OBJ.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = "0"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            Transfer()

            If FRMSTRING = "" Then
                tempattachment = PARTYNAME & "_SOREPORT"
            ElseIf FRMSTRING = "SOREPORT" Or FRMSTRING = "SOCAD" Then
                tempattachment = PARTYNAME & "_SOREPORT"
            ElseIf FRMSTRING = "SCHEDULEREPORT" Then
                tempattachment = "SCHEDULEREPORT"
            ElseIf FRMSTRING = "SAMPLENOTE" Then
                tempattachment = "SAMPLENOTE"
            ElseIf FRMSTRING = "SAMPLEPRICELIST" Then
                tempattachment = "SAMPLEPRICELIST"
            End If

            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = PARTYNAME
            If ClientName <> "MAHAVIRPOLYCOT" Then OBJWHATSAPP.AGENTNAME = AGENTNAME
            If PARTYNAME <> SHIPTONAME Then OBJWHATSAPP.OTHERNAME1 = SHIPTONAME
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & tempattachment & ".PDF")
            OBJWHATSAPP.FILENAME.Add(tempattachment & ".pdf")
            OBJWHATSAPP.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class