using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System;
using System.Reactive.Linq;
using System.Linq;

namespace DXUploadXAFBlazor.Module.Blazor.Middlewares;

public class UploadFileMiddleware
{
    private static readonly ISubject<(string name, byte[] bytes, string editor)> FormFileSubject =
        Subject.Synchronize(new Subject<(string name, byte[] bytes, string editor)>());

    public static IObservable<(string name, byte[] bytes, string editor)> FormFile => FormFileSubject.AsObservable();

    private readonly RequestDelegate _next;

    public UploadFileMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private static async Task<byte[]> StreamToByteArrayAsync(Stream input)
    {
        using MemoryStream ms = new MemoryStream();
        await input.CopyToAsync(ms);
        return ms.ToArray();
    }

    public async Task Invoke(HttpContext context)
    {
        string requestPath = context.Request.Path.Value.TrimStart('/');
        if (requestPath.StartsWith("api/Upload/UploadFile"))
        {
            if (!context.Request.HasFormContentType || !context.Request.Form.Files.Any())
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("No files uploaded.");
                return;
            }

            var formFile = context.Request.Form.Files.First();
            string editor = context.Request.Query["Editor"];

            var fileBytes = await StreamToByteArrayAsync(formFile.OpenReadStream());
            FormFileSubject.OnNext((formFile.FileName, fileBytes, editor));

            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("File uploaded successfully.");
        }
        else
        {
            await _next(context);
        }
    }
}
