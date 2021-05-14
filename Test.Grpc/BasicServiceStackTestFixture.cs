using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ServiceStack;
using Test.Grpc.ServerEvents;

namespace Test.Grpc
{
    public class BasicServiceStackTestFixture : IDisposable
    {
        public string BaseUri = "http://localhost:2000/";
        public string BaseGrpcUri = "http://localhost:2001/";
        public  ServiceStackHost AppHost { get; set; }

        public BasicServiceStackTestFixture()
        {
            AppHost ??= new TestAppHost("Testing", new[] {typeof(ServerEventsServices).Assembly})
                .Init()
                .Start(BaseUri);
        }
        public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);
        public ServerEventsClient CreateServerEventsClient(string[] channels) => new(BaseUri, channels);
        public ServerEventsClient CreateServerEventsClient() => new(BaseUri);
        public GrpcServiceClient CreateGrpcClient() => new GrpcServiceClient(BaseGrpcUri);

        public void Dispose()
        {
            if (this.AppHost != null)
            {
                this.AppHost.Dispose();
            }
        }
    }
}