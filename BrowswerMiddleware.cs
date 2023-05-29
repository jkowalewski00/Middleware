using System.Globalization;

namespace Middleware
{
    public class BrowswerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public BrowswerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"].ToString();

            if(userAgent.Contains("MSIE") || userAgent.Contains("Chromium") || userAgent.Contains("Edg"))
            {
                context.Response.Redirect("https://www.mozilla.org/pl/firefox/new/");
                return;
            }

            await _requestDelegate(context);
        }
    }

    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseBrowswerMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BrowswerMiddleware>();
        }
    }
}
