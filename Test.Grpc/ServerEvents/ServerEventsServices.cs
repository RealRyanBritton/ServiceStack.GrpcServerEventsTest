using ServiceStack;
using Test.Grpc.ServerEvents.Messages;

namespace Test.Grpc.ServerEvents
{
    public class ServerEventsServices : Service
    {
        private readonly IServerEvents _serverEvents;

        public ServerEventsServices(IServerEvents serverEvents)
        {
            _serverEvents = serverEvents;
        }
        
        public object Any(SendMessage request)
        {
            if (string.IsNullOrWhiteSpace(request.Channel))
            {
                _serverEvents.NotifyAll(request.Message);
            }
            else
            {
                _serverEvents.NotifyChannel(request.Channel, request.Message);
            }
            
            return new SendMessageResponse();
        }
    }
}