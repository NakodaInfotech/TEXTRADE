
Imports System.ComponentModel
Imports BL
'Imports System.Speech.Recognition

Public Class Reminder

    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    'Private WithEvents recognizer As New SpeechRecognitionEngine()

    Sub getsrno()
        Try
            For I As Integer = 0 To gridbill.RowCount - 1
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                ROW("SRNO") = I + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If TXTNARRATION.Text.Trim = "" Then
            EP.SetError(TXTNARRATION, " Please Fill Created by ")
            bln = False
        End If

        If DTDATE.Text.Trim <> "__/__/____" Then
            'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
            Dim TEMP As DateTime
            If Not DateTime.TryParse(DTDATE.Text, TEMP) Then
                EP.SetError(DTDATE, " Please Fill Proper Date")
                bln = False
            End If
        End If

        Return bln
    End Function

    Sub EDITROW()
        Try
            If gridbill.GetFocusedRowCellValue("NO") > 0 Then
                GRIDDOUBLECLICK = True
                TXTNO.Text = Val(gridbill.GetFocusedRowCellValue("NO"))
                TXTSRNO.Text = Val(gridbill.GetFocusedRowCellValue("SRNO"))
                TXTNARRATION.Text = gridbill.GetFocusedRowCellValue("NARRATION")
                CMBASSIGNUSER.Text = gridbill.GetFocusedRowCellValue("ASSIGNUSER")
                TXTCREATEDBY.Text = gridbill.GetFocusedRowCellValue("CREATEDBY")

                TXTNARRATION.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String(" SELECT ISNULL(REMINDER.R_NO, 0) AS NO, REMINDER.R_GRIDSRNO AS SRNO, REMINDER.R_DATE AS DATE, ISNULL(REMINDER.R_NARRATION, '') AS NARRATION, ISNULL(USERMASTER.User_Name, '') AS ASSIGNUSER, ISNULL(REMINDER.R_CREATEDBY, '') AS CREATEDBY, ISNULL(REMINDER.R_DONE, 0) AS DONE FROM REMINDER LEFT OUTER JOIN USERMASTER ON REMINDER.R_ASSIGNUSERID = USERMASTER.User_id WHERE REMINDER.R_YEARID = " & YearId & " ORDER BY REMINDER.R_DATE, REMINDER.R_NO", "", "")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
            getsrno()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Reminder_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                Me.Close()
            ElseIf e.KeyCode = Keys.F5 Then
                gridbilldetails.Focus()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            TXTNO.Clear()
            TXTSRNO.Clear()
            DTDATE.Text = Now.Date
            TXTNARRATION.Clear()
            TXTCREATEDBY.Clear()
            CMBASSIGNUSER.Text = ""

            GRIDDOUBLECLICK = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Reminder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'REMINDER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillUSER(CMBASSIGNUSER, "")
            CLEAR()



            FILLGRID()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub SAVE()
        Try
            Dim ALPARAVAL As New ArrayList
            Dim OBJSM As New ClsReminder

            ALPARAVAL.Add(Val(TXTSRNO.Text.Trim))
            ALPARAVAL.Add(Format(Convert.ToDateTime(DTDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(TXTNARRATION.Text.Trim)
            ALPARAVAL.Add(CMBASSIGNUSER.Text.Trim)
            ALPARAVAL.Add(TXTCREATEDBY.Text.Trim)
            ALPARAVAL.Add(0)    'DONE
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            OBJSM.ALPARAVAL = ALPARAVAL
            If GRIDDOUBLECLICK = False Then

                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable = OBJSM.SAVE()
                If DT.Rows.Count > 0 Then TXTNO.Text = Val(DT.Rows(0).Item(0))

            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                ALPARAVAL.Add(Val(TXTNO.Text.Trim))
                Dim INTRES As Integer = OBJSM.UPDATE()
                GRIDDOUBLECLICK = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        EDITROW()
    End Sub

    Private Sub gridbilldetails_KeyDown(sender As Object, e As KeyEventArgs) Handles gridbilldetails.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                Dim ROW As DataRow = gridbill.GetFocusedDataRow()

                Dim TEMPMSG As Integer = MsgBox("Wish To Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                'DELETE FROM SAMPLEBARCODE
                Dim OBJSM As New ClsReminder
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(ROW("NO"))
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)

                OBJSM.ALPARAVAL = ALPARAVAL
                Dim INTRES As Integer = OBJSM.DELETE()

                FILLGRID()

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub CMDTALK_Click(sender As Object, e As EventArgs) Handles CMDTALK.Click
    '    Try

    '        Dim choices As New Choices()
    '        choices.Add("kaise ho")
    '        choices.Add("mujhe")
    '        choices.Add("samajha")
    '        choices.Add("nahi")
    '        choices.Add("aata")
    '        choices.Add("kya haal hai")
    '        choices.Add("shukriya")
    '        choices.Add("ka")
    '        choices.Add("ki")

    '        Dim gb As New GrammarBuilder()
    '        gb.Append(choices)

    '        Dim grammar As New Grammar(gb)
    '        recognizer.LoadGrammar(grammar)
    '        recognizer.SetInputToDefaultAudioDevice()

    '        recognizer.InitialSilenceTimeout = TimeSpan.FromSeconds(3)
    '        recognizer.BabbleTimeout = TimeSpan.FromSeconds(2)
    '        recognizer.EndSilenceTimeout = TimeSpan.FromSeconds(1)
    '        recognizer.EndSilenceTimeoutAmbiguous = TimeSpan.FromSeconds(1.5)

    '        recognizer.RecognizeAsync(RecognizeMode.Multiple)
    '        'recognizer.RecognizeAsync()

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub recognizer_SpeechRecognized(sender As Object, e As SpeechRecognizedEventArgs) Handles recognizer.SpeechRecognized
    '    TXTNARRATION.AppendText(e.Result.Text & Environment.NewLine)
    '    recognizer.RecognizeAsyncStop()
    'End Sub

    'Private Sub Reminder_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    '    recognizer.RecognizeAsyncStop()
    'End Sub

    Private Sub TXTCREATEDBY_Validated(sender As Object, e As EventArgs) Handles TXTCREATEDBY.Validated
        Try
            If TXTNARRATION.Text.Trim <> "" And CMBASSIGNUSER.Text.Trim <> "" Then
                EP.Clear()
                If Not errorvalid() Then
                    Exit Sub
                End If
                SAVE()
                FILLGRID()

            Else
                MsgBox("Enter Narration Name", MsgBoxStyle.Critical)
                Exit Sub
            End If

            TXTNARRATION.Clear()
            CMBASSIGNUSER.Text = ""
            TXTCREATEDBY.Clear()


            TXTSRNO.Text = gridbill.RowCount + 1
            TXTNARRATION.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillUSER(ByRef ASSINGUSER As ComboBox, Optional ByVal CONDITION As String = "")
        Try
            Dim objclscommon As New ClsCommon
            Dim dt As DataTable = objclscommon.SEARCH(" DISTINCT User_Name as [UserName]", "", "USERMASTER", " and USERMASTER.USER_cmpid= " & CmpId & " ORDER BY USER_NAME ")
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "USERNAME"
                ASSINGUSER.DataSource = dt
                ASSINGUSER.DisplayMember = "USERNAME"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DTDATE_Validating(sender As Object, e As CancelEventArgs) Handles DTDATE.Validating
        Try
            If DTDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DTDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class