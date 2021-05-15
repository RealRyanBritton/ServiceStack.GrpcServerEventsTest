using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Text;
using Test.Grpc.ServerEvents.Messages;
using Xunit;

namespace Test.Grpc
{
    [Collection("Servicestack")]
    public class GrpcServerEventsTests
    {
        private readonly BasicServiceStackTestFixture _fixture;

        public GrpcServerEventsTests(BasicServiceStackTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Can_send_message_to_Server()
        {
            var client = _fixture.CreateClient();

            var response = client.Post(new SendMessage() {Message = "Hello"});

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Can_receieve_notification_from_server()
        {
            var client = _fixture.CreateGrpcClient();
            var stream = client.StreamAsync(new StreamServerEvents
            {
                Channels = new[] {"send-messages"}
            });

            var postTask = await client.PostAsync(new SendMessage() {Message = "Hello", Channel = "send-messages"});

            await foreach (var msg in stream)
            {
                if (msg.Op != "cmd")
                {
                    $"EVENT {msg.Selector} [{msg.Channel}]: #{msg.UserId} {msg.DisplayName}".Print();
                    var notification = msg.Json.FromJson<SendMessage>();
                    Assert.Equal("Hello", notification.Message);
                    break;
                }
                else
                { 
                    var command = $"EVENT {msg.Selector} [{msg.Channel}]: #{msg.UserId} {msg.DisplayName}";
                    command.Print();
                }
            }
        }
    }
}