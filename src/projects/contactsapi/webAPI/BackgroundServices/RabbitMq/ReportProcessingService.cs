using Core.Domain.ComplexTypes;
using Core.Domain.Entities;
using Core.Utilities.MessageBrokers;
using Core.Utilities.MessageBrokers.RabbitMq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using webAPI.Application.Services.ReportsService;

namespace ContactsAPI.BackgroundServices.RabbitMq;

public class ReportProcessingService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    
    public ReportProcessingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var reportService = scope.ServiceProvider.GetRequiredService<IReportService>();
                var consumerHelper = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();


                string reportId = "";
                QueueMessageWithTag? messageObj = null;

                try
                {
                    var rawMessage = await TryGetQueueMessageWithRetryAsync(consumerHelper, "report-processing-queue");

                    if (string.IsNullOrWhiteSpace(rawMessage))
                    {
                        await Task.Delay(500, stoppingToken);
                        continue;
                    }

                    messageObj = JsonConvert.DeserializeObject<QueueMessageWithTag>(rawMessage);

                    if (messageObj == null || string.IsNullOrWhiteSpace(messageObj.Message))
                    {
                        await Task.Delay(500, stoppingToken);
                        continue;
                    }

                    string decodedMessage = JsonConvert.DeserializeObject<string>(messageObj.Message);

                    var jsonObject = JObject.Parse(decodedMessage);
                    reportId = jsonObject["ReportId"]?.Value<string>() ?? "";

                    Console.WriteLine($"Rapor işleniyor - Rapor ID: {reportId}");

                    var reportIdGuid = Guid.Parse(reportId);
                    
                    var report = await reportService.GetAsync(x => x.Id == reportIdGuid);
                    if (report == null)
                    {
                        Console.WriteLine($"Rapor bulunamadı - Rapor ID: {reportId}");
                        await TryFailStatusUpdate(reportService, reportIdGuid);
                        consumerHelper.RejectMessage(messageObj.DeliveryTag);
                        continue;
                    }
                    
                    await TrySuccessStatusUpdate(reportService, reportIdGuid);

                    consumerHelper.AcknowledgeMessage(messageObj.DeliveryTag);
                    Console.WriteLine($"Rapor başarıyla işlendi - Rapor ID: {reportId}");
                }
                catch (JsonReaderException jsonEx)
                {
                    Console.WriteLine($"JSON ayrıştırma hatası: {jsonEx.Message}");
                    if (jsonEx.InnerException != null)
                        Console.WriteLine($"Inner Exception: {jsonEx.InnerException.Message}");
                    var reportIdGuid = Guid.Parse(reportId);
                    await TryFailStatusUpdate(reportService, reportIdGuid);

                    if (messageObj != null)
                        consumerHelper.RejectMessage(messageObj.DeliveryTag);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Genel hata oluştu: {ex.Message}");
                    if (ex.InnerException != null)
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");

                    var reportIdGuid = Guid.Parse(reportId);
                    await TryFailStatusUpdate(reportService, reportIdGuid);

                    if (messageObj != null)
                        consumerHelper.RejectMessage(messageObj.DeliveryTag);
                }
                finally
                {
                    await Task.Delay(500, stoppingToken);
                }
            }
        }

        private async Task TryFailStatusUpdate(IReportService reportService, Guid reportId)
        {
            try
            {
                Report? report = await reportService.GetAsync(x => x.Id == reportId, cancellationToken: default);

                if (report is not null)
                {
                    report.ReportStatu = Enums.ReportStatu.Failed;
                    await reportService.UpdateAsync(report);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata durumunda rapor status güncellenemedi: {ex.Message}");
            }
        }
        
        private async Task TrySuccessStatusUpdate(IReportService reportService, Guid reportId)
        {
            try
            {
                Report? report = await reportService.GetAsync(x => x.Id == reportId, cancellationToken: default);

                if (report is not null)
                {
                    report.ReportStatu = Enums.ReportStatu.Success;
                    await reportService.UpdateAsync(report);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata durumunda rapor status güncellenemedi: {ex.Message}");
            }
        }

        private async Task<string?> TryGetQueueMessageWithRetryAsync(IMessageConsumer? consumerHelper, string queueName, int retryCount = 3, int delayMs = 2000)
        {
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    return await consumerHelper.GetQueueMessageAsync(queueName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[RabbitMQ] Kuyruktan mesaj alınırken hata oluştu (Deneme {i + 1}/{retryCount}): Message: {ex.Message}, Inner Exception Message : {ex.InnerException?.Message}");
                    if (i == retryCount - 1)
                        throw;

                    await Task.Delay(delayMs);
                }
            }
            return null;
        }
}