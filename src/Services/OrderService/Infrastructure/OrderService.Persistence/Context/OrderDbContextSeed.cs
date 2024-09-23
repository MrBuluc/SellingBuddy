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
        public async Task SeedAsync(OrderDbContext context, ILogger<OrderDbContext> logger)
        {
            await CreatePolicy(logger, nameof(OrderDbContextSeed))
                .ExecuteAsync(async () =>
                {
                    string contentRootPath = "Seeding/Setup";

                    using (context)
                    {
                        context.Database.Migrate();

                        if (!context.Statuses.Any())
                        {
                            context.Statuses.AddRange(GetStatusFromFile(contentRootPath));
                        }

                        await context.SaveChangesAsync();
                    }
                });
        }

        private static AsyncRetryPolicy CreatePolicy(ILogger<OrderDbContext> logger, string prefix, int retries = 3) => Policy.Handle<SqlException>()
            .WaitAndRetryAsync(retryCount: retries, sleepDurationProvider: retry => TimeSpan.FromSeconds(5), onRetry: (exception, timeSpan, retry, ctx) =>
            {
                logger.LogWarning(exception, "[{prefix}} Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
            });

        private static IEnumerable<Status> GetStatusFromFile(string contentRootPath)
        {
            string fileName = Path.Combine(contentRootPath, "Status.txt");

            if (!File.Exists(fileName))
            {
                return GetPredefinedStatus();
            }

            int id = 1;
            return File.ReadAllLines(fileName)
                .Select(s => new Status
                {
                    Id = id++,
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
