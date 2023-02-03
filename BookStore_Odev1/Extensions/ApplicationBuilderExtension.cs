using BookStore.MiddleWares;
using Microsoft.AspNetCore.Diagnostics;

namespace BookStore.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<GlobalExceptionHandling>();
        }
         
    }
}
