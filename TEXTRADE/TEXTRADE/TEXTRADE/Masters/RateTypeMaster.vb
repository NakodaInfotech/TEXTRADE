
Imports BL

Public Class RateTypeMaster

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            If TXTRATE01.Text.Trim <> "" And TXTRATE02.Text.Trim <> "" And TXTRATE03.Text.Trim <> "" And TXTRATE04.Text.Trim <> "" And TXTRATE05.Text.Trim <> "" And TXTRATE06.Text.Trim <> "" And TXTRATE07.Text.Trim <> "" And TXTRATE08.Text.Trim <> "" And TXTRATE09.Text.Trim <> "" And TXTRATE10.Text.Trim <> "" And TXTRATE11.Text.Trim <> "" And TXTRATE12.Text.Trim <> "" And TXTRATE13.Text.Trim <> "" And TXTRATE14.Text.Trim <> "" And TXTRATE15.Text.Trim <> "" Then

                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(TXTRATE01.Text.Trim)
                ALPARAVAL.Add(TXTRATE02.Text.Trim)
                ALPARAVAL.Add(TXTRATE03.Text.Trim)
                ALPARAVAL.Add(TXTRATE04.Text.Trim)
                ALPARAVAL.Add(TXTRATE05.Text.Trim)
                ALPARAVAL.Add(TXTRATE06.Text.Trim)
                ALPARAVAL.Add(TXTRATE07.Text.Trim)
                ALPARAVAL.Add(TXTRATE08.Text.Trim)
                ALPARAVAL.Add(TXTRATE09.Text.Trim)
                ALPARAVAL.Add(TXTRATE10.Text.Trim)
                ALPARAVAL.Add(TXTRATE11.Text.Trim)
                ALPARAVAL.Add(TXTRATE12.Text.Trim)
                ALPARAVAL.Add(TXTRATE13.Text.Trim)
                ALPARAVAL.Add(TXTRATE14.Text.Trim)
                ALPARAVAL.Add(TXTRATE15.Text.Trim)
                ALPARAVAL.Add(TXTHEAD1.Text.Trim)
                ALPARAVAL.Add(TXTHEAD2.Text.Trim)
                ALPARAVAL.Add(TXTHEAD3.Text.Trim)
                ALPARAVAL.Add(TXTHEAD4.Text.Trim)
                ALPARAVAL.Add(TXTHEAD5.Text.Trim)
                ALPARAVAL.Add(TXTHEAD6.Text.Trim)
                ALPARAVAL.Add(TXTHEAD7.Text.Trim)
                ALPARAVAL.Add(TXTHEAD8.Text.Trim)
                ALPARAVAL.Add(TXTHEAD9.Text.Trim)
                ALPARAVAL.Add(TXTHEAD10.Text.Trim)
                ALPARAVAL.Add(TXTHEAD11.Text.Trim)
                ALPARAVAL.Add(TXTHEAD12.Text.Trim)
                ALPARAVAL.Add(TXTHEAD13.Text.Trim)
                ALPARAVAL.Add(TXTHEAD14.Text.Trim)
                ALPARAVAL.Add(TXTHEAD15.Text.Trim)

                ALPARAVAL.Add(CmpId)

                Dim OBJRATE As New ClsRateTypeMaster
                OBJRATE.alParaval = ALPARAVAL
                Dim INTERS As Integer = OBJRATE.SAVE
                MsgBox("Details Added")
                Me.Close()
            Else
                MsgBox("Enter all Fields", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RateTypeMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String("SELECT COLNAME AS COLNAME, RATENAME, HEADERNAME FROM RATETYPEMASTER  WHERE CMPID = " & CmpId, "", "")
            For Each DTROW As DataRow In DT.Rows
                If DTROW("COLNAME") = "RATE1" Then TXTRATE01.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE2" Then TXTRATE02.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE3" Then TXTRATE03.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE4" Then TXTRATE04.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE5" Then TXTRATE05.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE6" Then TXTRATE06.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE7" Then TXTRATE07.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE8" Then TXTRATE08.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE9" Then TXTRATE09.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE10" Then TXTRATE10.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE11" Then TXTRATE11.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE12" Then TXTRATE12.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE13" Then TXTRATE13.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE14" Then TXTRATE14.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE15" Then TXTRATE15.Text = DTROW("RATENAME")
                If DTROW("COLNAME") = "RATE1" Then TXTHEAD1.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE2" Then TXTHEAD2.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE3" Then TXTHEAD3.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE4" Then TXTHEAD4.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE5" Then TXTHEAD5.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE6" Then TXTHEAD6.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE7" Then TXTHEAD7.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE8" Then TXTHEAD8.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE9" Then TXTHEAD9.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE10" Then TXTHEAD10.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE11" Then TXTHEAD11.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE12" Then TXTHEAD12.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE13" Then TXTHEAD13.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE14" Then TXTHEAD14.Text = DTROW("HEADERNAME")
                If DTROW("COLNAME") = "RATE15" Then TXTHEAD15.Text = DTROW("HEADERNAME")
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class