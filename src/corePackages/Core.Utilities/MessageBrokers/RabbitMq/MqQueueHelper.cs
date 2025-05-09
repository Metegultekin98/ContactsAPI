using System.Text;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Core.Utilities.MessageBrokers.RabbitMq;

public class MqQueueHelper : IMessageBrokerHelper, IDisposable
    {
        private readonly MessageBrokerOptions _brokerOptions;
        private readonly IConnection _connection;

        public MqQueueHelper(IOptions<MessageBrokerOptions> brokerOptions)
        {
            _brokerOptions = brokerOptions.Value;

            var factory = new ConnectionFactory
            {
                HostName = _brokerOptions.HostName,
                UserName = _brokerOptions.UserName,
                Password = _brokerOptions.Password,
                AutomaticRecoveryEnabled = true,
                RequestedHeartbeat = TimeSpan.FromSeconds(60),
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            try
            {
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"RabbitMQ bağlantısı kurulamadı: {ex.Message}");
            }
        }

        public Task QueueMessageAsync(string queueName, object messageObject)
        {
            try
            {
                using var channel = _connection.CreateModel();

                channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = JsonConvert.SerializeObject(messageObject);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: queueName,
                    basicProperties: null,
                    body: body
                );

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"[RabbitMQ - Publish] Mesaj kuyruğa yazılamadı: {ex.Message}");
            }
        }


        public void Dispose()
        {
            if (_connection != null && _connection.IsOpen)
                _connection.Close();

            _connection?.Dispose();
        }
    }