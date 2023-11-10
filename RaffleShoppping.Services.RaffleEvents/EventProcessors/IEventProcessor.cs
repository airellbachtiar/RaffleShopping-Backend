namespace RaffleShoppping.Services.RaffleEvents.EventProcessors
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}