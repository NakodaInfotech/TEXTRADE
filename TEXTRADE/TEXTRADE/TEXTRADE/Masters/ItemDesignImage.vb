Imports BL
Imports System.IO
Imports System.ComponentModel

Public Class ItemDesignImage

    Public EDIT As Boolean              'Used for edit
    Public ITEMDESIGNNO As Integer            'Used for edit id
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            EDIT = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        CMBITEMNAME.Text = ""
        CMBDESIGNNAME.Text = ""
        PBIMAGE1.Image = Nothing
        PBIMAGE2.Image = Nothing
        PBIMAGEPATH.Image = Nothing
        TXTSETMTRS.Clear()
        GETMAXNO()
        EP.Clear()
        TXTPHOTOIMGPATH1.Clear()
        TXTPHOTOIMGPATH2.Clear()
        TXTPHOTOIMAGEUPLOADPATH.Clear()
        TXTUPLOADPATH.Clear()
        TXTFILENAME.Clear()
        If CATALOGPATH <> "" Then
            TabControl1.SelectedIndex = 1
            TXTUPLOADPATH.Text = CATALOGPATH
        End If
    End Sub

    Private Sub CMDPHOTOUPLOAD1_Click(sender As Object, e As EventArgs) Handles CMDPHOTOUPLOAD1.Click
        OpenFileDialog1.Filter = "Pictures (*.jpg;*.jpeg)|*.jpg;*.jpeg"
        OpenFileDialog1.ShowDialog()
        TXTPHOTOIMGPATH1.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTPHOTOIMGPATH1.Text.Trim.Length <> 0 Then PBIMAGE1.ImageLocation = TXTPHOTOIMGPATH1.Text.Trim
    End Sub

    Sub AUTOCOMPRESSIMG()
        Try
            Dim imgSource As System.Drawing.Image = PBIMAGE1.Image
            Dim imgOutput As System.Drawing.Image
            Dim intPercent As Integer = 50
            Dim intType As Integer = 0

            'resize the image by percent
            Dim intX, intY As Integer
            intX = Int(imgSource.Width / 100 * intPercent)
            intY = Int(imgSource.Height / 100 * intPercent)
            Dim bm As Drawing.Bitmap = New System.Drawing.Bitmap(intX, intY)
            Dim g As System.Drawing.Graphics = Drawing.Graphics.FromImage(bm)

            Select Case intType
                Case 0
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.Default
                Case 1
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.High
                Case 2
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBilinear
                Case 3
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
            End Select

            g.DrawImage(imgSource, 0, 0, intX, intY)
            imgOutput = bm
            PBIMAGE2.Image = imgOutput

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPHOTOUPLOAD2_Click(sender As Object, e As EventArgs) Handles CMDPHOTOUPLOAD2.Click
        OpenFileDialog1.Filter = "Pictures (*.jpg;*.jpeg)|*.jpg;*.jpeg"
        OpenFileDialog1.ShowDialog()
        TXTPHOTOIMGPATH2.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTPHOTOIMGPATH2.Text.Trim.Length <> 0 Then PBIMAGE2.ImageLocation = TXTPHOTOIMGPATH2.Text.Trim
    End Sub

    Private Sub CMDPHOTOREMOVE1_Click(sender As Object, e As EventArgs) Handles CMDPHOTOREMOVE1.Click
        PBIMAGE1.Image = Nothing
        TXTPHOTOIMGPATH1.Clear()
    End Sub

    Private Sub CMDPHOTOREMOVE2_Click(sender As Object, e As EventArgs) Handles CMDPHOTOREMOVE2.Click
        PBIMAGE2.Image = Nothing
        TXTPHOTOIMGPATH2.Clear()
    End Sub

    Private Sub CMDPHOTOVIEW1_Click(sender As Object, e As EventArgs) Handles CMDPHOTOVIEW1.Click
        Try
            If TXTPHOTOIMGPATH1.Text.Trim <> "" Then
                If Path.GetExtension(TXTPHOTOIMGPATH1.Text.Trim) = ".pdf" Then
                    System.Diagnostics.Process.Start(TXTPHOTOIMGPATH1.Text.Trim)
                Else
                    Dim objVIEW As New ViewImage
                    objVIEW.pbsoftcopy.Image = PBIMAGE1.Image
                    objVIEW.ShowDialog()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPHOTOVIEW2_Click(sender As Object, e As EventArgs) Handles CMDPHOTOVIEW2.Click
        Try
            If TXTPHOTOIMGPATH2.Text.Trim <> "" Then
                If Path.GetExtension(TXTPHOTOIMGPATH2.Text.Trim) = ".pdf" Then
                    System.Diagnostics.Process.Start(TXTPHOTOIMGPATH2.Text.Trim)
                Else
                    Dim objVIEW As New ViewImage
                    objVIEW.pbsoftcopy.Image = PBIMAGE2.Image
                    objVIEW.ShowDialog()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If EDIT = True Then

                Dim objcls As New ClsItemDesignImage()
                If MsgBox("Wish To Delete?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim alParaval As New ArrayList
                alParaval.Add(ITEMDESIGNNO)
                alParaval.Add(CmpId)
                alParaval.Add(YearId)
                objcls.alParaval = alParaval
                Dim DT As DataTable = objcls.Delete()
                MsgBox(DT.Rows(0).Item(0))
                CLEAR()
                EDIT = False

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAXNO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(ITEMDESIGN_NO),0) + 1 ", "  ITEMDESIGNIMAGE ", " AND ITEMDESIGN_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTITEMNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim intResult As Integer
            Dim Alparaval As New ArrayList

            Alparaval.Add(CMBITEMNAME.Text.Trim)
            Alparaval.Add(CMBDESIGNNAME.Text.Trim)

            If PBIMAGE1.Image IsNot Nothing Then
                Dim MS As New IO.MemoryStream
                PBIMAGE1.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                Alparaval.Add(MS.ToArray)
            Else
                Alparaval.Add(DBNull.Value)
            End If

            If PBIMAGE2.Image IsNot Nothing Then
                Dim MS As New IO.MemoryStream
                PBIMAGE2.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                Alparaval.Add(MS.ToArray)
            Else
                Alparaval.Add(DBNull.Value)
            End If

            Alparaval.Add(CmpId)
            Alparaval.Add(Userid)
            Alparaval.Add(YearId)
            Alparaval.Add(TXTSETMTRS.Text.Trim)
            Alparaval.Add(TXTFILENAME.Text.Trim)
            Alparaval.Add(TXTUPLOADPATH.Text.Trim)
            'Alparaval.Add(TXTPHOTOIMGPATH1.Text.Trim)

            Dim OBJIMAGE As New ClsItemDesignImage
            OBJIMAGE.alParaval = Alparaval
            If EDIT = False Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                intResult = OBJIMAGE.SAVE()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                Alparaval.Add(ITEMDESIGNNO)
                intResult = OBJIMAGE.UPDATE()
                'MsgBox("Details Updated")
            End If

            CLEAR()
            EDIT = False
            CMBITEMNAME.Focus()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNNAME_Enter(sender As Object, e As EventArgs) Handles CMBDESIGNNAME.Enter
        Try
            If CMBDESIGNNAME.Text.Trim = "" Then FILLDESIGN(CMBDESIGNNAME, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            fillitemname(CMBITEMNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            FILLDESIGN(CMBDESIGNNAME, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Try
            Dim bln As Boolean = True
            If CMBITEMNAME.Text.Trim = "" Then
                EP.SetError(CMBITEMNAME, " FILL ITEM NAME")
                bln = False
            End If

            If CMBDESIGNNAME.Text.Trim = "" And ClientName <> "MNARESH" Then
                EP.SetError(CMBDESIGNNAME, " FILL DESIGN NAME")
                bln = False
            End If
            If ClientName = "SNCM" Or ClientName = "MNARESH" Then
                If TXTSETMTRS.Text.Trim = "" Then
                    EP.SetError(TXTSETMTRS, "Enter Set Mtrs")
                    bln = False
                End If
            End If


            If CMBITEMNAME.Text.Trim <> "" And CMBDESIGNNAME.Text.Trim <> "" Then
                TXTFILENAME.Text = CMBITEMNAME.Text.Split(" "c).[Select](Function(x) x.ToUpper.First()).ToArray()
                TXTFILENAME.Text = TXTFILENAME.Text & "_" & Val(TXTITEMNO.Text.Trim) & "_" & CMBDESIGNNAME.Text.Trim & ".jpg"
            End If


            'COPY IMAGE TO THE LOCATION
            If TXTUPLOADPATH.Text.Trim <> "" And TXTFILENAME.Text.Trim <> "" And TXTPHOTOIMAGEUPLOADPATH.Text.Trim <> "" Then
                If File.Exists(TXTUPLOADPATH.Text.Trim) = False Then File.Copy(TXTPHOTOIMAGEUPLOADPATH.Text.Trim, TXTUPLOADPATH.Text.Trim, True)
            End If


            'CHECKING WHETHER IMAGE FOR SAME TIEM AND DESIGN IS PRESENT OR NOT
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" ITEMDESIGNIMAGE.ITEMDESIGN_NO AS ITEMDESIGNNO ", "", " ITEMDESIGNIMAGE INNER JOIN ITEMMASTER ON item_id = ITEMDESIGN_ITEMID INNER JOIN DESIGNMASTER ON DESIGN_ID=ITEMDESIGN_DESIGNID ", " AND ITEMMASTER.ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "' AND DESIGNMASTER.DESIGN_NO = '" & CMBDESIGNNAME.Text.Trim & "' AND ITEMDESIGN_YEARID = " & YearId)
            If DT.Rows.Count > 0 AndAlso (EDIT = False Or (EDIT = True And ITEMDESIGNNO <> Val(DT.Rows(0).Item("ITEMDESIGNNO")))) Then
                EP.SetError(CMBITEMNAME, " Image for This Item and Design is already Uploaded ")
                bln = False
            End If

            If ClientName <> "MNARESH" Then
                If PBIMAGE1.Image Is Nothing And PBIMAGE2.Image Is Nothing And PBIMAGEPATH.Image Is Nothing Then
                    EP.SetError(PBIMAGE1, " Upload Image")
                    bln = False
                End If
            End If
            Return bln
        Catch ex As Exception
            Throw ex
        End Try



    End Function

    Private Sub ItemDesignImage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            FILLCMB
            CLEAR()
            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim OBJIMAGE As New ClsItemDesignImage
                OBJIMAGE.alParaval.Add(ITEMDESIGNNO)
                OBJIMAGE.alParaval.Add(CmpId)
                OBJIMAGE.alParaval.Add(YearId)
                Dim DT As DataTable = OBJIMAGE.GETITEM()

                If DT.Rows.Count > 0 Then
                    CMBITEMNAME.Text = DT.Rows(0).Item("ITEMNAME")
                    CMBDESIGNNAME.Text = DT.Rows(0).Item("DESIGNNAME")
                    TXTITEMNO.Text = Val(DT.Rows(0).Item("ITEMDESIGNNO"))
                    If IsDBNull(DT.Rows(0).Item("IMAGE1")) = False Then
                        PBIMAGE1.Image = Image.FromStream(New IO.MemoryStream(DirectCast(DT.Rows(0).Item("IMAGE1"), Byte())))
                        TXTPHOTOIMGPATH1.Text = DT.Rows(0).Item("IMAGE1").ToString
                    Else
                        PBIMAGE1.Image = Nothing
                    End If
                    If IsDBNull(DT.Rows(0).Item("IMAGE2")) = False Then
                        PBIMAGE2.Image = Image.FromStream(New IO.MemoryStream(DirectCast(DT.Rows(0).Item("IMAGE2"), Byte())))
                        TXTPHOTOIMGPATH2.Text = DT.Rows(0).Item("IMAGE2").ToString
                    Else
                        PBIMAGE2.Image = Nothing
                    End If
                    TXTSETMTRS.Text = Val(DT.Rows(0).Item("SETMTRS"))
                    TXTFILENAME.Text = DT.Rows(0).Item("FILENAME")
                    TXTUPLOADPATH.Text = DT.Rows(0).Item("UPLOADPATH")
                    PBIMAGEPATH.ImageLocation = TXTUPLOADPATH.Text.Trim
                End If
            Else
                EDIT = False
                CLEAR()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCOMPRESS_Click(sender As Object, e As EventArgs) Handles CMDCOMPRESS.Click
        Try
            If PBIMAGE1.ImageLocation <> "" Then
                AUTOCOMPRESSIMG()
                TXTPHOTOIMGPATH2.Text = OpenFileDialog1.FileName
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBDESIGNNAME.Validating
        Try
            If CMBDESIGNNAME.Text.Trim <> "" Then DESIGNVALIDATE(CMBDESIGNNAME, e, Me, CMBITEMNAME.Text.Trim)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPDATE_Click(sender As Object, e As EventArgs) Handles CMDUPDATE.Click
        Try
            'GET DATA FROM ITEMDESIGNIMAGE 
            Dim OBJCMN As New ClsCommon
            Dim FROMROWNO As Integer = Val(InputBox("Enter Start Row No"))
            Dim TOROWNO As Integer = Val(InputBox("Enter End Row No"))

            Dim DT As DataTable = OBJCMN.SEARCH("ITEMDESIGNIMAGE.ITEMDESIGN_NO AS ITEMDESIGNNO ,DESIGNMASTER.DESIGN_NO AS DESIGNNAME, ITEMMASTER.item_name AS ITEMNAME, ITEMDESIGNIMAGE.ITEMDESIGN_IMAGE1 AS IMAGE1, ITEMDESIGNIMAGE.ITEMDESIGN_IMAGE2 AS IMAGE2 , ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_SETMTRS , 0) AS SETMTRS, ISNULL(imagepath,'') AS imagepath, 'E:\SOFTWARES\IMAGES\' + ISNULL(imagepath,'') AS OURIMGPATH", "", " ITEMDESIGNIMAGE LEFT OUTER JOIN  DESIGNMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = DESIGNMASTER.DESIGN_yearid AND ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = ITEMMASTER.item_yearid AND ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = ITEMMASTER.item_id INNER JOIN tempdesignimageupload ON ITEMDESIGN_ITEMID = ITEMID AND ITEMDESIGN_DESIGNID= DESIGNID", " and ITEMDESIGN_NO > " & Val(FROMROWNO) & " AND ITEMDESIGN_NO <= " & Val(TOROWNO) & "  AND  ITEMDESIGN_YEARID = " & YearId & " ORDER BY ITEMDESIGN_NO")
            For Each ROW As DataRow In DT.Rows
                EDIT = True
                ITEMDESIGNNO = Val(ROW("ITEMDESIGNNO"))
                ItemDesignImage_Load(sender, e)

                If Not File.Exists(TXTPHOTOIMGPATH1.Text) Then GoTo NEXTLINE
                If TXTPHOTOIMGPATH1.Text.Trim.Length <> 0 Then PBIMAGE1.ImageLocation = TXTPHOTOIMGPATH1.Text.Trim
                PBIMAGE1.Image = Image.FromFile(TXTPHOTOIMGPATH1.Text.Trim)
                PBIMAGE1.Refresh()


                Call CMDCOMPRESS_Click(sender, e)
                CMDOK_Click(sender, e)
NEXTLINE:
                'CLEAR()
            Next
        Catch ex As Exception
        Throw ex
        End Try
    End Sub

    Private Sub CMDDOWNLOAD_Click(sender As Object, e As EventArgs) Handles CMDDOWNLOAD.Click
        Try
            'GET DATA FROM ITEMDESIGNIMAGE 
            Dim OBJCMN As New ClsCommon
            Dim FROMROWNO As Integer = Val(InputBox("Enter Start Row No"))
            Dim TOROWNO As Integer = Val(InputBox("Enter End Row No"))
            Dim DT As DataTable = OBJCMN.SEARCH("  ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_NO, 0) AS ITEMDESIGNNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNAME, ITEMDESIGNIMAGE.ITEMDESIGN_IMAGE1 AS IMAGE1, ITEMDESIGNIMAGE.ITEMDESIGN_IMAGE2 AS IMAGE2, ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_SETMTRS, 0) AS SETMTRS  ", "", " ITEMDESIGNIMAGE LEFT OUTER JOIN DESIGNMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN ITEMMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = ITEMMASTER.item_id ", "  and ITEMDESIGN_NO > " & Val(FROMROWNO) & " AND ITEMDESIGN_NO <= " & Val(TOROWNO) & " AND ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = " & YearId & " ORDER BY ITEMDESIGNIMAGE.ITEMDESIGN_NO")
            For Each ROW As DataRow In DT.Rows
                EDIT = True
                CMBDESIGNNAME.Text = ROW("DESIGNNAME")
                CMBITEMNAME.Text = ROW("ITEMNAME")
                TXTITEMNO.Text = Val(ROW("ITEMDESIGNNO"))
                If IsDBNull(ROW("IMAGE1")) = False Then
                    PBIMAGE1.Image = Image.FromStream(New IO.MemoryStream(DirectCast(ROW("IMAGE1"), Byte())))
                    TXTPHOTOIMGPATH1.Text = ROW("IMAGE1").ToString
                    Me.PBIMAGE1.Image.Save(IO.Path.Combine("D:\IMAGES", TXTFILENAME.Text.Trim & ".jpg"))
                Else
                    PBIMAGE1.Image = Nothing
                End If
                If IsDBNull(ROW("IMAGE2")) = False Then
                    PBIMAGE2.Image = Image.FromStream(New IO.MemoryStream(DirectCast(ROW("IMAGE2"), Byte())))
                    TXTPHOTOIMGPATH2.Text = ROW("IMAGE2").ToString
                Else
                    PBIMAGE2.Image = Nothing
                End If
                TXTSETMTRS.Text = Val(ROW("SETMTRS"))
                TXTFILENAME.Text = CMBITEMNAME.Text.Split(" "c).[Select](Function(x) x.ToUpper.First()).ToArray()
                TXTFILENAME.Text = TXTFILENAME.Text & "_" & CMBDESIGNNAME.Text.Trim
                'PBIMAGE1.Image.Save("D:IMAGES\" & TXTFILENAME.Text.Trim & ".jpg")

                Dim DTUPDATE As DataTable = OBJCMN.Execute_Any_String("UPDATE ITEMDESIGNIMAGE SET ITEMDESIGN_FILENAME = '" & TXTFILENAME.Text.Trim & "' WHERE ITEMDESIGN_NO = " & Val(TXTITEMNO.Text.Trim) & " AND ITEMDESIGN_YEARID = " & YearId, "", "")

            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPLOADPATH_Click(sender As Object, e As EventArgs) Handles CMDUPLOADPATH.Click
        OpenFileDialog1.Filter = "Pictures (*.jpg;*.jpeg)|*.jpg;*.jpeg"
        OpenFileDialog1.ShowDialog()
        TXTPHOTOIMAGEUPLOADPATH.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTPHOTOIMAGEUPLOADPATH.Text.Trim.Length <> 0 Then
            PBIMAGEPATH.ImageLocation = TXTPHOTOIMAGEUPLOADPATH.Text.Trim
            TXTFILENAME.Text = CMBITEMNAME.Text.Split(" "c).[Select](Function(x) x.ToUpper.First()).ToArray()
            TXTFILENAME.Text = TXTFILENAME.Text & "_" & Val(TXTITEMNO.Text.Trim) & "_" & CMBDESIGNNAME.Text.Trim & ".jpg"
            TXTUPLOADPATH.Text = CATALOGPATH & "\" & TXTFILENAME.Text.Trim
        End If

    End Sub

    Private Sub CMDREMOVEPATH_Click(sender As Object, e As EventArgs) Handles CMDREMOVEPATH.Click
        PBIMAGEPATH.Image = Nothing
        TXTPHOTOIMAGEUPLOADPATH.Clear()
    End Sub

    Private Sub CMDVIEWPATH_Click(sender As Object, e As EventArgs) Handles CMDVIEWPATH.Click
        Try
            If TXTPHOTOIMAGEUPLOADPATH.Text.Trim <> "" Then
                If Path.GetExtension(TXTPHOTOIMAGEUPLOADPATH.Text.Trim) = ".pdf" Then
                    System.Diagnostics.Process.Start(TXTPHOTOIMAGEUPLOADPATH.Text.Trim)
                Else
                    Dim objVIEW As New ViewImage
                    objVIEW.pbsoftcopy.Image = PBIMAGEPATH.Image
                    objVIEW.ShowDialog()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(sender As Object, e As EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then fillitemname(CMBITEMNAME, " And ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then itemvalidate(CMBITEMNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTSETMTRS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTSETMTRS.KeyPress
        numdotkeypress(e, TXTSETMTRS, Me)
    End Sub


End Class