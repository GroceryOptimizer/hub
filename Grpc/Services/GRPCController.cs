namespace Grpc.Services
{
    public class GRPCController
    {



        // 1. skapa alla store instanser (clienter som vi får via connectorservice) och lagra i en lista
        // 2. ha en metod som vi kan kalla på via HubController.
        // 3. metoden ska skicka inventory anrop till alla stores
        // 4. metoden ska ta alla svar, parsa / sätta ihop det till en lista av VendorVisits inkl vendor id
        // 5. returna VendorVisit till HubController som sedan får returna det till frontend.

    }
}
