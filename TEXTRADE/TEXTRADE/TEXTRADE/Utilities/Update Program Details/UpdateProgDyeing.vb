
Imports System.ComponentModel
Imports BL
Imports DevExpress.XtraEditors.Controls

Public Class UpdateProgDyeing

    Private Sub UpdateProgDyeing_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateProgDyeing_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLDYEING()
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLDYEING()
        Try
            CMBDYEINGNAME.Items.Clear()
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("ACC_CMPNAME AS NAME", "", " LEDGERS INNER JOIN GROUPMASTER ON GROUP_ID = ACC_GROUPID  ", " AND GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS' AND ACC_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                For Each DTROW As DataRow In DT.Rows
                    CMBDYEINGNAME.Items.Add(DTROW("NAME"))
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim WHERECLAUSE As String = ""
            'If RBPENDING.Checked = True Then
            '    WHERECLAUSE = " AND (ISNULL(LEDGERS.ACC_CMPNAME,'') = '' OR ISNULL(LEDGERS.ACC_CMPNAME,'') = '')  AND (ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,'') = '' OR ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,'') = '__/__/____')   "
            'Else
            '    WHERECLAUSE = " AND (ISNULL(LEDGERS.ACC_CMPNAME,'') <> '' OR ISNULL(LEDGERS.ACC_CMPNAME,'') <> '')  AND (ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,'') <> '' AND ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,'') <> '__/__/____') "
            'End If

            If RBPENDING.Checked = True Then
                WHERECLAUSE = " AND (ISNULL(LEDGERS.ACC_CMPNAME,'') = '' OR ISNULL(LEDGERS.ACC_CMPNAME,'') = '')  "
            Else
                WHERECLAUSE = " AND (ISNULL(LEDGERS.ACC_CMPNAME,'') <> '' OR ISNULL(LEDGERS.ACC_CMPNAME,'') <> '')  "
            End If

            Dim OBJCMN As New ClsCommonMaster
            'Dim dt As DataTable = OBJCMN.search(" ALLPROGRAMMASTER.PROGRAM_NO AS SRNO, ALLPROGRAMMASTER.PROGRAM_DATE AS DATE, ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDRECDATE, '') AS CARDRECDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(DESIGNMASTER.DESIGN_CADNO,'') AS CADNO, ISNULL(ITEMMASTER.ITEM_GREYWIDTH,'') AS GREYWIDTH, ISNULL(ITEMMASTER.ITEM_WIDTH,'') AS WIDTH,  ISNULL(COLORMASTER.COLOR_name,'') AS COLOR, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS,0) AS MTRS, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_LOTNO,0) AS LOTNO,  ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,'') AS PROGISSDATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_STATUS,'') AS STATUS, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PRODCUTTING,'') AS PRODCUTTING,  ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING,'') AS FINISHCUTTING, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_INWARDDATE,'') AS INWARDDATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_RECDPCS,0) AS RECDPCS ", "", " ALLPROGRAMMASTER INNER JOIN ALLPROGRAMMASTER_DESC ON ALLPROGRAMMASTER.PROGRAM_NO = ALLPROGRAMMASTER_DESC.PROGRAM_NO AND  ALLPROGRAMMASTER.PROGRAM_YEARID = ALLPROGRAMMASTER_DESC.PROGRAM_YEARID INNER JOIN ITEMMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS ON ALLPROGRAMMASTER.PROGRAM_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN COLORMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_DESIGNID = DESIGNMASTER.DESIGN_id  ", WHERECLAUSE & " AND ALLPROGRAMMASTER.PROGRAM_YEARID = " & YearId & " ORDER BY ALLPROGRAMMASTER.PROGRAM_NO, ALLPROGRAMMASTER_DESC.PROGRAM_GRIDSRNO")
            Dim DT As DataTable = OBJCMN.search(" ALLPROGRAMMASTER.PROGRAM_NO AS SRNO, ALLPROGRAMMASTER_DESC.PROGRAM_GRIDSRNO AS GRIDSRNO, ALLPROGRAMMASTER.PROGRAM_DATE AS DATE, ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDRECDATE, '') AS CARDRECDATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ITEMMASTER.item_name AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_NO,'') AS DESIGNNO, ISNULL(DESIGNMASTER.DESIGN_CADNO,'') AS CADNO, ISNULL(ITEMMASTER.ITEM_GREYWIDTH,'') AS GREYWIDTH, ISNULL(ITEMMASTER.ITEM_WIDTH,'') AS WIDTH,  ISNULL(COLORMASTER.COLOR_name,'') AS COLOR, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PCS,0) AS MTRS, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_LOTNO,0) AS LOTNO,  ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PROGISSDATE,'') AS PROGISSDATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_STATUS,'') AS STATUS, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_PRODCUTTING,'') AS PRODCUTTING,  ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_FINISHCUTTING,'') AS FINISHCUTTING, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_INWARDDATE,'') AS INWARDDATE, ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_RECDPCS,0) AS RECDPCS, ISNULL(MILLMASTER.MILL_NAME,'') AS MILLNAME,ISNULL(ALLPROGRAMMASTER_DESC.PROGRAM_RATE,'') AS RATE, ALLPROGRAMMASTER.PROGRAM_TYPE AS TYPE ", "", " ALLPROGRAMMASTER INNER JOIN ALLPROGRAMMASTER_DESC ON ALLPROGRAMMASTER.PROGRAM_NO = ALLPROGRAMMASTER_DESC.PROGRAM_NO AND ALLPROGRAMMASTER.PROGRAM_TYPE = ALLPROGRAMMASTER_DESC.PROGRAM_TYPE AND  ALLPROGRAMMASTER.PROGRAM_YEARID = ALLPROGRAMMASTER_DESC.PROGRAM_YEARID INNER JOIN ITEMMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN LEDGERS ON ALLPROGRAMMASTER.PROGRAM_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN COLORMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON ALLPROGRAMMASTER_DESC.PROGRAM_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN MILLMASTER ON DESIGNMASTER.DESIGN_MILLID = MILLMASTER.MILL_ID ", WHERECLAUSE & " AND ISNULL(ALLPROGRAMMASTER.PROGRAM_CARDRECDATE,'') <> '' AND ALLPROGRAMMASTER_DESC.PROGRAM_STATUS <> 'CANCEL' AND ALLPROGRAMMASTER.PROGRAM_YEARID = " & YearId & " ORDER BY ALLPROGRAMMASTER.PROGRAM_NO, ALLPROGRAMMASTER_DESC.PROGRAM_GRIDSRNO")
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
                If IsDBNull(DTROW("NAME")) = False And DTROW("NAME") <> "" And IsDBNull(DTROW("PROGISSDATE")) = False AndAlso DTROW("PROGISSDATE") <> "" Then
                    If DTROW("MILLNAME") <> DTROW("NAME") Then

                        If Val(DTROW("RATE")) = 0 Then DTROW("RATE") = 1

                        Dim TEMPNAMEID As Integer = 0
                        DT = OBJCMN.search(" ACC_ID AS NAMEID", "", " LEDGERS ", " AND ACC_CMPNAME = '" & DTROW("NAME") & "' AND ACC_YEARID = " & YearId)
                        TEMPNAMEID = DT.Rows(0).Item(0)
                        If DTROW("TYPE") = "PROGRAM" Then
                            DT = OBJCMN.Execute_Any_String("UPDATE PROGRAMMASTER SET PROGRAM_LEDGERID = " & Val(TEMPNAMEID) & " WHERE PROGRAM_NO = " & Val(DTROW("SRNO")) & " AND PROGRAM_YEARID = " & YearId, "", "")
                            DT = OBJCMN.Execute_Any_String("UPDATE PROGRAMMASTER_DESC SET PROGRAM_PROGISSDATE = '" & DTROW("PROGISSDATE") & "', PROGRAM_RATE = " & Val(DTROW("RATE")) & " WHERE PROGRAM_NO = " & Val(DTROW("SRNO")) & " AND PROGRAM_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND PROGRAM_YEARID = " & YearId, "", "")
                        Else
                            DT = OBJCMN.Execute_Any_String("UPDATE OPENINGPROGRAMMASTER SET OPPROGRAM_LEDGERID = " & Val(TEMPNAMEID) & " WHERE OPPROGRAM_NO = " & Val(DTROW("SRNO")) & " AND OPPROGRAM_YEARID = " & YearId, "", "")
                            DT = OBJCMN.Execute_Any_String("UPDATE OPENINGPROGRAMMASTER_DESC SET OPPROGRAM_PROGISSDATE = '" & DTROW("PROGISSDATE") & "', OPPROGRAM_RATE = " & Val(DTROW("RATE")) & " WHERE OPPROGRAM_NO = " & Val(DTROW("SRNO")) & " AND OPPROGRAM_GRIDSRNO = " & Val(DTROW("GRIDSRNO")) & " AND OPPROGRAM_YEARID = " & YearId, "", "")
                        End If

                        'UPDATING MILLID IN DESIGNMASTER
                        If DTROW("MILLNAME") <> "" Then
                            DT = OBJCMN.Execute_Any_String("UPDATE DESIGNMASTER SET DESIGN_MILLID = MILLMASTER.MILL_ID FROM MILLMASTER WHERE MILL_NAME = '" & DTROW("MILLNAME") & "' AND DESIGN_NO = '" & DTROW("DESIGNNO") & "' AND DESIGN_YEARID = " & YearId, "", "")
                        End If
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
            If IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "NAME")) = False Then
                Dim DTROW As DataRow = gridbill.GetFocusedDataRow
                If DTROW("NAME") <> "" And DTROW("MILLNAME") = "" Then
                    'FETCH CODE OF DYEING LEDGER AND ADD IN MILLNAME
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.search("ACC_CODE AS MILLCODE", "", " LEDGERS ", " AND ACC_CMPNAME = '" & DTROW("NAME") & "' AND ACC_YEARID = " & YearId)
                    If DT.Rows.Count > 0 Then DTROW("MILLNAME") = DT.Rows(0).Item("MILLCODE")
                End If
            End If


            If IsDBNull(gridbill.GetRowCellValue(e.RowHandle, "PROGISSDATE")) = False Then

                If gridbill.GetRowCellValue(e.RowHandle, "PROGISSDATE") <> "" Then


                    'CHECK THAT DATE SHOULD BE 10 CHARS
                    If Len(gridbill.GetRowCellValue(e.RowHandle, "PROGISSDATE")) <> 10 Then
                        e.Valid = False
                        gridbill.SetColumnError(GISSDTAE, "Invalid Date")
                        Exit Sub
                    End If


                    'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                    Dim TEMP As DateTime
                    If Not DateTime.TryParse(gridbill.GetRowCellValue(e.RowHandle, "PROGISSDATE"), TEMP) Then
                        e.Valid = False
                        gridbill.SetColumnError(GISSDTAE, "Invalid Date")
                        Exit Sub
                    Else
                        If (gridbill.GetRowCellValue(e.RowHandle, "PROGISSDATE") < Convert.ToDateTime(gridbill.GetRowCellValue(e.RowHandle, "DATE")).Date) Then
                            e.Valid = False
                            gridbill.SetColumnError(GISSDTAE, "Date must be After Card Date & Card Issue Date")
                            Exit Sub
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try

            Dim ROW As DataRow = gridbill.GetFocusedDataRow
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If IsDBNull(ROW("NAME")) = True Then
                MsgBox("No Row To Delete")
                Exit Sub
            End If

            If MsgBox("Delete Data?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If ROW("TYPE") = "PROGRAM" Then
                DT = OBJCMN.Execute_Any_String("UPDATE PROGRAMMASTER SET PROGRAM_LEDGERID =0 WHERE PROGRAM_NO = " & Val(ROW("SRNO")) & " AND PROGRAM_YEARID = " & YearId, "", "")
                DT = OBJCMN.Execute_Any_String("UPDATE PROGRAMMASTER_DESC SET PROGRAM_PROGISSDATE = ''  WHERE PROGRAM_NO = " & Val(ROW("SRNO")) & " AND PROGRAM_YEARID = " & YearId, "", "")
            Else
                DT = OBJCMN.Execute_Any_String("UPDATE OPENINGPROGRAMMASTER SET OPPROGRAM_LEDGERID =0  WHERE OPPROGRAM_NO = " & Val(ROW("SRNO")) & " AND OPPROGRAM_YEARID = " & YearId, "", "")
                DT = OBJCMN.Execute_Any_String("UPDATE OPENINGPROGRAMMASTER_DESC SET OPPROGRAM_PROGISSDATE = ''  WHERE OPPROGRAM_NO = " & Val(ROW("SRNO")) & " AND OPPROGRAM_YEARID = " & YearId, "", "")
            End If
            fillgrid()
            gridbill.Focus()

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
                DTROW("NAME") = DTROWUP("NAME")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_ValidatingEditor(sender As Object, e As BaseContainerValidateEditorEventArgs) Handles gridbill.ValidatingEditor
        Try
            Dim VIEW As DevExpress.XtraGrid.Views.Grid.GridView = sender
            Dim VAL As Object = e.Value
            If VIEW.FocusedColumn.FieldName = "NAME" And IsDBNull(VAL) = False Then
                Dim DTROW As DataRow = gridbill.GetFocusedDataRow
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ACC_CODE AS MILLCODE", "", " LEDGERS ", " AND ACC_CMPNAME = '" & VAL & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then DTROW("MILLNAME") = DT.Rows(0).Item("MILLCODE")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class