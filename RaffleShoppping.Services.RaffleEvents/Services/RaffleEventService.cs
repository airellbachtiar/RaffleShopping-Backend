using Azure.Identity;
using Azure.Messaging.ServiceBus;

namespace RaffleShoppping.Services.RaffleEvents.Services
{
    public class RaffleEventService
    {
        public RaffleEventService() { }

        public async Task ReceiveMessageAsync()
        {
            ServiceBusClient client;
            ServiceBusProcessor processor;

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient("raffleshopping.servicebus.windows.net",
                new DefaultAzureCredential(), clientOptions);

            processor = client.CreateProcessor("catalogs", new ServiceBusProcessorOptions());

            try
            {
                processor.ProcessMessageAsync += MessageHandler;

                processor.ProcessErrorAsync += ErrorHandler;

                await processor.StartProcessingAsync();

                await Task.Delay(TimeSpan.FromSeconds(30));

                await processor.StopProcessingAsync();
            }
            finally
            {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }

            async Task MessageHandler(ProcessMessageEventArgs args)
            {
                string body = args.Message.Body.ToString();
                Console.WriteLine($"Received: {body}");

                await args.CompleteMessageAsync(args.Message);
            }

            Task ErrorHandler(ProcessErrorEventArgs args)
            {
                Console.WriteLine(args.Exception.ToString());
                return Task.CompletedTask;
            }
        }
    }
}
