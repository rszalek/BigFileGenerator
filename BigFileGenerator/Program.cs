using BigFileGenerator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(serviceCollection =>
    {
        serviceCollection.AddTransient<App>();
    })
    .Build();

// Getting App service to run
var myApp = ActivatorUtilities.CreateInstance<App>(host.Services);
await myApp.Run();

await host.RunAsync();