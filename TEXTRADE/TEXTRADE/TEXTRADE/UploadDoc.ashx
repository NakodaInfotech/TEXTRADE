Public Class UploadDoc :   IHttpHandler
{
    Public void ProcessRequest(HttpContext context)
    {
        Try
        {
            If (context.Request.Files.Count == 0)
            {
                context.Response.Write("No file received");
                Return;
            }

            HttpPostedFile file = context.Request.Files[0];
            
            // The filename contains the relative path Documents/SALEINVOICE/UPLOADDOCS/filename.pdf
            String relativePath = file.FileName.Replace("/", "\\");
            
            // Build full server path
            String savePath = context.Server.MapPath("~/" + relativePath);
            
            // Create directory if Not exists
            String dir = Path.GetDirectoryName(savePath);
            If (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            
            file.SaveAs(savePath);
            
            context.Response.ContentType = "text/plain";
            context.Response.Write("OK:" + relativePath);
        }
        Catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.Write("Error: " + ex.Message);
        }
    }

    Public bool IsReusable => False;
}
