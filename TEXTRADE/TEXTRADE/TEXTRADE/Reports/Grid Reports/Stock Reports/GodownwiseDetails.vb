
Imports System.IO
Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class GodownwiseDetails

    Public FRMSTRING As String
    Public TEMPDESIGNNO As String = ""
    Public TEMPCOLOR As String = ""
    Public TEMPGODOWN As String = ""
    Public TEMPITEMNAME As String = ""
    Public WHERECLAUSE As String = ""

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Opening_Stock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
           End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Opening_Stock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            CMDREFRESH_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try

            'IF CHK IS TRUE THEN GET ALL YEARID UDER THAT COMPANY
            Dim TEMPCONDITION As String = ""
            If WHERECLAUSE <> "" Then TEMPCONDITION = TEMPCONDITION & WHERECLAUSE
            If CHKALLCMP.Checked = True Then TEMPCONDITION = TEMPCONDITION & " And YEARID IN (SELECT YEAR_ID FROM YEARMASTER WHERE YEAR_STARTDATE = '" & Format(AccFrom.Date, "MM/dd/yyyy") & "')" Else TEMPCONDITION = TEMPCONDITION & " AND YEARID = " & YearId


            If ClientName = "YAMUNESH" Or ClientName = "MOMAI" Or ClientName = "AXIS" Then
                TEMPCONDITION = TEMPCONDITION & " AND ROUND(PCS,0) > 0 "
            ElseIf ClientName = "MOHAN" Then
                TEMPCONDITION = TEMPCONDITION & " AND ROUND(MTRS,2) > 0 "
            End If


            If TEMPITEMNAME <> "" Then TEMPCONDITION = TEMPCONDITION & " AND ITEMNAME='" & TEMPITEMNAME & "'"
            If TEMPDESIGNNO <> "" Then TEMPCONDITION = TEMPCONDITION & " AND DESIGNNO='" & TEMPDESIGNNO & "'"
            If TEMPCOLOR <> "" Then TEMPCONDITION = TEMPCONDITION & " AND COLOR='" & TEMPCOLOR & "'"
            'If TEMPGODOWN <> "" Then TEMPCONDITION = TEMPCONDITION & " AND GODOWN='" & TEMPGODOWN & "'"

            Dim GROUPBY As String = " GROUP BY ITEMNAME, QUALITY, PIECETYPE, BARCODE, UNIT, BALENO, MILLNAME"
            If TEMPDESIGNNO <> "" Then GROUPBY = GROUPBY & ", DESIGNNO"
            If TEMPCOLOR <> "" Then GROUPBY = GROUPBY & ", COLOR"
            If TEMPGODOWN <> "" Then GROUPBY = GROUPBY & ", GODOWN" Else TEMPGODOWN = USERGODOWN

            Dim OBJCMN As New ClsCommon
            Dim dt As DataTable

            Dim DTUNIT As DataTable = OBJCMN.search("UNIT_ABBR", "", "DEFAULTSTOCKUNIT", "")
            If DTUNIT.Rows.Count > 0 Then TEMPCONDITION = TEMPCONDITION & " AND UNIT IN (SELECT UNIT_ABBR FROM DEFAULTSTOCKUNIT)"

            'If TEMPDESIGNNO = "" And TEMPCOLOR = "" And TEMPGODOWN = "" Then
            dt = OBJCMN.Execute_Any_String(" SELECT SUM(PCS) AS TOTALPCS, SUM(MTRS) AS TOTALMTRS, DESIGNNO, ITEMNAME, QUALITY, COLOR ,GODOWN, LOTNO, BALENO, CHALLANNO, PIECETYPE, BARCODE, UNIT, ITEMCODE, CATEGORY,PURRATE, SALERATE, DESIGNRATE,RACK,SHELF, MILLNAME, DATE, JOBBERNAME, TYPE, GRIDREMARKS, PURNAME, ROUND(PURRATE*SUM(MTRS),2) AS AMOUNT, DISPLAYNAME, SUM(WT) AS WT FROM  BARCODESTOCK WHERE 1 = 1 " & TEMPCONDITION & " GROUP BY DESIGNNO, ITEMNAME, QUALITY, LOTNO, BALENO, CHALLANNO, COLOR ,GODOWN, PIECETYPE, BARCODE, UNIT, ITEMCODE, CATEGORY, PURRATE,RACK,SHELF, SALERATE, DESIGNRATE, MILLNAME, DATE, JOBBERNAME, TYPE, GRIDREMARKS, PURNAME, DISPLAYNAME ORDER BY DESIGNNO, QUALITY, COLOR", "", "")
            'Else
            'dt = OBJCMN.Execute_Any_String(" SELECT SUM(PCS) AS TOTALPCS, SUM(MTRS) AS TOTALMTRS,'" & TEMPDESIGNNO & "' AS DESIGNNO, '" & TEMPCOLOR & "' AS COLOR , '" & TEMPGODOWN & "' AS GODOWN, ITEMNAME, QUALITY,  BALENO, PIECETYPE, BARCODE, UNIT, MILLNAME FROM BARCODESTOCK WHERE 1 = 1 " & TEMPCONDITION & GROUPBY, "", "")
            'End If
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Stock In Hand.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo
            opti.SheetName = "Stock In Hand"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Stock In Hand", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub GodownwiseDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "SVS" Then GITEMNAME.Visible = False
        If ClientName = "VINIT" Then
            GDESIGNNO.Visible = False
            GCOLOR.Visible = False
            GRACK.Visible = False
            GSHELF.Visible = False
            GPURNAME.Visible = True
        End If
        If ClientName = "SAFFRON" Then
            GITEMCODE.Visible = True
            GITEMCODE.VisibleIndex = 0
            GCATEGORY.Visible = True
            GCATEGORY.VisibleIndex = 2
            GDISPLAYNAME.Visible = True
            GDISPLAYNAME.VisibleIndex = 3
        End If

        If ClientName = "AVIS" Or ClientName = "REALCORPORATION" Then
            GMILLNAME.Visible = True
            GMILLNAME.VisibleIndex = GDESIGNNO.VisibleIndex + 1
        End If



        If ClientName = "SAKARIA" Or ClientName = "TINUMINU" Or ClientName = "RADHA" Then
            GNAME.Visible = True
            GNAME.VisibleIndex = GGODOWN.VisibleIndex + 1
            GPURNAME.Visible = True
        End If

        If ClientName = "KARAN" Then
            GCATEGORY.Visible = True
            GCATEGORY.VisibleIndex = GITEMNAME.VisibleIndex + 1
        End If

        If ClientName = "SOFTAS" Then
            GQUALITY.VisibleIndex = GTOTALMTRS.VisibleIndex + 1
            GRACK.VisibleIndex = GQUALITY.VisibleIndex + 1
        End If

        If ClientName = "DILIP" Or ClientName = "DILIPNEW" Then
            GQUALITY.Visible = False
            GDESIGNNO.Visible = False
            GCOLOR.Visible = False
        End If

        If ClientName = "BIGAPPLE" Then
            GPIECETYPE.Visible = False
            GRACK.Visible = False
            GSHELF.Visible = False
            GCHALLANNO.Visible = False
            GDATE.Visible = False
            GPURRATE.Visible = False
            GAMT.Visible = False
            GWT.Visible = True
            GWT.VisibleIndex = GTOTALMTRS.VisibleIndex + 1
        End If


    End Sub

    Private Sub CMDSAVELAYOUT_Click(sender As Object, e As EventArgs) Handles CMDSAVELAYOUT.Click
        Try
            Dim layoutFileName As String = $"{Me.Name}"
            Dim layoutPath As String = System.IO.Path.Combine(Application.StartupPath, layoutFileName)
            gridbill.SaveLayoutToXml(layoutPath)
            'MessageBox.Show("Layout saved as: " & layoutFileName)




            ' Prompt user for filename
            Dim userFileName As String = InputBox("Enter a name for the layout file (without extension):", "Save Layout", Me.Name)

            ' Exit if the user cancels or enters nothing
            If String.IsNullOrWhiteSpace(userFileName) Then
                MessageBox.Show("Save cancelled.")
                Exit Sub
            End If

            ' Add .xml extension and construct path
            Dim FileName As String = $"{userFileName}.xml"

            ' Save layout to file
            gridbill.SaveLayoutToXml(layoutPath)
            MessageBox.Show("Layout saved as: " & FileName)

            ' Read file content
            Dim xmlContent As String = File.ReadAllText(layoutPath)



            Dim OBJSELECTSG As New SelectCustomLayout
            OBJSELECTSG.FORMNAMES = layoutFileName
            OBJSELECTSG.FILENAME = FileName
            OBJSELECTSG.FILES = xmlContent
            OBJSELECTSG.ShowDialog()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class