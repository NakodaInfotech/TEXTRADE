Imports BL
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.XtraGrid.Views.Grid

Public Class BeamJobInDetails
    Friend WithEvents GRIDBEAM As DataGridView
    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    '
    'Sub fillgrid()

    '    Try

    '        Dim OBJCMN As New ClsCommon

    '        Dim DT As DataTable = OBJCMN.SEARCH("  BJI_NO AS BEAMJONO,BJI_DATE AS BEAMJODATE, LEDGERS.ACC_CMPNAME AS NAME,GODOWNMASTER.GODOWN_NAME AS GODOWN, BJI_JONO AS JONO, BJI_REMARKS AS REMARKS, BJI_TOTALJOBMTRS AS TOTALMTRS ", "", " BEAMJOBININNER JOIN LEDGERS ON BEAMJOBIN.BJI_LEDGERID = LEDGERS.ACC_ID INNER JOIN GODOWNMASTER ON BEAMJOBIN.BJI_GODOWNID = GODOWNMASTER.GODOWN_ID ", " AND BJI_YEARID = " & YearId & " ORDER BY BJI_NO DESC ")
    '        GRIDDETAILS.RowCount = 0

    '        For Each ROW As DataRow In DT.Rows

    '            GRIDDETAILS.Rows.Add(
    '                GRIDDETAILS.RowCount + 1, "", ROW("NAME"), ROW("GODOWN"), ROW("BEAMJONO"), ROW("JONO"), "",
    '                Format(Convert.ToDateTime(ROW("BEAMJODATE")), "dd/MM/yyyy"),
    '                Format(Val(ROW("TOTALMTRS")), "0.00"),
    '                "",
    '                "",
    '                "",
    '                "",
    '                "",
    '                ROW("REMARKS")
    '                )

    '        Next

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub
    'Private Sub GRIDDETAILS_DoubleClick(sender As Object, e As EventArgs) Handles GRIDDETAILS.DoubleClick

    '    If GRIDDETAILS.CurrentRow Is Nothing Then Exit Sub

    '    Dim OBJ As New BeamJobIn

    '    OBJ.MdiParent = MDIMain
    '    OBJ.EDIT = True
    '    OBJ.TEMPBEAMJONO = Val(GRIDDETAILS.CurrentRow.Cells(GBEAMJONO.Index).Value)

    '    OBJ.Show()

    '    Me.Close()

    ' End Sub
End Class