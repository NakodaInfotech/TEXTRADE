﻿
Imports BL
Imports DevExpress.XtraEditors.Controls

Public Class UpdateProgCardRecdDate

    Private Sub UpdateProgCardRecdDate_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateProgCardRecdDate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim WHERECLAUSE As String = ""
            If RBPENDING.Checked = True Then
                WHERECLAUSE = " AND (ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDRECDATE,'') = '' OR ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDRECDATE,'' ) = '')"
            Else
                WHERECLAUSE = " AND ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDRECDATE,'') <> '' AND ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDRECDATE,'') <> '__/__/____'"
            End If

            Dim OBJCMN As New ClsCommonMaster
            Dim dt As DataTable = OBJCMN.search(" ALLPROGRAMMASTER.PROGRAM_NO AS SRNO, ALLPROGRAMMASTER.PROGRAM_DATE AS DATE, ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDRECDATE, '') AS CARDRECDATE, ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDISSUEDATE,'') AS CARDISSUEDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(DESIGNMASTER.DESIGN_CADNO,'') AS CADNO, ISNULL(ITEMMASTER.ITEM_GREYWIDTH,'') AS GREYWIDTH, ISNULL(ITEMMASTER.ITEM_WIDTH,'') AS WIDTH,  ISNULL(COLORMASTER.COLOR_name,'') AS COLOR, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS,0) AS MTRS, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_LOTNO,0) AS LOTNO,  ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,'') AS PROGISSDATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_STATUS,'') AS STATUS, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PRODCUTTING,'') AS PRODCUTTING,  ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING,'') AS FINISHCUTTING, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_INWARDDATE,'') AS INWARDDATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_RECDPCS,0) AS RECDPCS, ALLPROGRAMMASTER.PROGRAM_TYPE AS TYPE ", "", " ALLPROGRAMMASTER INNER JOIN ALLPROGRAMMASTER_DESC ON ALLPROGRAMMASTER.PROGRAM_NO = ALLPROGRAMMASTER_DESC.PROGRAM_NO AND ALLPROGRAMMASTER.PROGRAM_TYPE = ALLPROGRAMMASTER_DESC.PROGRAM_TYPE AND  ALLPROGRAMMASTER.PROGRAM_YEARID = ALLPROGRAMMASTER_DESC.PROGRAM_YEARID INNER JOIN ITEMMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS ON ALLPROGRAMMASTER.PROGRAM_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN COLORMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_DESIGNID = DESIGNMASTER.DESIGN_id  ", WHERECLAUSE & " AND ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDISSUEDATE,'') <> '' AND ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDISSUEDATE,'') <> '__/__/____' AND ALLPROGRAMMASTER_DESC.PROGRAM_STATUS <> 'CANCEL' AND ALLPROGRAMMASTER.PROGRAM_YEARID = " & YearId & " ORDER BY ALLPROGRAMMASTER.PROGRAM_NO, ALLPROGRAMMASTER_DESC.PROGRAM_GRIDSRNO  ")
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

            Dim SELECTEDROWS As Int32() = gridbill.GetSelectedRows()
            For I As Integer = 0 To Val(SELECTEDROWS.Length - 1)
                Dim DTROW As DataRow = gridbill.GetDataRow(SELECTEDROWS(I))

                If IsDBNull(DTROW("CARDRECDATE")) = True Then DTROW("CARDRECDATE") = Format(Now.Date, "dd/MM/yyyy")
                If DTROW("CARDRECDATE") = "" Then DTROW("CARDRECDATE") = Format(Now.Date, "dd/MM/yyyy")

                If IsDBNull(DTROW("CARDRECDATE")) = False AndAlso DTROW("CARDRECDATE") <> "" Then
                    If DTROW("TYPE") = "PROGRAM" Then
                        DT = OBJCMN.Execute_Any_String("UPDATE PROGRAMMASTER SET PROGRAM_CARDRECDATE = '" & DTROW("CARDRECDATE") & "' WHERE PROGRAM_NO = " & Val(DTROW("SRNO")) & " AND PROGRAM_YEARID = " & YearId, "", "")
                    Else
                        DT = OBJCMN.Execute_Any_String("UPDATE OPENINGPROGRAMMASTER SET OPPROGRAM_CARDRECDATE = '" & DTROW("CARDRECDATE") & "' WHERE OPPROGRAM_NO = " & Val(DTROW("SRNO")) & " AND OPPROGRAM_YEARID = " & YearId, "", "")
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
            If IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "CARDRECDATE")) = False Then

                If gridbill.GetRowCellValue(e.RowHandle, "CARDRECDATE") <> "" Then

                    'CHECK THAT DATE SHOULD BE 10 CHARS
                    If Len(gridbill.GetRowCellValue(e.RowHandle, "CARDRECDATE")) <> 10 Then
                        e.Valid = False
                        gridbill.SetColumnError(GCARDRECDATE, "Invalid Date")
                        Exit Sub
                    End If

                    'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                    Dim TEMP As DateTime
                    If Not DateTime.TryParse(gridbill.GetRowCellValue(e.RowHandle, "CARDRECDATE"), TEMP) Then
                        e.Valid = False
                        gridbill.SetColumnError(GCARDRECDATE, "Invalid Date")
                        Exit Sub
                    Else
                        If (gridbill.GetRowCellValue(e.RowHandle, "CARDRECDATE") < Convert.ToDateTime(gridbill.GetRowCellValue(e.RowHandle, "DATE")).Date) Or (gridbill.GetRowCellValue(e.RowHandle, "CARDRECDATE") < Convert.ToDateTime(gridbill.GetRowCellValue(e.RowHandle, "CARDISSUEDATE")).Date) Then
                            e.Valid = False
                            gridbill.SetColumnError(GCARDRECDATE, "Date must be After Card Date & Card Issue Date")
                            Exit Sub
                        End If
                    End If
                End If

            End If
            'WE WILL SAVE ON BUTTON CLICK
            'If IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "CARDRECDATE")) = False And gridbill.GetRowCellValue(e.RowHandle, "CARDRECDATE") <> "" Then If MsgBox("Save Entry?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Call CMDOK_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try

            Dim ROW As DataRow = gridbill.GetFocusedDataRow
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If IsDBNull(ROW("CARDRECDATE")) = True Then
                MsgBox("No Row To Delete")
                Exit Sub
            End If

            If MsgBox("Delete Data?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            If ROW("TYPE") = "PROGRAM" Then
                DT = OBJCMN.Execute_Any_String("UPDATE PROGRAMMASTER SET PROGRAM_CARDRECDATE = '' WHERE PROGRAM_NO = " & Val(ROW("SRNO")) & " AND PROGRAM_YEARID = " & YearId, "", "")
            Else
                DT = OBJCMN.Execute_Any_String("UPDATE OPENINGPROGRAMMASTER SET OPPROGRAM_CARDRECDATE = '' WHERE OPPROGRAM_NO = " & Val(ROW("SRNO")) & " AND OPPROGRAM_YEARID = " & YearId, "", "")
            End If
            fillgrid()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbilldetails.KeyDown
        Try
            If e.KeyCode = Keys.F12 AndAlso gridbill.FocusedRowHandle - 1 >= 0 Then
                'COPY DATA FROM UPPER LINE

                Dim DTROWUP As DataRow = gridbill.GetDataRow(gridbill.FocusedRowHandle - 1)
                Dim DTROW As DataRow = gridbill.GetFocusedDataRow
                DTROW("CARDRECDATE") = DTROWUP("CARDRECDATE")

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RBENTERED_CheckedChanged(sender As Object, e As EventArgs) Handles RBENTERED.CheckedChanged
        fillgrid()
    End Sub

    'Sub FILLGRID()
    '    Try
    '        Dim OBJ As New ClsCommonMaster
    '        Dim DT As New DataTable
    '        If RBPENDING.Checked = True Then
    '            DT = OBJ.search("  ", "  ", "  ", "  ")
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class