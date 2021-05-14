using System.Runtime.Serialization;

namespace Test.Grpc.ServerEvents.Messages
{
    
    [DataContract]
    public class ServerEventsNotification 
    {
        [DataMember(Order = 1)]
        public string Messsage { get; set; }
    }
}