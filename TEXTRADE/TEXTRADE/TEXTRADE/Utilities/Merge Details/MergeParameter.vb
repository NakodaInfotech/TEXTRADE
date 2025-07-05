Imports System.ComponentModel
Imports BL

Public Class MergeParameter

    Public EDIT As Boolean = False

    Sub CLEAR()
        Try
            EP.Clear()
            GETMAXNO()
            CMBTYPE.Text = ""
            CMBOLDNAME.Text = ""
            CMBREPLACE.Text = ""

            ENTRYDATE.Text = Now.Date
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAXNO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(MP_no),0) + 1 ", " MERGEPARAMETER ", " and MP_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSRNO.Text = Val(DTTABLE.Rows(0).Item(0))
    End Sub

    Private Sub MergeParameter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CLEAR()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            If MsgBox("Please Take a Backup Before Merging, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim alParaval As New ArrayList
            alParaval.Add(Format(Convert.ToDateTime(ENTRYDATE.Text).Date, "MM/dd/yyyy"))
            alParaval.Add(CMBTYPE.Text.Trim)
            alParaval.Add(CMBOLDNAME.Text)
            alParaval.Add(CMBREPLACE.Text)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)


            Dim OBJMERGE As New ClsMergeParameter
            OBJMERGE.alParaval = alParaval
            Dim DT As DataTable = OBJMERGE.SAVE()
            TXTSRNO.Text = Val(DT.Rows(0).Item(0))
            MessageBox.Show("Entry Merged Successfully")

            CLEAR()
            CMBTYPE.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtype_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTYPE.Validated
        Try
            CMBOLDNAME.Text = ""
            CMBREPLACE.Text = ""

            If CMBTYPE.Text.Trim = "AREA" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLAREA(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLAREA(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "BLANKET" Then
                If CMBOLDNAME.Text.Trim = "" Then fillBLANKET(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then fillBLANKET(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "CATEGORY" Then
                If CMBOLDNAME.Text.Trim = "" Then fillCATEGORY(CMBOLDNAME, EDIT)
                If CMBREPLACE.Text.Trim = "" Then fillCATEGORY(CMBREPLACE, EDIT)
            ElseIf CMBTYPE.Text.Trim = "CITY" Then
                If CMBOLDNAME.Text.Trim = "" Then fillCITY(CMBOLDNAME, EDIT)
                If CMBREPLACE.Text.Trim = "" Then fillCITY(CMBREPLACE, EDIT)
            ElseIf CMBTYPE.Text.Trim = "COLOR" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLCOLOR(CMBOLDNAME, "", "")
                If CMBREPLACE.Text.Trim = "" Then FILLCOLOR(CMBREPLACE, "", "")
            ElseIf CMBTYPE.Text.Trim = "CONTRACTOR" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLCONTRACT(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLCONTRACT(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "COUNTRY" Then
                If CMBOLDNAME.Text.Trim = "" Then fillCOUNTRY(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then fillCOUNTRY(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "CURRENCY" Then
                If CMBOLDNAME.Text.Trim = "" Then fillCURRENCY(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then fillCURRENCY(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "DEPARTMENT" Then
                If CMBOLDNAME.Text.Trim = "" Then filldepartment(CMBOLDNAME, EDIT)
                If CMBREPLACE.Text.Trim = "" Then filldepartment(CMBREPLACE, EDIT)
            ElseIf CMBTYPE.Text.Trim = "DESIGN" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLDESIGN(CMBOLDNAME, "")
                If CMBREPLACE.Text.Trim = "" Then FILLDESIGN(CMBREPLACE, "")
            ElseIf CMBTYPE.Text.Trim = "DESIGNATION" Then
                If CMBOLDNAME.Text.Trim = "" Then filldesignation(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then filldesignation(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "EMAIL" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLEMAIL(CMBOLDNAME, EDIT)
                If CMBREPLACE.Text.Trim = "" Then FILLEMAIL(CMBREPLACE, EDIT)
            ElseIf CMBTYPE.Text.Trim = "EMPLOYEE" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLEMP(CMBOLDNAME, EDIT)
                If CMBREPLACE.Text.Trim = "" Then FILLEMP(CMBREPLACE, EDIT)
            ElseIf CMBTYPE.Text.Trim = "GODOWN" Then
                If CMBOLDNAME.Text.Trim = "" Then fillGODOWN(CMBOLDNAME, EDIT)
                If CMBREPLACE.Text.Trim = "" Then fillGODOWN(CMBREPLACE, EDIT)
            ElseIf CMBTYPE.Text.Trim = "GROUP" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLGROUP(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLGROUP(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "GROUPOFCOMPANY" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLGROUPCOMPANY(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLGROUPCOMPANY(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "MACHINE" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLMACHINE(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLMACHINE(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "MERCHANT" Then
                If CMBOLDNAME.Text.Trim = "" Then fillitemname(CMBOLDNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
                If CMBREPLACE.Text.Trim = "" Then fillitemname(CMBREPLACE, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'")
            ElseIf CMBTYPE.Text.Trim = "MILLNAME" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLMILL(CMBOLDNAME, EDIT)
                If CMBREPLACE.Text.Trim = "" Then FILLMILL(CMBREPLACE, EDIT)
            ElseIf CMBTYPE.Text.Trim = "PACKINGTYPE" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLPACKINGTYPE(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLPACKINGTYPE(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "PARTYBANK" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLBANK(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLBANK(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "PIECETYPE" Then
                If CMBOLDNAME.Text.Trim = "" Then fillPIECETYPE(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then fillPIECETYPE(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "PROCESS" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLPROCESS(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLPROCESS(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "QUALITY" Then
                If CMBOLDNAME.Text.Trim = "" Then fillQUALITY(CMBOLDNAME, EDIT)
                If CMBREPLACE.Text.Trim = "" Then fillQUALITY(CMBREPLACE, EDIT)
            ElseIf CMBTYPE.Text.Trim = "RACK" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLRACK(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLRACK(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "SALESMAN" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLSALESMAN(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLSALESMAN(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "SAMPLETYPE" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLSAMPLETYPE(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLSAMPLETYPE(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "SHELF" Then
                If CMBOLDNAME.Text.Trim = "" Then FILLSHELF(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then FILLSHELF(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "STATE" Then
                If CMBOLDNAME.Text.Trim = "" Then fillSTATE(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then fillSTATE(CMBREPLACE)
            ElseIf CMBTYPE.Text.Trim = "STORE ITEM NAME" Then
                If CMBOLDNAME.Text.Trim = "" Then fillitemname(CMBOLDNAME, " AND ITEMMASTER.ITEM_FRMSTRING = 'ITEM NAME'")
                If CMBREPLACE.Text.Trim = "" Then fillitemname(CMBREPLACE, " AND ITEMMASTER.ITEM_FRMSTRING = 'ITEM NAME'")
            ElseIf CMBTYPE.Text.Trim = "TAX" Then
                If CMBOLDNAME.Text.Trim = "" Then filltax(CMBOLDNAME, EDIT)
                If CMBREPLACE.Text.Trim = "" Then filltax(CMBREPLACE, EDIT)
            ElseIf CMBTYPE.Text.Trim = "TERM" Then
                If CMBOLDNAME.Text.Trim = "" Then fillTERM(CMBOLDNAME, EDIT)
                If CMBREPLACE.Text.Trim = "" Then fillTERM(CMBREPLACE, EDIT)
            ElseIf CMBTYPE.Text.Trim = "UNIT" Then
                If CMBOLDNAME.Text.Trim = "" Then fillunit(CMBOLDNAME)
                If CMBREPLACE.Text.Trim = "" Then fillunit(CMBREPLACE)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Function ERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True

            If CMBTYPE.Text.Trim = "" Then
                EP.SetError(CMBTYPE, " Please Select Type")
                BLN = False
            End If

            If CMBOLDNAME.Text.Trim = CMBREPLACE.Text.Trim Then
                EP.SetError(CMBREPLACE, " Please Fill Diff. Value!")
                BLN = False
            End If

            If ENTRYDATE.Text = "__/__/____" Then
                EP.SetError(ENTRYDATE, " Please Enter Proper Date")
                BLN = False
            Else
                If Not datecheck(ENTRYDATE.Text) Then
                    EP.SetError(ENTRYDATE, "Date not in Accounting Year")
                    BLN = False
                End If
            End If

            Return BLN
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Function

    Private Sub ENTRYDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ENTRYDATE.GotFocus
        ENTRYDATE.SelectionStart = 0
    End Sub

    Private Sub ENTRYDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ENTRYDATE.Validating
        Try
            If ENTRYDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(ENTRYDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDCLEAR_Click(sender As Object, e As EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            EDIT = False
            CMBTYPE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJMERGE As New MergeParameterDetails
            OBJMERGE.MdiParent = MDIMain
            OBJMERGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub CMBOLDNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBOLDNAME.Validating
        Try
            If CMBOLDNAME.Text.Trim = "" Then Exit Sub
            If CMBTYPE.Text.Trim = "AREA" Then
                AREAVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "BLANKET" Then
                BLANKETVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "CATEGORY" Then
                CATEGORYVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "CITY" Then
                CITYVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "COLOR" Then
                COLORVALIDATE(CMBOLDNAME, e, Me, "", "")
            ElseIf CMBTYPE.Text.Trim = "CONTRACTOR" Then
                CONTRACTVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "COUNTRY" Then
                COUNTRYVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "CURRENCY" Then
                CURRENCYVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "DEPARTMENT" Then
                DEPARTMENTVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "DESIGN" Then
                DESIGNVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "DESIGNATION" Then
                designationvalidate(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "EMAIL" Then
                EMAILVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "EMPLOYEE" Then
                EMPVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "GODOWN" Then
                GODOWNVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "GROUPOFCOMPANY" Then
                GROUPCOMPANYVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "MACHINE" Then
                MACHINEVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text = "MERCHANT" Then
                itemvalidate(CMBOLDNAME, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
            ElseIf CMBTYPE.Text.Trim = "MILLNAME" Then
                MILLVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "PACKINGTYPE" Then
                PACKINGTYPEVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "PARTYBANK" Then
                PARTYBANKvalidate(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "PIECETYPE" Then
                PIECETYPEvalidate(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "PROCESS" Then
                PROCESSVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "QUALITY" Then
                QUALITYVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "RACK" Then
                RACKVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "SALESMAN" Then
                SALESMANVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "SAMPLETYPE" Then
                SAMPLETYPEVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "SHELF" Then
                SHELFVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "STATE" Then
                STATEVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text = "STORE ITEM NAME" Then
                STOREITEMVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "TAX" Then
                TAXvalidate(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "TERM" Then
                TERMVALIDATE(CMBOLDNAME, e, Me)
            ElseIf CMBTYPE.Text.Trim = "UNIT" Then
                unitvalidate(CMBOLDNAME, e, Me)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBREPLACE_Validating(sender As Object, e As CancelEventArgs) Handles CMBREPLACE.Validating
        Try
            If CMBREPLACE.Text.Trim = "" Then Exit Sub
            If CMBTYPE.Text.Trim = "AREA" Then
                AREAVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "BLANKET" Then
                BLANKETVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "CATEGORY" Then
                CATEGORYVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "CITY" Then
                CITYVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "COLOR" Then
                COLORVALIDATE(CMBREPLACE, e, Me, "", "")
            ElseIf CMBTYPE.Text.Trim = "CONTRACTOR" Then
                CONTRACTVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "COUNTRY" Then
                COUNTRYVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "CURRENCY" Then
                CURRENCYVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "DEPARTMENT" Then
                DEPARTMENTVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "DESIGN" Then
                DESIGNVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "DESIGNATION" Then
                designationvalidate(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "EMAIL" Then
                EMAILVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "EMPLOYEE" Then
                EMPVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "GODOWN" Then
                GODOWNVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "GROUPOFCOMPANY" Then
                GROUPCOMPANYVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "MACHINE" Then
                MACHINEVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "MERCHANT" Then
                itemvalidate(CMBREPLACE, e, Me, " AND ITEMMASTER.ITEM_FRMSTRING = 'MERCHANT'", "MERCHANT")
            ElseIf CMBTYPE.Text.Trim = "MILLNAME" Then
                MILLVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "PACKINGTYPE" Then
                PACKINGTYPEVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "PARTYBANK" Then
                PARTYBANKvalidate(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "PIECETYPE" Then
                PIECETYPEvalidate(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "PROCESS" Then
                PROCESSVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "QUALITY" Then
                QUALITYVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "RACK" Then
                RACKVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "SALESMAN" Then
                SALESMANVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "SAMPLETYPE" Then
                SAMPLETYPEVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "SHELF" Then
                SHELFVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "STATE" Then
                STATEVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "STORE ITEM NAME" Then
                STOREITEMVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "TAX" Then
                TAXvalidate(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "TERM" Then
                TERMVALIDATE(CMBREPLACE, e, Me)
            ElseIf CMBTYPE.Text.Trim = "UNIT" Then
                unitvalidate(CMBREPLACE, e, Me)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class