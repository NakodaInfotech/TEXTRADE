
Imports BL
Imports System.Text.RegularExpressions

Public Class SendMultipleMail

    Public DT As New DataTable
    Public FORMTYPE As String

    Private Sub SendMultipleMail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            fillcmb(cmbfirstadd)
            fillcmb(cmbsecondadd)
            fillcmb(cmbthirdadd)
            fillcmb(cmbfourthadd)
            fillcmb(cmbfifthadd)
            gridbilldetails.DataSource = DT
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillcmb(ByRef cmb As System.Windows.Forms.ComboBox)
        Try
            If cmb.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("email_id", "", "EmailMaster", " and email_cmpid = " & CmpId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "Email_id"
                    cmb.DataSource = dt
                    cmb.DisplayMember = "Email_id"
                End If
                cmb.SelectAll()
                cmb.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub cmbvalidate(ByRef cmb As System.Windows.Forms.ComboBox)
        Try
            Dim intresult As Integer
            If cmb.Text.Trim <> "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("Email_id", "", "EmailMaster", " and Email_id = '" & cmb.Text.Trim & "' and Email_cmpid = " & CmpId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Email Address not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(cmb.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objEmailmaster As New ClsEmailMaster
                        objEmailmaster.alParaval = alParaval
                        intresult = objEmailmaster.save()
                        'e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbfirstadd_Enter(sender As Object, e As EventArgs) Handles cmbfirstadd.Enter
        Try
            fillcmb(cmbfirstadd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbfirstadd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbfirstadd.Validating
        Try
            cmbvalidate(cmbfirstadd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbsecondadd_Enter(sender As Object, e As EventArgs) Handles cmbsecondadd.Enter
        Try
            fillcmb(cmbsecondadd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbsecondadd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbsecondadd.Validating
        Try
            cmbvalidate(cmbsecondadd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbthirdadd_Enter(sender As Object, e As EventArgs) Handles cmbthirdadd.Enter
        Try
            fillcmb(cmbthirdadd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbthirdadd_Validated(sender As Object, e As EventArgs) Handles cmbthirdadd.Validated
        Try
            cmbvalidate(cmbthirdadd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbfourthadd_Enter(sender As Object, e As EventArgs) Handles cmbfourthadd.Enter
        Try
            fillcmb(cmbfourthadd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbfourthadd_Validated(sender As Object, e As EventArgs) Handles cmbfourthadd.Validated
        Try
            cmbvalidate(cmbfourthadd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbfifthadd_Enter(sender As Object, e As EventArgs) Handles cmbfifthadd.Enter
        Try
            fillcmb(cmbfifthadd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbfifthadd_Validated(sender As Object, e As EventArgs) Handles cmbfifthadd.Validated
        Try
            cmbvalidate(cmbfifthadd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try
            If cmbfirstadd.Text.Trim <> "" Then
                If MsgBox("Send All Mails on Single E-mail id?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    cmbfirstadd.Text = ""
                    cmbsecondadd.Text = ""
                    cmbthirdadd.Text = ""
                    cmbfourthadd.Text = ""
                    cmbfifthadd.Text = ""
                End If
            End If

            Dim TEMPMAILBODY As String = TXTMAILBODY.Text.Trim
            Dim pattern As String
            pattern = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"

            For i As Integer = 0 To gridbill.RowCount - 1
                TXTMAILBODY.Clear()
                Dim DTROW As DataRow = gridbill.GetDataRow(i)


                If cmbfirstadd.Text.Trim <> "" Then
                    DTROW("PARTYEMAILID") = cmbfirstadd.Text.Trim
                    DTROW("AGENTEMAILID") = ""
                End If


                TXTMAILBODY.Text = TEMPMAILBODY & vbCrLf & vbCrLf & "<b>To," & vbCrLf & DTROW("NAME") & "</b>" & vbCrLf & vbCrLf & vbCrLf & "Please find attached herewith <b>" & DTROW("SUBJECT") & "</b> of Amount Rs. <b>" & Format(Val(DTROW("GRANDTOTAL")), "0.00") & "</b>" & vbCrLf & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & "Kindly print the attached copy of " & FORMTYPE & " for your audit reference if required." & vbCrLf & vbCrLf & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & "<b><span style=""color:red"">Kindly verify your GSTIN, and let us know in case of any issues</span></b>" & vbCrLf & vbCrLf & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & "To support Digital India initiative, we would appreciate if you can do the payment by NEFT / Fund Transfer / UPI on the below provided bank details. And provide us with the transaction details on the below contact details." & vbCrLf & vbCrLf & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & "In case you do not have net banking / UPI facility, you can courier us the cheque on our below mentioned office address or deposit the cheque at our bank branch near you. Send us photograph or scanned copy of Deposited Bank Slip with cheque details mentioned on it for our reference on the below mentioned Email." & vbCrLf & vbCrLf & vbCrLf

                TXTMAILBODY.Text = TXTMAILBODY.Text & "<b>Our Bank Details" & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & "Bank Name & Branch : " & CMPBANK & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & "Account No : " & CMPACCNO & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & "IFSC Code : " & CMPIFSC & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & "UPI : " & CMPUPI & vbCrLf & vbCrLf & vbCrLf


                TXTMAILBODY.Text = TXTMAILBODY.Text & "Contact Details :" & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & CmpName & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & CMPADDRESS & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & "Tel No : " & CMPTEL & vbCrLf


                TXTMAILBODY.Text = TXTMAILBODY.Text & "Regards" & vbCrLf
                TXTMAILBODY.Text = TXTMAILBODY.Text & CmpName & "</b>"

                'TXTMAILBODY.Text = TXTMAILBODY.Text & "<b>" & CmpName & vbCrLf & vbCrLf & "VISA SERVICE / AIR TICKETING / IMMIGRATION / INTERNATIONAL TOUR PACKAGES" & vbCrLf & vbCrLf & DT.Rows(0).Item("ADD1") & vbCrLf & DT.Rows(0).Item("ADD2") & vbCrLf & DT.Rows(0).Item("EMAIL") & vbCrLf & "MEMBER : ETAA, IAAI." & vbCrLf & vbCrLf & "SABIR TINWALA  MOB 09820494895" & vbCrLf & "(PROPRIETOR)" & vbCrLf & vbCrLf & "RAZZAQUE = 09920640208" & vbCrLf & vbCrLf & CmpName & vbCrLf & DT.Rows(0).Item("BANKNAME") & vbCrLf & "BRANCH - " & DT.Rows(0).Item("BRANCH") & vbCrLf & "CURRENT A/C NO - " & DT.Rows(0).Item("ACCNO") & vbCrLf & "RTGS / NEFT IFSC CODE - " & DT.Rows(0).Item("IFSCCODE") & vbCrLf & vbCrLf & "GSTIN NO 27ANFPT8848K2Z2</b>"


                TXTMAILBODY.Text = "<span style=""font-family:Tahoma;font-size: 10pt;"">" & TXTMAILBODY.Text & "</span>"
                TXTMAILBODY.Text = TXTMAILBODY.Text.Replace(vbNewLine, "<br/>")
                Dim ATTACHMENT As New ArrayList
                ATTACHMENT.Add(DTROW("ATTACHMENT"))
                If DTROW("PARTYEMAILID") <> "" AndAlso Regex.IsMatch(DTROW("PARTYEMAILID"), pattern) Then sendemail(DTROW("PARTYEMAILID"), "", TXTMAILBODY.Text, DTROW("SUBJECT"), ATTACHMENT, 1)
                If DTROW("AGENTEMAILID") <> "" AndAlso Regex.IsMatch(DTROW("AGENTEMAILID"), pattern) Then sendemail(DTROW("AGENTEMAILID"), "", TXTMAILBODY.Text, DTROW("SUBJECT"), ATTACHMENT, 1)
                If cmbsecondadd.Text.Trim <> "" AndAlso Regex.IsMatch(cmbsecondadd.Text.Trim, pattern) Then sendemail(cmbsecondadd.Text.Trim, "", TXTMAILBODY.Text, DTROW("SUBJECT"), ATTACHMENT, 1)
                If cmbthirdadd.Text.Trim <> "" AndAlso Regex.IsMatch(cmbthirdadd.Text.Trim, pattern) Then sendemail(cmbthirdadd.Text.Trim, "", TXTMAILBODY.Text, DTROW("SUBJECT"), ATTACHMENT, 1)
                If cmbfourthadd.Text.Trim <> "" AndAlso Regex.IsMatch(cmbfourthadd.Text.Trim, pattern) Then sendemail(cmbfourthadd.Text.Trim, "", TXTMAILBODY.Text, DTROW("SUBJECT"), ATTACHMENT, 1)
                If cmbfifthadd.Text.Trim <> "" AndAlso Regex.IsMatch(cmbfifthadd.Text.Trim, pattern) Then sendemail(cmbfifthadd.Text.Trim, "", TXTMAILBODY.Text, DTROW("SUBJECT"), ATTACHMENT, 1)

            Next
            MsgBox("Mail sent Successfully")
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SendMultipleMail_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "SAKARIA" Then TXTMAILBODY.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class