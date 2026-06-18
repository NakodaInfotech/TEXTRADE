<%@ WebHandler Language="C#" Class="UploadDoc" %>

using System;
using System.IO;
using System.Web;

public class UploadDoc : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            if (context.Request.Files.Count == 0)
            {
                context.Response.StatusCode = 400;
                context.Response.Write("No file received");
                return;
            }

            HttpPostedFile file = context.Request.Files[0];

            if (file.ContentLength == 0)
            {
                context.Response.StatusCode = 400;
                context.Response.Write("Empty file");
                return;
            }

            // The filename field contains relative path like:
            // Documents/SALEINVOICE/UPLOADDOCS/1001_1_invoice.pdf
            string relativePath = file.FileName
                .Replace("/", "\\")
                .TrimStart('\\');

            // Build full physical path on server
            string savePath = context.Server.MapPath("~/" + relativePath.Replace("\\", "/"));

            // Create directory if it doesn't exist
            string dir = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            // Save the file
            file.SaveAs(savePath);

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = 200;
            context.Response.Write("OK:" + relativePath);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/plain";
            context.Response.Write("Error: " + ex.Message);
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}