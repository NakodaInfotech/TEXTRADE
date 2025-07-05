Imports System.ComponentModel
Imports BL

Public Class UpdateLotNo

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            TXTGRNNO.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            EP.Clear()
            TXTGRNNO.Clear()
            TXTNAME.Clear()
            TXTDYEINGNAME.Clear()
            TXTCHALLANNO.Clear()
            TXTITEMNAME.Clear()
            TXTPCS.Clear()
            TXTMTRS.Clear()
            CMBDYEING.Text = ""
            TXTLOTNO.Clear()
            RECDATE.Text = Now.Date
            CMBTYPE.SelectedIndex = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPDATE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDUPDATE.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim DTLEDGER As DataTable = OBJCMN.SEARCH("ISNULL(ACC_ID,0) AS LEDGERID", "", " LEDGERS", " AND ACC_CMPNAME = '" & CMBDYEING.Text.Trim & "' AND ACC_YEARID = " & YearId)
            Dim DT As New DataTable
            If CMBTYPE.Text = "GRN" Then
                DT = OBJCMN.Execute_Any_String(" UPDATE GRN SET GRN.GRN_PLOTNO = '" & TXTLOTNO.Text.Trim & "', GRN_RECDATE = '" & RECDATE.Text & "', GRN_TOLEDGERID = " & DTLEDGER.Rows(0).Item("LEDGERID") & " WHERE GRN.GRN_NO = " & Val(TXTGRNNO.Text.Trim) & " And GRN.GRN_TYPE = 'Job Work' AND GRN.GRN_YEARID = " & YearId, "", "")
                DT = OBJCMN.Execute_Any_String(" UPDATE CHECKINGMASTER SET CHECKINGMASTER.CHECK_LOTNO = '" & TXTLOTNO.Text.Trim & "', CHECK_LEDGERID = " & DTLEDGER.Rows(0).Item("LEDGERID") & " WHERE CHECKINGMASTER.CHECK_GRNNO = " & Val(TXTGRNNO.Text.Trim) & " And CHECKINGMASTER.CHECK_TYPE = 'Job Work' AND CHECKINGMASTER.CHECK_YEARID = " & YearId, "", "")
                DT = OBJCMN.Execute_Any_String(" UPDATE LOTTAGGING_DESC SET LOTTAGGING_DESC.LOTTAG_LOTNO = '" & TXTLOTNO.Text.Trim & "' WHERE LOTTAGGING_DESC.LOTTAG_FROMNO = " & Val(TXTGRNNO.Text.Trim) & " And LOTTAGGING_DESC.LOTTAG_FROMTYPE = 'CHECKING' AND LOTTAGGING_DESC.LOTTAG_YEARID = " & YearId, "", "")
                DT = OBJCMN.Execute_Any_String(" UPDATE PURCHASEMASTER_DESC SET PURCHASEMASTER_DESC.BILL_LOTNO = '" & TXTLOTNO.Text.Trim & "' WHERE PURCHASEMASTER_DESC.BILL_GRNNO = " & Val(TXTGRNNO.Text.Trim) & " And PURCHASEMASTER_DESC.BILL_TYPE = 'Job Work' AND PURCHASEMASTER_DESC.BILL_YEARID = " & YearId, "", "")
            ElseIf CMBTYPE.Text.Trim = "JOBOUT" Then
                DT = OBJCMN.Execute_Any_String(" UPDATE JOBOUT SET JOBOUT.JO_LOTNO = '" & TXTLOTNO.Text.Trim & "', JO_LEDGERID = " & DTLEDGER.Rows(0).Item("LEDGERID") & " WHERE JOBOUT.JO_NO = " & Val(TXTGRNNO.Text.Trim) & " And JOBOUT.JO_YEARID = " & YearId, "", "")
                DT = OBJCMN.Execute_Any_String(" UPDATE JOBIN SET JOBIN.JI_LOTNO = '" & TXTLOTNO.Text.Trim & "', JI_LEDGERID = " & DTLEDGER.Rows(0).Item("LEDGERID") & " WHERE JOBIN.JI_JOBOUTNO = " & Val(TXTGRNNO.Text.Trim) & " And JOBIN.JI_YEARID = " & YearId, "", "")
            Else
                DT = OBJCMN.Execute_Any_String(" UPDATE STOCKMASTER SET STOCKMASTER.SM_LOTNO = '" & TXTLOTNO.Text.Trim & "', SM_LEDGERIDTO = " & DTLEDGER.Rows(0).Item("LEDGERID") & " WHERE STOCKMASTER.SM_PARTYCHALLANNO = '" & TXTGRNNO.Text.Trim & "' And STOCKMASTER.SM_YEARID = " & YearId, "", "")
            End If
            MsgBox("Lot No Updated Successfully")
            CLEAR()
            TXTGRNNO.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If TXTGRNNO.Text.Trim.Length = 0 Then
            EP.SetError(TXTGRNNO, "Enter GRN No")
            bln = False
        End If

        Dim OBJCMN As New ClsCommon
        Dim DTTABLE As New DataTable

        'CHECK WHETHER MTRS ARE RECD FROM JOBBER OR NOT, IF RECD THEN DONT ALLOW TO UPDATE
        DTTABLE = OBJCMN.SEARCH("ISNULL(LOT_VIEW.RECDMTRS,0) AS RECDMTRS, ISNULL(LOT_VIEW.LOTNO, '') AS LOTNO", "", "LOT_VIEW", " AND LOT_VIEW.GRNNO = " & Val(TXTGRNNO.Text.Trim) & " AND JOBBERNAME = '" & CMBDYEING.Text.Trim & "' AND GRNTYPE = CASE WHEN '" & CMBTYPE.Text.Trim & "' = 'GRN' THEN 'CHECKING' WHEN '" & CMBTYPE.Text.Trim & "' = 'JOBOUT' THEN 'JOBOUT' ELSE 'OPENING' END AND YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 AndAlso Val(DTTABLE.Rows(0).Item("RECDMTRS")) > 0 And (DTTABLE.Rows(0).Item("LOTNO")) <> "" Then
            EP.SetError(TXTGRNNO, "Unable to Update Lot No, Material Recd")
            bln = False
        End If


        If Val(TXTGRNNO.Text.Trim) <> 0 Then
            If CMBTYPE.Text.Trim = "GRN" Then
                'DTTABLE = OBJCMN.SEARCH(" ISNULL(GRN.GRN_NO, 0) AS GRNNO, ISNULL(GRN.GRN_PLOTNO, '0') AS LOTNO, ISNULL(GRN.GRN_INHOUSECHECKDONE, 0) AS INHOUSECHECKINGDONE, ISNULL(GRN_DESC.GRN_CHECKDONE, 0) AS CHECKINGDONE, ISNULL(LEDGERS.ACC_CMPNAME,'') AS DYEINGNAME ", "", " GRN INNER JOIN GRN_DESC ON GRN.GRN_NO = GRN_DESC.GRN_NO AND GRN.GRN_TYPE = GRN_DESC.GRN_GRIDTYPE AND GRN.GRN_YEARID = GRN_DESC.GRN_YEARID INNER JOIN LEDGERS ON ACC_ID = GRN_TOLEDGERID ", " AND GRN_TYPE = 'Job Work' AND GRN.GRN_NO = " & Val(TXTGRNNO.Text.Trim) & " AND GRN.GRN_YEARID = " & YearId)
                'If DTTABLE.Rows.Count > 0 AndAlso (Convert.ToBoolean(DTTABLE.Rows(0).Item("INHOUSECHECKINGDONE")) = True Or Convert.ToBoolean(DTTABLE.Rows(0).Item("CHECKINGDONE")) = True) Then
                '    If Val(TXTGRNNO.Text.Trim) <> 0 Then EP.SetError(TXTGRNNO, "GRN Checking Already Done")
                '    bln = False
                'End If
            ElseIf CMBTYPE.Text.Trim = "JOBOUT" Then
                DTTABLE = OBJCMN.SEARCH(" ISNULL(JOBOUT.JO_NO, 0) AS GRNNO, ISNULL(JOBOUT.JO_LOTNO, '0') AS LOTNO, ISNULL(JOBOUT.JO_RECDMTRS, 0) AS RECDMTRS, ISNULL(JOBOUT.JO_LOTCOMPLETED, 0) AS LOTCOMPLETED", "", " JOBOUT INNER JOIN JOBOUT_DESC ON JOBOUT.JO_NO = JOBOUT_DESC.JO_NO AND JOBOUT.JO_YEARID = JOBOUT_DESC.JO_YEARID ", " AND JOBOUT.JO_NO = " & Val(TXTGRNNO.Text.Trim) & " AND JOBOUT.JO_YEARID = " & YearId)
                If DTTABLE.Rows.Count > 0 AndAlso (Val(DTTABLE.Rows(0).Item("RECDMTRS")) > 0 Or Convert.ToBoolean(DTTABLE.Rows(0).Item("LOTCOMPLETED")) = True) And (DTTABLE.Rows(0).Item("LOTNO")) <> "" Then
                    If Val(TXTGRNNO.Text.Trim) <> 0 Then EP.SetError(TXTGRNNO, "Job In Done or Lot Locked")
                    bln = False
                End If
            Else
                'DTTABLE = OBJCMN.SEARCH(" ISNULL(STOCKMASTER.SM_NO, 0) AS SMNO, ISNULL(JOBOUT.JO_LOTNO, '0') AS LOTNO, ISNULL(JOBOUT.JO_RECDMTRS, 0) AS RECDMTRS, ISNULL(JOBOUT.JO_LOTCOMPLETED, 0) AS LOTCOMPLETED", "", " JOBOUT INNER JOIN JOBOUT_DESC ON JOBOUT.JO_NO = JOBOUT_DESC.JO_NO AND JOBOUT.JO_YEARID = JOBOUT_DESC.JO_YEARID ", " AND JOBOUT.JO_NO = " & Val(TXTGRNNO.Text.Trim) & " AND JOBOUT.JO_YEARID = " & YearId)
                'If DTTABLE.Rows.Count > 0 AndAlso (Val(DTTABLE.Rows(0).Item("RECDMTRS")) > 0 Or Convert.ToBoolean(DTTABLE.Rows(0).Item("LOTCOMPLETED")) = True) Then
                '    If Val(TXTGRNNO.Text.Trim) <> 0 Then EP.SetError(TXTGRNNO, "Job In Done or Lot Locked")
                '    bln = False
                'End If
            End If


            'LOTNO DUPLICATION CHECKING
            If TXTDYEINGNAME.Text.Trim <> "" And TXTLOTNO.Text.Trim <> "" Then
                Dim DT As New DataTable
                If CMBTYPE.Text.Trim = "GRN" Then
                    DT = OBJCMN.SEARCH(" GRN_NO AS GRNNO ", "", " GRN INNER JOIN LEDGERS ON GRN_TOLEDGERID = ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & CMBDYEING.Text.Trim & "' AND GRN_PLOTNO = '" & TXTLOTNO.Text.Trim & "' AND GRN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If Val(DT.Rows(0).Item(0)) <> Val(TXTGRNNO.Text.Trim) Then
                            MsgBox("Lot No Already Exists in Inward No " & DT.Rows(0).Item(0), MsgBoxStyle.Critical)
                            bln = False
                        End If
                    End If

                ElseIf CMBTYPE.Text.Trim = "JOBOUT" Then
                    DT = OBJCMN.SEARCH(" JO_NO AS JONO ", "", " JOBOUT INNER JOIN LEDGERS ON JO_LEDGERID = ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & CMBDYEING.Text.Trim & "' AND JO_LOTNO = '" & TXTLOTNO.Text.Trim & "' AND JO_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If Val(DT.Rows(0).Item(0)) <> Val(TXTGRNNO.Text.Trim) Then
                            MsgBox("Lot No Already Exists in Job Out No " & DT.Rows(0).Item(0), MsgBoxStyle.Critical)
                            bln = False
                        End If
                    End If

                Else
                    DT = OBJCMN.SEARCH(" SM_PARTYCHALLANNO AS GRNNO ", "", " STOCKMASTER INNER JOIN LEDGERS ON SM_LEDGERIDTO = ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & CMBDYEING.Text.Trim & "' AND SM_LOTNO = '" & TXTLOTNO.Text.Trim & "' AND SM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If Val(DT.Rows(0).Item(0)) <> Val(TXTGRNNO.Text.Trim) Then
                            MsgBox("Lot No Already Exists in Inward No " & DT.Rows(0).Item(0), MsgBoxStyle.Critical)
                            bln = False
                        End If
                    End If
                End If
            End If


        End If

        If CMBDYEING.Text.Trim = "" And ClientName <> "AVIS" Then
            EP.SetError(CMBDYEING, "Dyeing Name Cannot be Blank")
            bln = False
        End If

        If TXTLOTNO.Text.Trim = "" And ClientName <> "AVIS" Then
            EP.SetError(TXTLOTNO, "Lot No Cannot be Blank")
            bln = False
        End If


        If RECDATE.Text = "__/__/____" Then
            EP.SetError(RECDATE, " Please Enter Proper Date")
            bln = False
        End If

        Return bln
    End Function

    Private Sub TXTGRNNO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTGRNNO.KeyPress
        If CMBTYPE.Text <> "OPENING" Then numkeypress(e, sender, Me)
    End Sub

    Private Sub UpdateLotNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGRNNO_Validated(sender As Object, e As EventArgs) Handles TXTGRNNO.Validated
        Try
            If TXTGRNNO.Text.Trim.Length > 0 Then

                If CMBTYPE.Text = "GRN" Then

                    'GET DYEING NAME
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(LEDGERS.ACC_CMPNAME,'') AS NAME, ISNULL(GRN.GRN_CHALLANNO,'') AS CHALLANNO, ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME, ISNULL(GRN.GRN_TOTALQTY,0) AS PCS, ISNULL(GRN.GRN_TOTALMTRS,0) AS MTRS, ISNULL(DYEINGLEDGERS.ACC_CMPNAME,'') AS DYEINGNAME, GRN_RECDATE AS RECDATE, ISNULL(GRN.GRN_PLOTNO,'') AS LOTNO", "", "GRN INNER JOIN LEDGERS ON GRN.GRN_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN LEDGERS AS DYEINGLEDGERS ON GRN.GRN_TOLEDGERID = DYEINGLEDGERS.ACC_ID INNER JOIN GRN_DESC ON GRN.GRN_NO = GRN_DESC.GRN_NO AND GRN.GRN_TYPE = GRN_DESC.GRN_GRIDTYPE AND GRN.GRN_YEARID = GRN_DESC.GRN_YEARID LEFT OUTER JOIN ITEMMASTER ON GRN_DESC.GRN_ITEMID = ITEMMASTER.ITEM_ID", " AND GRN_TYPE = 'Job Work' AND GRN.GRN_NO = " & Val(TXTGRNNO.Text.Trim) & " AND GRN.GRN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TXTNAME.Text = DT.Rows(0).Item("NAME")
                        TXTCHALLANNO.Text = DT.Rows(0).Item("CHALLANNO")
                        TXTITEMNAME.Text = DT.Rows(0).Item("ITEMNAME")
                        TXTPCS.Text = Val(DT.Rows(0).Item("PCS"))
                        TXTMTRS.Text = Val(DT.Rows(0).Item("MTRS"))
                        TXTDYEINGNAME.Text = DT.Rows(0).Item("DYEINGNAME")
                        CMBDYEING.Text = DT.Rows(0).Item("DYEINGNAME")
                        TXTLOTNO.Text = DT.Rows(0).Item("LOTNO")
                        RECDATE.Text = Convert.ToDateTime(DT.Rows(0).Item("RECDATE")).Date
                    End If

                ElseIf CMBTYPE.Text.Trim = "JOBOUT" Then

                    'GET JOBBER NAME
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(LEDGERS.ACC_CMPNAME,'') AS NAME, ISNULL(JOBOUT.JO_CHALLANNO,'') AS CHALLANNO, ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME, ISNULL(JOBOUT.JO_TOTALPCS,0) AS PCS, ISNULL(JOBOUT.JO_TOTALMTRS,0) AS MTRS, ISNULL(LEDGERS.ACC_CMPNAME,'') AS DYEINGNAME, JO_DATE AS RECDATE, ISNULL(JOBOUT.JO_LOTNO,'') AS LOTNO ", "", "JOBOUT INNER JOIN LEDGERS ON JOBOUT.JO_LEDGERID = LEDGERS.ACC_ID INNER JOIN JOBOUT_DESC ON JOBOUT.JO_NO = JOBOUT_DESC.JO_NO AND JOBOUT.JO_YEARID = JOBOUT_DESC.JO_YEARID LEFT OUTER JOIN ITEMMASTER ON JOBOUT_DESC.JO_ITEMID = ITEMMASTER.ITEM_ID", " AND JOBOUT.JO_NO = " & Val(TXTGRNNO.Text.Trim) & " AND JOBOUT.JO_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TXTNAME.Text = DT.Rows(0).Item("NAME")
                        TXTCHALLANNO.Text = DT.Rows(0).Item("CHALLANNO")
                        TXTITEMNAME.Text = DT.Rows(0).Item("ITEMNAME")
                        TXTPCS.Text = Val(DT.Rows(0).Item("PCS"))
                        TXTMTRS.Text = Val(DT.Rows(0).Item("MTRS"))
                        TXTDYEINGNAME.Text = DT.Rows(0).Item("DYEINGNAME")
                        CMBDYEING.Text = DT.Rows(0).Item("DYEINGNAME")
                        TXTLOTNO.Text = DT.Rows(0).Item("LOTNO")
                        RECDATE.Text = Convert.ToDateTime(DT.Rows(0).Item("RECDATE")).Date
                    End If

                Else

                    'GET DYEING NAME
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(STOCKMASTER.SM_PARTYCHALLANNO, '') AS CHALLANNO, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, SUM(ISNULL(STOCKMASTER.SM_PCS, 0)) AS PCS, SUM(ISNULL(STOCKMASTER.SM_MTRS, 0)) AS MTRS, ISNULL(DYEINGLEDGERS.Acc_cmpname, '') AS DYEINGNAME, SM_DATE AS RECDATE, ISNULL(STOCKMASTER.SM_LOTNO, '') AS LOTNO ", "", "STOCKMASTER LEFT OUTER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS DYEINGLEDGERS ON STOCKMASTER.SM_LEDGERIDTO = DYEINGLEDGERS.Acc_id LEFT OUTER JOIN ITEMMASTER ON STOCKMASTER.SM_ITEMID = ITEMMASTER.item_id ", " AND STOCKMASTER.SM_TYPE = 'JOBBERSTOCK' AND STOCKMASTER.SM_PARTYCHALLANNO = '" & TXTGRNNO.Text.Trim & "' AND STOCKMASTER.SM_YEARID = " & YearId & " GROUP BY ISNULL(LEDGERS.Acc_cmpname, ''), ISNULL(STOCKMASTER.SM_PARTYCHALLANNO, ''), ISNULL(ITEMMASTER.item_name, ''), ISNULL(DYEINGLEDGERS.Acc_cmpname, ''), SM_DATE, ISNULL(STOCKMASTER.SM_LOTNO, '')")
                    If DT.Rows.Count > 0 Then
                        TXTNAME.Text = DT.Rows(0).Item("NAME")
                        TXTCHALLANNO.Text = DT.Rows(0).Item("CHALLANNO")
                        TXTITEMNAME.Text = DT.Rows(0).Item("ITEMNAME")
                        TXTPCS.Text = Val(DT.Rows(0).Item("PCS"))
                        TXTMTRS.Text = Val(DT.Rows(0).Item("MTRS"))
                        TXTDYEINGNAME.Text = DT.Rows(0).Item("DYEINGNAME")
                        CMBDYEING.Text = DT.Rows(0).Item("DYEINGNAME")
                        TXTLOTNO.Text = DT.Rows(0).Item("LOTNO")
                        RECDATE.Text = Convert.ToDateTime(DT.Rows(0).Item("RECDATE")).Date
                    End If

                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateLotNo_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLNAME(CMBDYEING, False, " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' and ACC_TYPE = 'ACCOUNTS'")
            CLEAR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECDATE_Validating(sender As Object, e As CancelEventArgs) Handles RECDATE.Validating
        Try
            If RECDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(RECDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTLOTNO_Validating(sender As Object, e As CancelEventArgs) Handles TXTLOTNO.Validating
        Try
            If TXTDYEINGNAME.Text.Trim <> "" And TXTLOTNO.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable
                If CMBTYPE.Text.Trim = "GRN" Then
                    DT = OBJCMN.SEARCH(" GRN_NO AS GRNNO ", "", " GRN INNER JOIN LEDGERS ON GRN_TOLEDGERID = ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & CMBDYEING.Text.Trim & "' AND GRN_PLOTNO = '" & TXTLOTNO.Text.Trim & "' AND GRN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If Val(DT.Rows(0).Item(0)) <> Val(TXTGRNNO.Text.Trim) Then
                            MsgBox("Lot No Already Exists in Inward No " & DT.Rows(0).Item(0), MsgBoxStyle.Critical)
                            e.Cancel = True
                            Exit Sub
                        End If
                    End If

                ElseIf CMBTYPE.Text.Trim = "JOBOUT" Then
                    DT = OBJCMN.SEARCH(" JO_NO AS JONO ", "", " JOBOUT INNER JOIN LEDGERS ON JO_LEDGERID = ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & CMBDYEING.Text.Trim & "' AND JO_LOTNO = '" & TXTLOTNO.Text.Trim & "' AND JO_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If Val(DT.Rows(0).Item(0)) <> Val(TXTGRNNO.Text.Trim) Then
                            MsgBox("Lot No Already Exists in Job Out No " & DT.Rows(0).Item(0), MsgBoxStyle.Critical)
                            e.Cancel = True
                            Exit Sub
                        End If
                    End If

                Else
                    DT = OBJCMN.SEARCH(" SM_PARTYCHALLANNO AS GRNNO ", "", " STOCKMASTER INNER JOIN LEDGERS ON SM_LEDGERIDTO = ACC_ID ", " AND LEDGERS.ACC_CMPNAME = '" & CMBDYEING.Text.Trim & "' AND SM_LOTNO = '" & TXTLOTNO.Text.Trim & "' AND SM_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        If Val(DT.Rows(0).Item(0)) <> Val(TXTGRNNO.Text.Trim) Then
                            MsgBox("Lot No Already Exists in Challan No " & DT.Rows(0).Item(0), MsgBoxStyle.Critical)
                            e.Cancel = True
                            Exit Sub
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class