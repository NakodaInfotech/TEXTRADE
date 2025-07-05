Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Security
Imports System.Windows.Forms
Imports DevExpress.Pdf
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraEditors

Namespace DevExpressTest.Docs.Demos
    Public Class PdfFileHelper
        Private ReadOnly documentProcessor As PdfDocumentProcessor
        Private ReadOnly viewer As PdfViewer
        Private password As SecureString

        Public Sub New(ByVal documentProcessor As PdfDocumentProcessor, ByVal viewer As PdfViewer)
            Me.documentProcessor = documentProcessor
            Me.viewer = viewer
            AddHandler documentProcessor.PasswordRequested, AddressOf OnDocumentServerPasswordRequested
            AddHandler viewer.PasswordRequested, AddressOf OnViewerPasswordRequested
        End Sub
        Public Sub MergeFile()
            Dim fileName As String = OpenFileDialog()
            If (Not String.IsNullOrEmpty(fileName)) Then
                Do
                    Try
                        documentProcessor.AppendDocument(fileName)
                        Exit Do
                    Catch e1 As PdfIncorrectPasswordException
                        If password Is Nothing Then
                            Exit Do
                        End If
                        XtraMessageBox.Show("The password is incorrect. Please make sure that Caps Lock is not enabled.", fileName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch
                        Dim message As String = "Unable to load the PDF document because the following file is not available or it is not a valid PDF document." & Constants.vbCrLf & "{0}" & Constants.vbCrLf & "Please ensure that the application can access this file and that it is valid, or specify a different file."
                        XtraMessageBox.Show(String.Format(message, fileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Do
                    End Try
                Loop
                Try
                    Using stream As New MemoryStream()
                        documentProcessor.SaveDocument(stream)
                        stream.Position = 0
                        viewer.LoadDocument(stream)
                        viewer.ScrollVertical(Integer.MaxValue)
                    End Using
                Catch
                End Try
            End If
        End Sub
        Public Sub OpenFile()
            Dim fileName As String = OpenFileDialog()
            If String.IsNullOrEmpty(fileName) Then
                Return
            End If
            Do
                Try
                    documentProcessor.LoadDocument(fileName, True)
                    Exit Do
                Catch e1 As PdfIncorrectPasswordException
                    If password Is Nothing Then
                        Exit Do
                    End If
                    XtraMessageBox.Show("The password is incorrect. Please make sure that Caps Lock is not enabled.", fileName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch
                    Dim message As String = "Unable to load the PDF document because the following file is not available or it is not a valid PDF document." & Constants.vbCrLf & "{0}" & Constants.vbCrLf & "Please ensure that the application can access this file and that it is valid, or specify a different file."
                    XtraMessageBox.Show(String.Format(message, fileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End Try
            Loop
            If (Not String.IsNullOrEmpty(fileName)) Then
                viewer.LoadDocument(fileName)
            End If
        End Sub

        Private Function OpenFileDialog() As String
            Using openDialog As New OpenFileDialog()
                openDialog.Filter = "PDF Files (*.pdf)|*.pdf"
                openDialog.RestoreDirectory = True
                If openDialog.ShowDialog() = DialogResult.OK Then
                    Return openDialog.FileName
                End If
                Return Nothing
            End Using
        End Function
        Public Function SaveFileDialog() As String
            Using saveDialog As New SaveFileDialog()
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
                saveDialog.RestoreDirectory = True
                If saveDialog.ShowDialog() = DialogResult.OK Then
                    Return saveDialog.FileName
                End If
                Return Nothing
            End Using
        End Function
        Private Sub OnDocumentServerPasswordRequested(ByVal sender As Object, ByVal e As PdfPasswordRequestedEventArgs)
            'Using form As New PasswordForm(Path.GetFileName(e.FileName))
            '    form.StartPosition = FormStartPosition.CenterParent
            '    If form.ShowDialog() = DialogResult.OK Then
            '        password = New SecureString()
            '        For Each c As Char In form.Password
            '            password.AppendChar(c)
            '        Next c
            '        e.Password = password
            '    Else
            '        password = Nothing
            '    End If
            'End Using
        End Sub
        Private Sub OnViewerPasswordRequested(ByVal sender As Object, ByVal e As PdfPasswordRequestedEventArgs)
            e.Password = password
        End Sub
    End Class
End Namespace
