using Microsoft.AspNetCore.Builder;

namespace CatalogService.Application.Exceptions
{
    public static class ConfigureExeptionMiddleware
    {
        public static void ConfigureExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
