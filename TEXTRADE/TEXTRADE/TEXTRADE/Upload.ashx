<%@ WebHandler Language="VB" Class="Upload" %>
Imports System.Web
Public Class Upload
    Implements IHttpHandler

    ''' <summary>
    '''  You will need to configure this handler in the Web.config file of your 
    '''  web and register it with IIS before being able to use it. For more information
    '''  see the following link: https://go.microsoft.com/?linkid=8101007
    ''' </summary>
#Region "IHttpHandler Members"

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            ' Return false in case your Managed Handler cannot be reused for another request.
            ' Usually this would be false in case you have some state information preserved per request.
            Return False
        End Get
    End Property

    Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"

        Try
            Dim file As HttpPostedFile = context.Request.Files("file")
            If file Is Nothing OrElse file.ContentLength = 0 Then
                context.Response.Write("No file uploaded.")
                Return
            End If

            ' Save folder path - make sure this folder path matches your IIS setup and has write permissions
            Dim savePath As String = context.Server.MapPath("~/images/")

            If Not IO.Directory.Exists(savePath) Then
                IO.Directory.CreateDirectory(savePath)
            End If

            Dim fileName As String = IO.Path.GetFileName(file.FileName)
            Dim fullPath As String = IO.Path.Combine(savePath, fileName)

            file.SaveAs(fullPath)

            context.Response.Write("File uploaded successfully: " & fileName)
        Catch ex As Exception
            context.Response.Write("Upload failed: " & ex.Message)
        End Try
    End Sub

#End Region

End Class
