﻿
Imports BL
Imports DevExpress.XtraEditors.Controls

Public Class UpdateProgStatus

    Private Sub UpdateProgStatus_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateProgStatus_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLSTATUS()
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLSTATUS()
        Try
            CMBSTATUS.Items.Clear()
            CMBSTATUS.Items.Add("HOLD FOR GREY")
            CMBSTATUS.Items.Add("SHORT ORDER")
            CMBSTATUS.Items.Add("NOT PRINTABLE")
            CMBSTATUS.Items.Add("CANCEL")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            gridbill.OptionsSelection.CheckBoxSelectorColumnWidth = 30
            gridbill.OptionsSelection.MultiSelect = True
            gridbill.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect

            Dim WHERECLAUSE As String = ""
            If RBPENDING.Checked = True Then
                WHERECLAUSE = " AND  ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_STATUS, '') = '' "
            Else
                WHERECLAUSE = " AND  ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_STATUS, '') <> '' "

            End If

            Dim OBJCMN As New ClsCommonMaster
            'Dim dt As DataTable = OBJCMN.search(" ALLPROGRAMMASTER.PROGRAM_NO AS SRNO, ALLPROGRAMMASTER_DESC.PROGRAM_GRIDSRNO AS GRIDSRNO, ALLPROGRAMMASTER.PROGRAM_DATE AS DATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PROGISSDATE, '') AS PROGISSDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(DESIGNMASTER.DESIGN_CADNO,'') AS CADNO, ISNULL(ITEMMASTER.ITEM_GREYWIDTH,'') AS GREYWIDTH, ISNULL(ITEMMASTER.ITEM_WIDTH,'') AS WIDTH,  ISNULL(COLORMASTER.COLOR_name,'') AS COLOR, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS,0) AS MTRS, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_LOTNO,0) AS LOTNO,  ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,'') AS PROGISSDATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_STATUS,'') AS STATUS, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PRODCUTTING,'') AS PRODCUTTING,  ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING,'') AS FINISHCUTTING, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_INWARDDATE,'') AS INWARDDATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_RECDPCS,0) AS RECDPCS ", "", " ALLPROGRAMMASTER INNER JOIN ALLPROGRAMMASTER_DESC ON ALLPROGRAMMASTER.PROGRAM_NO = ALLPROGRAMMASTER_DESC.PROGRAM_NO AND  ALLPROGRAMMASTER.PROGRAM_YEARID = ALLPROGRAMMASTER_DESC.PROGRAM_YEARID INNER JOIN ITEMMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS ON ALLPROGRAMMASTER.PROGRAM_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN COLORMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_DESIGNID = DESIGNMASTER.DESIGN_id  ", WHERECLAUSE & " AND ISNULL(PROGRAM_LEDGERID,0) <> 0 AND ALLPROGRAMMASTER.PROGRAM_YEARID = " & YearId & " ORDER BY ALLPROGRAMMASTER.PROGRAM_NO, ALLPROGRAMMASTER_DESC.PROGRAM_GRIDSRNO")
            Dim dt As DataTable = OBJCMN.search(" ALLPROGRAMMASTER.PROGRAM_NO AS SRNO, ALLPROGRAMMASTER_DESC.PROGRAM_GRIDSRNO AS GRIDSRNO, ALLPROGRAMMASTER.PROGRAM_DATE AS DATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PROGISSDATE, '') AS PROGISSDATE, ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDRECDATE,'') AS CARDRECDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_LOTNO, '') AS LOTNO, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGNNO, ISNULL(DESIGNMASTER.DESIGN_CADNO, '') AS CADNO, ISNULL(ITEMMASTER.ITEM_GREYWIDTH, '') AS GREYWIDTH, ISNULL(ITEMMASTER.ITEM_WIDTH, '') AS WIDTH, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS, 0) AS MTRS, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_LOTNO, 0) AS LOTNO, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PROGISSDATE, '') AS PROGISSDATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_STATUS, '') AS STATUS, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PRODCUTTING, '') AS PRODCUTTING, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING, '') AS FINISHCUTTING, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_INWARDDATE, '') AS INWARDDATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_RECDPCS, 0) AS RECDPCS, (ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS, 0)-ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_RECDPCS, 0)) AS BALMTRS, (CASE WHEN ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS, 0) > 0 THEN ((ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS, 0)-ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_RECDPCS, 0))/ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS, 0))*100  ELSE  0 END) AS BALPER, ALLPROGRAMMASTER.PROGRAM_TYPE AS TYPE, ISNULL(LEDGERS.Acc_cmpname, '') AS DYEINGNAME ", "", "  ALLPROGRAMMASTER INNER JOIN ALLPROGRAMMASTER_DESC ON ALLPROGRAMMASTER.PROGRAM_NO = ALLPROGRAMMASTER_DESC.PROGRAM_NO AND ALLPROGRAMMASTER.PROGRAM_TYPE = ALLPROGRAMMASTER_DESC.PROGRAM_TYPE AND  ALLPROGRAMMASTER.PROGRAM_YEARID = ALLPROGRAMMASTER_DESC.PROGRAM_YEARID INNER JOIN ITEMMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS ON ALLPROGRAMMASTER.PROGRAM_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN COLORMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_DESIGNID = DESIGNMASTER.DESIGN_id ", WHERECLAUSE & " AND (ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS, 0)-ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_RECDPCS, 0)) > 0  AND ALLPROGRAMMASTER.PROGRAM_YEARID = " & YearId & " ORDER BY ALLPROGRAMMASTER.PROGRAM_NO, ALLPROGRAMMASTER_DESC.PROGRAM_GRIDSRNO")
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
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            For I As Integer = 0 To Val(gridbill.RowCount - 1)
                Dim DTROW As DataRow = gridbill.GetDataRow(I)
                If IsDBNull(DTROW("STATUS")) = False AndAlso DTROW("STATUS") <> "" Then
                    If DTROW("TYPE") = "PROGRAM" Then
                        DT = OBJCMN.Execute_Any_String("UPDATE PROGRAMMASTER_DESC SET PROGRAM_STATUS = '" & DTROW("STATUS") & "' WHERE PROGRAM_NO = " & Val(DTROW("SRNO")) & " AND PROGRAM_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND PROGRAM_YEARID = " & YearId, "", "")
                    Else
                        DT = OBJCMN.Execute_Any_String("UPDATE OPENINGPROGRAMMASTER_DESC SET OPPROGRAM_STATUS = '" & DTROW("STATUS") & "' WHERE OPPROGRAM_NO = " & Val(DTROW("SRNO")) & " AND OPPROGRAM_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND OPPROGRAM_YEARID = " & YearId, "", "")
                    End If
                End If
            Next

            fillgrid()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles gridbill.InvalidRowException
        e.ExceptionMode = ExceptionMode.NoAction
    End Sub

    Private Sub gridbill_ValidateRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles gridbill.ValidateRow
        Try
            'If IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "NAME")) = False Then If MsgBox("Save Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Call CMDOK_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try
            If RBENTERED.Checked = True Then
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable

                If MsgBox("Delete Data?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
                For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                    Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                    Dim DYEINGID As Integer = 0
                    If DTROW("TYPE") = "PROGRAM" Then
                        DT = OBJCMN.Execute_Any_String("UPDATE PROGRAMMASTER_DESC SET PROGRAM_STATUS = '' WHERE PROGRAM_NO = " & Val(DTROW("SRNO")) & " AND PROGRAM_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND PROGRAM_YEARID = " & YearId, "", "")
                    Else
                        DT = OBJCMN.Execute_Any_String("UPDATE OPENINGPROGRAMMASTER_DESC SET OPPROGRAM_STATUS = '' WHERE OPPROGRAM_NO = " & Val(DTROW("SRNO")) & " AND OPPROGRAM_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND OPPROGRAM_YEARID = " & YearId, "", "")
                    End If
                Next

                fillgrid()
                gridbill.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbilldetails.KeyDown
        Try
            If e.KeyCode = Keys.F12 And gridbill.FocusedRowHandle - 1 >= 0 Then
                'COPY DATA FROM UPPER LINE
                Dim DTROWUP As DataRow = gridbill.GetDataRow(gridbill.FocusedRowHandle - 1)
                Dim DTROW As DataRow = gridbill.GetFocusedDataRow
                DTROW("STATUS") = DTROWUP("STATUS")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDAPPLY_Click(sender As Object, e As EventArgs) Handles CMDAPPLY.Click
        Try
            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))
                DTROW("STATUS") = CMBMAINSTATUS.Text.Trim
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class