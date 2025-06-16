using FleetTrackerSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace FleetTrackerSystem.API.Middlewares
{
    public class TransactionMiddleWare
    {
        FeetTrackerDbContext _dbContext;
        ILogger<TransactionMiddleWare> _logger;


        public TransactionMiddleWare(FeetTrackerDbContext context,
            ILogger<TransactionMiddleWare> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            
            IDbContextTransaction transaction = null;

            try
            {
                transaction = await _dbContext.Database.BeginTransactionAsync();

                next(httpContext);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                _logger.LogError("");

                throw;
            }
        }
    }
}
