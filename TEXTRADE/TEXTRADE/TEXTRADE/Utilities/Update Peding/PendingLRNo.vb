
Imports System.IO
Imports BL
Imports DevExpress.XtraEditors.Controls

Public Class PendingLRNo

    Dim TEMPMSG As Integer = 0
    Dim TEMPMSGEWAY As Integer = 0

    Private Sub PendingReturnDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PendingReturnDate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim INVWHERECLAUSE As String = ""
            Dim PURRETWHERECLAUSE As String = ""
            If RBPENDING.Checked = True Then
                INVWHERECLAUSE = " AND ISNULL(INVOICEMASTER.INVOICE_LRNO,'') = '' "
                PURRETWHERECLAUSE = " AND ISNULL(PURCHASERETURN.PR_LRNO,'') = '' "
                GCHK.Visible = False
                'gridbill.OptionsSelection.CheckBoxSelectorColumnWidth = 0
                'gridbill.OptionsSelection.MultiSelect = False
                'gridbill.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            Else
                INVWHERECLAUSE = " AND ISNULL(INVOICEMASTER.INVOICE_LRNO,'') <> ''"
                PURRETWHERECLAUSE = " AND ISNULL(PURCHASERETURN.PR_LRNO,'') <> ''"
                GCHK.Visible = True
                GCHK.VisibleIndex = 0
                'gridbill.OptionsSelection.CheckBoxSelectorColumnWidth = 30
                'gridbill.OptionsSelection.MultiSelect = True
                'gridbill.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
            End If

            Dim OBJCMN As New ClsCommonMaster
            'THIS CODE WAS ONLY FOR INVOICEMASTER, NOW WE HAVE ADDED PUR RET IN THIS
            'Dim dt As DataTable = OBJCMN.search(" INVOICEMASTER.INVOICE_NO AS INVOICENO, INVOICEMASTER.INVOICE_REGISTERID AS REGID, LEDGERS.Acc_cmpname AS NAME, INVOICEMASTER.INVOICE_DATE AS DATE,  ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, INVOICEMASTER.INVOICE_LRNO AS LRNO, (CASE WHEN INVOICEMASTER.INVOICE_LRNO ='' THEN NULL ELSE INVOICEMASTER.INVOICE_LRDATE END) AS LRDATE, INVOICEMASTER.INVOICE_TOTALPCS AS PCS, INVOICEMASTER.INVOICE_TOTALMTRS AS MTRS, ISNULL(INVOICEMASTER.INVOICE_GDNNO,'') AS CHALLANNO, ISNULL(SHIPTOLEDGERS.ACC_CMPNAME,'') AS SHIPTO, ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(INVOICE_BALENOFROM,0) AS BALENOFROM, ISNULL(INVOICE_EWAYBILLNO,'') AS EWAYBILLNO, ISNULL(REGISTER_NAME,'') AS REGNAME, ISNULL(FROMCITYMASTER.CITY_NAME,'') AS FROMCITY, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL,0) AS GRANDTOTAL ", "", " INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.REGISTER_ID INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS TRANSLEDGERS ON INVOICEMASTER.INVOICE_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS SHIPTOLEDGERS ON INVOICEMASTER.INVOICE_PACKINGID = SHIPTOLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON INVOICE_TOCITYID = CITY_ID LEFT OUTER JOIN CITYMASTER AS FROMCITYMASTER ON INVOICE_TRANSCITYID = FROMCITYMASTER.CITY_ID", WHERECLAUSE & " AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER.INVOICE_REGISTERID, INVOICEMASTER.INVOICE_NO, ISNULL(FROMCITYMASTER.CITY_NAME,'') ")
            Dim dt As DataTable = OBJCMN.search("*, CAST(0 AS BIT) AS CHK", "", "(SELECT INVOICEMASTER.INVOICE_NO AS INVOICENO, INVOICEMASTER.INVOICE_REGISTERID AS REGID, LEDGERS.Acc_cmpname AS NAME, INVOICEMASTER.INVOICE_DATE AS DATE,  ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, INVOICEMASTER.INVOICE_LRNO AS LRNO, (CASE WHEN INVOICEMASTER.INVOICE_LRNO ='' THEN NULL ELSE INVOICEMASTER.INVOICE_LRDATE END) AS LRDATE, INVOICEMASTER.INVOICE_TOTALPCS AS PCS, INVOICEMASTER.INVOICE_TOTALMTRS AS MTRS, ISNULL(INVOICEMASTER.INVOICE_GDNNO,'') AS CHALLANNO, ISNULL(SHIPTOLEDGERS.ACC_CMPNAME,'') AS SHIPTO, ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(INVOICE_BALENOFROM,0) AS BALENOFROM, ISNULL(INVOICE_EWAYBILLNO,'') AS EWAYBILLNO, ISNULL(REGISTER_NAME,'') AS REGNAME, ISNULL(FROMCITYMASTER.CITY_NAME,'') AS FROMCITY, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL,0) AS GRANDTOTAL, 'INVOICE' AS TYPE, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, INVOICEMASTER.INVOICE_PRINTINITIALS AS PRINTINITIALS, ISNULL(LEDGERS.ACC_WHATSAPPNO,'') AS PARTYWHATSAPP, ISNULL(AGENTLEDGERS.ACC_WHATSAPPNO,'') AS AGENTWHATSAPP FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.REGISTER_ID INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS TRANSLEDGERS ON INVOICEMASTER.INVOICE_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS SHIPTOLEDGERS ON INVOICEMASTER.INVOICE_PACKINGID = SHIPTOLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON INVOICEMASTER.INVOICE_AGENTID = AGENTLEDGERS.Acc_id  LEFT OUTER JOIN CITYMASTER ON INVOICE_TOCITYID = CITY_ID LEFT OUTER JOIN CITYMASTER AS FROMCITYMASTER ON INVOICE_TRANSCITYID = FROMCITYMASTER.CITY_ID WHERE 1=1 " & INVWHERECLAUSE & " AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " UNION ALL SELECT PURCHASERETURN.PR_NO AS INVOICENO, 0 AS REGID, LEDGERS.Acc_cmpname AS NAME, PURCHASERETURN.PR_DATE AS DATE,  ISNULL(TRANSLEDGERS.Acc_cmpname, '') AS TRANSNAME, PURCHASERETURN.PR_LRNO AS LRNO, (CASE WHEN PURCHASERETURN.PR_LRNO ='' THEN NULL ELSE PURCHASERETURN.PR_LRDATE END) AS LRDATE, PURCHASERETURN.PR_TOTALPCS AS PCS, PURCHASERETURN.PR_TOTALMTRS AS MTRS, ISNULL(PURCHASERETURN.PR_PRCHNO,'') AS CHALLANNO, ISNULL(SHIPTOLEDGERS.ACC_CMPNAME,'') AS SHIPTO, ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO, ISNULL(CITYMASTER.CITY_NAME,'') AS CITY, ISNULL(PR_NOOFBALES,0) AS BALENOFROM, ISNULL(PR_EWAYBILLNO,'') AS EWAYBILLNO, '' AS REGNAME, '' AS FROMCITY, ISNULL(PURCHASERETURN.PR_GRANDTOTAL,0) AS GRANDTOTAL, 'PURRETURN' AS TYPE, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, PURCHASERETURN.PR_PRINTINITIALS AS PRINTINITIALS, ISNULL(LEDGERS.ACC_WHATSAPPNO,'') AS PARTYWHATSAPP, ISNULL(AGENTLEDGERS.ACC_WHATSAPPNO,'') AS AGENTWHATSAPP FROM PURCHASERETURN INNER JOIN LEDGERS ON PURCHASERETURN.PR_LEDGERID = LEDGERS.Acc_id INNER JOIN LEDGERS AS TRANSLEDGERS ON PURCHASERETURN.PR_transledgerid = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS SHIPTOLEDGERS ON PURCHASERETURN.PR_DELIVERYATID = SHIPTOLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON PURCHASERETURN.PR_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN CITYMASTER ON PR_TOCITYID = CITY_ID WHERE 1=1 " & PURRETWHERECLAUSE & " AND PURCHASERETURN.PR_YEARID = " & YearId & " ) AS T", " ORDER BY T.REGID, T.INVOICENO, T.FROMCITY ")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try


            If ALLOWWHATSAPP = True Then
                TEMPMSG = MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo)
                TEMPMSGEWAY = MsgBox("Send Challan | Invoice | Eway ?", MsgBoxStyle.YesNo)
                If Not CHECKWHASTAPPEXP() Then
                    MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech On 02249724411", MsgBoxStyle.Critical)
                End If
            End If


            For I As Integer = 0 To gridbill.RowCount - 1
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If ROW Is Nothing Then Exit Sub
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable

                'If IsDBNull(ROW("RDATE")) = False Then DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_RETURNDATE = '" & Format(ROW("RDATE"), "MM/dd/yyyy") & "' WHERE INVOICE_NO = " & ROW("GRNO") & " AND INVOICE_YEARID = " & YearId, "", "")
                If ROW("LRNO") <> "" And IsDBNull(ROW("LRDATE")) = False Then
                    If ROW("TYPE") = "INVOICE" Then
                        DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_LRDATE = '" & Format(ROW("LRDATE"), "MM/dd/yyyy") & "', INVOICE_LRNO = '" & ROW("LRNO") & "' WHERE INVOICE_NO = " & Val(ROW("INVOICENO")) & " AND INVOICE_REGISTERID = " & Val(ROW("REGID")) & " AND INVOICE_YEARID = " & YearId, "", "")
                    ElseIf ROW("TYPE") = "PURRETURN" Then
                        DT = OBJCMN.Execute_Any_String("UPDATE PURCHASERETURN SET PR_LRDATE = '" & Format(ROW("LRDATE"), "MM/dd/yyyy") & "', PR_LRNO = '" & ROW("LRNO") & "' WHERE PR_NO = " & Val(ROW("INVOICENO")) & " AND PR_YEARID = " & YearId, "", "")
                    End If

                    'SEND INVOIC TO PARTY DIRECTLY MAIL
                    'KEPT THIS CODE ON HOLD AS IT TAKES A LOT OF TIME
                    If ClientName = "VSTRADERS" Or ClientName = "AVIS" Then SENDDIRECTMAIL(ROW("NAME"), Val(ROW("INVOICENO")), ROW("REGNAME"))

                    If ALLOWWHATSAPP = True And TEMPMSG = vbYes Then
                        SENDWHATSAPP(ROW("NAME"), Val(ROW("INVOICENO")), ROW("REGNAME"), ROW("AGENTNAME"), ROW("SHIPTO"), ROW("EWAYBILLNO"))
                    End If

                End If

NEXTLINE:
            Next
            fillgrid()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SENDDIRECTMAIL(PARTYNAME As String, INVOICENO As Integer, REGISTER As String)
        Try
            'CHECK WHETHER EMAILID IS PRESENT IN LEDGER OR NOT, IF NOT THEN EXIT SUB WITH MESSAGE
            Dim OBJCMN As New ClsCommon
            Dim DTEMAIL As DataTable = OBJCMN.SEARCH("ACC_EMAIL AS EMAILID", "", "LEDGERS", "AND ACC_CMPNAME = '" & PARTYNAME & "' AND ACC_YEARID = " & YearId)
            If DTEMAIL.Rows.Count > 0 AndAlso DTEMAIL.Rows(0).Item("EMAILID") <> "" Then

                Dim ALATTACHMENT As New ArrayList
                Dim OBJINVOICE As New saledesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.DIRECTPRINT = True
                OBJINVOICE.FRMSTRING = "INVOICE"
                OBJINVOICE.DIRECTMAIL = True
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STATE_REMARK,'') AS STATECODE", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(INVOICENO) & " AND REGISTER_NAME = '" & REGISTER & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                OBJINVOICE.registername = REGISTER
                OBJINVOICE.PRINTSETTING = PRINTDIALOG
                OBJINVOICE.INVNO = Val(INVOICENO)
                OBJINVOICE.NOOFCOPIES = 1
                OBJINVOICE.Show()
                OBJINVOICE.Close()
                ALATTACHMENT.Add(Application.StartupPath & "\INVOICE_" & Val(INVOICENO) & ".pdf")

                sendemail(DTEMAIL.Rows(0).Item("EMAILID"), ALATTACHMENT(0), "", "Invoice", ALATTACHMENT, ALATTACHMENT.Count, "", "", "", "", "")
                MsgBox("Mail Sent")
            Else
                MsgBox("Add E-Mail id in Ledger")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SENDWHATSAPP(ByVal NAME As String, INVOICENO As Integer, ByVal REGNAME As String, ByVal AGENTNAME As String, ByVal SHIPTO As String, EWAYBILLNO As String)
        Try


            Dim WHATSAPPNO As String = ""
            Dim OBJINVOICE As New saledesign
            OBJINVOICE.MdiParent = MDIMain
            OBJINVOICE.DIRECTPRINT = True
            OBJINVOICE.FRMSTRING = "INVOICE"
            OBJINVOICE.DIRECTMAIL = True
            OBJINVOICE.PARTYNAME = NAME
            OBJINVOICE.INVOICECOPYNAME = "CUSTOMER COPY"
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STATE_REMARK,'') AS STATECODE, ISNULL(LEDGERS.ACC_WHATSAPPNO,'') AS WHATSAPPNO", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(INVOICENO) & " AND REGISTER_NAME = '" & REGNAME & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
            If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
            If DT.Rows.Count > 0 Then WHATSAPPNO = DT.Rows(0).Item("WHATSAPPNO")
            OBJINVOICE.registername = REGNAME
            OBJINVOICE.PRINTSETTING = PRINTDIALOG
            OBJINVOICE.INVNO = Val(INVOICENO)
            OBJINVOICE.NOOFCOPIES = 1
            OBJINVOICE.Show()
            OBJINVOICE.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PARTYNAME = NAME
            OBJWHATSAPP.AGENTNAME = AGENTNAME
            If NAME <> SHIPTO Then OBJWHATSAPP.OTHERNAME1 = SHIPTO
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & NAME & "INVOICE_" & Val(INVOICENO) & ".pdf")
            OBJWHATSAPP.FILENAME.Add(NAME & "INVOICE_" & Val(INVOICENO) & ".pdf")


            ''SEND CHALLAN | INVOICE | EWAYBILL 
            If TEMPMSGEWAY = vbYes Then
                Dim DTDESC As DataTable = OBJCMN.Execute_Any_String("select DISTINCT INVOICE_FROMNO AS GDNNO FROM INVOICEMASTER_DESC INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(INVOICENO) & " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "' AND INVOICE_YEARID = " & YearId & " ORDER BY INVOICE_FROMNO ", "", "")
                For Each DTROW As DataRow In DTDESC.Rows
                    Dim OBJGDN As New GDNDESIGN
                    OBJGDN.MdiParent = MDIMain
                    OBJGDN.DIRECTPRINT = True
                    OBJGDN.FRMSTRING = "GDN"
                    OBJGDN.DIRECTMAIL = True
                    OBJGDN.vendorname = NAME
                    OBJGDN.agentname = AGENTNAME
                    OBJGDN.FORMULA = "{GDN.GDN_no}=" & Val(DTROW("GDNNO")) & " and {GDN.GDN_yearid}=" & YearId
                    OBJGDN.JONO = Val(DTROW("GDNNO"))
                    OBJGDN.NOOFCOPIES = 1
                    OBJGDN.WHITELABEL = 0
                    OBJGDN.HIDEPCSDETAILS = 0
                    OBJGDN.PRINTINYARDS = 0
                    OBJGDN.Show()
                    OBJGDN.Close()
                    OBJWHATSAPP.PATH.Add(Application.StartupPath & "\" & NAME & "GDN_" & Val(DTROW("GDNNO")) & ".pdf")
                    OBJWHATSAPP.FILENAME.Add(NAME & "GDN_" & Val(DTROW("GDNNO")) & ".pdf")
                Next


                'ADDING EWAYBILL IF PRESENT
                If File.Exists(Application.StartupPath & "\EWB_" & EWAYBILLNO & ".Pdf") Then
                    OBJWHATSAPP.PATH.Add(Application.StartupPath & "\EWB_" & EWAYBILLNO & ".Pdf")
                    OBJWHATSAPP.FILENAME.Add("\EWB_" & EWAYBILLNO & ".Pdf")
                End If
            End If

            Dim SENDER As New Object
            Dim E As New EventArgs
            Call OBJWHATSAPP.SendWhatsapp_Load(SENDER, E)
            Call OBJWHATSAPP.CMDSEND_Click(SENDER, E)



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles gridbill.InvalidRowException
        e.ExceptionMode = ExceptionMode.NoAction
    End Sub

    Private Sub gridbill_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles gridbill.ValidateRow
        Try
            If IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "LRDATE")) = False Then
                If gridbill.GetRowCellValue(e.RowHandle, "LRDATE") < Convert.ToDateTime(gridbill.GetRowCellValue(e.RowHandle, "DATE")).Date Then
                    e.Valid = False
                    gridbill.SetColumnError(GLRDATE, "Date must be After Invoice Date")
                    Exit Sub
                End If
            End If

            'WE WILL SAVE ALL THE ENTRIES 
            'If IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "LRDATE")) = False And IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "LRNO")) = False Then If MsgBox("Save Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Call CMDOK_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try

            Dim ROW As DataRow = gridbill.GetFocusedDataRow
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If IsDBNull(ROW("LRNO")) = True Then
                MsgBox("No Row To Delete")
                Exit Sub
            End If

            If MsgBox("Delete Data?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If ROW("TYPE") = "INVOICE" Then
                DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_LRDATE = '', INVOICE_LRNO = '' WHERE INVOICE_NO = " & Val(ROW("INVOICENO")) & " AND INVOICE_REGISTERID = " & Val(ROW("REGID")) & " AND INVOICE_YEARID = " & YearId, "", "")
            ElseIf ROW("TYPE") = "PURRETURN" Then
                DT = OBJCMN.Execute_Any_String("UPDATE PURCHASERETURN SET PR_LRDATE = PR_DATE, PR_LRNO = '' WHERE PR_NO = " & Val(ROW("INVOICENO")) & " AND PR_YEARID = " & YearId, "", "")
            End If
            fillgrid()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSMS_Click(sender As Object, e As EventArgs) Handles CMDSMS.Click
        Try
            If RBENTERED.Checked = True Then

                If ALLOWWHATSAPP = True Then

                    Dim ALATTACHMENT As New ArrayList
                    Dim FILENAME As New ArrayList
                    Dim DTWHATSAPP As New DataTable

                    DTWHATSAPP.Columns.Add("INVNO")
                    DTWHATSAPP.Columns.Add("REGID")
                    DTWHATSAPP.Columns.Add("REGNAME")
                    DTWHATSAPP.Columns.Add("PRINTINITIALS")
                    DTWHATSAPP.Columns.Add("DATE")
                    DTWHATSAPP.Columns.Add("NAME")
                    DTWHATSAPP.Columns.Add("PARTYWHATSAPP")
                    DTWHATSAPP.Columns.Add("AGENTNAME")
                    DTWHATSAPP.Columns.Add("AGENTWHATSAPP")
                    DTWHATSAPP.Columns.Add("GRANDTOTAL")
                    DTWHATSAPP.Columns.Add("SUBJECT")
                    DTWHATSAPP.Columns.Add("ATTACHMENT")
                    DTWHATSAPP.Columns.Add("FILENAME")


                    For I As Integer = 0 To gridbill.RowCount - 1
                        Dim ROW As DataRow = gridbill.GetDataRow(I)
                        If Convert.ToBoolean(ROW("CHK")) = True Then
                            Dim OBJINVOICE As New saledesign
                            OBJINVOICE.MdiParent = MDIMain
                            OBJINVOICE.DIRECTPRINT = True
                            OBJINVOICE.FRMSTRING = "INVOICE"
                            OBJINVOICE.DIRECTMAIL = False
                            OBJINVOICE.DIRECTWHATSAPP = True
                            OBJINVOICE.INVOICETRANS = False
                            OBJINVOICE.INVOICERETAIL = False
                            OBJINVOICE.INVOICECOPYNAME = "CUSTOMER COPY"
                            OBJINVOICE.PARTYNAME = ROW("NAME")
                            OBJINVOICE.AGENTNAME = ROW("AGENTNAME")
                            Dim OBJCMN As New ClsCommon
                            Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(STATE_REMARK,'') AS STATECODE, ISNULL(REGISTERMASTER.REGISTER_ID,0) AS REGID", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.ACC_STATEID = STATE_ID INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICEMASTER.INVOICE_REGISTERID ", " AND INVOICEMASTER.INVOICE_NO = " & Val(ROW("INVOICENO")) & " AND REGISTER_NAME = '" & ROW("REGNAME") & "' AND INVOICEMASTER.INVOICE_YEARID = " & YearId)
                            If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("STATECODE") <> CMPSTATECODE Then OBJINVOICE.IGSTFORMAT = True
                            OBJINVOICE.registername = ROW("REGNAME")
                            OBJINVOICE.PRINTSETTING = PRINTDIALOG
                            OBJINVOICE.INVNO = Val(ROW("INVOICENO"))
                            OBJINVOICE.NOOFCOPIES = 1
                            OBJINVOICE.BLANKPAPER = False
                            OBJINVOICE.Show()
                            OBJINVOICE.Close()
                            ALATTACHMENT.Add(Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("INVOICENO")) & ".pdf")
                            FILENAME.Add(ROW("NAME") & "INVOICE_" & Val(ROW("INVOICENO")) & ".pdf")

                            'ADDING IN DTWHATSAPP
                            DTWHATSAPP.Rows.Add(ROW("INVOICENO"), ROW("REGID"), ROW("REGNAME"), ROW("PRINTINITIALS"), ROW("DATE"), ROW("NAME"), ROW("PARTYWHATSAPP"), ROW("AGENTNAME"), ROW("AGENTWHATSAPP"), Val(ROW("GRANDTOTAL")), UCase(CmpName) & " - Invoice No. " & ROW("PRINTINITIALS") & " Dated " & ROW("DATE"), Application.StartupPath & "\" & ROW("NAME") & "INVOICE_" & Val(ROW("INVOICENO")) & ".pdf", ROW("NAME") & "INVOICE_" & Val(ROW("INVOICENO")) & ".pdf")
                            DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_SENDWHATSAPP = 1 FROM InvoiceMaster INNER JOIN REGISTERMASTER On INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id WHERE INVOICE_NO = " & Val(ROW("INVOICENO")) & " AND REGISTERMASTER.REGISTER_NAME = '" & ROW("REGNAME") & "'  AND INVOICE_YEARID = " & YearId, "", "")
                        End If
                    Next


                    If DTWHATSAPP.Rows.Count = 0 Then Exit Sub
                    Dim OBJWHATSAPP As New SendMultipleWhatsapp
                    OBJWHATSAPP.PATH = ALATTACHMENT
                    OBJWHATSAPP.FILENAME = FILENAME
                    OBJWHATSAPP.DT = DTWHATSAPP
                    OBJWHATSAPP.ShowDialog()

                End If


                'If MsgBox("Send SMS?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                'For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                '    SMSCODE(SELECTEDROWS(I))
                'Next
                'MsgBox("Message Sent")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SMSCODE(ROWNO As Integer)
        If ALLOWSMS = True Then
            Dim ROW As DataRow = gridbill.GetDataRow(ROWNO)

            If ClientName <> "KOTHARI" And ROW("MOBILENO") = "" Then Exit Sub
            If ClientName = "KOTHARI" And ROW("SHIPTO") = "" Then Exit Sub

            If ClientName = "NVAHAN" And ROW("LRNO") = "" Then
                If MsgBox("LR No not entered, Wish to send SMS?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            End If

            Dim MSG As String = ""
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            Dim DTINV As DataTable = OBJCMN.SEARCH("ITEM_NAME AS ITEMNAME, INVOICE_BALENO AS GRIDBALENO, INVOICE_PCS AS PCS, INVOICE_MTRS AS MTRS", "", "INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICE_ITEMID = ITEM_ID", " AND INVOICE_NO = " & Val(ROW("INVOICENO")) & " AND INVOICE_REGISTERID = " & Val(ROW("REGID")) & " AND INVOICE_YEARID = " & YearId)

            If ClientName = "KOTHARI" Then
                MSG = MSG & ROW("SHIPTO") & " - " & ROW("CITY") & "\n"
                MSG = MSG & "GOODS DISPATCHED" & "\n"
                MSG = MSG & "BALE NO." & Val(ROW("INVOICENO")) & " X " & ROW("BALENOFROM") & "\n"
                MSG = MSG & "L.R.NO" & ROW("LRNO") & " DT. " & ROW("LRDATE") & "\n"
                MSG = MSG & ROW("EWAYBILLNO")

            ElseIf ClientName = "KCRAYON" Then
                MSG = "INV NO " & Val(ROW("INVOICENO")) & "\n"
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & ROW("TRANSNAME") & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & "TRANSPORT NAME-" & DT.Rows(0).Item("TRANSCODE") & " & LRNO-" & ROW("LRNO") & "\n"
                For Each INVROW As DataRow In DT.Rows
                    MSG = MSG & INVROW("ITEMNAME") & "-" & Format(Val(INVROW("MTRS")), "0.00") & "\n"
                Next
                MSG = MSG & "THANK YOU"

            ElseIf ClientName = "NVAHAN" Then
                MSG = "GOODS DESP" & vbCrLf
                MSG = MSG & "INV-" & Val(ROW("INVOICENO")) & vbCrLf
                MSG = MSG & "LRNO-" & ROW("LRNO") & vbCrLf
                MSG = MSG & "DT-" & ROW("LRDATE") & vbCrLf
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & ROW("TRANSNAME") & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & "TRANS-" & DT.Rows(0).Item("TRANSCODE") & vbCrLf
                For Each INVROW As DataRow In DT.Rows
                    MSG = MSG & "ITEM-" & INVROW("ITEMNAME") & vbCrLf & "PCS-" & Val(INVROW("PCS")) & vbCrLf & "MTRS-" & Val(INVROW("MTRS")) & vbCrLf & "BALE-" & INVROW("GRIDBALENO")
                Next

            ElseIf ClientName = "YASHVI" Then
                MSG = ROW("NAME") & "\n"
                MSG = MSG & "BALENO-" & ROW("CHALLANNO") & "\n"
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & ROW("TRANSNAME") & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & DT.Rows(0).Item("TRANSCODE") & "\n"
                MSG = MSG & "LRNO-" & ROW("LRNO") & "\n"
                MSG = MSG & "DT-" & ROW("LRDATE") & "\n"
                MSG = MSG & "QTY-" & Val(ROW("MTRS")) & "\n"
                MSG = MSG & CmpName

            ElseIf ClientName = "SANGHVI" Or ClientName = "TINUMINU" Then
                MSG = "INV NO" & Val(ROW("INVOICENO")) & "\n"
                MSG = MSG & "DT-" & ROW("DATE") & "\n"
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & ROW("TRANSNAME") & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & "TRANSPORT NAME-" & DT.Rows(0).Item("TRANSCODE") & " & LRNO-" & ROW("LRNO") & "\n"
                MSG = MSG & "LRDT-" & ROW("LRDATE") & "\n"
                MSG = MSG & " & BUNDLES-" & ROW("BALENOFROM") & "\n"
                'For Each ROW As DataGridViewRow In GRIDINVOICE.Rows
                '    MSG = MSG & ROW.Cells(GITEMNAME.Index).Value & "-" & Format(Val(ROW.Cells(Gmtrs.Index).Value), "0.00") & "\n"
                'Next
                MSG = MSG & "THANK YOU"
            Else
                MSG = "SALE NO " & Val(ROW("INVOICENO")) & "\n"
                DT = OBJCMN.SEARCH("ACC_CODE AS TRANSCODE", "", "LEDGERS", " AND ACC_CMPNAME = '" & ROW("TRANSNAME") & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then MSG = MSG & "TRANSPORT -" & DT.Rows(0).Item("TRANSCODE") & " & LRNO-" & ROW("LRNO") & "(" & ROW("LRDATE").Trim & ")" & "\n"
                For Each INVROW As DataRow In DT.Rows
                    MSG = MSG & INVROW("ITEMNAME") & "-" & Format(Val(INVROW("MTRS")), "0.00") & "\n"
                Next
                MSG = MSG & ROW("BALENOFROM") & "\n"
                MSG = MSG & "THANK YOU"
            End If

            If SENDMSG(MSG, ROW("MOBILENO")) = "1701" Then
                DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_SMSSEND = 1 WHERE INVOICE_NO = " & Val(ROW("INVOICENO")) & " AND INVOICE_REGISTERID = " & Val(ROW("REGID")) & " AND INVOICE_YEARID = " & YearId, "", "")
            Else
                MsgBox("Error Sending Message")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub CMDEXPORT_Click(sender As Object, e As EventArgs) Handles CMDEXPORT.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Pending LR Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Pending LR Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Pending LR Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Pending LR Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub PendingLRNo_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "SHUBHI" Or ClientName = "SUBHLAXMI" Then
                GFROMCITY.VisibleIndex = GMTRS.VisibleIndex + 1
                GCITY.VisibleIndex = GFROMCITY.VisibleIndex + 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class