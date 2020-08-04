using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcNetCore
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly ExampleService.ExampleServiceClient exampleServiceClient;

        public GreeterService(ILogger<GreeterService> logger, Example.ExampleService.ExampleServiceClient exampleServiceClient)
        {
            _logger = logger;
            this.exampleServiceClient = exampleServiceClient;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }


        public override async Task SayHelloStream(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            await Task.Delay(1000);
            await responseStream.WriteAsync(new HelloReply { Message = "Response 1" });
            await Task.Delay(1000);
            await responseStream.WriteAsync(new HelloReply { Message = "Response 2" });
            await Task.Delay(1000);
            await responseStream.WriteAsync(new HelloReply { Message = "Response 3" });
        }


    }
}
