
Imports BL

Public Class BlockDateEntry

    Private Sub BlockDate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("*", "", "BLOCKDATE", " AND YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                DTSALEDATE.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("SALEDATE")).Date, "dd/MM/yyyy")
                DTPURDATE.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("PURDATE")).Date, "dd/MM/yyyy")
                DTCNDATE.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("CNDATE")).Date, "dd/MM/yyyy")
                DTDNDATE.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("DNDATE")).Date, "dd/MM/yyyy")
                DTEXPDATE.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("EXPDATE")).Date, "dd/MM/yyyy")
                DTGREYRECTRANS.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("GREYRECTRANSDATE")).Date, "dd/MM/yyyy")
                DTGREYRECPROCESS.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("GREYRECPROCESSDATE")).Date, "dd/MM/yyyy")
                DTGRN.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("GRNDATE")).Date, "dd/MM/yyyy")
                DTDYEINGREC.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("DYEINGRECDATE")).Date, "dd/MM/yyyy")
                DTISSUEPACK.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("ISSUEPAC")).Date, "dd/MM/yyyy")
                DTRECPACK.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("RECPACKDATE")).Date, "dd/MM/yyyy")
                DTJOBOUT.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("JODATE")).Date, "dd/MM/yyyy")
                DTJOBIN.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("JIDATE")).Date, "dd/MM/yyyy")
                DTSTOCKADJ.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("STOCKADJDATE")).Date, "dd/MM/yyyy")
                DTSALERETCHL.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("SRCHALLANDATE")).Date, "dd/MM/yyyy")
                DTPURRETCH.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("PRCHALLANDATE")).Date, "dd/MM/yyyy")
                DTPO.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("PODATE")).Date, "dd/MM/yyyy")
                DTSO.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("SODATE")).Date, "dd/MM/yyyy")
                DTSTORESPO.Value = Format(Convert.ToDateTime(DT.Rows(0).Item("STOREPODATE")).Date, "dd/MM/yyyy")
            Else
                DTSALEDATE.Value = Now.Date
                DTPURDATE.Value = Now.Date
                DTCNDATE.Value = Now.Date
                DTDNDATE.Value = Now.Date
                DTEXPDATE.Value = Now.Date
                DTGREYRECTRANS.Value = Now.Date
                DTGREYRECPROCESS.Value = Now.Date
                DTGRN.Value = Now.Date
                DTDYEINGREC.Value = Now.Date
                DTISSUEPACK.Value = Now.Date
                DTRECPACK.Value = Now.Date
                DTJOBOUT.Value = Now.Date
                DTJOBOUT.Value = Now.Date
                DTJOBIN.Value = Now.Date
                DTSTOCKADJ.Value = Now.Date
                DTSALERETCHL.Value = Now.Date
                DTPURRETCH.Value = Now.Date
                DTPO.Value = Now.Date
                DTSO.Value = Now.Date
                DTSTORESPO.Value = Now.Date
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try
            If Not datecheck(DTSALEDATE.Value.Date) Then
                MsgBox("Sale Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTPURDATE.Value.Date) Then
                MsgBox("Purchase Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTCNDATE.Value.Date) Then
                MsgBox("Credit Note Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTDNDATE.Value.Date) Then
                MsgBox("Debit Note Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTEXPDATE.Value.Date) Then
                MsgBox("Voucher Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTGREYRECTRANS.Value.Date) Then
                MsgBox(" Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTGREYRECPROCESS.Value.Date) Then
                MsgBox(" Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTGRN.Value.Date) Then
                MsgBox("GRN Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTDYEINGREC.Value.Date) Then
                MsgBox("Dyeing Receiving Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTISSUEPACK.Value.Date) Then
                MsgBox("Issue To Pack Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTRECPACK.Value.Date) Then
                MsgBox("Receive From Pack Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTJOBOUT.Value.Date) Then
                MsgBox("JobOut Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTJOBIN.Value.Date) Then
                MsgBox("JobIn Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTSTOCKADJ.Value.Date) Then
                MsgBox("Dtock Adjustment Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTSALERETCHL.Value.Date) Then
                MsgBox("Sale Return Challan Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTPURRETCH.Value.Date) Then
                MsgBox("Purchase Return Challan Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTPO.Value.Date) Then
                MsgBox("PO Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTSO.Value.Date) Then
                MsgBox("SO Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Not datecheck(DTSTORESPO.Value.Date) Then
                MsgBox("Store Purchase Order Date not in Accounting Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Wish to Block the Dates?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM BLOCKDATE WHERE YEARID = " & YearId, "", "")
            DT = OBJCMN.Execute_Any_String("INSERT INTO BLOCKDATE VALUES ('" & Format(DTSALEDATE.Value.Date, "MM/dd/yyyy") & "','" & Format(DTPURDATE.Value.Date, "MM/dd/yyyy") & "','" & Format(DTCNDATE.Value.Date, "MM/dd/yyyy") & "','" & Format(DTDNDATE.Value.Date, "MM/dd/yyyy") & "','" & Format(DTEXPDATE.Value.Date, "MM/dd/yyyy") & "', '" & Format(DTGREYRECTRANS.Value.Date, "MM/dd/yyyy") & "',   '" & Format(DTGREYRECPROCESS.Value.Date, "MM/dd/yyyy") & "',  '" & Format(DTGRN.Value.Date, "MM/dd/yyyy") & "',  '" & Format(DTDYEINGREC.Value.Date, "MM/dd/yyyy") & "',  '" & Format(DTISSUEPACK.Value.Date, "MM/dd/yyyy") & "',  '" & Format(DTRECPACK.Value.Date, "MM/dd/yyyy") & "',  '" & Format(DTJOBOUT.Value.Date, "MM/dd/yyyy") & "',  '" & Format(DTJOBIN.Value.Date, "MM/dd/yyyy") & "',  '" & Format(DTSTOCKADJ.Value.Date, "MM/dd/yyyy") & "',  '" & Format(DTSALERETCHL.Value.Date, "MM/dd/yyyy") & "',  '" & Format(DTPURRETCH.Value.Date, "MM/dd/yyyy") & "',  '" & Format(DTPO.Value.Date, "MM/dd/yyyy") & "', '" & Format(DTSO.Value.Date, "MM/dd/yyyy") & "', '" & Format(DTSTORESPO.Value.Date, "MM/dd/yyyy") & "',   " & YearId & ")", "", "")
            SALEBLOCKDATE = DTSALEDATE.Value.Date
            PURBLOCKDATE = DTPURDATE.Value.Date
            CNBLOCKDATE = DTCNDATE.Value.Date
            DNBLOCKDATE = DTDNDATE.Value.Date
            EXPBLOCKDATE = DTEXPDATE.Value.Date
            GREYRTBLOCKDATE = DTGREYRECTRANS.Value.Date
            GREYRPBLOCKDATE = DTGREYRECPROCESS.Value.Date
            GRNBLOCKDATE = DTGRN.Value.Date
            DYEINGRECBLOCKDATE = DTDYEINGREC.Value.Date
            ISSUEPACKBLOCKDATE = DTISSUEPACK.Value.Date
            RECPACKBLOCKDATE = DTRECPACK.Value.Date
            JOBLOCKDATE = DTJOBOUT.Value.Date
            JIBLOCKDATE = DTJOBIN.Value.Date
            STOCKADJBLOCKDATE = DTSTOCKADJ.Value.Date
            SALERETCHBLOCKDATE = DTSALERETCHL.Value.Date
            PURRETCHBLOCKDATE = DTPURRETCH.Value.Date
            POBLOCKDATE = DTPO.Value.Date
            SOBLOCKDATE = DTSO.Value.Date
            STOREPOBLOCKDATE = DTSTORESPO.Value.Date
            MsgBox("Block Date Added")
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(sender As Object, e As EventArgs) Handles CMDDELETE.Click
        Try
            If MsgBox("Wish to Delete Block Date?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM BLOCKDATE WHERE YEARID = " & YearId, "", "")
            MsgBox("Block Date Removed")
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BlockDateEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub
End Class