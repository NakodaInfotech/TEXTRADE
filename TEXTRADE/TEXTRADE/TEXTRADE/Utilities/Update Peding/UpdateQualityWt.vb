
Imports BL

Public Class UpdateQualityWt

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
            TXTCHALLANNO.Clear()
            TXTITEMNAME.Clear()
            TXTPCS.Clear()
            TXTMTRS.Clear()
            TXTDYEINGNAME.Clear()
            TXTLOTNO.Clear()
            TXTQUALITYWT.Clear()
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
            Dim DT As New DataTable
            If CMBTYPE.Text = "GRN" Then
                DT = OBJCMN.Execute_Any_String(" UPDATE GRN SET GRN.GRN_BALEWT = " & Val(TXTQUALITYWT.Text.Trim) & " WHERE GRN.GRN_NO = " & Val(TXTGRNNO.Text.Trim) & " And GRN.GRN_TYPE = 'Job Work' AND GRN.GRN_YEARID = " & YearId, "", "")
                DT = OBJCMN.Execute_Any_String(" UPDATE CHECKINGMASTER SET CHECKINGMASTER.CHECK_QUALITYWT = " & Val(TXTQUALITYWT.Text.Trim) & " WHERE CHECKINGMASTER.CHECK_GRNNO = " & Val(TXTGRNNO.Text.Trim) & " And CHECKINGMASTER.CHECK_TYPE = 'Job Work' AND CHECKINGMASTER.CHECK_YEARID = " & YearId, "", "")
            End If
            MsgBox("Quality Wt Updated Successfully")
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

        If Val(TXTQUALITYWT.Text.Trim) = 0 Then
            EP.SetError(TXTQUALITYWT, "Enter Quality Wt")
            bln = False
        End If

        Return bln
    End Function

    Private Sub TXTGRNNO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTGRNNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub UpdateQualityWt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                    Dim DT As DataTable = OBJCMN.SEARCH(" TOP 1 ISNULL(LEDGERS.ACC_CMPNAME,'') AS NAME, ISNULL(GRN.GRN_CHALLANNO,'') AS CHALLANNO, ISNULL(ITEMMASTER.ITEM_NAME,'') AS ITEMNAME, ISNULL(GRN.GRN_TOTALQTY,0) AS PCS, ISNULL(GRN.GRN_TOTALMTRS,0) AS MTRS, ISNULL(DYEINGLEDGERS.ACC_CMPNAME,'') AS DYEINGNAME, GRN_RECDATE AS RECDATE, ISNULL(GRN.GRN_PLOTNO,'') AS LOTNO, ISNULL(GRN.GRN_BALEWT,0) AS QUALITYWT", "", "GRN INNER JOIN LEDGERS ON GRN.GRN_LEDGERID = LEDGERS.ACC_ID LEFT OUTER JOIN LEDGERS AS DYEINGLEDGERS ON GRN.GRN_TOLEDGERID = DYEINGLEDGERS.ACC_ID INNER JOIN GRN_DESC ON GRN.GRN_NO = GRN_DESC.GRN_NO AND GRN.GRN_TYPE = GRN_DESC.GRN_GRIDTYPE AND GRN.GRN_YEARID = GRN_DESC.GRN_YEARID LEFT OUTER JOIN ITEMMASTER ON GRN_DESC.GRN_ITEMID = ITEMMASTER.ITEM_ID", " AND GRN_TYPE = 'Job Work' AND GRN.GRN_NO = " & Val(TXTGRNNO.Text.Trim) & " AND GRN.GRN_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then
                        TXTNAME.Text = DT.Rows(0).Item("NAME")
                        TXTCHALLANNO.Text = DT.Rows(0).Item("CHALLANNO")
                        TXTITEMNAME.Text = DT.Rows(0).Item("ITEMNAME")
                        TXTPCS.Text = Val(DT.Rows(0).Item("PCS"))
                        TXTMTRS.Text = Val(DT.Rows(0).Item("MTRS"))
                        TXTDYEINGNAME.Text = DT.Rows(0).Item("DYEINGNAME")
                        TXTLOTNO.Text = DT.Rows(0).Item("LOTNO")
                        TXTQUALITYWT.Text = Val(DT.Rows(0).Item("QUALITYWT"))
                    End If

                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateQualityWt_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            CLEAR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQUALITYWT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTQUALITYWT.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub
End Class