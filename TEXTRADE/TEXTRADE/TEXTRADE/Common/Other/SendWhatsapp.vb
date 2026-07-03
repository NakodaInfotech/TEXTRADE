
Imports System.ComponentModel
Imports Newtonsoft.Json.Linq
Imports BL
Imports System.IO
Imports HtmlAgilityPack
Imports System.Net
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class SendWhatsapp

    Public PARTYNAME As String = ""
    Public AGENTNAME As String = ""
    Public OTHERNAME1 As String = ""
    Public OTHERNAME2 As String = ""
    Public OTHERNAME3 As String = ""
    Public SALESMAN As String = ""
    Public PATH As New ArrayList
    Public FILENAME As New ArrayList
    Dim RESPONSE As String = ""
    Public FRMSTRING As String = ""
    Public CAPTION As New ArrayList
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
            FILLSALESMAN(CMBSALESMAN)

            CMBNAME.Text = PARTYNAME
            CMBAGENTNAME.Text = AGENTNAME
            CMBOTHERNAME1.Text = OTHERNAME1
            CMBOTHERNAME2.Text = OTHERNAME2
            CMBOTHERNAME3.Text = OTHERNAME3
            CMBSALESMAN.Text = SALESMAN

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
            If SALESMAN <> "" Then CMBSALESMAN_Validating(SALESMAN, EN)

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
            Dim STOCKSELECT As String = ""
            Dim GROUPBY As String = ""        ' ← EMPTY BY DEFAULT
            'If CHKSTOCK.Checked = True Then
            '    'JOINCLAUSE = " INNER JOIN BARCODESTOCK ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = BARCODESTOCK.ITEMID AND ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = BARCODESTOCK.DESIGNID AND ITEMDESIGNIMAGE.ITEMDESIGN_COLORID = BARCODESTOCK.COLORID AND BARCODESTOCK.PIECETYPE = 'FRESH'"
            '    JOINCLAUSE = " INNER JOIN BARCODESTOCK ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = BARCODESTOCK.ITEMID AND ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = BARCODESTOCK.DESIGNID AND BARCODESTOCK.PIECETYPE = 'FRESH'"

            '    Dim OBJCMN As New ClsCommon
            '    Dim DTUNIT As DataTable = OBJCMN.SEARCH("UNIT_ABBR", "", "DEFAULTSTOCKUNIT", "")
            '    If DTUNIT.Rows.Count > 0 Then WHERECLAUSE = " AND BARCODESTOCK.UNIT IN (SELECT UNIT_ABBR FROM DEFAULTSTOCKUNIT)"

            '    STOCKSELECT = ", ISNULL(SUM(BARCODESTOCK.MTRS),0) AS STOCKMTR"   ' ← ADD THIS INSIDE IF BLOCK ONLY
            '    GROUPBY = " GROUP BY ITEMMASTER.item_name, DESIGNMASTER.DESIGN_NO, ITEMDESIGNIMAGE.ITEMDESIGN_NO, ITEMDESIGNIMAGE.ITEMDESIGN_FILENAME"  ' ← ONLY WHEN CHECKED
            'End If
            If CHKSTOCK.Checked = True Then
                ' THIS RUNS FOR ALL CLIENTS - filters stock items
                JOINCLAUSE = " INNER JOIN BARCODESTOCK ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = BARCODESTOCK.ITEMID AND ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = BARCODESTOCK.DESIGNID AND BARCODESTOCK.PIECETYPE = 'FRESH'"

                Dim OBJCMN As New ClsCommon
                Dim DTUNIT As DataTable = OBJCMN.SEARCH("UNIT_ABBR", "", "DEFAULTSTOCKUNIT", "")
                If DTUNIT.Rows.Count > 0 Then WHERECLAUSE = " AND BARCODESTOCK.UNIT IN (SELECT UNIT_ABBR FROM DEFAULTSTOCKUNIT)"

                ' THIS RUNS ONLY FOR SHEETAL - adds stock MTR column and GROUP BY
                If ClientName = "SHEETAL" Then
                    STOCKSELECT = ", ISNULL(SUM(BARCODESTOCK.MTRS),0) AS STOCKMTR"
                    GROUPBY = " GROUP BY ITEMMASTER.item_name, DESIGNMASTER.DESIGN_NO, ITEMDESIGNIMAGE.ITEMDESIGN_NO, ITEMDESIGNIMAGE.ITEMDESIGN_FILENAME"
                End If
            End If

            'Dim DTDESIGN As DataTable = objclsCMST.search(" DISTINCT CAST(0 AS BIT) AS CHK, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ITEMDESIGNIMAGE.ITEMDESIGN_NO AS CATALOGNO ", "", " ITEMDESIGNIMAGE INNER JOIN ITEMMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN COLORMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = DESIGNMASTER.DESIGN_id " & JOINCLAUSE, " and ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = " & YearId)

            Dim DTDESIGN As New DataTable
            If RBUPLOAD.Checked = True Then
                DTDESIGN = objclsCMST.search(" DISTINCT CAST(0 AS BIT) AS CHK, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, '' AS COLOR, ITEMDESIGNIMAGE.ITEMDESIGN_NO AS CATALOGNO, ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_FILENAME,'') AS FILENAME" & STOCKSELECT, "", " ITEMDESIGNIMAGE INNER JOIN ITEMMASTER On ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = ITEMMASTER.item_id INNER JOIN DESIGNMASTER On ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = DESIGNMASTER.DESIGN_id " & JOINCLAUSE, " And ITEMDESIGNIMAGE.ITEMDESIGN_IMAGE1 Is Not NULL And ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = " & YearId & WHERECLAUSE & " GROUP BY ITEMMASTER.item_name, DesignMaster.DESIGN_NO, ItemDesignImage.ITEMDESIGN_NO, ItemDesignImage.ITEMDESIGN_FILENAME")
            Else
                DTDESIGN = objclsCMST.search(" DISTINCT CAST(0 As BIT) As CHK, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, '' AS COLOR, ITEMDESIGNIMAGE.ITEMDESIGN_NO AS CATALOGNO, ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_FILENAME,'') AS FILENAME" & STOCKSELECT, "", " ITEMDESIGNIMAGE INNER JOIN ITEMMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_ITEMID = ITEMMASTER.item_id INNER JOIN DESIGNMASTER ON ITEMDESIGNIMAGE.ITEMDESIGN_DESIGNID = DESIGNMASTER.DESIGN_id " & JOINCLAUSE, " AND ISNULL(ITEMDESIGNIMAGE.ITEMDESIGN_FILENAME,'') <> '' and ITEMDESIGNIMAGE.ITEMDESIGN_YEARID = " & YearId & WHERECLAUSE & " GROUP BY ITEMMASTER.item_name, DESIGNMASTER.DESIGN_NO, ITEMDESIGNIMAGE.ITEMDESIGN_NO, ITEMDESIGNIMAGE.ITEMDESIGN_FILENAME")
            End If
            GRIDDESIGNDETAILS.DataSource = DTDESIGN
            If DTDESIGN.Rows.Count > 0 Then
                GRIDDESIGN.FocusedRowHandle = GRIDDESIGN.RowCount - 1
                GRIDDESIGN.TopRowIndex = GRIDDESIGN.RowCount - 15
            End If
            TXTFROM.Clear()
            TXTTO.Clear()
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

    Private Sub CMBSALESMAN_Validating(sender As Object, e As CancelEventArgs) Handles CMBSALESMAN.Validating
        Try
            If CMBSALESMAN.Text.Trim <> "" Then SALESMANVALIDATE(CMBSALESMAN, e, Me, TXTSALESMANNO.Text)
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


            'FOR SENDING IMAGES
            If FRMSTRING = "DIRECTWHATSAPP" Then
                GRIDDESIGN.ClearColumnsFilter()
                For i As Integer = 0 To GRIDDESIGN.RowCount - 1
                    Dim dtrow As DataRow = GRIDDESIGN.GetDataRow(i)
                    If Convert.ToBoolean(dtrow("CHK")) = True Then

                        Dim STOCKTEXT As String = ""
                        If CHKSTOCK.Checked = True AndAlso ClientName = "SHEETAL" Then
                            STOCKTEXT = " | Stock: " & dtrow("STOCKMTR") & " Mtrs"
                        End If


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
                                CAPTION.Add("Design: " & dtrow("DESIGNNO") & " | Stock: " & dtrow("STOCKMTR") & " Mtrs")
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
                                CAPTION.Add("Design: " & dtrow("DESIGNNO") & " | Stock: " & dtrow("STOCKMTR") & " Mtrs")
                                MyWebClient.Dispose()
                                MyImage.Dispose()
                            Else
                                'CHECK WHETHER FILE IS PRESENT IN LOCATION OR NOT IF NOT THE SKIP
                                If File.Exists(CATALOGPATH & "\" & dtrow("FILENAME")) = True Then
                                    PATH.Add(CATALOGPATH & "\" & dtrow("FILENAME"))
                                    FILENAME.Add(dtrow("FILENAME"))
                                    CAPTION.Add("Design: " & dtrow("DESIGNNO") & STOCKTEXT)
                                End If

                            End If
NEXTLINE:
                        End If

                    End If
                Next
            End If


            If PATH.Count = 0 Then Exit Sub
            Dim strArray() As String


            For I As Integer = 0 To PATH.Count - 1
                strArray = Split(TXTPARTYNO.Text.Trim, ";")

                For J As Integer = 0 To strArray.Count - 1
                    If TXTPARTYNO.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(J), PATH(I), FILENAME(I), CAPTION(I))
                        ERRORMESSAGE(TXTPARTYNO.Text)
                    End If
                Next

                strArray = Split(TXTAGENTNO.Text.Trim, ";")
                For K As Integer = 0 To strArray.Count - 1
                    If TXTAGENTNO.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(K), PATH(I), FILENAME(I), CAPTION(I))
                        ERRORMESSAGE(TXTAGENTNO.Text)
                    End If
                Next

                strArray = Split(TXTOTHERNO1.Text.Trim, ";")
                For L As Integer = 0 To strArray.Count - 1
                    If TXTOTHERNO1.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(L), PATH(I), FILENAME(I), CAPTION(I))
                        ERRORMESSAGE(TXTOTHERNO1.Text)
                    End If
                Next

                strArray = Split(TXTOTHERNO2.Text.Trim, ";")
                For M As Integer = 0 To strArray.Count - 1
                    If TXTOTHERNO2.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(M), PATH(I), FILENAME(I), CAPTION(I))
                        ERRORMESSAGE(TXTOTHERNO2.Text)
                    End If
                Next

                strArray = Split(TXTOTHERNO3.Text.Trim, ";")
                For N As Integer = 0 To strArray.Count - 1
                    If TXTOTHERNO3.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(N), PATH(I), FILENAME(I), CAPTION(I))
                        ERRORMESSAGE(TXTOTHERNO3.Text)
                    End If
                Next

                strArray = Split(TXTSALESMANNO.Text.Trim, ";")
                For N As Integer = 0 To strArray.Count - 1
                    If TXTSALESMANNO.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(N), PATH(I), FILENAME(I), CAPTION(I))
                        ERRORMESSAGE(TXTSALESMANNO.Text)
                    End If
                Next

                If TXTAUTOCC.Text.Trim <> "" Then
                    RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTAUTOCC.Text.Trim, PATH(I), FILENAME(I), CAPTION(I))
                    ERRORMESSAGE(TXTAUTOCC.Text)
                End If


                'SENDING WHATSAPP TO MULTIPLE LEDGERS SELECTED
                If FRMSTRING = "DIRECTWHATSAPP" Then
                    gridbill.ClearColumnsFilter()
                    For J As Integer = 0 To gridbill.RowCount - 1
                        Dim dtrow As DataRow = gridbill.GetDataRow(J)
                        If Convert.ToBoolean(dtrow("CHK")) = True Then
                            strArray = Split(dtrow("WHATSAPP"), ";")

                            For M As Integer = 0 To strArray.Count - 1
                                If dtrow("WHATSAPP") <> "" Then
                                    RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & strArray(M), PATH(I), FILENAME(I), CAPTION(I))
                                    ERRORMESSAGE(dtrow("WHATSAPP"))
                                End If
                            Next
                        End If
                    Next
                End If
            Next




            'TEXT MESSAGE SHOULD BE SEND ONLY ONCE
            If TXTMESSAGE.Text.Trim <> "" Then
                If TXTPARTYNO.Text.Trim <> "" Then
                    strArray = Split(TXTPARTYNO.Text.Trim, ";")
                    For N As Integer = 0 To strArray.Count - 1
                        Await SENDWHATSAPPMESSAGE("91" & strArray(N), TXTMESSAGE.Text.Trim)
                    Next
                End If
                If TXTAGENTNO.Text.Trim <> "" Then
                    strArray = Split(TXTAGENTNO.Text.Trim, ";")
                    For N As Integer = 0 To strArray.Count - 1
                        Await SENDWHATSAPPMESSAGE("91" & strArray(N), TXTMESSAGE.Text.Trim)
                    Next
                End If
                If TXTOTHERNO1.Text.Trim <> "" Then
                    strArray = Split(TXTOTHERNO1.Text.Trim, ";")
                    For N As Integer = 0 To strArray.Count - 1
                        Await SENDWHATSAPPMESSAGE("91" & strArray(N), TXTMESSAGE.Text.Trim)
                    Next
                End If
                If TXTOTHERNO2.Text.Trim <> "" Then
                    strArray = Split(TXTOTHERNO2.Text.Trim, ";")
                    For N As Integer = 0 To strArray.Count - 1
                        Await SENDWHATSAPPMESSAGE("91" & strArray(N), TXTMESSAGE.Text.Trim)
                    Next
                End If
                If TXTOTHERNO3.Text.Trim <> "" Then
                    strArray = Split(TXTOTHERNO3.Text.Trim, ";")
                    For N As Integer = 0 To strArray.Count - 1
                        Await SENDWHATSAPPMESSAGE("91" & strArray(N), TXTMESSAGE.Text.Trim)
                    Next
                End If
                If TXTSALESMANNO.Text.Trim <> "" Then
                    strArray = Split(TXTSALESMANNO.Text.Trim, ";")
                    For N As Integer = 0 To strArray.Count - 1
                        Await SENDWHATSAPPMESSAGE("91" & strArray(N), TXTMESSAGE.Text.Trim)
                    Next
                End If
                If TXTAUTOCC.Text.Trim <> "" Then
                    strArray = Split(TXTAUTOCC.Text.Trim, ";")
                    For N As Integer = 0 To strArray.Count - 1
                        Await SENDWHATSAPPMESSAGE("91" & strArray(N), TXTMESSAGE.Text.Trim)
                    Next
                End If

                'SENDING WHATSAPP TO MULTIPLE LEDGERS SELECTED
                If FRMSTRING = "DIRECTWHATSAPP" Then
                    gridbill.ClearColumnsFilter()
                    For J As Integer = 0 To gridbill.RowCount - 1
                        Dim dtrow As DataRow = gridbill.GetDataRow(J)
                        If Convert.ToBoolean(dtrow("CHK")) = True Then
                            strArray = Split(dtrow("WHATSAPP").Text.Trim, ";")
                            For N As Integer = 0 To strArray.Count - 1
                                RESPONSE = Await SENDWHATSAPPMESSAGE("91" & strArray(N), TXTMESSAGE.Text.Trim)
                            Next
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

    Private Sub GRIDDESIGN_CustomColumnDisplayText(
        sender As Object,
        e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs
    ) Handles GRIDDESIGN.CustomColumnDisplayText

        Dim view As GridView = CType(sender, GridView)

        If e.Column.FieldName = "SRNO" AndAlso e.ListSourceRowIndex >= 0 Then
            Dim rowHandle As Integer = view.GetRowHandle(e.ListSourceRowIndex)
            If rowHandle >= 0 Then
                Dim visibleIndex As Integer = view.GetVisibleIndex(rowHandle)
                e.DisplayText = (visibleIndex + 1).ToString()
            End If
        End If
    End Sub

    Private Sub TXTTO_Validated(sender As Object, e As EventArgs) Handles TXTTO.Validated
        Try
            ApplyRangeSelection()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ApplyRangeSelection()
        Dim fromNo As Integer
        Dim toNo As Integer

        ' Validate numbers
        If Not Integer.TryParse(TXTFROM.Text, fromNo) Then Exit Sub
        If Not Integer.TryParse(TXTTO.Text, toNo) Then Exit Sub

        ' Swap if user wrote From > To
        If fromNo > toNo Then
            Dim tmp = fromNo
            fromNo = toNo
            toNo = tmp
        End If

        Dim view As GridView = GRIDDESIGN

        view.BeginUpdate()
        Try
            ' Sab row ke checkbox pehle clear kar do (optional)
            For i As Integer = 0 To view.RowCount - 1
                Dim rowHandle As Integer = view.GetVisibleRowHandle(i)
                If rowHandle < 0 Then Continue For

                ' i = visible index (0-based), isliye SrNo = i + 1
                Dim srNo As Integer = i + 1

                Dim inRange As Boolean = (srNo >= fromNo AndAlso srNo <= toNo)

                ' "CHK" yaha tumhare checkbox column ka FieldName hai
                view.SetRowCellValue(rowHandle, "CHK", inRange)
            Next
        Finally
            view.EndUpdate()
        End Try
    End Sub
End Class