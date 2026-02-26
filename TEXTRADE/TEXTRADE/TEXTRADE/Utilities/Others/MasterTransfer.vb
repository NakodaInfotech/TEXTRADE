Imports System.IO.Compression
Imports BL
Imports Microsoft.VisualBasic.ApplicationServices


Public Class MasterTransfer
    Dim INTRES As Integer
    Dim OBJTRF As New ClsMasterTransfer
    Public FRMSTRING As String = ""
    'Sub fillcmp()
    '    Try
    '        Dim objclscommon As New ClsCommonMaster
    '        Dim dt As DataTable
    '        Dim whereclause As String = ""
    '        dt = objclscommon.search(" distinct user_cmpid", "", "UserMaster", " and User_Name = '" & UserName & "'")
    '        For Each DTROW As DataRow In dt.Rows
    '            If whereclause = "" Then
    '                whereclause = " AND CMP_ID IN (" & DTROW(0)
    '            Else
    '                whereclause = whereclause & "," & DTROW(0)
    '            End If
    '        Next
    '        whereclause = whereclause & ")"

    '        If SHOWHIDDENCMP = False Then whereclause = whereclause & " AND CMPMASTER.CMP_PASSWORD <> 'Infosys@123'"
    '        'dt = objclscommon.search("CMP_NAME, year_dbname, year_cmpid, year_startdate, year_enddate, year_id", "", "YearMaster INNER JOIN cmpmaster on cmp_id = year_cmpid", whereclause)
    '        dt = objclscommon.search("CMP_NAME, CMP_id", "", "cmpmaster", whereclause)
    '        If dt.Rows.Count > 0 Then
    '            dt.DefaultView.Sort = "cmp_name"
    '            gridcmp.DataSource = dt
    '            gridcmp.Columns(1).Visible = False
    '            'gridcmp.Columns(2).Visible = False
    '            'gridcmp.Columns(3).Visible = False
    '            'gridcmp.Columns(4).Visible = False
    '            'gridcmp.Columns(5).Visible = False
    '            gridcmp.Columns(0).HeaderText = "Company Name"
    '            gridcmp.Columns(0).Width = 270
    '        End If
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub
    Sub FILLCOMPANY(ByRef CMBCOMPANY As ComboBox)
        Try
            Cursor.Current = Cursors.WaitCursor
            If CMBCOMPANY.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" CMP_ID AS CMPID , CMP_NAME AS NAME ", "", "CMPMASTER")
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "NAME"
                    CMBCOMPANY.DisplayMember = "NAME"
                    CMBCOMPANY.ValueMember = "ID"
                    CMBCOMPANY.SelectedItem = Nothing
                End If
                CMBCOMPANY.DataSource = dt
                CMBCOMPANY.SelectedIndex = -1
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Sub FILLCMB()
        If CMBOLDCMP.Text.Trim = "" Then FILLCOMPANY(CMBOLDCMP)
        If CMBNEWCMP.Text.Trim = "" Then FILLCOMPANY(CMBNEWCMP)
    End Sub


    Private Sub MasterTransfer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBOLDCMP_Enter(sender As Object, e As EventArgs) Handles CMBOLDCMP.Enter
        Try
            If CMBOLDCMP.Text.Trim = "" Then FILLCOMPANY(CMBOLDCMP)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNEWCMP_Enter(sender As Object, e As EventArgs) Handles CMBNEWCMP.Enter
        Try
            If CMBNEWCMP.Text.Trim = "" Then FILLCOMPANY(CMBNEWCMP)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()

    End Sub

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try


            backup()
            'INTIMATE IF USER HAS SELECTED WRONG YEAR
            If CMBOLDCMP.Text.Trim = CMBNEWCMP.Text.Trim Then
                MsgBox("You have selected the Wrong Company.")
                Exit Sub
            End If


            Dim SELECTEDCMP As String = ""
            Dim TEMPMSG As Integer = MsgBox("Transfer Data from Selected Company?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                TEMPMSG = MsgBox("Are you sure, wish to Proceed?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim OBJCMN As New ClsCommon
                    Dim DT As DataTable = OBJCMN.SEARCH(" CMP_ID AS CMPID  ", "", " CMPMASTER", " AND CMP_NAME = '" & CMBOLDCMP.Text & "'")
                    If DT.Rows.Count > 0 Then
                        For Each DTROW As DataRow In DT.Rows

                            SELECTEDCMP = DTROW("CMPID")

                            If CHKOTHERMASTER.Checked = True Then
                                'CMPTRANSFERUSER(SELECTEDCMP)

                                'TRANSFERGROUP(SELECTEDCMP)
                                'TRANSFERLOCATION(SELECTEDCMP)
                                CMPTRANSFERMATERIALTYPE(SELECTEDCMP)
                                CMPTRANSFERCATEGORY(SELECTEDCMP)
                                CMPTRANSFERDYEDTYPE(SELECTEDCMP)
                                CMPTRANSFERPROCESS(SELECTEDCMP)
                                'CMPTRANSFERSAMPLETYPE(SELECTEDCMP)
                                CMPTRANSFERUNIT(SELECTEDCMP)
                                'CMPTRANSFERCONTRACT(SELECTEDCMP)
                                'CMPTRANSFERCURRENCY(SELECTEDCMP)
                                CMPTRANSFERMACHINE(SELECTEDCMP)
                                CMPTRANSFERSALESMAN(SELECTEDCMP)
                                CMPTRANSFERRACKSHELF(SELECTEDCMP)


                                CMPTRANSFERHSN(SELECTEDCMP)
                                'CMPTRANSFERYARNQUALITY(SELECTEDCMP)
                                'CMPTRANSFERMILL(SELECTEDCMP)
                                'CMPTRANSFERDESIGNER(SELECTEDCMP) 'USED IN DESIGN MASTER
                                'CMPTRANSFERVEHICLE(SELECTEDCMP)

                                'CMPTRANSFERWEAVE(SELECTEDCMP)
                                'CMPTRANSFERLOOM(SELECTEDCMP)

                                CMPTRANSFERITEM(SELECTEDCMP)
                                CMPTRANSFERCOLOR(SELECTEDCMP)

                                CMPTRANSFERDESIGN(SELECTEDCMP)
                                'CMPTRANSFERDEPARTMENT(SELECTEDCMP)
                                'CMPTRANSFERDESIGNATION(SELECTEDCMP)
                                'CMPTRANSFERPIECETYPE(SELECTEDCMP)
                                'CMPTRANSFERPACKING(SELECTEDCMP)
                                'CMPTRANSFERCHALLANTYPE(SELECTEDCMP)
                                'CMPTRANSFERTERM(SELECTEDCMP)
                                CMPTRANSFERQUALITY(SELECTEDCMP)

                                'CMPTRANSFERREORDER(SELECTEDCMP)
                                'CMPTRANSFERCATALOG(SELECTEDCMP)
                                'CMPTRANSFERREASON(SELECTEDCMP)
                                'CMPTRANSFERNARRATION(SELECTEDCMP)
                                'CMPTRANSFERPARTYBANK(SELECTEDCMP)
                                'CMPTRANSFERGODOWN(SELECTEDCMP)
                                'CMPTRANSFERGOC(SELECTEDCMP)
                                'CMPTRANSFERREGISTER(SELECTEDCMP)
                            End If
                            If CHKLEDGER.Checked = True Then
                                CMPTRANSFERGROUP(SELECTEDCMP)
                                CMPTRANSFERLOCATION(SELECTEDCMP)
                                CMPTRANSFERTRANSPORT(SELECTEDCMP)
                                CMPTRANSFERAGENTS(SELECTEDCMP)

                                CMPTRANSFERACCOUNTS(SELECTEDCMP)
                                CMPTRANSFEREMPLOYEES(SELECTEDCMP)
                                MsgBox("Masters Transferred Successfully")

                            End If
                        Next
                    End If



                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERMATERIALTYPE(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERMATERIALTYPE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERCATEGORY(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERCATEGORY()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERDYEDTYPE(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERDYEDTYPE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERPROCESS(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERPROCESS()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERUNIT(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERUNIT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERMACHINE(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERMACHINE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERSALESMAN(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERSALESMAN()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERRACKSHELF(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERRACKSHELF()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERHSN(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERHSN()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERITEM(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERITEM()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERCOLOR(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERCOLOR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERDESIGN(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERDESIGN()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERQUALITY(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERQUALITY()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERGROUP(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERGROUP()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERTRANSPORT(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERTRANSPORT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERAGENTS(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERAGENTS()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERACCOUNTS(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERACCOUNTS()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFEREMPLOYEES(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFEREMPLOYEES()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CMPTRANSFERLOCATION(ByVal SELECTEDCMP As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDCMP)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.CMPTRANSFERLOCATION()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub backup()
        'TAKE BACKUP
        Dim TEMPMSG As Integer = MsgBox("Create Backup?", MsgBoxStyle.YesNo)
        If TEMPMSG = vbYes Then

            'CHECKING FOR BACKUP FOLDER
            If FileIO.FileSystem.DirectoryExists("C:\TEXTRADEBACKUP") = False Then FileIO.FileSystem.CreateDirectory("C:\TEXTRADEBACKUP")


            'COPY THE BACKUP FILE IN DIRECTIORY AND THEN CREATE ZIP
            If FileIO.FileSystem.DirectoryExists("C:\TEXTRADEBACKUP\BACKUP") = False Then FileIO.FileSystem.CreateDirectory("C:\TEXTRADEBACKUP\BACKUP")


            'IF SAME DATE'S BACKUP EXIST THEN DELETE IT THEN RECREATE IT
            If FileIO.FileSystem.FileExists("C:\TEXTRADEBACKUP\BACKUP\TEXTRADE BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".bak") Then FileIO.FileSystem.DeleteFile("C:\TEXTRADEBACKUP\BACKUP\TEXTRADE BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".bak")

            Dim OBJCMN As New ClsCommon
            On Error Resume Next
            Dim DT As DataTable = OBJCMN.Execute_Any_String(" BACKUP DATABASE TEXTRADE TO DISK='C:\TEXTRADEBACKUP\BACKUP\TEXTRADE BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".BAK'", "", "")


            ZipFile.CreateFromDirectory("C:\TEXTRADEBACKUP\BACKUP", "C:\TEXTRADEBACKUP\TEXTRADE BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".zip", CompressionLevel.Optimal, False)

            'DELETE THE BACKUP FOLDER
            If FileIO.FileSystem.DirectoryExists("C:\TEXTRADEBACKUP\BACKUP") = True Then FileIO.FileSystem.DeleteDirectory("C:\TEXTRADEBACKUP\BACKUP", FileIO.DeleteDirectoryOption.DeleteAllContents)


            MsgBox("Backup Completed")
        End If

    End Sub
End Class