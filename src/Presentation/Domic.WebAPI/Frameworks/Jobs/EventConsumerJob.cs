using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.WebAPI.Frameworks.Jobs;

public class EventConsumerJob : IHostedService
{
    private readonly IMessageBroker _messageBroker;

    public EventConsumerJob(IMessageBroker messageBroker) => _messageBroker = messageBroker;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _messageBroker.NameOfAction  = nameof(EventConsumerJob);
        _messageBroker.NameOfService = Service.AuthService;
        
        _messageBroker.Subscribe("");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}