using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace BlsDistributedChat.Infra.EventBus.RabbitMq
{
    public abstract class BaseRabbitMqConsumer : IHostedService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<object> _logger;

        protected BaseRabbitMqConsumer(IRabbitMqConnection RabbitMqConnection, ILogger<object> logger)
        {
            _connection = RabbitMqConnection.Connection;
            _channel = _connection.CreateModel();
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
