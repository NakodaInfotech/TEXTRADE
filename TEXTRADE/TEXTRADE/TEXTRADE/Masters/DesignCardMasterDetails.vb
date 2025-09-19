Imports BL

Public Class DesignCardMasterDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean
    Public FRMSTRING As String
    Private Sub DesignCardMasterDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'DESIGN MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillgrid()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
    Sub fillgrid()
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim objClsCommon As New ClsCommonMaster
            Dim dttable As DataTable = objClsCommon.search("   DESIGNMASTER.DESIGN_NO AS DESIGNNO, ISNULL(MILLMASTER.MILL_NAME, '') AS MILLNAME, ISNULL(DESIGNMASTER.DESIGN_CADNO, '') AS CADNO, ISNULL(DESIGNMASTER.DESIGN_PURRATE, 0) AS PURRATE, ISNULL(DESIGNMASTER.DESIGN_SALERATE, 0) AS SALERATE, ISNULL(DESIGNMASTER.DESIGN_WRATE, 0) AS WRATE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME, ISNULL(DESIGNMASTER.DESIGN_BLOCKED, 0)  AS BLOCKED,  DESIGNMASTER.DESIGN_CREATED AS CREATED", "", " DESIGNMASTER LEFT OUTER JOIN ITEMMASTER ON DESIGNMASTER.DESIGN_ITEMID = ITEMMASTER.item_id LEFT OUTER JOIN  MILLMASTER ON DESIGNMASTER.DESIGN_MILLID = MILLMASTER.MILL_ID ", " and design_yearid = " & YearId)
            GRIDBILLDETAILS.DataSource = dttable
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class