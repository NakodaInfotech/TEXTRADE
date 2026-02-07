Imports BL
Imports System.Windows.Forms
Imports System.ComponentModel

Public Class BeamMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean
    Public FRMSTRING As String
    Public EDIT As Boolean
    Public TEMPBEAMNAME As String
    Public TEMPBEAMID As Integer

    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer

#Region "Form Events"

    Private Sub BeamMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'BEAM MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            CLEAR()
            fillcmb()

            TXTBEAMDESC.Text = TEMPBEAMNAME

            If EDIT Then LOADEDITDATA()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw
        End Try
    End Sub

    Private Sub BeamMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Close()
    End Sub

#End Region

#Region "Clear / Fill"

    Sub CLEAR()
        TXTBEAMDESC.Clear()
        TXTTL.Clear()
        TXTHSNCODE.Clear()

        TXTSRNO.Clear()
        CMBGRIDQUALITY.Text = ""
        CMBSHADE.Text = ""
        TXTGRIDENDS.Clear()
        TXTGRIDWT.Clear()

        GRIDBEAM.Rows.Clear()

        TXTTOTALENDS.Clear()
        TXTTOTALWT.Clear()
    End Sub

    Sub fillcmb()
        If CMBGRIDQUALITY.Text = "" Then fillQUALITY(CMBGRIDQUALITY, EDIT)
        If CMBSHADE.Text = "" Then FILLCOLOR(CMBSHADE)

    End Sub
    '=========================================
    ' SIMPLE COLOR FILL (NO DESIGN / ITEM)
    '=========================================
    Public Sub FILLCOLOR(ByRef CMBCOLOR As ComboBox)
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable

            DT = OBJCMN.SEARCH(
            "COLOR_NAME",
            "",
            "COLORMASTER",
            " AND COLOR_YEARID = " & YearId & " ORDER BY COLOR_NAME"
        )

            CMBCOLOR.DataSource = DT
            CMBCOLOR.DisplayMember = "COLOR_NAME"
            CMBCOLOR.ValueMember = "COLOR_NAME"
            CMBCOLOR.SelectedIndex = -1

        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Edit Load"

    Sub LOADEDITDATA()
        If USEREDIT = False And USERVIEW = False Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        Dim objCommon As New ClsCommonMaster
        Dim dt As DataTable = objCommon.search(
            "ISNULL(BEAMMASTER.BEAM_ID,0) BEAMID,
             ISNULL(BEAMMASTER.BEAM_NAME,'') BEAMNAME,
             ISNULL(HSNMASTER.HSN_CODE,'') HSNCODE,
             ISNULL(BEAMMASTER_DESC.BEAM_SRNO,0) GRIDSRNO,
             ISNULL(GRIDQUALITYMASTER.QUALITY_NAME,'') GRIDQUALITY,
             ISNULL(COLORMASTER.COLOR_NAME,'') SHADE,
             ISNULL(BEAMMASTER_DESC.BEAM_GRIDENDS,0) GRIDENDS,
             ISNULL(BEAMMASTER_DESC.BEAM_GRIDWT,0) GRIDWT",
            "",
            "QUALITYMASTER GRIDQUALITYMASTER 
             RIGHT JOIN BEAMMASTER_DESC ON GRIDQUALITYMASTER.QUALITY_ID = BEAMMASTER_DESC.BEAM_GRIDQUALITYID
             LEFT JOIN COLORMASTER ON BEAMMASTER_DESC.BEAM_SHADEID = COLORMASTER.COLOR_ID
             RIGHT JOIN BEAMMASTER ON BEAMMASTER_DESC.BEAM_ID = BEAMMASTER.BEAM_ID
             LEFT JOIN HSNMASTER ON BEAMMASTER.BEAM_HSNCODEID = HSNMASTER.HSN_ID",
            " AND BEAMMASTER.BEAM_ID = " & TEMPBEAMID & " AND BEAMMASTER.BEAM_yearid = " & YearId)

        For Each r As DataRow In dt.Rows
            TXTBEAMDESC.Text = r("BEAMNAME").ToString
            TXTHSNCODE.Text = r("HSNCODE").ToString

            If r("GRIDQUALITY").ToString <> "" Then
                GRIDBEAM.Rows.Add(r("GRIDSRNO"), r("GRIDQUALITY"), r("SHADE"), r("GRIDENDS"), r("GRIDWT"))
            End If
        Next

        GETSRNO(GRIDBEAM)
        TOTAL()
    End Sub

#End Region

#Region "Grid Logic"

    Sub GETSRNO(grid As DataGridView)
        For Each r As DataGridViewRow In grid.Rows
            r.Cells(0).Value = r.Index + 1
        Next
    End Sub

    Sub TOTAL()
        TXTTOTALENDS.Text = "0"
        TXTTOTALWT.Text = "0.000"

        For Each r As DataGridViewRow In GRIDBEAM.Rows
            TXTTOTALENDS.Text = Val(TXTTOTALENDS.Text) + Val(r.Cells(GENDS.Index).Value)
            TXTTOTALWT.Text = Val(TXTTOTALWT.Text) + Val(r.Cells(GWTPER.Index).Value)
        Next
    End Sub

#End Region

#Region "Validation / Save"

    Private Function errorvalid() As Boolean
        EP.Clear()

        If TXTBEAMDESC.Text.Trim = "" Then
            EP.SetError(TXTBEAMDESC, "Enter Beam Name")
            Return False
        End If

        If TXTTL.Text.Trim = "" Then
            EP.SetError(TXTTL, "Enter Tapline")
            Return False
        End If

        If GRIDBEAM.RowCount = 0 Then
            EP.SetError(TXTBEAMDESC, "Enter Yarn Details")
            Return False
        End If

        Return True
    End Function

#End Region

End Class
