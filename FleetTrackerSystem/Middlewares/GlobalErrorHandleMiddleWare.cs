using FleetTrackerSystem.Application.ViewModels;
using FleetTrackerSystem.Domain.Enums;

namespace FleetTrackerSystem.API.Middlewares
{
    public class GlobalErrorHandleMiddleWare:IMiddleware
    {
        ILogger<GlobalErrorHandleMiddleWare> _logger;

        public GlobalErrorHandleMiddleWare(ILogger<GlobalErrorHandleMiddleWare> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                ErrorResponseViewModel errorResponse = new ErrorResponseViewModel { ErrorCode = ErrorCode.UnExcepectedError };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
