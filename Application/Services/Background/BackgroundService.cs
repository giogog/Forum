using Contracts;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class StatusBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public StatusBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repositoryManager = scope.ServiceProvider.GetRequiredService<IRepositoryManager>();
                var topics = await repositoryManager.TopicRepository.GetAllTopicAsyncWithConditionAsync(t => t.Status == Status.Active);

                foreach (var topic in topics)
                {
                    if ((DateTime.Now - topic.Created).TotalDays >= 7)
                    {
                        //topic.Status = Status.Inactive;
                        //repositoryManager.TopicRepository.UpdateTopicAsync(topic);
                    }
                }

                await repositoryManager.SaveAsync();
            }

            // Wait for 24 hours
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}
