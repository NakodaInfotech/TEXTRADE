Imports BL

Public Class SelectAgencySO


    Public PARTYNAME As String = ""
    Public FRMSTRING As String = ""
    Public DT As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectAgencySO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectAgencySO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid("")
    End Sub

    Sub fillgrid(ByVal WHERE As String)
        Try
            Dim OPWHERE As String = ""
            Cursor.Current = Cursors.WaitCursor

            If PARTYNAME <> "" Then
                WHERE = WHERE & " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & PARTYNAME & "'"
                OPWHERE = OPWHERE & " AND ISNULL(LEDGERS.ACC_CMPNAME,'') = '" & PARTYNAME & "'"
            End If
            If FRMSTRING = "" Then
                If SALEORDERONMTRS = False Then WHERE = WHERE & " and (AGENCYSALEORDER_DESC.ASO_QTY - AGENCYSALEORDER_DESC.ASO_RECDQTY) > 0 AND AGENCYSALEORDER_DESC.ASO_CLOSED=0 " Else WHERE = WHERE & " and (AGENCYSALEORDER_DESC.ASO_MTRS - AGENCYSALEORDER_DESC.ASO_RECDMTRS) > 0 AND AGENCYSALEORDER_DESC.ASO_CLOSED=0 "
                If SALEORDERONMTRS = False Then OPWHERE = OPWHERE & " and (OPENINGAGENCYSALEORDER_DESC.OPASO_QTY - OPENINGAGENCYSALEORDER_DESC.OPASO_RECDQTY) > 0 AND OPENINGAGENCYSALEORDER_DESC.OPASO_CLOSED=0 " Else OPWHERE = OPWHERE & " and (OPENINGAGENCYSALEORDER_DESC.OPASO_MTRS - OPENINGSALEORDER_DESC.OPASO_RECDMTRS) > 0 AND OPENINGSALEORDER_DESC.OPASO_CLOSED=0 "
            End If

            Dim objclspreq As New ClsCommon()
            'THIS IS CODE WITHOUT YARNSALE ORDER AND YARNOPENIUNG SALE ORDER
            Dim DT As DataTable = objclspreq.SEARCH("CAST (0 AS BIT) AS CHK,*", "", " (SELECT AGENCYSALEORDER.ASO_no AS SONO, AGENCYSALEORDER.ASO_date AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGER.Acc_cmpname, '') AS AGENTNAME, AGENCYSALEORDER.ASO_pono AS [PONO], ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], (AGENCYSALEORDER_DESC.ASO_QTY - AGENCYSALEORDER_DESC.ASO_RECDQTY) AS [QTY], (AGENCYSALEORDER_DESC.ASO_MTRS - AGENCYSALEORDER_DESC.ASO_RECDMTRS) AS [MTRS], AGENCYSALEORDER.ASO_remarks AS REMARKS, AGENCYSALEORDER_DESC.ASO_GRIDSRNO AS GRIDSRNO, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(DESIGN_NO,'') AS DESIGNNO, ISNULL(COLOR_NAME,'') AS COLOR, ISNULL(TRANSLEDGERS.ACC_CMPNAME,'') AS TRANSNAME, ISNULL(AGENCYSALEORDER.ASO_REFNO,'') AS REFNO, AGENCYSALEORDER_DESC.ASO_RATE AS RATE, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME, 'AGENCYSALEORDER' AS TYPE, ISNULL(AGENCYSALEORDER_DESC.ASO_PARTYPONO,'') AS GRIDPARTYPONO, ISNULL(PACKINGTYPEMASTER.PACKINGTYPE_NAME,'') AS PACKINGTYPE, ISNULL(AGENCYSALEORDER_DESC.ASO_GRIDREMARKS, '') AS GRIDDESC FROM AGENCYSALEORDER INNER JOIN LEDGERS ON AGENCYSALEORDER.ASO_ledgerid = LEDGERS.Acc_id INNER JOIN AGENCYSALEORDER_DESC ON AGENCYSALEORDER.ASO_no = AGENCYSALEORDER_DESC.ASO_NO AND AGENCYSALEORDER.ASO_YEARID = AGENCYSALEORDER_DESC.ASO_YEARID LEFT OUTER JOIN PACKINGTYPEMASTER ON AGENCYSALEORDER.ASO_PACKINGTYPEID = PACKINGTYPEMASTER.PACKINGTYPE_ID LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON AGENCYSALEORDER.ASO_PACKINGID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN ITEMMASTER ON AGENCYSALEORDER_DESC.ASO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGER ON AGENCYSALEORDER.ASO_Agentid = AGENTLEDGER.Acc_id LEFT OUTER JOIN ADDRESSMASTER ON AGENCYSALEORDER.ASO_HASTEID = ADDRESSMASTER.ADDRESS_ID LEFT OUTER JOIN DESIGNMASTER ON AGENCYSALEORDER_DESC.ASO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON AGENCYSALEORDER_DESC.ASO_COLORID = COLOR_ID LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON AGENCYSALEORDER.ASO_TRANSID = TRANSLEDGERS.ACC_ID LEFT OUTER JOIN CITYMASTER ON AGENCYSALEORDER.ASO_CITYID = CITYMASTER.CITY_ID WHERE  AGENCYSALEORDER.ASO_VERIFIED=1 AND  AGENCYSALEORDER.ASO_YEARID = " & YearId & WHERE & " UNION ALL SELECT OPENINGAGENCYSALEORDER.OPASO_no AS SONO, OPENINGAGENCYSALEORDER.OPASO_date AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(AGENTLEDGER.Acc_cmpname, '') AS AGENTNAME, OPENINGAGENCYSALEORDER.OPASO_pono AS [PONO], ISNULL(ITEMMASTER.item_name,'') AS [ITEMNAME], (OPENINGAGENCYSALEORDER_DESC.OPASO_QTY - OPENINGAGENCYSALEORDER_DESC.OPASO_RECDQTY) AS [QTY], (OPENINGAGENCYSALEORDER_DESC.OPASO_MTRS - OPENINGAGENCYSALEORDER_DESC.OPASO_RECDMTRS) AS [MTRS], OPENINGAGENCYSALEORDER.OPASO_remarks AS REMARKS, OPENINGAGENCYSALEORDER_DESC.OPASO_GRIDSRNO AS GRIDSRNO, ISNULL(PACKINGLEDGERS.Acc_cmpname, '') AS DELIVERYAT, ISNULL(DESIGN_NO,'') AS DESIGNNO, ISNULL(COLOR_NAME,'') AS COLOR, ISNULL(TRANSLEDGERS.ACC_CMPNAME,'') AS TRANSNAME, ISNULL(OPENINGAGENCYSALEORDER.OPASO_REFNO,'') AS REFNO, OPENINGAGENCYSALEORDER_DESC.OPASO_RATE AS RATE, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME, 'OPENING' AS TYPE, ISNULL(OPENINGAGENCYSALEORDER_DESC.OPASO_PARTYPONO,'') AS GRIDPARTYPONO, ISNULL(PACKINGTYPEMASTER.PACKINGTYPE_NAME,'') AS PACKINGTYPE , ISNULL(OPENINGAGENCYSALEORDER_DESC.OPASO_GRIDREMARKS, '') AS GRIDDESC FROM OPENINGAGENCYSALEORDER INNER JOIN LEDGERS ON OPENINGAGENCYSALEORDER.OPASO_ledgerid = LEDGERS.Acc_id INNER JOIN OPENINGAGENCYSALEORDER_DESC ON OPENINGAGENCYSALEORDER.OPASO_no = OPENINGAGENCYSALEORDER_DESC.OPASO_NO AND OPENINGAGENCYSALEORDER.OPASO_YEARID = OPENINGAGENCYSALEORDER_DESC.OPASO_YEARID LEFT OUTER JOIN PACKINGTYPEMASTER ON OPENINGAGENCYSALEORDER.OPASO_PACKINGTYPEID = PACKINGTYPEMASTER.PACKINGTYPE_ID LEFT OUTER JOIN LEDGERS AS PACKINGLEDGERS ON OPENINGAGENCYSALEORDER.OPASO_PACKINGID = PACKINGLEDGERS.Acc_id LEFT OUTER JOIN ITEMMASTER ON OPENINGAGENCYSALEORDER_DESC.OPASO_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGER ON OPENINGAGENCYSALEORDER.OPASO_Agentid = AGENTLEDGER.Acc_id LEFT OUTER JOIN ADDRESSMASTER ON OPENINGAGENCYSALEORDER.OPASO_HASTEID = ADDRESSMASTER.ADDRESS_ID LEFT OUTER JOIN DESIGNMASTER ON OPENINGAGENCYSALEORDER_DESC.OPASO_DESIGNID = DESIGN_ID LEFT OUTER JOIN COLORMASTER ON OPENINGAGENCYSALEORDER_DESC.OPASO_COLORID = COLOR_ID LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON OPENINGAGENCYSALEORDER.OPASO_TRANSID = TRANSLEDGERS.ACC_ID LEFT OUTER JOIN CITYMASTER ON OPENINGAGENCYSALEORDER.OPASO_CITYID = CITYMASTER.CITY_ID WHERE OPENINGAGENCYSALEORDER.OPASO_VERIFIED=1 AND OPENINGAGENCYSALEORDER.OPASO_YEARID = " & YearId & OPWHERE & ") AS T", " ORDER BY T.DATE, T.SONO, T.GRIDSRNO")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            DT.Columns.Add("DATE")
            DT.Columns.Add("NAME")
            DT.Columns.Add("ITEMNAME")
            DT.Columns.Add("DESIGN")
            DT.Columns.Add("COLOR")
            DT.Columns.Add("QTY")
            DT.Columns.Add("MTRS")
            DT.Columns.Add("PONO")
            DT.Columns.Add("AGENTNAME")
            DT.Columns.Add("TRANSNAME")
            DT.Columns.Add("CITYNAME")
            DT.Columns.Add("DELIVERYAT")
            DT.Columns.Add("REFNO")
            DT.Columns.Add("RATE")
            DT.Columns.Add("SONO")
            DT.Columns.Add("GRIDSRNO")
            DT.Columns.Add("TYPE")
            DT.Columns.Add("GRIDPARTYPONO")
            DT.Columns.Add("PACKINGTYPE")
            DT.Columns.Add("REMARKS")
            DT.Columns.Add("GRIDDESC")


            Dim TEMPDISPATCHTO As String = ""
            Dim TEMPPARTYPONO As String = ""
            Dim TEMPALLOW As Boolean = False
            'WE NEED TO INTIMATE IF WE HAVE SELECTED ORDER OF DIFF SHIPTO PARTY

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If TEMPDISPATCHTO = "" And dtrow("DELIVERYAT") <> "" Then
                        TEMPDISPATCHTO = dtrow("DELIVERYAT")
                    ElseIf TEMPDISPATCHTO <> "" And dtrow("DELIVERYAT") <> "" And TEMPDISPATCHTO <> dtrow("DELIVERYAT") Then
                        If TEMPALLOW = False Then
                            If MsgBox("You have selected Orders of Different Ship To Party, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                DT.Rows.Clear()
                                Exit For
                            Else
                                TEMPALLOW = True
                            End If
                        End If
                    End If


                    If TEMPPARTYPONO = "" And dtrow("PONO") <> "" Then
                        TEMPPARTYPONO = dtrow("PONO")
                    ElseIf TEMPPARTYPONO <> "" And dtrow("PONO") <> "" And TEMPPARTYPONO <> dtrow("PONO") Then
                        If TEMPALLOW = False Then
                            If MsgBox("You have selected Orders of Different Party PO No, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                DT.Rows.Clear()
                                Exit For
                            Else
                                TEMPALLOW = True
                            End If
                        End If
                    End If

                    DT.Rows.Add(dtrow("DATE"), dtrow("NAME"), dtrow("ITEMNAME"), dtrow("DESIGNNO"), dtrow("COLOR"), Val(dtrow("QTY")), Val(dtrow("MTRS")), dtrow("PONO"), dtrow("AGENTNAME"), dtrow("TRANSNAME"), dtrow("CITYNAME"), dtrow("DELIVERYAT"), dtrow("REFNO"), Val(dtrow("RATE")), Val(dtrow("SONO")), Val(dtrow("GRIDSRNO")), dtrow("TYPE"), dtrow("GRIDPARTYPONO"), dtrow("PACKINGTYPE"), dtrow("REMARKS"), dtrow("GRIDDESC"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CHKSELECTALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSELECTALL.CheckedChanged
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

    Private Sub SelectAgencySO_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            If ClientName = "MASHOK" Or ClientName = "ABHEE" Then
                GDESIGNNO.Visible = False
                GCOLOR.Visible = False
                GREFNO.Visible = False
                GGRIDDESC.VisibleIndex = GITEM.VisibleIndex + 1
                GRATE.VisibleIndex = GGRIDDESC.VisibleIndex + 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class


