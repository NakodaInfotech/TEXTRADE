Imports BL

Public Class CmpBankDetails

    Public frmstring As String        'Used from Displaying Customer, Vendor, Employee Master
    Public edit As Boolean = True

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(TXTBANK.Text.Trim)
            alParaval.Add(TXTACCNO.Text.Trim)
            alParaval.Add(TXTIFSC.Text.Trim)
            alParaval.Add(TXTUPI.Text.Trim)

            alParaval.Add(TXTBANK1.Text.Trim)
            alParaval.Add(TXTACCNO1.Text.Trim)
            alParaval.Add(TXTIFSC1.Text.Trim)
            alParaval.Add(TXTUPI1.Text.Trim)

            alParaval.Add(TXTBANK2.Text.Trim)
            alParaval.Add(TXTACCNO2.Text.Trim)
            alParaval.Add(TXTIFSC2.Text.Trim)
            alParaval.Add(TXTUPI2.Text.Trim)
            alParaval.Add(CmpId)

            Dim objAccountsMaster As New ClsBankDetails
            objAccountsMaster.alParaval = alParaval
            'objAccountsMaster.frmstring = frmstring

            If edit = False Then
                IntResult = objAccountsMaster.save()
                'edit = True
                MsgBox("Details Added")
            Else
                'alParaval.Add(tempAccountId)
                IntResult = objAccountsMaster.update()
                MsgBox("Details Updated")
            End If
            edit = False

            CLEAR()
            TXTBANK.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        'If CMBNAME.Text.Trim.Length = 0 Then
        '    EP.SetError(CMBNAME, "Fill Party Name")
        '    bln = False
        'End If

        'If GRIDBOOKINGS.RowCount = 0 Then
        '    EP.SetError(GRIDBOOKINGS, "Fill The Grid")
        '    bln = False
        'End If
        Return bln
    End Function

    Private Sub CmpBankDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'fillcmb()
            CLEAR()

            'cmbname.Enabled = True

            If edit = True Then

                Dim OBJINVOICE As New ClsBankDetails()
                Dim DT As DataTable = OBJINVOICE.GETCMPBANKDTLS(CmpId)

                If DT.Rows.Count > 0 Then
                    For Each dr As DataRow In DT.Rows
                        TXTBANK.Text = Convert.ToString(dr("BANK"))
                        TXTACCNO.Text = Convert.ToString(dr("ACCNO"))
                        TXTIFSC.Text = Convert.ToString(dr("IFSC"))
                        TXTUPI.Text = Convert.ToString(dr("UPI"))

                        TXTBANK1.Text = Convert.ToString(dr("BANK1"))
                        TXTACCNO1.Text = Convert.ToString(dr("ACCNO1"))
                        TXTIFSC1.Text = Convert.ToString(dr("IFSC1"))
                        TXTUPI1.Text = Convert.ToString(dr("UPI1"))

                        TXTBANK2.Text = Convert.ToString(dr("BANK2"))
                        TXTACCNO2.Text = Convert.ToString(dr("ACCNO2"))
                        TXTIFSC2.Text = Convert.ToString(dr("IFSC2"))
                        TXTUPI2.Text = Convert.ToString(dr("UPI2"))
                    Next
                Else
                    edit = False
                    CLEAR()
                End If
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub CLEAR()
        TXTBANK.Clear()
        TXTACCNO.Clear()
        TXTUPI.Clear()
        TXTIFSC.Clear()
        TXTBANK1.Clear()
        TXTACCNO1.Clear()
        TXTUPI1.Clear()
        TXTIFSC1.Clear()
        TXTBANK2.Clear()
        TXTACCNO2.Clear()
        TXTUPI2.Clear()
        TXTIFSC2.Clear()
    End Sub

End Class