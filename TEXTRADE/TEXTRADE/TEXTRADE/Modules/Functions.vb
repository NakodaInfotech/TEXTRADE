
Imports System.Net.Mail
Imports BL
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports System.Web
Imports System.Security
Imports WAProAPI

Module Functions

    Sub BARCODEPRINTING(BARCODE As String, PIECETYPE As String, ITEMNAME As String, QUALITY As String, DESIGNNO As String, SHADE As String, UNIT As String, LOTNO As String, BALENO As String, GRIDDESC As String, MTRS As Double, QTY As Double, CUT As Double, Optional RACK As String = "", Optional TEMPHEADER As String = "", Optional SUPRIYAHEADER As String = "", Optional WHOLESALEBARCODE As Integer = 0, Optional WEAVERCHNO As String = "", Optional WEAVERNAME As String = "", Optional SHELF As String = "", Optional ENTRYDATE As String = "")
        Try

            Dim dirresults As String = ""
            Dim oWrite As System.IO.StreamWriter
            oWrite = File.CreateText(Application.StartupPath & "\Barcode.txt")

            'IF CLIENT WANT TO PRINT ALL THE BARCODE THEN USE THE CODE BELOW
            If ClientName = "TARUN" Then GoTo PRINTALL

            If (PIECETYPE <> "FRESH" And ClientName <> "KENCOT" And ClientName <> "KARAN" And ClientName <> "SPCORP") Or ((ClientName = "KARAN" Or ClientName = "SPCORP") And PIECETYPE <> "FRESH" And PIECETYPE <> "TP") Then
                oWrite.Dispose()
                Exit Sub
            End If

PRINTALL:
            If ClientName = "SVS" Then
                oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q400")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q200,25")
                oWrite.WriteLine("KI80")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>N")
                oWrite.WriteLine("A376,160,2,2,1,1,N,""QUALITY""")
                oWrite.WriteLine("A376,114,2,2,1,1,N,""D.NO""")
                oWrite.WriteLine("A376,136,2,2,1,1,N,""SHADE""")
                oWrite.WriteLine("B384,91,2,1,2,4,61,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A279,24,2,2,1,1,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A197,114,2,2,1,1,N,""QTY""")
                oWrite.WriteLine("A376,183,2,2,1,1,N,""" & CmpName & """")    'cmpname
                oWrite.WriteLine("A277,114,2,2,1,1,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A291,114,2,2,1,1,N,"":""")
                oWrite.WriteLine("A291,136,2,2,1,1,N,"":""")
                oWrite.WriteLine("A277,136,2,2,1,1,N,""" & SHADE & """")
                oWrite.WriteLine("A291,162,2,2,1,1,N,"":""")
                oWrite.WriteLine("A277,162,2,2,1,1,N,""" & QUALITY & """")
                oWrite.WriteLine("A157,114,2,2,1,1,N,"":""")
                oWrite.WriteLine("A143,114,2,2,1,1,N,""" & Format(Val(MTRS), "0.00") & " " & UNIT & """")
                oWrite.WriteLine("P1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "KOTHARI" Then

                If TEMPHEADER = 1 Then
                    oWrite.WriteLine("SIZE 66.2 mm, 140 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")


                    oWrite.WriteLine("TEXT 511,52,""ROMAN.TTF"",90,1,18,""ITEM NAME""")
                    oWrite.WriteLine("TEXT 511,451,""ROMAN.TTF"",90,1,18,"":""")
                    oWrite.WriteLine("TEXT 511,503,""ROMAN.TTF"",90,1,18,""" & ITEMNAME & """")
                    oWrite.WriteLine("TEXT 418,52,""ROMAN.TTF"",90,1,18,""DESIGN NO""")
                    oWrite.WriteLine("TEXT 418,451,""ROMAN.TTF"",90,1,18,"":""")
                    oWrite.WriteLine("TEXT 418,503,""ROMAN.TTF"",90,1,18,""" & DESIGNNO & """")
                    oWrite.WriteLine("TEXT 325,52,""ROMAN.TTF"",90,1,18,""SHADE""")
                    oWrite.WriteLine("TEXT 325,451,""ROMAN.TTF"",90,1,18,"":""")
                    oWrite.WriteLine("TEXT 325,503,""ROMAN.TTF"",90,1,18,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 232,52,""ROMAN.TTF"",90,1,18,""WIDTH""")
                    oWrite.WriteLine("TEXT 232,451,""ROMAN.TTF"",90,1,18,"":""")


                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String = ""
                    Dim TEMPHSNCODE As String = ""
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT, ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                        TEMPHSNCODE = DT.Rows(0).Item("HSNCODE")
                    End If

                    oWrite.WriteLine("TEXT 232,503,""ROMAN.TTF"",90,1,18,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("TEXT 137,52,""ROMAN.TTF"",90,1,18,""LTH. (MTRS)""")
                    oWrite.WriteLine("TEXT 137,451,""ROMAN.TTF"",90,1,18,"":""")
                    oWrite.WriteLine("TEXT 137,499,""ROMAN.TTF"",90,1,18,""" & Format(Val(MTRS), "0.00") & GRIDDESC & """")
                    oWrite.WriteLine("TEXT 90,1241,""ROMAN.TTF"",90,1,10,""MADE IN INDIA""")
                    oWrite.WriteLine("TEXT 723,1142,""ROMAN.TTF"",90,1,10,""SKU" & Now.Day & Now.Month & Now.Year & Now.Hour & Now.Minute & Now.Second & "24KT" & """")
                    oWrite.WriteLine("QRCODE 606,1185,L,10,A,90,M2,S7,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 201,1262,""ROMAN.TTF"",90,1,9,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 129,1589,""ROMAN.TTF"",0,10,10,""HSN CODE : " & TEMPHSNCODE & """")
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()
                Else

                    oWrite.WriteLine("SIZE 66.2 mm, 140 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")


                    oWrite.WriteLine("TEXT 518,52,""ROMAN.TTF"",90,1,22,""" & ITEMNAME & """")
                    oWrite.WriteLine("TEXT 371,52,""ROMAN.TTF"",90,1,22,""" & DESIGNNO & """")
                    oWrite.WriteLine("TEXT 371,536,""ROMAN.TTF"",90,1,22,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 221,52,""ROMAN.TTF"",90,1,16,""WIDTH""")
                    oWrite.WriteLine("TEXT 221,287,""ROMAN.TTF"",90,1,16,"":""")

                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPHSNCODE As String = ""
                    Dim TEMPWIDTH As String = ""
                    Dim TEMPMRP As String = ""
                    Dim TEMPYEAR As String = ""
                    Dim TEMPMONTH As String = ""
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE, ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                        TEMPMRP = Val(DT.Rows(0).Item("RATE"))
                        TEMPHSNCODE = DT.Rows(0).Item("HSNCODE")
                    End If

                    oWrite.WriteLine("TEXT 221,338,""ROMAN.TTF"",90,1,16,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("TEXT 128,52,""ROMAN.TTF"",90,1,16,""QTY""")
                    oWrite.WriteLine("TEXT 128,287,""ROMAN.TTF"",90,1,16,"":""")
                    oWrite.WriteLine("TEXT 128,338,""ROMAN.TTF"",90,1,16,""" & Format(Val(CUT), "0.00") & """")
                    oWrite.WriteLine("TEXT 128,487,""ROMAN.TTF"",90,1,16,""X""")
                    oWrite.WriteLine("QRCODE 686,1185,Q,10,A,90,M2,S7,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 294,1226,""ROMAN.TTF"",90,1,12,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 128,555,""ROMAN.TTF"",90,1,16,""" & Format(Val(QTY), "0") & " Pcs""")
                    oWrite.WriteLine("TEXT 221,892,""ROMAN.TTF"",90,1,16,""M.R.P.""")
                    oWrite.WriteLine("TEXT 221,1161,""ROMAN.TTF"",90,1,16,"":""")
                    oWrite.WriteLine("TEXT 221,1207,""ROMAN.TTF"",90,1,16,""" & TEMPMRP & "/- PER PC""")
                    oWrite.WriteLine("TEXT 128,892,""ROMAN.TTF"",90,1,16,""PKG ON""")
                    oWrite.WriteLine("TEXT 128,1161,""ROMAN.TTF"",90,1,16,"":""")

                    'GET MONTH AND YEAR
                    DT = OBJCMN.SEARCH(" DATE ", "", " ALLBARCODESTOCK ", " AND BARCODE = '" & BARCODE & "' AND YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPMONTH = Format(Convert.ToDateTime(DT.Rows(0).Item("DATE")).Date, "MMM")
                        TEMPYEAR = Format(Convert.ToDateTime(DT.Rows(0).Item("DATE")).Date, "yyyy")
                    End If

                    oWrite.WriteLine("TEXT 128,1207,""ROMAN.TTF"",90,1,16,""" & UCase(TEMPMONTH) & " " & TEMPYEAR & """")
                    oWrite.WriteLine("TEXT 740,1142,""ROMAN.TTF"",90,1,10,""SKU" & Now.Day & Now.Month & Now.Year & Now.Hour & Now.Minute & Now.Second & "24KT" & """")
                    oWrite.WriteLine("TEXT 198,1589,""ROMAN.TTF"",0,10,10,""HSN CODE : " & TEMPHSNCODE & """")
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()

                End If


            ElseIf ClientName = "JAGDAMBA" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 758,359,""0"",180,16,16,""ITEM""")
                oWrite.WriteLine("TEXT 597,359,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 565,359,""0"",180,16,16,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 758,291,""0"",180,16,16,""D. NO""")
                oWrite.WriteLine("TEXT 597,291,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 565,291,""0"",180,16,16,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 758,223,""0"",180,16,16,""SHADE""")
                oWrite.WriteLine("TEXT 597,223,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 565,223,""0"",180,16,16,""" & SHADE & """")


                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 758,155,""0"",180,16,16,""WIDTH""")
                oWrite.WriteLine("TEXT 597,155,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 565,155,""0"",180,16,16,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 758,87,""0"",180,16,16,""MTRS""")
                oWrite.WriteLine("TEXT 597,87,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 565,87,""0"",180,16,16,""" & Format(Val(MTRS), "0.00") & """")

                oWrite.WriteLine("QRCODE 286,299,L,10,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 304,70,""0"",180,14,14,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "REALCORPORATION" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 748,375,""0"",180,20,20,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 748,289,""0"",180,16,16,""D. NO""")
                oWrite.WriteLine("TEXT 562,289,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 542,289,""0"",180,16,16,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 748,233,""0"",180,16,16,""SHADE""")
                oWrite.WriteLine("TEXT 562,233,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 542,233,""0"",180,16,16,""" & SHADE & """")
                oWrite.WriteLine("TEXT 748,120,""0"",180,16,16,""MTRS""")
                oWrite.WriteLine("TEXT 562,120,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 542,120,""0"",180,16,16,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("QRCODE 233,277,L,9,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 222,70,""0"",180,10,10,""" & BARCODE & """") 'BARCODE

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 748,64,""0"",180,16,16,""WIDTH""")
                oWrite.WriteLine("TEXT 562,64,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 542,64,""0"",180,16,16,""" & TEMPWIDTH & """")
                oWrite.WriteLine("BAR 31,308, 721, 3")
                oWrite.WriteLine("TEXT 748,177,""0"",180,16,16,""BALE NO""")
                oWrite.WriteLine("TEXT 562,177,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 542,177,""0"",180,16,16,""" & BALENO & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "SNCM" Then

                oWrite.WriteLine("SIZE 97.5 mm, 80.1 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 750,576,""ROMAN.TTF"",180,1,22,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 750,452,""ROMAN.TTF"",180,1,22,""B. No""")
                oWrite.WriteLine("TEXT 578,452,""ROMAN.TTF"",180,1,22,"":""")
                oWrite.WriteLine("TEXT 538,493,""ROMAN.TTF"",180,1,54,""" & BALENO & """")
                oWrite.WriteLine("TEXT 750,326,""ROMAN.TTF"",180,1,22,""D. NO""")
                oWrite.WriteLine("TEXT 578,326,""ROMAN.TTF"",180,1,22,"":""")
                oWrite.WriteLine("TEXT 538,326,""ROMAN.TTF"",180,1,22,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 750,165,""ROMAN.TTF"",180,1,22,""Mtrs""")
                oWrite.WriteLine("TEXT 578,165,""ROMAN.TTF"",180,1,22,"":""")
                oWrite.WriteLine("TEXT 538,165,""ROMAN.TTF"",180,1,22,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 750,84,""ROMAN.TTF"",180,1,22,""L. No""")
                oWrite.WriteLine("TEXT 578,84,""ROMAN.TTF"",180,1,22,"":""")
                oWrite.WriteLine("TEXT 538,84,""ROMAN.TTF"",180,1,22,""" & LOTNO & """")
                oWrite.WriteLine("TEXT 750,246,""ROMAN.TTF"",180,1,22,""Shade""")
                oWrite.WriteLine("TEXT 578,246,""ROMAN.TTF"",180,1,22,"":""")
                oWrite.WriteLine("TEXT 538,246,""ROMAN.TTF"",180,1,22,""" & SHADE & """")
                oWrite.WriteLine("QRCODE 259,319,L,9,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 248,120,""ROMAN.TTF"",180,1,9,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "MBB" Then

                oWrite.WriteLine("SIZE 72.5 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")


                oWrite.WriteLine("TEXT 491,329,""0"",180,11,11,""D. NO""")
                oWrite.WriteLine("TEXT 380,331,""0"",180,11,11,"":""")
                oWrite.WriteLine("TEXT 363,331,""0"",180,11,11,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 491,230,""0"",180,11,11,""YARDS""")
                oWrite.WriteLine("TEXT 380,232,""0"",180,11,11,"":""")


                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim TEMPRATE As Double
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If
                Dim DT1 As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_SALERATE, 0) AS RATE ", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & DESIGNNO & "' AND DESIGN_YEARID = " & YearId)
                If DT1.Rows.Count > 0 Then
                    TEMPRATE = Val(DT1.Rows(0).Item("RATE"))
                End If

                oWrite.WriteLine("TEXT 363,232,""0"",180,11,11,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 491,180,""0"",180,11,11,""RATE""")
                oWrite.WriteLine("TEXT 380,179,""0"",180,11,11,"":""")

                oWrite.WriteLine("TEXT 363,179,""0"",180,11,11,""" & Format(Val(TEMPRATE), "0.00") & """")
                oWrite.WriteLine("QRCODE 163,247,L,7,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 491,379,""0"",180,12,12,""" & ITEMNAME & """")
                oWrite.WriteLine("BAR 16, 342, 487, 4")
                oWrite.WriteLine("TEXT 491,280,""0"",180,11,11,""WIDTH""")
                oWrite.WriteLine("TEXT 380,281,""0"",180,11,11,"":""")
                oWrite.WriteLine("TEXT 363,281,""0"",180,11,11,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 403,132,""0"",180,10,10,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "REGALIA" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")


                oWrite.WriteLine("TEXT 757,367,""ROMAN.TTF"",180,1,20,""ITEM""")
                oWrite.WriteLine("TEXT 576,367,""ROMAN.TTF"",180,1,20,"":""")
                oWrite.WriteLine("TEXT 548,367,""ROMAN.TTF"",180,1,20,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 757,296,""ROMAN.TTF"",180,1,20,""D. NO""")
                oWrite.WriteLine("TEXT 576,296,""ROMAN.TTF"",180,1,20,"":""")
                oWrite.WriteLine("TEXT 548,296,""ROMAN.TTF"",180,1,20,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 757,225,""ROMAN.TTF"",180,1,20,""SHADE""")
                oWrite.WriteLine("TEXT 576,225,""ROMAN.TTF"",180,1,20,"":""")
                oWrite.WriteLine("TEXT 548,225,""ROMAN.TTF"",180,1,20,""" & SHADE & """")


                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If
                oWrite.WriteLine("TEXT 757,153,""ROMAN.TTF"",180,1,20,""WIDTH""")
                oWrite.WriteLine("TEXT 576,153,""ROMAN.TTF"",180,1,20,"":""")
                oWrite.WriteLine("TEXT 548,153,""ROMAN.TTF"",180,1,20,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 757,82,""ROMAN.TTF"",180,1,20,""MTRS""")
                oWrite.WriteLine("TEXT 576,82,""ROMAN.TTF"",180,1,20,"":""")
                oWrite.WriteLine("TEXT 548,82,""ROMAN.TTF"",180,1,20,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("QRCODE 259,269,L,9,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 265,63,""ROMAN.TTF"",180,1,12,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "SIMPLEX" Then

                If TEMPHEADER = 1 Then
                    oWrite.WriteLine("SIZE 49.5 mm, 38 mm")
                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")


                    oWrite.WriteLine("TEXT 382,287,""0"",180,12,12,""" & ITEMNAME & """")
                    oWrite.WriteLine("TEXT 382,245,""0"",180,12,12,""DESIGN NO""")
                    oWrite.WriteLine("TEXT 221,245,""0"",180,12,12,"":""")
                    oWrite.WriteLine("TEXT 203,245,""0"",180,12,12,""" & DESIGNNO & """")
                    oWrite.WriteLine("TEXT 382,204,""0"",180,12,12,""SHADE""")
                    oWrite.WriteLine("TEXT 221,204,""0"",180,12,12,"":""")
                    oWrite.WriteLine("TEXT 203,204,""0"",180,12,12,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 382,163,""0"",180,12,12,""WIDTH""")
                    oWrite.WriteLine("TEXT 221,163,""0"",180,12,12,"":""")


                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    End If

                    oWrite.WriteLine("TEXT 203,163,""0"",180,12,12,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("TEXT 382,121,""0"",180,12,12,""METERS""")
                    oWrite.WriteLine("TEXT 221,121,""0"",180,12,12,"":""")
                    oWrite.WriteLine("TEXT 203,121,""0"",180,12,12,""" & Format(Val(MTRS), "0.00") & GRIDDESC & """")
                    oWrite.WriteLine("BARCODE 382,83,""128M"",48,0,180,2,4,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 270,30,""0"",180,8,8,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()
                Else

                    oWrite.WriteLine("SIZE 49.5 mm, 38 mm")
                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")


                    oWrite.WriteLine("TEXT 382,287,""0"",180,12,12,""" & ITEMNAME & """")
                    oWrite.WriteLine("TEXT 382,245,""0"",180,12,12,""SHADE""")
                    oWrite.WriteLine("TEXT 273,245,""0"",180,12,12,"":""")
                    oWrite.WriteLine("TEXT 256,245,""0"",180,12,12,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 382,204,""0"",180,12,12,""SIZE""")
                    oWrite.WriteLine("TEXT 273,204,""0"",180,12,12,"":""")
                    oWrite.WriteLine("TEXT 256,204,""0"",180,12,12,""" & DESIGNNO & """")
                    oWrite.WriteLine("TEXT 382,163,""0"",180,12,12,""STYLE""")
                    oWrite.WriteLine("TEXT 273,163,""0"",180,12,12,"":""")


                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String
                    Dim TEMPMRP As String
                    Dim TEMPREMARKS As String
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE, ISNULL(ITEMMASTER.ITEM_REMARKS,'') AS REMARKS ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                        TEMPMRP = Val(DT.Rows(0).Item("RATE"))
                        TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                    End If

                    oWrite.WriteLine("TEXT 256,163,""0"",180,12,12,""" & TEMPREMARKS & """")
                    oWrite.WriteLine("TEXT 382,121,""0"",180,12,12,""M.R.P.""")
                    oWrite.WriteLine("TEXT 273,121,""0"",180,12,12,"":""")
                    oWrite.WriteLine("TEXT 256,121,""0"",180,12,12,""" & TEMPMRP & "/-""")
                    oWrite.WriteLine("BARCODE 382,83,""128M"",48,0,180,2,4,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 270,30,""0"",180,8,8,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 164,111,""0"",180,6,6,""(Incl. of all Taxes)""")
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()

                End If

            ElseIf ClientName = "SHREENAKODA" Then

                oWrite.WriteLine("I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q803")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q400,B25")
                oWrite.WriteLine("KI80")
                oWrite.WriteLine("N")
                oWrite.WriteLine("A765,324,2,4,1,1,N,""QUALITY""")
                oWrite.WriteLine("A765,269,2,4,1,1,N,""DESIGN NO""")
                oWrite.WriteLine("A765,214,2,4,1,1,N,""SHADE NO""")
                oWrite.WriteLine("A592,324,2,4,1,1,N,"":""")
                oWrite.WriteLine("A592,269,2,4,1,1,N,"":""")
                oWrite.WriteLine("A592,214,2,4,1,1,N,"":""")
                oWrite.WriteLine("A567,327,2,2,2,2,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A567,217,2,4,2,2,N,""" & SHADE & """")
                oWrite.WriteLine("A568,272,2,4,2,2,N,""" & DESIGNNO & """")

                'GET PACKINGTYPE FROM PARTYMASTER AND SHOW HERE, WHEN WEAVERNAME IS PRESENT
                Dim TEMPPACKINGTYPE As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(PACKINGTYPE_NAME, '') AS PACKINGTYPE ", "", " LEDGERS LEFT OUTER JOIN PACKINGTYPEMASTER ON LEDGERS.ACC_PACKINGTYPEID = PACKINGTYPEMASTER.PACKINGTYPE_id ", " AND LEDGERS.ACC_CMPNAME = '" & WEAVERNAME & "' AND LEDGERS.ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPPACKINGTYPE = DT.Rows(0).Item("PACKINGTYPE")
                End If

                oWrite.WriteLine("A764,391,2,2,3,3,N,""" & TEMPPACKINGTYPE & """") 'PACKINGTYPE

                oWrite.WriteLine("LO8,339,756,3")
                oWrite.WriteLine("b85,95,Q,m2,s8,eL,iA,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A273,93,2,4,1,1,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A765,159,2,4,1,1,N,""MTRS""")
                oWrite.WriteLine("A592,159,2,4,1,1,N,"":""")
                oWrite.WriteLine("A568,162,2,2,2,2,N,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("A765,48,2,4,1,1,N,""RACK""")
                oWrite.WriteLine("A592,48,2,4,1,1,N,"":""")
                oWrite.WriteLine("A567,51,2,2,2,2,N,""" & RACK & """")
                oWrite.WriteLine("A766,104,2,4,1,1,N,""SERIES""")
                oWrite.WriteLine("A593,104,2,4,1,1,N,"":""")
                oWrite.WriteLine("A568,107,2,2,2,2,N,""" & GRIDDESC & """")     'SERIES
                oWrite.WriteLine("A281,48,2,4,1,1,N,""WIDTH""")
                oWrite.WriteLine("A202,48,2,4,1,1,N,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                DT = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("A177,48,2,4,1,1,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("P1")
                oWrite.Dispose()

            ElseIf ClientName = "SARAYU" Then

                oWrite.WriteLine("I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q799")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q400,B25")
                oWrite.WriteLine("KI80")
                oWrite.WriteLine("N")
                oWrite.WriteLine("A768,237,2,2,2,2,N,""M.NA""")
                oWrite.WriteLine("A649,237,2,2,2,2,N,"":""")
                If GRIDDESC <> "" Then oWrite.WriteLine("A618,237,2,2,2,2,N,""" & GRIDDESC & """") Else oWrite.WriteLine("A618,237,2,2,2,2,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A768,97,2,2,2,2,N,""MTRS""")
                oWrite.WriteLine("A649,97,2,2,2,2,N,"":""")
                oWrite.WriteLine("A768,50,2,2,2,2,N,""WIDTH""")
                oWrite.WriteLine("A649,50,2,2,2,2,N,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("A618,50,2,2,2,2,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("A618,97,2,2,2,2,N,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("A618,191,2,2,2,2,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A649,191,2,2,2,2,N,"":""")
                oWrite.WriteLine("A768,191,2,2,2,2,N,""D.NO""")
                oWrite.WriteLine("A768,144,2,2,2,2,N,""C.NO""")
                oWrite.WriteLine("A649,144,2,2,2,2,N,"":""")
                oWrite.WriteLine("A618,144,2,2,2,2,N,""" & SHADE & """")
                oWrite.WriteLine("b63,63,Q,m2,s6,eL,iA,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A217,45,2,3,1,1,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("P1")
                oWrite.Dispose()

            ElseIf ClientName = "TEXTILEINDIA" Then

                oWrite.WriteLine("I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q779")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("KIZZQ0")
                oWrite.WriteLine("KI9+0.0")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q400,B25")
                oWrite.WriteLine("N")
                oWrite.WriteLine("A766,379,2,3,3,3,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A766,305,2,3,2,2,N,""D. NO""")
                oWrite.WriteLine("A607,305,2,3,2,2,N,"":""")
                oWrite.WriteLine("A560,305,2,3,2,2,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A766,244,2,3,2,2,N,""SHADE""")
                oWrite.WriteLine("A607,244,2,3,2,2,N,"":""")
                oWrite.WriteLine("A560,244,2,3,2,2,N,""" & SHADE & """")
                oWrite.WriteLine("A766,183,2,3,2,2,N,""WIDTH""")
                oWrite.WriteLine("A607,183,2,3,2,2,N,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("A560,183,2,3,2,2,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("A377,183,2,3,2,2,N,""MTRS""")
                oWrite.WriteLine("A248,183,2,3,2,2,N,"":""")
                oWrite.WriteLine("A202,183,2,3,2,2,N,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("B766,111,2,1,4,8,72,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A502,32,2,3,1,1,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("P1")
                oWrite.Dispose()


            ElseIf ClientName = "VALIANT" Then

                If TEMPHEADER = 2 Then
                    oWrite.WriteLine("SIZE 99.10 mm, 60.1 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")


                    oWrite.WriteLine("TEXT 776,304,""0"",180,11,11,""MERCHANT""")
                    oWrite.WriteLine("TEXT 609,306,""0"",180,14,14,"":""")
                    oWrite.WriteLine("TEXT 583,314,""0"",180,20,20,""" & ITEMNAME & """")
                    oWrite.WriteLine("TEXT 776,252,""0"",180,11,11,""DESIGN""")
                    oWrite.WriteLine("TEXT 609,254,""0"",180,14,14,"":""")
                    oWrite.WriteLine("TEXT 583,254,""0"",180,14,14,""" & DESIGNNO & """")
                    oWrite.WriteLine("TEXT 377,252,""0"",180,11,11,""SHADE""")
                    oWrite.WriteLine("TEXT 267,254,""0"",180,14,14,"":""")
                    oWrite.WriteLine("TEXT 241,254,""0"",180,14,14,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 776,206,""0"",180,11,11,""MTRS""")
                    oWrite.WriteLine("TEXT 609,207,""0"",180,14,14,"":""")
                    oWrite.WriteLine("TEXT 583,207,""0"",180,14,14,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("TEXT 372,206,""0"",180,11,11,""WIDTH""")
                    oWrite.WriteLine("TEXT 267,207,""0"",180,14,14,"":""")


                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    End If

                    oWrite.WriteLine("TEXT 241,207,""0"",180,14,14,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("BARCODE 776,159,""128M"",84,0,180,3,6,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 776,69,""0"",180,10,10,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()
                Else

                    oWrite.WriteLine("SIZE 99.10 mm, 50 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")


                    oWrite.WriteLine("TEXT 757,323,""0"",180,11,11,""MERCHANT""")
                    oWrite.WriteLine("TEXT 589,325,""0"",180,14,14,"":""")
                    oWrite.WriteLine("TEXT 563,333,""0"",180,20,20,""" & ITEMNAME & """")
                    oWrite.WriteLine("TEXT 757,271,""0"",180,11,11,""DESIGN""")
                    oWrite.WriteLine("TEXT 589,272,""0"",180,14,14,"":""")
                    oWrite.WriteLine("TEXT 563,272,""0"",180,14,14,""" & DESIGNNO & """")
                    oWrite.WriteLine("TEXT 358,271,""0"",180,11,11,""SHADE""")
                    oWrite.WriteLine("TEXT 248,272,""0"",180,14,14,"":""")
                    oWrite.WriteLine("TEXT 222,272,""0"",180,14,14,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 757,224,""0"",180,11,11,""MTRS""")
                    oWrite.WriteLine("TEXT 589,226,""0"",180,14,14,"":""")
                    oWrite.WriteLine("TEXT 563,226,""0"",180,14,14,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("TEXT 353,224,""0"",180,11,11,""WIDTH""")
                    oWrite.WriteLine("TEXT 248,226,""0"",180,14,14,"":""")


                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    End If

                    oWrite.WriteLine("TEXT 222,226,""0"",180,14,14,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("BARCODE 757,178,""128M"",84,0,180,3,6,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 757,88,""0"",180,10,10,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()

                End If

            ElseIf ClientName = "MAHAVIRPOLYCOT" Then

                oWrite.WriteLine("I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q779")
                oWrite.WriteLine("S3")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("D8")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q800,25")
                oWrite.WriteLine("KI81")
                oWrite.WriteLine("N")
                oWrite.WriteLine("A764,393,2,2,2,2,N,""HSN""")
                oWrite.WriteLine("A764,338,2,2,2,2,N,""Width""")
                oWrite.WriteLine("A764,502,2,2,2,2,N,""Shade""")
                oWrite.WriteLine("A765,609,2,2,2,2,N,""Item""")
                oWrite.WriteLine("b123,335,Q,m2,s7,eL,iA,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A270,332,2,2,1,1,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A34,314,1,3,1,1,N,""" & UNIT & """")
                oWrite.WriteLine("A765,447,2,2,2,2,N,""Mtrs""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim TEMPHSN As String = ""
                Dim TEMPREMARKS As String = ""
                Dim TEMPQUALITY As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE, ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_SELVEDGE, '') AS SELVEDGE, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS  ", "", " ITEMMASTER LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.item_HSNCODEID = HSNMASTER.HSN_ID ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPHSN = DT.Rows(0).Item("HSNCODE")
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPQUALITY = DT.Rows(0).Item("SELVEDGE")
                    TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                End If

                oWrite.WriteLine("A556,393,2,2,2,2,N,""" & TEMPHSN & """")
                oWrite.WriteLine("A556,502,2,2,2,2,N,""" & SHADE & """")
                oWrite.WriteLine("A556,338,2,2,2,2,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("A556,447,2,2,2,2,N,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("A556,609,2,2,2,2,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A764,557,2,2,2,2,N,""D.No""")
                oWrite.WriteLine("A596,609,2,2,2,2,N,"":""")
                oWrite.WriteLine("A556,557,2,2,2,2,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A596,557,2,2,2,2,N,"":""")
                oWrite.WriteLine("A596,502,2,2,2,2,N,"":""")
                oWrite.WriteLine("A596,393,2,2,2,2,N,"":""")
                oWrite.WriteLine("A596,338,2,2,2,2,N,"":""")
                oWrite.WriteLine("A596,447,2,2,2,2,N,"":""")
                oWrite.WriteLine("A764,283,2,2,2,2,N,""Quality""")
                oWrite.WriteLine("A556,280,2,4,1,1,N,""" & TEMPQUALITY & """") 'SELVEDGE-REMARKS
                oWrite.WriteLine("A596,283,2,2,2,2,N,"":""")
                oWrite.WriteLine("A34,428,1,3,1,1,N,""" & RACK & "-" & SHELF & """")
                oWrite.WriteLine("A611,178,2,2,2,2,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A611,117,2,4,1,1,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A611,69,2,2,2,2,N,""" & SHADE & """")
                oWrite.WriteLine("A367,69,2,2,2,2,N,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("A6,172,3,2,2,2,N,""" & RACK & "-" & SHELF & """")
                oWrite.WriteLine("b128,49,Q,m2,s4,eL,iA,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A212,45,2,2,1,1,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("P1")
                oWrite.Dispose()

            ElseIf ClientName = "RAJKRIPA" Then

                If GRIDDESC = "" Then GRIDDESC = ITEMNAME
                If TEMPHEADER = 1 Then  'FOR LUMP
                    oWrite.WriteLine("<xpml><page quantity='0' pitch='75.1 mm'></xpml>^XA")
                    oWrite.WriteLine("^MCY^PMN")
                    oWrite.WriteLine("^PW779")
                    oWrite.WriteLine("~JSN^MMT")
                    oWrite.WriteLine("^JZY")
                    oWrite.WriteLine("^LH0,0^LRN")
                    oWrite.WriteLine("^XZ")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='0' pitch='75.1 mm'></xpml><xpml></page></xpml><xpml><page quantity='1' pitch='75.1 mm'></xpml>^XA")
                    oWrite.WriteLine("^FT22,140")
                    oWrite.WriteLine("^CI0")

                    'GET PACKINGTYPE FROM PARTYMASTER AND SHOW HERE, WHEN WEAVERNAME IS PRESENT
                    Dim TEMPCATEGORY As String = ""
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY, ISNULL(ITEMMASTER.ITEM_DISPLAYNAME,'') AS DISPLAYNAME ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                    End If

                    oWrite.WriteLine("^A0N,45,61^FD" & GRIDDESC & "^FS")
                    oWrite.WriteLine("^FT22,211")
                    oWrite.WriteLine("^ADN,36,20^FDSHADE^FS")
                    oWrite.WriteLine("^FT170,211")
                    oWrite.WriteLine("^ADN,36,20^FD:^FS")
                    oWrite.WriteLine("^FT189,212")
                    oWrite.WriteLine("^A0N,45,61^FD" & SHADE & "^FS")
                    oWrite.WriteLine("^FT21,271")
                    oWrite.WriteLine("^ADN,36,20^FDQLTY^FS")
                    oWrite.WriteLine("^FT170,271")
                    oWrite.WriteLine("^ADN,36,20^FD:^FS")
                    oWrite.WriteLine("^FT189,271")
                    oWrite.WriteLine("^ADN,36,20^FD" & TEMPCATEGORY & "^FS") 'PACKINGTYPE
                    oWrite.WriteLine("^FT21,342")
                    oWrite.WriteLine("^ADN,36,20^FDMTRS^FS")
                    oWrite.WriteLine("^FT170,342")
                    oWrite.WriteLine("^ADN,36,20^FD:^FS")
                    oWrite.WriteLine("^FT189,343")
                    oWrite.WriteLine("^A0N,45,61^FD" & Format(Val(MTRS), "0.00") & "^FS")
                    oWrite.WriteLine("^FT405,342")
                    oWrite.WriteLine("^ADN,36,20^FDGRADE^FS")
                    oWrite.WriteLine("^FT536,342")
                    oWrite.WriteLine("^ADN,36,20^FD:^FS")
                    oWrite.WriteLine("^FT563,343")
                    oWrite.WriteLine("^A0N,45,61^FD" & UNIT & "^FS")
                    oWrite.WriteLine("^FO30,464")
                    oWrite.WriteLine("^BY4^BCN,88,N,N,,A^FD" & BARCODE & "^FS") 'BARCODE
                    oWrite.WriteLine("^FT269,580")
                    oWrite.WriteLine("^AAN,27,15^FD" & BARCODE & "^FS") 'BARCODE
                    oWrite.WriteLine("^FT22,68")
                    oWrite.WriteLine("^A0N,45,61^FD" & WEAVERNAME & "^FS")    'DYEINGNAME
                    oWrite.WriteLine("^FT331,211")
                    oWrite.WriteLine("^ADN,36,20^FDLOT NO^FS")
                    oWrite.WriteLine("^FT485,211")
                    oWrite.WriteLine("^ADN,36,20^FD:^FS")
                    oWrite.WriteLine("^FT515,212")
                    oWrite.WriteLine("^A0N,45,61^FD" & LOTNO & "^FS")
                    oWrite.WriteLine("^FO20,363")
                    oWrite.WriteLine("^GB724,87,3^FS")
                    oWrite.WriteLine("^FT460,271")
                    oWrite.WriteLine("^ADN,36,20^FDKATA MTR^FS")
                    oWrite.WriteLine("^FT653,271")
                    oWrite.WriteLine("^ADN,36,20^FD:^FS")
                    oWrite.WriteLine("^FT30,390")
                    oWrite.WriteLine("^ADN,18,10^FDREMARKS^FS")
                    oWrite.WriteLine("^PQ1,0,1,Y")
                    oWrite.WriteLine("^XZ")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                    oWrite.Dispose()
                Else
                    oWrite.WriteLine("<xpml><page quantity='0' pitch='75.1 mm'></xpml>^XA")
                    oWrite.WriteLine("^MCY^PMN")
                    oWrite.WriteLine("^PW779")
                    oWrite.WriteLine("~JSN^MMT")
                    oWrite.WriteLine("^JZY")
                    oWrite.WriteLine("^LH0,0^LRN")
                    oWrite.WriteLine("^XZ")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='0' pitch='75.1 mm'></xpml><xpml></page></xpml><xpml><page quantity='1' pitch='75.1 mm'></xpml>^XA")
                    oWrite.WriteLine("^FT20,236")
                    oWrite.WriteLine("^CI0")


                    'GET PACKINGTYPE FROM PARTYMASTER AND SHOW HERE, WHEN WEAVERNAME IS PRESENT
                    Dim TEMPCATEGORY As String = ""
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(CATEGORYMASTER.CATEGORY_NAME,'') AS CATEGORY, ISNULL(ITEMMASTER.ITEM_DISPLAYNAME,'') AS DISPLAYNAME ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                    End If

                    oWrite.WriteLine("^A0N,51,69^FD" & GRIDDESC & "^FS")
                    oWrite.WriteLine("^FT20,301")
                    oWrite.WriteLine("^ADN,36,20^FDSHADE^FS")
                    oWrite.WriteLine("^FT194,301")
                    oWrite.WriteLine("^ADN,36,20^FD:^FS")
                    oWrite.WriteLine("^FT224,302")
                    oWrite.WriteLine("^A0N,45,61^FD" & SHADE & "^FS")
                    oWrite.WriteLine("^FT20,368")
                    oWrite.WriteLine("^ADN,36,20^FDQUALITY^FS")
                    oWrite.WriteLine("^FT194,368")
                    oWrite.WriteLine("^ADN,36,20^FD:^FS")
                    oWrite.WriteLine("^FT224,370")
                    oWrite.WriteLine("^A0N,45,61^FD" & TEMPCATEGORY & "^FS") 'PACKINGTYPE
                    oWrite.WriteLine("^FT20,436")
                    oWrite.WriteLine("^ADN,36,20^FDMTRS^FS")
                    oWrite.WriteLine("^FT194,436")
                    oWrite.WriteLine("^ADN,36,20^FD:^FS")
                    oWrite.WriteLine("^FT224,437")
                    oWrite.WriteLine("^A0N,45,61^FD" & Format(Val(MTRS), "0.00") & "^FS")
                    oWrite.WriteLine("^FT439,436")
                    oWrite.WriteLine("^ADN,36,20^FDGRADE^FS")
                    oWrite.WriteLine("^FT571,436")
                    oWrite.WriteLine("^ADN,36,20^FD:^FS")
                    oWrite.WriteLine("^FT607,437")
                    oWrite.WriteLine("^A0N,45,61^FD" & UNIT & "^FS")
                    oWrite.WriteLine("^FO30,450")
                    oWrite.WriteLine("^BY4^BCN,102,N,N,,A^FD" & BARCODE & "^FS") 'BARCODE
                    oWrite.WriteLine("^FT269,580")
                    oWrite.WriteLine("^AAN,27,15^FD" & BARCODE & "^FS") 'BARCODE
                    oWrite.WriteLine("^PQ1,0,1,Y")
                    oWrite.WriteLine("^XZ")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                    oWrite.Dispose()
                End If

            ElseIf ClientName = "CHINTAN" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 57.5 mm, 15 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("BARCODE 443,109,""128M"",76,0,180,2,4,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 343,29,""0"",180,9,9,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "MANSI" Then


                If TEMPHEADER = 4 Then

                    '100X50 BAROCDE

                    oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")

                    oWrite.WriteLine("TEXT 761,369,""ROMAN.TTF"",180,16,16,""ITEM""")
                    oWrite.WriteLine("TEXT 604,369,""ROMAN.TTF"",180,16,16,"":""")
                    If GRIDDESC = "" Then oWrite.WriteLine("TEXT 576,369,""ROMAN.TTF"",180,16,16,""" & ITEMNAME & """") Else oWrite.WriteLine("TEXT 576,369,""ROMAN.TTF"",180,16,16,""" & GRIDDESC & """")
                    oWrite.WriteLine("TEXT 761,304,""ROMAN.TTF"",180,16,16,""SHADE""")
                    oWrite.WriteLine("TEXT 604,304,""ROMAN.TTF"",180,16,16,"":""")
                    oWrite.WriteLine("TEXT 576,304,""ROMAN.TTF"",180,16,16,""" & SHADE & """")

                    'GET WIDTH LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    End If

                    oWrite.WriteLine("TEXT 761,233,""ROMAN.TTF"",180,16,16,""WIDTH""")
                    oWrite.WriteLine("TEXT 604,233,""ROMAN.TTF"",180,16,16,"":""")
                    oWrite.WriteLine("TEXT 576,233,""ROMAN.TTF"",180,16,16,""" & TEMPWIDTH & """")


                    oWrite.WriteLine("TEXT 312,233,""ROMAN.TTF"",180,16,16,""MTRS""")
                    oWrite.WriteLine("TEXT 185,233,""ROMAN.TTF"",180,16,16,"":""")
                    If UNIT = "TP" Then oWrite.WriteLine("TEXT 156,233,""ROMAN.TTF"",180,16,16,""" & Format(Val(MTRS), "0.00") & " " & UNIT & """") Else oWrite.WriteLine("TEXT 156,233,""ROMAN.TTF"",180,16,16,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("BARCODE 761,168,""128M"",97,0,180,4,8,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 505,64,""ROMAN.TTF"",180,12,12,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()

                ElseIf TEMPHEADER <> 3 Then

                    'NORMAL BAROCDE

                    oWrite.WriteLine("SIZE 99.10 mm, 75.1 mm")
                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")

                    oWrite.WriteLine("TEXT 758,387,""ROMAN.TTF"",180,1,18,""ITEM""")
                    oWrite.WriteLine("TEXT 578,387,""ROMAN.TTF"",180,1,18,"":""")
                    If GRIDDESC = "" Then oWrite.WriteLine("TEXT 548,387,""ROMAN.TTF"",180,1,18,""" & ITEMNAME & """") Else oWrite.WriteLine("TEXT 548,387,""ROMAN.TTF"",180,1,18,""" & GRIDDESC & """")
                    oWrite.WriteLine("TEXT 758,307,""ROMAN.TTF"",180,1,18,""SHADE""")
                    oWrite.WriteLine("TEXT 578,307,""ROMAN.TTF"",180,1,18,"":""")
                    oWrite.WriteLine("TEXT 548,313,""ROMAN.TTF"",180,1,22,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 758,227,""ROMAN.TTF"",180,1,18,""MTRS""")
                    oWrite.WriteLine("TEXT 578,227,""ROMAN.TTF"",180,1,18,"":""")
                    If UNIT = "TP" Then oWrite.WriteLine("TEXT 548,235,""ROMAN.TTF"",180,1,24,""" & Format(Val(MTRS), "0.00") & " " & UNIT & """") Else oWrite.WriteLine("TEXT 548,235,""ROMAN.TTF"",180,1,24,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("BARCODE 758,164,""128M"",102,0,180,3,6,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 579,57,""ROMAN.TTF"",180,1,12,""" & BARCODE & """") 'BARCODE
                    If TEMPHEADER = 1 Then
                        oWrite.WriteLine("TEXT 686,548,""0"",180,57,40,""YOGESH""")
                        oWrite.WriteLine("TEXT 625,445,""ROMAN.TTF"",180,1,8,""SHIRTING FABRICS FOR EVERY GENERATION""")
                        oWrite.WriteLine("TEXT 687,571,""ROMAN.TTF"",180,1,8,""ONLY""")
                    End If
                    oWrite.WriteLine("BAR 33,407, 730, 3")
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()

                Else
                    'GARMENT BARCODE
                    oWrite.WriteLine("SIZE 57.5 mm, 40 mm")
                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")

                    oWrite.WriteLine("TEXT 322,307,""ROMAN.TTF"",180,1,18,""TRESOS""")
                    oWrite.WriteLine("TEXT 401,211,""ROMAN.TTF"",180,1,12,""SHADE""")
                    oWrite.WriteLine("TEXT 286,211,""ROMAN.TTF"",180,1,12,"":""")
                    oWrite.WriteLine("TEXT 268,211,""ROMAN.TTF"",180,1,12,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 401,170,""ROMAN.TTF"",180,1,12,""SIZE""")
                    oWrite.WriteLine("TEXT 286,170,""ROMAN.TTF"",180,1,12,"":""")
                    oWrite.WriteLine("TEXT 268,170,""ROMAN.TTF"",180,1,12,""" & QUALITY & """")

                    'GET RATE AS MRP FROM ITEMMASTER
                    Dim TEMPMRP As String = ""
                    Dim TEMPDESC As String = ""
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_RATE, 0) AS RATE, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPMRP = Val(DT.Rows(0).Item("RATE"))
                        TEMPDESC = DT.Rows(0).Item("REMARKS")
                    End If

                    If TEMPMRP > 0 Then
                        oWrite.WriteLine("TEXT 401,130,""ROMAN.TTF"",180,1,12,""M.R.P.""")
                        oWrite.WriteLine("TEXT 286,130,""ROMAN.TTF"",180,1,12,"":""")
                        oWrite.WriteLine("TEXT 268,130,""ROMAN.TTF"",180,1,12,""" & TEMPMRP & "/-" & """")
                        oWrite.WriteLine("TEXT 204,99,""ROMAN.TTF"",180,1,6,""( Incl. of all taxes )""")
                    Else
                        oWrite.WriteLine("TEXT 401,130,""ROMAN.TTF"",180,1,12,""DESC""")
                        oWrite.WriteLine("TEXT 286,130,""ROMAN.TTF"",180,1,12,"":""")
                        oWrite.WriteLine("TEXT 268,130,""ROMAN.TTF"",180,1,12,""" & TEMPDESC & """")
                    End If

                    oWrite.WriteLine("TEXT 401,251,""ROMAN.TTF"",180,1,12,""STYLE""")
                    oWrite.WriteLine("TEXT 286,251,""ROMAN.TTF"",180,1,12,"":""")
                    oWrite.WriteLine("TEXT 268,251,""ROMAN.TTF"",180,1,12,""" & ITEMNAME & """")
                    oWrite.WriteLine("BARCODE 419,76,""128M"",43,0,180,2,4,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 300,31,""ROMAN.TTF"",180,1,8,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 26,114,""ROMAN.TTF"",90,1,6,""( Made In INDIA )""")
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()
                End If

            ElseIf ClientName = "SST" Then


                If TEMPHEADER = 1 Then

                    'NORMAL BAROCDE
                    oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")

                    oWrite.WriteLine("TEXT 761,375,""ROMAN.TTF"",180,1,18,""ITEM""")
                    oWrite.WriteLine("TEXT 589,375,""ROMAN.TTF"",180,1,18,"":""")
                    oWrite.WriteLine("TEXT 562,375,""ROMAN.TTF"",180,1,18,""" & ITEMNAME & """")
                    oWrite.WriteLine("TEXT 761,300,""ROMAN.TTF"",180,1,18,""D. NO""")
                    oWrite.WriteLine("TEXT 589,300,""ROMAN.TTF"",180,1,18,"":""")
                    oWrite.WriteLine("TEXT 562,300,""ROMAN.TTF"",180,1,18,""" & DESIGNNO & """")
                    oWrite.WriteLine("TEXT 761,222,""ROMAN.TTF"",180,1,18,""SHADE""")
                    oWrite.WriteLine("TEXT 589,222,""ROMAN.TTF"",180,1,18,"":""")
                    oWrite.WriteLine("TEXT 562,222,""ROMAN.TTF"",180,1,18,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 339,300,""ROMAN.TTF"",180,1,18,""WIDTH""")
                    oWrite.WriteLine("TEXT 179,300,""ROMAN.TTF"",180,1,18,"":""")

                    'GET WIDTH LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    End If

                    oWrite.WriteLine("TEXT 151,300,""ROMAN.TTF"",180,1,18,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("TEXT 318,222,""ROMAN.TTF"",180,1,18,""MTRS""")
                    oWrite.WriteLine("TEXT 179,222,""ROMAN.TTF"",180,1,18,"":""")
                    oWrite.WriteLine("TEXT 151,222,""ROMAN.TTF"",180,1,18,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("BARCODE 755,153,""128M"",102,0,180,4,8,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 490,47,""ROMAN.TTF"",180,1,11,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("BAR 14,318, 747, 3")
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()

                Else

                    'GARMENT BARCODE
                    oWrite.WriteLine("SIZE 50.1 mm, 50 mm")
                    oWrite.WriteLine("SPEED 5")
                    oWrite.WriteLine("DENSITY 7")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")

                    oWrite.WriteLine("TEXT 378,382,""ROMAN.TTF"",180,1,12,""" & ITEMNAME & """")
                    oWrite.WriteLine("TEXT 378,327,""ROMAN.TTF"",180,1,12,""D. NO""")
                    oWrite.WriteLine("TEXT 270,327,""ROMAN.TTF"",180,1,12,"":""")
                    oWrite.WriteLine("TEXT 251,327,""ROMAN.TTF"",180,1,12,""" & DESIGNNO & """")
                    oWrite.WriteLine("TEXT 378,284,""ROMAN.TTF"",180,1,12,""SHADE""")
                    oWrite.WriteLine("TEXT 270,284,""ROMAN.TTF"",180,1,12,"":""")
                    oWrite.WriteLine("TEXT 251,284,""ROMAN.TTF"",180,1,12,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 378,237,""ROMAN.TTF"",180,1,12,""WIDTH""")
                    oWrite.WriteLine("TEXT 270,237,""ROMAN.TTF"",180,1,12,"":""")

                    'GET RATE AS MRP FROM ITEMMASTER
                    Dim TEMPMRP As String = ""
                    Dim TEMPWIDTH As String
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_RATE, 0) AS RATE, ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPMRP = Val(DT.Rows(0).Item("RATE"))
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    End If

                    oWrite.WriteLine("TEXT 251,237,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """")

                    'WHITOUT MRP
                    If TEMPHEADER = 2 Then
                        oWrite.WriteLine("TEXT 378,191,""ROMAN.TTF"",180,1,12,""MTRS""")
                        oWrite.WriteLine("TEXT 270,191,""ROMAN.TTF"",180,1,12,"":""")
                        oWrite.WriteLine("TEXT 251,191,""ROMAN.TTF"",180,1,12,""" & Format(Val(MTRS), "0.00") & """")
                        oWrite.WriteLine("BARCODE 378,144,""128M"",90,0,180,2,4,""" & BARCODE & """") 'BARCODE
                        oWrite.WriteLine("TEXT 267,49,""ROMAN.TTF"",180,1,8,""" & BARCODE & """") 'BARCODE
                        oWrite.WriteLine("BAR 23,342, 359, 3")
                    Else
                        oWrite.WriteLine("TEXT 155,284,""ROMAN.TTF"",180,1,12,""QTY""")
                        oWrite.WriteLine("TEXT 82,284,""ROMAN.TTF"",180,1,12,"":""")
                        oWrite.WriteLine("TEXT 63,284,""ROMAN.TTF"",180,1,12,""1 N""")
                        oWrite.WriteLine("BARCODE 378,133,""128M"",90,0,180,2,4,""" & BARCODE & """") 'BARCODE
                        oWrite.WriteLine("TEXT 267,38,""ROMAN.TTF"",180,1,8,""" & BARCODE & """") 'BARCODE
                        oWrite.WriteLine("TEXT 378,195,""ROMAN.TTF"",180,1,12,""M.R.P.""")
                        oWrite.WriteLine("TEXT 270,195,""ROMAN.TTF"",180,1,12,"":""")
                        oWrite.WriteLine("TEXT 251,195,""ROMAN.TTF"",180,1,12,""" & TEMPMRP & "/-" & """")
                        oWrite.WriteLine("TEXT 251,159,""ROMAN.TTF"",180,1,4,""( Incl. of all taxes )""")
                        oWrite.WriteLine("BAR 14,343, 370, 3")
                    End If

                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()
                End If

            ElseIf ClientName = "RUCHITA" Then

                oWrite.WriteLine("SIZE 99.10 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 771,377,""ROMAN.TTF"",180,1,16,""ITEM""")
                oWrite.WriteLine("TEXT 622,377,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 597,377,""ROMAN.TTF"",180,1,16,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 771,316,""ROMAN.TTF"",180,1,16,""D. NO""")
                oWrite.WriteLine("TEXT 622,316,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 597,316,""ROMAN.TTF"",180,1,16,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 771,256,""ROMAN.TTF"",180,1,16,""SHADE""")
                oWrite.WriteLine("TEXT 622,256,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 597,256,""ROMAN.TTF"",180,1,16,""" & SHADE & """")
                oWrite.WriteLine("TEXT 771,134,""ROMAN.TTF"",180,1,16,""WIDTH""")
                oWrite.WriteLine("TEXT 622,134,""ROMAN.TTF"",180,1,16,"":""")

                'GET WIDTH LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 597,134,""ROMAN.TTF"",180,1,16,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 771,73,""ROMAN.TTF"",180,1,16,""MTRS""")
                oWrite.WriteLine("TEXT 622,73,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 597,73,""ROMAN.TTF"",180,1,16,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("QRCODE 279,296,L,10,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 274,69,""ROMAN.TTF"",180,1,12,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 771,195,""ROMAN.TTF"",180,1,16,""LOTNO""")
                oWrite.WriteLine("TEXT 622,195,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 597,195,""ROMAN.TTF"",180,1,16,""" & LOTNO & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "ANMOL" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 99.10 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 779,377,""0"",180,18,18,""ITEM""")
                oWrite.WriteLine("TEXT 613,377,""0"",180,18,18, "":""")
                oWrite.WriteLine("TEXT 583,377,""0"",180,18,18,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 779,318,""0"",180,18,18,""D. NO""")
                oWrite.WriteLine("TEXT 613,318,""0"",180,18,18, "":""")
                oWrite.WriteLine("TEXT 583,318,""0"",180,18,18,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 779,258,""0"",180,18,18,""SHADE""")
                oWrite.WriteLine("TEXT 613,258,""0"",180,18,18, "":""")
                oWrite.WriteLine("TEXT 583,258,""0"",180,18,18,""" & SHADE & """")
                oWrite.WriteLine("TEXT 779,194,""0"",180,18,18,""WIDTH""")
                oWrite.WriteLine("TEXT 613,194,""0"",180,18,18, "":""")


                'GET WIDTH FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 583,194,""0"",180,18,18,""" & TEMPWIDTH & """")

                oWrite.WriteLine("TEXT 323,190,""0"",180,18,18,""MTRS""")
                oWrite.WriteLine("TEXT 187,188,""0"",180,18,18, "":""")
                oWrite.WriteLine("TEXT 165,188,""0"",180,18,18,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 779,134,""128M"",89,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 513,41,""0"",180,11,11,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 49,206,""0"",90,11,11,""" & RACK & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()




            ElseIf ClientName = "PARTOBA" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 746,267,""ROMAN.TTF"",180,1,14,""D. NO""")
                oWrite.WriteLine("TEXT 604,267,""ROMAN.TTF"",180,1,14, "":""")
                oWrite.WriteLine("TEXT 578,272,""ROMAN.TTF"",180,1,18,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 383,267,""ROMAN.TTF"",180,1,14,""SHADE""")
                oWrite.WriteLine("TEXT 249,267,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 230,272,""ROMAN.TTF"",180,1,18,""" & SHADE & """")
                oWrite.WriteLine("TEXT 746,202,""ROMAN.TTF"",180,1,14,""GRADE""")
                oWrite.WriteLine("TEXT 604,202,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 578,202,""ROMAN.TTF"",180,1,14,""" & UNIT & """")
                oWrite.WriteLine("TEXT 383,202,""ROMAN.TTF"",180,1,14,""MTRS""")
                oWrite.WriteLine("TEXT 249,202,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 230,210,""ROMAN.TTF"",180,1,20,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 732,151,""128M"",102,0,180,3,6,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 553,43,""ROMAN.TTF"",180,1,12,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 525,379,""ROMAN.TTF"",180,1,22,""PARTOBA""")
                oWrite.WriteLine("BAR 39,300, 699, 3")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "SSC" Then

                oWrite.WriteLine("SIZE 99.10 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 776,377,""ROMAN.TTF"",180,1,18,""ITEM""")
                oWrite.WriteLine("TEXT 631,377,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("TEXT 598,377,""ROMAN.TTF"",180,1,18,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 776,313,""ROMAN.TTF"",180,1,18,""D. NO""")
                oWrite.WriteLine("TEXT 631,313,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("TEXT 598,313,""ROMAN.TTF"",180,1,22,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 776,247,""ROMAN.TTF"",180,1,18,""BALE""")
                oWrite.WriteLine("TEXT 631,247,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("TEXT 598,247,""ROMAN.TTF"",180,1,22,""" & BALENO & """")
                If UNIT = "Pcs" Then
                    oWrite.WriteLine("TEXT 776,178,""ROMAN.TTF"",180,1,18,""PCS""")
                    oWrite.WriteLine("TEXT 631,178,""ROMAN.TTF"",180,1,18,"":""")
                    oWrite.WriteLine("TEXT 598,178,""ROMAN.TTF"",180,1,18,""" & Format(Val(QTY), "0.00") & """")
                Else
                    oWrite.WriteLine("TEXT 776,178,""ROMAN.TTF"",180,1,18,""MTRS""")
                    oWrite.WriteLine("TEXT 631,178,""ROMAN.TTF"",180,1,18,"":""")
                    oWrite.WriteLine("TEXT 598,178,""ROMAN.TTF"",180,1,18,""" & Format(Val(MTRS), "0.00") & "  (" & GRIDDESC & ")" & """")
                End If
                oWrite.WriteLine("BARCODE 776,114,""128M"",72,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 492,37,""ROMAN.TTF"",180,1,9,""" & BARCODE & """") 'BARCODE
                'oWrite.WriteLine("TEXT 301,178,""ROMAN.TTF"",180,1,18,""CUT""")
                'oWrite.WriteLine("TEXT 199,178,""ROMAN.TTF"",180,1,18,"":""")
                'oWrite.WriteLine("TEXT 170,178,""ROMAN.TTF"",180,1,18,""" & Format(Val(CUT), "0.00") & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "SMT" Then

                oWrite.WriteLine("SIZE 63.5 mm, 139.8 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 451,429,""ROMAN.TTF"",90,1,16,""QUALITY""")
                oWrite.WriteLine("TEXT 451,624,""ROMAN.TTF"",90,1,16,"":""")
                oWrite.WriteLine("TEXT 451,659,""ROMAN.TTF"",90,1,16,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 372,429,""ROMAN.TTF"",90,1,16,""DESIGN""")
                oWrite.WriteLine("TEXT 372,624,""ROMAN.TTF"",90,1,16,"":""")
                oWrite.WriteLine("TEXT 372,659,""ROMAN.TTF"",90,1,16,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 293,429,""ROMAN.TTF"",90,1,16,""MTRS""")
                oWrite.WriteLine("TEXT 293,624,""ROMAN.TTF"",90,1,16,"":""")
                oWrite.WriteLine("TEXT 293,659,""ROMAN.TTF"",90,1,16,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 214,429,""ROMAN.TTF"",90,1,16,""LOTNO""")
                oWrite.WriteLine("TEXT 214,624,""ROMAN.TTF"",90,1,16,"":""")
                oWrite.WriteLine("TEXT 214,659,""ROMAN.TTF"",90,1,16,""" & LOTNO & """")
                oWrite.WriteLine("BARCODE 155,380,""128M"",92,0,90,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 57,627,""ROMAN.TTF"",90,1,12,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "VSTRADERS" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 760,370,""ROMAN.TTF"",180,1,16,""ITEM""")
                oWrite.WriteLine("TEXT 598,370,""ROMAN.TTF"",180,1,16, "":""")
                oWrite.WriteLine("TEXT 567,370,""ROMAN.TTF"",180,1,18,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 760,304,""ROMAN.TTF"",180,1,16,""SHADE""")
                oWrite.WriteLine("TEXT 598,304,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 567,304,""ROMAN.TTF"",180,1,18,""" & SHADE & """")
                oWrite.WriteLine("TEXT 350,242,""ROMAN.TTF"",180,1,16,""WIDTH""")
                oWrite.WriteLine("TEXT 202,242,""ROMAN.TTF"",180,1,16,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 171,242,""ROMAN.TTF"",180,1,16,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 760,242,""ROMAN.TTF"",180,1,16,""GRADE""")
                oWrite.WriteLine("TEXT 598,242,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 567,242,""ROMAN.TTF"",180,1,16,""" & UNIT & """")
                oWrite.WriteLine("TEXT 350,184,""ROMAN.TTF"",180,1,16,""MTRS""")
                oWrite.WriteLine("TEXT 202,184,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 171,184,""ROMAN.TTF"",180,1,16,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 760,184,""ROMAN.TTF"",180,1,16,""LOT NO""")
                oWrite.WriteLine("TEXT 598,184,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 567,184,""ROMAN.TTF"",180,1,16,""" & LOTNO & """")
                oWrite.WriteLine("BARCODE 751,131,""128M"",77,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 496,49,""ROMAN.TTF"",180,1,12,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 363,304,""ROMAN.TTF"",180,1,16,""SERIES""")
                oWrite.WriteLine("TEXT 202,304,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 172,304,""ROMAN.TTF"",180,1,16,""" & GRIDDESC & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "SIDDHIRAJ" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 762,383,""ROMAN.TTF"",180,1,16,""ITEM""")
                oWrite.WriteLine("TEXT 609,383,""ROMAN.TTF"",180,1,16, "":""")
                oWrite.WriteLine("TEXT 585,383,""ROMAN.TTF"",180,1,18,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 762,320,""ROMAN.TTF"",180,1,16,""D.NO""")
                oWrite.WriteLine("TEXT 609,320,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 585,320,""ROMAN.TTF"",180,1,16,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 762,253,""ROMAN.TTF"",180,1,16,""SHADE""")
                oWrite.WriteLine("TEXT 609,253,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 585,253,""ROMAN.TTF"",180,1,18,""" & SHADE & """")
                oWrite.WriteLine("TEXT 762,186,""ROMAN.TTF"",180,1,16,""MTRS""")
                oWrite.WriteLine("TEXT 609,186,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 585,186,""ROMAN.TTF"",180,1,20,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 371,186,""ROMAN.TTF"",180,1,16,""WIDTH""")
                oWrite.WriteLine("TEXT 226,186,""ROMAN.TTF"",180,1,16,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 203,186,""ROMAN.TTF"",180,1,16,""" & TEMPWIDTH & """")

                oWrite.WriteLine("BARCODE 762,129,""128M"",78,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 506,44,""ROMAN.TTF"",180,1,12,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "SIDDHGIRI" Then

                oWrite.WriteLine("SIZE 101.6 mm, 50.8 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 778,377,""ROMAN.TTF"",180,1,18,""ITEM""")
                oWrite.WriteLine("TEXT 592,377,""ROMAN.TTF"",180,1,18, "":""")
                oWrite.WriteLine("TEXT 564,377,""ROMAN.TTF"",180,1,18,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 778,304,""ROMAN.TTF"",180,1,18,""D.NO""")
                oWrite.WriteLine("TEXT 592,304,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("TEXT 564,304,""ROMAN.TTF"",180,1,18,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 778,232,""ROMAN.TTF"",180,1,18,""SHADE""")
                oWrite.WriteLine("TEXT 592,232,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("TEXT 564,232,""ROMAN.TTF"",180,1,18,""" & SHADE & """")

                oWrite.WriteLine("TEXT 778,159,""ROMAN.TTF"",180,1,18,""WIDTH""")
                oWrite.WriteLine("TEXT 592,159,""ROMAN.TTF"",180,1,18,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 564,159,""ROMAN.TTF"",180,1,18,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 778,87,""ROMAN.TTF"",180,1,18,""MTRS""")
                oWrite.WriteLine("TEXT 592,87,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("TEXT 564,87,""ROMAN.TTF"",180,1,18,""" & Format(Val(MTRS), "0.00") & """")


                oWrite.WriteLine("QRCODE 253,303,L,9,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 272,103,""ROMAN.TTF"",180,1,14,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 501,33,""ROMAN.TTF"",180,1,9,""PREMINUM FABRIC""")
                oWrite.WriteLine("TEXT 140,48,""ROMAN.TTF"",180,1,14,""" & RACK & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "KUNAL" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")

                oWrite.WriteLine("TEXT 765,301,""0"",180,14,14,""ITEM""")
                oWrite.WriteLine("TEXT 613,301,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 591,301,""0"",180,14,14,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 765,202,""0"",180,14,14,""SHADE""")
                oWrite.WriteLine("TEXT 613,202,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 591,202,""0"",180,14,14,""" & SHADE & """")
                oWrite.WriteLine("TEXT 765,152,""0"",180,14,14,""SERIES""")
                oWrite.WriteLine("TEXT 613,152,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 591,152,""0"",180,14,14,""" & GRIDDESC & """")
                oWrite.WriteLine("TEXT 318,202,""0"",180,14,14,""MTRS""")
                oWrite.WriteLine("TEXT 166,202,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 145,202,""0"",180,14,14,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 765,99,""128M"",61,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 481,33,""0"",180,9,9,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 318,152,""0"",180,14,14,""WIDTH""")
                oWrite.WriteLine("TEXT 166,152,""0"",180,14,14,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 145,152,""0"",180,14,14,""" & TEMPWIDTH & """")

                If TEMPHEADER = 1 Then oWrite.WriteLine("TEXT 503,385,""0"",180,20,20,""KEMILON""")

                oWrite.WriteLine("BAR 24,320, 740, 3")
                oWrite.WriteLine("TEXT 765,252,""0"",180,14,14,""D. NO""")
                oWrite.WriteLine("TEXT 613,252,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 591,252,""0"",180,14,14,""" & DESIGNNO & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "SUPEEMA" Then

                oWrite.WriteLine("SIZE 62.5 mm, 48 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 473,358,""ROMAN.TTF"",180,1,12,""ITEM""")
                oWrite.WriteLine("TEXT 355,358,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 333,358,""ROMAN.TTF"",180,1,12,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 473,263,""ROMAN.TTF"",180,1,12,""D.NO""")
                oWrite.WriteLine("TEXT 355,263,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 333,263,""ROMAN.TTF"",180,1,12,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 473,215,""ROMAN.TTF"",180,1,12,""SHADE""")
                oWrite.WriteLine("TEXT 355,215,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 333,215,""ROMAN.TTF"",180,1,12,""" & SHADE & """")
                oWrite.WriteLine("TEXT 473,167,""ROMAN.TTF"",180,1,12,""MTRS""")
                oWrite.WriteLine("TEXT 355,167,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 333,172,""ROMAN.TTF"",180,1,12,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("QRCODE 179,303,L,7,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("BARCODE 464,125,""128M"",81,0,180,2,4,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 364,39,""ROMAN.TTF"",180,1,10,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 473,311,""ROMAN.TTF"",180,1,12,""UNIT""")
                oWrite.WriteLine("TEXT 355,311,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 333,311,""ROMAN.TTF"",180,1,12,""" & UNIT & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "SHANTI" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 750,307,""ROMAN.TTF"",180,1,14,""ARTICLE""")
                oWrite.WriteLine("TEXT 591,307,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 572,312,""ROMAN.TTF"",180,1,18,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 750,255,""ROMAN.TTF"",180,1,14,""D.NO""")
                oWrite.WriteLine("TEXT 591,255,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 572,255,""ROMAN.TTF"",180,1,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 750,203,""ROMAN.TTF"",180,1,14,""SHADE""")
                oWrite.WriteLine("TEXT 591,203,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 572,203,""ROMAN.TTF"",180,1,14,""" & SHADE & """")
                oWrite.WriteLine("TEXT 356,255,""ROMAN.TTF"",180,1,14,""WIDTH""")
                oWrite.WriteLine("TEXT 232,255,""ROMAN.TTF"",180,1,14,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 213,255,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")

                oWrite.WriteLine("TEXT 340,203,""ROMAN.TTF"",180,1,14,""MTRS""")
                oWrite.WriteLine("TEXT 252,203,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 213,216,""ROMAN.TTF"",180,1,24,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 739,133,""128M"",80,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 483,47,""ROMAN.TTF"",180,1,12,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 498,382,""ROMAN.TTF"",180,1,18,""MOKSHA""")
                oWrite.WriteLine("BAR 28,327, 723, 3")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "SPCORP" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q812")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q406,25")
                oWrite.WriteLine("KI80")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>N")
                oWrite.WriteLine("A749,379,2,3,2,2,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A749,311,2,2,2,2,N,""DESIGN""")
                oWrite.WriteLine("A597,311,2,2,2,2,N,"":""")
                oWrite.WriteLine("A564,311,2,2,2,2,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A749,261,2,2,2,2,N,""SHADE""")
                oWrite.WriteLine("A597,261,2,2,2,2,N,"":""")
                oWrite.WriteLine("A564,261,2,2,2,2,N,""" & SHADE & """")
                oWrite.WriteLine("A749,211,2,2,2,2,N,""WIDTH""")
                oWrite.WriteLine("A597,211,2,2,2,2,N,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("A564,211,2,2,2,2,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("A355,211,2,2,2,2,N,""LOTNO""")
                oWrite.WriteLine("A229,211,2,2,2,2,N,"":""")
                oWrite.WriteLine("A196,211,2,2,2,2,N,""" & LOTNO & """") 'BARCODE
                oWrite.WriteLine("A749,158,2,2,2,2,N,""MTRS""")
                oWrite.WriteLine("A597,158,2,2,2,2,N,"":""")
                oWrite.WriteLine("A564,158,2,2,2,2,N,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("B749,112,2,1,3,6,65,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A539,42,2,2,1,1,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A355,158,2,2,2,2,N,""GRADE""")
                oWrite.WriteLine("A229,158,2,2,2,2,N,"":""")
                oWrite.WriteLine("A196,158,2,2,2,2,N,""" & PIECETYPE & """")    'cmpname
                oWrite.WriteLine("LO15,327,753,3")
                oWrite.WriteLine("P1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()


            ElseIf ClientName = "SONU" Then

                oWrite.WriteLine("G0")
                oWrite.WriteLine("n")
                oWrite.WriteLine("M0500")
                oWrite.WriteLine("O0214")
                oWrite.WriteLine("V0")
                oWrite.WriteLine("t1")
                oWrite.WriteLine("Kf0070")
                oWrite.WriteLine("L")
                oWrite.WriteLine("D11")
                oWrite.WriteLine("ySPM")
                oWrite.WriteLine("A2")

                oWrite.WriteLine("1911A1401490013" & ITEMNAME)
                oWrite.WriteLine("1911A1401230013D. NO")
                oWrite.WriteLine("1911A1401230082:")
                oWrite.WriteLine("1911A1401210094" & DESIGNNO)
                oWrite.WriteLine("1911A1400960013SHADE")
                oWrite.WriteLine("1911A1400960082:")
                oWrite.WriteLine("1911A1400950094" & SHADE)
                oWrite.WriteLine("1911A1400690013WIDTH")
                oWrite.WriteLine("1911A1400690082:")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim TEMPHSN As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY,  ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.item_HSNCODEID = HSNMASTER.HSN_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPHSN = DT.Rows(0).Item("HSNCODE")
                End If

                oWrite.WriteLine("1911A1400680094" & TEMPWIDTH)
                oWrite.WriteLine("1911A1400680157MTRS")
                oWrite.WriteLine("1911A1400690216:")
                oWrite.WriteLine("1911A1400680224" & Format(Val(MTRS), "0.00"))
                oWrite.WriteLine("1e6304600210015B" & BARCODE)
                oWrite.WriteLine("1911A1000040100" & BARCODE)
                oWrite.WriteLine("1911A1401240171HSN")
                oWrite.WriteLine("1911A1401250216:")
                oWrite.WriteLine("1911A1401240224" & TEMPHSN)
                oWrite.WriteLine("Q0001")
                oWrite.WriteLine("E")
                oWrite.Dispose()


            ElseIf ClientName = "VINAYAK" Then

                oWrite.WriteLine("SIZE 72.5 mm, 45 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 555,341,""ROMAN.TTF"",180,1,20,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 555,268,""ROMAN.TTF"",180,1,14,""D.NO""")
                oWrite.WriteLine("TEXT 417,268,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 388,268,""ROMAN.TTF"",180,1,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 555,213,""ROMAN.TTF"",180,1,14,""SHADE""")
                oWrite.WriteLine("TEXT 417,213,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 388,213,""ROMAN.TTF"",180,1,14,""" & SHADE & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 555,156,""ROMAN.TTF"",180,1,14,""WIDTH""")
                oWrite.WriteLine("TEXT 417,156,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 388,156,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 298,156,""ROMAN.TTF"",180,1,14,""MTRS""")
                oWrite.WriteLine("TEXT 179,156,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 156,161,""ROMAN.TTF"",180,1,18,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 556,110,""128M"",66,0,180,3,6,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 373,38,""ROMAN.TTF"",180,1,10,""" & BARCODE & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "KARAN" Then

                oWrite.WriteLine("SIZE 99.10 mm, 75.1 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 788,551,""ROMAN.TTF"",180,1,16,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 758,462,""ROMAN.TTF"",180,1,18,""D.NO""")
                oWrite.WriteLine("BAR 39,491, 719, 3")
                oWrite.WriteLine("TEXT 758,393,""ROMAN.TTF"",180,1,18,""SHADE""")
                oWrite.WriteLine("TEXT 584,393,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("TEXT 584,462,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("TEXT 556,462,""ROMAN.TTF"",180,1,18,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 556,393,""ROMAN.TTF"",180,1,18,""" & SHADE & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim TEMPUNIT As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPUNIT = DT.Rows(0).Item("UNIT")
                End If

                oWrite.WriteLine("TEXT 196,252,""ROMAN.TTF"",180,1,18,""" & TEMPWIDTH & """")
                If UCase(TEMPUNIT) = "PCS" Then oWrite.WriteLine("TEXT 758,252,""ROMAN.TTF"",180,1,18,""PCS""") Else oWrite.WriteLine("TEXT 758,252,""ROMAN.TTF"",180,1,18,""MTRS""")
                oWrite.WriteLine("TEXT 584,252,""ROMAN.TTF"",180,1,18,"":""")
                If UCase(TEMPUNIT) = "PCS" Then oWrite.WriteLine("TEXT 556,252,""ROMAN.TTF"",180,1,18,""" & Format(Val(QTY), "0") & """") Else oWrite.WriteLine("TEXT 556,252,""ROMAN.TTF"",180,1,18,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 758,190,""128M"",102,0,180,3,6,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 639,82,""ROMAN.TTF"",180,1,18,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 758,322,""ROMAN.TTF"",180,1,18,""GRADE""")
                oWrite.WriteLine("TEXT 584,322,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("TEXT 558,322,""ROMAN.TTF"",180,1,18,""" & PIECETYPE & """")
                oWrite.WriteLine("TEXT 398,252,""ROMAN.TTF"",180,1,18,""WIDTH""")
                oWrite.WriteLine("TEXT 224,252,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "NTC" Then

                oWrite.WriteLine("SIZE 99.10 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 591,361,""ROMAN.TTF"",180,1,16,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 769,361,""ROMAN.TTF"",180,1,16,""ITEM""")
                oWrite.WriteLine("TEXT 620,361,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 591,299,""ROMAN.TTF"",180,1,16,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 769,299,""ROMAN.TTF"",180,1,16,""D.NO""")
                oWrite.WriteLine("TEXT 620,299,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 176,299,""ROMAN.TTF"",180,1,16,""" & SHADE & """")
                oWrite.WriteLine("TEXT 342,299,""ROMAN.TTF"",180,1,16,""SHADE""")
                oWrite.WriteLine("TEXT 205,299,""ROMAN.TTF"",180,1,16,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 591,234,""ROMAN.TTF"",180,1,16,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 769,234,""ROMAN.TTF"",180,1,16,""WIDTH""")
                oWrite.WriteLine("TEXT 620,234,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 176,234,""ROMAN.TTF"",180,1,16,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 342,234,""ROMAN.TTF"",180,1,16,""MTRS""")
                oWrite.WriteLine("TEXT 205,234,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 55,234,""ROMAN.TTF"",180,1,16,""" & GRIDDESC & """")
                oWrite.WriteLine("BARCODE 769,153,""128M"",87,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 528,59,""ROMAN.TTF"",180,1,14,""" & BARCODE & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "MNIKHIL" Then

                oWrite.WriteLine("SIZE 75 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 562,387,""ROMAN.TTF"",180,1,14,""" & CmpName & """")
                If GRIDDESC = "" Then oWrite.WriteLine("TEXT 575,326,""ROMAN.TTF"",180,1,14,""Quality""") Else oWrite.WriteLine("TEXT 575,326,""ROMAN.TTF"",180,1,14,""Name""")
                oWrite.WriteLine("TEXT 420,326,""ROMAN.TTF"",180,1,14,"":""")
                If GRIDDESC = "" Then oWrite.WriteLine("TEXT 397,326,""ROMAN.TTF"",180,1,14,""" & ITEMNAME & """") Else oWrite.WriteLine("TEXT 397,326,""ROMAN.TTF"",180,1,14,""" & GRIDDESC & """")
                oWrite.WriteLine("TEXT 575,275,""ROMAN.TTF"",180,1,14,""Design""")
                oWrite.WriteLine("TEXT 420,275,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 397,275,""ROMAN.TTF"",180,1,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 575,224,""ROMAN.TTF"",180,1,14,""Shade""")
                oWrite.WriteLine("TEXT 420,224,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 397,224,""ROMAN.TTF"",180,1,14,""" & SHADE & """")
                oWrite.WriteLine("TEXT 575,173,""ROMAN.TTF"",180,1,14,""Mtrs""")
                oWrite.WriteLine("TEXT 420,173,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 397,173,""ROMAN.TTF"",180,1,14,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 575,72,""128M"",62,0,180,3,6,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("BAR 38,339, 527, 3")
                oWrite.WriteLine("TEXT 575,122,""ROMAN.TTF"",180,1,14,""Piece No""")
                oWrite.WriteLine("TEXT 420,122,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 397,122,""ROMAN.TTF"",180,1,14,""" & BARCODE & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "HRITI" Then

                oWrite.WriteLine("SIZE 75 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 562,387,""ROMAN.TTF"",180,1,14,""" & CmpName & """")
                oWrite.WriteLine("TEXT 575,326,""ROMAN.TTF"",180,1,14,""Quality""")
                oWrite.WriteLine("TEXT 420,326,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 397,326,""ROMAN.TTF"",180,1,14,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 575,275,""ROMAN.TTF"",180,1,14,""Design""")
                oWrite.WriteLine("TEXT 420,275,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 397,275,""ROMAN.TTF"",180,1,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 575,224,""ROMAN.TTF"",180,1,14,""Shade""")
                oWrite.WriteLine("TEXT 420,224,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 397,224,""ROMAN.TTF"",180,1,14,""" & SHADE & """")
                oWrite.WriteLine("TEXT 575,173,""ROMAN.TTF"",180,1,14,""Mtrs""")
                oWrite.WriteLine("TEXT 420,173,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 397,173,""ROMAN.TTF"",180,1,14,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 575,72,""128M"",62,0,180,3,6,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("BAR 38,339, 527, 3")
                oWrite.WriteLine("TEXT 575,122,""ROMAN.TTF"",180,1,14,""Piece No""")
                oWrite.WriteLine("TEXT 420,122,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 397,122,""ROMAN.TTF"",180,1,14,""" & BARCODE & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "KRISHNA" Then

                If TEMPHEADER = 1 Then
                    oWrite.WriteLine("SIZE 99.10 mm, 60.1 mm")
                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")
                    oWrite.WriteLine("TEXT 773,456,""ROMAN.TTF"",180,1,17,""QUALITY""")
                    oWrite.WriteLine("TEXT 773,400,""ROMAN.TTF"",180,1,17,""D.NO.""")
                    oWrite.WriteLine("TEXT 773,344,""ROMAN.TTF"",180,1,17,""SHADE NO.""")
                    oWrite.WriteLine("TEXT 773,292,""ROMAN.TTF"",180,1,17,""MTRS""")
                    oWrite.WriteLine("TEXT 773,236,""ROMAN.TTF"",180,1,17,""LOT NO.""")
                    oWrite.WriteLine("TEXT 773,180,""ROMAN.TTF"",180,1,17,""GRADE""")
                    oWrite.WriteLine("TEXT 549,456,""ROMAN.TTF"",180,1,17,"":""")
                    oWrite.WriteLine("TEXT 549,401,""ROMAN.TTF"",180,1,17,"":""")
                    oWrite.WriteLine("TEXT 549,344,""ROMAN.TTF"",180,1,17,"":""")
                    oWrite.WriteLine("TEXT 549,289,""ROMAN.TTF"",180,1,17,"":""")
                    oWrite.WriteLine("TEXT 549,234,""ROMAN.TTF"",180,1,17,"":""")
                    oWrite.WriteLine("TEXT 549,177,""ROMAN.TTF"",180,1,17,"":""")
                    oWrite.WriteLine("TEXT 516,456,""ROMAN.TTF"",180,1,17,""" & ITEMNAME & """")
                    oWrite.WriteLine("TEXT 516,401,""ROMAN.TTF"",180,1,17,""" & DESIGNNO & """")
                    oWrite.WriteLine("TEXT 516,347,""ROMAN.TTF"",180,1,17,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 516,289,""ROMAN.TTF"",180,1,17,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("TEXT 516,234,""ROMAN.TTF"",180,1,17,""" & LOTNO & """")
                    oWrite.WriteLine("TEXT 516,180,""ROMAN.TTF"",180,1,17,""" & UNIT & """")
                    oWrite.WriteLine("BARCODE 773,131,""128M"",75,0,180,3,6,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 611,49,""ROMAN.TTF"",180,1,14,""" & BARCODE & """")
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()
                Else
                    oWrite.WriteLine("SIZE 99.10 mm, 75.1 mm")
                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")
                    oWrite.WriteLine("TEXT 759,422,""ROMAN.TTF"",180,1,18,""" & DESIGNNO & """")
                    oWrite.WriteLine("BAR 39,359, 719, 3")
                    oWrite.WriteLine("TEXT 758,343,""ROMAN.TTF"",180,1,18,""" & Format(Val(QTY), "0") & " " & UNIT & """")
                    oWrite.WriteLine("TEXT 230,343,""ROMAN.TTF"",180,1,18,""" & Format(Val(CUT), "0.00") & " CUT" & """")
                    oWrite.WriteLine("TEXT 758,284,""ROMAN.TTF"",180,1,18,""ROYAL ICON PACK""")
                    oWrite.WriteLine("TEXT 758,225,""ROMAN.TTF"",180,1,18,""M.R.P.""")
                    oWrite.WriteLine("TEXT 606,225,""ROMAN.TTF"",180,1,18,"":""")

                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPMRP As Double = 0.0
                    Dim TEMPHSN As String = ""
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE ", "", " ITEMMASTER LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.item_HSNCODEID = HSNMASTER.HSN_ID ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPHSN = DT.Rows(0).Item("HSNCODE")
                    End If


                    DT = OBJCMN.SEARCH(" ISNULL(DESIGNMASTER.DESIGN_SALERATE,0) AS RATE ", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & DESIGNNO & "' AND DESIGN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPMRP = Val(DT.Rows(0).Item("RATE"))
                    End If

                    oWrite.WriteLine("TEXT 579,225,""ROMAN.TTF"",180,1,18,""" & Format(Val(TEMPMRP), "0.00") & "/-  P.P." & """")
                    oWrite.WriteLine("TEXT 758,167,""ROMAN.TTF"",180,1,18,""HSN""")
                    oWrite.WriteLine("TEXT 606,167,""ROMAN.TTF"",180,1,18,"":""")
                    oWrite.WriteLine("TEXT 579,167,""ROMAN.TTF"",180,1,18,""" & TEMPHSN & """")
                    oWrite.WriteLine("BARCODE 758,111,""128M"",75,0,180,3,6,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 596,41,""ROMAN.TTF"",180,1,14,""" & BARCODE & """")
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()
                End If



            ElseIf ClientName = "PARAMOUNT" Then

                oWrite.WriteLine("SIZE 99.10 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 771,255,""ROMAN.TTF"",180,1,12,""D.NO""")
                oWrite.WriteLine("TEXT 635,255,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 611,255,""ROMAN.TTF"",180,1,12,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 771,203,""ROMAN.TTF"",180,1,12,""QUALITY""")
                oWrite.WriteLine("TEXT 635,203,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 611,203,""ROMAN.TTF"",180,1,12,""" & QUALITY & """")
                oWrite.WriteLine("TEXT 366,203,""ROMAN.TTF"",180,1,12,""MTR/KG""")
                oWrite.WriteLine("TEXT 239,203,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 224,203,""ROMAN.TTF"",180,1,12,""" & Format(Val(MTRS), "0.000") & " " & UNIT & """")
                oWrite.WriteLine("TEXT 771,151,""ROMAN.TTF"",180,1,12,""SHADE""")
                oWrite.WriteLine("TEXT 635,151,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 611,151,""ROMAN.TTF"",180,1,12,""" & SHADE & """")


                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 285,151,""ROMAN.TTF"",180,1,12,""WIDTH""")
                oWrite.WriteLine("TEXT 167,151,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 135,151,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """")
                oWrite.WriteLine("BARCODE 776,106,""128M"",68,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 474,33,""ROMAN.TTF"",180,1,8,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 769,381,""0"",180,23,18,""" & CmpName & """")
                oWrite.WriteLine("BAR 12,326, 759, 3")
                oWrite.WriteLine("TEXT 771,300,""ROMAN.TTF"",180,1,12,""NAME""")
                oWrite.WriteLine("TEXT 635,300,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 611,300,""ROMAN.TTF"",180,1,12,""" & GRIDDESC & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "SHASHWAT" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 758,373,""ROMAN.TTF"",180,1,14,""QUALITY""")
                oWrite.WriteLine("TEXT 598,373,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 574,379,""ROMAN.TTF"",180,1,18,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 758,306,""ROMAN.TTF"",180,1,14,""DESIGN""")
                oWrite.WriteLine("TEXT 597,306,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 574,306,""ROMAN.TTF"",180,1,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 758,252,""ROMAN.TTF"",180,1,14,""COLOR""")
                oWrite.WriteLine("TEXT 574,252,""ROMAN.TTF"",180,1,14,""" & SHADE & """")
                oWrite.WriteLine("TEXT 758,198,""ROMAN.TTF"",180,1,14,""MTRS""")
                oWrite.WriteLine("TEXT 597,198,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 574,198,""ROMAN.TTF"",180,1,18,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 704,147,""128M"",102,0,180,3,6,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 504,40,""ROMAN.TTF"",180,1,10,""" & BARCODE & """")
                oWrite.WriteLine("BAR 18,321, 739, 3")
                oWrite.WriteLine("TEXT 309,198,""ROMAN.TTF"",180,1,14,""WIDTH""")
                oWrite.WriteLine("TEXT 184,198,""ROMAN.TTF"",180,1,14,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 161,198,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 597,252,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "MSANCHITKUMAR" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 751,378,""0"",180,16,16,""QUALITY""")
                oWrite.WriteLine("TEXT 563,378,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 538,378,""0"",180,16,16,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 538,310,""0"",180,16,16,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 563,310,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 751,310,""0"",180,16,16,""DESIGN""")
                oWrite.WriteLine("TEXT 538,242,""0"",180,16,16,""" & SHADE & """")
                oWrite.WriteLine("TEXT 563,242,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 751,242,""0"",180,16,16,""SHADE""")
                oWrite.WriteLine("TEXT 751,106,""0"",180,16,16,""WIDTH""")
                oWrite.WriteLine("TEXT 564,106,""0"",180,16,16,"":""")


                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim TEMPUNIT As String = ""
                Dim TEMPHSNCODE As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT, ISNULL(HSNMASTER.HSN_CODE,'') AS HSNCODE ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNITMASTER.UNIT_ID LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSNMASTER.HSN_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPHSNCODE = DT.Rows(0).Item("HSNCODE")
                End If

                oWrite.WriteLine("TEXT 538,106,""0"",180,16,16,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 538,174,""0"",180,16,16,""" & PIECETYPE & """")
                oWrite.WriteLine("TEXT 563,174,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 751,174,""0"",180,16,16,""GRADE""")
                oWrite.WriteLine("TEXT 135,174,""0"",180,16,16,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 160,174,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 282,174,""0"",180,16,16,""MTRS""")
                oWrite.WriteLine("BARCODE 436,119,""128M"",69,0,180,2,4,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 357,43,""0"",180,12,12,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 135,242,""0"",180,16,16,""" & TEMPHSNCODE & """")
                oWrite.WriteLine("TEXT 160,242,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 254,242,""0"",180,16,16,""HSN""")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "MOOLTEX" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q799")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q400,B25")
                oWrite.WriteLine("KI80")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>N")
                If GRIDDESC = "" Then oWrite.WriteLine("A763,378,2,1,4,4,N,""" & ITEMNAME & """") Else oWrite.WriteLine("A763,378,2,1,4,4,N,""" & GRIDDESC & """")
                oWrite.WriteLine("A763,301,2,1,3,3,N,""DESIGN""")
                oWrite.WriteLine("A577,301,2,1,3,3,N,"":""")
                oWrite.WriteLine("A541,301,2,1,3,3,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A763,238,2,1,3,3,N,""SHADE""")
                oWrite.WriteLine("A577,238,2,1,3,3,N,"":""")
                oWrite.WriteLine("A541,238,2,1,3,3,N,""" & SHADE & """")
                oWrite.WriteLine("A763,178,2,1,3,3,N,""WIDTH""")
                oWrite.WriteLine("A577,178,2,1,3,3,N,"":""")

                'GET REMARKS AND WIDTH LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim TEMPREMARKS As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("A541,178,2,1,3,3,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("A370,178,2,1,3,3,N,""MTRS""")
                oWrite.WriteLine("A250,178,2,1,3,3,N,"":""")
                oWrite.WriteLine("A214,178,2,1,3,3,N,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("B763,121,2,1,3,6,83,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A568,31,2,3,1,1,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("P1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "SUPRIYA" Then

                If TEMPHEADER <> "5" And TEMPHEADER <> "6" And TEMPHEADER <> "7" Then
                    oWrite.WriteLine("<xpml><page quantity='0' pitch='101.6 mm'></xpml>I8,A")
                    oWrite.WriteLine("ZN")
                    oWrite.WriteLine("q812")
                    oWrite.WriteLine("O")
                    oWrite.WriteLine("JF")
                    oWrite.WriteLine("ZT")
                    oWrite.WriteLine("Q812,25")
                    oWrite.WriteLine("KI80")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='101.6 mm'></xpml>N")
                    oWrite.WriteLine("A749,777,2,3,4,4,N,""" & SUPRIYAHEADER & """")
                    oWrite.WriteLine("LO49,673,711,3")
                    oWrite.WriteLine("A760,639,2,2,3,3,N,""" & ITEMNAME & """")
                    oWrite.WriteLine("A760,552,2,3,2,2,N,""D.NO""")
                    oWrite.WriteLine("A621,553,2,3,2,2,N,"":""")
                    oWrite.WriteLine("A580,553,2,3,2,2,N,""" & DESIGNNO & """")
                    oWrite.WriteLine("A760,474,2,3,2,2,N,""MTRS""")
                    oWrite.WriteLine("A621,475,2,3,2,2,N,"":""")
                    oWrite.WriteLine("A581,475,2,3,2,2,N,""" & Format(Val(MTRS), "0.00") & "  (" & GRIDDESC & ")" & """")
                    oWrite.WriteLine("A358,547,2,3,2,2,N,""WIDTH""")
                    oWrite.WriteLine("A205,547,2,3,2,2,N,"":""")

                    'GET REMARKS AND WIDTH LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String = ""
                    Dim TEMPREMARKS As String = ""
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                        TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                    End If

                    oWrite.WriteLine("A177,547,2,3,2,2,N,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("A760,406,2,3,2,2,N,""SHADE""")
                    oWrite.WriteLine("A621,406,2,3,2,2,N,"":""")
                    oWrite.WriteLine("A580,406,2,3,2,2,N,""" & SHADE & """")
                    oWrite.WriteLine("B780,202,2,1,3,6,79,N,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("A592,117,2,3,1,1,N,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("A760,328,2,3,2,2,N,""GRADE""")
                    oWrite.WriteLine("A621,328,2,3,2,2,N,"":""")
                    oWrite.WriteLine("A580,328,2,3,2,2,N,""" & UNIT & """")
                    If TEMPHEADER <> "3" And TEMPHEADER <> "4" Then oWrite.WriteLine("A780,66,2,2,2,2,N,""" & CmpName & """")    'cmpname
                    oWrite.WriteLine("A557,260,2,2,2,2,N,""" & TEMPREMARKS & """")
                    oWrite.WriteLine("P1")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                    oWrite.Dispose()

                ElseIf TEMPHEADER = "6" Or TEMPHEADER = "7" Then

                    oWrite.WriteLine("<xpml><page quantity='0' pitch='45.0 mm'></xpml>I8,A")
                    oWrite.WriteLine("ZN")
                    oWrite.WriteLine("q620")
                    oWrite.WriteLine("O")
                    oWrite.WriteLine("JF")
                    oWrite.WriteLine("ZT")
                    oWrite.WriteLine("Q360,25")
                    oWrite.WriteLine("KI80")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='45.0 mm'></xpml>N")
                    oWrite.WriteLine("A453,339,2,4,2,2,N,""" & SUPRIYAHEADER & """")
                    oWrite.WriteLine("LO21,287,576,3")
                    oWrite.WriteLine("A597,278,2,2,2,2,N,""" & ITEMNAME & """")
                    oWrite.WriteLine("A597,232,2,2,2,2,N,""D.NO""")
                    oWrite.WriteLine("A477,232,2,2,2,2,N,"":""")
                    oWrite.WriteLine("A448,232,2,2,2,2,N,""" & DESIGNNO & """")
                    oWrite.WriteLine("A246,232,2,2,2,2,N,""MTRS""")
                    oWrite.WriteLine("A145,232,2,2,2,2,N,"":""")
                    oWrite.WriteLine("A121,232,2,2,2,2,N,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("A597,187,2,2,2,2,N,""SHADE""")
                    oWrite.WriteLine("A477,187,2,2,2,2,N,"":""")
                    oWrite.WriteLine("A448,187,2,2,2,2,N,""" & SHADE & """")
                    oWrite.WriteLine("A270,187,2,2,2,2,N,""WIDTH""")
                    oWrite.WriteLine("A144,187,2,2,2,2,N,"":""")

                    'GET REMARKS AND WIDTH LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String = ""
                    Dim TEMPREMARKS As String = ""
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                        TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                    End If

                    oWrite.WriteLine("A114,187,2,2,2,2,N,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("A597,140,2,2,2,2,N,""GRADE""")
                    oWrite.WriteLine("A477,140,2,2,2,2,N,"":""")
                    oWrite.WriteLine("A448,140,2,2,2,2,N,""" & TEMPREMARKS & """")
                    oWrite.WriteLine("B597,93,2,1,2,4,38,N,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("A477,50,2,1,1,1,N,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("A477,26,2,2,1,1,N,""" & CmpName & """")    'cmpname
                    oWrite.WriteLine("P1")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                    oWrite.Dispose()

                ElseIf TEMPHEADER = "5" Then

                    oWrite.WriteLine("<xpml><page quantity='0' pitch='45.0 mm'></xpml>I8,A")
                    oWrite.WriteLine("ZN")
                    oWrite.WriteLine("q620")
                    oWrite.WriteLine("O")
                    oWrite.WriteLine("JF")
                    oWrite.WriteLine("ZT")
                    oWrite.WriteLine("Q360,25")
                    oWrite.WriteLine("KI80")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='45.0 mm'></xpml>N")
                    oWrite.WriteLine("A598,340,2,4,1,1,N,""QUALITY""")
                    oWrite.WriteLine("A475,340,2,4,1,1,N,"":""")
                    oWrite.WriteLine("A446,340,2,4,1,1,N,""" & ITEMNAME & """")
                    oWrite.WriteLine("A598,298,2,4,1,1,N,""LENGTH""")
                    oWrite.WriteLine("A475,298,2,4,1,1,N,"":""")
                    oWrite.WriteLine("A446,298,2,4,1,1,N,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("A262,298,2,4,1,1,N,""WIDTH""")
                    oWrite.WriteLine("A173,298,2,4,1,1,N,"":""")

                    'GET REMARKS AND WIDTH LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String = ""
                    Dim TEMPRATE As Double = 0
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_RATE, 0) AS RATE ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                        TEMPRATE = Val(DT.Rows(0).Item("RATE"))
                    End If

                    oWrite.WriteLine("A143,298,2,4,1,1,N,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("A262,256,2,4,1,1,N,""GRADE""")
                    oWrite.WriteLine("A173,256,2,4,1,1,N,"":""")
                    oWrite.WriteLine("A143,256,2,4,1,1,N,""" & UNIT & """")

                    oWrite.WriteLine("A598,256,2,4,1,1,N,""D.NO""")
                    oWrite.WriteLine("A475,256,2,4,1,1,N,"":""")
                    oWrite.WriteLine("A446,256,2,4,1,1,N,""" & DESIGNNO & """")
                    oWrite.WriteLine("A598,215,2,4,1,1,N,""SHADE""")
                    oWrite.WriteLine("A475,215,2,4,1,1,N,"":""")
                    oWrite.WriteLine("A446,215,2,4,1,1,N,""" & SHADE & """")
                    oWrite.WriteLine("A262,215,2,4,1,1,N,""M.R.P.""")
                    oWrite.WriteLine("A173,215,2,4,1,1,N,"":""")
                    oWrite.WriteLine("A143,215,2,4,1,1,N,""" & TEMPRATE & """")

                    oWrite.WriteLine("B606,168,2,1,2,4,77,N,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("A511,85,2,3,1,1,N,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("A194,181,2,1,1,1,N,""(INC. OF ALL TAXES)""")
                    oWrite.WriteLine("A535,43,2,4,1,1,N,""" & CmpName & """")    'cmpname
                    oWrite.WriteLine("P1")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                    oWrite.Dispose()

                End If

            ElseIf ClientName = "MARKIN" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='75.1 mm'></xpml>SIZE 97.5 mm, 75.1 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='75.1 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("BARCODE 667,160,""128M"",74,0,180,3,6,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 500,80,""ROMAN.TTF"",180,1,12,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 667,453,""ROMAN.TTF"",180,1,16,""DESIGN""")
                oWrite.WriteLine("TEXT 667,394,""ROMAN.TTF"",180,1,16,""WIDTH""")
                oWrite.WriteLine("TEXT 667,335,""ROMAN.TTF"",180,1,16,""COLOR""")
                oWrite.WriteLine("TEXT 667,276,""ROMAN.TTF"",180,1,16,""LOTNO""")
                oWrite.WriteLine("TEXT 667,217,""ROMAN.TTF"",180,1,16,""MTRS""")
                oWrite.WriteLine("TEXT 476,453,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 476,394,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 476,333,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 476,276,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 476,217,""ROMAN.TTF"",180,1,16,"":""")
                oWrite.WriteLine("TEXT 434,453,""ROMAN.TTF"",180,1,16,""" & ITEMNAME & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 434,394,""ROMAN.TTF"",180,1,16,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 434,335,""ROMAN.TTF"",180,1,16,""" & SHADE & """")
                oWrite.WriteLine("TEXT 434,276,""ROMAN.TTF"",180,1,16,""" & LOTNO & """")
                oWrite.WriteLine("TEXT 434,225,""ROMAN.TTF"",180,1,22,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "MOHATUL" Then


                If TEMPHEADER = "1" Then
                    oWrite.WriteLine("<xpml><page quantity='0' pitch='75.1 mm'></xpml>SIZE 97.5 mm, 50 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50 mm'></xpml>SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")
                    oWrite.WriteLine("TEXT 745,378,""0"",180,22,22,""" & ITEMNAME & """")
                    oWrite.WriteLine("BAR 158,321, 587, 3")
                    oWrite.WriteLine("TEXT 745,299,""0"",180,18,18,""D.NO""")
                    oWrite.WriteLine("TEXT 575,299,""0"",180,18,18,"":""")
                    oWrite.WriteLine("TEXT 551,299,""0"",180,18,18,""" & DESIGNNO & """")
                    oWrite.WriteLine("TEXT 333,299,""0"",180,18,18,""WIDTH""")
                    oWrite.WriteLine("TEXT 165,299,""0"",180,18,18,"":""")

                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    End If

                    oWrite.WriteLine("TEXT 137,299,""0"",180,18,18,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("TEXT 745,233,""0"",180,18,18,""SHADE""")
                    oWrite.WriteLine("TEXT 575,233,""0"",180,18,18,"":""")
                    oWrite.WriteLine("TEXT 551,233,""0"",180,18,18,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 333,233,""0"",180,18,18,""MTRS""")
                    oWrite.WriteLine("TEXT 185,233,""0"",180,18,18,"":""")
                    oWrite.WriteLine("TEXT 157,233,""0"",180,18,18,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("BARCODE 745,103,""128M"",63,0,180,3,6,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 551,34,""0"",180,10,10,""" & BARCODE & """")
                    oWrite.WriteLine("TEXT 745,166,""0"",180,18,18,""LOT NO""")
                    oWrite.WriteLine("TEXT 575,166,""0"",180,18,18,"":""")
                    oWrite.WriteLine("TEXT 551,166,""0"",180,18,18,""" & LOTNO & """")
                    oWrite.WriteLine("TEXT 157,159,""0"",180,12,12,""" & RACK & """")
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                    oWrite.Dispose()

                Else

                    oWrite.WriteLine("<xpml><page quantity='0' pitch='37.5 mm'></xpml>SIZE 99.10 mm, 37.5 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='37.5 mm'></xpml>SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")
                    oWrite.WriteLine("TEXT 781,273,""0"",180,12,12,""ITEM""")
                    oWrite.WriteLine("TEXT 651,273,""0"",180,12,12,"":""")
                    oWrite.WriteLine("TEXT 630,277,""0"",180,16,16,""" & ITEMNAME & """")
                    oWrite.WriteLine("BAR 131, 230, 653, 3")
                    oWrite.WriteLine("TEXT 781,217,""0"",180,12,12,""DESIGN""")
                    oWrite.WriteLine("TEXT 651,217,""0"",180,12,12,"":""")
                    oWrite.WriteLine("TEXT 630,217,""0"",180,12,12,""" & DESIGNNO & """")
                    oWrite.WriteLine("TEXT 504,217,""0"",180,12,12,""WIDTH""")
                    oWrite.WriteLine("TEXT 397,217,""0"",180,12,12,"":""")

                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH As String = ""
                    Dim TEMPHSNCODE As String = ""
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY, ISNULL(HSN_CODE,'') AS HSNCODE", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                        TEMPHSNCODE = DT.Rows(0).Item("HSNCODE")
                    End If

                    oWrite.WriteLine("TEXT 377,217,""0"",180,12,12,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("TEXT 781,170,""0"",180,12,12,""SHADE""")
                    oWrite.WriteLine("TEXT 651,170,""0"",180,12,12,"":""")
                    oWrite.WriteLine("TEXT 630,170,""0"",180,12,12,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 504,170,""0"",180,12,12,""MTRS""")
                    oWrite.WriteLine("TEXT 397,170,""0"",180,12,12,"":""")
                    oWrite.WriteLine("TEXT 377,175,""0"",180,16,16,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("BARCODE 781,130,""39"",71,0,180,2,5,""" & BARCODE & """")
                    oWrite.WriteLine("TEXT 622,52,""0"",180,9,9,""" & BARCODE & """")
                    oWrite.WriteLine("TEXT 218,217,""0"",180,12,12,""" & TEMPHSNCODE & """")
                    oWrite.WriteLine("TEXT 300,216,""0"",180,12,12,""HSN""")
                    oWrite.WriteLine("TEXT 234,217,""0"",180,12,12,"":""")
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                    oWrite.Dispose()
                End If



            ElseIf ClientName = "MOMAI" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='25.0 mm'></xpml>SIZE 47.5 mm, 25 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 365,188,""0"",180,14,14,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 365,146,""0"",180,14,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 365,102,""0"",180,9,9,""" & SHADE & """")
                oWrite.WriteLine("TEXT 172,101,""0"",180,8,8,""MRP""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPMRP As Double = 0
                Dim OBJCMN As New ClsCommon
                'FIRST CHECK WITH LEDGER NAME IN WHERECLAUSE IF NO RECORD IS FOUND THEN GET DATA WITHOUT PARTY NAME
                Dim WHERECLAUSE As String = " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & WEAVERNAME & "'"
                Dim DT As DataTable = OBJCMN.SEARCH("ISNULL(PL_RATE,0) AS RATE", "", "PRICELISTMASTER LEFT OUTER JOIN ITEMMASTER ON PL_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN DESIGNMASTER ON PL_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON PL_COLORID = COLORMASTER.COLOR_ID LEFT OUTER JOIN LEDGERS ON PL_LEDGERID = LEDGERS.ACC_ID ", " AND ITEMMASTER.ITEM_NAME = '" & ITEMNAME & "' AND DESIGNMASTER.DESIGN_NO = '" & DESIGNNO & "' AND PL_YEARID = " & YearId & WHERECLAUSE)
                If DT.Rows.Count > 0 Then
                    TEMPMRP = Val(DT.Rows(0).Item("RATE"))
                Else
                    DT = OBJCMN.SEARCH("ISNULL(PL_RATE,0) AS RATE", "", "PRICELISTMASTER LEFT OUTER JOIN ITEMMASTER ON PL_ITEMID = ITEMMASTER.ITEM_ID LEFT OUTER JOIN DESIGNMASTER ON PL_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON PL_COLORID = COLORMASTER.COLOR_ID  LEFT OUTER JOIN LEDGERS ON PL_LEDGERID = LEDGERS.ACC_ID ", " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '' AND ITEMMASTER.ITEM_NAME = '" & ITEMNAME & "' AND DESIGNMASTER.DESIGN_NO = '" & DESIGNNO & "' AND PL_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then TEMPMRP = Val(DT.Rows(0).Item("RATE"))
                End If


                oWrite.WriteLine("TEXT 119,107,""0"",180,13,13, """ & TEMPMRP & """")
                oWrite.WriteLine("TEXT 98,71,""0"",180,4,4,""(Inc. of all Taxes)""")
                oWrite.WriteLine("TEXT 68,138,""0"",180,7,7,""1PCS""")
                oWrite.WriteLine("BARCODE 365,72,""128M"",52,0,180,1,2, """ & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 325,17,""0"",180,6,6, """ & BARCODE & """")

                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "SHALIBHADRA" Then

                oWrite.WriteLine("SIZE 73.7 mm, 50.8 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 572,382,""ROMAN.TTF"",180,1,18,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 572,309,""ROMAN.TTF"",180,1,12,""SHADE""")
                oWrite.WriteLine("TEXT 459,309,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 444,309,""ROMAN.TTF"",180,1,12,""" & SHADE & """")
                oWrite.WriteLine("TEXT 259,309,""ROMAN.TTF"",180,1,12,""D.NO""")
                oWrite.WriteLine("TEXT 175,309,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 160,309,""ROMAN.TTF"",180,1,12,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 572,254,""ROMAN.TTF"",180,1,12,""MTRS""")
                oWrite.WriteLine("TEXT 459,254,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 444,261,""ROMAN.TTF"",180,1,18,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 259,254,""ROMAN.TTF"",180,1,12,""LOT""")
                oWrite.WriteLine("TEXT 175,254,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 160,254,""ROMAN.TTF"",180,1,12,""" & LOTNO & """")
                oWrite.WriteLine("TEXT 572,200,""ROMAN.TTF"",180,1,12,""WIDTH""")
                oWrite.WriteLine("TEXT 459,200,""ROMAN.TTF"",180,1,12,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(HSN_CODE,'') AS HSNCODE ", "", " ITEMMASTER LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.item_HSNCODEID = HSNMASTER.HSN_ID ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 444,200,""ROMAN.TTF"",180,1,12,""" & TEMPWIDTH & """")
                oWrite.WriteLine("BAR 32,325, 542, 3")
                oWrite.WriteLine("BARCODE 572,145,""128M"",89,0,180,3,6,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 405,50,""ROMAN.TTF"",180,1,12,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "KCRAYON" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='75.1 mm'></xpml>SIZE 97.50 mm, 75.1 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='75.1 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 752,546,""ROMAN.TTF"",180,1,24,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 542,455,""ROMAN.TTF"",180,1,18,""" & SHADE & """")
                oWrite.WriteLine("TEXT 748,455,""ROMAN.TTF"",180,1,18,""SHADE""")
                oWrite.WriteLine("TEXT 574,455,""ROMAN.TTF"",180,1,18,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim TEMPHSNCODE As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(HSN_CODE,'') AS HSNCODE ", "", " ITEMMASTER LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.item_HSNCODEID = HSNMASTER.HSN_ID ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPHSNCODE = DT.Rows(0).Item("HSNCODE")
                End If

                oWrite.WriteLine("TEXT 542,274,""ROMAN.TTF"",180,1,18,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 748,274,""ROMAN.TTF"",180,1,18,""WIDTH""")
                oWrite.WriteLine("TEXT 574,274,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("TEXT 542,191,""ROMAN.TTF"",180,1,18,""" & TEMPHSNCODE & """")
                oWrite.WriteLine("TEXT 748,191,""ROMAN.TTF"",180,1,18,""HSN""")
                oWrite.WriteLine("TEXT 574,191,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("TEXT 542,101,""ROMAN.TTF"",180,1,18,""" & PIECETYPE & """")
                oWrite.WriteLine("TEXT 748,101,""ROMAN.TTF"",180,1,18,""GRADE""")
                oWrite.WriteLine("TEXT 574,101,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("QRCODE 266,385,L,10,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 261,115,""ROMAN.TTF"",180,1,12,""" & BARCODE & """")
                If GRIDDESC <> "" Then
                    oWrite.WriteLine("TEXT 542,370,""ROMAN.TTF"",180,1,22,""" & Format(Val(MTRS), "0.00") & "TP" & """")
                Else
                    oWrite.WriteLine("TEXT 542,370,""ROMAN.TTF"",180,1,22,""" & Format(Val(MTRS), "0.00") & """")
                End If
                oWrite.WriteLine("TEXT 748,364,""ROMAN.TTF"",180,1,18,""MTRS""")
                oWrite.WriteLine("TEXT 574,364,""ROMAN.TTF"",180,1,18,"":""")
                oWrite.WriteLine("BAR 8,468, 759, 9")
                oWrite.WriteLine("BOX 6,36,773,570,9")
                oWrite.WriteLine("BAR 293,40, 9, 432")
                oWrite.WriteLine("BAR 297,386, 472, 9")
                oWrite.WriteLine("BAR 297,296, 472, 9")
                oWrite.WriteLine("BAR 297,206, 472, 9")
                oWrite.WriteLine("BAR 297,119, 472, 9")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "DAKSH" Then

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(HSN_CODE,'') AS HSNCODE ", "", " ITEMMASTER LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.item_HSNCODEID = HSNMASTER.HSN_ID ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If


                If TEMPHEADER = "1" Then
                    oWrite.WriteLine("G0")
                    oWrite.WriteLine("n")
                    oWrite.WriteLine("M0500")
                    oWrite.WriteLine("O0214")
                    oWrite.WriteLine("V0")
                    oWrite.WriteLine("t1")
                    oWrite.WriteLine("Kf0070")
                    oWrite.WriteLine("L")
                    oWrite.WriteLine("D11")
                    oWrite.WriteLine("ySPM")
                    oWrite.WriteLine("A2")
                    If GRIDDESC <> "" Then oWrite.WriteLine("1911C2401560027" & GRIDDESC) Else oWrite.WriteLine("1911C2401560027LINEN VENZO")
                    oWrite.WriteLine("1X1100001550005L263001")
                    oWrite.WriteLine("1e4203600230043B" & BARCODE)
                    oWrite.WriteLine("1911C1000060084" & BARCODE)
                    oWrite.WriteLine("1911C1401280011" & ITEMNAME)
                    oWrite.WriteLine("1911C1001090012SHADE")
                    oWrite.WriteLine("1911C1000610012WIDTH")
                    oWrite.WriteLine("1911C1001090077:")
                    oWrite.WriteLine("1911C1000610077:")
                    oWrite.WriteLine("1911C1401060086" & SHADE)
                    oWrite.WriteLine("1911C1000610086" & TEMPWIDTH)
                    oWrite.WriteLine("1911C1000840012MTRS")
                    oWrite.WriteLine("1911C1000840077:")
                    oWrite.WriteLine("1911C1400810086" & Format(Val(MTRS), "0.00"))
                    oWrite.WriteLine("1911C1001090162D. NO")
                    oWrite.WriteLine("1911C1001090204:")
                    oWrite.WriteLine("1911C1201080212" & DESIGNNO)
                    oWrite.WriteLine("1911C1000840162LOT")
                    oWrite.WriteLine("1911C1000840204:")
                    oWrite.WriteLine("1911C1000840213" & LOTNO)
                    oWrite.WriteLine("Q0001")
                    oWrite.WriteLine("E")
                    oWrite.Dispose()
                Else
                    oWrite.WriteLine("G0")
                    oWrite.WriteLine("n")
                    oWrite.WriteLine("M1379")
                    oWrite.WriteLine("O0214")
                    oWrite.WriteLine("V0")
                    oWrite.WriteLine("t1")
                    oWrite.WriteLine("Kf0070")
                    oWrite.WriteLine("L")
                    oWrite.WriteLine("D11")
                    oWrite.WriteLine("ySPM")
                    oWrite.WriteLine("A2")
                    oWrite.WriteLine("4e6303600190208B" & BARCODE)
                    oWrite.WriteLine("ySPM")
                    oWrite.WriteLine("4911C1200200230" & BARCODE)
                    oWrite.WriteLine("4911C2400200124" & ITEMNAME)
                    oWrite.WriteLine("4911C1803410186SHADE")
                    oWrite.WriteLine("4911C1200210150WIDTH")
                    oWrite.WriteLine("4911C1804340186:")
                    oWrite.WriteLine("4911C1804430190" & SHADE)
                    oWrite.WriteLine("4911C1803570222MTRS")
                    oWrite.WriteLine("4911C1804340222:")
                    oWrite.WriteLine("4911C2404440230" & Format(Val(MTRS), "0.00"))
                    oWrite.WriteLine("4911C1803580149D. NO")
                    oWrite.WriteLine("4911C1804340149:")
                    oWrite.WriteLine("4911C1804430153" & DESIGNNO)
                    oWrite.WriteLine("4911C1202180148LOT")
                    oWrite.WriteLine("4911C1202550149:")
                    oWrite.WriteLine("4911C1202690149" & LOTNO)
                    oWrite.WriteLine("4911C0802200225(Made In India)")
                    oWrite.WriteLine("4911C1201130149" & TEMPWIDTH)
                    oWrite.WriteLine("4911C1201000149:")
                    oWrite.WriteLine("Q0001")
                    oWrite.WriteLine("E")
                    oWrite.Dispose()
                End If



            ElseIf ClientName = "PARAS" Then

                oWrite.WriteLine("SIZE 99.10 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 620,371,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 600,374,""ROMAN.TTF"",180,1,16,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 782,371,""ROMAN.TTF"",180,1,14,""QUALITY""")
                oWrite.WriteLine("TEXT 782,310,""ROMAN.TTF"",180,1,14,""DESIGN""")
                oWrite.WriteLine("TEXT 600,310,""ROMAN.TTF"",180,1,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 620,310,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 360,310,""ROMAN.TTF"",180,1,14,""WIDTH""")
                oWrite.WriteLine("TEXT 237,310,""ROMAN.TTF"",180,1,14,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If
                oWrite.WriteLine("TEXT 211,310,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")


                oWrite.WriteLine("TEXT 782,249,""ROMAN.TTF"",180,1,14,""LOTNO""")
                oWrite.WriteLine("TEXT 620,249,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 600,249,""ROMAN.TTF"",180,1,14,""" & LOTNO & """")
                oWrite.WriteLine("TEXT 363,249,""ROMAN.TTF"",180,1,14,""SHADE""")
                oWrite.WriteLine("TEXT 231,249,""ROMAN.TTF"",180,1,14,"": """)
                oWrite.WriteLine("TEXT 211,249,""ROMAN.TTF"",180,1,14,""" & SHADE & """")
                oWrite.WriteLine("TEXT 782,187,""ROMAN.TTF"",180,1,14,""MTRS""")
                oWrite.WriteLine("TEXT 620,187,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("BARCODE 776,134,""128M"",83,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 499,47,""ROMAN.TTF"",180,1,11,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 600,192,""ROMAN.TTF"",180,1,18,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "ARIHANT" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("BARCODE 458,154,""128M"",106,0,180,2,4,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 359,42,""0"",180,10,10,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 459,377,""0"",180,16,16,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 458,318,""0"",180,12,12,""D.NO""")
                oWrite.WriteLine("TEXT 458,267,""0"",180,12,12,""WIDTH""")
                oWrite.WriteLine("TEXT 133,318,""0"",180,12,12,""" & LOTNO & """")
                oWrite.WriteLine("TEXT 458,210,""0"",180,12,12,""MTRS""")
                oWrite.WriteLine("TEXT 355,318,""0"",180,12,12,"":""")
                oWrite.WriteLine("TEXT 355,267,""0"",180,12,12,"":""")
                oWrite.WriteLine("TEXT 355,210,""0"",180,12,12,"":""")
                oWrite.WriteLine("TEXT 327,318,""0"",180,12,12,""" & DESIGNNO & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If
                oWrite.WriteLine("TEXT 327,267,""0"",180,12,12,""" & TEMPWIDTH & """")

                oWrite.WriteLine("TEXT 327,216,""0"",180,18,18,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "KEMLINO" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 581,354,""ROMAN.TTF"",180,1,19,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 738,282,""ROMAN.TTF"",180,1,14,""D.NO""")
                oWrite.WriteLine("TEXT 738,228,""ROMAN.TTF"",180,1,14,""SHADE""")
                oWrite.WriteLine("TEXT 738,172,""ROMAN.TTF"",180,1,14,""MTRS""")
                oWrite.WriteLine("TEXT 738,119,""ROMAN.TTF"",180,1,14,""UNIT""")
                oWrite.WriteLine("QRCODE 237,280,L,10,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 237,65,""ROMAN.TTF"",180,1,10,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 609,282,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 609,228,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 609,172,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 609,119,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 581,282,""ROMAN.TTF"",180,1,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 581,228,""ROMAN.TTF"",180,1,14,""" & SHADE & """")
                oWrite.WriteLine("TEXT 581,176,""ROMAN.TTF"",180,1,18,""" & Format(Val(MTRS), "0.00") & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If
                oWrite.WriteLine("TEXT 581,67,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")


                oWrite.WriteLine("TEXT 738,67,""ROMAN.TTF"",180,1,14,""WIDTH""")
                oWrite.WriteLine("TEXT 609,67,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 581,119,""ROMAN.TTF"",180,1,14,""" & UNIT & """")
                oWrite.WriteLine("TEXT 738,348,""ROMAN.TTF"",180,1,14,""PROD""")
                oWrite.WriteLine("TEXT 609,348,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("BAR 29,297, 708, 3")
                oWrite.WriteLine("TEXT 410,119,""ROMAN.TTF"",180,1,14,""" & LOTNO & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "KRFABRICS" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 573,375,""ROMAN.TTF"",180,1,22,""" & CmpName & """")
                oWrite.WriteLine("BAR 204,318, 369, 3")
                oWrite.WriteLine("TEXT 746,286,""ROMAN.TTF"",180,1,14,""QUALITY""")
                oWrite.WriteLine("TEXT 578,286,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 551,286,""ROMAN.TTF"",180,1,14,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 746,231,""ROMAN.TTF"",180,1,14,""D.NO""")
                oWrite.WriteLine("TEXT 578,231,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 551,231,""ROMAN.TTF"",180,1,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 746,174,""ROMAN.TTF"",180,1,14,""SHADE""")
                oWrite.WriteLine("TEXT 578,174,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 551,174,""ROMAN.TTF"",180,1,14,""" & SHADE & """")
                oWrite.WriteLine("TEXT 746,123,""ROMAN.TTF"",180,1,14,""MTRS""")
                oWrite.WriteLine("TEXT 578,123,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 551,128,""ROMAN.TTF"",180,1,18,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 404,118,""ROMAN.TTF"",180,1,10,""" & GRIDDESC & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 746,68,""ROMAN.TTF"",180,1,14,""WIDTH""")
                oWrite.WriteLine("TEXT 578,68,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 551,68,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")
                oWrite.WriteLine("QRCODE 248,231,L,9,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 248,39,""ROMAN.TTF"",180,1,9,""" & BARCODE & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "INDRAPUJAIMPEX" Then

                oWrite.WriteLine("SIZE 77.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 607,375,""ROMAN.TTF"",180,1,18,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 607,309,""ROMAN.TTF"",180,1,14,""DESIGN""")
                oWrite.WriteLine("TEXT 462,308,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 439,312,""ROMAN.TTF"",180,1,16,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 607,252,""ROMAN.TTF"",180,1,14,""SHADE""")
                oWrite.WriteLine("TEXT 462,252,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 439,255,""ROMAN.TTF"",180,1,16,""" & SHADE & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If
                oWrite.WriteLine("TEXT 289,200,""ROMAN.TTF"",180,1,14,""WIDTH""")
                oWrite.WriteLine("TEXT 165,200,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 142,200,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 607,200,""ROMAN.TTF"",180,1,14,""LOTNO""")
                oWrite.WriteLine("TEXT 462,200,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 439,200,""ROMAN.TTF"",180,1,14,""" & LOTNO & """")

                oWrite.WriteLine("TEXT 289,252,""ROMAN.TTF"",180,1,14,""MTRS""")
                oWrite.WriteLine("TEXT 165,252,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 142,257,""ROMAN.TTF"",180,1,18,""" & Format(Val(MTRS), "0.00") & """")

                oWrite.WriteLine("BARCODE 560,151,""128M"",94,0,180,3,6,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 385,52,""ROMAN.TTF"",180,1,9,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 588,37,""ROMAN.TTF"",180,1,10,""" & PIECETYPE & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "INDRANI" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='38.0 mm'></xpml>SIZE 47.5 mm, 38 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='38.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("BARCODE 367,130,""128M"",69,0,180,2,4,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 267,56,""0"",180,10,10,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 367,226,""0"",180,10,10,""D. NO""")
                oWrite.WriteLine("TEXT 279,226,""0"",180,10,10,"":""")
                oWrite.WriteLine("TEXT 263,226,""0"",180,10,10,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 367,180,""0"",180,10,10,""SO NO""")
                oWrite.WriteLine("TEXT 279,180,""0"",180,10,10,"":""")
                oWrite.WriteLine("TEXT 263,180,""0"",180,10,10,""" & BALENO & """")
                oWrite.WriteLine("TEXT 367,271,""0"",180,10,10,""ITEM""")
                oWrite.WriteLine("TEXT 279,271,""0"",180,10,10,"":""")
                oWrite.WriteLine("TEXT 263,271,""0"",180,10,10,""" & ITEMNAME & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()


            ElseIf ClientName = "DJIMPEX" Then

                oWrite.WriteLine("SIZE 99.10 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 768,362,""ROMAN.TTF"",180,1,14,""QUALITY""")
                oWrite.WriteLine("TEXT 768,303,""ROMAN.TTF"",180,1,14,""DESIGN""")
                oWrite.WriteLine("TEXT 768,244,""ROMAN.TTF"",180,1,14,""SHADE""")
                oWrite.WriteLine("TEXT 768,185,""ROMAN.TTF"",180,1,14,""WIDTH""")
                oWrite.WriteLine("TEXT 271,232,""ROMAN.TTF"",180,1,14,""MTRS""")
                oWrite.WriteLine("TEXT 614,362,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 614,303,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 614,244,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 614,185,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 170,235,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 593,362,""ROMAN.TTF"",180,1,14,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 593,303,""ROMAN.TTF"",180,1,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 593,244,""ROMAN.TTF"",180,1,14,""" & SHADE & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 593,185,""ROMAN.TTF"",180,1,14,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 149,235,""ROMAN.TTF"",180,1,16,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 768,133,""128M"",76,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 768,51,""ROMAN.TTF"",180,1,12,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 253,51,""ROMAN.TTF"",180,1,11,""WWW.DJIMPEX.IN""")
                oWrite.WriteLine("TEXT 270,185,""ROMAN.TTF"",180,1,14,""YDS""")
                oWrite.WriteLine("TEXT 170,185,""ROMAN.TTF"",180,1,14,"":""")
                oWrite.WriteLine("TEXT 149,189,""ROMAN.TTF"",180,1,16,""" & Format(Val(MTRS) * 1.094, "0.00") & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "RATAN" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 745,378,""0"",180,11,11,""QUALITY""")
                oWrite.WriteLine("TEXT 745,330,""0"",180,11,11,""DESIGN""")
                oWrite.WriteLine("TEXT 745,282,""0"",180,11,11,""SHADE""")
                oWrite.WriteLine("TEXT 308,186,""0"",180,11,11,""MTRS""")
                oWrite.WriteLine("TEXT 745,186,""0"",180,13,13,""WIDTH""")
                oWrite.WriteLine("BARCODE 745,126,""128M"",70,0,180,3,6,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 567,50,""0"",180,12,12,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 590,378,""0"",180,11,11,"":""")
                oWrite.WriteLine("TEXT 590,330,""0"",180,11,11,"":""")
                oWrite.WriteLine("TEXT 590,282,""0"",180,11,11,"":""")
                oWrite.WriteLine("TEXT 590,186,""0"",180,11,11,"":""")
                oWrite.WriteLine("TEXT 216,186,""0"",180,11,11,"":""")
                oWrite.WriteLine("TEXT 564,382,""0"",180,15,15,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 564,331,""0"",180,13,13,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 564,282,""0"",180,11,11,""" & SHADE & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 564,186,""0"",180,11,11,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 188,193,""0"",180,18,18,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 745,234,""0"",180,11,11,""LOT NO""")
                oWrite.WriteLine("TEXT 590,234,""0"",180,11,11,"":""")
                oWrite.WriteLine("TEXT 564,234,""0"",180,11,11,""" & LOTNO & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "KENCOT" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.8 mm'></xpml>SIZE 101.6 mm, 50.8 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("SPEED 4")
                oWrite.WriteLine("DENSITY 10")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.8 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 506,377,""ROMAN.TTF"",180,1,17,""" & ITEMNAME & """")
                oWrite.WriteLine("BARCODE 780,140,""128M"",85,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 484,50,""ROMAN.TTF"",180,1,9,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 780,298,""ROMAN.TTF"",180,1,14,""DESIGN NO""")
                oWrite.WriteLine("TEXT 321,298,""ROMAN.TTF"",180,1,14,""SHADE NO""")
                oWrite.WriteLine("TEXT 585,302,""ROMAN.TTF"",180,1,17,"":""")
                oWrite.WriteLine("TEXT 125,302,""ROMAN.TTF"",180,1,17,"":""")
                oWrite.WriteLine("TEXT 555,311,""ROMAN.TTF"",180,1,24,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 95,305,""ROMAN.TTF"",180,1,17,""" & SHADE & """")
                oWrite.WriteLine("TEXT 382,209,""ROMAN.TTF"",180,1,14,""WIDTH""")
                oWrite.WriteLine("TEXT 266,214,""ROMAN.TTF"",180,1,17,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 243,213,""0"",180,17,17,""" & TEMPWIDTH & """")

                oWrite.WriteLine("TEXT 677,214,""ROMAN.TTF"",180,1,17,"":""")
                oWrite.WriteLine("TEXT 780,209,""ROMAN.TTF"",180,1,14,""MTRS""")
                oWrite.WriteLine("TEXT 625,223,""ROMAN.TTF"",180,1,24,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 780,373,""ROMAN.TTF"",180,1,14,""MERCHANT NO :""")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "DRDRAPES" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 734,364,""0"",180,13,13,""Quality""")
                oWrite.WriteLine("TEXT 734,304,""0"",180,13,13,""Design""")
                oWrite.WriteLine("TEXT 735,246,""0"",180,13,13,""Shade""")
                oWrite.WriteLine("TEXT 734,128,""0"",180,13,13,""Mtrs""")
                oWrite.WriteLine("TEXT 615,362,""0"",180,13,13,"":""")
                oWrite.WriteLine("TEXT 615,304,""0"",180,13,13,"":""")
                oWrite.WriteLine("TEXT 615,246,""0"",180,13,13,"":""")
                oWrite.WriteLine("TEXT 615,128,""0"",180,13,13,"":""")
                oWrite.WriteLine("TEXT 595,362,""0"",180,13,13,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 595,306,""0"",180,14,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 595,248,""0"",180,14,14,""" & SHADE & """")
                oWrite.WriteLine("TEXT 595,131,""0"",180,14,14,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("QRCODE 275,253,L,8,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 288,67,""0"",180,10,10,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 735,187,""0"",180,13,13,""Lot No""")
                oWrite.WriteLine("TEXT 615,187,""0"",180,13,13,"":""")
                oWrite.WriteLine("TEXT 595,189,""0"",180,13,13,""" & LOTNO & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "SUCCESS" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='60.1 mm'></xpml>SIZE 77.5 mm, 60.1 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='60.1 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 582,259,""0"",180,16,16,""" & ITEMNAME & """")
                oWrite.WriteLine("BARCODE 529,346,""128M"",54,0,180,2,4,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 433,290,""0"",180,10,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 587,202,""0"",180,12,12,""D. NO""")
                oWrite.WriteLine("TEXT 473,202,""0"",180,12,12,"":""")
                oWrite.WriteLine("TEXT 453,202,""0"",180,12,12,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 264,202,""0"",180,12,12,""SHADE""")
                oWrite.WriteLine("TEXT 148,202,""0"",180,12,12,"":""")
                oWrite.WriteLine("TEXT 130,202,""0"",180,12,12,""" & SHADE & """")
                oWrite.WriteLine("TEXT 587,155,""0"",180,12,12,""GRADE""")
                oWrite.WriteLine("TEXT 473,155,""0"",180,12,12,"":""")
                oWrite.WriteLine("TEXT 453,155,""0"",180,12,12,""" & PIECETYPE & """")
                oWrite.WriteLine("TEXT 264,155,""0"",180,12,12,""MTRS""")
                oWrite.WriteLine("TEXT 148,155,""0"",180,12,12,"":""")
                oWrite.WriteLine("TEXT 130,155,""0"",180,12,12,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 521,110,""0"",180,7,7,""FAST TO NORMAL WASHING. BLENDED FABRIC""")
                oWrite.WriteLine("TEXT 462,82,""0"",180,7,7,""POLYSTER - 65%     VISCOSE - 35%""")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "YASHVI" Then

                'THIS IS PREPRINTED LABELS
                If TEMPHEADER = "Y" Then
                    oWrite.WriteLine("SIZE 47.5 mm, 150 mm")
                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")
                    oWrite.WriteLine("TEXT 9,728,""ROMAN.TTF"",270,1,14,""ITEM""")
                    oWrite.WriteLine("TEXT 62,728,""ROMAN.TTF"",270,1,14,""DESIGN""")
                    oWrite.WriteLine("TEXT 116,728,""ROMAN.TTF"",270,1,14,""SHADE""")
                    oWrite.WriteLine("TEXT 169,728,""ROMAN.TTF"",270,1,14,""MTRS""")
                    oWrite.WriteLine("TEXT 116,373,""ROMAN.TTF"",270,1,14,""WIDTH""")
                    oWrite.WriteLine("TEXT 9,590,""ROMAN.TTF"",270,1,14,"":""")
                    oWrite.WriteLine("TEXT 62,590,""ROMAN.TTF"",270,1,14,"":""")
                    oWrite.WriteLine("TEXT 116,590,""ROMAN.TTF"",270,1,14,"":""")
                    oWrite.WriteLine("TEXT 169,590,""ROMAN.TTF"",270,1,14,"":""")
                    oWrite.WriteLine("TEXT 116,260,""ROMAN.TTF"",270,1,14,"":""")
                    If GRIDDESC = "" Then
                        oWrite.WriteLine("TEXT 9,571,""ROMAN.TTF"",270,1,14,""" & ITEMNAME & """")
                        oWrite.WriteLine("TEXT 62,571,""ROMAN.TTF"",270,1,14,""" & DESIGNNO & """")
                    Else
                        oWrite.WriteLine("TEXT 9,571,""ROMAN.TTF"",270,1,14,""" & GRIDDESC & """")
                        oWrite.WriteLine("TEXT 62,571,""ROMAN.TTF"",270,1,14,""" & GRIDDESC & """")
                    End If
                    oWrite.WriteLine("TEXT 116,571,""ROMAN.TTF"",270,1,14,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 169,571,""ROMAN.TTF"",270,1,14,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("TEXT 169,451,""ROMAN.TTF"",270,1,14,""" & UNIT & """")

                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                        TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                        TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                    End If

                    oWrite.WriteLine("TEXT 116,240,""ROMAN.TTF"",270,1,14,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("BARCODE 217,751,""128M"",71,0,270,3,6,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 293,537,""ROMAN.TTF"",270,1,9,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 331,585,""ROMAN.TTF"",270,1,11,""" & TEMPREMARKS & """")
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()

                ElseIf TEMPHEADER = "B" Then
                    oWrite.WriteLine("<xpml><page quantity='0' pitch='100.1 mm'></xpml>SIZE 99.10 mm, 100.1 mm")
                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='100.1 mm'></xpml>SET TEAR ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")
                    oWrite.WriteLine("TEXT 710,591,""0"",180,16,16,""ITEM""")
                    oWrite.WriteLine("TEXT 710,532,""0"",180,16,16,""DESIGN""")
                    oWrite.WriteLine("TEXT 710,473,""0"",180,16,16,""SHADE""")
                    oWrite.WriteLine("TEXT 710,414,""0"",180,16,16,""MTRS""")
                    oWrite.WriteLine("TEXT 710,355,""0"",180,16,16,""WIDTH""")
                    oWrite.WriteLine("TEXT 555,591,""0"",180,16,16,"":""")
                    oWrite.WriteLine("TEXT 555,532,""0"",180,16,16,"":""")
                    oWrite.WriteLine("TEXT 555,473,""0"",180,16,16,"":""")
                    oWrite.WriteLine("TEXT 555,414,""0"",180,16,16,"":""")
                    oWrite.WriteLine("TEXT 555,355,""0"",180,16,16,"":""")
                    If GRIDDESC = "" Then
                        oWrite.WriteLine("TEXT 525,591,""0"",180,16,16,""" & ITEMNAME & """")
                        oWrite.WriteLine("TEXT 525,532,""0"",180,16,16,""" & DESIGNNO & """")
                    Else
                        oWrite.WriteLine("TEXT 525,591,""0"",180,16,16,""" & GRIDDESC & """")
                        oWrite.WriteLine("TEXT 525,532,""0"",180,16,16,""" & GRIDDESC & """")
                    End If
                    oWrite.WriteLine("TEXT 525,473,""0"",180,16,16,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 525,414,""0"",180,16,16,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("TEXT 390,408,""0"",180,14,14,""" & UNIT & """")

                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                        TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                        TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                    End If

                    oWrite.WriteLine("TEXT 525,355,""0"",180,16,16,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("QRCODE 281,521,L,10,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 276,306,""0"",180,10,10,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 583,265,""0"",180,14,14,""" & TEMPREMARKS & """")


                    oWrite.WriteLine("TEXT 515,177,""0"",180,11,11,""ITEM""")
                    oWrite.WriteLine("TEXT 515,137,""0"",180,11,11,""DESIGN""")
                    oWrite.WriteLine("TEXT 515,97,""0"",180,11,11,""SHADE""")
                    oWrite.WriteLine("TEXT 515,56,""0"",180,11,11,""MTRS""")
                    oWrite.WriteLine("TEXT 409,177,""0"",180,11,11,"":""")
                    oWrite.WriteLine("TEXT 409,137,""0"",180,11,11,"":""")
                    oWrite.WriteLine("TEXT 409,97,""0"",180,11,11,"":""")
                    oWrite.WriteLine("TEXT 409,56,""0"",180,11,11,"":""")
                    If GRIDDESC = "" Then
                        oWrite.WriteLine("TEXT 390,177,""0"",180,11,11,""" & ITEMNAME & """")
                        oWrite.WriteLine("TEXT 390,137,""0"",180,11,11,""" & DESIGNNO & """")
                    Else
                        oWrite.WriteLine("TEXT 390,177,""0"",180,11,11,""" & GRIDDESC & """")
                        oWrite.WriteLine("TEXT 390,137,""0"",180,11,11,""" & GRIDDESC & """")
                    End If
                    oWrite.WriteLine("TEXT 390,97,""0"",180,11,11,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 390,56,""0"",180,11,11,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("QRCODE 216,127,L,5,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 91,26,""0"",90,8,8,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                    oWrite.Dispose()

                Else
                    oWrite.WriteLine("SIZE 75.5 mm, 50 mm")
                    oWrite.WriteLine("GAP 3 mm, 0 mm")
                    oWrite.WriteLine("DIRECTION 0,0")
                    oWrite.WriteLine("REFERENCE 0,0")
                    oWrite.WriteLine("OFFSET 0 mm")
                    oWrite.WriteLine("SET PEEL OFF")
                    oWrite.WriteLine("SET CUTTER OFF")
                    oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                    oWrite.WriteLine("SET TEAR ON")
                    oWrite.WriteLine("ON")
                    oWrite.WriteLine("CLS")
                    oWrite.WriteLine("CODEPAGE 1252")
                    oWrite.WriteLine("TEXT 511,253,""ROMAN.TTF"",180,1,11,""QUALITY""")
                    oWrite.WriteLine("TEXT 511,220,""ROMAN.TTF"",180,1,11,""DESIGN""")
                    oWrite.WriteLine("TEXT 511,186,""ROMAN.TTF"",180,1,11,""SHADE NO""")
                    oWrite.WriteLine("TEXT 511,152,""ROMAN.TTF"",180,1,11,""MTRS""")
                    oWrite.WriteLine("TEXT 511,119,""ROMAN.TTF"",180,1,11,""WIDTH""")
                    oWrite.WriteLine("TEXT 335,253,""ROMAN.TTF"",180,1,11,"":""")
                    oWrite.WriteLine("TEXT 335,220,""ROMAN.TTF"",180,1,11,"":""")
                    oWrite.WriteLine("TEXT 335,186,""ROMAN.TTF"",180,1,11,"":""")
                    oWrite.WriteLine("TEXT 335,152,""ROMAN.TTF"",180,1,11,"":""")
                    oWrite.WriteLine("TEXT 335,119,""ROMAN.TTF"",180,1,11,"":""")
                    If GRIDDESC = "" Then
                        oWrite.WriteLine("TEXT 315,253,""ROMAN.TTF"",180,1,11,""" & ITEMNAME & """")
                        oWrite.WriteLine("TEXT 315,220,""ROMAN.TTF"",180,1,11,""" & DESIGNNO & """")
                    Else
                        oWrite.WriteLine("TEXT 315,253,""ROMAN.TTF"",180,1,11,""" & GRIDDESC & """")
                        oWrite.WriteLine("TEXT 315,220,""ROMAN.TTF"",180,1,11,""" & GRIDDESC & """")
                    End If
                    oWrite.WriteLine("TEXT 315,186,""ROMAN.TTF"",180,1,11,""" & SHADE & """")
                    oWrite.WriteLine("TEXT 315,152,""ROMAN.TTF"",180,1,11,""" & Format(Val(MTRS), "0.00") & """")
                    oWrite.WriteLine("TEXT 190,152,""ROMAN.TTF"",180,1,11,""" & UNIT & """")

                    'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                    Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                        TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                        TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                    End If

                    oWrite.WriteLine("TEXT 315,119,""ROMAN.TTF"",180,1,11,""" & TEMPWIDTH & """")
                    oWrite.WriteLine("TEXT 511,307,""ROMAN.TTF"",180,1,15,""" & TEMPHEADER & """")
                    oWrite.WriteLine("TEXT 31,257,""ROMAN.TTF"",270,1,8,""" & TEMPREMARKS & """")
                    oWrite.WriteLine("BARCODE 507,86,""128M"",43,0,180,2,4,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("TEXT 388,38,""ROMAN.TTF"",180,1,8,""" & BARCODE & """") 'BARCODE
                    oWrite.WriteLine("PRINT 1,1")
                    oWrite.Dispose()
                End If


            ElseIf ClientName = "TARUN" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 755,241,""0"",180,14,14,""DESIGN""")
                oWrite.WriteLine("TEXT 299,241,""0"",180,14,14,""SHADE""")
                oWrite.WriteLine("TEXT 755,184,""0"",180,14,14,""WIDTH""")
                oWrite.WriteLine("TEXT 755,352,""0"",180,14,14,""MERCHANT""")
                oWrite.WriteLine("TEXT 755,299,""0"",180,14,14,""QUALITY""")
                oWrite.WriteLine("BARCODE 767,136,""128M"",55,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 502,75,""0"",180,12,12,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 544,352,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 544,299,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 544,241,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 544,184,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 163,241,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 299,184,""0"",180,14,14,""MTRS""")
                oWrite.WriteLine("TEXT 163,184,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 516,352,""0"",180,14,14,""" & ITEMNAME & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                    TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                End If

                oWrite.WriteLine("TEXT 516,299,""0"",180,14,14,""" & TEMPCATEGORY & """")
                oWrite.WriteLine("TEXT 516,241,""0"",180,14,14,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 516,184,""0"",180,14,14,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 139,241,""0"",180,14,14,""" & SHADE & """")
                oWrite.WriteLine("TEXT 139,184,""0"",180,14,14,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "YUMILONE" Or ClientName = "REVAANT" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 721,375,""0"",180,16,16,""MERCHANT""")
                oWrite.WriteLine("TEXT 721,320,""0"",180,16,16,""DESIGN""")
                oWrite.WriteLine("TEXT 721,265,""0"",180,16,16,""SHADE""")
                oWrite.WriteLine("TEXT 721,210,""0"",180,16,16,""WIDTH""")
                oWrite.WriteLine("TEXT 296,210,""0"",180,16,16,""MTRS""")
                oWrite.WriteLine("BARCODE 728,143,""128M"",88,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 486,49,""0"",180,12,12,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 501,375,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 501,320,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 501,265,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 501,210,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 174,210,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 479,375,""0"",180,16,16,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 479,320,""0"",180,16,16,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 479,265,""0"",180,16,16,""" & SHADE & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                    TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                End If

                oWrite.WriteLine("TEXT 479,210,""0"",180,16,16,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 151,215,""0"",180,20,20,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "ALENCOT" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.8 mm'></xpml>G0")
                oWrite.WriteLine("n")
                oWrite.WriteLine("M0500")
                oWrite.WriteLine("O0214")
                oWrite.WriteLine("V0")
                oWrite.WriteLine("t1")
                oWrite.WriteLine("Kf0070")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.8 mm'></xpml>L")
                oWrite.WriteLine("D11")
                oWrite.WriteLine("ySPM")
                oWrite.WriteLine("A2")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                    TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                End If

                oWrite.WriteLine("1911C1401710016" & TEMPREMARKS)
                oWrite.WriteLine("1911C1200750149" & TEMPWIDTH)
                oWrite.WriteLine("1911C1201440070" & TEMPCATEGORY)

                oWrite.WriteLine("1911C1201440007Quality")
                oWrite.WriteLine("1911C1201210007Design")
                oWrite.WriteLine("1911C1001450063:")
                oWrite.WriteLine("1911C1201210070" & ITEMNAME)
                oWrite.WriteLine("1911C1001210063:")
                oWrite.WriteLine("1911C1200750007Mtrs")
                oWrite.WriteLine("1911C1000760063:")
                oWrite.WriteLine("1911C1200750070" & Format(Val(MTRS), "0.00"))
                oWrite.WriteLine("1e4204000300000B" & BARCODE)
                oWrite.WriteLine("1911A1200110028" & BARCODE)
                oWrite.WriteLine("1X1100101710011P0010001016900110169017701710177")
                oWrite.WriteLine("1911C1200980007Shade")
                oWrite.WriteLine("1911C1000990063:")
                oWrite.WriteLine("1911C1200980070" & SHADE)
                oWrite.WriteLine("Q0001")
                oWrite.WriteLine("E")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "AVIS" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.8 mm'></xpml>G0")
                oWrite.WriteLine("n")
                oWrite.WriteLine("M0739")
                oWrite.WriteLine("O0214")
                oWrite.WriteLine("V0")
                oWrite.WriteLine("t1")
                oWrite.WriteLine("Kf0070")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.8 mm'></xpml>L")
                oWrite.WriteLine("D11")
                oWrite.WriteLine("ySPM")
                oWrite.WriteLine("A2")
                oWrite.WriteLine("1911C1402500039Quality")
                oWrite.WriteLine("1911C1402230039D. No")
                oWrite.WriteLine("1911C1401950039Shade")
                oWrite.WriteLine("1911C1401670039Grade")
                oWrite.WriteLine("1911C1401390039Mtrs")
                oWrite.WriteLine("1e6303800410038B" & BARCODE)
                oWrite.WriteLine("1911C1200220120" & BARCODE)
                oWrite.WriteLine("1911C1402500118:")
                oWrite.WriteLine("1911C1402230118:")
                oWrite.WriteLine("1911C1401950118:")
                oWrite.WriteLine("1911C1401670118:")
                oWrite.WriteLine("1911C1401390118:")
                oWrite.WriteLine("1911C1402500141" & ITEMNAME)
                oWrite.WriteLine("1911C1402230141" & DESIGNNO)
                oWrite.WriteLine("1911C1401950141" & SHADE)
                oWrite.WriteLine("1911C1401670141" & UNIT)
                oWrite.WriteLine("1911C1401390141" & Format(Val(MTRS), "0.00"))
                If GRIDDESC <> "" Then oWrite.WriteLine("1911C1001180141 (" & GRIDDESC & ")")
                oWrite.WriteLine("1911C1400890039Lot No")
                oWrite.WriteLine("1911C1400890118:")
                oWrite.WriteLine("1911C1400890141" & LOTNO)
                oWrite.WriteLine("Q0001")
                oWrite.WriteLine("E")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "SURYODAYA" Then

                oWrite.WriteLine("SIZE 99.10 mm, 26.4 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 784,187,""ROMAN.TTF"",180,1,12,""MRC NAME""")
                oWrite.WriteLine("TEXT 612,187,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 592,187,""ROMAN.TTF"",180,1,12,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 784,140,""ROMAN.TTF"",180,1,12,""DESIGN NO""")
                oWrite.WriteLine("TEXT 612,140,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 592,140,""ROMAN.TTF"",180,1,12,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 784,93,""ROMAN.TTF"",180,1,12,""SHADE NO""")
                oWrite.WriteLine("TEXT 612,93,""ROMAN.TTF"",180,1,12,"":""")
                oWrite.WriteLine("TEXT 592,93,""ROMAN.TTF"",180,1,12,""" & SHADE & """")
                oWrite.WriteLine("TEXT 784,46,""ROMAN.TTF"",180,1,12,""METERS""")
                oWrite.WriteLine("TEXT 612,46,""ROMAN.TTF"",180,1,12,"":""")
                If UNIT <> "TP" Then UNIT = ""
                oWrite.WriteLine("TEXT 592,51,""ROMAN.TTF"",180,1,16,""" & Format(Val(MTRS), "0.00") & " " & UNIT & """")
                oWrite.WriteLine("BARCODE 387,120,""128M"",65,0,180,2,4,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 312,50,""ROMAN.TTF"",180,1,12,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 99,188,""ROMAN.TTF"",180,1,8,""(R)""")
                oWrite.WriteLine("TEXT 300,184,""ROMAN.TTF"",180,1,20,""" & GRIDDESC & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "SOFTAS" Then

                oWrite.WriteLine("G0")
                oWrite.WriteLine("n")
                oWrite.WriteLine("M0500")
                oWrite.WriteLine("O0214")
                oWrite.WriteLine("V0")
                oWrite.WriteLine("t1")
                oWrite.WriteLine("Kf0070")
                oWrite.WriteLine("L")
                oWrite.WriteLine("D11")
                oWrite.WriteLine("H18")
                oWrite.WriteLine("ySPM")
                oWrite.WriteLine("A2")

                oWrite.WriteLine("1911A1400740009WIDTH")
                If TEMPHEADER = "PRINTSERIES" Then
                    oWrite.WriteLine("1911C2401530009" & ITEMNAME)
                Else
                    If GRIDDESC <> "" Then oWrite.WriteLine("1911C2401530009" & GRIDDESC) Else oWrite.WriteLine("1911C2401530009" & ITEMNAME)
                End If
                oWrite.WriteLine("1911A1401030009SHADE")
                oWrite.WriteLine("1911A1401030089:")
                oWrite.WriteLine("1911A1401030100" & SHADE)
                oWrite.WriteLine("1911A1400740089:")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("1911A1400740100" & TEMPWIDTH)
                oWrite.WriteLine("1911A1400160009MTRS")
                oWrite.WriteLine("1911A1400160089:")
                oWrite.WriteLine("1911C1800120100" & Format(Val(MTRS), "0.00") & " " & QUALITY)
                oWrite.WriteLine("1W1D88000004402642,LA," & BARCODE)
                oWrite.WriteLine("1911A0800300264" & BARCODE)
                oWrite.WriteLine("1911A1400450009LOT NO")
                oWrite.WriteLine("1911A1400450089:")
                oWrite.WriteLine("1911A1400450100" & LOTNO)
                oWrite.WriteLine("1911A1401330009SERIES")
                oWrite.WriteLine("1911A1401330089:")
                If TEMPHEADER = "PRINTSERIES" Then oWrite.WriteLine("1911C1801290100" & GRIDDESC)
                oWrite.WriteLine("1911A1401620009ITEM")
                oWrite.WriteLine("1911A1401620089:")
                oWrite.WriteLine("1911A1400040261RACK")
                oWrite.WriteLine("1911A1400040320:")
                oWrite.WriteLine("1911A1400040332" & RACK)
                oWrite.WriteLine("Q0001")
                oWrite.WriteLine("E")
                oWrite.Dispose()

            ElseIf ClientName = "GELATO" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='70.1 mm'></xpml>G0")
                oWrite.WriteLine("n")
                oWrite.WriteLine("M0690")
                oWrite.WriteLine("O0214")
                oWrite.WriteLine("V0")
                oWrite.WriteLine("t1")
                oWrite.WriteLine("Kf0070")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='70.1 mm'></xpml>L")
                oWrite.WriteLine("D11")
                oWrite.WriteLine("H06")
                oWrite.WriteLine("ySPM")
                oWrite.WriteLine("A2")
                oWrite.WriteLine("4911C1400060021D.NO")
                oWrite.WriteLine("4911C1400750021:")
                oWrite.WriteLine("4911C1400930021" & DESIGNNO)

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPMRP As Double = 0
                Dim TEMPWSP As Double = 0
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGNMASTER.DESIGN_SALERATE, 0) AS MRP, ISNULL(DESIGNMASTER.DESIGN_WRATE,0) AS WSP", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & DESIGNNO & "' AND DESIGN_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPMRP = Val(DT.Rows(0).Item("MRP"))
                    TEMPWSP = Val(DT.Rows(0).Item("WSP"))
                End If

                oWrite.WriteLine("4911C1400060065STYLE")
                oWrite.WriteLine("4911C1400750065:")
                oWrite.WriteLine("4911C1400930065" & ITEMNAME)
                oWrite.WriteLine("4911C1400060041SIZE")
                oWrite.WriteLine("4911C1400750041:")
                oWrite.WriteLine("4911C1400930041" & SHADE)

                If UCase(UNIT) <> "MTRS" Then
                    oWrite.WriteLine("4911C1400060089QTY")
                    oWrite.WriteLine("4911C1400750089:")
                    oWrite.WriteLine("4911C14009300891")
                Else
                    oWrite.WriteLine("4911C1400060089MTRS")
                    oWrite.WriteLine("4911C1400750089: ")
                    oWrite.WriteLine("4911C1400930089" & Format(Val(MTRS), "0.00"))
                End If


                If TEMPHEADER = "2" Then
                    oWrite.WriteLine("4911C1401290091MRP")
                    oWrite.WriteLine("4911C1401750091:")
                    oWrite.WriteLine("4911C1401900091" & Val(TEMPMRP))
                    oWrite.WriteLine("4911C0801700106(BOYS / KIDS)")
                ElseIf TEMPHEADER = "3" Then
                    oWrite.WriteLine("4911C1401290091WSP")
                    oWrite.WriteLine("4911C1401750091:")
                    oWrite.WriteLine("4911C1401900091" & Val(TEMPWSP))
                    oWrite.WriteLine("4911C0801700106(BOYS / KIDS)")
                End If

                oWrite.WriteLine("4e6303200070140B" & BARCODE)
                oWrite.WriteLine("4911A0800070109" & BARCODE)
                oWrite.WriteLine("Q0001")
                oWrite.WriteLine("E")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "INDRAPUJAFABRICS" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='75.1 mm'></xpml>G0")
                oWrite.WriteLine("n")
                oWrite.WriteLine("M0739")
                oWrite.WriteLine("O0214")
                oWrite.WriteLine("V0")
                oWrite.WriteLine("t1")
                oWrite.WriteLine("Kf0070")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='75.1 mm'></xpml>L")
                oWrite.WriteLine("D11")
                oWrite.WriteLine("ySPM")
                oWrite.WriteLine("A2")
                oWrite.WriteLine("1911C1802610007" & ITEMNAME)
                oWrite.WriteLine("1911C1802180007D.NO")
                oWrite.WriteLine("1911C1802180098:")
                oWrite.WriteLine("1911C1802180110" & DESIGNNO)
                oWrite.WriteLine("1911C1801850007SHADE")
                oWrite.WriteLine("1911C1801850098:")
                oWrite.WriteLine("1911C1801850110" & SHADE)
                oWrite.WriteLine("1911C1801520007LOTNO")
                oWrite.WriteLine("1911C1801520098:")
                oWrite.WriteLine("1911C1801520110" & LOTNO)
                oWrite.WriteLine("1911C1800870217WIDTH")
                oWrite.WriteLine("1911C1801520284:")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("1911C1800870316" & TEMPWIDTH)
                oWrite.WriteLine("1911C1801520212MTRS")
                oWrite.WriteLine("1X1100001560211L071001")
                oWrite.WriteLine("1911C1800870308:")
                If UCase(GRIDDESC.Trim) = "TP" Then oWrite.WriteLine("1911C1801520292" & Format(Val(MTRS), "0.00") & "*") Else oWrite.WriteLine("1911C1801520292" & Format(Val(MTRS), "0.00"))
                oWrite.WriteLine("1e8405200290014B" & BARCODE)
                oWrite.WriteLine("1911A1000110132" & BARCODE)
                oWrite.WriteLine("1911C1801190007GRADE")
                oWrite.WriteLine("1911C1801190098:")
                oWrite.WriteLine("1911C1801190110FRESH")
                oWrite.WriteLine("1911C1800870007UNIT")
                oWrite.WriteLine("1911C1800870098:")
                oWrite.WriteLine("1911C1800870110" & UNIT)
                oWrite.WriteLine("1X1100002570004L362001")
                oWrite.WriteLine("Q0001")
                oWrite.WriteLine("E")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "SBA" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.8 mm'></xpml>G0")
                oWrite.WriteLine("n")
                oWrite.WriteLine("M0500")
                oWrite.WriteLine("O0214")
                oWrite.WriteLine("V0")
                oWrite.WriteLine("t1")
                oWrite.WriteLine("Kf0070")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.8 mm'></xpml>L")
                oWrite.WriteLine("D11")
                oWrite.WriteLine("ySPM")
                oWrite.WriteLine("A2")
                oWrite.WriteLine("1911A2401590067" & ITEMNAME)
                oWrite.WriteLine("1911A1001430011QUALITY")
                oWrite.WriteLine("1911A1001430079:")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH, TEMPCATEGORY, TEMPREMARKS, TEMPCMPSTAMP As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPCATEGORY = DT.Rows(0).Item("CATEGORY")
                    TEMPREMARKS = DT.Rows(0).Item("REMARKS")
                End If

                oWrite.WriteLine("1911A1001430090" & TEMPCATEGORY)
                oWrite.WriteLine("1911A1001240090" & TEMPREMARKS)
                oWrite.WriteLine("1911A1001070090" & TEMPWIDTH)

                oWrite.WriteLine("1911A1001070011WIDTH")
                oWrite.WriteLine("1911A1001070079:")

                oWrite.WriteLine("1911A1001070185DESIGN NO")
                oWrite.WriteLine("1911A1001070267:")
                oWrite.WriteLine("1911A1001070276" & DESIGNNO)
                oWrite.WriteLine("1e6304700360025B" & BARCODE)
                oWrite.WriteLine("1911A0800220128" & BARCODE)
                oWrite.WriteLine("1911A1000880011MTRS")
                oWrite.WriteLine("1911A1000880079:")
                oWrite.WriteLine("1911A1400850090" & Format(Val(MTRS), "0.00"))
                oWrite.WriteLine("1911A1000880185SHADE")
                oWrite.WriteLine("1911A1000880267:")
                oWrite.WriteLine("1911A1000880276" & SHADE)
                oWrite.WriteLine("1911A1000080140A PRODUCT OF ")
                oWrite.WriteLine("1X1100000010253L117028")
                oWrite.WriteLine("A1")
                DT = OBJCMN.SEARCH(" ISNULL(CMPMASTER.CMP_BUSINESSLINE, '') AS CMPSTAMP", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
                If DT.Rows.Count > 0 Then TEMPCMPSTAMP = DT.Rows(0).Item("CMPSTAMP")
                oWrite.WriteLine("1911A1800010255" & TEMPCMPSTAMP)

                oWrite.WriteLine("A2")
                oWrite.WriteLine("1X1100001610007L376003")

                oWrite.WriteLine("Q0001")
                oWrite.WriteLine("E")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "POOJA" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='38.0 mm'></xpml>SIZE 97.5 mm, 38 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='38.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 25,27,""0"",0,11,11,""ITEM""")
                oWrite.WriteLine("TEXT 149,27,""0"",0,11,11,"":""")
                oWrite.WriteLine("TEXT 172,22,""0"",0,16,16,""" & ITEMNAME & """")
                oWrite.WriteLine("BAR 22,73, 663, 3")
                oWrite.WriteLine("TEXT 25,87,""0"",0,11,11,""DESIGN""")
                oWrite.WriteLine("TEXT 149,87,""0"",0,11,11,"":""")
                oWrite.WriteLine("TEXT 172,86,""0"",0,11,11,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 296,87,""0"",0,11,11,""WIDTH""")
                oWrite.WriteLine("TEXT 395,87,""0"",0,11,11,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim TEMPHSNCODE As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY, ISNULL(HSN_CODE,'') AS HSNCODE", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPHSNCODE = DT.Rows(0).Item("HSNCODE")
                End If

                oWrite.WriteLine("TEXT 413,87,""0"",0,11,11,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 472,87,""0"",0,11,11,""HSN""")
                oWrite.WriteLine("TEXT 532,87,""0"",0,11,11,"":""")
                oWrite.WriteLine("TEXT 545,87,""0"",0,11,11,""" & TEMPHSNCODE & """")
                oWrite.WriteLine("TEXT 25,128,""0"",0,11,11,""SHADE""")
                oWrite.WriteLine("TEXT 149,128,""0"",0,11,11,"":""")
                oWrite.WriteLine("TEXT 172,128,""0"",0,11,11,""" & SHADE & """")
                oWrite.WriteLine("TEXT 296,128,""0"",0,11,11,""MTRS""")
                oWrite.WriteLine("TEXT 395,128,""0"",0,11,11,"":""")
                oWrite.WriteLine("TEXT 413,127,""0"",0,14,14,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 690,236,""0"",270,12,12,""K&B CLUB*""")
                oWrite.WriteLine("BAR 727,18, 3, 280")
                oWrite.WriteLine("TEXT 732,225,""0"",270,11,11,""MUMBAI-2""")
                oWrite.WriteLine("BARCODE 64,172,""128M"",82,0,0,3,6,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 248,259,""0"",0,10,10,""" & BARCODE & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "DETLINE" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.8 mm'></xpml>I8,A")
                oWrite.WriteLine("q792")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q406,25")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.8 mm'></xpml>N")
                oWrite.WriteLine("A762,304,2,1,3,3,N,""D. NO""")
                oWrite.WriteLine("A595,304,2,1,3,3,N,"":""")

                oWrite.WriteLine("A554,304,2,1,3,3,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A762,237,2,1,3,3,N,""SHADE""")
                oWrite.WriteLine("A595,237,2,1,3,3,N,"":""")
                oWrite.WriteLine("A554,237,2,1,3,3,N,""" & SHADE & """")
                oWrite.WriteLine("A762,173,2,1,3,3,N,""WIDTH""")
                oWrite.WriteLine("A595,173,2,1,3,3,N,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("A554,173,2,1,3,3,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("A423,239,2,1,3,3,N,""MTRS""")
                oWrite.WriteLine("A303,237,2,1,3,3,N,"":""")
                oWrite.WriteLine("A266,241,2,2,3,3,N,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("B762,119,2,1,3,6,65,N,""" & BARCODE & """")
                oWrite.WriteLine("A647,50,2,2,2,2,N,""" & BARCODE & """")
                If GRIDDESC <> "" Then oWrite.WriteLine("A521,381,2,2,3,3,N,""" & GRIDDESC & """")
                oWrite.WriteLine("LO246,326,298,3")
                oWrite.WriteLine("P1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()


            ElseIf ClientName = "MYCOT" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='100.1 mm'></xpml>SIZE 97.5 mm, 100.1 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='100.1 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 757,509,""2"",180,2,2,""DESIGN""")
                oWrite.WriteLine("TEXT 757,436,""2"",180,2,2,""SHADE""")
                oWrite.WriteLine("TEXT 757,366,""2"",180,2,2,""WIDTH""")
                oWrite.WriteLine("TEXT 366,366,""2"",180,2,2,""MTRS""")
                oWrite.WriteLine("BARCODE 767,294,""128M"",96,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 529,188,""1"",180,2,2,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 588,509,""2"",180,2,2,"":""")
                oWrite.WriteLine("TEXT 588,436,""2"",180,2,2,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 559,366,""2"",180,2,2,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 244,366,""2"",180,2,2,"":""")
                oWrite.WriteLine("TEXT 559,509,""2"",180,2,2,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 559,436,""2"",180,2,2,""" & SHADE & """")
                oWrite.WriteLine("TEXT 588,366,""2"",180,2,2,"":""")
                oWrite.WriteLine("TEXT 211,372,""3"",180,2,2,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "RMANILAL" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 72.5 mm, 45 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("SET RIBBON OFF")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 558,332,""0"",180,12,12,""ITEM""")
                oWrite.WriteLine("TEXT 442,332,""0"",180,12,12,"":""")
                oWrite.WriteLine("TEXT 420,338,""0"",180,16,16,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 558,279,""0"",180,12,12,""DESIGN""")
                oWrite.WriteLine("TEXT 442,279,""0"",180,12,12,"":""")
                oWrite.WriteLine("TEXT 420,279,""0"",180,12,12,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 558,225,""0"",180,12,12,""SHADE""")
                oWrite.WriteLine("TEXT 442,225,""0"",180,12,12,"":""")
                oWrite.WriteLine("TEXT 420,225,""0"",180,12,12,""" & SHADE & """")
                oWrite.WriteLine("TEXT 558,170,""0"",180,12,12,""WIDTH""")
                oWrite.WriteLine("TEXT 442,170,""0"",180,12,12,"":""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 420,170,""0"",180,12,12,""" & TEMPWIDTH & """")

                oWrite.WriteLine("TEXT 558,115,""0"",180,12,12,""MTRS""")
                oWrite.WriteLine("TEXT 442,115,""0"",180,12,12,"":""")
                oWrite.WriteLine("TEXT 420,121,""0"",180,16,16,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 558,61,""0"",180,12,12,""" & BARCODE & """")
                oWrite.WriteLine("QRCODE 202,209,L,8,A,180,M2,S7,""" & BARCODE & """") 'BARCODE

                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()


            ElseIf ClientName = "SUNCOTT" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='50.0 mm'></xpml>SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='50.0 mm'></xpml>SET TEAR ON")
                oWrite.WriteLine("ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 754,375,""0"",180,16,16,""ITEM""")
                oWrite.WriteLine("TEXT 754,316,""0"",180,16,16,""DESIGN""")
                oWrite.WriteLine("TEXT 754,258,""0"",180,16,16,""SHADE""")
                oWrite.WriteLine("TEXT 754,197,""0"",180,16,16,""WIDTH""")
                oWrite.WriteLine("TEXT 338,197,""0"",180,16,16,""MTRS""")
                oWrite.WriteLine("TEXT 592,375,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 592,316,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 592,258,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 592,197,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 210,197,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 564,380,""0"",180,20,20,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 564,316,""0"",180,16,16,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 564,258,""0"",180,16,16,""" & SHADE & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 564,197,""0"",180,16,16,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 190,201,""0"",180,20,20,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 767,135,""128M"",74,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 503,55,""0"",180,12,12,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 376,258,""0"",180,16,16,""LOT NO""")
                oWrite.WriteLine("TEXT 210,258,""0"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 187,258,""0"",180,16,16,""" & LOTNO & """")

                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "MANMANDIR" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 754,375,""ROMAN.TTF"",180,16,16,""ITEM""")
                oWrite.WriteLine("TEXT 754,318,""ROMAN.TTF"",180,16,16,""DESIGN""")
                oWrite.WriteLine("TEXT 754,258,""ROMAN.TTF"",180,16,16,""SHADE""")
                oWrite.WriteLine("TEXT 754,197,""ROMAN.TTF"",180,16,16,""WIDTH""")
                oWrite.WriteLine("TEXT 338,197,""ROMAN.TTF"",180,16,16,""MTRS""")
                oWrite.WriteLine("TEXT 592,375,""ROMAN.TTF"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 592,318,""ROMAN.TTF"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 592,258,""ROMAN.TTF"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 592,197,""ROMAN.TTF"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 210,197,""ROMAN.TTF"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 564,380,""ROMAN.TTF"",180,20,20,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 564,318,""ROMAN.TTF"",180,16,16,""" & DESIGNNO & """")
                oWrite.WriteLine("TEXT 564,258,""ROMAN.TTF"",180,16,16,""" & SHADE & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 564,197,""ROMAN.TTF"",180,16,16,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 190,201,""ROMAN.TTF"",180,20,20,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("BARCODE 767,135,""128M"",74,0,180,4,8,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 503,55,""ROMAN.TTF"",180,12,12,""" & BARCODE & """")
                oWrite.WriteLine("TEXT 358,318,""ROMAN.TTF"",180,16,16,""LOT NO""")
                oWrite.WriteLine("TEXT 192,318,""ROMAN.TTF"",180,16,16,"":""")
                oWrite.WriteLine("TEXT 160,318,""ROMAN.TTF"",180,16,16,""" & LOTNO & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "CC" Then

                oWrite.WriteLine("I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q416")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q192,25")
                oWrite.WriteLine("N")
                oWrite.WriteLine("B401,109,2,1,2,4,65,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A401,38,2,2,1,1,N,""" & BARCODE & """") 'BARCODE


                Dim CPCODE As String = "RP"
                Dim OBJCMN As New ClsCommon

                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_PURRATE,0) AS PURRATE, ISNULL(DESIGN_SALERATE,0) AS SALERATE, ISNULL(DESIGN_WRATE,0) AS WRATE", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & DESIGNNO & "' AND DESIGN_YEARID =  " & YearId)
                If DT.Rows.Count > 0 Then
                    'If WHOLESALEBARCODE = 7 Then oWrite.WriteLine("A228,151,2,1,2,2,N,""" & Val(DT.Rows(0).Item("SALERATE")) & "/-""") Else oWrite.WriteLine("A137,179,2,3,1,1,N,""" & Val(DT.Rows(0).Item("WRATE")) / 10 & """")
                    oWrite.WriteLine("A228,151,2,1,2,2,N,""" & Val(DT.Rows(0).Item("SALERATE")) & "/-""")
                    oWrite.WriteLine("A405,149,2,3,1,1,N,""" & "NO" & Val(DT.Rows(0).Item("WRATE")) & """")


                    'CODE FOR PURRATE (MEBTCPSHAN)
                    For POS As Integer = 0 To Len(Format(Val(DT.Rows(0).Item("PURRATE")), "0")) - 1
                        Select Case DT.Rows(0).Item("PURRATE").ToString.Substring(POS, 1)
                            Case "0"
                                CPCODE = CPCODE & "M"
                            Case "1"
                                CPCODE = CPCODE & "E"
                            Case "2"
                                CPCODE = CPCODE & "B"
                            Case "3"
                                CPCODE = CPCODE & "T"
                            Case "4"
                                CPCODE = CPCODE & "C"
                            Case "5"
                                CPCODE = CPCODE & "P"
                            Case "6"
                                CPCODE = CPCODE & "S"
                            Case "7"
                                CPCODE = CPCODE & "H"
                            Case "8"
                                CPCODE = CPCODE & "A"
                            Case "9"
                                CPCODE = CPCODE & "N"
                        End Select
                    Next
                    oWrite.WriteLine("A152,40,2,2,1,1,N,""" & CPCODE & """")


                Else
                    oWrite.WriteLine("A228,151,2,1,2,2,N,""")   'SALERATE
                    oWrite.WriteLine("A152,40,2,2,1,1,N,""")    'CPCODE
                End If

                oWrite.WriteLine("A405,175,2,2,1,1,N,""D.No""")
                oWrite.WriteLine("A359,175,2,2,1,1,N,"":""")
                oWrite.WriteLine("A345,175,2,2,1,1,N,""" & DESIGNNO & """")
                oWrite.WriteLine("P1")
                oWrite.Dispose()

            ElseIf ClientName = "C3" Then

                oWrite.WriteLine("SIZE 74.10 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")


                Dim CPCODE As String = "RP"
                Dim OBJCMN As New ClsCommon

                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_PURRATE,0) AS PURRATE, ISNULL(DESIGN_SALERATE,0) AS SALERATE, ISNULL(DESIGN_WRATE,0) AS WRATE, ISNULL(DESIGN_LINE1,'') AS LINE1, ISNULL(DESIGN_LINE2,0) AS LINE2", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & DESIGNNO & "' AND DESIGN_YEARID =  " & YearId)
                If DT.Rows.Count > 0 Then
                    'If WHOLESALEBARCODE = 7 Then oWrite.WriteLine("A228,151,2,1,2,2,N,""" & Val(DT.Rows(0).Item("SALERATE")) & "/-""") Else oWrite.WriteLine("A137,179,2,3,1,1,N,""" & Val(DT.Rows(0).Item("WRATE")) / 10 & """")
                    oWrite.WriteLine("TEXT 570,379,""ROMAN.TTF"",180,1,12,""" & DT.Rows(0).Item("LINE1") & """") 'DESIGN LINE1
                    oWrite.WriteLine("TEXT 570,330,""ROMAN.TTF"",180,1,12,""" & DT.Rows(0).Item("LINE2") & """") 'DESIGN LINE2
                    oWrite.WriteLine("TEXT 564,264,""ROMAN.TTF"",180,1,12,""" & "NO55" & Val(DT.Rows(0).Item("WRATE")) & "33""")
                    oWrite.WriteLine("TEXT 156,264,""ROMAN.TTF"",180,1,12,""" & "9981" & Val(DT.Rows(0).Item("SALERATE")) & """")

                    oWrite.WriteLine("BARCODE 563,198,""128M"",102,0,180,3,6,""" & BARCODE & """")
                    oWrite.WriteLine("TEXT 563,91,""ROMAN.TTF"",180,1,12,""" & BARCODE & """")


                    'CODE FOR PURRATE (MEBTCPSHAN)
                    For POS As Integer = 0 To Len(Format(Val(DT.Rows(0).Item("PURRATE")), "0")) - 1
                        Select Case DT.Rows(0).Item("PURRATE").ToString.Substring(POS, 1)
                            Case "0"
                                CPCODE = CPCODE & "M"
                            Case "1"
                                CPCODE = CPCODE & "E"
                            Case "2"
                                CPCODE = CPCODE & "B"
                            Case "3"
                                CPCODE = CPCODE & "T"
                            Case "4"
                                CPCODE = CPCODE & "C"
                            Case "5"
                                CPCODE = CPCODE & "P"
                            Case "6"
                                CPCODE = CPCODE & "S"
                            Case "7"
                                CPCODE = CPCODE & "H"
                            Case "8"
                                CPCODE = CPCODE & "A"
                            Case "9"
                                CPCODE = CPCODE & "N"
                        End Select
                    Next
                    oWrite.WriteLine("TEXT 200,75,""ROMAN.TTF"",180,1,12,""" & CPCODE & """")
                End If
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()


            ElseIf ClientName = "PURPLE" Then

                oWrite.WriteLine("I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q401")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q304,25")
                oWrite.WriteLine("N")
                oWrite.WriteLine("B389,114,2,1,2,4,80,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A298,28,2,3,1,1,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A380,286,2,4,1,1,N,""Item""")
                oWrite.WriteLine("A311,286,2,4,1,1,N,"":""")
                oWrite.WriteLine("A283,286,2,4,1,1,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A380,243,2,4,1,1,N,""D.No""")
                oWrite.WriteLine("A311,243,2,4,1,1,N,"":""")
                oWrite.WriteLine("A283,243,2,4,1,1,N,""" & DESIGNNO & """")


                Dim CPCODE As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(DESIGN_PURRATE,0) AS PURRATE, ISNULL(DESIGN_SALERATE,0) AS SALERATE, ISNULL(DESIGN_WRATE,0) AS WRATE, ISNULL(DESIGN_PURRATE,0) AS PRATE", "", " DESIGNMASTER ", " AND DESIGN_NO = '" & DESIGNNO & "' AND DESIGN_YEARID =  " & YearId)
                If DT.Rows.Count > 0 Then
                    oWrite.WriteLine("A283,200,2,4,1,1,N,""" & Val(DT.Rows(0).Item("SALERATE")) & "/-""")
                    oWrite.WriteLine("A117,200,2,4,1,1,N,""19" & Val(DT.Rows(0).Item("WRATE")) & "6""")

                    'CODE FOR PURRATE (LOTUSDAIRY)
                    For POS As Integer = 0 To Len(Format(Val(DT.Rows(0).Item("PRATE")), "0")) - 1
                        Select Case DT.Rows(0).Item("PRATE").ToString.Substring(POS, 1)
                            Case "1"
                                CPCODE = CPCODE & "L"
                            Case "2"
                                CPCODE = CPCODE & "O"
                            Case "3"
                                CPCODE = CPCODE & "T"
                            Case "4"
                                CPCODE = CPCODE & "U"
                            Case "5"
                                CPCODE = CPCODE & "S"
                            Case "6"
                                CPCODE = CPCODE & "D"
                            Case "7"
                                CPCODE = CPCODE & "A"
                            Case "8"
                                CPCODE = CPCODE & "I"
                            Case "9"
                                CPCODE = CPCODE & "R"
                            Case "0"
                                CPCODE = CPCODE & "Y"
                        End Select
                    Next

                    oWrite.WriteLine("A380,157,2,4,1,1,N,""" & CPCODE & """")

                Else
                    oWrite.WriteLine("A283,200,2,4,1,1,N,""")    'SALERATE
                    oWrite.WriteLine("A117,200,2,4,1,1,N,""")    'WHOLESALERATE
                    oWrite.WriteLine("A380,157,2,4,1,1,N,""")    'PURRATE
                End If

                oWrite.WriteLine("A380,200,2,4,1,1,N,""MRP""")
                oWrite.WriteLine("A311,200,2,4,1,1,N,"":""")
                oWrite.WriteLine("A149,157,2,4,1,1,N,""" & SHADE & """")
                oWrite.WriteLine("P1")
                oWrite.Dispose()

            ElseIf ClientName = "MNARESH" Then

                oWrite.WriteLine("I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q799")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("KIZZQ0")
                oWrite.WriteLine("KI9+0.0")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q400,25")
                oWrite.WriteLine("Arglabel 500 31")
                oWrite.WriteLine("exit")
                oWrite.WriteLine("N")
                oWrite.WriteLine("A770,367,2,2,2,2,N,""ITEM""")
                oWrite.WriteLine("B776,132,2,1,4,8,78,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A538,48,2,1,2,2,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A770,200,2,2,2,2,N,""BALE""")
                oWrite.WriteLine("A651,367,2,2,2,2,N,"":""")
                oWrite.WriteLine("A651,200,2,2,2,2,N,"":""")
                oWrite.WriteLine("A625,367,2,2,2,2,N,""" & ITEMNAME & """")

                'IT WAS WIDTH INSTAEAD OF BALENO, WE HAVE CHANGED AS PER THEIR REQUEST
                ''GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                'Dim TEMPWIDTH As String
                'Dim OBJCMN As New ClsCommon
                'Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                'If DT.Rows.Count > 0 Then
                '    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                'End If

                oWrite.WriteLine("A625,200,2,2,2,2,N,""" & BALENO & """")
                oWrite.WriteLine("A289,214,2,3,3,3,N,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("A421,200,2,2,2,2,N,""MTRS""")
                oWrite.WriteLine("A318,200,2,2,2,2,N,"":""")
                oWrite.WriteLine("A770,256,2,2,2,2,N,""SHADE""")
                oWrite.WriteLine("A651,256,2,2,2,2,N,"":""")
                oWrite.WriteLine("A625,256,2,2,2,2,N,""" & SHADE & """")
                oWrite.WriteLine("A770,312,2,2,2,2,N,""D.NO""")
                oWrite.WriteLine("A651,312,2,2,2,2,N,"":""")
                oWrite.WriteLine("A625,312,2,2,2,2,N,""" & DESIGNNO & """")
                oWrite.WriteLine("P1")
                oWrite.Dispose()

            ElseIf ClientName = "MANINATH" Then

                oWrite.WriteLine("I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q812")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q406,25")
                oWrite.WriteLine("N")
                oWrite.WriteLine("A772,386,2,4,2,2,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A772,310,2,3,2,2,N,""D.NO""")
                oWrite.WriteLine("A772,243,2,3,2,2,N,""SHADE""")
                oWrite.WriteLine("A772,174,2,3,2,2,N,""MTRS""")
                oWrite.WriteLine("B772,110,2,1,3,6,67,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A592,37,2,4,1,1,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A367,174,2,3,2,2,N,""WIDTH""")
                oWrite.WriteLine("A608,310,2,3,2,2,N,"":""")
                oWrite.WriteLine("A608,243,2,3,2,2,N,"":""")
                oWrite.WriteLine("A608,174,2,3,2,2,N,"":""")
                oWrite.WriteLine("A219,174,2,3,2,2,N,"":""")
                oWrite.WriteLine("A580,310,2,3,2,2,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A580,243,2,3,2,2,N,""" & SHADE & """")
                oWrite.WriteLine("A580,174,2,3,2,2,N,""" & Format(Val(MTRS), "0.00") & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("A184,174,2,3,2,2,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("P1")
                oWrite.Dispose()

            ElseIf ClientName = "DEVEN" Then

                oWrite.WriteLine("I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q609")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("KIZZQ0")
                oWrite.WriteLine("KI9+0.0")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q426,25")
                oWrite.WriteLine("Arglabel 533 31")
                oWrite.WriteLine("exit")
                oWrite.WriteLine("N")
                oWrite.WriteLine("A562,385,2,2,3,3,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A563,313,2,1,2,2,N,""LOT""")
                oWrite.WriteLine("A456,313,2,1,2,2,N,"":""")
                oWrite.WriteLine("A433,313,2,1,2,2,N,""" & LOTNO & """")
                oWrite.WriteLine("A202,313,2,1,2,2,N,""CMS""")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("A105,313,2,1,2,2,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("A133,313,2,1,2,2,N,"":""")
                oWrite.WriteLine("A563,259,2,1,2,2,N,""D NO""")
                oWrite.WriteLine("A455,259,2,1,2,2,N,"":""")
                oWrite.WriteLine("A432,259,2,1,2,2,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A223,259,2,1,2,2,N,""S NO""")
                oWrite.WriteLine("A104,259,2,1,2,2,N,""" & SHADE & """")
                oWrite.WriteLine("A132,259,2,1,2,2,N,"":""")
                oWrite.WriteLine("A563,206,2,1,2,2,N,""MTRS""")
                oWrite.WriteLine("A455,206,2,1,2,2,N,"":""")
                oWrite.WriteLine("A432,206,2,1,3,3,N,""" & MTRS & """")
                oWrite.WriteLine("B583,142,2,1,3,6,89,N,""" & BARCODE & """")
                oWrite.WriteLine("A411,47,2,4,1,1,N,""" & BARCODE & """")
                oWrite.WriteLine("P1")
                oWrite.Dispose()

            ElseIf ClientName = "AXIS" Then

                oWrite.WriteLine("I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q401")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q304,B25")
                oWrite.WriteLine("KI80")
                oWrite.WriteLine("N")
                oWrite.WriteLine("B388,94,2,1,2,4,62,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A269,26,2,1,1,1,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A395,289,2,4,1,1,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A284,149,2,1,2,2,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A381,196,2,1,2,2,N,""SHADE""")
                oWrite.WriteLine("A209,149,2,1,2,2,N,""MRP""")
                If TEMPHEADER = "1" Then
                    oWrite.WriteLine("A380,242,2,1,2,2,N,""QTY""")
                    oWrite.WriteLine("A284,242,2,1,2,2,N,"":""")
                    oWrite.WriteLine("A265,242,2,1,2,2,N,""1""")
                Else
                    oWrite.WriteLine("A380,242,2,1,2,2,N,""MTRS""")
                    oWrite.WriteLine("A284,242,2,1,2,2,N,"":""")
                    If Val(MTRS) > 0 Then oWrite.WriteLine("A265,242,2,1,2,2,N,""" & Format(Val(MTRS), "0.00") & """")
                End If
                oWrite.WriteLine("A284,194,2,1,2,2,N,"":""")
                oWrite.WriteLine("A265,194,2,1,2,2,N,""" & SHADE & """")
                oWrite.WriteLine("A149,149,2,1,2,2,N,"":""")

                'GET MRP FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim TEMPMRP As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_RATE, '') AS RATE, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPMRP = Val(DT.Rows(0).Item("RATE"))
                End If

                oWrite.WriteLine("A131,149,2,1,2,2,N,""" & TEMPMRP & "/-""")
                oWrite.WriteLine("A380,149,2,1,2,2,N,""SIZE""")
                oWrite.WriteLine("A301,149,2,1,2,2,N,"":""")
                oWrite.WriteLine("A203,114,2,1,1,1,N,""(Incl. of All Taxes)""")
                oWrite.WriteLine("P1")
                oWrite.Dispose()

            ElseIf ClientName = "SANGHVI" Then

                oWrite.WriteLine("I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q428")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q400,25")
                oWrite.WriteLine("N")
                oWrite.WriteLine("A404,196,2,4,1,1,N,""MTRS""")
                oWrite.WriteLine("A404,152,2,4,1,1,N,""COLOR""")
                oWrite.WriteLine("A325,196,2,4,1,1,N,"":""")
                oWrite.WriteLine("A325,152,2,4,1,1,N,"":""")
                oWrite.WriteLine("A368,363,2,4,1,1,N,""TINU MINU EMBROIDERY""")
                oWrite.WriteLine("A404,240,2,4,1,1,N,""WIDTH""")
                oWrite.WriteLine("A304,196,2,4,1,1,N,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("A304,152,2,4,1,1,N,""" & SHADE & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("A304,240,2,4,1,1,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("A325,240,2,4,1,1,N,"":""")
                oWrite.WriteLine("b24,161,Q,m2,s6,eL,iA,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A404,329,2,4,1,1,N,""QLTY""")
                oWrite.WriteLine("A325,329,2,4,1,1,N,"":""")
                oWrite.WriteLine("A304,329,2,4,1,1,N,""" & QUALITY & """")
                oWrite.WriteLine("A199,196,2,4,1,1,N,""" & BALENO & """")
                oWrite.WriteLine("A404,285,2,4,1,1,N,""D.NO""")
                oWrite.WriteLine("A325,285,2,4,1,1,N,"":""")
                oWrite.WriteLine("A304,285,2,4,1,1,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A405,112,2,4,1,1,N,""" & BARCODE & """")
                oWrite.WriteLine("P1")
                oWrite.Dispose()

            ElseIf ClientName = "TINUMINU" Then

                oWrite.WriteLine("SIZE 97.5 mm, 50 mm")
                oWrite.WriteLine("GAP 3 mm, 0 mm")
                oWrite.WriteLine("DIRECTION 0,0")
                oWrite.WriteLine("REFERENCE 0,0")
                oWrite.WriteLine("OFFSET 0 mm")
                oWrite.WriteLine("SET PEEL OFF")
                oWrite.WriteLine("SET CUTTER OFF")
                oWrite.WriteLine("SET PARTIAL_CUTTER OFF")
                oWrite.WriteLine("SET TEAR ON")
                oWrite.WriteLine("CLS")
                oWrite.WriteLine("CODEPAGE 1252")
                oWrite.WriteLine("TEXT 591,368,""0"",180,14,14,,"":""")
                oWrite.WriteLine("TEXT 562,368,""0"",180,14,14,""" & QUALITY & """")
                oWrite.WriteLine("TEXT 747,317,""0"",180,14,14,""ITEM""")
                oWrite.WriteLine("TEXT 591,317,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 562,317,""0"",180,14,14,""" & ITEMNAME & """")
                oWrite.WriteLine("TEXT 747,266,""0"",180,14,14,""SHADE""")
                oWrite.WriteLine("TEXT 591,266,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 562,266,""0"",180,14,14,""" & SHADE & """")
                oWrite.WriteLine("TEXT 747,215,""0"",180,14,14,""WIDTH""")
                oWrite.WriteLine("TEXT 591,215,""0"",180,14,14,"":""")


                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(ITEMMASTER.ITEM_REMARKS, '') AS REMARKS, ISNULL(CATEGORYMASTER.CATEGORY_NAME, '') AS CATEGORY", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                End If

                oWrite.WriteLine("TEXT 562,215,""0"",180,14,14,""" & TEMPWIDTH & """")
                oWrite.WriteLine("TEXT 747,164,""0"",180,14,14,""MTRS""")
                oWrite.WriteLine("TEXT 591,164,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 562,164,""0"",180,14,14,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("TEXT 747,114,""0"",180,14,14,""LOTNO""")
                oWrite.WriteLine("TEXT 591,114,""0"",180,14,14,"":""")
                oWrite.WriteLine("TEXT 562,114,""0"",180,14,14,""" & LOTNO & """")
                oWrite.WriteLine("TEXT 747,62,""0"",180,14,14,""" & BARCODE & """")
                oWrite.WriteLine("QRCODE 300,264,L,10,A,180,M2,S7,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("TEXT 747,368,""0"",180,14,14,""QUALITY""")
                oWrite.WriteLine("TEXT 44,126,""0"",90,9,9,""" & ENTRYDATE & """")
                oWrite.WriteLine("PRINT 1,1")
                oWrite.Dispose()

            ElseIf ClientName = "KDFAB" Then

                oWrite.WriteLine("<xpml><page quantity='0' pitch='101.6 mm'></xpml>I8,A")
                oWrite.WriteLine("ZN")
                oWrite.WriteLine("q792")
                oWrite.WriteLine("O")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("KIZZQ0")
                oWrite.WriteLine("KI9+0.0")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("Q812,25")
                oWrite.WriteLine("Arglabel 1016 31")
                oWrite.WriteLine("exit")
                oWrite.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='101.6 mm'></xpml>N")
                oWrite.WriteLine("B761,309,2,1,3,6,161,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A717,134,2,4,2,2,N,""" & BARCODE & """") 'BARCODE
                oWrite.WriteLine("A754,563,2,3,2,2,N,""NAME""")
                oWrite.WriteLine("A754,477,2,3,2,2,N,""D.NO""")
                oWrite.WriteLine("A754,391,2,3,2,2,N,""MTRS""")
                oWrite.WriteLine("A637,563,2,3,2,2,N,"":""")
                oWrite.WriteLine("A637,477,2,3,2,2,N,"":""")
                oWrite.WriteLine("A637,391,2,3,2,2,N,"":""")
                oWrite.WriteLine("A606,563,2,3,2,2,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A606,477,2,3,2,2,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A595,400,2,2,4,4,N,""" & Format(Val(MTRS), "0.00") & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")

                oWrite.WriteLine("A113,477,2,3,2,2,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("A294,391,2,3,2,2,N,""" & BALENO & """")
                oWrite.WriteLine("P1")
                oWrite.WriteLine("<xpml></page></xpml><xpml><end/></xpml>")
                oWrite.Dispose()

            ElseIf ClientName = "BRILLANTO" Then

                oWrite.WriteLine("I8,A,001")
                oWrite.WriteLine("")
                oWrite.WriteLine("")
                oWrite.WriteLine("Q384,024")
                oWrite.WriteLine("q863")
                oWrite.WriteLine("rN")
                oWrite.WriteLine("S3")
                oWrite.WriteLine("D14")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("O")
                oWrite.WriteLine("R253,0")
                oWrite.WriteLine("f100")
                oWrite.WriteLine("N")
                oWrite.WriteLine("A341,164,2,3,1,1,N,""Grade""")
                oWrite.WriteLine("A342,202,2,3,1,1,N,""Shade No.""")
                oWrite.WriteLine("A344,238,2,3,1,1,N,""Width""")
                oWrite.WriteLine("A344,274,2,3,1,1,N,""Mtrs""")
                oWrite.WriteLine("A342,309,2,3,1,1,N,""M. Name""")
                oWrite.WriteLine("A213,164,2,3,1,1,N,"":""")
                oWrite.WriteLine("A213,202,2,3,1,1,N,"":""")
                oWrite.WriteLine("A213,238,2,3,1,1,N,"":""")
                oWrite.WriteLine("A213,274,2,3,1,1,N,"":""")
                oWrite.WriteLine("A213,309,2,3,1,1,N,"":""")
                oWrite.WriteLine("A213,345,2,3,1,1,N,"":""")
                oWrite.WriteLine("A198,164,2,3,1,1,N,""" & PIECETYPE & """")
                oWrite.WriteLine("A198,202,2,3,1,1,N,""" & SHADE & """")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                oWrite.WriteLine("A198,238,2,3,1,1,N,""" & TEMPWIDTH & """")
                oWrite.WriteLine("A111,273,2,3,1,1,N,""" & UNIT & """")

                oWrite.WriteLine("A198,274,2,3,1,1,N,""" & Format(Val(MTRS), "0.00") & """")
                oWrite.WriteLine("A198,309,2,3,1,1,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A198,345,2,3,1,1,N,""" & DESIGNNO & """")
                oWrite.WriteLine("A342,345,2,3,1,1,N,""Design No""")
                oWrite.WriteLine("B352,122,2,1,2,6,81,B,""" & BARCODE & """")

                oWrite.WriteLine("P1")
                oWrite.Dispose()

            ElseIf ClientName = "TCOT" Then

                oWrite.WriteLine("G0")
                oWrite.WriteLine("n")
                oWrite.WriteLine("M0690")
                oWrite.WriteLine("O0214")
                oWrite.WriteLine("V0")
                oWrite.WriteLine("t1")
                oWrite.WriteLine("Kf0070")
                oWrite.WriteLine("L")
                oWrite.WriteLine("D11")
                oWrite.WriteLine("ySPM")
                oWrite.WriteLine("A2")
                oWrite.WriteLine("1911C1202480037QLTY")
                oWrite.WriteLine("1911C1202250037DSGN.NO")
                oWrite.WriteLine("1911C1202040037CH.NO.")
                oWrite.WriteLine("1911C1201820037SHD.NO.")
                oWrite.WriteLine("1911C1201600037LOT NO")
                oWrite.WriteLine("1911C1201380037WIDTH")
                oWrite.WriteLine("1911C1201160037MTRS")
                oWrite.WriteLine("1911C1200940037GRADE")
                oWrite.WriteLine("1911C1200710037RACK")
                oWrite.WriteLine("1911C1202480124:")
                oWrite.WriteLine("1911C1202250124:")
                oWrite.WriteLine("1911C1202040124:")
                oWrite.WriteLine("1911C1201820124:")
                oWrite.WriteLine("1911C1201600124:")
                oWrite.WriteLine("1911C1200940124:")
                oWrite.WriteLine("1911C1201160124:")
                oWrite.WriteLine("1911C1201380124:")
                oWrite.WriteLine("1911C1200710124:")
                oWrite.WriteLine("1e6303300310036B" & BARCODE)
                oWrite.WriteLine("1911C1200110114" & BARCODE)
                oWrite.WriteLine("1911C1202480138" & ITEMNAME)
                oWrite.WriteLine("1911C1202250138" & DESIGNNO)
                oWrite.WriteLine("1911C1202040138" & BALENO)
                oWrite.WriteLine("1911C1201820138" & SHADE)
                oWrite.WriteLine("1911C1201600138" & LOTNO)

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")


                oWrite.WriteLine("1911C1201380138" & TEMPWIDTH)
                oWrite.WriteLine("1911C1201160138" & Format(Val(MTRS), "0.00"))
                oWrite.WriteLine("1911C1200710138" & RACK)
                oWrite.WriteLine("1911C1200940138" & PIECETYPE)
                oWrite.WriteLine("Q0001")
                oWrite.WriteLine("E")

                oWrite.Dispose()


            ElseIf ClientName = "MANS" Then

                oWrite.WriteLine("I8,A,001")
                oWrite.WriteLine("")
                oWrite.WriteLine("")
                oWrite.WriteLine("Q400,024")
                oWrite.WriteLine("q831")
                oWrite.WriteLine("rN")
                oWrite.WriteLine("S5")
                oWrite.WriteLine("D15")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("O")
                oWrite.WriteLine("R116,0")
                oWrite.WriteLine("f100")
                oWrite.WriteLine("N")
                oWrite.WriteLine("A401,166,2,2,2,2,N,""" & Format(Val(MTRS), "0.00") & """")    'MTRS
                oWrite.WriteLine("A402,216,2,1,2,2,N,""" & QUALITY & """")
                oWrite.WriteLine("A402,271,2,2,2,2,N,""" & SHADE & """")       'COLOR
                oWrite.WriteLine("A571,216,2,1,2,2,N,""BLEND""")
                oWrite.WriteLine("A572,166,2,1,2,2,N,""METER""")
                oWrite.WriteLine("A572,265,2,1,2,2,N,""SHADE""")
                oWrite.WriteLine("A428,166,2,1,2,2,N,"":""")
                oWrite.WriteLine("A402,314,2,1,2,2,N,""" & ITEMNAME & """")
                oWrite.WriteLine("A428,216,2,1,2,2,N,"":""")
                oWrite.WriteLine("A428,265,2,1,2,2,N,"":""")
                If TEMPHEADER = "1" Then
                    'oWrite.WriteLine("A457,381,2,3,2,2,N,""Salvatroe""")
                ElseIf TEMPHEADER = "2" Then
                    oWrite.WriteLine("A457,381,2,3,2,2,N,""D o n B i o n""")
                Else
                    oWrite.WriteLine("A457,381,2,3,2,2,N,""OCM - PREMIZA""")
                End If
                oWrite.WriteLine("A572,314,2,1,2,2,N,""PRODUCT""")
                oWrite.WriteLine("A428,314,2,1,2,2,N,"":""")
                oWrite.WriteLine("B572,105,2,1,3,9,63,B,""" & BARCODE & """")       'BARCODE
                oWrite.WriteLine("P1")

                oWrite.Dispose()

            ElseIf ClientName = "ANOX" Then

                oWrite.WriteLine("CT~~CD,~CC^~CT~")
                oWrite.WriteLine("^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR6,6~SD10^JUS^LRN^CI0^XZ")
                oWrite.WriteLine("^XA")
                oWrite.WriteLine("^MMT")
                oWrite.WriteLine("^PW799")
                oWrite.WriteLine("^LL0599")
                oWrite.WriteLine("^LS0")
                oWrite.WriteLine("^FT165,78^A0N,56,55^FH\^FD" & CmpName & "^FS")
                oWrite.WriteLine("^FO162,80^GB476,0,2^FS")
                oWrite.WriteLine("^FT258,122^A0N,34,33^FH\^FDCOMMIT TO FASHION^FS")
                oWrite.WriteLine("^FT50,199^A0N,51,50^FH\^FDQUALITY^FS")
                oWrite.WriteLine("^FT50,253^A0N,51,50^FH\^FDDESIGN NO.^FS")
                oWrite.WriteLine("^FT50,307^A0N,51,50^FH\^FDSHADE NO.^FS")
                oWrite.WriteLine("^FT50,361^A0N,51,50^FH\^FDMTRS^FS")
                oWrite.WriteLine("^FT50,415^A0N,51,50^FH\^FDWIDTH^FS")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH As String = ""
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id ", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                oWrite.WriteLine("^FT381,199^A0N,51,50^FH\^FD" & ITEMNAME & "^FS")
                oWrite.WriteLine("^FT381,253^A0N,51,50^FH\^FD" & DESIGNNO & "^FS")
                oWrite.WriteLine("^FT381,307^A0N,51,50^FH\^FD" & SHADE & "^FS")
                oWrite.WriteLine("^FT381,361^A0N,51,50^FH\^FD" & Format(Val(MTRS), "0.00") & "^FS")
                oWrite.WriteLine("^FT381,415^A0N,51,50^FH\^FD" & TEMPWIDTH & "^FS")
                oWrite.WriteLine("^FO26,137^GB755,0,2^FS")
                oWrite.WriteLine("^FT306,199^A0N,51,50^FH\^FD:^FS")
                oWrite.WriteLine("^FT306,253^A0N,51,50^FH\^FD:^FS")
                oWrite.WriteLine("^FT306,307^A0N,51,50^FH\^FD:^FS")
                oWrite.WriteLine("^FT306,361^A0N,51,50^FH\^FD:^FS")
                oWrite.WriteLine("^FT306,415^A0N,51,50^FH\^FD:^FS")
                oWrite.WriteLine("^BY4,3,94^FT39,523^BCN,,Y,N")
                oWrite.WriteLine("^FD>:" & BARCODE & "^FS")
                oWrite.WriteLine("^PQ2,0,1,Y^XZ")


                oWrite.Dispose()


            ElseIf ClientName = "SAFFRON" Then

                'ALL ENTRIES ARE IN FRESHTYPE
                'If PIECETYPE <> "FRESH" Then EXIT SUB

                oWrite.WriteLine("I8,A,001")
                oWrite.WriteLine("")
                oWrite.WriteLine("")
                oWrite.WriteLine("Q400,024")
                oWrite.WriteLine("q831")
                oWrite.WriteLine("rN")
                oWrite.WriteLine("S5")
                oWrite.WriteLine("D2")
                oWrite.WriteLine("ZT")
                oWrite.WriteLine("JF")
                oWrite.WriteLine("O")
                oWrite.WriteLine("R136,0")
                oWrite.WriteLine("f100")
                oWrite.WriteLine("N")

                'GET REMARKS FROM CATEGORYMASTER LEFT OUTER JOIN FROM ITEMMASTER
                Dim TEMPWIDTH, TEMPCONTAIN As String
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(CATEGORYMASTER.category_remarks, '') AS WIDTH, ISNULL(ITEMMASTER.item_remarks, '') AS CONTAIN, ISNULL(ITEMMASTER.item_DISPLAYNAME, '') AS DISPLAYNAME, ISNULL(HSN_CODE,'') AS HSNCODE ", "", " ITEMMASTER LEFT OUTER JOIN CATEGORYMASTER ON ITEMMASTER.item_categoryid = CATEGORYMASTER.category_id LEFT OUTER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID", " AND ITEM_NAME = '" & ITEMNAME & "' AND ITEM_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    TEMPWIDTH = DT.Rows(0).Item("WIDTH")
                    TEMPCONTAIN = DT.Rows(0).Item("CONTAIN")
                End If

                oWrite.WriteLine("A419,146,0,1,3,3,N,""" & DT.Rows(0).Item("HSNCODE") & """")    'HSNCODE
                oWrite.WriteLine("A151,154,0,1,2,2,N,""" & TEMPWIDTH & """")    'GIVE ITEM CATEGORY'S REMARKS
                oWrite.WriteLine("A133,102,0,1,3,3,N,""" & Format(Val(MTRS), "0.00") & """")    'MTRS
                oWrite.WriteLine("A459,104,0,1,3,3,N,""" & SHADE & """")       'COLOR
                oWrite.WriteLine("A8,6,0,1,3,3,N,""" & DT.Rows(0).Item("DISPLAYNAME") & """")       'QUALITY
                oWrite.WriteLine("A171,199,0,1,2,2,N,""" & TEMPCONTAIN & """")        'ITEMREMARKS
                oWrite.WriteLine("A231,57,0,1,3,3,N,""" & ITEMNAME & """")      'ITEMNAME
                oWrite.WriteLine("A11,200,0,1,2,2,N,""Contain:""")
                oWrite.WriteLine("A318,154,0,1,2,2,N,""HSN :""")
                oWrite.WriteLine("A318,111,0,1,2,2,N,""Shade :""")
                oWrite.WriteLine("A11,153,0,1,2,2,N,""Width :""")
                oWrite.WriteLine("A11,60,0,1,2,2,N,""Design No :""")
                oWrite.WriteLine("A11,107,0,1,2,2,N,""Mtrs :""")
                oWrite.WriteLine("A265,107,0,1,2,2,N,""" & GRIDDESC & """")      'FOR TP
                oWrite.WriteLine("B8,257,0,1,2,6,87,B,""" & BARCODE & """")       'BARCODE
                oWrite.WriteLine("P1")

                oWrite.Dispose()

            End If

            'Printing Barcode
            Dim psi As New ProcessStartInfo()
            psi.FileName = "cmd.exe"
            psi.RedirectStandardInput = False
            psi.RedirectStandardOutput = True
            psi.Arguments = "/c print " & Application.StartupPath & "\Barcode.txt"    ' specify your command
            'psi.Arguments = "/c print D:\Barcode.txt"    ' specify your command
            'psi.Arguments = "print /d:\\admin-pc\ARGOX D:\Barcode.txt"    ' specify your command
            psi.UseShellExecute = False

            Dim proc As Process
            proc = Process.Start(psi)
            dirresults = proc.StandardOutput.ReadToEnd() ' // read from stdout
            '// do something with result stream
            proc.WaitForExit()
            proc.Dispose()

            'THIS LINE IS WRITTEN TO DISPOSE THE BARCODE NOTEPAD OBJECT, WHEN CURSOR COMES DIRECTLY ON NEXTLINE CODE
            oWrite.Dispose()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#Region "WHATSAPP"

    Function CHECKWHASTAPPEXP() As Boolean
        Dim BLN As Boolean = True
        If Now.Date > WHATSAPPEXPDATE Then
            BLN = False
        End If
        Return BLN
    End Function

    Function GETWHATSAPPBASEURL() As String
        Dim WHATSAPPBASEURL As String = ""

        'READ BASEURL FROM C DRIVE
        If File.Exists("C:\WHATSAPPBASEURL.txt") Then
            Dim oRead As System.IO.StreamReader = File.OpenText("C:\WHATSAPPBASEURL.txt")
            WHATSAPPBASEURL = oRead.ReadToEnd
        End If
        Return WHATSAPPBASEURL
    End Function

    Async Function SENDWHATSAPPATTACHMENT(WHATSAPPNO As String, PATH As String, FILENAME As String) As Threading.Tasks.Task(Of String)
        Dim RESPONSE As String = ""
        Dim waMediaMsgBody As SendMediaMsgJson = New SendMediaMsgJson()
        Dim Attachment As String = Convert.ToBase64String(File.ReadAllBytes(PATH))
        Dim AttachmentFileName As String = FILENAME
        waMediaMsgBody.base64data = Attachment
        waMediaMsgBody.mimeType = MimeMapping.GetMimeMapping(AttachmentFileName)
        'waMediaMsgBody.caption = "APIMethod SendMediaMessage from CISPLWhatsAppAPI.dll"
        waMediaMsgBody.filename = AttachmentFileName
        Dim txnResp As TxnRespWithSendMessageDtls = Await APIMethods.SendMediaMessageAsync(WHATSAPPNO, waMediaMsgBody)
        RESPONSE = JsonConvert.SerializeObject(txnResp, Formatting.Indented)

        Return RESPONSE
    End Function

    Async Function SENDWHATSAPPMESSAGE(WHATSAPPNO As String, TEXTMESSAGE As String) As Threading.Tasks.Task(Of String)
        Dim RESPONSE As String = ""
        Dim Body As SendTextMsgJson = New SendTextMsgJson()
        Body.text = TEXTMESSAGE
        Dim txnResp As TxnRespWithSendMessageDtls = Await APIMethods.SendTextMessageAsync(WHATSAPPNO, Body)
        RESPONSE = JsonConvert.SerializeObject(txnResp, Formatting.Indented)
        Return RESPONSE
    End Function

    Async Function CHECKMOBILECONNECTSTATUS() As Threading.Tasks.Task(Of String)
        Dim RESPONSE As String = ""
        Dim txnResp As TxnRespWithConnectionState = Await APIMethods.GetConnectionStateAsync()
        RESPONSE = JsonConvert.SerializeObject(txnResp, Formatting.Indented)
        Return RESPONSE
    End Function

#End Region


    Sub VIEWFORM(ByVal TYPE As String, ByVal EDIT As Boolean, ByVal BILLNO As Integer, ByVal REGTYPE As String)
        Try
            If TYPE = "PURCHASE" Then

                Dim OBJPURCHASE As New PurchaseMaster
                OBJPURCHASE.MdiParent = MDIMain
                OBJPURCHASE.EDIT = EDIT
                OBJPURCHASE.TEMPBILLNO = BILLNO
                OBJPURCHASE.TEMPREGNAME = REGTYPE
                OBJPURCHASE.Show()

            ElseIf TYPE = "CHALLAN" Then

                Dim OBJGDN As New GDN
                OBJGDN.MdiParent = MDIMain
                OBJGDN.EDIT = EDIT
                OBJGDN.TEMPGDNNO = BILLNO
                OBJGDN.Show()

            ElseIf TYPE = "SALE" Then

                Dim OBJSALE As New InvoiceMaster
                OBJSALE.MdiParent = MDIMain
                OBJSALE.EDIT = EDIT
                OBJSALE.TEMPINVOICENO = BILLNO
                OBJSALE.TEMPREGNAME = REGTYPE
                OBJSALE.Show()

            ElseIf TYPE = "MATREC" Then

                Dim OBJMATREC As New MaterialReceipt
                OBJMATREC.MdiParent = MDIMain
                OBJMATREC.EDIT = EDIT
                OBJMATREC.TEMPMATRECNO = BILLNO
                OBJMATREC.Show()

            ElseIf TYPE = "INHOUSECHECKING" Or TYPE = "INHOUSECHECKINGISS" Then

                Dim OBJCHECK As New InHouseChecking
                OBJCHECK.MdiParent = MDIMain
                OBJCHECK.EDIT = EDIT
                OBJCHECK.TEMPCHECKINGNO = BILLNO
                OBJCHECK.Show()

            ElseIf TYPE = "ISSUEPACKING" Then

                Dim OBJISS As New IssueToPacking
                OBJISS.MdiParent = MDIMain
                OBJISS.EDIT = EDIT
                OBJISS.TEMPISSUENO = BILLNO
                OBJISS.Show()

            ElseIf TYPE = "RECPACKING" Then

                Dim OBJREC As New RecFromPacking
                OBJREC.MdiParent = MDIMain
                OBJREC.EDIT = EDIT
                OBJREC.TEMPRECNO = BILLNO
                OBJREC.Show()

            ElseIf TYPE = "JOBOUT" Then

                Dim OBJJO As New JobOut
                OBJJO.MdiParent = MDIMain
                OBJJO.EDIT = EDIT
                OBJJO.TEMPJONO = BILLNO
                OBJJO.Show()

            ElseIf TYPE = "JOBIN" Then

                Dim OBJJI As New JobIn
                OBJJI.MdiParent = MDIMain
                OBJJI.EDIT = EDIT
                OBJJI.TEMPJOBINNO = BILLNO
                OBJJI.Show()

            ElseIf TYPE = "PAYMENT" Then

                Dim OBJPAYMENT As New PaymentMaster
                OBJPAYMENT.MdiParent = MDIMain
                OBJPAYMENT.EDIT = EDIT
                OBJPAYMENT.TEMPPAYMENTNO = BILLNO
                OBJPAYMENT.TEMPREGNAME = REGTYPE
                OBJPAYMENT.Show()

            ElseIf TYPE = "RECEIPT" Then

                Dim OBJREC As New Receipt
                OBJREC.MdiParent = MDIMain
                OBJREC.EDIT = EDIT
                OBJREC.TEMPRECEIPTNO = BILLNO
                OBJREC.TEMPREGNAME = REGTYPE
                OBJREC.Show()

            ElseIf TYPE = "JOURNAL" Then

                Dim OBJJV As New journal
                OBJJV.MdiParent = MDIMain
                OBJJV.EDIT = EDIT
                OBJJV.TEMPJVNO = BILLNO
                OBJJV.TEMPREGNAME = REGTYPE
                OBJJV.Show()

            ElseIf TYPE = "DEBITNOTE" Then

                Dim OBJDN As New DebitNote
                OBJDN.MdiParent = MDIMain
                OBJDN.edit = EDIT
                OBJDN.TEMPDNNO = BILLNO
                OBJDN.TEMPREGNAME = REGTYPE
                OBJDN.Show()

            ElseIf TYPE = "CREDITNOTE" Then

                Dim OBJCN As New CREDITNOTE
                OBJCN.MdiParent = MDIMain
                OBJCN.edit = EDIT
                OBJCN.TEMPCNNO = BILLNO
                OBJCN.TEMPREGNAME = REGTYPE
                OBJCN.Show()

            ElseIf TYPE = "CONTRA" Then

                Dim OBJCON As New ContraEntry
                OBJCON.MdiParent = MDIMain
                OBJCON.EDIT = EDIT
                OBJCON.tempcontrano = BILLNO
                OBJCON.TEMPREGNAME = REGTYPE
                OBJCON.Show()

            ElseIf TYPE = "EXPENSE" Then

                Dim OBJEXP As New ExpenseVoucher
                OBJEXP.MdiParent = MDIMain
                OBJEXP.EDIT = EDIT
                OBJEXP.TEMPEXPNO = BILLNO
                OBJEXP.TEMPREGNAME = REGTYPE
                OBJEXP.FRMSTRING = "NONPURCHASE"
                OBJEXP.Show()

            ElseIf TYPE = "SALE RETURN" Or TYPE = "SALERETURN" Then

                Dim OBJSALERET As New SaleReturn
                OBJSALERET.MdiParent = MDIMain
                OBJSALERET.EDIT = EDIT
                OBJSALERET.TEMPSALRETNO = BILLNO
                OBJSALERET.Show()

            ElseIf TYPE = "PUR RETURN" Or TYPE = "PURCHASERETURN" Then

                Dim OBJPURRET As New PurchaseReturn
                OBJPURRET.MdiParent = MDIMain
                OBJPURRET.EDIT = EDIT
                OBJPURRET.TEMPPRNO = BILLNO
                OBJPURRET.Show()

            ElseIf TYPE = "STKADJ" Then

                Dim OBJSTOCKADJ As New StockReco
                OBJSTOCKADJ.MdiParent = MDIMain
                OBJSTOCKADJ.EDIT = EDIT
                OBJSTOCKADJ.TEMPRECONO = BILLNO
                OBJSTOCKADJ.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function ErrHandle(ByVal Errcode As Integer) As Boolean
        Dim bln As Boolean = False
        If Errcode = -675406840 Then
            MsgBox("Check Internet Connection")
            bln = True
        End If
        Return bln
    End Function

    Public Sub pcase(ByRef txt As Object)
        txt.Text = StrConv(txt.Text, VbStrConv.ProperCase)
    End Sub

    Public Sub uppercase(ByRef txt As Object)
        txt.Text = StrConv(txt.Text, VbStrConv.Uppercase)
    End Sub

    Public Sub lowercase(ByRef txt As Object)
        txt.Text = StrConv(txt.Text, VbStrConv.Lowercase)
    End Sub

#Region "IN WORDS FUNCTION"

    Function CurrencyToWord(ByVal Num As Decimal) As String

        'I have created this function for converting amount in indian rupees (INR). 
        'You can manipulate as you wish like decimal setting, Doller (any currency) Prefix.

        Dim strNum As String
        Dim strNumDec As String
        Dim StrWord As String
        strNum = Num

        If InStr(1, strNum, ".") <> 0 Then
            strNumDec = Mid(strNum, InStr(1, strNum, ".") + 1)

            If Len(strNumDec) = 1 Then
                strNumDec = strNumDec + "0"
            End If
            If Len(strNumDec) > 2 Then
                strNumDec = Mid(strNumDec, 1, 2)
            End If

            strNum = Mid(strNum, 1, InStr(1, strNum, ".") - 1)
            StrWord = IIf(CDbl(strNum) = 1, " Rupee ", " Rupees ") + NumToWord(CDbl(strNum)) + IIf(CDbl(strNumDec) > 0, " and Paise" + cWord3(CDbl(strNumDec)), "")
        Else
            StrWord = IIf(CDbl(strNum) = 1, " Rupee ", " Rupees ") + NumToWord(CDbl(strNum))
        End If
        CurrencyToWord = StrWord & " Only"
        Return CurrencyToWord

    End Function

    Function NumToWord(ByVal Num As Decimal) As String

        'I divided this function in two part.
        '1. Three or less digit number.
        '2. more than three digit number.
        Dim strNum As String
        Dim StrWord As String
        strNum = Num

        If Len(strNum) <= 3 Then
            StrWord = cWord3(CDbl(strNum))
        Else
            StrWord = cWordG3(CDbl(Mid(strNum, 1, Len(strNum) - 3))) + " " + cWord3(CDbl(Mid(strNum, Len(strNum) - 2)))
        End If
        NumToWord = StrWord

    End Function

    Function cWordG3(ByVal Num As Decimal) As String

        '2. more than three digit number.
        Dim strNum As String = ""
        Dim StrWord As String = ""
        Dim readNum As String = ""
        strNum = Num
        If Len(strNum) Mod 2 <> 0 Then
            readNum = CDbl(Mid(strNum, 1, 1))
            If readNum <> "0" Then
                StrWord = retWord(readNum)
                readNum = CDbl("1" + strReplicate("0", Len(strNum) - 1) + "000")
                StrWord = StrWord + " " + retWord(readNum)
            End If
            strNum = Mid(strNum, 2)
        End If
        While Not Len(strNum) = 0
            readNum = CDbl(Mid(strNum, 1, 2))
            If readNum <> "0" Then
                StrWord = StrWord + " " + cWord3(readNum)
                readNum = CDbl("1" + strReplicate("0", Len(strNum) - 2) + "000")
                StrWord = StrWord + " " + retWord(readNum)
            End If
            strNum = Mid(strNum, 3)
        End While
        cWordG3 = StrWord
        Return cWordG3

    End Function

    Function strReplicate(ByVal str As String, ByVal intD As Integer) As String

        'This fucntion padded "0" after the number to evaluate hundred, thousand and on....
        'using this function you can replicate any Charactor with given string.
        strReplicate = ""
        For i As Integer = 1 To intD
            strReplicate = strReplicate + str
        Next
        Return strReplicate

    End Function

    Function cWord3(ByVal Num As Decimal) As String

        '1. Three or less digit number.
        Dim strNum As String = ""
        Dim StrWord As String = ""
        Dim readNum As String = ""
        If Num < 0 Then Num = Num * -1
        strNum = Num

        If Len(strNum) = 3 Then
            readNum = CDbl(Mid(strNum, 1, 1))
            StrWord = retWord(readNum) + " Hundred"
            strNum = Mid(strNum, 2, Len(strNum))
        End If

        If Len(strNum) <= 2 Then
            If CDbl(strNum) >= 0 And CDbl(strNum) <= 20 Then
                StrWord = StrWord + " " + retWord(CDbl(strNum))
            Else
                StrWord = StrWord + " " + retWord(CDbl(Mid(strNum, 1, 1) + "0")) + " " + retWord(CDbl(Mid(strNum, 2, 1)))
            End If
        End If

        strNum = CStr(Num)
        cWord3 = StrWord
        Return cWord3

    End Function

    Function retWord(ByVal Num As Decimal) As String
        'This two dimensional array store the primary word convertion of number.
        retWord = ""
        Dim ArrWordList(,) As Object = {{0, ""}, {1, "One"}, {2, "Two"}, {3, "Three"}, {4, "Four"},
                                        {5, "Five"}, {6, "Six"}, {7, "Seven"}, {8, "Eight"}, {9, "Nine"},
                                        {10, "Ten"}, {11, "Eleven"}, {12, "Twelve"}, {13, "Thirteen"}, {14, "Fourteen"},
                                        {15, "Fifteen"}, {16, "Sixteen"}, {17, "Seventeen"}, {18, "Eighteen"}, {19, "Nineteen"},
                                        {20, "Twenty"}, {30, "Thirty"}, {40, "Forty"}, {50, "Fifty"}, {60, "Sixty"},
                                        {70, "Seventy"}, {80, "Eighty"}, {90, "Ninety"}, {100, "Hundred"}, {1000, "Thousand"},
                                        {100000, "Lakh"}, {10000000, "Crore"}}

        For i As Integer = 0 To UBound(ArrWordList)
            If Num = ArrWordList(i, 0) Then
                retWord = ArrWordList(i, 1)
                Exit For
            End If
        Next
        Return retWord

    End Function

#End Region

    Function SENDMSG(ByVal MSG As String, ByVal MOBILENO As String) As String
        Try
            Dim WEBREQUEST As HttpWebRequest = Nothing
            Dim WEBRESPONSE As HttpWebResponse = Nothing
            Dim USERNAME As String = ""
            Dim PASSWORD As String = ""
            Dim SENDER As String = ""
            'Dim objSMS As New routesmsdll.SMS
            'If MOBILENO <> "" Then objSMS.MobileNo = MOBILENO

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If ClientName = "ALENCOT" Then
                USERNAME = "kumarsilk"
                PASSWORD = "infosys123"
                SENDER = "LRDASP"

            ElseIf ClientName = "MOHAN" Then
                USERNAME = "nako-mohanfab"
                PASSWORD = "mohanfab"
                SENDER = "MKNITT"

            ElseIf ClientName = "KCRAYON" Then
                USERNAME = "nako-kcryon"
                PASSWORD = "kcrayon"
                SENDER = "KCRYON"

            ElseIf ClientName = "KOTHARI" Then
                USERNAME = "nako-kothari"
                PASSWORD = "kothari1"
                SENDER = "LLUCAS"

            ElseIf ClientName = "YASHVI" Then
                USERNAME = "nako-yashvi"
                PASSWORD = "yashvi12"
                SENDER = "YSVCRN"

            ElseIf ClientName = "SANGHVI" Then
                USERNAME = "nako-sanghvi"
                PASSWORD = "sanghvi1"
                SENDER = "SSSONS"

            ElseIf ClientName = "DRDRAPES" Then
                USERNAME = "drdrape"
                PASSWORD = "drdrapes"
                SENDER = "DRDMUM"

            ElseIf ClientName = "SAKARIA" Then
                USERNAME = "sakaria"
                PASSWORD = "sakaria123"
                SENDER = "SAKRIA"

            ElseIf ClientName = "NVAHAN" Then
                WEBREQUEST = DirectCast(WEBREQUEST.Create("http://app.smsnix.com/vendorsms/pushsms.aspx?user=Girish Shah&password=tomstracy&msisdn=" & MOBILENO & "&sid=NVAHAN&msg=" & MSG & "&fl=0&gwid=2"), HttpWebRequest)
                Try
                    WEBRESPONSE = DirectCast(WEBREQUEST.GetResponse(), HttpWebResponse)
                Catch ex As WebException
                    WEBRESPONSE = DirectCast(ex.Response, HttpWebResponse)
                End Try
                Return "1701"
                Exit Function
            End If

            'OLD CODE
            'objSMS.Message = MSG
            'objSMS.IpAddress = "103.16.101.52"
            'objSMS.dlr = 1
            'objSMS.MessageType = routesmsdll.MESSAGE_TYPE.mTEXT
            'Dim response As String = objSMS.sendMessage()
            'Return (response.ToString.Substring(0, 4))

            Dim NEWMSG As String = System.Web.HttpUtility.UrlEncode(MSG)
            WEBREQUEST = DirectCast(WEBREQUEST.Create("http://nakoda.alert.ind.in/sms_api/sendsms.php?username=" & USERNAME & "&password=" & PASSWORD & "&mobile=" & MOBILENO & "&sendername=" & SENDER & "&message=" & NEWMSG), HttpWebRequest)
            Try
                WEBRESPONSE = DirectCast(WEBREQUEST.GetResponse(), HttpWebResponse)
            Catch ex As WebException
                WEBRESPONSE = DirectCast(ex.Response, HttpWebResponse)
            End Try
            Return "1701"

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub CHALLANTYPE(ByRef CMBTYPE As ComboBox)
        Try
            If CMBTYPE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" CHALLANTYPE_NAME ", "", " CHALLANTYPEMASTER ", " And CHALLANTYPE_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "CHALLANTYPE_NAME"
                    CMBTYPE.DisplayMember = "CHALLANTYPE_NAME"
                End If
                CMBTYPE.DataSource = dt
                CMBTYPE.SelectedIndex = -1
                CMBTYPE.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLJOBOUTTYPE(ByRef CMBTYPE As ComboBox)
        Try
            If CMBTYPE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" JOTYPE_NAME ", "", " JOBOUTTYPEMASTER ", " And JOTYPE_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "JOTYPE_NAME"
                    CMBTYPE.DisplayMember = "JOTYPE_NAME"
                End If
                CMBTYPE.DataSource = dt
                CMBTYPE.SelectedIndex = -1
                CMBTYPE.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub filldesignation(ByRef cmbdesignation As ComboBox)
        Try
            If cmbdesignation.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" designation_NAME ", "", " designationMaster ", " And designation_cmpid=" & CmpId & " And designation_locationid = " & Locationid & " And designation_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "designation_NAME"
                    cmbdesignation.DisplayMember = "designation_NAME"
                    cmbdesignation.Text = ""
                End If
                cmbdesignation.DataSource = dt
                cmbdesignation.SelectedIndex = -1
                cmbdesignation.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillPIECETYPE(ByRef CMBPIECETYPE As ComboBox)
        Try
            If CMBPIECETYPE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" PIECETYPE_NAME ", "", " PIECETYPEMaster ", " And PIECETYPE_cmpid=" & CmpId & " And PIECETYPE_locationid = " & Locationid & " And PIECETYPE_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "PIECETYPE_NAME"
                    CMBPIECETYPE.DisplayMember = "PIECETYPE_NAME"
                    CMBPIECETYPE.Text = ""
                End If
                CMBPIECETYPE.DataSource = dt
                CMBPIECETYPE.SelectedIndex = -1
                CMBPIECETYPE.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillCURRENCY(ByRef CMBCURRENCY As ComboBox)
        Try
            If CMBCURRENCY.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" CURR_NAME ", "", " CURRENCYMASTER ", " And CURR_cmpid=" & CmpId & " And CURR_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "CURR_NAME"
                    CMBCURRENCY.DisplayMember = "CURR_NAME"
                    CMBCURRENCY.Text = ""
                End If
                CMBCURRENCY.DataSource = dt
                CMBCURRENCY.SelectedIndex = -1
                CMBCURRENCY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub CURRENCYVALIDATE(ByRef CMBCURRENCY As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBCURRENCY.Text.Trim <> "" Then
                uppercase(CMBCURRENCY)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" CURR_NAME ", "", " CURRENCYMASTER", " and CURR_NAME = '" & CMBCURRENCY.Text.Trim & "' and CURR_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Currancy not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim a As String = CMBCURRENCY.Text.Trim
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBCURRENCY.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)


                        Dim objRACK As New ClsCurrencyMaster
                        objRACK.alParaval = alParaval
                        Dim IntResult As Integer = objRACK.SAVE()


                        dt = objclscommon.search(" CURR_ID AS ID, CURR_NAME AS NAME ", "", " CURRANCYMASTER", " and CURR_NAME = '" & CMBCURRENCY.Text.Trim & "' and CURR_yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBCURRENCY.DataSource
                            If CMBCURRENCY.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ID"), CMBCURRENCY.Text.Trim)
                                    CMBCURRENCY.Text = a
                                End If
                            End If
                        End If

                    Else
                        CMBCURRENCY.Focus()
                        CMBCURRENCY.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub FILLDISTRICT(ByRef CMBDISTRICT As ComboBox)
        Try
            If CMBDISTRICT.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" DISTINCT DISTRICT_ID, DISTRICT_NAME ", "", " DISTRICTMASTER ", " and DISTRICT_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "DISTRICT_NAME"
                    CMBDISTRICT.DisplayMember = "DISTRICT_NAME"
                    CMBDISTRICT.ValueMember = "DISTRICT_ID"
                    CMBDISTRICT.Text = ""
                    uppercase(CMBDISTRICT)
                End If
                CMBDISTRICT.DataSource = dt
                CMBDISTRICT.SelectedIndex = -1
                CMBDISTRICT.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCHALLANTYPE(ByRef CMBTYPE As ComboBox)
        Try
            If CMBTYPE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" CHALLANTYPE_NAME ", "", " CHALLANTYPEMASTER ", " And CHALLANTYPE_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "CHALLANTYPE_NAME"
                    CMBTYPE.DisplayMember = "CHALLANTYPE_NAME"
                End If
                CMBTYPE.DataSource = dt
                CMBTYPE.SelectedIndex = -1
                CMBTYPE.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCOLOR(ByRef CMBCOLOR As ComboBox, ByVal DESIGNNO As String, ByVal ITEMNAME As String)
        Try
            If CMBCOLOR.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                Dim WHERECLAUSE As String = ""
                If FETCHITEMWISEDESIGN = True And DESIGNNO <> "" Then WHERECLAUSE = " AND ISNULL(DESIGNMASTER_COLOR.DESIGN_BLOCKED,0) = 'FALSE' And ISNULL(DESIGNMASTER.DESIGN_NO,'')='" & DESIGNNO & "'"
                If FETCHITEMWISESHADE = True And ITEMNAME <> "" Then WHERECLAUSE = " And ISNULL(ITEMMASTER.ITEM_NAME,'')='" & ITEMNAME & "'"
                dt = objclscommon.search(" DISTINCT COLOR_ID, COLOR_NAME ", "", " COLORMASTER LEFT OUTER JOIN DESIGNMASTER_COLOR ON DESIGNMASTER_COLOR.DESIGN_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON DESIGNMASTER.DESIGN_id = DESIGNMASTER_COLOR.DESIGN_ID LEFT OUTER JOIN ITEMMASTER_COLOR ON ITEMMASTER_COLOR.ITEM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN ITEMMASTER ON ITEMMASTER.ITEM_id = ITEMMASTER_COLOR.ITEM_ID   ", WHERECLAUSE & " and COLOR_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "COLOR_NAME"
                    CMBCOLOR.DisplayMember = "COLOR_NAME"
                    CMBCOLOR.ValueMember = "COLOR_ID"
                    CMBCOLOR.Text = ""
                    uppercase(CMBCOLOR)
                End If
                CMBCOLOR.DataSource = dt
                CMBCOLOR.SelectedIndex = -1
                CMBCOLOR.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLDESIGN(ByRef cmbDESIGN As ComboBox, ByVal ITEMNAME As String)
        Try

            If cmbDESIGN.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster

                Dim WHERECLAUSE As String = ""
                If FETCHITEMWISEDESIGN = True And ITEMNAME <> "" Then WHERECLAUSE = " AND ISNULL(ITEMMASTER.ITEM_NAME,'')='" & ITEMNAME & "'"
                Dim dt As DataTable = objclscommon.search(" DESIGN_ID, DESIGN_NO ", "", "  DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGNMASTER.DESIGN_ITEMID=ITEMMASTER.ITEM_ID ", WHERECLAUSE & " AND ISNULL(DESIGN_BLOCKED,0) = 'FALSE' and DESIGN_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "DESIGN_NO"
                    cmbDESIGN.DisplayMember = "DESIGN_NO"
                    cmbDESIGN.ValueMember = "DESIGN_ID"
                    cmbDESIGN.Text = ""
                End If
                cmbDESIGN.DataSource = dt
                cmbDESIGN.SelectedIndex = -1
                cmbDESIGN.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLAREA(ByRef cmbname As ComboBox)
        Try
            If cmbname.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" AREA_name ", "", " AREAMaster", " and AREA_cmpid=" & CmpId & " AND AREA_LOCATIONID = " & Locationid & " AND AREA_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "AREA_name"
                    cmbname.DisplayMember = "AREA_name"
                    cmbname.Text = ""
                End If
                cmbname.DataSource = dt
                cmbname.SelectedIndex = -1
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AREAVALIDATE(ByRef CMBAREA As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBAREA.Text.Trim <> "" Then
                uppercase(CMBAREA)
                Dim objclscommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objclscommon.search("AREA_name", "", "AREAMaster", " and AREA_name = '" & CMBAREA.Text.Trim & "' AND AREA_CMPID = " & CmpId & " AND AREA_LOCATIONID = " & Locationid & " AND AREA_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBAREA.Text.Trim
                    Dim tempmsg As Integer = MsgBox("AREA not present, Add New?", MsgBoxStyle.YesNo, " ")
                    If tempmsg = vbYes Then
                        CMBAREA.Text = a
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        objyearmaster.savearea(CMBAREA.Text.Trim, CmpId, Locationid, Userid, YearId, " and AREA_name = '" & CMBAREA.Text.Trim & "' AND AREA_CMPID = " & CmpId & " AND AREA_LOCATIONID = " & Locationid & " AND AREA_YEARID = " & YearId)
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillCOUNTRY(ByRef cmbname As ComboBox)
        Try
            If cmbname.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" COUNTRY_name ", "", " COUNTRYMaster", " and COUNTRY_cmpid=" & CmpId & " AND COUNTRY_LOCATIONID = " & Locationid & " AND COUNTRY_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "COUNTRY_name"
                    cmbname.DisplayMember = "COUNTRY_name"
                    cmbname.Text = ""
                End If
                cmbname.DataSource = dt
                cmbname.SelectedIndex = -1
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub BLANKETVALIDATE(ByRef CMBBLANKETNAME As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBBLANKETNAME.Text.Trim <> "" Then
                uppercase(CMBBLANKETNAME)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" BLANKET_NAME ", "", " BLANKETMASTER ", " and BLANKET_NAME = '" & CMBBLANKETNAME.Text.Trim & "'  and BLANKET_YEARID  = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Blanket not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim a As String = CMBBLANKETNAME.Text.Trim
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBBLANKETNAME.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objEmailmaster As New ClsBlanketMaster
                        objEmailmaster.ALPARAVAL = alParaval
                        Dim IntResult As Integer = objEmailmaster.SAVE()

                        '                        dt = objclscommon.search(" BLANKET_ID AS BLANKETID, BLANKET_NAME AS BLANKETNAME ", "", " BLANKETMASTER", " and BLANKET_NAME = '" & CMBBLANKETNAME.Text.Trim & "' and BLANKET_yearid = " & YearId)
                        '                        If dt.Rows.Count > 0 Then
                        '                            Dim dt1 As New DataTable
                        '                            dt1 = CMBBLANKETNAME.DataSource
                        '                            If CMBBLANKETNAME.DataSource <> Nothing Then
                        'line1:
                        '                                If dt1.Rows.Count > 0 Then
                        '                                    dt1.Rows.Add(dt.Rows(0).Item("BLANKETID"), CMBBLANKETNAME.Text.Trim)
                        '                                    CMBBLANKETNAME.Text = a
                        '                                End If
                        '                            End If
                        '                        End If
                    Else
                        CMBBLANKETNAME.Focus()
                        CMBBLANKETNAME.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub COUNTRYVALIDATE(ByRef CMBCOUNTRY As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBCOUNTRY.Text.Trim <> "" Then
                uppercase(CMBCOUNTRY)
                Dim objclscommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objclscommon.search("COUNTRY_name", "", "COUNTRYMaster", " and COUNTRY_name = '" & CMBCOUNTRY.Text.Trim & "' AND COUNTRY_CMPID = " & CmpId & " AND COUNTRY_LOCATIONID = " & Locationid & " AND COUNTRY_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBCOUNTRY.Text.Trim
                    Dim tempmsg As Integer = MsgBox("COUNTRY not present, Add New?", MsgBoxStyle.YesNo, " ")
                    If tempmsg = vbYes Then
                        CMBCOUNTRY.Text = a
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        objyearmaster.savecountry(CMBCOUNTRY.Text.Trim, CmpId, Locationid, Userid, YearId, " and COUNTRY_name = '" & CMBCOUNTRY.Text.Trim & "' AND COUNTRY_CMPID = " & CmpId & " AND COUNTRY_LOCATIONID = " & Locationid & " AND COUNTRY_YEARID = " & YearId)
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLCOSTCENTER(ByRef CMBCOSTCENTERNAME As ComboBox)
        Try
            If CMBCOSTCENTERNAME.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" COSTCENTER_name ", "", " COSTCENTERMASTER", " and COSTCENTER_cmpid=" & CmpId & " AND COSTCENTER_locationid = " & Locationid & " AND COSTCENTER_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "COSTCENTER_name"
                    CMBCOSTCENTERNAME.DisplayMember = "COSTCENTER_name"
                    CMBCOSTCENTERNAME.Text = ""
                End If
                CMBCOSTCENTERNAME.DataSource = dt
                CMBCOSTCENTERNAME.SelectedIndex = -1
                CMBCOSTCENTERNAME.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub COSTCENTERVALIDATE(ByRef CMBCOSTCENTERNAME As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBCOSTCENTERNAME.Text.Trim <> "" Then
                uppercase(CMBCOSTCENTERNAME)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("COSTCENTER_id", "", "COSTCENTERMaster", " and COSTCENTER_NAME = '" & CMBCOSTCENTERNAME.Text.Trim & "' and COSTCENTER_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("COSTCENTER not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBCOSTCENTERNAME.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objclsCOSTCENTER As New ClsCostCenterMaster
                        objclsCOSTCENTER.alParaval = alParaval
                        Dim IntResult As Integer = objclsCOSTCENTER.save()
                    Else
                        CMBCOSTCENTERNAME.Focus()
                        CMBCOSTCENTERNAME.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillSTATE(ByRef cmbname As ComboBox)
        Try
            If cmbname.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" STATE_name ", "", " STATEMaster", " and STATE_cmpid=" & CmpId & " AND STATE_LOCATIONID = " & Locationid & " AND STATE_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "STATE_name"
                    cmbname.DisplayMember = "STATE_name"
                    cmbname.Text = ""
                End If
                cmbname.DataSource = dt
                cmbname.SelectedIndex = -1
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub STATEVALIDATE(ByRef CMBSTATE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBSTATE.Text.Trim <> "" Then
                uppercase(CMBSTATE)
                Dim objclscommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objclscommon.search("STATE_name", "", "STATEMaster", " and STATE_name = '" & CMBSTATE.Text.Trim & "' AND STATE_CMPID = " & CmpId & " AND STATE_LOCATIONID = " & Locationid & " AND STATE_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBSTATE.Text.Trim
                    Dim tempmsg As Integer = MsgBox("STATE not present, Add New?", MsgBoxStyle.YesNo, " ")
                    If tempmsg = vbYes Then
                        CMBSTATE.Text = a
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        objyearmaster.savestate(CMBSTATE.Text.Trim, CmpId, Locationid, Userid, YearId, " and STATE_name = '" & CMBSTATE.Text.Trim & "' AND STATE_CMPID = " & CmpId & " AND STATE_LOCATIONID = " & Locationid & " AND STATE_YEARID = " & YearId)
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub designationvalidate(ByRef cmbdesignation As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If cmbdesignation.Text.Trim <> "" Then
                uppercase(cmbdesignation)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" designation_NAME ", "", "designationMaster", " and designation_NAME = '" & cmbdesignation.Text.Trim & "' and designation_cmpid = " & CmpId & " and designation_locationid = " & Locationid & " and designation_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("designation not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(cmbdesignation.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objdesignation As New ClsdesignationMaster
                        objdesignation.alParaval = alParaval
                        Dim IntResult As Integer = objdesignation.save()
                        'e.Cancel = True
                    Else
                        cmbdesignation.Focus()
                        cmbdesignation.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PIECETYPEvalidate(ByRef cmbPIECETYPE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If cmbPIECETYPE.Text.Trim <> "" Then
                uppercase(cmbPIECETYPE)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" PIECETYPE_NAME ", "", "PIECETYPEMaster", " and PIECETYPE_NAME = '" & cmbPIECETYPE.Text.Trim & "' and PIECETYPE_cmpid = " & CmpId & " and PIECETYPE_locationid = " & Locationid & " and PIECETYPE_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("PIECETYPE not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(UCase(cmbPIECETYPE.Text.Trim))
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objPIECETYPE As New ClsPieceTypeMaster
                        objPIECETYPE.alParaval = alParaval
                        Dim IntResult As Integer = objPIECETYPE.save()
                        'e.Cancel = True
                    Else
                        cmbPIECETYPE.Focus()
                        cmbPIECETYPE.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub COLORVALIDATE(ByRef cmbCOLOR As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, ByVal DESIGNNO As String, ByVal ITEMNAME As String)
        Try

            If cmbCOLOR.Text.Trim <> "" Then
                uppercase(cmbCOLOR)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                If (ClientName = "YASHVI" Or ClientName = "AVIS" Or ClientName = "SOFTAS" Or ClientName = "SUPRIYA" Or ClientName = "MAHAVIRPOLYCOT") And DESIGNNO <> "" Then
                    dt = objclscommon.search(" COLOR_NAME ", "", " COLORMaster INNER JOIN DESIGNMASTER_COLOR ON COLOR_ID = DESIGN_COLORID INNER JOIN DESIGNMASTER ON DESIGNMASTER_COLOR.DESIGN_ID = DESIGNMASTER.DESIGN_ID", " AND ISNULL(DESIGNMASTER_COLOR.DESIGN_BLOCKED,0) = 'FALSE' AND DESIGNMASTER.DESIGN_NO = '" & DESIGNNO & "' and COLOR_NAME = '" & cmbCOLOR.Text.Trim & "'  and COLOR_yearid = " & YearId)
                    If dt.Rows.Count = 0 Then
                        e.Cancel = True
                        MsgBox("Add Shade From Design Master", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                ElseIf FETCHITEMWISESHADE = True And ITEMNAME <> "" Then
                    dt = objclscommon.search(" COLOR_NAME ", "", " COLORMaster INNER JOIN ITEMMASTER_COLOR ON COLOR_ID = ITEM_COLORID INNER JOIN ITEMMASTER ON ITEMMASTER_COLOR.ITEM_ID = ITEMMASTER.ITEM_ID", " AND ITEMMASTER.ITEM_NAME = '" & ITEMNAME & "' and COLOR_NAME = '" & cmbCOLOR.Text.Trim & "'  and COLOR_yearid = " & YearId)
                    If dt.Rows.Count = 0 Then
                        e.Cancel = True
                        MsgBox("Add Shade From Item Master", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                Else
                    dt = objclscommon.search(" COLOR_NAME ", "", " COLORMaster", " and COLOR_NAME = '" & cmbCOLOR.Text.Trim & "'  and COLOR_yearid = " & YearId)
                End If
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("COLOR not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(cmbCOLOR.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objCOLOR As New ClsColorMaster
                        objCOLOR.alParaval = alParaval
                        Dim IntResult As Integer = objCOLOR.save()
                        'e.Cancel = True
                    Else
                        cmbCOLOR.Focus()
                        cmbCOLOR.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub DISTRICTVALIDATE(ByRef cmbDISTRICT As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If cmbDISTRICT.Text.Trim <> "" Then
                uppercase(cmbDISTRICT)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" DISTRICT_NAME ", "", " DISTRICTMaster", " and DISTRICT_NAME = '" & cmbDISTRICT.Text.Trim & "'  and DISTRICT_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("DISTRICT not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(cmbDISTRICT.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objDISTRICT As New ClsDistrictMaster
                        objDISTRICT.alParaval = alParaval
                        Dim IntResult As Integer = objDISTRICT.SAVE()
                        'e.Cancel = True
                    Else
                        cmbDISTRICT.Focus()
                        cmbDISTRICT.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function setcolor(ByVal no As Integer) As String
        Try
            Dim str As String = String.Empty
            Dim remainder As Integer
            remainder = no Mod 30
            Select Case remainder
                Case 0
                    str = "Turquoise"
                Case 1
                    str = "LightGreen"
                Case 2
                    str = "LightSkyBlue"
                Case 3
                    str = "Lavender"
                Case 4
                    str = "Plum"
                Case 5
                    str = "Pink"
                Case 6
                    str = "LightCyan"
                Case 7
                    str = "Gold"
                Case 8
                    str = "Silver"
                Case 9
                    str = "Khaki"
                Case 10
                    str = "LIGHTCORAL"
                Case 11
                    str = "MISTYROSE"
                Case 12
                    str = "LIGHTSALMON"
                Case 13
                    str = "SEASHELL"
                Case 14
                    str = "PEACHPUFF"
                Case 15
                    str = "CORNSILK"
                Case 16
                    str = "YELLOWGREEN"
                Case 17
                    str = "HotPink"
                Case 18
                    str = "HONEYDEW"
                Case 19
                    str = "LAVENDER"
                Case 20
                    str = "THISTLE"
                Case 21
                    str = "PINK"
                Case 22
                    str = "GREENYELLOW"
                Case 23
                    str = "SkyBlue"
                Case 24
                    str = "LightCyan"
                Case 25
                    str = "Lime"
                Case 26
                    str = "Wheat"
                Case 27
                    str = "Cornsilk"
                Case 28
                    str = "DarkOrange"
                Case 29
                    str = "PaleVioletRed"
                Case 30
                    str = "LIGHTPINK"

            End Select
            Return str
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Sub fillform(ByRef CHKFORM As CheckedListBox, ByRef edit As Boolean, Optional ByVal WHERECLAUSE As String = "")
        Try
            If CHKFORM.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" form_name ", "", " FORMTYPE", WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "FORM_name"
                    CHKFORM.DataSource = dt
                    CHKFORM.DisplayMember = "FORM_name"
                    If edit = False Then CHKFORM.Text = ""

                End If
                ''CHKFORM.SelectedIndex = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLFORMTYPE(ByRef CMBFORM As ComboBox, ByRef edit As Boolean, Optional ByVal WHERECLAUSE As String = "")
        Try
            If CMBFORM.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" form_name ", "", " FORMTYPE", WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "FORM_name"
                    CMBFORM.DataSource = dt
                    CMBFORM.DisplayMember = "FORM_name"
                    If edit = False Then CMBFORM.Text = ""
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub filltax(ByRef cmbtax As ComboBox, ByRef edit As Boolean)
        Try
            If cmbtax.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" tax_name ", "", " TaxMaster", " and Tax_cmpid=" & CmpId & " AND TAX_LOCATIONID = " & Locationid & " AND TAX_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "tax_name"
                    cmbtax.DisplayMember = "tax_name"
                    If edit = False Then cmbtax.Text = ""
                End If
                cmbtax.DataSource = dt
                cmbtax.SelectedIndex = -1
                cmbtax.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillUSER(ByRef CMBUSER As ComboBox, Optional ByVal CONDITION As String = "")
        Try
            Dim objclscommon As New ClsCommon
            Dim dt As DataTable

            dt = objclscommon.search(" DISTINCT User_Name as [UserName]", "", "USERMASTER", " and USERMASTER.USER_cmpid= " & CmpId & " ORDER BY USER_NAME ")
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "USERNAME"
                CMBUSER.DisplayMember = "USERNAME"
                CMBUSER.Text = ""
            End If
            CMBUSER.DataSource = dt
            CMBUSER.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub USERvalidate(ByRef CMBUSER As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBUSER.Text.Trim <> "" Then
                uppercase(CMBUSER)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("User_Name ", "", " USERMASTER ", "   and User_Name = '" & CMBUSER.Text.Trim & "' and USER_cmpid = " & CmpId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBUSER.Text.Trim
                    Dim tempmsg As Integer = MsgBox("USER not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBUSER.Text = a
                        Dim objuser As New UserMaster
                        'objuser.TEMPUSER = CMBUSER.Text.Trim()
                        ' OBJDESIGN.TEMPMERCHANT = CMBMERCHANT.Text.Trim()
                        objuser.ShowDialog()
                        dt = objclscommon.search("User_Name ", "", " USERMASTER", " and User_Name = '" & CMBUSER.Text.Trim & "'  and User_yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBUSER.DataSource
                            If CMBUSER.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("USER"), CMBUSER.Text.Trim)
                                    CMBUSER.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub


    Sub ALPHEBETKEYPRESS(ByVal han As KeyPressEventArgs, ByVal sen As Control, ByVal frm As System.Windows.Forms.Form)
        If (AscW(han.KeyChar) >= 65 And AscW(han.KeyChar) <= 90) Or (AscW(han.KeyChar) >= 97 And AscW(han.KeyChar) <= 122) Or AscW(han.KeyChar) = 8 Then
            han.KeyChar = han.KeyChar
        Else
            han.KeyChar = ""
        End If
        If AscW(han.KeyChar) = Keys.Escape Then
            frm.Close()
        End If
    End Sub

    Sub numdot3(ByVal han As KeyPressEventArgs, ByVal txt As TextBox, ByVal frm As System.Windows.Forms.Form)
        Dim mypos As Integer

        mypos = InStr(1, txt.Text, ".")

        If AscW(han.KeyChar) > 47 And AscW(han.KeyChar) < 58 Or AscW(han.KeyChar) = 8 Or AscW(han.KeyChar) = 46 Then
            han.KeyChar = han.KeyChar
        Else
            han.KeyChar = ""
        End If


        If AscW(han.KeyChar) > 47 And AscW(han.KeyChar) < 58 And mypos <> "0" Then
            If txt.SelectionStart = mypos + 3 Then
                han.KeyChar = ""
            End If
        End If

        If txt.SelectionStart >= mypos Then
            txt.SelectionLength = 1
            han.KeyChar = han.KeyChar
        End If

        If AscW(han.KeyChar) = 46 Then

            'test = True
            mypos = InStr(1, txt.Text, ".")
            If mypos <> "0" Then txt.SelectionStart = mypos
            If mypos = 0 Then
                han.KeyChar = han.KeyChar
            Else
                han.KeyChar = ""
            End If

        End If

        If AscW(han.KeyChar) = Keys.Escape Then
            frm.Close()
        End If
    End Sub

    Sub numdot(ByVal han As KeyPressEventArgs, ByVal txt As TextBox, ByVal frm As System.Windows.Forms.Form)
        Dim mypos As Integer

        mypos = InStr(1, txt.Text, ".")

        If AscW(han.KeyChar) > 47 And AscW(han.KeyChar) < 58 Or AscW(han.KeyChar) = 8 Or AscW(han.KeyChar) = 46 Then
            han.KeyChar = han.KeyChar
        Else
            han.KeyChar = ""
        End If


        If AscW(han.KeyChar) > 47 And AscW(han.KeyChar) < 58 And mypos <> "0" Then
            If txt.SelectionStart = mypos + 2 Then
                han.KeyChar = ""
            End If
        End If

        If txt.SelectionStart >= mypos Then
            txt.SelectionLength = 1
            han.KeyChar = han.KeyChar
        End If

        If AscW(han.KeyChar) = 46 Then

            'test = True
            mypos = InStr(1, txt.Text, ".")
            If mypos <> "0" Then txt.SelectionStart = mypos
            If mypos = 0 Then
                han.KeyChar = han.KeyChar
            Else
                han.KeyChar = ""
            End If

        End If

        If AscW(han.KeyChar) = Keys.Escape Then
            frm.Close()
        End If
    End Sub

    Sub numdotkeypress(ByVal han As KeyPressEventArgs, ByVal sen As Object, ByVal frm As System.Windows.Forms.Form)
        Dim mypos As Integer

        If AscW(han.KeyChar) >= 48 And AscW(han.KeyChar) <= 57 Or AscW(han.KeyChar) = 8 Then
            han.KeyChar = han.KeyChar
        ElseIf AscW(han.KeyChar) = 46 Then
            mypos = InStr(1, sen.Text, ".")
            If mypos = 0 Then
                han.KeyChar = han.KeyChar
            Else
                han.KeyChar = ""
            End If
        Else
            han.KeyChar = ""
        End If

        If AscW(han.KeyChar) = Keys.Escape Then
            frm.Close()
        End If
    End Sub

    Sub numkeypress(ByVal han As KeyPressEventArgs, ByVal sen As Object, ByVal frm As System.Windows.Forms.Form)

        If AscW(han.KeyChar) >= 48 And AscW(han.KeyChar) <= 57 Or AscW(han.KeyChar) = 8 Then
            han.KeyChar = han.KeyChar
        Else
            han.KeyChar = ""
        End If

        If AscW(han.KeyChar) = Keys.Escape Then
            frm.Close()
        End If
    End Sub

    Function getmax(ByVal fldname As String, ByVal tbname As String, Optional ByVal whereclause As String = "") As DataTable
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim DTTABLE As DataTable

            Dim objclscommon As New ClsCommon()
            DTTABLE = objclscommon.GETMAXNO(fldname, tbname, whereclause)

            Return DTTABLE
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Function

    Function GETAGENTNAME(ByVal PARTYNAME As String) As String
        Try
            Dim OBJCMN As New ClsCommon()
            Dim DTTABLE As DataTable = OBJCMN.Execute_Any_String("SELECT ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME FROM LEDGERS INNER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.ACC_ID WHERE LEDGERS.ACC_CMPNAME = '" & PARTYNAME & "' AND LEDGERS.ACC_YEARID = " & YearId, "", "")
            If DTTABLE.Rows.Count > 0 Then Return DTTABLE.Rows(0).Item("AGENTNAME") Else Return ""
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Function getfirstdate(ByVal cmpid As Integer, Optional ByVal monthname As String = "", Optional ByVal monthno As Integer = 0) As Date
        Try
            Dim objcmn As New ClsCommon
            Dim ddate As Date
            If monthname <> "" And monthno = 0 Then
                If monthname = "April" Then monthno = 4
                If monthname = "May" Then monthno = 5
                If monthname = "June" Then monthno = 6
                If monthname = "July" Then monthno = 7
                If monthname = "August" Then monthno = 8
                If monthname = "September" Then monthno = 9
                If monthname = "October" Then monthno = 10
                If monthname = "November" Then monthno = 11
                If monthname = "December" Then monthno = 12
                If monthname = "January" Then monthno = 1
                If monthname = "February" Then monthno = 2
                If monthname = "March" Then monthno = 3

                If monthno < 4 Then
                    ddate = (objcmn.getfirstdate(Convert.ToDateTime((monthno & "/01/" & Year(AccTo)))))
                Else
                    ddate = (objcmn.getfirstdate(Convert.ToDateTime((monthno & "/01/" & Year(AccFrom)))))
                End If
            End If
            Return ddate
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function getlastdate(ByVal cmpid As Integer, Optional ByVal monthname As String = "", Optional ByVal monthno As Integer = 0) As Date
        Try
            Dim objcmn As New ClsCommon
            Dim ddate As Date
            If monthname <> "" And monthno = 0 Then
                If monthname = "April" Then monthno = 4
                If monthname = "May" Then monthno = 5
                If monthname = "June" Then monthno = 6
                If monthname = "July" Then monthno = 7
                If monthname = "August" Then monthno = 8
                If monthname = "September" Then monthno = 9
                If monthname = "October" Then monthno = 10
                If monthname = "November" Then monthno = 11
                If monthname = "December" Then monthno = 12
                If monthname = "January" Then monthno = 1
                If monthname = "February" Then monthno = 2
                If monthname = "March" Then monthno = 3

                If monthno < 4 Then
                    ddate = (objcmn.getlastdate(Convert.ToDateTime((monthno & "/01/" & Year(AccTo)))))
                Else
                    ddate = (objcmn.getlastdate(Convert.ToDateTime((monthno & "/01/" & Year(AccFrom)))))
                End If
            End If
            Return ddate
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function datecheck(ByVal dateval As Date) As Boolean
        Dim bln As Boolean = True
        If dateval.Date > AccTo.Date Or dateval.Date < AccFrom.Date Then
            bln = False
        End If
        Return bln
    End Function

    Sub enterkeypress(ByVal han As KeyPressEventArgs, ByVal frm As System.Windows.Forms.Form)
        If AscW(han.KeyChar) = 13 Then
            SendKeys.Send("{Tab}")
            han.KeyChar = ""
        End If
    End Sub

    Sub FILLEMAIL(ByRef CMBEMAIL As ComboBox, ByRef edit As Boolean)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBEMAIL.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" EMAIL_ID ", "", " EMAILMASTER", " AND EMAIL_CMPID = " & CmpId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "EMAIL_ID"
                    CMBEMAIL.DisplayMember = "EMAIL_ID"
                    CMBEMAIL.Text = ""
                End If
                CMBEMAIL.DataSource = dt
                CMBEMAIL.SelectedIndex = -1
                CMBEMAIL.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub EMAILVALIDATE(ByRef CMBEMAIL As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBEMAIL.Text.Trim <> "" Then
                uppercase(CMBEMAIL)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" EMAIL_ID ", "", " EMAILMASTER ", " and EMAIL_ID = '" & CMBEMAIL.Text.Trim & "'  and EMAIL_CMPID  = " & CmpId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Email not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBEMAIL.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objEmailmaster As New ClsEmailMaster
                        objEmailmaster.alParaval = alParaval
                        Dim IntResult As Integer = objEmailmaster.save()
                    Else
                        CMBEMAIL.Focus()
                        CMBEMAIL.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLEMP(ByRef CMBEMP As ComboBox, ByRef edit As Boolean)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBEMP.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" EMP_NAME ", "", " EMPLOYEEMASTER", " AND EMP_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "EMP_NAME"
                    CMBEMP.DisplayMember = "EMP_NAME"
                    If edit = False Then CMBEMP.Text = ""
                End If
                CMBEMP.DataSource = dt
                CMBEMP.SelectedIndex = -1
                CMBEMP.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub EMPVALIDATE(ByRef CMBEMP As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBEMP.Text.Trim <> "" Then
                uppercase(CMBEMP)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("EMP_NAME", "", "EMPLOYEEMASTER", " and EMP_NAME = '" & CMBEMP.Text.Trim & "' AND EMP_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBEMP.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Account not present, Add New?", MsgBoxStyle.YesNo, "TEXPRO")
                    If tempmsg = vbYes Then
                        CMBEMP.Text = a
                        Dim OBJEMP As New EmployeeMaster
                        OBJEMP.TEMPEMPNAME = CMBEMP.Text.Trim()
                        OBJEMP.ShowDialog()
                        dt = objclscommon.search("EMP_NAME", "", "EMPLOYEEMASTER", " and EMP_name = '" & CMBEMP.Text.Trim & "' AND EMP_YEARID = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As DataTable
                            dt1 = CMBEMP.DataSource
                            If CMBEMP.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBEMP.Text.Trim)
                                    CMBEMP.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillACCCODE(ByRef CMBCODE As ComboBox, Optional ByVal CONDITION As String = "")
        Try
            Dim objclscommon As New ClsCommon
            Dim dt As DataTable

            dt = objclscommon.search(" DISTINCT ACC_CODE ", "", " LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID ", " and ACC_cmpid=" & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId & CONDITION)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "ACC_CODE"
                CMBCODE.DisplayMember = "ACC_CODE"
                CMBCODE.Text = ""
            End If
            CMBCODE.DataSource = dt
            CMBCODE.SelectedIndex = -1
            CMBCODE.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLNAME(ByRef cmbname As ComboBox, ByRef edit As Boolean, ByVal CONDITION As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search("LEDGERS.ACC_ID, LEDGERS.ACC_cmpname ", "", "LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID ", " AND ISNULL(ACC_BLOCKED,'FALSE') = 'FALSE' and LEDGERS.ACC_cmpid=" & CmpId & " and LEDGERS.ACC_Locationid=" & Locationid & " and LEDGERS.ACC_Yearid=" & YearId & CONDITION)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "ACC_cmpname"
                    cmbname.DisplayMember = "ACC_cmpname"
                End If
                cmbname.DataSource = dt
                cmbname.SelectedIndex = -1
                cmbname.SelectAll()
                cmbname.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLSACCODE(ByRef CMBSACCODE As ComboBox, ByRef edit As Boolean)
        Try
            If CMBSACCODE.Text.Trim = "" Then
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable = OBJCMN.search(" HSN_ITEMDESC ", "", " HSNMASTER ", " AND HSN_TYPE = 'Services' and HSN_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "HSN_ITEMDESC"
                    CMBSACCODE.DisplayMember = "HSN_ITEMDESC"
                    If edit = False Then CMBSACCODE.Text = ""
                End If
                CMBSACCODE.DataSource = dt
                CMBSACCODE.SelectedIndex = -1
                CMBSACCODE.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub filltransname(ByRef cmbname As ComboBox, ByRef edit As Boolean, ByVal CONDITION As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" LEDGERS.ACC_ID, LEDGERS.ACC_cmpname ", "", "LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID ", " and LEDGERS.ACC_cmpid=" & CmpId & " and LEDGERS.ACC_Locationid=" & Locationid & " and LEDGERS.ACC_Yearid=" & YearId & CONDITION)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "ACC_cmpname"
                    cmbname.DisplayMember = "ACC_cmpname"
                    cmbname.Text = ""
                End If
                cmbname.DataSource = dt
                cmbname.SelectedIndex = -1
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillledger(ByRef cmbname As ComboBox, ByRef edit As Boolean, ByVal WHERECLAUSE As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search("ACC_ID, acc_cmpname ", "", "LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID", " AND ISNULL(ACC_BLOCKED,'FALSE') = 'FALSE' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId & WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "ACC_cmpname"
                    cmbname.DisplayMember = "ACC_cmpname"
                    cmbname.Text = ""
                End If
                cmbname.DataSource = dt
                cmbname.SelectedIndex = -1
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLDISPATCH(ByRef CMBDISPATCH As ComboBox, ByRef EDIT As Boolean, ByVal WHERECLAUSE As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBDISPATCH.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search("ADDRESS_ALIAS AS ALIAS", "", " LEDGERS INNER JOIN ADDRESSMASTER ON ADDRESS_LEDGERID = ACC_ID ", " AND ACC_YEARID = " & YearId & WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "ALIAS"
                    CMBDISPATCH.DisplayMember = "ALIAS"
                    CMBDISPATCH.Text = ""
                End If
                CMBDISPATCH.DataSource = dt
                CMBDISPATCH.SelectedIndex = -1
                CMBDISPATCH.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub DISPATCHVALIDATE(ByRef CMBDISPATCH As ComboBox, ByRef CMBNAME As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, ByRef txtadd As System.Windows.Forms.TextBox, ByVal WHERECLAUSE As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBDISPATCH.Text.Trim <> "" Then
                uppercase(CMBDISPATCH)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search("ADDRESS_ALIAS AS ALIAS, ISNULL(ADDRESS_FULL,'') AS [ADDRESS]", "", " LEDGERS INNER JOIN ADDRESSMASTER ON ADDRESS_LEDGERID = ACC_ID ", " AND ADDRESS_ALIAS = '" & CMBDISPATCH.Text.Trim & "' AND ACC_YEARID = " & YearId & WHERECLAUSE)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBDISPATCH.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Account not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBDISPATCH.Text = a
                        Dim OBJADDRESS As New addressMaster
                        OBJADDRESS.TEMPNAME = CMBNAME.Text.Trim()
                        OBJADDRESS.TEMPALIAS = CMBDISPATCH.Text.Trim()
                        OBJADDRESS.ShowDialog()
                        dt = objclscommon.search("ADDRESS_ALIAS AS ALIAS, ISNULL(ADDRESS_FULL,'') AS [ADDRESS]", "", " LEDGERS INNER JOIN ADDRESSMASTER ON ADDRESS_LEDGERID = ACC_ID ", " AND ADDRESS_ALIAS = '" & CMBDISPATCH.Text.Trim & "' AND ACC_YEARID = " & YearId & WHERECLAUSE)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As DataTable
                            dt1 = CMBDISPATCH.DataSource
                            If CMBDISPATCH.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ALIAS"), CMBDISPATCH.Text.Trim)
                                    CMBDISPATCH.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                Else
                    txtadd.Text = dt.Rows(0).Item("ADDRESS")
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillagentledger(ByRef cmbname As ComboBox, ByRef edit As Boolean, ByVal WHERECLAUSE As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search("ACC_ID, acc_cmpname ", "", "LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID", " AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId & WHERECLAUSE)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "ACC_cmpname"
                    cmbname.DisplayMember = "ACC_cmpname"
                    cmbname.Text = ""
                End If
                cmbname.DataSource = dt
                cmbname.SelectedIndex = -1
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLSALESMAN(ByRef CMBSALESMAN As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBSALESMAN.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" SALESMAN_NAME ", "", " SALESMANMASTER ", " AND SALESMAN_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "SALESMAN_NAME"
                    CMBSALESMAN.DisplayMember = "SALESMAN_NAME"
                    CMBSALESMAN.Text = ""
                End If
                CMBSALESMAN.DataSource = dt
                CMBSALESMAN.SelectedIndex = -1
                CMBSALESMAN.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub SALESMANVALIDATE(ByRef CMBSALESMAN As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBSALESMAN.Text.Trim <> "" Then
                uppercase(CMBSALESMAN)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" SALESMAN_NAME ", "", " SALESMANMASTER ", " AND SALESMAN_NAME = '" & CMBSALESMAN.Text.Trim & "' AND SALESMAN_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBSALESMAN.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Salesman not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBSALESMAN.Text = a
                        Dim OBJSALES As New SalesmanMaster
                        OBJSALES.TEMPNAME = CMBSALESMAN.Text.Trim()
                        OBJSALES.ShowDialog()
                        dt = objclscommon.search(" SALESMAN_NAME ", "", " SALESMANMASTER ", " AND SALESMAN_NAME = '" & CMBSALESMAN.Text.Trim & "' AND SALESMAN_YEARid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBSALESMAN.DataSource
                            If CMBSALESMAN.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBSALESMAN.Text.Trim)
                                    CMBSALESMAN.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub fillDESIGNER(ByRef CMBCREATEDBY As ComboBox, Optional ByVal CONDITION As String = "")
        Try
            Dim objclscommon As New ClsCommon
            Dim dt As DataTable

            dt = objclscommon.SEARCH(" DISTINCT DESIGNER_NAME", "", "DESIGNERMASTER", " and DESIGNERMASTER.DESIGNER_CMPID= " & CmpId & " ORDER BY DESIGNER_NAME ")
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "DESIGNER_NAME"
                CMBCREATEDBY.DisplayMember = "DESIGNER_NAME"
                CMBCREATEDBY.Text = ""
            End If
            CMBCREATEDBY.DataSource = dt
            CMBCREATEDBY.SelectedIndex = -1
            CMBCREATEDBY.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub DESIGNERVALIDATE(ByRef CMBDESIGNERNAME As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBDESIGNERNAME.Text.Trim <> "" Then
                uppercase(CMBDESIGNERNAME)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" DESIGNER_NAME ", "", " DESIGNERMASTER ", " AND DESIGNER_NAME = '" & CMBDESIGNERNAME.Text.Trim & "' AND DESIGNER_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBDESIGNERNAME.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Designer not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBDESIGNERNAME.Text = a
                        Dim OBJSALES As New DesignerMaster
                        OBJSALES.TEMPNAME = CMBDESIGNERNAME.Text.Trim()
                        OBJSALES.ShowDialog()
                        dt = objclscommon.search(" DESIGNER_NAME ", "", " DESIGNERMASTER ", " AND DESIGNER_NAME = '" & CMBDESIGNERNAME.Text.Trim & "' AND DESIGNER_YEARID = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBDESIGNERNAME.DataSource
                            If CMBDESIGNERNAME.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBDESIGNERNAME.Text.Trim)
                                    CMBDESIGNERNAME.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub FILLVEHICLE(ByRef CMBVEHICLENAME As ComboBox, Optional ByVal CONDITION As String = "")
        Try
            Dim objclscommon As New ClsCommon
            Dim dt As DataTable

            dt = objclscommon.SEARCH(" DISTINCT VEHICLE_NAME", "", "VEHICLEMASTER", " and VEHICLEMASTER.VEHICLE_CMPID= " & CmpId & " ORDER BY VEHICLE_NAME ")
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "VEHICLE_NAME"
                CMBVEHICLENAME.DisplayMember = "VEHICLE_NAME"
                CMBVEHICLENAME.Text = ""
            End If
            CMBVEHICLENAME.DataSource = dt
            CMBVEHICLENAME.SelectedIndex = -1
            CMBVEHICLENAME.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub VEHICLEVALIDATE(ByRef CMBVEHICLENAME As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBVEHICLENAME.Text.Trim <> "" Then
                uppercase(CMBVEHICLENAME)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" VEHICLE_NAME ", "", " VEHICLEMASTER ", " AND VEHICLE_NAME = '" & CMBVEHICLENAME.Text.Trim & "' AND VEHICLE_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBVEHICLENAME.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Vehicle not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBVEHICLENAME.Text = a
                        Dim OBJSALES As New DesignerMaster
                        OBJSALES.TEMPNAME = CMBVEHICLENAME.Text.Trim()
                        OBJSALES.ShowDialog()
                        dt = objclscommon.search(" VEHICLE_NAME ", "", " VEHICLEMASTER ", " AND VEHICLE_NAME = '" & CMBVEHICLENAME.Text.Trim & "' AND VEHICLE_YEARID = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBVEHICLENAME.DataSource
                            If CMBVEHICLENAME.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBVEHICLENAME.Text.Trim)
                                    CMBVEHICLENAME.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub filljobbername(ByRef cmbname As ComboBox, ByRef edit As Boolean, ByVal CONDITION As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" LEDGERS.ACC_ID, LEDGERS.ACC_cmpname ", "", "LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID AND GROUP_CMPID = ACC_CMPID AND GROUP_LOCATIONID = ACC_LOCATIONID AND GROUP_YEARID = ACC_YEARID ", " and LEDGERS.ACC_cmpid=" & CmpId & " and LEDGERS.ACC_Locationid=" & Locationid & " and LEDGERS.ACC_Yearid=" & YearId & CONDITION)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "ACC_cmpname"
                    cmbname.DisplayMember = "ACC_cmpname"
                    cmbname.ValueMember = "ACC_ID"
                    cmbname.Text = ""
                    'If edit = False Then cmbname.Text = ""
                End If
                cmbname.DataSource = dt
                cmbname.SelectedIndex = -1
                cmbname.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub filldepartment(ByRef CMBDEPARTMENT As ComboBox, ByRef edit As Boolean)
        Try
            If CMBDEPARTMENT.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" DEPARTMENT_name ", "", " DEPARTMENTMaster", " and DEPARTMENT_cmpid=" & CmpId & " AND DEPARTMENT_LOCATIONID = " & Locationid & " AND DEPARTMENT_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "DEPARTMENT_name"
                    CMBDEPARTMENT.DisplayMember = "DEPARTMENT_name"
                    If edit = False Then CMBDEPARTMENT.Text = ""
                End If
                CMBDEPARTMENT.DataSource = dt
                CMBDEPARTMENT.SelectedIndex = -1

                CMBDEPARTMENT.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLMILL(ByRef CMBMILLNAME As ComboBox, ByRef edit As Boolean)
        Try
            If CMBMILLNAME.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" MILL_name ", "", " MILLMASTER", " AND MILL_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "MILL_name"
                    CMBMILLNAME.DisplayMember = "MILL_name"
                    CMBMILLNAME.Text = ""
                End If
                CMBMILLNAME.DataSource = dt
                CMBMILLNAME.SelectedIndex = -1
                CMBMILLNAME.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillYARNQUALITY(ByRef CMBYARNQUALITY As ComboBox, ByRef edit As Boolean)
        Try
            If CMBYARNQUALITY.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" YARN_name ", "", " YARNQUALITYMASTER", " and YARN_cmpid=" & CmpId & " AND YARN_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "YARN_name"
                    CMBYARNQUALITY.DisplayMember = "YARN_name"
                    CMBYARNQUALITY.Text = ""
                End If
                CMBYARNQUALITY.DataSource = dt
                CMBYARNQUALITY.SelectedIndex = -1
                CMBYARNQUALITY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillQUALITY(ByRef CMBQUALITY As ComboBox, ByRef edit As Boolean)
        Try
            If CMBQUALITY.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" QUALITY_ID, QUALITY_name ", "", " QUALITYMaster", " and QUALITY_cmpid=" & CmpId & " AND QUALITY_LOCATIONID = " & Locationid & " AND QUALITY_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "QUALITY_name"
                    CMBQUALITY.DisplayMember = "QUALITY_name"
                    CMBQUALITY.ValueMember = "QUALITY_ID"
                    uppercase(CMBQUALITY)
                    CMBQUALITY.Text = ""
                End If
                CMBQUALITY.DataSource = dt
                CMBQUALITY.SelectedIndex = -1
                CMBQUALITY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillCATEGORY(ByRef CMBCATEGORY As ComboBox, ByRef edit As Boolean)
        Try
            If CMBCATEGORY.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" CATEGORY_name ", "", " CATEGORYMaster", " and CATEGORY_cmpid=" & CmpId & " AND CATEGORY_LOCATIONID = " & Locationid & " AND CATEGORY_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "CATEGORY_name"
                    CMBCATEGORY.DisplayMember = "CATEGORY_name"
                    If edit = False Then CMBCATEGORY.Text = ""
                End If
                CMBCATEGORY.DataSource = dt
                CMBCATEGORY.SelectedIndex = -1
                CMBCATEGORY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillCITY(ByRef CMBCITY As ComboBox, ByRef edit As Boolean)
        Try
            If CMBCITY.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" CITY_name ", "", " CITYMaster", " and CITY_cmpid=" & CmpId & " AND CITY_LOCATIONID = " & Locationid & " AND CITY_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "CITY_name"
                    CMBCITY.DisplayMember = "CITY_name"
                    CMBCITY.Text = ""
                End If
                CMBCITY.DataSource = dt
                CMBCITY.SelectedIndex = -1
                CMBCITY.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLPROCESS(ByRef CMBPROCESS As ComboBox)
        Try
            If CMBPROCESS.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" PROCESS_name ", "", " PROCESSMASTER", " AND PROCESS_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "PROCESS_name"
                    CMBPROCESS.DisplayMember = "PROCESS_name"
                    CMBPROCESS.Text = ""
                End If
                CMBPROCESS.DataSource = dt
                CMBPROCESS.SelectedIndex = -1
                CMBPROCESS.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSAMPLETYPE(ByRef CMBSAMPLETYPE As ComboBox)
        Try
            If CMBSAMPLETYPE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" SAMPLETYPE_name ", "", " SAMPLETYPEMASTER", " AND SAMPLETYPE_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "SAMPLETYPE_name"
                    CMBSAMPLETYPE.DisplayMember = "SAMPLETYPE_name"
                    CMBSAMPLETYPE.Text = ""
                End If
                CMBSAMPLETYPE.DataSource = dt
                CMBSAMPLETYPE.SelectedIndex = -1
                CMBSAMPLETYPE.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillregister(ByRef cmbregister As ComboBox, ByVal condition As String)
        Try
            If cmbregister.Text.Trim = "" Then

                Dim objclscommon As New ClsCommon
                Dim dt As DataTable
                dt = objclscommon.search(" Register_name ", "", "RegisterMaster ", condition & " AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "Register_name"
                    cmbregister.DisplayMember = "Register_name"
                    cmbregister.Text = ""
                End If
                cmbregister.DataSource = dt
                cmbregister.SelectedIndex = -1
                cmbregister.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub NAMEVALIDATE(ByRef cmbname As ComboBox, ByRef CMBACCCODE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, ByRef txtadd As System.Windows.Forms.TextBox, ByVal WHERECLAUSE As String, Optional ByVal GROUPNAME As String = "", Optional ByVal TYPE As String = "ACCOUNTS", Optional ByRef TRANSNAME As String = "", Optional ByRef AGENTNAME As String = "", Optional ByRef WHATSAPPNO As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim <> "" Then
                uppercase(cmbname)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search("isnull(LEDGERS.acc_add,'') as [ADDRESS], isnull(LEDGERS.ACC_CODE,'') as CODE,LEDGERS_1.ACC_CMPNAME AS TRANSNAME,LEDGERS_2.ACC_CMPNAME AS AGENTNAME, ISNULL(LEDGERS.ACC_WHATSAPPNO,'') AS WHATSAPPNO ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid AND LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_1.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_1.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_1.Acc_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_2.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_2.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_2.Acc_yearid ", " and LEDGERS.acc_cmpname = '" & cmbname.Text.Trim & "' and LEDGERS.acc_YEARid = " & YearId & WHERECLAUSE)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbname.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Ledger not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        cmbname.Text = a
                        Dim objVendormaster As New AccountsMaster
                        objVendormaster.frmstring = "ACCOUNTS"
                        objVendormaster.tempAccountsName = cmbname.Text.Trim()
                        objVendormaster.TEMPGROUPNAME = GROUPNAME
                        objVendormaster.tempTYPE = TYPE

                        objVendormaster.ShowDialog()
                        dt = objclscommon.search("isnull(LEDGERS.acc_add,'') as [ADDRESS], isnull(LEDGERS.ACC_CODE,'') as CODE,LEDGERS_1.ACC_CMPNAME AS TRANSNAME,LEDGERS_2.ACC_CMPNAME AS AGENTNAME, ISNULL(LEDGERS.ACC_WHATSAPPNO,'') AS WHATSAPPNO, LEDGERS.ACC_ID AS ACCID ", "", "    LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid AND LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_1.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_1.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_1.Acc_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_2.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_2.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_2.Acc_yearid ", " and LEDGERS.acc_cmpname = '" & cmbname.Text.Trim & "' and LEDGERS.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.acc_YEARid = " & YearId & WHERECLAUSE)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = cmbname.DataSource
                            If cmbname.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ACCID"), cmbname.Text.Trim)
                                    cmbname.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                        Exit Sub
                    End If
                Else
                    txtadd.Text = dt.Rows(0).Item("ADDRESS")
                    If ClientName = "MVIKASKUMAR" Or ClientName = "SUPRIYA" Then
                        TRANSNAME = dt.Rows(0).Item(2).ToString
                        AGENTNAME = dt.Rows(0).Item(3).ToString
                    Else
                        If TRANSNAME = "" Then TRANSNAME = dt.Rows(0).Item(2).ToString
                        If AGENTNAME = "" Then AGENTNAME = dt.Rows(0).Item(3).ToString
                    End If
                    If WHATSAPPNO = "" Then WHATSAPPNO = dt.Rows(0).Item("WHATSAPPNO")
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub ledgervalidate(ByRef cmbname As ComboBox, ByVal CMBACCCODE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, ByRef txtadd As System.Windows.Forms.TextBox, ByVal WHERECLAUSE As String, Optional ByVal GROUPNAME As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbname.Text.Trim <> "" Then
                uppercase(cmbname)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("acc_add, isnull( ACC_CODE,''), REGISTER_NAME AS REGISTERNAME", "", " LEDGERS INNER JOIN GROUPMASTER ON GROUPMASTER.group_id = LEDGERS.Acc_groupid AND GROUPMASTER.group_cmpid = LEDGERS.Acc_cmpid AND GROUPMASTER.group_locationid = LEDGERS.Acc_locationid AND GROUPMASTER.group_yearid = LEDGERS.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id AND LEDGERS.Acc_cmpid = REGISTERMASTER.register_cmpid AND LEDGERS.Acc_locationid = REGISTERMASTER.register_locationid AND LEDGERS.Acc_yearid = REGISTERMASTER.register_yearid ", " and acc_cmpname = '" & cmbname.Text.Trim & "' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId & WHERECLAUSE)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbname.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Account not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        cmbname.Text = a
                        Dim objVendormaster As New AccountsMaster
                        objVendormaster.frmstring = "ACCOUNTS"
                        objVendormaster.tempAccountsName = cmbname.Text.Trim()
                        objVendormaster.TEMPGROUPNAME = GROUPNAME
                        objVendormaster.ShowDialog()
                        dt = objclscommon.search("acc_add, REGISTER_NAME AS REGISTERNAME, ACC_ID AS ACCID", "", " LEDGERS INNER JOIN GROUPMASTER ON GROUPMASTER.group_id = LEDGERS.Acc_groupid AND GROUPMASTER.group_cmpid = LEDGERS.Acc_cmpid AND GROUPMASTER.group_locationid = LEDGERS.Acc_locationid AND GROUPMASTER.group_yearid = LEDGERS.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id AND LEDGERS.Acc_cmpid = REGISTERMASTER.register_cmpid AND LEDGERS.Acc_locationid = REGISTERMASTER.register_locationid AND LEDGERS.Acc_yearid = REGISTERMASTER.register_yearid ", " and acc_cmpname = '" & cmbname.Text.Trim & "' AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId & WHERECLAUSE)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As DataTable
                            dt1 = cmbname.DataSource
                            If cmbname.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ACCID"), cmbname.Text.Trim)
                                    cmbname.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                Else
                    txtadd.Text = dt.Rows(0).Item(0).ToString
                    CMBACCCODE.Text = dt.Rows(0).Item(1)
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FORMvalidate(ByRef cmbform As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbform.Text.Trim <> "" Then
                uppercase(cmbform)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("FORM_NAME", "", "FORMTYPE", " and FORM_NAME = '" & cmbform.Text.Trim & "'")
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbform.Text.Trim
                    Dim tempmsg As Integer = MsgBox("FORM not present, Add New?", MsgBoxStyle.YesNo, "YARNTRADE")
                    If tempmsg = vbYes Then
                        cmbform.Text = a
                        Dim OBJFORM As New citymaster
                        OBJFORM.frmstring = "FORMTYPE"
                        OBJFORM.txtname.Text = cmbform.Text.Trim()
                        OBJFORM.ShowDialog()
                        dt = objclscommon.search("FORM_name", "", "FORMTYPE", " and FORM_name = '" & cmbform.Text.Trim & "'")
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = cmbform.DataSource
                            If cmbform.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(cmbform.Text.Trim)
                                    cmbform.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub TAXvalidate(ByRef CMBTAX As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBTAX.Text.Trim <> "" Then
                uppercase(CMBTAX)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("TAX_NAME", "", "TAXMaster", " and TAX_NAME = '" & CMBTAX.Text.Trim & "' and TAX_cmpid = " & CmpId & " and TAX_Locationid = " & Locationid & " and TAX_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBTAX.Text.Trim
                    Dim tempmsg As Integer = MsgBox("TAX not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBTAX.Text = a
                        Dim OBJTAX As New Taxmaster
                        OBJTAX.txtname.Text = CMBTAX.Text.Trim()
                        OBJTAX.ShowDialog()
                        dt = objclscommon.search("TAX_name", "", "TAXMaster", " and TAX_name = '" & CMBTAX.Text.Trim & "' and TAX_cmpid = " & CmpId & " and TAX_Locationid = " & Locationid & " and TAX_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBTAX.DataSource
                            If CMBTAX.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBTAX.Text.Trim)
                                    CMBTAX.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub DESIGNVALIDATE(ByRef CMBDESIGN As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, Optional ByVal ITEMNAME As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBDESIGN.Text.Trim <> "" Then
                uppercase(CMBDESIGN)
                Dim WHERECLAUSE As String = ""
                If FETCHITEMWISEDESIGN = True And ITEMNAME <> "" Then WHERECLAUSE = " AND ISNULL(ITEMMASTER.ITEM_NAME,'')='" & ITEMNAME & "'"
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search("DESIGN_NO ", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGNMASTER.DESIGN_ITEMID=ITEMMASTER.ITEM_ID ", "  AND ISNULL(DESIGN_BLOCKED,0) = 'FALSE' " & WHERECLAUSE & " and DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' and DESIGN_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBDESIGN.Text.Trim
                    Dim tempmsg As Integer = MsgBox("DESIGN not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBDESIGN.Text = a
                        Dim OBJDESIGN As New DesignMaster
                        OBJDESIGN.tempdesignno = CMBDESIGN.Text.Trim()
                        ' OBJDESIGN.TEMPMERCHANT = CMBMERCHANT.Text.Trim()
                        OBJDESIGN.ShowDialog()
                        dt = objclscommon.search("DESIGN_ID AS DESIGNID, DESIGN_NO ", "", "  DESIGNMASTER ", "  AND ISNULL(DESIGN_BLOCKED,0) = 'FALSE' and DESIGN_NO = '" & CMBDESIGN.Text.Trim & "' and DESIGN_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBDESIGN.DataSource
                            If CMBDESIGN.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("DESIGNID"), CMBDESIGN.Text.Trim)
                                    CMBDESIGN.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub DEPARTMENTVALIDATE(ByRef CMBDEPARTMENT As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBDEPARTMENT.Text.Trim <> "" Then
                uppercase(CMBDEPARTMENT)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("DEPARTMENT_id", "", "DEPARTMENTMaster", " and DEPARTMENT_NAME = '" & CMBDEPARTMENT.Text.Trim & "' and DEPARTMENT_cmpid = " & CmpId & " and DEPARTMENT_LOCATIONid = " & Locationid & " and DEPARTMENT_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("DEPARTMENT not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBDEPARTMENT.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objclsDEPARTMENT As New ClsDepartmentMaster
                        objclsDEPARTMENT.alParaval = alParaval
                        Dim IntResult As Integer = objclsDEPARTMENT.SAVE()
                    Else
                        CMBDEPARTMENT.Focus()
                        CMBDEPARTMENT.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PROCESSVALIDATE(ByRef CMBPROCESS As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBPROCESS.Text.Trim <> "" Then
                uppercase(CMBPROCESS)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("PROCESS_id", "", "PROCESSMaster", " and PROCESS_NAME = '" & CMBPROCESS.Text.Trim & "' and PROCESS_cmpid = " & CmpId & " and PROCESS_LOCATIONid = " & Locationid & " and PROCESS_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("PROCESS not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBPROCESS.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objclsPROCESS As New ClsProcessMaster
                        objclsPROCESS.alParaval = alParaval
                        Dim IntResult As Integer = objclsPROCESS.save()
                    Else
                        CMBPROCESS.Focus()
                        CMBPROCESS.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub SAMPLETYPEVALIDATE(ByRef CMBSAMPLETYPE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBSAMPLETYPE.Text.Trim <> "" Then
                uppercase(CMBSAMPLETYPE)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("SAMPLETYPE_id", "", "SAMPLETYPEMaster", " and SAMPLETYPE_NAME = '" & CMBSAMPLETYPE.Text.Trim & "' and SAMPLETYPE_cmpid = " & CmpId & " and SAMPLETYPE_LOCATIONid = " & Locationid & " and SAMPLETYPE_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("SAMPLETYPE not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBSAMPLETYPE.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objclsSAMPLETYPE As New ClsSampleTypeMaster
                        objclsSAMPLETYPE.alParaval = alParaval
                        Dim IntResult As Integer = objclsSAMPLETYPE.save()
                    Else
                        CMBSAMPLETYPE.Focus()
                        CMBSAMPLETYPE.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub


    Sub FILLNATURE(ByRef CMBNATURE As ComboBox)
        Try
            Dim objclscommon As New ClsCommon
            Dim dt As DataTable

            dt = objclscommon.search(" PAY_name ", "", " NATUREOFPAYMENTMaster", "")
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "PAY_name"
                CMBNATURE.DisplayMember = "PAY_name"
                CMBNATURE.Text = ""
            End If
            CMBNATURE.DataSource = dt
            CMBNATURE.SelectedIndex = -1
            CMBNATURE.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub NATUREVALIDATE(ByRef CMBNATURE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBNATURE.Text.Trim <> "" Then
                uppercase(CMBNATURE)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("PAY_id", "", "NATUREOFPAYMENTMASTER", " and PAY_NAME = '" & CMBNATURE.Text.Trim & "'")
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("NATURE OF PAYMENT not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBNATURE.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim OBJNATUREOFPAYMENT As New ClsNatureOfPayment
                        OBJNATUREOFPAYMENT.alParaval = alParaval
                        Dim IntResult As Integer = OBJNATUREOFPAYMENT.SAVE()
                    Else
                        CMBNATURE.Focus()
                        CMBNATURE.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLSTOREITEMNAME(ByRef CMBSTOREITEM As ComboBox)
        Try
            If CMBSTOREITEM.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" STOREITEM_name ", "", " STOREITEMMaster", " AND STOREITEM_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "STOREITEM_name"
                    CMBSTOREITEM.DisplayMember = "STOREITEM_name"
                    CMBSTOREITEM.Text = ""
                End If
                CMBSTOREITEM.DataSource = dt
                CMBSTOREITEM.SelectedIndex = -1
                CMBSTOREITEM.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub STOREITEMVALIDATE(ByRef CMBSTOREITEM As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, Optional UNIT As Object = Nothing)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBSTOREITEM.Text.Trim <> "" Then
                uppercase(CMBSTOREITEM)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("   STOREITEMMASTER.STOREITEM_name AS STOREITEM, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT", "", "  STOREITEMMASTER LEFT OUTER JOIN UNITMASTER ON STOREITEM_UNITID = UNITMASTER.UNIT_ID ", " and  STOREITEMMASTER.STOREITEM_name = '" & CMBSTOREITEM.Text.Trim & "' and STOREITEMMASTER.STOREITEM_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBSTOREITEM.Text.Trim
                    Dim tempmsg As Integer = MsgBox("STOREITEM not present, Add New?", MsgBoxStyle.YesNo, CmpName)
                    If tempmsg = vbYes Then
                        CMBSTOREITEM.Text = a
                        Dim objSTOREITEMmaster As New StoreItemMaster
                        objSTOREITEMmaster.TEMPITEMNAME = CMBSTOREITEM.Text.Trim()

                        objSTOREITEMmaster.ShowDialog()
                        dt = objclscommon.search("   STOREITEMMASTER.STOREITEM_name AS STOREITEM, ISNULL(UNITMASTER.UNIT_ABBR,'') AS UNIT ", "", "  STOREITEMMASTER LEFT OUTER JOIN UNITMASTER ON STOREITEM_UNITID = UNITMASTER.UNIT_ID ", " and  STOREITEMMASTER.STOREITEM_name = '" & CMBSTOREITEM.Text.Trim & "' and STOREITEMMASTER.STOREITEM_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBSTOREITEM.DataSource
                            If CMBSTOREITEM.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBSTOREITEM.Text.Trim)
                                    CMBSTOREITEM.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                Else
                    If IsNothing(UNIT) = False Then UNIT.TEXT = dt.Rows(0).Item("UNIT")
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub QUALITYVALIDATE(ByRef CMBQUALITY As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, Optional ByRef unit As String = "", Optional ByRef ProcessNAME As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBQUALITY.Text.Trim <> "" Then
                uppercase(CMBQUALITY)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("   QUALITYMASTER.QUALITY_name AS QUALITY, ProcessMASTER.Process_name AS Process, UNITMASTER.unit_abbr AS UNIT ", "", "  QUALITYMASTER LEFT OUTER JOIN ProcessMASTER ON QUALITYMASTER.QUALITY_ProcessID = ProcessMASTER.Process_id AND QUALITYMASTER.QUALITY_cmpid = ProcessMASTER.Process_cmpid AND QUALITYMASTER.QUALITY_locationid = ProcessMASTER.Process_locationid AND QUALITYMASTER.QUALITY_yearid = ProcessMASTER.Process_yearid LEFT OUTER JOIN UNITMASTER ON QUALITYMASTER.QUALITY_UNITID = UNITMASTER.unit_id AND QUALITYMASTER.QUALITY_cmpid = UNITMASTER.unit_cmpid AND QUALITYMASTER.QUALITY_locationid = UNITMASTER.unit_locationid AND QUALITYMASTER.QUALITY_yearid = UNITMASTER.unit_yearid", " and  QUALITYMASTER.QUALITY_name = '" & CMBQUALITY.Text.Trim & "' and QUALITYMASTER.QUALITY_cmpid = " & CmpId & " and QUALITYMASTER.QUALITY_Locationid = " & Locationid & " and QUALITYMASTER.QUALITY_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBQUALITY.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Quality not present, Add New?", MsgBoxStyle.YesNo, CmpName)
                    If tempmsg = vbYes Then
                        CMBQUALITY.Text = a
                        Dim objqualitymaster As New QualityMaster
                        objqualitymaster.tempQualityName = CMBQUALITY.Text.Trim()

                        objqualitymaster.ShowDialog()
                        dt = objclscommon.search("   QUALITYMASTER.QUALITY_name AS QUALITY, ProcessMASTER.Process_name AS Process, UNITMASTER.unit_abbr AS UNIT, QUALITY_ID AS QUALITYID ", "", "  QUALITYMASTER LEFT OUTER JOIN ProcessMASTER ON QUALITYMASTER.QUALITY_ProcessID = ProcessMASTER.Process_id AND QUALITYMASTER.QUALITY_cmpid = ProcessMASTER.Process_cmpid AND QUALITYMASTER.QUALITY_locationid = ProcessMASTER.Process_locationid AND QUALITYMASTER.QUALITY_yearid = ProcessMASTER.Process_yearid LEFT OUTER JOIN UNITMASTER ON QUALITYMASTER.QUALITY_UNITID = UNITMASTER.unit_id AND QUALITYMASTER.QUALITY_cmpid = UNITMASTER.unit_cmpid AND QUALITYMASTER.QUALITY_locationid = UNITMASTER.unit_locationid AND QUALITYMASTER.QUALITY_yearid = UNITMASTER.unit_yearid", " and  QUALITYMASTER.QUALITY_name = '" & CMBQUALITY.Text.Trim & "' and QUALITYMASTER.QUALITY_cmpid = " & CmpId & " and QUALITYMASTER.QUALITY_Locationid = " & Locationid & " and QUALITYMASTER.QUALITY_Yearid = " & YearId)
                        'dt = objclscommon.search("Process_name", "", "ProcessMaster", " and Process_name = '" & CMBQUALITY.Text.Trim & "' and Process_cmpid = " & CmpId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBQUALITY.DataSource
                            If CMBQUALITY.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("QUALITYID"), CMBQUALITY.Text.Trim)
                                    CMBQUALITY.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                Else

                    unit = dt.Rows(0).Item(2).ToString
                    ProcessNAME = dt.Rows(0).Item(1).ToString
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub CATEGORYVALIDATE(ByRef CMBCATEGORY As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBCATEGORY.Text.Trim <> "" Then
                uppercase(CMBCATEGORY)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("CATEGORY_id", "", "CATEGORYMaster", " and CATEGORY_NAME = '" & CMBCATEGORY.Text.Trim & "' and CATEGORY_cmpid = " & CmpId & " and CATEGORY_LOCATIONid = " & Locationid & " and CATEGORY_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("CATEGORY not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBCATEGORY.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objclsCATEGORY As New ClsCategoryMaster
                        objclsCATEGORY.alParaval = alParaval
                        Dim IntResult As Integer = objclsCATEGORY.save()
                    Else
                        CMBCATEGORY.Focus()
                        CMBCATEGORY.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub CITYVALIDATE(ByRef CMBCITY As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBCITY.Text.Trim <> "" Then
                uppercase(CMBCITY)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("CITY_id", "", "CITYMaster", " and CITY_NAME = '" & CMBCITY.Text.Trim & "' and CITY_cmpid = " & CmpId & " and CITY_LOCATIONid = " & Locationid & " and CITY_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("CITY not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then

                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If

                        Dim alParaval As New ArrayList

                        alParaval.Add(UCase(CMBCITY.Text.Trim))
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objclsCITY As New ClsCityMaster
                        objclsCITY.alParaval = alParaval
                        Dim IntResult As Integer = objclsCITY.save()
                    Else
                        CMBCITY.Focus()
                        CMBCITY.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub MILLVALIDATE(ByRef CMBMILLNAME As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBMILLNAME.Text.Trim <> "" Then
                uppercase(CMBMILLNAME)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("MILL_id", "", "MILLMASTER", " and MILL_NAME = '" & CMBMILLNAME.Text.Trim & "' and MILL_cmpid = " & CmpId & "and MILL_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Mill not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBMILLNAME.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)

                        Dim objclsCATEGORY As New ClsMillMaster
                        objclsCATEGORY.alParaval = alParaval
                        Dim IntResult As Integer = objclsCATEGORY.SAVE()
                    Else
                        CMBMILLNAME.Focus()
                        CMBMILLNAME.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub YARNQUALITYVALIDATE(ByRef CMBYARNQUALITY As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBYARNQUALITY.Text.Trim <> "" Then
                uppercase(CMBYARNQUALITY)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("YARN_id", "", "YARNQUALITYMASTER", " and YARN_NAME = '" & CMBYARNQUALITY.Text.Trim & "' and YARN_cmpid = " & CmpId & "and YARN_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBYARNQUALITY.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Yarn Quality not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBYARNQUALITY.Text = a
                        Dim OBJYARN As New YarnQualityMaster
                        OBJYARN.tempname = CMBYARNQUALITY.Text.Trim()
                        OBJYARN.ShowDialog()
                        dt = objclscommon.search("YARN_name", "", " YARNQUALITYMASTER ", " and  YARN_name = '" & CMBYARNQUALITY.Text.Trim & "' and YARN_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBYARNQUALITY.DataSource
                            If CMBYARNQUALITY.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBYARNQUALITY.Text.Trim)
                                    CMBYARNQUALITY.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillitemname(ByRef cmbitemname As ComboBox, ByVal CONDITION As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclscommon.search(" ITEM_ID, item_name ", "", " itemMaster inner join MaterialTypeMaster on MaterialTypeMaster.Material_id = ItemMaster.Item_materialtypeid and MaterialTypeMaster.Material_cmpid = ItemMaster.Item_cmpid and MaterialTypeMaster.Material_locationid = ItemMaster.Item_locationid and MaterialTypeMaster.Material_yearid = ItemMaster.Item_yearid ", CONDITION & " AND ISNULL(ITEM_BLOCKED,0) = 'FALSE' and item_Yearid=" & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "item_name"
                cmbitemname.DisplayMember = "item_name"
                cmbitemname.ValueMember = "ITEM_ID"
                cmbitemname.Text = ""
                uppercase(cmbitemname)
            End If
            cmbitemname.DataSource = dt
            cmbitemname.SelectedIndex = -1
            cmbitemname.SelectAll()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillitemcode(ByRef cmbitemcode As ComboBox, ByVal CONDITION As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclscommon.search("ITEM_ID, item_code ", "", " itemMaster inner join MaterialTypeMaster on MaterialTypeMaster.Material_id = ItemMaster.Item_materialtypeid and MaterialTypeMaster.Material_cmpid = ItemMaster.Item_cmpid and MaterialTypeMaster.Material_locationid = ItemMaster.Item_locationid and MaterialTypeMaster.Material_yearid = ItemMaster.Item_yearid ", CONDITION & " AND ISNULL(ITEM_BLOCKED,0) = 'FALSE' and item_Yearid=" & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "item_code"
                cmbitemcode.DisplayMember = "item_code"
                cmbitemcode.Text = ""
                uppercase(cmbitemcode)
            End If
            cmbitemcode.DataSource = dt
            cmbitemcode.SelectedIndex = -1
            cmbitemcode.SelectAll()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillunit(ByRef cmbunit As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable = objclscommon.search(" unit_abbr ", "", " UnitMaster ", " and unit_cmpid=" & CmpId & " and unit_Locationid=" & Locationid & " and unit_Yearid=" & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "unit_abbr"
                cmbunit.DisplayMember = "unit_abbr"
            End If
            cmbunit.DataSource = dt
            cmbunit.SelectedIndex = -1
            cmbunit.SelectAll()
            cmbunit.Text = ""

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLBLANKET(ByRef CMBBLANKETNAME As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBBLANKETNAME.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" BLANKET_NAME ", "", " BLANKETMASTER ", " and BLANKET_cmpid=" & CmpId & "  and BLANKET_Yearid=" & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "BLANKET_NAME"
                    CMBBLANKETNAME.DisplayMember = "BLANKET_NAME"
                    CMBBLANKETNAME.Text = ""
                End If
                CMBBLANKETNAME.DataSource = dt
                CMBBLANKETNAME.SelectedIndex = -1
                CMBBLANKETNAME.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLDYEDTYPE(ByRef CMBDYEDTYPE As ComboBox)
        Try
            If CMBDYEDTYPE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" DYEDTYPE_NAME ", "", " DYEDTYPEMASTER ", " and DYEDTYPE_YEARID=" & YearId & " ORDER BY DYEDTYPE_NAME")
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "DYEDTYPE_NAME"
                    CMBDYEDTYPE.DisplayMember = "DYEDTYPE_NAME"
                    CMBDYEDTYPE.Text = ""
                End If
                CMBDYEDTYPE.DataSource = dt
                CMBDYEDTYPE.SelectedIndex = -1
                CMBDYEDTYPE.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub itemvalidate(ByRef cmbitemname As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, ByVal CONDITION As String, ByVal FRMSTRING As String)
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbitemname.Text.Trim <> "" Then
                uppercase(cmbitemname)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("item_name", "", " itemMaster inner join MaterialTypeMaster on MaterialTypeMaster.Material_id = ItemMaster.Item_materialtypeid and MaterialTypeMaster.Material_cmpid = ItemMaster.Item_cmpid and MaterialTypeMaster.Material_Locationid = ItemMaster.Item_Locationid and MaterialTypeMaster.Material_Yearid = ItemMaster.Item_Yearid ", CONDITION & " AND ISNULL(ITEM_BLOCKED,0) = 'FALSE' and  item_name = '" & cmbitemname.Text.Trim & "' and item_cmpid = " & CmpId & " and item_Locationid = " & Locationid & " and item_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbitemname.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Item not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        cmbitemname.Text = a
                        Dim objitemmaster As New ItemMaster
                        objitemmaster.tempItemName = cmbitemname.Text.Trim()
                        objitemmaster.frmstring = FRMSTRING
                        objitemmaster.ShowDialog()
                        dt = objclscommon.search("item_name, ITEM_ID AS ITEMID", "", " itemMaster inner join MaterialTypeMaster on MaterialTypeMaster.Material_id = ItemMaster.Item_materialtypeid and MaterialTypeMaster.Material_cmpid = ItemMaster.Item_cmpid and MaterialTypeMaster.Material_Locationid = ItemMaster.Item_Locationid and MaterialTypeMaster.Material_Yearid = ItemMaster.Item_Yearid ", CONDITION & " AND ISNULL(ITEM_BLOCKED,0) = 'FALSE' and  item_name = '" & cmbitemname.Text.Trim & "' and item_cmpid = " & CmpId & " and item_Locationid = " & Locationid & " and item_Yearid = " & YearId)
                        'dt = objclscommon.search("item_name", "", "itemMaster", " and item_name = '" & cmbitemname.Text.Trim & "' and item_cmpid = " & CmpId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = cmbitemname.DataSource
                            If cmbitemname.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ITEMID"), cmbitemname.Text.Trim)
                                    cmbitemname.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub itemcodevalidate(ByRef cmbitemcode As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, ByVal CONDITION As String, ByVal FRMSTRING As String, ByRef itemname As String, Optional ByRef unit As String = "", Optional ByRef folds As String = "", Optional ByRef category As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            If cmbitemcode.Text.Trim <> "" Then
                uppercase(cmbitemcode)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("item_code,item_name as itemname, ITEM_ID AS ITEMID", "", " ITEMMASTER ", CONDITION & " AND ISNULL(ITEM_BLOCKED,0) = 'FALSE' and  item_code = '" & cmbitemcode.Text.Trim & "' and item_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbitemcode.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Item not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        cmbitemcode.Text = a
                        Dim objitemmaster As New ItemMaster
                        objitemmaster.tempItemCODE = cmbitemcode.Text.Trim()
                        objitemmaster.frmstring = FRMSTRING

                        objitemmaster.ShowDialog()
                        dt = objclscommon.search("item_code,item_name as itemname, ITEM_ID AS ITEMID", "", " ITEMMASTER ", CONDITION & " AND ISNULL(ITEM_BLOCKED,0) = 'FALSE' and  item_code = '" & cmbitemcode.Text.Trim & "' and item_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = cmbitemcode.DataSource
                            If cmbitemcode.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ITEMID"), cmbitemcode.Text.Trim)
                                    cmbitemcode.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                Else
                    itemname = dt.Rows(0).Item("itemname")
                End If

            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub unitvalidate(ByRef cmbunit As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            Cursor.Current = Cursors.WaitCursor
            If cmbunit.Text.Trim <> "" Then
                uppercase(cmbunit)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" unit_abbr ", "", "UnitMaster", " and unit_abbr = '" & cmbunit.Text.Trim & "' and unit_cmpid = " & CmpId & " and unit_Locationid = " & Locationid & " and unit_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbunit.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Unit not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        cmbunit.Text = a
                        Dim objunitmaster As New UnitMaster
                        objunitmaster.frmString = "UNIT"
                        objunitmaster.txtabbr.Text = cmbunit.Text.Trim()
                        objunitmaster.ShowDialog()
                        dt = objclscommon.search(" unit_abbr ", "", "UnitMaster", " and unit_abbr = '" & cmbunit.Text.Trim & "' and unit_cmpid = " & CmpId & " and unit_Locationid = " & Locationid & " and unit_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = cmbunit.DataSource
                            If cmbunit.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(cmbunit.Text.Trim)
                                    cmbunit.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLRACK(ByRef CMBRACK As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBRACK.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" RACK_ID AS ID , RACK_NAME AS NAME ", "", "RACKMASTER", " And RACK_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "NAME"
                    CMBRACK.DisplayMember = "NAME"
                    CMBRACK.ValueMember = "ID"
                    CMBRACK.SelectedItem = Nothing
                End If
                CMBRACK.DataSource = dt
                CMBRACK.SelectedIndex = -1
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLCONTRACT(ByRef CMBCONTRACT As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBCONTRACT.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" CONTRACT_ID AS ID , CONTRACT_NAME AS NAME ", "", "CONTRACTMASTER", " And CONTRACT_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "NAME"
                    CMBCONTRACT.DisplayMember = "NAME"
                    CMBCONTRACT.ValueMember = "ID"
                    CMBCONTRACT.SelectedItem = Nothing
                End If
                CMBCONTRACT.DataSource = dt
                CMBCONTRACT.SelectedIndex = -1
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub CONTRACTVALIDATE(ByRef cmbcontractor As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If cmbcontractor.Text.Trim <> "" Then
                uppercase(cmbcontractor)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" CONTRACT_NAME ", "", " CONTRACTMASTER", " and CONTRACT_NAME = '" & cmbcontractor.Text.Trim & "' and CONTRACT_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Contractor not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim a As String = cmbcontractor.Text.Trim
                        Dim alParaval As New ArrayList

                        alParaval.Add(cmbcontractor.Text.Trim)
                        alParaval.Add(0)    'RATE
                        alParaval.Add("")   'REMARKS
                        alParaval.Add(CmpId)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)


                        Dim objRACK As New ClsContractMaster
                        objRACK.alParaval = alParaval
                        Dim IntResult As Integer = objRACK.SAVE()


                        dt = objclscommon.search(" CONTRACT_ID AS ID, CONTRACT_NAME AS NAME ", "", " CONTRACTMASTER", " and CONTRACT_NAME = '" & cmbcontractor.Text.Trim & "' and CONTRACT_yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = cmbcontractor.DataSource
                            If cmbcontractor.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ID"), cmbcontractor.Text.Trim)
                                    cmbcontractor.Text = a
                                End If
                            End If
                        End If

                    Else
                        cmbcontractor.Focus()
                        cmbcontractor.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub RACKVALIDATE(ByRef CMBRACK As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBRACK.Text.Trim <> "" Then
                uppercase(CMBRACK)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" RACK_NAME ", "", " RACKMASTER", " and RACK_NAME = '" & CMBRACK.Text.Trim & "' and RACK_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Rack not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim a As String = CMBRACK.Text.Trim
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBRACK.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)


                        Dim objRACK As New ClsRackMaster
                        objRACK.alParaval = alParaval
                        Dim IntResult As Integer = objRACK.SAVE()


                        dt = objclscommon.search(" RACK_ID AS ID, RACK_NAME AS NAME ", "", " RACKMASTER", " and RACK_NAME = '" & CMBRACK.Text.Trim & "' and RACK_yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBRACK.DataSource
                            If CMBRACK.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ID"), CMBRACK.Text.Trim)
                                    CMBRACK.Text = a
                                End If
                            End If
                        End If

                    Else
                        CMBRACK.Focus()
                        CMBRACK.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub FILLSHELF(ByRef CMBSHELF As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBSHELF.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" SHELF_ID AS ID , SHELF_NAME AS NAME ", "", "SHELFMASTER", " And SHELF_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "NAME"
                    CMBSHELF.DisplayMember = "NAME"
                    CMBSHELF.ValueMember = "ID"
                    CMBSHELF.SelectedItem = Nothing
                End If
                CMBSHELF.DataSource = dt
                CMBSHELF.SelectedIndex = -1
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub SHELFVALIDATE(ByRef CMBSHELF As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBSHELF.Text.Trim <> "" Then
                uppercase(CMBSHELF)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" SHELF_NAME ", "", " SHELFMASTER", " and SHELF_NAME = '" & CMBSHELF.Text.Trim & "' and SHELF_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Shelf not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim a As String = CMBSHELF.Text.Trim
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBSHELF.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)


                        Dim objRACK As New ClsShelfMaster
                        objRACK.alParaval = alParaval
                        Dim IntResult As Integer = objRACK.SAVE()


                        dt = objclscommon.search("SHELF_ID AS ID, SHELF_NAME AS NAME ", "", " SHELFMASTER", " and SHELF_NAME = '" & CMBSHELF.Text.Trim & "' and SHELF_yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBSHELF.DataSource
                            If CMBSHELF.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ID"), CMBSHELF.Text.Trim)
                                    CMBSHELF.Text = a
                                End If
                            End If
                        End If

                    Else
                        CMBSHELF.Focus()
                        CMBSHELF.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub fillTERM(ByRef CMBTERM As ComboBox, ByRef edit As Boolean)
        Try
            If CMBTERM.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                Dim WHERECLAUSE As String = ""
                dt = objclscommon.search(" TERM_NAME ", "", "TERMMaster", WHERECLAUSE & " and TERM_cmpid=" & CmpId & " AND TERM_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "TERM_NAME"
                    CMBTERM.DisplayMember = "TERM_NAME"
                    If edit = False Then CMBTERM.Text = ""
                End If
                CMBTERM.DataSource = dt
                CMBTERM.SelectedIndex = -1
                CMBTERM.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TERMVALIDATE(ByRef CMBTERM As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBTERM.Text.Trim <> "" Then
                uppercase(CMBTERM)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" TERM_NAME ", "", " TERMMASTER", " and TERM_NAME = '" & CMBTERM.Text.Trim & "' and TERM_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Term not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim a As String = CMBTERM.Text.Trim
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBTERM.Text.Trim)
                        alParaval.Add(0)    'CRDAYS
                        alParaval.Add(0)    'OTHER PER
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)


                        Dim objRACK As New ClsTermMaster
                        objRACK.alParaval = alParaval
                        Dim IntResult As Integer = objRACK.SAVE()


                        dt = objclscommon.search(" TERM_ID AS ID, TERM_NAME AS NAME ", "", " TERMMASTER", " and TERM_NAME = '" & CMBTERM.Text.Trim & "' and TERM_yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBTERM.DataSource
                            If CMBTERM.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ID"), CMBTERM.Text.Trim)
                                    CMBTERM.Text = a
                                End If
                            End If
                        End If

                    Else
                        CMBTERM.Focus()
                        CMBTERM.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If


        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLGROUPCOMPANY(ByRef CMBGRPCOMPANY As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBGRPCOMPANY.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" GOC_ID AS ID , GOC_NAME AS NAME ", "", "GROUPOFCOMPANIESMASTER", " And GOC_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "NAME"
                    CMBGRPCOMPANY.DisplayMember = "NAME"
                    CMBGRPCOMPANY.ValueMember = "ID"
                    CMBGRPCOMPANY.SelectedItem = Nothing
                End If
                CMBGRPCOMPANY.DataSource = dt
                CMBGRPCOMPANY.SelectedIndex = -1
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub GROUPCOMPANYVALIDATE(ByRef CMBGRPCOMPANY As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBGRPCOMPANY.Text.Trim <> "" Then
                uppercase(CMBGRPCOMPANY)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" GOC_NAME ", "", " GROUPOFCOMPANIESMASTER ", " and GOC_NAME = '" & CMBGRPCOMPANY.Text.Trim & "' and GOC_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Company not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim a As String = CMBGRPCOMPANY.Text.Trim
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBGRPCOMPANY.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)


                        Dim objRACK As New ClsGroupOfCompanies
                        objRACK.alParaval = alParaval
                        Dim IntResult As Integer = objRACK.SAVE()


                        dt = objclscommon.search(" GOC_ID AS ID, GOC_NAME AS NAME ", "", " GROUPOFCOMPANIESMASTER", " and GOC_NAME = '" & CMBGRPCOMPANY.Text.Trim & "' and GOC_yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBGRPCOMPANY.DataSource
                            If CMBGRPCOMPANY.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ID"), CMBGRPCOMPANY.Text.Trim)
                                    CMBGRPCOMPANY.Text = a
                                End If
                            End If
                        End If

                    Else
                        CMBGRPCOMPANY.Focus()
                        CMBGRPCOMPANY.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub FILLPACKINGTYPE(ByRef CMBGRPCOMPANY As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBGRPCOMPANY.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" PACKINGTYPE_ID AS ID , PACKINGTYPE_NAME AS NAME ", "", "PACKINGTYPEMASTER", " And PACKINGTYPE_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "NAME"
                    CMBGRPCOMPANY.DisplayMember = "NAME"
                    CMBGRPCOMPANY.ValueMember = "ID"
                    CMBGRPCOMPANY.SelectedItem = Nothing
                End If
                CMBGRPCOMPANY.DataSource = dt
                CMBGRPCOMPANY.SelectedIndex = -1
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PACKINGTYPEVALIDATE(ByRef CMBPACKINGTYPE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBPACKINGTYPE.Text.Trim <> "" Then
                uppercase(CMBPACKINGTYPE)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" PACKINGTYPE_NAME ", "", " PACKINGTYPEMASTER ", " and PACKINGTYPE_NAME = '" & CMBPACKINGTYPE.Text.Trim & "' and PACKINGTYPE_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Packing Type not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim a As String = CMBPACKINGTYPE.Text.Trim
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBPACKINGTYPE.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(0)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objRACK As New ClsPackingTypeMaster
                        objRACK.alParaval = alParaval
                        Dim IntResult As Integer = objRACK.save()


                        dt = objclscommon.search(" PACKINGTYPE_ID AS ID, PACKINGTYPE_NAME AS NAME ", "", " PACKINGTYPEMASTER", " and PACKINGTYPE_NAME = '" & CMBPACKINGTYPE.Text.Trim & "' and PACKINGTYPE_yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBPACKINGTYPE.DataSource
                            If CMBPACKINGTYPE.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ID"), CMBPACKINGTYPE.Text.Trim)
                                    CMBPACKINGTYPE.Text = a
                                End If
                            End If
                        End If

                    Else
                        CMBPACKINGTYPE.Focus()
                        CMBPACKINGTYPE.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub FILLMACHINE(ByRef CMBMACHINE As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBMACHINE.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable

                dt = objclscommon.search(" MACHINE_ID AS ID , MACHINE_NAME AS NAME ", "", " MACHINEMASTER ", " And MACHINE_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "NAME"
                    CMBMACHINE.DisplayMember = "NAME"
                    CMBMACHINE.ValueMember = "ID"
                    CMBMACHINE.SelectedItem = Nothing
                End If
                CMBMACHINE.DataSource = dt
                CMBMACHINE.SelectedIndex = -1
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub MACHINEVALIDATE(ByRef CMBMACHINE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBMACHINE.Text.Trim <> "" Then
                uppercase(CMBMACHINE)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" MACHINE_NAME ", "", "MACHINEMASTER", " and MACHINE_NAME = '" & CMBMACHINE.Text.Trim & "' and MACHINE_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Machine not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        Dim a As String = CMBMACHINE.Text.Trim
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBMACHINE.Text.Trim)
                        alParaval.Add(0)    'AREA
                        alParaval.Add(0)    'LENGTH
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)


                        Dim objRACK As New ClsMachineMaster
                        objRACK.alParaval = alParaval
                        Dim IntResult As Integer = objRACK.SAVE()


                        dt = objclscommon.search(" MACHINE_ID AS ID, MACHINE_NAME AS NAME ", "", "MACHINEMASTER", " and MACHINE_NAME = '" & CMBMACHINE.Text.Trim & "' and MACHINE_yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBMACHINE.DataSource
                            If CMBMACHINE.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("ID"), CMBMACHINE.Text.Trim)
                                    CMBMACHINE.Text = a
                                End If
                            End If
                        End If

                    Else
                        CMBMACHINE.Focus()
                        CMBMACHINE.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub ACCCODEVALIDATE(ByRef CMBCODE As ComboBox, ByVal CMBACCNAME As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form, ByRef TXTADD As System.Windows.Forms.TextBox, Optional ByVal WHERECLAUSE As String = "", Optional ByVal GROUPNAME As String = "")
        Try
            If CMBCODE.Text.Trim <> "" Then
                uppercase(CMBCODE)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("acc_CMPNAME, ACC_ADD", "", "Ledgers inner join groupmaster on groupmaster.group_id = ledgers.acc_groupid and groupmaster.group_cmpid = ledgers.acc_cmpid and groupmaster.group_locationid = ledgers.acc_locationid and groupmaster.group_yearid = ledgers.acc_yearid", " and acc_cODE = '" & CMBCODE.Text.Trim & "' and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId & WHERECLAUSE)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("Ledger not present, Add New?", MsgBoxStyle.YesNo, "")
                    If tempmsg = vbYes Then
                        Dim objVendormaster As New AccountsMaster
                        objVendormaster.frmstring = "ACCOUNTS"
                        objVendormaster.tempAccountsCode = CMBCODE.Text.Trim()
                        objVendormaster.TEMPGROUPNAME = GROUPNAME
                        objVendormaster.ShowDialog()
                        dt = objclscommon.search("ACC_CODE", "", "Ledgers inner join groupmaster on groupmaster.group_id = ledgers.acc_groupid and groupmaster.group_cmpid = ledgers.acc_cmpid and groupmaster.group_locationid = ledgers.acc_locationid and groupmaster.group_yearid = ledgers.acc_yearid", " and acc_cODE = '" & CMBCODE.Text.Trim & "' and acc_cmpid = " & CmpId & " and acc_LOCATIONid = " & Locationid & " and acc_YEARid = " & YearId & WHERECLAUSE)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As DataTable
                            Dim a As String = CMBCODE.Text.Trim
                            dt1 = CMBCODE.DataSource
                            If CMBCODE.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBCODE.Text.Trim)
                                    CMBCODE.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                        Exit Sub
                    End If
                Else
                    CMBACCNAME.Text = dt.Rows(0).Item(0)
                    TXTADD.Text = dt.Rows(0).Item(1)
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        End Try
    End Sub

    Sub fillGODOWN(ByRef CMBGODOWN As ComboBox, ByRef edit As Boolean)
        Try
            If CMBGODOWN.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                Dim WHERECLAUSE As String = ""
                dt = objclscommon.search(" GODOWN_name ", "", " GODOWNMaster", WHERECLAUSE & " AND GODOWN_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "GODOWN_name"
                    CMBGODOWN.DisplayMember = "GODOWN_name"
                    If edit = False Then CMBGODOWN.Text = ""
                End If
                CMBGODOWN.DataSource = dt
                CMBGODOWN.SelectedIndex = -1
                CMBGODOWN.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GODOWNVALIDATE(ByRef CMBGODOWN As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBGODOWN.Text.Trim <> "" Then
                uppercase(CMBGODOWN)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("GODOWN_id", "", "GODOWNMaster", " and GODOWN_NAME = '" & CMBGODOWN.Text.Trim & "' and GODOWN_cmpid = " & CmpId & " and GODOWN_LOCATIONid = " & Locationid & " and GODOWN_YEARid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("GODOWN not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then

                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If

                        Dim alParaval As New ArrayList

                        alParaval.Add(UCase(CMBGODOWN.Text.Trim))
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objclsGODOWN As New ClsGodownMaster
                        objclsGODOWN.alParaval = alParaval
                        Dim IntResult As Integer = objclsGODOWN.save()
                    Else
                        CMBGODOWN.Focus()
                        CMBGODOWN.SelectAll()
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub HSNITEMDESCVALIDATE(ByRef CMBHSNCODE As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBHSNCODE.Text.Trim <> "" Then
                uppercase(CMBHSNCODE)
                Dim OBJCMN As New ClsCommonMaster
                Dim dt As DataTable
                dt = OBJCMN.search("   ISNULL(HSN_CODE, '') AS HSNCODE", "", "  HSNMASTER ", "and  HSN_CODE = '" & CMBHSNCODE.Text.Trim & "' and HSN_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBHSNCODE.Text.Trim
                    Dim tempmsg As Integer = MsgBox("HSN/SAC Code Not present, Add New?", MsgBoxStyle.YesNo, CmpName)
                    If tempmsg = vbYes Then
                        CMBHSNCODE.Text = a
                        Dim OBJDELIVERY As New HSNMaster
                        OBJDELIVERY.tempHSNCODE = CMBHSNCODE.Text.Trim()

                        OBJDELIVERY.ShowDialog()
                        dt = OBJCMN.search("   ISNULL(HSN_CODE, '') AS HSNCODE", "", "  HSNMASTER ", " and  HSN_CODE = '" & CMBHSNCODE.Text.Trim & "' and HSN_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBHSNCODE.DataSource
                            If CMBHSNCODE.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBHSNCODE.Text.Trim)
                                    CMBHSNCODE.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub FILLGROUP(ByRef CMBGROUP As ComboBox)
        Try
            If CMBGROUP.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("group_name", "", "GroupMaster", " and group_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "Group_name"
                    CMBGROUP.DisplayMember = "group_name"
                    CMBGROUP.Text = ""
                End If
                CMBGROUP.DataSource = dt
                CMBGROUP.SelectedIndex = -1
                CMBGROUP.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLBANK(ByRef CMBBANK As ComboBox)
        Try
            If CMBBANK.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" PARTYBANK_name ", "", " PARTYBANKMaster ", " and PARTYBANK_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "PARTYBANK_name"
                    CMBBANK.DisplayMember = "PARTYBANK_name"
                    CMBBANK.Text = ""
                End If
                CMBBANK.DataSource = dt
                CMBBANK.SelectedIndex = -1
                CMBBANK.SelectAll()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PARTYBANKvalidate(ByRef CMBPARTYBANK As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try

            If CMBPARTYBANK.Text.Trim <> "" Then
                uppercase(CMBPARTYBANK)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search(" PARTYBANK_name ", "", "PARTYBANKMaster", " and PARTYBANK_name = '" & CMBPARTYBANK.Text.Trim & "' and PARTYBANK_cmpid = " & CmpId & " and PARTYBANK_locationid = " & Locationid & " and PARTYBANK_yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim tempmsg As Integer = MsgBox("PARTYBANK Name not present, Add New?", MsgBoxStyle.YesNo, "BROKERMATE")
                    If tempmsg = vbYes Then
                        Dim alParaval As New ArrayList

                        alParaval.Add(CMBPARTYBANK.Text.Trim)
                        alParaval.Add("")
                        alParaval.Add(CmpId)
                        alParaval.Add(Locationid)
                        alParaval.Add(Userid)
                        alParaval.Add(YearId)
                        alParaval.Add(0)

                        Dim objPIECETYPE As New ClsPARTYBANKMaster
                        objPIECETYPE.alParaval = alParaval
                        Dim IntResult As Integer = objPIECETYPE.save()
                        'e.Cancel = True
                    Else
                        CMBPARTYBANK.Focus()
                        CMBPARTYBANK.SelectAll()
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PARENTDESIGNOVALIDATE(ByRef CMBPARENTDESIGNNO As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBPARENTDESIGNNO.Text.Trim <> "" Then
                uppercase(CMBPARENTDESIGNNO)
                Dim WHERECLAUSE As String = ""
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search("DESIGN_PARENTDESIGNID ", "", " DESIGNMASTER  ", "  AND ISNULL(DESIGN_BLOCKED,0) = 'FALSE' " & WHERECLAUSE & " and DESIGN_NO = '" & CMBPARENTDESIGNNO.Text.Trim & "' and DESIGN_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBPARENTDESIGNNO.Text.Trim
                    Dim tempmsg As Integer = MsgBox("PARENTDESIGNNO not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBPARENTDESIGNNO.Text = a
                        Dim OBJDESIGN As New DesignMaster
                        OBJDESIGN.tempdesignno = CMBPARENTDESIGNNO.Text.Trim()
                        ' OBJDESIGN.TEMPMERCHANT = CMBMERCHANT.Text.Trim()
                        OBJDESIGN.ShowDialog()
                        dt = objclscommon.search("DESIGN_ID AS DESIGNID, DESIGN_PARENTDESIGNID ", "", "  DESIGNMASTER ", "  AND ISNULL(DESIGN_BLOCKED,0) = 'FALSE' and DESIGN_PARENTDESIGNID = '" & CMBPARENTDESIGNNO.Text.Trim & "' and DESIGN_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As New DataTable
                            dt1 = CMBPARENTDESIGNNO.DataSource
                            If CMBPARENTDESIGNNO.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(dt.Rows(0).Item("DESIGNID"), CMBPARENTDESIGNNO.Text.Trim)
                                    CMBPARENTDESIGNNO.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

#Region "FUNCTION FOR EMAIL"

    Sub sendemail(ByVal toMail As String, ByVal tempattachment As String, ByVal mailbody As String, ByVal subject As String, Optional ByVal ALATTACHMENT As ArrayList = Nothing, Optional ByVal NOOFATTACHMENTS As Integer = 0, Optional ByVal TEMPATTACHMENT1 As String = "", Optional ByVal TEMPATTACHMENT2 As String = "", Optional ByVal TEMPATTACHMENT3 As String = "", Optional ByVal TEMPATTACHMENT4 As String = "", Optional ByVal TEMPATTACHMENT5 As String = "", Optional ByVal TEMPATTACHMENT6 As String = "")

        'Dim mailBody As String
        Try
            Cursor.Current = Cursors.WaitCursor

            'create the mail message
            Dim mail As New MailMessage
            Dim MAILATTACHMENT As Attachment

            'set the addresses
            'mail.From = New MailAddress("siddhivinayaksynthetics@gmail.com", CmpName)
            'mail.From = New MailAddress("gulkitjain@gmail.com", "TexPro V1.0")

            mail.To.Add(toMail)

            '''' GIVING ISSUE IN DIRECT MULTIPLE PRINT IN INVOICE.
            ''set the content
            'mail.Subject = subject
            'mail.Body = mailbody
            'mail.IsBodyHtml = True
            'MAILATTACHMENT = New Attachment(tempattachment)
            'mail.Attachments.Add(MAILATTACHMENT)

            'If TEMPATTACHMENT1 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT1)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If

            'If TEMPATTACHMENT2 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT2)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If

            'If TEMPATTACHMENT3 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT3)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If

            'If TEMPATTACHMENT4 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT4)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If


            'If TEMPATTACHMENT5 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT5)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If

            'If TEMPATTACHMENT6 <> "" Then
            '    MAILATTACHMENT = New Attachment(TEMPATTACHMENT6)
            '    mail.Attachments.Add(MAILATTACHMENT)
            'End If

            'set the content
            mail.Subject = subject
            mail.Body = mailbody
            mail.IsBodyHtml = True
            If NOOFATTACHMENTS <= 1 Then
                If ALATTACHMENT.Count > 0 Then MAILATTACHMENT = New Attachment(ALATTACHMENT(0)) Else MAILATTACHMENT = New Attachment(tempattachment)
                mail.Attachments.Add(MAILATTACHMENT)
            Else
                For I As Integer = 0 To NOOFATTACHMENTS - 1
                    MAILATTACHMENT = New Attachment(ALATTACHMENT(I))
                    mail.Attachments.Add(MAILATTACHMENT)
                Next
            End If


            'send the message
            Dim smtp As New SmtpClient

            'set username and password
            Dim nc As New System.Net.NetworkCredential


            'GET SMTP, EMAILADD AND PASSWORD FROM USERMASTER
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("USER_SMTP AS SMTP, USER_SMTPEMAIL AS EMAIL, USER_SMTPPASS AS PASS", "", " USERMASTER", " AND USER_NAME = '" & UserName & "' and USER_CMPID = " & CmpId)
            If DT.Rows.Count > 0 Then
                If DT.Rows(0).Item("SMTP") = "" Then smtp.Host = "smtp.gmail.com" Else smtp.Host = DT.Rows(0).Item("SMTP")
                'smtp.Port = (25)
                smtp.Port = (587)


                smtp.EnableSsl = True
                mail.From = New MailAddress(DT.Rows(0).Item("EMAIL"), CmpName)
                nc.UserName = DT.Rows(0).Item("EMAIL")
                nc.Password = DT.Rows(0).Item("PASS") '"qhokuzymfmcxtoge"

            Else

                smtp.Host = "smtp.gmail.com"
                'smtp.Port = (25)
                smtp.Port = (587)
                smtp.EnableSsl = True

                mail.From = New MailAddress("noreply.textrade@gmail.com", CmpName)
                nc.UserName = "noreply.textrade@gmail.com"
                nc.Password = "tlaztqqpfbelzglr"

            End If


            'smtp.Timeout = 20000
            smtp.Timeout = 50000

            smtp.Credentials = nc

            smtp.Send(mail)
            mail.Dispose()

        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

#End Region

    Function checkrowlinedel(ByVal gridsrno As Integer, ByVal txtno As TextBox) As Boolean
        Dim bln As Boolean = True
        If gridsrno = Val(txtno.Text.Trim) Then
            bln = False
        End If
        Return bln
    End Function

    Sub commakeypress(ByVal han As KeyPressEventArgs, ByVal sen As Control, ByVal frm As System.Windows.Forms.Form)
        If AscW(han.KeyChar) = 44 Then
            han.KeyChar = ""
        End If

    End Sub

    'Sub FILLCMB(ByVal CMBYARNQUALITY As ComboBox)
    '    Try
    '        If CMBYARNQUALITY.Text.Trim = "" Then
    '            Dim objclscommon As New ClsCommonMaster
    '            Dim dt As DataTable
    '            dt = objclscommon.search(" YARN_NAME ", "", " YARNQUALITYMASTER ", " and YARN_cmpid=" & CmpId & "  and YARN_YEARID = " & YearId)
    '            CMBYARNQUALITY.DataSource = dt
    '            If dt.Rows.Count > 0 Then
    '                dt.DefaultView.Sort = "YARN_NAME"
    '                CMBYARNQUALITY.DisplayMember = "YARN_NAME"
    '                CMBYARNQUALITY.Text = ""
    '            End If
    '            CMBYARNQUALITY.SelectAll()
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Sub GROUPVALIDATE(ByRef CMBNAME As ComboBox, ByRef e As System.ComponentModel.CancelEventArgs, ByRef frm As System.Windows.Forms.Form)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBNAME.Text.Trim <> "" Then
                uppercase(CMBNAME)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("GROUP_NAME", "", "GROUPMASTER", " and GROUP_NAME = '" & CMBNAME.Text.Trim & "' AND GROUP_YEARID = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBNAME.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Account not present, Add New?", MsgBoxStyle.YesNo, "TEXTRADE")
                    If tempmsg = vbYes Then
                        CMBNAME.Text = a
                        Dim OBJEMP As New EmployeeMaster
                        OBJEMP.TEMPEMPNAME = CMBNAME.Text.Trim()
                        OBJEMP.ShowDialog()
                        dt = objclscommon.search("GROUP_NAME", "", "GROUPMASTER", " and GROUP_name = '" & CMBNAME.Text.Trim & "' AND GROUP_YEARID = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As DataTable
                            dt1 = CMBNAME.DataSource
                            If CMBNAME.DataSource <> Nothing Then
line1:
                                If dt1.Rows.Count > 0 Then
                                    dt1.Rows.Add(CMBNAME.Text.Trim)
                                    CMBNAME.Text = a
                                End If
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
End Module
