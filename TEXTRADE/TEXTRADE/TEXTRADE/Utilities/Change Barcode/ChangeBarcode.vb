
Imports BL
Imports System.IO
Imports System.ComponentModel

Public Class ChangeBarcode

    Public TEMPNO As Integer
    Public EDIT As Boolean

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ChangeBarcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBPIECETYPE.Text.Trim = "" Then fillPIECETYPE(CMBPIECETYPE)
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
            If CMBQUALITY.Text.Trim = "" Then fillQUALITY(CMBQUALITY, EDIT)
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, "")
            If CMBUNIT.Text.Trim = "" Then fillunit(CMBUNIT)
            If CMBRACK.Text.Trim = "" Then FILLRACK(CMBRACK)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ChangeBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FILLCMB()
            CLEAR()

            If EDIT = True Then

                Dim objSTOCK As New ClsChangeBarcode()
                Dim dttable As DataTable = objSTOCK.SELECTCHANGEBARCODE(TEMPNO, CmpId, Locationid, YearId)
                If dttable.Rows.Count > 0 Then
                    For Each dr As DataRow In dttable.Rows
                        TXTSRNO.Text = TEMPNO
                        ENTRYDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        CMBITEMNAME.Text = dr("NEWITEMNAME")
                        CMBQUALITY.Text = dr("NEWQUALITY")
                        CMBDESIGN.Text = dr("NEWDESIGNNO")
                        TXTGODOWN.Text = dr("GODOWN")
                        CMBPIECETYPE.Text = dr("NEWPIECETYPE")
                        CMBSHADE.Text = dr("NEWCOLOR")
                        CMBUNIT.Text = dr("NEWUNIT")
                        TXTSTAMPING.Text = dr("NEWSTAMPING")
                        TXTDESC.Text = dr("NEWDESC")
                        CMBRACK.Text = dr("NEWRACK")
                        TXTREMARKS.Text = dr("REMARKS")

                        'Item Grid
                        GRIDCHANGEBARCODE.Rows.Add(0, dr("PIECETYPE"), dr("ITEMNAME"), dr("QUALITY"), dr("GRIDREMARKS"), dr("DESIGNNO"), dr("COLOR"), dr("UNIT"), dr("LOTNO"), Format(Val(dr("CUT")), "0.00"), Format(Val(dr("MTRS")), "0.00"), dr("RACK"), dr("SHELF"), dr("BARCODE"), Val(dr("FROMNO")), Val(dr("FROMSRNO")), dr("TYPE"))
                    Next
                    GETSRNO(GRIDCHANGEBARCODE)
                Else
                    EDIT = False
                    CLEAR()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            EP.Clear()
            txtbarcode.Clear()
            txtcopies.Text = 1
            TXTPIECETYPE.Clear()
            TXTITEMNAME.Clear()
            TXTQUALITY.Clear()
            TXTDESIGN.Clear()
            TXTSHADE.Clear()
            TXTUNIT.Clear()
            TXTRACK.Clear()
            TXTMTRS.Clear()
            TXTLOTNO.Clear()

            CMBITEMNAME.Text = ""
            CMBQUALITY.Text = ""
            CMBDESIGN.Text = ""
            TXTGODOWN.Clear()
            CMBPIECETYPE.Text = ""
            CMBSHADE.Text = ""
            CMBUNIT.Text = ""
            TXTSTAMPING.Clear()
            TXTDESC.Clear()
            CMBRACK.Text = ""
            TXTREMARKS.Clear()
            GETMAXNO()

            ENTRYDATE.Text = Now.Date
            GRIDCHANGEBARCODE.RowCount = 0

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True


            If CMBITEMNAME.Text.Trim = "" And CMBDESIGN.Text.Trim = "" And CMBQUALITY.Text.Trim = "" And CMBPIECETYPE.Text.Trim = "" And CMBSHADE.Text.Trim = "" And CMBUNIT.Text.Trim = "" And CMBRACK.Text.Trim = "" Then
                EP.SetError(txtbarcode, "Fill Proper Data")
                BLN = False
            End If

            If GRIDCHANGEBARCODE.RowCount = 0 Then
                EP.SetError(txtbarcode, "Fill Item Details")
                BLN = False
            End If


            If ENTRYDATE.Text = "__/__/____" Then
                EP.SetError(ENTRYDATE, " Please Enter Proper Date")
                BLN = False
            Else
                If Not datecheck(ENTRYDATE.Text) Then
                    EP.SetError(ENTRYDATE, "Date not in Accounting Year")
                    BLN = False
                End If
            End If



            Return BLN
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            EDIT = False
            txtbarcode.Focus()
            TEMPNO = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAXNO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(CB_NO),0) + 1 ", " CHANGEBARCODE ", " AND CB_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSRNO.Text = Val(DTTABLE.Rows(0).Item(0))
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            'DONT ALLOW TO MODIFY DONE BY GULKIT
            If EDIT = True Then Exit Sub


            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(ENTRYDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBITEMNAME.Text.Trim)
            alParaval.Add(CMBQUALITY.Text.Trim)
            alParaval.Add(CMBDESIGN.Text.Trim)
            alParaval.Add(TXTGODOWN.Text.Trim)
            alParaval.Add(CMBPIECETYPE.Text.Trim)
            alParaval.Add(CMBSHADE.Text.Trim)
            alParaval.Add(CMBUNIT.Text.Trim)
            alParaval.Add(TXTSTAMPING.Text.Trim)
            alParaval.Add(TXTDESC.Text.Trim)
            alParaval.Add(CMBRACK.Text.Trim)
            alParaval.Add(TXTREMARKS.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim GRIDSRNO As String = ""
            Dim PIECETYPE As String = ""
            Dim ITEMNAME As String = ""
            Dim QUALITY As String = ""
            Dim GRIDDESC As String = ""
            Dim DESIGNNO As String = ""
            Dim SHADE As String = ""
            Dim UNIT As String = ""
            Dim LOTNO As String = ""
            Dim CUT As String = ""
            Dim MTRS As String = ""
            Dim RACK As String = ""
            Dim SHELF As String = ""
            Dim BARCODE As String = ""
            Dim FROMNO As String = ""
            Dim FROMSRNO As String = ""
            Dim FROMTYPE As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCHANGEBARCODE.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(row.Cells(GSRNO.Index).Value)
                        PIECETYPE = row.Cells(GPIECETYPE.Index).Value.ToString
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        If row.Cells(GPRINTDESC.Index).Value <> Nothing Then GRIDDESC = row.Cells(GPRINTDESC.Index).Value.ToString Else GRIDDESC = ""
                        DESIGNNO = row.Cells(GDESIGN.Index).Value.ToString
                        SHADE = row.Cells(GSHADE.Index).Value.ToString
                        UNIT = row.Cells(GUNIT.Index).Value.ToString
                        LOTNO = row.Cells(GLOTNO.Index).Value.ToString
                        CUT = Val(row.Cells(GCUT.Index).Value)
                        MTRS = Val(row.Cells(GMTRS.Index).Value)
                        RACK = row.Cells(GRACK.Index).Value.ToString
                        SHELF = row.Cells(GSHELF.Index).Value.ToString
                        BARCODE = row.Cells(GBARCODE.Index).Value.ToString
                        FROMNO = Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = row.Cells(GFROMTYPE.Index).Value.ToString

                    Else

                        GRIDSRNO = GRIDSRNO & "|" & Val(row.Cells(GSRNO.Index).Value)
                        PIECETYPE = PIECETYPE & "|" & row.Cells(GPIECETYPE.Index).Value.ToString
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value.ToString
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        If row.Cells(GPRINTDESC.Index).Value <> Nothing Then GRIDDESC = GRIDDESC & "|" & row.Cells(GPRINTDESC.Index).Value.ToString Else GRIDDESC = GRIDDESC & "|" & ""
                        DESIGNNO = DESIGNNO & "|" & row.Cells(GDESIGN.Index).Value.ToString
                        SHADE = SHADE & "|" & row.Cells(GSHADE.Index).Value.ToString
                        UNIT = UNIT & "|" & row.Cells(GUNIT.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        CUT = CUT & "|" & Val(row.Cells(GLOTNO.Index).Value)
                        MTRS = MTRS & "|" & Val(row.Cells(GMTRS.Index).Value)
                        RACK = RACK & "|" & row.Cells(GRACK.Index).Value
                        SHELF = SHELF & "|" & row.Cells(GSHELF.Index).Value
                        BARCODE = BARCODE & "|" & row.Cells(GBARCODE.Index).Value.ToString
                        FROMNO = FROMNO & "|" & Val(row.Cells(GFROMNO.Index).Value)
                        FROMSRNO = FROMSRNO & "|" & Val(row.Cells(GFROMSRNO.Index).Value)
                        FROMTYPE = FROMTYPE & "|" & row.Cells(GFROMTYPE.Index).Value.ToString

                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(PIECETYPE)
            alParaval.Add(ITEMNAME)
            alParaval.Add(QUALITY)
            alParaval.Add(GRIDDESC)
            alParaval.Add(DESIGNNO)
            alParaval.Add(SHADE)
            alParaval.Add(UNIT)
            alParaval.Add(LOTNO)
            alParaval.Add(CUT)
            alParaval.Add(MTRS)
            alParaval.Add(RACK)
            alParaval.Add(SHELF)
            alParaval.Add(BARCODE)
            alParaval.Add(FROMNO)
            alParaval.Add(FROMSRNO)
            alParaval.Add(FROMTYPE)


            Dim OBJCHANGE As New ClsChangeBarcode()
            OBJCHANGE.alParaval = alParaval

            If EDIT = False Then
                Dim DTTABLE As DataTable = OBJCHANGE.SAVE()
                MsgBox("Details Added")
                TXTSRNO.Text = DTTABLE.Rows(0).Item(0)
                TEMPNO = DTTABLE.Rows(0).Item(0)

            End If
            UPDATEBARCODES()
            EDIT = False
            TEMPNO = 0
            CLEAR()
            txtbarcode.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub UPDATEBARCODES()
        Try
            'UPDATE THE BARCODE FIRST THEN PRINT
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            Dim UPDATEQUERY As String = ""


            Dim PIECETYPEID As Integer = 0
            Dim ITEMID As Integer = 0
            Dim QUALITYID As Integer = 0
            Dim DESIGNID As Integer = 0
            Dim COLORID As Integer = 0
            Dim UNITID As Integer = 0
            Dim RACKID As Integer = 0

            If CMBPIECETYPE.Text.Trim <> "" Then
                DT = OBJCMN.search("PIECETYPE_ID AS PIECETYPEID", "", "PIECETYPEMASTER", " AND PIECETYPE_NAME = '" & CMBPIECETYPE.Text.Trim & "' AND PIECETYPE_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then PIECETYPEID = DT.Rows(0).Item("PIECETYPEID")
            End If

            If CMBITEMNAME.Text.Trim <> "" Then
                DT = OBJCMN.search("ITEM_ID AS ITEMID", "", "ITEMMASTER", " AND ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then ITEMID = DT.Rows(0).Item("ITEMID")
            End If

            If CMBQUALITY.Text.Trim <> "" Then
                DT = OBJCMN.search("QUALITY_ID AS QUALITYID", "", "QUALITYMASTER", " AND QUALITY_NAME = '" & CMBQUALITY.Text.Trim & "' AND QUALITY_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then QUALITYID = DT.Rows(0).Item("QUALITYID")
            End If

            If CMBDESIGN.Text.Trim <> "" Then
                DT = OBJCMN.search("DESIGN_ID AS DESIGNID", "", "DESIGNMASTER", " AND DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' AND DESIGN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then DESIGNID = DT.Rows(0).Item("DESIGNID")
            Else
                DESIGNID = 0
            End If

            If CMBSHADE.Text.Trim <> "" Then
                DT = OBJCMN.search("COLOR_ID AS COLORID", "", "COLORMASTER", " AND COLOR_NAME = '" & CMBSHADE.Text.Trim & "' AND COLOR_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then COLORID = DT.Rows(0).Item("COLORID")
            Else
                COLORID = 0
            End If

            If CMBUNIT.Text.Trim <> "" Then
                DT = OBJCMN.search("UNIT_ID AS UNITID", "", "UNITMASTER", " AND UNIT_ABBR = '" & CMBUNIT.Text.Trim & "' AND UNIT_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then UNITID = DT.Rows(0).Item("UNITID")
            End If

            If CMBRACK.Text.Trim <> "" Then
                DT = OBJCMN.search("RACK_ID AS RACKID", "", "RACKMASTER", " AND RACK_NAME = '" & CMBRACK.Text.Trim & "' AND RACK_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then RACKID = DT.Rows(0).Item("RACKID")
            End If

            For Each ROW As DataGridViewRow In GRIDCHANGEBARCODE.Rows
                If ROW.Cells(GFROMTYPE.Index).Value = "OPENING" Then
                    UPDATEQUERY = "SM_MODIFIED = GETDATE()"
                    If CMBPIECETYPE.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SM_PIECETYPEID = " & PIECETYPEID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_PIECETYPEID = 0"
                    If CMBITEMNAME.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SM_ITEMID = " & ITEMID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_ITEMID = 0"
                    If CMBQUALITY.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SM_QUALITYID = " & QUALITYID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_QUALITYID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", SM_DESIGNID = " & DESIGNID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_DESIGNID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", SM_COLORID = " & COLORID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_COLORID = 0"
                    If CMBUNIT.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SM_UNITID = " & UNITID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_UNITID = 0"
                    If CMBRACK.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SM_RACKID = " & RACKID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_UNITID = 0"
                    If TXTSTAMPING.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SM_GRIDREMARKS = '" & TXTSTAMPING.Text.Trim & "'"

                    DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET " & UPDATEQUERY & " WHERE SM_NO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND SM_YEARID = " & YearId, "", "")


                ElseIf ROW.Cells(GFROMTYPE.Index).Value = "GRN" Then
                    UPDATEQUERY = "GRN_MODIFIED = GETDATE()"
                    If CMBPIECETYPE.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", GRN_PIECETYPEID = " & PIECETYPEID 'Else UPDATEQUERY = UPDATEQUERY + ", GRN_PIECETYPEID = 0"
                    If CMBITEMNAME.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", GRN_ITEMID = " & ITEMID 'Else UPDATEQUERY = UPDATEQUERY + ", GRN_ITEMID = 0"
                    If CMBQUALITY.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", GRN_QUALITYID = " & QUALITYID 'Else UPDATEQUERY = UPDATEQUERY + ", GRN_QUALITYID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", GRN_DESIGNID = " & DESIGNID 'Else UPDATEQUERY = UPDATEQUERY + ", GRN_DESIGNID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", GRN_COLORID = " & COLORID 'Else UPDATEQUERY = UPDATEQUERY + ", GRN_COLORID = 0"
                    If CMBUNIT.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", GRN_QTYUNITID = " & UNITID 'Else UPDATEQUERY = UPDATEQUERY + ", GRN_QTYUNITID = 0"
                    If CMBRACK.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", GRN_RACKID = " & RACKID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_UNITID = 0"
                    If TXTSTAMPING.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", GRN_GRIDREMARKS = '" & TXTSTAMPING.Text.Trim & "'"

                    DT = OBJCMN.Execute_Any_String(" UPDATE GRN_DESC SET " & UPDATEQUERY & " WHERE GRN_NO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND GRN_GRIDSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND GRN_GRIDTYPE ='FANCY MATERIAL' AND GRN_YEARID = " & YearId, "", "")


                ElseIf ROW.Cells(GFROMTYPE.Index).Value = "MATREC" Then
                    UPDATEQUERY = "MATREC_MODIFIED = GETDATE()"
                    If CMBITEMNAME.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", MATREC_ITEMID = " & ITEMID 'Else UPDATEQUERY = UPDATEQUERY + ", MATREC_ITEMID = 0"
                    If CMBQUALITY.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", MATREC_QUALITYID = " & QUALITYID 'Else UPDATEQUERY = UPDATEQUERY + ", MATREC_QUALITYID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", MATREC_DESIGNID = " & DESIGNID 'Else UPDATEQUERY = UPDATEQUERY + ", MATREC_DESIGNID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", MATREC_COLORID = " & COLORID 'Else UPDATEQUERY = UPDATEQUERY + ", MATREC_COLORID = 0"
                    If CMBUNIT.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", MATREC_QTYUNITID = " & UNITID 'Else UPDATEQUERY = UPDATEQUERY + ", MATREC_QTYUNITID = 0"
                    If CMBRACK.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", MATREC_RACKID = " & RACKID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_UNITID = 0"

                    DT = OBJCMN.Execute_Any_String(" UPDATE MATERIALRECEIPT_DESC SET " & UPDATEQUERY & " WHERE MATREC_NO= " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND MATREC_GRIDSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND MATREC_YEARID = " & YearId, "", "")


                ElseIf ROW.Cells(GFROMTYPE.Index).Value = "INHOUSECHECK" Then
                    UPDATEQUERY = "CHECK_USERID = " & Userid
                    If CMBPIECETYPE.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", CHECK_PIECETYPEID = " & PIECETYPEID 'Else UPDATEQUERY = UPDATEQUERY + ", CHECK_PIECETYPEID = 0"
                    If CMBITEMNAME.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", CHECK_ITEMID = " & ITEMID 'Else UPDATEQUERY = UPDATEQUERY + ", CHECK_ITEMID = 0"
                    If CMBQUALITY.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", CHECK_QUALITYID = " & QUALITYID 'Else UPDATEQUERY = UPDATEQUERY + ", CHECK_QUALITYID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", CHECK_DESIGNID = " & DESIGNID 'Else UPDATEQUERY = UPDATEQUERY + ", CHECK_DESIGNID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", CHECK_COLORID = " & COLORID 'Else UPDATEQUERY = UPDATEQUERY + ", CHECK_COLORID = 0"
                    If CMBUNIT.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", CHECK_UNITID = " & UNITID 'Else UPDATEQUERY = UPDATEQUERY + ", CHECK_UNITID = 0"
                    If CMBRACK.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", CHECK_RACKID = " & RACKID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_UNITID = 0"
                    If TXTSTAMPING.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", CHECK_NARR = '" & TXTSTAMPING.Text.Trim & "'"

                    DT = OBJCMN.Execute_Any_String(" UPDATE INHOUSECHECKING_DESC SET " & UPDATEQUERY & " WHERE CHECK_NO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND CHECK_GRIDSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND CHECK_YEARID = " & YearId & "", "", "")


                ElseIf ROW.Cells(GFROMTYPE.Index).Value = "FINALPACKING" Then
                    UPDATEQUERY = "FP_USERID = " & Userid
                    If CMBITEMNAME.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", FP_ITEMID = " & ITEMID 'Else UPDATEQUERY = UPDATEQUERY + ", FP_ITEMID = 0"
                    If CMBQUALITY.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", FP_QUALITYID = " & QUALITYID 'Else UPDATEQUERY = UPDATEQUERY + ", FP_QUALITYID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", FP_DESIGNID = " & DESIGNID 'Else UPDATEQUERY = UPDATEQUERY + ", FP_DESIGNID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", FP_COLORID = " & COLORID 'Else UPDATEQUERY = UPDATEQUERY + ", FP_COLORID = 0"

                    DT = OBJCMN.Execute_Any_String(" UPDATE FINALPACKING_DESC SET " & UPDATEQUERY & " WHERE FP_NO = " & Val(ROW.Cells(GFROMNO.Index).Value) & "  AND FP_GRIDSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND FP_YEARID = " & YearId, "", "")


                ElseIf ROW.Cells(GFROMTYPE.Index).Value = "JOBIN" Then
                    UPDATEQUERY = "JI_MODIFIED = GETDATE()"
                    If CMBPIECETYPE.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", JI_PIECETYPEID = " & PIECETYPEID 'Else UPDATEQUERY = UPDATEQUERY + ", JI_PIECETYPEID = 0"
                    If CMBITEMNAME.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", JI_ITEMID = " & ITEMID 'Else UPDATEQUERY = UPDATEQUERY + ", JI_ITEMID = 0"
                    If CMBQUALITY.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", JI_QUALITYID = " & QUALITYID 'Else UPDATEQUERY = UPDATEQUERY + ", JI_QUALITYID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", JI_DESIGNID = " & DESIGNID 'Else UPDATEQUERY = UPDATEQUERY + ", JI_DESIGNID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", JI_COLORID = " & COLORID 'Else UPDATEQUERY = UPDATEQUERY + ", JI_COLORID = 0"
                    If CMBUNIT.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", JI_QTYUNITID= " & UNITID 'Else UPDATEQUERY = UPDATEQUERY + ", JI_QTYUNITID = 0"
                    If CMBRACK.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", JI_RACKID = " & RACKID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_UNITID = 0"

                    DT = OBJCMN.Execute_Any_String(" UPDATE JOBIN_DESC SET " & UPDATEQUERY & " WHERE JI_NO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND JI_GRIDSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND JI_YEARID = " & YearId, "", "")

                ElseIf ROW.Cells(GFROMTYPE.Index).Value = "PACKING" Then
                    UPDATEQUERY = "REC_MODIFIED = GETDATE()"
                    If CMBPIECETYPE.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", REC_PIECETYPEID = " & PIECETYPEID 'Else UPDATEQUERY = UPDATEQUERY + ", REC_PIECETYPEID = 0"
                    If CMBITEMNAME.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", REC_ITEMID = " & ITEMID 'Else UPDATEQUERY = UPDATEQUERY + ", REC_ITEMID = 0"
                    If CMBQUALITY.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", REC_QUALITYID = " & QUALITYID 'Else UPDATEQUERY = UPDATEQUERY + ", REC_QUALITYID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", REC_DESIGNID = " & DESIGNID 'Else UPDATEQUERY = UPDATEQUERY + ", REC_DESIGNID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", REC_COLORID = " & COLORID 'Else UPDATEQUERY = UPDATEQUERY + ", REC_COLORID = 0"
                    If CMBUNIT.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", REC_QTYUNITID = " & UNITID 'Else UPDATEQUERY = UPDATEQUERY + ", REC_QTYUNITID = 0"
                    If CMBRACK.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", REC_RACKID = " & RACKID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_UNITID = 0"
                    If TXTSTAMPING.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", REC_GRIDREMARKS = '" & TXTSTAMPING.Text.Trim & "'"

                    DT = OBJCMN.Execute_Any_String(" UPDATE RECPACKING_DESC SET " & UPDATEQUERY & " WHERE REC_NO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND REC_GRIDSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND REC_YEARID = " & YearId, "", "")

                ElseIf ROW.Cells(GFROMTYPE.Index).Value = "SALERETCHALLAN" Then
                    UPDATEQUERY = "SRCH_MODIFIED = GETDATE()"
                    If CMBPIECETYPE.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SRCH_PIECETYPEID = " & PIECETYPEID 'Else UPDATEQUERY = UPDATEQUERY + ", SRCH_PIECETYPEID = 0"
                    If CMBITEMNAME.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SRCH_ITEMID = " & ITEMID 'Else UPDATEQUERY = UPDATEQUERY + ", SRCH_ITEMID = 0"
                    If CMBQUALITY.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SRCH_QUALITYID = " & QUALITYID 'Else UPDATEQUERY = UPDATEQUERY + ", SRCH_QUALITYID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", SRCH_DESIGNID = " & DESIGNID 'Else UPDATEQUERY = UPDATEQUERY + ", SRCH_DESIGNID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", SRCH_COLORID = " & COLORID 'Else UPDATEQUERY = UPDATEQUERY + ", SRCH_COLORID = 0"
                    If CMBUNIT.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SRCH_QTYUNITID = " & UNITID 'Else UPDATEQUERY = UPDATEQUERY + ", SRCH_QTYUNITID = 0"
                    If CMBRACK.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SRCH_RACKID = " & RACKID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_UNITID = 0"

                    DT = OBJCMN.Execute_Any_String(" UPDATE SALERETURNCHALLAN_DESC SET " & UPDATEQUERY & " WHERE SRCH_NO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND SRCH_GRIDSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND SRCH_YEARID = " & YearId, "", "")

                ElseIf ROW.Cells(GFROMTYPE.Index).Value = "SALERET" Then
                    UPDATEQUERY = "SALRET_MODIFIED = GETDATE()"
                    If CMBPIECETYPE.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SALRET_PIECETYPEID = " & PIECETYPEID 'Else UPDATEQUERY = UPDATEQUERY + ", SALRET_PIECETYPEID = 0"
                    If CMBITEMNAME.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SALRET_ITEMID = " & ITEMID 'Else UPDATEQUERY = UPDATEQUERY + ", SALRET_ITEMID = 0"
                    If CMBQUALITY.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SALRET_QUALITYID = " & QUALITYID 'Else UPDATEQUERY = UPDATEQUERY + ", SALRET_QUALITYID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", SALRET_DESIGNID = " & DESIGNID 'Else UPDATEQUERY = UPDATEQUERY + ", SALRET_DESIGNID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", SALRET_COLORID = " & COLORID 'Else UPDATEQUERY = UPDATEQUERY + ", SALRET_COLORID = 0"
                    If CMBUNIT.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SALRET_QTYUNITID = " & UNITID 'Else UPDATEQUERY = UPDATEQUERY + ", SALRET_QTYUNITID = 0"
                    If CMBRACK.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SALRET_RACKID = " & RACKID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_UNITID = 0"

                    DT = OBJCMN.Execute_Any_String(" UPDATE SALERETURN_DESC SET " & UPDATEQUERY & " WHERE SALRET_NO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND SALRET_GRIDSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND SALRET_YEARID = " & YearId, "", "")

                ElseIf ROW.Cells(GFROMTYPE.Index).Value = "STOCKADJUSTMENT" Then
                    UPDATEQUERY = "SA_MODIFIED = GETDATE()"
                    If CMBPIECETYPE.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SA_PIECETYPEID = " & PIECETYPEID 'Else UPDATEQUERY = UPDATEQUERY + ", SA_PIECETYPEID = 0"
                    If CMBITEMNAME.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SA_ITEMID = " & ITEMID 'Else UPDATEQUERY = UPDATEQUERY + ", SA_ITEMID = 0"
                    If CMBQUALITY.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SA_QUALITYID = " & QUALITYID 'Else UPDATEQUERY = UPDATEQUERY + ", SA_QUALITYID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", SA_DESIGNID = " & DESIGNID 'Else UPDATEQUERY = UPDATEQUERY + ", SA_DESIGNID = 0"
                    UPDATEQUERY = UPDATEQUERY + ", SA_COLORID = " & COLORID 'Else UPDATEQUERY = UPDATEQUERY + ", SA_COLORID = 0"
                    If CMBUNIT.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SA_QTYUNITID = " & UNITID 'Else UPDATEQUERY = UPDATEQUERY + ", SA_QTYUNITID = 0"
                    If CMBRACK.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SA_RACKID = " & RACKID 'Else UPDATEQUERY = UPDATEQUERY + ", SM_UNITID = 0"
                    If TXTSTAMPING.Text.Trim <> "" Then UPDATEQUERY = UPDATEQUERY + ", SA_GRIDDESC = '" & TXTSTAMPING.Text.Trim & "'"

                    DT = OBJCMN.Execute_Any_String(" UPDATE STOCKADJUSTMENT_INDESC SET " & UPDATEQUERY & " WHERE SA_NO = " & Val(ROW.Cells(GFROMNO.Index).Value) & " AND SA_GRIDSRNO = " & Val(ROW.Cells(GFROMSRNO.Index).Value) & " AND SA_YEARID = " & YearId, "", "")

                End If
            Next



            If MsgBox("Wish to Print Barcode?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                Dim WHOLESALEBARCODE As Integer = 0
                If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then WHOLESALEBARCODE = MsgBox("Wish to Print Wholesale Barcode?", MsgBoxStyle.YesNo)

                Dim TEMPHEADER As String = ""
                If ClientName = "YASHVI" Then
                    TEMPHEADER = InputBox("Enter Sticker Type (M/N/Y/B)")
                    If TEMPHEADER <> "M" And TEMPHEADER <> "N" And TEMPHEADER <> "Y" And TEMPHEADER <> "B" Then Exit Sub
                    If TEMPHEADER = "M" Then TEMPHEADER = "MAFATLAL"
                    If TEMPHEADER = "N" Then TEMPHEADER = ""
                End If

                If ClientName = "GELATO" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR MRP" & Chr(13) & "3 FOR WSP")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
                End If

                If ClientName = "DAKSH" Or ClientName = "KUNAL" Or ClientName = "VALIANT" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                If ClientName = "SST" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED" & Chr(13) & "3 FOR MRP")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
                End If

                If ClientName = "MANSI" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR PRE PRINTED" & Chr(13) & "3 FOR MRP" & Chr(13) & "4 FOR 100 X 50")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" And TEMPHEADER <> "4" Then Exit Sub
                End If

                If ClientName = "RAJKRIPA" Or ClientName = "MOHATUL" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR LUMP" & Chr(13) & "2 FOR CUTPACK")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                If ClientName = "MANS" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR SALVATROE" & Chr(13) & "2 FOR DONBION" & Chr(13) & "2 FOR OCM")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" Then Exit Sub
                End If

                If ClientName = "AXIS" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR PCS" & Chr(13) & "2 FOR MTRS")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                If ClientName = "KRISHNA" Or ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Or ClientName = "SIMPLEX" Then
                    TEMPHEADER = InputBox("Enter Sticker Type " & Chr(13) & "1 FOR NORMAL" & Chr(13) & "2 FOR BOX STICKER")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" Then Exit Sub
                End If

                Dim SUPRIYAHEADER As String = ""
                If ClientName = "SUPRIYA" Then
                    TEMPHEADER = InputBox("Enter Sticker Type (1/2/3/4/5/6/7)")
                    If TEMPHEADER <> "1" And TEMPHEADER <> "2" And TEMPHEADER <> "3" And TEMPHEADER <> "4" And TEMPHEADER <> "5" And TEMPHEADER <> "6" And TEMPHEADER <> "7" Then Exit Sub
                    If TEMPHEADER = "1" Or TEMPHEADER = "6" Then SUPRIYAHEADER = "ROYAL TEX"
                    If TEMPHEADER = "2" Or TEMPHEADER = "7" Then SUPRIYAHEADER = "DEEP BLUE"
                    If TEMPHEADER = "3" Then SUPRIYAHEADER = ""
                    If TEMPHEADER = "4" Then SUPRIYAHEADER = "KAMDHENU"
                    If TEMPHEADER = "5" Then SUPRIYAHEADER = "5"
                End If

                For Each ROW As DataGridViewRow In GRIDCHANGEBARCODE.Rows
                    For I As Integer = 1 To Val(txtcopies.Text.Trim)
                        BARCODEPRINTING(ROW.Cells(GBARCODE.Index).Value, ROW.Cells(GPIECETYPE.Index).Value, CMBITEMNAME.Text.Trim, CMBQUALITY.Text.Trim, CMBDESIGN.Text.Trim, CMBSHADE.Text.Trim, CMBUNIT.Text.Trim, ROW.Cells(GLOTNO.Index).Value, ROW.Cells(GPRINTDESC.Index).Value, ROW.Cells(GPRINTDESC.Index).Value, Val(ROW.Cells(GMTRS.Index).Value), 1, Val(ROW.Cells(GCUT.Index).Value), ROW.Cells(GRACK.Index).Value, TEMPHEADER, SUPRIYAHEADER, WHOLESALEBARCODE, "", "", ROW.Cells(GSHELF.Index).Value)
                    Next
                Next

            End If

        Catch ex As Exception
            Throw ex
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

    Private Sub txtremarks_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTREMARKS.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJREMARKS As New SelectRemarks
                OBJREMARKS.FRMSTRING = "NARRATION"
                OBJREMARKS.ShowDialog()
                If OBJREMARKS.TEMPNAME <> "" Then TXTREMARKS.Text = OBJREMARKS.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJstock As New ChangeBarcodeDetails
            OBJstock.MdiParent = MDIMain
            OBJstock.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Try
            Call CMDOK_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcopies.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub ENTRYDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ENTRYDATE.GotFocus
        ENTRYDATE.SelectionStart = 0
    End Sub

    Private Sub ENTRYDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ENTRYDATE.Validating
        Try
            If ENTRYDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(ENTRYDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBITEMNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem
                OBJItem.FRMSTRING = "MERCHANT"
                OBJItem.STRSEARCH = " and ITEM_cmpid = " & CmpId & " and ITEM_LOCATIONid = " & Locationid & " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then CMBITEMNAME.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITY_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBQUALITY.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJQ As New SelectQuality
                OBJQ.FRMSTRING = "QUALITY"
                OBJQ.ShowDialog()
                If OBJQ.TEMPNAME <> "" Then CMBQUALITY.Text = OBJQ.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGN_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBDESIGN.Enter
        Try
            If CMBDESIGN.Text.Trim = "" Then FILLDESIGN(CMBDESIGN, CMBITEMNAME.Text.Trim)
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

    Private Sub CMBDESIGN_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGN.Validating
        Try
            If CMBDESIGN.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGN, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSHADE.Enter
        Try
            If CMBSHADE.Text.Trim = "" Then FILLCOLOR(CMBSHADE, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBSHADE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJCOLOR As New SelectShade
                OBJCOLOR.ShowDialog()
                If OBJCOLOR.TEMPNAME <> "" Then CMBSHADE.Text = OBJCOLOR.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSHADE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSHADE.Validating
        Try
            If CMBSHADE.Text.Trim <> "" Then COLORVALIDATE(CMBSHADE, e, Me, CMBDESIGN.Text.Trim, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub ChangeBarcode_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "CC" Or ClientName = "C3" Or ClientName = "SHREEDEV" Then CHKBARCODE.Visible = True
        If ClientName = "SANGHVI" Or ClientName = "TINUMINU" Or ClientName = "KDFAB" Then
            LBLDESC.Visible = True
            TXTDESC.Visible = True
        End If
    End Sub

    Private Sub txtbarcode_Validated(sender As Object, e As EventArgs) Handles txtbarcode.Validated
        Try
            If txtbarcode.Text.Trim <> "" Then

                'GET DATA FROM BARCODE
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("TOP 1 *", "", "BARCODESTOCK", " AND BARCODE = '" & txtbarcode.Text.Trim & "' AND YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If DT.Rows(0).Item("DONE") = 1 Then
                        MsgBox("Barcode is Locked", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    For Each ROW As DataGridViewRow In GRIDCHANGEBARCODE.Rows
                        If LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(txtbarcode.Text.Trim) Then GoTo LINE1
                    Next

                    TXTPIECETYPE.Text = DT.Rows(0).Item("PIECETYPE")
                    TXTITEMNAME.Text = DT.Rows(0).Item("ITEMNAME")
                    TXTQUALITY.Text = DT.Rows(0).Item("QUALITY")
                    TXTDESIGN.Text = DT.Rows(0).Item("DESIGNNO")
                    TXTSHADE.Text = DT.Rows(0).Item("COLOR")
                    TXTUNIT.Text = DT.Rows(0).Item("UNIT")
                    TXTGODOWN.Text = DT.Rows(0).Item("GODOWN")
                    TXTLOTNO.Text = DT.Rows(0).Item("CHALLANNO")
                    TXTMTRS.Text = Format((Val(DT.Rows(0).Item("MTRS"))), "0.00")
                    TXTSTAMPING.Text = DT.Rows(0).Item("GRIDREMARKS")
                    TXTRACK.Text = DT.Rows(0).Item("RACK")

                    CMBPIECETYPE.Text = DT.Rows(0).Item("PIECETYPE")
                    CMBITEMNAME.Text = DT.Rows(0).Item("ITEMNAME")
                    CMBDESIGN.Text = DT.Rows(0).Item("DESIGNNO")
                    CMBQUALITY.Text = DT.Rows(0).Item("QUALITY")
                    CMBSHADE.Text = DT.Rows(0).Item("COLOR")
                    CMBUNIT.Text = DT.Rows(0).Item("UNIT")
                    CMBRACK.Text = DT.Rows(0).Item("RACK")

                    TXTFROMNO.Text = Val(DT.Rows(0).Item("FROMNO"))
                    TXTFROMSRNO.Text = Val(DT.Rows(0).Item("FROMSRNO"))
                    TXTTYPE.Text = DT.Rows(0).Item("TYPE")

                    GRIDCHANGEBARCODE.Rows.Add(GRIDCHANGEBARCODE.RowCount + 1, DT.Rows(0).Item("PIECETYPE"), DT.Rows(0).Item("ITEMNAME"), DT.Rows(0).Item("QUALITY"), DT.Rows(0).Item("GRIDREMARKS"), DT.Rows(0).Item("DESIGNNO"), DT.Rows(0).Item("COLOR"), DT.Rows(0).Item("UNIT"), DT.Rows(0).Item("LOTNO"), Format(Val(DT.Rows(0).Item("CUT")), "0.00"), Format(Val(DT.Rows(0).Item("MTRS")), "0.00"), DT.Rows(0).Item("RACK"), DT.Rows(0).Item("SHELF"), DT.Rows(0).Item("BARCODE"), Val(DT.Rows(0).Item("FROMNO")), Val(DT.Rows(0).Item("FROMSRNO")), DT.Rows(0).Item("TYPE"))

LINE1:

                    txtbarcode.Clear()
                    txtbarcode.Focus()

                Else
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    txtbarcode.Clear()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDGDN_KeyDown(sender As Object, e As KeyEventArgs) Handles GRIDCHANGEBARCODE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCHANGEBARCODE.RowCount > 0 Then GRIDCHANGEBARCODE.Rows.RemoveAt(GRIDCHANGEBARCODE.CurrentRow.Index)
            GETSRNO(GRIDCHANGEBARCODE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPIECETYPE_Enter(sender As Object, e As EventArgs) Handles CMBPIECETYPE.Enter
        Try
            If CMBPIECETYPE.Text.Trim = "" Then fillPIECETYPE(CMBPIECETYPE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPIECETYPE_Validating(sender As Object, e As CancelEventArgs) Handles CMBPIECETYPE.Validating
        Try
            If CMBPIECETYPE.Text.Trim <> "" Then PIECETYPEvalidate(CMBPIECETYPE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTSTOCK_Click(sender As Object, e As EventArgs) Handles CMDSELECTSTOCK.Click
        Try
            Dim DTGDN As New DataTable
            Dim OBJSELECTGDN As New SelectStockGDN
            OBJSELECTGDN.ShowDialog()
            DTGDN = OBJSELECTGDN.DT

            If DTGDN.Rows.Count > 0 Then
                For Each DTROWPS As DataRow In DTGDN.Rows

                    'CHECK WHETHER BARCODE IS ALREADY PRESENT IN GRID OR NOT
                    For Each ROW As DataGridViewRow In GRIDCHANGEBARCODE.Rows
                        If DTROWPS("BARCODE") <> "" And LCase(ROW.Cells(GBARCODE.Index).Value) = LCase(DTROWPS("BARCODE")) Or (DTROWPS("BARCODE") = "" And Val(ROW.Cells(GFROMNO.Index).Value) = Val(DTROWPS("FROMNO")) And Val(ROW.Cells(GFROMSRNO.Index).Value) = Val(DTROWPS("FROMSRNO"))) Then GoTo LINE1
                    Next

                    Dim GRIDDESC As String = ""
                    If ClientName = "AVIS" Then GRIDDESC = DTROWPS("LOTNO")
                    GRIDCHANGEBARCODE.Rows.Add(0, DTROWPS("PIECETYPE"), DTROWPS("ITEMNAME"), DTROWPS("QUALITY"), DTROWPS("GRIDREMARKS"), DTROWPS("DESIGNNO"), DTROWPS("COLOR"), DTROWPS("UNIT"), DTROWPS("LOTNO"), Format(Val(DTROWPS("CUT")), "0.00"), Format(Val(DTROWPS("MTRS")), "0.00"), DTROWPS("RACK"), DTROWPS("SHELF"), DTROWPS("BARCODE"), Val(DTROWPS("FROMNO")), Val(DTROWPS("FROMSRNO")), DTROWPS("TYPE"))
LINE1:
                Next
                CMDSELECTSTOCK.Enabled = True
                GETSRNO(GRIDCHANGEBARCODE)
                GRIDCHANGEBARCODE.FirstDisplayedScrollingRowIndex = GRIDCHANGEBARCODE.RowCount - 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validated(sender As Object, e As EventArgs) Handles CMBITEMNAME.Validated
        Try
            If CMBITEMNAME.Text.Trim <> "" Then
                'GET DISPLAY NAME IN GRIDREMARKS
                If ClientName = "RAJKRIPA" Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_DISPLAYNAME, '') AS DISPLAYNAME", "", " ITEMMASTER ", " AND ITEMMASTER.ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "' AND ITEMMASTER.ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        For Each DTROW As DataRow In DT.Rows
                            TXTDESC.Text = (DT.Rows(0).Item("DISPLAYNAME"))
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBRACK_Enter(sender As Object, e As EventArgs) Handles CMBRACK.Enter
        Try
            If CMBRACK.Text.Trim = "" Then FILLRACK(CMBRACK)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBRACK_Validating(sender As Object, e As CancelEventArgs) Handles CMBRACK.Validating
        Try
            If CMBRACK.Text.Trim <> "" Then RACKVALIDATE(CMBRACK, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class