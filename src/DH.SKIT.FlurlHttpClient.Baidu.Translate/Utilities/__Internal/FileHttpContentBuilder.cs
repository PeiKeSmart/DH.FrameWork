using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace SKIT.FlurlHttpClient.Baidu.Translate.Utilities
{
    internal static class FileHttpContentBuilder
    {
        public static MultipartFormDataContent Build(string fileName, byte[] fileBytes, string fileContentType, string formDataName)
        {
            return Build(fileName: fileName, fileBytes: fileBytes, fileContentType: fileContentType, formDataName: formDataName, (_) => { });
        }

        public static MultipartFormDataContent Build(string fileName, byte[] fileBytes, string fileContentType, string formDataName, Action<HttpContent> configureFileHttpContent)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
            if (formDataName == null) throw new ArgumentNullException(nameof(formDataName));
            if (configureFileHttpContent == null) throw new ArgumentNullException(nameof(configureFileHttpContent));

            fileName = fileName.Replace("\"", "");
            fileBytes = fileBytes ?? Array.Empty<byte>();
            fileContentType = string.IsNullOrEmpty(fileContentType) ? "application/octet-stream" : fileContentType;
            formDataName = formDataName.Replace("\"", "");

            ByteArrayContent fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(fileContentType);
            configureFileHttpContent(fileContent);

            string boundary = "--BOUNDARY--" + DateTimeOffset.Now.Ticks.ToString("x");
            MultipartFormDataContent httpContent = new MultipartFormDataContent(boundary);
            httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse($"multipart/form-data; boundary={boundary}");
            httpContent.Add(fileContent, $"\"{formDataName}\"", $"\"{HttpUtility.UrlEncode(fileName)}\"");
            return httpContent;
        }
    }
}
