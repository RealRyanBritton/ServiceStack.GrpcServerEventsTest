using System.Reflection;
using Funq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using ServiceStack;

namespace Test.Grpc
{
    public class TestAppHost : AppSelfHostBase
    {
        public override void Configure(Container container)
        {
            this.Plugins.Add(new GrpcFeature(this.GetApp()));
            this.Plugins.Add(new ServerEventsFeature());
        }

        public override void Configure(IServiceCollection services)
        {
            services.AddServiceStackGrpc();
        }
        
        public override void ConfigureKestrel(KestrelServerOptions options)
        {
            options.ListenLocalhost(2001, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
            options.ListenLocalhost(2000, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1;
            });
        }
        
        public TestAppHost(string serviceName, Assembly[] assembliesWithServices) : base(serviceName, assembliesWithServices)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
        }
    }
}