Imports System.Runtime.Remoting.Metadata.W3cXsd2001
Imports System.Windows.Forms
Imports BL
Public Class SecurityInwardOutward
    'following two variables is only for used in edit mode....
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim gridDoubleClick As Boolean
    Dim tempRow As Integer

    Public edit As Boolean
    Public TEMPloanNO As String
    Public tempMsg As Integer
    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
    Sub clear()

        'tstxtbillno.Clear()
        'cmbname.Text = ""
        ''TXTMATERIAL.Clear()
        ''TXTQUALITY.Clear()
        'TXTWT.Clear()
        'TXTVEHICLENO.Clear()
        'WEFDATE.Text = Now.Date
        'txtremarks.Clear()

        'EP.Clear()
        ''txtsrno.Clear()
        ''cmbitemname.Text = ""
        ''txtgridremarks.Clear()
        'TXTQTY.Clear()
        ''cmbqtyunit.Text = ""

        'txtremarks.Clear()
        'cmbname.Text = ""
        ''gridloan.RowCount = 0
        ''lbltotalqty.Text = 0.0


        'gridDoubleClick = False
        ''txtadd.Clear()


        'getmax_loan_no() 'this function is for to get max value from the Purchase loanuisition table

        'If gridloan.RowCount > 0 Then
        '    txtsrno.Text = Val(gridloan.Rows(gridloan.RowCount - 1).Cells(gsrno.Index).Value) + 1
        'Else
        '    txtsrno.Text = 1
        'End If

    End Sub

    Private Sub cmbname_Enter(sender As Object, e As EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then FILLNAME(cmbname, edit, " AND (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' or GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class