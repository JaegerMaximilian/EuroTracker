using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Euro24Tracker.Data;
using System.Text.Json.Serialization;

namespace Euro24Tracker.Controllers
{
    public class StartupUrlPrinter
    {
        private readonly IServer _server;

        public StartupUrlPrinter(IServer server)
        {
            _server = server;
        }

        public void PrintUrls()
        {
            var addresses = _server.Features.Get<IServerAddressesFeature>().Addresses;
            foreach (var address in addresses)
            {
                Console.WriteLine($"Application is running on: {address}");
            }
        }
    }
}
