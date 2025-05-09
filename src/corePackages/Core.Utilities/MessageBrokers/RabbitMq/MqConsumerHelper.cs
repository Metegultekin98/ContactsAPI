using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Core.Utilities.MessageBrokers.RabbitMq;

public class MqConsumerHelper : IMessageConsumer, IDisposable
    {
        private readonly MessageBrokerOptions _brokerOptions;
        private readonly IConnection _connection;
        private IModel? _channel;

        public MqConsumerHelper(IOptions<MessageBrokerOptions> brokerOptions)
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

        public Task<string?> GetQueueMessageAsync(string queueName)
        {
            return Task.Run(() =>
            {
                try
                {
                    _channel = _connection.CreateModel();

                    _channel.QueueDeclare(
                        queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var result = _channel.BasicGet(queueName, autoAck: false);

                    if (result == null)
                        return null;

                    var message = Encoding.UTF8.GetString(result.Body.ToArray());

                    var queueMessageWithTag = new QueueMessageWithTag
                    {
                        Message = message,
                        DeliveryTag = result.DeliveryTag
                    };

                    return JsonConvert.SerializeObject(queueMessageWithTag);
                }
                catch (Exception ex)
                {
                    throw new BusinessException($"[RabbitMQ - Consume] Hata oluştu: {ex.Message}");
                }
            });
        }

        public void AcknowledgeMessage(ulong deliveryTag)
        {
            try
            {
                _channel?.BasicAck(deliveryTag, multiple: false);
                CleanupChannel();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"[RabbitMQ - Ack] Hata oluştu: {ex.Message}");
            }
        }

        public void RejectMessage(ulong deliveryTag)
        {
            try
            {
                _channel?.BasicNack(deliveryTag, multiple: false, requeue: true);
                CleanupChannel();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"[RabbitMQ - Reject] Hata oluştu: {ex.Message}");
            }
        }

        private void CleanupChannel()
        {
            if (_channel?.IsOpen == true)
                _channel.Close();

            _channel?.Dispose();
            _channel = null;
        }

        public void Dispose()
        {
            CleanupChannel();

            if (_connection?.IsOpen == true)
                _connection.Close();

            _connection?.Dispose();
        }
    }