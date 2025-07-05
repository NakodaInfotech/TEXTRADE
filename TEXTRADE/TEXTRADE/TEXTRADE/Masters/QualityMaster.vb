
Imports BL
Imports System.Windows.Forms
Imports System.Data
Imports System.IO


Public Class QualityMaster
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim IntResult As Integer
    Public EDIT As Boolean
    Public tempQualityName As String
    Public tempQualityId As Integer
    Public FRMSTRING As String = ""
    Dim RESPONSE As String = ""


    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            'IF CHANGES MADE HERE THEN MAKE SAME CHANGES IN FOLLOWING FORMS
            ' 1) TXTPRNO_Validating IN MATERIALRECEIPT
            ' 2) TXTCHNO_Validated IN STOCKRECO


            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList

            alParaval.Add(UCase(cmbQuality.Text.Trim))
            alParaval.Add(cmbprocessname.Text.Trim)
            alParaval.Add(cmbunit.Text.Trim)
            alParaval.Add(cmbitemname.Text.Trim)
            alParaval.Add(TXTREED.Text.Trim)
            alParaval.Add(TXTPICK.Text.Trim)
            alParaval.Add(TXTCOUNT.Text.Trim)
            alParaval.Add(TXTWIDTH.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(TXTWARP.Text.Trim)
            alParaval.Add(TXTWEFT.Text.Trim)
            alParaval.Add(TXTSELVEDGE.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)
            If PBPHOTO.Image IsNot Nothing Then
                Dim MS As New IO.MemoryStream
                PBPHOTO.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                alParaval.Add(MS.ToArray)
            Else
                alParaval.Add(DBNull.Value)
            End If

            Dim objclsProcessMaster As New ClsQualityMaster
            objclsProcessMaster.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objclsProcessMaster.save()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempQualityId)
                IntResult = objclsProcessMaster.Update()
                MsgBox("Details Updated")
                EDIT = False

            End If

            clear()
            cmbQuality.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Function CHECKDUPLICATE() As Boolean
        Try
            Dim BLN As Boolean = True
            pcase(cmbQuality)
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable
            If (EDIT = False) Or (EDIT = True And LCase(cmbQuality.Text) <> LCase(tempQualityName)) Then
                dt = objclscommon.search("QUALITY_name", "", "QUALITYMaster", " and QUALITY_name = '" & cmbQuality.Text.Trim & "'  And QUALITY_cmpid = " & CmpId & " And QUALITY_locationid = " & Locationid & " And QUALITY_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Quality Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                    BLN = False
                End If
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If cmbQuality.Text.Trim.Length = 0 Then
            Ep.SetError(cmbQuality, "Fill Process Name")
            bln = False
        End If
        If Not CHECKDUPLICATE() Then
            Ep.SetError(cmbQuality, "Quality Already Exists")
            bln = False
        End If

        Return bln
    End Function

    Sub clear()
        Try
            cmbQuality.Text = ""
            cmbunit.Text = ""
            cmbprocessname.Text = ""
            TXTREED.Clear()
            TXTPICK.Clear()
            TXTCOUNT.Clear()
            TXTWIDTH.Clear()
            txtremarks.Clear()
            TXTWARP.Clear()
            TXTWEFT.Clear()
            TXTSELVEDGE.Clear()
            PBPHOTO.Image = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbunit_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbunit.Enter
        Try
            If cmbunit.Text.Trim = "" Then fillunit(cmbunit)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbunit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbunit.Validating
        Try
            If cmbunit.Text.Trim <> "" Then unitvalidate(cmbunit, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub QualityMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Sub fillcmb()
        Try
            If cmbprocessname.Text.Trim = "" Then FILLPROCESS(cmbprocessname)
            If cmbQuality.Text.Trim = "" Then fillQUALITY(cmbQuality, EDIT)
            If cmbunit.Text.Trim = "" Then fillunit(cmbunit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ProcessMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillcmb()
            cmbQuality.Text = tempQualityName

            If EDIT = True Then

                Dim dttable As New DataTable
                Dim objCommon As New ClsCommonMaster

                dttable = objCommon.search(" QUALITYMASTER.QUALITY_id AS QUALITYID, QUALITYMASTER.QUALITY_name AS QUALITY, ISNULL(PROCESSMASTER.PROCESS_NAME, '') AS PROCESS, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, QUALITYMASTER.QUALITY_REED AS REED, QUALITYMASTER.QUALITY_PICK AS PICK, QUALITYMASTER.QUALITY_COUNT AS COUNT, QUALITYMASTER.QUALITY_WIDTH AS WIDTH, QUALITYMASTER.QUALITY_remarks AS REMARKS, QUALITYMASTER.QUALITY_WARP AS WARP, QUALITYMASTER.QUALITY_WEFT AS WEFT, QUALITYMASTER.QUALITY_SELVEDGE AS SELVEDGE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, QUALITYMASTER.QUALITY_PHOTO AS IMGPATH ", "", "  QUALITYMASTER LEFT OUTER JOIN PROCESSMASTER ON QUALITYMASTER.QUALITY_processid = PROCESSMASTER.PROCESS_ID AND QUALITYMASTER.QUALITY_cmpid = PROCESSMASTER.PROCESS_CMPID AND QUALITYMASTER.QUALITY_locationid = PROCESSMASTER.PROCESS_LOCATIONID AND QUALITYMASTER.QUALITY_yearid = PROCESSMASTER.PROCESS_YEARID LEFT OUTER JOIN UNITMASTER ON QUALITYMASTER.QUALITY_unitid = UNITMASTER.unit_id AND QUALITYMASTER.QUALITY_cmpid = UNITMASTER.unit_cmpid AND QUALITYMASTER.QUALITY_locationid = UNITMASTER.unit_locationid AND  QUALITYMASTER.QUALITY_yearid = UNITMASTER.unit_yearid LEFT OUTER JOIN ITEMMASTER ON QUALITYMASTER.QUALITY_itemid = ITEMMASTER.item_id AND QUALITYMASTER.QUALITY_cmpid = ITEMMASTER.item_cmpid AND QUALITYMASTER.QUALITY_locationid = ITEMMASTER.item_locationid AND  QUALITYMASTER.QUALITY_yearid = ITEMMASTER.item_yearid ", " and QUALITYMASTER.QUALITY_Name = '" & tempQualityName & "' and QUALITYMASTER.QUALITY_cmpid = " & CmpId & " and QUALITYMASTER.QUALITY_locationid = " & Locationid & " and QUALITYMASTER.QUALITY_yearid = " & YearId)
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If dttable.Rows.Count > 0 Then
                    For Each ITEM As DataRow In dttable.Rows

                        tempQualityId = ITEM("QUALITYID")

                        cmbprocessname.Text = ITEM("Process").ToString

                        cmbunit.Text = ITEM("UNIT").ToString
                        cmbQuality.Text = ITEM("QUALITY").ToString
                        cmbitemname.Text = ITEM("ITEMNAME").ToString

                        TXTREED.Text = ITEM("REED").ToString
                        TXTPICK.Text = ITEM("PICK").ToString
                        TXTCOUNT.Text = ITEM("COUNT").ToString
                        TXTWIDTH.Text = ITEM("WIDTH").ToString

                        txtremarks.Text = ITEM("REMARKS").ToString
                        TXTWARP.Text = ITEM("WARP").ToString
                        TXTWEFT.Text = ITEM("WEFT").ToString
                        TXTSELVEDGE.Text = ITEM("SELVEDGE").ToString
                        If IsDBNull(dttable.Rows(0).Item("IMGPATH")) = False Then
                            PBPHOTO.Image = Image.FromStream(New IO.MemoryStream(DirectCast(dttable.Rows(0).Item("IMGPATH"), Byte())))
                            TXTPHOTOIMGPATH.Text = dttable.Rows(0).Item("IMGPATH").ToString
                        Else
                            PBPHOTO.Image = Nothing
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub cmbProcessname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbprocessname.Enter
        Try
            If cmbprocessname.Text.Trim = "" Then FILLPROCESS(cmbprocessname)

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBQUALITYNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbQuality.Validating
        Try
            If cmbQuality.Text.Trim <> "" Then
                uppercase(cmbQuality)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                If (EDIT = False) Or (EDIT = True And LCase(cmbQuality.Text) <> LCase(tempQualityName)) Then
                    dt = objclscommon.search("QUALITY_name", "", "QUALITYMaster", " and QUALITY_name = '" & cmbQuality.Text.Trim & "'  And QUALITY_cmpid = " & CmpId & " And QUALITY_locationid = " & Locationid & " And QUALITY_yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Process Name Already Exists", MsgBoxStyle.Critical, "TEXTRADE")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbQUALITY_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuality.Enter
        Try
            If cmbQuality.Text.Trim = "" Then fillQUALITY(cmbQuality, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    'Private Sub cmbProcessname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbprocessname.Validating
    '    Try
    '        If cmbprocessname.Text.Trim <> "" Then PROCESSVALIDATE(cmbprocessname, e, Me)
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub

    Private Sub cmbitemname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitemname.Enter
        Try
            If cmbitemname.Text.Trim = "" Then fillitemname(cmbitemname, " AND ITEM_FRMSTRING='ITEM'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbitemname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbitemname.Validating
        Try
            If cmbitemname.Text.Trim <> "" Then itemvalidate(cmbitemname, e, Me, " AND ITEM_FRMSTRING='ITEM'", "ITEM")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTREED_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTREED.KeyPress
        'Try
        '    numdotkeypress(e, TXTREED, Me)
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub TXTPICK_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPICK.KeyPress
        'Try
        '    numdotkeypress(e, TXTPICK, Me)
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub TXTCOUNT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXTCOUNT.KeyDown
        Try
            If e.KeyCode = Keys.F1 And ClientName = "SVS" Then
                Dim OBJLEDGER As New SelectHSN
                OBJLEDGER.STRSEARCH = " AND HSN_TYPE='GOODS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then TXTCOUNT.Text = OBJLEDGER.TEMPCODE
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CMDPHOTOUPLOAD_Click(sender As Object, e As EventArgs) Handles CMDPHOTOUPLOAD.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpeg;*.png"
        OpenFileDialog1.ShowDialog()
        TXTPHOTOIMGPATH.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If TXTPHOTOIMGPATH.Text.Trim.Length <> 0 Then PBPHOTO.ImageLocation = TXTPHOTOIMGPATH.Text.Trim
    End Sub

    Private Sub CMDPHOTOREMOVE_Click(sender As Object, e As EventArgs) Handles CMDPHOTOREMOVE.Click
        Try
            PBPHOTO.Image = Nothing
            TXTPHOTOIMGPATH.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDWHATSAPP_Click(sender As Object, e As EventArgs) Handles CMDWHATSAPP.Click
        Try
            Dim DT As New DataTable
            Dim OBJBOOK As New ClsCommon
            errorvalid()
            If EDIT = True Or TXTPHOTOIMGPATH.Text.Trim = "" Then
                SENDWHATSAPP(tempQualityId)
            Else
                Exit Sub
            End If
            'DT = OBJBOOK.Execute_Any_String("UPDATE BOOKINGMASTER SET BOOKING_SENDWHATSAPP = 1 FROM BOOKINGMASTER INNER JOIN REGISTERMASTER On BOOKINGMASTER.BOOKING_REGID = REGISTERMASTER.register_id WHERE BOOKING_NO = '" & TEMPOFFICERNAME & "'  AND BOOKING_YEARID = " & YearId, "", "")
            'LBLWHATSAPP.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP(TEMPBOOKNO As Integer)

        Try
            If Not errorvalid() Then
                Exit Sub
            End If
            If MsgBox("Wish to Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If
            'FOR SENDING QRCODE

            Dim OBJWHATSAPP As New SendWhatsapp
            Dim OBJCMN As New ClsCommon
            'Dim DTIMG As DataTable = OBJCMN.SEARCH("CATALOG_PHOTO AS PHOTO", "", " CATALOGMASTER ", " AND CATALOG_NO = " & dtrow("CATALOGNO") & " AND CATALOG_YEARID = " & YearId)
            Dim DTIMG As DataTable = OBJCMN.SEARCH(" ISNULL(QUALITY_id, 0) AS QUALITYID, ISNULL(QUALITY_PHOTO, 0) AS PHOTO", "", " QUALITYMASTER ", " AND QUALITY_NAME = '" & cmbQuality.Text.Trim & "' AND QUALITY_YEARID = " & YearId)
            For Each DR As DataRow In DTIMG.Rows
                Dim _MemoryStream As New System.IO.MemoryStream()
                Dim _BinaryFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                _BinaryFormatter.Serialize(_MemoryStream, DR("PHOTO"))
                _MemoryStream.ToArray()
                File.WriteAllBytes(Application.StartupPath & "\" & cmbQuality.Text.Trim & YearId & ".jpeg”, DirectCast(DR("PHOTO"), Byte()))
                OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & cmbQuality.Text.Trim & YearId & ".jpeg”)
                OBJWHATSAPP.FILENAME.Add(cmbQuality.Text.Trim & YearId & ".jpeg”)
            Next
            OBJWHATSAPP.MSG = TXTSELVEDGE.Text.Trim
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCOUNT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCOUNT.KeyPress
        Try
            numdotkeypress(e, TXTCOUNT, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTWIDTH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTWIDTH.KeyPress
        Try
            numdotkeypress(e, TXTWIDTH, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub QualityMaster_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "SVS" Then
                TXTCOUNT.BackColor = Color.LemonChiffon
                LBLCOUNT.Text = "HSN Code"
                TXTCOUNT.ReadOnly = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class