
Imports BL

Public Class SelectPurLRStock

    Public ITEMNAME As String = ""
    Public DT As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectPurLRStock_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectPurLRStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim WHERE As String = ""
            If ITEMNAME <> "" Then
                WHERE = WHERE & " AND ITEMNAME = '" & ITEMNAME & "'"
            End If

            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search("*", "", " PURCHASELRSTOCK ", WHERE & " AND PURCHASELRSTOCK.YEARID=" & YearId & " AND PURCHASELRSTOCK.SOLD = 0 ORDER BY TYPE, BILLNO")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
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
            Dim TEMPITEMNAME As String = ""
            Dim TEMPALLOW As Boolean = False
            'WE NEED TO INTIMATE IF WE HAVE SELECTED ORDER OF DIFF SHIPTO PARTY

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    If ClientName = "SIDDHGIRI" Then dtrow("GRIDPARTYPONO") = ""
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

                    If ClientName = "ABHEE" Then
                        If TEMPITEMNAME = "" And dtrow("ITEMNAME") <> "" Then
                            TEMPITEMNAME = dtrow("ITEMNAME")
                        ElseIf TEMPITEMNAME <> "" And dtrow("ITEMNAME") <> "" And TEMPITEMNAME <> dtrow("ITEMNAME") Then
                            MsgBox("You have selected Different Itemname", MsgBoxStyle.Critical)
                            DT.Rows.Clear()
                            Exit For
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

    Private Sub CHKSELECT_CheckedChanged(sender As Object, e As EventArgs) Handles CHKSELECT.CheckedChanged
        Try
            If gridbilldetails.Visible = True Then
                For i As Integer = 0 To gridbill.RowCount - 1
                    Dim dtrow As DataRow = gridbill.GetDataRow(i)
                    dtrow("CHK") = CHKSELECT.Checked
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class