
Imports System.ComponentModel
Imports BL
Imports Newtonsoft.Json.Linq

Public Class SendMultipleWhatsapp

    Public DT As New DataTable
    Public PATH As New ArrayList
    Public FILENAME As New ArrayList
    Dim RESPONSE As String = ""
    Public FRMSTRING As String = ""

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub


    Private Sub CMBNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, TXTADD, " And LEDGERS.ACC_TYPE='ACCOUNTS'", "SUNDRY DEBTORS", "ACCOUNTS", "", "", TXTPARTYNO.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENTNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBAGENTNAME.Validating
        Try
            If CMBAGENTNAME.Text.Trim <> "" Then namevalidate(CMBAGENTNAME, CMBCODE, e, Me, TXTADD, " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'", "SUNDRY CREDITORS", "ACCOUNTS", "", "", TXTAGENTNO.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOTHERNAME1_Validating(sender As Object, e As CancelEventArgs) Handles CMBOTHERNAME1.Validating
        Try
            If CMBOTHERNAME1.Text.Trim <> "" Then namevalidate(CMBOTHERNAME1, CMBCODE, e, Me, TXTADD, " AND LEDGERS.ACC_TYPE='ACCOUNTS'", "SUNDRY DEBTORS", "ACCOUNTS", "", "", TXTOTHERNO1.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOTHERNAME2_Validating(sender As Object, e As CancelEventArgs) Handles CMBOTHERNAME2.Validating
        Try
            If CMBOTHERNAME2.Text.Trim <> "" Then namevalidate(CMBOTHERNAME2, CMBCODE, e, Me, TXTADD, " AND LEDGERS.ACC_TYPE='ACCOUNTS'", "SUNDRY DEBTORS", "ACCOUNTS", "", "", TXTOTHERNO2.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOTHERNAME3_Validating(sender As Object, e As CancelEventArgs) Handles CMBOTHERNAME3.Validating
        Try
            If CMBOTHERNAME3.Text.Trim <> "" Then namevalidate(CMBOTHERNAME3, CMBCODE, e, Me, TXTADD, " AND LEDGERS.ACC_TYPE='ACCOUNTS'", "SUNDRY DEBTORS", "ACCOUNTS", "", "", TXTOTHERNO3.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Async Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try

            'FIRST CHECK STATUS OF MOBILE CONNECTION
            Dim CONNECTSTATUS As String = JObject.Parse(Await CHECKMOBILECONNECTSTATUS())("success")
            If CONNECTSTATUS = "False" Then
                MsgBox("Mobile Not connected, Please Check Connection", MsgBoxStyle.Critical)
                Exit Sub
            End If


            If TXTPARTYNO.Text.Trim = "" Then
                TXTPARTYNO.Clear()
                TXTAGENTNO.Clear()
                TXTOTHERNO1.Clear()
                TXTOTHERNO2.Clear()
                TXTOTHERNO3.Clear()


                For I As Integer = 0 To gridbill.RowCount - 1
                    Dim DTROW As DataRow = gridbill.GetDataRow(I)
                    If DTROW("PARTYWHATSAPP") <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & DTROW("PARTYWHATSAPP"), DTROW("ATTACHMENT"), DTROW("FILENAME"))
                        ERRORMESSAGE(DTROW("PARTYWHATSAPP"))
                    End If

                    If DTROW("AGENTWHATSAPP") <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & DTROW("AGENTWHATSAPP"), DTROW("ATTACHMENT"), DTROW("FILENAME"))
                        ERRORMESSAGE(DTROW("AGENTWHATSAPP"))
                    End If

                    If TXTAUTOCC.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTAUTOCC.Text.Trim, PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTAUTOCC.Text)
                    End If


                    'For J As Integer = 0 To GRIDNAME.RowCount - 1
                    '    Dim DTROW1 As DataRow = GRIDNAME.GetDataRow(J)
                    '    If Convert.ToBoolean(dtrow("CHK")) = True Then
                    '        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & DTROW1("WHATSAPP"), DTROW("ATTACHMENT"), DTROW("FILENAME"))
                    '        ERRORMESSAGE(DTROW1("WHATSAPP"))
                    '    End If
                    'Next
                Next



            Else

                If MsgBox("Send All Whatsapp on Single Ledger?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub


                'SENDING ALL FILES TO SINGLE LEDGERS
                If PATH.Count = 0 Then Exit Sub


                For I As Integer = 0 To PATH.Count - 1
                    If TXTPARTYNO.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTPARTYNO.Text.Trim, PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTPARTYNO.Text)
                    End If
                    If TXTAGENTNO.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTAGENTNO.Text.Trim, PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTAGENTNO.Text)
                    End If
                    If TXTOTHERNO1.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTOTHERNO1.Text.Trim, PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTOTHERNO1.Text)
                    End If
                    If TXTOTHERNO2.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTOTHERNO2.Text.Trim, PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTOTHERNO2.Text)
                    End If
                    If TXTOTHERNO3.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTOTHERNO3.Text.Trim, PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTOTHERNO3.Text)
                    End If
                    If TXTAUTOCC.Text.Trim <> "" Then
                        RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & TXTAUTOCC.Text.Trim, PATH(I), FILENAME(I))
                        ERRORMESSAGE(TXTAUTOCC.Text)
                    End If


                    'SENDING WHATSAPP TO MULTIPLE LEDGERS SELECTED
                    For J As Integer = 0 To GRIDNAME.RowCount - 1
                        Dim dtrow As DataRow = GRIDNAME.GetDataRow(J)
                        If Convert.ToBoolean(dtrow("CHK")) = True Then
                            RESPONSE = Await SENDWHATSAPPATTACHMENT("91" & dtrow("WHATSAPP"), PATH(I), FILENAME(I))
                            ERRORMESSAGE(dtrow("WHATSAPP"))
                        End If
                    Next
                Next


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
                        For J As Integer = 0 To GRIDNAME.RowCount - 1
                            Dim dtrow As DataRow = GRIDNAME.GetDataRow(J)
                            If Convert.ToBoolean(dtrow("CHK")) = True Then
                                RESPONSE = Await SENDWHATSAPPMESSAGE("91" & dtrow("WHATSAPP"), TXTMESSAGE.Text.Trim)
                            End If
                        Next
                    End If
                End If

            End If



            MsgBox("Message Sent", MsgBoxStyle.Information)
            Me.Close()
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

    Private Sub SendMultipleWhatsapp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            gridbilldetails.DataSource = DT


            Dim OBJCMN As New ClsCommon
            Dim DTNAME As DataTable = OBJCMN.search(" CAST(0 AS BIT) AS CHK, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(GROUPMASTER.group_name, '') AS [GROUP], ISNULL(CITYMASTER.city_name, '') AS CITY,  ISNULL(AREAMASTER.area_name, '') AS AREA, ISNULL(LEDGERS.ACC_WHATSAPPNO, '') AS WHATSAPP, ISNULL(GROUPOFCOMPANIESMASTER.GOC_NAME, '') AS GRPCOM ", "", " GROUPMASTER RIGHT OUTER JOIN LEDGERS LEFT OUTER JOIN GROUPOFCOMPANIESMASTER ON LEDGERS.ACC_GOCID = GROUPOFCOMPANIESMASTER.GOC_ID LEFT OUTER JOIN AREAMASTER ON LEDGERS.Acc_areaid = AREAMASTER.area_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id ON GROUPMASTER.group_id = LEDGERS.Acc_groupid ", " AND GROUPMASTER.GROUP_SECONDARY IN ('SUNDRY CREDITORS', 'SUNDRY DEBTORS')  AND LEDGERS.ACC_WHATSAPPNO <> '' and acc_yearid = " & YearId)
            GRIDNAMEDETAILS.DataSource = DTNAME
            If DTNAME.Rows.Count > 0 Then
                GRIDNAME.FocusedRowHandle = gridbill.RowCount - 1
                GRIDNAME.TopRowIndex = gridbill.RowCount - 15
            End If


            'IF AUTOCC IS TRUE THEN GET THE MOBILE NO FROM CMPMASTER AND SHOW IN AUTOCC
            Dim DTCC As New DataTable
            If WHATSAPPAUTOCC = True Then
                DTCC = OBJCMN.search("ISNULL(CMP_TEL,'') AS MOBILENO", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
                If DTCC.Rows.Count > 0 Then TXTAUTOCC.Text = DTCC.Rows(0).Item("MOBILENO")
            End If

            fillname(CMBNAME, False, " AND LEDGERS.ACC_TYPE='ACCOUNTS'")
            fillname(CMBAGENTNAME, False, " AND GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' AND LEDGERS.ACC_TYPE='AGENT'")
            fillname(CMBOTHERNAME1, False, " AND LEDGERS.ACC_TYPE='ACCOUNTS'")
            fillname(CMBOTHERNAME2, False, " AND LEDGERS.ACC_TYPE='ACCOUNTS'")
            fillname(CMBOTHERNAME3, False, " AND LEDGERS.ACC_TYPE='ACCOUNTS'")

            CMBNAME.Text = ""
            CMBAGENTNAME.Text = ""
            CMBOTHERNAME1.Text = ""
            CMBOTHERNAME2.Text = ""
            CMBOTHERNAME3.Text = ""

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class