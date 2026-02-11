Imports BL
Public Class RollsRecdFromWarper

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, GRIDUPLOADDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub
    Public TEMPROLLSRECDNO As Integer
    Dim TEMPMSG As Integer
    Private Sub CMDSAVE_Click(sender As Object, e As EventArgs) Handles CMDSAVE.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(ROLLSRECDDATE.Text.Trim).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBOURGODOWN.Text.Trim)
            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(TXTCHALLANNO.Text.Trim)
            alParaval.Add(DTCHALLANDATE.Text.Trim)
            alParaval.Add(Val(TXTWARPINGNO.Text.Trim))
            alParaval.Add(Val(TXTPROGRAMNO.Text.Trim))
            alParaval.Add(Val(TXTPROGRAMSRNO.Text.Trim))
            alParaval.Add(Format(Convert.ToDateTime(PROGRAMDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(Val(TXTENDS.Text.Trim))
            alParaval.Add(Val(TXTTOTALENDS.Text.Trim))
            alParaval.Add(Val(TXTLENGTH.Text.Trim))
            alParaval.Add(Val(TXTCUT.Text.Trim))
            alParaval.Add(Val(TXTCOUNT.Text.Trim))
            alParaval.Add(Val(TXTLONGATION.Text.Trim))
            alParaval.Add(Val(TXTTL.Text.Trim))

            alParaval.Add(Val(TXTROLLS.Text))
            alParaval.Add(Format(Val(TXTWT.Text), "0.000"))

            alParaval.Add(Val(LBLTOTALCONES.Text))
            alParaval.Add(Format(Val(LBLTOTALGROSSWT.Text.Trim), "0.000"))
            alParaval.Add(Format(Val(LBLTOTALNETTWT.Text.Trim), "0.000"))

            alParaval.Add(Val(TXTUSEDFRESH.Text.Trim))
            alParaval.Add(Val(TXTUSEDFRESHWT.Text.Trim))
            alParaval.Add(Val(TXTUSEDFRESHNETT.Text.Trim))
            alParaval.Add(Val(TXTUSEDWINDING.Text.Trim))
            alParaval.Add(Val(TXTUSEDWINDINGWT.Text.Trim))
            alParaval.Add(Val(TXTUSEDWINDINGNETT.Text.Trim))
            alParaval.Add(Val(TXTUSEDFIRKA.Text.Trim))
            alParaval.Add(Val(TXTUSEDFIRKAWT.Text.Trim))
            alParaval.Add(Val(TXTUSEDFIRKANETT.Text.Trim))

            alParaval.Add(Val(TXTRETFRESH.Text.Trim))
            alParaval.Add(Val(TXTRETFRESHWT.Text.Trim))
            alParaval.Add(Val(TXTRETFRESHNETT.Text.Trim))
            alParaval.Add(Val(TXTRETWINDING.Text.Trim))
            alParaval.Add(Val(TXTRETWINDINGWT.Text.Trim))
            alParaval.Add(Val(TXTRETWINDINGNETT.Text.Trim))
            alParaval.Add(Val(TXTRETFIRKA.Text.Trim))
            alParaval.Add(Val(TXTRETFIRKAWT.Text.Trim))
            alParaval.Add(Val(TXTRETFIRKANETT.Text.Trim))

            alParaval.Add(TXTREMARKS.Text.Trim)
            alParaval.Add(CMBWINDINGMILL.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim SRNO As String = ""
            Dim QUALITY As String = ""
            Dim MILLNAME As String = ""
            Dim LOTNO As String = ""
            Dim CONES As String = ""
            Dim GROSSWT As String = ""
            Dim NETTWT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDROLLS.Rows
                If row.Cells(gsrno.Index).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = row.Cells(gsrno.Index).Value
                        QUALITY = row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = row.Cells(GMILLNAME.Index).Value.ToString
                        LOTNO = Val(row.Cells(GLOTNO.Index).Value)
                        CONES = Val(row.Cells(GCONES.Index).Value)
                        GROSSWT = Format(Val(row.Cells(GGROSSWT.Index).Value), "0.000")
                        NETTWT = Format(Val(row.Cells(GNETTWT.Index).Value), "0.000")
                    Else
                        SRNO = SRNO & "|" & row.Cells(gsrno.Index).Value
                        QUALITY = QUALITY & "|" & row.Cells(GQUALITY.Index).Value.ToString
                        MILLNAME = MILLNAME & "|" & row.Cells(GMILLNAME.Index).Value.ToString
                        LOTNO = LOTNO & "|" & row.Cells(GLOTNO.Index).Value.ToString
                        CONES = CONES & "|" & Val(row.Cells(GCONES.Index).Value)
                        GROSSWT = GROSSWT & "|" & Format(Val(row.Cells(GGROSSWT.Index).Value), "0.000")
                        NETTWT = NETTWT & "|" & row.Cells(GNETTWT.Index).Value.ToString
                    End If
                End If
            Next

            alParaval.Add(SRNO)
            alParaval.Add(QUALITY)
            alParaval.Add(MILLNAME)
            alParaval.Add(LOTNO)
            alParaval.Add(CONES)
            alParaval.Add(GROSSWT)
            alParaval.Add(NETTWT)


            '            Dim OBJROLLSREC As New ClsRollsReceived
            '            OBJROLLSREC.alParaval = alParaval

            '            If EDIT = False Then
            '                If USERADD = False Then
            '                    MsgBox("Insufficient Rights")
            '                    Exit Sub
            '                End If
            '                Dim DT As DataTable = OBJROLLSREC.SAVE()
            '                TEMPROLLSRECDNO = DT.Rows(0).Item(0)
            '                TXTROLLSRECDNO.Text = TEMPROLLSRECDNO
            '                MsgBox("Details Added")

            '            Else
            '                If USEREDIT = False Then
            '                    MsgBox("Insufficient Rights")
            '                    Exit Sub
            '                End If
            '                alParaval.Add(TEMPROLLSRECDNO)
            '                IntResult = OBJROLLSREC.UPDATE()
            '                EDIT = False
            '                MsgBox("Details Updated")

            '            End If

            '            If lbllocked.Visible = False Then
            '                If MsgBox("Issue Rolls Directly to Sizer?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '                    Dim OBJSIZER As New DirectIssueSizer
            '                    OBJSIZER.ShowDialog()
            '                    If OBJSIZER.cmbname.Text.Trim = "" Then GoTo LINE1
            '                    DIRECTISSUESIZER(OBJSIZER.cmbname.Text.Trim)
            '                End If
            '            End If

            'LINE1:
            '            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            '            CLEAR()
            '            Show NEXT BILL ON EDIT MODE DONT CLEAR
            '            Call toolnext_Click(sender, e)
            ROLLSRECDDATE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True


        If ROLLSRECDDATE.Text = "__/__/____" Then
            EP.SetError(ROLLSRECDDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(ROLLSRECDDATE.Text) Then
                EP.SetError(ROLLSRECDDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If DTCHALLANDATE.Text = "__/__/____" Then
            EP.SetError(DTCHALLANDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(DTCHALLANDATE.Text) Then
                EP.SetError(DTCHALLANDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If ROLLSRECDDATE.Text.Trim <> "__/__/____" And DTCHALLANDATE.Text.Trim <> "__/__/____" Then
            If Convert.ToDateTime(ROLLSRECDDATE.Text).Date < Convert.ToDateTime(DTCHALLANDATE.Text).Date Then
                EP.SetError(DTCHALLANDATE, " Please Enter Proper Challan Date")
                bln = False
            End If
        End If

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Fill Jobber Name")
            bln = False
        End If

        If CMBWINDINGMILL.Text.Trim.Length = 0 And ClientName <> "SASHWINKUMAR" And (Val(TXTUSEDWINDING.Text.Trim) > 0 Or Val(TXTUSEDFIRKA.Text.Trim) > 0 Or Val(TXTRETWINDING.Text.Trim) > 0 Or Val(TXTRETFIRKA.Text.Trim) > 0) Then
            EP.SetError(CMBWINDINGMILL, "Please Fill Mill Name")
            bln = False
        End If

        If Val(TXTROLLS.Text.Trim) = 0 Then
            EP.SetError(CMBNAME, "Please Enter Rolls")
            bln = False
        End If

        If Val(TXTENDS.Text.Trim) = 0 Then
            EP.SetError(TXTENDS, "Please Enter Ends")
            bln = False
        End If

        If Val(TXTLENGTH.Text.Trim) = 0 Then
            EP.SetError(TXTLENGTH, "Please Enter Length")
            bln = False
        End If

        If CMBOURGODOWN.Text.Trim.Length = 0 Then
            EP.SetError(CMBOURGODOWN, " Please Fill Godown ")
            bln = False
        End If

        If TXTPROGRAMNO.Text.Trim.Length = 0 And ClientName = "SASHWINKUMAR" Then
            EP.SetError(TXTPROGRAMNO, " Please Select Program")
            bln = False
        End If

        If GRIDROLLS.RowCount = 0 Then
            EP.SetError(TXTNETTWT, "Enter Proper Details")
            bln = False
        End If

        For Each row As DataGridViewRow In GRIDROLLS.Rows
            If Val(row.Cells(GGROSSWT.Index).Value) = 0 Then
                EP.SetError(TXTNETTWT, "Gross Wt Cannot be 0")
                bln = False
            End If

            If Val(row.Cells(GNETTWT.Index).Value) = 0 Then
                EP.SetError(TXTNETTWT, "Nett Wt Cannot be 0")
                bln = False
            End If
        Next


        'WARPER CAN USE EXTRA CONES, IF MATERIAL IS SHORT THEN HE WILL USE EXTRA CONES
        'If ClientName <> "JASHOK" Then
        '    If (Val(TXTUSEDFRESH.Text.Trim) + Val(TXTUSEDWINDING.Text.Trim) + Val(TXTUSEDFIRKA.Text.Trim)) <> Val(TXTENDS.Text.Trim) Then
        '        EP.SetError(TXTENDS, "Ends Does not Match with Used Details")
        '        bln = False
        '    End If
        'End If

        If (Val(TXTUSEDFRESH.Text.Trim) + Val(TXTUSEDWINDING.Text.Trim) + Val(TXTUSEDFIRKA.Text.Trim)) <> (Val(TXTRETFRESH.Text.Trim) + Val(TXTRETWINDING.Text.Trim) + Val(TXTRETFIRKA.Text.Trim)) Then
            EP.SetError(TXTENDS, "Used Details Does not Match with Return Details")
            bln = False
        End If


        If Format(((Val(TXTUSEDFRESHWT.Text.Trim) + Val(TXTUSEDWINDINGNETT.Text.Trim) + Val(TXTUSEDFIRKANETT.Text.Trim)) - (Val(TXTRETFRESHNETT.Text.Trim) + Val(TXTRETWINDINGNETT.Text.Trim) + Val(TXTRETFIRKANETT.Text.Trim))), "0.000") <> Format(Val(TXTWT.Text.Trim), "0.000") Then
            EP.SetError(TXTENDS, "Roll Wt Does not Match with Entered Details")
            bln = False
        End If

        'DONE TEMPORARILY
        'If lbllocked.Visible = True Then
        '    EP.SetError(lbllocked, "Unable to Modify, Entry Locked")
        '    bln = False
        'End If

        Return bln
    End Function

    Sub CALCWT()
        Try
            TXTWT.Text = Format((Val(TXTUSEDFRESHWT.Text.Trim) + Val(TXTUSEDWINDINGNETT.Text.Trim) + Val(TXTUSEDFIRKANETT.Text.Trim)) - (Val(TXTRETFRESHNETT.Text.Trim) + Val(TXTRETWINDINGNETT.Text.Trim) + Val(TXTRETFIRKANETT.Text.Trim)), "0.000")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class