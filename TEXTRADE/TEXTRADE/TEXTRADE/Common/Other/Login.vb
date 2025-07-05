Imports BL

Public Class Login

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        End
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        End
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            CHECKVERSION()
            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            If txtusername.Text.Trim = "Admin" Then
                GoTo Line2
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT2 As DataTable = OBJCMN.Execute_Any_String(" UPDATE USERMASTER SET USER_CHK = 1", " WHERE USER_NAME='" & txtusername.Text.Trim & "' and user_cmpid='" & CmpId & "' and user_locationid='" & Locationid & "' and user_yearid='" & YearId & "'", "")
Line2:
            Dim objlogin As New clsLogin
            UserName = txtusername.Text.Trim
            Mydate = Now.Date
            Cmpdetails.Show()
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If txtusername.Text.Trim.Length = 0 Then
            EP.SetError(txtusername, "Fill User Name")
            bln = False
        End If

        If txtpassword.Text.Trim.Length = 0 Then
            EP.SetError(txtpassword, "Fill Password")
            bln = False
        End If

        Dim objcmn As New ClsCommon
        Dim dt As DataTable = objcmn.search("User_id, User_password", "", "UserMaster", " and user_namE= '" & txtusername.Text.Trim & "'")
        If dt.Rows.Count > 0 Then
            For Each DTROW As DataRow In dt.Rows
                If txtpassword.Text.Trim <> DTROW(1).ToString Then
                    bln = False
                Else
                    Userid = DTROW(0)

                    ''*********SESSION CHECKING****************

                    If txtusername.Text.Trim = "Admin" Then
                        GoTo line1
                    End If

                    'Dim dt1 As DataTable = objcmn.search(" USER_CHK", "", " USERMASTER", " and user_namE= '" & txtusername.Text.Trim & "' and user_cmpid='" & CmpId & "' and user_locationid='" & Locationid & "' and user_yearid='" & YearId & "'")
                    'If dt1.Rows.Count > 0 Then
                    '    If dt1.Rows(0).Item("USER_CHK") = "1" Then
                    '        'Ep.SetError(txtpassword, "Please Logout from another system !")
                    '        MsgBox("Please Logout from another system", MsgBoxStyle.Critical)
                    '        bln = False
                    '        End
                    '    End If
                    'Else
                    '    'IF CLIENTNAME IS NOT PRESENT
                    '    End
                    'End If
line1:
                    bln = True
                    Exit For
                End If
            Next
        Else
            Ep.SetError(txtusername, "Invalid User")
            bln = False
        End If
        If bln = False Then Ep.SetError(txtpassword, "Incorrect Password")

        Return bln
    End Function

    Private Sub txtusername_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtusername.Validated
        txtusername.Text = StrConv(txtusername.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub Login_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt = True And e.KeyCode = Windows.Forms.Keys.L Then       'for Login
            Call cmdok_Click(sender, e)
        ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            End
        ElseIf e.Alt = True And e.Control = True And e.KeyCode = Keys.A Then
            SHOWHIDDENCMP = True
        End If
    End Sub

    Sub CHECKVERSION()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.SEARCH(" VERSION_NO AS VERSION, VERSION_CLIENTNAME AS CLIENTNAME, VERSION_REPORTTYPE AS REPORTTYPE, ISNULL(VERSION_ALLOWBARCODE,0)  AS ALLOWBARCODE, ISNULL(VERSION_INVOICELINEGST,0) AS INVLINEGST, ISNULL(VERSION_PURCHASELINEGST,0) AS PURLINEGST, ISNULL(VERSION_ALLOWSMS,0) AS ALLOWSMS, ISNULL(VERSION_MANUALINVNO,0) AS MANUALINVNO, ISNULL(VERSION_ITEMWISEDESIGN,0) AS ITEMWISEDESIGN, ISNULL(VERSION_GRNINCHECKING,0) AS GRNINCHECKING , ISNULL(VERSION_MANUALBILLNO,0) AS MANUALBILLNO, ISNULL(VERSION_ALLOWEWAYBILL,0) AS ALLOWEWAYBILL, ISNULL(VERSION_PRINTEWAYBILL,0) AS PRINTEWAYBILL, ISNULL(VERSION_ADDPROFITINCAPITAL,0) AS ADDPROFITINCAPITAL, ISNULL(VERSION_MANUALCNDN,0) AS MANUALCNDN, ISNULL(VERSION_MANUALGDNNO,0) AS MANUALGDNNO, ISNULL(VERSION_SALEAUTODISC,0) AS SALEAUTODISCOUNT, ISNULL(VERSION_ALLOWPACKINGSLIP,0) AS ALLOWPACKINGSLIP, ISNULL(VERSION_LOTSTATUSONMTRS,0) AS LOTSTATUSONMTRS, ISNULL(VERSION_SALEORDERONMTRS,0) AS SALEORDERONMTRS,  ISNULL(VERSION_SHOWJOBINLOTSTATUS,0) AS SHOWJOBINLOTSTATUS,  ISNULL(VERSION_GRIDLOTNO,0) AS GRIDLOTNO,  ISNULL(VERSION_ALLOWEINVOICE,0) AS ALLOWEINVOICE,  ISNULL(VERSION_TRANSPORTCOPYA4,0) AS TRANSPORTCOPYA4,  ISNULL(VERSION_CNDNA5,0) AS CNDNA5,  ISNULL(VERSION_ALLOWWHATSAPP,0) AS ALLOWWHATSAPP, ISNULL(VERSION_WHATSAPPACT,GETDATE()) AS WHATSAPPACT, ISNULL(VERSION_WHATSAPPAUTOCC,0) AS WHATSAPPAUTOCC, ISNULL(VERSION_FETCHRATEFROMCHALLAN,0) AS FETCHRATEFROMCHALLAN, ISNULL(VERSION_AUTOBROKERAGE,0) AS AUTOBROKERAGE, ISNULL(VERSION_YARNISSUEA5,0) AS YARNISSUEA5, ISNULL(VERSION_BANKFORCHQ,'') AS BANKFORCHQ, ISNULL(VERSION_ITEMWISESHADE,0) AS ITEMWISESHADE, ISNULL(VERSION_SHOWGDNCOLUMNS,0) AS SHOWGDNCOLUMNS, ISNULL(VERSION_CHECKBARCODE,0) AS CHECKBARCODE, ISNULL(VERSION_JOBOUTA5,0) AS JOBOUTA5, ISNULL(VERSION_TOPHEADER,0) AS TOPHEADER, ISNULL(VERSION_CENTREHEADER,1) AS CENTREHEADER, ISNULL(VERSION_SHOWSRNO,0) AS SHOWSRNO, ISNULL(VERSION_SHOWITEMDESIGN,0) AS SHOWITEMDESIGN, ISNULL(VERSION_SHOWSIGNONINVOICE,0) AS SHOWSIGNONINVOICE, ISNULL(VERSION_MANUALRECPACKQTY,0) AS MANUALRECPACKQTY, ISNULL(VERSION_PCSTOPCSDETAILS,0) AS PCSTOPCSDETAILS, ISNULL(VERSION_GODNAME,'')  AS GODNAME , ISNULL(VERSION_ALLOWMANUALJONO,0) AS ALLOWMANUALJONO, ISNULL(VERSION_CUTWHEN1,0) AS CHALLANCUTWHEN1, ISNULL(VERSION_CUT,0) AS CHALLANCUT, ISNULL(VERSION_CATALOGPATH,'') AS CATALOGPATH, ISNULL(VERSION_CATALOGIP,'') AS CATALOGIP ", "", " VERSION", "")
            If DT.Rows.Count > 0 Then
                REPORTTYPE = DT.Rows(0).Item("REPORTTYPE")
                ClientName = DT.Rows(0).Item("CLIENTNAME")
                ALLOWBARCODEPRINT = Convert.ToBoolean(DT.Rows(0).Item("ALLOWBARCODE"))
                If Convert.ToBoolean(DT.Rows(0).Item("INVLINEGST")) = True Then INVOICESCREENTYPE = "LINE GST" Else INVOICESCREENTYPE = "TOTAL GST"
                If Convert.ToBoolean(DT.Rows(0).Item("PURLINEGST")) = True Then PURCHASESCREENTYPE = "LINE GST" Else PURCHASESCREENTYPE = "TOTAL GST"
                ALLOWSMS = Convert.ToBoolean(DT.Rows(0).Item("ALLOWSMS"))
                ALLOWMANUALINVNO = Convert.ToBoolean(DT.Rows(0).Item("MANUALINVNO"))
                FETCHITEMWISEDESIGN = Convert.ToBoolean(DT.Rows(0).Item("ITEMWISEDESIGN"))
                FETCHGRNINCHECKING = Convert.ToBoolean(DT.Rows(0).Item("GRNINCHECKING"))
                ALLOWMANUALBILLNO = Convert.ToBoolean(DT.Rows(0).Item("MANUALBILLNO"))
                ALLOWEWAYBILL = Convert.ToBoolean(DT.Rows(0).Item("ALLOWEWAYBILL"))
                PRINTEWAYBILL = Convert.ToBoolean(DT.Rows(0).Item("PRINTEWAYBILL"))
                ADDPROFITINCAPITAL = Convert.ToBoolean(DT.Rows(0).Item("ADDPROFITINCAPITAL"))
                ALLOWMANUALCNDN = Convert.ToBoolean(DT.Rows(0).Item("MANUALCNDN"))
                ALLOWMANUALGDNNO = Convert.ToBoolean(DT.Rows(0).Item("MANUALGDNNO"))
                SALEAUTODISCOUNT = Convert.ToBoolean(DT.Rows(0).Item("SALEAUTODISCOUNT"))
                ALLOWPACKINGSLIP = Convert.ToBoolean(DT.Rows(0).Item("ALLOWPACKINGSLIP"))
                LOTSTATUSONMTRS = Convert.ToBoolean(DT.Rows(0).Item("LOTSTATUSONMTRS"))
                SALEORDERONMTRS = Convert.ToBoolean(DT.Rows(0).Item("SALEORDERONMTRS"))
                SHOWJOBINLOTSTATUS = Convert.ToBoolean(DT.Rows(0).Item("SALEORDERONMTRS"))
                GRIDLOTNO = Convert.ToBoolean(DT.Rows(0).Item("GRIDLOTNO"))
                ALLOWEINVOICE = Convert.ToBoolean(DT.Rows(0).Item("ALLOWEINVOICE"))
                TRANSPORTCOPYA4 = Convert.ToBoolean(DT.Rows(0).Item("TRANSPORTCOPYA4"))
                CNDNA5 = Convert.ToBoolean(DT.Rows(0).Item("CNDNA5"))
                ALLOWWHATSAPP = Convert.ToBoolean(DT.Rows(0).Item("ALLOWWHATSAPP"))
                If ALLOWWHATSAPP = True Then WHATSAPPEXPDATE = Convert.ToDateTime(DT.Rows(0).Item("WHATSAPPACT")).Date.AddYears(1) Else WHATSAPPEXPDATE = Now.Date
                WHATSAPPAUTOCC = Convert.ToBoolean(DT.Rows(0).Item("WHATSAPPAUTOCC"))
                FETCHRATEFROMCHALLAN = Convert.ToBoolean(DT.Rows(0).Item("FETCHRATEFROMCHALLAN"))
                AUTOBROKERAGE = Convert.ToBoolean(DT.Rows(0).Item("AUTOBROKERAGE"))
                YARNISSUEA5 = Convert.ToBoolean(DT.Rows(0).Item("YARNISSUEA5"))
                BANKFORCHQPRINT = DT.Rows(0).Item("BANKFORCHQ")
                FETCHITEMWISESHADE = Convert.ToBoolean(DT.Rows(0).Item("ITEMWISESHADE"))
                SHOWGDNCOLUMNS = Convert.ToBoolean(DT.Rows(0).Item("SHOWGDNCOLUMNS"))
                CHECKBARCODEERRORVALID = Convert.ToBoolean(DT.Rows(0).Item("CHECKBARCODE"))
                JOBOUTA5 = Convert.ToBoolean(DT.Rows(0).Item("JOBOUTA5"))
                INVTOPHEADER = Convert.ToBoolean(DT.Rows(0).Item("TOPHEADER"))
                INVCENTREHEADER = Convert.ToBoolean(DT.Rows(0).Item("CENTREHEADER"))
                INVSHOWSRNO = Convert.ToBoolean(DT.Rows(0).Item("SHOWSRNO"))
                INVSHOWITEMDESIGN = Convert.ToBoolean(DT.Rows(0).Item("SHOWITEMDESIGN"))
                SHOWSIGNONINVOICE = Convert.ToBoolean(DT.Rows(0).Item("SHOWSIGNONINVOICE"))
                MANUALRECPACKQTY = Convert.ToBoolean(DT.Rows(0).Item("MANUALRECPACKQTY"))
                PCSTOPCSDETAILS = Convert.ToBoolean(DT.Rows(0).Item("PCSTOPCSDETAILS"))
                GODNAME = DT.Rows(0).Item("GODNAME")

                ALLOWMANUALJONO = Convert.ToBoolean(DT.Rows(0).Item("ALLOWMANUALJONO"))
                CHALLANCUTWHEN1 = Convert.ToBoolean(DT.Rows(0).Item("CHALLANCUTWHEN1"))
                CHALLANCUT = Convert.ToBoolean(DT.Rows(0).Item("CHALLANCUT"))
                CATALOGPATH = DT.Rows(0).Item("CATALOGPATH")
                CATALOGIP = DT.Rows(0).Item("CATALOGIP")



                HIDEACCOUNTSEXCEPTINVOICE = False
                HIDEACCOUNTS = False
                HIDESTOCK = False
                HIDEYARN = True
                HIDEAGENCY = True
                HIDESTORES = True
                HIDEPAYROLL = True
                HIDESAMPLEMODULE = True
                HIDEDYEINGPROGRAM = True
                DISCONTINUECLIENT = False
                HIDEPOSTER = True
                ITEMCOSTCENTRE = False


                If ClientName = "ABHEE" Then  '(ABHISHEK BHAI - AHMEDABAD)
                    HIDEAGENCY = False
                    If Now.Date > DateTime.Parse("15.06.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "AKASHDEEP" Then
                    DISCONTINUECLIENT = True
                    'HIDEYARN = False
                    'HIDEACCOUNTS = True
                    'If Now.Date > DateTime.Parse("15.09.2024 00:00") Then
                    '    Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                    'GoTo LINE1
                    'End If
                ElseIf ClientName = "ALENCOT" Then  '(KUMAR BHAI)
                    If Now.Date > DateTime.Parse("15.06.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "AMAN" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "ANMOL" Then
                    If Now.Date > DateTime.Parse("15.09.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "ANANT" Then
                    If Now.Date > DateTime.Parse("15.09.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "ANOX" Then
                    DISCONTINUECLIENT = True
                    'HIDESAMPLEMODULE = False
                    'HIDECATALOG = False
                    'HIDEDYEINGPROGRAM = False
                    'HIGHVERSION = True
                    'If Now.Date > DateTime.Parse("15.05.2024 00:00") Then
                    '    Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                    '    GoTo LINE1
                    'End If
                ElseIf ClientName = "ARIHANT" Then  '(MAHIPAL BHAI)
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "ARIHANTGARMENTS" Then  '(HITESH CHAPLOT)
                    If Now.Date > DateTime.Parse("15.05.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "ARPITA" Then
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "AFW" Then  '(ASHWAMEGH FASHION WORLD) BHARATJI AHMEDABAD
                    If Now.Date > DateTime.Parse("15.06.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "AVIS" Then
                    HIDECATALOG = False
                    HIDEPAYROLL = False
                    HIDEDYEINGPROGRAM = False
                    ITEMCOSTCENTRE = True
                    HIGHVERSION = True
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "AXIS" Then
                    DISCONTINUECLIENT = True
                    'If Now.Date > DateTime.Parse("15.01.2024 00:00") Then
                    '    Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                    '    GoTo LINE1
                    'End If
                ElseIf ClientName = "BALAJI" Then
                    HIDEACCOUNTS = True
                    If Now.Date > DateTime.Parse("15.05.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "BARKHA" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "BIGAPPLE" Then     '(DARSHAN JAIN)
                    HIDEGREY = True
                    HIDEACCOUNTS = True
                    If Now.Date > DateTime.Parse("15.06.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "BRILLANTO" Then
                    DISCONTINUECLIENT = True
                    'If Now.Date > DateTime.Parse("15.07.2022 00:00") Then
                    '    Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                    '    GoTo LINE1
                    'End If
                ElseIf ClientName = "CC" Then
                    If Now.Date > DateTime.Parse("15.09.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "C3" Then
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "CHANDRA" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "CHINTAN" Then  '(MANISH CHANDARIA)
                    HIDEYARN = False
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "DAKSH" Then
                    If Now.Date > DateTime.Parse("15.05.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "DEMO" Then
                    'HIDESAMPLEMODULE = False
                    'HIDECATALOG = False
                    'HIDEDYEINGPROGRAM = False
                    'HIGHVERSION = True
                    If Now.Date > DateTime.Parse("02.02.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "DETLINE" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "DEVEN" Then
                    If Now.Date > DateTime.Parse("15.10.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "DILIP" Then    '(VIRAJ INTERNATIONAL) --- YASH JAIN
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "DILIPNEW" Then     '(DILIP TEXTILES) -- DILIP BHAI
                    If Now.Date > DateTime.Parse("15.11.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "DJIMPEX" Then
                    HIDEACCOUNTS = True
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "DRDRAPES" Then
                    ALLOWDIGITALSIGN = True
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "GELATO" Then
                    HIDETALLYDATAEXPORT = False
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.12.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "HRITI" Then    '(MEHUL JAIN)
                    HIDETALLYDATAEXPORT = False
                    If Now.Date > DateTime.Parse("15.06.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "INDRANI" Then
                    DISCONTINUECLIENT = True
                    'HIDEACCOUNTS = True
                    'If Now.Date > DateTime.Parse("15.02.2023 00:00") Then
                    '    Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                    '    GoTo LINE1
                    'End If
                ElseIf ClientName = "INDRAPUJAFABRICS" Then
                    HIDESAMPLEMODULE = False
                    HIDEDYEINGPROGRAM = False
                    HIGHVERSION = True
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "INDRAPUJAIMPEX" Then
                    HIDESAMPLEMODULE = False
                    HIDEDYEINGPROGRAM = False
                    HIGHVERSION = True
                    If Now.Date > DateTime.Parse("15.11.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "JAGDAMBA" Then
                    HIDESAMPLEMODULE = False
                    HIDEDYEINGPROGRAM = False
                    HIDEPAYROLL = False
                    HIGHVERSION = True
                    If Now.Date > DateTime.Parse("15.06.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "KARAN" Then
                    HIDESAMPLEMODULE = False
                    HIDEYARN = False
                    HIDEDYEINGPROGRAM = False
                    If Now.Date > DateTime.Parse("15.11.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "KCRAYON" Then
                    HIDEDYEINGPROGRAM = False
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "KDFAB" Then
                    HIDESAMPLEMODULE = False
                    HIDECATALOG = False
                    If Now.Date > DateTime.Parse("15.07.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "KENCOT" Then
                    HIDEACCOUNTSEXCEPTINVOICE = True
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "KEMLINO" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "KOTHARI" Then  '(VENIL BHAI)
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "KOTHARINEW" Then   '(MAHENDRA BHAI)
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.11.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "KRFABRICS" Then    '(ABHISHEK BAFNA)
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.05.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "KRISHNA" Then
                    HIDESAMPLEMODULE = False
                    HIDESTORES = False
                    HIDECATALOG = False
                    If Now.Date > DateTime.Parse("15.12.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "KREEVE" Then   '(KARTIK BHAI)
                    HIDESTORES = False
                    If Now.Date > DateTime.Parse("15.04.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "KUNAL" Then    '(PREM CHAPPARWAL)
                    If Now.Date > DateTime.Parse("15.05.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "LEEFABRICO" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MAFATLAL" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MAHAJAN" Then
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MAHAVIR" Then      '(SANJAY JAIN)
                    If Now.Date > DateTime.Parse("15.09.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MAHAVIRPOLYCOT" Then
                    HIDESAMPLEMODULE = False
                    HIDECATALOG = False
                    HIDEDYEINGPROGRAM = False
                    HIGHVERSION = True
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MANINATH" Then '(MUKESH BHAI)
                    HIDEACCOUNTS = True
                    If Now.Date > DateTime.Parse("15.07.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MANISH" Then   '(DHRUV DESAI)
                    HIDEGREY = True
                    HIDEACCOUNTSEXCEPTINVOICE = True
                    If Now.Date > DateTime.Parse("15.12.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MANMANDIR" Then
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.01.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MANS" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MANSI" Then    '(KUNAL JETHA)
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MARKIN" Then
                    HIDEYARN = False
                    If Now.Date > DateTime.Parse("15.07.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MASHOK" Then
                    HIDEYARN = False
                    If Now.Date > DateTime.Parse("15.05.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MBB" Then
                    If Now.Date > DateTime.Parse("15.10.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MINALFAB" Then
                    If Now.Date > DateTime.Parse("15.06.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MITESHBHAI" Then   '(MITESH KISHANLALJI)
                    If Now.Date > DateTime.Parse("15.04.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MNARESH" Then
                    HIGHVERSION = True
                    HIDESAMPLEMODULE = False
                    HIDEPAYROLL = False
                    HIDESTORES = False
                    HIDECATALOG = False
                    HIDEDYEINGPROGRAM = False
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MNIKHIL" Then  '(JIGNESH SAKARIA)
                    HIDESAMPLEMODULE = False
                    HIDEACCOUNTSEXCEPTINVOICE = True
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MOHAN" Then    '(RAVI KEWALAMANI)
                    DISCONTINUECLIENT = True
                    'HIDEYARN = False
                    'If Now.Date > DateTime.Parse("15.05.2024 00:00") Then
                    '    Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                    '    GoTo LINE1
                    'End If
                ElseIf ClientName = "MOHATUL" Then
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MOMAI" Then
                    HIDESTORES = False
                    If Now.Date > DateTime.Parse("15.11.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MONOGRAM" Then
                    If Now.Date > DateTime.Parse("15.11.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MOOLTEX" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MSANCHITKUMAR" Then
                    If Now.Date > DateTime.Parse("15.07.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MVIKASKUMAR" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "MYCOT" Then
                    If Now.Date > DateTime.Parse("15.07.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "NAKODAINFOTECH" Then
                    HIDECATALOG = False
                    HIDESAMPLEMODULE = False
                    HIGHVERSION = True
                    HIDESTORES = False
                    HIDEPAYROLL = False
                    HIDEDYEINGPROGRAM = False
                    If Now.Date > DateTime.Parse("15.04.2035 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "NAMOCOT" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "NAYRA" Then
                    DISCONTINUECLIENT = True
                    'HIDEYARN = False
                    'If Now.Date > DateTime.Parse("15.04.2024 00:00") Then
                    '    Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                    '    GoTo LINE1
                    'End If
                ElseIf ClientName = "NVAHAN" Then
                    DISCONTINUECLIENT = True
                    'If Now.Date > DateTime.Parse("15.04.2022 00:00") Then
                    '    Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                    '    GoTo LINE1
                    'End If
                ElseIf ClientName = "NIRAJ" Then
                    If Now.Date > DateTime.Parse("15.05.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "NTC" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "PARAMOUNT" Then
                    HIDEACCOUNTS = True
                    If Now.Date > DateTime.Parse("15.11.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "PARAS" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "PARTOBA" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "POONAM" Then   '(RAMESH BHAI BHAVANI)
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "POOJA" Then
                    If Now.Date > DateTime.Parse("15.01.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "PURPLE" Then
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "RADHA" Then
                    If Now.Date > DateTime.Parse("15.07.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If

                ElseIf ClientName = "RAJDEEP" Then    '(RISHABH JAIN)
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If

                ElseIf ClientName = "RAJKRIPA" Then
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.12.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "RATAN" Then    '(JIGNESH SOTTANY)
                    If Now.Date > DateTime.Parse("15.06.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "REALCORPORATION" Then
                    HIGHVERSION = True
                    HIDECATALOG = False
                    HIDESTORES = False
                    HIDESAMPLEMODULE = False
                    HIDEDYEINGPROGRAM = False
                    HIDEPAYROLL = False
                    If Now.Date > DateTime.Parse("15.03.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "REGALIA" Then
                    If Now.Date > DateTime.Parse("15.11.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "REVAANT" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "RMANILAL" Then
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.07.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "RUCHITA" Then
                    HIDEACCOUNTS = True
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.11.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SAFFRON" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SAKARIA" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SANGHVI" Then
                    HIDESAMPLEMODULE = False
                    HIDECATALOG = False
                    HIDETALLYDATAEXPORT = False
                    If Now.Date > DateTime.Parse("15.07.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SARAYU" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SBA" Then
                    If Now.Date > DateTime.Parse("15.05.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SHAILESHTRADING" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SHALIBHADRA" Then  '(PUNYAM CREATION) --- VIJAY BHAI
                    If Now.Date > DateTime.Parse("15.05.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SHANTI" Then
                    If Now.Date > DateTime.Parse("15.12.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SHASHWAT" Then
                    If Now.Date > DateTime.Parse("15.06.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SHREENAKODA" Then  '(MEHUL JAIN)
                    HIDESAMPLEMODULE = False
                    HIDECATALOG = False
                    If Now.Date > DateTime.Parse("15.09.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SHREEVALLABH" Then     '(RAJKAMAL | ROYAL) -- AKSHAY JAIN VALLABH
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SHUBHI" Then
                    If Now.Date > DateTime.Parse("15.09.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SIDDHIRAJ" Then    '(APIL BHAI)
                    If Now.Date > DateTime.Parse("15.06.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SIDDHGIRI" Then    '(AKSHIT BHAI)
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SIDDHPOLYCOT" Then  '(KUMAR BHAI)
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SIMPLEX" Then
                    If Now.Date > DateTime.Parse("15.10.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SKF" Then
                    HIDEYARN = False
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SMS" Then 'SHAH MUKESHKUMAR SHANTILAL
                    If Now.Date > DateTime.Parse("15.07.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SMT" Then 'SHREE MANIRATNA TEXTILES
                    If Now.Date > DateTime.Parse("15.09.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If

                ElseIf ClientName = "SNCM" Then 'SHREE NAKODA COTTON MILLS AHMEDABAD (SHUBHAM)
                    HIGHVERSION = True
                    HIDECATALOG = False
                    HIDESTORES = False
                    HIDESAMPLEMODULE = False
                    HIDEDYEINGPROGRAM = False
                    HIDEPAYROLL = False
                    If Now.Date > DateTime.Parse("15.02.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If

                ElseIf ClientName = "SOFTAS" Then
                    HIDEPOSTER = False
                    If Now.Date > DateTime.Parse("15.06.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SONAL" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SONU" Then
                    HIDESAMPLEMODULE = False
                    If Now.Date > DateTime.Parse("15.02.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SPCORP" Then
                    HIDESAMPLEMODULE = False
                    HIDEYARN = False
                    HIDEDYEINGPROGRAM = False
                    HIGHVERSION = True
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SSC" Then 'SUBHASH SALES CORPORATION
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SST" Then 'SHREE SIDDHIVINAY TEXTILES (INDRAJEET AND VIJAY)
                    HIDEACCOUNTS = True
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SUCCESS" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SUBHLAXMI" Then    '(RISHABH AGARWAL)
                    If Now.Date > DateTime.Parse("15.07.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SUPEEMA" Then
                    HIDESAMPLEMODULE = False
                    HIDECATALOG = False
                    HIDEDYEINGPROGRAM = False
                    HIGHVERSION = True
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SUPRIYA" Then
                    HIDEDYEINGPROGRAM = False
                    HIDESAMPLEMODULE = False
                    HIDEPAYROLL = False
                    HIGHVERSION = True
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SURYODAYA" Then
                    HIDEDYEINGPROGRAM = False
                    HIDESAMPLEMODULE = False
                    HIDECATALOG = False
                    HIGHVERSION = True
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "SVS" Then
                    HIDEACCOUNTSEXCEPTINVOICE = True
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "TARUN" Then
                    If Now.Date > DateTime.Parse("15.10.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "TCOT" Then
                    If Now.Date > DateTime.Parse("15.06.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If

                ElseIf ClientName = "TEXTILEINDIA" Then
                    HIDEGREY = True
                    HIDEACCOUNTS = True
                    If Now.Date > DateTime.Parse("15.08.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "TINUMINU" Then
                    HIDESTORES = False
                    If Now.Date > DateTime.Parse("15.05.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "VAISHALI" Then '(MIHIR BHAI)
                    DISCONTINUECLIENT = True
                    'HIDEYARN = False
                    'HIDEACCOUNTS = True
                    'HIDESTORES = False
                    'If Now.Date > DateTime.Parse("15.12.2024 00:00") Then
                    '    Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                    '    GoTo LINE1
                    'End If
                ElseIf ClientName = "VALIANT" Then
                    HIDEYARN = False
                    HIGHVERSION = True
                    If Now.Date > DateTime.Parse("01.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "VINAYAK" Then
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "VINIT" Then    '(LALIT KOTHARI)
                    DISCONTINUECLIENT = True
                    'If Now.Date > DateTime.Parse("15.11.2025 00:00") Then
                    '    Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                    '    GoTo LINE1
                    'End If
                ElseIf ClientName = "VINTAGEINDIA" Then
                    HIDESAMPLEMODULE = False
                    HIDECATALOG = False
                    HIDEDYEINGPROGRAM = False
                    HIDESTORES = False
                    HIDEACCOUNTSEXCEPTINVOICE = True
                    HIGHVERSION = True
                    HIDEPOSTER = False
                    If Now.Date > DateTime.Parse("15.02.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "VSTRADERS" Then
                    If Now.Date > DateTime.Parse("15.09.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "YASHVI" Then
                    HIDESTORES = False
                    HIDEDYEINGPROGRAM = False
                    HIDECATALOG = False
                    HIDESAMPLEMODULE = False
                    HIGHVERSION = True
                    HIDEPAYROLL = False
                    If Now.Date > DateTime.Parse("15.04.2026 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                ElseIf ClientName = "YUMILONE" Then
                    If Now.Date > DateTime.Parse("15.10.2025 00:00") Then
                        Dim DTNEW As DataTable = OBJCMN.Execute_Any_String("UPDATE VERSION SET VERSION_NO='1.0.0000'", "", "")
                        GoTo LINE1
                    End If
                Else
                    GoTo LINE1
                End If

                If DT.Rows(0).Item("VERSION") <> "1.0.094" Then
                    MsgBox("Please Install New Version", MsgBoxStyle.Critical)
LINE1:
                    MsgBox(" VERSION EXPIRED PLEASE CONTACT NAKODA INFOTECH ON 02249724411", MsgBoxStyle.Critical)
                    End
                End If
            Else
                'IF CLIENTNAME IS NOT PRESENT
                End
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtpassword_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpassword.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If txtusername.Text.Trim <> "" And txtpassword.Text.Trim <> "" Then Call cmdok_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Login_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "dd/MM/yyyy")
            SHOWHIDDENCMP = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try

            'FOR EXCEL MIS (AVIS)
            If TimeOfDay.Hour = 20 And TimeOfDay.Minute = 30 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" VERSION_NO AS VERSION, VERSION_CLIENTNAME AS CLIENTNAME, VERSION_REPORTTYPE AS REPORTTYPE, ISNULL(VERSION_ALLOWBARCODE,0)  AS ALLOWBARCODE, ISNULL(VERSION_INVOICELINEGST,0) AS INVLINEGST, ISNULL(VERSION_PURCHASELINEGST,0) AS PURLINEGST, ISNULL(VERSION_ALLOWSMS,0) AS ALLOWSMS, ISNULL(VERSION_MANUALINVNO,0) AS MANUALINVNO, ISNULL(VERSION_ITEMWISEDESIGN,0) AS ITEMWISEDESIGN, ISNULL(VERSION_GRNINCHECKING,0) AS GRNINCHECKING , ISNULL(VERSION_MANUALBILLNO,0) AS MANUALBILLNO, ISNULL(VERSION_ALLOWEWAYBILL,0) AS ALLOWEWAYBILL, ISNULL(VERSION_PRINTEWAYBILL,0) AS PRINTEWAYBILL, ISNULL(VERSION_ADDPROFITINCAPITAL,0) AS ADDPROFITINCAPITAL, ISNULL(VERSION_MANUALCNDN,0) AS MANUALCNDN, ISNULL(VERSION_MANUALGDNNO,0) AS MANUALGDNNO, ISNULL(VERSION_SALEAUTODISC,0) AS SALEAUTODISCOUNT, ISNULL(VERSION_ALLOWPACKINGSLIP,0) AS ALLOWPACKINGSLIP, ISNULL(VERSION_LOTSTATUSONMTRS,0) AS LOTSTATUSONMTRS, ISNULL(VERSION_SALEORDERONMTRS,0) AS SALEORDERONMTRS,  ISNULL(VERSION_SHOWJOBINLOTSTATUS,0) AS SHOWJOBINLOTSTATUS,  ISNULL(VERSION_GRIDLOTNO,0) AS GRIDLOTNO", "", " VERSION", "")
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("CLIENTNAME") = "AVIS" Then

                    DT = OBJCMN.search("TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMP_DISPLAYEDNAME = 'AVIS INDUSTRIES PVT. LTD.' ORDER BY YEAR_STARTDATE DESC")
                    If DT.Rows.Count > 0 Then
                        CmpId = DT.Rows(0).Item("CMPID")
                        YearId = DT.Rows(0).Item("YEARID")
                    End If

                    Dim OBJRPT As New clsReportDesigner("MIS Report", System.AppDomain.CurrentDomain.BaseDirectory & "MIS Report.xlsx", 0)
                    OBJRPT.MISALLDAILY_EXCEL(CmpId, YearId, Now.Date, Now.Date)
                    Dim ALATTACHMENT As New ArrayList
                    ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "MIS Report.xlsx")
                    sendemail("rm@avisindustries.in,gm@avisindustries.in,aroraaoc@gmail.com,infoavisindustries@gmail.com", ALATTACHMENT(0), "MIS Report", "MIS REPORT AS ON " & Format(Now.Date, "dd/MM/yyyy"), ALATTACHMENT, ALATTACHMENT.Count, "", "", "", "", "")
                    End
                End If
            End If



            'FOR EXCEL MIS (YASHVI)
            If TimeOfDay.Hour = 20 And TimeOfDay.Minute = 5 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" VERSION_NO AS VERSION, VERSION_CLIENTNAME AS CLIENTNAME, VERSION_REPORTTYPE AS REPORTTYPE, ISNULL(VERSION_ALLOWBARCODE,0)  AS ALLOWBARCODE, ISNULL(VERSION_INVOICELINEGST,0) AS INVLINEGST, ISNULL(VERSION_PURCHASELINEGST,0) AS PURLINEGST, ISNULL(VERSION_ALLOWSMS,0) AS ALLOWSMS, ISNULL(VERSION_MANUALINVNO,0) AS MANUALINVNO, ISNULL(VERSION_ITEMWISEDESIGN,0) AS ITEMWISEDESIGN, ISNULL(VERSION_GRNINCHECKING,0) AS GRNINCHECKING , ISNULL(VERSION_MANUALBILLNO,0) AS MANUALBILLNO, ISNULL(VERSION_ALLOWEWAYBILL,0) AS ALLOWEWAYBILL, ISNULL(VERSION_PRINTEWAYBILL,0) AS PRINTEWAYBILL, ISNULL(VERSION_ADDPROFITINCAPITAL,0) AS ADDPROFITINCAPITAL, ISNULL(VERSION_MANUALCNDN,0) AS MANUALCNDN, ISNULL(VERSION_MANUALGDNNO,0) AS MANUALGDNNO, ISNULL(VERSION_SALEAUTODISC,0) AS SALEAUTODISCOUNT, ISNULL(VERSION_ALLOWPACKINGSLIP,0) AS ALLOWPACKINGSLIP, ISNULL(VERSION_LOTSTATUSONMTRS,0) AS LOTSTATUSONMTRS, ISNULL(VERSION_SALEORDERONMTRS,0) AS SALEORDERONMTRS,  ISNULL(VERSION_SHOWJOBINLOTSTATUS,0) AS SHOWJOBINLOTSTATUS,  ISNULL(VERSION_GRIDLOTNO,0) AS GRIDLOTNO", "", " VERSION", "")
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("CLIENTNAME") = "YASHVI" Then

                    DT = OBJCMN.search("TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMP_DISPLAYEDNAME = 'YASHVI CREATION' ORDER BY YEAR_STARTDATE DESC")
                    If DT.Rows.Count > 0 Then
                        CmpId = DT.Rows(0).Item("CMPID")
                        YearId = DT.Rows(0).Item("YEARID")
                    End If

                    Dim OBJRPT As New clsReportDesigner("MIS Report", System.AppDomain.CurrentDomain.BaseDirectory & "MIS Report.xlsx", 0)
                    OBJRPT.MISALLDAILY_EXCEL(CmpId, YearId, Now.Date, Now.Date)
                    Dim ALATTACHMENT As New ArrayList
                    ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "MIS Report.xlsx")
                    sendemail("arihantexim@yahoo.co.in", ALATTACHMENT(0), "MIS Report", "MIS REPORT AS ON " & Format(Now.Date, "dd/MM/yyyy"), ALATTACHMENT, ALATTACHMENT.Count, "", "", "", "", "")
                    End
                End If
            End If




            'FOR EXCEL MIS (MAHAVIRPOLYCOT)
            If TimeOfDay.Hour = 22 And TimeOfDay.Minute = 0 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" VERSION_NO AS VERSION, VERSION_CLIENTNAME AS CLIENTNAME, VERSION_REPORTTYPE AS REPORTTYPE, ISNULL(VERSION_ALLOWBARCODE,0)  AS ALLOWBARCODE, ISNULL(VERSION_INVOICELINEGST,0) AS INVLINEGST, ISNULL(VERSION_PURCHASELINEGST,0) AS PURLINEGST, ISNULL(VERSION_ALLOWSMS,0) AS ALLOWSMS, ISNULL(VERSION_MANUALINVNO,0) AS MANUALINVNO, ISNULL(VERSION_ITEMWISEDESIGN,0) AS ITEMWISEDESIGN, ISNULL(VERSION_GRNINCHECKING,0) AS GRNINCHECKING , ISNULL(VERSION_MANUALBILLNO,0) AS MANUALBILLNO, ISNULL(VERSION_ALLOWEWAYBILL,0) AS ALLOWEWAYBILL, ISNULL(VERSION_PRINTEWAYBILL,0) AS PRINTEWAYBILL, ISNULL(VERSION_ADDPROFITINCAPITAL,0) AS ADDPROFITINCAPITAL, ISNULL(VERSION_MANUALCNDN,0) AS MANUALCNDN, ISNULL(VERSION_MANUALGDNNO,0) AS MANUALGDNNO, ISNULL(VERSION_SALEAUTODISC,0) AS SALEAUTODISCOUNT, ISNULL(VERSION_ALLOWPACKINGSLIP,0) AS ALLOWPACKINGSLIP, ISNULL(VERSION_LOTSTATUSONMTRS,0) AS LOTSTATUSONMTRS, ISNULL(VERSION_SALEORDERONMTRS,0) AS SALEORDERONMTRS,  ISNULL(VERSION_SHOWJOBINLOTSTATUS,0) AS SHOWJOBINLOTSTATUS,  ISNULL(VERSION_GRIDLOTNO,0) AS GRIDLOTNO", "", " VERSION", "")
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("CLIENTNAME") = "MAHAVIRPOLYCOT" Then

                    DT = OBJCMN.search("TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMP_DISPLAYEDNAME = 'MAHAVIR POLYCOT' ORDER BY YEAR_STARTDATE DESC")
                    If DT.Rows.Count > 0 Then
                        CmpId = DT.Rows(0).Item("CMPID")
                        YearId = DT.Rows(0).Item("YEARID")
                    End If

                    Dim OBJRPT As New clsReportDesigner("MIS Report", System.AppDomain.CurrentDomain.BaseDirectory & "MIS Report.xlsx", 0)
                    OBJRPT.MISALLDAILY_EXCEL(CmpId, YearId, Now.Date, Now.Date)
                    Dim ALATTACHMENT As New ArrayList
                    ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "MIS Report.xlsx")
                    sendemail("mastercot@yahoo.in,ankitrshah81@yahoo.com,rpshah6012@gmail.com", ALATTACHMENT(0), "MIS Report", "MIS REPORT AS ON " & Format(Now.Date, "dd/MM/yyyy"), ALATTACHMENT, ALATTACHMENT.Count, "", "", "", "", "")
                    End
                End If
            End If



            'FOR DAILY DISPATCH (AVIS)
            If TimeOfDay.Hour = 22 And TimeOfDay.Minute = 30 Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" VERSION_NO AS VERSION, VERSION_CLIENTNAME AS CLIENTNAME, VERSION_REPORTTYPE AS REPORTTYPE, ISNULL(VERSION_ALLOWBARCODE,0)  AS ALLOWBARCODE, ISNULL(VERSION_INVOICELINEGST,0) AS INVLINEGST, ISNULL(VERSION_PURCHASELINEGST,0) AS PURLINEGST, ISNULL(VERSION_ALLOWSMS,0) AS ALLOWSMS, ISNULL(VERSION_MANUALINVNO,0) AS MANUALINVNO, ISNULL(VERSION_ITEMWISEDESIGN,0) AS ITEMWISEDESIGN, ISNULL(VERSION_GRNINCHECKING,0) AS GRNINCHECKING , ISNULL(VERSION_MANUALBILLNO,0) AS MANUALBILLNO, ISNULL(VERSION_ALLOWEWAYBILL,0) AS ALLOWEWAYBILL, ISNULL(VERSION_PRINTEWAYBILL,0) AS PRINTEWAYBILL, ISNULL(VERSION_ADDPROFITINCAPITAL,0) AS ADDPROFITINCAPITAL, ISNULL(VERSION_MANUALCNDN,0) AS MANUALCNDN, ISNULL(VERSION_MANUALGDNNO,0) AS MANUALGDNNO, ISNULL(VERSION_SALEAUTODISC,0) AS SALEAUTODISCOUNT, ISNULL(VERSION_ALLOWPACKINGSLIP,0) AS ALLOWPACKINGSLIP, ISNULL(VERSION_LOTSTATUSONMTRS,0) AS LOTSTATUSONMTRS, ISNULL(VERSION_SALEORDERONMTRS,0) AS SALEORDERONMTRS,  ISNULL(VERSION_SHOWJOBINLOTSTATUS,0) AS SHOWJOBINLOTSTATUS,  ISNULL(VERSION_GRIDLOTNO,0) AS GRIDLOTNO", "", " VERSION", "")
                If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("CLIENTNAME") = "AVIS" Then

                    DT = OBJCMN.search("TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMP_DISPLAYEDNAME = 'AVIS INDUSTRIES PVT. LTD.' ORDER BY YEAR_STARTDATE DESC")
                    If DT.Rows.Count > 0 Then
                        CmpId = DT.Rows(0).Item("CMPID")
                        YearId = DT.Rows(0).Item("YEARID")
                    End If

                    Dim OBJRPT As New clsReportDesigner("Daily Dispatch Report", System.AppDomain.CurrentDomain.BaseDirectory & "Daily Dispatch Report.xlsx", 0)
                    OBJRPT.DAILYDISPATCH_EXCEL(CmpId, YearId, Now.Date, Now.Date)
                    Dim ALATTACHMENT As New ArrayList
                    ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "Daily Dispatch Report.xlsx")
                    sendemail("rm@avisindustries.in,gm@avisindustries.in,infoavisindustries@gmail.com", ALATTACHMENT(0), "Daily Dispatch Report", "DAILY DISPATCH REPORT AS ON " & Format(Now.Date, "dd/MM/yyyy"), ALATTACHMENT, ALATTACHMENT.Count, "", "", "", "", "")
                    End
                End If
            End If



            ''MULTICOMPANY OUTSTANDING (YASHVI)
            'If TimeOfDay.DayOfWeek = DayOfWeek.Monday And TimeOfDay.Hour = 10 And TimeOfDay.Minute = 30 Then
            '    Dim OBJCMN As New ClsCommon
            '    Dim DT As DataTable = OBJCMN.SEARCH(" VERSION_NO AS VERSION, VERSION_CLIENTNAME AS CLIENTNAME, VERSION_REPORTTYPE AS REPORTTYPE, ISNULL(VERSION_ALLOWBARCODE,0)  AS ALLOWBARCODE, ISNULL(VERSION_INVOICELINEGST,0) AS INVLINEGST, ISNULL(VERSION_PURCHASELINEGST,0) AS PURLINEGST, ISNULL(VERSION_ALLOWSMS,0) AS ALLOWSMS, ISNULL(VERSION_MANUALINVNO,0) AS MANUALINVNO, ISNULL(VERSION_ITEMWISEDESIGN,0) AS ITEMWISEDESIGN, ISNULL(VERSION_GRNINCHECKING,0) AS GRNINCHECKING , ISNULL(VERSION_MANUALBILLNO,0) AS MANUALBILLNO, ISNULL(VERSION_ALLOWEWAYBILL,0) AS ALLOWEWAYBILL, ISNULL(VERSION_PRINTEWAYBILL,0) AS PRINTEWAYBILL, ISNULL(VERSION_ADDPROFITINCAPITAL,0) AS ADDPROFITINCAPITAL, ISNULL(VERSION_MANUALCNDN,0) AS MANUALCNDN, ISNULL(VERSION_MANUALGDNNO,0) AS MANUALGDNNO, ISNULL(VERSION_SALEAUTODISC,0) AS SALEAUTODISCOUNT, ISNULL(VERSION_ALLOWPACKINGSLIP,0) AS ALLOWPACKINGSLIP, ISNULL(VERSION_LOTSTATUSONMTRS,0) AS LOTSTATUSONMTRS, ISNULL(VERSION_SALEORDERONMTRS,0) AS SALEORDERONMTRS,  ISNULL(VERSION_SHOWJOBINLOTSTATUS,0) AS SHOWJOBINLOTSTATUS,  ISNULL(VERSION_GRIDLOTNO,0) AS GRIDLOTNO", "", " VERSION", "")
            '    If DT.Rows.Count > 0 AndAlso DT.Rows(0).Item("CLIENTNAME") = "YASHVI" Then

            '        DT = OBJCMN.SEARCH("TOP 1 YEAR_CMPID AS CMPID, YEAR_ID AS YEARID", "", " YEARMASTER INNER JOIN CMPMASTER ON YEAR_CMPID = CMP_ID", " AND CMP_DISPLAYEDNAME = 'AVIS INDUSTRIES PVT. LTD.' ORDER BY YEAR_STARTDATE DESC")
            '        If DT.Rows.Count > 0 Then
            '            CmpId = DT.Rows(0).Item("CMPID")
            '            YearId = DT.Rows(0).Item("YEARID")
            '        End If

            '        Dim OBJRPT As New clsReportDesigner("Daily Dispatch Report", System.AppDomain.CurrentDomain.BaseDirectory & "Daily Dispatch Report.xlsx", 0)
            '        OBJRPT.DAILYDISPATCH_EXCEL(CmpId, YearId, Now.Date, Now.Date)
            '        Dim ALATTACHMENT As New ArrayList
            '        ALATTACHMENT.Add(System.AppDomain.CurrentDomain.BaseDirectory & "Daily Dispatch Report.xlsx")
            '        sendemail("rm@avisindustries.in,gm@avisindustries.in,infoavisindustries@gmail.com", ALATTACHMENT(0), "Daily Dispatch Report", "DAILY DISPATCH REPORT AS ON " & Format(Now.Date, "dd/MM/yyyy"), ALATTACHMENT, ALATTACHMENT.Count, "", "", "", "", "")
            '        End
            '    End If
            'End If






        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Login_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Timer1.Enabled = False
    End Sub
End Class
