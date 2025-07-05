
Imports System.ComponentModel
Imports BL

Public Class YarnRecdFromJobber

    Dim IntResult As Integer
    Dim GRIDDOUBLECLICK As Boolean
    Dim GRIDUPLOADDOUBLECLICK As Boolean
    Public EDIT As Boolean          'used for editing
    Public tempYARNno As Integer     'used for poation no while editing
    Public temptypename, TEMPPARTYBILLNO As String 'used for poation no while editing
    Dim TEMPROW As Integer
    Dim TEMPUPLOADROW As Integer
    Public Shared selectPOtable As New DataTable
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim TEMPMSG As Integer
    Public FRMSTRING As String
    Dim PARTYCHALLANNO As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        EP.Clear()
        CMBTONAME.Enabled = True
        CMBTONAME.Text = ""
        CMBPROCESS.Text = ""

        CMBYARNQUALITY.Text = ""
        CMBDESIGN.Text = ""
        If USERGODOWN <> "" Then CMBGODOWN.Text = USERGODOWN Else CMBGODOWN.Text = ""
        CMBTONAME.Text = ""
        CMBTONAME.Enabled = True
        CMBJONO.Text = ""
        CMBJONO.Enabled = True
        TXTPROGNO.Clear()
        TXTPROGFROMTYPE.Clear()

        TXTBALWT.Clear()

        TXTLOTNO.Clear()
        txtPartyMtrs.Clear()
        dtpchallan.Value = Now.Date
        txtchallan.Clear()
        'txtpono.Clear()
        'podate.Value = now.date

        txtadd.Clear()
        YARNDATE.Text = Now.Date
        'RECDATE.Text = now.date

        cmbtrans.Text = ""

        txttransref.Clear()
        txttransremarks.Clear()

        txtremarks.Clear()

        txtuploadsrno.Clear()
        txtuploadname.Clear()
        txtuploadremarks.Clear()
        txtimgpath.Clear()
        TXTFILENAME.Clear()
        TXTNEWIMGPATH.Clear()
        PBSoftCopy.Image = Nothing
        PBSoftCopy.ImageLocation = ""
        gridupload.RowCount = 0



        lbllocked.Visible = False
        PBlock.Visible = False

        txtsrno.Text = 1
        txtgridremarks.Clear()
        CMBYARNQUALITY.Text = ""
        CMBMILL.Text = ""
        cmbcolor.Text = ""
        TXTJOBBERLOTNO.Clear()
        txtqty.Clear()
        TXTWT.Clear()
        TXTCUT.Clear()
        TXTMTRS.Clear()
        TXTCONES.Clear()
        GRIDYARN.RowCount = 0
        GRIDPROG.RowCount = 0
        cmbtrans.Text = ""

        CMDSELECTPROG.Enabled = True
        GRIDDOUBLECLICK = False
        GRIDUPLOADDOUBLECLICK = False

        getmaxno()


        LBLTOTALCONES.Text = 0
        lbltotalqty.Text = 0
        LBLTOTALWT.Text = 0

    End Sub

    Sub TOTAL()
        Try
            LBLTOTALCUT.Text = 0
            LBLTOTALMTRS.Text = 0
            LBLTOTALCONES.Text = 0.0
            LBLTOTALWT.Text = 0.0
            lbltotalqty.Text = 0.0
            For Each ROW As DataGridViewRow In GRIDYARN.Rows
                If ROW.Cells(gsrno.Index).Value <> Nothing Then
                    lbltotalqty.Text = Format(Val(lbltotalqty.Text) + Val(ROW.Cells(GQTY.Index).EditedFormattedValue), "0.00")
                    LBLTOTALCUT.Text = Format(Val(LBLTOTALCUT.Text) + Val(ROW.Cells(GCUT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALMTRS.Text = Format(Val(LBLTOTALMTRS.Text) + Val(ROW.Cells(GMTRS.Index).EditedFormattedValue), "0.00")
                    LBLTOTALWT.Text = Format(Val(LBLTOTALWT.Text) + Val(ROW.Cells(GWT.Index).EditedFormattedValue), "0.00")
                    LBLTOTALCONES.Text = Format(Val(LBLTOTALCONES.Text) + Val(ROW.Cells(GCONES.Index).EditedFormattedValue), "0.00")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        EDIT = False
        YARNDATE.Focus()
    End Sub

    Private Sub GRNDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles YARNDATE.GotFocus
        YARNDATE.SelectAll()
    End Sub

    Private Sub GRNDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles YARNDATE.Validating
        Try
            If YARNDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(YARNDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                Else
                    'SAME DATE FOR CHALLANDATE / LRDATE 
                    'If ClientName = "DAKSH" Or ClientName = "SHALIBHADRA" Then
                    '    dtpchallan.Value = Convert.ToDateTime(GRNDATE.Text).Date
                    '    DTLRDATE.Value = Convert.ToDateTime(GRNDATE.Text).Date
                    'End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAXNO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(YARN_no),0) + 1 ", "YARNRECDJOBBER", " and YARN_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTYARNNO.Text = Val(DTTABLE.Rows(0).Item(0))
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True

            If CMBTONAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBTONAME, " Please Fill Company Name ")
                bln = False
            End If

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, "Checking Done, Delete Checking First")
                bln = False
            End If

            If ClientName = "NAYRA" And Val(TXTPROGNO.Text.Trim) = 0 Then
                If MsgBox("Beam Program Not Selected, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    EP.SetError(TXTPROGNO, "Select Program Details")
                    bln = False
                End If
            End If

            If GRIDYARN.RowCount = 0 Then
                EP.SetError(TabControl1, "Fill Item Details")
                bln = False
            End If


            If ClientName = "NAYRA" And Val(TXTPROGNO.Text.Trim) = 0 Then
                EP.SetError(TXTPROGNO, "Select Program")
                bln = False
            End If


            'coz if it it other item type then mtrs will be blank
            'if want to enable then check for materialtype
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable

            If txtchallan.Text.Trim <> "" Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(txtchallan.Text.Trim)) Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    DT = objclscommon.search(" GRN.GRN_challanno, LEDGERS.ACC_cmpname", "", " GRN inner join LEDGERS on LEDGERS.ACC_id = GRN.GRN_ledgerid AND LEDGERS.ACC_CMPid = GRN.GRN_CMPid AND LEDGERS.ACC_LOCATIONid = GRN.GRN_lOCATIONid AND LEDGERS.ACC_YEARid = GRN.GRN_YEARid", " and GRN.GRN_challanno = '" & txtchallan.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & CMBTONAME.Text.Trim & "' AND GRN_CMPID =" & CmpId & " AND GRN_LOCATIONID =" & Locationid & " AND GRN_YEARID =" & YearId)
                    If DT.Rows.Count > 0 Then
                        EP.SetError(txtchallan, "Challan No. Already Exists")
                        bln = False
                    End If
                End If
            End If


            'FOR ORDER CHECKING, FIRST REMOVE GDNQTY
            Dim TEMPPROGROWNO As Integer = -1
            Dim TEMPPROGMATCH As Boolean = False
            If GRIDPROG.RowCount > 0 Then

                For Each ORDROW As DataGridViewRow In GRIDPROG.Rows
                    ORDROW.Cells(ORECDWT.Index).Value = 0
                Next

                For Each ROW As DataGridViewRow In GRIDYARN.Rows
                    For Each ORDROW As DataGridViewRow In GRIDPROG.Rows
                        If ROW.Cells(GYARNQUALITY.Index).Value = ORDROW.Cells(OYARNQUALITY.Index).Value And ROW.Cells(GDESIGN.Index).Value = ORDROW.Cells(ODESIGN.Index).Value And ROW.Cells(gcolor.Index).Value = ORDROW.Cells(OCOLOR.Index).Value Then
                            TEMPPROGMATCH = True
                            'IF ITEM / DESIGN / SHADE IS MATCHED BUT THE QTY IS FULL THEN WE NEED TO KEEP THIS ROWNO IN TEMP AND NEED TO CHECK FURTHER ALSO
                            'IF WE GET ANY NEW MATHING THEN WE NEED TO INSERT THERE
                            'IF NO MATCHING IS FOUND IN FURTHER ROWS THEN WE NEED TO ADD QTY IN THIS TEMPROW
                            If Val(ORDROW.Cells(ORECDWT.Index).Value) >= Val(ORDROW.Cells(OWT.Index).Value) Then
                                TEMPPROGROWNO = ORDROW.Index
                                GoTo CHECKNEXTLINE
                            End If
                            ORDROW.Cells(ORECDWT.Index).Value = Val(ORDROW.Cells(ORECDWT.Index).Value) + Val(ROW.Cells(GWT.Index).Value)
                            TEMPPROGROWNO = -1
                            Exit For
CHECKNEXTLINE:
                        End If
                    Next
                    'IF NO FURTHER MACHING IS FOUND BUT WE HAVE TEMPPROGROWNO THEN ADD VALUE IN THAT ROW
                    If TEMPPROGROWNO >= 0 Then
                        GRIDPROG.Rows(TEMPPROGROWNO).Cells(ORECDWT.Index).Value = Val(GRIDPROG.Rows(TEMPPROGROWNO).Cells(ORECDWT.Index).Value) + Val(ROW.Cells(GWT.Index).Value)
                        TEMPPROGROWNO = -1
                    End If
                    If TEMPPROGMATCH = False Then
                        ROW.DefaultCellStyle.BackColor = Color.LightGreen
                        If MsgBox("There are Items which are not Present in Selected Order, Wish to Proceed", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            EP.SetError(CMBTONAME, "There are Items which are not Present in Selected Order")
                            bln = False
                        End If
                    End If
                    TEMPPROGMATCH = False
                Next
            End If



            If YARNDATE.Text = "__/__/____" Then
                EP.SetError(YARNDATE, " Please Enter Proper Date")
                bln = False
            Else
                If Not datecheck(YARNDATE.Text) Then
                    EP.SetError(YARNDATE, "Date not in Accounting Year")
                    bln = False
                End If
            End If


            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()

            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(YARNDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBGODOWN.Text.Trim)

            alParaval.Add(CMBTONAME.Text.Trim)
            alParaval.Add(CMBPROCESS.Text.Trim)
            alParaval.Add(txtchallan.Text.Trim)
            alParaval.Add(Format(dtpchallan.Value.Date, "MM/dd/yyyy"))

            alParaval.Add(cmbtrans.Text.Trim)

            alParaval.Add(Val(lbltotalqty.Text))
            alParaval.Add(Val(LBLTOTALCUT.Text))
            alParaval.Add(Val(LBLTOTALMTRS.Text))
            alParaval.Add(Val(LBLTOTALWT.Text))

            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CMBJONO.Text.Trim)
            alParaval.Add(TXTBALWT.Text.Trim)
            alParaval.Add(Val(TXTPROGNO.Text.Trim))
            alParaval.Add(TXTPROGFROMTYPE.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)


            Dim gridsrno As String = ""
            Dim YARNQUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim DESIGN As String = ""
            Dim JOBBERLOTNO As String = ""
            Dim COLOR As String = ""
            Dim LOTNO As String = ""
            Dim qty As String = ""
            Dim CUT As String = ""
            Dim MTRS As String = ""
            Dim WT As String = ""
            Dim CONES As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDYARN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        YARNQUALITY = row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        JOBBERLOTNO = row.Cells(GJOBBERLOTNO.Index).Value.ToString
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        qty = Val(row.Cells(GQTY.Index).Value)
                        CUT = Val(row.Cells(GCUT.Index).Value)
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        WT = Val(row.Cells(GWT.Index).Value)
                        CONES = Val(row.Cells(GCONES.Index).Value)

                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value

                        YARNQUALITY = YARNQUALITY & "|" & row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        JOBBERLOTNO = JOBBERLOTNO & "|" & row.Cells(GJOBBERLOTNO.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        qty = qty & "|" & Val(row.Cells(GQTY.Index).Value)
                        CUT = CUT & "|" & Val(row.Cells(GCUT.Index).Value)
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        WT = WT & "|" & Val(row.Cells(GWT.Index).Value)
                        CONES = CONES & "|" & Val(row.Cells(GCONES.Index).Value)

                    End If
                End If
            Next

            alParaval.Add(gridsrno)
            alParaval.Add(YARNQUALITY)
            alParaval.Add(MILLNAME)
            alParaval.Add(DESIGN)
            alParaval.Add(JOBBERLOTNO)
            alParaval.Add(COLOR)
            alParaval.Add(LOTNO)
            alParaval.Add(qty)
            alParaval.Add(CUT)
            alParaval.Add(MTRS)
            alParaval.Add(WT)
            alParaval.Add(CONES)



            Dim PROGGRIDSRNO As String = ""
            Dim PROGYARNQUALITY As String = ""
            Dim PROGDESIGN As String = ""
            Dim PROGCOLOR As String = ""
            Dim PROGWT As String = ""
            Dim PROGFROMNO As String = ""
            Dim PROGFROMSRNO As String = ""
            Dim PROGFROMTYPE As String = ""
            Dim PROGRECDWT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDPROG.Rows
                If row.Cells(0).Value <> Nothing AndAlso Val(row.Cells(ORECDWT.Index).Value) > 0 Then

                    If PROGGRIDSRNO = "" Then
                        PROGGRIDSRNO = Val(row.Cells(OSRNO.Index).Value)
                        PROGYARNQUALITY = row.Cells(OYARNQUALITY.Index).Value.ToString
                        PROGDESIGN = row.Cells(ODESIGN.Index).Value.ToString
                        PROGCOLOR = row.Cells(OCOLOR.Index).Value.ToString
                        PROGWT = Val(row.Cells(OWT.Index).Value)
                        PROGFROMNO = Val(row.Cells(OFROMNO.Index).Value)
                        PROGFROMSRNO = Val(row.Cells(OFROMSRNO.Index).Value)
                        PROGFROMTYPE = row.Cells(OFROMTYPE.Index).Value.ToString
                        PROGRECDWT = Val(row.Cells(ORECDWT.Index).Value)
                    Else
                        PROGGRIDSRNO = PROGGRIDSRNO & "|" & Val(row.Cells(OSRNO.Index).Value)
                        PROGYARNQUALITY = PROGYARNQUALITY & "|" & row.Cells(OYARNQUALITY.Index).Value.ToString
                        PROGDESIGN = PROGDESIGN & "|" & row.Cells(ODESIGN.Index).Value.ToString
                        PROGCOLOR = PROGCOLOR & "|" & row.Cells(OCOLOR.Index).Value.ToString
                        PROGWT = PROGWT & "|" & Val(row.Cells(OWT.Index).Value)
                        PROGFROMNO = PROGFROMNO & "|" & Val(row.Cells(OFROMNO.Index).Value)
                        PROGFROMSRNO = PROGFROMSRNO & "|" & Val(row.Cells(OFROMSRNO.Index).Value)
                        PROGFROMTYPE = PROGFROMTYPE & "|" & row.Cells(OFROMTYPE.Index).Value.ToString
                        PROGRECDWT = PROGRECDWT & "|" & Val(row.Cells(ORECDWT.Index).Value)
                    End If
                End If
            Next

            alParaval.Add(PROGGRIDSRNO)
            alParaval.Add(PROGYARNQUALITY)
            alParaval.Add(PROGDESIGN)
            alParaval.Add(PROGCOLOR)
            alParaval.Add(PROGWT)
            alParaval.Add(PROGFROMNO)
            alParaval.Add(PROGFROMSRNO)
            alParaval.Add(PROGFROMTYPE)
            alParaval.Add(PROGRECDWT)


            Dim objclsGRN As New ClsYarnRecdFromJobber()
            objclsGRN.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Dim DTTABLE As DataTable = objclsGRN.SAVE()
                TXTYARNNO.Text = Val(DTTABLE.Rows(0).Item(0))
                MsgBox("Details Added")

                If FRMSTRING = "GRN FANCY" Then PRINTREPORT(DTTABLE.Rows(0).Item(0))

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempYARNno)

                IntResult = objclsGRN.UPDATE()
                MsgBox("Details Updated")

            End If



            If EDIT = False And (ClientName = "VAISHALI" Or ClientName = "KARAN" Or ClientName = "NAYRA" Or ClientName = "AKASHDEEP") Then
                If MsgBox("Issue Yarn Directly?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim OBJISSUE As New YarnDirectIssueJobber
                    OBJISSUE.ShowDialog()
                    If OBJISSUE.CMBJOBBER.Text.Trim = "" Then GoTo LINE1
                    DIRECTISSUEJOBBER(OBJISSUE.CMBJOBBER.Text.Trim, OBJISSUE.CMBPROCESS.Text.Trim, OBJISSUE.txtremarks.Text.Trim)
                End If
            End If

LINE1:


            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            EDIT = False

            clear()
            YARNDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub DIRECTISSUEJOBBER(ByVal JOBBERNAME As String, PROCESSNAME As String, REMARKS As String)
        Try
            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(Format(Convert.ToDateTime(YARNDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBGODOWN.Text.Trim)
            ALPARAVAL.Add(JOBBERNAME)
            ALPARAVAL.Add("")   'MACHINE
            ALPARAVAL.Add(PROCESSNAME)   'PROCESS
            ALPARAVAL.Add(txtchallan.Text.Trim)

            ALPARAVAL.Add(cmbtrans.Text.Trim)

            ALPARAVAL.Add(Val(lbltotalqty.Text))
            ALPARAVAL.Add(Val(LBLTOTALWT.Text))
            ALPARAVAL.Add(Val(LBLTOTALCONES.Text))

            ALPARAVAL.Add(REMARKS)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)
            ALPARAVAL.Add(0)


            Dim gridsrno As String = ""
            Dim YARNQUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim DESIGN As String = ""
            Dim COLOR As String = ""
            Dim LOTNO As String = ""
            Dim qty As String = ""
            Dim WT As String = ""
            Dim CONES As String = ""
            Dim LRNO As String = ""
            Dim LRDATE As String = ""


            For Each row As Windows.Forms.DataGridViewRow In GRIDYARN.Rows
                If row.Cells(0).Value <> Nothing Then
                    If gridsrno = "" Then
                        gridsrno = row.Cells(gsrno.Index).Value.ToString
                        YARNQUALITY = row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        DESIGN = row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = row.Cells(gcolor.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        qty = row.Cells(GQTY.Index).Value.ToString
                        WT = row.Cells(GWT.Index).Value
                        CONES = row.Cells(GCONES.Index).Value.ToString
                        LRNO = ""
                        LRDATE = Format(Convert.ToDateTime(YARNDATE.Text).Date, "MM/dd/yyyy")


                    Else
                        gridsrno = gridsrno & "|" & row.Cells(gsrno.Index).Value

                        YARNQUALITY = YARNQUALITY & "|" & row.Cells(GYARNQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        DESIGN = DESIGN & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(gcolor.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        qty = qty & "|" & row.Cells(GQTY.Index).Value
                        WT = WT & "|" & row.Cells(GWT.Index).Value
                        CONES = CONES & "|" & row.Cells(GCONES.Index).Value
                        LRNO = LRNO & "|" & ""
                        LRDATE = LRDATE & "|" & Format(Convert.ToDateTime(YARNDATE.Text).Date, "MM/dd/yyyy")

                    End If
                End If
            Next

            ALPARAVAL.Add(gridsrno)
            ALPARAVAL.Add(YARNQUALITY)
            ALPARAVAL.Add(MILLNAME)
            ALPARAVAL.Add(DESIGN)
            ALPARAVAL.Add(COLOR)
            ALPARAVAL.Add(LOTNO)
            ALPARAVAL.Add(qty)
            ALPARAVAL.Add(WT)

            ALPARAVAL.Add(CONES)
            ALPARAVAL.Add(LRNO)
            ALPARAVAL.Add(LRDATE)
            ALPARAVAL.Add(0)    'CHKYARNRECD
            ALPARAVAL.Add("")    'BEAMRECDDESC

            Dim OBJYAARNISSUE As New ClsYarnIssue
            OBJYAARNISSUE.alParaval = ALPARAVAL
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim DT As DataTable = OBJYAARNISSUE.SAVE()
            MsgBox("Yarn Issued To Dyeing")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SAVEUPLOAD()

        Try
            Dim OBJBILL As New ClsYarnRecdFromJobber


            For Each row As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If row.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(tempYARNno)
                    'ALPARAVAL.Add(TEMPREGNAME)
                    ALPARAVAL.Add(row.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(row.Cells(GUNAME.Index).Value)

                    PBSoftCopy.Image = row.Cells(GUIMGPATH.Index).Value
                    PBSoftCopy.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJBILL.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJBILL.SAVEUPLOAD()
                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If errorvalid() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1 Then       'for Delete
            TabControl1.SelectedIndex = (0)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2 Then       'for Delete
            TabControl1.SelectedIndex = (1)
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            toolprevious_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.F5 Then
            GRIDYARN.Focus()
        End If
    End Sub

    Private Sub GRN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'JOB IN'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
            FILLCMB()
            clear()


            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objclsYARN As New ClsYarnRecdFromJobber()
                Dim dttable As New DataTable

                dttable = objclsYARN.selectYARN(tempYARNno, CmpId, Locationid, YearId, FRMSTRING)

                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows

                        TXTYARNNO.Text = tempYARNno
                        YARNDATE.Text = Format(Convert.ToDateTime(dr("YARNDATE")).Date, "dd/MM/yyyy")
                        CMBTONAME.Text = Convert.ToString(dr("TONAME").ToString)
                        CMBTONAME.Enabled = False
                        CMBPROCESS.Text = dr("PROCESS")
                        CMBJONO.Text = Convert.ToString(dr("JONO").ToString)
                        CMBJONO.Enabled = False

                        CMBGODOWN.Text = Convert.ToString(dr("GODOWN").ToString)
                        TXTLOTNO.Text = Convert.ToString(dr("LOTNO").ToString)
                        txtchallan.Text = Convert.ToString(dr("CHALLANNO").ToString)
                        PARTYCHALLANNO = txtchallan.Text.Trim
                        TXTPROGNO.Text = Val(dr("PROGNO"))
                        TXTPROGFROMTYPE.Text = dr("PROGFROMTYPE")

                        dtpchallan.Value = Format(Convert.ToDateTime(dr("CHALLANDATE")).Date, "dd/MM/yyyy")
                        cmbtrans.Text = dr("TRANSNAME").ToString
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        GRIDYARN.Rows.Add(dr("GRIDSRNO").ToString, dr("YARNQUALITY").ToString, dr("MILLNAME").ToString, dr("DESIGNNO").ToString, dr("JOBBERLOTNO"), dr("COLOR"), dr("LOTNO"), Format(Val(dr("qty")), "0.00"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("MTRS")), "0.00"), Format(Val(dr("WT")), "0.00"), Format(Val(dr("CONES")), "0"))

                    Next
                    total()
                Else
                    EDIT = False
                    clear()
                End If

                Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.search(" YARNRECDJOBBER_UPLOAD.YARN_SRNO AS GRIDSRNO, YARNRECDJOBBER_UPLOAD.YARN_REMARKS AS REMARKS, YARNRECDJOBBER_UPLOAD.YARN_NAME AS NAME, YARNRECDJOBBER_UPLOAD.YARN_PHOTO AS IMGPATH ", "", " YARNRECDJOBBER_UPLOAD ", " AND YARNRECDJOBBER_UPLOAD.YARN_NO = " & tempYARNno & " AND YARN_YEARID = " & YearId & " ORDER BY YARNRECDJOBBER_UPLOAD.YARN_SRNO")
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        gridupload.Rows.Add(DTR("GRIDSRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                    Next
                End If


                'ORDER GRID
                'Dim OBJCMN As New ClsCommon
                dttable = OBJCMN.search(" YARNRECDJOBBER_PROGDETAILS.YARN_GRIDSRNO AS GRIDSRNO, YARNQUALITYMASTER.YARN_name AS YARNQUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(YARNRECDJOBBER_PROGDETAILS.YARN_PROGWT,0) AS PROGWT, YARNRECDJOBBER_PROGDETAILS.YARN_FROMNO AS FROMNO, YARNRECDJOBBER_PROGDETAILS.YARN_FROMSRNO AS FROMSRNO, YARNRECDJOBBER_PROGDETAILS.YARN_FROMTYPE AS FROMTYPE, ISNULL(YARNRECDJOBBER_PROGDETAILS.YARN_WT,0) AS RECDWT ", "", " YARNRECDJOBBER_PROGDETAILS INNER JOIN YARNQUALITYMASTER ON YARNRECDJOBBER_PROGDETAILS.YARN_YARNQUALITYID= YARNQUALITYMASTER.YARN_id LEFT OUTER JOIN COLORMASTER ON YARNRECDJOBBER_PROGDETAILS.YARN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON YARNRECDJOBBER_PROGDETAILS.YARN_DESIGNID = DESIGNMASTER.DESIGN_id  ", " AND YARNRECDJOBBER_PROGDETAILS.YARN_NO = " & tempYARNno & " AND YARNRECDJOBBER_PROGDETAILS.YARN_YEARID = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each DTR As DataRow In dttable.Rows
                        GRIDPROG.Rows.Add(Val(DTR("GRIDSRNO")), DTR("YARNQUALITY"), DTR("DESIGNNO"), DTR("COLOR"), Val(DTR("PROGWT")), Val(DTR("FROMNO")), Val(DTR("FROMSRNO")), DTR("FROMTYPE"), Val(DTR("RECDWT")))
                    Next
                End If
                getsrno(GRIDPROG)

            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Sub FILLCMB()
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
            If CMBTONAME.Text.Trim = "" Then fillname(CMBTONAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
            If CMBCODE.Text.Trim = "" Then fillACCCODE(CMBCODE, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
            If CMBTOCODE.Text.Trim = "" Then fillACCCODE(CMBTOCODE, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")

            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' and ACC_TYPE = 'TRANSPORT'")

            fillYARNQUALITY(CMBYARNQUALITY, EDIT)
            FILLMILL(CMBMILL, EDIT)
            FILLDESIGN(CMBDESIGN, "")
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, "")

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_ENTER(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBGODOWN.Enter
        Try
            If CMBGODOWN.Text.Trim = "" Then fillGODOWN(CMBGODOWN, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbGodown_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBGODOWN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectGodown
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then CMBGODOWN.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBGODOWN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBGODOWN.Validating
        Try
            If CMBGODOWN.Text.Trim <> "" Then GODOWNVALIDATE(CMBGODOWN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objgrndetails As New YarnRecdFromJobberDetails()
            objgrndetails.MdiParent = MDIMain
            'objgrndetails.FRMSTRING = FRMSTRING
            objgrndetails.Show()
            objgrndetails.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtrans.Enter
        Try
            If cmbtrans.Text.Trim = "" Then fillname(cmbtrans, EDIT, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtrans.Validating
        Try
            If cmbtrans.Text.Trim <> "" Then namevalidate(cmbtrans, CMBCODE, e, Me, TXTTRANSADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "Sundry Creditors", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub GETSRNO(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtchallan_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtchallan.Validating
        Try
            If txtchallan.Text.Trim.Length > 0 Then
                If (EDIT = False) Or (EDIT = True And LCase(PARTYCHALLANNO) <> LCase(txtchallan.Text.Trim)) Then
                    'for search
                    Dim objclscommon As New ClsCommon()
                    Dim dt As New DataTable
                    dt = objclscommon.search(" GRN.GRN_challanno, LEDGERS.ACC_cmpname", "", " GRN inner join LEDGERS on LEDGERS.ACC_id = GRN.GRN_ledgerid AND LEDGERS.ACC_CMPid = GRN.GRN_CMPid AND LEDGERS.ACC_LOCATIONid = GRN.GRN_lOCATIONid AND LEDGERS.ACC_YEARid = GRN.GRN_YEARid", " and GRN.GRN_challanno = '" & txtchallan.Text.Trim & "' and LEDGERS.ACC_cmpname = '" & CMBTONAME.Text.Trim & "' AND GRN_CMPID =" & CmpId & " AND GRN_LOCATIONID =" & Locationid & " AND GRN_YEARID =" & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Challan No. Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub dtpchallan_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpchallan.Validating
        If Not datecheck(dtpchallan.Value) Then
            MsgBox("Date Not in Current Accounting Year")
            e.Cancel = True
        End If
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) > 0 Then
                GRIDYARN.RowCount = 0
                tempYARNno = Val(tstxtbillno.Text)
                If tempYARNno > 0 Then
                    EDIT = True
                    GRN_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        GRIDYARN.Enabled = True

        Dim TEMPQTY As Integer = Val(txtqty.Text.Trim)
        If GRIDDOUBLECLICK = False Then
            GRIDYARN.Rows.Add(Val(txtsrno.Text.Trim), CMBYARNQUALITY.Text.Trim, CMBMILL.Text.Trim, CMBDESIGN.Text.Trim, TXTJOBBERLOTNO.Text.Trim, cmbcolor.Text.Trim, TXTLOTNO.Text.Trim, Format(Val(txtqty.Text.Trim), "0.00"), Val(TXTCUT.Text.Trim), Val(TXTMTRS.Text.Trim), Format(Val(TXTWT.Text.Trim), "0.000"), Format(Val(TXTCONES.Text.Trim), "0"))
            GETSRNO(GRIDYARN)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDYARN.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            GRIDYARN.Item(GYARNQUALITY.Index, TEMPROW).Value = CMBYARNQUALITY.Text.Trim
            GRIDYARN.Item(GMILLNAME.Index, TEMPROW).Value = CMBMILL.Text.Trim
            GRIDYARN.Item(GDESIGN.Index, TEMPROW).Value = CMBDESIGN.Text.Trim
            GRIDYARN.Item(GJOBBERLOTNO.Index, TEMPROW).Value = TXTJOBBERLOTNO.Text.Trim
            GRIDYARN.Item(gcolor.Index, TEMPROW).Value = cmbcolor.Text.Trim
            GRIDYARN.Item(GQTY.Index, TEMPROW).Value = Format(Val(txtqty.Text.Trim), "0.00")
            GRIDYARN.Item(GCUT.Index, TEMPROW).Value = Format(Val(TXTCUT.Text.Trim), "0")
            GRIDYARN.Item(GMTRS.Index, TEMPROW).Value = Format(Val(TXTMTRS.Text.Trim), "0.00")
            GRIDYARN.Item(GWT.Index, TEMPROW).Value = Format(Val(TXTWT.Text.Trim), "0.000")
            GRIDYARN.Item(GCONES.Index, TEMPROW).Value = Format(Val(TXTCONES.Text.Trim), "0")

            GRIDDOUBLECLICK = False

        End If

        TOTAL()

        GRIDYARN.FirstDisplayedScrollingRowIndex = GRIDYARN.RowCount - 1


        txtgridremarks.Clear()
        'CMBYARNQUALITY.Text = ""
        TXTJOBBERLOTNO.Clear()
        CMBMILL.Text = ""
        CMBDESIGN.Text = ""
        cmbcolor.Text = ""
        TXTLOTNO.Clear()
        txtqty.Clear()
        TXTWT.Clear()
        TXTCONES.Clear()

        txtPartyMtrs.Clear()
        txtCheckPcs.Clear()
        TXTBARCODE.Clear()
        txtsrno.Text = Val(GRIDYARN.RowCount) + 1
        CMBYARNQUALITY.Focus()


    End Sub

    Private Sub cmdupload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdupload.Click

        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpeg;*.png"
        OpenFileDialog1.ShowDialog()
        txtimgpath.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If txtimgpath.Text.Trim.Length <> 0 Then PBSoftCopy.ImageLocation = txtimgpath.Text.Trim
    End Sub

    Private Sub txtuploadname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
        Try
            If txtuploadremarks.Text.Trim <> "" And txtuploadname.Text.Trim <> "" And PBSoftCopy.ImageLocation <> "" Then
                FILLUPLOAD()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLUPLOAD()

        If GRIDUPLOADDOUBLECLICK = False Then
            gridupload.Rows.Add(Val(txtuploadsrno.Text.Trim), txtuploadremarks.Text.Trim, txtuploadname.Text.Trim, PBSoftCopy.Image)
            getsrno(gridupload)
        ElseIf GRIDUPLOADDOUBLECLICK = True Then

            gridupload.Item(GUSRNO.Index, TEMPUPLOADROW).Value = txtuploadsrno.Text.Trim
            gridupload.Item(GUREMARKS.Index, TEMPUPLOADROW).Value = txtuploadremarks.Text.Trim
            gridupload.Item(GUNAME.Index, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
            gridupload.Item(GUIMGPATH.Index, TEMPUPLOADROW).Value = PBSoftCopy.Image

            GRIDUPLOADDOUBLECLICK = False

        End If
        gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1

        txtuploadsrno.Text = gridupload.RowCount + 1
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSoftCopy.Image = Nothing
        txtimgpath.Clear()

        txtuploadremarks.Focus()

    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And gridupload.Item(GUSRNO.Index, e.RowIndex).Value <> Nothing Then

                GRIDUPLOADDOUBLECLICK = True
                txtuploadsrno.Text = gridupload.Item(GUSRNO.Index, e.RowIndex).Value
                txtuploadremarks.Text = gridupload.Item(GUREMARKS.Index, e.RowIndex).Value
                txtuploadname.Text = gridupload.Item(GUNAME.Index, e.RowIndex).Value
                PBSoftCopy.Image = gridupload.Item(GUIMGPATH.Index, e.RowIndex).Value

                TEMPUPLOADROW = e.RowIndex
                txtuploadremarks.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
        Try
            If e.KeyCode = Keys.Delete And gridupload.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                If GRIDUPLOADDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                gridupload.Rows.RemoveAt(gridupload.CurrentRow.Index)
                getsrno(gridupload)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSoftCopy.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtuploadsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuploadsrno.GotFocus
        If GRIDUPLOADDOUBLECLICK = False Then
            If gridupload.RowCount > 0 Then
                txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(GUSRNO.Index).Value) + 1
            Else
                txtuploadsrno.Text = 1
            End If
        End If
    End Sub

    Sub EDITROW()
        Try
            If GRIDYARN.CurrentRow.Index >= 0 And GRIDYARN.Item(gsrno.Index, GRIDYARN.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                txtsrno.Text = GRIDYARN.Item(gsrno.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                CMBYARNQUALITY.Text = GRIDYARN.Item(GYARNQUALITY.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                CMBMILL.Text = GRIDYARN.Item(GMILLNAME.Index, GRIDYARN.CurrentRow.Index).Value.ToString

                CMBDESIGN.Text = GRIDYARN.Item(GDESIGN.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                TXTJOBBERLOTNO.Text = GRIDYARN.Item(GJOBBERLOTNO.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                cmbcolor.Text = GRIDYARN.Item(gcolor.Index, GRIDYARN.CurrentRow.Index).Value.ToString
                TXTLOTNO.Text = GRIDYARN.Item(GLOTNO.Index, GRIDYARN.CurrentRow.Index).Value.ToString

                txtqty.Text = Val(GRIDYARN.Item(GQTY.Index, GRIDYARN.CurrentRow.Index).Value)
                TXTCUT.Text = Val(GRIDYARN.Item(GCUT.Index, GRIDYARN.CurrentRow.Index).Value)
                TXTMTRS.Text = Val(GRIDYARN.Item(GMTRS.Index, GRIDYARN.CurrentRow.Index).Value)
                TXTWT.Text = Val(GRIDYARN.Item(GWT.Index, GRIDYARN.CurrentRow.Index).Value)
                TXTCONES.Text = Val(GRIDYARN.Item(GCONES.Index, GRIDYARN.CurrentRow.Index).Value)

                TEMPROW = GRIDYARN.CurrentRow.Index
                CMBYARNQUALITY.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridgrn_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDYARN.CellDoubleClick
        EDITROW()
    End Sub

    Private Sub toolprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor

            GRIDYARN.RowCount = 0
LINE1:
            temptypename = FRMSTRING
            tempYARNno = Val(TXTYARNNO.Text) - 1
            If tempYARNno > 0 Then
                EDIT = True
                GRN_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDYARN.RowCount = 0 And tempYARNno > 1 Then
                TXTYARNNO.Text = tempYARNno
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            GRIDYARN.RowCount = 0
LINE1:
            tempYARNno = Val(TXTYARNNO.Text) + 1
            temptypename = FRMSTRING
            getmaxno()
            Dim MAXNO As Integer = TXTYARNNO.Text.Trim
            clear()
            If Val(TXTYARNNO.Text) - 1 >= tempYARNno Then
                EDIT = True
                GRN_Load(sender, e)
            Else
                clear()
                EDIT = False
            End If
            If GRIDYARN.RowCount = 0 And tempYARNno < MAXNO Then
                TXTYARNNO.Text = tempYARNno
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtqty.KeyPress, TXTCONES.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Dim IntResult As Integer
        Try

            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If lbllocked.Visible = True Then
                    MsgBox("Unable to Delete, Checking Done / Item Used", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                TEMPMSG = MsgBox("Delete Yarn?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTYARNNO.Text.Trim)
                    alParaval.Add(FRMSTRING)
                    alParaval.Add(CmpId)
                    alParaval.Add(Locationid)
                    alParaval.Add(Userid)
                    alParaval.Add(YearId)

                    Dim Clsgrn As New ClsYarnRecdFromJobber()
                    Clsgrn.alParaval = alParaval
                    IntResult = Clsgrn.Delete()
                    MsgBox("Yarn Deleted")
                    clear()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub gridgrn_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GRIDYARN.CellValidating
        Try

            Dim colNum As Integer = GRIDYARN.Columns(e.ColumnIndex).Index
            If String.IsNullOrEmpty(e.FormattedValue.ToString) Then Return

            Select Case colNum

                Case GQTY.Index, GWT.Index
                    Dim dDebit As Decimal
                    Dim bValid As Boolean = Decimal.TryParse(e.FormattedValue.ToString, dDebit)

                    If bValid Then
                        If GRIDYARN.CurrentCell.Value = Nothing Then GRIDYARN.CurrentCell.Value = "0.00"
                        GRIDYARN.CurrentCell.Value = Convert.ToDecimal(GRIDYARN.Item(colNum, e.RowIndex).Value)
                        total()
                    Else
                        MessageBox.Show("Invalid Number Entered")
                        e.Cancel = True
                        Exit Sub
                    End If

            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridgrn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDYARN.KeyDown

        Try
            If e.KeyCode = Keys.Delete And GRIDYARN.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDYARN.Rows.RemoveAt(GRIDYARN.CurrentRow.Index)
                getsrno(GRIDYARN)
                total()
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Keys.F12 And GRIDYARN.RowCount > 0 Then
                'If gridgrn.CurrentRow.Cells(gitemname.Index).Value <> "" Then
                '    gridgrn.Rows.Add(CloneWithValues(gridgrn.CurrentRow))
                '    getsrno(gridgrn)
                '    total()
                'End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcolor.Validating
        Try
            If cmbcolor.Text.Trim <> "" Then COLORVALIDATE(cmbcolor, e, Me, CMBDESIGN.Text.Trim, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTONAME.Enter
        Try
            If CMBTONAME.Text.Trim = "" Then fillname(CMBTONAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTONAME.Validating
        Try
            'If cmbname.Text.Trim <> "" Then namevalidate(cmbname, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS", cmbtrans.Text, CMBBROKER.Text)
            If CMBTONAME.Text.Trim <> "" Then namevalidate(CMBTONAME, CMBCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS", cmbtrans.Text, "")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTONAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTONAME.Enter
        Try
            If CMBTONAME.Text.Trim = "" Then fillname(CMBTONAME, EDIT, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTONAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTONAME.Validating
        Try
            If CMBTONAME.Text.Trim <> "" Then namevalidate(CMBTONAME, CMBTOCODE, e, Me, txtadd, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBYARNQUALITY.Enter
        Try
            If CMBYARNQUALITY.Text.Trim = "" Then fillYARNQUALITY(CMBYARNQUALITY, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbQUALITY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBYARNQUALITY.Validating
        Try
            If CMBYARNQUALITY.Text.Trim <> "" Then YARNQUALITYVALIDATE(CMBYARNQUALITY, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
        Try
            If gridupload.SelectedRows.Count > 0 Then
                Dim objVIEW As New ViewImage
                objVIEW.pbsoftcopy.Image = PBSoftCopy.Image
                objVIEW.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then
                If ClientName = "CC"  Or ClientName = "C3" Or ClientName = "SHREEDEV" Then
                    PRINTREPORT(tempYARNno)
                Else
                    'PRINTBARCODE()
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtPartyMtrs_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPartyMtrs.Validated
        'If Val(TXTLRNO.Text) <> 0 And Val(txtPartyMtrs.Text) <> 0 Then
        '    txtCheckPcs.Text = Format(Val(TXTLRNO.Text) - Val(txtPartyMtrs.Text), "0.00")
        'End If
    End Sub

    ' Private Sub TXTMTRS_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTLRNO.Validated
    'CALC()
    'End Sub

    'Private Sub TXTMTRS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTLRNO.Validating
    'If Val(TXTLRNO.Text) > 0 Then
    '    txtPartyMtrs.Text = Val(TXTLRNO.Text)
    'End If
    ' End Sub

    Private Sub CMBCODE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCODE.Enter
        Try
            If CMBCODE.Text.Trim = "" Then fillACCCODE(CMBCODE, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCODE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBCODE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBTONAME.Text = OBJLEDGER.TEMPNAME
                'If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTONAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE = 'ACCOUNTS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBTONAME.Text = OBJLEDGER.TEMPNAME
                'If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCODE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTOCODE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBTONAME.Text = OBJLEDGER.TEMPNAME
                'If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTONAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTONAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBTONAME.Text = OBJLEDGER.TEMPNAME
                'If OBJLEDGER.TEMPAGENT <> "" Then CMBBROKER.Text = OBJLEDGER.TEMPAGENT
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCODE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCODE.Validating
        Try
            If CMBCODE.Text.Trim <> "" Then ACCCODEVALIDATE(CMBCODE, CMBTONAME, e, Me, TXTTRANSADD, "", "SUNDRY CREDITORS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCODE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTOCODE.Validating
        Try
            If CMBTOCODE.Text.Trim <> "" Then ACCCODEVALIDATE(CMBTOCODE, CMBTONAME, e, Me, TXTTRANSADD, "", "SUNDRY CREDITORS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtrans.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPNAME <> "" Then cmbtrans.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBDESIGN.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJD As New SelectDesign
                OBJD.ShowDialog()
                If OBJD.TEMPNAME <> "" Then CMBDESIGN.Text = OBJD.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcolor.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCOLOR As New SelectShade
                OBJCOLOR.ShowDialog()
                If OBJCOLOR.TEMPNAME <> "" Then cmbcolor.Text = OBJCOLOR.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtremarks_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtremarks.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then txtremarks.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal INVOICENO As Integer)
        Try

            TEMPMSG = MsgBox("Wish To Print Barcode?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                Dim OBJGRN As New GRNDesign
                OBJGRN.MdiParent = MDIMain
                OBJGRN.WHERECLAUSE = " {YARNRECDJOBBER.YARN_NO}= " & INVOICENO & "  AND {YARNRECDJOBBER.YARN_YEARID}=" & YearId
                OBJGRN.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GRN_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "VAISHALI" Then
                TXTLOTNO.BackColor = Color.Linen
                TXTLOTNO.ReadOnly = True
                TXTLOTNO.TabStop = False
                GQTY.HeaderText = "Box/Tube"
            End If

            If ClientName = "NAYRA" Then GLOTNO.HeaderText = "Lot/Beam No"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        cmdok_Click(sender, e)
    End Sub

    Private Sub CMBMILL_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBMILL.Enter
        Try
            If CMBMILL.Text.Trim = "" Then FILLMILL(CMBMILL, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMILL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBMILL.Validating
        Try
            If CMBMILL.Text.Trim <> "" Then MILLVALIDATE(CMBMILL, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWT.KeyPress, TXTMTRS.KeyPress, TXTCUT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub CMDSELECTPROG_Click(sender As Object, e As EventArgs) Handles CMDSELECTPROG.Click
        Try
            If CMBTONAME.Text.Trim = "" Then
                MsgBox("Select Party Name", MsgBoxStyle.Critical)
                CMBTONAME.Focus()
                Exit Sub
            End If

            Dim DTPO As New DataTable
            If ClientName = "VAISHALI" Or ClientName = "AKASHDEEP" Then
                Dim OBJPROG As New SelectDyeingProg
                OBJPROG.PARTYNAME = CMBTONAME.Text.Trim
                OBJPROG.ShowDialog()
                DTPO = OBJPROG.DT

                If DTPO.Rows.Count > 0 Then

                    'BEFORE ADDING THE ROW IN ORDERDER GRID CHECK WHETHER SAME ORDERNO AN SRNO IS PRESENT IN GRID OR NOT
                    For Each DTROW As DataRow In DTPO.Rows
                        For Each ROW As DataGridViewRow In GRIDPROG.Rows
                            If Val(ROW.Cells(OFROMNO.Index).Value) = Val(DTROW("PROGNO")) And Val(ROW.Cells(OFROMSRNO.Index).Value) = Val(DTROW("GRIDSRNO")) And ROW.Cells(OFROMTYPE.Index).Value = DTROW("TYPE") Then GoTo NEXTLINE
                        Next

                        GRIDPROG.Rows.Add(0, DTROW("YARNQUALITY"), DTROW("DESIGNNO"), DTROW("COLOR"), Val(DTROW("WT")), DTROW("PROGNO"), DTROW("GRIDSRNO"), DTROW("TYPE"), 0)
NEXTLINE:
                    Next
                    GETSRNO(GRIDPROG)

                End If

            Else
                Dim OBJPROG As New SelectBeamProg
                OBJPROG.PARTYNAME = CMBTONAME.Text.Trim
                OBJPROG.ShowDialog()
                DTPO = OBJPROG.DT
                If DTPO.Rows.Count > 0 Then
                    TXTPROGNO.Text = Val(DTPO.Rows(0).Item("PROGNO"))
                    TXTPROGFROMTYPE.Text = DTPO.Rows(0).Item("TYPE")
                    GETSRNO(GRIDPROG)
                End If
            End If

            TOTAL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBJONO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBJONO.Validating
        Try
            If Val(CMBJONO.Text) > 0 Then

                Dim OBJCMN As New ClsCommon
                'FILL COMBO WHICH HAS BEEN OUT

                'FOR OPENING
                'IF USER HAS NOT WRITTEN BILLNO THEN IT WONT BE SHOWN HERE
                'IF USER HAS WRITTEN LOTNO THEN IT WONT BE SHOWN HERE

                Dim DT As DataTable = OBJCMN.search(" (ISNULL(YARN_TOTALWT,0)-ISNULL(YARN_RECDWT,0)) AS BALANCEWT, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(PROCESS_NAME,'') AS PROCESS ", "", "YARNISSUE INNER JOIN LEDGERS ON YARNISSUE.YARN_ledgerid = LEDGERS.Acc_id LEFT OUTER JOIN PROCESSMASTER ON YARN_PROCESSID = PROCESS_ID", " AND (ISNULL(YARN_TOTALWT,0)-ISNULL(YARN_RECDWT,0)) > 0 AND YARNISSUE.YARN_CLOSE=0 AND ISNULL(YARNISSUE.YARN_NO,'')='" & CMBJONO.Text.Trim & "' AND ISNULL(LEDGERS.Acc_cmpname,'')='" & CMBTONAME.Text.Trim & "' AND YARNISSUE.YARN_YEARID=" & YearId)

                If DT.Rows.Count > 0 Then
                    If DT.Rows(0).Item("BALANCEWT") > 0 Then
                        CMBJONO.Enabled = False
                        TXTBALWT.Text = Format(Val(DT.Rows(0).Item("BALANCEWT")), "0.00")
                        CMBPROCESS.Text = DT.Rows(0).Item("PROCESS")
                    Else
                        MsgBox("Challan Already Cleared", MsgBoxStyle.Critical)
                        e.Cancel = True
                        CMBJONO.Text = ""
                        Exit Sub
                    End If
                Else
                    MsgBox("Invalid Challan No !", MsgBoxStyle.Critical)
                    e.Cancel = True
                    CMBJONO.Text = ""
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTONAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTONAME.Validated
        Try
            If EDIT = False And CMBTONAME.Text.Trim <> "" Then
                CMBJONO.Items.Clear()
                'FILL JOBOUT NO
                'IF USER HAS NOT WRITTEN BILLNO THEN IT WONT BE SHOWN HERE
                'IF USER HAS WRITTEN LOTNO THEN IT WONT BE SHOWN HERE
                Dim OBJCMN As New ClsCommon
                'Dim DT As DataTable = OBJCMN.search(" JONO ", "", " (SELECT JOBOUT.JO_no AS JONO FROM JOBOUT INNER JOIN LEDGERS ON JOBOUT.JO_ledgerid = LEDGERS.Acc_id WHERE LEDGERS.Acc_CMPNAME='" & cmbname.Text.Trim & "' AND ROUND((JOBOUT.JO_TOTALMTRS - JOBOUT.JO_RECDMTRS),2) > 0 AND JOBOUT.JO_CLOSE=0 AND JOBOUT.JO_YEARID = " & YearId & " UNION ALL SELECT DISTINCT SM_BILLNO AS JONO FROM STOCKMASTER INNER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERIDTO= LEDGERS.Acc_id WHERE LEDGERS.ACC_CMPNAME = '" & cmbname.Text.Trim & "' AND ROUND((SM_MTRS - SM_OUTMTRS),2) > 0 AND SM_BILLNO <> 0 AND (SM_LOTNO = '' or SM_LOTNO = 0) AND SM_YEARID = " & YearId & ") AS T", "")
                Dim DT As DataTable = OBJCMN.search(" ISNULL(YARN_NO,0) AS JONO, ISNULL(LEDGERS.Acc_cmpname,'') AS NAME ", "", " YARNISSUE INNER JOIN LEDGERS ON YARNISSUE.YARN_ledgerid = LEDGERS.Acc_id", " AND (ISNULL(YARN_TOTALWT,0)-ISNULL(YARN_RECDWT,0)) > 0 AND YARNISSUE.YARN_CLOSE=0 AND ISNULL(LEDGERS.Acc_cmpname,'')='" & CMBTONAME.Text.Trim & "' AND YARNISSUE.YARN_CMPID=" & CmpId & " AND YARNISSUE.YARN_YEARID=" & YearId)

                If DT.Rows.Count > 0 Then
                    For Each DTROW As DataRow In DT.Rows
                        CMBJONO.Items.Add(DTROW("JONO"))
                    Next
                    CMBTONAME.Enabled = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCONES_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTCONES.Validated
        Try
            If ClientName = "VAISHALI" Then
                'FETCH CONEWT FROM MILLMASTER
                If Val(TXTWT.Text.Trim) = 0 And Val(TXTCONES.Text.Trim) <> 0 And CMBMILL.Text.Trim <> "" Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search("ISNULL(MILL_REMARK,0) AS CONEWT", "", "MILLMASTER ", " AND MILL_NAME = '" & CMBMILL.Text.Trim & "' AND MILL_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then TXTWT.Text = Format(Val(TXTCONES.Text.Trim) * Val(DT.Rows(0).Item("CONEWT")), "0.00")
                End If
            End If

            If CMBYARNQUALITY.Text.Trim <> "" And Val(TXTWT.Text.Trim) > 0 Then

                'check whether same quality and shade is present in grid or not
                If ClientName = "VAISHALI" And cmbcolor.Text.Trim <> "" Then
                    For Each ROW As DataGridViewRow In GRIDYARN.Rows
                        If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And ROW.Index <> TEMPROW) Then
                            If ROW.Cells(GYARNQUALITY.Index).Value = CMBYARNQUALITY.Text.Trim And ROW.Cells(gcolor.Index).Value = cmbcolor.Text.Trim Then
                                If MsgBox("Quality with same Shade already present, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                            End If
                        End If
                    Next
                End If

                fillgrid()
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtqty_Validated(sender As Object, e As EventArgs) Handles txtqty.Validated
        Try
            If ClientName = "VAISHALI" Then
                'FETCH CONEWT FROM YARNQUALITYMASTER
                If Val(TXTWT.Text.Trim) = 0 And Val(txtqty.Text.Trim) <> 0 And CMBYARNQUALITY.Text.Trim <> "" Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search("ISNULL(YARN_BOXTUBEWT,0) AS BOXWT", "", "YARNQUALITYMASTER ", " AND YARN_NAME = '" & CMBYARNQUALITY.Text.Trim & "' AND YARN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then TXTWT.Text = Format(Val(txtqty.Text.Trim) * Val(DT.Rows(0).Item("BOXWT")), "0.00")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCUT_Validated(sender As Object, e As EventArgs) Handles TXTCUT.Validated
        Try
            If Val(TXTPROGNO.Text.Trim) > 0 And Val(TXTCUT.Text.Trim) > 0 Then
                'GET PROGRAMWT FROM BEAMPROGRAM AND MULTIPLY WITH CUT TO GET TOTALWT
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                If TXTPROGFROMTYPE.Text.Trim = "BEAMPROGRAM" Then
                    DT = OBJCMN.search("ISNULL(BEAMPROGRAM.BEAMPRM_TOTALWT,0) AS PROGWT", "", " BEAMPROGRAM ", " AND BEAMPROGRAM.BEAMPRM_NO = " & Val(TXTPROGNO.Text.Trim) & " AND BEAMPROGRAM.BEAMPRM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then TXTWT.Text = Format(Val(TXTCUT.Text.Trim) * Val(DT.Rows(0).Item("PROGWT")), "0.000")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbcolor_Enter(sender As Object, e As EventArgs) Handles cmbcolor.Enter
        Try
            Cursor.Current = Cursors.WaitCursor
            FILLCOLOR(cmbcolor, CMBDESIGN.Text.Trim, "")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
End Class