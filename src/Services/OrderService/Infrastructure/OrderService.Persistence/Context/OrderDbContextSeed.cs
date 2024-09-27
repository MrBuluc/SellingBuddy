using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderService.Domain.AggregateModels.OrderAggregate;
using Polly;
using Polly.Retry;

namespace OrderService.Persistence.Context
{
    public class OrderDbContextSeed
    {
        public async Task SeedAsync(OrderDbContext context, ILogger<OrderDbContext> logger, string contentRootPath)
        {
            await CreatePolicy(logger, nameof(OrderDbContextSeed))
                .ExecuteAsync(() => ProcessSeeding(context, Path.Combine(Directory.GetParent(contentRootPath)!.Parent!.FullName, "Infrastructure", "OrderService.Persistence", "Seeding", "Setup")));
        }

        private static AsyncRetryPolicy CreatePolicy(ILogger<OrderDbContext> logger, string prefix, int retries = 3) => Policy.Handle<SqlException>()
            .WaitAndRetryAsync(retryCount: retries, sleepDurationProvider: retry => TimeSpan.FromSeconds(5), onRetry: (exception, timeSpan, retry, ctx) =>
            {
                logger.LogWarning(exception, "[{prefix}} Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
            });

        private static async Task ProcessSeeding(OrderDbContext context, string setupDirPath)
        {
            using (context)
            {
                if (!context.Statuses.Any())
                {
                    await context.Statuses.AddRangeAsync(GetStatusFromFile(setupDirPath));
                    await context.SaveChangesAsync(CancellationToken.None);
                }
            }
        }

        private static IEnumerable<Status> GetStatusFromFile(string contentPath)
        {
            string fileName = Path.Combine(contentPath, "Status.txt");

            if (!File.Exists(fileName))
            {
                return GetPredefinedStatus();
            }

            return File.ReadAllLines(fileName)
                .Select(s => new Status
                {
                    Name = s
                }).Where(s => s is not null);
        }

        private static List<Status> GetPredefinedStatus() => new()
        {
            Status.Submitted,
            Status.AwaitingValidation,
            Status.StockConfirmed,
            Status.Paid,
            Status.Shipped,
            Status.Cancelled
        };
    }
}
