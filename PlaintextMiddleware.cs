using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApp
{
    public class PlaintextMiddleware
    {
        private static readonly byte[] HelloWorldPayload = Encoding.UTF8.GetBytes("Hello, World!");

        private readonly RequestDelegate _next;

        public PlaintextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            return WriteResponse(httpContext.Response);
        }

        public static Task WriteResponse(HttpResponse response)
        {
            var payloadLength = HelloWorldPayload.Length;
            response.StatusCode = 200;
            response.ContentType = "text/plain";
            response.ContentLength = payloadLength;
            return response.Body.WriteAsync(HelloWorldPayload, 0, payloadLength);
        }
    }

    public static class PlaintextMiddlewareExtensions
    {
        public static IApplicationBuilder UsePlainText(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PlaintextMiddleware>();
        }
    }
}
