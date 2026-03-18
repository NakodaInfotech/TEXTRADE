Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Runtime.Remoting.Metadata.W3cXsd2001
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports BL
Imports DevExpress.Diagram.Core.Native
Imports DevExpress.Utils.CommonDialogs
Public Class SecurityInwardOutward
    'following two variables is only for used in edit mode....
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean, GRIDUPLOADDOUBLECLICK As Boolean     'USED FOR RIGHT MANAGEMAENT
    Dim gridDoubleClick As Boolean
    Dim tempRow As Integer

    Public edit As Boolean
    Public TEMPSECNO As String
    Public tempMsg As Integer
    Dim TEMPUPLOADROW, TEMPPARAMETERROW As Integer

    Private Sub SecurityInwardOutward_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'SALE INVOICE'")

            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            clear()
            If edit = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim objSTOCK As New ClsSecurityInwardOutward()
                Dim dttable As DataTable = objSTOCK.SELECTSECURITYINOUT(TEMPSECNO, CmpId, Locationid, YearId)
                If dttable.Rows.Count > 0 Then

                    For Each dr As DataRow In dttable.Rows
                        TXTSECNO.Text = TEMPSECNO
                        TXTSECNO.ReadOnly = True
                        DTSECDATE.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")
                        TXTVEHICLENO.Text = dr("VEHICLENO")
                        TXTWT.Text = dr("WT")
                        cmbname.Text = dr("NAME")
                        TXTMATRERIAL.Text = dr("MATERIAL")
                        txtremarks.Text = Convert.ToString(dr("remarks").ToString)
                        TXTQUANTITY.Text = dr("QTY")



                    Next

                    Dim OBJCM As New ClsCommon
                    dttable = OBJCM.SEARCH(" SEC_GRIDSRNO AS GRIDSRNO, SEC_NAME AS NAME, SEC_IMGPATH AS IMGPATH, SEC_MAINSRNO AS MAINSRNO, ISNULL(SEC_IMGUPLOADPATH, '') AS IMAGEUPLOADPATH ", "", " SECURITYINOUT_UPLOAD ", " AND SEC_NO = " & TEMPSECNO & " AND SEC_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            'If IsDBNull(DTR("IMGPATH")) = False Then GRIDUPLOADDESC.Rows.Add(Val(DTR("GRIDSRNO")), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))), Val(DTR("MAINSRNO"))) Else GRIDUPLOADDESC.Rows.Add(Val(DTR("GRIDSRNO")), DTR("NAME"), Nothing, Val(DTR("MAINSRNO")))
                            If IsDBNull(DTR("IMGPATH")) = False Then GRIDUPLOADDESC.Rows.Add(Val(DTR("GRIDSRNO")), DTR("NAME"), DTR("IMGPATH"), Val(DTR("MAINSRNO")), DTR("IMAGEUPLOADPATH")) Else GRIDUPLOADDESC.Rows.Add(Val(DTR("GRIDSRNO")), DTR("NAME"), Nothing, Val(DTR("MAINSRNO")))
                            TXTUPLOADPATH.Text = DTR("IMGPATH")
                            PBIMG.ImageLocation = TXTUPLOADPATH.Text.Trim
                        Next
                    End If
                Else
                    edit = False
                    clear()
                End If

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub



    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
    Sub clear()
        cmbname.Text = ""
        TXTWT.Clear()
        TXTMATRERIAL.Clear()
        TXTVEHICLENO.Clear()
        DTSECDATE.Text = Now.Date
        TXTQUANTITY.Clear()
        txtremarks.Clear()
        EP.Clear()

        TXTUPLOADSRNO.Text = 1
        txtuploadname.Clear()
        PBIMG.ImageLocation = ""
        TXTPHOTOIMAGEUPLOADPATH.Clear()
        gridupload.RowCount = 0
        gridDoubleClick = False
        getmaxno()
        TXTUPLOADPATH.Clear()


        'EP.Clear()
        ''txtsrno.Clear()
        ''cmbitemname.Text = ""
        ''txtgridremarks.Clear()
        'TXTQTY.Clear()
        ''cmbqtyunit.Text = ""

        'txtremarks.Clear()
        'cmbname.Text = ""
        ''gridloan.RowCount = 0
        ''lbltotalqty.Text = 0.0


        'gridDoubleClick = False
        ''txtadd.Clear()


        'getmax_loan_no() 'this function is for to get max value from the Purchase loanuisition table

        'If gridloan.RowCount > 0 Then
        '    txtsrno.Text = Val(gridloan.Rows(gridloan.RowCount - 1).Cells(gsrno.Index).Value) + 1
        'Else
        '    txtsrno.Text = 1
        'End If

    End Sub

    Private Sub cmbname_Enter(sender As Object, e As EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        clear()
        edit = False
        DTSECDATE.Focus()
    End Sub

    Sub getmaxno()
        Dim DTTABLE As New DataTable
        DTTABLE = getmax(" isnull(max(SEC_NO),0) + 1 ", " SECURITYINOUT ", " AND SEC_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSECNO.Text = DTTABLE.Rows(0).Item(0)
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

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            Cursor.Current = Cursors.WaitCursor
            Dim IntResult As Integer
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(Val(TXTSECNO.Text.Trim))

            alParaval.Add(Format(Convert.ToDateTime(DTSECDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(cmbname.Text.Trim)
            alParaval.Add(TXTWT.Text.Trim)
            alParaval.Add(TXTMATRERIAL.Text.Trim)
            alParaval.Add(TXTVEHICLENO.Text.Trim)
            alParaval.Add(TXTQUANTITY.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim objclsPurord As New ClsSecurityInwardOutward()
            objclsPurord.alParaval = alParaval

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = objclsPurord.SAVE()
                TXTSECNO.Text = DT.Rows(0).Item(0)
                MessageBox.Show("Details Added")

            Else


                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPSECNO)
                IntResult = objclsPurord.UPDATE()
                MessageBox.Show("Details Updated")
                edit = False
            End If

            If GRIDUPLOADDESC.RowCount > 0 Then SAVEIMAGE()
            ' PRINTBARCODE()

            clear()
            'CMDSELECTSTOCK.Visible = True

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try


    End Sub

    Private Sub toolprevious_Click(sender As Object, e As EventArgs) Handles toolprevious.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor
            gridupload.RowCount = 0
            GRIDUPLOADDESC.RowCount = 0
LINE1:
            TEMPSECNO = Val(TXTSECNO.Text) - 1
            If TEMPSECNO > 0 Then
                edit = True
                SecurityInwardOutward_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If gridupload.RowCount = 0 And GRIDUPLOADDESC.RowCount = 0 And TEMPSECNO > 1 Then
                TXTSECNO.Text = TEMPSECNO
                'GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(sender As Object, e As EventArgs) Handles toolnext.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
LINE1:
            TEMPSECNO = Val(TXTSECNO.Text) + 1
            getmaxno()
            Dim MAXNO As Integer = TXTSECNO.Text.Trim
            clear()
            If Val(TXTSECNO.Text) - 1 >= TEMPSECNO Then
                edit = True
                SecurityInwardOutward_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If gridupload.RowCount = 0 And GRIDUPLOADDESC.RowCount = 0 And TEMPSECNO < MAXNO Then
                TXTSECNO.Text = TEMPSECNO
                'GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJstock As New SecurityInwardOutwardDetails
            OBJstock.MdiParent = MDIMain
            OBJstock.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        If cmbname.Text.Trim = "" Then FILLNAME(cmbname, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')")
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            If edit = True Then
                If MsgBox("Wish to Delete Security In/Out?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub



                Dim ALPARAVAL As New ArrayList
                Dim OBSTOCK As New ClsSecurityInwardOutward

                ALPARAVAL.Add(TEMPSECNO)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                OBSTOCK.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBSTOCK.DELETE()
                MsgBox("Security In/Out Deleted Succesfully")
                clear()
                edit = False
                DTSECDATE.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdupload_Click(sender As Object, e As EventArgs) Handles cmdupload.Click
        OpenFileDialog1.Filter = "Pictures (*.jpg;*.jpeg)|*.jpg;*.jpeg"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then TXTPHOTOIMAGEUPLOADPATH.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTPHOTOIMAGEUPLOADPATH.Text.Trim.Length <> 0 Then
            PBIMG.ImageLocation = TXTPHOTOIMAGEUPLOADPATH.Text.Trim
            txtuploadname.Text = TXTSECNO.Text & "-" & GRIDUPLOADDESC.Item(DSRNO.Index, GRIDUPLOADDESC.CurrentRow.Index).Value.ToString & "-" & TXTUPLOADSRNO.Text & "-" & YearId & ".jpg"
            'TXTFILENAME.Text = TXTFILENAME.Text & "_" & Val(TXTITEMNO.Text.Trim) & "_" & CMBDESIGNNAME.Text.Trim & ".jpg"
            TXTUPLOADPATH.Text = CATALOGPATH & "\" & txtuploadname.Text.Trim
            'GRIDUPLOADDESC.Item(DIMGPATH.Index, GRIDQC.CurrentRow.Index).Value = CATALOGPATH & "\" & txtuploadname.Text.Trim
        End If
    End Sub

    Private Sub CMDREMOVE_Click(sender As Object, e As EventArgs) Handles CMDREMOVE.Click
        Try
            PBIMG.Image = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDVIEW_Click(sender As Object, e As EventArgs) Handles CMDVIEW.Click
        Try
            'If gridupload.SelectedRows.Count > 0 Then
            '    Dim OBJCM As New ClsCommon
            '    Dim DTTABLE As DataTable
            '    DTTABLE = OBJCM.SEARCH(" FQC_GRIDSRNO AS GRIDSRNO, FQC_NAME AS NAME, FQC_IMGPATH AS IMGPATH, FQC_MAINSRNO AS MAINSRNO", "", " FINALQUALITYCHECK_UPLOAD", " AND FQC_NO = " & TEMPQCNO & " AND  FQC_MAINSRNO =   " & gridupload.Item(GQCSRNO.Index, gridupload.CurrentRow.Index).Value & "   AND   FQC_GRIDSRNO = " & gridupload.Item(GGRIDUPLOADSRNO.Index, gridupload.CurrentRow.Index).Value & " AND FQC_YEARID = " & YearId)
            '    If DTTABLE.Rows.Count > 0 Then
            '        For Each DTR As DataRow In DTTABLE.Rows
            '            'If IsDBNull(DTR("IMGPATH")) = False Then GRIDUPLOADDESC.Rows.Add(Val(DTR("GRIDSRNO")), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))), Val(DTR("MAINSRNO"))) Else GRIDUPLOADDESC.Rows.Add(Val(DTR("GRIDSRNO")), DTR("NAME"), Nothing, Val(DTR("MAINSRNO")))
            '            If IsDBNull(DTR("IMGPATH")) = False Then GRIDUPLOADDESC.Rows.Add(Val(DTR("GRIDSRNO")), DTR("NAME"), DTR("IMGPATH"), Val(DTR("MAINSRNO"))) Else GRIDUPLOADDESC.Rows.Add(Val(DTR("GRIDSRNO")), DTR("NAME"), Nothing, Val(DTR("MAINSRNO")))
            '            TXTUPLOADPATH.Text = DTR("IMGPATH")
            '            PBIMG.ImageLocation = TXTUPLOADPATH.Text.Trim
            '            If Path.GetExtension(DTR("IMGPATH")) = ".pdf" Then
            '                System.Diagnostics.Process.Start(DTR("IMGPATH"))
            '            Else
            '                Dim objVIEW As New ViewImage
            '                objVIEW.LoadImage(PBIMG.ImageLocation)
            '                'objVIEW.pbsoftcopy.Image = PBIMG.ImageLocation
            '                objVIEW.ShowDialog()
            '            End If
            '        Next
            '    End If
            'End If

            Dim imgPath As String = PBIMG.ImageLocation

            If imgPath.StartsWith("http", StringComparison.OrdinalIgnoreCase) Then
                imgPath = DownloadImageToTemp(imgPath)
            End If

            Dim objVIEW As New ViewImage
            objVIEW.StartPosition = FormStartPosition.CenterScreen
            objVIEW.FormBorderStyle = FormBorderStyle.FixedSingle
            objVIEW.MaximizeBox = False
            objVIEW.MinimizeBox = False
            objVIEW.Size = New Size(900, 600)   ' 👈 adjust if needed
            objVIEW.LoadImage(imgPath)
            objVIEW.ShowDialog()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function DownloadImageToTemp(url As String) As String
        Dim tempPath As String = Path.Combine(Path.GetTempPath(), Path.GetFileName(url))
        Using wc As New Net.WebClient()
            wc.DownloadFile(url, tempPath)
        End Using
        Return tempPath
    End Function

    Private Sub CMDRMV_Click(sender As Object, e As EventArgs) Handles CMDRMV.Click
        'If Convert.ToBoolean(GRIDQC.Rows(GRIDQC.CurrentRow.Index).Cells(GDONE.Index).Value) = True Or lbllocked.Visible = True Then
        '    MsgBox("Item Locked", MsgBoxStyle.Critical)
        '    Exit Sub
        'End If

        'If GRIDDOUBLECLICK = True Then
        '    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
        '    Exit Sub
        'End If
        Dim qcSrno As Integer = Val(GRIDUPLOADDESC.Rows(GRIDUPLOADDESC.CurrentRow.Index).Cells(DSRNO.Index).Value)

        'Loop backward to avoid skipping rows
        For i As Integer = GRIDUPLOADDESC.Rows.Count - 1 To 0 Step -1



            If Val(GRIDUPLOADDESC.Rows(i).Cells("DMAINSRNO").Value) = qcSrno Then

                Dim imgPath As String = Convert.ToString(GRIDUPLOADDESC.Rows(i).Cells(DIMGPATH.Index).Value)

                'Delete image from mapped drive
                If Not String.IsNullOrWhiteSpace(imgPath) Then
                    If File.Exists(imgPath) Then
                        File.Delete(imgPath)
                    End If
                End If

                'Remove row from grid
                GRIDUPLOADDESC.Rows.RemoveAt(i)
                gridupload.Rows.Clear()
                'gridupload.Refresh()
                'Application.DoEvents()


            End If

        Next
    End Sub

    Sub GETDESCDATA(ByVal ROWNO As Integer)
        Try
            gridupload.RowCount = 0
            PBIMG.Image = Nothing
            txtuploadname.Clear()
            For Each ROW As DataGridViewRow In GRIDUPLOADDESC.Rows
                If ROW.Cells(DMAINSRNO.Index).Value = ROWNO Then
                    gridupload.Rows.Add(ROW.Cells(DSRNO.Index).Value, ROW.Cells(DNAME.Index).Value, ROW.Cells(DIMGPATH.Index).Value, Val(ROW.Cells(DMAINSRNO.Index).Value), ROW.Cells(DIMAGEUPLOADPATH.Index).Value)
                End If
            Next
            GETSRNO(gridupload)
            TXTUPLOADSRNO.Text = gridupload.RowCount + 1

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SecurityInwardOutward_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            If ERRORVALID() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1 Then       'for Delete
            TabControl1.SelectedIndex = (0)
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2 Then       'for Delete
            TabControl1.SelectedIndex = (1)
        ElseIf e.KeyCode = Keys.OemPipe Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
            tstxtbillno.Focus()
            tstxtbillno.SelectAll()
        ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
            toolprevious_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
            toolnext_Click(sender, e)
        ElseIf e.KeyCode = Keys.F5 Then     'grid focus
            gridupload.Focus()
        ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
            Call OpenToolStripButton_Click(sender, e)
        End If
    End Sub


    Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True

            If cmbname.Text.Trim.Length = 0 Then
                EP.SetError(cmbname, " Please Fill Party Name")
                bln = False
            End If


            For Each ROW1 As DataGridViewRow In GRIDUPLOADDESC.Rows
                If DIMGPATH.Index.ToString <> "" Then

                    TXTUPLOADPATH.Text = CATALOGPATH & "\" & ROW1.Cells(DNAME.Index).Value.ToString()
                End If
            Next

            'COPY IMAGE TO THE LOCATION
            For Each ROW As DataGridViewRow In GRIDUPLOADDESC.Rows
                If DriveInfo.GetDrives().Any(Function(drive) drive.Name = CATALOGPATH & "\") Then
                    If ROW.Cells(DIMGPATH.Index).Value.ToString() <> "" And ROW.Cells(DNAME.Index).Value.ToString() <> "" And ROW.Cells(DIMAGEUPLOADPATH.Index).Value.ToString() <> "" Then
                        If File.Exists(ROW.Cells(DIMGPATH.Index).Value.ToString()) = False Then File.Copy(ROW.Cells(DIMAGEUPLOADPATH.Index).Value.ToString(), ROW.Cells(DIMGPATH.Index).Value.ToString(), True)
                    End If
                Else
                    TXTUPLOADPATH.Text = CATALOGIP & ROW.Cells(DNAME.Index).Value.ToString
                    If ROW.Cells(DIMGPATH.Index).Value.ToString <> "" And ROW.Cells(DNAME.Index).Value.ToString <> "" And ROW.Cells(DIMAGEUPLOADPATH.Index).Value <> "" Then
                        If File.Exists(ROW.Cells(DIMAGEUPLOADPATH.Index).Value) Then UPLOADIMAGE(ROW.Cells(DIMAGEUPLOADPATH.Index).Value, CATALOGIP)
                    End If
                End If
            Next
            'If gridupload.RowCount = 0 And GRIDUPLOADDESC.RowCount = 0 Then
            '    EP.SetError(TabControl1, "Fill Item Details")
            '    bln = False
            'End If
            'CHEKC BARCODE IS PRESENT IN DATABASE OR NOT

            If Not datecheck(DTSECDATE.Text) Then
                EP.SetError(DTSECDATE, "Date not in Accounting Year")
                bln = False
            End If

            If TXTVEHICLENO.Text.Trim.Length = 0 Then
                EP.SetError(TXTVEHICLENO, "Please Fill Vehicle No")
                bln = False
            End If

            Return bln
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Sub SAVEIMAGE()
        Try
            'UPLOAD IMAGE
            Dim OBJQC As New ClsSecurityInwardOutward
            For Each row As Windows.Forms.DataGridViewRow In GRIDUPLOADDESC.Rows
                Dim ALPARAVAL As New ArrayList
                If row.Cells(0).Value <> Nothing Then

                    ALPARAVAL.Add(Val(TXTSECNO.Text.Trim))
                    ALPARAVAL.Add(Val(row.Cells(DSRNO.Index).Value))
                    ALPARAVAL.Add(row.Cells(DNAME.Index).Value.ToString)
                    ALPARAVAL.Add(row.Cells(DIMGPATH.Index).Value.ToString)

                    'If row.Cells(DIMGPATH.Index).Value IsNot Nothing Then
                    '    Dim MS As New IO.MemoryStream
                    '    PBIMG.Image = row.Cells(DIMGPATH.Index).Value
                    '    Dim IMG As New Bitmap(PBIMG.Image)
                    '    IMG.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    '    ALPARAVAL.Add(MS.ToArray)
                    'Else
                    '    ALPARAVAL.Add(DBNull.Value)
                    'End If

                    ALPARAVAL.Add(Val(row.Cells(DMAINSRNO.Index).Value))
                    ALPARAVAL.Add(row.Cells(DIMAGEUPLOADPATH.Index).Value.ToString)
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(YearId)

                    OBJQC.alParaval = ALPARAVAL
                    Dim INTRES As Integer = OBJQC.SAVEIMAGE()

                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'FOR IMAGE
    Sub FILLGRIDSCAN()
        Try
            Dim sourcePath As String = TXTPHOTOIMAGEUPLOADPATH.Text.Trim
            Dim targetPath As String = TXTUPLOADPATH.Text.Trim

            'Dim resizedPath As String = ResizeAndSaveFromFile(sourcePath, targetPath)


            If GRIDUPLOADDOUBLECLICK = False Then

                'gridupload.Rows.Add(Val(TXTUPLOADSRNO.Text.Trim), txtuploadname.Text.Trim, PBIMG.Image, Val(GRIDQC.CurrentRow.Cells(gsrno.Index).Value))
                gridupload.Rows.Add(Val(TXTUPLOADSRNO.Text.Trim), txtuploadname.Text.Trim, TXTUPLOADPATH.Text.Trim, Val(GRIDUPLOADDESC.CurrentRow.Cells(DSRNO.Index).Value), TXTPHOTOIMAGEUPLOADPATH.Text.Trim)

                'GRIDUPLOADDESC.Rows.Add(Val(TXTUPLOADSRNO.Text.Trim), txtuploadname.Text.Trim, PBIMG.Image, Val(GRIDQC.CurrentRow.Cells(gsrno.Index).Value))
                GRIDUPLOADDESC.Rows.Add(Val(TXTUPLOADSRNO.Text.Trim), txtuploadname.Text.Trim, TXTUPLOADPATH.Text.Trim, Val(GRIDUPLOADDESC.CurrentRow.Cells(DSRNO.Index).Value), TXTPHOTOIMAGEUPLOADPATH.Text.Trim)

                uploadgetsrno(gridupload)

            ElseIf GRIDUPLOADDOUBLECLICK = True Then


                'FIRST GETTING ROWNO WITH RESPECT TO GRIDPAYDESC'S SRNO AND PAYMENT'S GRIDSRNO
                Dim ROWNO As Integer = 0
                For Each ROW As DataGridViewRow In GRIDUPLOADDESC.Rows
                    If ROW.Cells(DSRNO.Index).Value = gridupload.CurrentRow.Cells(GGRIDUPLOADSRNO.Index).Value And ROW.Cells(DMAINSRNO.Index).Value = (GRIDUPLOADDESC.CurrentRow.Index + 1) Then
                        ROWNO = ROW.Index
                        Exit For
                    End If
                Next

                GRIDUPLOADDESC.Item(DSRNO.Index, ROWNO).Value = TXTUPLOADSRNO.Text.Trim
                GRIDUPLOADDESC.Item(DNAME.Index, ROWNO).Value = txtuploadname.Text.Trim
                'GRIDUPLOADDESC.Item(DIMGPATH.Index, ROWNO).Value = PBIMG.Image
                GRIDUPLOADDESC.Item(DIMGPATH.Index, ROWNO).Value = TXTUPLOADPATH.Text.Trim


                gridupload.Item(GGRIDUPLOADSRNO.Index, TEMPUPLOADROW).Value = TXTUPLOADSRNO.Text.Trim
                gridupload.Item(GNAME.Index, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
                'gridupload.Item(GIMGPATH.Index, TEMPUPLOADROW).Value = PBIMG.Image
                gridupload.Item(GIMGPATH.Index, TEMPUPLOADROW).Value = TXTUPLOADPATH.Text.Trim

                GRIDUPLOADDOUBLECLICK = False

            End If
            gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub



    Sub uploadgetsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            'If edit = False Then
            Dim i As Integer = 0
            For Each row As DataGridViewRow In grid.Rows
                If row.Visible = True Then
                    row.Cells(GGRIDUPLOADSRNO.Index).Value = i + 1
                    i = i + 1
                End If
            Next
            'End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Async Sub UPLOADIMAGE(FILEPATH, UPLOADPATH)
        Await UploadFileAsync(FILEPATH, UPLOADPATH)
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(sender As Object, e As EventArgs) Handles tooldelete.Click
        Dim IntResult As Integer
        Try

            If edit = True Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete Security Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim alParaval As New ArrayList
                alParaval.Add(TEMPSECNO)
                alParaval.Add(YearId)

                Dim clspo As New ClsSecurityInwardOutward
                clspo.alParaval = alParaval
                IntResult = clspo.DELETE()
                MsgBox("Security Entry Deleted")
                clear()
                edit = False

            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Public Shared Async Function UploadFileAsync(filePath As String, uploadUrl As String) As Task
        Try
            Using client As New HttpClient()
                Using content As New MultipartFormDataContent()
                    ' Read the JPG into memory
                    Dim fileContent As New ByteArrayContent(File.ReadAllBytes(filePath))
                    fileContent.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg")

                    ' "file" here is the form field name expected by the server-side script
                    content.Add(fileContent, "image", Path.GetFileName(filePath))

                    ' Send the POST
                    Dim response = Await client.PostAsync(uploadUrl, content)

                    ' Check result
                    If response.IsSuccessStatusCode Then
                        Dim serverReply = Await response.Content.ReadAsStringAsync()
                        Console.WriteLine("Upload successful! Server said: " & serverReply)
                    Else
                        Console.WriteLine("Upload failed: " & response.StatusCode)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Upload failed: " & ex.Message)
        End Try
    End Function


    Private Async Function gridupload_CellClickAsync(sender As Object, e As DataGridViewCellEventArgs) As Task Handles gridupload.CellClick
        Try

            If e.RowIndex >= 0 Then

                'GET DESIGN FROM PATH
                If DriveInfo.GetDrives().Any(Function(drive) drive.Name = CATALOGPATH & "\") Then
                    Dim imgPath As String = Convert.ToString(gridupload.Rows(e.RowIndex).Cells("GIMGPATH").Value)
                    If IO.File.Exists(imgPath) Then
                        PBIMG.ImageLocation = imgPath
                    Else
                        PBIMG.Image = Nothing
                    End If
                Else
                    '    'Get design from database BLOB
                    '    Dim OBJCMN As New ClsCommon
                    '    Dim DT As New DataTable

                    '    Dim designNo As Integer = Val(gridupload.Rows(e.RowIndex).Cells("ITEMDESIGNNO").Value)

                    '    DT = OBJCMN.SEARCH(
                    '    "FINALQUALITYCHECK_UPDLOAD.FQC_IMAGEPATH AS IMAGEPATH",
                    '    "",
                    '    " FINALQUALITYCHECK_UPDLOAD ",
                    '    " AND FINALQUALITYCHECK_UPDLOAD.FQC_NO = " & TEMPQCNO &
                    '    " AND FINALQUALITYCHECK_UPDLOAD.FQC_YEARID = " & YearId &
                    '    "AND FINALQUALITYCHECK_UPDLOAD.FQC_GRIDSRNO = " & Convert.ToString(gridupload.Rows(e.RowIndex).Cells("GGRIDUPLOADSRNO").Value) &
                    '    " ORDER BY FINALQUALITYCHECK_UPDLOAD.FQC_NO "
                    ')

                    '    If DT.Rows.Count > 0 AndAlso Not IsDBNull(DT.Rows(0)("IMAGEPATH")) Then
                    '        PBIMG.Image = Image.FromStream(New IO.MemoryStream(CType(DT.Rows(0)("IMAGEPATH"), Byte())))
                    '    Else
                    '        PBIMG.Image = Nothing
                    '    End If


                    Dim imgPath As String = CATALOGIP & Convert.ToString(gridupload.Rows(e.RowIndex).Cells("GNAME").Value)
                    If Await ImageExistsAsync(imgPath) Then
                        PBIMG.ImageLocation = imgPath
                    Else
                        'IF STILL THE IMAGE IS BLANK THEN OPENB IT FROM LOCAL PATH
                        PBIMG.ImageLocation = gridupload.Rows(e.RowIndex).Cells("GIMAGEUPLOADPATH").Value
                    End If
                End If
            End If


        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Function

    Public Async Function ImageExistsAsync(imageUrl As String) As Task(Of Boolean)
        Try
            Using client As New HttpClient()
                Using response = Await client.SendAsync(New HttpRequestMessage(HttpMethod.Head, imageUrl))
                    Return response.IsSuccessStatusCode
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function


    Function ResizeAndSaveFromFile(originalPath As String, savePath As String) As String
        Try
            'Safely load file into memory
            Dim imgBytes() As Byte = File.ReadAllBytes(originalPath)
            Dim ms As New MemoryStream(imgBytes)
            Dim original As Image = Image.FromStream(ms)

            Dim targetWidth As Integer = 700
            Dim ratio As Double = targetWidth / original.Width
            Dim newHeight As Integer = CInt(original.Height * ratio)

            Using bmp As New Bitmap(targetWidth, newHeight)
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.DrawImage(original, 0, 0, targetWidth, newHeight)
                End Using

                Dim folder As String = Path.GetDirectoryName(savePath)
                If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)
                bmp.Save(savePath, Imaging.ImageFormat.Jpeg)
            End Using

            original.Dispose()
            ms.Dispose()

            Return savePath

        Catch ex As Exception
            MsgBox("Resize Error: " & ex.Message)
            Return ""
        End Try
    End Function

    Private Sub txtuploadname_Validating(sender As Object, e As CancelEventArgs) Handles txtuploadname.Validating
        Try
            If PBIMG.ImageLocation <> "" And txtuploadname.Text.Trim <> "" And GRIDUPLOADDESC.RowCount > 0 Then
                FILLGRIDSCAN()
                txtuploadname.Clear()
                'PBIMG.ImageLocation = ""
                TXTUPLOADPATH.Clear() 'NEW CODE
                TXTPHOTOIMAGEUPLOADPATH.Clear()
                txtuploadname.Focus()
                TXTUPLOADSRNO.Text = gridupload.RowCount + 1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtremarks_KeyDown(sender As Object, e As KeyEventArgs) Handles txtremarks.KeyDown
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
End Class