using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RaffleShoppping.Services.RaffleEvents.EventProcessors;
using RaffleShoppping.Services.RaffleEvents.Models;

namespace RaffleShoppping.Services.RaffleEvents.ServiceBus
{
    public class RaffleEventServiceBusReceiver : BackgroundService
    {
        private readonly IEventProcessor _eventProcessor;
        private readonly ServiceBusProcessor _processor;

        public RaffleEventServiceBusReceiver(IEventProcessor eventProcessor, IOptions<AzureServiceBusSettings> azureServiceBusSettings)
        {
            _eventProcessor = eventProcessor;
            _processor = azureServiceBusSettings.Value.ServiceBusClient.CreateProcessor(azureServiceBusSettings.Value.QueueName, new ServiceBusProcessorOptions());
        }

        private async Task ProcessMessageHandler(ProcessMessageEventArgs args)
        {
            try
            {
                var messageBody = args.Message.Body.ToString();

                _eventProcessor.ProcessEvent(messageBody);

                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception)
            {
                await args.AbandonMessageAsync(args.Message);
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += ProcessMessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
            await _processor.StartProcessingAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }

            await _processor.StopProcessingAsync(stoppingToken);
        }

        #pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public override async void Dispose()
        #pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
            if (_processor.IsProcessing)
            {
                try
                {
                    await _processor.StopProcessingAsync();
                    await _processor.DisposeAsync();
                }
                catch
                {
                    throw;
                }
            }

            base.Dispose();
        }
    }
}
