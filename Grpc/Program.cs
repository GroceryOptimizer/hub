using System;
using System.Threading.Tasks;

using Grpc.Net.Client;

using VendorProto;  // The namespace we specified in the proto file

namespace HubClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The server is listening on localhost:50051
            using var channel = GrpcChannel.ForAddress("http://localhost:50051");

            // The generated client is VendorService.VendorServiceClient
            var client = new VendorService.VendorServiceClient(channel);

            Console.WriteLine("Sending message to Go Vendor service...");
            var request = new SendMessageRequest { Message = "Hello from C# client!" };

            // Perform the gRPC call
            var reply = await client.SendMessageAsync(request);

            Console.WriteLine("Received reply: " + reply.Reply);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
