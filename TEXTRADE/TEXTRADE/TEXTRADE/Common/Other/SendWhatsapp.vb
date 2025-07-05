
Imports System.ComponentModel
Imports Newtonsoft.Json.Linq
Imports BL
Imports System.IO
Imports HtmlAgilityPack
Imports System.Net

Public Class SendWhatsapp

    Public PARTYNAME As String = ""
    Public AGENTNAME As String = ""
    Public OTHERNAME1 As String = ""
    Public OTHERNAME2 As String = ""
    Public OTHERNAME3 As String = ""
    Public PATH As New ArrayList
    Public FILENAME As New ArrayList
    Dim RESPONSE As String = ""
    Public FRMSTRING As String = ""

    Public MSG As String = ""

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Public Sub SendWhatsapp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If CATALOGPATH <> "" Then RBPATH.Checked = True Else RBUPLOAD.Checked = True
            If FRMSTRING = "DIRECTWHATSAPP" Then FILLGRID()


            'IF AUTOCC IS TRUE THEN GET THE MOBILE NO FROM CMPMASTER AND SHOW IN AUTOCC
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If WHATSAPPAUTOCC = True Then
                DT = OBJCMN.SEARCH("ISNULL(CMP_TEL,'') AS MOBILENO", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
                If DT.Rows.Count > 0 Then TXTAUTOCC.Text = DT.Rows(0).Item("MOBILENO")
            End If

            TXTMESSAGE.Text = MSG

            FILLNAME(CMBNAME, False, "")
            FILLNAME(CMBAGENTNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
            FILLNAME(CMBOTHERNAME1, False, "")
            FILLNAME(CMBOTHERNAME2, False, "")
            FILLNAME(CMBOTHERNAME3, False, "")

            CMBNAME.Text = PARTYNAME
            CMBAGENTNAME.Text = AGENTNAME
            CMBOTHERNAME1.Text = OTHERNAME1
            CMBOTHERNAME2.Text = OTHERNAME2
            CMBOTHERNAME3.Text = OTHERNAME3

            'GETSALESMAN NO FOR KOTHARI
            If ClientName = "KOTHARI" Or ClientName = "KOTHARINEW" Then
                DT = OBJCMN.Execute_Any_String("SELECT ISNULL(SALESMAN_MOBILENO,'') AS MOBILENO FROM LEDGERS INNER JOIN SALESMANMASTER ON LEDGERS.ACC_SALESMANID = SALESMAN_ID WHERE LEDGERS.ACC_CMPNAME = '" & PARTYNAME & "' AND LEDGERS.ACC_YEARID = " & YearId, "", "")
                If DT.Rows.Count > 0 Then TXTOTHERNO2.Text = DT.Rows(0).Item("MOBILENO")
            End If

            If ClientName = "MAHAVIRPOLYCOT" Then AGENTNAME = ""

            Dim EN As New CancelEventArgs
            If PARTYNAME <> "" Then CMBNAME_Validating(CMBNAME, EN)
            If AGENTNAME <> "" Then CMBAGENTNAME_Validating(CMBAGENTNAME, EN)
            If OTHERNAME1 <> "" Then CMBOTHERNAME1_Validating(CMBOTHERNAME1, EN)
            If OTHERNAME2 <> "" Then CMBOTHERNAME2_Validating(CMBOTHERNAME2, EN)
            If OTHERNAME3 <> "" Then CMBOTHERNAME3_Validating(CMBOTHERNAME3, EN)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try

            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" CAST(0 AS BIT) AS CHK, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(GROUPMASTER.group_name, '') AS [GROUP], ISNULL(CITYMASTER.city_name, '') AS CITY,  ISNULL(AREAMASTER.area_name, '') AS AREA, ISNULL(LEDGERS.ACC_WHATSAPPNO, '') AS WHATSAPP, ISNULL(GROUPOFCOMPANIESMASTER.GOC_NAME, '') AS GRPCOM ", "", " GROUPMASTER RIGHT OUTER JOIN LEDGERS LEFT OUTER JOIN GROUPOFCOMPANIESMASTER ON LEDGERS.ACC_GOCID = GROUPOFCOMPANIESMASTER.GOC_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.Acc_areaid = AREAMASTER.area_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id ON GROUPMASTER.group_id = LEDGERS.Acc_groupid ", " AND GROUPMASTER.GROUP_SECONDARY IN ('SUNDRY CREDITORS', 'SUNDRY DEBTORS')  AND LEDGERS.ACC_WHATSAPPNO <> '' and acc_yearid = " & YearId)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If


            Dim JOINCLAUSE As String = ""
            Dim WHERECLAUSE As String = ""
            If CHKSTOCK.Checked = True Then
                'JOINCLAUSE = " INNER JOIN BARCODESTOCK ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = BARCODESTOCK.ITEMID AND ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = BARCODESTOCK.DESIGNID AND ITEMDESIGNIMAGE.ITEMDESIGN_COLORID = BARCODESTOCK.COLORID AND BARCODESTOCK.PIECETYPE = 'FRESH'"
                JOINCLAUSE = " INNER JOIN BARCODESTOCK ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = BARCODESTOCK.ITEMID AND ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = BARCODESTOCK.DESIGNID AND BARCODESTOCK.PIECETYPE = 'FRESH'"

                Dim OBJCMN As New ClsCommon
                Dim DTUNIT As DataTable = OBJCMN.SEARCH("UNIT_ABBR", "", "DEFAULTSTOCKUNIT", "")
                If DTUNIT.Rows.Count > 0 Then WHERECLAUSE = " AND BARCODESTOCK.UNIT IN (SELECT UNIT_ABBR FROM DEFAULTSTOCKUNIT)"

            End If

            'Dim DTDESIGN As DataTable = objclsCMST.search(" DISTINCT CAST(0 AS BIT) AS CHK, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ITEMDESIGNIMAGE.ITEMDESIGN_NO AS CATALOGNO ", "", " ITEMDESIGNIMAGE INNER JOIN ITEMMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = DESIGNMASTER.DESIGN_id " & JOINCLAUSE, " and ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = " & YearId)

            Dim DTDESIGN As New DataTable
            If RBUPLOAD.Checked = True Then
                DTDESIGN = objclsCMST.search(" DISTINCT CAST(0 AS BIT) AS CHK, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, '' AS COLOR, ITEMDESIGNIMAGE.ITEMDESIGN_NO AS CATALOGNO, ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_FILENAME,'') AS FILENAME ", "", " ITEMDESIGNIMAGE INNER JOIN ITEMMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = ITEMMASTER.item_id INNER JOIN DESIGNMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = DESIGNMASTER.DESIGN_id " & JOINCLAUSE, " AND ITEMDESIGNIMAGE.ITEMDESIGN_IMAGE1 IS NOT NULL and ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = " & YearId & WHERECLAUSE)
            Else
                DTDESIGN = objclsCMST.search(" DISTINCT CAST(0 AS BIT) AS CHK, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, '' AS COLOR, ITEMDESIGNIMAGE.ITEMDESIGN_NO AS CATALOGNO, ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_FILENAME,'') AS FILENAME ", "", " ITEMDESIGNIMAGE INNER JOIN ITEMMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = ITEMMASTER.item_id INNER JOIN DESIGNMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = DESIGNMASTER.DESIGN_id " & JOINCLAUSE, " AND ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_FILENAME,'') <> '' and ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = " & YearId & WHERECLAUSE)
            End If
            GRIDDESIGNDETAILS.DataSource = DTDESIGN
            If DTDESIGN.Rows.Count > 0 Then
                GRIDDESIGN.FocusedRowHandle = GRIDDESIGN.RowCount - 1
                GRIDDESIGN.TopRowIndex = GRIDDESIGN.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBNAME, CMBCODE, e, Me, TXTADD, " ", "SUNDRY DEBTORS", "ACCOUNTS", "", "", TXTPARTYNO.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENTNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBAGENTNAME.Validating
        Try
            If CMBAGENTNAME.Text.Trim <> "" Then NAMEVALIDATE(CMBAGENTNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", TXTAGENTNO.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOTHERNAME1_Validating(sender As Object, e As CancelEventArgs) Handles CMBOTHERNAME1.Validating
        Try
            If CMBOTHERNAME1.Text.Trim <> "" Then NAMEVALIDATE(CMBOTHERNAME1, CMBCODE, e, Me, TXTADD, "", "SUNDRY DEBTORS", "ACCOUNTS", "", "", TXTOTHERNO1.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOTHERNAME2_Validating(sender As Object, e As CancelEventArgs) Handles CMBOTHERNAME2.Validating
        Try
            If CMBOTHERNAME2.Text.Trim <> "" Then NAMEVALIDATE(CMBOTHERNAME2, CMBCODE, e, Me, TXTADD, "", "SUNDRY DEBTORS", "ACCOUNTS", "", "", TXTOTHERNO2.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOTHERNAME3_Validating(sender As Object, e As CancelEventArgs) Handles CMBOTHERNAME3.Validating
        Try
            If CMBOTHERNAME3.Text.Trim <> "" Then NAMEVALIDATE(CMBOTHERNAME3, CMBCODE, e, Me, TXTADD, "", "SUNDRY DEBTORS", "ACCOUNTS", "", "", TXTOTHERNO3.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function CheckAddress(IMAGEURL As String) As Boolean
        Try
            Dim URL As String = (IMAGEURL)
            Dim request As WebRequest = WebRequest.Create(URL)
            Dim response As WebResponse = request.GetResponse()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


    Public Async Sub CMDSEND_Click(sender As Object, e As EventArgs) Handles CMDSEND.Click
        Try

            'FIRST CHECK STATUS OF MOBILE CONNECTION
            Dim CONNECTSTATUS As String = JObject.Parse(Await CHECKMOBILECONNECTSTATUS())("success")
            If CONNECTSTATUS = "False" Then
                MsgBox("Mobile Not connected, Please Check Connection", MsgBoxStyle.Critical)
                Exit Sub
            End If





            'TESTING CODE

            ''WEB CLIENT IS NEEDED TO DO THE DOWNLOAD
            'Dim MyWebClient As New System.Net.WebClient

            ''BYTE ARRAY HOLDS THE DATA
            'Dim ImageInBytes() As Byte = MyWebClient.DownloadData("http://122.179.159.186:8142/TEXTRADE/IMAGES/1.JPEG")

            ''CREATE A BITMAP FROM A STREAM (STREAM IS CREATED FROM THE DOWNLOADED BYTES)
            'Dim MyImage As New Bitmap(New IO.MemoryStream(ImageInBytes))

            ''SAVE THE IMAGE TO WHATEVER FILE NAME YOU WANT
            ''PATH(1) = Application.StartupPath & "\IMAGES\1.JPEG"
            'MyImage.Save(Application.StartupPath & "\IMAGES\1.JPEG")


            'Dim mainUrl As String = "http://122.179.159.186:8142/TEXTRADE/IMAGES/"
            'Dim doc As HtmlDocument
            'doc = New HtmlDocument()
            'Dim sourceString As String = New System.Net.WebClient().DownloadString(mainUrl)
            'doc.LoadHtml(sourceString)
            'For Each link As HtmlNode In doc.DocumentNode.SelectNodes("//img")
            '    Dim linkAddress = GetAbsoluteUrl(link.Attributes("src").Value, mainUrl)
            '    Dim MyWebClient As New System.Net.WebClient
            '    Dim ImageInBytes() As Byte = MyWebClient.DownloadData(linkAddress)
            '    Dim MyImage As New Bitmap(New IO.MemoryStream(ImageInBytes))
            '    MyImage.Save(Application.StartupPath & "\IMAGES\" & linkAddress)
            '    RESPONSE = Await SENDWHATSAPPATTACHMENT("919987603607", linkAddress, "1.JPEG")
            '    ERRORMESSAGE(TXTPARTYNO.Text)
            '    Console.WriteLine("Image: {0}", linkAddress)
            'Next


            'FOR SENDING IMAGES
            '            If FRMSTRING = "DIRECTWHATSAPP" Then
            '                GRIDDESIGN.ClearColumnsFilter()
            '                For i As Integer = 0 To GRIDDESIGN.RowCount - 1
            '                    Dim dtrow As DataRow = GRIDDESIGN.GetDataRow(i)
            '                    If Convert.ToBoolean(dtrow("CHK")) = True Then
            '                        Dim OBJCMN As New ClsCommon
            '                        'Dim DTIMG As DataTable = OBJCMN.SEARCH("CATALOG_PHOTO AS PHOTO", "", " CATALOGMASTER ", " AND CATALOG_NO = " & dtrow("CATALOGNO") & " AND CATALOG_YEARID = " & YearId)
            '                        Dim DTIMG As DataTable = OBJCMN.SEARCH("ITEMDESIGN_FILENAME AS FILENAME", "", " ITEMDESIGNIMAGE ", " AND ISNULL(ITEMDESIGN_FILENAME,'')<>'' AND ITEMDESIGN_NO = " & dtrow("CATALOGNO") & " AND ITEMDESIGN_YEARID = " & YearId)
            '                        For Each DR As DataRow In DTIMG.Rows
            '                            'Dim MyWebClient As New System.Net.WebClient
            '                            'Dim ImageInBytes() As Byte = MyWebClient.DownloadData("http://122.179.159.186:8142/TEXTRADE/IMAGES/" & DR("FILENAME") & ".jpg")
            '                            'Dim MyImage As New Bitmap(New IO.MemoryStream(ImageInBytes))
            '                            'MyImage.Save(Application.StartupPath & "\IMAGES\" & DR("FILENAME") & ".jpg")

            '                            Dim TEMPPATH As String = CATALOGPATH & "\" & DR("FILENAME")
            '                            Dim TEMPFILENAME As String = DR("FILENAME")

            '                            'CHECK WHETHER FILE IS PRESENT IN LOCATION OR NOT IF NOT THE SKIP
            '                            If File.Exists(TEMPPATH) = False Then GoTo SKIPLINE

            '                            RESPONSE = Await SENDWHATSAPPATTACHMENT("919987603607", TEMPPATH, TEMPFILENAME)
            '                            ERRORMESSAGE(TXTPARTYNO.Text)


            '                            'Dim _MemoryStream As New System.IO.MemoryStream()
            '                            'Dim _BinaryFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            '                            '_BinaryFormatter.Serialize(_MemoryStream, DR("PHOTO"))
            '                            '_MemoryStream.ToArray()
            '                            'File.WriteAllBytes(Application.StartupPath & “\" & dtrow("ITEMNAME") & dtrow("CATALOGNO") & YearId & ".jpeg”, DirectCast(DR("PHOTO"), Byte()))
            '                            'PATH.Add(Application.StartupPath & “\" & dtrow("ITEMNAME") & dtrow("CATALOGNO") & YearId & ".jpeg”)
            '                            'FILENAME.Add(dtrow("ITEMNAME") & dtrow("CATALOGNO") & YearId & ".jpeg”)
            'SKIPLINE:
            '                        Next
            '                    End If
            '                Next
            '            End If




            'CODE ENDS



            'FOR SENDING IMAGES
            If FRMSTRING = "DIRECTWHATSAPP" Then
                GRIDDESIGN.ClearColumnsFilter()
                For i As Integer = 0 To GRIDDESIGN.RowCount - 1
                    Dim dtrow As DataRow = GRIDDESIGN.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then

                        If RBUPLOAD.Checked = True Then
                            Dim OBJCMN As New ClsCommon
                            Dim DTIMG As DataTable = OBJCMN.SEARCH("ITEMDESIGN_IMAGE1 AS PHOTO", "", " ITEMDESIGNIMAGE ", " AND ITEMDESIGN_IMAGE1 IS NOT NULL AND ITEMDESIGN_NO = " & dtrow("CATALOGNO") & " AND ITEMDESIGN_YEARID = " & YearId)
                            For Each DR As DataRow In DTIMG.Rows
                                Dim _MemoryStream As New System.IO.MemoryStream()
                                Dim _BinaryFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                                _BinaryFormatter.Serialize(_MemoryStream, DR("PHOTO"))
                                _MemoryStream.ToArray()
                                File.WriteAllBytes(Application.StartupPath & “\" & dtrow("ITEMNAME") & dtrow("CATALOGNO") & YearId & ".jpeg”, DirectCast(DR("PHOTO"), Byte()))
                                PATH.Add(Application.StartupPath & “\" & dtrow("ITEMNAME") & dtrow("CATALOGNO") & YearId & ".jpeg”)
                                FILENAME.Add(dtrow("ITEMNAME") & dtrow("CATALOGNO") & YearId & ".jpeg”)
                            Next

                        Else

                            'IF DRIVE IS NOT PRESENT IN LOCAL MACHINE THEN WE NEED TO DOWNLOAD THE FILE FROM IP
                            If My.Computer.FileSystem.DirectoryExists(CATALOGPATH) = False Then
                                Dim MyWebClient As New System.Net.WebClient
                                'If Not (CheckAddress(CATALOGIP & dtrow("FILENAME"))) Then GoTo NEXTLINE

                                Dim ImageInBytes() As Byte = MyWebClient.DownloadData(CATALOGIP & dtrow("FILENAME"))
                                Dim MyImage As New Bitmap(New IO.MemoryStream(ImageInBytes))

                                'CHECK WHETHER IMAGES FOLDER IS PRESENT OR NOT, THEN CREATE NEW IMAGES
                                If IO.Directory.Exists(Application.StartupPath & "\IMAGES") = False Then IO.Directory.CreateDirectory(Application.StartupPath & "\IMAGES")
                                MyImage.Save(Application.StartupPath & "\IMAGES\" & dtrow("FILENAME"))
                                PATH.Add(Application.StartupPath & “\IMAGES\" & dtrow("FILENAME"))
                                FILENAME.Add(dtrow("FILENAME"))
                                MyWebClient.Dispose()
                                MyImage.Dispose()
                            Else
                                'CHECK WHETHER FILE IS PRESENT IN LOCATION OR NOT IF NOT THE SKIP
                                If File.Exists(CATALOGPATH & "\" & dtrow("FILENAME")) = True Then
                                    PATH.Add(CATALOGPATH & "\" & dtrow("FILENAME"))
                                    FILENAME.Add(dtrow("FILENAME"))
                                End If

                            End If
NEXTLINE:
                        End If

                    End If
                Next
            End If


            If PATH.Count = 0 Then Exit Sub


            For I As Integer = 0 To PATH.Count - 1
                'If TXTPARTYNO.Text.Trim <> "" Then
                '    RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTPARTYNO.Text.Trim, PATH(I), FILENAME(I))
                '    ERRORMESSAGE(TXTPARTYNO.Text)
                'End If
                Dim strArray() As String
                strArray = Split(TXTPARTYNO.Text.Trim, ";")

                For J As Integer = 0 To strArray.Count - 1
                    If TXTPARTYNO.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(J), PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTPARTYNO.Text)
                    End If
                Next
                'If TXTAGENTNO.Text.Trim <> "" Then
                '    RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTAGENTNO.Text.Trim, PATH(I), FILENAME(I))
                '    ERRORMESSAGE(TXTAGENTNO.Text)
                'End If
                strArray = Split(TXTAGENTNO.Text.Trim, ";")
                For K As Integer = 0 To strArray.Count - 1
                    If TXTAGENTNO.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(K), PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTAGENTNO.Text)
                    End If
                Next
                'If TXTOTHERNO1.Text.Trim <> "" Then
                '    RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTOTHERNO1.Text.Trim, PATH(I), FILENAME(I))
                '    ERRORMESSAGE(TXTOTHERNO1.Text)
                'End If
                strArray = Split(TXTOTHERNO1.Text.Trim, ";")

                For L As Integer = 0 To strArray.Count - 1
                    If TXTOTHERNO1.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(L), PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTOTHERNO1.Text)
                    End If
                Next
                'If TXTOTHERNO2.Text.Trim <> "" Then
                '    RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTOTHERNO2.Text.Trim, PATH(I), FILENAME(I))
                '    ERRORMESSAGE(TXTOTHERNO2.Text)
                'End If
                strArray = Split(TXTOTHERNO2.Text.Trim, ";")

                For M As Integer = 0 To strArray.Count - 1
                    If TXTOTHERNO2.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(M), PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTOTHERNO2.Text)
                    End If
                Next
                'If TXTOTHERNO3.Text.Trim <> "" Then
                '    RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTOTHERNO3.Text.Trim, PATH(I), FILENAME(I))
                '    ERRORMESSAGE(TXTOTHERNO3.Text)
                'End If
                strArray = Split(TXTOTHERNO3.Text.Trim, ";")

                For N As Integer = 0 To strArray.Count - 1
                    If TXTOTHERNO3.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(N), PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTOTHERNO3.Text)
                    End If
                Next

                If TXTAUTOCC.Text.Trim <> "" Then
                    RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTAUTOCC.Text.Trim, PATH(I), FILENAME(I))
                    ERRORMESSAGE(TXTAUTOCC.Text)
                End If


                'SENDING WHATSAPP TO MULTIPLE LEDGERS SELECTED
                If FRMSTRING = "DIRECTWHATSAPP" Then
                    gridbill.ClearColumnsFilter()
                    For J As Integer = 0 To gridbill.RowCount - 1
                        Dim dtrow As DataRow = gridbill.GetDataRow(J)
                        If Convert.ToBoolean(dtrow("CHK")) = True Then
                            RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & dtrow("WHATSAPP"), PATH(I), FILENAME(I))
                            ERRORMESSAGE(dtrow("WHATSAPP"))
                        End If
                    Next
                End If
            Next


            'DELETE ALL THE FILES IN PATH ARRAY
            If FRMSTRING = "DIRECTWHATSAPP" Then
                For I As Integer = 0 To PATH.Count - 1
                    If File.Exists(PATH(I)) Then File.Delete(PATH(I))
                Next
            End If


            'TEXT MESSAGE SHOULD BE SEND ONLY ONCE
            If TXTMESSAGE.Text.Trim <> "" Then
                If TXTPARTYNO.Text.Trim <> "" Then Await SENDWHATSAPPMESSAGE("91" & TXTPARTYNO.Text.Trim, TXTMESSAGE.Text.Trim)
                If TXTAGENTNO.Text.Trim <> "" Then Await SENDWHATSAPPMESSAGE("91" & TXTAGENTNO.Text.Trim, TXTMESSAGE.Text.Trim)
                If TXTOTHERNO1.Text.Trim <> "" Then Await SENDWHATSAPPMESSAGE("91" & TXTOTHERNO1.Text.Trim, TXTMESSAGE.Text.Trim)
                If TXTOTHERNO2.Text.Trim <> "" Then Await SENDWHATSAPPMESSAGE("91" & TXTOTHERNO2.Text.Trim, TXTMESSAGE.Text.Trim)
                If TXTOTHERNO3.Text.Trim <> "" Then Await SENDWHATSAPPMESSAGE("91" & TXTOTHERNO3.Text.Trim, TXTMESSAGE.Text.Trim)
                If TXTAUTOCC.Text.Trim <> "" Then Await SENDWHATSAPPMESSAGE("91" & TXTAUTOCC.Text.Trim, TXTMESSAGE.Text.Trim)

                'SENDING WHATSAPP TO MULTIPLE LEDGERS SELECTED
                If FRMSTRING = "DIRECTWHATSAPP" Then
                    gridbill.ClearColumnsFilter()
                    For J As Integer = 0 To gridbill.RowCount - 1
                        Dim dtrow As DataRow = gridbill.GetDataRow(J)
                        If Convert.ToBoolean(dtrow("CHK")) = True Then
                            RESPONSE = Await SENDWHATSAPPMESSAGE("91" & dtrow("WHATSAPP"), TXTMESSAGE.Text.Trim)
                        End If
                    Next
                End If
            End If


            MsgBox("Message Sent", MsgBoxStyle.Information)
            'Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub ERRORMESSAGE(NO As String)
        Try
            If RESPONSE <> "" Then
                Dim STATUS As String = JObject.Parse(RESPONSE)("success")
                Dim ERRORMSG As String = JObject.Parse(RESPONSE)("message")
                If STATUS = "False" Then MsgBox("Error While Sending Msg to " & NO & ", Error - " & ERRORMSG & ", Response - " & RESPONSE)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SendWhatsapp_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If FRMSTRING = "DIRECTWHATSAPP" Then TabControl1.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECTALL.CheckedChanged
        Try
            If gridbilldetails.Visible = True Then
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CHK") = CHKSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKDESIGNSELECTALL_CheckedChanged(sender As Object, e As EventArgs) Handles CHKDESIGNSELECTALL.CheckedChanged
        Try
            If GRIDDESIGNDETAILS.Visible = True Then
                For i As Integer = 0 To GRIDDESIGN.RowCount - 1
                    Dim dtrow As DataRow = GRIDDESIGN.GetDataRow(i)
                    dtrow("CHK") = CHKDESIGNSELECTALL.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class