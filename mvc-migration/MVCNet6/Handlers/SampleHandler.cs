namespace MVCNet6.Handlers
{
    public class SampleHandler
    {
        public SampleHandler(RequestDelegate next)
        {
                
        }

        public async Task Invoke(HttpContext context)
        {
            string response = GenerateResponse(context);

            context.Response.ContentType = GetContentType();
            await context.Response.WriteAsync(response);
        }

        private string GenerateResponse(HttpContext context)
        {
            string title = context.Request.Query["title"];
            return string.Format("Title of the report: {0}", title);
        }

        private string GetContentType()
        {
            return "text/plain";
        }
    }

    public static class SampleHandlerExtensions
    {
        public static IApplicationBuilder UseSampleHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SampleHandler>();
        }
    }
}
