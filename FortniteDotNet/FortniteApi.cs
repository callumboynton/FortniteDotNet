using FortniteDotNet.Services;

namespace FortniteDotNet
{
    public class FortniteApi
    {
        public AccountService AccountService { get; }
        public ChannelsService ChannelsService { get; }
        public EventsService EventsService { get; }
        public FortniteService FortniteService { get; }
        
        public FortniteApi()
        {
            AccountService = new();
            ChannelsService = new();
            EventsService = new();
            FortniteService = new();
        }
    }
}