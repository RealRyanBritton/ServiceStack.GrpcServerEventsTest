using System.Runtime.Serialization;
using ServiceStack;

namespace Test.Grpc.ServerEvents.Messages
{
    [Route("/send-message", verbs: "POST")]
    [DataContract]
    public class SendMessage : IReturn<SendMessageResponse>
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
        
        [DataMember(Order = 2)]
        public string Channel { get; set; }
    }
    
    [DataContract]
    public class SendMessageResponse : IHasResponseStatus
    {
        [DataMember(Order = 1)]
        public ResponseStatus ResponseStatus { get; set; }
    }
}