using Microsoft.AspNetCore.Http.Extensions;
using MVCNet6.Configurations;

namespace MVCNet6.Modules
{
    public class RedirectConfiguration
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public RedirectConfiguration(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            string newUrl = RedirectUrls(context.Request.Path);

            if (!string.IsNullOrWhiteSpace(newUrl))
            {
                context.Response.Redirect(newUrl);
            }

            await _next.Invoke(context);
        }

        private string RedirectUrls(string rawUrl)
        {
            var redirectSection = _configuration.GetSection("Redirects").Get<List<Redirect>>();

            foreach (Redirect redirect in redirectSection)
            {
                if (redirect.Old == rawUrl)
                {
                    return redirect.New;
                }
            }

            return string.Empty;
        }
    }

    public static class RedirectConfigurationExtensions
    {
        public static IApplicationBuilder UseRedirectConfiguration(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RedirectConfiguration>();
        }
    }
}
