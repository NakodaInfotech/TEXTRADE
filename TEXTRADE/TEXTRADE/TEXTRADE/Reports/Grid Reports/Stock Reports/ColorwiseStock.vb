
Imports BL
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class ColorwiseStock

    Public FRMSTRING, TEMPDESIGNNO, TEMPQUALITY As String
    Public WHERECLAUSE As String = ""
    Public FROMDATE, TODATE As Date
    Public TEMPMTRS As Double = 0.0

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Opening_Stock_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Opening_Stock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            fillgrid(" and yearid=" & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform()
        Try
            Dim ObjGodownwiseStock As New GodownwiseStock
            ObjGodownwiseStock.MdiParent = MDIMain
            ObjGodownwiseStock.TEMPDESIGNNO = gridbill.GetFocusedRowCellValue("DESIGNNO")
            ObjGodownwiseStock.TEMPCOLOR = gridbill.GetFocusedRowCellValue("COLOR")
            ObjGodownwiseStock.TEMPQUALITY = gridbill.GetFocusedRowCellValue("QUALITY")
            ObjGodownwiseStock.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal TEMPCONDITION)
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If FRMSTRING = "NILSTOCKREPORT" Then

                If ALLOWBARCODEPRINT = True Then
                    DT = OBJCMN.Execute_Any_String(" SELECT ITEMNAME, DESIGNNO, COLOR FROM BARCODESTOCKREGISTER WHERE YEARID = " & YearId & WHERECLAUSE & " AND DATE <= '" & Format(FROMDATE, "MM/dd/yyyy") & "' GROUP BY ITEMNAME, DESIGNNO, COLOR HAVING ROUND(SUM(MTRS),2) > " & Val(TEMPMTRS) & "     EXCEPT  SELECT ITEMNAME, DESIGNNO, COLOR FROM BARCODESTOCKREGISTER WHERE YEARID = " & YearId & WHERECLAUSE & " AND DATE <= '" & Format(TODATE, "MM/dd/yyyy") & "' GROUP BY ITEMNAME, DESIGNNO, COLOR HAVING ROUND(SUM(MTRS),2) > " & Val(TEMPMTRS) & " order by ITEMNAME, DESIGNNO, COLOR", "", "")
                Else
                    DT = OBJCMN.Execute_Any_String(" SELECT ITEMNAME, DESIGNNO, COLOR FROM STOCKREGISTER WHERE YEARID = " & YearId & WHERECLAUSE & " AND DATE <= '" & Format(FROMDATE, "MM/dd/yyyy") & "' GROUP BY ITEMNAME, DESIGNNO, COLOR HAVING ROUND(SUM(MTRS)-SUM(ISSMTRS),2) > " & Val(TEMPMTRS) & "     EXCEPT  SELECT ITEMNAME, DESIGNNO, COLOR FROM STOCKREGISTER WHERE YEARID = " & YearId & WHERECLAUSE & " AND DATE <= '" & Format(TODATE, "MM/dd/yyyy") & "' GROUP BY ITEMNAME, DESIGNNO, COLOR HAVING ROUND(SUM(MTRS)-SUM(ISSMTRS),2) > " & Val(TEMPMTRS) & " order by ITEMNAME, DESIGNNO, COLOR", "", "")
                End If

            Else

                If ClientName <> "MOMAI" And ClientName <> "AXIS" And ClientName <> "AVIS" Then TEMPCONDITION = TEMPCONDITION & " AND ROUND(MTRS,0) > 0 "
                If TEMPQUALITY <> "" Then TEMPCONDITION = TEMPCONDITION & " AND QUALITY='" & TEMPQUALITY & "' "
                If TEMPDESIGNNO <> "" Then TEMPCONDITION = TEMPCONDITION & " AND DESIGNNO='" & TEMPDESIGNNO & "' "

                Dim GROUPBY As String = " GROUP BY COLOR"
                If TEMPQUALITY <> "" Then GROUPBY = GROUPBY & ", QUALITY"
                If TEMPDESIGNNO <> "" Then GROUPBY = GROUPBY & ", DESIGNNO"


                If TEMPQUALITY = "" And TEMPDESIGNNO = "" Then
                    If CHKNILSTOCK.CheckState = CheckState.Checked Then
                        DT = OBJCMN.search(" SUM(T.PCS) AS TOTALPCS, SUM(T.MTRS) AS TOTALMTRS, T.QUALITY, T.DESIGNNO, T.COLOR ", "", " (SELECT SUM(PCS) AS PCS, SUM(MTRS) AS MTRS, QUALITY, DESIGNNO, COLOR FROM BARCODESTOCK WHERE 1=1 " & WHERECLAUSE & TEMPCONDITION & " GROUP BY QUALITY, DESIGNNO, COLOR UNION ALL SELECT DISTINCT 0,0, QUALITY, DESIGNNO, COLOR FROM OUTBARCODESTOCK) AS T ", " GROUP BY T.QUALITY, T.DESIGNNO, T.COLOR")
                    Else
                        DT = OBJCMN.search(" SUM(PCS) AS TOTALPCS, SUM(MTRS) AS TOTALMTRS, ITEMNAME, QUALITY, DESIGNNO, COLOR, MILLNAME ", "", "  BARCODESTOCK", WHERECLAUSE & TEMPCONDITION & " GROUP BY ITEMNAME, QUALITY, DESIGNNO, COLOR, MILLNAME ")
                    End If
                Else
                    DT = OBJCMN.search(" SUM(PCS) AS TOTALPCS, SUM(MTRS) AS TOTALMTRS,'" & TEMPQUALITY & "' AS QUALITY,'" & TEMPDESIGNNO & "' AS DESIGNNO, COLOR", "", "  BARCODESTOCK", TEMPCONDITION & GROUPBY)
                End If

            End If


            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLWHATSAPP_Click(sender As Object, e As EventArgs) Handles TOOLWHATSAPP.Click
        Try
            SENDWHATSAPP()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub SENDWHATSAPP()
        Try
            If ALLOWWHATSAPP = False Then Exit Sub
            If Not CHECKWHASTAPPEXP() Then
                MsgBox("Whatsapp Package has Expired, Kindly contact Nakoda Infotech On 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Send Whatsapp?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String("DELETE FROM TEMPNILSTOCK WHERE YEARID = " & YearId, "", "")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                DT = OBJCMN.Execute_Any_String("INSERT INTO TEMPNILSTOCK VALUES ('" & dtrow("ITEMNAME") & "','" & dtrow("DESIGNNO") & "','" & dtrow("COLOR") & "'," & CmpId & "," & YearId & ")", "", "")
            Next

            Dim OBJSTOCK As New StockDesign
            OBJSTOCK.MdiParent = MDIMain
            OBJSTOCK.DIRECTPRINT = True
            OBJSTOCK.FRMSTRING = "NILSTOCKREPORT"
            OBJSTOCK.DIRECTWHATSAPP = True
            OBJSTOCK.Show()
            OBJSTOCK.Close()


            Dim OBJWHATSAPP As New SendWhatsapp
            OBJWHATSAPP.PATH.Add(Application.StartupPath & "\NILSTOCKREPORT.pdf")
            OBJWHATSAPP.FILENAME.Add("NILSTOCKREPORT.pdf")
            OBJWHATSAPP.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Shadewise Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Shadewise Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Shadewise Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)
        Catch ex As Exception
            MsgBox("Shade Stock Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        Try
            showform()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ColorwiseStock_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If FRMSTRING = "NILSTOCKREPORT" Then
                GQUALITY.Visible = False
                GMILLNAME.Visible = False
                CHKNILSTOCK.Visible = False
                GTOTALPCS.Visible = False
                GTOTALMTRS.Visible = False
                TOOLWHATSAPP.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class