using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;

namespace GrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
                {
                    await GrpcClient();
                }).GetAwaiter().GetResult();
        }

        public static async Task GrpcClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new GrpcNetCore.Greeter.GreeterClient(channel);

            var response = await client.SayHelloAsync(
                new GrpcNetCore.HelloRequest { Name = "World" });
            Console.WriteLine(response.Message);

            var streamResponse = client.SayHelloStream(new GrpcNetCore.HelloRequest { Name = "Stream Example" });
            while (await streamResponse.ResponseStream.MoveNext())
            {
                Console.WriteLine(streamResponse.ResponseStream.Current.Message);
            }
            Console.WriteLine("End of response");
            Console.ReadLine();
        }
    }
}
